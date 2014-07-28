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

namespace System
{
	/// <summary>
	/// <seealso cref="StringExtension" />
	/// </summary>
	static public class PhoneNumberParser
	{
		// religlio (seeking wisdom from your elders) — derivation to the word religion.
		#region phone
		public const string ph11 = (@"^(?<1>\d{1})?[-\( ]*?(?<2>\d{3})?[- ]*?(?<3>\d{3})?[- ]*(?<4>\d{4})$");
		public const string ph10 = (@"^(?<1>\d{3})?[- ]*?(?<2>\d{3})?[- ]*(?<3>\d{4})$");
		public const string ph07 = (@"^(?<1>\d{3})?[- ]*(?<2>\d{4})$");
		public const string ph_cvt11d	= "$1-$2-$3-$4";
		public const string ph_cvt11	= "$1 ($2) $3-$4";
		public const string ph_cvt10	= "($1) $2-$3";
		public const string ph_cvt10f	= "1 ($1) $2-$3";
		public const string ph_cvt10d	= "1-$1-$2-$3";
		public const string ph_cvt07	= "$1-$2";
		
		/// <summary>
		/// strips a digit string of tab characters, parenthesis and dashes.
		/// </summary>
		/// <param name="phone"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		static public string StripPhone(this string phone)
		{
			return phone
				.Replace(" ",	"")
				.Replace("\t",	"")
				.Replace("-",	"")
				.Replace("(",	"")
				.Replace(")",	"")
			;
		}
		static public string GetPhone(this string phone)
		{
			string xnumber = phone.StripPhone();
			if (xnumber.Length==11) return Regex.Replace(xnumber,ph11,ph_cvt11);
			else if (xnumber.Length==10) return Regex.Replace(xnumber,ph10,ph_cvt10);
			else if (xnumber.Length==7) return Regex.Replace(xnumber,ph07,ph_cvt07);
			return phone;
		}
		static public string GetPhoneP(this string phone)
		{
			string xnumber = phone.StripPhone();
			// if (xnumber.Length==10) xnumber = "1"+xnumber;
			if (xnumber.Length==11) return Regex.Replace(xnumber,ph11,ph_cvt11);
			else if (xnumber.Length==10) return Regex.Replace(xnumber,ph10,ph_cvt10f);
			else if (xnumber.Length==7) return Regex.Replace(xnumber,ph07,ph_cvt07);
			return phone;
		}
		static public string GetPhoneD(this string phone)
		{
			string xnumber = phone.StripPhone();
			// if (xnumber.Length==10) xnumber = "1"+xnumber;
			if (xnumber.Length==11) return Regex.Replace(xnumber,ph11,ph_cvt11d);
			else if (xnumber.Length==10) return Regex.Replace(xnumber,ph10,ph_cvt10d);
			else if (xnumber.Length==7) return Regex.Replace(xnumber,ph07,ph_cvt07);
			return phone;
		}
		#endregion
	}
}
