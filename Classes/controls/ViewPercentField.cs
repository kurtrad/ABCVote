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
	public partial class ViewPercentField : ViewControl
	{
		protected static bool skipViewPercentFieldCtor = false;
		public ViewPercentField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic result = null;
			if((XVar)(data[this.field] == null)  || (XVar)(data[this.field] == ""))
			{
				return "";
			}
			result = XVar.Clone(MVCFunctions.Concat(data[this.field] * 100, "%"));
			if(XVar.Pack(this.searchHighlight))
			{
				result = XVar.Clone(highlightSearchWord((XVar)(result), new XVar(false), (XVar)(data[this.field])));
			}
			return result;
		}
		public override XVar getTextValue(dynamic data)
		{
			if((XVar)(data[this.field] != null)  && (XVar)(data[this.field] != ""))
			{
				return MVCFunctions.Concat(data[this.field] * 100, "%");
			}
			return "";
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
				dynamic word = null, wordArr = null;
				word = XVar.Clone(_searchWord.Value * 100);
				word = XVar.Clone(MVCFunctions.preg_replace(new XVar("/0{0,2}$/"), new XVar(""), (XVar)(word)));
				wordArr = XVar.Clone(MVCFunctions.str_split((XVar)(word)));
				searchWordArr.InitAndSetArrayItem(MVCFunctions.implode(new XVar("[^\\d]?"), (XVar)(wordArr)), null);
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
