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

namespace System.Cor3.Mvc
{
	/// <summary>
	/// <para>
	/// to have files that were offline, presented by the server,
	/// we would need some sort of file server that would work in conjunction
	/// with the MediaFile.
	/// </para>
	/// <para>
	/// We also would require a file-server that would produce a buffered
	/// copy of the file.
	/// </para>
	/// </summary>
	public class MediaFile
	{
		static readonly string[] mediaAudioType = { "aac","mp3","m4a","ogg" };
		static readonly string[] mediaVideoType = { "m4v","mpeg","avi","flv","mov" };
		static readonly string[] mediaImageType = { "jpeg","jpg","gif","bmp","png","svg" };
		
		/// equivelant to directory
		public string category;
		public string mediafile;
		
		/// <summary>
		/// here we need to check to see that a file exists in a
		/// designated set of locations.
		/// </summary>
		/// <param name="mediaFile"></param>
		/// <returns></returns>
		virtual public string GetFileLocation(string mediaFile)
		{
			return mediafile;
		}

		string fileName { get { return mediafile; } }

		string extension
		{
			get
			{
				FileInfo fi = GetFileInfo(fileName);
				return fi.Exists ? fi.Extension : string.Empty;
			}
		}

		static public FileInfo GetFileInfo(string filename)
		{
			if (File.Exists(filename)) return new FileInfo(filename);
			return null;
		}
	}
}
