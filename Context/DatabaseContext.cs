/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 11/30/2011
 * Time: 14:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Cor3.Data.Engine;
using System.Cor3.Data.Map.Types;
using System.Cor3.Mvc;
using System.Data;
using System.Runtime.Remoting.Contexts;

namespace System.Cor3.Data
{

	public class DatabaseContext<TConnection,TCommand,TAdapter,TParameter>
		: DatabaseShortContext<TConnection,TCommand,TAdapter,TParameter>
		where TConnection:IDbConnection
		where TCommand:IDbCommand, new()
		where TAdapter:IDbDataAdapter,IDisposable,new()
		where TParameter:IDbDataParameter
	{
		
		const string query_statement_select = "SELECT @fields\nFROM @table@category@order";
		const string query_statement_update = "UPDATE @table\nset @fieldset\n@where";
		const string query_statement_insert = "INSERT INTO @table @fieldset";
		const string query_statement1 = "SELECT @fields\nFROM @table@filter@order";
		
		public string CategoryName { get; set; }
		public string CategoryTitle { get; set; }
		public string CategoryPk { get; set; }
		public string TableCategory { get; set; }
		
		public event EventHandler TableCategoryChanged;
		public string[] TableFields { get;set; }
		public OrderMode OrderMode { get; set; }
		public string OrderValue { get; set; }
	
		// we would like a data view for the Categories
		// we would like a data view for the Entries (dependent on default category?)
		public string SELECT { get { return statementSelect(); } }
		public string CATEGORIES { get { return statementCategories(); } }
		public string TABLES { get { return statementTables(); } }
//		public string UPDATE { get { return statementUpdate(); } }
		
		public string statementSelect()
		{
			string ordr = "\n".QOrderBy(OrderValue.QBrace(), OrderMode== OrderMode.Ascending);
			string catg = "\n".QWhere(TableCategory.QBrace().QEqual("@"+TableCategory));
			return query_statement_select
				//+"@fields"
				.Replace("@fields","*")
				//+"@table"
				.Replace("@table",TableName.QBrace())
				//+"@category"
				.Replace("@category",string.IsNullOrEmpty(TableCategory)?"":"\n".QWhere(TableCategory.QBrace().QEqual("@"+TableCategory)))
				//+"@order"
				.Replace("@order",string.IsNullOrEmpty(OrderValue)?"":ordr)
				;
		}
		public string statementCategories()
		{
			return query_statement_select
				//+"@fields"
				.Replace("@fields","*")
				//+"@table"
				.Replace("@table",CategoryName.QBrace())
				//+"@category"
//				.Replace("@category","".QWhere(CategoryName.QBrace().QEqual("@"+CategoryValue)))
				.Replace("@category","")
				//+"@order"
				.Replace("@order","\n".QOrderBy(CategoryTitle.QBrace(), OrderMode== OrderMode.Ascending))
				;
		}
		public string statementTables()
		{
			return query_statement1
				//+"@fields"
				.Replace("@fields","*")
				//+"@table"
				.Replace("@table",master_table.QBrace())
				//+"@category"
				.Replace("@category","\n".QWhere("type".QBrace().QEqual(master_table.QCurly())))
				//+"@order"
				.Replace("@order","")
				.Replace("@filter","")
				;
		}
		public string statementInsert()
		{
			return statementInsert(TableFields);
		}
		public string statementInsert(params string[] fields)
		{
			string fieldout = "@field";
			
			List<string> listc = new List<string>();
			List<string> listv = new List<string>();
			foreach (string field in fields)
			{
				listc.Add(string.Format("[{0}]",field));
			}
			foreach (string field in fields)
			{
				listv.Add(string.Format("@{0}",field));
			}
			
			return query_statement_insert
				//+"@fields"
//				.Replace("@field","*")
				.Replace(
					"@fieldset",
					string.Concat(
						"(\r\n	",
						string.Join(", ",listc.ToArray()),
						")\r\nVALUES (",
						string.Join(", ",listv.ToArray()),
						")"
					)
				)
				//+"@table"
				.Replace("@table",TableName.QBrace())
				//+"@category"
//				.Replace("@category","".QWhere(CategoryName.QBrace().QEqual("@"+CategoryValue)))
				.Replace("@category","")
				//+"@order"
				.Replace("@order","\n".QOrderBy(CategoryTitle.QBrace(), OrderMode== OrderMode.Ascending))
				;
		}
		public string statementUpdate(DataRowView row)
		{
			return statementUpdate(row,TableFields);
		}
		public string statementUpdate(DataRowView row, params string[] fields)
		{
			string fieldparam = "@fieldset";
			string fieldparams = fieldparam;
			string[] list = new string[fields.Length];
			for (int i=0; i < fields.Length; i++)
			{
				list[i] = string.Format("[{0}] = @{0}",fields[i]);
			}
			fieldparams = fieldparams.Replace("@fieldset","");
			return query_statement_update
				//+"@fields"
				.Replace("@fieldset", string.Join(",\n	",list))
				//+"@table"
				.Replace("@table",TableName.QBrace())
				//+"@category"
//				.Replace("@category","".QWhere(CategoryName.QBrace().QEqual("@"+CategoryValue)))
//				.Replace("@category","")
				//+"@order"
				.Replace("@where","".QWhere(this.TablePk.QBrace().QEqual(row[TablePk].ToString())))
				;
		}
		
		public void SetFields(params string[] TableFields)
		{
			this.TableFields = TableFields;
		}
		
		
		public DatabaseContext()
			: this(null,null,null,null,null,null,null,null,null,OrderMode.Undefined,null)
		{
		}
		public DatabaseContext(
			string TableName,
			string TablePk,
			string TableTitle,
			string TableFilterName,
			string TableFilterValue,
			string CategoryName,
			string CategoryTitle,
			string CategoryPk,
			string CategoryValue,
			OrderMode SortMode,
			string OrderValue
		)
		{
			this.TableName = TableName;
			this.TablePk = TablePk;
			this.TableTitle = TableTitle;
			this.CategoryName = TableFilterName;
			this.TableCategory = TableFilterValue;
			this.OrderMode = SortMode;
			this.OrderValue = OrderValue;
		}
	}

}
