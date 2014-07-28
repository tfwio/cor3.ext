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
	//	public delegate void rowFilter(SqlManager m, DataRowView row, string field);
	
	public class SQLiteContext : QueryBasicContext<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter, SQLiteParameter>
	{
		#region Category
		public override DataSet InsertCategory(string q)
		{
			category.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				category = db.Delete(Context.CategoryName,q,InsertCategoryAdapter,InsertCategoryFillOperation);
			return category;
		}
		public override SQLiteDataAdapter InsertCategoryAdapter(DbOp op, string query, SQLiteConnection connection)
		{
			return new SQLiteDataAdapter(query,connection);
		}
		public override int InsertCategoryFillOperation(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return 0;
		}
	
		public override DataSet DeleteCategory(string q)
		{
			category.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				category = db.Delete(Context.CategoryName,q,DeleteCategoryAdapter,DeleteCategoryFillOperation);
			return category;
		}
		public override SQLiteDataAdapter DeleteCategoryAdapter(DbOp op, string query, SQLiteConnection connection)
		{
			return new SQLiteDataAdapter(query,connection);
		}
		public override int DeleteCategoryFillOperation(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return 0;
		}
	
		public override DataSet UpdateCategory(string q)
		{
			category.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				category = db.Delete(Context.CategoryName,q,UpdateCategoryAdapter,UpdateCategoryFillOperation);
			return category;
		}
		public override SQLiteDataAdapter UpdateCategoryAdapter(DbOp op, string query, SQLiteConnection connection)
		{
			return new SQLiteDataAdapter(query,connection);
		}
		public override int UpdateCategoryFillOperation(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return 0;
		}
	
		public override DataSet SelectCategory(string q)
		{
			category.Tables.Clear();
			category.Tables.Add(Context.CategoryName);
			using (SQLiteDb db = new SQLiteDb(datafile))
				category = db.Select(Context.CategoryName,q,SelectCategoryAdapter,SelectCategoryFillOperation);
			return category;
		}
		public override SQLiteDataAdapter SelectCategoryAdapter(DbOp op, string query, SQLiteConnection connection)
		{
			throw new NotImplementedException();
		}
		public override int SelectCategoryFillOperation(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			A.Fill(D);
			return 0;
		}
		#endregion
		#region House
	
		public override DataSet Select(string q)
		{
			if (data==null) data=new DataSet();
			data.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				data = db.Select(Context.TableName,q,a_sel,f_sel);
			return data;
		}
		public override SQLiteDataAdapter  a_sel(DbOp op, string query, SQLiteConnection connection)
		{
			return new SQLiteDataAdapter(query,connection);
		}
		public override int f_sel(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			A.Fill(D);
			return 0;
		}
	
		public override DataSet Insert(string q)
		{
			data.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				data = db.Insert(Context.TableName,q,a_ins,f_ins);
			return data;
		}
		public override SQLiteDataAdapter a_ins(DbOp op, string query, SQLiteConnection connection)
		{
			SQLiteDataAdapter A = new SQLiteDataAdapter(query,connection);
			A.InsertCommand = new SQLiteCommand(query,connection);
			return A;
		}
		public override int f_ins(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return 0;
		}
	
		public override DataSet Delete(string q)
		{
			data.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				data = db.Delete(Context.TableName,q,a_del,f_del);
			return data;
		}
		public override SQLiteDataAdapter a_del(DbOp op, string query, SQLiteConnection connection)
		{
			SQLiteDataAdapter A = new SQLiteDataAdapter(query,connection);
			A.DeleteCommand = new SQLiteCommand(query,connection);
			return A;
		}
		public override int f_del(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return 0;
		}
	
		public override DataSet Update(string q)
		{
			data.Tables.Clear();
			using (SQLiteDb db = new SQLiteDb(datafile))
				data = db.Update(Context.TableName,q,a_upd,f_upd);
			return data;
		}
		public override SQLiteDataAdapter a_upd(DbOp op, string query, SQLiteConnection connection)
		{
			SQLiteDataAdapter A = new SQLiteDataAdapter(query,connection);
			A.UpdateCommand = new SQLiteCommand(query,connection);
			return A;
		}
		public override int f_upd(SQLiteDataAdapter A, DataSet D, string tablename)
		{
			return -1;
		}
	
		#endregion
	
		public SQLiteContext(string path) : base(path)
		{
		}
	}
}
