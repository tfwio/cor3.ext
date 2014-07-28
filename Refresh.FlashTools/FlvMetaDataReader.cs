/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/27/2011
 * Time: 4:41 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.IO;
namespace Refresh.FlashTools
{
	/// <summary>
	/// Reads the meta information embedded in an FLV file
	/// </summary>
	public class FlvMetaDataReader
	{
	    /// <summary>
	    /// Reads the meta information (if present) in an FLV
	    /// </summary>
	    /// <param name="path">The path to the FLV file</returns>
	    public static FlvMetaInfo GetFlvMetaInfo(string path)
	    {
	        if (!File.Exists(path))
	        {
	            throw new Exception(String.Format("File '{0}' doesn't exist for FlvMetaDataReader.GetFlvMetaInfo(path)", path));
	        }
	        bool hasMetaData = false;
	        double duration = 0;
	        double width = 0;
	        double height = 0;
	        double videoDataRate = 0;
	        double audioDataRate = 0;
	        Double frameRate = 0;
	        DateTime creationDate = DateTime.MinValue;
	        // open file 
	        FileStream fileStream = new FileStream(path, FileMode.Open);
	        try
	        {
	            // read where "onMetaData"
	            byte[] bytes = new byte[10];
	            fileStream.Seek(27, SeekOrigin.Begin);
	            int result = fileStream.Read(bytes, 0, 10);
	            // if "onMetaData" exists then proceed to read the attributes
	            string onMetaData = ByteArrayToString(bytes);
	            if (onMetaData == "onMetaData")
	            {
	                hasMetaData = true;
	                // 16 bytes past "onMetaData" is the data for "duration" 
	                duration = GetNextDouble(fileStream, 16, 8);
	                // 8 bytes past "duration" is the data for "width"
	                width = GetNextDouble(fileStream, 8, 8);
	                // 9 bytes past "width" is the data for "height"
	                height = GetNextDouble(fileStream, 9, 8);
	                // 16 bytes past "height" is the data for "videoDataRate"
	                videoDataRate = GetNextDouble(fileStream, 16, 8);
	                // 16 bytes past "videoDataRate" is the data for "audioDataRate"
	                audioDataRate = GetNextDouble(fileStream, 16, 8);
	                // 12 bytes past "audioDataRate" is the data for "frameRate"
	                frameRate = GetNextDouble(fileStream, 12, 8);
	                // read in bytes for creationDate manually
	                fileStream.Seek(17, SeekOrigin.Current);
	                byte[] seekBytes = new byte[24];
	                result = fileStream.Read(seekBytes, 0, 24);
	                string dateString = ByteArrayToString(seekBytes);
	                // create .NET readable date string
	                // cut off Day of Week
	                dateString = dateString.Substring(4);
	                // grab 1) month and day, 2) year, 3) time
	                dateString = dateString.Substring(0, 6) + " " + dateString.Substring(16, 4) + " " + dateString.Substring(7, 8);
	                // .NET 2.0 has DateTime.TryParse
	                try
	                {
	                    creationDate = Convert.ToDateTime(dateString);
	                }
	                catch { }
	            }
	        }
	        catch (Exception)
	        {
	            // no error handling
	        }
	        finally
	        {
	            fileStream.Close();
	        }
	        return new FlvMetaInfo(hasMetaData, duration, width, height, videoDataRate, audioDataRate, frameRate, creationDate);
	    }
	    private static Double GetNextDouble(FileStream fileStream, int offset, int length)
	    {
	        // move the desired number of places in the array
	        fileStream.Seek(offset, SeekOrigin.Current);
	        // create byte array
	        byte[] bytes = new byte[length];
	        // read bytes
	        int result = fileStream.Read(bytes, 0, length);
	        // convert to double (all flass values are written in reverse order)
	        return ByteArrayToDouble(bytes, true);
	    }
	    private static string ByteArrayToString(byte[] bytes)
	    {
	        string byteString = string.Empty;
	        foreach (byte b in bytes)
	        {
	            byteString += Convert.ToChar(b).ToString();
	        }
	        return byteString;
	    }
	    private static Double ByteArrayToDouble(byte[] bytes, bool readInReverse)
	    {
	        if (bytes.Length != 8)
	            throw new Exception("bytes must be exactly 8 in Length");
	        if (readInReverse)
	            Array.Reverse(bytes);
	        return BitConverter.ToDouble(bytes, 0);
	    }
	}
}
