using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace WebCrawler
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void doCrawl_Click(object sender, EventArgs e) {
			ConfigurationManager.AppSettings["url"] = urlToCrawl.Text;
			if (bfs.Checked) {
				Crawler.CrawlSite();
			}
			if (dfs.Checked) {
				Console.WriteLine("ASDF");
				//CrawlerBFS.CrawlSite();
			}
		}

		private void beginCrawl(object sender, DoWorkEventArgs e) {

		}

		private void crawlEnded(object sender, RunWorkerCompletedEventArgs e) {

		}

		private void crawlProgress(object sender, ProgressChangedEventArgs e) {

		}
	}
}
