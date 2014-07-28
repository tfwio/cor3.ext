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
using System.Linq;
using System.Text;
using System.Web.Mvc;

using System.Data;
using System.Collections.Specialized;


namespace System.Cor3.Mvc
{
	static public class DataExtension
	{
		#region JSON Data Extensions
		static public string QuickMatch(this Controller controller, string tableName, string action, string sortCriteria, params ColumnInfo[] cols)
		{
			return controller.QuickMatch(tableName,action,string.Empty,sortCriteria,0,int.MaxValue,false, false,cols);
		}
		static public string QuickMatch(this Controller controller, string tableName, string action, string sortCriteria, bool topRec, params ColumnInfo[] cols)
		{
			return controller.QuickMatch(tableName,action,string.Empty,sortCriteria,0,int.MaxValue,topRec,false,cols);
		}
		static public string QuickMatch(this Controller controller, string tableName, string action, string sortCriteria, bool topRec, bool distinct, params ColumnInfo[] cols)
		{
			return controller.QuickMatch(tableName,action,string.Empty,sortCriteria,0,int.MaxValue,topRec,distinct,cols);
		}
		static public string QuickMatch(
			this Controller controller,
			string tableName, string defaultAction,
			string searchCriteria, string sortCriteria,
			int rowStart, int rowCount,
			bool topRecord, bool distinct,
			params ColumnInfo[] cols)
		{
			return controller.GetTableInfo (
				tableName,
				sortCriteria,	// sort
				searchCriteria,	// percent sign denotes a wildcard.
				string.Empty,	// references
				defaultAction, ColumnModeType.DQuote, rowStart, rowCount,
				topRecord, distinct,
				cols
			);
		}
		#endregion
	}
}
