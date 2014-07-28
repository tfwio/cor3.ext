/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 11/30/2011
 * Time: 14:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Cor3.Data.Engine;
using System.Cor3.Data.Map.Types;
using System.Cor3.Mvc;
using System.Data;
using System.Data.SQLite;

namespace System.Cor3.Data
{
	public enum OrderMode
	{
		Undefined,
		Ascending,
		Descending
	}
	public abstract class QueryBasicContext<TConnection,TCommand,TAdapter,TParameter>
		: IDisposable
		where TConnection:IDbConnection
		where TCommand:IDbCommand, new()
		where TAdapter:IDbDataAdapter,IDisposable,new()
		where TParameter:IDbDataParameter
	{
		//TAdapterCallback|RowParamCallback
		public delegate DataSet TSqlExecute(string q);
		public delegate TAdapter RowParamCallback(DbOp o, string q, TConnection c);
		public delegate int TFillCallback(TAdapter A, DataSet D, string tablename);
		
		public delegate TAdapter parameterize(DataRowView row);
		public delegate void TCommandParamsCallback(TCommand C);
		public delegate void TQueryInfoCallback(TCommand C, string table, string field, DataRowView row);

		internal protected DatabaseContext<TConnection,TCommand,TAdapter,TParameter> Context { get; set; }

		//;
		internal readonly string datafile = null;
		internal DataSet category = null;

		public DataSet Category {
			get { return category; }
			set { category = value; }
		}

		#region Callback
		public TSqlExecute					InsertCategoryCB;
		public SQLiteDb.RowParamCallback	InsertCategoryAdapterCB;
		public SQLiteDb.DataFillCallback	InsertCategoryFillOperationCB;
		
		public TSqlExecute					DeleteCategoryCB;
		public SQLiteDb.RowParamCallback	DeleteCategoryAdapterCB;
		public SQLiteDb.DataFillCallback	DeleteCategoryFillOperationCB;
		
		public TSqlExecute					UpdateCategoryCB;
		public SQLiteDb.RowParamCallback	UpdateCategoryAdapterCB;
		public SQLiteDb.DataFillCallback	UpdateCategoryFillOperationCB;
		
		public TSqlExecute					SelectCategoryCB;
		public SQLiteDb.RowParamCallback	SelectCategoryAdapterCB;
		public SQLiteDb.DataFillCallback	SelectCategoryFillOperationCB;
		#endregion
		
		#region Category
		abstract public DataSet InsertCategory(string q);
		abstract public TAdapter InsertCategoryAdapter(DbOp op, string query, TConnection connection);
		abstract public int InsertCategoryFillOperation(TAdapter A, DataSet D, string tablename);

		abstract public DataSet DeleteCategory(string q);
		abstract public TAdapter DeleteCategoryAdapter(DbOp op, string query, TConnection connection);
		abstract public int DeleteCategoryFillOperation(TAdapter A, DataSet D, string tablename);

		abstract public DataSet UpdateCategory(string q);
		abstract public TAdapter UpdateCategoryAdapter(DbOp op, string query, TConnection connection);
		abstract public int UpdateCategoryFillOperation(TAdapter A, DataSet D, string tablename);

		abstract public DataSet SelectCategory(string q);
		abstract public TAdapter SelectCategoryAdapter(DbOp op, string query, TConnection connection);
		abstract public int SelectCategoryFillOperation(TAdapter A, DataSet D, string tablename);
		#endregion
		
		internal DataSet data = null;
		
		public DataSet Data {
			get { return data; }
			set { data = value; }
		}

		#region House

		abstract public DataSet Select(string q);
		abstract public TAdapter a_sel(DbOp op, string query, TConnection connection);
		abstract public int f_sel(TAdapter A, DataSet D, string tablename);

		abstract public DataSet Insert(string q);
		abstract public TAdapter a_ins(DbOp op, string query, TConnection connection);
		abstract public int f_ins(TAdapter A, DataSet D, string tablename);

		abstract public DataSet Delete(string q);
		abstract public TAdapter a_del(DbOp op, string query, TConnection connection);
		abstract public int f_del(TAdapter A, DataSet D, string tablename);

		abstract public DataSet Update(string q);
		abstract public TAdapter a_upd(DbOp op, string query, TConnection connection);
		abstract public int f_upd(TAdapter A, DataSet D, string tablename);

		#endregion

		public string TableContent { get { return Context.TableContent; } }
		public string TableName { get { return Context.TableName; } }
		public string[] Fields { get { return Context.TableFields; } }
		
		
		public QueryBasicContext(string db)
		{
			datafile = db;
		}
		/// <summary>
		/// here, we'll clean up and info from the datasets.
		/// </summary>
		void IDisposable.Dispose()
		{
			category.Tables.Clear();
			category.Clear();
			category.Dispose();
			category = null;
			
			data.Tables.Clear();
			data.Clear();
			data.Dispose();
			data = null;
		}
		
	}

}
