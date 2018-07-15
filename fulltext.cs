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
		public XVar fulltext()
		{
			try
			{
				dynamic _connection = null, cViewControl = null, container = null, data = XVar.Array(), field = null, fieldValue = null, htmlEncodedValue = null, keys = XVar.Array(), keysArr = XVar.Array(), lookup = null, lookupInRegisterPage = null, mainField = null, mainTable = null, mode = null, pageType = null, qResult = null, returnJSON = null, searchClauseObj = null, sessionPrefix = null, sql = null, table = null, where = null;
				ProjectSettings pSet;
				mode = XVar.Clone(MVCFunctions.postvalue(new XVar("mode")));
				table = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
				field = XVar.Clone(MVCFunctions.postvalue(new XVar("field")));
				pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pagetype")));
				mainTable = XVar.Clone(MVCFunctions.postvalue(new XVar("maintable")));
				mainField = XVar.Clone(MVCFunctions.postvalue(new XVar("mainfield")));
				lookup = new XVar(false);
				if((XVar)(mainTable)  && (XVar)(mainField))
				{
					lookup = new XVar(true);
				}
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(CommonFunctions.GetTableByShort((XVar)(table))), (XVar)(pageType)));
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(CommonFunctions.GetTableByShort((XVar)(table))), (XVar)(pSet)));
				_connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
				lookupInRegisterPage = new XVar(false);
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(pSet.getListFields())))))
				{
					lookupInRegisterPage = new XVar(false);
				}
				if((XVar)((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))  && (XVar)(!(XVar)(lookupInRegisterPage)))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", ""));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(!(XVar)(pSet.checkFieldPermissions((XVar)(field)))))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", "Error: You have not permission for read this text"));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(!(XVar)(GlobalVars.gQuery.HasGroupBy())))
				{
					GlobalVars.gQuery.RemoveAllFieldsExcept((XVar)(pSet.getFieldIndex((XVar)(field))));
				}
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
				if((XVar)(!(XVar)(qResult))  || (XVar)(!(XVar)(data = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc()))))))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", "Error: Wrong SQL query"));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				fieldValue = XVar.Clone(data[field]);
				sessionPrefix = XVar.Clone(pSet.getOriginalTableName());
				if(mode == Constants.LIST_DASHBOARD)
				{
					sessionPrefix = XVar.Clone(MVCFunctions.Concat("Dashboard_", pSet.getOriginalTableName()));
				}
				if(XVar.Pack(lookup))
				{
					sessionPrefix = XVar.Clone(MVCFunctions.Concat(pSet.getOriginalTableName(), "_lookup_", mainTable, "_", mainField));
				}
				searchClauseObj = XVar.Clone(SearchClause.UnserializeObject((XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_advsearch")])));
				container = XVar.Clone(new ViewControlsContainer((XVar)(pSet), new XVar(Constants.PAGE_LIST), new XVar(null)));
				cViewControl = XVar.Clone(container.getControl((XVar)(field)));
				if((XVar)(cViewControl.localControlsContainer)  && (XVar)(!(XVar)(cViewControl.linkAndDisplaySame)))
				{
					cViewControl.localControlsContainer.fullText = new XVar(true);
				}
				else
				{
					cViewControl.container.fullText = new XVar(true);
				}
				if(XVar.Pack(searchClauseObj))
				{
					dynamic useViewControl = null;
					if((XVar)(searchClauseObj.bIsUsedSrch)  || (XVar)(useViewControl))
					{
						cViewControl.searchClauseObj = XVar.Clone(searchClauseObj);
						cViewControl.searchHighlight = new XVar(true);
					}
				}
				htmlEncodedValue = XVar.Clone(cViewControl.showDBValue((XVar)(data), new XVar("")));
				returnJSON = XVar.Clone(new XVar("success", true, "textCont", MVCFunctions.nl2br((XVar)(htmlEncodedValue))));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
