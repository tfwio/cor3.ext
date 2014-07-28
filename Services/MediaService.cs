/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 7/27/2011
 * Time: 10:02 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Web.Mvc;
using System.Cor3.Mvc.Media;

namespace System.Cor3.Mvc.Services
{
	/// <summary>
	/// THIS CLASS IS WIP, designed to be integreated as a replacement to the EXTENSION CLASSES.
	/// This class should generally serve as a foundation for other media server classes
	/// such as a video file server, image file server etcetera.
	/// </summary>
	public class MediaService
	{
		static readonly int defaultBufferSize = 4096*32;
		static readonly int defaultBufferInterval = 0;

		#region This is a replacement for the Video and Media-Extensions
		protected bool HasFile(Controller controller, string fileCategory, out string cat, out string file)
		{
			cat = file = null;
			if (!controller.CheckParam("cat", out cat)) return false;
			if (!controller.CheckParam("file", out file)) return false;
			if (cat!=fileCategory) return false;
			return true;
		}
		protected bool HasVideoFile(Controller controller, out string cat, out string file)
		{
			return HasFile(controller,"vid", out cat, out file);
		}
		protected bool HasImageFile(Controller controller, out string cat, out string file)
		{
			return HasFile(controller,"img", out cat, out file);
		}

		public void GetVideoFile(Controller controller, string file)
		{
			if (string.IsNullOrEmpty(file)) return;
			else if (!System.IO.File.Exists(file)) return;
			controller.ServeVideo(file);
		}
	
		public void ServeVideo(Controller controller, string filename)
		{
			controller.ServeVideo(filename,defaultBufferSize,defaultBufferInterval);
		}
		#endregion
		
		public void CheckVideos(Controller controller)
		{
			string cat,file;
			if (HasVideoFile(controller, out cat, out file)) 
			{
				controller.GetVideoFile(file);
				return;
			}

			controller.checkForMovies();
			controller.checkForMovieDir();
		}
		
		public MediaService()
		{
		}
	}
}
