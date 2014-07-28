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

namespace System.Cor3.Mvc
{
	static public class HtmlExtension
	{
		const string a_link = @" href=""{link}""";
		const string a_class = @" class=""{class}""";
		const string a_click = @" click=""{click}""";
		const string a_name = @" name=""{name}""";
		const string a_style = @" style=""{style}""";
		
		public const string tag_template = @"<{name}{tags} />";
		public const string tag_template_content = @"<{name}{tags}>{content}</{name}{tags}>";
		static public readonly string TextArea = "textarea".ToHtmlTag(
			"{value}",
			new pair("name","{name}"),
			new pair("rows","{rows}"),
			new pair("cols","{cols}")
		);
		static public string ToHtmlTag(this string tag, params pair[] values)
		{
			string items = tag_template.Replace("{name}",tag);
			foreach (pair value in values)
			{
				items += value.ToString();
			}
			return items.Replace("{tags}",items);
		}
		static public string ToHtmlTag(this string tag, string content, params pair[] values)
		{
			string items = tag_template_content.Replace("{name}",tag).Replace("{content}",content);
			List<string> list = new List<string>();
			foreach (pair value in values) list.Add( value.ToString() );
			string attrs = string.Join(" ",list.ToArray());
			if (string.IsNullOrEmpty(attrs)) return items.Replace("{tags}","");
			return items.Replace("{tags}",attrs);
		}
		
		static public void Tag(this System.Web.Mvc.HtmlHelper page, string tag, string message)
		{
			page.ViewContext.Writer.Write(string.Format("<{tag}>{0}</{tag}>".Replace("{tag}",tag),message));
		}
		static public string HtmlTag(this string content, string tagName, params pair[] attributes) {
			return tagName.ToHtmlTag(content,attributes);
		}
		
		#region Specific
		static public void HtmlH1(this System.Web.Mvc.HtmlHelper page, string message) { page.Tag("h1",message); }
		static public void HtmlH2(this System.Web.Mvc.HtmlHelper page, string message) { page.Tag("h2",message); }
		static public void HtmlParagraph(this System.Web.Mvc.HtmlHelper page, string message) { page.Tag("p",message); }
		static public void HtmlDiv(this System.Web.Mvc.HtmlHelper page, string message) { page.Tag("div",message); }
		static public void HtmlSpan(this System.Web.Mvc.HtmlHelper page, string message) { page.Tag("span",message); }
		
		static public string HtmlH1(this string input) { return input.HtmlTag("h1"); }
		static public string HtmlH2(this string input) { return input.HtmlTag("h2"); }
		static public string HtmlParagraph(this string input) { return input.HtmlTag("p"); }
		static public string HtmlDiv(this string input) { return input.HtmlTag("div"); }
		static public string HtmlSpan(this string input) { return input.HtmlTag("span"); }
		
		static public string HtmlA(this string input, string url)
		{
			return input.HtmlA(url,null,null,null,null);
		}
		static public string HtmlA(this string input, string url, string cls, string name, string style, string click)
		{
			return @"<a{link}{class}{name}{style}{click}>{input}</a>"
				.Replace("{link}", string.IsNullOrEmpty(url) ? " " : a_link.Replace("{link}",url) )
				.Replace("{class}", string.IsNullOrEmpty(cls) ? "" : a_class.Replace("{class}",cls) )
				.Replace("{name}", string.IsNullOrEmpty(name) ? "" : a_name.Replace("{name}",name) )
				.Replace("{style}", string.IsNullOrEmpty(style) ? "" : a_style.Replace("{style}",style) )
				.Replace("{click}", string.IsNullOrEmpty(click) ? "" : a_click.Replace("{click}",click) )
				.Replace("{input}",input);
		}
		static public string HtmlUl(this string input) { return input.HtmlTag("ul"); }
		static public string HtmlOl(this string input) { return input.HtmlTag("ol"); }
		static public string HtmlLi(this string input) { return input.HtmlTag("li"); }
		
		#endregion
		
	}
}
