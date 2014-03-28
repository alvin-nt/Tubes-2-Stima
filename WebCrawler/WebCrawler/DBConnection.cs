using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;
using System.Configuration;


namespace WebCrawler
{
	/// <summary>
	/// Class yang menangani koneksi ke MySQL, berikut dengan operator-operatornya
	/// </summary>
	class DBConnection
	{
		#region Private Instance Fields
		private MySqlConnection SQLConn;

		private string server;
		private string database;
		private string uid;
		private string password;
		//private string table; // seems unused.
		
		private List<string> _exceptions = new List<string>();
		#endregion
		#region Constants
		#endregion

		public List<string> Exceptions
		{
			get { return _exceptions; }
			set { _exceptions = value; }
		}
		/// <summary>
		/// Menginisialisasi DBConnection (konstruktor)
		/// </summary>
		public DBConnection()
		{
			Initialize();
		}

		/// <summary>
		/// Deklarasi dan Inisialisasi Variabel yang akan digunakan
		/// </summary>
		private void Initialize()
		{
			server = ConfigurationManager.AppSettings["dbServer"];
			database = ConfigurationManager.AppSettings["dbName"];
			uid = ConfigurationManager.AppSettings["dbUser"];
			password = ConfigurationManager.AppSettings["dbPass"];

			// set the configuration
			StringBuilder connectionString = new StringBuilder();
			connectionString.Append("SERVER=" + server + ";");
			connectionString.Append("DATABASE=" + database + ";");
			connectionString.Append("UID=" + uid + ";");
			connectionString.Append("PASSWORD= " + password + ";");

			SQLConn = new MySqlConnection(connectionString.ToString());
		}

		/// <summary>
		/// Membuka koneksi baru sebelum mengquery tabel
		/// </summary>
		/// <returns></returns>
		private bool OpenConnection()
		{
			try
			{
				SQLConn.Open();
				return true;
			} catch (MySqlException ex)
			{
				/// error berdasarkan number:
				/// 0: cannot connect to server
				/// 1045: Invalid username/password
				switch(ex.Number)
				{
					case 0:
						Console.WriteLine("MySQL Error: Cannot connect to server. Contact administrator");
						break;
					case 1045:
						Console.WriteLine("MySQL Error: Invalid username/password, please try again");
						break;
				}
				_exceptions.Add(ex.ToString());

				return false;
			}
		}

		/// <summary>
		/// Menutup koneksi untuk melepas sumber daya dan menyatakan bahwa koneksi tidak lagi dibutuhkan
		/// </summary>
		/// <returns>true kalo berhasil ditutup, false kalo tidak</returns>
		private bool CloseConnection()
		{
			try
			{
				SQLConn.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				_exceptions.Add("MySQL (Closing Connection) Error: " + ex);
				return false;
			}
		}

		/// <summary>
		/// Membentuk backup dari database yang ada
		/// </summary>
		public void Backup()
		{
			try
			{
				DateTime Time = DateTime.Now;
				int year = Time.Year;
				int month = Time.Month;
				int day = Time.Day;
				int hour = Time.Hour;
				int minute = Time.Minute;
				int second =Time.Second;
				int millisecond = Time.Millisecond;

				string path;
				path = "C:\\MySqlBackup" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-"  + second + "-" + millisecond + ".sql";
				StreamWriter file = new StreamWriter(path);

				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = "mysqldump";
				psi.RedirectStandardInput = false;
				psi.RedirectStandardOutput = true;
				psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
				psi.UseShellExecute = false;

				Process process = Process.Start(psi);

				string output;
				output = process.StandardOutput.ReadToEnd();
				file.WriteLine(output);
				process.WaitForExit();
				file.Close();
				process.Close();
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error , unable to backup!" + ex);
			}
		}

