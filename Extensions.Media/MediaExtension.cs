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
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc.Media
{
	static public class MediaExtension
	{
		#region const
		const string html_swfDynamic1 = @"


";
		const string html_swfDynamic2 = @"

		<div id=""myContent"">
			<h1>Alternative content</h1>
			<p><a href=""http://www.adobe.com/go/getflashplayer""><img src=""/content/get_flash_player.gif"" alt=""Get Adobe Flash player"" /></a></p>
		</div>

";
		const string html_ShockwaveObjectEmbed = @"<object
	id='{movie-id}'
	classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'
	codebase='http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab'
	width='{movie-width}' height='{movie-height}' >

    <param name='src' value='{movie-src}'/>
    <param name='flashVars' value='allowScriptAccess=always'/>

    <embed
     	type='application/x-shockwave-flash'
		name='{movie-id}'
		src='{movie-src}'
		pluginspage='http://www.adobe.com/go/getflashplayer'
		width='{embed-width}' height='{embed-height}' flashVars=''
		bgColor='{background-color}'>
		<param name='flashVars' value='allowScriptAccess=always'/>

	</embed>
    
</object>";
		
		#endregion

		// SWF v8 Embed Tag
		// ================

		#region ProvideSwfObject
		/// <summary>
		/// this translates the contstant above (swf embed tag – string) to a embed tag
		/// for asserting to any View.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="objId">the name of your object</param>
		/// <param name="paramSrc">the movie source URI</param>
		/// <param name="oWidth">the object tag's width attribute</param>
		/// <param name="oHeight">the object tag's height attribute</param>
		/// <param name="eWidth">the embed tag (within the object tag) is usually maximized within the object's contstraint, such as=‘100%’</param>
		/// <param name="eHeight">see eWidth</param>
		static public void ProvideSwfObject(
			this HtmlHelper controller,
			string objId, string paramSrc,
			string oWidth, string oHeight,
			string eWidth, string eHeight)
		{
			controller.ViewContext.Writer.Write(
				html_ShockwaveObjectEmbed
					.Replace("{movie-id}", objId)
					.Replace("{movie-width}", oWidth)
					.Replace("{movie-height}", oHeight)
					.Replace("{movie-src}", paramSrc)
					.Replace("{embed-width}", eWidth)
					.Replace("{embed-height}", eHeight)
					.Replace("{background-color}", "#FF0000")
			);
		}
		#endregion
		
		public static int SortFileList(this Controller controller, FileInfo f1, FileInfo f2)
		{
			return f1.Name.CompareTo(f2.Name);
		}

		public static void checkForMovieDir(this Controller controller)
		{
			if (string.IsNullOrEmpty(controller.Request.Params["mov-link"]))
			{
				controller.ViewData["mov-link"] = "nothing to list";
				return;
			}
			else
			{
				string movlink = controller.Request.Params["mov-link"];
				controller.ViewData["mov-link"] = movlink.HtmlH1();
				string directory = MvcApp.This.VideoLocations[movlink];
				DirectoryInfo di = new DirectoryInfo(directory);
				List<FileInfo> listA = new List<FileInfo>();
				foreach (string ext in MvcApp.ExtensionsForVideo)
				{
					FileInfo[] items = di.GetFiles("*.{ext}".Replace("{ext}",ext),SearchOption.AllDirectories);
					listA.AddRange(items);
					items = null;
				}
				listA.Sort(controller.SortFileList);
				List<string> list = new List<string>();
				foreach (FileInfo fi in listA)
				{
					list.Add(fi.Name.HtmlA(
						"javascript:messageIt('"+Uri.EscapeDataString(@"http://vaio/movies?cat=vid&file=".Glob()+fi.FullName.Replace("\\","/").Replace(@"'",@"\'")+@"')"),
						null,
						null,
						null,
						null
					).HtmlLi());
				}
				controller.ViewData["mov-link"] += @"{replace}".HtmlUl()
					.Replace("{replace}",string.Join("\r\n\t\t\t",list.ToArray()));
			}
			
		}

		public static void checkForMovies(this Controller controller)
		{
			List<string> vidDirs = new List<string>();
			List<string> dirLinks = new List<string>();
			foreach (KeyValuePair<string,string> kvp in MvcApp.This.VideoLocations)
			{
				vidDirs.Add(kvp.Value);
				dirLinks.Add(
					"{movie-name}"
						.HtmlA("?mov-link={val}".Replace("{val}",kvp.Key),null,"{movie-directory}",null,null)
						.Replace("{movie-name}",kvp.Key)
						.Replace("{movie-directory}",kvp.Value)
						.HtmlLi()
					);
			}
			controller.ViewData["movie-dirs"] = string.Join("\r\n",dirLinks.ToArray());;
		}
	
	}
}
