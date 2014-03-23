using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SHDocVw; // ini buat menampilkan laporan, pake link ke dll yang di-include

namespace CSWebTest
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
        private static StringBuilder _logBuffer = new StringBuilder();
        #endregion

        /// <summary>
        /// Mulai crawling
        /// </summary>
        public static void CrawlSite()
        {
            Console.WriteLine("Beginning crawl.");

            CrawlPage(ConfigurationManager.AppSettings["url"]);

            StringBuilder sb = CreateReport();

            WriteReportToDisk(sb.ToString());

            OpenReportInIE();

            Console.WriteLine("finished crawl");
        }

        /// <summary>
        /// crawl sebuah page
        /// </summary>
        /// <param name="url">URL dari page yang mau di-crawl</param>
        public static void CrawlPage(string url)
        {
            if (!PageHasBeenCrawled(url))
            {
                string htmlText = GetWebText(url);

                Page page = new Page();
                page.Text = htmlText;
                page.URL = url;
                page.CalculateViewStateSize();

                _pages.Add(page);

                LinkParser linkParser = new LinkParser();
                linkParser.ParseLinks(page, url);

                CSSClassParser classParser = new CSSClassParser();
                classParser.ParseForCSSClasses(page);

                // add data to main data lists
                MergeList(_externalUrls, linkParser.ExternalUrls);
                MergeList(_otherUrls, linkParser.OtherUrls);
                MergeList(_failedUrls, linkParser.BadUrls);
                MergeList(_classes, classParser.Classes);

                // rekursi -- ini membuat algoritmanya jadi DFS
                // TODO: buat jadi multithreaded
                foreach (string link in linkParser.GoodUrls)
                {
                    string formattedLink = link;
                    try
                    {
                        formattedLink = FixPath(url, formattedLink);
                        if (formattedLink != String.Empty)
                        {
                            CrawlPage(formattedLink);
                        }
                    }
                    catch (Exception exc)
                    {
                        _failedUrls.Add(formattedLink +
                                        " (on page at url " + url + ") - " +
                                        exc.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Fiksasi path, dari relatif menjadi absolute URL yang bisa dijalankan
        /// </summary>
        /// <param name="originatingUrl">URL </param>
        /// <param name="link">link yang mau dibenerin </param>
        /// <returns>URL yang sudah fix untuk di-fetch</returns>
        public static string FixPath(string originatingUrl, string link)
        {
            string formattedLink = String.Empty;
            string urlSetting = ConfigurationManager.AppSettings["url"].ToString();

            // kalo ketemu "../" >> relative path
            if (link.IndexOf("../") > -1)
            {
                formattedLink = ResolveRelativePaths(link, originatingUrl);
            } 
            // kalo ketemu link yang cocok dengan URL
            else if (originatingUrl.IndexOf(urlSetting) > -1
                && link.IndexOf(urlSetting) > -1)
            {
                formattedLink = originatingUrl.Substring(0, originatingUrl.LastIndexOf("/") + 1) + link;
            }
            // ????
            else if (link.IndexOf(urlSetting) == -1)
            {
                formattedLink = urlSetting + link;
            }

            return formattedLink;
        }

        private static string ResolveRelativePaths(string relativeUrl, string originatingUrl)
        {
            string resolvedUrl = String.Empty;

            // split dua-duanya berdasarkan "/"
            string[] relativeUrlArray = relativeUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] originatingUrlElements = originatingUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int indexOfFirstNonRelativePathElement = 0;
            
            int i = 0;
            bool continueLoop = (relativeUrlArray[i] != "..");
            while (i <= relativeUrlArray.Length && continueLoop)
            {
                continueLoop = (relativeUrlArray[i] != "..");
                if (!continueLoop)
                {
                    indexOfFirstNonRelativePathElement = i;
                }
                else
                {
                    i++;
                }
            }

            // count of originating url elements to use >> elemen dari url yang asli
            // yang akan dipakai untuk mengubah string yang dipisah dengan '/'
            int countOfOriginatingUrlElementsToUse = originatingUrlElements.Length -
                                                     indexOfFirstNonRelativePathElement -1;
            for (int j = 0; j < countOfOriginatingUrlElementsToUse; j++)
            {
                if (originatingUrlElements[j] == "http:" ||
                    originatingUrlElements[j] == "https:")
                {
                    resolvedUrl += originatingUrlElements[j] + "//";
                }
                else
                {
                    resolvedUrl += originatingUrlElements[j] + "/";
                }
            }

            // loop terakhir memasukkan url relatif
            for (int j = 0; j < relativeUrlArray.Length; j++)
            {
                if (j >= indexOfFirstNonRelativePathElement)
                {
                    resolvedUrl += relativeUrlArray[j];

                    // kalo belom sampai ujung, tambahin "/"
                    if (i < relativeUrlArray.Length - 1)
                    {
                        resolvedUrl += "/";
                    }
                }
            }

            return resolvedUrl;
        }

        /// <summary>
        /// ngecek apakah page uda dicrawl
        /// </summary>
        /// <param name="URL">url yang mau diperiksa</param>
        /// <returns>TRUE >> page uda di-crawl</returns>
        private static bool PageHasBeenCrawled(string URL)
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
        private static void MergeList(List<string> targetList,
                                      List<string> sourceList)
        {
            foreach (string str in sourceList)
            {
                if (!targetList.Contains(str))
                    targetList.Add(str);
            }
        }

        /// <summary>
        /// Dapatkan teks respon dari URL yang diberikan
        /// </summary>
        /// <param name="URL">URL yang mau di-fetch</param>
        /// <returns>Text balasan</returns>
        private static string GetWebText(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.UserAgent = "Crawler Coba-Coba";

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
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
