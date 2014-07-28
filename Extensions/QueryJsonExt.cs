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
	static public class QueryJsonExt
	{
		#region JsonTable (old?)
		static public string QueryJsonTable(
			this Controller controller,
			SqlDbA dataClass,
			string tableName,
			string tableOrder,
			string tableConditions,
			string tableReferences,
			string defaultAction,
			ColumnModeType columnMode,
			int start, int length, params ColumnInfo[] cols)
		{
			return controller.QueryJsonTable(dataClass,tableName,tableOrder,tableConditions,tableReferences,defaultAction,columnMode,start,length,false, false,cols);
		}
		/// <summary>
		/// currently this is used by JsonController.Drivers(T:Model)
		/// </summary>
		static public string QueryJsonTable(
			this Controller controller,
			SqlDbA dataClass,
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
			DataSet ds = controller.QueryDataResult( dataClass, tableName, newQuery, start, length, cols);
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
		// this gets a data-result
		#region QueryJsonTable2
		/// <summary>
		/// • This is an update to the previous Json Function, however I had noticed that in the case
		/// we retrieve large amounts of data, we were getting too much information, or providing the
		/// column names for each of the elements—for every element; so this has been corrected.
		/// <para>
		/// 	• Also it is worth noting that the data is now filtered by type.
		/// 	This feature is for testing.  I'm not sure if it is json complient.
		/// </para>
		/// </summary>
		static public string QueryJsonTable2<TSql>(
			this Controller controller,
			string tableName, string tableOrder, string tableConditions, string tableReferences,
			string defaultAction,
			ColumnModeType columnMode,
			long start, long length,
			bool topRecord, bool distinct,
			params ColumnInfo[] cols) where TSql:SqlDbA, new()
		{
			// create a new reference to our data profile
			TSql dataClass = new TSql();
			// initialization
			List<string> listColumns = new List<string>(), listInfo = null;
			// Generate the query/string
			string newQuery = controller.SqlPagedQuery( defaultAction, tableOrder, tableConditions, tableReferences, topRecord, distinct );
			// query the database for our dataset
			DataSet ds = controller.QueryDataResult( dataClass, tableName, newQuery, start, length, cols );
			// a list of the columns
			string jCols = ColumnHelper.GetCdfCols(cols);
			// build view
			foreach (DataRowView c in ds.Tables[tableName].DefaultView)
			{
				listInfo = JsonDataRowFormatter(c,cols);
				listColumns.Add( string.Join(",",listInfo.ToArray()).QBrace() );
				listInfo.Clear();
			}
			
			// dump view
			string jData = string.Join(",\n",listColumns.ToArray());
			string strout = string.Format(Resources.jsonPagedFormat1, defaultAction, ds.Tables[tableName].Rows.Count, jCols, jData)
				.Replace("{start-offset}",start.ToString());
			
			// clean up
			jCols = null; jData = null;
			listColumns.Clear(); listColumns = null;
			listInfo.Clear(); listInfo = null;
			ds.Clear(); ds.Dispose(); ds = null;
			// maybe we should abstract this.
			dataClass.GlobalData.Tables.Clear();
			dataClass.GlobalData.Clear();
			dataClass = null;
			return strout;
		}
		#endregion

		/// <summary>
		/// This was in the DataSetExtensions class.
		/// <para>It originated in the Prime-MVC Project.</para>
		/// <para>Found it's way into System.Cor3.Mvc</para>
		/// </summary>
		/// <param name="row"></param>
		/// <param name="cols"></param>
		/// <returns></returns>
		static public List<string> JsonDataRowFormatter(DataRowView row, params ColumnInfo[] cols)
		{
			List<string> l1 = new List<string>();
			foreach (ColumnInfo ci in cols)
			{
				l1.Add(
					string.Format(
						ColumnHelper.getQuoteFilter(row[ci.Name]),
						ci.Name,
						ci.ForceEscape ?  row[ci.Name].ToString().HtmlEscape() : row[ci.Name]
					)
					//.Replace("{xresults}",c["rtotal"]==DBNull.Value ? "-1": c["rtotal"].ToString())
				);
			}
			return l1;
		}
	}
}
