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
		public XVar lookupsuggest()
		{
			try
			{
				dynamic LookupSQL = null, LookupSQLTable = null, LookupType = null, contextParams = XVar.Array(), data = XVar.Array(), displayFieldIndex = null, displayFieldName = null, field = null, isExistParent = null, likeConditionField = null, likeField = null, likeWheres = XVar.Array(), linkAndDisplaySame = null, linkFieldIndex = null, linkFieldName = null, lookupCipherer = null, lookupConnection = null, lookupField = null, lookupIndices = XVar.Array(), lookupOrderBy = null, lookupPSet = null, lookupQueryObj = null, lookupTable = null, lwDisplayField = null, masterTable = null, pageType = null, parentCtrlsData = XVar.Array(), qResult = null, respObj = null, searchByLinkField = null, strLookupWhere = null, strUniqueOrderBy = null, table = null, value = null, values = XVar.Array(), var_response = XVar.Array();
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				table = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pageType")));
				GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(table)));
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName)));
				GlobalVars.gSettings = XVar.Clone(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(pageType)));
				field = XVar.Clone(MVCFunctions.postvalue(new XVar("searchField")));
				if(GlobalVars.gSettings.getEntityType() == Constants.titDASHBOARD)
				{
					dynamic dashFields = XVar.Array();
					dashFields = XVar.Clone(GlobalVars.gSettings.getDashboardSearchFields());
					table = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(dashFields[field][0]["table"])));
					GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(table)));
					field = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(dashFields[field][0]["field"])));
					if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
					{
						MVCFunctions.Echo(new XVar(0));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
					Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
						"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
					GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName)));
					GlobalVars.gSettings = XVar.Clone(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(pageType)));
				}
				masterTable = XVar.Clone(MVCFunctions.postvalue(new XVar("masterTable")));
				if((XVar)(masterTable != XVar.Pack(""))  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(masterTable, "_masterRecordData"))))
				{
					contextParams.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(masterTable, "_masterRecordData")], "masterData");
				}
				contextParams.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("data")))), "data");
				RunnerContext.push((XVar)(new RunnerContextItem((XVar)(pageType), (XVar)(contextParams))));
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
					dynamic checkResult = null;
					checkResult = new XVar(true);
					if(field == "username")
					{
						checkResult = new XVar(false);
					}
					if(field == "password")
					{
						checkResult = new XVar(false);
					}
					if(XVar.Pack(checkResult))
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
				isExistParent = XVar.Clone(MVCFunctions.postvalue(new XVar("isExistParent")));
				searchByLinkField = XVar.Clone(MVCFunctions.postvalue(new XVar("searchByLinkField")));
				parentCtrlsData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("parentCtrlsData")))));
				value = XVar.Clone(MVCFunctions.postvalue(new XVar("searchFor")));
				values = XVar.Clone((XVar.Pack(MVCFunctions.postvalue(new XVar("multiselection"))) ? XVar.Pack(CommonFunctions.splitvalues((XVar)(value))) : XVar.Pack(new XVar(0, value))));
				lookupField = new XVar("");
				foreach (KeyValuePair<XVar, dynamic> f in GlobalVars.gSettings.getFieldsList().GetEnumerator())
				{
					if((XVar)(MVCFunctions.GoodFieldName((XVar)(f.Value)) == field)  && (XVar)(GlobalVars.gSettings.getEditFormat((XVar)(f.Value)) == Constants.EDIT_FORMAT_LOOKUP_WIZARD))
					{
						LookupType = XVar.Clone(GlobalVars.gSettings.getLookupType((XVar)(f.Value)));
						if((XVar)(LookupType == Constants.LT_LOOKUPTABLE)  || (XVar)(LookupType == Constants.LT_QUERY))
						{
							lookupField = XVar.Clone(f.Value);
							break;
						}
					}
				}
				if(XVar.Pack(!(XVar)(lookupField)))
				{
					respObj = XVar.Clone(new XVar("success", false, "data", XVar.Array()));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respObj)));
					MVCFunctions.Echo(new XVar(""));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				lookupTable = XVar.Clone(GlobalVars.gSettings.getLookupTable((XVar)(lookupField)));
				linkFieldName = XVar.Clone(GlobalVars.gSettings.getLinkField((XVar)(lookupField)));
				displayFieldName = XVar.Clone(GlobalVars.gSettings.getDisplayField((XVar)(lookupField)));
				linkAndDisplaySame = XVar.Clone(displayFieldName == linkFieldName);
				if(LookupType == Constants.LT_QUERY)
				{
					lookupConnection = XVar.Clone(GlobalVars.cman.byTable((XVar)(lookupTable)));
				}
				else
				{
					dynamic connId = null;
					connId = XVar.Clone(GlobalVars.gSettings.getNotProjectLookupTableConnId((XVar)(lookupField)));
					lookupConnection = XVar.Clone((XVar.Pack(MVCFunctions.strlen((XVar)(connId))) ? XVar.Pack(GlobalVars.cman.byId((XVar)(connId))) : XVar.Pack(GlobalVars.cman.getDefault())));
				}
				lookupOrderBy = XVar.Clone(GlobalVars.gSettings.getLookupOrderBy((XVar)(lookupField)));
				if(lookupConnection.dbType == Constants.nDATABASE_MSSQLServer)
				{
					strUniqueOrderBy = XVar.Clone(lookupOrderBy);
				}
				if(LookupType == Constants.LT_QUERY)
				{
					lookupPSet = XVar.Clone(new ProjectSettings((XVar)(lookupTable), (XVar)(pageType)));
					lookupCipherer = XVar.Clone(new RunnerCipherer((XVar)(lookupTable)));
					lookupQueryObj = XVar.Clone(lookupPSet.getSQLQuery());
					if(XVar.Pack(GlobalVars.gSettings.getCustomDisplay((XVar)(lookupField))))
					{
						lookupQueryObj.AddCustomExpression((XVar)(displayFieldName), (XVar)(lookupPSet), (XVar)(GlobalVars.strTableName), (XVar)(lookupField));
					}
					lookupQueryObj.ReplaceFieldsWithDummies((XVar)(lookupPSet.getBinaryFieldsIndices()));
				}
				else
				{
					dynamic lwLinkField = null;
					LookupSQLTable = new XVar("SELECT ");
					lwLinkField = XVar.Clone(lookupConnection.addFieldWrappers((XVar)(GlobalVars.gSettings.getLinkField((XVar)(lookupField)))));
					if(XVar.Pack(GlobalVars.gSettings.isLookupUnique((XVar)(lookupField))))
					{
						LookupSQLTable = MVCFunctions.Concat(LookupSQLTable, "DISTINCT ");
					}
					LookupSQLTable = MVCFunctions.Concat(LookupSQLTable, GlobalVars.cipherer.GetLookupFieldName((XVar)(lwLinkField), (XVar)(lookupField), new XVar(null), new XVar(true)));
					if(lookupConnection.dbType == Constants.nDATABASE_MSSQLServer)
					{
						if((XVar)(strUniqueOrderBy)  && (XVar)(GlobalVars.gSettings.isLookupUnique((XVar)(lookupField))))
						{
							LookupSQLTable = MVCFunctions.Concat(LookupSQLTable, ",", lookupConnection.addFieldWrappers((XVar)(strUniqueOrderBy)));
						}
					}
					lwDisplayField = XVar.Clone(RunnerPage.sqlFormattedDisplayField((XVar)(lookupField), (XVar)(lookupConnection), (XVar)(GlobalVars.gSettings)));
					if(XVar.Pack(!(XVar)(linkAndDisplaySame)))
					{
						LookupSQLTable = MVCFunctions.Concat(LookupSQLTable, ",", (XVar.Pack(lwDisplayField == lwLinkField) ? XVar.Pack(GlobalVars.cipherer.GetFieldName((XVar)(lwDisplayField), (XVar)(lookupField), new XVar(true))) : XVar.Pack(lwDisplayField)));
					}
					LookupSQLTable = MVCFunctions.Concat(LookupSQLTable, " FROM ", lookupConnection.addTableWrappers((XVar)(lookupTable)), " ");
				}
				strLookupWhere = XVar.Clone(CommonFunctions.prepareLookupWhere((XVar)(lookupField), (XVar)(GlobalVars.gSettings)));
				if(LookupType == Constants.LT_QUERY)
				{
					dynamic secOpt = null;
					secOpt = XVar.Clone(lookupPSet.getAdvancedSecurityType());
					if(secOpt == Constants.ADVSECURITY_VIEW_OWN)
					{
						strLookupWhere = XVar.Clone(CommonFunctions.whereAdd((XVar)(strLookupWhere), (XVar)(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(lookupTable)))));
					}
				}
				if(XVar.Pack(strLookupWhere))
				{
					strLookupWhere = XVar.Clone(MVCFunctions.Concat(" (", strLookupWhere, ")  AND "));
				}
				if(LookupType == Constants.LT_QUERY)
				{
					if(XVar.Pack(GlobalVars.gSettings.getCustomDisplay((XVar)(lookupField))))
					{
						likeField = XVar.Clone((XVar.Pack(searchByLinkField) ? XVar.Pack(linkFieldName) : XVar.Pack(displayFieldName)));
					}
					else
					{
						likeField = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)((XVar.Pack(searchByLinkField) ? XVar.Pack(linkFieldName) : XVar.Pack(displayFieldName))), (XVar)(lookupConnection), (XVar)(lookupPSet), (XVar)(GlobalVars.cipherer)));
					}
				}
				else
				{
					likeField = XVar.Clone(GlobalVars.cipherer.GetFieldName((XVar)(lwDisplayField), (XVar)(lookupField)));
				}
				if(XVar.Pack(searchByLinkField))
				{
					likeConditionField = XVar.Clone((XVar.Pack(LookupType == Constants.LT_QUERY) ? XVar.Pack(linkFieldName) : XVar.Pack(lookupField)));
				}
				else
				{
					likeConditionField = XVar.Clone((XVar.Pack(LookupType == Constants.LT_QUERY) ? XVar.Pack(displayFieldName) : XVar.Pack(lookupField)));
				}
				likeWheres = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> fieldValue in values.GetEnumerator())
				{
					if(LookupType == Constants.LT_QUERY)
					{
						likeWheres.InitAndSetArrayItem(MVCFunctions.Concat(likeField, lookupCipherer.GetLikeClause((XVar)(likeConditionField), (XVar)(fieldValue.Value))), null);
					}
					else
					{
						likeWheres.InitAndSetArrayItem(MVCFunctions.Concat(likeField, GlobalVars.cipherer.GetLikeClause((XVar)(likeConditionField), (XVar)(fieldValue.Value))), null);
					}
				}
				strLookupWhere = MVCFunctions.Concat(strLookupWhere, MVCFunctions.implode(new XVar(" OR "), (XVar)(likeWheres)));
				if((XVar)(isExistParent)  && (XVar)(GlobalVars.gSettings.useCategory((XVar)(lookupField))))
				{
					dynamic parentWhereParts = XVar.Array();
					parentWhereParts = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> cData in GlobalVars.gSettings.getParentFieldsData((XVar)(lookupField)).GetEnumerator())
					{
						dynamic arLookupWhere = XVar.Array(), category = null, lookupCategory = XVar.Array();
						arLookupWhere = XVar.Clone(XVar.Array());
						category = XVar.Clone(parentCtrlsData[cData.Value["main"]]);
						lookupCategory = XVar.Clone((XVar.Pack(category == XVar.Pack("")) ? XVar.Pack(XVar.Array()) : XVar.Pack(CommonFunctions.splitvalues((XVar)(category)))));
						foreach (KeyValuePair<XVar, dynamic> arLookupCategory in lookupCategory.GetEnumerator())
						{
							dynamic catField = null, cvalue = null;
							cvalue = XVar.Clone(CommonFunctions.make_db_value((XVar)(cData.Value["main"]), (XVar)(arLookupCategory.Value)));
							if(XVar.Pack(lookupPSet))
							{
								catField = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)(cData.Value["lookup"]), (XVar)(lookupConnection), (XVar)(lookupPSet), (XVar)(GlobalVars.cipherer)));
							}
							else
							{
								catField = XVar.Clone(lookupConnection.addFieldWrappers((XVar)(cData.Value["lookup"])));
							}
							arLookupWhere.InitAndSetArrayItem(MVCFunctions.Concat(catField, "=", cvalue), null);
						}
						if(XVar.Pack(MVCFunctions.count(arLookupWhere)))
						{
							parentWhereParts.InitAndSetArrayItem(MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" OR "), (XVar)(arLookupWhere)), ")"), null);
						}
					}
					if(MVCFunctions.count(parentWhereParts) == MVCFunctions.count(GlobalVars.gSettings.getParentFieldsData((XVar)(lookupField))))
					{
						strLookupWhere = XVar.Clone(CommonFunctions.whereAdd((XVar)(strLookupWhere), (XVar)(MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" AND "), (XVar)(parentWhereParts)), ")"))));
					}
					else
					{
						respObj = XVar.Clone(new XVar("success", false, "data", XVar.Array()));
						MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respObj)));
						MVCFunctions.Echo(new XVar(""));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
				}
				if(XVar.Pack(MVCFunctions.strlen((XVar)(lookupOrderBy))))
				{
					lookupOrderBy = XVar.Clone(lookupConnection.addFieldWrappers((XVar)(lookupOrderBy)));
					if(XVar.Pack(GlobalVars.gSettings.isLookupDesc((XVar)(lookupField))))
					{
						lookupOrderBy = MVCFunctions.Concat(lookupOrderBy, " DESC");
					}
				}
				if(LookupType == Constants.LT_QUERY)
				{
					LookupSQL = XVar.Clone(lookupQueryObj.buildSQL_default((XVar)(strLookupWhere)));
					if(XVar.Pack(MVCFunctions.strlen((XVar)(lookupOrderBy))))
					{
						LookupSQL = MVCFunctions.Concat(LookupSQL, " ORDER BY ", lookupOrderBy);
					}
				}
				else
				{
					LookupSQL = XVar.Clone(MVCFunctions.Concat(LookupSQLTable, " where ", strLookupWhere));
					if((XVar)(!(XVar)(GlobalVars.gSettings.isLookupUnique((XVar)(lookupField))))  || (XVar)(Constants.nDATABASE_Access != lookupConnection.dbType))
					{
						if(XVar.Pack(lookupOrderBy))
						{
							LookupSQL = MVCFunctions.Concat(LookupSQL, " ORDER BY ", lookupOrderBy);
						}
					}
				}
				lookupIndices = XVar.Clone(CommonFunctions.GetLookupFieldsIndexes((XVar)(GlobalVars.gSettings), (XVar)(lookupField)));
				linkFieldIndex = XVar.Clone(lookupIndices["linkFieldIndex"]);
				displayFieldIndex = XVar.Clone(lookupIndices["displayFieldIndex"]);
				var_response = XVar.Clone(XVar.Array());
				qResult = XVar.Clone(lookupConnection.query((XVar)(LookupSQL)));
				while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
				{
					if((XVar)(LookupType == Constants.LT_QUERY)  && (XVar)(GlobalVars.gSettings.isLookupUnique((XVar)(lookupField))))
					{
						dynamic uniqueArray = XVar.Array();
						if(XVar.Pack(!(XVar)(uniqueArray as object != null)))
						{
							uniqueArray = XVar.Clone(XVar.Array());
						}
						if(XVar.Pack(MVCFunctions.in_array((XVar)(data[displayFieldIndex]), (XVar)(uniqueArray))))
						{
							continue;
						}
						uniqueArray.InitAndSetArrayItem(data[displayFieldIndex], null);
					}
					data.InitAndSetArrayItem(GlobalVars.cipherer.DecryptField((XVar)(lookupField), (XVar)(data[linkFieldIndex])), linkFieldIndex);
					if(LookupType == Constants.LT_QUERY)
					{
						data.InitAndSetArrayItem(GlobalVars.cipherer.DecryptField((XVar)(displayFieldName), (XVar)(data[displayFieldIndex])), displayFieldIndex);
					}
					var_response.InitAndSetArrayItem(data[linkFieldIndex], null);
					var_response.InitAndSetArrayItem(data[displayFieldIndex], null);
				}
				respObj = XVar.Clone(new XVar("success", true, "data", MVCFunctions.array_slice((XVar)(var_response), new XVar(0), new XVar(40))));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respObj)));
				MVCFunctions.Echo(new XVar(""));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
