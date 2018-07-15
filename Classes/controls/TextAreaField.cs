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
	public partial class TextAreaField : TextControl
	{
		protected static bool skipTextAreaFieldCtor = false;
		private bool skipTextControlCtorSurrogate = new Func<bool>(() => skipTextControlCtor = true).Invoke();
		public TextAreaField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipTextAreaFieldCtor)
			{
				skipTextAreaFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.format = new XVar(Constants.EDIT_FORMAT_TEXT_AREA);
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

			dynamic nHeight = null, nWidth = null;
			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			nWidth = XVar.Clone(this.pageObject.pSetEdit.getNCols((XVar)(this.field)));
			nHeight = XVar.Clone(this.pageObject.pSetEdit.getNRows((XVar)(this.field)));
			if(XVar.Pack(this.pageObject.pSetEdit.isUseRTE((XVar)(this.field))))
			{
				dynamic browser = null;
				value = XVar.Clone(RTESafe((XVar)(value)));
				browser = new XVar("");
				if(MVCFunctions.postvalue("browser") == "ie")
				{
					browser = new XVar("&browser=ie");
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<iframe frameborder=\"0\" vspace=\"0\" hspace=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" id=\"", this.cfield, "\" ", (XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508 == true)) ? XVar.Pack(MVCFunctions.Concat("alt=\"", this.strLabel, "\" ")) : XVar.Pack("")), "name=\"", this.cfield, "\" title=\"Basic rich text editor\" style='"));
				if(XVar.Pack(!(XVar)(this.pageObject.mobileTemplateMode())))
				{
					MVCFunctions.Echo(MVCFunctions.Concat("width: ", nWidth + 1, "px;"));
				}
				MVCFunctions.Echo(MVCFunctions.Concat("height: ", nHeight + 100, "px;'"));
				MVCFunctions.Echo(MVCFunctions.Concat(" src=\"", MVCFunctions.GetTableLink(new XVar("rte"), new XVar(""), (XVar)(MVCFunctions.Concat("ptype=", this.pageObject.pageType, "&table=", CommonFunctions.GetTableURL((XVar)(this.pageObject.tName)), "&", "id=", this.id, "&", this.iquery, browser, "&", (XVar.Pack((XVar)(mode == Constants.MODE_ADD)  || (XVar)(mode == Constants.MODE_INLINE_ADD)) ? XVar.Pack("action=add") : XVar.Pack(""))))), "\">"));
				MVCFunctions.Echo("</iframe>");
			}
			else
			{
				dynamic classString = null, style = null;
				classString = new XVar("");
				style = XVar.Clone(MVCFunctions.Concat("height: ", nHeight, "px;"));
				if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					classString = new XVar(" class=\"form-control\"");
					if((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))
					{
						style = MVCFunctions.Concat(style, "width: ", nWidth, "px;");
					}
				}
				else
				{
					if(XVar.Pack(!(XVar)(this.pageObject.mobileTemplateMode())))
					{
						style = MVCFunctions.Concat(style, "width: ", nWidth, "px;");
					}
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<textarea ", getPlaceholderAttr(), " id=\"", this.cfield, "\" ", classString, " alt=\"", this.strLabel, "\" name=\"", this.cfield, "\" style=\"", style, "\">", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "</textarea>"));
			}
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		public override XVar getFirstElementId()
		{
			return this.cfield;
		}
		protected virtual XVar RTESafe(dynamic _param_text)
		{
			#region pass-by-value parameters
			dynamic text = XVar.Clone(_param_text);
			#endregion

			dynamic tmpString = null;
			tmpString = XVar.Clone(MVCFunctions.trim((XVar)(text)));
			if(XVar.Pack(!(XVar)(tmpString)))
			{
				return "";
			}
			tmpString = XVar.Clone(MVCFunctions.str_replace(new XVar("'"), new XVar("&#39;"), (XVar)(tmpString)));
			tmpString = XVar.Clone(MVCFunctions.str_replace((XVar)(MVCFunctions.chr(new XVar(10))), new XVar(" "), (XVar)(tmpString)));
			tmpString = XVar.Clone(MVCFunctions.str_replace((XVar)(MVCFunctions.chr(new XVar(13))), new XVar(" "), (XVar)(tmpString)));
			return tmpString;
		}
		protected virtual XVar CreateCKeditor(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"disabledCKE_", this.cfield, "\"><textarea id=\"", this.cfield, "\" name=\"", this.cfield, "\" rows=\"8\" cols=\"60\">", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "</textarea></div>"));
			return null;
		}
	}
}
