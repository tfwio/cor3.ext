/*
 * User: oio
 * Date: 6/8/2011
 * Time: 11:20 PM
 */
using System;

namespace System.Cor3.Mvc
{
	public enum ActionTypes
	{
		Default,
		Delete,
		Error,
		Edit,
		Select,
		SelectChildren,
		SelectStack,
		SelectSiblings,
		Update,
		Insert,
		InsertChild,
		InsertRoot,
		InsertSibling,
		AfterInsert,
	}
	public enum ActionResponseTypes
	{
		DefaultResponse,
		UpdateResponse,
		EditResponse,
		CreateResponse,
	}
	public enum FormInputType
	{
		Default,
		Radio,
		Checkbox,
		TextBox,
		Textarea,
		Option,
		Select,
	}
}
