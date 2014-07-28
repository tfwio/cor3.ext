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
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{


	static public class XmlExtension
	{
		static public string Glob(this System.Web.Mvc.HtmlHelper control, string input)
		{
			return input.FilterRoot().FilterContent();
		}

		static public JsonResult PrintJson(this Controller controller, object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return controller.GetJson(data,"text/json",System.Text.Encoding.UTF8,JsonRequestBehavior.AllowGet);
		}
		static public JsonResult GetJson(this Controller controller, object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonResult
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior
			};
		}
		static public JsonResult GetJson(this Controller controller, object data, JsonRequestBehavior behavior)
		{
			return controller.GetJson(data, null, null, behavior);
		}

		static public string TableHelper(
			this Controller controller, DataView view,
			params ColumnInfo[] columnNames)
		{
			string sNews = ResourceCollection.table_template;
			string sCols = string.Empty;
			string sHead = string.Empty;
			foreach (ColumnInfo col in columnNames)
				if (col.IsVisible) sHead += string.Format(ResourceCollection.column_row_template,col.Name)
					.Replace("${tokens}",col.WidthAttribute);
			sNews = sNews.Replace("${colnames}",sHead);
			foreach (DataRowView note in view)
			{
				string colBuilder = string.Empty;
				string innerCols = ResourceCollection.row_template;
				string idcol = string.Empty;
				foreach (ColumnInfo col in columnNames)
				{
					if (col.IsId) idcol = note[col.Name].ToString();
					if (col.IsVisible) colBuilder += string.Format(ResourceCollection.col_template,note[col.Name])
						.Replace("${info}",string.Format("{0}",col.Info));
				}
				sCols += innerCols
					.Replace("$(rowid)", string.Format(" id=\"{0}\"", idcol))
					.Replace("${innercols}", colBuilder);
			}
			return sNews.Replace("${tablecols}",sCols);
		}
	}
}
