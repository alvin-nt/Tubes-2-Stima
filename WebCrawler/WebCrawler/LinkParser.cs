using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Configuration;
using HtmlAgilityPack;

namespace WebCrawler
{
	/// <summary>
	/// Ini class dari LinkParser
	/// Kebanyakan bekerja dengan dokumen yang sudah di-load
	/// </summary>
	public class LinkParser
	{
		#region Constructor

		/// <summary>
		/// Default ctor
		/// </summary>
		public LinkParser(string filename = "domains.txt")
		{
			// asumsi: link di ConfigurationManager valid
			if (!classLoaded)
			{
				// dapatkan hanya domain dari website
				GenericLink = ConfigurationManager.AppSettings["url"];
				GenericLink = GenericLink.Substring(GenericLink.IndexOf('/') + 2);
				GenericLink = GenericLink.Substring(0, GenericLink.IndexOf('/'));

				FileOperators.LoadToListOfString(filename, _TLDName);

				classLoaded = true;
			}

			if (_TLDName.Count == 0) // diulang jika ada file yang belum ada
			{
				FileOperators.LoadToListOfString(filename, _TLDName);   
			}
		}
		#endregion
		#region Private Instance Fields
		/// ini daftar semua instansiasi List of URLs, beserta dengan exceptions
		/// 
		
		private List<string> _goodUrls = new List<string>(); // URL website internal
		private List<string> _badUrls = new List<string>(); // URL kosong/ngaco
		private List<string> _otherUrls = new List<string>(); // untuk URL yang merujuk ke non-website
		private List<string> _externalUrls = new List<string>(); // URL website eksternal
		private List<string> _exceptions = new List<string>(); // penampung exceptions

		/// <summary>
		/// daftar semua Top-Level Domain, untuk kebutuhan parsing
		/// </summary>
		private static List<string> _TLDName = new List<string>();

		/// <summary>
		/// Konstanta domain website yang sedang di-crawl, tanpa protocol
		/// </summary>
		private static string GenericLink;

		private static bool classLoaded = false;
		#endregion
		#region Public Properties
		/// <summary>
		/// ini buat definisi getter & setter dari semua instansiasi di Private Instance Fields
		/// </summary>
		/// 

		public List<string> GoodUrls
		{
			get { return _goodUrls; }
			set { _goodUrls = value; }
		}

		public List<string> BadUrls
		{
			get { return _badUrls; }
			set { _badUrls = value; }
		}

		public List<string> ExternalUrls
		{
			get { return _externalUrls; }
			set { _externalUrls = value; }
		}

		public List<string> OtherUrls
		{
			get { return _otherUrls; }
			set { _otherUrls = value; }
		}

		public List<string> Exceptions
		{
			get { return _exceptions; }
			set { _exceptions = value; }
		}

		public List<string> TLDName
		{
			get { return _TLDName; }
			set { _TLDName = value; }
		}
		#endregion

		
		/// <summary>
		/// Memproses semua Link yang terdapat dalam sebuah dokumen Html
		/// </summary>
		/// <param name="doc">dokumen HTML yang mau di-cek</param>
		/// <param name="sourceUri">URI sumbernya</param>
		public void ParseLinks(HtmlDocument doc, Uri sourceUri)
		{
			// ambil root dari HTML doc
			var root = doc.DocumentNode;
			
			// ambil semua node yang adalah anchor
			var aNodes = root.Descendants("a").ToList();

			foreach (var aNode in aNodes)
			{
				// dapatkan isi tag href
				var hrefLink = aNode.GetAttributeValue("href", String.Empty);

				if (hrefLink != String.Empty)
				{
					if (!GoodUrls.Contains(hrefLink))
					{
						if (IsExternalWebPage(hrefLink))
						{
							_externalUrls.Add(hrefLink);
						}
						else if (IsAWebPage(hrefLink))
						{
							GoodUrls.Add(hrefLink);
						}
						else
						{
							// bikin jadi absolut, abis itu baru dimasukkin ke dalam list
							hrefLink = Crawler.FixLink(hrefLink, sourceUri);
							OtherUrls.Add(hrefLink);
						}
					}
				}
				else // link kosong!
				{
					_badUrls.Add("Blank URL page on " + hrefLink);
				}
			}
		}
			
		/// <summary>
		/// Apakah href menunjuk pada halaman web?
		/// </summary>
		/// <param name="foundHref">Nilai href yang sedang dicek</param>
		/// <returns>Boolean</returns>
		private static bool IsAWebPage(string foundHref)
		{
			bool retval;

			if (foundHref.StartsWith("javascript:"))
				retval = false;
			else
			{
				// cek extension
				string extension = foundHref.Substring(foundHref.LastIndexOf(".") + 1,
									foundHref.Length - foundHref.LastIndexOf(".") - 1);
				extension = extension.ToLower();

				if (extension.Contains("php"))
				{
					retval = true;
				}
				else
				{
					switch (extension)
					{
						case "xml":
						case "asp":
						case "aspx":
						case "html":
						case "htm":
							retval = true;
							break;
						default:
							retval = false;
							break;
					}
				}

			}

			return retval;
		}

		/// <summary>
		/// Memeriksa apakah URL menunjuk pada halaman luar
		/// NOTE: butuh perbandingan dengan RootURL
		/// </summary>
		/// <param name="Url">string URL yang diperiksa</param>
		/// <returns>Boolean -- perlu dijelasin? -_-"</returns>
		private static bool IsExternalWebPage(string Url)
		{
			bool retval = false;

			string currentUrl = new Uri(ConfigurationManager.AppSettings["url"]).ToString();

			if (Url.StartsWith(ConfigurationManager.AppSettings["authority"]))
			{
				retval = false;
			}
			else if (((Url.StartsWith("http://") || Url.StartsWith("https://")) && 
					 !Url.StartsWith(currentUrl)) || 
					 Url.StartsWith("www."))
			{
				retval = true;
			}
			else
			{
				for (int i = 0; i < _TLDName.Count && !retval; i++)
				{
					string domain = _TLDName[i];

					bool haveTLD = Url.Contains("." + domain + "/") || Url.Contains("." + domain);
					retval = haveTLD ? !Url.Contains(GenericLink) : false;
				}
			}

			return retval;
		}
	}
}