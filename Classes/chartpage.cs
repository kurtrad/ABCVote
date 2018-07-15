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
	public partial class ChartPage : RunnerPage
	{
		public dynamic show_message_block = XVar.Pack(false);
		protected static bool skipChartPageCtor = false;
		public ChartPage(dynamic var_params = null)
			:base((XVar)var_params)
		{
			if(skipChartPageCtor)
			{
				skipChartPageCtor = false;
				return;
			}
			#region default values
			if(var_params as Object == null) var_params = new XVar("");
			#endregion

			this.jsSettings.InitAndSetArrayItem(this.searchClauseObj.simpleSearchActive, "tableSettings", this.tName, "simpleSearchActive");
			this.jsSettings.InitAndSetArrayItem(getStartMasterKeys(), "tableSettings", this.tName, "startMasterKeys");
		}
		protected override XVar assignSessionPrefix()
		{
			if(this.mode == Constants.CHART_DASHBOARD)
			{
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
			}
			else
			{
				this.sessionPrefix = XVar.Clone(this.tName);
			}
			return null;
		}
		public override XVar buildSearchPanel()
		{
			if(this.mode == Constants.CHART_DASHBOARD)
			{
				return null;
			}
			base.buildSearchPanel();
			return null;
		}
		public virtual XVar process()
		{
			if((XVar)(this.mode == Constants.CHART_DASHDETAILS)  || (XVar)((XVar)(this.mode == Constants.CHART_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_LIST)  || (XVar)(this.masterPageType == Constants.PAGE_REPORT))))
			{
				updateDetailsTabTitles();
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessChart"))))
			{
				this.eventsObject.BeforeProcessChart(this);
			}
			processGridTabs();
			doCommonAssignments();
			addButtonHandlers();
			addCommonJs();
			commonAssign();
			buildSearchPanel();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
			displayMasterTableInfo();
			showPage();
			return null;
		}
		protected override XVar getRowCountByTab(dynamic _param_tab)
		{
			#region pass-by-value parameters
			dynamic tab = XVar.Clone(_param_tab);
			#endregion

			dynamic countSQL = null, sql = XVar.Array();
			sql = XVar.Clone(getTabSQLComponents((XVar)(tab)));
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			countSQL = XVar.Clone(GlobalVars.strSQL);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryChart"))))
			{
				dynamic sqlModifiedInEvent = null, strSQLbak = null, strWhereBak = null, tstrWhereClause = null, whereModifiedInEvent = null;
				strSQLbak = XVar.Clone(GlobalVars.strSQL);
				sqlModifiedInEvent = new XVar(false);
				whereModifiedInEvent = new XVar(false);
				tstrWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				strWhereBak = XVar.Clone(tstrWhereClause);
				this.eventsObject.BeforeQueryChart((XVar)(GlobalVars.strSQL), ref tstrWhereClause, new XVar(""));
				whereModifiedInEvent = XVar.Clone(tstrWhereClause != strWhereBak);
				sqlModifiedInEvent = XVar.Clone(GlobalVars.strSQL != strSQLbak);
				if(XVar.Pack(sqlModifiedInEvent))
				{
					return limitRowCount((XVar)(CommonFunctions.GetRowCount((XVar)(GlobalVars.strSQL), (XVar)(this.connection))));
				}
				if(XVar.Pack(whereModifiedInEvent))
				{
					countSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, tstrWhereClause)), (XVar)(sql["mandatoryHaving"])));
				}
			}
			return limitRowCount((XVar)(this.connection.getFetchedRowsNumber((XVar)(countSQL))));
		}
		public override XVar getMasterTableSQLClause(dynamic _param_basedOnProp = null)
		{
			#region default values
			if(_param_basedOnProp as Object == null) _param_basedOnProp = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic basedOnProp = XVar.Clone(_param_basedOnProp);
			#endregion

			if(this.mode == Constants.CHART_DASHBOARD)
			{
				return "";
			}
			return base.getMasterTableSQLClause();
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ROW_NUMBER() over () as DB2_ROW_NUMBER ");
			}
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Search"), (XVar)(this.tName)), "mandatoryWhere", null);
			return sql;
		}
		public virtual XVar getStartMasterKeys()
		{
			dynamic data = XVar.Array(), detailTablesData = XVar.Array(), fetchedArray = null, masterKeysArr = XVar.Array(), rs = null, sql = XVar.Array();
			sql = XVar.Clone(getSubsetSQLComponents());
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, getOrderByClause());
			rs = XVar.Clone(this.connection.queryPage((XVar)(GlobalVars.strSQL), new XVar(1), new XVar(1), new XVar(true)));
			fetchedArray = XVar.Clone(rs.fetchAssoc());
			data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
			detailTablesData = XVar.Clone(this.pSet.getDetailTablesArr());
			masterKeysArr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> detail in detailTablesData.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> mk in detail.Value["masterKeys"].GetEnumerator())
				{
					masterKeysArr.InitAndSetArrayItem(new XVar(MVCFunctions.Concat("masterkey", mk.Key + 1), data[mk.Value]), detail.Value["dDataSourceTable"]);
				}
			}
			return masterKeysArr;
		}
		public virtual XVar doCommonAssignments()
		{
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.xt.assign(new XVar("searchPanel"), new XVar(true));
			if(XVar.Pack(isShowMenu()))
			{
				this.xt.assign(new XVar("menu_block"), new XVar(true));
			}
			this.xt.assign(new XVar("chart_block"), new XVar(true));
			this.xt.assign(new XVar("asearch_link"), new XVar(true));
			this.xt.assign(new XVar("exportpdflink_attrs"), new XVar("onclick='chart.saveAsPDF();'"));
			this.xt.assign(new XVar("advsearchlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"advButton", this.id, "\"")));
			if(XVar.Pack(!(XVar)(CommonFunctions.GetChartXML((XVar)(this.shortTableName)))))
			{
				this.xt.assign(new XVar("chart_block"), new XVar(false));
			}
			this.xt.assign(new XVar("message_block"), new XVar(true));
			if((XVar)((XVar)((XVar)(this.mode == Constants.CHART_SIMPLE)  || (XVar)(this.mode == Constants.CHART_DASHBOARD))  && (XVar)(this.pSet.noRecordsOnFirstPage()))  && (XVar)(!(XVar)(this.searchClauseObj.isSearchFunctionalityActivated())))
			{
				this.show_message_block = new XVar(true);
				this.xt.displayBrickHidden(new XVar("chart"));
				this.xt.assign(new XVar("chart_block"), new XVar(false));
				this.xt.assign(new XVar("message"), (XVar)(noRecordsMessage()));
				this.xt.assign(new XVar("message_class"), new XVar("alert-warning"));
			}
			if(XVar.Pack(!(XVar)(this.show_message_block)))
			{
				this.xt.displayBrickHidden(new XVar("message"));
			}
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.xt.assign(new XVar("tableinfomobile_block"), new XVar(true));
			}
			prepareBreadcrumbs(new XVar("main"));
			assignChartElement();
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage((XVar)(this.isDisplayLoading)));
			if((XVar)(!(XVar)(isDashboardElement()))  && (XVar)(!(XVar)(mobileTemplateMode())))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div id=\"search_suggest\" class=\"search_suggest\"></div>");
			}
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public virtual XVar assignChartElement()
		{
			dynamic chartXtParams = XVar.Array(), resizeChart = null;
			resizeChart = new XVar(true);
			if((XVar)((XVar)(this.mode == Constants.CHART_SIMPLE)  || (XVar)(this.mode == Constants.CHART_DASHBOARD))  || (XVar)((XVar)(this.mode == Constants.CHART_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_VIEW)  || (XVar)(this.masterPageType == Constants.PAGE_EDIT))))
			{
				resizeChart = new XVar(false);
			}
			chartXtParams = XVar.Clone(new XVar("id", this.id, "table", this.tName, "ctype", this.pSet.getChartType(), "resize", resizeChart, "chartName", this.shortTableName, "chartPreview", (XVar)(!XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.CHART_SIMPLE)))  && (XVar)(this.mode != Constants.CHART_DASHBOARD)));
			if((XVar)(this.mode == Constants.CHART_DASHBOARD)  || (XVar)(this.mode == Constants.CHART_DASHDETAILS))
			{
				if((XVar)(this.dashElementData.KeyExists("width"))  || (XVar)(this.dashElementData.KeyExists("height")))
				{
					chartXtParams.InitAndSetArrayItem(true, "dashResize");
					chartXtParams.InitAndSetArrayItem(this.dashElementData["width"], "dashWidth");
					chartXtParams.InitAndSetArrayItem(this.dashElementData["height"], "dashHeight");
				}
				if(this.mode == Constants.CHART_DASHBOARD)
				{
					chartXtParams.InitAndSetArrayItem(true, "dash");
					chartXtParams.InitAndSetArrayItem(this.dashTName, "dashTName");
					chartXtParams.InitAndSetArrayItem(this.dashElementName, "dashElementName");
				}
				else
				{
					chartXtParams.InitAndSetArrayItem(this.dashElementData["reload"], "refreshTime");
				}
			}
			this.xt.assign_function((XVar)(MVCFunctions.Concat(this.shortTableName, "_chart")), new XVar("xt_showchart"), (XVar)(chartXtParams));
			return null;
		}
		public virtual XVar prepareDetailsForEditViewPage()
		{
			addButtonHandlers();
			this.xt.assign(new XVar("body"), (XVar)(this.body));
			this.xt.assign(new XVar("chart_block"), new XVar(true));
			this.xt.assign(new XVar("message_block"), new XVar(true));
			return null;
		}
		protected override XVar getExtraAjaxPageParams()
		{
			dynamic returnJSON = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			if(this.mode == Constants.REPORT_DETAILS)
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(getProceedLink(), returnJSON["headerCont"]), "headerCont");
			}
			return returnJSON;
		}
		public virtual XVar beforeShowChart()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowChart"))))
			{
				this.eventsObject.BeforeShowChart((XVar)(this.xt), ref this.templatefile, this);
			}
			return null;
		}
		public virtual XVar showPage()
		{
			dynamic bricksExcept = XVar.Array();
			beforeShowChart();
			if((XVar)((XVar)(this.mode == Constants.CHART_DETAILS)  || (XVar)(this.mode == Constants.CHART_DASHBOARD))  || (XVar)(this.mode == Constants.CHART_DASHDETAILS))
			{
				addControlsJSAndCSS();
				fillSetCntrlMaps();
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.body.InitAndSetArrayItem("", "begin");
				this.body.InitAndSetArrayItem("", "end");
				this.xt.assign(new XVar("body"), (XVar)(this.body));
				bricksExcept = XVar.Clone(new XVar(0, "chart", 1, "message"));
				if(XVar.Pack(displayTabsInPage()))
				{
					bricksExcept.InitAndSetArrayItem("bsgrid_tabs", null);
				}
				this.xt.hideAllBricksExcept((XVar)(bricksExcept));
				displayAJAX((XVar)(this.templatefile), (XVar)(this.id + 1));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			if(this.mode == Constants.CHART_POPUPDETAILS)
			{
				dynamic respArr = XVar.Array();
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination"));
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.body.InitAndSetArrayItem("", "begin");
				this.body.InitAndSetArrayItem("", "end");
				this.xt.hideAllBricksExcept((XVar)(bricksExcept));
				this.xt.prepare_template((XVar)(this.templatefile));
				respArr = XVar.Clone(XVar.Array());
				respArr.InitAndSetArrayItem(true, "success");
				respArr.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "body");
				respArr.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("counter")), "counter");
				this.xt.assign(new XVar("container_master"), new XVar(false));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respArr)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			display((XVar)(this.templatefile));
			return null;
		}
		public override XVar processGridTabs()
		{
			dynamic ctChanged = null;
			ctChanged = XVar.Clone(base.processGridTabs());
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_chartTabWhere")] = getCurrentTabWhere();
			return ctChanged;
		}
		public override XVar gridTabsAvailable()
		{
			return true;
		}
		public override XVar displayTabsInPage()
		{
			return (XVar)(simpleMode())  || (XVar)((XVar)(this.mode == Constants.CHART_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_VIEW)  || (XVar)(this.masterPageType == Constants.PAGE_EDIT)));
		}
	}
}
