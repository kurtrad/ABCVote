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
	public partial class FilterControl : XClass
	{
		protected dynamic id;
		protected dynamic fName;
		protected dynamic gfName;
		protected dynamic tName;
		protected ProjectSettings pSet;
		protected dynamic totals;
		protected dynamic useTotals;
		protected dynamic multiSelect;
		protected dynamic cipherer;
		protected dynamic filteredFields;
		protected dynamic filtered = XVar.Pack(false);
		protected dynamic totalsfName;
		protected dynamic strSQL;
		protected dynamic viewControl;
		protected dynamic aggregate;
		protected dynamic totalsOptions = XVar.Array();
		protected dynamic visible = XVar.Pack(true);
		protected dynamic filterFormat;
		protected dynamic useApllyBtn = XVar.Pack(false);
		protected dynamic separator = XVar.Pack("~~");
		protected dynamic totalViewControl;
		protected dynamic showCollapsed = XVar.Pack(false);
		protected dynamic whereComponents;
		protected dynamic fieldType;
		protected dynamic valuesObtainedFromDB = XVar.Array();
		protected dynamic onDemandHiddenItemClassName = XVar.Pack("filter-hidden");
		protected dynamic connection;
		public dynamic dependent = XVar.Pack(false);
		public dynamic parentFilterName = XVar.Pack("");
		public FilterControl(dynamic _param_fName, dynamic _param_pageObj, dynamic _param_id, dynamic _param_viewControls)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObj = XVar.Clone(_param_pageObj);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.id = XVar.Clone(id);
			this.fName = XVar.Clone(fName);
			this.gfName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(this.fName)));
			this.tName = XVar.Clone(pageObj.tName);
			this.connection = XVar.Clone(pageObj.connection);
			this.pSet = XVar.UnPackProjectSettings(pageObj.pSet);
			this.cipherer = XVar.Clone(pageObj.cipherer);
			this.totalsOptions = XVar.Clone(new XVar(Constants.FT_COUNT, "COUNT", Constants.FT_MIN, "MIN", Constants.FT_MAX, "MAX"));
			this.totals = XVar.Clone(this.pSet.getFilterFieldTotal((XVar)(fName)));
			this.totalsfName = XVar.Clone(this.pSet.getFilterTotalsField((XVar)(fName)));
			if((XVar)(!(XVar)(this.totalsfName))  || (XVar)(this.totals == Constants.FT_COUNT))
			{
				this.totalsfName = XVar.Clone(this.fName);
			}
			this.useTotals = XVar.Clone(this.totals != Constants.FT_NONE);
			this.multiSelect = XVar.Clone(this.pSet.getFilterFiledMultiSelect((XVar)(fName)));
			this.whereComponents = XVar.Clone(pageObj.getWhereComponents());
			this.filteredFields = XVar.Clone(pageObj.searchClauseObj.filteredFields);
			this.fieldType = XVar.Clone(this.pSet.getFieldType((XVar)(this.fName)));
			if(XVar.Pack(MVCFunctions.count(this.filteredFields[this.fName])))
			{
				this.filtered = new XVar(true);
			}
			assignViewControls((XVar)(viewControls));
			this.showCollapsed = XVar.Clone(this.pSet.showCollapsed((XVar)(this.fName)));
		}
		protected virtual XVar assignViewControls(dynamic _param_viewControls)
		{
			#region pass-by-value parameters
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.viewControl = XVar.Clone(viewControls.getControl((XVar)(this.fName)));
			this.viewControl.searchHighlight = new XVar(false);
			this.viewControl.isUsedForFilter = new XVar(true);
			if((XVar)(this.totals == Constants.FT_MIN)  || (XVar)(this.totals == Constants.FT_MAX))
			{
				this.totalViewControl = XVar.Clone(viewControls.getControl((XVar)(this.totalsfName)));
				this.totalViewControl.searchHighlight = new XVar(false);
				this.totalViewControl.isUsedForFilter = new XVar(true);
			}
			return null;
		}
		protected virtual XVar getDbFieldName(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			return RunnerPage._getFieldSQLDecrypt((XVar)(fName), (XVar)(this.connection), (XVar)(this.pSet), (XVar)(this.cipherer));
		}
		protected virtual XVar setAggregateType()
		{
			dynamic totalsOption = null;
			totalsOption = XVar.Clone((XVar.Pack(this.useTotals) ? XVar.Pack(this.totals) : XVar.Pack(Constants.FT_COUNT)));
			this.aggregate = XVar.Clone(this.totalsOptions[totalsOption]);
			return null;
		}
		protected virtual XVar buildSQL()
		{
			this.strSQL = XVar.Clone(MVCFunctions.Concat("SELECT ", getTotals(), " from ( ", buildBasicSQL(), ") a"));
			return null;
		}
		protected virtual XVar getTotals()
		{
			return "";
		}
		protected virtual XVar getCaseStatement(dynamic _param_condition, dynamic _param_trueValue, dynamic _param_falseValue)
		{
			#region pass-by-value parameters
			dynamic condition = XVar.Clone(_param_condition);
			dynamic trueValue = XVar.Clone(_param_trueValue);
			dynamic falseValue = XVar.Clone(_param_falseValue);
			#endregion

			if(this.connection.dbType == Constants.nDATABASE_Access)
			{
				return MVCFunctions.Concat("IIF(", condition, ", ", trueValue, ", ", falseValue, ")");
			}
			return MVCFunctions.Concat("case when ", condition, " then ", trueValue, " else ", falseValue, " end");
		}
		protected virtual XVar getCombinedFilterWhere(dynamic _param_useAllFilters = null)
		{
			#region default values
			if(_param_useAllFilters as Object == null) _param_useAllFilters = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic useAllFilters = XVar.Clone(_param_useAllFilters);
			#endregion

			dynamic whereClause = null;
			whereClause = XVar.Clone(this.whereComponents["commonWhere"]);
			foreach (KeyValuePair<XVar, dynamic> fWhere in this.whereComponents["filterWhere"].GetEnumerator())
			{
				if((XVar)(useAllFilters)  || (XVar)(fWhere.Key != this.fName))
				{
					whereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(whereClause), (XVar)(fWhere.Value)));
				}
			}
			return whereClause;
		}
		protected virtual XVar getCombinedFilterHaving()
		{
			dynamic havingClause = null;
			havingClause = XVar.Clone(this.whereComponents["commonHaving"]);
			foreach (KeyValuePair<XVar, dynamic> fHaving in this.whereComponents["filterHaving"].GetEnumerator())
			{
				if(fHaving.Key != this.fName)
				{
					havingClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(havingClause), (XVar)(fHaving.Value)));
				}
			}
			return havingClause;
		}
		public virtual XVar addFilterControlToControlsMap(dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			dynamic ctrlsMap = null;
			ctrlsMap = XVar.Clone(getBaseContolsMapParams());
			pageObj.controlsMap.InitAndSetArrayItem(ctrlsMap, "filters", "controls", null);
			return null;
		}
		protected virtual XVar getBaseContolsMapParams()
		{
			dynamic ctrlsMap = XVar.Array();
			ctrlsMap = XVar.Clone(XVar.Array());
			ctrlsMap.InitAndSetArrayItem(this.fName, "fieldName");
			ctrlsMap.InitAndSetArrayItem(this.gfName, "gfieldName");
			ctrlsMap.InitAndSetArrayItem(this.filterFormat, "filterFormat");
			ctrlsMap.InitAndSetArrayItem(this.multiSelect, "multiSelect");
			ctrlsMap.InitAndSetArrayItem(this.filtered, "filtered");
			ctrlsMap.InitAndSetArrayItem(this.separator, "separator");
			ctrlsMap.InitAndSetArrayItem(this.showCollapsed, "collapsed");
			if(XVar.Pack(this.filtered))
			{
				ctrlsMap.InitAndSetArrayItem(this.filteredFields[this.fName]["values"], "defaultValuesArray");
				ctrlsMap.InitAndSetArrayItem(XVar.Array(), "defaultShowValues");
				foreach (KeyValuePair<XVar, dynamic> dv in ctrlsMap["defaultValuesArray"].GetEnumerator())
				{
					ctrlsMap.InitAndSetArrayItem(getValueToShow((XVar)(dv.Value)), "defaultShowValues", null);
				}
			}
			return ctrlsMap;
		}
		protected virtual XVar getValueToShow(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return null;
		}
		protected virtual XVar getTotalValueToShow(dynamic _param_totalValue)
		{
			#region pass-by-value parameters
			dynamic totalValue = XVar.Clone(_param_totalValue);
			#endregion

			if((XVar)(this.totals == Constants.FT_MIN)  || (XVar)(this.totals == Constants.FT_MAX))
			{
				dynamic totalData = null;
				totalData = XVar.Clone(new XVar(this.totalsfName, totalValue));
				totalValue = XVar.Clone(this.totalViewControl.showDBValue((XVar)(totalData), new XVar("")));
			}
			return totalValue;
		}
		public virtual XVar buildFilterCtrlBlockArray(dynamic _param_pageObj, dynamic _param_dFilterBlocks = null)
		{
			#region default values
			if(_param_dFilterBlocks as Object == null) _param_dFilterBlocks = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			dynamic dFilterBlocks = XVar.Clone(_param_dFilterBlocks);
			#endregion

			dynamic filterCtrlBlocks = null;
			addFilterControlToControlsMap((XVar)(pageObj));
			filterCtrlBlocks = XVar.Clone(XVar.Array());
			if((XVar)(this.multiSelect != Constants.FM_ALWAYS)  && (XVar)(this.filtered))
			{
				filterCtrlBlocks = XVar.Clone(getFilteredFilterBlocks());
				if(this.multiSelect == Constants.FM_NONE)
				{
					return filterCtrlBlocks;
				}
			}
			addFilterBlocksFromDB((XVar)(filterCtrlBlocks));
			if((XVar)(this.multiSelect != Constants.FM_NONE)  && (XVar)(this.filtered))
			{
				addOutRangeValuesToFilter((XVar)(filterCtrlBlocks));
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(filterCtrlBlocks))))
			{
				this.visible = new XVar(false);
			}
			extraBlocksProcessing(ref filterCtrlBlocks);
			return filterCtrlBlocks;
		}
		protected virtual XVar extraBlocksProcessing(ref dynamic filterCtrlBlocks)
		{
			sortFilterBlocks(ref filterCtrlBlocks);
			return null;
		}
		protected virtual XVar sortFilterBlocks(ref dynamic filterCtrlBlocks)
		{
			return null;
		}
		protected virtual XVar isTruncated()
		{
			return false;
		}
		public static XVar compareBlocksByNumericValues(dynamic _param_block1, dynamic _param_block2)
		{
			#region pass-by-value parameters
			dynamic block1 = XVar.Clone(_param_block1);
			dynamic block2 = XVar.Clone(_param_block2);
			#endregion

			if(block1["sortValue"] < block2["sortValue"])
			{
				return -1;
			}
			if(block2["sortValue"] < block1["sortValue"])
			{
				return 1;
			}
			return 0;
		}
		public static XVar compareBlocksByStringValues(dynamic _param_block1, dynamic _param_block2)
		{
			#region pass-by-value parameters
			dynamic block1 = XVar.Clone(_param_block1);
			dynamic block2 = XVar.Clone(_param_block2);
			#endregion

			dynamic caseCompareResult = null, sortValue1 = null, sortValue2 = null;
			sortValue1 = XVar.Clone(block1["sortValue"]);
			sortValue2 = XVar.Clone(block2["sortValue"]);
			caseCompareResult = XVar.Clone(MVCFunctions.strcasecmp((XVar)(sortValue1), (XVar)(sortValue2)));
			if(caseCompareResult == XVar.Pack(0))
			{
				return -MVCFunctions.strcmp((XVar)(sortValue1), (XVar)(sortValue2));
			}
			return caseCompareResult;
		}
		protected virtual XVar addOutRangeValuesToFilter(dynamic filterCtrlBlocks)
		{
			dynamic visibilityClass = null;
			visibilityClass = XVar.Clone((XVar.Pack(this.multiSelect == Constants.FM_ON_DEMAND) ? XVar.Pack(this.onDemandHiddenItemClassName) : XVar.Pack("")));
			foreach (KeyValuePair<XVar, dynamic> value in this.filteredFields[this.fName]["values"].GetEnumerator())
			{
				dynamic filterControl = null;
				if(XVar.Pack(MVCFunctions.in_array((XVar)(value.Value), (XVar)(this.valuesObtainedFromDB))))
				{
					continue;
				}
				filterControl = XVar.Clone(this.Invoke("buildControl", (XVar)(new XVar(this.fName, value.Value))));
				filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl), (XVar)(visibilityClass), (XVar)(value.Value)), null);
			}
			return null;
		}
		protected virtual XVar getFilterBlockStructure(dynamic _param_filterControl, dynamic _param_visibilityClass = null, dynamic _param_value = null, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_visibilityClass as Object == null) _param_visibilityClass = new XVar("");
			if(_param_value as Object == null) _param_value = new XVar("");
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic filterControl = XVar.Clone(_param_filterControl);
			dynamic visibilityClass = XVar.Clone(_param_visibilityClass);
			dynamic value = XVar.Clone(_param_value);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			return new XVar(MVCFunctions.Concat(this.gfName, "_filter"), filterControl, MVCFunctions.Concat("visibilityClass_", this.gfName), visibilityClass);
		}
		protected virtual XVar getFilteredFilterBlocks()
		{
			dynamic filterControl = null, filterCtrlBlocks = XVar.Array();
			filterControl = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in this.filteredFields[this.fName]["values"].GetEnumerator())
			{
				dynamic classes = null, delButtonHtml = null, parentFiltersData = null, showValue = null;
				showValue = XVar.Clone(getValueToShow((XVar)(value.Value)));
				delButtonHtml = XVar.Clone(getDelButtonHtml((XVar)(this.gfName), (XVar)(this.id), (XVar)(value.Value)));
				filterControl = XVar.Clone(MVCFunctions.Concat(delButtonHtml, "<span dir=\"LTR\">", showValue, "</span>"));
				parentFiltersData = XVar.Clone(getParentFiltersDataForFilteredBlock((XVar)(value.Value)));
				classes = XVar.Clone(MVCFunctions.Concat("filter-ready-value", (XVar.Pack(this.multiSelect == Constants.FM_ON_DEMAND) ? XVar.Pack(" ondemand") : XVar.Pack(""))));
				filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl), (XVar)(classes), (XVar)(value.Value), (XVar)(parentFiltersData)), null);
			}
			return filterCtrlBlocks;
		}
		protected virtual XVar getParentFiltersDataForFilteredBlock(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return XVar.Array();
		}
		protected virtual XVar addFilterBlocksFromDB(dynamic filterBlocks)
		{
			return null;
		}
		protected virtual XVar getControlHTML(dynamic _param_value, dynamic _param_showValue, dynamic _param_dataValue, dynamic _param_totalValue, dynamic _param_separator, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic showValue = XVar.Clone(_param_showValue);
			dynamic dataValue = XVar.Clone(_param_dataValue);
			dynamic totalValue = XVar.Clone(_param_totalValue);
			dynamic separator = XVar.Clone(_param_separator);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			dynamic encodeDataValue = null, extraDataAttrs = null, filterControl = null, label = null, pageType = null;
			filterControl = new XVar("");
			encodeDataValue = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(dataValue)));
			extraDataAttrs = XVar.Clone(getExtraDataAttrs((XVar)(parentFiltersData)));
			pageType = new XVar("list");
			if(Constants.titREPORT == this.pSet.getEntityType())
			{
				pageType = new XVar("report");
			}
			else
			{
				if(Constants.titCHART == this.pSet.getEntityType())
				{
					pageType = new XVar("chart");
				}
			}
			if(this.multiSelect != Constants.FM_NONE)
			{
				dynamic checkBox = null, checkedAttr = null, style = null;
				style = XVar.Clone((XVar.Pack((XVar)(this.filtered)  || (XVar)(this.multiSelect == Constants.FM_ALWAYS)) ? XVar.Pack("") : XVar.Pack("style=\"display: none;\"")));
				checkedAttr = XVar.Clone(getCheckedAttr((XVar)(value), (XVar)(parentFiltersData)));
				checkBox = XVar.Clone(MVCFunctions.Concat("<input type=\"checkbox\" ", checkedAttr, " name=\"f[]\" value=\"", encodeDataValue, "\" ", extraDataAttrs, " class=\"multifilter-checkbox filter_", this.gfName, "_", this.id, "\" ", style, ">"));
				filterControl = MVCFunctions.Concat(filterControl, checkBox, "&nbsp;");
			}
			if(this.multiSelect != Constants.FM_ALWAYS)
			{
				dynamic href = null;
				href = XVar.Clone(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(this.tName))), (XVar)(pageType), (XVar)(MVCFunctions.Concat("f=(", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(this.fName)))), separator, encodeDataValue, ")"))));
				label = XVar.Clone(MVCFunctions.Concat("<a href=\"", href, "\" data-filtervalue=\"", encodeDataValue, "\" ", extraDataAttrs, " class=\"", this.gfName, "-filter-value\">", showValue, "</a>"));
			}
			else
			{
				label = XVar.Clone(MVCFunctions.Concat("<span>", showValue, "</span>"));
			}
			if((XVar)(this.useTotals)  && (XVar)(totalValue != XVar.Pack("")))
			{
				label = MVCFunctions.Concat(label, "<span>&nbsp;(", totalValue, ")</span>");
			}
			filterControl = MVCFunctions.Concat(filterControl, "<span dir=\"LTR\">", label, "</span>");
			return filterControl;
		}
		protected virtual XVar getExtraDataAttrs(dynamic _param_parentFiltersData)
		{
			#region pass-by-value parameters
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			return "";
		}
		protected virtual XVar getCheckedAttr(dynamic _param_value, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			if((XVar)(this.multiSelect == Constants.FM_NONE)  || (XVar)((XVar)(this.filtered)  && (XVar)(!(XVar)(MVCFunctions.in_array((XVar)(value), (XVar)(this.filteredFields[this.fName]["values"]))))))
			{
				return "";
			}
			return "checked=\"checked\"";
		}
		public virtual XVar getFilterButtonParams(dynamic _param_dBtnParams = null)
		{
			#region default values
			if(_param_dBtnParams as Object == null) _param_dBtnParams = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic dBtnParams = XVar.Clone(_param_dBtnParams);
			#endregion

			return new XVar("attrs", MVCFunctions.Concat("id=\"filter_", this.gfName, "_", this.id, "\""), "hasMultiselectBtn", this.multiSelect == Constants.FM_ON_DEMAND, "hasApplyBtn", this.useApllyBtn);
		}
		public virtual XVar getFilterState()
		{
			return new XVar("visible", this.visible, "filtered", this.filtered, "collapsed", this.showCollapsed, "truncated", isTruncated(), "showMoreHidden", isShowMoreHidden());
		}
		protected virtual XVar isShowMoreHidden()
		{
			return false;
		}
		public virtual XVar getFilterExtraControls(dynamic _param_dExtraCtrls = null)
		{
			#region default values
			if(_param_dExtraCtrls as Object == null) _param_dExtraCtrls = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic dExtraCtrls = XVar.Clone(_param_dExtraCtrls);
			#endregion

			dynamic selectAllAttrs = null;
			selectAllAttrs = new XVar("");
			if((XVar)(!(XVar)(this.filtered))  && (XVar)(!XVar.Equals(XVar.Pack(this.multiSelect), XVar.Pack(Constants.FM_NONE))))
			{
				selectAllAttrs = new XVar("checked=\"checked\"");
			}
			if(this.multiSelect == Constants.FM_ON_DEMAND)
			{
				selectAllAttrs = MVCFunctions.Concat(selectAllAttrs, " style=\"display: none;\"");
			}
			return new XVar("clearLink", this.filtered, "selectAllAttrs", selectAllAttrs, "numberOfExtraItemsToShow", getNumberOfExtraItemsToShow());
		}
		protected virtual XVar getNumberOfExtraItemsToShow()
		{
			return 0;
		}
		public virtual XVar isVisible()
		{
			return this.visible;
		}
		public virtual XVar isCollapsed()
		{
			return this.showCollapsed;
		}
		public virtual XVar isFiltered()
		{
			return this.filtered;
		}
		protected virtual XVar getDelButtonHtml(dynamic _param_gfName, dynamic _param_id, dynamic _param_deleteValue)
		{
			#region pass-by-value parameters
			dynamic gfName = XVar.Clone(_param_gfName);
			dynamic id = XVar.Clone(_param_id);
			dynamic deleteValue = XVar.Clone(_param_deleteValue);
			#endregion

			dynamic html = null;
			deleteValue = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(deleteValue)));
			html = XVar.Clone(MVCFunctions.Concat("<a class=\"delFilterCtrlButt_", gfName, "_", id, " delete-button\" data-delete=\"", deleteValue, "\" data-icon=\"remove\" href=\"#\"></a>"));
			return html;
		}
		protected virtual XVar decryptDataRow(dynamic data)
		{
			if(XVar.Pack(this.cipherer.isFieldPHPEncrypted((XVar)(this.fName))))
			{
				data.InitAndSetArrayItem(this.cipherer.DecryptField((XVar)(this.fName), (XVar)(data[this.fName])), this.fName);
			}
			return null;
		}
		public virtual XVar getLabel(dynamic _param_type, dynamic _param_message)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic message = XVar.Clone(_param_message);
			#endregion

			if(XVar.Equals(XVar.Pack(var_type), XVar.Pack("Text")))
			{
				return message;
			}
			return CommonFunctions.GetCustomLabel((XVar)(message));
		}
		public static XVar getFilterControl(dynamic _param_fName, dynamic _param_pageObj, dynamic _param_id, dynamic _param_viewControls)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObj = XVar.Clone(_param_pageObj);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			dynamic contorlType = null, fieldType = null;
			contorlType = XVar.Clone(pageObj.pSet.getFilterFieldFormat((XVar)(fName)));
			switch(((XVar)contorlType).ToString())
			{
				case Constants.FF_VALUE_LIST:
					return new FilterValuesList((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
				case Constants.FF_BOOLEAN:
					return new FilterBoolean((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
				case Constants.FF_INTERVAL_LIST:
					return new FilterIntervalList((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
				case Constants.FF_INTERVAL_SLIDER:
					fieldType = XVar.Clone(pageObj.pSet.getFieldType((XVar)(fName)));
					if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(fieldType))))
					{
						return new FilterIntervalDateSlider((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
					}
					if(XVar.Pack(CommonFunctions.IsTimeType((XVar)(fieldType))))
					{
						return new FilterIntervalTimeSlider((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
					}
					return new FilterIntervalSlider((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
				default:
					return new FilterValuesList((XVar)(fName), (XVar)(pageObj), (XVar)(id), (XVar)(viewControls));
			}
			return null;
		}
		protected virtual XVar buildBasicSQL()
		{
			dynamic mandatoryHaving = XVar.Array(), mandatoryWhere = XVar.Array(), optionalHaving = XVar.Array(), optionalWhere = XVar.Array(), query = null, sqlParts = XVar.Array();
			query = XVar.Clone(this.pSet.getSQLQuery());
			sqlParts = XVar.Clone(query.getSqlComponents());
			mandatoryWhere = XVar.Clone(XVar.Array());
			mandatoryHaving = XVar.Clone(XVar.Array());
			optionalWhere = XVar.Clone(XVar.Array());
			optionalHaving = XVar.Clone(XVar.Array());
			if(XVar.Pack(this.whereComponents["searchUnionRequired"]))
			{
				optionalWhere.InitAndSetArrayItem(this.whereComponents["searchWhere"], null);
				optionalHaving.InitAndSetArrayItem(this.whereComponents["searchHaving"], null);
			}
			else
			{
				mandatoryWhere.InitAndSetArrayItem(this.whereComponents["searchWhere"], null);
				mandatoryHaving.InitAndSetArrayItem(this.whereComponents["searchHaving"], null);
			}
			sqlParts["from"] = MVCFunctions.Concat(sqlParts["from"], this.whereComponents["joinFromPart"]);
			foreach (KeyValuePair<XVar, dynamic> w in this.whereComponents["filterWhere"].GetEnumerator())
			{
				if(w.Key != this.fName)
				{
					mandatoryWhere.InitAndSetArrayItem(w.Value, null);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> w in this.whereComponents["filterHaving"].GetEnumerator())
			{
				if(w.Key != this.fName)
				{
					mandatoryHaving.InitAndSetArrayItem(w.Value, null);
				}
			}
			mandatoryWhere.InitAndSetArrayItem(this.whereComponents["security"], null);
			mandatoryWhere.InitAndSetArrayItem(this.whereComponents["master"], null);
			return SQLQuery.buildSQL((XVar)(sqlParts), (XVar)(mandatoryWhere), (XVar)(mandatoryHaving), (XVar)(optionalWhere), (XVar)(optionalHaving));
		}
		protected virtual XVar getNotNullWhere()
		{
			dynamic ret = XVar.Array(), wName = null;
			ret = XVar.Clone(XVar.Array());
			wName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.fName)));
			ret.InitAndSetArrayItem(MVCFunctions.Concat(wName, " is not NULL"), null);
			if((XVar)(this.connection.dbType != Constants.nDATABASE_Oracle)  && (XVar)(CommonFunctions.IsCharType((XVar)(this.fieldType))))
			{
				ret.InitAndSetArrayItem(MVCFunctions.Concat(wName, " <> ''"), null);
			}
			if(XVar.Pack(!(XVar)(this.dependent)))
			{
				return ret;
			}
			foreach (KeyValuePair<XVar, dynamic> p in this.pSet.getParentFiltersNames((XVar)(this.fName)).GetEnumerator())
			{
				dynamic wp = null;
				wp = XVar.Clone(this.connection.addFieldWrappers((XVar)(p.Value)));
				ret.InitAndSetArrayItem(MVCFunctions.Concat(wp, " is not NULL"), null);
				if((XVar)(this.connection.dbType != Constants.nDATABASE_Oracle)  && (XVar)(CommonFunctions.IsCharType((XVar)(this.pSet.getFieldType((XVar)(p.Value))))))
				{
					ret.InitAndSetArrayItem(MVCFunctions.Concat(wp, " <> ''"), null);
				}
			}
			return ret;
		}
	}
}
