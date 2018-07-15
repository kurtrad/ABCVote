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
	public partial class ViewDatabaseAudioField : ViewControl
	{
		protected static bool skipViewDatabaseAudioFieldCtor = false;
		public ViewDatabaseAudioField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic title = null, titleField = null, value = null;
			value = new XVar("");
			title = new XVar("");
			titleField = XVar.Clone(this.container.pSet.getAudioTitleField((XVar)(this.field)));
			if(XVar.Pack(titleField))
			{
				title = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(data[titleField])));
			}
			if((XVar)(data[this.field] != null)  && (XVar)(this.container.pageType != Constants.PAGE_PRINT))
			{
				dynamic href = null, link = null;
				if(XVar.Pack(!(XVar)(title)))
				{
					title = new XVar("Track.mp3");
				}
				href = XVar.Clone(MVCFunctions.GetTableLink(new XVar("getfile"), new XVar(""), (XVar)(MVCFunctions.Concat("table=", CommonFunctions.GetTableURL((XVar)(this.container.pSet._table)), "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), keylink, "&filename=", title))));
				link = XVar.Clone(MVCFunctions.Concat("<a title=\"", title, "\" href=\"", href, "\">", title, "</a>"));
				value = XVar.Clone(MVCFunctions.Concat("<audio controls preload=\"none\" src=\"", href, "\">", link, "</audio>"));
			}
			else
			{
				value = XVar.Clone(title);
			}
			return value;
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic titleField = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(data[this.field])))))
			{
				return "";
			}
			titleField = XVar.Clone(this.container.pSet.getAudioTitleField((XVar)(this.field)));
			if((XVar)(titleField)  && (XVar)(data[titleField]))
			{
				return data[titleField];
			}
			return "<<Audio>>";
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
