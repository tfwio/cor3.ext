/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{

	static public class VideoExtension
	{
		static public bool HasVideoFile(this Controller controller, out string cat, out string file)
		{
			cat = file = null;
			if (!controller.CheckParam("cat", out cat)) return false;
			if (!controller.CheckParam("file", out file)) return false;
			if (cat!="vid") return false;
			return true;
		}
		static public bool HasImageFile(this Controller controller, out string cat, out string file)
		{
			cat = file = null;
			if (!controller.CheckParam("cat", out cat)) return false;
			if (!controller.CheckParam("file", out file)) return false;
			if (cat!="img") return false;
			return true;
		}
		static public void GetVideoFile(this Controller controller, string file)
		{
			if (string.IsNullOrEmpty(file)) return;
			else if (!System.IO.File.Exists(file)) return;
			controller.ServeVideo(file);
		}
	
		static void ServeVideo(this Controller controller, string filename)
		{
			controller.ServeVideo(filename,4096*32,300);
		}
		static void ContentTypeFromVideoExtension(this Controller controller, FileInfo fi)
		{
			switch (fi.Extension)
			{
					case ".flv": controller.Response.ContentType = "video/x-flv"; break;
					case ".mov": controller.Response.ContentType = "video/quicktime"; break;
					case ".avi": controller.Response.ContentType = "video/avi"; break;
					case ".mp4": controller.Response.ContentType = "video/mp4"; break;
					case ".mpeg": controller.Response.ContentType = "video/mpeg"; break;
					default: break;
			}
		}
		static void ServeVideo(this Controller controller, string filename, int buffersize, int sleepInterval)
		{
			FileInfo fi = new FileInfo(filename);
			controller.ContentTypeFromVideoExtension(fi);
			controller.Response.Cache.SetCacheability(HttpCacheability.Public);
			controller.Response.Cache.SetLastModified(DateTime.Now);
	//			controller.Response.AppendHeader("Content-Type", "video/x-flv");
			controller.Response.AppendHeader("Content-Length", fi.Length.ToString());
			using (FileStream fstream = new FileStream(filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
			{
				long posNew = 0;
				long diff = fstream.Length % buffersize;
				byte[] buffer = new byte[buffersize];
				for (long pos = 0; pos < fstream.Length; pos += buffersize)
				{
					if (!controller.Response.IsClientConnected) goto end;
					if (pos >= fstream.Length) break;
					fstream.Seek(pos,SeekOrigin.Begin);
					posNew = pos+fstream.Read(buffer,0,buffersize);
					
					controller.Response.OutputStream.Write(buffer,0,buffersize);
					controller.Response.Flush();
					Threading.Thread.Sleep(sleepInterval);
				}
				fstream.Seek(posNew,SeekOrigin.Begin);
				Array.Resize(ref buffer,unchecked((int)diff-1));
				fstream.Read(buffer,0,unchecked((int)diff-1));
				controller.Response.BinaryWrite(buffer);
				controller.Response.Flush();
			end:
				// clear the data buffer
				Array.Clear(buffer,0,buffer.Length);
				buffer = null;
				
				if (sleepInterval > 0) Threading.Thread.Sleep(sleepInterval);
			}
		}
	
		static public void ServeImage(this Controller controller)
		{
			string cat,file;
			if (!controller.CheckParam("cat", out cat)) return;
			if (!controller.CheckParam("file", out file)) return;
			if (cat=="img") controller.ServeImage(file);
		}
		static void ServeImage(this Controller controller, string filename)
		{
			controller.ServeImage(filename,1024,900);
		}
		static void ServeImage(this Controller controller, string filename, int buffersize, int sleepInterval)
		{
			FileInfo fi = new FileInfo(filename);
			switch (fi.Extension)
			{
					case ".png": controller.Response.ContentType = "image/png"; break;
					case ".gif": controller.Response.ContentType = "image/gif"; break;
					case ".bmp": controller.Response.ContentType = "image/bmp"; break;
					case ".svg": controller.Response.ContentType = "image/svg+xml"; break;
				case ".jpg":
					case ".jpeg": controller.Response.ContentType = "image/jpeg"; break;
					default: break;
			}
			using (FileStream fstream = new FileStream(filename,FileMode.Open))
			{
				byte[] buffer = new byte[buffersize];
				long posNew = fstream.Position + buffersize;
				while ( ( posNew = fstream.Position + buffersize ) < fstream.Length )
				{
					fstream.Read(buffer,unchecked((int)fstream.Position),buffersize);
					controller.Response.BinaryWrite(buffer);
					controller.Response.Flush();
					Threading.Thread.Sleep(sleepInterval);
				}
				long diff = buffersize - fstream.Position;
				Array.Resize(ref buffer,unchecked((int)diff));
				fstream.Read(buffer,unchecked((int)fstream.Position),unchecked((int)diff));
				controller.Response.BinaryWrite(buffer);
				controller.Response.Flush();
				
				// clear the data buffer
				Array.Clear(buffer,0,buffer.Length);
				buffer = null;
				
				Threading.Thread.Sleep(sleepInterval);
			}
		}
	
		static public bool CheckParam(this Controller controller, string varname, out string value)
		{
			value = null;
			if (string.IsNullOrEmpty(controller.Request.Params[varname])) return false;
			value = controller.Request.Params[varname];
			return true;
		}
	
	}
}
