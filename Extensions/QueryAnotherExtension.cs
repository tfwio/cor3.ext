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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{

	/*
	 * to spite any difference (this is old), this class should generally
	 * contain only functions that return data or something other then string.
	 * */
	
	/// <summary>
	/// <para>
	/// this class is very much based on the resources in the class its self,
	/// where the queries contain fields to be replaced.  See the Resources class
	/// for the queries, and the fields that are required to execute a query using
	/// these extensions.
	/// </para>
	/// <para>
	/// For the most part, the queries are intended to be provided to either
	/// a single record result or a paged set of values for long result-sets.
	/// </para>
	/// <para>
	/// in the future, I would like to see<br/>
	/// •	aplphabetized results<br/>
	/// •	flat-table results<br/>
	/// •	aplphabetized results<br/>
	/// </para>
	/// </summary>
	static public class QueryAnotherExtension
	{

		/// <summary>
		/// Controller Extension
		/// <para>&lt;TDB&gt; The type of database</para>
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="tableName">This table is used in the generated query.</param>
		/// <param name="distinct"></param>
		/// <returns></returns>
		static public long? GetRowCount<TDB>(this Controller controller, string tableName, bool distinct)
			where TDB:SqlDbA,new()
		{
			long? rowcount = null;
			using (
				DataSet ds = controller.ExecuteQuery<TDB>(
					"select{distinct} count(*) as [rowcount] from [{tableName}];"
						.Replace("{tableName}",tableName)
						.Replace("{distinct}", distinct ? " DISTINCT" : string.Empty),
					tableName))
			{
				if (ds.Tables[tableName].Rows.Count > 0)
				{
					rowcount = (long) ds.Tables[tableName].DefaultView[0].Row.Field<int>("rowcount");
				}
			}
			return rowcount;
		}

		static public DataSet QueryDataResult(
			this Controller controller,
			SqlDbA dataClass,
			string tablename,
			string query,
			long start, long length,
			params ColumnInfo[] cols)
		{
			// provide a table-query
			DataSet ds = controller.GetDataSet( dataClass, query, start, length, tablename, cols);
			return ds;
		}

		static public DataSet ExecuteQuery<TSql>(this Controller controller, string query, string tablename, params ColumnInfo[] cols)
			where TSql:SqlDbA, new()
		{
			DataSet ds = new DataSet("results");
			ds.Tables.Add(tablename);
			DataTable table = ds.Tables[tablename];
			foreach (ColumnInfo col in cols) { col.ToTable(table); }
			TSql dsource = new TSql();
			try
			{
				// controller.ViewData["Errors"] += "SqlDataAdapter".HtmlH1();
				using (SqlConnection connection = dsource.Connection)
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(query,connection))
					{
						connection.Open();
						adapter.SelectCommand.ExecuteNonQuery();
						adapter.Fill(ds,tablename);
						connection.Close();
					}
				}
			} catch (Exception error) {
				string errr = query.HtmlParagraph();
				controller.ViewData["Errors"] += "error".HtmlH1();
				controller.ViewData["Errors"] += "There was an error executing the query".HtmlTag("b").HtmlParagraph();
				controller.ViewData["Errors"] += error.Message.HtmlParagraph();
				controller.ViewData["Errors"] += errr;
			}
			return ds;
		}

		static public DataSet QueryInsert<TSql>(this Controller controller, string query, string tablename, params ColumnInfo[] cols)
			where TSql:SqlDbA, new()
		{
			DataSet ds = new DataSet("results");
			ds.Tables.Add(tablename);
			DataTable table = ds.Tables[tablename];
			foreach (ColumnInfo col in cols) { col.ToTable(table); }
			TSql dsource = new TSql();
			try
			{
				// controller.ViewData["Errors"] += "SqlDataAdapter".HtmlH1();
				using (SqlConnection connection = dsource.Connection)
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(query,connection))
					{
						adapter.InsertCommand = new SqlCommand(query,connection);
						connection.Open();
						adapter.InsertCommand.ExecuteNonQuery();
						adapter.Fill(ds,tablename);
						connection.Close();
					}
				}
			} catch (Exception error) {
				string errr = query.HtmlParagraph();
				controller.ViewData["Errors"] += "error".HtmlH1();
				controller.ViewData["Errors"] += "There was an error executing the query".HtmlTag("b").HtmlParagraph();
				controller.ViewData["Errors"] += error.Message.HtmlParagraph();
				controller.ViewData["Errors"] += errr;
			}
			return ds;
		}

		#region GetDataSet (paged)
		/// <summary>
		/// where in the Resources.query_page_master has empty: topology and distinct portions
		/// </summary>
		/// <returns></returns>
		static public DataSet ExecuteQueryPage<TSql>(this Controller controller, int start, int length, string tablename, params string[] cols)
			where TSql:SqlDbA, new()
		{
			TSql db = new TSql();
			DataSet ds = controller.GetDataSet(
				db,
				Resources.query_page_master.Replace("{topology}","")
				.Replace("{distinct}",""),
				start,length,tablename,cols);
			db = null;
			return ds;
		}
		/// <summary>
		/// this function will result in the generation and execution of two queries.
		/// <para>	1. info in the number of records in a table</para>
		/// <para>	2. the data in the table within the boundaries specified by start and length parameters</para>
		/// </summary>
		/// <param name="manager">(this) extention object</param>
		/// <param name="query">a string with table-fields, table-name, page-start and page-len all ready for pre-processing.</param>
		/// <param name="start">the row index from within the query's results.</param>
		/// <param name="length">the maximum number of rows retrieved.</param>
		/// <param name="tablename">the name of the table in the database—and stored in the dataset</param>
		/// <param name="cols">the column names might conflict with the resulting sql, by default rows are not surrounded with brace chars ‘[’ and ‘]’.
		/// </param>
		/// <returns></returns>
		static public DataSet GetDataSet(
			this Controller controller,
			SqlDbA dataClass,
			string query, int start, int length,
			string tablename, params string[] cols)
		{
			
			DataSet ds = new DataSet("dataTables");
			ds.Tables.Add(tablename);
			string tableFields = string.Join(",",cols);
			foreach (string col in cols)
			{
				ds.Tables[tablename].Columns.Add(col,typeof(string));
			}
			using (SqlConnection connection = dataClass.Connection) {
				using (SqlCommand command = new SqlCommand(
					query
					.Replace("{table-fields}",string.Format("{0}",tableFields))
					.Replace("{table-name}",string.Format("{0}",tablename))
					.Replace("{page-start}",string.Format("{0}",start))
					.Replace("{page-len}",string.Format("{0}",length)),
					connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						connection.Open();
						command.ExecuteNonQuery();
						adapter.Fill(ds.Tables[tablename]);
						connection.Close();
						
					}
//					DataClass.InsertNewsParams(row.Row,command);
				}
			}
			return ds;
		}

		/// <summary>
		/// Inserts a custom table (particularly for json output, and useage with
		/// jQuery.dataTables extension) that contains information about the feature
		/// table of information.  Then… a string is returned which consists of
		/// a portion of SQL which is simply a list of Fields to be selected.
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="tableName"></param>
		/// <param name="cols"></param>
		/// <returns></returns>
		static string AddColumnInfo(this DataSet ds, string tableName, params ColumnInfo[] cols)
		{
			ds.Tables.Add("cols");
			ds.Tables["cols"].Columns.Add("name",typeof(string));
			ds.Tables["cols"].Columns.Add("width",typeof(int));
			ds.Tables["cols"].Columns.Add("show",typeof(bool));
			ds.Tables["cols"].Columns.Add("iskey",typeof(bool));
			ds.Tables.Add(tableName);
			List<string> colnames = new List<string>();
			foreach (ColumnInfo col in cols)
			{
				// SqlServerFieldString formats the string with braces (i hope)
				colnames.Add(string.Format("[{0}]",col.Name));
				col.ToTable(ds.Tables[tableName]);
				col.ToColTable(ds.Tables["cols"]);
			}
			return string.Join(",",colnames.ToArray());
		}
		
		/// <summary>
		/// this function will result in the generation and execution of two queries.
		/// <para>1. info in the number of records in a table</para>
		/// <para>2. the data in the table within the boundaries specified by start and length parameters</para>
		/// </summary>
		/// <param name="manager">(this) extention object</param>
		/// <param name="query">a string with table-fields, table-name, page-start and page-len all ready for pre-processing.</param>
		/// <param name="start">the row index from within the query's results.</param>
		/// <param name="length">the maximum number of rows retrieved.</param>
		/// <param name="tablename">the name of the table in the database—and stored in the dataset</param>
		/// <param name="cols">ColumnInfo elements.</param>
		/// <returns>returns a dataset with a table named from the tablename param, and a table containing column informations</returns>
		static public DataSet GetDataSet(
			this Controller controller,
			SqlDbA dataClass,
			string query, long start, long length,
			string tablename, params ColumnInfo[] cols)
		{
			DataSet ds = new DataSet("dataTables");
			string colFields = ds.AddColumnInfo(tablename,cols);
			using (SqlConnection connection = dataClass.Connection) {
				using (SqlCommand command = new SqlCommand(
					query
					.Replace("{table-fields}",string.Format("{0}",colFields))
					.Replace("{table-name}",string.Format("[{0}]",tablename))
					.Replace("{page-start}",string.Format("{0}",start))
					.Replace("{page-len}",string.Format("{0}",length)),
					connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						connection.Open();
						command.ExecuteNonQuery();
						adapter.Fill(ds.Tables[tablename]);
						connection.Close();
						
					}
//					DataClass.InsertNewsParams(row.Row,command);
				}
			}
			return ds;
		}
		#endregion

		#region GetTableInfo


		#endregion

		#region GeneratePagedQuery, PagedQueryExecute

		static public DataSet PagedQueryExecute(
			this Controller controller,
			System.Cor3.Data.Engine.SqlDbA db,
			string tablename,
			string query,
			int start, int length,
			params ColumnInfo[] cols)
		{
			// provide a table-query
			DataSet ds = controller.GetPagedQuery( db, query, start, length, tablename, cols);
			return ds;
		}

		#endregion

		#region GetTotalRows
		static public long GetTotalRows(this Controller controller, SqlDbA DataClass, string tableName)
		{
			return controller.GetTotalRows(DataClass, tableName,"ReturnMe","TestVar");
		}
		static public long GetTotalRows(this Controller controller, SqlDbA DataClass, string tablename, string resultName, string varName)
		{
			DataSet ds = new DataSet("dataTables");
			ds.Tables.Add(tablename);
			ds.Tables[tablename].Columns.Add(resultName,typeof(long));
			using (SqlConnection connection = DataClass.Connection) {
				using (SqlCommand command = new SqlCommand(
					@"select {var-name}.CT as {result-name} from ( select Count(*) as CT from [{table-name}] ) as {var-name}"
					.Replace("{table-name}",string.Format("{0}",tablename))
					.Replace("{result-name}",string.Format("{0}",resultName))
					.Replace("{var-name}",string.Format("{0}",varName)),
					connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						connection.Open();
						command.ExecuteNonQuery();
						adapter.Fill(ds.Tables[tablename]);
						connection.Close();
					}
//					DataClass.InsertNewsParams(row.Row,command);
				}
			}
			
			long ivalue = (long)ds.Tables[tablename].Rows[0][0];
			ds.Tables[tablename].Clear();
			ds.Tables[tablename].Columns.Clear();
			ds.Tables.Clear();
			ds.Clear();
			ds = null;
			return ivalue;
		}
		#endregion
		
		#region GetPagedQuery, GetRequestInfo, QueryInnerJoin
		/// <summary>
		/// this function will result in the generation and execution of two queries.
		/// <para>	1. info in the number of records in a table</para>
		/// <para>	2. the data in the table within the boundaries specified by start and length parameters</para>
		/// </summary>
		/// <param name="manager">(this) database container</param>
		/// <param name="query">a string with table-fields, table-name, page-start and page-len all ready for pre-processing.</param>
		/// <param name="start">the row index from within the query's results.</param>
		/// <param name="length">the maximum number of rows retrieved.</param>
		/// <param name="tablename">the name of the table in the database—and stored in the dataset</param>
		/// <param name="cols">the column names might conflict with the resulting sql, by default rows are not surrounded with brace chars ‘[’ and ‘]’.
		/// </param>
		/// <returns>
		/// </returns>
		static public DataSet GetPagedQuery(
			this Controller controller,
			SqlDbA manager,
			string query, int start, int length,
			string tablename, params string[] cols)
		{
			DataSet ds = new DataSet("database");
			ds.Tables.Add(tablename);
			controller.Response.Write(tablename.HtmlH1());
			string tableFields = string.Join(",",cols);
			foreach (string col in cols)
			{
				ds.Tables[tablename].Columns.Add(col,typeof(string));
			}
			using (SqlConnection connection = manager.Connection) {
				using (SqlCommand command = new SqlCommand(
					query
					.Replace("{table-fields}",string.Format("{0}",tableFields))
					.Replace("{table-name}",string.Format("{0}",tablename.QBrace()))
					.Replace("{page-start}",string.Format("{0}",start))
					.Replace("{page-len}",string.Format("{0}",length)),
					connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						connection.Open();
						try{
							command.ExecuteNonQuery();
							adapter.Fill(ds.Tables[tablename]);
						} catch {
							controller.Response.Write("error executing query: {query}".Replace("{query}",query.HtmlParagraph()));
						}
						finally {
							controller.Response.Write("final".HtmlH1());
							connection.Close();
						}
						
					}
//					DataClass.InsertNewsParams(row.Row,command);
				}
			}
			return ds;
		}

		/// <summary>
		/// this function will result in the generation and execution of two queries.
		/// <para>1. info in the number of records in a table</para>
		/// <para>2. the data in the table within the boundaries specified by start and length parameters</para>
		/// </summary>
		/// <param name="manager">(this) extention object</param>
		/// <param name="query">a string with table-fields, table-name, page-start and page-len all ready for pre-processing.</param>
		/// <param name="start">the row index from within the query's results.</param>
		/// <param name="length">the maximum number of rows retrieved.</param>
		/// <param name="tablename">the name of the table in the database—and stored in the dataset</param>
		/// <param name="cols">ColumnInfo elements.</param>
		/// <returns>
		/// <para>
		/// • the resulting dataset that is returned should contain information regarding
		/// the featured result set ‘theDataSet.Tables[tableName]’
		/// </para>
		/// <para>• number of total rows in the result-set</para>
		/// <para>• number of visible rows</para>
		/// <para>• ‘name’</para>
		/// <para>• ‘width’</para>
		/// <para>• ‘show’</para>
		/// <para>• ‘iskey’</para>
		/// <para>• </para>
		/// <para>• </para>
		/// <para>• </para>
		/// <para>• </para>
		/// <seealso cref="System.Cor3.Mvc.Resources.query_page_master" />
		/// </returns>
		static public DataSet GetPagedQuery(
			this Controller controller,
			SqlDbA dataSource,
			string query, int start, int length,
			string tablename, params ColumnInfo[] cols)
		{
			DataSet ds = new DataSet("dataTables");
			ds.Tables.Add("cols");
			ds.Tables["cols"].Columns.Add("name",typeof(string));
			ds.Tables["cols"].Columns.Add("width",typeof(int));
			ds.Tables["cols"].Columns.Add("show",typeof(bool));
			ds.Tables["cols"].Columns.Add("iskey",typeof(bool));
			ds.Tables.Add(tablename);
			List<string> colnames = new List<string>();
			foreach (ColumnInfo col in cols)
			{
				// SqlServerFieldString formats the string with braces (i hope)
				colnames.Add(string.Format("[{0}]",col.Name));
				col.ToTable(ds.Tables[tablename]);
				col.ToColTable(ds.Tables["cols"]);
			}
			string colFields = string.Join(",",colnames.ToArray());
			string q = string.Empty;
			using (SqlConnection connection = dataSource.Connection) {
				using (SqlCommand command = new SqlCommand(
					q = query
					.Replace("{table-fields}",string.Format("{0}",colFields))
					.Replace("{table-name}",string.Format("[{0}]",tablename))
					.Replace("{page-start}",string.Format("{0}",start))
					.Replace("{page-len}",string.Format("{0}",length)),
					connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						connection.Open();
						try {
							command.ExecuteNonQuery();
							adapter.Fill(ds.Tables[tablename]);
						} catch (Exception ex) {
							controller.Response.Write("error".HtmlH1());
							controller.Response.Write( ex.Message.HtmlTag("pre"));
							controller.Response.Write( "source: {}".Replace("{}",ex.Source.HtmlTag("pre")));
							controller.Response.Write( "source: {}".Replace("{}",ex.ToString().HtmlTag("pre")));
							controller.Response.Write(string.Format("query: {0}",q).HtmlTag("pre"));
						} finally {
							connection.Close();
						}
					}
//					DataClass.InsertNewsParams(row.Row,command);
				}
			}
			return ds;
		}
		
		/// <summary>
		/// Prints some information on the request info.
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		static public string GetRequestInfo(this Controller page)
		{
			string section = string.Empty;
			foreach (string str in page.Request.Form.AllKeys)
			{
				section += string.Concat(
					"<b>",str,"</b>:" +
					"<blockquote>", page.Request.Params[str], "</blockquote>\r\n"
				);
			}
			return section;
		}
		internal const string query_inner_join = "INNER JOIN {table-ref} ON {field-source} = {field-destination}";
		static public string QueryInnerJoin(this Controller controller, string refTable, string fieldSource, string fieldDest)
		{
			return query_inner_join
				.Replace("{table-ref}",refTable)
				.Replace("{field-source}",fieldSource)
				.Replace("{field-destination}",fieldDest);
		}
		#endregion

	}
	

}
