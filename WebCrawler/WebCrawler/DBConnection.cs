using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WebCrawler
{
	/// <summary>
	/// Class yang menangani koneksi ke MySQL, berikut dengan operator-operatornya
	/// </summary>
	/// <remarks>ini masih pure copy-paste dari yang dikasih dariel, belum diadaptasi buat program</remarks>
	class DBConnection
	{
		#region Constants
		private MySqlConnection SQLConn;

		private string server;
		private string database;
		private string uid;
		private string password;
		
		#endregion

		public DBConnection()
		{
			Initialize();
		}

		public DBConnection(string serverName,
							string dbName,
							string uidIn,
							string dbPassword)
		{
			Initialize(serverName, dbName, uidIn, dbPassword);
		}

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

		public void Insert()
		{
			string query = "INSERT INTO tableinfo (name,age) VALUES ('John Smith', '33')";

			if (this.OpenConnection() == true)
			{
				MySqlCommand cmd = new MySqlCommand(query,SQLConn);

				cmd.ExecuteNonQuery();

				this.CloseConnection();
			}
		}

		public void Update()
		{
			string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name = 'John Smith'";

			if (this.OpenConnection()==true)
			{
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandText = query;
				cmd.Connection = SQLConn;

				cmd.ExecuteNonQuery();

				this.CloseConnection();
			}
		}

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

		public int Count()
		{
			string query = "SELECT Count(*) FROM tableinfo";
			int Count = -1;

			if(this.OpenConnection()==true)
			{
				MySqlCommand cmd = new MySqlCommand(query,SQLConn);
				Count = int.Parse(cmd.ExecuteScalar()+"");
				this.CloseConnection();

				return Count;
			}
			else
			{
				return Count;
			}
		}


	}
}
