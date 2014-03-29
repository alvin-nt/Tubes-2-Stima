using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebCrawler
{
	class TextParser
	{
		#region Constructor
		public TextParser(string filename = "ignored.txt")
		{
			if (_ignore.Count == 0)
			{
				FileOperators.LoadToListOfString(filename, _ignore);
			}
		} 
		#endregion
		#region Constants
		private const string _REGEX_PASS1 = @"<[^>]+>|&nbsp;";
		private const string _REGEX_PASS2 = @"\s{2,}";
		
		#endregion
		#region Private Instance Fields
		private List<string> _keywords = new List<string>();
		private static List<string> _ignore = new List<string>();

		private List<string> _exceptions = new List<string>();
		#endregion
		#region Public Properties
		public List<string> Keywords
		{
			get { return _keywords; }
			set { _keywords = value; }
		}

		public static List<string> Ignore
		{
			get { return _ignore; }
			set { _ignore = value; }
		}

		public List<string> Exceptions
		{
			get { return _exceptions; }
			set { _exceptions = value; }
		}
		#endregion

		/// <summary>
		/// Mengekstraksi Keywords dari sebuah dokumen
		/// Keyword akan diekstraksi dari tag-tag sebagai berikut:
		/// p, a, title, h1, h2, h3, h4, h5, h6
		/// </summary>
		/// <param name="doc">dokumen HTML yang akan di-parsing</param>
		public void GetKeyWords(HtmlDocument doc)
		{
			var root = doc.DocumentNode;

			ExtractKeywordsFromTag("p", root);
			ExtractKeywordsFromTag("a", root);
			ExtractKeywordsFromTag("title", root);
			ExtractKeywordsFromTag("h1", root);
			ExtractKeywordsFromTag("h2", root);
			ExtractKeywordsFromTag("h3", root);
			ExtractKeywordsFromTag("h4", root);
			ExtractKeywordsFromTag("h5", root);
			ExtractKeywordsFromTag("h6", root);
		}
		
		/// <summary>
		/// Mengekstraksi semua teks yang ada di tag, lalu dipecah menjadi keyword
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="root"></param>
		private void ExtractKeywordsFromTag(string tag, HtmlNode root)
		{
			// tag sedikit diubah
			tag = "//" + tag;

			if (root.HasChildNodes)
			{
				var tagNodes = root.SelectNodes(tag);

				if (tagNodes != null)
				{
					try
					{
						foreach (var tagNode in tagNodes)
						{
							// nge-split pake whitespace
							List<string> buffer = new List<string>(NormalizeHTML(tagNode).Trim().Split(null));

							if (buffer.Count > 0)
							{
								buffer = buffer.Where(x => !String.IsNullOrWhiteSpace(x) && x.Length >= 3).ToList();

								Crawler.MergeList(_keywords, buffer);
							}
						}
					}
					catch (Exception ex)
					{
						_exceptions.Add("Tag <" + tag + "> parsing error: " + ex);
					}
				}
			}
		}

		/// <summary>
		/// Melakukan normalisasi terhadap HTML Text:
		/// 1. Pembersihan terhadap tag/simbol yang tersisa
		/// 2. Pengubahan semua karakter menjadi small caps
		/// 3. Pengubahan special characters menjadi whitespace
		/// </summary>
		/// <param name="node">Node HTML yang dimaksud</param>
		/// <returns>string yang berisi teks HTML yang sudah 'normal'</returns>
		private string NormalizeHTML(HtmlNode node)
		{
			StringBuilder sb = new StringBuilder();
			
			// filter -- 2-pass
			string temp = Regex.Replace(node.InnerText, _REGEX_PASS1, "").Trim();
			temp = Regex.Replace(temp, _REGEX_PASS2, " ");

			// hapus semua special character (kecuali apostrophe di tengah) & ubah ke lower case
			for (int i = 0; i < temp.Length; i++)
			{
				char c = temp[i];

				if ((c >= '0' && c <= '9') || 
					(c >= 'A' && c <= 'Z') || 
					(c >= 'a' && c <= 'z'))
				{
					sb.Append(Char.ToLower(c));
				}
				else if ((c == '.' || c == '_' || c == ' ' || c == '-' || c == '/' || c == ':') && 
						(i != 0 && i != temp.Length-1))
				{
					sb.Append(' ');
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// Mendapatkan judul dari dokumen HTML
		/// </summary>
		/// <param name="doc"></param>
		/// <returns></returns>
		public string GetTitle(HtmlDocument doc)
		{
			string tag = "//title";
			string title = "(no title)";
			var root = doc.DocumentNode;

			if (root.HasChildNodes)
			{
				var titleNode = root.SelectSingleNode(tag);

				if (titleNode != null)
				{
					var temp = titleNode.InnerText.Trim();
					title = (String.IsNullOrWhiteSpace(temp) ? "(no title)" : temp);
				}
			}

			return title;
		}
	}
}
