/* oio : 6/8/2011 11:20 PM */
using System;

namespace Cor3Extensions
{
	/// <summary>
	/// <seealso cref="ParserExtension" />
	/// </summary>
	static public class StringExtension
	{
		// the two field_squote and field_dquote constants have been moved from 
		// resourcecollection.cs and put here.
		// check for redundence.
		internal const string field_squote = "'{0}'";
		internal const string field_dquote = @"""{0}""";
		
		static public string SpaceBefore(this string input) { return string.Format(" {0}",input); }
		/// <summary>
		/// adds a space after the input string
		/// </summary>
		/// <returns>the resulting string</returns>
		static public string Space(this string input)		{ return string.Format(" {0}",input); }
		/// <summary>
		/// alias for ‘Space()’
		/// </summary>
		/// <returns></returns>
		static public string SpaceAfter(this string input)	{ return input.Space(); }
		static public string CommaSp(this string input)		{ return string.Concat(", ", input); }
		static public string Comma(this string input)		{ return string.Concat(",", input); }
		static public string DQuote(this string input)		{ return string.Format(field_dquote,input); }
		static public string SQuote(this string input)		{ return string.Format(field_squote,input); }
		static public string Tab(this string input, int count) { string carrier = string.Empty; for (int i = 0;i < count; i++) carrier = string.Concat(input,"\t"); return carrier;  }
		static public string Tab(this string input)			{ return input.Tab(1); }
		static public string Tabify(this string input, int count) { string carrier = string.Empty; for (int i = 0;i < count; i++) carrier = string.Concat("\t",input); return carrier;  }
		static public string Tabify(this string input)		{ return input.Tab(1); }
		static public string BreakWin(this string input)	{ return string.Concat(input,"\r\n"); }
		static public string BreakX(this string input)		{ return string.Concat(input,"\n"); }
		static public string BreakM(this string input)		{ return string.Concat(input,"\r"); }
	}
}
