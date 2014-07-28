/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace System.Cor3.Mvc
{
	public enum PhoneMode
	{
		/// <summary>
		/// such as ‘(100) 200-3000’.
		/// </summary>
		Parenthesis,
		/// <summary>
		/// such as ‘1 (200) 300-4000’.
		/// </summary>
		Parenthesis1,
		/// <summary>
		/// such as ‘100-200-3000’.
		/// </summary>
		Dashed,
		/// <summary>
		/// such as ‘1-200-300-4000’.
		/// </summary>
		Dashed1,
	}
}
