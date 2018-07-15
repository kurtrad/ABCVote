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
	public partial class FileFieldSingle : EditControl
	{
		protected static bool skipFileFieldSingleCtor = false;
		public FileFieldSingle(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipFileFieldSingleCtor)
			{
				skipFileFieldSingleCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.format = new XVar(Constants.EDIT_FORMAT_FILE);
		}
		public override XVar addJSFiles()
		{
			this.pageObject.AddJSFile(new XVar("include/zoombox/zoombox.js"));
			return null;
		}
		public override XVar addCSSFiles()
		{
			this.pageObject.AddCSSFile(new XVar("include/zoombox/zoombox.css"));
			return null;
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

			dynamic disp = null, filename_size = null, strfilename = null, strtype = null;
			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			if((XVar)(this.pageObject.pageType == Constants.PAGE_SEARCH)  || (XVar)(this.pageObject.pageType == Constants.PAGE_LIST))
			{
				dynamic classString = null;
				classString = new XVar("");
				if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					classString = new XVar(" class=\"form-control\"");
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" ", classString, this.inputStyle, " type=\"text\" ", (XVar.Pack(mode == Constants.MODE_SEARCH) ? XVar.Pack("autocomplete=\"off\" ") : XVar.Pack("")), (XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508 == true)) ? XVar.Pack(MVCFunctions.Concat("alt=\"", this.strLabel, "\" ")) : XVar.Pack("")), "name=\"", this.cfield, "\" ", this.pageObject.pSetEdit.getEditParams((XVar)(this.field)), " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\">"));
				buildControlEnd((XVar)(validate), (XVar)(mode));
				return null;
			}
			if(mode == Constants.MODE_SEARCH)
			{
				this.format = new XVar("");
			}
			disp = new XVar("");
			strfilename = new XVar("");
			filename_size = new XVar(30);
			if(XVar.Pack(this.pageObject.pSetEdit.isUseTimestamp((XVar)(this.field))))
			{
				filename_size = new XVar(50);
			}
			if((XVar)(mode == Constants.MODE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_EDIT))
			{
				dynamic fileData = XVar.Array(), fileName = null, newUploaderFilesData = XVar.Array(), newUploaderWasUsed = null, viewFormat = null;
				newUploaderFilesData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value)));
				newUploaderWasUsed = XVar.Clone((XVar)(MVCFunctions.is_array((XVar)(newUploaderFilesData)))  && (XVar)(0 < MVCFunctions.count(newUploaderFilesData)));
				fileData = XVar.Clone((XVar.Pack(newUploaderWasUsed) ? XVar.Pack(newUploaderFilesData[0]) : XVar.Pack(XVar.Array())));
				fileName = XVar.Clone((XVar.Pack(newUploaderWasUsed) ? XVar.Pack(fileData["usrName"]) : XVar.Pack(value)));
				viewFormat = XVar.Clone(this.pageObject.pSetEdit.getViewFormat((XVar)(this.field)));
				if((XVar)(viewFormat == Constants.FORMAT_FILE)  || (XVar)(viewFormat == Constants.FORMAT_FILE_IMAGE))
				{
					disp = XVar.Clone(MVCFunctions.Concat(getFileOrImageMarkup((XVar)(value), (XVar)(fileName), (XVar)(newUploaderWasUsed), (XVar)(fileData)), "<br />"));
				}
				strfilename = XVar.Clone(MVCFunctions.Concat("<input type=hidden name=\"filenameHidden_", this.cfieldname, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(fileName)), "\"><br>", "Filename", "&nbsp;&nbsp;<input type=\"text\" style=\"background-color:gainsboro\" disabled id=\"filename_", this.cfieldname, "\" name=\"filename_", this.cfieldname, "\" size=\"", filename_size, "\" maxlength=\"100\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(fileName)), "\">"));
				strtype = XVar.Clone(MVCFunctions.Concat("<br><input id=\"", this.ctype, "_keep\" type=\"Radio\" name=\"", this.ctype, "\" value=\"upload0\" checked class=\"rnr-uploadtype\">", "Keep"));
				if((XVar)((XVar)(MVCFunctions.strlen((XVar)(value)))  || (XVar)(mode == Constants.MODE_INLINE_EDIT))  && (XVar)(!(XVar)(this.pageObject.pSetEdit.isRequired((XVar)(this.field)))))
				{
					strtype = MVCFunctions.Concat(strtype, "<input id=\"", this.ctype, "_delete\" type=\"Radio\" name=\"", this.ctype, "\" value=\"upload1\" class=\"rnr-uploadtype\">", "Delete");
				}
				strtype = MVCFunctions.Concat(strtype, "<input id=\"", this.ctype, "_update\" type=\"Radio\" name=\"", this.ctype, "\" value=\"upload2\" class=\"rnr-uploadtype\">", "Update");
			}
			else
			{
				strtype = XVar.Clone(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"upload2\">"));
				strfilename = XVar.Clone(MVCFunctions.Concat("<br>", "Filename", "&nbsp;&nbsp;<input type=\"text\" id=\"filename_", this.cfieldname, "\" name=\"filename_", this.cfieldname, "\" size=\"", filename_size, "\" maxlength=\"100\">"));
			}
			MVCFunctions.Echo(MVCFunctions.Concat(disp, strtype));
			if((XVar)(mode == Constants.MODE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_EDIT))
			{
				MVCFunctions.Echo("<br>");
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"File\" id=\"", this.cfield, "\" ", (XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508 == true)) ? XVar.Pack(MVCFunctions.Concat("alt=\"", this.strLabel, "\" ")) : XVar.Pack("")), " name=\"", this.cfield, "\" >", strfilename));
			MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"Hidden\" id=\"notempty_", this.cfieldname, "\" value=\"", (XVar.Pack(MVCFunctions.strlen((XVar)(value))) ? XVar.Pack(1) : XVar.Pack(0)), "\">"));
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		public virtual XVar getFileOrImageMarkup(dynamic _param_value, dynamic _param_fileName, dynamic _param_newUploaderWasUsed, dynamic _param_fileData)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic fileName = XVar.Clone(_param_fileName);
			dynamic newUploaderWasUsed = XVar.Clone(_param_newUploaderWasUsed);
			dynamic fileData = XVar.Clone(_param_fileData);
			#endregion

			dynamic altAttr = null, cachedValue = null, disp = null, filePath = null, finalFilePath = null, finalUploadFolder = null, imageValue = null, uploadFolder = null;
			cachedValue = XVar.Clone(value);
			if(XVar.Pack(newUploaderWasUsed))
			{
				finalFilePath = XVar.Clone(filePath = XVar.Clone(fileData["name"]));
			}
			else
			{
				uploadFolder = XVar.Clone(this.pageObject.pSetEdit.getUploadFolder((XVar)(this.field)));
				filePath = XVar.Clone(MVCFunctions.Concat(uploadFolder, value));
				finalUploadFolder = XVar.Clone(this.pageObject.pSetEdit.getFinalUploadFolder((XVar)(this.field)));
				finalFilePath = XVar.Clone(MVCFunctions.Concat(finalUploadFolder, value));
			}
			if(XVar.Pack(!(XVar)(CommonFunctions.CheckImageExtension((XVar)(fileName)))))
			{
				return MVCFunctions.Concat("<a target=\"_blank\" href=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(filePath)))), "\">", MVCFunctions.runner_htmlspecialchars((XVar)(fileName)), "</a>");
			}
			altAttr = XVar.Clone((XVar.Pack(this.is508) ? XVar.Pack(MVCFunctions.Concat(" alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(fileName)), "\"")) : XVar.Pack("")));
			if(XVar.Pack(!(XVar)(MVCFunctions.myfile_exists((XVar)(MVCFunctions.getabspath((XVar)(finalFilePath)))))))
			{
				filePath = new XVar("images/no_image.gif");
			}
			if(XVar.Pack(this.pageObject.pSetEdit.showThumbnail((XVar)(this.field))))
			{
				dynamic finalThumbPath = null, thumbPath = null;
				if(XVar.Pack(newUploaderWasUsed))
				{
					finalThumbPath = XVar.Clone(thumbPath = XVar.Clone(fileData["thumbnail"]));
				}
				else
				{
					dynamic thumbprefix = null;
					thumbprefix = XVar.Clone(this.pageObject.pSetEdit.getStrThumbnail((XVar)(this.field)));
					thumbPath = XVar.Clone(MVCFunctions.Concat(uploadFolder, thumbprefix, fileName));
					finalThumbPath = XVar.Clone(MVCFunctions.Concat(finalUploadFolder, thumbprefix, fileName));
				}
				if(MVCFunctions.substr((XVar)(thumbPath), new XVar(0), new XVar(7)) != "http://")
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.myfile_exists((XVar)(MVCFunctions.getabspath((XVar)(finalThumbPath)))))))
					{
						thumbPath = XVar.Clone(filePath);
					}
				}
				return MVCFunctions.Concat("<a target=\"_blank\" href=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(filePath)))), "\" class='zoombox zgallery'>", "<img", altAttr, " border=0 src=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(thumbPath)))), "\"></a>");
			}
			imageValue = XVar.Clone(filePath);
			if((XVar)(filePath != "images/no_image.gif")  && (XVar)(!(XVar)(newUploaderWasUsed)))
			{
				if(51200 < MVCFunctions.filesize((XVar)(MVCFunctions.Concat(finalUploadFolder, fileName))))
				{
					imageValue = new XVar("images/icons/jpg.png");
				}
			}
			disp = XVar.Clone(MVCFunctions.Concat("<img ", altAttr, "src=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(imageValue)))), "\" border=0>"));
			if(imageValue != "images/no_image.gif")
			{
				disp = XVar.Clone(MVCFunctions.Concat("<a target=\"_blank\" href=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(filePath)))), "\">", disp, "</a>"));
			}
			return disp;
		}
		public override XVar readWebValue(dynamic avalues, dynamic blobfields, dynamic _param_legacy1, dynamic _param_legacy2, dynamic filename_values)
		{
			#region pass-by-value parameters
			dynamic legacy1 = XVar.Clone(_param_legacy1);
			dynamic legacy2 = XVar.Clone(_param_legacy2);
			#endregion

			getPostValueAndType();
			if(XVar.Pack(MVCFunctions.FieldSubmitted((XVar)(MVCFunctions.Concat(this.goodFieldName, "_", this.id)))))
			{
				dynamic fileNameForPrepareFunc = null;
				fileNameForPrepareFunc = XVar.Clone(CommonFunctions.securityCheckFileName((XVar)(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("filename_", this.goodFieldName, "_", this.id))))));
				if(this.pageObject.pageType != Constants.PAGE_EDIT)
				{
					this.webValue = XVar.Clone(MVCFunctions.prepare_upload((XVar)(this.field), new XVar("upload2"), (XVar)(fileNameForPrepareFunc), (XVar)(fileNameForPrepareFunc), new XVar(""), (XVar)(this.id), (XVar)(this.pageObject)));
				}
				else
				{
					if(MVCFunctions.substr((XVar)(this.webType), new XVar(0), new XVar(4)) == "file")
					{
						dynamic prepearedFile = XVar.Array();
						prepearedFile = XVar.Clone(MVCFunctions.prepare_file((XVar)(this.webValue), (XVar)(this.field), (XVar)(this.webType), (XVar)(fileNameForPrepareFunc), (XVar)(this.id)));
						if(!XVar.Equals(XVar.Pack(prepearedFile), XVar.Pack(false)))
						{
							dynamic filename = null;
							this.webValue = XVar.Clone(prepearedFile["value"]);
							filename = XVar.Clone(prepearedFile["filename"]);
						}
						else
						{
							this.webValue = new XVar(false);
						}
					}
					else
					{
						if(MVCFunctions.substr((XVar)(this.webType), new XVar(0), new XVar(6)) == "upload")
						{
							if(XVar.Pack(fileNameForPrepareFunc))
							{
								this.webValue = XVar.Clone(fileNameForPrepareFunc);
							}
							if(this.webType == "upload1")
							{
								dynamic oldValues = XVar.Array();
								oldValues = XVar.Clone(this.pageObject.getOldRecordData());
								fileNameForPrepareFunc = XVar.Clone(oldValues[this.field]);
							}
							this.webValue = XVar.Clone(MVCFunctions.prepare_upload((XVar)(this.field), (XVar)(this.webType), (XVar)(fileNameForPrepareFunc), (XVar)(this.webValue), new XVar(""), (XVar)(this.id), (XVar)(this.pageObject)));
						}
					}
				}
			}
			else
			{
				this.webValue = new XVar(false);
			}
			if(XVar.Pack(!(XVar)(XVar.Equals(XVar.Pack(this.webValue), XVar.Pack(false)))))
			{
				if((XVar)(this.webValue)  && (XVar)(this.pageObject.pSetEdit.getCreateThumbnail((XVar)(this.field))))
				{
					dynamic contents = null, ext = null, thumb = null;
					contents = XVar.Clone(MVCFunctions.GetUploadedFileContents((XVar)(MVCFunctions.Concat("value_", this.goodFieldName, "_", this.id))));
					ext = XVar.Clone(CommonFunctions.CheckImageExtension((XVar)(MVCFunctions.GetUploadedFileName((XVar)(MVCFunctions.Concat("value_", this.goodFieldName, "_", this.id))))));
					thumb = XVar.Clone(MVCFunctions.CreateThumbnail((XVar)(contents), (XVar)(this.pageObject.pSetEdit.getThumbnailSize((XVar)(this.field))), (XVar)(ext)));
					this.pageObject.filesToSave.InitAndSetArrayItem(new SaveFile((XVar)(thumb), (XVar)(MVCFunctions.Concat(this.pageObject.pSetEdit.getStrThumbnail((XVar)(this.field)), this.webValue)), (XVar)(this.pageObject.pSetEdit.getUploadFolder((XVar)(this.field))), (XVar)(this.pageObject.pSetEdit.isAbsolute((XVar)(this.field)))), null);
				}
				avalues.InitAndSetArrayItem(this.webValue, this.field);
			}
			return null;
		}
		public override XVar makeWidthStyle(dynamic _param_widthPx)
		{
			#region pass-by-value parameters
			dynamic widthPx = XVar.Clone(_param_widthPx);
			#endregion

			if(XVar.Pack(0) == widthPx)
			{
				return "";
			}
			return MVCFunctions.Concat("min-width: ", widthPx, "px");
		}
	}
}
