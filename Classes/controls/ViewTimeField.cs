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
	public partial class ViewTimeField : ViewControl
	{
		protected static bool skipViewTimeFieldCtor = false;
		public ViewTimeField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic result = null;
			result = XVar.Clone(getTextValue((XVar)(data)));
			if((XVar)(!(XVar)(this.container.forExport))  || (XVar)((XVar)(this.container.forExport != "excel")  && (XVar)(this.container.forExport != "csv")))
			{
				result = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(result)));
			}
			return result;
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic numbers = XVar.Array(), result = null;
			result = new XVar("");
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(this.fieldType))))
			{
				return CommonFunctions.str_format_time((XVar)(CommonFunctions.db2time((XVar)(data[this.field]))));
			}
			numbers = XVar.Clone(CommonFunctions.parsenumbers((XVar)(data[this.field])));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(numbers))))
			{
				return "";
			}
			while(MVCFunctions.count(numbers) < 3)
			{
				numbers.InitAndSetArrayItem(0, null);
			}
			if(MVCFunctions.count(numbers) == 6)
			{
				return CommonFunctions.str_format_time((XVar)(new XVar(0, 0, 1, 0, 2, 0, 3, numbers[3], 4, numbers[4], 5, numbers[5])));
			}
			return CommonFunctions.str_format_time((XVar)(new XVar(0, 0, 1, 0, 2, 0, 3, numbers[0], 4, numbers[1], 5, numbers[2])));
		}
	}
}
