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
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{

	static public class MVCApplicationExtensions
	{
		
		
		const string tpl_styles = "\r\n\t\t<link type='text/css' rel='stylesheet' href='{0}' />";
		const string tpl_scripts = "\r\n\t\t<script type='text/javascript' language='javascript' src='{0}'></script>";
		
		static public void AddViewScript(this Controller controller, string view, params string[] scripts)
		{
			controller.AddToView(view,tpl_scripts,scripts);
		}
		static public void AddViewStyle(this Controller controller, string view, params string[] css)
		{
			controller.AddToView(view,tpl_styles,css);
		}
		
		static public void AddToView(this Controller controller, string view, string template, params string[] scripts)
		{
			string returnvalue = null;
			List<string> list = new List<string>();
			
			foreach (string script in scripts) list.Add(string.Format(template, script.Glob()));
			string[] array = list.ToArray();
			list.Clear(); list = null;
			returnvalue = string.Join("",array);
			Array.Clear(array,0,array.Length); array = null;
			controller.ViewData[view] += returnvalue;
			returnvalue = null;
		}
	}
}
