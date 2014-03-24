using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace WebCrawler
{
    /// <summary>
    /// ini class yang membungkus semua parsing Link dan Teks
    /// </summary>
    class Parser
    {
        public Uri Url { get; set; }

        private LinkParser _linkParser = new LinkParser();
        private TextParser _textParser = new TextParser();

        private List<string> _keyWords = new List<string>();
        
        public void ParseURL(Uri URL)
        {

        }
    }
}
