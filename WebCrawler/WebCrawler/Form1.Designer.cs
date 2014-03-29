namespace WebCrawler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.doCrawl = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.urlToCrawl = new System.Windows.Forms.TextBox();
			this.dfs = new System.Windows.Forms.RadioButton();
			this.bfs = new System.Windows.Forms.RadioButton();
			this.crawler = new System.ComponentModel.BackgroundWorker();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.externalUrl = new System.Windows.Forms.Label();
			this.ignoredUrl = new System.Windows.Forms.Label();
			this.BytesDownloaded = new System.Windows.Forms.Label();
			this.pagesCrawled = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.currentCrawlText = new System.Windows.Forms.Label();
			this.htmlOutput = new System.Windows.Forms.CheckBox();
			this.logFile = new System.Windows.Forms.SaveFileDialog();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.inDbTable = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.inDbPass = new System.Windows.Forms.TextBox();
			this.inDbUser = new System.Windows.Forms.TextBox();
			this.inDbName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.inDbServer = new System.Windows.Forms.TextBox();
			this.depthLimit = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.abortCrawl = new System.Windows.Forms.Button();
			this.urlUpdater = new System.ComponentModel.BackgroundWorker();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.depthLimit)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// doCrawl
			// 
			this.doCrawl.Location = new System.Drawing.Point(516, 41);
			this.doCrawl.Name = "doCrawl";
			this.doCrawl.Size = new System.Drawing.Size(80, 29);
			this.doCrawl.TabIndex = 0;
			this.doCrawl.Text = "Begin Crawl";
			this.doCrawl.UseVisualStyleBackColor = true;
			this.doCrawl.Click += new System.EventHandler(this.doCrawl_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Crawl URL";
			// 
			// urlToCrawl
			// 
			this.urlToCrawl.Location = new System.Drawing.Point(77, 10);
			this.urlToCrawl.Name = "urlToCrawl";
			this.urlToCrawl.Size = new System.Drawing.Size(519, 20);
			this.urlToCrawl.TabIndex = 2;
			this.urlToCrawl.Text = "http://informatika.stei.itb.ac.id/~rinaldi.munir/";
			// 
			// dfs
			// 
			this.dfs.AutoSize = true;
			this.dfs.Location = new System.Drawing.Point(6, 42);
			this.dfs.Name = "dfs";
			this.dfs.Size = new System.Drawing.Size(76, 17);
			this.dfs.TabIndex = 4;
			this.dfs.Text = "Depth First";
			this.dfs.UseVisualStyleBackColor = true;
			// 
			// bfs
			// 
			this.bfs.AutoSize = true;
			this.bfs.Checked = true;
			this.bfs.Location = new System.Drawing.Point(6, 19);
			this.bfs.Name = "bfs";
			this.bfs.Size = new System.Drawing.Size(84, 17);
			this.bfs.TabIndex = 3;
			this.bfs.TabStop = true;
			this.bfs.Text = "Breadth First";
			this.bfs.UseVisualStyleBackColor = true;
			// 
			// crawler
			// 
			this.crawler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.beginCrawl);
			this.crawler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.crawlEnded);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(128, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Depth Limit:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.externalUrl);
			this.groupBox1.Controls.Add(this.ignoredUrl);
			this.groupBox1.Controls.Add(this.BytesDownloaded);
			this.groupBox1.Controls.Add(this.pagesCrawled);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(244, 111);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 80);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Statistics";
			// 
			// externalUrl
			// 
			this.externalUrl.AutoSize = true;
			this.externalUrl.Location = new System.Drawing.Point(318, 51);
			this.externalUrl.Name = "externalUrl";
			this.externalUrl.Size = new System.Drawing.Size(13, 13);
			this.externalUrl.TabIndex = 16;
			this.externalUrl.Text = "0";
			// 
			// ignoredUrl
			// 
			this.ignoredUrl.AutoSize = true;
			this.ignoredUrl.Location = new System.Drawing.Point(318, 29);
			this.ignoredUrl.Name = "ignoredUrl";
			this.ignoredUrl.Size = new System.Drawing.Size(13, 13);
			this.ignoredUrl.TabIndex = 15;
			this.ignoredUrl.Text = "0";
			// 
			// BytesDownloaded
			// 
			this.BytesDownloaded.AutoSize = true;
			this.BytesDownloaded.Location = new System.Drawing.Point(128, 51);
			this.BytesDownloaded.Name = "BytesDownloaded";
			this.BytesDownloaded.Size = new System.Drawing.Size(13, 13);
			this.BytesDownloaded.TabIndex = 14;
			this.BytesDownloaded.Text = "0";
			// 
			// pagesCrawled
			// 
			this.pagesCrawled.AutoSize = true;
			this.pagesCrawled.Location = new System.Drawing.Point(128, 29);
			this.pagesCrawled.Name = "pagesCrawled";
			this.pagesCrawled.Size = new System.Drawing.Size(13, 13);
			this.pagesCrawled.TabIndex = 13;
			this.pagesCrawled.Text = "0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(222, 29);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Ignored URLs:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(222, 51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "External URLs:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 51);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(105, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "kBytes Downloaded:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 29);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Pages Crawled:";
			// 
			// currentCrawlText
			// 
			this.currentCrawlText.AutoSize = true;
			this.currentCrawlText.Location = new System.Drawing.Point(7, 21);
			this.currentCrawlText.Name = "currentCrawlText";
			this.currentCrawlText.Size = new System.Drawing.Size(0, 13);
			this.currentCrawlText.TabIndex = 8;
			// 
			// htmlOutput
			// 
			this.htmlOutput.AutoSize = true;
			this.htmlOutput.Location = new System.Drawing.Point(131, 43);
			this.htmlOutput.Name = "htmlOutput";
			this.htmlOutput.Size = new System.Drawing.Size(105, 17);
			this.htmlOutput.TabIndex = 9;
			this.htmlOutput.Text = "Output as HTML";
			this.htmlOutput.UseVisualStyleBackColor = true;
			// 
			// logFile
			// 
			this.logFile.DefaultExt = "html";
			this.logFile.FileName = "HSTlog.html";
			this.logFile.Filter = "HTML File|*.html";
			this.logFile.InitialDirectory = "C:\\";
			this.logFile.Title = "Save Log to...";
			this.logFile.FileOk += new System.ComponentModel.CancelEventHandler(this.logFile_FileOK);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 25);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(38, 13);
			this.label8.TabIndex = 10;
			this.label8.Text = "Server";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.inDbTable);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.inDbPass);
			this.groupBox2.Controls.Add(this.inDbUser);
			this.groupBox2.Controls.Add(this.inDbName);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.inDbServer);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(16, 36);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(219, 155);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Database Settings";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(7, 129);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(34, 13);
			this.label12.TabIndex = 19;
			this.label12.Text = "Table";
			// 
			// inDbTable
			// 
			this.inDbTable.Location = new System.Drawing.Point(61, 126);
			this.inDbTable.Name = "inDbTable";
			this.inDbTable.Size = new System.Drawing.Size(139, 20);
			this.inDbTable.TabIndex = 18;
			this.inDbTable.Text = "index";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(7, 103);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(53, 13);
			this.label11.TabIndex = 17;
			this.label11.Text = "Password";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 77);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(55, 13);
			this.label10.TabIndex = 16;
			this.label10.Text = "Username";
			// 
			// inDbPass
			// 
			this.inDbPass.Location = new System.Drawing.Point(61, 100);
			this.inDbPass.Name = "inDbPass";
			this.inDbPass.Size = new System.Drawing.Size(139, 20);
			this.inDbPass.TabIndex = 15;
			this.inDbPass.Text = "stima2";
			// 
			// inDbUser
			// 
			this.inDbUser.Location = new System.Drawing.Point(61, 74);
			this.inDbUser.Name = "inDbUser";
			this.inDbUser.Size = new System.Drawing.Size(139, 20);
			this.inDbUser.TabIndex = 14;
			this.inDbUser.Text = "stima2";
			// 
			// inDbName
			// 
			this.inDbName.Location = new System.Drawing.Point(61, 48);
			this.inDbName.Name = "inDbName";
			this.inDbName.Size = new System.Drawing.Size(139, 20);
			this.inDbName.TabIndex = 13;
			this.inDbName.Text = "stima2";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 51);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(53, 13);
			this.label9.TabIndex = 12;
			this.label9.Text = "DB Name";
			// 
			// inDbServer
			// 
			this.inDbServer.Location = new System.Drawing.Point(61, 22);
			this.inDbServer.Name = "inDbServer";
			this.inDbServer.Size = new System.Drawing.Size(139, 20);
			this.inDbServer.TabIndex = 11;
			this.inDbServer.Text = "localhost";
			// 
			// depthLimit
			// 
			this.depthLimit.Location = new System.Drawing.Point(197, 19);
			this.depthLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.depthLimit.Name = "depthLimit";
			this.depthLimit.Size = new System.Drawing.Size(55, 20);
			this.depthLimit.TabIndex = 12;
			this.depthLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.bfs);
			this.groupBox3.Controls.Add(this.depthLimit);
			this.groupBox3.Controls.Add(this.htmlOutput);
			this.groupBox3.Controls.Add(this.dfs);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(244, 36);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(266, 69);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Crawl Settings";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.currentCrawlText);
			this.groupBox4.Location = new System.Drawing.Point(16, 198);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(580, 46);
			this.groupBox4.TabIndex = 14;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Currently Crawling";
			// 
			// abortCrawl
			// 
			this.abortCrawl.Location = new System.Drawing.Point(516, 77);
			this.abortCrawl.Name = "abortCrawl";
			this.abortCrawl.Size = new System.Drawing.Size(80, 29);
			this.abortCrawl.TabIndex = 15;
			this.abortCrawl.Text = "Abort Crawl";
			this.abortCrawl.UseVisualStyleBackColor = true;
			this.abortCrawl.Click += new System.EventHandler(this.abortCrawl_Click);
			// 
			// urlUpdater
			// 
			this.urlUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.urlUpdaterStart);
			this.urlUpdater.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.urlUpdaterModify);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 257);
			this.Controls.Add(this.abortCrawl);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.urlToCrawl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.doCrawl);
			this.Name = "Form1";
			this.Text = "Hubble Search Telescope Crawler";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.depthLimit)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button doCrawl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox urlToCrawl;
		private System.Windows.Forms.RadioButton dfs;
		private System.Windows.Forms.RadioButton bfs;
		private System.ComponentModel.BackgroundWorker crawler;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label currentCrawlText;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label externalUrl;
		private System.Windows.Forms.Label ignoredUrl;
		private System.Windows.Forms.Label BytesDownloaded;
		private System.Windows.Forms.Label pagesCrawled;
		private System.Windows.Forms.CheckBox htmlOutput;
		private System.Windows.Forms.SaveFileDialog logFile;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox inDbName;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox inDbServer;
		private System.Windows.Forms.NumericUpDown depthLimit;
		private System.Windows.Forms.TextBox inDbPass;
		private System.Windows.Forms.TextBox inDbUser;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox inDbTable;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button abortCrawl;
		private System.ComponentModel.BackgroundWorker urlUpdater;
    }
}

