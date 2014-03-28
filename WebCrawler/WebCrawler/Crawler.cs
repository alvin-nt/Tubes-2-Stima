using HtmlAgilityPack;
using SHDocVw; // ini buat menampilkan laporan, pake link ke dll yang di-include
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace WebCrawler
{
	public static class Crawler
	{

		#region Private Fields
		private static List<Page> _pages = new List<Page>();
		private static List<string> _externalUrls = new List<string>();
		private static List<string> _otherUrls = new List<string>();
		private static List<string> _failedUrls = new List<string>();
		private static List<string> _exceptions = new List<string>();
		private static List<string> _classes = new List<string>();
		private static List<string> _keywords = new List<string>();

		private static List<string> _ignoredPages = new List<string>();

		private static StringBuilder _logBuffer = new StringBuilder();
		#endregion

		/// <summary>
		/// Mulai crawling
		/// </summary>
		public static void CrawlSite(string filename = "ignored_pages.txt")
		{
			Console.WriteLine("Beginning crawl.");

			try
			{
				FileOperators.LoadToListOfString(filename, _ignoredPages);
			}
			catch (IOException ex)
			{
				_exceptions.Add("Crawler Error: " + ex);
			}

			string configUrl = ConfigurationManager.AppSettings["url"];
			Uri rootUrl = new Uri(configUrl);
			CrawlPage(rootUrl, 10);

			StringBuilder sb = CreateReport();

			WriteReportToDisk(sb.ToString());

			OpenReportInIE();

			Console.WriteLine("finished crawl");
		}

		/// <summary>
		/// Melakukan crawling dari sebuah page -- metode DFS
		/// </summary>
		/// <param name="URL">URL yang mau di-crawl</param>
		/// <param name="depth">kedalaman maksimum, defaultnya 10</param>
		public static void CrawlPage(Uri URL, int depth = 10)
		{
			if (!PageHasBeenCrawled(URL) && depth > 0)
			{
				HtmlDocument doc = new HtmlDocument();

				// output sementara
				Console.WriteLine("Crawling " + URL);

				try
				{
					doc = GetDocument(doc, URL);

					// assign dia punya page value
					Page page = new Page();
					page.URL = URL;
					page.Text = doc.DocumentNode.OuterHtml;
					
					page.CalculateViewStateSize();
					
					// lakukan parsing
					LinkParser lParser = new LinkParser();
					lParser.ParseLinks(doc, URL);

					TextParser tParser = new TextParser();
					tParser.GetKeyWords(doc);
					page.Title = tParser.GetTitle(doc);

					// dapatkan data
					MergeList(_externalUrls, lParser.ExternalUrls);
					MergeList(_otherUrls, lParser.OtherUrls);
					MergeList(_failedUrls, lParser.BadUrls);
										 
					MergeList(_keywords, tParser.Keywords);
					
					_exceptions.AddRange(tParser.Exceptions);
					_exceptions.AddRange(lParser.Exceptions);

					// tes keyword
					page.Keywords.AddRange(tParser.Keywords);

					_pages.Add(page);

					// masukkan ke database
					DBConnection dbConn = new DBConnection();
					dbConn.AddPageToTable(page);

					_exceptions.AddRange(dbConn.Exceptions);

					foreach (var nextUrl in lParser.GoodUrls)
					{
						try
						{
							var next = FixLink(nextUrl, URL);
							Uri nextURL = new Uri(next);
							CrawlPage(nextURL, depth-1);
						}
						catch (Exception exc)
						{
							_failedUrls.Add(nextUrl +
									" (on page at url " + URL + ") - " +
									exc.Message);
						}
					}
				}

				catch (WebException ex)
				{
					_exceptions.Add("Error loading document: " + ex);
				}
				finally
				{
					/* kosong */
				}
			}
		}

		/// <summary>
		/// Mengembalikan return value berupa dokumen
		/// </summary>
		/// <param name="doc">Dokumen HTML yang sudah di-instansiasi</param>
		/// <param name="URL">URL tujuan</param>
		/// <returns>dokumen HTML yang sudah di-load</returns>
		private static HtmlDocument GetDocument(HtmlDocument doc, Uri URL)
		{
			HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(URL);
			oReq.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

			HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();

			if (resp.ContentType.StartsWith("text/html", StringComparison.InvariantCultureIgnoreCase))
			{
				try
				{
					var resultStream = resp.GetResponseStream();
					doc.Load(resultStream);
				}
				catch (WebException )
				{
					throw ;
				}
			}

			return doc;
		}

		/// <summary>
		/// Apakah page sudah di-crawl?
		/// </summary>
		/// <param name="URL">URL dari page</param>
		/// <returns></returns>
		private static bool PageHasBeenCrawled(Uri URL)
		{
			bool retval = false;

			int i = 0;
			while (i < _pages.Count && !retval)
			{
				retval = (_pages[i].URL == URL);
				i++;
			}

			return retval;
		}

		/// <summary>
		/// Merge two lists of string
		/// </summary>
		/// <param name="targetList">target List</param>
		/// <param name="sourceList">source List</param>
		public static void MergeList(List<string> targetList,
									  IEnumerable<string> sourceList)
		{
			foreach (string str in sourceList)
			{
				if (!targetList.Contains(str))
					targetList.Add(str);
			}
		}

		/// <summary>
		/// Mengubah relative link menjadi absolute
		/// </summary>
		/// <param name="relativeLink">link yang relatif</param>
		/// <param name="sourceUri">URI sumber, absolut</param>
		/// <returns>Link absolut sebagai string</returns>
		public static string FixLink(string relativeLink, Uri sourceUri)
		{
			Uri retval = new Uri(sourceUri, relativeLink);

			return retval.ToString();
		}



		#region Logging and Reporting

		private static void WriteReportToDisk(string contents)
		{
			string fileName = ConfigurationManager.AppSettings["logTextFileName"].ToString();
			FileStream fStream = null;
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
				fStream = File.Create(fileName);
			}
			else
			{
				fStream = File.OpenWrite(fileName);
			}

			using (TextWriter writer = new StreamWriter(fStream))
			{
				writer.WriteLine(contents);
				writer.Flush();
			}

			fStream.Dispose();
		}

		/// <summary>
		/// Creates a report out of the data gathered.
		/// </summary>
		/// <returns></returns>
		private static StringBuilder CreateReport()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<html><head><title>Crawl Report</title><style>");
			sb.Append("table { border: solid 3px black; border-collapse: collapse; }");
			sb.Append("table tr th { font-weight: bold; padding: 3px; padding-left: 10px; padding-right: 10px; }");
			sb.Append("table tr td { border: solid 1px black; padding: 3px;}");
			sb.Append("h1, h2, p { font-family: Rockwell; }");
			sb.Append("p { font-family: Rockwell; font-size: smaller; }");
			sb.Append("h2 { margin-top: 45px; }");
			sb.Append("</style></head><body>");
			sb.Append("<h1>Crawl Report</h1>");

			sb.Append("<h2>Internal Urls - In Order Crawled</h2>");
			sb.Append("<p>These are the pages found within the site. The size is calculated by getting value of the Length of the text of the response text. This is the order in which they were crawled.</p>");

			sb.Append("<table><tr><th>Page Size</th><th>Viewstate Size</th><th>Url</th></tr>");

			foreach (Page page in _pages)
			{
				sb.Append("<tr><td>");
				sb.Append(page.Size.ToString());
				sb.Append("</td><td>");
				sb.Append(page.ViewStateSize.ToString());
				sb.Append("</td><td>");
				sb.Append(page.URL);
				sb.Append("</td></tr>");
			}

			sb.Append("</table>");


			sb.Append("<h2>Internal Urls - In Order of Size</h2>");
			sb.Append("<p>These are the pages found within the site. The size is calculated by getting value of the Length of the text of the response text. This is the order in terms of total page size.</p>");

			sb.Append("<table><tr><th>Page Size</th><th>Viewstate Size</th><th>Url</th></tr>");

			List<Page> sortedList = new List<Page>();
			foreach (Page page in _pages)
			{
				if (sortedList.Count == 0)
				{
					sortedList.Add(page);
				}
				else
				{
					for (int i = 0; i <= sortedList.Count - 1; i++)
					{
						Page sortedPage = sortedList[i];

						if (sortedPage.Size > page.Size)
						{
							sortedList.Insert(i, page);
							break;
						}
						else if (i == sortedList.Count - 1)
						{
							sortedList.Add(page);
							break;
						}
					}
				}
			}

			for (int i = sortedList.Count - 1; i >= 0; i--)
			{
				Page page = sortedList[i];

				sb.Append("<tr><td>");
				sb.Append(page.Size.ToString());
				sb.Append("</td><td>");
				sb.Append(page.ViewStateSize.ToString());
				sb.Append("</td><td>");
				sb.Append(page.URL);
				sb.Append("</td></tr>");
			}
			sb.Append("</table>");


			sb.Append("<h2>External Urls</h2>");
			sb.Append("<p>These are the links to the pages outside the site.</p>");

			sb.Append("<table><tr><th>Url</th></tr>");

			foreach (string str in _externalUrls)
			{
				sb.Append("<tr><td>");
				sb.Append(str);
				sb.Append("</td></tr>");
			}

			sb.Append("</table>");

			sb.Append("<h2>Other Urls</h2>");
			sb.Append("<p>These are the links to things on the site that are not html files (html, aspx, etc.), like images and css files.</p>");

			sb.Append("<table><tr><th>Url</th></tr>");

			foreach (string str in _otherUrls)
			{
				sb.Append("<tr><td>");
				sb.Append(str);
				sb.Append("</td></tr>");
			}

			sb.Append("</table>");

			sb.Append("<h2>Bad Urls</h2>");
			sb.Append("<p>Any bad urls will be listed here.</p>");

			sb.Append("<table><tr><th>Url</th></tr>");

			if (_failedUrls.Count > 0)
			{
				foreach (string str in _failedUrls)
				{
					sb.Append("<tr><td>");
					sb.Append(str);
					sb.Append("</td></tr>");
				}
			}
			else
			{
				sb.Append("<tr><td>No bad urls.</td></tr>");
			}

			sb.Append("</table>");


			sb.Append("<h2>Exceptions</h2>");
			sb.Append("<p>Any exceptions that were thrown would be shown here.</p>");

			sb.Append("<table><tr><th>Exception</th></tr>");

			if (_exceptions.Count > 0)
			{
				foreach (string str in _exceptions)
				{
					sb.Append("<tr><td>");
					sb.Append(str);
					sb.Append("</td></tr>");
				}
			}
			else
			{
				sb.Append("<tr><td>No exceptions thrown.</td></tr>");
			}

			sb.Append("</table>");

			sb.Append("<h2>Css Classes</h2>");
			sb.Append("<p>These are the css classes used on the site, just in case you want to know which ones you're using and compare that with your css...</p>");

			sb.Append("<table><tr><th>Class</th></tr>");

			if (_classes.Count > 0)
			{
				foreach (string str in _classes)
				{
					sb.Append("<tr><td>");
					sb.Append(str);
					sb.Append("</td></tr>");
				}
			}
			else
			{
				sb.Append("<tr><td>No classes found.</td></tr>");
			}

			sb.Append("</table>");

			sb.Append("</body></html>");
			return sb;
		}

		private static void OpenReportInIE()
		{
			object o = new object();
			InternetExplorer ie = new InternetExplorer();
			IWebBrowserApp wb = (IWebBrowserApp)ie;
			wb.Visible = true;

			wb.Navigate(ConfigurationManager.AppSettings["logTextFileName"].ToString(), ref o, ref o, ref o, ref o);
		}

		#endregion

	}
}
