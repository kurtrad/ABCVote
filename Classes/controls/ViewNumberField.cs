using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using runnerDotNet;
namespace runnerDotNet
{
	public partial class ViewNumberField : ViewControl
	{
		protected static bool skipViewNumberFieldCtor = false;
		public ViewNumberField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic result = null;
			result = XVar.Clone(getTextValue((XVar)(data)));
			if(XVar.Pack(this.searchHighlight))
			{
				result = XVar.Clone(highlightSearchWord((XVar)(result), new XVar(false), (XVar)(data[this.field])));
			}
			return result;
		}
		public override XVar getTextValue(dynamic data)
		{
			return CommonFunctions.str_format_number((XVar)(data[this.field]), (XVar)(this.container.pSet.isDecimalDigits((XVar)(this.field))));
		}
		public override XVar getValueHighlighted(dynamic _param_value, dynamic _param_highlightData)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic highlightData = XVar.Clone(_param_highlightData);
			#endregion

			dynamic searchOpt = null, searchWord = null, searchWordArr = XVar.Array();
			searchWordArr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> _searchWord in highlightData["searchWords"].GetEnumerator())
			{
				dynamic curSearchWord = null, isStringOfNumbers = null, searchWordArray = null;
				curSearchWord = XVar.Clone(_searchWord.Value);
				isStringOfNumbers = XVar.Clone(MVCFunctions.preg_match(new XVar("/^[\\d]+$/"), (XVar)(_searchWord.Value)));
				if(XVar.Pack(!(XVar)(isStringOfNumbers)))
				{
					dynamic decimals = null, quantifier = null;
					decimals = XVar.Clone(this.container.pSet.isDecimalDigits((XVar)(this.field)));
					curSearchWord = XVar.Clone(CommonFunctions.str_format_number((XVar)(_searchWord.Value), (XVar)(decimals)));
					quantifier = XVar.Clone(MVCFunctions.Concat("{1,", decimals, "}"));
					if(decimals <= 1)
					{
						quantifier = new XVar("?");
					}
					curSearchWord = XVar.Clone(MVCFunctions.preg_replace((XVar)(MVCFunctions.Concat("/0", quantifier, "$/")), new XVar(""), (XVar)(_searchWord.Value)));
					curSearchWord = XVar.Clone(MVCFunctions.preg_replace(new XVar("/\\.$/"), new XVar(""), (XVar)(_searchWord.Value)));
				}
				searchWordArray = XVar.Clone(MVCFunctions.str_split((XVar)(curSearchWord)));
				curSearchWord = XVar.Clone(MVCFunctions.implode(new XVar("[^\\d]?"), (XVar)(searchWordArray)));
				searchWordArr.InitAndSetArrayItem(curSearchWord, null);
			}
			searchWord = XVar.Clone(MVCFunctions.implode(new XVar("|"), (XVar)(searchWordArr)));
			searchOpt = XVar.Clone(highlightData["searchOpt"]);
			switch(((XVar)searchOpt).ToString())
			{
				case "Equals":
					return addHighlightingSpan((XVar)(value));
				case "Starts with":
					return MVCFunctions.preg_replace((XVar)(MVCFunctions.Concat("/^(", searchWord, ")/")), (XVar)(addHighlightingSpan(new XVar("$1"))), (XVar)(value));
				case "Contains":
					return MVCFunctions.preg_replace((XVar)(MVCFunctions.Concat("/(", searchWord, ")/")), (XVar)(addHighlightingSpan(new XVar("$1"))), (XVar)(value));
				default:
					return value;
			}
			return null;
		}
	}
}
