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
		public XVar rte()
		{
			try
			{
				dynamic _connection = null, cfield = null, data = XVar.Array(), field = null, id = null, nHeight = null, nWidth = null, onsubmit = null, ptype = null, table = null;
				ProjectSettings pSet;
				table = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if((XVar)((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))  && (XVar)(MVCFunctions.postvalue("action") != "add"))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				ptype = XVar.Clone(MVCFunctions.postvalue(new XVar("ptype")));
				field = XVar.Clone(MVCFunctions.postvalue(new XVar("field")));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(ptype)));
				if((XVar)(!(XVar)(pSet.checkFieldPermissions((XVar)(field))))  && (XVar)(MVCFunctions.postvalue("action") != "add"))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				_connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
				data = new XVar(false);
				if(MVCFunctions.postvalue("action") != "add")
				{
					dynamic keys = XVar.Array(), keysArr = XVar.Array(), qResult = null, sql = null, where = null;
					keysArr = XVar.Clone(pSet.getTableKeys());
					keys = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> k in keysArr.GetEnumerator())
					{
						keys.InitAndSetArrayItem(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("key", k.Key + 1))), k.Value);
					}
					where = XVar.Clone(CommonFunctions.KeyWhere((XVar)(keys)));
					if(pSet.getAdvancedSecurityType() == Constants.ADVSECURITY_VIEW_OWN)
					{
						where = XVar.Clone(CommonFunctions.whereAdd((XVar)(where), (XVar)(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(GlobalVars.strTableName)))));
					}
					sql = XVar.Clone(GlobalVars.gQuery.gSQLWhere((XVar)(where)));
					qResult = XVar.Clone(_connection.query((XVar)(sql)));
					if(XVar.Pack(!(XVar)(qResult)))
					{
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
					data = XVar.Clone(qResult.fetchAssoc());
				}
				else
				{
					data = XVar.Clone(XVar.Array());
					data.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(GlobalVars.strTableName, "_", field, "_rte")], field);
				}
				nWidth = XVar.Clone(pSet.getNCols((XVar)(field)));
				nHeight = XVar.Clone(pSet.getNRows((XVar)(field)));
				id = XVar.Clone(MVCFunctions.postvalue(new XVar("id")));
				cfield = XVar.Clone(MVCFunctions.Concat("value_", MVCFunctions.GoodFieldName((XVar)(field)), "_", (XVar.Pack(!XVar.Equals(XVar.Pack(id), XVar.Pack(""))) ? XVar.Pack(id) : XVar.Pack("1"))));
				if(MVCFunctions.postvalue("browser") == "ie")
				{
					onsubmit = new XVar("onsubmit=\"updateRTEs();\"");
				}
				else
				{
					onsubmit = XVar.Clone(MVCFunctions.Concat("onsubmit=\"updateRTEs();return this.elements['", cfield, "'].value;\""));
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<html><body style=\"margin:0;\"><form name=\"rteform\" ", onsubmit, ">"));
				MVCFunctions.Echo(MVCFunctions.Concat("<script type=\"text/javascript\" src=\"", MVCFunctions.GetRootPathForResources(new XVar("include/rte/richtext.js")), "\"></script>\r\n"));
				MVCFunctions.Echo("<script language=\"JavaScript\" type=\"text/javascript\">");
				MVCFunctions.Echo("initRTE('include/rte/images/', 'include/rte/', '');\r\n");
				MVCFunctions.Echo(MVCFunctions.Concat("{var rte = new richTextEditor('", cfield, "');"));
				MVCFunctions.Echo(MVCFunctions.Concat("rte.width= ", nWidth, ";"));
				MVCFunctions.Echo(MVCFunctions.Concat("rte.height= ", nHeight, ";"));
				MVCFunctions.Echo("rte.html = '");
				if((XVar)(data)  && (XVar)(data[field] != null))
				{
					MVCFunctions.Echo(CommonFunctions.jsreplace((XVar)(data[field])));
				}
				MVCFunctions.Echo("';");
				MVCFunctions.Echo("rte.build();}");
				MVCFunctions.Echo("</script>");
				MVCFunctions.Echo("</form></body></html>");
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
