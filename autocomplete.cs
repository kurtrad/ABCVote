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
	public partial class GlobalController : BaseController
	{
		public XVar autocomplete()
		{
			try
			{
				dynamic contextParams = XVar.Array(), control = null, editControls = null, field = null, isExistParent = null, masterTable = null, mode = null, pageType = null, parentCtrlsData = null, respObj = null, shortTableName = null;
				ProjectSettings pSet;
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				shortTableName = XVar.Clone(MVCFunctions.postvalue(new XVar("shortTName")));
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", shortTableName, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				field = XVar.Clone(MVCFunctions.postvalue(new XVar("field")));
				if(GlobalVars.strTableName != "dbo._ABCSecurity")
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
					{
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
					if((XVar)((XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Edit"))))  && (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Add")))))  && (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
					{
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
				}
				else
				{
					dynamic checkField = null;
					checkField = new XVar(true);
					if(field == "username")
					{
						checkField = new XVar(false);
					}
					if(field == "password")
					{
						checkField = new XVar(false);
					}
					if(XVar.Pack(checkField))
					{
						if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
						{
							return MVCFunctions.GetBuferContentAndClearBufer();
						}
						if((XVar)((XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Edit"))))  && (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Add")))))  && (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
						{
							return MVCFunctions.GetBuferContentAndClearBufer();
						}
					}
				}
				pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pageType")));
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName)));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(pageType)));
				editControls = XVar.Clone(new EditControlsContainer(new XVar(null), (XVar)(pSet), (XVar)(pageType), (XVar)(GlobalVars.cipherer)));
				control = XVar.Clone(editControls.getControl((XVar)(field)));
				contextParams = XVar.Clone(XVar.Array());
				contextParams.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("data")))), "data");
				masterTable = XVar.Clone(MVCFunctions.postvalue(new XVar("masterTable")));
				if((XVar)(masterTable != XVar.Pack(""))  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(masterTable, "_masterRecordData"))))
				{
					dynamic masterControlsData = XVar.Array(), masterData = XVar.Array();
					masterData = XVar.Clone(XSession.Session[MVCFunctions.Concat(masterTable, "_masterRecordData")]);
					masterControlsData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("masterData")))));
					foreach (KeyValuePair<XVar, dynamic> mValue in masterControlsData.GetEnumerator())
					{
						masterData.InitAndSetArrayItem(mValue.Value, mValue.Key);
					}
					contextParams.InitAndSetArrayItem(masterData, "masterData");
				}
				RunnerContext.push((XVar)(new RunnerContextItem(new XVar(Constants.CONTEXT_ROW), (XVar)(contextParams))));
				parentCtrlsData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("parentCtrlsData")))));
				isExistParent = XVar.Clone(MVCFunctions.postvalue(new XVar("isExistParent")));
				mode = XVar.Clone(MVCFunctions.intval((XVar)(MVCFunctions.postvalue(new XVar("mode")))));
				respObj = XVar.Clone(new XVar("success", true, "data", control.getLookupContentToReload((XVar)(XVar.Equals(XVar.Pack(isExistParent), XVar.Pack("1"))), (XVar)(mode), (XVar)(parentCtrlsData))));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respObj)));
				RunnerContext.pop();
				MVCFunctions.Echo(new XVar(""));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
