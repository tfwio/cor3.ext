/* oio : 6/8/2011 11:20 PM */
using System;

namespace System.Cor3.Mvc
{
	public class ResourceCollection
	{
		
		internal const string field_brace = "[{0}]";
		internal const string field_squote = "'{0}'";
		internal const string field_dquote = @"""{0}""";
		
		internal const int tablewidth = -1;
		internal static readonly string table_template = @"
			<table width={tablewidth} id=""myDataTable"" class=""display"">
				<thead>
					<tr>${colnames}
					</tr>
				</thead>
				<tbody>${tablecols}
				</tbody>
			</table>".Replace("{tablewidth}",string.Format("{0}%",100));
		internal const string column_row_template = @"
					<th${{tokens}}>{0}</th>";
		internal const string row_template = @"
					<tr$(rowid)>${innercols}</tr>";
		internal const string col_template = "<td${{info}}>{0}</td>";
		internal const string styleFilter = @"
		<link type='text/css' rel='stylesheet' href='{0}' />";
		internal const string scriptFilter = @"
		<script type='text/javascript' language='javascript' src='{0}'></script>";
	}
}
