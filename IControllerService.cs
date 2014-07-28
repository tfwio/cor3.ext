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
using System.Data;
using System.IO;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Cor3.Mvc
{

	public class IControllerService<TModel,TKeyIndex> 
	where TModel:IIT<TKeyIndex>, new()
	{
		virtual protected string SqlDefault { get { throw new NotImplementedException(); } }
		virtual protected string SqlSelect { get { return SqlDefault; } }
		virtual protected string SqlUpdate { get { throw new NotImplementedException(); } }
		virtual protected string SqlInsert { get { throw new NotImplementedException(); } }
		virtual protected string SqlAfterInsert { get { throw new NotImplementedException(); } }
		virtual protected string SqlDelete { get { throw new NotImplementedException(); } }
		virtual protected string SqlInsertSibling { get { throw new NotImplementedException(); } }
		virtual protected string SqlInsertChild { get { throw new NotImplementedException(); } }
		virtual protected string SqlInsertRoot { get { throw new NotImplementedException(); } }
		virtual protected List<string> Select(rfilter template) { throw new NotImplementedException(); }
		virtual protected DataSet DataSelect { get { throw new NotImplementedException(); } }
		virtual protected DataSet DataSelectDefault { get { throw new NotImplementedException(); } }
		virtual protected DataSet DataSelectChildren { get { throw new NotImplementedException(); } }
		virtual protected DataSet Delete(TModel model) { throw new NotImplementedException(); }
		virtual protected DataSet Update(TModel model) { throw new NotImplementedException(); }
		virtual protected DataSet Insert(TModel model) { throw new NotImplementedException(); }
		virtual protected bool UpdateModel(TModel model) { return false; }
	}

}
