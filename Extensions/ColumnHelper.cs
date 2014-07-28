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
	static public class ColumnHelper
	{
		#region QueryJsonTable (helpers)
		static public string getQuoteFilter(object obj)
		{
			return getQuoteFilter(obj,@"""""");
		}
		static public string getQuoteFilter(object obj, string nullValue)
		{
			if (obj==DBNull.Value) return nullValue;
			else if (obj is string) return Resources.filter_dq_explicit_key;
			else if (obj is int) return Resources.filter_dq_explicit_key;
			else if (obj is int?) return Resources.filter_dq_explicit_key;
			else if (obj is float) return Resources.filter_dq_explicit_key;
			else if (obj is float?) return Resources.filter_dq_explicit_key;
			else if (obj is double) return Resources.filter_dq_explicit_key;
			else if (obj is double?) return Resources.filter_dq_explicit_key;
			else if (obj is long) return Resources.filter_dq_explicit_key;
			else if (obj is long?) return Resources.filter_dq_explicit_key;
			else if (obj is short) return Resources.filter_dq_explicit_key;
			else if (obj is short?) return Resources.filter_dq_explicit_key;
			else if (obj is byte) return Resources.filter_dq_explicit_key;
			else if (obj is byte?) return Resources.filter_dq_explicit_key;
			else if (obj is bool) return Resources.filter_dq_explicit_key;
			else if (obj is bool?) return Resources.filter_dq_explicit_key;
			else return Resources.filter_dq_nkey;
	//			if (typeC == ColumnModeType.DQuote   ) return row_DQuote;
	//			else if (typeC == ColumnModeType.DQuoteKey) return row_DQuoteKey;
	//			else if (typeC == ColumnModeType.SQuote   ) return row_SQuote;
	//			else if (typeC == ColumnModeType.SQuoteKey) return row_SQuoteKey;
	//			throw new ArgumentException(typeC.ToString());
		}
		#endregion
		#region Column Listing (private/static)
		static public string GetCdfCols(params ColumnInfo[] cols) { return GetCdfCols(ColumnModeType.DQuote,cols); }
		static public string GetCdfCols(ColumnModeType defaultMode, params ColumnInfo[] cols)
		{
			List<string> listColumns = new List<string>();
			// build columns
			foreach (ColumnInfo c in cols) listColumns.Add(string.Format(GetQuoteFilter(defaultMode),null,c.Name));
			string jCols = string.Join(",",listColumns.ToArray());
			listColumns.Clear();
			return jCols;
		}
		#endregion
		
		static public string GetQuoteFilter(this ColumnInfo col, ColumnModeType type)
		{
			if (type == ColumnModeType.DQuote) return Resources.filter_dq_nkey;
			else if (type == ColumnModeType.DQuoteKey) return Resources.filter_dq_key;
			else if (type == ColumnModeType.SQuote   ) return Resources.filter_sq_nkey;
			else if (type == ColumnModeType.SQuoteKey) return Resources.filter_sq_key;
			throw new ArgumentException(type.ToString());
		}
		static public string GetQuoteFilter(ColumnModeType type)
		{
			if (type == ColumnModeType.DQuote   ) return Resources.filter_dq_nkey;
			else if (type == ColumnModeType.DQuoteKey) return Resources.filter_dq_key;
			else if (type == ColumnModeType.SQuote   ) return Resources.filter_sq_nkey;
			else if (type == ColumnModeType.SQuoteKey) return Resources.filter_sq_key;
			throw new ArgumentException(type.ToString());
		}
	}
}
