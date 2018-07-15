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
	public partial class UploadHandler : XClass
	{
		public dynamic formStamp;
		public dynamic pageType;
		public dynamic table;
		public dynamic field;
		public dynamic tkeys;
		public dynamic options;
		public ProjectSettings pSet = null;
		public UploadHandler(dynamic _param_options = null)
		{
			#region default values
			if(_param_options as Object == null) _param_options = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic options = XVar.Clone(_param_options);
			#endregion

			this.formStamp = new XVar("");
			this.pageType = new XVar("");
			this.table = new XVar("");
			this.field = new XVar("");
			this.tkeys = new XVar("");
			this.options = XVar.Clone(XVar.Array());
			this.options.InitAndSetArrayItem(MVCFunctions.GetTableLink(new XVar("mfhandler")), "script_url");
			this.options.InitAndSetArrayItem("files", "param_name");
			this.options.InitAndSetArrayItem("DELETE", "delete_type");
			this.options.InitAndSetArrayItem(null, "max_totalFile_size");
			this.options.InitAndSetArrayItem(null, "max_file_size");
			this.options.InitAndSetArrayItem(0, "min_file_size");
			this.options.InitAndSetArrayItem(false, "resizeOnUpload");
			this.options.InitAndSetArrayItem(".+$", "accept_file_types");
			this.options.InitAndSetArrayItem(null, "max_number_of_files");
			this.options.InitAndSetArrayItem(null, "max_width");
			this.options.InitAndSetArrayItem(null, "max_height");
			this.options.InitAndSetArrayItem(1, "min_width");
			this.options.InitAndSetArrayItem(1, "min_height");
			this.options.InitAndSetArrayItem(XVar.Array(), "image_versions");
			if(XVar.Pack(options))
			{
				foreach (KeyValuePair<XVar, dynamic> value in options.GetEnumerator())
				{
					if(XVar.Pack(this.options.KeyExists(value.Key)))
					{
						this.options.InitAndSetArrayItem(options[value.Key], value.Key);
					}
				}
			}
		}
		public virtual XVar getFullUrl()
		{
			dynamic https = null;
			https = XVar.Clone((XVar)(MVCFunctions.strlen((XVar)(MVCFunctions.GetServerVariable("HTTPS"))) != 0)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.GetServerVariable("HTTPS")), XVar.Pack("off"))));
			return MVCFunctions.Concat((XVar.Pack(https) ? XVar.Pack("https://") : XVar.Pack("http://")), (XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.GetRemoteUser())) != 0) ? XVar.Pack(MVCFunctions.Concat(MVCFunctions.GetRemoteUser(), "@")) : XVar.Pack("")), (XVar.Pack(MVCFunctions.SERVERKeyExists("HTTP_HOST")) ? XVar.Pack(MVCFunctions.GetServerVariable("HTTP_HOST")) : XVar.Pack(MVCFunctions.Concat(MVCFunctions.GetServerName(), (XVar.Pack((XVar)((XVar)(https)  && (XVar)(XVar.Equals(XVar.Pack(MVCFunctions.GetServerPort()), XVar.Pack(443))))  || (XVar)(XVar.Equals(XVar.Pack(MVCFunctions.GetServerPort()), XVar.Pack(80)))) ? XVar.Pack("") : XVar.Pack(MVCFunctions.Concat(":", MVCFunctions.GetServerPort())))))), MVCFunctions.substr((XVar)(MVCFunctions.GetScriptName()), new XVar(0), (XVar)(MVCFunctions.strrpos((XVar)(MVCFunctions.GetScriptName()), new XVar("/")))));
		}
		public virtual XVar set_file_delete_url(dynamic _param_file)
		{
			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			#endregion

			file.InitAndSetArrayItem(MVCFunctions.Concat(this.options["script_url"], "?file=", MVCFunctions.RawUrlEncode((XVar)(file["name"]))), "delete_url");
			file.InitAndSetArrayItem(this.options["delete_type"], "delete_type");
			if(!XVar.Equals(XVar.Pack(file["delete_type"]), XVar.Pack("DELETE")))
			{
				file["delete_url"] = MVCFunctions.Concat(file["delete_url"], "&_method=DELETE");
			}
			return null;
		}
		public virtual XVar get_file_object(dynamic _param_file_name)
		{
			#region pass-by-value parameters
			dynamic file_name = XVar.Clone(_param_file_name);
			#endregion

			dynamic file_path = null;
			file_path = XVar.Clone(MVCFunctions.Concat(this.pSet.getUploadFolder((XVar)(this.field)), file_name));
			if((XVar)(MVCFunctions.file_exists(file_path))  && (XVar)(!XVar.Equals(XVar.Pack(file_name[0]), XVar.Pack("."))))
			{
				dynamic file = XVar.Array(), path_parts = XVar.Array();
				file = XVar.Clone(XVar.Array());
				file.InitAndSetArrayItem(false, "error");
				file.InitAndSetArrayItem(file_path, "name");
				file.InitAndSetArrayItem(file_name, "usrName");
				path_parts = XVar.Clone(pathinfo_local((XVar)(file_name)));
				file.InitAndSetArrayItem(CommonFunctions.getContentTypeByExtension((XVar)(path_parts["extension"])), "type");
				file.InitAndSetArrayItem(MVCFunctions.filesize((XVar)(file_path)), "size");
				file.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(file_path)), "url");
				file.InitAndSetArrayItem("", "thumbnail");
				foreach (KeyValuePair<XVar, dynamic> options in this.options["image_versions"].GetEnumerator())
				{
					if(XVar.Pack(file_path))
					{
						dynamic thumbPath = null;
						thumbPath = XVar.Clone(MVCFunctions.Concat(this.pSet.getUploadFolder((XVar)(this.field)), this.pSet.getStrThumbnail((XVar)(this.field)), file_name));
						if(XVar.Pack(MVCFunctions.file_exists(MVCFunctions.getabspath((XVar)(thumbPath)))))
						{
							file.InitAndSetArrayItem(thumbPath, "thumbnail");
							path_parts = XVar.Clone(pathinfo_local((XVar)(thumbPath)));
							file.InitAndSetArrayItem(CommonFunctions.getContentTypeByExtension((XVar)(path_parts["extension"])), "thumbnail_type");
							file.InitAndSetArrayItem(MVCFunctions.filesize((XVar)(MVCFunctions.getabspath((XVar)(thumbPath)))), "thumbnail_size");
						}
						else
						{
							file.InitAndSetArrayItem(file_path, "thumbnail");
							file.InitAndSetArrayItem(file["type"], "thumbnail_type");
							file.InitAndSetArrayItem(file["size"], "thumbnail_size");
						}
					}
				}
				set_file_delete_url((XVar)(file));
				return file;
			}
			return null;
		}
		public virtual XVar validate(dynamic _param_uploadedFile, dynamic file, dynamic _param_error, dynamic _param_file_size, dynamic _param_index, dynamic _param_uploadDir)
		{
			#region pass-by-value parameters
			dynamic uploadedFile = XVar.Clone(_param_uploadedFile);
			dynamic var_error = XVar.Clone(_param_error);
			dynamic file_size = XVar.Clone(_param_file_size);
			dynamic index = XVar.Clone(_param_index);
			dynamic uploadDir = XVar.Clone(_param_uploadDir);
			#endregion

			if(XVar.Pack(var_error))
			{
				file.InitAndSetArrayItem(codeToMessage((XVar)(var_error)), "error");
				return false;
			}
			if(XVar.Pack(!(XVar)(file["name"])))
			{
				file.InitAndSetArrayItem("File name was not provided", "error");
				return false;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.preg_match((XVar)(this.options["accept_file_types"]), (XVar)(file["name"])))))
			{
				file.InitAndSetArrayItem("File type is not acceptable", "error");
				return false;
			}
			if((XVar)(this.options["max_file_size"])  && (XVar)((XVar)(this.options["max_file_size"] * 1024 < file_size)  || (XVar)(this.options["max_file_size"] * 1024 < file["size"])))
			{
				file.InitAndSetArrayItem(MVCFunctions.mysprintf(new XVar("File size exceeds limit of %s kbytes"), (XVar)(new XVar(0, this.options["max_file_size"]))), "error");
				return false;
			}
			if((XVar)(this.options["min_file_size"])  && (XVar)(file_size < this.options["min_file_size"] * 1024))
			{
				file.InitAndSetArrayItem(MVCFunctions.mysprintf(new XVar("File size must not be less than %s kbytes"), (XVar)(new XVar(0, this.options["min_file_size"]))), "error");
				return false;
			}
			if((XVar)(MVCFunctions.IsNumeric(this.options["max_totalFile_size"]))  && (XVar)(this.options["max_totalFile_size"] * 1024 < getUploadFilesSize() + file["size"]))
			{
				file.InitAndSetArrayItem(MVCFunctions.mysprintf(new XVar("Total files size exceeds limit of %s kbytes"), (XVar)(new XVar(0, this.options["max_totalFile_size"]))), "error");
				return false;
			}
			if((XVar)(MVCFunctions.IsNumeric(this.options["max_number_of_files"]))  && (XVar)((XVar)(this.options["max_number_of_files"] <= getUploadFilesCount())  && (XVar)(0 < this.options["max_number_of_files"])))
			{
				if(1 < this.options["max_number_of_files"])
				{
					file.InitAndSetArrayItem(MVCFunctions.mysprintf(new XVar("You can upload no more than %s files"), (XVar)(new XVar(0, this.options["max_number_of_files"]))), "error");
				}
				else
				{
					file.InitAndSetArrayItem("You can upload only one file", "error");
				}
				return false;
			}
			if(XVar.Pack(CommonFunctions.isImageType((XVar)(uploadedFile["type"]))))
			{
				dynamic image_size = XVar.Array(), img_height = null, img_width = null;
				image_size = XVar.Clone(MVCFunctions.runner_getimagesize((XVar)(uploadedFile["tmp_name"]), (XVar)(uploadedFile)));
				img_width = XVar.Clone(image_size[0]);
				img_height = XVar.Clone(image_size[1]);
				if(XVar.Pack(MVCFunctions.IsNumeric(img_width)))
				{
					if((XVar)((XVar)((XVar)(this.options["max_width"])  && (XVar)(this.options["max_width"] < img_width))  || (XVar)((XVar)(this.options["max_height"])  && (XVar)(this.options["max_height"] < img_height)))  && (XVar)(!(XVar)(this.options["resizeOnUpload"])))
					{
						file.InitAndSetArrayItem("maxResolution", "error");
						return false;
					}
					if((XVar)((XVar)(this.options["min_width"])  && (XVar)(img_width < this.options["min_width"]))  || (XVar)((XVar)(this.options["min_height"])  && (XVar)(img_height < this.options["min_height"])))
					{
						file.InitAndSetArrayItem("minResolution", "error");
						return false;
					}
				}
			}
			return true;
		}
		public virtual XVar getUploadFilesCount()
		{
			dynamic result = null;
			result = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> fileArray in XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].GetEnumerator())
			{
				if((XVar)(!(XVar)(fileArray.Value["deleted"]))  && (XVar)(0 < MVCFunctions.count(fileArray.Value)))
				{
					result++;
				}
			}
			return result;
		}
		public virtual XVar getUploadFilesSize()
		{
			dynamic result = null;
			result = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> fileArray in XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(fileArray.Value["deleted"])))
				{
					result += fileArray.Value["file"]["size"];
				}
			}
			return result;
		}
		public virtual XVar handle_form_data(dynamic _param_file, dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			dynamic index = XVar.Clone(_param_index);
			#endregion

			return null;
		}
		public virtual XVar handle_file_upload(dynamic _param_uploadedFile, dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic uploadedFile = XVar.Clone(_param_uploadedFile);
			dynamic index = XVar.Clone(_param_index);
			#endregion

			dynamic file = XVar.Array(), fileInfo = null, name = null, path_parts = XVar.Array(), size = null, tmpName = null, uploadDir = null, uploadDirRelative = null, var_error = null, var_type = null;
			tmpName = XVar.Clone(uploadedFile["tmp_name"]);
			name = XVar.Clone(uploadedFile["name"]);
			size = XVar.Clone(uploadedFile["size"]);
			var_type = XVar.Clone(uploadedFile["type"]);
			var_error = XVar.Clone(uploadedFile["error"]);
			fileInfo = XVar.Clone(new XVar("name", name, "size", MVCFunctions.intval((XVar)(size)), "type", var_type, "isThumbnail", false));
			uploadDir = XVar.Clone(this.pSet.getFinalUploadFolder((XVar)(this.field), (XVar)(fileInfo)));
			uploadDirRelative = XVar.Clone(this.pSet.getUploadFolder((XVar)(this.field), (XVar)(fileInfo)));
			file = XVar.Clone(XVar.Array());
			file.InitAndSetArrayItem(false, "error");
			file.InitAndSetArrayItem(MVCFunctions.trim_file_name((XVar)(name), (XVar)(var_type), (XVar)(index), this), "name");
			file.InitAndSetArrayItem(file["name"], "usrName");
			file.InitAndSetArrayItem(MVCFunctions.intval((XVar)(size)), "size");
			switch(((XVar)var_type).ToString())
			{
				case "image/png":
				case "image/x-png":
					file.InitAndSetArrayItem("image/png", "type");
					break;
				case "image/jpeg":
				case "image/pjpeg":
					file.InitAndSetArrayItem("image/jpeg", "type");
					break;
				default:
					file.InitAndSetArrayItem(var_type, "type");
					break;
			}
			path_parts = XVar.Clone(pathinfo_local((XVar)(name)));
			if(file["type"] == "")
			{
				file.InitAndSetArrayItem(CommonFunctions.getContentTypeByExtension((XVar)(path_parts["extension"])), "type");
			}
			file.InitAndSetArrayItem(false, "isImg");
			file.InitAndSetArrayItem("", "thumbnail");
			if(XVar.Pack(this.pSet.isMakeDirectoryNeeded((XVar)(this.field))))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.makeSurePathExists((XVar)(uploadDir)))))
				{
					file.InitAndSetArrayItem("Upload folder doesn't exist", "error");
					return file;
				}
			}
			else
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.is_dir((XVar)(uploadDir)))))
				{
					file.InitAndSetArrayItem("Upload folder doesn't exist", "error");
					return file;
				}
			}
			if(XVar.Pack(validate((XVar)(uploadedFile), (XVar)(file), (XVar)(var_error), (XVar)(size), (XVar)(index), (XVar)(uploadDir))))
			{
				dynamic file_path = null, file_size = null;
				file.InitAndSetArrayItem(CommonFunctions.CheckImageExtension((XVar)(tmpName)) != false, "isImg");
				handle_form_data((XVar)(file), (XVar)(index));
				file.InitAndSetArrayItem(tempnam_sfx((XVar)(uploadDir), (XVar)(path_parts["filename"]), (XVar)(path_parts["extension"])), "name");
				file_path = XVar.Clone(MVCFunctions.Concat(uploadDir, file["name"]));
				MVCFunctions.clearstatcache();
				MVCFunctions.upload_File((XVar)(uploadedFile), (XVar)(file_path));
				file_size = XVar.Clone(MVCFunctions.filesize((XVar)(file_path)));
				if(XVar.Pack(this.options["resizeOnUpload"]))
				{
					dynamic new_file_name = null, tempOptions = null;
					tempOptions = XVar.Clone(new XVar("max_width", this.options["max_width"], "max_height", this.options["max_width"]));
					new_file_name = XVar.Clone(tempnam_sfx((XVar)(uploadDir), (XVar)(path_parts["filename"]), (XVar)(path_parts["extension"])));
					if(XVar.Pack(create_scaled_image((XVar)(MVCFunctions.Concat(uploadDir, file["name"])), (XVar)(uploadDir), (XVar)(new_file_name), (XVar)(tempOptions), (XVar)(file), new XVar(false), (XVar)(uploadDirRelative), (XVar)(uploadedFile))))
					{
						MVCFunctions.unlink((XVar)(file_path));
						file.InitAndSetArrayItem(new_file_name, "name");
						file_path = XVar.Clone(MVCFunctions.Concat(uploadDir, new_file_name));
						file_size = XVar.Clone(MVCFunctions.filesize((XVar)(file_path)));
					}
				}
				if(XVar.Equals(XVar.Pack(file_size), XVar.Pack(file["size"])))
				{
					file.InitAndSetArrayItem(MVCFunctions.Concat(uploadDir, MVCFunctions.RawUrlEncode((XVar)(file["name"]))), "url");
					foreach (KeyValuePair<XVar, dynamic> options in this.options["image_versions"].GetEnumerator())
					{
						dynamic fileInfoThimbnail = null, thumbUploadDir = null, thumbUploadDirRelative = null, thumbnail_name = null;
						fileInfoThimbnail = XVar.Clone(new XVar("name", name, "size", MVCFunctions.intval((XVar)(size)), "type", var_type, "isThumbnail", true));
						thumbUploadDir = XVar.Clone(this.pSet.getFinalUploadFolder((XVar)(this.field), (XVar)(fileInfoThimbnail)));
						thumbUploadDirRelative = XVar.Clone(this.pSet.getUploadFolder((XVar)(this.field), (XVar)(fileInfoThimbnail)));
						if(XVar.Pack(this.pSet.isMakeDirectoryNeeded((XVar)(this.field))))
						{
							if(XVar.Pack(!(XVar)(MVCFunctions.makeSurePathExists((XVar)(thumbUploadDir)))))
							{
								continue;
							}
						}
						thumbnail_name = XVar.Clone(tempnam_sfx((XVar)(thumbUploadDir), (XVar)(MVCFunctions.Concat(options.Value["thumbnailPrefix"], path_parts["filename"])), (XVar)(path_parts["extension"])));
						if(XVar.Pack(create_scaled_image((XVar)(MVCFunctions.Concat(uploadDir, file["name"])), (XVar)(thumbUploadDir), (XVar)(thumbnail_name), (XVar)(options.Value), (XVar)(file), new XVar(true), (XVar)(thumbUploadDirRelative), (XVar)(uploadedFile))))
						{
							MVCFunctions.clearstatcache();
							file_size = XVar.Clone(MVCFunctions.filesize((XVar)(file_path)));
						}
					}
				}
				else
				{
					MVCFunctions.unlink((XVar)(file_path));
					file.InitAndSetArrayItem("abort", "error");
				}
				file.InitAndSetArrayItem(file_size, "size");
				file.InitAndSetArrayItem(MVCFunctions.Concat(uploadDirRelative, file["name"]), "name");
				set_file_delete_url((XVar)(file));
			}
			return file;
		}
		public virtual XVar get()
		{
			dynamic file_name = null, info = null;
			file_name = new XVar(null);
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("file")))
			{
				MVCFunctions.basename((XVar)(GlobalVars.cman.byTable((XVar)(this.table)).stripSlashesBinary((XVar)(MVCFunctions.postvalue("file")))));
			}
			if(XVar.Pack(file_name))
			{
				info = XVar.Clone(get_file_object((XVar)(file_name)));
			}
			else
			{
				info = XVar.Clone(this.Invoke("get_file_objects"));
			}
			MVCFunctions.Header("Content-type", "application/json");
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(info)));
			return null;
		}
		public virtual XVar post()
		{
			dynamic info = XVar.Array(), json = null, result = XVar.Array(), upload = XVar.Array();
			upload = XVar.Clone(MVCFunctions.uploadFiles((XVar)(this.options["param_name"])));
			info = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> uploadedFile in upload.GetEnumerator())
			{
				info.InitAndSetArrayItem(handle_file_upload((XVar)(uploadedFile.Value), (XVar)(uploadedFile.Key)), null);
			}
			MVCFunctions.Header("Vary", "Accept");
			result = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> file in info.GetEnumerator())
			{
				dynamic userFile = XVar.Array();
				if(XVar.Equals(XVar.Pack(file.Value["error"]), XVar.Pack(false)))
				{
					dynamic sessionArray = XVar.Array();
					sessionArray = XVar.Clone(XVar.Array());
					sessionArray.InitAndSetArrayItem(file.Value, "file");
					sessionArray.InitAndSetArrayItem(false, "fromDB");
					sessionArray.InitAndSetArrayItem(false, "deleted");
					XSession.Session.InitAndSetArrayItem(sessionArray, MVCFunctions.Concat("mupload_", this.formStamp), file.Value["usrName"]);
				}
				userFile = XVar.Clone(buildUserFile((XVar)(file.Value)));
				if(XVar.Pack(!(XVar)(userFile["isImg"])))
				{
					userFile.InitAndSetArrayItem(true, "isImg");
					userFile.InitAndSetArrayItem(MVCFunctions.Concat(userFile["url"], "&icon=1"), "thumbnail_url");
				}
				result.InitAndSetArrayItem(userFile, null);
			}
			json = XVar.Clone(MVCFunctions.my_json_encode((XVar)(result)));
			if(XVar.Pack(MVCFunctions.IsJSONAccepted()))
			{
				MVCFunctions.Header("Content-type", "application/json");
			}
			else
			{
				MVCFunctions.Header("Content-type", "text/plain");
			}
			MVCFunctions.Echo(json);
			return null;
		}
		public virtual XVar buildUserFile(dynamic _param_file)
		{
			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			#endregion

			dynamic hasThumbnail = null, userFile = XVar.Array();
			userFile = XVar.Clone(XVar.Array());
			userFile.InitAndSetArrayItem(file["usrName"], "name");
			userFile.InitAndSetArrayItem(file["size"], "size");
			userFile.InitAndSetArrayItem(file["type"], "type");
			userFile.InitAndSetArrayItem(CommonFunctions.CheckImageExtension((XVar)(file["name"])) != false, "isImg");
			if(XVar.Pack(file["error"]))
			{
				userFile.InitAndSetArrayItem(file["error"], "error");
			}
			hasThumbnail = XVar.Clone(file["thumbnail"] != "");
			userFile.InitAndSetArrayItem(MVCFunctions.GetTableLink(new XVar("mfhandler"), new XVar(""), (XVar)(MVCFunctions.Concat("file=", MVCFunctions.RawUrlEncode((XVar)(userFile["name"])), "&table=", MVCFunctions.RawUrlEncode((XVar)(this.table)), "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), "&pageType=", MVCFunctions.RawUrlEncode((XVar)(this.pageType)), (XVar.Pack(this.tkeys != "") ? XVar.Pack(this.tkeys) : XVar.Pack(MVCFunctions.Concat("&fkey=", this.formStamp)))))), "url");
			if(XVar.Pack(hasThumbnail))
			{
				userFile.InitAndSetArrayItem(MVCFunctions.Concat(userFile["url"], "&thumbnail=1"), "thumbnail_url");
			}
			else
			{
				userFile.InitAndSetArrayItem("", "thumbnail_url");
			}
			return userFile;
		}
		public virtual XVar delete()
		{
			dynamic fileName = null, success = null;
			fileName = XVar.Clone(MVCFunctions.postvalue(new XVar("fileName")));
			success = new XVar(false);
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].KeyExists(fileName)))
			{
				if(XVar.Pack(!(XVar)(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)][fileName]["fromDB"])))
				{
					dynamic file_path = null, sessionFile = XVar.Array();
					sessionFile = XVar.Clone(XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)][fileName]["file"]);
					file_path = XVar.Clone(sessionFile["name"]);
					if(XVar.Pack(MVCFunctions.file_exists(file_path)))
					{
						success = XVar.Clone(MVCFunctions.unlink((XVar)(file_path)));
					}
					if((XVar)(success)  && (XVar)(sessionFile["thumbnail"] != ""))
					{
						dynamic file = null;
						file = XVar.Clone(sessionFile["thumbnail"]);
						if(XVar.Pack(MVCFunctions.file_exists(file)))
						{
							MVCFunctions.unlink((XVar)(file));
						}
					}
					XSession.Session[MVCFunctions.Concat("mupload_", this.formStamp)].Remove(fileName);
				}
				else
				{
					XSession.Session.InitAndSetArrayItem(true, MVCFunctions.Concat("mupload_", this.formStamp), fileName, "deleted");
					success = new XVar(true);
				}
			}
			MVCFunctions.Header("Content-type", "application/json");
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(success)));
			return null;
		}
		public virtual XVar tempnam_sfx(dynamic _param_path, dynamic _param_prefix, dynamic _param_suffix)
		{
			#region pass-by-value parameters
			dynamic path = XVar.Clone(_param_path);
			dynamic prefix = XVar.Clone(_param_prefix);
			dynamic suffix = XVar.Clone(_param_suffix);
			#endregion

			dynamic file = null, fileName = null;
			do
			{
				fileName = XVar.Clone(MVCFunctions.Concat(prefix, "_", CommonFunctions.generatePassword(new XVar(8)), ".", suffix));
				file = XVar.Clone(MVCFunctions.Concat(path, fileName));
				if(XVar.Pack(MVCFunctions.try_create_new_file((XVar)(file))))
				{
					return fileName;
				}
			}
			while(XVar.Pack(true));
			return "";
		}
		public virtual XVar create_scaled_image(dynamic _param_file_path, dynamic _param_uploadDir, dynamic _param_new_file_name, dynamic _param_options, dynamic file, dynamic _param_isThumbnail, dynamic _param_uploadDirRelative, dynamic _param_uploadedFile)
		{
			#region pass-by-value parameters
			dynamic file_path = XVar.Clone(_param_file_path);
			dynamic uploadDir = XVar.Clone(_param_uploadDir);
			dynamic new_file_name = XVar.Clone(_param_new_file_name);
			dynamic options = XVar.Clone(_param_options);
			dynamic isThumbnail = XVar.Clone(_param_isThumbnail);
			dynamic uploadDirRelative = XVar.Clone(_param_uploadDirRelative);
			dynamic uploadedFile = XVar.Clone(_param_uploadedFile);
			#endregion

			dynamic img_height = null, img_size = XVar.Array(), img_width = null, new_file_path = null, new_height = null, new_width = null, scale = null, success = null;
			img_size = XVar.Clone(MVCFunctions.runner_getimagesize((XVar)(file_path), (XVar)(uploadedFile)));
			img_width = XVar.Clone(img_size[0]);
			img_height = XVar.Clone(img_size[1]);
			new_file_path = XVar.Clone(MVCFunctions.Concat(uploadDir, new_file_name));
			if((XVar)(!(XVar)(img_width))  || (XVar)(!(XVar)(img_height)))
			{
				MVCFunctions.unlink((XVar)(new_file_path));
				return false;
			}
			scale = XVar.Clone(MVCFunctions.min((XVar)(options["max_width"] / img_width), (XVar)(options["max_height"] / img_height)));
			if(1 <= scale)
			{
				if(!XVar.Equals(XVar.Pack(file_path), XVar.Pack(new_file_path)))
				{
					dynamic result = null;
					result = XVar.Clone(MVCFunctions.copy((XVar)(file_path), (XVar)(new_file_path)));
					if((XVar)(result)  && (XVar)(isThumbnail))
					{
						file.InitAndSetArrayItem(MVCFunctions.Concat(uploadDirRelative, new_file_name), "thumbnail");
						file.InitAndSetArrayItem(MVCFunctions.filesize((XVar)(file_path)), "thumbnail_size");
						file.InitAndSetArrayItem(file["type"], "thumbnail_type");
					}
					return result;
				}
				return false;
			}
			new_width = XVar.Clone(img_width * scale);
			new_height = XVar.Clone(img_height * scale);
			success = XVar.Clone(MVCFunctions.imageCreateThumb((XVar)(new_width), (XVar)(new_height), (XVar)(img_width), (XVar)(img_height), (XVar)(file_path), (XVar)(options), (XVar)(new_file_path), (XVar)(uploadedFile)));
			if(XVar.Pack(success))
			{
				if(XVar.Pack(isThumbnail))
				{
					file.InitAndSetArrayItem(MVCFunctions.Concat(uploadDirRelative, new_file_name), "thumbnail");
					file.InitAndSetArrayItem(MVCFunctions.filesize((XVar)(new_file_path)), "thumbnail_size");
					file.InitAndSetArrayItem(file["type"], "thumbnail_type");
				}
				else
				{
					file.InitAndSetArrayItem(new_file_name, "name");
					file.InitAndSetArrayItem(MVCFunctions.filesize((XVar)(new_file_path)), "size");
				}
			}
			return success;
		}
		public virtual XVar pathinfo_local(dynamic _param_path)
		{
			#region pass-by-value parameters
			dynamic path = XVar.Clone(_param_path);
			#endregion

			dynamic ret = XVar.Array();
			ret = XVar.Clone(MVCFunctions.pathinfo((XVar)(path)));
			if(XVar.Pack(!(XVar)(ret.KeyExists("filename"))))
			{
				dynamic extlen = null;
				extlen = XVar.Clone(MVCFunctions.strlen((XVar)(ret["extension"])));
				if(XVar.Pack(extlen))
				{
					++(extlen);
				}
				ret.InitAndSetArrayItem(MVCFunctions.substr((XVar)(ret["basename"]), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(ret["basename"])) - extlen)), "filename");
			}
			return ret;
		}
		public virtual XVar codeToMessage(dynamic _param_code)
		{
			#region pass-by-value parameters
			dynamic code = XVar.Clone(_param_code);
			#endregion

			dynamic message = null;
			switch(((XVar)code).ToInt())
			{
				case 1:
					message = new XVar("UPLOAD_ERR_INI_SIZE: The uploaded file exceeds the upload_max_filesize directive in php.ini");
					break;
				case 2:
					message = new XVar("UPLOAD_ERR_FORM_SIZE: The uploaded file exceeds the MAX_FILE_SIZE directive that was specified in the HTML form");
					break;
				case 3:
					message = new XVar("UPLOAD_ERR_PARTIAL: The uploaded file was only partially uploaded");
					break;
				case 4:
					message = new XVar("UPLOAD_ERR_NO_FILE: No file was uploaded");
					break;
				case 6:
					message = new XVar("UPLOAD_ERR_NO_TMP_DIR: Missing a temporary folder");
					break;
				case 7:
					message = new XVar("UPLOAD_ERR_CANT_WRITE: Failed to write file to disk");
					break;
				case 8:
					message = new XVar("UPLOAD_ERR_EXTENSION: File upload stopped by extension");
					break;
				default:
					message = new XVar("Unknown upload error");
					break;
			}
			return message;
		}
	}
}
