using System;
using System.Collections.Generic;
using HtmlAgilityPack;

/// <summary>
/// ini kelas halaman web
/// </summary>

namespace WebCrawler
{
	public class Page
	{
		#region Constructor

		/// <summary>
		/// Default ctor
		/// </summary>
		public Page() { /* kosong */}
		
		#endregion
		#region Private Instance Fields
		/// <summary>
		/// semua instansiasi atribut (private)
		/// </summary>
		/// 

		private int _size; // ukuran page?
		private string _text; // teks dalam page?
		private string _title;
		private Uri _url; // URL dari page
		private int _viewStateSize; // ??
		private List<string> _keywords = new List<string>();

		#endregion
		#region Public Properties
		/// <summary>
		/// Getter - setter dari Private Instance Fields
		/// </summary>
		/// 
		public int Size
		{
			get { return _size; }
		}
		
		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				_size = value.Length;
			}
		}

		public string Title
		{
			get { return _title; }
			set	{ _title = value; }
		}

		public Uri URL
		{
			get { return _url; }
			set { _url = value; }
		}

		public List<string> Keywords
		{
			get { return _keywords; }
			set { _keywords = value; }
		}

		public int ViewStateSize
		{
			get { return _viewStateSize; }
			set { _viewStateSize = value; }
		}
		#endregion
		
		/// <summary>
		/// ini untuk menghitung ViewState
		/// </summary>
		public void CalculateViewStateSize()
		{
			int startingIndex = Text.IndexOf("id=\"__VIEWSTATE\"");
			if (startingIndex > -1) // ketemu
			{
				// cari indeks bagian string yang menunjukkan ViewState
				int indexOfViewstateValueNode = Text.IndexOf("value=\"", startingIndex);
				int indexOfClosingQuotationMark = Text.IndexOf("\"", indexOfViewstateValueNode + 7); // +7, http://
				string viewstateValue = Text.Substring(indexOfViewstateValueNode + 7,
						indexOfClosingQuotationMark - (indexOfViewstateValueNode + 7));

				ViewStateSize = viewstateValue.Length;
			}
		}

		/// <summary>
		/// untuk mengeluarkan keyword
		/// </summary>
		public void printKeywords()
		{
			foreach (string word in _keywords)
			{
				Console.WriteLine("Keyword: " + word);
			}
		}
	}
}
