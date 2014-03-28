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
using System.Threading;

namespace WebCrawler {
	public partial class Form1 : Form {

		class GuiDataCapsule {
			public string currentURL;
			public string totalCrawled;
			public string totalSize;
			public string ignored;
			public string external;
			public GuiDataCapsule(string _currentURL, string _totalCrawled, string _totalSize, string _ignored, string _external) {
				currentURL = _currentURL;
				totalCrawled = _totalCrawled;
				totalSize = _totalSize;
				ignored = _ignored;
				external = _external;
			}
		}

		public Form1() {
			InitializeComponent();
			abortCrawl.Enabled = false;
			urlUpdater.WorkerReportsProgress = true;
			urlUpdater.RunWorkerAsync();
		}

		private void doCrawl_Click(object sender, EventArgs e) {
			abortCrawl.Enabled = true;
			doCrawl.Enabled = false;
			ConfigurationManager.AppSettings["dbName"] = inDbName.Text;
			ConfigurationManager.AppSettings["dbTable"] = inDbTable.Text;
			ConfigurationManager.AppSettings["dbUser"] = inDbUser.Text;
			ConfigurationManager.AppSettings["dbPass"] = inDbPass.Text;
			ConfigurationManager.AppSettings["dbServer"] = inDbServer.Text;
			ConfigurationManager.AppSettings["crawlDepth"] = depthLimit.Text;
			ConfigurationManager.AppSettings["url"] = urlToCrawl.Text;
			if (htmlOutput.Checked) {
				logFile.ShowDialog();
			}
			crawler.RunWorkerAsync();
		}

		private void beginCrawl(object sender, DoWorkEventArgs e) {
			if (bfs.Checked) {
				Console.WriteLine("Ugh cant do...");
				//CrawlerBFS.CrawlSite();
			} else if (dfs.Checked) {

				Crawler.CrawlSite();
			}
		}

		private void crawlEnded(object sender, RunWorkerCompletedEventArgs e) {
			abortCrawl.Enabled = false;
			doCrawl.Enabled = true;
			ConfigurationManager.AppSettings["abort"] = "no";
			ConfigurationManager.AppSettings["currentURL"] = "";
		}

		private void crawlProgress(object sender, ProgressChangedEventArgs e) {

		}

		private void logFile_FileOK(object sender, CancelEventArgs e) {
			ConfigurationManager.AppSettings["logTextFileName"] = logFile.FileName;
		}

		private void abortCrawl_Click(object sender, EventArgs e) {
			ConfigurationManager.AppSettings["abort"] = "yes";
		}

		private void urlUpdaterStart(object sender, DoWorkEventArgs e) {
			while (true) {
				urlUpdater.ReportProgress(0, new GuiDataCapsule(ConfigurationManager.AppSettings["currentURL"],
																ConfigurationManager.AppSettings["totalCrawled"],
																ConfigurationManager.AppSettings["bytesCrawled"],
																ConfigurationManager.AppSettings["ignoredURL"],
																ConfigurationManager.AppSettings["externalURL"]));
				Thread.Sleep(20);
			}
		}

		private void urlUpdaterModify(object sender, ProgressChangedEventArgs e) {
			GuiDataCapsule data = e.UserState as GuiDataCapsule;
			currentCrawlText.Text = data.currentURL;
			pagesCrawled.Text = data.totalCrawled;
			BytesDownloaded.Text = data.totalSize;
			ignoredUrl.Text = data.ignored;
			externalUrl.Text = data.external;
		}
	}
}
