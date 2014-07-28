/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Cor3.Data;
using System.Cor3.Data.Engine;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{
//	static public class MultiMarkDown
//	{
//		const string temp_foldername = "temp";
//		const string temp_filename = "temp.text";
//		public static string _mmd_path = "";
//		public static string mmd_path { get { return _mmd_path; } set { _mmd_path = value; } }
//		const string dir_mmd = @"D:\DEV\BIN\MMD";
//		const string app_mmd = @"multimarkdown.exe";
//		static readonly string dir_localappdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
//		static readonly string dir_appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
//		static readonly string file_local_temp = Path.Combine(Path.Combine(dir_localappdata,temp_foldername),temp_filename);
//		static readonly string file_temp = Path.Combine(Path.Combine(dir_appdata,temp_foldername),temp_filename);
//		static public string MultiMarkDownHtml(this string input)
//		{
//			mmd_load_content(input);
//			return mmd_new();
//		}
//		public static string Localfile { get; set; }
//		public static string mmd_new() { return mmd_new(null); }
//		public static string mmd_new(string fileinput) { return mmd_new(app_mmd,fileinput==null?file_local_temp:fileinput); }
//		public static string mmd_new(string m, string fileinput)
//		{
//			string stri = System.IO.File.ReadAllText(fileinput,System.Text.Encoding.UTF8);
//			FileInfo f = new FileInfo(fileinput);
//			string result = string.Empty;
//			ProcessStartInfo info = new ProcessStartInfo(m,fileinput);
//			info.CreateNoWindow = true;
//			info.UseShellExecute = false;
//			info.RedirectStandardOutput = true;
//			using (Process p = Process.Start(info))
//				using (StreamReader reader = p.StandardOutput)
//					result = reader.ReadToEnd();
//			return result;
//		}
//		public static string mmd_load_content(string input)
//		{
//			System.IO.File.WriteAllText(file_local_temp,input,System.Text.Encoding.Default);
//			return input;
//		}
//		public static string mmd_load()
//		{
//			string f = ControlUtil.FGet("*.text|*.text");
//			if (!string.IsNullOrEmpty(f)) return mmd_load(f);
//			return String.Empty;
//		}
//		public static string mmd_load(string fname)
//		{
//			string stro = string.Empty;
//			if (!string.IsNullOrEmpty(fname))
//			{
//				stro = System.IO.File.ReadAllText(Localfile = fname,System.Text.Encoding.UTF8);
//				System.IO.File.WriteAllText(file_local_temp,stro,System.Text.Encoding.Default);
//			}
//			return stro;
//		}
//	}
}
