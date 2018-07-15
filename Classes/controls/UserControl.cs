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
	public partial class UserControl : EditControl
	{
		protected static bool skipUserControlCtor = false;
		public UserControl(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection) {}

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
			buildUserControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		public virtual XVar buildUserControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			return null;
		}
		public virtual XVar initUserControl()
		{
			return null;
		}
		public virtual XVar getUserSearchOptions()
		{
			return XVar.Array();
		}
		public override XVar getSearchOptions(dynamic _param_selOpt, dynamic _param_not, dynamic _param_both)
		{
			#region pass-by-value parameters
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			return buildSearchOptions((XVar)(getUserSearchOptions()), (XVar)(selOpt), (XVar)(var_not), (XVar)(both));
		}
		public override XVar init()
		{
			dynamic field = null, tName = null;
			return null;
		}
	}
}
