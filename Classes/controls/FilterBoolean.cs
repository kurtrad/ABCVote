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
	public partial class FilterBoolean : FilterControl
	{
		protected static bool skipFilterBooleanCtor = false;
		public FilterBoolean(dynamic _param_fName, dynamic _param_pageObject, dynamic _param_id, dynamic _param_viewControls)
			:base((XVar)_param_fName, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_viewControls)
		{
			if(skipFilterBooleanCtor)
			{
				skipFilterBooleanCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.separator = new XVar("~checked~");
			this.filterFormat = new XVar(Constants.FF_BOOLEAN);
			setAggregateType();
			buildSQL();
		}
		protected override XVar getTotals()
		{
			dynamic bNeedQuotes = null, booleanData = XVar.Array(), fullFieldName = null, fullTotalFieldName = null, totals = XVar.Array(), var_type = null;
			var_type = XVar.Clone(this.pSet.getFieldType((XVar)(this.fName)));
			bNeedQuotes = XVar.Clone(CommonFunctions.NeedQuotes((XVar)(var_type)));
			fullFieldName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.fName)));
			fullTotalFieldName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.totalsfName)));
			booleanData = XVar.Clone(new XVar(0, "checked", 1, "unchecked"));
			totals = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> _var_type in booleanData.GetEnumerator())
			{
				dynamic caseCondition = null, caseStatement = null, var_checked = null;
				var_checked = XVar.Clone(_var_type.Value == "checked");
				caseCondition = XVar.Clone(CheckboxField.constructFieldWhere((XVar)(fullFieldName), (XVar)(bNeedQuotes), (XVar)(var_checked), (XVar)(_var_type.Value), (XVar)(this.connection.dbType)));
				caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(fullTotalFieldName), new XVar("null")));
				totals.InitAndSetArrayItem(MVCFunctions.Concat(this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(_var_type.Value))), null);
				if((XVar)(this.useTotals)  && (XVar)(this.fName != this.totalsfName))
				{
					caseStatement = XVar.Clone(getCaseStatement((XVar)(caseCondition), (XVar)(fullFieldName), new XVar("null")));
					totals.InitAndSetArrayItem(MVCFunctions.Concat(this.aggregate, "(", caseStatement, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat(this.fName, _var_type.Value)))), null);
				}
			}
			return MVCFunctions.implode(new XVar(", "), (XVar)(totals));
		}
		protected override XVar getValueToShow(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic showValue = null, var_checked = null;
			var_checked = XVar.Clone(value == "on");
			showValue = XVar.Clone(getShownValue((XVar)(var_checked)));
			return showValue;
		}
		protected virtual XVar getShownValue(dynamic _param_checked)
		{
			#region pass-by-value parameters
			dynamic var_checked = XVar.Clone(_param_checked);
			#endregion

			dynamic message = null, var_type = null;
			if(XVar.Pack(var_checked))
			{
				var_type = XVar.Clone(this.pSet.getFilterCheckedMessageType((XVar)(this.fName)));
				message = XVar.Clone(this.pSet.getFilterCheckedMessage((XVar)(this.fName)));
			}
			else
			{
				var_type = XVar.Clone(this.pSet.getFilterUncheckedMessageType((XVar)(this.fName)));
				message = XVar.Clone(this.pSet.getFilterUncheckedMessage((XVar)(this.fName)));
			}
			return getLabel((XVar)(var_type), (XVar)(message));
		}
		protected override XVar addFilterBlocksFromDB(dynamic filterCtrlBlocks)
		{
			dynamic aggFuncIsCount = null, data = XVar.Array(), dataTotals = XVar.Array();
			data = XVar.Clone(this.connection.query((XVar)(this.strSQL)).fetchAssoc());
			decryptDataRow((XVar)(data));
			aggFuncIsCount = XVar.Clone(this.aggregate == this.totalsOptions[Constants.FT_COUNT]);
			dataTotals = XVar.Clone(new XVar("checked", data["checked"], "unchecked", data["unchecked"]));
			foreach (KeyValuePair<XVar, dynamic> total in dataTotals.GetEnumerator())
			{
				dynamic fieldTotal = null;
				if((XVar)(this.useTotals)  && (XVar)(this.fName != this.totalsfName))
				{
					fieldTotal = XVar.Clone(data[MVCFunctions.Concat(this.fName, total.Key)]);
				}
				if((XVar)((XVar)(aggFuncIsCount)  && (XVar)((XVar)(XVar.Pack(0) < total.Value)  || (XVar)(XVar.Pack(0) < fieldTotal)))  || (XVar)((XVar)(!(XVar)(aggFuncIsCount))  && (XVar)((XVar)(!(XVar)(total.Value == null))  || (XVar)(!(XVar)(fieldTotal == null)))))
				{
					dynamic booleanData = null, filterControl = null;
					booleanData = XVar.Clone(new XVar("checked", total.Key == "checked", "total", total.Value));
					filterControl = XVar.Clone(buildControl((XVar)(booleanData)));
					filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl)), null);
				}
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

			dynamic separator = null, showValue = null, totalValue = null, value = null, var_checked = null;
			var_checked = XVar.Clone(data["checked"]);
			value = XVar.Clone((XVar.Pack(var_checked) ? XVar.Pack("on") : XVar.Pack("off")));
			showValue = XVar.Clone(getShownValue((XVar)(var_checked)));
			totalValue = XVar.Clone(getTotalValueToShow((XVar)(data["total"])));
			separator = XVar.Clone(this.separator);
			return getControlHTML((XVar)(value), (XVar)(showValue), (XVar)(value), (XVar)(totalValue), (XVar)(separator));
		}
	}
}
