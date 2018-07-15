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
		public XVar searchsuggest()
		{
			try
			{
				dynamic _connection = null, allSearchFields = XVar.Array(), controls = null, detailKeys = XVar.Array(), numberOfSuggests = null, query = null, result = XVar.Array(), returnJSON = XVar.Array(), searchClauseObj = null, searchField = null, searchFor = null, searchOpt = null, table = null, var_response = XVar.Array(), whereClauses = XVar.Array();
				ProjectSettings pSet;
				CommonFunctions.add_nocache_headers();
				table = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
				GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(table)));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", table, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				searchFor = XVar.Clone(MVCFunctions.trim((XVar)(MVCFunctions.postvalue(new XVar("searchFor")))));
				if(searchFor == XVar.Pack(""))
				{
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(new XVar("success", true, "result", ""))));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				_connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
				var_response = XVar.Clone(XVar.Array());
				searchOpt = XVar.Clone((XVar.Pack(MVCFunctions.postvalue(new XVar("start"))) ? XVar.Pack("Starts with") : XVar.Pack("Contains")));
				searchField = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(MVCFunctions.postvalue(new XVar("searchField")))));
				numberOfSuggests = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("searchSuggestsNumber"), new XVar(10)));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), new XVar(Constants.PAGE_SEARCH)));
				query = XVar.Clone(pSet.getSQLQuery());
				if(searchField == XVar.Pack(""))
				{
					allSearchFields = XVar.Clone(pSet.getGoogleLikeFields());
				}
				else
				{
					allSearchFields = XVar.Clone(pSet.getAllSearchFields());
				}
				detailKeys = XVar.Clone(XVar.Array());
				whereClauses = XVar.Clone(XVar.Array());
				whereClauses.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(GlobalVars.strTableName)), null);
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName)));
				controls = XVar.Clone(new EditControlsContainer(new XVar(null), (XVar)(pSet), new XVar(Constants.PAGE_LIST), (XVar)(GlobalVars.cipherer)));
				if(XSession.Session[MVCFunctions.Concat(GlobalVars.strTableName, "_mastertable")] != "")
				{
					dynamic i = null, j = null, masterTablesInfoArr = XVar.Array(), masterWhere = null, mastervalue = null;
					masterWhere = new XVar("");
					masterTablesInfoArr = XVar.Clone(pSet.getMasterTablesArr((XVar)(GlobalVars.strTableName)));
					i = new XVar(0);
					for(;i < MVCFunctions.count(masterTablesInfoArr); i++)
					{
						if(XSession.Session[MVCFunctions.Concat(GlobalVars.strTableName, "_mastertable")] != masterTablesInfoArr[i]["mDataSourceTable"])
						{
							continue;
						}
						detailKeys = XVar.Clone(masterTablesInfoArr[i]["detailKeys"]);
						j = new XVar(0);
						for(;j < MVCFunctions.count(detailKeys); j++)
						{
							mastervalue = XVar.Clone(GlobalVars.cipherer.MakeDBValue((XVar)(detailKeys[j]), (XVar)(XSession.Session[MVCFunctions.Concat(GlobalVars.strTableName, "_masterkey", j + 1)]), new XVar(""), new XVar(true)));
							if(mastervalue == "null")
							{
								masterWhere = MVCFunctions.Concat(masterWhere, RunnerPage._getFieldSQL((XVar)(detailKeys[j]), (XVar)(_connection), (XVar)(pSet)), " is NULL ");
							}
							else
							{
								masterWhere = MVCFunctions.Concat(masterWhere, RunnerPage._getFieldSQLDecrypt((XVar)(detailKeys[j]), (XVar)(_connection), (XVar)(pSet), (XVar)(GlobalVars.cipherer)), "=", mastervalue);
							}
						}
						break;
					}
					whereClauses.InitAndSetArrayItem(masterWhere, null);
				}
				searchClauseObj = XVar.Clone(SearchClause.getSearchObject((XVar)(GlobalVars.strTableName), new XVar(""), (XVar)(GlobalVars.strTableName), (XVar)(GlobalVars.cipherer)));
				searchClauseObj.processFiltersWhere((XVar)(_connection));
				foreach (KeyValuePair<XVar, dynamic> filteredField in searchClauseObj.filteredFields.GetEnumerator())
				{
					whereClauses.InitAndSetArrayItem(filteredField.Value["where"], null);
				}
				result = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> f in allSearchFields.GetEnumerator())
				{
					dynamic clausesData = XVar.Array(), distinct = null, fType = null, fieldControl = null, having = null, isAggregateField = null, qResult = null, row = XVar.Array(), sql = XVar.Array(), subQuery = null, val = null, where = null;
					fType = XVar.Clone(pSet.getFieldType((XVar)(f.Value)));
					if((XVar)((XVar)((XVar)(!(XVar)(CommonFunctions.IsCharType((XVar)(fType))))  && (XVar)(!(XVar)(CommonFunctions.IsNumberType((XVar)(fType)))))  && (XVar)(!(XVar)(CommonFunctions.IsGuid((XVar)(fType)))))  || (XVar)(MVCFunctions.in_array((XVar)(f.Value), (XVar)(detailKeys))))
					{
						continue;
					}
					if((XVar)(_connection.dbType == Constants.nDATABASE_Oracle)  && (XVar)(CommonFunctions.IsTextType((XVar)(fType))))
					{
						continue;
					}
					if((XVar)((XVar)(searchField != XVar.Pack(""))  && (XVar)(searchField != MVCFunctions.GoodFieldName((XVar)(f.Value))))  || (XVar)(!(XVar)(pSet.checkFieldPermissions((XVar)(f.Value)))))
					{
						continue;
					}
					fieldControl = XVar.Clone(controls.getControl((XVar)(f.Value)));
					isAggregateField = XVar.Clone(pSet.isAggregateField((XVar)(f.Value)));
					where = XVar.Clone(fieldControl.getSuggestWhere((XVar)(searchOpt), (XVar)(searchFor), (XVar)(isAggregateField)));
					having = XVar.Clone(fieldControl.getSuggestHaving((XVar)(searchOpt), (XVar)(searchFor), (XVar)(isAggregateField)));
					if((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(where))))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(having)))))
					{
						continue;
					}
					distinct = new XVar("DISTINCT");
					if((XVar)(_connection.dbType == Constants.nDATABASE_MSSQLServer)  || (XVar)(_connection.dbType == Constants.nDATABASE_Access))
					{
						if(XVar.Pack(CommonFunctions.IsTextType((XVar)(fType))))
						{
							distinct = new XVar("");
						}
					}
					sql = XVar.Clone(query.getSqlComponents());
					clausesData = XVar.Clone(fieldControl.getSelectColumnsAndJoinFromPart((XVar)(searchFor), (XVar)(searchOpt), new XVar(true)));
					if(0 == MVCFunctions.strlen((XVar)(clausesData["joinFromPart"])))
					{
						subQuery = XVar.Clone(SQLQuery.buildSQL((XVar)(sql), (XVar)(whereClauses), (XVar)(XVar.Array()), (XVar)(new XVar(0, where)), (XVar)(new XVar(0, having))));
						GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("SELECT ", distinct, " st.", _connection.addFieldWrappers((XVar)(f.Value)), " from (", subQuery, ") st"));
					}
					else
					{
						sql["from"] = MVCFunctions.Concat(sql["from"], clausesData["joinFromPart"]);
						sql.InitAndSetArrayItem(MVCFunctions.Concat("SELECT ", distinct, " ", clausesData["selectColumns"], " as ", _connection.addFieldWrappers(new XVar("_srchfld_"))), "head");
						subQuery = XVar.Clone(SQLQuery.buildSQL((XVar)(sql), (XVar)(whereClauses), (XVar)(XVar.Array()), (XVar)(new XVar(0, where)), (XVar)(new XVar(0, having))));
						GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("SELECT ", _connection.addFieldWrappers(new XVar("_srchfld_")), " from (", subQuery, ") st"));
					}
					qResult = XVar.Clone(_connection.queryPage((XVar)(GlobalVars.strSQL), new XVar(1), (XVar)(numberOfSuggests), new XVar(true)));
					while((XVar)(row = XVar.Clone(qResult.fetchNumeric()))  && (XVar)(MVCFunctions.count(var_response) < numberOfSuggests))
					{
						val = XVar.Clone(GlobalVars.cipherer.DecryptField((XVar)(f.Value), (XVar)(row[0])));
						if(XVar.Pack(CommonFunctions.IsGuid((XVar)(fType))))
						{
							val = XVar.Clone(MVCFunctions.substr((XVar)(val), new XVar(1), new XVar(-1)));
						}
						fieldControl.suggestValue((XVar)(MVCFunctions.Concat("_", val)), (XVar)(searchFor), (XVar)(var_response), (XVar)(row));
					}
				}
				_connection.close();
				MVCFunctions.ksort(ref var_response, new XVar(Constants.SORT_STRING));
				foreach (KeyValuePair<XVar, dynamic> realValue in var_response.GetEnumerator())
				{
					dynamic pos = null, strRealValue = null, strValue = null;
					if(numberOfSuggests < MVCFunctions.count(result))
					{
						break;
					}
					strValue = XVar.Clone((XVar.Pack(realValue.Key[0] == "_") ? XVar.Pack(MVCFunctions.substr((XVar)(realValue.Key), new XVar(1))) : XVar.Pack(realValue.Key)));
					strRealValue = XVar.Clone((XVar.Pack(realValue.Value[0] == "_") ? XVar.Pack(MVCFunctions.substr((XVar)(realValue.Value), new XVar(1))) : XVar.Pack(realValue.Value)));
					pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(strValue), (XVar)(searchFor), new XVar(0)));
					if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
					{
						result.InitAndSetArrayItem(new XVar("value", MVCFunctions.runner_htmlspecialchars((XVar)(strValue)), "realValue", strRealValue), null);
					}
					else
					{
						dynamic highlightedValue = null;
						highlightedValue = XVar.Clone(MVCFunctions.Concat(MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.substr((XVar)(strValue), new XVar(0), (XVar)(pos)))), "<b>", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.substr((XVar)(strValue), (XVar)(pos), (XVar)(MVCFunctions.strlen((XVar)(searchFor)))))), "</b>", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.substr((XVar)(strValue), (XVar)(pos + MVCFunctions.strlen((XVar)(searchFor))))))));
						result.InitAndSetArrayItem(new XVar("value", highlightedValue, "realValue", strRealValue), null);
					}
				}
				returnJSON = XVar.Clone(XVar.Array());
				returnJSON.InitAndSetArrayItem(true, "success");
				returnJSON.InitAndSetArrayItem(result, "result");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.Echo(new XVar(""));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
