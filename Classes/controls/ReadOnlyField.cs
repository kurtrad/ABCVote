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
	public partial class ReadOnlyField : TextControl
	{
		protected static bool skipReadOnlyFieldCtor = false;
		private bool skipTextControlCtorSurrogate = new Func<bool>(() => skipTextControlCtor = true).Invoke();
		public ReadOnlyField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipReadOnlyFieldCtor)
			{
				skipReadOnlyFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.format = new XVar(Constants.EDIT_FORMAT_READONLY);
		}
		public override XVar buildControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			if((XVar)((XVar)((XVar)(mode == Constants.MODE_EDIT)  || (XVar)(mode == Constants.MODE_ADD))  || (XVar)(mode == Constants.MODE_INLINE_EDIT))  || (XVar)(mode == Constants.MODE_INLINE_ADD))
			{
				MVCFunctions.Echo(MVCFunctions.Concat("<span id=\"readonly_", this.cfield, "\" ", this.inputStyle, ">", this.pageObject.readOnlyFields[this.field], "</span>"));
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"Hidden\" name=\"", this.cfield, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\">"));
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
	}
}
