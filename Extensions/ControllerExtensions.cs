/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 7/27/2011
 * Time: 10:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Web.Mvc;

namespace System.Cor3.Mvc
{
	/// <summary>
	/// Description of ControllerExtensions.
	/// </summary>
	static public class ControllerExtensions
	{
		/// <summary>
		/// Checks general parameters (not form parameters) for a value
		/// and asserts the value to the output parameter “value” if
		/// the parameter was found.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="varname"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		static public bool CheckParam(this Controller controller, string varname, out string value)
		{
			value = null;
			if (string.IsNullOrEmpty(controller.Request.Params[varname])) return false;
			value = controller.Request.Params[varname];
			return true;
		}
	}
}
