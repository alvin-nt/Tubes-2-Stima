using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Configuration;

namespace CSWebTest
{
    /// <summary>
    /// Ini class dari LinkParser
    /// </summary>
    public class LinkParser
    {
        /// ngetes Region
        #region Constructor

        /// <summary>
        /// Default ctor
        /// </summary>
        public LinkParser() { /* kosong */ }
        #endregion
        #region Constants
        /// <summary>
        /// Definisi konstanta
        /// </summary>
        
        /// ini buat regex
        private const string _LINK_REGEX = "href=\"[a-zA-Z./:&\\d_-]+\"";
        #endregion
        #region Private Instance Fields
        /// ini daftar semua instansiasi List of URLs, beserta dengan exceptions
        /// 

        private List<string> _goodUrls = new List<string>();
        private List<string> _badUrls = new List<string>();
        private List<string> _otherUrls = new List<string>();
        private List<string> _externalUrls = new List<string>();
        private List<string> _exceptions = new List<string>();
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
        #endregion

        /// <summary>
        /// Melakukan parsing pada halaman, untuk mencari link
        /// </summary>
        /// <param name="page"> Page yang mau di-parsing. Page adalah class sendiri</param>
        /// <param name="sourceUrl"> URL halaman tersebut</param>
        /// 
        public void ParseLinks(Page page, string sourceUrl)
        {
            // regex?
            MatchCollection matches = Regex.Matches(page.Text, _LINK_REGEX);

            for (int i = 0; i <= matches.Count - 1; i++) // cari jumlah yang cocok
            {
                // loop: cari elemen, apakah dia masuk salah satu elemen yang ada?
                Match anchorMatch = matches[i];

                if (anchorMatch.Value == String.Empty) // blank
                {
                    BadUrls.Add("Blank url value on page " + sourceUrl);
                    continue;
                }

                string foundHref = null;
                try
                {
                    foundHref = anchorMatch.Value.Replace("href=\"", "");
                    foundHref = foundHref.Substring(0, foundHref.IndexOf("\""));
                }
                catch (Exception exc)
                {
                    Exceptions.Add("Error parsing matched href: " + exc.Message);
                }

                if (!GoodUrls.Contains(foundHref))
                {
                    if (IsExternalUrl(foundHref))
                    {
                        _externalUrls.Add(foundHref);
                    }
                    else if (!IsAWebPage(foundHref))
                    {
                        foundHref = Crawler.FixPath(sourceUrl, foundHref);
                        _otherUrls.Add(foundHref);
                    }
                    else
                    {
                        GoodUrls.Add(foundHref);
                    }
                }
            }
        }

        /// <summary>
        /// Apakah URL menunjuk pada web luar?
        /// </summary>
        /// <param name="url">URL yang sedang dicek</param>
        /// <returns>Boolean, yang menunjukkan URL menunjuk pada halaman luar/tidak</returns>
        private static bool IsExternalUrl(string url)
        {
            if (url.IndexOf(ConfigurationManager.AppSettings["authority"]) > -1)
            {
                return false;
            }
            else { 
                try {
                    if (url.Substring(0, 7) == "http://" || 
                         url.Substring(0, 3) == "www" ||
                         url.Substring(0, 8) == "https://")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exc) // trick: kalo bablas, berarti emang bukan web page
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Apakah href menunjuk pada halaman web?
        /// </summary>
        /// <param name="foundHref">Nilai href yang sedang dicek</param>
        /// <returns>Boolean, yang menunjukkan URL menunjuk pada halaman web</returns>
        private static bool IsAWebPage(string foundHref)
        {
            if (foundHref.IndexOf("javascript:") == 0)
                return false;
            else
            {
                // cek extension
                string extension = foundHref.Substring(foundHref.LastIndexOf(".") + 1,
                                    foundHref.Length - foundHref.LastIndexOf(".") - 1);
                extension = extension.ToLower();
                switch (extension)
                {
                    case "php":
                    case "xml":
                    case "asp":
                    case "aspx":
                    case "html":
                    case "htm":
                        return true;
                    default:
                        return false;
                }
            }
        }

    }
}