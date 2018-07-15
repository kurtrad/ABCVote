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
	public partial class ViewShortDateField : ViewControl
	{
		protected static bool skipViewShortDateFieldCtor = false;
		public ViewShortDateField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return getTextValue((XVar)(data));
		}
		public override XVar getExportValue(dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return data[this.field];
			return showDBValue((XVar)(data), (XVar)(keylink));
		}
		public override XVar getTextValue(dynamic data)
		{
			return CommonFunctions.format_shortdate((XVar)(CommonFunctions.db2time((XVar)(data[this.field]))));
		}
	}
}
