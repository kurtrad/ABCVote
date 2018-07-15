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
	public partial class ViewAudioFileField : ViewFileField
	{
		protected static bool skipViewAudioFileFieldCtor = false;
		public ViewAudioFileField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic fileName = null, value = null;
			value = new XVar("");
			fileName = XVar.Clone(data[this.field]);
			if(XVar.Pack(MVCFunctions.strlen((XVar)(fileName))))
			{
				dynamic fieldIsUrl = null, filesArray = XVar.Array(), title = null, titleField = null;
				fieldIsUrl = XVar.Clone(this.container.pSet.isVideoUrlField((XVar)(this.field)));
				if(XVar.Pack(!(XVar)(fieldIsUrl)))
				{
					this.upload_handler.tkeys = XVar.Clone(keylink);
					filesArray = XVar.Clone(getFilesArray((XVar)(fileName)));
				}
				else
				{
					filesArray = XVar.Clone(new XVar(0, fileName));
				}
				title = new XVar("");
				titleField = XVar.Clone(this.container.pSet.getAudioTitleField((XVar)(this.field)));
				if(XVar.Pack(titleField))
				{
					title = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(data[titleField])));
				}
				foreach (KeyValuePair<XVar, dynamic> file in filesArray.GetEnumerator())
				{
					dynamic href = null, link = null;
					if((XVar)((XVar)(this.container.pageType == Constants.PAGE_EXPORT)  || (XVar)(this.container.pageType == Constants.PAGE_PRINT))  || (XVar)(this.container.forExport != ""))
					{
						if(value != XVar.Pack(""))
						{
							value = MVCFunctions.Concat(value, ", ");
						}
						value = MVCFunctions.Concat(value, (XVar.Pack(fieldIsUrl) ? XVar.Pack(file.Value) : XVar.Pack(file.Value["usrName"])));
						continue;
					}
					if(XVar.Pack(!(XVar)(fieldIsUrl)))
					{
						if(XVar.Pack(!(XVar)(MVCFunctions.file_exists((XVar)(MVCFunctions.getabspath((XVar)(file.Value["name"])))))))
						{
							continue;
						}
					}
					if(XVar.Pack(fieldIsUrl))
					{
						href = XVar.Clone(file.Value);
					}
					else
					{
						dynamic userFile = XVar.Array();
						userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(file.Value)));
						href = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])));
						if((XVar)(!(XVar)(title))  || (XVar)(!(XVar)(titleField)))
						{
							title = XVar.Clone(userFile["name"]);
						}
					}
					link = XVar.Clone(MVCFunctions.Concat("<a title=\"", title, "\" href=\"", href, "\">", title, "</a>"));
					value = MVCFunctions.Concat(value, (XVar.Pack(value == XVar.Pack("")) ? XVar.Pack("") : XVar.Pack("<br />")), "<audio controls preload=\"none\" src=\"", href, "\">", link, "</audio>");
				}
			}
			return value;
		}
	}
}
