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
using System.Cor3.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{
	static public class QueryStringGenerationExtension
	{

		#region SqlPagedQuery

		static public string SqlPagedQuery(
			this Controller controller,
			string tableOrder,string tableConditions,string tableReferences,
			bool topRecord, bool distinct)
		{ return controller.SqlPagedQuery(null,tableOrder,tableConditions,tableReferences,topRecord,distinct); }
		/// <summary>
		/// Generate the query/string
		/// </summary>
		static public string SqlPagedQuery(
			this Controller controller,
			string defaultAction,
			string tableOrder, string tableConditions, string tableReferences,
			bool topRecord, bool distinct)
		{
			return controller.GetQueryString(
				Resources.query_page_master
				.Replace("{distinct}",distinct ? " DISTINCT" : string.Empty)
				.Replace("{topology}", topRecord ? " TOP 1" : string.Empty),
				tableOrder,tableConditions,tableReferences
			).Replace("{action-value}",defaultAction==null ? "" : defaultAction);
		}
		#endregion


		#region GeneratePagedQuery

		// this function also executes the query.
		[Obsolete("see SqlPagedQuery()")]
		static public string GeneratePagedQuery(
			this Controller controller,
			System.Cor3.Data.Engine.SqlDbA db,
			string tableName,
			string tableOrder,
			string tableConditions,
			string tableReferences,
			string defaultAction,
			ColumnModeType columnMode,
			int start, int length, params ColumnInfo[] cols)
		{
			return controller.GeneratePagedQuery( db, tableName, tableOrder, tableConditions,tableReferences,defaultAction,columnMode,start,length,false, false,cols);
		}

		// this function also executes the query.
		//     there is another function that does this exactly however is consolidated
		// to use a row2string formatter.
		[Obsolete("see SqlPagedQuery()")]
		static public string GeneratePagedQuery(
			this Controller controller,
			System.Cor3.Data.Engine.SqlDbA db,
			string tableName,
			string tableOrder, string tableConditions, string tableReferences,
			string defaultAction,
			ColumnModeType columnMode,
			int start, int length,
			bool topRecord, bool distinct,
			params ColumnInfo[] cols)
		{
			string newQuery = controller.GetQueryString(
				Resources.query_page_master
				.Replace("{distinct}",distinct ? " DISTINCT" : string.Empty)
				.Replace("{topology}", topRecord ? " TOP 1" : string.Empty),
				tableOrder,
				tableConditions,
				tableReferences
			).Replace("{action-value}",defaultAction);
			DataSet ds = controller.PagedQueryExecute( db, tableName, newQuery, start, length, cols);
			// virtually – our string builder
			List<string> l = new List<string>();
			List<string> l1 = new List<string>();
			// the string builder is used to:
			// build columns
			foreach (ColumnInfo c in cols) l.Add(string.Format(c.GetQuoteFilter(ColumnModeType.DQuote),string.Empty,c.Name));
			// dump columns: CDF, as above    —  string.Format
			// such as: "col1","col2"
			string jCols = string.Join(",",l.ToArray());
			l.Clear();
			// the string builder is used to:
			// build view
			foreach (DataRowView c in ds.Tables[tableName].DefaultView)
			{
				l1.Clear();
				foreach (ColumnInfo ci in cols)
				{
					l1.Add(
						string.Format(
							ci.GetQuoteFilter(columnMode),
							ci.Name,
							ci.ForceEscape ?  c[ci.Name].ToString().HtmlEscape() : c[ci.Name] ) );
				}
				l.Add( string.Join(",",l1.ToArray()).QBrace() );
			}
			// dump view
			string jData = string.Join(",",l.ToArray());
			string strout = string.Format(Resources.jsonFormat1, defaultAction, ds.Tables[tableName].Rows.Count, jCols, jData);
			// clean up
			l.Clear(); l = null;
			l1.Clear(); l1 = null;
			ds.Clear(); ds.Dispose(); ds = null;
			return strout;
		}
		#endregion

		
		#region QPageFilter, GetQueryString
		static public string QPageFilter(this string query, int start, int length, string tablename, params ColumnInfo[] cols)
		{
			List<string> colnames = new List<string>();
			foreach (ColumnInfo col in cols) { colnames.Add(col.Name.QBrace()); }

			string colFields = string.Join(",",colnames.ToArray());
			return query
				.Replace("{table-fields}",colFields)
				.Replace("{table-name}",tablename.QBrace())
				.Replace("{page-start}",string.Format("{0}",start))
				.Replace("{page-len}",string.Format("{0}",length));
		}

		/// <summary />
		/// <param name="controller"></param>
		/// <param name="inputString">the string being parsed</param>
		/// <param name="sortString">replaces a field ‘<pre>{table-sort}</pre>’.</param>
		/// <returns>replaces fields/ranges from input string</returns>
		static public string GetQueryString(this Controller controller, string inputString, string sortString)
		{
			return GetQueryString(controller,inputString,sortString,string.Empty,string.Empty);
		}

		/// <summary/>
		/// <param name="controller"></param>
		/// <param name="inputString">the string being parsed</param>
		/// <param name="sortString">replaces a field ‘<pre>{table-sort}</pre>’.</param>
		/// <param name="conditionString">replaces a field ‘<pre>{conditions}</pre>’.</param>
		/// <returns>replaces fields/ranges from input string</returns>
		static public string GetQueryString(
			this Controller controller,
			string inputString,
			string sortString,
			string conditionString,
			string tableReferences)
		{
			return inputString
				.Replace("{table-sort}",sortString)
				.Replace("{conditions}",conditionString)
				.Replace("{references}",tableReferences);
		}
		#endregion



	}
}
