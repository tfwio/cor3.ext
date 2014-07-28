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
using System.Cor3.Data;
using System.Data;
using System.IO;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Cor3.Mvc
{
	public delegate string rfilter(DataRowView row);

	// “application” module
	public class MvcApp : System.Web.HttpApplication
	{
		// There shall be only one app.
		static public MvcApp This;
		
		#region File Extensions
		
		static readonly public string[] ExtensionsForVideo =
			new string[] {
			"mov","mpeg","flv","mp4","mpv","webm","avi"
		};
		static readonly public string[] ExtensionsForAudio =
			new string[] {
			"mp3","m4a","aac" //,"ogg","flac"
		};
		static readonly public string[] ExtensionsForGraphics =
			new string[] {
			"gif","jpeg","jpg","png","bmp","svg" //,"ogg","flac"
		};
		
		#endregion

		#region File Dictionary Tables
		public Dictionary<string,string> Scripts	= new Dictionary<string, string>();
		public Dictionary<string,string> Styles	= new Dictionary<string, string>();
		public Dictionary<string,string> MediaLocations	= new Dictionary<string, string>();
		public Dictionary<string,string> AudioLocations	= new Dictionary<string, string>();
		public Dictionary<string,string> VideoLocations	= new Dictionary<string, string>();
		#endregion

		virtual public void AddPrimaryScripts()
		{
		}
		virtual public void AddPrimaryStyles()
		{
		}

		// is that right?
		virtual public void SubScripts()
		{
			AddPrimaryScripts();
			AddPrimaryStyles();
		}
		
		virtual public void InitializeScriptsAndStyles()
		{
			Scripts.Clear();
			Styles.Clear();
			// on memos mvc we want content, here we want scripts
			
			// I'm not quite clear weather or not it breaks the program not to have directories added to the following lists.
//			AddStyles ( "{root}/content/site.css" );
//			AddVideoLocations(
//				@"C:\downloads"
//			);
//			AddAudioLocations(
//				@"D:\Musiq\__radio\_select"
//			);
			
		}
		
		/**
		 * The following constrcuts allow you to create uri lists, however does not account
		 * (as it once had) for combining partial uri with the root-uri.
		 * 
		 * See the script-extensions (ScriptExtensions) for juxtiposition of the root-path to the string path
		 * provided to each of these methods.
		 */

		#region AddVideo
		protected void AddAudioLocation(string uriPath) { AddAudioLocation(System.IO.Path.GetFileNameWithoutExtension(uriPath),uriPath); }
		protected void AddAudioLocation(string name, string path) { AudioLocations.Add(name, (path.Glob())); }
		virtual public void AddAudioLocations(params string[] paths) { foreach (string path in paths) AddAudioLocation(path); }
		#endregion
		#region AddVideo
		protected void AddVideoLocation(string uriPath) { AddVideoLocation(System.IO.Path.GetFileNameWithoutExtension(uriPath),uriPath); }
		protected void AddVideoLocation(string name, string path) { if (Directory.Exists(path)) VideoLocations.Add(name, (path.Glob())); }
		virtual public void AddVideoLocations(params string[] scripts) { foreach (string script in scripts) AddVideoLocation(script); }
		#endregion
		#region AddScript
		protected void AddScript(string uriPath) { AddScript(System.IO.Path.GetFileNameWithoutExtension(uriPath),uriPath); }
		protected void AddScript(string name, string path) { Scripts.Add(name, (path.Glob())); }
		virtual public void AddScripts(params string[] scripts) { foreach (string script in scripts) AddScript(script); }
		#endregion
		#region AddStyle
		virtual public void AddStyles(params string[] styles) { foreach (string script in styles) AddStyle(script); }
		protected void AddStyle(string uriPath) { AddStyle(System.IO.Path.GetFileNameWithoutExtension(uriPath), uriPath); }
		protected void AddStyle(string name, string path) { Styles.Add(name, (path.Glob())); }
		#endregion

		virtual public void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "News", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
		protected void Application_Start()
		{
			InitializeScriptsAndStyles();
			AreaRegistration.RegisterAllAreas();
			RegisterRoutes(RouteTable.Routes);
			This = this;
		}
	}
}
