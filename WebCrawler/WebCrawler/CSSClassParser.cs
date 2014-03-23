using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSWebTest
{
    class CSSClassParser
    {
        #region Constructor
        public CSSClassParser() { /* kosong */}
        #endregion
        #region Constants

        private const string _CSS_CLASS_REGEX = "class=\"[a-zA./:&\\d_]+\"";
        
        #endregion
        #region Private Instance Fields

        private List<string> _classes = new List<string>();

        #endregion
        #region Public Properties

        public List<string> Classes
        {
            get { return _classes; }
            set { _classes = value; }
        }
        #endregion

        /// <summary>
        /// melakukan parsing terhadap page u/ mencari CSS class yang dipakai
        /// </summary>
        /// <param name="page">page yang mau ditelusuri</param>
        public void ParseForCSSClasses(Page page)
        {
            MatchCollection matches = Regex.Matches(page.Text, _CSS_CLASS_REGEX);

            for (int i = 0; i < matches.Count; i++)
            {
                Match classMatch = matches[i];

                // splitting terhadap page yang memenuhi
                string[] classesArray = classMatch.Value.Substring(
                                        classMatch.Value.IndexOf('"') + 1,
                                        classMatch.Value.LastIndexOf('"') - 
                                        classMatch.Value.IndexOf('"') - 1).Split(
                                            new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // tambahkan setiap hal yang matching
                foreach (string classVal in classesArray)
                {
                    if (!_classes.Contains(classVal))
                    {
                        _classes.Add(classVal);
                    }
                }
            }
        }
    }
}
