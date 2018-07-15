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
	public partial class ViewDatabaseFileField : ViewControl
	{
		protected static bool skipViewDatabaseFileFieldCtor = false;
		public ViewDatabaseFileField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic fileName = null, fileNameF = null, value = null;
			value = new XVar("");
			fileNameF = XVar.Clone(this.container.pSet.getFilenameField((XVar)(this.field)));
			if(XVar.Pack(fileNameF))
			{
				fileName = XVar.Clone(data[fileNameF]);
				if(XVar.Pack(!(XVar)(fileName)))
				{
					fileName = new XVar("file.bin");
				}
			}
			else
			{
				fileName = new XVar("file.bin");
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(data[this.field]))))
			{
				value = XVar.Clone(MVCFunctions.Concat("<a href='", MVCFunctions.GetTableLink(new XVar("getfile"), new XVar(""), (XVar)(MVCFunctions.Concat("table=", CommonFunctions.GetTableURL((XVar)(this.container.pSet._table)), "&filename=", MVCFunctions.RawUrlEncode((XVar)(fileName)), "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), keylink))), "'>"));
				value = MVCFunctions.Concat(value, MVCFunctions.runner_htmlspecialchars((XVar)(fileName)));
				value = MVCFunctions.Concat(value, "</a>");
			}
			return value;
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic fileNameField = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(data[this.field])))))
			{
				return "";
			}
			fileNameField = XVar.Clone(this.container.pSet.getFilenameField((XVar)(this.field)));
			if((XVar)(fileNameField)  && (XVar)(data[fileNameField]))
			{
				return data[fileNameField];
			}
			return "<<File>>";
		}
		public override XVar getExportValue(dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return "LONG BINARY DATA - CANNOT BE DISPLAYED";
		}
	}
}
