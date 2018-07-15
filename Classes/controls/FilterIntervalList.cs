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
	public partial class FilterIntervalList : FilterControl
	{
		protected dynamic sqlTotalsString;
		protected dynamic intervalsData = XVar.Array();
		protected dynamic showWithNoRecords = XVar.Pack(false);
		protected static bool skipFilterIntervalListCtor = false;
		public FilterIntervalList(dynamic _param_fName, dynamic _param_pageObject, dynamic _param_id, dynamic _param_viewControls)
			:base((XVar)_param_fName, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_viewControls)
		{
			if(skipFilterIntervalListCtor)
			{
				skipFilterIntervalListCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.separator = new XVar("~interval~");
			this.filterFormat = new XVar(Constants.FF_INTERVAL_LIST);
			this.showWithNoRecords = XVar.Clone(this.pSet.showWithNoRecords((XVar)(fName)));
			this.useApllyBtn = XVar.Clone(this.multiSelect == Constants.FM_ALWAYS);
			setAggregateType();
			processRowIntervalData();
			buildSQL();
		}
		protected virtual XVar processRowIntervalData()
		{
			dynamic intervalsRowData = XVar.Array(), totals = XVar.Array();
			totals = XVar.Clone(XVar.Array());
			intervalsRowData = XVar.Clone(this.pSet.getFilterIntervals((XVar)(this.fName)));
			foreach (KeyValuePair<XVar, dynamic> intervalData in intervalsRowData.GetEnumerator())
			{
				dynamic caseCondition = null, idx = null;
				idx = XVar.Clone(intervalData.Value["index"]);
				caseCondition = XVar.Clone(getCaseCondition((XVar)(intervalData.Value)));
				totals.InitAndSetArrayItem(getTotalString((XVar)(caseCondition), (XVar)(idx)), null);
				setIntervalData((XVar)(MVCFunctions.Concat(this.fName, idx)), (XVar)(intervalData.Value), (XVar)(caseCondition));
			}
			this.sqlTotalsString = XVar.Clone(MVCFunctions.implode(new XVar(", "), (XVar)(totals)));
			return null;
		}
		protected virtual XVar getTotalString(dynamic _param_caseCondition, dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic caseCondition = XVar.Clone(_param_caseCondition);
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			dynamic caseStatement = null, totalString = null, wName = null, wTotalName = null;
			wName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.fName)));
			wTotalName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.totalsfName)));
			caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(wName), new XVar("null")));
			totalString = XVar.Clone(MVCFunctions.Concat(this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat(this.fName, idx))), " "));
			if((XVar)(this.useTotals)  && (XVar)(this.totalsfName != this.fName))
			{
				caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(wTotalName), new XVar("null")));
				totalString = MVCFunctions.Concat(totalString, ", ", this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat("TOTAL", idx))));
			}
			return totalString;
		}
		protected virtual XVar getTotalStringOld(dynamic _param_caseCondition, dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic caseCondition = XVar.Clone(_param_caseCondition);
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			dynamic caseStatement = null, fullFieldName = null, fullTotalFieldName = null, totalString = null;
			fullFieldName = XVar.Clone(getDbFieldName((XVar)(this.fName)));
			fullTotalFieldName = XVar.Clone(getDbFieldName((XVar)(this.totalsfName)));
			caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(fullFieldName), new XVar("null")));
			totalString = XVar.Clone(MVCFunctions.Concat(this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat(this.fName, idx))), " "));
			if((XVar)(this.useTotals)  && (XVar)(this.totalsfName != this.fName))
			{
				caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(fullTotalFieldName), new XVar("null")));
				totalString = MVCFunctions.Concat(totalString, ", ", this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat("TOTAL", idx))));
			}
			return totalString;
		}
		protected virtual XVar getCaseCondition(dynamic _param_interval)
		{
			#region pass-by-value parameters
			dynamic interval = XVar.Clone(_param_interval);
			#endregion

			return getIntervalFilterWhere((XVar)(this.fName), (XVar)(interval), (XVar)(this.pSet), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(this.connection), (XVar)(this.connection.addFieldWrappers((XVar)(this.fName))));
		}
		public static XVar getIntervalFilterWhere(dynamic _param_fName, dynamic _param_intervalData, dynamic _param_pSet_packed, dynamic _param_cipherer, dynamic _param_tableName, dynamic _param_connection, dynamic _param_sqlFieldName = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_sqlFieldName as Object == null) _param_sqlFieldName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic intervalData = XVar.Clone(_param_intervalData);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic tableName = XVar.Clone(_param_tableName);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic sqlFieldName = XVar.Clone(_param_sqlFieldName);
			#endregion

			if(sqlFieldName == XVar.Pack(""))
			{
				sqlFieldName = XVar.Clone(RunnerPage._getFieldSQL((XVar)(fName), (XVar)(connection), (XVar)(pSet)));
			}
			if(XVar.Pack(intervalData["remainder"]))
			{
				dynamic conditions = XVar.Array(), index = null, intervalsData = XVar.Array();
				index = XVar.Clone(intervalData["index"]);
				intervalsData = XVar.Clone(pSet.getFilterIntervals((XVar)(fName)));
				conditions = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> _intervalData in intervalsData.GetEnumerator())
				{
					if(XVar.Pack(_intervalData.Value["noLimits"]))
					{
						return "1=0";
					}
					if(_intervalData.Value["index"] == index)
					{
						continue;
					}
					conditions.InitAndSetArrayItem(getLimitsConditions((XVar)(fName), (XVar)(sqlFieldName), (XVar)(_intervalData.Value), (XVar)(cipherer), (XVar)(tableName), (XVar)(connection), new XVar(true)), null);
				}
				return MVCFunctions.implode(new XVar(" AND "), (XVar)(conditions));
			}
			if(XVar.Pack(intervalData["noLimits"]))
			{
				return MVCFunctions.Concat(sqlFieldName, " is not NULL AND ", sqlFieldName, " <> '' ");
			}
			return getLimitsConditions((XVar)(fName), (XVar)(sqlFieldName), (XVar)(intervalData), (XVar)(cipherer), (XVar)(tableName), (XVar)(connection));
		}
		public static XVar getLimitsConditions(dynamic _param_fName, dynamic _param_fullFieldName, dynamic _param_intervalData, dynamic _param_cipherer, dynamic _param_tableName, dynamic _param_connection, dynamic _param_inverted = null)
		{
			#region default values
			if(_param_inverted as Object == null) _param_inverted = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic fullFieldName = XVar.Clone(_param_fullFieldName);
			dynamic intervalData = XVar.Clone(_param_intervalData);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic tableName = XVar.Clone(_param_tableName);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic inverted = XVar.Clone(_param_inverted);
			#endregion

			dynamic condition = XVar.Array(), fValue = null, signLow = null, signUp = null;
			signLow = XVar.Clone(getIntervalSign((XVar)(intervalData["lowerLimitType"]), new XVar(true), (XVar)(inverted)));
			signUp = XVar.Clone(getIntervalSign((XVar)(intervalData["upperLimitType"]), new XVar(false), (XVar)(inverted)));
			condition = XVar.Clone(XVar.Array());
			if(XVar.Pack(signLow))
			{
				fValue = XVar.Clone(getLimitValue((XVar)(fName), (XVar)(intervalData), (XVar)(cipherer), (XVar)(tableName), new XVar(true)));
				if(XVar.Pack(intervalData["caseSensetive"]))
				{
					condition.InitAndSetArrayItem(MVCFunctions.Concat(fValue, signLow, fullFieldName), null);
				}
				else
				{
					condition.InitAndSetArrayItem(MVCFunctions.Concat(connection.upper((XVar)(fValue)), signLow, connection.upper((XVar)(fullFieldName))), null);
				}
			}
			if(XVar.Pack(signUp))
			{
				fValue = XVar.Clone(getLimitValue((XVar)(fName), (XVar)(intervalData), (XVar)(cipherer), (XVar)(tableName), new XVar(false)));
				if(XVar.Pack(intervalData["caseSensetive"]))
				{
					condition.InitAndSetArrayItem(MVCFunctions.Concat(fullFieldName, signUp, fValue), null);
				}
				else
				{
					condition.InitAndSetArrayItem(MVCFunctions.Concat(connection.upper((XVar)(fullFieldName)), signUp, connection.upper((XVar)(fValue))), null);
				}
			}
			return MVCFunctions.Concat("(", MVCFunctions.implode((XVar)((XVar.Pack(inverted) ? XVar.Pack(" OR ") : XVar.Pack(" AND "))), (XVar)(condition)), ")");
		}
		public static XVar getLimitValue(dynamic _param_fName, dynamic _param_intervalData, dynamic _param_cipherer, dynamic _param_tableName, dynamic _param_isLower)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic intervalData = XVar.Clone(_param_intervalData);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic tableName = XVar.Clone(_param_tableName);
			dynamic isLower = XVar.Clone(_param_isLower);
			#endregion

			dynamic fValue = null, isFieldEncrypted = null, keyPrefix = null;
			keyPrefix = XVar.Clone((XVar.Pack(isLower) ? XVar.Pack("lower") : XVar.Pack("upper")));
			isFieldEncrypted = XVar.Clone(cipherer.isFieldEncrypted((XVar)(fName)));
			if(XVar.Pack(intervalData[MVCFunctions.Concat(keyPrefix, "UsesExpression")]))
			{
				fValue = XVar.Clone(MVCFunctions.getIntervalLimitsExpressions((XVar)(tableName), (XVar)(fName), (XVar)(intervalData["index"]), (XVar)(isLower)));
			}
			else
			{
				fValue = XVar.Clone(intervalData[MVCFunctions.Concat(keyPrefix, "Limit")]);
			}
			if(XVar.Pack(isFieldEncrypted))
			{
				return cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true));
			}
			return CommonFunctions.make_db_value((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(""), (XVar)(tableName));
		}
		public static XVar getIntervalSign(dynamic _param_limitType, dynamic _param_isLowerBound, dynamic _param_inverted = null)
		{
			#region default values
			if(_param_inverted as Object == null) _param_inverted = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic limitType = XVar.Clone(_param_limitType);
			dynamic isLowerBound = XVar.Clone(_param_isLowerBound);
			dynamic inverted = XVar.Clone(_param_inverted);
			#endregion

			if((XVar)((XVar)(limitType == Constants.FIL_MORE_THAN)  && (XVar)(isLowerBound))  || (XVar)((XVar)(limitType == Constants.FIL_LESS_THAN)  && (XVar)(!(XVar)(isLowerBound))))
			{
				if(XVar.Pack(inverted))
				{
					return ">=";
				}
				return "<";
			}
			if((XVar)((XVar)(limitType == Constants.FIL_MORE_THAN_OR_EQUAL)  && (XVar)(isLowerBound))  || (XVar)((XVar)(limitType == Constants.FIL_LESS_THAN_OR_EQUAL)  && (XVar)(!(XVar)(isLowerBound))))
			{
				if(XVar.Pack(inverted))
				{
					return ">";
				}
				return "<=";
			}
			return "";
		}
		protected virtual XVar setIntervalData(dynamic _param_key, dynamic _param_intervalData, dynamic _param_caseCondition)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			dynamic intervalData = XVar.Clone(_param_intervalData);
			dynamic caseCondition = XVar.Clone(_param_caseCondition);
			#endregion

			dynamic message = null, var_type = null;
			var_type = XVar.Clone(intervalData["intervalLabelNameType"]);
			message = XVar.Clone(intervalData["intervalLabelText"]);
			this.intervalsData.InitAndSetArrayItem(getLabel((XVar)(var_type), (XVar)(message)), key, "label");
			this.intervalsData.InitAndSetArrayItem(caseCondition, key, "where");
			this.intervalsData.InitAndSetArrayItem(intervalData["index"], key, "index");
			return null;
		}
		protected override XVar getTotals()
		{
			return this.sqlTotalsString;
		}
		protected override XVar getValueToShow(dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic index = XVar.Clone(_param_index);
			#endregion

			dynamic interval = XVar.Array(), showValue = null;
			interval = XVar.Clone(this.pSet.getFilterIntervalDatabyIndex((XVar)(this.fName), (XVar)(index)));
			showValue = XVar.Clone(getLabel((XVar)(interval["intervalLabelNameType"]), (XVar)(interval["intervalLabelText"])));
			return showValue;
		}
		protected override XVar addFilterBlocksFromDB(dynamic filterCtrlBlocks)
		{
			dynamic aggFuncIsCount = null, data = XVar.Array(), visibilityClass = null;
			visibilityClass = XVar.Clone((XVar.Pack((XVar)(this.filtered)  && (XVar)(this.multiSelect == Constants.FM_ON_DEMAND)) ? XVar.Pack(this.onDemandHiddenItemClassName) : XVar.Pack("")));
			data = XVar.Clone(this.connection.query((XVar)(this.strSQL)).fetchAssoc());
			decryptDataRow((XVar)(data));
			aggFuncIsCount = XVar.Clone(this.aggregate == this.totalsOptions[Constants.FT_COUNT]);
			foreach (KeyValuePair<XVar, dynamic> intervalData in this.intervalsData.GetEnumerator())
			{
				dynamic fieldValue = null, totalValue = null;
				fieldValue = XVar.Clone(data[intervalData.Key]);
				totalValue = XVar.Clone(data[intervalData.Key]);
				if((XVar)(this.useTotals)  && (XVar)(this.fName != this.totalsfName))
				{
					totalValue = XVar.Clone(data[MVCFunctions.Concat("TOTAL", intervalData.Value["index"])]);
				}
				if((XVar)((XVar)(this.showWithNoRecords)  || (XVar)((XVar)(aggFuncIsCount)  && (XVar)((XVar)(XVar.Pack(0) < totalValue)  || (XVar)(XVar.Pack(0) < fieldValue))))  || (XVar)((XVar)(!(XVar)(aggFuncIsCount))  && (XVar)((XVar)(!(XVar)(totalValue == null))  || (XVar)(!(XVar)(fieldValue == null)))))
				{
					dynamic filterControl = null;
					this.valuesObtainedFromDB.InitAndSetArrayItem(intervalData.Value["index"], null);
					intervalData.Value.InitAndSetArrayItem(totalValue, "total");
					filterControl = XVar.Clone(buildControl((XVar)(intervalData.Value)));
					filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl), (XVar)(visibilityClass), (XVar)(intervalData.Value["index"])), null);
				}
			}
			return null;
		}
		protected override XVar getFilterBlockStructure(dynamic _param_filterControl, dynamic _param_visibilityClass, dynamic _param_value, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic filterControl = XVar.Clone(_param_filterControl);
			dynamic visibilityClass = XVar.Clone(_param_visibilityClass);
			dynamic value = XVar.Clone(_param_value);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			if(this.multiSelect != Constants.FM_ALWAYS)
			{
				visibilityClass = MVCFunctions.Concat(visibilityClass, " filter-link");
			}
			return new XVar(MVCFunctions.Concat(this.gfName, "_filter"), filterControl, MVCFunctions.Concat("visibilityClass_", this.gfName), visibilityClass, "sortValue", value);
		}
		protected override XVar sortFilterBlocks(ref dynamic filterCtrlBlocks)
		{
			MVCFunctions.usort((XVar)(filterCtrlBlocks), (XVar)(new XVar(0, "FilterControl", 1, "compareBlocksByNumericValues")));
			return null;
		}
		protected override XVar addOutRangeValuesToFilter(dynamic filterCtrlBlocks)
		{
			dynamic visibilityClass = null;
			visibilityClass = XVar.Clone((XVar.Pack(this.multiSelect == Constants.FM_ON_DEMAND) ? XVar.Pack(this.onDemandHiddenItemClassName) : XVar.Pack("")));
			foreach (KeyValuePair<XVar, dynamic> value in this.filteredFields[this.fName]["values"].GetEnumerator())
			{
				dynamic filterControl = null, label = null;
				if(XVar.Pack(MVCFunctions.in_array((XVar)(value.Value), (XVar)(this.valuesObtainedFromDB))))
				{
					continue;
				}
				foreach (KeyValuePair<XVar, dynamic> intervalData in this.intervalsData.GetEnumerator())
				{
					if(intervalData.Value["index"] == value.Value)
					{
						label = XVar.Clone(intervalData.Value["label"]);
						break;
					}
				}
				filterControl = XVar.Clone(buildControl((XVar)(new XVar("index", value.Value, "label", label))));
				filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl), (XVar)(visibilityClass), (XVar)(value.Value)), null);
			}
			return null;
		}
		protected virtual XVar buildControl(dynamic _param_data, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			dynamic dataValue = null, separator = null, showValue = null, totalValue = null, value = null;
			value = XVar.Clone(data["index"]);
			showValue = XVar.Clone(data["label"]);
			dataValue = XVar.Clone(data["index"]);
			totalValue = XVar.Clone(getTotalValueToShow((XVar)(data["total"])));
			separator = XVar.Clone(this.separator);
			return getControlHTML((XVar)(value), (XVar)(showValue), (XVar)(dataValue), (XVar)(totalValue), (XVar)(separator));
		}
	}
}