		/// <summary>
		/// Merestorasi database yang ada
		/// </summary>
		public void Restore()
		{
			try
			{
				string path;
				path = "C:\\MySqlBackup.sql";
				StreamReader file = new StreamReader(path);
				string input = file.ReadToEnd();
				file.Close();

				ProcessStartInfo psi = new ProcessStartInfo();
				psi.FileName = "mysql";
				psi.RedirectStandardInput = true;
				psi.RedirectStandardOutput = false;
				psi.Arguments = string.Format(@"-u{0] -p{1} -h{2} {3}", uid, password, server, database);
				psi.UseShellExecute = false;
				
				Process process = Process.Start(psi);
				process.StandardInput.WriteLine(input);
				process.StandardInput.Close();
				process.WaitForExit();
				process.Close();
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error , unable to restore!" + ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="page"></param>
		public void AddPageToTable(Page page)
		{
			string URL = page.URL.ToString();

			try {
				if (this.OpenConnection() == true)
				{
					if (!CheckIfURLExists(URL))
					{ 
						addArrayOfKeyword(page.Keywords);
						addURLToTable(page.URL.ToString(), page.Keywords, page.Title);
					}
				}
			} 
			catch (MySqlException ex) 
			{
				_exceptions.Add("MySQL Error: " + ex);
			}
			finally
			{
				this.CloseConnection();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stringEnum"></param>
		private void addArrayOfKeyword(List<string> stringEnum)
		{
			string tableName = ConfigurationManager.AppSettings["dbTable"];
			// buat querynya
			StringBuilder sb = new StringBuilder();
			bool firstInsert = true;

			sb.AppendFormat("ALTER TABLE `{0}`", tableName);
			for (int i = 0; i < stringEnum.Count - 1; i++)
			{
				string str = stringEnum[i];
				
				if (!CheckIfKeywordExists(str))
				{
					if (firstInsert)
					{
						sb.AppendFormat(" ADD (`{0}` BOOLEAN NOT NULL DEFAULT FALSE", str);
						firstInsert = false;
					}
					else
						sb.AppendFormat(", `{0}` BOOLEAN NOT NULL DEFAULT FALSE", str);
				}
			}
			
			// cek elemen terakhir
			if (!CheckIfKeywordExists(stringEnum[stringEnum.Count-1]))
			{
				if (firstInsert)
				{
					sb.AppendFormat(" ADD (`{0}` BOOLEAN NOT NULL DEFAULT FALSE);", stringEnum[stringEnum.Count - 1]);
					firstInsert = false;
				}
				else
					sb.AppendFormat(", `{0}` BOOLEAN NOT NULL DEFAULT FALSE);", stringEnum[stringEnum.Count - 1]);
			}
			else
			{
				if (!firstInsert)
					sb.Append(");");
				else
					sb.Append(";");
			}

			ExecuteQuery(sb.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="URL"></param>
		/// <returns></returns>
		private bool CheckIfURLExists(string URL)
		{
			bool retval = false;

			using (MySqlCommand cmd = new MySqlCommand("check_url", SQLConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("url_target", URL);
				cmd.Parameters.AddWithValue("retval", MySqlDbType.Int32);
				cmd.Parameters["retval"].Direction = ParameterDirection.ReturnValue;
				
				try
				{
					cmd.ExecuteNonQuery();
					
					retval = Convert.ToBoolean(cmd.Parameters["retval"].Value);
				} 
				catch (MySqlException)
				{
					throw;
				}
			}

			return retval;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		private bool CheckIfKeywordExists(string word)
		{
			bool retval = false;

			using (MySqlCommand cmd = new MySqlCommand("check_keyword", SQLConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("keyword", word);
				cmd.Parameters.AddWithValue("retval", MySqlDbType.Int32);
				cmd.Parameters["retval"].Direction = ParameterDirection.ReturnValue;
				
				try
				{
					cmd.ExecuteNonQuery();

					retval = Convert.ToBoolean(cmd.Parameters["retval"].Value);
				} 
				catch (MySqlException)
				{
					throw;
				}
			}

			return retval;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="URL"></param>
		/// <param name="keyWords"></param>
		/// <param name="title"></param>
		private void addURLToTable(string URL, List<string> keyWords,
								   string title = "(no title)")
		{
			string tableName = ConfigurationManager.AppSettings["dbTable"];
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("INSERT INTO `{0}` (`url`, `title`", tableName);
			int max = keyWords.Count;

			foreach (string keyword in keyWords)
			{
				sb.AppendFormat(", `{0}`", keyword);
			}

			sb.AppendFormat(") VALUES ('{0}', '{1}'", URL, title);
			for (int i = 0; i < max; i++)
			{
				sb.Append(", TRUE");
			}
			sb.Append(");");

			ExecuteQuery(sb.ToString());
		}

		private void ExecuteQuery(string query)
		{
			// eksekusi
			try 
			{ 
				MySqlCommand cmd = new MySqlCommand(query, SQLConn);
				cmd.ExecuteNonQuery();
			}
			catch (MySqlException)
			{
				throw;
			}
		}
	}
}