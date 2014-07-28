/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 7/7/2011
 * Time: 12:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace System.Cor3.Mvc.Service
{
	/// <summary>
	/// A requirement of this service is to provide a HTML document to a HTTP-Stream.
	/// </summary>
	public class PdfService
	{
		// FIXME: this is a conversion from a Controller function to a PDF Service.
		/// <summary>
		/// if the ID or PDFORDER variables are supplied with related
		/// orders that exist in the database, then a PDF file for the
		/// respective order is streamed to the browser.
		/// </summary>
		/// <param name="id">a ORDER ID</param>
		/// <param name="pdfOrder">Same as ID</param>
		/// <returns>True if a PDF file had been served.</returns>
		public Stream GetPdfOrder(RootController controller, string fileName)
		{
			string defaultfilename = " filename={file-name}.pdf";
			string outputfile = string.IsNullOrEmpty(fileName)
				? defaultfilename.Replace("{file-name}",DateTime.Now.ToString("yyyy.dd.MM.HH.mm.ss"))
				: defaultfilename.Replace("{file-name}",fileName);
			if (!string.IsNullOrEmpty(pdfOrder))
			{
				byte[] data = PrintManager.GetStream(pdfOrder);
				if (data!=null) // we only set metadata if the page has pdf/content.
				{
					controller.Response.ContentType = "application/pdf";
					controller.Response.AppendHeader("Content-Disposition",outputfile);
					controller.Response.BinaryWrite(data);
					Array.Clear(data,0,data.Length);
					data = null;
					return true;
				}
			}
			else if (!string.IsNullOrEmpty(id))
			{
				byte[] data = PrintManager.GetStream(id);
				if (data!=null)
				{
					controller.Response.ContentType = "application/pdf";
					controller.Response.BinaryWrite(data);
					Array.Clear(data,0,data.Length);
					data = null;
					return true;
				}
			}
			return false;
		}

		public PdfService()
		{
		}
	}
}
