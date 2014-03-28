using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;


namespace WebCrawler
{
	/// <summary>
	/// Class yang menangani koneksi ke MySQL, berikut dengan operator-operatornya
	/// </summary>
	/// <remarks>ini masih pure copy-paste dari yang dikasih dariel, belum diadaptasi buat program</remarks>
	class DBConnection
	{
		#region Private Instance Fields
		private MySqlConnection SQLConn;

		private string server;
		private string database;
		private string uid;
		private string password;
		private string table;
		#endregion

		private static const string DefaultTable = "crawlerIndex";
		/// <summary>
		/// Menginisialisasi DBConnection (konstruktor)
		/// </summary>
		public DBConnection()
		{
			Initialize();
		}

		/// <summary>
		/// Konstruktor DBConnection dengan parameter
		/// </summary>
		/// <param name="serverName"></param>
		/// <param name="dbName"></param>
		/// <param name="uidIn"></param>
		/// <param name="dbPassword"></param>
		public DBConnection(string serverName,
							string dbName,
							string uidIn,
							string dbPassword)
		{
			Initialize(serverName, dbName, uidIn, dbPassword);
		}

		/// <summary>
		/// Deklarasi dan Inisialisasi Variabel yang akan digunakan
		/// </summary>
		/// <param name="serverName"></param>
		/// <param name="dbName"></param>
		/// <param name="uidIn"></param>
		/// <param name="dbPassword"></param>
		private void Initialize(string serverName = "localhost",
								string dbName = "stima2",
								string uidIn = "stima2",
								string dbPassword = "stima2")
		{
			server = serverName;
			database = dbName;
			uid = uidIn;
			password = dbPassword;

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
				return false;
			}
		}

		/// <summary>
		/// Menutup koneksi untuk melepas sumber daya dan menyatakan bahwa koneksi tidak lagi dibutuhkan
		/// </summary>
		/// <returns></returns>
		private bool CloseConnection()
		{
			try
			{
				SQLConn.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Menghilangkan data yang ada di database
		/// </summary>
		public void Delete()
		{
			string query = "DELETE FROM tableinfo WHERE name='John Smith'";

			if (this.OpenConnection()==true)
			{
				MySqlCommand cmd = new MySqlCommand(query, SQLConn);
				cmd.ExecuteNonQuery();
				this.CloseConnection();
			}
		}

		/// <summary>
		/// Menyeleksi data dari query dan memasukkannya ke dalam list
		/// </summary>
		/// <returns></returns>
		public List< string >[] Select()
		{
			string query = "SELECT * FROM tableinfo";

			List< string >[] list = new List<string>[3];
			list[0] = new List<string>();
			list[1] = new List<string>();
			list[2] = new List<string>();

			if(this.OpenConnection()==true)
			{
				MySqlCommand cmd = new MySqlCommand(query,SQLConn);
				MySqlDataReader dataReader = cmd.ExecuteReader();

				while(dataReader.Read())
				{
					list[0].Add(dataReader["id"] + "");
					list[1].Add(dataReader["name"] + "");
					list[2].Add(dataReader["age"] + "");
				}

				dataReader.Close();

				this.CloseConnection();

				return list;
			}
			else
			{
				return list;
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
		

		/*
		using (DbConnection connection = new SqlConnection("Your connection string")) {
				connection.Open();
				using (DbCommand command = new SqlCommand("alter table [Product] add [ProductId] int default 0 NOT NULL")) {
				command.Connection = connection;
				command.ExecuteNonQuery();
			}
		}
		*/

		public void AddPageToTable(Page page)
		{
			string URL = page.URL.ToString();

			try {
				foreach (string keyword in page.Keywords)
				{
					AddColumn(keyword);
					SetKeywordTrue(URL, keyword);
				}
			} catch (MySqlException ex) {
				Console.WriteLine(ex);
			}
		}

		private bool CheckIfColumnExists(string word)
		{
			bool retval = false;

			using (MySqlCommand cmd = new MySqlCommand("check_column", SQLConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(new MySqlParameter("keyword", SqlDbType.VarChar) { 
									Value = word, Direction = ParameterDirection.Input 
									});
				cmd.Parameters.Add(retval);

				ExecuteProc(cmd);
				Console.WriteLine("Retval: " + retval);
			}

			return retval;
		}

		private bool AddColumn(string word)
		{
			bool retval = false;
			
			using (MySqlCommand cmd = new MySqlCommand("add_column", SQLConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;
				
				cmd.Parameters.Add(new MySqlParameter("keyword", SqlDbType.VarChar) { 
									Value = word, Direction = ParameterDirection.Input 
									});
				cmd.Parameters.Add(retval);

				ExecuteProc(cmd);
				Console.WriteLine("Retval: " + retval);
			}

			return retval;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="Keyword"></param>
		/// <returns></returns>
		private bool SetKeywordTrue(string Url, string Keyword)
		{
			bool retval = false;

			using (MySqlCommand cmd = new MySqlCommand("set_keyword_true", SQLConn))
			{
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(new MySqlParameter("url_in", SqlDbType.VarChar) { 
									Value = Url, Direction = ParameterDirection.Input 
									});
				cmd.Parameters.Add(new MySqlParameter("keyword", SqlDbType.VarChar) { 
									Value = Keyword, Direction = ParameterDirection.Input
									});
				cmd.Parameters.Add(retval);

				ExecuteProc(cmd);
				Console.WriteLine("Retval: " + retval);
			}

			return retval;
		}
		
		private void ExecuteProc(MySqlCommand cmd)
		{
			if (this.OpenConnection() == true)
			{
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (MySqlException)
				{
					throw;
				}
				finally
				{
					this.CloseConnection();
				}
			}
		}

	}
}

//bedanya addcolumn, checkifcolumnexists, sama setkeywordtrue