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
	public partial class FileField : EditControl
	{
		public dynamic upload_handler = XVar.Pack(null);
		public dynamic formStamp = XVar.Pack("");
		protected static bool skipFileFieldCtor = false;
		public FileField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipFileFieldCtor)
			{
				skipFileFieldCtor = false;
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
			if((XVar)((XVar)(this.pageObject.pageType == Constants.PAGE_ADD)  || (XVar)(this.pageObject.pageType == Constants.PAGE_EDIT))  || (XVar)(this.pageObject.pageType == Constants.PAGE_REGISTER))
			{
				this.pageObject.AddJSFile(new XVar("include/mupload.js"));
				this.pageObject.AddJSFile(new XVar("include/zoombox/zoombox.js"));
			}
			return null;
		}
		public override XVar addCSSFiles()
		{
			if((XVar)((XVar)(this.pageObject.pageType == Constants.PAGE_ADD)  || (XVar)(this.pageObject.pageType == Constants.PAGE_EDIT))  || (XVar)(this.pageObject.pageType == Constants.PAGE_REGISTER))
			{
				this.pageObject.AddCSSFile(new XVar("include/zoombox/zoombox.css"));
			}
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

			dynamic filesArray = XVar.Array(), jsonValue = null, multiple = null, userFilesArray = XVar.Array();
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
			this.formStamp = XVar.Clone(CommonFunctions.generatePassword(new XVar(15)));
			initUploadHandler();
			this.upload_handler.formStamp = XVar.Clone(this.formStamp);
			filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value)));
			if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
			{
				if(XVar.Pack(!(XVar)(value)))
				{
					jsonValue = new XVar("[]");
				}
				else
				{
					dynamic uploadedFile = null;
					uploadedFile = XVar.Clone(this.upload_handler.get_file_object((XVar)(value)));
					if(XVar.Pack(uploadedFile == null))
					{
						filesArray = XVar.Clone(XVar.Array());
					}
					else
					{
						filesArray = XVar.Clone(new XVar(0, MVCFunctions.my_json_decode((XVar)(MVCFunctions.my_json_encode((XVar)(uploadedFile))))));
					}
				}
			}
			if(this.pageObject.pageType == Constants.PAGE_EDIT)
			{
				if(0 < MVCFunctions.count(this.pageObject.keys))
				{
					dynamic i = null;
					i = new XVar(1);
					foreach (KeyValuePair<XVar, dynamic> keyValue in this.pageObject.keys.GetEnumerator())
					{
						this.upload_handler.tkeys = MVCFunctions.Concat(this.upload_handler.tkeys, "&key", i, "=", MVCFunctions.RawUrlEncode((XVar)(keyValue.Value)));
						i++;
					}
				}
			}
			XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)] = XVar.Array();
			userFilesArray = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.is_array((XVar)(filesArray))))
			{
				foreach (KeyValuePair<XVar, dynamic> file in filesArray.GetEnumerator())
				{
					dynamic sessionArray = XVar.Array(), userFile = XVar.Array();
					sessionArray = XVar.Clone(XVar.Array());
					sessionArray.InitAndSetArrayItem(file.Value, "file");
					sessionArray.InitAndSetArrayItem(true, "fromDB");
					sessionArray.InitAndSetArrayItem(false, "deleted");
					XSession.Session.InitAndSetArrayItem(sessionArray, MVCFunctions.Concat("mupload_", this.formStamp), file.Value["usrName"]);
					userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(file.Value)));
					if(XVar.Pack(!(XVar)(userFile["isImg"])))
					{
						userFile.InitAndSetArrayItem(true, "isImg");
						userFile.InitAndSetArrayItem(true, "isIco");
						userFile.InitAndSetArrayItem(MVCFunctions.Concat(userFile["url"], "&icon=1"), "thumbnail_url");
					}
					userFilesArray.InitAndSetArrayItem(userFile, null);
				}
			}
			jsonValue = XVar.Clone(MVCFunctions.my_json_encode((XVar)(userFilesArray)));
			multiple = new XVar("");
			if((XVar)(!(XVar)(CommonFunctions.isIOS()))  && (XVar)(this.pageObject.pSetEdit.getMaxNumberOfFiles((XVar)(this.field)) != 1))
			{
				multiple = new XVar("multiple ");
			}
			MVCFunctions.Echo(MVCFunctions.Concat("\r\n <!-- The file upload form used as target for the file upload widget -->\r\n    <form id=\"fileupload_", this.cfieldname, "\" action=\"", MVCFunctions.GetTableLink(new XVar("mfhandler")), "\" method=\"POST\" enctype=\"multipart/form-data\">\r\n    \r\n    <input type=\"hidden\" name=\"formStamp_", this.cfieldname, "\" id=\"formStamp_", this.cfieldname, "\" value=\"", this.formStamp, "\" />\r\n    <input type=\"hidden\" name=\"_action\" value=\"POST\" />\r\n    <input type=\"hidden\" id=\"value_", this.cfieldname, "\" name=\"value_", this.cfieldname, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(jsonValue)), "\" />\r\n    \r\n    <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->\r\n        <div class=\"fileupload-buttonbar\">\r\n            <div class=\"span7\">\r\n                <!-- The fileinput-button span is used to style the file input field as button -->\r\n \t\t\t\t<SPAN class=\"btn btn-primary btn-sm fileinput-button\">\r\n\t\t\t\t\t<A class=\"rnr-button filesUpload button\" href=\"#\" ><input class=\"fileinput-button-input\" type=\"file\" name=\"files[]\" value=\"", "Add files", "\" ", multiple, " />", "Add files", "</A>\r\n\t\t\t\t</SPAN>", "\r\n                \r\n            </div>\r\n            <!-- The global progress information -->\r\n            <div class=\"fileupload-progress fade\">\r\n                <!-- The global progress bar -->\r\n                <div class=\"progress\" role=\"progressbar\" aria-valuemin=\"0\" aria-valuenow=\"0\" aria-valuemax=\"100\">\r\n                    <div style=\"width:0;\" class=\"bar progress-bar progress-bar-info progress-bar-striped active\"  ></div>\r\n                </div>\r\n                <!-- The extended global progress information -->\r\n                <div class=\"progress-extended\">&nbsp;</div>\r\n            </div>\r\n        </div>\r\n        <!-- The loading indicator is shown during file processing -->\r\n        <div class=\"fileupload-loading\"></div>\r\n        <!-- The table listing the files available for upload/download -->\r\n        <table class=\"mupload-files\"><tbody class=\"files\"></tbody></table>\r\n    </form>\r\n    "));
			if(XVar.Pack(!(XVar)(this.container.globalVals.KeyExists("muploadTemplateIncluded"))))
			{
				MVCFunctions.Echo(MVCFunctions.Concat("<script type=\"text/x-tmpl\" id=\"template-download\">{% for (var i=0, file; file=o.files[i]; i++) { %}\r\n    <tr class=\"template-download fade\">\r\n        {% if (file.error) { %}\r\n            <td></td>\r\n            <td class=\"name\"><span class=\"text-muted\">{%=file.name%}</span></td>\r\n            <td class=\"size\"><span class=\"text-muted\" dir=\"LTR\">{%=o.formatFileSize(file.size)%}</span></td>\r\n            <td colspan=2 class=\"error\"><span class=\"text-danger rnr-error\">", "", " {%=locale.fileupload.errors[file.error] || file.error%}</span></td>\r\n        {% } else { %}\r\n            <td class=\"preview\">{% if (file.thumbnail_url) { %}\r\n                <a href=\"{%=file.url%}\" title=\"{%=file.name%}\" rel=\"gallery\" download=\"{%=file.name%}\" \r\n                \t{% if (!file.isIco) { %} class=\"zoombox zgallery\" {% } %} \r\n                \t><img class=\"mupload-preview-img\" src=\"{%=file.thumbnail_url%}&src=1\"></a>\r\n            {% } else { %}\r\n            \t{% if (file.isImg) { %}\r\n            \t\t<a href=\"{%=file.url%}&nodisp=1\" title=\"{%=file.name%}\" rel=\"gallery\" download=\"{%=file.name%}\" class=\"zoombox zgallery\"><img class=\"mupload-preview-img\" src=\"{%=file.url%}&src=1\"></a>\r\n            \t{% } %}\r\n            {% } %}</td>\r\n            <td class=\"name\">\r\n                <a href=\"{%=file.url%}\" title=\"{%=file.name%}\" rel=\"{%=file.thumbnail_url&&'gallery'%}\" download=\"{%=file.name%}\">{%=file.name%}</a>\r\n            </td>\r\n            <td class=\"size\"><span dir=\"LTR\">{%=o.formatFileSize(file.size)%}</span></td>\r\n\t\t\t<td></td>\r\n\t\t\t<td class=\"delete\">\r\n\t\t\t\t{% if (!file.error) { %}\r\n\t\t\t\t<SPAN class=\"btn btn-xs btn-default delete\" data-type=\"{%=file.delete_type%}\" data-url=\"{%=file.delete_url%}\" data-name=\"{%=file.name%}\">\r\n\t\t\t\t\t<A href=\"#\" >", "Delete", "</A>\r\n\t\t\t\t\t</SPAN>\r\n\t\t\t\t{% } %}\r\n\t\t\t</td>\r\n        {% } %}\r\n    </tr>\r\n{% } %}\r\n</script>\r\n<script type=\"text/x-tmpl\" id=\"template-upload\">{% for (var i=0, file; file=o.files[i]; i++) { %}\r\n    <tr class=\"template-upload fade\">\r\n        <td class=\"preview\"><span class=\"fade\"></span></td>\r\n        {% if (file.error) { %}\r\n\t\t\t<td class=\"name\"><span class=\"text-muted\">{%=file.name%}</span></td>\r\n\t\t\t<td class=\"size\"><span class=\"text-muted\">{%=o.formatFileSize(file.size)%}</span></td>\r\n            <td class=\"error\" colspan=\"2\"><span class=\"text-danger rnr-error\">", "", " {%=locale.fileupload.errors[file.error] || file.error%}</span></td>\r\n        {% } else if (o.files.valid && !i) { %}\r\n\t\t\t<td class=\"name\"><span>{%=file.name%}</span></td>\r\n\t\t\t<td class=\"size\"><span>{%=o.formatFileSize(file.size)%}</span></td>\r\n            <td>\r\n                <div class=\"progress progress-success progress-striped active\" role=\"progressbar\" aria-valuemin=\"0\" \r\n                \taria-valuemax=\"100\" aria-valuenow=\"0\"><div class=\"progress-bar bar\" style=\"width:0;\"></div></div>\r\n            </td>\r\n        {% } else { %}\r\n            <td></td>\r\n        {% } %}\r\n        <td class=\"cancel\">{% if (!i) { %}\r\n        \t{% if (!file.error) { %}\r\n        \t<SPAN class=\"btn btn-default btn-xs\">\r\n\t\t\t\t<A href=\"#\" >", "Cancel", "</A>\r\n\t\t\t\t</SPAN>\r\n\t\t\t{% } %}\r\n        {% } %}</td>\r\n    </tr>\r\n{% } %}</script>"));
				this.container.globalVals.InitAndSetArrayItem(true, "muploadTemplateIncluded");
			}
			buildControlEnd((XVar)(validate), (XVar)(mode));
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
		public virtual XVar initUploadHandler()
		{
			if(XVar.Pack(this.upload_handler == null))
			{
				this.upload_handler = XVar.Clone(new UploadHandler((XVar)(CommonFunctions.getOptionsForMultiUpload((XVar)(this.pageObject.pSet), (XVar)(this.field)))));
				this.upload_handler.pSet = XVar.Clone(this.pageObject.pSetEdit);
				this.upload_handler.field = XVar.Clone(this.field);
				this.upload_handler.table = XVar.Clone(this.pageObject.tName);
				this.upload_handler.pageType = XVar.Clone(this.pageObject.pageType);
			}
			return null;
		}
		public override XVar readWebValue(dynamic avalues, dynamic blobfields, dynamic _param_legacy1, dynamic _param_legacy2, dynamic filename_values)
		{
			#region pass-by-value parameters
			dynamic legacy1 = XVar.Clone(_param_legacy1);
			dynamic legacy2 = XVar.Clone(_param_legacy2);
			#endregion

			getPostValueAndType();
			this.formStamp = XVar.Clone(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("formStamp_", this.goodFieldName, "_", this.id))));
			if((XVar)(MVCFunctions.FieldSubmitted((XVar)(MVCFunctions.Concat(this.goodFieldName, "_", this.id))))  && (XVar)(this.formStamp != ""))
			{
				dynamic filesArray = XVar.Array();
				filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(this.webValue)));
				if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
				{
					this.webValue = new XVar("");
				}
				else
				{
					dynamic result = XVar.Array(), searchStr = null, uploadDir = null;
					if(0 < MVCFunctions.count(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)]))
					{
						foreach (KeyValuePair<XVar, dynamic> fileArray in XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].GetEnumerator())
						{
							fileArray.Value.InitAndSetArrayItem(true, "deleted");
						}
					}
					result = XVar.Clone(XVar.Array());
					uploadDir = XVar.Clone(this.pageObject.pSetEdit.getLinkPrefix((XVar)(this.field)));
					searchStr = new XVar("");
					foreach (KeyValuePair<XVar, dynamic> file in filesArray.GetEnumerator())
					{
						if(XVar.Pack(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].KeyExists(file.Value["name"])))
						{
							dynamic sessionFile = XVar.Array();
							sessionFile = XVar.Clone(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)][file.Value["name"]]["file"]);
							searchStr = MVCFunctions.Concat(searchStr, file.Value["name"], ",!");
							result.InitAndSetArrayItem(new XVar("name", sessionFile["name"], "usrName", file.Value["name"], "size", sessionFile["size"], "type", sessionFile["type"]), null);
							if((XVar)(this.pageObject.pSetEdit.getCreateThumbnail((XVar)(this.field)))  && (XVar)(sessionFile["thumbnail"] != ""))
							{
								dynamic lastIndex = null;
								lastIndex = XVar.Clone(MVCFunctions.count(result) - 1);
								result.InitAndSetArrayItem(sessionFile["thumbnail"], lastIndex, "thumbnail");
								result.InitAndSetArrayItem(sessionFile["thumbnail_type"], lastIndex, "thumbnail_type");
								result.InitAndSetArrayItem(sessionFile["thumbnail_size"], lastIndex, "thumbnail_size");
							}
							XSession.Session.InitAndSetArrayItem(false, MVCFunctions.Concat("mupload_", this.formStamp), file.Value["name"], "deleted");
						}
					}
					if(0 < MVCFunctions.count(result))
					{
						result.InitAndSetArrayItem(MVCFunctions.Concat(searchStr, ":sStrEnd"), 0, "searchStr");
						this.webValue = XVar.Clone(MVCFunctions.my_json_encode_unescaped_unicode((XVar)(result)));
					}
					else
					{
						this.webValue = new XVar("");
					}
				}
			}
			else
			{
				this.webValue = new XVar(false);
			}
			if(XVar.Pack(!(XVar)(XVar.Equals(XVar.Pack(this.webValue), XVar.Pack(false)))))
			{
				if(this.connection.dbType == Constants.nDATABASE_Informix)
				{
					if(XVar.Pack(CommonFunctions.IsTextType((XVar)(this.pageObject.pSetEdit.getFieldType((XVar)(this.field))))))
					{
						blobfields.InitAndSetArrayItem(this.field, null);
					}
				}
				avalues.InitAndSetArrayItem(this.webValue, this.field);
			}
			return null;
		}
		public virtual XVar showDBValue(dynamic _param_value, dynamic _param_keyLink)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic keyLink = XVar.Clone(_param_keyLink);
			#endregion

			dynamic filesArray = XVar.Array(), imageValue = null, keylink = null;
			imageValue = new XVar("");
			initUploadHandler();
			this.upload_handler.tkeys = XVar.Clone(keylink);
			filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value)));
			if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
			{
				if(value == XVar.Pack(""))
				{
					filesArray = XVar.Clone(XVar.Array());
				}
				else
				{
					dynamic uploadedFile = null;
					uploadedFile = XVar.Clone(this.upload_handler.get_file_object((XVar)(value)));
					if(XVar.Pack(uploadedFile == null))
					{
						filesArray = XVar.Clone(XVar.Array());
					}
					else
					{
						filesArray = XVar.Clone(new XVar(0, uploadedFile));
					}
				}
			}
			foreach (KeyValuePair<XVar, dynamic> imageFile in filesArray.GetEnumerator())
			{
				dynamic userFile = XVar.Array();
				userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(imageFile.Value)));
				if(this.pageObject.pSetEdit.getViewFormat((XVar)(this.field)) == Constants.FORMAT_FILE)
				{
					imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imageValue != XVar.Pack("")) ? XVar.Pack("<br>") : XVar.Pack("")));
					imageValue = MVCFunctions.Concat(imageValue, "<a href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">", MVCFunctions.runner_htmlspecialchars((XVar)((XVar.Pack(imageFile.Value["usrName"] != "") ? XVar.Pack(imageFile.Value["usrName"]) : XVar.Pack(imageFile.Value["name"])))), "</a>");
				}
				else
				{
					if(XVar.Pack(CommonFunctions.CheckImageExtension((XVar)(imageFile.Value["name"]))))
					{
						dynamic imgHeight = null, imgWidth = null;
						imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imageValue != XVar.Pack("")) ? XVar.Pack("<br>") : XVar.Pack("")));
						if(XVar.Pack(this.pageObject.pSetEdit.showThumbnail((XVar)(this.field))))
						{
							dynamic thumbname = null;
							thumbname = XVar.Clone(userFile["thumbnail_url"]);
							imageValue = MVCFunctions.Concat(imageValue, "<a target=_blank");
							imageValue = MVCFunctions.Concat(imageValue, " href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\" class='zoombox'>");
							imageValue = MVCFunctions.Concat(imageValue, "<img");
							if((XVar)(thumbname == XVar.Pack(""))  || (XVar)(imageFile.Value["name"] == imageFile.Value["thumbnail"]))
							{
								imgWidth = XVar.Clone(this.pageObject.pSetEdit.getImageWidth((XVar)(this.field)));
								imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imgWidth) ? XVar.Pack(MVCFunctions.Concat(" width=", imgWidth)) : XVar.Pack("")));
								imgHeight = XVar.Clone(this.pageObject.pSetEdit.getImageHeight((XVar)(this.field)));
								imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imgHeight) ? XVar.Pack(MVCFunctions.Concat(" height=", imgHeight)) : XVar.Pack("")));
							}
							imageValue = MVCFunctions.Concat(imageValue, " border=0");
							if(XVar.Pack(this.is508))
							{
								imageValue = MVCFunctions.Concat(imageValue, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
							}
							imageValue = MVCFunctions.Concat(imageValue, " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["thumbnail_url"])), "\"></a>");
						}
						else
						{
							imageValue = MVCFunctions.Concat(imageValue, "<img");
							imgWidth = XVar.Clone(this.pageObject.pSetEdit.getImageWidth((XVar)(this.field)));
							imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imgWidth) ? XVar.Pack(MVCFunctions.Concat(" width=", imgWidth)) : XVar.Pack("")));
							imgHeight = XVar.Clone(this.pageObject.pSetEdit.getImageHeight((XVar)(this.field)));
							imageValue = MVCFunctions.Concat(imageValue, (XVar.Pack(imgHeight) ? XVar.Pack(MVCFunctions.Concat(" height=", imgHeight)) : XVar.Pack("")));
							imageValue = MVCFunctions.Concat(imageValue, " border=0");
							if(XVar.Pack(this.is508))
							{
								imageValue = MVCFunctions.Concat(imageValue, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
							}
							imageValue = MVCFunctions.Concat(imageValue, " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">");
						}
					}
				}
			}
			return imageValue;
		}
		public override XVar SQLWhere(dynamic _param_SearchFor, dynamic _param_strSearchOption, dynamic _param_SearchFor2, dynamic _param_etype, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic SearchFor = XVar.Clone(_param_SearchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic SearchFor2 = XVar.Clone(_param_SearchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			dynamic baseResult = null, gstrField = null;
			baseResult = XVar.Clone(baseSQLWhere((XVar)(strSearchOption)));
			if(XVar.Equals(XVar.Pack(baseResult), XVar.Pack(false)))
			{
				return "";
			}
			if(baseResult != XVar.Pack(""))
			{
				return baseResult;
			}
			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(this.var_type))))
			{
				gstrField = XVar.Clone(getFieldSQLDecrypt());
				if((XVar)((XVar)(!(XVar)(this.btexttype))  && (XVar)(!(XVar)(this.pageObject.cipherer.isFieldPHPEncrypted((XVar)(this.field)))))  && (XVar)(this.pageObject.pSetEdit.getNCSearch()))
				{
					gstrField = XVar.Clone(this.connection.upper((XVar)(gstrField)));
				}
			}
			else
			{
				if((XVar)(strSearchOption == "Contains")  || (XVar)(strSearchOption == "Starts with"))
				{
					gstrField = XVar.Clone(this.connection.field2char((XVar)(getFieldSQLDecrypt()), (XVar)(this.var_type)));
				}
				else
				{
					gstrField = XVar.Clone(getFieldSQLDecrypt());
				}
			}
			if((XVar)(strSearchOption == "Contains")  || (XVar)(strSearchOption == "Starts with"))
			{
				SearchFor = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(SearchFor)));
			}
			if(strSearchOption == "Contains")
			{
				SearchFor = XVar.Clone(MVCFunctions.Concat("%", SearchFor, "%"));
			}
			else
			{
				if(strSearchOption == "Starts with")
				{
					SearchFor = XVar.Clone(MVCFunctions.Concat(SearchFor, "%"));
				}
			}
			if((XVar)((XVar)(strSearchOption == "Contains")  || (XVar)(strSearchOption == "Starts with"))  || (XVar)(strSearchOption == "Equals"))
			{
				return buildWhere((XVar)(gstrField), (XVar)(SearchFor), (XVar)(strSearchOption == "Equals"));
			}
			return "";
		}
		public virtual XVar buildWhere(dynamic _param_gstrField, dynamic _param_value, dynamic _param_equals = null)
		{
			#region default values
			if(_param_equals as Object == null) _param_equals = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic gstrField = XVar.Clone(_param_gstrField);
			dynamic value = XVar.Clone(_param_value);
			dynamic equals = XVar.Clone(_param_equals);
			#endregion

			dynamic likeVal = null, notLikeVal = null, testSymbols = null;
			likeVal = XVar.Clone(this.connection.prepareString((XVar)(MVCFunctions.Concat("%searchStr\":\"", value, ":sStrEnd\"%"))));
			notLikeVal = XVar.Clone(this.connection.prepareString((XVar)(value)));
			if((XVar)((XVar)(!(XVar)(this.btexttype))  && (XVar)(CommonFunctions.IsCharType((XVar)(this.var_type))))  && (XVar)(this.pageObject.pSetEdit.getNCSearch()))
			{
				likeVal = XVar.Clone(this.connection.upper((XVar)(likeVal)));
				notLikeVal = XVar.Clone(this.connection.upper((XVar)(notLikeVal)));
			}
			if(this.connection.dbType == Constants.nDATABASE_Access)
			{
				testSymbols = new XVar("'_{%}_'");
			}
			else
			{
				testSymbols = new XVar("'[{%'");
			}
			return MVCFunctions.Concat("((", gstrField, " ", this.var_like, " ", testSymbols, " and ", gstrField, " ", this.var_like, " ", likeVal, ") or (", gstrField, " not ", this.var_like, " ", testSymbols, " and ", gstrField, " ", (XVar.Pack(equals) ? XVar.Pack("=") : XVar.Pack(this.var_like)), " ", notLikeVal, "))");
		}
		public override XVar getSearchOptions(dynamic _param_selOpt, dynamic _param_not, dynamic _param_both)
		{
			#region pass-by-value parameters
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			dynamic isPHPEncripted = null, optionsArray = XVar.Array();
			optionsArray = XVar.Clone(XVar.Array());
			isPHPEncripted = XVar.Clone(this.pageObject.cipherer.isFieldPHPEncrypted((XVar)(this.field)));
			if(XVar.Pack(!(XVar)(isPHPEncripted)))
			{
				optionsArray.InitAndSetArrayItem(Constants.CONTAINS, null);
				optionsArray.InitAndSetArrayItem(Constants.EQUALS, null);
			}
			optionsArray.InitAndSetArrayItem(Constants.EMPTY_SEARCH, null);
			if(XVar.Pack(both))
			{
				if(XVar.Pack(!(XVar)(isPHPEncripted)))
				{
					optionsArray.InitAndSetArrayItem(Constants.NOT_CONTAINS, null);
					optionsArray.InitAndSetArrayItem(Constants.NOT_EQUALS, null);
				}
				optionsArray.InitAndSetArrayItem(Constants.NOT_EMPTY, null);
			}
			return buildSearchOptions((XVar)(optionsArray), (XVar)(selOpt), (XVar)(var_not), (XVar)(both));
		}
		public override XVar suggestValue(dynamic _param_value, dynamic _param_searchFor, dynamic var_response, dynamic row)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			#endregion

			dynamic filesArray = XVar.Array();
			if(XVar.Pack(!(XVar)(value)))
			{
				return null;
			}
			value = XVar.Clone(MVCFunctions.substr((XVar)(value), new XVar(1)));
			filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value)));
			if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
			{
				var_response.InitAndSetArrayItem(MVCFunctions.Concat("_", value), MVCFunctions.Concat("_", value));
			}
			else
			{
				dynamic i = null, pos = null;
				i = new XVar(0);
				for(;(XVar)(i < MVCFunctions.count(filesArray))  && (XVar)(MVCFunctions.count(var_response) < 10); i++)
				{
					if(XVar.Pack(this.pageObject.pSetEdit.getNCSearch()))
					{
						pos = XVar.Clone(MVCFunctions.stripos((XVar)(filesArray[i]["usrName"]), (XVar)(searchFor)));
					}
					else
					{
						pos = XVar.Clone(MVCFunctions.strpos((XVar)(filesArray[i]["usrName"]), (XVar)(searchFor)));
					}
					if(!XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
					{
						var_response.InitAndSetArrayItem(MVCFunctions.Concat("_", filesArray[i]["usrName"]), MVCFunctions.Concat("_", filesArray[i]["usrName"]));
					}
				}
			}
			return null;
		}
		public override XVar afterSuccessfulSave()
		{
			if(0 < MVCFunctions.count(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)]))
			{
				foreach (KeyValuePair<XVar, dynamic> fileArray in XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].GetEnumerator())
				{
					if(XVar.Equals(XVar.Pack(fileArray.Value["deleted"]), XVar.Pack(true)))
					{
						dynamic file_path = null;
						file_path = XVar.Clone(fileArray.Value["file"]["name"]);
						if(XVar.Pack(MVCFunctions.file_exists(file_path)))
						{
							MVCFunctions.unlink((XVar)(file_path));
						}
						if(fileArray.Value["file"]["thumbnail"] != "")
						{
							file_path = XVar.Clone(fileArray.Value["file"]["thumbnail"]);
							if(XVar.Pack(MVCFunctions.file_exists(file_path)))
							{
								MVCFunctions.unlink((XVar)(file_path));
							}
						}
					}
				}
			}
			XSession.Session.Remove(MVCFunctions.Concat("mupload_", this.formStamp));
			return null;
		}
		public override XVar getFieldValueCopy(dynamic _param_fieldValue)
		{
			#region pass-by-value parameters
			dynamic fieldValue = XVar.Clone(_param_fieldValue);
			#endregion

			dynamic absoluteUploadDirPath = null, filesData = XVar.Array(), uploadFolder = null;
			initUploadHandler();
			uploadFolder = XVar.Clone(this.pageObject.pSetEdit.getUploadFolder((XVar)(this.field)));
			absoluteUploadDirPath = XVar.Clone(this.pageObject.pSetEdit.getFinalUploadFolder((XVar)(this.field)));
			filesData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(fieldValue)));
			if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesData))))  || (XVar)(MVCFunctions.count(filesData) == 0))
			{
				return fieldValue;
			}
			foreach (KeyValuePair<XVar, dynamic> fileData in filesData.GetEnumerator())
			{
				dynamic info = XVar.Array(), newFieldName = null;
				info = XVar.Clone(this.upload_handler.pathinfo_local((XVar)(fileData.Value["usrName"])));
				newFieldName = XVar.Clone(this.upload_handler.tempnam_sfx((XVar)(absoluteUploadDirPath), (XVar)(info["filename"]), (XVar)(info["extension"])));
				MVCFunctions.runner_copy_file((XVar)(MVCFunctions.getabspath((XVar)(fileData.Value["name"]))), (XVar)(MVCFunctions.Concat(absoluteUploadDirPath, newFieldName)));
				filesData.InitAndSetArrayItem(MVCFunctions.Concat(uploadFolder, newFieldName), fileData.Key, "name");
				if(XVar.Pack(this.pageObject.pSetEdit.getCreateThumbnail((XVar)(this.field))))
				{
					dynamic newThumbName = null, thumbnailPrefix = null;
					thumbnailPrefix = XVar.Clone(this.pageObject.pSetEdit.getStrThumbnail((XVar)(this.field)));
					newThumbName = XVar.Clone(this.upload_handler.tempnam_sfx((XVar)(absoluteUploadDirPath), (XVar)(MVCFunctions.Concat(thumbnailPrefix, info["filename"])), (XVar)(info["extension"])));
					MVCFunctions.runner_copy_file((XVar)(MVCFunctions.getabspath((XVar)(fileData.Value["thumbnail"]))), (XVar)(MVCFunctions.Concat(absoluteUploadDirPath, newThumbName)));
					filesData.InitAndSetArrayItem(MVCFunctions.Concat(uploadFolder, newThumbName), fileData.Key, "thumbnail");
				}
			}
			return MVCFunctions.my_json_encode((XVar)(filesData));
		}
	}
}
