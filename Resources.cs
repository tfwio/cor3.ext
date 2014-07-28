#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion


namespace System.Cor3.Mvc
{
	static public class Resources
	{
//		protected static string rootpath = "/prime";
		static readonly string __ROOT__ = "/memos";
		public static  string Rootpath { get { return __ROOT__; } }
		static internal readonly string __CONTENT__ = "/content";
		public static string Contentpath {
			get { return __CONTENT__; }
		}

		#region String Filters
		public const string filter_dq_nkey  = @"""{1}""";
		public const string filter_dq_key = @"""{0}"": ""{1}""";
		public const string filter_sq_nkey  = @"'{1}'";
		public const string filter_sq_key = @"'{0}': '{1}'";
		public const string filter_dq_explicit_key  = @"{0}: ""{1}""";
		#endregion
		
		#region Date Formats
		internal const string fmt_date = "yy/MM/dd";
		internal const string fmt_time = "hh:mm tt";
		#endregion
		
		#region Html Link templates
		internal const string html_orderTag = "order_{0}";
		internal const string html_specialLink = "<a class=\"order\" id=\"{{url}}\"><li>{0} {1} d:{3} c:{4}</li></a>\r\n";
		#endregion

		#region Sql
		internal const string jsonPagedFormat1 = @"{{ ""start-offset"": {{start-offset}}, ""action"": ""{0}"", ""tresults"": ""{{xresults}}"", ""nresults"": ""{1}"",
""cols"": [{2}],
""data"": [{3}] }}";
		internal const string jsonFormat1 = @"{{ ""action"": ""{0}"", ""tresults"": ""{{xresults}}"", ""nresults"": ""{1}"", ""cols"": [{2}], ""data"": [{3}] }}";
		
		// /*, @NPage as NumberOfPages, RowNum*/
		// internal const string table_rows = "[news_id], [news_date], [news_detail]";
		internal const string query_page_master = @"
	DECLARE @RC INT
	DECLARE @PL INT
	DECLARE @RCOUNT INT
	DECLARE @NPage INT
	SET @RC = {page-start}
	SET @PL = {page-len}
	SET @RCOUNT = (SELECT COUNT(*) from {table-name})
	SET @NPage = @RCOUNT / @PL

	select{distinct}
		{table-fields}
	from (
		select{topology}
			{table-fields},
			ROW_NUMBER() over( {table-sort} ) as RowNum,
			@RCOUNT as [rtotal]
		from {table-name}
		{references}
		{conditions}
	) as TheDriver
	where TheDriver.RowNum <= @RC+@PL
	  AND TheDriver.RowNum >= @RC
";
		/// <summary>
		/// A simple JSON action (for jQuery.dataTables plugin)
		/// </summary>
		internal const string common_table = @"{{ action: ""{0}"", ""sEcho"": {1}, ""iDisplayStart"": {2}, ""iDisplayLength"": {3}, ""iTotalRecords"": {4}, ""iTotalDisplayRecords"":{5}, ";
		/// <summary>
		/// This was for the most part a test, and isn't used (to my knowlege)
		/// </summary>
		internal const string sqlqu = @"
SELECT TOP 100
	[order], driver1,
	customerContacts.Name, customerContacts.[addr-city],
	tDriver.[fname]+''''+tDriver.[lname] as Drivername
	FROM         [order] INNER JOIN
	                      customerContacts ON [order].customer = customerContacts.id
	                      inner join tDriver ON [order].driver1 = tDriver.driverno";
		#endregion

	}
}