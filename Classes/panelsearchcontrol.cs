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
	public partial class PanelSearchControl : SearchControl
	{
		protected static bool skipPanelSearchControlCtor = false;
		public PanelSearchControl(dynamic _param_id, dynamic _param_tName, dynamic searchClauseObj, dynamic pageObj) // proxy constructor
			:base((XVar)_param_id, (XVar)_param_tName, (XVar)searchClauseObj, (XVar)pageObj) {}

		public override XVar getCtrlParamsArr(dynamic _param_fName, dynamic _param_recId, dynamic _param_fieldNum, dynamic _param_value, dynamic _param_opt, dynamic _param_renderHidden = null, dynamic _param_isCached = null)
		{
			#region default values
			if(_param_renderHidden as Object == null) _param_renderHidden = new XVar(false);
			if(_param_isCached as Object == null) _param_isCached = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic recId = XVar.Clone(_param_recId);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic value = XVar.Clone(_param_value);
			dynamic opt = XVar.Clone(_param_opt);
			dynamic renderHidden = XVar.Clone(_param_renderHidden);
			dynamic isCached = XVar.Clone(_param_isCached);
			#endregion

			dynamic control = null, ctrlsMap = null, parameters = XVar.Array();
			parameters = XVar.Clone(base.buildCtrlParamsArr((XVar)(fName), (XVar)(recId), (XVar)(fieldNum), (XVar)(value), (XVar)(opt), (XVar)(renderHidden), (XVar)(isCached)));
			control = XVar.Clone(XVar.Array());
			parameters.InitAndSetArrayItem(true, "additionalCtrlParams", "searchPanelControl");
			parameters.InitAndSetArrayItem((XVar.Pack(this.pageObj.mobileTemplateMode()) ? XVar.Pack("width: 35%;") : XVar.Pack("width: 115px;")), "additionalCtrlParams", "style");
			ctrlsMap = XVar.Clone(getExtraControlMap());
			this.pageObj.fillControlsMap((XVar)(ctrlsMap), new XVar(true));
			return XTempl.create_function_assignment(new XVar("xt_buildeditcontrol"), (XVar)(parameters));
		}
		protected virtual XVar getExtraControlMap()
		{
			dynamic ctrlsMap = XVar.Array();
			ctrlsMap = XVar.Clone(new XVar("controls", XVar.Array()));
			ctrlsMap.InitAndSetArrayItem(true, "controls", "skipDependencies");
			return ctrlsMap;
		}
		public virtual XVar simpleSearchFieldCombo(dynamic _param_fNamesArr, dynamic _param_selOpt)
		{
			#region pass-by-value parameters
			dynamic fNamesArr = XVar.Clone(_param_fNamesArr);
			dynamic selOpt = XVar.Clone(_param_selOpt);
			#endregion

			dynamic options = null;
			options = new XVar("");
			if(MVCFunctions.count(this.pSet.getGoogleLikeFields()) != 0)
			{
				options = XVar.Clone(MVCFunctions.Concat("<option value=\"\" >", "Any field", "</option>"));
			}
			foreach (KeyValuePair<XVar, dynamic> fName in fNamesArr.GetEnumerator())
			{
				dynamic fLabel = null;
				fLabel = XVar.Clone(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(fName.Value)))));
				options = MVCFunctions.Concat(options, "<option value=\"", fName.Value, "\" ", (XVar.Pack(selOpt == fName.Value) ? XVar.Pack("selected") : XVar.Pack("")), ">", fLabel, "</option>");
			}
			return options;
		}
		public override XVar getCtrlSearchTypeOptions(dynamic _param_fName, dynamic _param_selOpt, dynamic _param_not, dynamic _param_flexible = null, dynamic _param_both = null)
		{
			#region default values
			if(_param_flexible as Object == null) _param_flexible = new XVar(false);
			if(_param_both as Object == null) _param_both = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic flexible = XVar.Clone(_param_flexible);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			if((XVar)(!(XVar)(flexible))  && (XVar)((XVar)(selOpt == Constants.EMPTY_SEARCH)  || (XVar)(selOpt == Constants.NOT_EMPTY)))
			{
				return getControl((XVar)(fName)).buildSearchOptions((XVar)(new XVar(0, Constants.EMPTY_SEARCH, 1, Constants.NOT_EMPTY)), (XVar)(selOpt), (XVar)(var_not), new XVar(true));
			}
			return getControl((XVar)(fName)).getSearchOptions((XVar)(selOpt), (XVar)(var_not), new XVar(true));
		}
	}
}
