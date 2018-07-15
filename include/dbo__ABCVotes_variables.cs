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
	public static partial class dbo__ABCVotes_Variables
	{
		public static void Apply()
		{
			GlobalVars.strTableName = new XVar("dbo._ABCVotes");
			XSession.Session["OwnerID"] = XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")];
			GlobalVars.strOriginalTableName = new XVar("dbo._ABCVotes");
			GlobalVars.gstrOrderBy = new XVar("");
			if((XVar)(MVCFunctions.strlen((XVar)(GlobalVars.gstrOrderBy)))  && (XVar)(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(GlobalVars.gstrOrderBy), new XVar(0), new XVar(8)))) != "order by"))
			{
				GlobalVars.gstrOrderBy = XVar.Clone(MVCFunctions.Concat("order by ", GlobalVars.gstrOrderBy));
			}
			GlobalVars.gSettings = XVar.Clone(new ProjectSettings(new XVar("dbo._ABCVotes")));
			GlobalVars.gQuery = XVar.Clone(GlobalVars.gSettings.getSQLQuery());
			GlobalVars.eventObj = GlobalVars.tableEvents["dbo._ABCVotes"];
			GlobalVars.reportCaseSensitiveGroupFields = new XVar(false);
			GlobalVars.gstrSQL = XVar.Clone(GlobalVars.gQuery.gSQLWhere(new XVar("")));
		}
	}

}
