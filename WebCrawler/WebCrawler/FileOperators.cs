using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebCrawler
{
	public abstract class FileOperators
	{
		/// <summary>
		/// Melakukan pengisian kata-kata ke dalam List of String
		/// </summary>
		/// <param name="filename">nama file yang mau di-load</param>
		/// <param name="list">List of String</param>
		public static void LoadToListOfString(string filename, List<string> list)
		{
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
				
				Console.WriteLine(filename + ": read " + counter + " lines");

				file.Close();
			}
			catch (IOException ) // filenya ngga ada
			{
				throw ;
			}
			catch (Exception ) // error lain
			{
				throw ;
			}
		}
	}
}
