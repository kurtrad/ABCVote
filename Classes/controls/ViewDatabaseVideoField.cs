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
	public partial class ViewDatabaseVideoField : ViewControl
	{
		protected static bool skipViewDatabaseVideoFieldCtor = false;
		public ViewDatabaseVideoField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar addJSFiles()
		{
			AddJSFile(new XVar("include/video/projekktor.js"));
			getJSControl();
			return null;
		}
		public override XVar addCSSFiles()
		{
			AddCSSFile(new XVar("include/video/theme/style.css"));
			return null;
		}
		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic fileName = null, fileNameF = null, value = null;
			value = new XVar("");
			if((XVar)(data[this.field] != null)  && (XVar)(this.container.pageType != Constants.PAGE_PRINT))
			{
				dynamic href = null, vHeight = null, vWidth = null, var_type = null, videoId = null;
				videoId = XVar.Clone(MVCFunctions.Concat("video_", MVCFunctions.GoodFieldName((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(this.field)))), "_"));
				videoId = MVCFunctions.Concat(videoId, getContainer().id, "_");
				if(getContainer().pageType != Constants.PAGE_ADD)
				{
					videoId = MVCFunctions.Concat(videoId, getContainer().recId);
				}
				else
				{
					videoId = MVCFunctions.Concat(videoId, MVCFunctions.postvalue(new XVar("id")));
				}
				var_type = new XVar("video/mp4");
				fileName = new XVar("file.mp4");
				fileNameF = XVar.Clone(getContainer().pSet.getFilenameField((XVar)(this.field)));
				if(XVar.Pack(fileNameF))
				{
					fileName = XVar.Clone(data[fileNameF]);
					if(XVar.Pack(!(XVar)(fileName)))
					{
						fileName = new XVar("file.mp4");
					}
					else
					{
						var_type = XVar.Clone(CommonFunctions.getContentTypeByExtension((XVar)(MVCFunctions.substr((XVar)(fileName), (XVar)(MVCFunctions.strrpos((XVar)(fileName), new XVar(".")))))));
					}
				}
				href = XVar.Clone(MVCFunctions.GetTableLink(new XVar("mfhandler"), new XVar(""), (XVar)(MVCFunctions.Concat("filename=", fileName, "&table=", MVCFunctions.RawUrlEncode((XVar)(getContainer().pSet._table)), "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), "&pageType=", getContainer().pageType, keylink))));
				vWidth = XVar.Clone(getContainer().pSet.getVideoWidth((XVar)(this.field)));
				vHeight = XVar.Clone(getContainer().pSet.getVideoHeight((XVar)(this.field)));
				if(vWidth == XVar.Pack(0))
				{
					vWidth = new XVar(300);
				}
				if(vHeight == XVar.Pack(0))
				{
					vHeight = new XVar(200);
				}
				value = MVCFunctions.Concat(value, "\r\n\t\t\t\t<div style=\"width:", vWidth, "px; height:", vHeight, "px;\">\r\n\t\t\t\t<video class=\"projekktor\" width=\"", vWidth, "\" height=\"", vHeight, "\" id=\"", videoId, "\" type=\"", var_type, "\" src=\"", href, "\" >\r\n\t\t\t\t</video></div>");
				if(this.pageObject != null)
				{
					this.pageObject.controlsMap.InitAndSetArrayItem(videoId, "video", null);
				}
			}
			else
			{
				fileNameF = XVar.Clone(getContainer().pSet.getFilenameField((XVar)(this.field)));
				if(XVar.Pack(fileNameF))
				{
					fileName = XVar.Clone(data[fileNameF]);
					if(XVar.Pack(!(XVar)(fileName)))
					{
						value = XVar.Clone(fileName);
					}
				}
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
			fileNameField = XVar.Clone(getContainer().pSet.getFilenameField((XVar)(this.field)));
			if((XVar)(fileNameField)  && (XVar)(data[fileNameField]))
			{
				return data[fileNameField];
			}
			return "<<Video>>";
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
