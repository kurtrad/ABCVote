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
	public partial class ViewVideoFileField : ViewFileField
	{
		protected static bool skipViewVideoFileFieldCtor = false;
		public ViewVideoFileField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject) {}

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

			dynamic fieldIsUrl = null, fileName = null, value = null;
			value = new XVar("");
			fieldIsUrl = XVar.Clone(this.container.pSet.isVideoUrlField((XVar)(this.field)));
			fileName = XVar.Clone(data[this.field]);
			if(XVar.Pack(MVCFunctions.strlen((XVar)(fileName))))
			{
				dynamic counter = null, filesArray = XVar.Array(), pageType = null, printOrExportPage = null;
				if(XVar.Pack(!(XVar)(fieldIsUrl)))
				{
					this.upload_handler.tkeys = XVar.Clone(keylink);
					filesArray = XVar.Clone(getFilesArray((XVar)(fileName)));
				}
				else
				{
					filesArray = XVar.Clone(new XVar(0, fileName));
				}
				pageType = XVar.Clone(this.container.pageType);
				printOrExportPage = XVar.Clone((XVar)((XVar)(pageType == Constants.PAGE_EXPORT)  || (XVar)(pageType == Constants.PAGE_PRINT))  || (XVar)(this.container.forExport != ""));
				counter = new XVar(0);
				foreach (KeyValuePair<XVar, dynamic> file in filesArray.GetEnumerator())
				{
					dynamic href = null, vHeight = null, vWidth = null, var_type = null, videoId = null;
					if(XVar.Pack(printOrExportPage))
					{
						if(value != XVar.Pack(""))
						{
							value = MVCFunctions.Concat(value, ", ");
						}
						if(XVar.Pack(fieldIsUrl))
						{
							value = MVCFunctions.Concat(value, fileName);
						}
						else
						{
							value = MVCFunctions.Concat(value, file.Value["usrName"]);
						}
						continue;
					}
					if(XVar.Pack(!(XVar)(fieldIsUrl)))
					{
						if(XVar.Pack(!(XVar)(MVCFunctions.file_exists((XVar)(MVCFunctions.getabspath((XVar)(file.Value["name"])))))))
						{
							continue;
						}
					}
					videoId = XVar.Clone(MVCFunctions.Concat("video_", MVCFunctions.GoodFieldName((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(this.field)))), "_"));
					videoId = MVCFunctions.Concat(videoId, getContainer().id, "_");
					if((XVar)(pageType != Constants.PAGE_ADD)  && (XVar)(pageType != Constants.PAGE_EDIT))
					{
						videoId = MVCFunctions.Concat(videoId, getContainer().recId);
					}
					else
					{
						videoId = MVCFunctions.Concat(videoId, MVCFunctions.postvalue(new XVar("id")));
					}
					videoId = MVCFunctions.Concat(videoId, "_", counter++);
					if(XVar.Pack(fieldIsUrl))
					{
						href = XVar.Clone(fileName);
						if(fileName != XVar.Pack(""))
						{
							dynamic ext = null, pos = null;
							pos = XVar.Clone(MVCFunctions.strrpos((XVar)(fileName), new XVar(".")));
							ext = XVar.Clone(MVCFunctions.substr((XVar)(fileName), (XVar)(pos)));
							var_type = XVar.Clone(CommonFunctions.getContentTypeByExtension((XVar)(ext)));
							if(var_type == "application/octet-stream")
							{
								var_type = new XVar("video/flv");
							}
						}
					}
					else
					{
						dynamic userFile = XVar.Array();
						userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(file.Value)));
						href = XVar.Clone(userFile["url"]);
						if(XVar.Pack(!(XVar)(getContainer().pSet.isRewindEnabled((XVar)(this.field)))))
						{
							href = MVCFunctions.Concat(href, (XVar.Pack(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(href), new XVar("?"))), XVar.Pack(false))) ? XVar.Pack("?") : XVar.Pack("&")), "norange=1");
						}
						if(file.Value["type"] == "application/octet-stream")
						{
							var_type = new XVar("video/flv");
						}
						else
						{
							var_type = XVar.Clone(file.Value["type"]);
						}
					}
					if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(var_type), new XVar("video"))), XVar.Pack(0)))
					{
						continue;
					}
					if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(href), new XVar("rndVal="))), XVar.Pack(false)))
					{
						href = MVCFunctions.Concat(href, (XVar.Pack(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(href), new XVar("?"))), XVar.Pack(false))) ? XVar.Pack("?") : XVar.Pack("&")), "rndVal=", MVCFunctions.rand(new XVar(0), new XVar(99999999)));
					}
					else
					{
						dynamic endPos = null, startPos = null;
						startPos = XVar.Clone(MVCFunctions.strpos((XVar)(href), new XVar("rndVal=")) + 7);
						endPos = XVar.Clone(MVCFunctions.strpos((XVar)(href), new XVar("&"), (XVar)(startPos)));
						href = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(href), new XVar(0), (XVar)(startPos)), MVCFunctions.rand(new XVar(0), new XVar(99999999)), (XVar.Pack(endPos != -1) ? XVar.Pack(MVCFunctions.substr((XVar)(href), (XVar)(endPos))) : XVar.Pack(""))));
					}
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
					value = MVCFunctions.Concat(value, "<div style=\"width:", vWidth, "px; height:", vHeight, "px;\">\r\n\t\t\t\t\t<video class=\"projekktor\"  width=\"", vWidth, "\" height=\"", vHeight, "\"  id=\"", videoId, "\" type=\"", var_type, "\" src=\"", href, "\">\r\n\t\t\t\t\t</video></div>");
					if(this.pageObject != null)
					{
						this.pageObject.controlsMap.InitAndSetArrayItem(videoId, "video", null);
					}
				}
			}
			return value;
		}
	}
}
