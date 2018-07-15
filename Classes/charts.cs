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
	public partial class Chart : XClass
	{
		protected dynamic strSQL;
		protected dynamic header;
		protected dynamic footer;
		protected dynamic y_axis_label;
		protected dynamic strLabel;
		protected dynamic arrDataLabels = XVar.Array();
		protected dynamic arrDataSeries = XVar.Array();
		protected dynamic chrt_array = XVar.Array();
		public dynamic webchart;
		protected dynamic cname;
		protected dynamic gstrOrderBy;
		protected dynamic table_type;
		protected dynamic cipherer = XVar.Pack(null);
		protected ProjectSettings pSet = null;
		protected dynamic searchClauseObj = XVar.Pack(null);
		protected dynamic sessionPrefix = XVar.Pack("");
		protected dynamic detailTablesData = XVar.Array();
		protected dynamic pageId;
		protected dynamic showDetails = XVar.Pack(true);
		protected dynamic chartPreview = XVar.Pack(false);
		protected dynamic dashChart = XVar.Pack(false);
		protected dynamic dashChartFirstPointSelected = XVar.Pack(false);
		protected dynamic detailMasterKeys = XVar.Pack("");
		protected dynamic dashTName = XVar.Pack("");
		protected dynamic dashElementName = XVar.Pack("");
		protected dynamic connection;
		protected dynamic _2d;
		protected dynamic noRecordsFound = XVar.Pack(false);
		protected dynamic singleSeries = XVar.Pack(false);
		public Chart(dynamic ch_array, dynamic _param_param)
		{
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.webchart = XVar.Clone(param["webchart"]);
			if(XVar.Pack(this.webchart))
			{
				this.chrt_array = XVar.Clone(CommonFunctions.Convert_Old_Chart((XVar)(ch_array)));
			}
			else
			{
				this.chrt_array = XVar.Clone(ch_array);
			}
			setConnection();
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.chrt_array["tables"][0])));
			this.showDetails = XVar.Clone(param["showDetails"]);
			if(XVar.Pack(this.showDetails))
			{
				dynamic i = null, strPerm = null;
				this.detailTablesData = XVar.Clone(this.pSet.getDetailTablesArr());
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.detailTablesData); i++)
				{
					strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(this.detailTablesData[i]["dDataSourceTable"])));
					if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))
					{
						this.detailTablesData.Remove(i);
					}
				}
			}
			this.pageId = XVar.Clone(param["pageId"]);
			this.chrt_array.InitAndSetArrayItem(false, "appearance", "autoupdate");
			this.table_type = XVar.Clone(this.chrt_array["table_type"]);
			if(XVar.Pack(!(XVar)(this.table_type)))
			{
				this.table_type = new XVar("project");
			}
			this.cname = XVar.Clone(param["cname"]);
			this.sessionPrefix = XVar.Clone(this.chrt_array["tables"][0]);
			this.chartPreview = XVar.Clone(param["chartPreview"]);
			this.dashChart = XVar.Clone(param["dashChart"]);
			if(XVar.Pack(this.dashChart))
			{
				this.dashTName = XVar.Clone(param["dashTName"]);
				this.dashElementName = XVar.Clone(param["dashElementName"]);
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.sessionPrefix));
			}
			this.gstrOrderBy = XVar.Clone(param["gstrOrderBy"]);
			if((XVar)((XVar)(!(XVar)(this.webchart))  && (XVar)(!(XVar)(this.chartPreview)))  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_advsearch"))))
			{
				this.searchClauseObj = XVar.Clone(SearchClause.UnserializeObject((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")])));
			}
			if(XVar.Pack(isProjectDB()))
			{
				this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.chrt_array["tables"][0])));
			}
			setBasicChartProp();
			this.strSQL = XVar.Clone(getSQL());
			if(XVar.Pack(CommonFunctions.tableEventExists(new XVar("UpdateChartSettings"), (XVar)(GlobalVars.strTableName))))
			{
				GlobalVars.eventObj = XVar.Clone(CommonFunctions.getEventObject((XVar)(GlobalVars.strTableName)));
				GlobalVars.eventObj.UpdateChartSettings(this);
			}
		}
		protected virtual XVar setSpecParams(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			if(var_params["name"] == "")
			{
				return null;
			}
			if(this.table_type != "db")
			{
				this.arrDataSeries.InitAndSetArrayItem((XVar.Pack(var_params["agr_func"]) ? XVar.Pack(var_params["label"]) : XVar.Pack(var_params["name"])), null);
			}
			else
			{
				this.arrDataSeries.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["name"]), null);
			}
			return null;
		}
		protected virtual XVar setDataLabels(dynamic _param_params, dynamic _param_gTableName)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			dynamic gTableName = XVar.Clone(_param_gTableName);
			#endregion

			dynamic chartType = null;
			chartType = XVar.Clone(this.chrt_array["chart_type"]["type"]);
			if((XVar)(this.table_type == "project")  && (XVar)(!(XVar)(this.webchart)))
			{
				if((XVar)(chartType != "candlestick")  && (XVar)(chartType != "ohlc"))
				{
					this.arrDataLabels.InitAndSetArrayItem(chart_xmlencode((XVar)(CommonFunctions.GetFieldLabel((XVar)(gTableName), (XVar)(MVCFunctions.GoodFieldName((XVar)(var_params["name"])))))), null);
				}
				else
				{
					this.arrDataLabels.InitAndSetArrayItem(chart_xmlencode((XVar)(CommonFunctions.GetFieldLabel((XVar)(gTableName), (XVar)(MVCFunctions.GoodFieldName((XVar)(var_params["ohlcOpen"])))))), null);
				}
			}
			else
			{
				if(XVar.Pack(!(XVar)(chart_xmlencode((XVar)(var_params["label"])))))
				{
					if((XVar)(chartType != "candlestick")  && (XVar)(chartType != "ohlc"))
					{
						this.arrDataLabels.InitAndSetArrayItem(chart_xmlencode((XVar)(var_params["name"])), null);
					}
					else
					{
						this.arrDataLabels.InitAndSetArrayItem(chart_xmlencode((XVar)(var_params["ohlcOpen"])), null);
					}
				}
				else
				{
					this.arrDataLabels.InitAndSetArrayItem(chart_xmlencode((XVar)(var_params["label"])), null);
				}
			}
			return null;
		}
		protected virtual XVar setBasicChartProp()
		{
			dynamic i = null;
			this.header = XVar.Clone(this.chrt_array["appearance"]["head"]);
			this.footer = XVar.Clone(this.chrt_array["appearance"]["foot"]);
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.chrt_array["parameters"]) - 1; i++)
			{
				setSpecParams((XVar)(this.chrt_array["parameters"][i]));
				setDataLabels((XVar)(this.chrt_array["parameters"][i]), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.chrt_array["tables"][0]))));
			}
			if(this.chrt_array["chart_type"]["type"] != "gauge")
			{
				dynamic chartParams = XVar.Array(), var_params = XVar.Array();
				chartParams = XVar.Clone(this.chrt_array["parameters"]);
				var_params = XVar.Clone(chartParams[MVCFunctions.count(chartParams) - 1]);
				if(this.table_type != "db")
				{
					this.strLabel = XVar.Clone(var_params["name"]);
				}
				else
				{
					this.strLabel = XVar.Clone((XVar.Pack(var_params["agr_func"]) ? XVar.Pack(MVCFunctions.Concat(var_params["agr_func"], "_", var_params["table"], "_", var_params["name"])) : XVar.Pack(MVCFunctions.Concat(var_params["table"], "_", var_params["name"]))));
				}
			}
			if(MVCFunctions.count(this.arrDataLabels) == 1)
			{
				this.y_axis_label = XVar.Clone(this.arrDataLabels[0]);
			}
			else
			{
				this.y_axis_label = XVar.Clone(this.chrt_array["appearance"]["y_axis_label"]);
			}
			return null;
		}
		protected virtual XVar getMasterWhere()
		{
			dynamic detailKeysByM = XVar.Array(), i = null, mValue = null, masterTable = null, masterWhereParts = XVar.Array();
			if(XVar.Pack(this.dashChart))
			{
				return "";
			}
			masterTable = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")]);
			detailKeysByM = XVar.Clone(this.pSet.getDetailKeysByMasterTable((XVar)(masterTable)));
			masterWhereParts = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(detailKeysByM); i++)
			{
				if((XVar)(this.cipherer)  && (XVar)(this.cipherer.isEncryptionByPHPEnabled()))
				{
					mValue = XVar.Clone(this.cipherer.MakeDBValue((XVar)(detailKeysByM[i]), (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1)])));
				}
				else
				{
					mValue = XVar.Clone(CommonFunctions.make_db_value((XVar)(detailKeysByM[i]), (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1)])));
				}
				if(MVCFunctions.strlen((XVar)(mValue)) != 0)
				{
					masterWhereParts.InitAndSetArrayItem(MVCFunctions.Concat(RunnerPage._getFieldSQLDecrypt((XVar)(detailKeysByM[i]), (XVar)(this.connection), (XVar)(this.pSet), (XVar)(this.cipherer)), "=", mValue), null);
				}
				else
				{
					masterWhereParts.InitAndSetArrayItem("1=0", null);
				}
			}
			return MVCFunctions.implode(new XVar(" and "), (XVar)(masterWhereParts));
		}
		protected virtual XVar getSQL()
		{
			dynamic orderBy = null, sql = XVar.Array(), strOrderBy = null;
			if(XVar.Pack(this.webchart))
			{
				return getWebChartSQL();
			}
			sql = XVar.Clone(getSubsetSQLComponents());
			orderBy = XVar.Clone(new OrderClause((XVar)(this.pSet), (XVar)(this.cipherer), (XVar)(this.sessionPrefix), (XVar)(this.connection)));
			strOrderBy = XVar.Clone(orderBy.getOrderByExpression());
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, strOrderBy);
			GlobalVars.eventObj = XVar.Clone(CommonFunctions.getEventObject((XVar)(this.pSet.getTableName())));
			if(XVar.Pack(GlobalVars.eventObj.exists(new XVar("BeforeQueryChart"))))
			{
				dynamic backupOrder = null, backupSQL = null, backupWhere = null, strWhereClause = null;
				strWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				backupSQL = XVar.Clone(GlobalVars.strSQL);
				backupWhere = XVar.Clone(strWhereClause);
				backupOrder = XVar.Clone(strOrderBy);
				GlobalVars.eventObj.BeforeQueryChart((XVar)(GlobalVars.strSQL), ref strWhereClause, ref strOrderBy);
				if(backupSQL != GlobalVars.strSQL)
				{
					return GlobalVars.strSQL;
				}
				if(backupWhere != strWhereClause)
				{
					GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, strWhereClause)), (XVar)(sql["mandatoryHaving"]), (XVar)(XVar.Array()), (XVar)(sql["optionalHaving"])));
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, strOrderBy);
				}
				else
				{
					if(backupOrder != strOrderBy)
					{
						GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
						GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, strOrderBy);
					}
				}
			}
			return GlobalVars.strSQL;
		}
		protected virtual XVar getSubsetSQLComponents()
		{
			dynamic mandatoryHaving = XVar.Array(), mandatoryWhere = XVar.Array(), optionalHaving = XVar.Array(), optionalWhere = XVar.Array(), sqlParts = XVar.Array();
			sqlParts = XVar.Clone(this.pSet.getSQLQuery().getSqlComponents());
			mandatoryWhere = XVar.Clone(XVar.Array());
			mandatoryHaving = XVar.Clone(XVar.Array());
			optionalWhere = XVar.Clone(XVar.Array());
			optionalHaving = XVar.Clone(XVar.Array());
			if((XVar)(!(XVar)(this.chartPreview))  && (XVar)(this.searchClauseObj))
			{
				dynamic editControls = null, whereComponents = XVar.Array();
				editControls = XVar.Clone(new EditControlsContainer(new XVar(null), (XVar)(this.pSet), new XVar(Constants.PAGE_SEARCH), (XVar)(this.cipherer)));
				whereComponents = XVar.Clone(RunnerPage.sGetWhereComponents((XVar)(this.pSet.getSQLQuery()), (XVar)(this.pSet), (XVar)(this.searchClauseObj), (XVar)(editControls), (XVar)(this.connection)));
				if(XVar.Pack(whereComponents["searchUnionRequired"]))
				{
					optionalWhere.InitAndSetArrayItem(whereComponents["searchWhere"], null);
					optionalHaving.InitAndSetArrayItem(whereComponents["searchHaving"], null);
				}
				else
				{
					mandatoryWhere.InitAndSetArrayItem(whereComponents["searchWhere"], null);
					mandatoryHaving.InitAndSetArrayItem(whereComponents["searchHaving"], null);
				}
				sqlParts["from"] = MVCFunctions.Concat(sqlParts["from"], whereComponents["joinFromPart"]);
				foreach (KeyValuePair<XVar, dynamic> f in whereComponents["filterWhere"].GetEnumerator())
				{
					mandatoryWhere.InitAndSetArrayItem(f.Value, null);
				}
				foreach (KeyValuePair<XVar, dynamic> f in whereComponents["filterHaving"].GetEnumerator())
				{
					mandatoryHaving.InitAndSetArrayItem(f.Value, null);
				}
			}
			mandatoryWhere.InitAndSetArrayItem(getMasterWhere(), null);
			mandatoryWhere.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.chrt_array["tables"][0])), null);
			mandatoryWhere.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_chartTabWhere")], null);
			return new XVar("sqlParts", sqlParts, "mandatoryWhere", mandatoryWhere, "mandatoryHaving", mandatoryHaving, "optionalWhere", optionalWhere, "optionalHaving", optionalHaving);
		}
		protected virtual XVar getWebChartSQL()
		{
			dynamic masterWhere = null, searchHavingClause = null, strSearchCriteria = null, strWhereClause = null;
			GlobalVars.gQuery = XVar.Clone(this.pSet.getSQLQuery());
			masterWhere = XVar.Clone(getMasterWhere());
			strWhereClause = new XVar("");
			searchHavingClause = new XVar("");
			strSearchCriteria = new XVar("and");
			if(XVar.Pack(!(XVar)(this.webchart)))
			{
				if((XVar)(!(XVar)(this.chartPreview))  && (XVar)(this.searchClauseObj))
				{
					dynamic editControls = null, whereComponents = XVar.Array();
					editControls = XVar.Clone(new EditControlsContainer(new XVar(null), (XVar)(this.pSet), new XVar(Constants.PAGE_SEARCH), (XVar)(this.cipherer)));
					whereComponents = XVar.Clone(RunnerPage.sGetWhereComponents((XVar)(GlobalVars.gQuery), (XVar)(this.pSet), (XVar)(this.searchClauseObj), (XVar)(editControls), (XVar)(this.connection)));
					strWhereClause = XVar.Clone(whereComponents["searchWhere"]);
					foreach (KeyValuePair<XVar, dynamic> fWhere in whereComponents["filterWhere"].GetEnumerator())
					{
						strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(fWhere.Value)));
					}
					searchHavingClause = XVar.Clone(whereComponents["searchHaving"]);
					foreach (KeyValuePair<XVar, dynamic> fHaving in whereComponents["filterHaving"].GetEnumerator())
					{
						searchHavingClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(searchHavingClause), (XVar)(fHaving.Value)));
					}
					strSearchCriteria = XVar.Clone((XVar.Pack(whereComponents["searchUnionRequired"]) ? XVar.Pack("or") : XVar.Pack("and")));
				}
			}
			else
			{
				if(this.table_type != "project")
				{
					GlobalVars.strTableName = XVar.Clone(MVCFunctions.Concat("webchart", this.cname));
				}
				strWhereClause = XVar.Clone(CommonFunctions.CalcSearchParam((XVar)(this.table_type != "project")));
			}
			if(XVar.Pack(strWhereClause))
			{
				this.chrt_array["where"] = MVCFunctions.Concat(this.chrt_array["where"], (XVar.Pack(this.chrt_array["where"]) ? XVar.Pack(MVCFunctions.Concat(" AND (", strWhereClause, ")")) : XVar.Pack(MVCFunctions.Concat(" WHERE (", strWhereClause, ")"))));
			}
			if(this.table_type == "project")
			{
				dynamic strOrderBy = null, strSQLbak = null;
				if(XVar.Pack(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.chrt_array["tables"][0]))))
				{
					strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(GlobalVars.strTableName)))));
				}
				GlobalVars.strSQL = XVar.Clone(GlobalVars.gQuery.gSQLWhere((XVar)(strWhereClause)));
				strOrderBy = XVar.Clone(this.gstrOrderBy);
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", strOrderBy);
				if(XVar.Pack(masterWhere))
				{
					strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(masterWhere)));
				}
				strSQLbak = XVar.Clone(GlobalVars.strSQL);
				if(XVar.Pack(CommonFunctions.tableEventExists(new XVar("BeforeQueryChart"), (XVar)(GlobalVars.strTableName))))
				{
					dynamic tstrSQL = null;
					tstrSQL = XVar.Clone(GlobalVars.strSQL);
					GlobalVars.eventObj.BeforeQueryChart((XVar)(tstrSQL), ref strWhereClause, ref strOrderBy);
					GlobalVars.strSQL = XVar.Clone(tstrSQL);
				}
				if(strSQLbak == GlobalVars.strSQL)
				{
					GlobalVars.strSQL = XVar.Clone(GlobalVars.gQuery.gSQLWhere((XVar)(strWhereClause)));
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", strOrderBy);
				}
			}
			if((XVar)(this.cname)  && (XVar)(this.table_type == "db"))
			{
				GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat(this.chrt_array["sql"], this.chrt_array["where"], this.chrt_array["group_by"], this.chrt_array["order_by"]));
			}
			else
			{
				if((XVar)(this.cname)  && (XVar)(this.table_type == "custom"))
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.IsStoredProcedure((XVar)(this.chrt_array["sql"])))))
					{
						dynamic sql_query = null;
						sql_query = XVar.Clone(this.chrt_array["sql"]);
						if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
						{
							dynamic pos = null;
							pos = XVar.Clone(MVCFunctions.strrpos((XVar)(MVCFunctions.strtoupper((XVar)(sql_query))), new XVar("ORDER BY")));
							if(XVar.Pack(pos))
							{
								sql_query = XVar.Clone(MVCFunctions.substr((XVar)(sql_query), new XVar(0), (XVar)(pos)));
							}
						}
						if(this.connection.dbType != Constants.nDATABASE_Oracle)
						{
							GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("select * from (", sql_query, ") as ", this.connection.addFieldWrappers(new XVar("custom_query")), this.chrt_array["where"]));
						}
						else
						{
							GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("select * from (", sql_query, ")", this.chrt_array["where"]));
						}
					}
					else
					{
						GlobalVars.strSQL = XVar.Clone(this.chrt_array["sql"]);
					}
				}
			}
			return GlobalVars.strSQL;
		}
		protected virtual XVar isProjectDB()
		{
			if(XVar.Pack(!(XVar)(this.webchart)))
			{
				return true;
			}
			if("dbo._ABCReports" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCVotes" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCSecurity" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCReports" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCReports" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCReports" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo._ABCReports" == this.chrt_array["tables"][0])
			{
				return true;
			}
			if("dbo.vwABCReportsVoteCount" == this.chrt_array["tables"][0])
			{
				return true;
			}
			return false;
		}
		protected virtual XVar setConnection()
		{
			if(XVar.Pack(isProjectDB()))
			{
				this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.chrt_array["tables"][0])));
			}
			else
			{
				this.connection = XVar.Clone(GlobalVars.cman.getDefault());
			}
			return null;
		}
		public virtual XVar setFooter(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.footer = XVar.Clone(name);
			return null;
		}
		public virtual XVar getFooter()
		{
			return this.footer;
		}
		public virtual XVar setHeader(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.header = XVar.Clone(name);
			return null;
		}
		public virtual XVar getHeader()
		{
			return this.header;
		}
		public virtual XVar setLabelField(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			this.strLabel = XVar.Clone(name);
			return null;
		}
		public virtual XVar getLabelField()
		{
			return this.strLabel;
		}
		protected virtual XVar getDetailedToolipMessage()
		{
			dynamic showClickHere = null;
			if((XVar)(!(XVar)(this.showDetails))  || (XVar)(!(XVar)(MVCFunctions.count(this.detailTablesData))))
			{
				return "";
			}
			showClickHere = new XVar(true);
			if(XVar.Pack(this.dashChart))
			{
				dynamic arrDElem = XVar.Array(), pDSet = null;
				showClickHere = new XVar(false);
				pDSet = XVar.Clone(new ProjectSettings((XVar)(this.dashTName)));
				arrDElem = XVar.Clone(pDSet.getDashboardElements());
				foreach (KeyValuePair<XVar, dynamic> elem in arrDElem.GetEnumerator())
				{
					if((XVar)(elem.Value["table"] == this.chrt_array["tables"][0])  && (XVar)(MVCFunctions.count(elem.Value["details"])))
					{
						showClickHere = new XVar(true);
					}
				}
			}
			if(XVar.Pack(showClickHere))
			{
				dynamic tableCaption = null;
				tableCaption = XVar.Clone(CommonFunctions.GetTableCaption((XVar)(this.detailTablesData[0]["dDataSourceTable"])));
				tableCaption = XVar.Clone((XVar.Pack(tableCaption) ? XVar.Pack(tableCaption) : XVar.Pack(this.detailTablesData[0]["dDataSourceTable"])));
				return MVCFunctions.Concat("\nClick here to see ", tableCaption, " details");
			}
			return "";
		}
		protected virtual XVar getNoDataMessage()
		{
			if(XVar.Pack(!(XVar)(this.noRecordsFound)))
			{
				return "";
			}
			if(XVar.Pack(!(XVar)(this.searchClauseObj)))
			{
				return "No data yet.";
			}
			if(XVar.Pack(this.searchClauseObj.isSearchFunctionalityActivated()))
			{
				return "No results found.";
			}
			return "No data yet.";
		}
		public virtual XVar write()
		{
			dynamic chart = XVar.Array(), data = XVar.Array();
			data = XVar.Clone(XVar.Array());
			chart = XVar.Clone(XVar.Array());
			setTypeSpecChartSettings((XVar)(chart));
			if((XVar)(this.chrt_array["appearance"]["color71"] != "")  || (XVar)(this.chrt_array["appearance"]["color91"] != ""))
			{
				chart.InitAndSetArrayItem(XVar.Array(), "background");
			}
			if(this.chrt_array["appearance"]["color71"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color71"]), "background", "fill");
			}
			if(this.chrt_array["appearance"]["color91"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color91"]), "background", "stroke");
			}
			if(XVar.Pack(this.noRecordsFound))
			{
				data.InitAndSetArrayItem(getNoDataMessage(), "noDataMessage");
				MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(data)));
				return null;
			}
			if((XVar)(this.chrt_array["appearance"]["sanim"] == "true")  && (XVar)(this.chrt_array["appearance"]["autoupdate"] != "true"))
			{
				chart.InitAndSetArrayItem(new XVar("enabled", "true", "duration", 1000), "animation");
			}
			if((XVar)(this.chrt_array["appearance"]["slegend"] == "true")  && (XVar)(!(XVar)(this.chartPreview)))
			{
				chart.InitAndSetArrayItem(new XVar("enabled", "true"), "legend");
			}
			else
			{
				chart.InitAndSetArrayItem(new XVar("enabled", false), "legend");
			}
			chart.InitAndSetArrayItem(false, "credits");
			chart.InitAndSetArrayItem(new XVar("enabled", "true", "text", this.header), "title");
			if(this.chrt_array["appearance"]["color101"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color101"]), "title", "fontColor");
			}
			data.InitAndSetArrayItem(chart, "chart");
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(data)));
			return null;
		}
		protected virtual XVar setTypeSpecChartSettings(dynamic chart)
		{
			return null;
		}
		protected virtual XVar getGrids()
		{
			dynamic grids = XVar.Array();
			grids = XVar.Clone(XVar.Array());
			if(this.chrt_array["appearance"]["sgrid"] == "true")
			{
				dynamic grid0 = XVar.Array(), stroke = null;
				stroke = XVar.Clone((XVar.Pack(this.chrt_array["appearance"]["color121"] != "") ? XVar.Pack(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color121"])) : XVar.Pack("#ddd")));
				grid0 = XVar.Clone(new XVar("enabled", true, "drawLastLine", false, "stroke", stroke, "scale", 0, "axis", 0));
				if(this.chrt_array["appearance"]["color81"] != "")
				{
					dynamic dataPlotBackgroundColor = null;
					dataPlotBackgroundColor = XVar.Clone(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color81"]));
					grid0.InitAndSetArrayItem(dataPlotBackgroundColor, "oddFill");
					grid0.InitAndSetArrayItem(dataPlotBackgroundColor, "evenFill");
				}
				grids.InitAndSetArrayItem(grid0, null);
				grids.InitAndSetArrayItem(new XVar("enabled", true, "drawLastLine", false, "stroke", stroke, "axis", 1), null);
			}
			return grids;
		}
		protected virtual XVar labelFormat(dynamic _param_fieldName, dynamic _param_data, dynamic _param_truncated = null)
		{
			#region default values
			if(_param_truncated as Object == null) _param_truncated = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			dynamic data = XVar.Clone(_param_data);
			dynamic truncated = XVar.Clone(_param_truncated);
			#endregion

			dynamic value = null, viewControls = null;
			if((XVar)(this.table_type == "db")  && (XVar)(MVCFunctions.count(this.chrt_array["customLabels"])))
			{
				fieldName = XVar.Clone(this.chrt_array["customLabels"][fieldName]);
			}
			viewControls = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), new XVar(Constants.PAGE_CHART)));
			value = XVar.Clone(CommonFunctions.html_special_decode((XVar)(viewControls.showDBValue((XVar)(fieldName), (XVar)(data)))));
			if((XVar)(truncated)  && (XVar)(50 < MVCFunctions.strlen((XVar)(value))))
			{
				value = XVar.Clone(MVCFunctions.Concat(MVCFunctions.runner_substr((XVar)(value), new XVar(0), new XVar(47)), "..."));
			}
			return chart_xmlencode((XVar)(value));
		}
		public virtual XVar get_data()
		{
			dynamic clickdata = XVar.Array(), data = XVar.Array(), i = null, qResult = null, row = null, series = XVar.Array(), strLabelFormat = null;
			data = XVar.Clone(XVar.Array());
			clickdata = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrDataSeries); i++)
			{
				data.InitAndSetArrayItem(XVar.Array(), i);
				clickdata.InitAndSetArrayItem(XVar.Array(), i);
			}
			qResult = XVar.Clone(this.connection.query((XVar)(this.strSQL)));
			if(this.cipherer != null)
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
			}
			else
			{
				row = XVar.Clone(qResult.fetchAssoc());
			}
			if(XVar.Pack(!(XVar)(row)))
			{
				this.noRecordsFound = new XVar(true);
			}
			while(XVar.Pack(row))
			{
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.arrDataSeries); i++)
				{
					data.InitAndSetArrayItem(getPoint((XVar)(i), (XVar)(row)), i, null);
					strLabelFormat = XVar.Clone(labelFormat((XVar)(this.strLabel), (XVar)(row)));
					clickdata.InitAndSetArrayItem(getActions((XVar)(row), (XVar)(this.arrDataSeries[i]), (XVar)(strLabelFormat)), i, null);
				}
				if(this.cipherer != null)
				{
					row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
				}
				else
				{
					row = XVar.Clone(qResult.fetchAssoc());
				}
			}
			this.connection.close();
			series = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrDataSeries); i++)
			{
				series.InitAndSetArrayItem(getSeriesData((XVar)(this.arrDataLabels[i]), (XVar)(data[i]), (XVar)(clickdata[i]), (XVar)(i), (XVar)(1 < MVCFunctions.count(this.arrDataSeries))), null);
			}
			return series;
		}
		protected virtual XVar getPoint(dynamic _param_seriesNumber, dynamic _param_row)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic row = XVar.Clone(_param_row);
			#endregion

			dynamic strDataSeries = null, strLabelFormat = null;
			strLabelFormat = XVar.Clone(labelFormat((XVar)(this.strLabel), (XVar)(row)));
			if((XVar)(this.table_type != "db")  || (XVar)(!(XVar)(MVCFunctions.count(this.chrt_array["customLabels"]))))
			{
				strDataSeries = XVar.Clone(row[this.arrDataSeries[seriesNumber]]);
			}
			else
			{
				strDataSeries = XVar.Clone(row[this.chrt_array["customLabels"][this.arrDataSeries[seriesNumber]]]);
			}
			return new XVar("x", strLabelFormat, "value", chart_xmlencode((XVar)(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(strDataSeries)) + 0)));
		}
		protected virtual XVar getSeriesData(dynamic _param_name, dynamic _param_pointsData, dynamic _param_clickData, dynamic _param_seriesNumber, dynamic _param_multiSeries = null)
		{
			#region default values
			if(_param_multiSeries as Object == null) _param_multiSeries = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic pointsData = XVar.Clone(_param_pointsData);
			dynamic clickData = XVar.Clone(_param_clickData);
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic multiSeries = XVar.Clone(_param_multiSeries);
			#endregion

			dynamic data = XVar.Array();
			data = XVar.Clone(new XVar("name", name, "data", pointsData, "xScale", "0", "yScale", "1", "seriesType", getSeriesType()));
			data.InitAndSetArrayItem(new XVar("enabled", this.chrt_array["appearance"]["sval"] == "true"), "labels");
			if(this.chrt_array["appearance"]["color61"] != "")
			{
				data.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color61"]), "labels", "fontColor");
			}
			if(XVar.Pack(clickData))
			{
				data.InitAndSetArrayItem(clickData, "clickData");
			}
			data.InitAndSetArrayItem(getSeriesTooltip((XVar)(multiSeries)), "tooltip");
			return data;
		}
		protected virtual XVar getSeriesTooltip(dynamic _param_multiSeries)
		{
			#region pass-by-value parameters
			dynamic multiSeries = XVar.Clone(_param_multiSeries);
			#endregion

			dynamic clickHereMessage = null, tooltipSettings = XVar.Array();
			tooltipSettings = XVar.Clone(new XVar("enabled", true));
			clickHereMessage = new XVar("");
			if((XVar)(this.showDetails)  && (XVar)(MVCFunctions.count(this.detailTablesData)))
			{
				clickHereMessage = XVar.Clone(getDetailedToolipMessage());
			}
			if(XVar.Pack(!(XVar)(multiSeries)))
			{
				tooltipSettings.InitAndSetArrayItem(MVCFunctions.Concat("{%Value}", clickHereMessage), "textFormatter");
			}
			else
			{
				if(XVar.Pack(clickHereMessage))
				{
					tooltipSettings.InitAndSetArrayItem(clickHereMessage, "valuePostfix");
				}
			}
			return tooltipSettings;
		}
		protected virtual XVar getSeriesType()
		{
			return "column";
		}
		protected virtual XVar chart_xmlencode(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return MVCFunctions.str_replace((XVar)(new XVar(0, "&", 1, "<", 2, ">", 3, "\"")), (XVar)(new XVar(0, "&amp;", 1, "&lt;", 2, "&gt;", 3, "&quot;")), (XVar)(str));
		}
		protected virtual XVar getActions(dynamic _param_data, dynamic _param_seriesId, dynamic _param_pointId)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic seriesId = XVar.Clone(_param_seriesId);
			dynamic pointId = XVar.Clone(_param_pointId);
			#endregion

			dynamic detailTableData = XVar.Array(), masterquery = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this.detailTablesData))))
			{
				return null;
			}
			if(XVar.Pack(this.dashChart))
			{
				dynamic masterKeysArr = XVar.Array();
				masterKeysArr = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> detail in this.detailTablesData.GetEnumerator())
				{
					foreach (KeyValuePair<XVar, dynamic> mk in detail.Value["masterKeys"].GetEnumerator())
					{
						masterKeysArr.InitAndSetArrayItem(new XVar(MVCFunctions.Concat("masterkey", mk.Key + 1), data[mk.Value]), detail.Value["dDataSourceTable"]);
					}
				}
				if(XVar.Pack(!(XVar)(this.dashChartFirstPointSelected)))
				{
					this.dashChartFirstPointSelected = new XVar(true);
					this.detailMasterKeys = XVar.Clone(MVCFunctions.my_json_encode((XVar)(masterKeysArr)));
				}
				return new XVar("masterKeys", masterKeysArr, "seriesId", seriesId, "pointId", pointId);
			}
			detailTableData = XVar.Clone(this.detailTablesData[0]);
			masterquery = XVar.Clone(MVCFunctions.Concat("mastertable=", MVCFunctions.RawUrlEncode((XVar)(GlobalVars.strTableName))));
			foreach (KeyValuePair<XVar, dynamic> mk in detailTableData["masterKeys"].GetEnumerator())
			{
				masterquery = MVCFunctions.Concat(masterquery, "&masterkey", mk.Key + 1, "=", MVCFunctions.RawUrlEncode((XVar)(data[mk.Value])));
			}
			return new XVar("url", MVCFunctions.GetTableLink((XVar)(detailTableData["dShortTable"]), (XVar)(detailTableData["dType"]), (XVar)(masterquery)));
		}
	}
	public partial class Chart_Bar : Chart
	{
		protected dynamic stacked;
		protected dynamic bar;
		protected static bool skipChart_BarCtor = false;
		public Chart_Bar(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_BarCtor)
			{
				skipChart_BarCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.stacked = XVar.Clone(param["stacked"]);
			this._2d = XVar.Clone(param["2d"]);
			this.bar = XVar.Clone(param["bar"]);
		}
		protected override XVar getSeriesType()
		{
			if(XVar.Pack(this.bar))
			{
				return "bar";
			}
			else
			{
				return "column";
			}
			return null;
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			chart.InitAndSetArrayItem(getScales(), "scales");
			if(XVar.Pack(this.bar))
			{
				chart.InitAndSetArrayItem("bar", "type");
			}
			else
			{
				chart.InitAndSetArrayItem("column", "type");
			}
			if(XVar.Pack(!(XVar)(this._2d)))
			{
				chart["type"] = MVCFunctions.Concat(chart["type"], "3d");
			}
			chart.InitAndSetArrayItem(0, "xScale");
			chart.InitAndSetArrayItem(1, "yScale");
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", this.y_axis_label)), "yAxes");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			return null;
		}
		protected virtual XVar getScales()
		{
			if((XVar)(this.stacked)  || (XVar)(this.chrt_array["appearance"]["slog"] == "true"))
			{
				dynamic arr = XVar.Array();
				arr = XVar.Clone(XVar.Array());
				if(XVar.Pack(this.stacked))
				{
					arr.InitAndSetArrayItem("value", "stackMode");
				}
				if(this.chrt_array["appearance"]["slog"] == "true")
				{
					arr.InitAndSetArrayItem(10, "logBase");
					arr.InitAndSetArrayItem("log", "type");
				}
				return new XVar(0, new XVar("names", XVar.Array()), 1, arr);
			}
			return XVar.Array();
		}
		protected virtual dynamic query(dynamic _param_strSQL)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			#endregion

			dynamic userLimit = null;
			userLimit = XVar.Clone(this.pSet.getRecordsLimit());
			if(XVar.Pack(userLimit))
			{
				return this.connection.queryPage((XVar)(strSQL), new XVar(1), (XVar)(userLimit), new XVar(true));
			}
			return this.connection.query((XVar)(strSQL));
		}
	}
	public partial class Chart_Line : Chart
	{
		protected dynamic type_line;
		protected static bool skipChart_LineCtor = false;
		public Chart_Line(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_LineCtor)
			{
				skipChart_LineCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.type_line = XVar.Clone(param["type_line"]);
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			chart.InitAndSetArrayItem("line", "type");
			chart.InitAndSetArrayItem(0, "xScale");
			chart.InitAndSetArrayItem(1, "yScale");
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem(new XVar("displayMode", "single"), "tooltip");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", this.y_axis_label)), "yAxes");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			return null;
		}
		protected override XVar getSeriesType()
		{
			switch(((XVar)this.type_line).ToString())
			{
				case "line":
					return "line";
				case "spline":
					return "spline";
				case "step_line":
					return "stepLine";
				default:
					return "line";
			}
			return null;
		}
	}
	public partial class Chart_Area : Chart
	{
		protected dynamic stacked;
		protected static bool skipChart_AreaCtor = false;
		public Chart_Area(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_AreaCtor)
			{
				skipChart_AreaCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.stacked = XVar.Clone(param["stacked"]);
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			if(XVar.Pack(this.stacked))
			{
				chart.InitAndSetArrayItem(getScales(), "scales");
			}
			chart.InitAndSetArrayItem("area", "type");
			chart.InitAndSetArrayItem(0, "xScale");
			chart.InitAndSetArrayItem(1, "yScale");
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem(new XVar("displayMode", "single"), "tooltip");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", this.y_axis_label)), "yAxes");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			return null;
		}
		protected override XVar getSeriesType()
		{
			return "area";
		}
		protected virtual XVar getScales()
		{
			if(XVar.Pack(this.stacked))
			{
				dynamic arr = XVar.Array();
				arr = XVar.Clone(XVar.Array());
				arr.InitAndSetArrayItem("value", "stackMode");
				if(this.chrt_array["appearance"]["sstacked"] == "true")
				{
					arr.InitAndSetArrayItem("percent", "stackMode");
					arr.InitAndSetArrayItem("10", "maximumGap");
					arr.InitAndSetArrayItem("100", "maximum");
				}
				return new XVar(0, new XVar("names", XVar.Array()), 1, arr);
			}
			return XVar.Array();
		}
	}
	public partial class Chart_Pie : Chart
	{
		protected dynamic pie;
		protected static bool skipChart_PieCtor = false;
		public Chart_Pie(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_PieCtor)
			{
				skipChart_PieCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.pie = XVar.Clone(param["pie"]);
			this._2d = XVar.Clone(param["2d"]);
			this.singleSeries = new XVar(true);
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			dynamic series = XVar.Array();
			series = XVar.Clone(get_data());
			chart.InitAndSetArrayItem(series[0]["data"], "data");
			chart.InitAndSetArrayItem(series[0]["clickData"], "clickData");
			chart.InitAndSetArrayItem(true, "singleSeries");
			chart.InitAndSetArrayItem(series[0]["tooltip"], "tooltip");
			if(XVar.Pack(this._2d))
			{
				chart.InitAndSetArrayItem("pie", "type");
			}
			else
			{
				chart.InitAndSetArrayItem("pie3d", "type");
			}
			if(XVar.Pack(!(XVar)(this.pie)))
			{
				chart.InitAndSetArrayItem("30%", "innerRadius");
			}
			if((XVar)(this.chrt_array["appearance"]["slegend"] == "true")  && (XVar)(!(XVar)(this.chartPreview)))
			{
				chart.InitAndSetArrayItem(new XVar("enabled", true, "text", this.footer), "legend", "title");
			}
			chart.InitAndSetArrayItem(new XVar("enabled", (XVar)(this.chrt_array["appearance"]["sval"] == "true")  || (XVar)(this.chrt_array["appearance"]["sname"] == "true")), "labels");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				if(XVar.Pack(this.chrt_array["appearance"]["sval"]))
				{
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color61"]), "labels", "fontColor");
				}
				else
				{
					if(XVar.Pack(this.chrt_array["appearance"]["sname"]))
					{
						chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "labels", "fontColor");
					}
				}
			}
			return null;
		}
	}
	public partial class Chart_Combined : Chart
	{
		protected static bool skipChart_CombinedCtor = false;
		public Chart_Combined(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_CombinedCtor)
			{
				skipChart_CombinedCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			chart.InitAndSetArrayItem("column", "type");
			chart.InitAndSetArrayItem(0, "xScale");
			chart.InitAndSetArrayItem(1, "yScale");
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", this.y_axis_label)), "yAxes");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			return null;
		}
		protected override XVar getSeriesType()
		{
			dynamic seriesNumber = null;
			if(seriesNumber == XVar.Pack(0))
			{
				return "line";
			}
			return "column";
		}
	}
	public partial class Chart_Funnel : Chart
	{
		protected dynamic inver;
		protected static bool skipChart_FunnelCtor = false;
		public Chart_Funnel(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_FunnelCtor)
			{
				skipChart_FunnelCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.inver = XVar.Clone(param["funnel_inv"]);
			this.singleSeries = new XVar(true);
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			dynamic series = XVar.Array();
			series = XVar.Clone(get_data());
			chart.InitAndSetArrayItem("pyramid", "type");
			chart.InitAndSetArrayItem(series[0]["data"], "data");
			chart.InitAndSetArrayItem(series[0]["clickData"], "clickData");
			chart.InitAndSetArrayItem(true, "singleSeries");
			chart.InitAndSetArrayItem(series[0]["tooltip"], "tooltip");
			if(XVar.Pack(this.inver))
			{
				chart.InitAndSetArrayItem(true, "reversed");
			}
			chart.InitAndSetArrayItem(new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"), "labels");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "labels", "fontColor");
			}
			return null;
		}
	}
	public partial class Chart_Bubble : Chart
	{
		protected dynamic arrDataSize = XVar.Array();
		protected static bool skipChart_BubbleCtor = false;
		public Chart_Bubble(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_BubbleCtor)
			{
				skipChart_BubbleCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this._2d = XVar.Clone(param["2d"]);
		}
		protected override XVar setSpecParams(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			base.setSpecParams((XVar)(var_params));
			if(var_params["name"] != "")
			{
				if(this.table_type != "db")
				{
					this.arrDataSize.InitAndSetArrayItem(var_params["size"], null);
				}
				else
				{
					this.arrDataSize.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["size"]), null);
				}
			}
			return null;
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			chart.InitAndSetArrayItem("scatter", "type");
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", true, "title", this.y_axis_label, "labels", new XVar("enabled", this.chrt_array["appearance"]["sval"] == "true"))), "yAxes");
			if(this.chrt_array["appearance"]["color61"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color61"]), "yAxes", 0, "labels", "fontColor");
			}
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			return null;
		}
		protected override XVar getSeriesType()
		{
			return "bubble";
		}
		protected override XVar getPoint(dynamic _param_seriesNumber, dynamic _param_row)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic row = XVar.Clone(_param_row);
			#endregion

			dynamic pointData = XVar.Array();
			pointData = XVar.Clone(base.getPoint((XVar)(seriesNumber), (XVar)(row)));
			pointData.InitAndSetArrayItem(chart_xmlencode((XVar)(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(row[this.arrDataSize[seriesNumber]])) + 0)), "size");
			return pointData;
		}
	}
	public partial class Chart_Gauge : Chart
	{
		protected dynamic arrGaugeColor = XVar.Array();
		protected dynamic gaugeType = XVar.Pack("");
		protected dynamic layout = XVar.Pack("");
		protected static bool skipChart_GaugeCtor = false;
		public Chart_Gauge(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_GaugeCtor)
			{
				skipChart_GaugeCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.gaugeType = XVar.Clone(param["gaugeType"]);
			this.layout = XVar.Clone(param["layout"]);
		}
		protected override XVar setSpecParams(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			base.setSpecParams((XVar)(var_params));
			if(var_params["name"] != "")
			{
				dynamic beginColor = null, endColor = null, gColor = null, k = null;
				k = new XVar(0);
				for(;(XVar)(MVCFunctions.is_array((XVar)(var_params["gaugeColorZone"])))  && (XVar)(k < MVCFunctions.count(var_params["gaugeColorZone"])); k++)
				{
					beginColor = XVar.Clone((double)var_params["gaugeColorZone"][k]["gaugeBeginColor"]);
					endColor = XVar.Clone((double)var_params["gaugeColorZone"][k]["gaugeEndColor"]);
					gColor = XVar.Clone(MVCFunctions.Concat("#", var_params["gaugeColorZone"][k]["gaugeColor"]));
					this.arrGaugeColor.InitAndSetArrayItem(new XVar(0, beginColor, 1, endColor, 2, gColor), MVCFunctions.count(this.arrDataSeries) - 1, null);
				}
			}
			return null;
		}
		public override XVar write()
		{
			dynamic chart = XVar.Array(), data = XVar.Array(), i = null;
			data = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrDataSeries); i++)
			{
				chart = XVar.Clone(XVar.Array());
				if(this.chrt_array["appearance"]["sanim"] == "true")
				{
					chart.InitAndSetArrayItem(new XVar("enabled", "true", "duration", 1000), "animation");
				}
				setGaugeSpecChartSettings((XVar)(chart), (XVar)(i));
				if((XVar)(this.chrt_array["appearance"]["color71"] != "")  || (XVar)(this.chrt_array["appearance"]["color91"] != ""))
				{
					chart.InitAndSetArrayItem(XVar.Array(), "background");
				}
				if(this.chrt_array["appearance"]["color71"] != "")
				{
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color71"]), "background", "fill");
				}
				if(this.chrt_array["appearance"]["color91"] != "")
				{
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color91"]), "background", "stroke");
				}
				if(XVar.Pack(this.noRecordsFound))
				{
					data.InitAndSetArrayItem(getNoDataMessage(), "noDataMessage");
					MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(data)));
					return null;
				}
				data.InitAndSetArrayItem(new XVar("gauge", chart), null);
			}
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(new XVar("gauge", data, "header", this.header, "footer", this.footer))));
			return null;
		}
		protected virtual XVar setGaugeSpecChartSettings(dynamic chart, dynamic _param_seriesNumber)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			#endregion

			dynamic series = XVar.Array();
			series = XVar.Clone(get_data());
			chart.InitAndSetArrayItem(series[seriesNumber]["data"], "data");
			chart.InitAndSetArrayItem(this.gaugeType, "type");
			chart.InitAndSetArrayItem(this.layout, "layout");
			chart.InitAndSetArrayItem(new XVar(0, getAxesSettings((XVar)(seriesNumber))), "axes");
			chart.InitAndSetArrayItem(false, "credits");
			chart.InitAndSetArrayItem(getCircularGaugeLabel((XVar)(seriesNumber), (XVar)(chart["data"][0])), "chartLabels");
			if(this.gaugeType == "circular")
			{
				chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", true)), "needles");
				chart.InitAndSetArrayItem(getColorRanges((XVar)(seriesNumber)), "ranges");
			}
			else
			{
				dynamic hasColorZones = null, scalesData = XVar.Array();
				hasColorZones = XVar.Clone((XVar)(0 < MVCFunctions.count(this.arrGaugeColor))  && (XVar)(this.arrGaugeColor.KeyExists(seriesNumber)));
				chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", true, "pointerType", "marker", "type", (XVar.Pack(this.layout == "horizontal") ? XVar.Pack("triangleUp") : XVar.Pack("triangleLeft")), "name", "", "offset", (XVar.Pack(hasColorZones) ? XVar.Pack("20%") : XVar.Pack("10%")), "dataIndex", 0)), "pointers");
				if(XVar.Pack(hasColorZones))
				{
					foreach (KeyValuePair<XVar, dynamic> val in this.arrGaugeColor[seriesNumber].GetEnumerator())
					{
						chart.InitAndSetArrayItem(new XVar("enabled", true, "pointerType", "rangeBar", "name", "", "offset", "10%", "dataIndex", val.Key + 1, "color", val.Value[2]), "pointers", null);
					}
				}
				scalesData = XVar.Clone(getGaugeScales((XVar)(seriesNumber)));
				chart.InitAndSetArrayItem(0, "scale");
				chart.InitAndSetArrayItem(new XVar(0, new XVar("maximum", scalesData["max"], "minimum", scalesData["min"], "ticks", new XVar("interval", scalesData["interval"]), "minorTicks", new XVar("interval", scalesData["interval"] / 2))), "scales");
			}
			return null;
		}
		protected virtual XVar getCircularGaugeLabel(dynamic _param_seriesNumber, dynamic _param_pointData)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic pointData = XVar.Clone(_param_pointData);
			#endregion

			dynamic label = XVar.Array();
			label = XVar.Clone(new XVar("enabled", true, "vAlign", "center", "hAlign", "center", "text", getChartLabelText((XVar)(seriesNumber), (XVar)(pointData["value"]))));
			if(this.gaugeType == "circular")
			{
				label.InitAndSetArrayItem(-150, "offsetY");
				label.InitAndSetArrayItem("center", "anchor");
				label.InitAndSetArrayItem(new XVar("enabled", true, "fill", "#fff", "cornerType", "round", "corner", 0), "background");
				label.InitAndSetArrayItem(new XVar("top", 15, "right", 20, "bottom", 15, "left", 20), "padding");
			}
			return new XVar(0, label);
		}
		protected virtual XVar getColorRanges(dynamic _param_seriesNumber)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			#endregion

			dynamic ranges = XVar.Array();
			ranges = XVar.Clone(XVar.Array());
			if((XVar)(0 < MVCFunctions.count(this.arrGaugeColor))  && (XVar)(this.arrGaugeColor.KeyExists(seriesNumber)))
			{
				foreach (KeyValuePair<XVar, dynamic> val in this.arrGaugeColor[seriesNumber].GetEnumerator())
				{
					ranges.InitAndSetArrayItem(new XVar("radius", 70, "from", val.Value[0], "to", val.Value[1], "fill", val.Value[2], "endSize", "10%", "startSize", "10%"), null);
				}
			}
			return ranges;
		}
		protected virtual XVar getAxesSettings(dynamic _param_seriesNumber)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			#endregion

			dynamic axes = XVar.Array();
			axes = XVar.Clone(XVar.Array());
			if(this.gaugeType == "circular")
			{
				dynamic scalesData = XVar.Array();
				axes.InitAndSetArrayItem(-150, "startAngle");
				axes.InitAndSetArrayItem(300, "sweepAngle");
				scalesData = XVar.Clone(getGaugeScales((XVar)(seriesNumber)));
				axes.InitAndSetArrayItem(new XVar("maximum", scalesData["max"], "minimum", scalesData["min"], "ticks", new XVar("interval", scalesData["interval"]), "minorTicks", new XVar("interval", scalesData["interval"] / 2)), "scale");
				axes.InitAndSetArrayItem(new XVar("type", "trapezoid", "interval", scalesData["interval"]), "ticks");
				axes.InitAndSetArrayItem(new XVar("enabled", true, "length", 2), "minorTicks");
				if(this.chrt_array["appearance"]["color131"] != "")
				{
					axes.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "fill");
				}
			}
			axes.InitAndSetArrayItem(true, "enabled");
			axes.InitAndSetArrayItem(new XVar("enabled", this.chrt_array["appearance"]["sval"] == "true"), "labels");
			if(this.chrt_array["appearance"]["color61"] != "")
			{
				axes.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color61"]), "labels", "fontColor");
			}
			return axes;
		}
		protected virtual XVar getGaugeScales(dynamic _param_seriesNumber)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			#endregion

			dynamic diff = null, interval = null, max = null, min = null, muls = XVar.Array(), slog = null;
			min = XVar.Clone(this.chrt_array["parameters"][seriesNumber]["gaugeMinValue"]);
			max = XVar.Clone(this.chrt_array["parameters"][seriesNumber]["gaugeMaxValue"]);
			if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(min))))
			{
				min = new XVar(0);
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(max))))
			{
				max = new XVar(100);
			}
			diff = XVar.Clone(max - min);
			slog = XVar.Clone((XVar)Math.Floor((double)((XVar)Math.Log10(diff))));
			interval = XVar.Clone((XVar)Math.Pow(10, slog - 2));
			muls = XVar.Clone(new XVar(0, 1, 1, 2, 2, 3, 3, 5, 4, 10));
			while(XVar.Pack(true))
			{
				foreach (KeyValuePair<XVar, dynamic> m in muls.GetEnumerator())
				{
					if(diff / (interval * m.Value) <= 10)
					{
						interval *= m.Value;
						break;
					}
				}
				if(diff / interval <= 10)
				{
					break;
				}
				interval *= 10;
			}
			return new XVar("min", min, "max", max, "interval", interval);
		}
		protected override XVar getSQL()
		{
			GlobalVars.strSQL = XVar.Clone(base.getSQL());
			if(this.table_type == "project")
			{
				dynamic g_orderindexes = XVar.Array(), p = null;
				g_orderindexes = XVar.Clone(GlobalVars.gSettings.getTableData(new XVar(".orderindexes")));
				p = XVar.Clone(MVCFunctions.strpos((XVar)(MVCFunctions.strtolower((XVar)(GlobalVars.strSQL))), new XVar("order by")));
				if((XVar)(XVar.Pack(0) < p)  && (XVar)(MVCFunctions.count(g_orderindexes)))
				{
					dynamic ob = null;
					ob = new XVar("ORDER BY");
					foreach (KeyValuePair<XVar, dynamic> val in g_orderindexes.GetEnumerator())
					{
						ob = MVCFunctions.Concat(ob, " ", val.Value[0], " ");
						if(val.Value[1] == "ASC")
						{
							ob = MVCFunctions.Concat(ob, "DESC");
						}
						else
						{
							ob = MVCFunctions.Concat(ob, "ASC");
						}
						if(val.Key + 1 != MVCFunctions.count(g_orderindexes))
						{
							ob = MVCFunctions.Concat(ob, ",");
						}
					}
					GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(GlobalVars.strSQL), new XVar(0), (XVar)(p)), ob));
				}
			}
			return GlobalVars.strSQL;
		}
		public override XVar get_data()
		{
			dynamic clickdata = XVar.Array(), data = XVar.Array(), i = null, qResult = null, row = null, series = XVar.Array();
			data = XVar.Clone(XVar.Array());
			qResult = XVar.Clone(this.connection.query((XVar)(this.strSQL)));
			if(this.cipherer != null)
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
			}
			else
			{
				row = XVar.Clone(qResult.fetchAssoc());
			}
			if(XVar.Pack(!(XVar)(row)))
			{
				this.noRecordsFound = new XVar(true);
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrDataSeries); i++)
			{
				if(XVar.Pack(row))
				{
					data.InitAndSetArrayItem(XVar.Array(), i);
					data.InitAndSetArrayItem(getPoint((XVar)(i), (XVar)(row)), i, null);
				}
			}
			series = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrDataSeries); i++)
			{
				series.InitAndSetArrayItem(getSeriesData((XVar)(this.arrDataLabels[i]), (XVar)(data[i]), (XVar)(clickdata[i]), (XVar)(i), (XVar)(1 < MVCFunctions.count(this.arrDataSeries))), null);
			}
			return series;
		}
		protected override XVar getSeriesData(dynamic _param_name, dynamic _param_pointsData, dynamic _param_clickData, dynamic _param_seriesNumber, dynamic _param_multiSeries = null)
		{
			#region default values
			if(_param_multiSeries as Object == null) _param_multiSeries = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic pointsData = XVar.Clone(_param_pointsData);
			dynamic clickData = XVar.Clone(_param_clickData);
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic multiSeries = XVar.Clone(_param_multiSeries);
			#endregion

			if((XVar)((XVar)(this.gaugeType == "linearGauge")  && (XVar)(0 < MVCFunctions.count(this.arrGaugeColor)))  && (XVar)(this.arrGaugeColor.KeyExists(seriesNumber)))
			{
				foreach (KeyValuePair<XVar, dynamic> val in this.arrGaugeColor[seriesNumber].GetEnumerator())
				{
					pointsData.InitAndSetArrayItem(new XVar("low", val.Value[0], "high", val.Value[1]), null);
				}
			}
			return new XVar("data", pointsData, "labelText", getChartLabelText((XVar)(seriesNumber), (XVar)(pointsData[0]["value"])));
		}
		protected virtual XVar getChartLabelText(dynamic _param_seriesNumber, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if((XVar)(this.table_type == "project")  && (XVar)(!(XVar)(this.webchart)))
			{
				dynamic data = null, fieldName = null, viewControls = null, viewValue = null;
				fieldName = XVar.Clone(this.arrDataSeries[seriesNumber]);
				viewControls = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), new XVar(Constants.PAGE_CHART)));
				data = XVar.Clone(new XVar(fieldName, value));
				viewValue = XVar.Clone(CommonFunctions.html_special_decode((XVar)(viewControls.showDBValue((XVar)(fieldName), (XVar)(data)))));
				return MVCFunctions.Concat(this.arrDataLabels[seriesNumber], ": ", viewValue);
			}
			return MVCFunctions.Concat(this.arrDataLabels[seriesNumber], ": ", value);
		}
	}
	public partial class Chart_Ohlc : Chart
	{
		protected dynamic ohcl_type;
		protected dynamic arrOHLC_high = XVar.Array();
		protected dynamic arrOHLC_low = XVar.Array();
		protected dynamic arrOHLC_open = XVar.Array();
		protected dynamic arrOHLC_close = XVar.Array();
		protected static bool skipChart_OhlcCtor = false;
		public Chart_Ohlc(dynamic ch_array, dynamic _param_param)
			:base((XVar)ch_array, (XVar)_param_param)
		{
			if(skipChart_OhlcCtor)
			{
				skipChart_OhlcCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic param = XVar.Clone(_param_param);
			#endregion

			this.ohcl_type = XVar.Clone(param["ohcl_type"]);
		}
		protected override XVar setSpecParams(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			if(this.table_type != "db")
			{
				this.arrOHLC_open.InitAndSetArrayItem(var_params["ohlcOpen"], null);
				this.arrOHLC_high.InitAndSetArrayItem(var_params["ohlcHigh"], null);
				this.arrOHLC_low.InitAndSetArrayItem(var_params["ohlcLow"], null);
				this.arrOHLC_close.InitAndSetArrayItem(var_params["ohlcClose"], null);
				return null;
			}
			if(XVar.Pack(var_params["agr_func"]))
			{
				this.arrOHLC_open.InitAndSetArrayItem(MVCFunctions.Concat(var_params["agr_func"], "_", var_params["table"], "_", var_params["ohlcOpen"]), null);
				this.arrOHLC_high.InitAndSetArrayItem(MVCFunctions.Concat(var_params["agr_func"], "_", var_params["table"], "_", var_params["ohlcHigh"]), null);
				this.arrOHLC_low.InitAndSetArrayItem(MVCFunctions.Concat(var_params["agr_func"], "_", var_params["table"], "_", var_params["ohlcLow"]), null);
				this.arrOHLC_close.InitAndSetArrayItem(MVCFunctions.Concat(var_params["agr_func"], "_", var_params["table"], "_", var_params["ohlcClose"]), null);
			}
			else
			{
				this.arrOHLC_open.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["ohlcOpen"]), null);
				this.arrOHLC_high.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["ohlcHigh"]), null);
				this.arrOHLC_low.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["ohlcLow"]), null);
				this.arrOHLC_close.InitAndSetArrayItem(MVCFunctions.Concat(var_params["table"], "_", var_params["ohlcClose"]), null);
			}
			return null;
		}
		public override XVar write()
		{
			dynamic chart = XVar.Array(), data = XVar.Array();
			data = XVar.Clone(XVar.Array());
			chart = XVar.Clone(XVar.Array());
			setTypeSpecChartSettings((XVar)(chart));
			if((XVar)(this.chrt_array["appearance"]["color71"] != "")  || (XVar)(this.chrt_array["appearance"]["color91"] != ""))
			{
				chart.InitAndSetArrayItem(XVar.Array(), "background");
			}
			if(this.chrt_array["appearance"]["color71"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color71"]), "background", "fill");
			}
			if(this.chrt_array["appearance"]["color91"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color91"]), "background", "stroke");
			}
			chart.InitAndSetArrayItem(false, "credits");
			chart.InitAndSetArrayItem(new XVar("enabled", "true", "text", this.header), "title");
			if(this.chrt_array["appearance"]["color101"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color101"]), "title", "fontColor");
			}
			if((XVar)(this.chrt_array["appearance"]["slegend"] == "true")  && (XVar)(!(XVar)(this.chartPreview)))
			{
				chart.InitAndSetArrayItem(new XVar("enabled", "true"), "legend");
			}
			data.InitAndSetArrayItem(chart, "chart");
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(data)));
			return null;
		}
		protected override XVar setTypeSpecChartSettings(dynamic chart)
		{
			chart.InitAndSetArrayItem(get_data(), "series");
			foreach (KeyValuePair<XVar, dynamic> var_params in this.chrt_array["parameters"].GetEnumerator())
			{
				if(var_params.Value["ohlcColor"] != "")
				{
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcColor"]), "series", var_params.Key, "fallingStroke");
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcColor"]), "series", var_params.Key, "fallingFill");
					if(this.ohcl_type == "ohcl")
					{
						chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcColor"]), "series", var_params.Key, "risingStroke");
						chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcColor"]), "series", var_params.Key, "risingFill");
					}
				}
				if((XVar)(var_params.Value["ohlcCandleColor"] != "")  && (XVar)(this.ohcl_type != "ohcl"))
				{
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcCandleColor"]), "series", var_params.Key, "risingStroke");
					chart.InitAndSetArrayItem(MVCFunctions.Concat("#", var_params.Value["ohlcCandleColor"]), "series", var_params.Key, "risingFill");
				}
			}
			chart.InitAndSetArrayItem(getGrids(), "grids");
			chart.InitAndSetArrayItem("financial", "type");
			chart.InitAndSetArrayItem(0, "xScale");
			chart.InitAndSetArrayItem(1, "yScale");
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", this.y_axis_label, "labels", new XVar("enabled", this.chrt_array["appearance"]["sval"] == "true"))), "yAxes");
			if(this.chrt_array["appearance"]["color61"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color61"]), "yAxes", 0, "labels", "fontColor");
			}
			chart.InitAndSetArrayItem(new XVar(0, new XVar("enabled", "true", "title", new XVar("text", this.footer), "labels", new XVar("enabled", this.chrt_array["appearance"]["sname"] == "true"))), "xAxes");
			if(this.chrt_array["appearance"]["color51"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color51"]), "xAxes", 0, "labels", "fontColor");
			}
			if(this.chrt_array["appearance"]["color111"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color111"]), "xAxes", 0, "title", "fontColor");
			}
			if(this.chrt_array["appearance"]["color131"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color131"]), "xAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["color141"] != "")
			{
				chart.InitAndSetArrayItem(MVCFunctions.Concat("#", this.chrt_array["appearance"]["color141"]), "yAxes", 0, "stroke");
			}
			if(this.chrt_array["appearance"]["slog"] == "true")
			{
				chart.InitAndSetArrayItem(new XVar(0, new XVar("names", XVar.Array()), 1, new XVar("logBase", 10, "type", "log")), "scales");
			}
			return null;
		}
		public override XVar get_data()
		{
			dynamic clickdata = XVar.Array(), data = XVar.Array(), i = null, qResult = null, row = null, series = XVar.Array(), strLabelFormat = null;
			data = XVar.Clone(XVar.Array());
			clickdata = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrOHLC_open); i++)
			{
				data.InitAndSetArrayItem(XVar.Array(), i);
				clickdata.InitAndSetArrayItem(XVar.Array(), i);
			}
			qResult = XVar.Clone(this.connection.query((XVar)(this.strSQL)));
			if(this.cipherer != null)
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
			}
			else
			{
				row = XVar.Clone(qResult.fetchAssoc());
			}
			if(XVar.Pack(!(XVar)(row)))
			{
				this.noRecordsFound = new XVar(true);
			}
			while(XVar.Pack(row))
			{
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.arrOHLC_open); i++)
				{
					data.InitAndSetArrayItem(getPoint((XVar)(i), (XVar)(row)), i, null);
					strLabelFormat = XVar.Clone(labelFormat((XVar)(this.strLabel), (XVar)(row)));
					clickdata.InitAndSetArrayItem(getActions((XVar)(row), (XVar)(this.arrDataSeries[i]), (XVar)(strLabelFormat)), i, null);
				}
				if(this.cipherer != null)
				{
					row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
				}
				else
				{
					row = XVar.Clone(qResult.fetchAssoc());
				}
			}
			this.connection.close();
			series = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrOHLC_open); i++)
			{
				series.InitAndSetArrayItem(getSeriesData((XVar)(this.arrDataLabels[i]), (XVar)(data[i]), (XVar)(clickdata[i]), (XVar)(i)), null);
			}
			return series;
		}
		protected override XVar getSeriesType()
		{
			if(this.ohcl_type == "ohcl")
			{
				return "ohlc";
			}
			return "candlestick";
		}
		protected override XVar getSeriesTooltip(dynamic _param_multiSeries)
		{
			#region pass-by-value parameters
			dynamic multiSeries = XVar.Clone(_param_multiSeries);
			#endregion

			dynamic tooltipSettings = null;
			tooltipSettings = XVar.Clone(new XVar("enabled", true));
			return tooltipSettings;
		}
		protected override XVar getPoint(dynamic _param_seriesNumber, dynamic _param_row)
		{
			#region pass-by-value parameters
			dynamic seriesNumber = XVar.Clone(_param_seriesNumber);
			dynamic row = XVar.Clone(_param_row);
			#endregion

			dynamic close = null, high = null, low = null, open = null;
			if((XVar)(this.table_type != "db")  || (XVar)(!(XVar)(MVCFunctions.count(this.chrt_array["customLabels"]))))
			{
				high = XVar.Clone(row[this.arrOHLC_high[seriesNumber]]);
				low = XVar.Clone(row[this.arrOHLC_low[seriesNumber]]);
				open = XVar.Clone(row[this.arrOHLC_open[seriesNumber]]);
				close = XVar.Clone(row[this.arrOHLC_close[seriesNumber]]);
			}
			else
			{
				high = XVar.Clone(row[this.chrt_array["customLabels"][this.arrOHLC_high[seriesNumber]]]);
				low = XVar.Clone(row[this.chrt_array["customLabels"][this.arrOHLC_low[seriesNumber]]]);
				open = XVar.Clone(row[this.chrt_array["customLabels"][this.arrOHLC_open[seriesNumber]]]);
				close = XVar.Clone(row[this.chrt_array["customLabels"][this.arrOHLC_close[seriesNumber]]]);
			}
			return new XVar("x", labelFormat((XVar)(this.strLabel), (XVar)(row)), "open", chart_xmlencode((XVar)(open + 0)), "high", chart_xmlencode((XVar)(high + 0)), "low", chart_xmlencode((XVar)(low + 0)), "close", chart_xmlencode((XVar)(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(close)) + 0)));
		}
	}
}
