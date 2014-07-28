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
	public class InfoModel
	{
		public string mode { get; set; }
		public string title { get; set; }
		public string s { get; set; } // used for searching, or alphabet filter?
		public string o { get; set; } // used for ordering
		public string v { get; set; } // used for parameter “Value”
		public string view { get; set; } // used for view selectors
	
		public long? record { get; set; }
		public long? offset { get; set; }
		public long? length { get; set; }
		
		public DateTime? DateFrom { get;set; }
		public DateTime? DateTo { get;set; }
		
		static public InfoModel SetDefault(InfoModel model)
		{
			InfoModel im = new InfoModel();
			if (model.s!=null) im.s = model.s;
			if (model.o!=null) im.o = model.o;
			if (model.v!=null) im.v = model.v;
			if (model.view!=null) im.view = model.view;
			if (model.mode!=null) im.mode = model.mode;
			if (model.title!=null) im.title = im.title;
			if (model.record.HasValue) im.record = model.record;
			if (model.offset.HasValue) im.offset = model.offset;
			if (model.length.HasValue) im.length = model.length;
	//			if (model.DateFrom.HasValue && (model.DateFrom.Value.ToString("yyyy/MM/dd")=="0001/01/01"))
			return im;
		}
	
		public InfoModel EnsureDefault(InfoModel model) { return EnsureDefault(model,true); }
	
		/// <summary>
		/// If a value is NULL, then inherit the “model” default
		/// </summary>
		/// <param name="model"></param>
		/// <param name="ePos">boolean: Ensure Position – Forces position values to default (if true).</param>
		public InfoModel EnsureDefault(InfoModel model, bool ePos)
		{
			if (mode==null) mode = model.mode;
			if (title==null) title = model.title;
			if (s==null) s = model.s;
			if (o==null) o = model.o;
			if (v==null) v = model.v;
			if (view==null) view = model.view;
			if (ePos) EnsurePosition(model);
			return this;
		}
	
		public void EnsurePosition(InfoModel model)
		{
			if (!record.HasValue) record = model.record;
			if (!offset.HasValue) offset = model.offset;
			if (!length.HasValue) length = model.length;
		}
	}
}
