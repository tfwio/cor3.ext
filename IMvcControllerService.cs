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
	public interface IMvcControllerService : IDisposable
	{
	ActionResult Respond();
	ActionResult Respond(InfoModel info);
	}
	public interface IMvcControllerService<TController,TModel> : IMvcControllerService
	where TController : Controller where TModel : class
	//		where TModel:IIT<TKeyIndex>, new()
	{
	ActionResult Respond(TModel model);
	}
}
