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
using System.Linq;
using System.Text;
using System.Web.Mvc;

using System.Data;
using System.Collections.Specialized;


namespace System.Cor3.Mvc
{
	static public class ScriptExtensions
	{
		/// <summary>
		/// This method converts the basic input to a root-bound path.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		static public string Glob(this string input) { return input.FilterRoot().FilterContent(); }
		static public string FilterRoot(this string input) { return input.Replace("{root}",RootController.Rootpath); }
		static public string FilterContent(this string input) { return input.Replace("{content}",RootController.contentpath); }

		//	=============================================================================
		
		static public string GetStyles(this MvcApp controller)
		{
			List<string> scriptout = new List<string>();
			foreach (KeyValuePair<string,string> item in controller.Styles)
				scriptout.Add(string.Format(ResourceCollection.styleFilter, item.Value));
			string[] scripts = scriptout.ToArray();
			scriptout.Clear();
			scriptout = null;
			string output = string.Join("",scripts);
			Array.Clear(scripts,0,scripts.Length);
			scripts = null;
			return output;
		}
		static public MvcApp GetApplication(this Controller controller)
		{
			return MvcApp.This;
		}

		//	=============================================================================

		static public string GetScripts(this MvcApp controller)
		{
			List<string> scriptout = new List<string>();
			foreach (KeyValuePair<string, string> item in controller.Scripts)
				scriptout.Add(string.Format(ResourceCollection.scriptFilter, (item.Value)));
			string[] scripts = scriptout.ToArray();
			scriptout.Clear();
			scriptout = null;
			string output = string.Join("", scripts);
			Array.Clear(scripts,0,scripts.Length);
			scripts = null;
			return output;
		}
	}
}
