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
	public partial class ViewFileDownloadField : ViewFileField
	{
		public dynamic sizeUnits = XVar.Array();
		protected static bool skipViewFileDownloadFieldCtor = false;
		public ViewFileDownloadField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject)
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject)
		{
			if(skipViewFileDownloadFieldCtor)
			{
				skipViewFileDownloadFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic container = XVar.Clone(_param_container);
			dynamic pageobject = XVar.Clone(_param_pageobject);
			#endregion

			dynamic pageObject = null;
			this.sizeUnits = XVar.Clone(new XVar(0, "KB", 1, "MB", 2, "GB", 3, "TB"));
		}
		public override XVar addJSFiles()
		{
			if(XVar.Pack(this.container.pSet.showThumbnail((XVar)(this.field))))
			{
				AddJSFile(new XVar("include/zoombox/zoombox.js"));
				getJSControl();
			}
			return null;
		}
		public override XVar addCSSFiles()
		{
			if(XVar.Pack(this.container.pSet.showThumbnail((XVar)(this.field))))
			{
				AddCSSFile(new XVar("include/zoombox/zoombox.css"));
			}
			return null;
		}
		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic filesArray = XVar.Array(), isExport = null, showThumbnails = null, value = null, zoomboxRand = null;
			value = new XVar("");
			this.upload_handler.tkeys = XVar.Clone(keylink);
			filesArray = XVar.Clone(getFilesArray((XVar)(data[this.field])));
			showThumbnails = XVar.Clone(this.container.pSet.showThumbnail((XVar)(this.field)));
			isExport = XVar.Clone((XVar)(this.container.pageType == Constants.PAGE_EXPORT)  || (XVar)(this.container.forExport != ""));
			if(XVar.Pack(showThumbnails))
			{
				zoomboxRand = XVar.Clone(MVCFunctions.rand(new XVar(11111), new XVar(99999)));
			}
			foreach (KeyValuePair<XVar, dynamic> file in filesArray.GetEnumerator())
			{
				dynamic userFile = XVar.Array();
				userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(file.Value)));
				if(XVar.Pack(!(XVar)(isExport)))
				{
					value = MVCFunctions.Concat(value, (XVar.Pack(value != XVar.Pack("")) ? XVar.Pack("<br>") : XVar.Pack("")));
					if((XVar)((XVar)(showThumbnails)  && (XVar)(userFile["thumbnail_url"] != ""))  && (XVar)(CommonFunctions.CheckImageExtension((XVar)(file.Value["name"]))))
					{
						value = MVCFunctions.Concat(value, "<a target=_blank href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\" class='zoombox zgallery", zoomboxRand, "'><img  border='0'");
						if(XVar.Pack(this.is508))
						{
							value = MVCFunctions.Concat(value, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
						}
						value = MVCFunctions.Concat(value, " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.GetRootPathForResources((XVar)(userFile["thumbnail_url"])))), "\" /></a>");
					}
					else
					{
						if(XVar.Pack(this.container.pSet.showIcon((XVar)(this.field))))
						{
							value = MVCFunctions.Concat(value, "<a href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\"><img style=\"vertical-align: middle;\" src=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.Concat("images/icons/", getFileIconByType((XVar)(file.Value["name"]), (XVar)(file.Value["type"]))))), "\" /></a>");
						}
					}
				}
				if(XVar.Pack(this.container.pSet.showCustomExpr((XVar)(this.field))))
				{
					value = MVCFunctions.Concat(value, MVCFunctions.fileCustomExpression((XVar)(file.Value), (XVar)(data), (XVar)(this.field), (XVar)(this.container.pageType)));
				}
				else
				{
					if(XVar.Pack(isExport))
					{
						value = MVCFunctions.Concat(value, (XVar.Pack(value != XVar.Pack("")) ? XVar.Pack(", ") : XVar.Pack("")), file.Value["usrName"]);
					}
					else
					{
						dynamic label = null;
						if((XVar)((XVar)((XVar)(showThumbnails)  && (XVar)(userFile["thumbnail_url"] != ""))  && (XVar)(CommonFunctions.CheckImageExtension((XVar)(file.Value["name"]))))  && (XVar)(value != XVar.Pack("")))
						{
							value = MVCFunctions.Concat(value, "<br />");
						}
						label = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)((XVar.Pack(file.Value["usrName"] != "") ? XVar.Pack(file.Value["usrName"]) : XVar.Pack(file.Value["name"])))));
						if(XVar.Pack(this.searchHighlight))
						{
							label = XVar.Clone(highlightSearchWord((XVar)(label), new XVar(true), new XVar("")));
						}
						value = MVCFunctions.Concat(value, "<a dir=\"LTR\" href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">", label, "</a>");
					}
				}
				if(XVar.Pack(this.container.pSet.showFileSize((XVar)(this.field))))
				{
					dynamic fileSizeAndUnit = XVar.Array();
					fileSizeAndUnit = XVar.Clone(getFileSizeAndUnits((XVar)(file.Value["size"])));
					value = MVCFunctions.Concat(value, " ", CommonFunctions.str_format_number((XVar)((XVar)Math.Round((double)(fileSizeAndUnit["size"]), 2))), " ", this.sizeUnits[fileSizeAndUnit["unitIndex"]]);
				}
			}
			return value;
		}
		public virtual XVar getFileSizeAndUnits(dynamic _param_size, dynamic _param_deepLevel = null)
		{
			#region default values
			if(_param_deepLevel as Object == null) _param_deepLevel = new XVar(0);
			#endregion

			#region pass-by-value parameters
			dynamic size = XVar.Clone(_param_size);
			dynamic deepLevel = XVar.Clone(_param_deepLevel);
			#endregion

			dynamic shrinkedSize = null;
			shrinkedSize = XVar.Clone(size / 1024);
			if((XVar)(1024 < shrinkedSize)  && (XVar)(deepLevel < MVCFunctions.count(this.sizeUnits) - 1))
			{
				return getFileSizeAndUnits((XVar)(shrinkedSize), (XVar)(deepLevel + 1));
			}
			return new XVar("size", shrinkedSize, "unitIndex", deepLevel);
		}
		public virtual XVar getFileIconByType(dynamic _param_file_name, dynamic _param_fileType)
		{
			#region pass-by-value parameters
			dynamic file_name = XVar.Clone(_param_file_name);
			dynamic fileType = XVar.Clone(_param_fileType);
			#endregion

			dynamic fileName = null;
			fileName = new XVar("no_image.gif");
			if(fileType == XVar.Pack(""))
			{
				fileType = XVar.Clone(CommonFunctions.getContentTypeByExtension((XVar)(MVCFunctions.substr((XVar)(file_name), (XVar)(MVCFunctions.strrpos((XVar)(file_name), new XVar(".")))))));
			}
			return CommonFunctions.getIconByFileType((XVar)(fileType), (XVar)(file_name));
		}
	}
}
