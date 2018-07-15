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
	public partial class CommonFunctions
	{
		public static XVar getLookupMainTableSettings(dynamic _param_lookupTable, dynamic _param_mainTableShortName, dynamic _param_mainField, dynamic _param_desiredPage = null)
		{
			#region default values
			if(_param_desiredPage as Object == null) _param_desiredPage = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic lookupTable = XVar.Clone(_param_lookupTable);
			dynamic mainTableShortName = XVar.Clone(_param_mainTableShortName);
			dynamic mainField = XVar.Clone(_param_mainField);
			dynamic desiredPage = XVar.Clone(_param_desiredPage);
			#endregion

			dynamic arr = XVar.Array(), effectivePage = null;
			if(XVar.Pack(!(XVar)(GlobalVars.lookupTableLinks.KeyExists(lookupTable))))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(GlobalVars.lookupTableLinks[lookupTable].KeyExists(MVCFunctions.Concat(mainTableShortName, ".", mainField)))))
			{
				return null;
			}
			arr = GlobalVars.lookupTableLinks[lookupTable][MVCFunctions.Concat(mainTableShortName, ".", mainField)];
			effectivePage = XVar.Clone(desiredPage);
			if(XVar.Pack(!(XVar)(arr.KeyExists(effectivePage))))
			{
				effectivePage = new XVar(Constants.PAGE_EDIT);
				if(XVar.Pack(!(XVar)(arr.KeyExists(effectivePage))))
				{
					if((XVar)(desiredPage == XVar.Pack(""))  && (XVar)(0 < MVCFunctions.count(arr)))
					{
						effectivePage = XVar.Clone(arr[0]);
					}
					else
					{
						return null;
					}
				}
			}
			return new ProjectSettings((XVar)(arr[effectivePage]["table"]), (XVar)(effectivePage));
		}
		public static XVar InitLookupLinks()
		{
			GlobalVars.lookupTableLinks = XVar.Clone(XVar.Array());
			return null;
		}
	}
}
