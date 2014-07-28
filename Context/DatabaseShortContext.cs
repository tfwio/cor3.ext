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
using System.Runtime.Remoting.Contexts;

namespace System.Cor3.Data
{
	public class DatabaseShortContext<TConnection,TCommand,TAdapter,TParameter>
		: IDisposable
		where TConnection:IDbConnection
		where TCommand:IDbCommand, new()
		where TAdapter:IDbDataAdapter,IDisposable,new()
		where TParameter:IDbDataParameter
	{
		void IDisposable.Dispose()
		{
			
		}
		internal const string
		datab = @"D:\DEV\WIN\CS_ROOT\Projects\Web.Etc\mvc.memos\newdb.sqlite",
		master_table = "sqlite_master",
		sql_table_select = "select * from [{0}] where type = 'table';";
		
		public string TableName { get; set; }
		public string TablePk { get; set; }
		public string TableTitle { get; set; }
		public string TableContent { get; set; }
		public DataRowView TableRow { get; set; }
		
		public QueryBasicContext<TConnection,TCommand,TAdapter,TParameter>.TCommandParamsCallback TableRowParams = null;
		public QueryBasicContext<TConnection,TCommand,TAdapter,TParameter>.TQueryInfoCallback TableAdapterParams = null;
	}

}
