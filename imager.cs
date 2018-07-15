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
		public XVar imager()
		{
			try
			{
				dynamic file = null, pdf = null, table = null, var_params = XVar.Array();
				table = XVar.Clone((XVar.Pack(pdf as object != null) ? XVar.Pack(var_params["table"]) : XVar.Pack(MVCFunctions.postvalue(new XVar("table")))));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if(XVar.Pack(!(XVar)(GlobalVars.gQuery as object != null)))
				{
					if(XVar.Pack(!(XVar)(GlobalVars.gSettings as object != null)))
					{
						GlobalVars.gSettings = XVar.Clone(new ProjectSettings((XVar)(CommonFunctions.GetTableByShort((XVar)(table)))));
					}
					GlobalVars.gQuery = XVar.Clone(GlobalVars.gSettings.getSQLQuery());
				}
				file = XVar.Clone(CommonFunctions.GetImageFromDB((XVar)(GlobalVars.gQuery), (XVar)(pdf as object != null), (XVar)((XVar.Pack(var_params as object != null) ? XVar.Pack(var_params) : XVar.Pack(XVar.Array())))));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
