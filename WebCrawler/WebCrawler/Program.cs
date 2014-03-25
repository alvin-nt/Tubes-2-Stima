using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebCrawler
{
	static class Program
	{
		/// <summary>
		/// Kode untuk inisialisasi console
		/// </summary>
		/// <returns></returns>
		
		public class NativeMethods 
		{
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool AllocConsole();
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			NativeMethods.AllocConsole();
			
			Crawler.CrawlSite();

			Console.ReadLine();
		}

	}
}
