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
	public partial class ViewCustomField : ViewControl
	{
		protected static bool skipViewCustomFieldCtor = false;
		public ViewCustomField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic fieldData = null, pageType = null, result = null;
			fieldData = XVar.Clone(data[this.field]);
			if(XVar.Pack(!(XVar)(this.displayField == null)))
			{
				fieldData = XVar.Clone(this.displayField);
			}
			pageType = XVar.Clone(this.container.pageType);
			if(XVar.Pack(this.pageObject))
			{
				if((XVar)(this.pageObject.mode == Constants.EDIT_INLINE)  && (XVar)(this.pageObject.pageType != Constants.PAGE_VIEW))
				{
					pageType = new XVar(Constants.PAGE_LIST);
				}
			}
			result = XVar.Clone(MVCFunctions.CustomExpression((XVar)(fieldData), (XVar)(data), (XVar)(this.field), (XVar)(pageType), (XVar)(this.container.tName)));
			if(XVar.Pack(this.searchHighlight))
			{
				result = XVar.Clone(highlightSearchWord((XVar)(result), new XVar(false), new XVar("")));
			}
			return result;
		}
		public override XVar getValueHighlighted(dynamic _param_value, dynamic _param_highlightData)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic highlightData = XVar.Clone(_param_highlightData);
			#endregion

			dynamic flags = null, prefix = null, searchOpt = null, tagPattern = null;
			searchOpt = XVar.Clone(highlightData["searchOpt"]);
			if(searchOpt == "Equals")
			{
				return addHighlightingSpan((XVar)(value));
			}
			flags = XVar.Clone((XVar.Pack(this.useUTF8) ? XVar.Pack("iu") : XVar.Pack("i")));
			prefix = XVar.Clone((XVar.Pack(searchOpt == "Starts with") ? XVar.Pack("^") : XVar.Pack("")));
			tagPattern = XVar.Clone(MVCFunctions.Concat("/(<[^=>]+\\s*(?:(?:[^=>]+=\\s*'[^']+'\\s*)|", "(?:[^=>]+=\\s*\"[^\"]+\"\\s*)", ")*>)/iU"));
			foreach (KeyValuePair<XVar, dynamic> searchWord in highlightData["searchWords"].GetEnumerator())
			{
				dynamic pattern = null, searchWordParts = XVar.Array();
				searchWordParts = XVar.Clone(MVCFunctions.preg_split((XVar)(tagPattern), (XVar)(searchWord.Value)));
				if(MVCFunctions.count(searchWordParts) == 1)
				{
					dynamic newSearchWord = null, replaced = null, res = null, valueArr = XVar.Array();
					res = new XVar("");
					replaced = new XVar(false);
					newSearchWord = XVar.Clone(MVCFunctions.preg_replace(new XVar("/^.*>|<.*$/U"), new XVar(""), (XVar)(searchWord.Value)));
					pattern = XVar.Clone(MVCFunctions.Concat("/", prefix, "(", MVCFunctions.preg_quote((XVar)(newSearchWord), new XVar("/")), ")/", flags));
					valueArr = XVar.Clone(getSplitStringWithCapturedDelimiters((XVar)(tagPattern), (XVar)(value)));
					foreach (KeyValuePair<XVar, dynamic> item in valueArr.GetEnumerator())
					{
						dynamic replacedItem = null;
						if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(item.Value)))))
						{
							continue;
						}
						if((XVar)((XVar)(item.Value[0] == "<")  || (XVar)(item.Value[MVCFunctions.strlen((XVar)(item.Value)) - 1] == ">"))  || (XVar)(replaced))
						{
							res = MVCFunctions.Concat(res, item.Value);
							continue;
						}
						if(XVar.Pack(!(XVar)(hasHTMLEntities((XVar)(item.Value)))))
						{
							replacedItem = XVar.Clone(MVCFunctions.preg_replace((XVar)(pattern), (XVar)(addHighlightingSpan(new XVar("$1"))), (XVar)(item.Value)));
						}
						else
						{
							replacedItem = XVar.Clone(highlightValueWithHTMLEntities((XVar)(item.Value), (XVar)(pattern)));
						}
						if((XVar)(searchOpt == "Starts with")  && (XVar)(item.Value != replacedItem))
						{
							replaced = new XVar(true);
						}
						res = MVCFunctions.Concat(res, replacedItem);
					}
					value = XVar.Clone(res);
					continue;
				}
				foreach (KeyValuePair<XVar, dynamic> item in searchWordParts.GetEnumerator())
				{
					if(XVar.Pack(MVCFunctions.trim((XVar)(item.Value))))
					{
						if((XVar)(item.Value[0] != "<")  && (XVar)(item.Value[MVCFunctions.strlen((XVar)(item.Value)) - 1] != ">"))
						{
							dynamic itemPattern = null, newItem = null;
							newItem = XVar.Clone(MVCFunctions.preg_replace(new XVar("/^.*>|<.*$/"), new XVar(""), (XVar)(item.Value)));
							itemPattern = XVar.Clone(MVCFunctions.preg_quote((XVar)(newItem), new XVar("/")));
							pattern = XVar.Clone(MVCFunctions.Concat("/(>[^>]*)(", itemPattern, ")([^<]*<)|^([^<>]*)(", itemPattern, ")(<)|(>)(", itemPattern, ")([^<>]*$)/U"));
							value = XVar.Clone(MVCFunctions.preg_replace((XVar)(pattern), (XVar)(MVCFunctions.Concat("$1", addHighlightingSpan(new XVar("$2")), "$3")), (XVar)(value)));
						}
					}
				}
			}
			return value;
		}
	}
}
