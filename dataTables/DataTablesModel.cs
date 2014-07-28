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

namespace System.Cor3.Mvc
{
	public class DataTablesModel
	{
		string tableName = "ajax";
		public string TableName { get { return tableName; } set { tableName = value; } }
//		List<string> aCols = null;
		readonly int start = 0, count = 0;
		public int Count { get { return count; } }
		public int Start { get { return start; } }
		
		public DataTablesModel(int start, int count, params ColumnInfo[] columns)
		{
			this.start = start;
			this.count = count;
			List<string> tableColumns = null;
			tableColumns = new List<string>();
			foreach (ColumnInfo col in columns) tableColumns.Add(col.Name);
		}
	}
}
