using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Form frm = new Form1();
			Application.Run(frm);
			//Console output will be redirected to "output" pane of Visual Studio.
		}

	}
}
