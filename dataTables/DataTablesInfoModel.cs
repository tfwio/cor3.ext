/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace System.Cor3.Mvc
{
	/// <summary>
	/// this is a basic dataTables DataModel which consists of
	/// informtion that is used in jquery.dataTables jquery plugin.
	/// </summary>
	public class DataTablesInfoModel
	{
		public bool bRegex			{ get;set; }
		public bool bRegex_0		{ get;set; }
		public bool bRegex_1		{ get;set; }
		public bool bRegex_2		{ get;set; }
		public bool bSearchable_0	{ get;set; }
		public bool bSearchable_1	{ get;set; }
		public bool bSearchable_2	{ get;set; }
		public bool bSortable_0		{ get;set; }
		public bool bSortable_1		{ get;set; }
		public bool bSortable_2		{ get;set; }
		public int iColumns			{ get;set; }
		public int iDisplayLength	{ get;set; }
		public int iDisplayStart	{ get;set; }
		public int iSortCol_0		{ get;set; }
		public int iSortingCols		{ get;set; }
		public string sColumns		{ get;set; }
		public string sEcho			{ get;set; }
		public string sSearch		{ get;set; }
		public string sSearch_0		{ get;set; }
		public string sSearch_1		{ get;set; }
		public string sSearch_2		{ get;set; }
		public string sSortDir_0	{ get;set; }
		
	}
}
