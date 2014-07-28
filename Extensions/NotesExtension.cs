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
	static public class NotesExtension
	{
		#region GetPrimeNotesTable
		static public DataSet GetPrimeNotesTable(this Controller controller, SqlDbA dataClass, int start, int length, string tablename, params string[] cols)
		{
			return controller.GetDataSet(
				dataClass,
				Resources.query_page_master.Replace("{topology}","")
				.Replace("{distinct}",""),
				start,length,tablename,cols);
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
		/// <param name="cols">the column names might conflict with the resulting sql, by default rows are not surrounded with brace chars ‘[’ and ‘]’.
		/// </param>
		/// <returns></returns>
		static public DataSet GetPrimeNotesTable(
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
		#endregion
	
	}
}
