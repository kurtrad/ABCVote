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
		public XVar detreccount()
		{
			try
			{
				dynamic dInd = null, dSTable = null, dTable = null, mKeys = null, mSTable = null, mTable = null, output = null, pageObject = null, pageType = null, respObj = null, var_params = XVar.Array();
				XTempl xt;
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				mSTable = XVar.Clone(MVCFunctions.postvalue(new XVar("mSTable")));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(mSTable)))))
				{
					respObj = XVar.Clone(new XVar("success", false, "error", "Wrong master short table name"));
					MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(respObj)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", mSTable, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				dSTable = XVar.Clone(MVCFunctions.postvalue(new XVar("dSTable")));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(dSTable)))))
				{
					respObj = XVar.Clone(new XVar("success", false, "error", "Wrong detail short table name"));
					MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(respObj)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", dSTable, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
				{
					respObj = XVar.Clone(new XVar("success", false, "error", ""));
					MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(respObj)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				mKeys = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("mKeys")))));
				mTable = XVar.Clone(MVCFunctions.postvalue(new XVar("mTable")));
				dTable = XVar.Clone(MVCFunctions.postvalue(new XVar("dTable")));
				pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pageType")));
				xt = XVar.UnPackXTempl(new XTempl());
				var_params = XVar.Clone(new XVar("pageType", pageType));
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(mTable, "tName");
				var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				pageObject = XVar.Clone(new RunnerPage((XVar)(var_params)));
				dInd = new XVar(0);
				for(;dInd < MVCFunctions.count(pageObject.allDetailsTablesArr); dInd++)
				{
					if(pageObject.allDetailsTablesArr[dInd]["dDataSourceTable"] == dTable)
					{
						break;
					}
				}
				output = XVar.Clone(pageObject.countDetailsRecsNoSubQ((XVar)(dInd), (XVar)(mKeys)));
				respObj = XVar.Clone(new XVar("success", true, "recsCount", output));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respObj)));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
