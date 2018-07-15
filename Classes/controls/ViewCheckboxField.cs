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
	public partial class ViewCheckboxField : ViewControl
	{
		protected static bool skipViewCheckboxFieldCtor = false;
		public ViewCheckboxField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic imgSrc = null, result = null, trueCondition = null;
			result = new XVar("<img src=\"");
			imgSrc = new XVar("images/check_");
			if(getHostPageDbType() == Constants.nDATABASE_PostgreSQL)
			{
				trueCondition = XVar.Clone((XVar)(XVar.Equals(XVar.Pack(data[this.field]), XVar.Pack("t")))  || (XVar)((XVar)(data[this.field] != 0)  && (XVar)(data[this.field] != "")));
			}
			else
			{
				trueCondition = XVar.Clone((XVar)(data[this.field] != 0)  && (XVar)(data[this.field] != ""));
			}
			imgSrc = MVCFunctions.Concat(imgSrc, (XVar.Pack(trueCondition) ? XVar.Pack("yes") : XVar.Pack("no")));
			result = MVCFunctions.Concat(result, MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.Concat(imgSrc, ".gif"))), "\" border=0");
			if(XVar.Pack(CommonFunctions.isEnableSection508()))
			{
				result = MVCFunctions.Concat(result, " alt=\" \"");
			}
			result = MVCFunctions.Concat(result, ">");
			return result;
		}
		protected virtual XVar getHostPageDbType()
		{
			return GlobalVars.cman.byTable((XVar)(this.container.tName)).dbType;
		}
		public override XVar getExportValue(dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return MVCFunctions.nl2br((XVar)(data[this.field]));
		}
	}
}
