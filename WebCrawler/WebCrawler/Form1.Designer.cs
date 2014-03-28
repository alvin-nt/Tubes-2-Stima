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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.currentCrawlText = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.pagesCrawled = new System.Windows.Forms.Label();
			this.BytesDownloaded = new System.Windows.Forms.Label();
			this.ignoredUrl = new System.Windows.Forms.Label();
			this.externalUrl = new System.Windows.Forms.Label();
			this.htmlOutput = new System.Windows.Forms.CheckBox();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// doCrawl
			// 
			this.doCrawl.Location = new System.Drawing.Point(601, 32);
			this.doCrawl.Name = "doCrawl";
			this.doCrawl.Size = new System.Drawing.Size(98, 24);
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
			this.urlToCrawl.Size = new System.Drawing.Size(622, 20);
			this.urlToCrawl.TabIndex = 2;
			// 
			// dfs
			// 
			this.dfs.AutoSize = true;
			this.dfs.Location = new System.Drawing.Point(143, 36);
			this.dfs.Name = "dfs";
			this.dfs.Size = new System.Drawing.Size(113, 17);
			this.dfs.TabIndex = 4;
			this.dfs.TabStop = true;
			this.dfs.Text = "Depth First Search";
			this.dfs.UseVisualStyleBackColor = true;
			// 
			// bfs
			// 
			this.bfs.AutoSize = true;
			this.bfs.Location = new System.Drawing.Point(16, 36);
			this.bfs.Name = "bfs";
			this.bfs.Size = new System.Drawing.Size(121, 17);
			this.bfs.TabIndex = 3;
			this.bfs.TabStop = true;
			this.bfs.Text = "Breadth First Search";
			this.bfs.UseVisualStyleBackColor = true;
			// 
			// crawler
			// 
			this.crawler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.beginCrawl);
			this.crawler.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.crawlProgress);
			this.crawler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.crawlEnded);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(305, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Depth Limit:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(374, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(42, 20);
			this.textBox1.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Currently Crawling:";
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
			this.groupBox1.Controls.Add(this.currentCrawlText);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(16, 205);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(686, 87);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Statistics";
			// 
			// currentCrawlText
			// 
			this.currentCrawlText.AutoSize = true;
			this.currentCrawlText.Location = new System.Drawing.Point(127, 26);
			this.currentCrawlText.Name = "currentCrawlText";
			this.currentCrawlText.Size = new System.Drawing.Size(54, 13);
			this.currentCrawlText.TabIndex = 8;
			this.currentCrawlText.Text = "<nothing>";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Pages Crawled:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Bytes Downloaded:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(222, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "External URLs:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(222, 51);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 13);
			this.label7.TabIndex = 12;
			this.label7.Text = "Ignored URLs:";
			// 
			// pagesCrawled
			// 
			this.pagesCrawled.AutoSize = true;
			this.pagesCrawled.Location = new System.Drawing.Point(127, 51);
			this.pagesCrawled.Name = "pagesCrawled";
			this.pagesCrawled.Size = new System.Drawing.Size(13, 13);
			this.pagesCrawled.TabIndex = 13;
			this.pagesCrawled.Text = "0";
			// 
			// BytesDownloaded
			// 
			this.BytesDownloaded.AutoSize = true;
			this.BytesDownloaded.Location = new System.Drawing.Point(127, 64);
			this.BytesDownloaded.Name = "BytesDownloaded";
			this.BytesDownloaded.Size = new System.Drawing.Size(13, 13);
			this.BytesDownloaded.TabIndex = 14;
			this.BytesDownloaded.Text = "0";
			// 
			// ignoredUrl
			// 
			this.ignoredUrl.AutoSize = true;
			this.ignoredUrl.Location = new System.Drawing.Point(318, 51);
			this.ignoredUrl.Name = "ignoredUrl";
			this.ignoredUrl.Size = new System.Drawing.Size(13, 13);
			this.ignoredUrl.TabIndex = 15;
			this.ignoredUrl.Text = "0";
			// 
			// externalUrl
			// 
			this.externalUrl.AutoSize = true;
			this.externalUrl.Location = new System.Drawing.Point(318, 64);
			this.externalUrl.Name = "externalUrl";
			this.externalUrl.Size = new System.Drawing.Size(13, 13);
			this.externalUrl.TabIndex = 16;
			this.externalUrl.Text = "0";
			// 
			// htmlOutput
			// 
			this.htmlOutput.AutoCheck = false;
			this.htmlOutput.AutoSize = true;
			this.htmlOutput.Location = new System.Drawing.Point(466, 37);
			this.htmlOutput.Name = "htmlOutput";
			this.htmlOutput.Size = new System.Drawing.Size(105, 17);
			this.htmlOutput.TabIndex = 9;
			this.htmlOutput.Text = "Output as HTML";
			this.htmlOutput.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 80);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(153, 13);
			this.label8.TabIndex = 10;
			this.label8.Text = "TODO: make databasesettings";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(711, 304);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.htmlOutput);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dfs);
			this.Controls.Add(this.bfs);
			this.Controls.Add(this.urlToCrawl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.doCrawl);
			this.Name = "Form1";
			this.Text = "Hubble Search Telescope Crawler";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label4;
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
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Label label8;
    }
}

