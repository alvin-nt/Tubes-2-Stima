using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebCrawler
{
	static class FileOperators
	{
		/// <summary>
		/// Melakukan pengisian kata-kata ke dalam List of String
		/// </summary>
		/// <param name="filename">nama file yang mau di-load</param>
		/// <param name="list">List of String</param>
		public static void LoadToListOfString(string filename, List<string> list)
		{
			string className = MethodBase.GetCurrentMethod().DeclaringType.Name;

			try
			{
				StreamReader file = new StreamReader(filename);

				string line;
				int counter = 0;
				while ((line = file.ReadLine()) != null)
				{
					if (!String.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
					{
						list.Add(line);
						counter++;
					}
				}
				
				Console.WriteLine(className + ": read " + counter + " lines");

				file.Close();
			}
			catch (IOException ex) // filenya ngga ada
			{
				throw ex;
			}
			catch (Exception ex) // error lain
			{
				throw ex;
			}
		}
	}
}
