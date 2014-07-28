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
using System.IO;
using System.Text.RegularExpressions;

namespace System.Cor3.Mvc
{

	
	public class ServeNavigation<T> where T:class
	{
		public int RecordOffset;
		public int ResordsPerPage;
		
		
		
		public T GetData()
		{
			return (T)null;
		}
	}

}
