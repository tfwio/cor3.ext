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
using System.Cor3.Mvc;


namespace System.Cor3.Mvc
{
	[HandleError]
	public class RootController : Controller
	{
		// FIXME: CHANGE THIS!
		static internal string rootpath = "/prime";
		
		/// <summary>
		/// this is to be set in the initializer.
		/// </summary>
		public static string Rootpath { get { return rootpath; } set { rootpath=value; } }
		
		
		static internal readonly string contentpath = "/content";
		/// <summary>
		/// </summary>
		virtual protected void RenewScripts()
		{
//			ViewPage vp;
			Scripts.Clear();
			Styles.Clear();
			
			ViewData["scripts"] = this.GetApplication().GetScripts();
			ViewData["styles"] = this.GetApplication().GetStyles();
		}
		
		#region Dictionaries
		// where key=name, value=src
		protected internal Dictionary<string, string> Scripts = new Dictionary<string, string>();
		protected internal Dictionary<string, string> Styles = new Dictionary<string, string>();
		#endregion
		
		
	}
}
