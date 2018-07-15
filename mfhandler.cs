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
	public partial class SessionStaticGlobalController : BaseController
	{
		public XVar mfhandler()
		{
			try
			{
				dynamic _connection = null, field = null, fileName = null, formStamp = null, fsFileName = null, fsize = null, ftype = null, havePermission = null, iconShowed = null, isDBFile = null, isPDF = null, outputAsAttachment = null, pageType = null, pdf = null, requestAction = null, sessionFile = XVar.Array(), upload_handler = null, value = null, var_params = XVar.Array();
				ProjectSettings pSet;
				isPDF = new XVar(false);
				if(XVar.Pack(isPDF))
				{
					GlobalVars.strTableName = XVar.Clone(var_params["table"]);
					field = XVar.Clone(var_params["field"]);
					pageType = XVar.Clone(var_params["pageType"]);
					outputAsAttachment = new XVar(false);
				}
				else
				{
					GlobalVars.strTableName = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
					field = XVar.Clone(MVCFunctions.postvalue(new XVar("field")));
					pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pageType")));
					outputAsAttachment = XVar.Clone(MVCFunctions.postvalue(new XVar("nodisp")) != 1);
				}
				if(GlobalVars.strTableName == XVar.Pack(""))
				{
					if(XVar.Pack(!(XVar)(isPDF)))
					{
						MVCFunctions.Echo("<p>No table name received</p>");
					}
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(field == XVar.Pack(""))
				{
					if(XVar.Pack(!(XVar)(isPDF)))
					{
						MVCFunctions.Echo("<p>No field name received</p>");
					}
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(!(XVar)(CommonFunctions.GetTableURL((XVar)(GlobalVars.strTableName)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(isPDF))
				{
					requestAction = new XVar("GET");
				}
				else
				{
					requestAction = XVar.Clone(MVCFunctions.postvalue("_action"));
				}
				_connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(pageType)));
				if(requestAction == "POST")
				{
					if((XVar)((XVar)((XVar)((XVar)((XVar)(pageType == Constants.PAGE_ADD)  && (XVar)(!(XVar)(pSet.appearOnAddPage((XVar)(field)))))  && (XVar)(!(XVar)(pSet.appearOnInlineAdd((XVar)(field)))))  || (XVar)((XVar)((XVar)(pageType == Constants.PAGE_EDIT)  && (XVar)(!(XVar)(pSet.appearOnEditPage((XVar)(field)))))  && (XVar)(!(XVar)(pSet.appearOnInlineEdit((XVar)(field))))))  || (XVar)((XVar)(pageType == Constants.PAGE_REGISTER)  && (XVar)(!(XVar)(pSet.appearOnRegisterOrSearchPage((XVar)(field), (XVar)(pageType))))))  || (XVar)((XVar)((XVar)(pageType != Constants.PAGE_ADD)  && (XVar)(pageType != Constants.PAGE_EDIT))  && (XVar)(pageType != Constants.PAGE_REGISTER)))
					{
						MVCFunctions.Echo(new XVar("You have no permissions for this action"));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
				}
				else
				{
					if((XVar)(!(XVar)(pSet.checkFieldPermissions((XVar)(field))))  && (XVar)((XVar)(pageType != Constants.PAGE_ADD)  || (XVar)((XVar)(!(XVar)(pSet.appearOnAddPage((XVar)(field))))  && (XVar)(!(XVar)(pSet.appearOnInlineAdd((XVar)(field)))))))
					{
						MVCFunctions.Echo(new XVar("You have no permissions for this action"));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
				}
				if(XVar.Pack(!(XVar)(isPDF)))
				{
					CommonFunctions.add_nocache_headers();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", CommonFunctions.GetTableURL((XVar)(GlobalVars.strTableName)), ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if(requestAction == "POST")
				{
					havePermission = XVar.Clone((XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Add")))  || (XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Edit"))));
				}
				else
				{
					havePermission = XVar.Clone(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")));
				}
				if((XVar)((XVar)(!(XVar)(CommonFunctions.isLogged()))  && (XVar)(pageType != Constants.PAGE_REGISTER))  || (XVar)(!(XVar)(havePermission)))
				{
					MVCFunctions.HeaderRedirect(new XVar("login"), new XVar(""), new XVar("message=expired"));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				upload_handler = XVar.Clone(new UploadHandler((XVar)(CommonFunctions.getOptionsForMultiUpload((XVar)(pSet), (XVar)(field)))));
				upload_handler.pSet = XVar.Clone(pSet);
				upload_handler.field = XVar.Clone(field);
				upload_handler.table = XVar.Clone(GlobalVars.strTableName);
				upload_handler.pageType = XVar.Clone(pageType);
				switch(((XVar)requestAction).ToString())
				{
					case "DELETE":
						CommonFunctions.printMFHandlerHeaders();
						formStamp = XVar.Clone(MVCFunctions.postvalue(new XVar("formStamp")));
						if(formStamp != XVar.Pack(""))
						{
							upload_handler.formStamp = XVar.Clone(formStamp);
							upload_handler.delete();
						}
						break;
					case "POST":
						CommonFunctions.printMFHandlerHeaders();
						formStamp = XVar.Clone(MVCFunctions.postvalue(new XVar("formStamp")));
						if(formStamp != XVar.Pack(""))
						{
							upload_handler.formStamp = XVar.Clone(formStamp);
							upload_handler.post();
						}
						break;
					case "GET":
					default:
						if(XVar.Pack(isPDF))
						{
							isDBFile = XVar.Clone(var_params.KeyExists("filename"));
							fileName = XVar.Clone((XVar.Pack(var_params.KeyExists("file")) ? XVar.Pack(var_params["file"]) : XVar.Pack(var_params["filename"])));
						}
						else
						{
							isDBFile = XVar.Clone(MVCFunctions.postvalue(new XVar("filename")) != "");
							fileName = XVar.Clone((XVar.Pack(MVCFunctions.postvalue(new XVar("file")) != "") ? XVar.Pack(MVCFunctions.postvalue(new XVar("file"))) : XVar.Pack(MVCFunctions.postvalue(new XVar("filename")))));
							formStamp = XVar.Clone(MVCFunctions.postvalue(new XVar("fkey")));
						}
						if(fileName == XVar.Pack(""))
						{
							MVCFunctions.Echo(new XVar(""));
							return MVCFunctions.GetBuferContentAndClearBufer();
						}
						sessionFile = new XVar(null);
						fsFileName = new XVar("");
						if((XVar)((XVar)(!(XVar)(isDBFile))  && (XVar)(formStamp != XVar.Pack("")))  && (XVar)(XSession.Session[MVCFunctions.Concat("mupload_", formStamp)].KeyExists(fileName)))
						{
							sessionFile = XVar.Clone(XSession.Session[MVCFunctions.Concat("mupload_", formStamp)][fileName]["file"]);
						}
						else
						{
							dynamic i = null, keys = XVar.Array(), qResult = null, queryObj = null, strWhereClause = null, tKeys = XVar.Array();
							keys = XVar.Clone(XVar.Array());
							tKeys = XVar.Clone(pSet.getTableKeys());
							i = new XVar(0);
							for(;i < MVCFunctions.count(tKeys); i++)
							{
								if(XVar.Pack(isPDF))
								{
									keys.InitAndSetArrayItem(var_params[MVCFunctions.Concat("key", i + 1)], tKeys[i]);
								}
								else
								{
									keys.InitAndSetArrayItem(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("key", i + 1))), tKeys[i]);
								}
							}
							strWhereClause = XVar.Clone(CommonFunctions.KeyWhere((XVar)(keys)));
							if(pSet.getAdvancedSecurityType() != Constants.ADVSECURITY_ALL)
							{
								strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(CommonFunctions.SecuritySQL(new XVar("Search")))));
							}
							queryObj = XVar.Clone(pSet.getQueryObject());
							if(XVar.Pack(!(XVar)(queryObj.HasGroupBy())))
							{
								queryObj.RemoveAllFieldsExcept((XVar)(pSet.getFieldIndex((XVar)(field))));
							}
							qResult = XVar.Clone(_connection.query((XVar)(queryObj.gSQLWhere((XVar)(strWhereClause)))));
							if(XVar.Pack(isDBFile))
							{
								if(XVar.Pack(qResult))
								{
									dynamic data = XVar.Array();
									data = XVar.Clone(qResult.fetchAssoc());
									if(XVar.Pack(data))
									{
										value = XVar.Clone(_connection.stripSlashesBinary((XVar)(data[field])));
									}
								}
							}
							else
							{
								dynamic row = XVar.Array();
								GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName), (XVar)(pSet)));
								row = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
								if(XVar.Pack(row))
								{
									dynamic filesArray = XVar.Array();
									filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(row[field])));
									if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
									{
										if(row[field] == "")
										{
											filesArray = XVar.Clone(XVar.Array());
										}
										else
										{
											dynamic uploadedFile = XVar.Array();
											uploadedFile = XVar.Clone(upload_handler.get_file_object((XVar)(row[field])));
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
									foreach (KeyValuePair<XVar, dynamic> uploadedFile in filesArray.GetEnumerator())
									{
										if(uploadedFile.Value["usrName"] == fileName)
										{
											sessionFile = XVar.Clone(uploadedFile.Value);
											break;
										}
									}
								}
							}
						}
						iconShowed = new XVar(false);
						if(XVar.Pack(isDBFile))
						{
							ftype = new XVar("");
							if(pSet.getViewFormat((XVar)(field)) == Constants.FORMAT_DATABASE_IMAGE)
							{
								if(XVar.Pack(!(XVar)(value)))
								{
									value = XVar.Clone(MVCFunctions.myfile_get_contents(new XVar("images/no_image.gif")));
								}
								ftype = XVar.Clone(MVCFunctions.SupposeImageType((XVar)(value)));
							}
							if(XVar.Pack(!(XVar)(ftype)))
							{
								ftype = XVar.Clone(CommonFunctions.getContentTypeByExtension((XVar)(MVCFunctions.substr((XVar)(fileName), (XVar)(MVCFunctions.strrpos((XVar)(fileName), new XVar(".")))))));
							}
							fsize = XVar.Clone(MVCFunctions.strlen_bin((XVar)(value)));
						}
						else
						{
							if(sessionFile != null)
							{
								dynamic isSRC = null, isThumbnail = null;
								isThumbnail = new XVar(false);
								isSRC = new XVar(false);
								if(XVar.Pack(isPDF))
								{
									isThumbnail = XVar.Clone(var_params.KeyExists("thumbnail"));
									isSRC = XVar.Clone(var_params.KeyExists("src"));
								}
								else
								{
									isThumbnail = XVar.Clone(MVCFunctions.postvalue(new XVar("thumbnail")) != "");
									isSRC = XVar.Clone(MVCFunctions.postvalue(new XVar("src")) == 1);
								}
								if(MVCFunctions.postvalue(new XVar("icon")) != "")
								{
									fsFileName = XVar.Clone(MVCFunctions.Concat("images/icons/", CommonFunctions.getIconByFileType((XVar)(sessionFile["type"]), (XVar)(sessionFile["name"]))));
									fsize = XVar.Clone(MVCFunctions.filesize((XVar)(MVCFunctions.getabspath((XVar)(fsFileName)))));
									ftype = new XVar("image/png");
								}
								else
								{
									if((XVar)((XVar)(isThumbnail)  && (XVar)(sessionFile["thumbnail"]))  && (XVar)(MVCFunctions.GDExist()))
									{
										fsFileName = XVar.Clone(sessionFile["thumbnail"]);
										fsize = XVar.Clone(sessionFile["thumbnail_size"]);
										ftype = XVar.Clone(sessionFile["thumbnail_type"]);
									}
									else
									{
										if((XVar)((XVar)(false)  && (XVar)((XVar)(pageType == Constants.PAGE_EDIT)  || (XVar)(pageType == Constants.PAGE_ADD)))  && (XVar)(isSRC))
										{
											iconShowed = new XVar(true);
											fsFileName = XVar.Clone(MVCFunctions.Concat("images/icons/", CommonFunctions.getIconByFileType((XVar)(sessionFile["type"]), (XVar)(sessionFile["name"]))));
											fsize = XVar.Clone(MVCFunctions.filesize((XVar)(MVCFunctions.getabspath((XVar)(fsFileName)))));
											ftype = new XVar("image/png");
										}
										else
										{
											fsFileName = XVar.Clone(sessionFile["name"]);
											fsize = XVar.Clone(sessionFile["size"]);
											ftype = XVar.Clone(sessionFile["type"]);
										}
									}
								}
							}
						}
						if((XVar)((XVar)(isDBFile)  && (XVar)(value))  || (XVar)(fsFileName != XVar.Pack("")))
						{
							dynamic norange = null;
							if(XVar.Pack(!(XVar)(isDBFile)))
							{
								if((XVar)((XVar)(!(XVar)(pSet.isAbsolute((XVar)(field))))  && (XVar)(!(XVar)(MVCFunctions.isAbsolutePath((XVar)(fsFileName)))))  || (XVar)(iconShowed))
								{
									fsFileName = XVar.Clone(MVCFunctions.getabspath((XVar)(fsFileName)));
								}
								if(XVar.Pack(!(XVar)(MVCFunctions.myfile_exists((XVar)(fsFileName)))))
								{
									fsFileName = XVar.Clone(MVCFunctions.getabspath(new XVar("images/no_image.gif")));
									fsize = XVar.Clone(MVCFunctions.filesize((XVar)(fsFileName)));
									ftype = new XVar("image/gif");
								}
							}
							if(XVar.Pack(isPDF))
							{
								dynamic file = null;
								if(XVar.Pack(isDBFile))
								{
									file = XVar.Clone(value);
								}
								else
								{
									file = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(fsFileName)));
								}
								return MVCFunctions.GetBuferContentAndClearBufer();
							}
							norange = XVar.Clone(MVCFunctions.postvalue(new XVar("norange")) == 1);
							if(MVCFunctions.postvalue(new XVar("norange")) == 1)
							{
								MVCFunctions.Header("Accept-Ranges", "none");
								MVCFunctions.Header("Cache-Control", "private");
								MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Type: ", ftype)));
								MVCFunctions.Header("Access-Control-Allow-Methods", "HEAD, GET, POST");
								if(XVar.Pack(outputAsAttachment))
								{
									MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=\"", fileName, "\"")));
								}
								MVCFunctions.SendContentLength((XVar)(fsize));
								if(MVCFunctions.GetServerVariable("REQUEST_METHOD") == "HEAD")
								{
									MVCFunctions.Echo(new XVar(""));
									return MVCFunctions.GetBuferContentAndClearBufer();
								}
								if(XVar.Pack(isDBFile))
								{
									MVCFunctions.echoBinary((XVar)(value));
								}
								else
								{
									MVCFunctions.printfile((XVar)(fsFileName));
								}
							}
							else
							{
								dynamic httpRange = null, printContentLength = null, range = null, range_orig = null, seek_end = null, seek_start = null, size_unit = null, tmparr = XVar.Array();
								size_unit = new XVar("");
								range_orig = new XVar("");
								httpRange = XVar.Clone(MVCFunctions.GetHttpRange());
								if(XVar.Pack(MVCFunctions.preg_match(new XVar("/^bytes=((\\d*-\\d*,? ?)+)$/"), (XVar)(httpRange))))
								{
									tmparr = XVar.Clone(MVCFunctions.explode(new XVar("="), (XVar)(httpRange)));
									size_unit = XVar.Clone(tmparr[0]);
									range_orig = XVar.Clone(tmparr[1]);
								}
								if(size_unit == "bytes")
								{
									if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(range_orig), new XVar(","))), XVar.Pack(false)))
									{
										dynamic extra_ranges = null;
										tmparr = XVar.Clone(MVCFunctions.explode(new XVar(","), (XVar)(range_orig)));
										range = XVar.Clone(tmparr[0]);
										extra_ranges = XVar.Clone(tmparr[1]);
									}
									else
									{
										range = XVar.Clone(range_orig);
									}
								}
								else
								{
									range = new XVar("-");
								}
								tmparr = XVar.Clone(MVCFunctions.explode(new XVar("-"), (XVar)(range)));
								seek_start = XVar.Clone(tmparr[0]);
								seek_end = XVar.Clone(tmparr[1]);
								seek_end = XVar.Clone((XVar.Pack(MVCFunctions.strlen((XVar)(seek_end)) == 0) ? XVar.Pack(fsize - 1) : XVar.Pack(MVCFunctions.min((XVar)(MVCFunctions.abs((XVar)(MVCFunctions.intval((XVar)(seek_end))))), (XVar)(fsize - 1)))));
								seek_start = XVar.Clone((XVar.Pack((XVar)(MVCFunctions.strlen((XVar)(seek_start)) == 0)  || (XVar)(seek_end < MVCFunctions.abs((XVar)(MVCFunctions.intval((XVar)(seek_start)))))) ? XVar.Pack(0) : XVar.Pack(MVCFunctions.max((XVar)(MVCFunctions.abs((XVar)(MVCFunctions.intval((XVar)(seek_start))))), new XVar(0)))));
								if((XVar)(XVar.Pack(0) < seek_start)  || (XVar)(seek_end < fsize - 1))
								{
									MVCFunctions.Header(new XVar("HTTP/1.1 206 Partial Content"));
								}
								MVCFunctions.Header("Accept-Ranges", "bytes");
								MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Range: bytes ", seek_start, "-", seek_end, "/", fsize)));
								if(XVar.Pack(outputAsAttachment))
								{
									MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=\"", fileName, "\"")));
								}
								printContentLength = new XVar(true);
								if(XVar.Pack(printContentLength))
								{
									MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Length: ", (seek_end - seek_start) + 1)));
								}
								MVCFunctions.Header("Cache-Control", "cache, must-revalidate");
								MVCFunctions.Header("Pragma", "public");
								MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Type: ", ftype)));
								if(MVCFunctions.GetServerVariable("REQUEST_METHOD") == "HEAD")
								{
									MVCFunctions.Echo(new XVar(""));
									return MVCFunctions.GetBuferContentAndClearBufer();
								}
								if(XVar.Pack(isDBFile))
								{
									MVCFunctions.echoBinaryPartial((XVar)(value), (XVar)(seek_start), (XVar)(seek_end));
								}
								else
								{
									MVCFunctions.printfileByRange((XVar)(fsFileName), (XVar)(seek_start), (XVar)(seek_end));
								}
							}
						}
						break;
				}
				MVCFunctions.Echo(new XVar(""));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
