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
	public partial class ReportPage : RunnerPage
	{
		public dynamic pagestart = XVar.Pack(0);
		public dynamic arrReport = XVar.Array();
		public dynamic arrGroupsPerPage = XVar.Array();
		public dynamic crossTable = XVar.Pack(false);
		public dynamic crosstableRefresh = XVar.Pack(false);
		protected dynamic noRecordsFound = XVar.Pack(false);
		protected dynamic crossTableObj = XVar.Pack(null);
		public dynamic x;
		public dynamic y;
		public dynamic dataField;
		public dynamic operation;
		public dynamic xType;
		public dynamic yType;
		protected static bool skipReportPageCtor = false;
		public ReportPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipReportPageCtor)
			{
				skipReportPageCtor = false;
				return;
			}
			this.crossTable = XVar.Clone(this.pSet.isCrossTabReport());
			this.jsSettings.InitAndSetArrayItem(this.crossTable, "tableSettings", this.tName, "crossTable");
			this.jsSettings.InitAndSetArrayItem(this.searchClauseObj.simpleSearchActive, "tableSettings", this.tName, "simpleSearchActive");
			if((XVar)((XVar)(this.mode == Constants.REPORT_DASHBOARD)  || (XVar)(this.mode == Constants.REPORT_DETAILS))  || (XVar)(this.mode == Constants.REPORT_DASHDETAILS))
			{
				if(this.mode != Constants.REPORT_DETAILS)
				{
					this.formBricks.InitAndSetArrayItem(new XVar(0, new XVar("name", "details_found", "align", "right")), "header");
				}
				this.formBricks.InitAndSetArrayItem(new XVar(0, "pagination_block"), "footer");
			}
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "pdfSettings");
			this.controlsMap.InitAndSetArrayItem(0, "pdfSettings", "allPagesMode");
		}
		protected override XVar assignSessionPrefix()
		{
			if(this.mode == Constants.REPORT_DASHBOARD)
			{
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
			}
			else
			{
				this.sessionPrefix = XVar.Clone(this.tName);
			}
			return null;
		}
		public override XVar setSessionVariables()
		{
			base.setSessionVariables();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
			if(XVar.Pack(!(XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")])))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")] = this.pSet.getInitialPageSize();
			}
			this.pageSize = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")]);
			if(XVar.Pack(!(XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")])))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("goto")))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = MVCFunctions.intval((XVar)(MVCFunctions.postvalue("goto")));
			}
			this.myPage = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")]);
			this.pagestart = XVar.Clone((this.myPage - 1) * this.pageSize);
			return null;
		}
		public override XVar buildSearchPanel()
		{
			if(this.mode == Constants.REPORT_DASHBOARD)
			{
				return null;
			}
			base.buildSearchPanel();
			return null;
		}
		public virtual XVar process()
		{
			dynamic extraParams = XVar.Array();
			if((XVar)(this.mode == Constants.REPORT_DASHDETAILS)  || (XVar)((XVar)(this.mode == Constants.REPORT_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_LIST)  || (XVar)(this.masterPageType == Constants.PAGE_REPORT))))
			{
				updateDetailsTabTitles();
			}
			if((XVar)(this.crossTable)  && (XVar)(!(XVar)(checkCrossParams())))
			{
				if(this.mode == Constants.REPORT_SIMPLE)
				{
					crossTableBaseRedirect();
					return null;
				}
				setDefaultParams();
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessReport"))))
			{
				this.eventsObject.BeforeProcessReport(this);
			}
			setDetailsBadgeStyles();
			extraParams = XVar.Clone(getExtraReportParams());
			setGoogleMapsParams((XVar)(extraParams["fieldsArr"]));
			if(XVar.Pack(this.googleMapCfg["isUseGoogleMap"]))
			{
				initGmaps();
			}
			buildSearchPanel();
			processGridTabs();
			setReportData((XVar)(extraParams));
			addCommonJs();
			addButtonHandlers();
			commonAssign();
			doCommonAssignments();
			addCustomCss();
			if(this.mode == Constants.REPORT_SIMPLE)
			{
				displayMasterTableInfo();
			}
			showPage();
			return null;
		}
		protected virtual XVar checkCrossParams()
		{
			if(XVar.Pack(!(XVar)(this.crossTable)))
			{
				return true;
			}
			return (XVar)((XVar)(MVCFunctions.strlen((XVar)(this.x)))  && (XVar)(MVCFunctions.strlen((XVar)(this.y))))  && (XVar)(MVCFunctions.strlen((XVar)(this.dataField)));
		}
		protected virtual XVar setDefaultParams()
		{
			dynamic prms = XVar.Array();
			prms = XVar.Clone(getDefaultCrossParams());
			this.x = XVar.Clone(prms["x"]);
			this.y = XVar.Clone(prms["y"]);
			this.dataField = XVar.Clone(prms["data"]);
			this.operation = XVar.Clone(prms["op"]);
			return null;
		}
		protected virtual XVar getDefaultCrossParams()
		{
			dynamic allNames = XVar.Array(), dataField = null, operation = null, reportFields = XVar.Array(), x = null, xNames = XVar.Array(), y = null, yNames = XVar.Array();
			if(XVar.Pack(!(XVar)(this.crossTable)))
			{
				return XVar.Array();
			}
			xNames = XVar.Clone(XVar.Array());
			yNames = XVar.Clone(XVar.Array());
			allNames = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in this.pSet.getReportGroupFieldsData().GetEnumerator())
			{
				dynamic axisName = null;
				axisName = XVar.Clone(value.Value["strGroupField"]);
				if(value.Value["crossTabAxis"] == 0)
				{
					xNames.InitAndSetArrayItem(axisName, null);
				}
				else
				{
					if(value.Value["crossTabAxis"] == 1)
					{
						yNames.InitAndSetArrayItem(axisName, null);
					}
					else
					{
						allNames.InitAndSetArrayItem(axisName, null);
					}
				}
			}
			if(0 < MVCFunctions.count(xNames))
			{
				x = XVar.Clone(xNames[0]);
			}
			else
			{
				x = XVar.Clone(allNames[0]);
			}
			if(0 < MVCFunctions.count(yNames))
			{
				y = XVar.Clone(yNames[0]);
			}
			else
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.count(xNames))))
				{
					y = XVar.Clone(allNames[1]);
				}
				else
				{
					y = XVar.Clone(allNames[0]);
				}
			}
			dataField = new XVar("");
			operation = new XVar("");
			reportFields = XVar.Clone(this.pSet.getFieldsList());
			foreach (KeyValuePair<XVar, dynamic> field in reportFields.GetEnumerator())
			{
				operation = new XVar("");
				if(XVar.Pack(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalMax"))))
				{
					operation = new XVar("max");
				}
				else
				{
					if(XVar.Pack(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalMin"))))
					{
						operation = new XVar("min");
					}
					else
					{
						if(XVar.Pack(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalAvg"))))
						{
							operation = new XVar("avg");
						}
						else
						{
							if(XVar.Pack(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalSum"))))
							{
								operation = new XVar("sum");
							}
						}
					}
				}
				if(XVar.Pack(operation))
				{
					dataField = XVar.Clone(field.Value);
					break;
				}
			}
			return new XVar("x", x, "y", y, "data", dataField, "op", operation);
		}
		protected virtual XVar getCurrentCrossParams()
		{
			dynamic prms = XVar.Array();
			prms = XVar.Clone(new XVar("x", this.x, "y", this.y, "data", this.dataField, "op", this.operation));
			if(XVar.Pack(this.xType))
			{
				prms.InitAndSetArrayItem(this.xType, "xtype");
			}
			if(XVar.Pack(this.yType))
			{
				prms.InitAndSetArrayItem(this.yType, "ytype");
			}
			return prms;
		}
		protected virtual XVar getDefaultCrossParamsString()
		{
			dynamic prms = XVar.Array();
			prms = XVar.Clone(getDefaultCrossParams());
			return MVCFunctions.Concat("x=", prms["x"], "&y=", prms["y"], "&data=", prms["data"], "&op=", prms["op"]);
		}
		protected virtual XVar crossTableBaseRedirect()
		{
			MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), (XVar)(getPageType()), (XVar)(getDefaultCrossParamsString()));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public virtual XVar setReportData(dynamic _param_options)
		{
			#region pass-by-value parameters
			dynamic options = XVar.Clone(_param_options);
			#endregion

			if(XVar.Pack(this.crossTable))
			{
				setCrosstabData((XVar)(options));
			}
			else
			{
				setStandartData((XVar)(options));
			}
			return null;
		}
		protected virtual XVar getCrossGroupFieldsSettings(dynamic _param_repGroupFields)
		{
			#region pass-by-value parameters
			dynamic repGroupFields = XVar.Clone(_param_repGroupFields);
			#endregion

			dynamic groupFields = XVar.Array(), xFieldsCount = XVar.Array(), xNames = XVar.Array(), yFieldsCount = XVar.Array(), yNames = XVar.Array();
			groupFields = XVar.Clone(XVar.Array());
			xNames = XVar.Clone(XVar.Array());
			yNames = XVar.Clone(XVar.Array());
			xFieldsCount = XVar.Clone(XVar.Array());
			yFieldsCount = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in repGroupFields.GetEnumerator())
			{
				if((XVar)(value.Value["crossTabAxis"] == 0)  || (XVar)(value.Value["crossTabAxis"] == 2))
				{
					++(xFieldsCount[value.Value["strGroupField"]]);
				}
				if((XVar)(value.Value["crossTabAxis"] == 1)  || (XVar)(value.Value["crossTabAxis"] == 2))
				{
					++(yFieldsCount[value.Value["strGroupField"]]);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> value in repGroupFields.GetEnumerator())
			{
				dynamic xAxis = null, yAxis = null;
				groupFields.InitAndSetArrayItem(value.Value["strGroupField"], value.Key, "name");
				xAxis = XVar.Clone((XVar)(value.Value["crossTabAxis"] == 0)  || (XVar)(value.Value["crossTabAxis"] == 2));
				yAxis = XVar.Clone((XVar)(value.Value["crossTabAxis"] == 1)  || (XVar)(value.Value["crossTabAxis"] == 2));
				if((XVar)((XVar)(0 == value.Value["groupInterval"])  || (XVar)((XVar)(xAxis)  && (XVar)(xFieldsCount[value.Value["strGroupField"]] < 2)))  || (XVar)((XVar)(yAxis)  && (XVar)(yFieldsCount[value.Value["strGroupField"]] < 2)))
				{
					groupFields.InitAndSetArrayItem(this.pSet.label((XVar)(value.Value["strGroupField"])), value.Key, "label");
				}
				else
				{
					groupFields.InitAndSetArrayItem(MVCFunctions.Concat(this.pSet.label((XVar)(value.Value["strGroupField"])), " - ", CrossTableReport.getCrossIntervalName((XVar)(this.pSet.getFieldType((XVar)(value.Value["strGroupField"]))), (XVar)(value.Value["groupInterval"]))), value.Key, "label");
				}
				groupFields.InitAndSetArrayItem(true, value.Key, "uniqueName");
				groupFields.InitAndSetArrayItem(value.Value["groupInterval"], value.Key, "int_type");
				if(XVar.Pack(!(XVar)(xNames[value.Value["strGroupField"]])))
				{
					xNames.InitAndSetArrayItem(XVar.Array(), value.Value["strGroupField"]);
				}
				if(XVar.Pack(!(XVar)(yNames[value.Value["strGroupField"]])))
				{
					yNames.InitAndSetArrayItem(XVar.Array(), value.Value["strGroupField"]);
				}
				if(value.Value["crossTabAxis"] == 0)
				{
					groupFields.InitAndSetArrayItem("x", value.Key, "group_type");
					xNames.InitAndSetArrayItem(value.Key, value.Value["strGroupField"], null);
				}
				else
				{
					if(value.Value["crossTabAxis"] == 1)
					{
						groupFields.InitAndSetArrayItem("y", value.Key, "group_type");
						yNames.InitAndSetArrayItem(value.Key, value.Value["strGroupField"], null);
					}
					else
					{
						groupFields.InitAndSetArrayItem("all", value.Key, "group_type");
						xNames.InitAndSetArrayItem(value.Key, value.Value["strGroupField"], null);
						yNames.InitAndSetArrayItem(value.Key, value.Value["strGroupField"], null);
					}
				}
			}
			foreach (KeyValuePair<XVar, dynamic> indices in xNames.GetEnumerator())
			{
				if(1 < MVCFunctions.count(indices.Value))
				{
					foreach (KeyValuePair<XVar, dynamic> ind in indices.Value.GetEnumerator())
					{
						groupFields.InitAndSetArrayItem(false, ind.Value, "uniqueName");
					}
				}
			}
			foreach (KeyValuePair<XVar, dynamic> indices in yNames.GetEnumerator())
			{
				if(1 < MVCFunctions.count(indices.Value))
				{
					foreach (KeyValuePair<XVar, dynamic> ind in indices.Value.GetEnumerator())
					{
						groupFields.InitAndSetArrayItem(false, ind.Value, "uniqueName");
					}
				}
			}
			return groupFields;
		}
		public virtual XVar setCrosstabData(dynamic _options)
		{
			dynamic var_params = XVar.Array();
			if((XVar)(this.pSetSearch.noRecordsOnFirstPage())  && (XVar)(!(XVar)(isSearchFunctionalityActivated())))
			{
				this.xt.assign(new XVar("container_grid"), new XVar(false));
				showNoRecordsMessage();
			}
			var_params = XVar.Clone(XVar.Array());
			var_params.InitAndSetArrayItem(this.x, "x");
			var_params.InitAndSetArrayItem(this.y, "y");
			var_params.InitAndSetArrayItem(this.dataField, "data");
			var_params.InitAndSetArrayItem(this.xType, "xType");
			var_params.InitAndSetArrayItem(this.yType, "yType");
			var_params.InitAndSetArrayItem(this.operation, "operation");
			var_params.InitAndSetArrayItem(getFieldClass((XVar)(this.x)), "headerClass");
			var_params.InitAndSetArrayItem(getFieldClass((XVar)(this.dataField)), "dataClass");
			var_params.InitAndSetArrayItem(this.tName, "tableName");
			var_params.InitAndSetArrayItem(this.pageType, "pageType");
			var_params.InitAndSetArrayItem(getCrossGroupFieldsSettings((XVar)(_options["repGroupFields"])), "groupFields");
			var_params.InitAndSetArrayItem(this.pSet.reportHasHorizontalSummary(), "xSummary");
			var_params.InitAndSetArrayItem(this.pSet.reportHasVerticalSummary(), "ySummary");
			var_params.InitAndSetArrayItem((XVar)(var_params["xSummary"])  || (XVar)(var_params["ySummary"]), "totalSummary");
			foreach (KeyValuePair<XVar, dynamic> value in _options["fieldsArr"].GetEnumerator())
			{
				var_params.InitAndSetArrayItem(value.Value["name"], "totals", value.Value["name"], "name");
				var_params.InitAndSetArrayItem(value.Value["label"], "totals", value.Value["name"], "label");
				var_params.InitAndSetArrayItem(value.Value["totalMax"], "totals", value.Value["name"], "max");
				var_params.InitAndSetArrayItem(value.Value["totalMin"], "totals", value.Value["name"], "min");
				var_params.InitAndSetArrayItem(value.Value["totalSum"], "totals", value.Value["name"], "sum");
				var_params.InitAndSetArrayItem(value.Value["totalAvg"], "totals", value.Value["name"], "avg");
			}
			this.crossTableObj = XVar.Clone(new CrossTableReport((XVar)(var_params), (XVar)(getBasicCrossTableSQL())));
			if(XVar.Pack(this.crosstableRefresh))
			{
				refreshCrossTable();
				return null;
			}
			if(XVar.Pack(this.crossTableObj.isEmpty()))
			{
				this.noRecordsFound = new XVar(true);
				this.jsSettings.InitAndSetArrayItem(getCurrentCrossParams(), "tableSettings", this.tName, "crossParams");
			}
			crossTableCommonAssign((XVar)(var_params["totalSummary"]));
			return null;
		}
		protected virtual XVar refreshCrossTable()
		{
			dynamic reportData = XVar.Array();
			reportData = XVar.Clone(XVar.Array());
			reportData.InitAndSetArrayItem(this.crossTableObj.getCrossTableData(), "rowsInfo");
			reportData.InitAndSetArrayItem(this.crossTableObj.getTotalsName(), "totalsName");
			reportData.InitAndSetArrayItem(this.crossTableObj.getCrossTableSummary(), "columnSummary");
			reportData.InitAndSetArrayItem(this.crossTableObj.getTotalSummary(), "totalSummary");
			reportData.InitAndSetArrayItem(getOperationCtrlMarkup(), "groupFuncCtrl");
			reportData.InitAndSetArrayItem(getFieldClass((XVar)(this.dataField)), "dataClass");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(reportData)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar getCrossFieldCtrlMarkup(dynamic _param_axis)
		{
			#region pass-by-value parameters
			dynamic axis = XVar.Clone(_param_axis);
			#endregion

			dynamic classAttr = null, options = XVar.Array();
			classAttr = new XVar("");
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				classAttr = XVar.Clone(MVCFunctions.Concat(" class=\"form-control bs-cross-dd", axis, "\" "));
			}
			options = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fData in this.crossTableObj.getCrossFieldsData((XVar)(axis)).GetEnumerator())
			{
				dynamic intervalDataAttr = null;
				intervalDataAttr = XVar.Clone((XVar.Pack(fData.Value["intervalType"]) ? XVar.Pack(MVCFunctions.Concat(" data-", axis, "type=\"", fData.Value["intervalType"], "\" ")) : XVar.Pack("")));
				options.InitAndSetArrayItem(MVCFunctions.Concat("<option ", intervalDataAttr, " value=\"", fData.Value["value"], "\" ", fData.Value["selected"], ">", fData.Value["label"], "</option>"), null);
			}
			return MVCFunctions.Concat("<select id=\"select_group_", axis, this.id, "\" ", classAttr, ">", MVCFunctions.implode(new XVar(""), (XVar)(options)), "</select>");
		}
		protected virtual XVar getDataFieldCtrlMarkup()
		{
			dynamic classAttr = null, options = XVar.Array();
			classAttr = new XVar("");
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				classAttr = new XVar(" class=\"form-control bs-cross-ddvalue\" ");
			}
			options = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fData in this.crossTableObj.getDataFieldsList().GetEnumerator())
			{
				options.InitAndSetArrayItem(MVCFunctions.Concat("<option value=\"", fData.Value["value"], "\" ", fData.Value["selected"], ">", MVCFunctions.runner_htmlspecialchars((XVar)(fData.Value["label"])), "</option>"), null);
			}
			return MVCFunctions.Concat("<select id=\"select_data", this.id, "\" ", classAttr, ">", MVCFunctions.implode(new XVar(""), (XVar)(options)), "</select>");
		}
		protected virtual XVar getOldOperationCtrlMarkup()
		{
			dynamic inputCtrls = XVar.Array();
			inputCtrls = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> opData in this.crossTableObj.getCurrentOperationList().GetEnumerator())
			{
				dynamic cheked = null;
				cheked = XVar.Clone((XVar.Pack(opData.Value["selected"] == "selected") ? XVar.Pack("checked") : XVar.Pack("")));
				inputCtrls.InitAndSetArrayItem(MVCFunctions.Concat("<input type=radio value='", opData.Value["value"], "' name=\"group_func", this.id, "\" ", cheked, "> ", opData.Value["label"]), null);
			}
			return MVCFunctions.implode(new XVar("&nbsp;&nbsp;"), (XVar)(inputCtrls));
		}
		protected virtual XVar getOperationCtrlMarkup()
		{
			dynamic options = XVar.Array();
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				return getOldOperationCtrlMarkup();
			}
			foreach (KeyValuePair<XVar, dynamic> opData in this.crossTableObj.getCurrentOperationList().GetEnumerator())
			{
				options.InitAndSetArrayItem(MVCFunctions.Concat("<option value=\"", opData.Value["value"], "\" ", opData.Value["selected"], ">", MVCFunctions.runner_htmlspecialchars((XVar)(opData.Value["label"])), "</option>"), null);
			}
			return MVCFunctions.Concat("<select id=\"group_func", this.id, "\" class=\"bs-cross-ddgroup form-control\">", MVCFunctions.implode(new XVar(""), (XVar)(options)), "</select>");
		}
		protected virtual XVar crossTableCommonAssign(dynamic _param_showSummary)
		{
			#region pass-by-value parameters
			dynamic showSummary = XVar.Clone(_param_showSummary);
			#endregion

			dynamic allow_export = null, grid_row = XVar.Array();
			if(XVar.Pack(!(XVar)(mobileTemplateMode())))
			{
				this.xt.assign(new XVar("cross_controls"), new XVar(true));
				this.xt.assign(new XVar("select_group_x"), (XVar)(getCrossFieldCtrlMarkup(new XVar("x"))));
				this.xt.assign(new XVar("select_group_y"), (XVar)(getCrossFieldCtrlMarkup(new XVar("y"))));
				this.xt.assign(new XVar("select_data"), (XVar)(getDataFieldCtrlMarkup()));
				this.xt.assign((XVar)((XVar.Pack(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("select_group") : XVar.Pack("group_func"))), (XVar)(getOperationCtrlMarkup()));
			}
			this.xt.assign(new XVar("totals"), (XVar)(this.crossTableObj.getTotalsName()));
			grid_row.InitAndSetArrayItem(this.crossTableObj.getCrossTableData(), "data");
			allow_export = XVar.Clone(this.permis[this.tName]["export"]);
			this.xt.assign(new XVar("export_link"), (XVar)((XVar)(allow_export)  && (XVar)(!(XVar)(this.noRecordsFound))));
			this.xt.assign(new XVar("prints_block"), (XVar)((XVar)(printAvailable())  && (XVar)(!(XVar)(this.noRecordsFound))));
			if(XVar.Pack(!(XVar)(isDashboardElement())))
			{
				this.xt.assign(new XVar("print_friendly"), (XVar)((XVar)(printAvailable())  && (XVar)(!(XVar)(this.noRecordsFound))));
			}
			if(XVar.Pack(!(XVar)(this.noRecordsFound)))
			{
				dynamic headerColspan = null, headers = null;
				this.xt.assign(new XVar("grid_row"), (XVar)(grid_row));
				headers = XVar.Clone(this.crossTableObj.getCrossTableHeader());
				this.xt.assignbyref(new XVar("group_header"), (XVar)(headers));
				this.xt.assignbyref(new XVar("col_summary"), (XVar)(this.crossTableObj.getCrossTableSummary()));
				this.xt.assignbyref(new XVar("total_summary"), (XVar)(this.crossTableObj.getTotalSummary()));
				this.xt.assignbyref(new XVar("summary_class"), (XVar)(getFieldClass((XVar)(this.dataField))));
				this.xt.assign(new XVar("cross_totals"), (XVar)(showSummary));
				headerColspan = XVar.Clone(MVCFunctions.count(grid_row["data"][0]["row_record"]["data"]));
				if(XVar.Pack(grid_row["data"][0]["row_record"]["cross_totals"]))
				{
					++(headerColspan);
				}
				if(1 < headerColspan)
				{
					this.xt.assign(new XVar("xselcell_attrs"), (XVar)(MVCFunctions.Concat("colspan=", headerColspan)));
				}
			}
			this.xt.assign(new XVar("crosstable_attrs"), (XVar)(MVCFunctions.Concat("&x=", this.x, "&y=", this.y, "&data=", this.dataField, "&op=", this.operation)));
			return null;
		}
		public virtual XVar setStandartData(dynamic _options)
		{
			dynamic pageSize = null, rb = null, sqlArray = null, whereComponents = XVar.Array();
			whereComponents = XVar.Clone(getWhereComponents());
			sqlArray = XVar.Clone(getReportSQLData());
			pageSize = XVar.Clone(this.pageSize);
			if(this.pageSize == -1)
			{
				pageSize = new XVar(0);
			}
			rb = XVar.Clone(new Report((XVar)(sqlArray), (XVar)(this.pSet.getTableData(new XVar(".orderindexes"))), (XVar)(this.connection), (XVar)(pageSize), new XVar(0), (XVar)(_options), (XVar)(whereComponents["searchWhere"]), (XVar)(whereComponents["searchHaving"]), this));
			this.arrReport = XVar.Clone(rb.getReport((XVar)(this.pagestart)));
			setDetailLinks();
			buildPagination();
			standardReportCommonAssign();
			return null;
		}
		public override XVar getMasterTableSQLClause(dynamic _param_basedOnProp = null)
		{
			#region default values
			if(_param_basedOnProp as Object == null) _param_basedOnProp = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic basedOnProp = XVar.Clone(_param_basedOnProp);
			#endregion

			if((XVar)(this.mode == Constants.REPORT_DASHBOARD)  && (XVar)(!(XVar)(this.dashElementData.KeyExists("masterTable"))))
			{
				return "";
			}
			return base.getMasterTableSQLClause();
		}
		protected virtual XVar standardReportCommonAssign()
		{
			dynamic allow_export = null, allow_search = null;
			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.tName)), "_dtable_column")), new XVar(true));
			this.xt.assign(new XVar("dtables_link"), new XVar(true));
			foreach (KeyValuePair<XVar, dynamic> value in this.arrReport["page"].GetEnumerator())
			{
				this.xt.assign((XVar)(value.Key), (XVar)(value.Value));
			}
			foreach (KeyValuePair<XVar, dynamic> value in this.arrReport["global"].GetEnumerator())
			{
				this.xt.assign((XVar)(value.Key), (XVar)(value.Value));
			}
			if(0 < MVCFunctions.count(this.arrReport["list"]))
			{
				this.xt.assign(new XVar("grid_row"), (XVar)(new XVar("data", this.arrReport["list"])));
			}
			else
			{
				this.noRecordsFound = new XVar(true);
			}
			allow_export = XVar.Clone(this.permis[this.tName]["export"]);
			allow_search = XVar.Clone(this.permis[this.tName]["search"]);
			this.xt.assign(new XVar("export_link"), (XVar)((XVar)(allow_export)  && (XVar)(0 < this.arrReport["countRows"])));
			this.xt.assign(new XVar("prints_block"), (XVar)((XVar)(allow_export)  && (XVar)(0 < this.arrReport["countRows"])));
			this.xt.assign(new XVar("printall_link"), (XVar)((XVar)((XVar)(allow_export)  && (XVar)(this.pageSize < this.arrReport["countRows"]))  && (XVar)(0 < this.pageSize)));
			this.xt.assign(new XVar("print_recspp"), (XVar)(this.pSet.getReportPrintGroupsPerPage()));
			if(XVar.Pack(!(XVar)(isDashboardElement())))
			{
				this.xt.assign(new XVar("print_friendly"), (XVar)((XVar)(printAvailable())  && (XVar)(0 < this.arrReport["countRows"])));
				this.xt.assign(new XVar("print_friendly_all"), (XVar)((XVar)(printAvailable())  && (XVar)(0 < this.arrReport["countRows"])));
			}
			if((XVar)((XVar)(this.mode == Constants.REPORT_SIMPLE)  && (XVar)(allow_search))  && (XVar)(MVCFunctions.count(this.arrGroupsPerPage)))
			{
				this.xt.assign(new XVar("recordspp_block"), new XVar(true));
				createPerPage();
			}
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				this.xt.assign(new XVar("details_found"), (XVar)(this.arrReport["countRows"] != 0));
			}
			this.xt.assign(new XVar("details_block"), (XVar)(this.arrReport["countRows"] != 0));
			this.xt.assign(new XVar("records_found"), (XVar)(this.arrReport["countRows"]));
			this.xt.assign(new XVar("pages_block"), (XVar)(this.arrReport["countRows"] != 0));
			this.xt.assign(new XVar("page"), (XVar)(this.myPage));
			this.xt.assign(new XVar("maxpages"), (XVar)(this.maxPages));
			this.xt.assign(new XVar("global_summary"), new XVar(true));
			return null;
		}
		public override XVar buildPagination()
		{
			dynamic lastrecord = null;
			this.maxPages = XVar.Clone(this.arrReport["maxpages"]);
			lastrecord = XVar.Clone(this.myPage * this.pageSize);
			if((XVar)(this.pageSize < 0)  || (XVar)(this.arrReport["countRows"] < lastrecord))
			{
				lastrecord = XVar.Clone(this.arrReport["countRows"]);
			}
			prepareRecordsIndicator((XVar)((this.myPage - 1) * this.pageSize + 1), (XVar)(lastrecord), (XVar)(this.arrReport["countRows"]));
			if(1 < this.maxPages)
			{
				dynamic counter = null, counterend = null, counterstart = null, limit = null, pageLinks = null, pagination = null, separator = null;
				this.xt.assign(new XVar("pagination_block"), new XVar(true));
				pagination = new XVar("");
				limit = new XVar(10);
				if(XVar.Pack(mobileTemplateMode()))
				{
					limit = new XVar(5);
				}
				counterstart = XVar.Clone(this.myPage - (limit - 1));
				if(this.myPage  %  limit != 0)
				{
					counterstart = XVar.Clone((this.myPage - this.myPage  %  limit) + 1);
				}
				counterend = XVar.Clone(counterstart + (limit - 1));
				if(this.maxPages < counterend)
				{
					counterend = XVar.Clone(this.maxPages);
				}
				if(counterstart != 1)
				{
					pagination = MVCFunctions.Concat(pagination, getPaginationLink(new XVar(1), new XVar("First")), "&nbsp;:&nbsp;");
					pagination = MVCFunctions.Concat(pagination, getPaginationLink((XVar)(counterstart - 1), new XVar("Previous")), "&nbsp;");
				}
				pageLinks = new XVar("");
				if(XVar.Pack(CommonFunctions.isRTL()))
				{
					counter = XVar.Clone(counterend);
					for(;counterstart <= counter; counter--)
					{
						pageLinks = MVCFunctions.Concat(pageLinks, separator, getPaginationLink((XVar)(counter), (XVar)(counter), (XVar)(counter == this.myPage)));
					}
				}
				else
				{
					counter = XVar.Clone(counterstart);
					for(;counter <= counterend; counter++)
					{
						pageLinks = MVCFunctions.Concat(pageLinks, separator, getPaginationLink((XVar)(counter), (XVar)(counter), (XVar)(counter == this.myPage)));
					}
				}
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					pageLinks = XVar.Clone(MVCFunctions.Concat("[", pageLinks, separator, "]"));
				}
				pagination = MVCFunctions.Concat(pagination, pageLinks);
				if(counterend != this.maxPages)
				{
					pagination = MVCFunctions.Concat(pagination, "&nbsp;", getPaginationLink((XVar)(counterend + 1), new XVar("Next")), "&nbsp;:&nbsp;");
					pagination = MVCFunctions.Concat(pagination, "&nbsp;", getPaginationLink((XVar)(this.maxPages), new XVar("Last")));
				}
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					pagination = XVar.Clone(MVCFunctions.Concat("<nav><ul class=\"pagination\" data-function=\"pagination", this.id, "\">", pagination, "</ul></nav>"));
				}
				else
				{
					pagination = XVar.Clone(MVCFunctions.Concat("<div data-function=\"pagination", this.id, "\">", pagination, "</div>"));
				}
				this.xt.assign(new XVar("pagination"), (XVar)(pagination));
			}
			else
			{
				if(XVar.Pack(!(XVar)(this.myPage)))
				{
					this.myPage = new XVar(1);
				}
			}
			return null;
		}
		protected virtual XVar setDetailLinks()
		{
			dynamic arrReportList = XVar.Array();
			if(this.mode == Constants.REPORT_DASHBOARD)
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> detailTableData in this.allDetailsTablesArr.GetEnumerator())
			{
				this.permis.InitAndSetArrayItem(getPermissions((XVar)(detailTableData.Value["dDataSourceTable"])), detailTableData.Value["dDataSourceTable"]);
				this.detailKeysByD.InitAndSetArrayItem(this.pSet.getDetailKeysByDetailTable((XVar)(detailTableData.Value["dDataSourceTable"])), null);
			}
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows");
			arrReportList = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> data in this.arrReport["list"].GetEnumerator())
			{
				dynamic gridRowInd = null, i = null, record = XVar.Array(), tKeys = XVar.Array();
				if(XVar.Pack(!(XVar)(data.Value.KeyExists("row_data"))))
				{
					continue;
				}
				record = XVar.Clone(XVar.Array());
				genId();
				gridRowInd = XVar.Clone(MVCFunctions.count(this.controlsMap["gridRows"]));
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd);
				this.controlsMap.InitAndSetArrayItem(this.recId, "gridRows", gridRowInd, "id");
				this.controlsMap.InitAndSetArrayItem(gridRowInd, "gridRows", gridRowInd, "rowInd");
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd, "keyFields");
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd, "keys");
				i = new XVar(0);
				for(;i < MVCFunctions.count(tKeys); i++)
				{
					this.controlsMap.InitAndSetArrayItem(tKeys[i], "gridRows", gridRowInd, "keyFields", i);
					this.controlsMap.InitAndSetArrayItem(data.Value[MVCFunctions.Concat(tKeys[i], "_value")], "gridRows", gridRowInd, "keys", i);
				}
				proccessDetailGridInfo((XVar)(record), (XVar)(data.Value), (XVar)(gridRowInd));
				record.InitAndSetArrayItem(MVCFunctions.Concat("data-record-id=\"", this.recId, "\""), "recordattrs");
				record.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"gridRow", this.recId, "\""), "rowattrs");
				arrReportList.InitAndSetArrayItem(MVCFunctions.array_merge_assoc((XVar)(data.Value), (XVar)(record)), data.Key);
				this.recIds.InitAndSetArrayItem(this.recId, null);
			}
			foreach (KeyValuePair<XVar, dynamic> data in arrReportList.GetEnumerator())
			{
				this.arrReport.InitAndSetArrayItem(data.Value, "list", data.Key);
			}
			return null;
		}
		protected virtual XVar getBasicCrossTableSQL()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(getSubsetSQLComponents());
			return SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"]));
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Search")), "mandatoryWhere", null);
			if((XVar)(this.pSet.noRecordsOnFirstPage())  && (XVar)(!(XVar)(this.searchClauseObj.isSearchFunctionalityActivated())))
			{
				sql.InitAndSetArrayItem(" 1=0 ", "mandatoryWhere", null);
			}
			return sql;
		}
		public virtual XVar getReportSQLData()
		{
			dynamic allHaving = null, allWhere = null, optHaving = null, optWhere = null, sql = XVar.Array();
			sql = XVar.Clone(getSubsetSQLComponents());
			sql.InitAndSetArrayItem(DB.PrepareSQL((XVar)(sql["sqlParts"]["head"])), "sqlParts", "head");
			sql.InitAndSetArrayItem(DB.PrepareSQL((XVar)(sql["sqlParts"]["from"])), "sqlParts", "from");
			sql.InitAndSetArrayItem(DB.PrepareSQL((XVar)(sql["sqlParts"]["where"])), "sqlParts", "where");
			sql.InitAndSetArrayItem(DB.PrepareSQL((XVar)(sql["sqlParts"]["groupby"])), "sqlParts", "groupby");
			sql.InitAndSetArrayItem(DB.PrepareSQL((XVar)(sql["sqlParts"]["having"])), "sqlParts", "having");
			optWhere = XVar.Clone(SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")));
			optHaving = XVar.Clone(SQLQuery.combineCases((XVar)(sql["optionalHaving"]), new XVar("or")));
			allWhere = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, sql["sqlParts"]["where"], 2, optWhere)), new XVar("and")));
			allHaving = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryHaving"]), new XVar("and")), 1, sql["sqlParts"]["having"], 2, optHaving)), new XVar("and")));
			return new XVar(0, sql["sqlParts"]["head"], 1, sql["sqlParts"]["from"], 2, allWhere, 3, sql["sqlParts"]["groupby"], 4, allHaving);
		}
		public virtual XVar prepareDetailsForEditViewPage()
		{
			addButtonHandlers();
			commonAssign();
			if(XVar.Pack(this.allDetailsTablesArr))
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			setReportData((XVar)(getExtraReportParams()));
			this.xt.assign(new XVar("cross_controls"), new XVar(false));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.assign(new XVar("recordspp_block"), new XVar(true));
			doCommonAssignments();
			createPerPage();
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.body.InitAndSetArrayItem("", "begin");
			this.body.InitAndSetArrayItem("", "end");
			this.xt.assign(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public virtual XVar getExtraReportParams()
		{
			dynamic extraParams = XVar.Array(), paramfieldArr = XVar.Array(), reportFields = XVar.Array();
			extraParams = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(this.crossTable)))
			{
				extraParams.InitAndSetArrayItem(this.tName, "tName");
				extraParams.InitAndSetArrayItem(this.shortTableName, "shortTName");
				extraParams.InitAndSetArrayItem(this.sessionPrefix, "sessionPrefix");
				extraParams.InitAndSetArrayItem(this.shortTableName, "shortTableName");
				extraParams.InitAndSetArrayItem(this.pSet.getTableKeys(), "tKeyFields");
				extraParams.InitAndSetArrayItem(this.pSet.reportHasPageSummary(), "repPageSummary");
				extraParams.InitAndSetArrayItem(this.pSet.reportHasGlobalSummary(), "repGlobalSummary");
				extraParams.InitAndSetArrayItem(this.pSet.getReportLayout(), "repLayout");
				extraParams.InitAndSetArrayItem(this.pSet.isGroupSummaryCountShown(), "showGroupSummaryCount");
				extraParams.InitAndSetArrayItem(this.pSet.reportDetailsShown(), "repShowDet");
				extraParams.InitAndSetArrayItem(this.pSet.reportTotalFieldsExist(), "isExistTotalFields");
			}
			extraParams.InitAndSetArrayItem(this.pSet.getReportGroupFieldsData(), "repGroupFields");
			extraParams.InitAndSetArrayItem(MVCFunctions.count(extraParams["repGroupFields"]), "repGroupFieldsCount");
			paramfieldArr = XVar.Clone(XVar.Array());
			reportFields = XVar.Clone(this.pSet.getFieldsList());
			foreach (KeyValuePair<XVar, dynamic> field in reportFields.GetEnumerator())
			{
				dynamic fieldArr = XVar.Array();
				fieldArr = XVar.Clone(XVar.Array());
				fieldArr.InitAndSetArrayItem(field.Value, "name");
				fieldArr.InitAndSetArrayItem(field.Value, "fName");
				fieldArr.InitAndSetArrayItem(this.pSet.label((XVar)(field.Value)), "label");
				fieldArr.InitAndSetArrayItem(MVCFunctions.GoodFieldName((XVar)(field.Value)), "goodName");
				fieldArr.InitAndSetArrayItem(true, "repPage");
				fieldArr.InitAndSetArrayItem(this.pSet.getViewFormat((XVar)(field.Value)), "viewFormat");
				fieldArr.InitAndSetArrayItem(this.pSet.getEditFormat((XVar)(field.Value)), "editFormat");
				fieldArr.InitAndSetArrayItem(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalMax")), "totalMax");
				fieldArr.InitAndSetArrayItem(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalMin")), "totalMin");
				fieldArr.InitAndSetArrayItem(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalAvg")), "totalAvg");
				fieldArr.InitAndSetArrayItem(this.pSet.getFieldData((XVar)(field.Value), new XVar("isTotalSum")), "totalSum");
				paramfieldArr.InitAndSetArrayItem(fieldArr, null);
			}
			extraParams.InitAndSetArrayItem(paramfieldArr, "fieldsArr");
			return extraParams;
		}
		public override XVar createPerPage()
		{
			dynamic allMessage = null, classString = null, i = null, reportGroupFields = null, rpp = null;
			classString = new XVar("");
			allMessage = new XVar("Show all");
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				classString = new XVar("class=\"form-control\"");
				allMessage = new XVar("All");
			}
			rpp = XVar.Clone(MVCFunctions.Concat("<select ", classString, " id=\"recordspp", this.id, "\">"));
			reportGroupFields = XVar.Clone(this.pSet.isReportWithGroups());
			if(XVar.Pack(reportGroupFields))
			{
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.arrGroupsPerPage); i++)
				{
					if(this.arrGroupsPerPage[i] != -1)
					{
						rpp = MVCFunctions.Concat(rpp, "<option value=\"", this.arrGroupsPerPage[i], "\" ", (XVar.Pack(this.pageSize == this.arrGroupsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", this.arrGroupsPerPage[i], "</option>");
					}
					else
					{
						rpp = MVCFunctions.Concat(rpp, "<option value=\"-1\" ", (XVar.Pack(this.pageSize == this.arrGroupsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", allMessage, "</option>");
					}
				}
			}
			else
			{
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.arrRecsPerPage); i++)
				{
					if(this.arrRecsPerPage[i] != -1)
					{
						rpp = MVCFunctions.Concat(rpp, "<option value=\"", this.arrRecsPerPage[i], "\" ", (XVar.Pack(this.pageSize == this.arrRecsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", this.arrRecsPerPage[i], "</option>");
					}
					else
					{
						rpp = MVCFunctions.Concat(rpp, "<option value=\"-1\" ", (XVar.Pack(this.pageSize == this.arrRecsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", allMessage, "</option>");
					}
				}
			}
			rpp = MVCFunctions.Concat(rpp, "</select>");
			if(XVar.Pack(reportGroupFields))
			{
				this.xt.assign(new XVar("grpsPerPage"), (XVar)(rpp));
			}
			else
			{
				this.xt.assign(new XVar("recsPerPage"), (XVar)(rpp));
			}
			return null;
		}
		public virtual XVar doCommonAssignments()
		{
			dynamic allow_export = null, allow_search = null;
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.xt.assign(new XVar("left_block"), new XVar(true));
			assignBody();
			this.xt.assign(new XVar("searchPanel"), new XVar(true));
			if(XVar.Pack(isShowMenu()))
			{
				this.xt.assign(new XVar("menu_block"), new XVar(true));
			}
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.xt.assign(new XVar("tableinfomobile_block"), new XVar(true));
			}
			prepareBreadcrumbs(new XVar("main"));
			allow_search = XVar.Clone(this.permis[this.tName]["search"]);
			allow_export = XVar.Clone(this.permis[this.tName]["export"]);
			this.xt.assign(new XVar("grid_block"), (XVar)(allow_search));
			this.xt.assign(new XVar("toplinks_block"), (XVar)(allow_search));
			this.xt.assign(new XVar("asearch_link"), (XVar)(allow_search));
			this.xt.assign(new XVar("print_link"), (XVar)(allow_export));
			this.xt.assign(new XVar("printlink_attrs"), (XVar)(MVCFunctions.Concat("id=print_", this.id, " href='#'")));
			this.xt.assign(new XVar("printalllink_attrs"), (XVar)(MVCFunctions.Concat("id=printAll_", this.id, " href='#'")));
			this.xt.assign(new XVar("excellink_attrs"), (XVar)(MVCFunctions.Concat("id=export_to_excel", this.id, " href='#'")));
			this.xt.assign(new XVar("wordlink_attrs"), (XVar)(MVCFunctions.Concat("id=export_to_word", this.id, " href='#'")));
			this.xt.assign(new XVar("pdflink_attrs"), (XVar)(MVCFunctions.Concat("id=export_to_pdf", this.id, " href='#'")));
			this.xt.assign(new XVar("advsearchlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"advButton", this.id, "\"")));
			if(XVar.Pack(this.noRecordsFound))
			{
				this.xt.assign(new XVar("container_grid"), new XVar(false));
				this.xt.assign(new XVar("grid_block"), new XVar(false));
				showNoRecordsMessage();
			}
			return null;
		}
		public override XVar addCommonJs()
		{
			base.addCommonJs();
			if(XVar.Pack(this.allDetailsTablesArr))
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			return null;
		}
		protected override XVar assignBody()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage((XVar)(this.isDisplayLoading)));
			if((XVar)(this.mode == Constants.REPORT_SIMPLE)  && (XVar)(!(XVar)(mobileTemplateMode())))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div id=\"search_suggest\" class=\"search_suggest\"></div>");
			}
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public virtual XVar beforeShowReport()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowReport"))))
			{
				this.eventsObject.BeforeShowReport((XVar)(this.xt), ref this.templatefile, this);
			}
			return null;
		}
		public virtual XVar showPage()
		{
			dynamic bricksExcept = null;
			beforeShowReport();
			if(this.mode == Constants.REPORT_SIMPLE)
			{
				display((XVar)(this.templatefile));
				return null;
			}
			this.body.InitAndSetArrayItem("", "begin");
			this.body.InitAndSetArrayItem("", "end");
			this.xt.assign(new XVar("body"), (XVar)(this.body));
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			if(this.mode == Constants.REPORT_DASHBOARD)
			{
				dynamic returnJSON = XVar.Array();
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination", 2, "details_found", 3, "message"));
				this.xt.hideAllBricksExcept((XVar)(bricksExcept));
				this.xt.prepare_template((XVar)(this.templatefile));
				addControlsJSAndCSS();
				fillSetCntrlMaps();
				returnJSON = XVar.Clone(XVar.Array());
				returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
				returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
				returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
				returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
				if(XVar.Pack(this.formBricks["footer"]))
				{
					returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["footer"]), new XVar(true)), "footerCont");
				}
				if(XVar.Pack(this.formBricks["header"]))
				{
					returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["header"]), new XVar(true)), "headerCont");
				}
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat("<span class=\"rnr-dbebrick\">", getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</span>", returnJSON["headerCont"]), "headerCont");
				assignFormFooterAndHeaderBricks(new XVar(false));
				this.xt.prepareContainers();
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("message_block")), this.xt.fetch_loaded(new XVar("grid_block"))), "html");
				}
				else
				{
					returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
				}
				returnJSON.InitAndSetArrayItem(this.flyId, "idStartFrom");
				returnJSON.InitAndSetArrayItem(true, "success");
				returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
				returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "CSSFiles");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
				return null;
			}
			if((XVar)(this.mode == Constants.REPORT_DETAILS)  || (XVar)(this.mode == Constants.REPORT_DASHDETAILS))
			{
				showDpAjax();
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
				return null;
			}
			if((XVar)(this.mode)  && (XVar)(this.mode == "listdetailspopup"))
			{
				dynamic respArr = XVar.Array();
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination"));
				this.xt.assign(new XVar("container_master"), new XVar(false));
				this.xt.assign(new XVar("cross_controls"), new XVar(false));
				this.xt.hideAllBricksExcept((XVar)(bricksExcept));
				this.xt.prepare_template((XVar)(this.templatefile));
				respArr = XVar.Clone(XVar.Array());
				respArr.InitAndSetArrayItem(true, "success");
				respArr.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "body");
				respArr.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("counter")), "counter");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(respArr)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
				return null;
			}
			return null;
		}
		protected virtual XVar showDpAjax()
		{
			dynamic bricksExcept = XVar.Array(), returnJSON = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			if((XVar)((XVar)((XVar)((XVar)(this.mode == Constants.REPORT_DETAILS)  && (XVar)(this.dashTName))  && (XVar)(this.dashElementName))  && (XVar)(!(XVar)(shouldDisplayDetailsPage())))  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_EDIT)  || (XVar)(this.masterPageType == Constants.PAGE_VIEW)))
			{
				returnJSON.InitAndSetArrayItem(true, "noData");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				return null;
			}
			if((XVar)((XVar)(this.mode == Constants.REPORT_DETAILS)  && (XVar)(this.masterPageType == Constants.PAGE_LIST))  && (XVar)(!(XVar)(shouldDisplayDetailsPage())))
			{
				returnJSON.InitAndSetArrayItem(false, "success");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				return null;
			}
			this.xt.unassign(new XVar("cross_controls"));
			this.xt.displayBrickHidden(new XVar("printpanel"));
			bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination", 2, "message"));
			if(this.mode != Constants.REPORT_DETAILS)
			{
				bricksExcept.InitAndSetArrayItem("details_found", null);
			}
			bricksExcept.InitAndSetArrayItem("bsgrid_tabs", null);
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			if(XVar.Pack(this.formBricks["footer"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["footer"]), new XVar(true)), "footerCont");
			}
			if(XVar.Pack(this.formBricks["header"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["header"]), new XVar(true)), "headerCont");
			}
			if((XVar)(this.mode == Constants.REPORT_DETAILS)  && (XVar)((XVar)((XVar)(this.masterPageType == Constants.PAGE_LIST)  || (XVar)(this.masterPageType == Constants.PAGE_REPORT))  || (XVar)(this.masterPageType == Constants.PAGE_CHART)))
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(getProceedLink(), returnJSON["headerCont"]), "headerCont");
			}
			else
			{
				if(this.mode == Constants.REPORT_DASHDETAILS)
				{
					returnJSON.InitAndSetArrayItem(MVCFunctions.Concat("<span class=\"rnr-dbebrick\">", getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</span>", returnJSON["headerCont"]), "headerCont");
				}
			}
			assignFormFooterAndHeaderBricks(new XVar(false));
			this.xt.prepareContainers();
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("grid_tabs")), this.xt.fetch_loaded(new XVar("message_block")), this.xt.fetch_loaded(new XVar("grid_block")), this.xt.fetch_loaded(new XVar("pagination_block"))), "html");
			}
			else
			{
				returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			}
			returnJSON.InitAndSetArrayItem(true, "success");
			returnJSON.InitAndSetArrayItem(this.id, "id");
			returnJSON.InitAndSetArrayItem(this.flyId, "idStartFrom");
			returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "additionalCSS");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			return null;
		}
		public override XVar printAvailable()
		{
			if((XVar)((XVar)(this.mode == Constants.REPORT_DASHBOARD)  || (XVar)(this.mode == Constants.REPORT_DETAILS))  || (XVar)(this.mode == Constants.REPORT_DASHDETAILS))
			{
				return false;
			}
			return base.printAvailable();
		}
		public override XVar showPageDp(dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			if(XVar.Pack(this.crossTable))
			{
				this.xt.unassign(new XVar("cross_controls"));
			}
			base.showPageDp((XVar)(var_params));
			return null;
		}
		public override XVar shouldDisplayDetailsPage()
		{
			if(XVar.Pack(!(XVar)(this.permis[this.tName]["search"])))
			{
				return false;
			}
			if((XVar)(this.noRecordsFound)  && (XVar)(0 == getGridTabsCount()))
			{
				return false;
			}
			return true;
		}
		protected virtual XVar getFieldClass(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic var_class = null;
			var_class = XVar.Clone(base.fieldClass((XVar)(field)));
			if(XVar.Pack(MVCFunctions.in_array((XVar)(this.pSet.getViewFormat((XVar)(field))), (XVar)(new XVar(0, Constants.FORMAT_DATE_SHORT, 1, Constants.FORMAT_DATE_LONG, 2, Constants.FORMAT_DATE_TIME)))))
			{
				return MVCFunctions.Concat(var_class, " rnr-field-crossdate");
			}
			return var_class;
		}
		public static XVar getMasterKeysFromRequest()
		{
			dynamic _masterKey = null, i = null, masterKeys = XVar.Array(), masterKeysReq = XVar.Array();
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("masterKeys")))
			{
				masterKeys = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue("masterKeys"))));
			}
			i = new XVar(0);
			masterKeysReq = XVar.Clone(XVar.Array());
			while(XVar.Pack(true))
			{
				i++;
				if(XVar.Pack(MVCFunctions.REQUESTKeyExists(MVCFunctions.Concat("masterkey", i))))
				{
					_masterKey = XVar.Clone(MVCFunctions.postvalue(MVCFunctions.Concat("masterkey", i)));
				}
				else
				{
					if(XVar.Pack(masterKeys.KeyExists(MVCFunctions.Concat("masterkey", i))))
					{
						_masterKey = XVar.Clone(masterKeys[MVCFunctions.Concat("masterkey", i)]);
					}
					else
					{
						break;
					}
				}
				masterKeysReq.InitAndSetArrayItem(_masterKey, i);
			}
			return masterKeysReq;
		}
		public static XVar readReportModeFromRequest()
		{
			dynamic mode = null, pageMode = null;
			mode = XVar.Clone(MVCFunctions.postvalue(new XVar("mode")));
			if(mode == "listdetails")
			{
				pageMode = new XVar(Constants.REPORT_DETAILS);
			}
			else
			{
				if(mode == "listdetailspopup")
				{
					pageMode = new XVar(Constants.REPORT_POPUPDETAILS);
				}
				else
				{
					if(mode == "dashreport")
					{
						pageMode = new XVar(Constants.REPORT_DASHBOARD);
					}
					else
					{
						if(mode == "dashdetails")
						{
							pageMode = new XVar(Constants.REPORT_DASHDETAILS);
						}
						else
						{
							pageMode = new XVar(Constants.REPORT_SIMPLE);
						}
					}
				}
			}
			return pageMode;
		}
		public override XVar gridTabsAvailable()
		{
			return true;
		}
		public override XVar displayTabsInPage()
		{
			return (XVar)(simpleMode())  || (XVar)((XVar)(this.mode == Constants.REPORT_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_VIEW)  || (XVar)(this.masterPageType == Constants.PAGE_EDIT)));
		}
		public override XVar renderPageBody()
		{
			dynamic blocks = null;
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				return base.renderPageBody();
			}
			blocks = XVar.Clone(new XVar(0, "grid_tabs", 1, "message", 2, "grid_block", 3, "pagination_block"));
			return fetchBlocksList((XVar)(blocks), new XVar(false));
		}
	}
}
