/*
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
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
	public class ControllerService : IMvcControllerService, IDisposable
	{
		void IDisposable.Dispose() { }
		
		protected Controller controller { get;set; }
		public ControllerService(Controller c)
		{
			this.controller = c;
		}
		virtual public ActionResult Respond()
		{
			throw new NotImplementedException();
		}
		virtual public ActionResult Respond(InfoModel model)
		{
			throw new NotImplementedException();
		}
	}
	public class ControllerService<T,M> : ControllerService
		where T:Controller
		where M:class
	{
		protected new T controller;
		virtual public ActionResult Respond(M model) { throw new NotImplementedException(); }
		public ControllerService(T c) : base(null)
		{
			this.controller = c;
		}
	}
}
