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
	public partial class CommonFunctions
	{
		public static XVar RunnerApply(dynamic obj, dynamic argsArr)
		{
			foreach (KeyValuePair<XVar, dynamic> var_var in argsArr.GetEnumerator())
			{
				MVCFunctions.setObjectProperty((XVar)(obj), (XVar)(var_var.Key), (XVar)(argsArr[var_var.Key]));
			}
			return null;
		}
		public static XVar GetImageFromDB(dynamic _param_gQuery_packed, dynamic _param_forPDF = null, dynamic _param_params = null)
		{
			#region packeted values
			SQLQuery _param_gQuery = XVar.UnPackSQLQuery(_param_gQuery_packed);
			#endregion

			#region default values
			if(_param_forPDF as Object == null) _param_forPDF = new XVar(false);
			if(_param_params as Object == null) _param_params = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			SQLQuery gQuery = XVar.Clone(_param_gQuery);
			dynamic forPDF = XVar.Clone(_param_forPDF);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic connection = null, data = XVar.Array(), field = null, keys = XVar.Array(), keysArr = XVar.Array(), secOpt = null, settings = null, sql = null, table = null, where = null;
			if(XVar.Pack(!(XVar)(forPDF)))
			{
				table = XVar.Clone(MVCFunctions.postvalue(new XVar("table")));
				GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(table)));
				settings = XVar.Clone(new ProjectSettings((XVar)(GlobalVars.strTableName)));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					return "";
				}
				if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
				{
					MVCFunctions.HeaderRedirect(new XVar("login"));
					return null;
				}
				field = XVar.Clone(MVCFunctions.postvalue(new XVar("field")));
				if(XVar.Pack(!(XVar)(settings.checkFieldPermissions((XVar)(field)))))
				{
					return CommonFunctions.DisplayNoImage();
				}
				keysArr = XVar.Clone(settings.getTableKeys());
				keys = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> k in keysArr.GetEnumerator())
				{
					keys.InitAndSetArrayItem(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("key", k.Key + 1))), k.Value);
				}
			}
			else
			{
				table = XVar.Clone(var_params["table"]);
				GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(table)));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(table)))))
				{
					MVCFunctions.ob_flush();
					HttpContext.Current.Response.End();
					throw new RunnerInlineOutputException();
				}
				settings = XVar.Clone(new ProjectSettings((XVar)(GlobalVars.strTableName)));
				field = XVar.Clone(var_params["field"]);
				keysArr = XVar.Clone(settings.getTableKeys());
				keys = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> k in keysArr.GetEnumerator())
				{
					keys.InitAndSetArrayItem(var_params[MVCFunctions.Concat("key", k.Key + 1)], k.Value);
				}
			}
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
			if(XVar.Pack(!(XVar)(gQuery.HasGroupBy())))
			{
				gQuery.RemoveAllFieldsExcept((XVar)(settings.getFieldIndex((XVar)(field))));
			}
			where = XVar.Clone(CommonFunctions.KeyWhere((XVar)(keys)));
			secOpt = XVar.Clone(settings.getAdvancedSecurityType());
			if(secOpt == Constants.ADVSECURITY_VIEW_OWN)
			{
				where = XVar.Clone(CommonFunctions.whereAdd((XVar)(where), (XVar)(CommonFunctions.SecuritySQL(new XVar("Search")))));
			}
			sql = XVar.Clone(gQuery.gSQLWhere((XVar)(where)));
			data = XVar.Clone(connection.query((XVar)(sql)).fetchAssoc());
			if(XVar.Pack(forPDF))
			{
				if(XVar.Pack(data))
				{
					return data[field];
				}
			}
			else
			{
				dynamic itype = null, pdf = null, value = null;
				if(XVar.Pack(!(XVar)(data)))
				{
					return CommonFunctions.DisplayNoImage();
				}
				if(MVCFunctions.postvalue(new XVar("src")) == 1)
				{
					value = XVar.Clone(MVCFunctions.myfile_get_contents(new XVar("images/icons/jpg.png")));
				}
				else
				{
					value = XVar.Clone(connection.stripSlashesBinary((XVar)(data[field])));
				}
				if(XVar.Pack(!(XVar)(value)))
				{
					if(XVar.Pack(MVCFunctions.postvalue(new XVar("alt"))))
					{
						value = XVar.Clone(connection.stripSlashesBinary((XVar)(data[MVCFunctions.postvalue(new XVar("alt"))])));
						if(XVar.Pack(!(XVar)(value)))
						{
							return CommonFunctions.DisplayNoImage();
						}
					}
					else
					{
						return CommonFunctions.DisplayNoImage();
					}
				}
				itype = XVar.Clone(MVCFunctions.SupposeImageType((XVar)(value)));
				if(XVar.Pack(!(XVar)(itype)))
				{
					return CommonFunctions.DisplayFile();
				}
				if(XVar.Pack(!(XVar)(pdf as object != null)))
				{
					MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Type: ", itype)));
					MVCFunctions.Header("Cache-Control", "private");
					MVCFunctions.SendContentLength((XVar)(MVCFunctions.strlen_bin((XVar)(value))));
				}
				MVCFunctions.echoBinary((XVar)(value));
				return "";
			}
			return null;
		}
		public static XVar redirectToLogin()
		{
			dynamic expired = null, url = null;
			expired = new XVar("");
			url = new XVar("http://");
			if((XVar)(MVCFunctions.GetServerVariable("HTTPS"))  && (XVar)(MVCFunctions.GetServerVariable("HTTPS") != "off"))
			{
				url = new XVar("https://");
			}
			url = MVCFunctions.Concat(url, MVCFunctions.GetServerVariable("HTTP_HOST"), MVCFunctions.GetServerVariable("REQUEST_URI"));
			if((XVar)(!(XVar)(GlobalVars.logoutPerformed))  && (XVar)(MVCFunctions.SERVERKeyExists("HTTP_REFERER")))
			{
				if((XVar)((XVar)(CommonFunctions.getDirectoryFromURI((XVar)(MVCFunctions.GetServerVariable("HTTP_REFERER"))) == CommonFunctions.getDirectoryFromURI((XVar)(url)))  && (XVar)(CommonFunctions.getFilenameFromURI((XVar)(MVCFunctions.GetServerVariable("HTTP_REFERER"))) != "index.htm"))  && (XVar)(MVCFunctions.GetServerVariable("HTTP_REFERER") != CommonFunctions.getDirectoryFromURI((XVar)(url))))
				{
					expired = new XVar("message=expired");
				}
			}
			if(XVar.Pack(!(XVar)(GlobalVars.logoutPerformed)))
			{
				expired = XVar.Clone(MVCFunctions.Concat("return=true&", expired));
			}
			MVCFunctions.HeaderRedirect(new XVar("login"), new XVar(""), (XVar)(expired));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public static XVar getDirectoryFromURI(dynamic _param_uri)
		{
			#region pass-by-value parameters
			dynamic uri = XVar.Clone(_param_uri);
			#endregion

			dynamic lastSlash = null, qMark = null;
			qMark = XVar.Clone(MVCFunctions.strpos((XVar)(uri), new XVar("?")));
			if(!XVar.Equals(XVar.Pack(qMark), XVar.Pack(false)))
			{
				uri = XVar.Clone(MVCFunctions.substr((XVar)(uri), new XVar(0), (XVar)(qMark)));
			}
			lastSlash = XVar.Clone(MVCFunctions.strrpos((XVar)(uri), new XVar("/")));
			if(!XVar.Equals(XVar.Pack(lastSlash), XVar.Pack(false)))
			{
				return MVCFunctions.Concat(MVCFunctions.substr((XVar)(uri), new XVar(0), (XVar)(lastSlash)), "/");
			}
			return uri;
		}
		public static XVar getFilenameFromURI(dynamic _param_uri)
		{
			#region pass-by-value parameters
			dynamic uri = XVar.Clone(_param_uri);
			#endregion

			dynamic lastSlash = null, qMark = null;
			qMark = XVar.Clone(MVCFunctions.strpos((XVar)(uri), new XVar("?")));
			if(!XVar.Equals(XVar.Pack(qMark), XVar.Pack(false)))
			{
				uri = XVar.Clone(MVCFunctions.substr((XVar)(uri), (XVar)(qMark)));
			}
			lastSlash = XVar.Clone(MVCFunctions.strrpos((XVar)(uri), new XVar("/")));
			if(!XVar.Equals(XVar.Pack(lastSlash), XVar.Pack(false)))
			{
				return MVCFunctions.substr((XVar)(uri), (XVar)(lastSlash + 1));
			}
			return uri;
		}
		public static XVar getLangFileName(dynamic _param_langName)
		{
			#region pass-by-value parameters
			dynamic langName = XVar.Clone(_param_langName);
			#endregion

			dynamic langArr = XVar.Array();
			langArr = XVar.Clone(XVar.Array());
			langArr.InitAndSetArrayItem("English", "English");
			return langArr[langName];
		}
		public static XVar GetGlobalData(dynamic _param_name, dynamic _param_defValue)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic defValue = XVar.Clone(_param_defValue);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.globalSettings.KeyExists(name))))
			{
				return defValue;
			}
			return GlobalVars.globalSettings[name];
		}
		public static XVar DisplayMap(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["addressField"]) ? XVar.Pack(var_params["addressField"]) : XVar.Pack("")), "mapsData", var_params["id"], "addressField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["latField"]) ? XVar.Pack(var_params["latField"]) : XVar.Pack("")), "mapsData", var_params["id"], "latField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["lngField"]) ? XVar.Pack(var_params["lngField"]) : XVar.Pack("")), "mapsData", var_params["id"], "lngField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["weightField"]) ? XVar.Pack(var_params["weightField"]) : XVar.Pack("")), "mapsData", var_params["id"], "weightField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["clustering"], "mapsData", var_params["id"], "clustering");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["heatMap"], "mapsData", var_params["id"], "heatMap");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar)((XVar)(var_params["showAllMarkers"])  || (XVar)(var_params["clustering"]))  || (XVar)(var_params["heatMap"]), "mapsData", var_params["id"], "showAllMarkers");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["width"]) ? XVar.Pack(var_params["width"]) : XVar.Pack(0)), "mapsData", var_params["id"], "width");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["height"]) ? XVar.Pack(var_params["height"]) : XVar.Pack(0)), "mapsData", var_params["id"], "height");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem("BIG_MAP", "mapsData", var_params["id"], "type");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["showCenterLink"]) ? XVar.Pack(var_params["showCenterLink"]) : XVar.Pack(0)), "mapsData", var_params["id"], "showCenterLink");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["descField"]) ? XVar.Pack(var_params["descField"]) : XVar.Pack(GlobalVars.pageObject.googleMapCfg["mapsData"][var_params["id"]]["addressField"])), "mapsData", var_params["id"], "descField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["description"]) ? XVar.Pack(var_params["description"]) : XVar.Pack(GlobalVars.pageObject.googleMapCfg["mapsData"][var_params["id"]]["addressField"])), "mapsData", var_params["id"], "descField");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["markerAsEditLink"], "mapsData", var_params["id"], "markerAsEditLink");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["markerIcon"]) ? XVar.Pack(var_params["markerIcon"]) : XVar.Pack("")), "mapsData", var_params["id"], "markerIcon");
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["markerField"]) ? XVar.Pack(var_params["markerField"]) : XVar.Pack("")), "mapsData", var_params["id"], "markerField");
			if(XVar.Pack(var_params.KeyExists("zoom")))
			{
				GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["zoom"], "mapsData", var_params["id"], "zoom");
			}
			if(XVar.Pack(GlobalVars.pageObject.googleMapCfg["mapsData"][var_params["id"]]["showCenterLink"]))
			{
				GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem((XVar.Pack(var_params["centerLinkText"]) ? XVar.Pack(var_params["centerLinkText"]) : XVar.Pack("")), "mapsData", var_params["id"], "centerLinkText");
			}
			GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["id"], "mainMapIds", null);
			if(XVar.Pack(var_params.KeyExists("APIkey")))
			{
				GlobalVars.pageObject.googleMapCfg.InitAndSetArrayItem(var_params["APIkey"], "APIcode");
			}
			return null;
		}
		public static XVar checkTableName(dynamic _param_shortTName, dynamic _param_type = null)
		{
			#region default values
			if(_param_type as Object == null) _param_type = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic shortTName = XVar.Clone(_param_shortTName);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(XVar.Pack(!(XVar)(shortTName)))
			{
				return false;
			}
			if((XVar)("dbo__ABCReports" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 0))))
			{
				return true;
			}
			if((XVar)("dbo__ABCVotes" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 0))))
			{
				return true;
			}
			if((XVar)("dbo__ABCSecurity" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 0))))
			{
				return true;
			}
			if((XVar)("ABC_Voting_Submitted1" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 1))))
			{
				return true;
			}
			if((XVar)("ABC_Voting_Recirculated1" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 1))))
			{
				return true;
			}
			if((XVar)("ABC_Voting_My_Voting" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 1))))
			{
				return true;
			}
			if((XVar)("ABC_Voting_Batch_Create" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 1))))
			{
				return true;
			}
			if((XVar)("dbo_vwABCReportsVoteCount" == shortTName)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(var_type), XVar.Pack(false)))  && (XVar)(var_type == 0))))
			{
				return true;
			}
			return false;
		}
		public static XVar GetPasswordField(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.cPasswordField;
		}
		public static XVar GetUserNameField(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.cUserNameField;
		}
		public static XVar GetDisplayNameField(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.cDisplayNameField;
		}
		public static XVar GetEmailField(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.cEmailField;
		}
		public static XVar GetTablesList(dynamic _param_pdfMode = null)
		{
			#region default values
			if(_param_pdfMode as Object == null) _param_pdfMode = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic pdfMode = XVar.Clone(_param_pdfMode);
			#endregion

			dynamic arr = XVar.Array(), strPerm = null;
			arr = XVar.Clone(XVar.Array());
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("dbo._ABCReports")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("dbo._ABCReports", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("dbo._ABCVotes")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("dbo._ABCVotes", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("dbo._ABCSecurity")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("dbo._ABCSecurity", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("ABC_Voting_Submitted")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("ABC_Voting_Submitted", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("ABC_Voting_Recirculated")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("ABC_Voting_Recirculated", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("ABC_Voting_My_Voting")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("ABC_Voting_My_Voting", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("ABC_Voting_Batch_Create")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("ABC_Voting_Batch_Create", null);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions(new XVar("dbo.vwABCReportsVoteCount")));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)))  || (XVar)((XVar)(pdfMode)  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))))
			{
				arr.InitAndSetArrayItem("dbo.vwABCReportsVoteCount", null);
			}
			return arr;
		}
		public static XVar GetTablesListWithoutSecurity()
		{
			dynamic arr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			arr.InitAndSetArrayItem("dbo._ABCReports", null);
			arr.InitAndSetArrayItem("dbo._ABCVotes", null);
			arr.InitAndSetArrayItem("dbo._ABCSecurity", null);
			arr.InitAndSetArrayItem("ABC_Voting_Submitted", null);
			arr.InitAndSetArrayItem("ABC_Voting_Recirculated", null);
			arr.InitAndSetArrayItem("ABC_Voting_My_Voting", null);
			arr.InitAndSetArrayItem("ABC_Voting_Batch_Create", null);
			arr.InitAndSetArrayItem("dbo.vwABCReportsVoteCount", null);
			return arr;
		}
		public static XVar GetFullFieldName(dynamic _param_field, dynamic _param_table = null, dynamic _param_addAs = null, dynamic _param_connection = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_addAs as Object == null) _param_addAs = new XVar(true);
			if(_param_connection as Object == null) _param_connection = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic table = XVar.Clone(_param_table);
			dynamic addAs = XVar.Clone(_param_addAs);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			dynamic fname = null;
			ProjectSettings pSet;
			if(table == XVar.Pack(""))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			if(XVar.Pack(!(XVar)(connection)))
			{
				connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(table)));
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			fname = XVar.Clone(RunnerPage._getFieldSQL((XVar)(field), (XVar)(connection), (XVar)(pSet)));
			if((XVar)(pSet.hasEncryptedFields())  && (XVar)(!(XVar)(connection.isEncryptionByPHPEnabled())))
			{
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(table)));
				return MVCFunctions.Concat(GlobalVars.cipherer.GetFieldName((XVar)(fname), (XVar)(field)), (XVar.Pack((XVar)(GlobalVars.cipherer.isFieldEncrypted((XVar)(field)))  && (XVar)(addAs)) ? XVar.Pack(MVCFunctions.Concat(" as ", connection.addFieldWrappers((XVar)(field)))) : XVar.Pack("")));
			}
			return fname;
		}
		public static XVar GetChartType(dynamic _param_shorttable)
		{
			#region pass-by-value parameters
			dynamic shorttable = XVar.Clone(_param_shorttable);
			#endregion

			return "";
		}
		public static XVar GetShorteningForLargeText(dynamic _param_strValue, dynamic _param_cNumberOfChars)
		{
			#region pass-by-value parameters
			dynamic strValue = XVar.Clone(_param_strValue);
			dynamic cNumberOfChars = XVar.Clone(_param_cNumberOfChars);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(MVCFunctions.runner_substr((XVar)(strValue), new XVar(0), (XVar)(cNumberOfChars)));
			return MVCFunctions.runner_htmlspecialchars((XVar)(ret));
		}
		public static XVar AddLinkPrefix(dynamic _param_pSet_packed, dynamic _param_field, dynamic _param_link)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic field = XVar.Clone(_param_field);
			dynamic link = XVar.Clone(_param_link);
			#endregion

			if((XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(link), new XVar("://"))), XVar.Pack(false)))  && (XVar)(MVCFunctions.substr((XVar)(link), new XVar(0), new XVar(7)) != "mailto:"))
			{
				return MVCFunctions.Concat(pSet.getLinkPrefix((XVar)(field)), link);
			}
			return link;
		}
		public static XVar GetTotalsForTime(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic time = XVar.Array();
			time = XVar.Clone(CommonFunctions.parsenumbers((XVar)(value)));
			while(MVCFunctions.count(time) < 3)
			{
				time.InitAndSetArrayItem(0, null);
			}
			return time;
		}
		public static XVar GetTotals(dynamic _param_field, dynamic _param_value, dynamic _param_stype, dynamic _param_iNumberOfRows, dynamic _param_sFormat, dynamic _param_ptype, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic stype = XVar.Clone(_param_stype);
			dynamic iNumberOfRows = XVar.Clone(_param_iNumberOfRows);
			dynamic sFormat = XVar.Clone(_param_sFormat);
			dynamic ptype = XVar.Clone(_param_ptype);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic d = null, data = null, days = null, h = null, m = null, s = null, sValue = null;
			days = new XVar(0);
			if(stype == "AVERAGE")
			{
				if(XVar.Pack(iNumberOfRows))
				{
					if(sFormat == Constants.FORMAT_TIME)
					{
						if(XVar.Pack(value))
						{
							value = XVar.Clone((XVar)Math.Round((double)(value / iNumberOfRows), 0));
							s = XVar.Clone(value  %  60);
							value -= s;
							value /= 60;
							m = XVar.Clone(value  %  60);
							value -= m;
							value /= 60;
							h = XVar.Clone(value  %  24);
							value -= h;
							value /= 24;
							d = XVar.Clone(value);
							value = XVar.Clone(MVCFunctions.Concat((XVar.Pack(d != XVar.Pack(0)) ? XVar.Pack(MVCFunctions.Concat(d, "d ")) : XVar.Pack("")), MVCFunctions.mysprintf(new XVar("%02d:%02d:%02d"), (XVar)(new XVar(0, h, 1, m, 2, s)))));
						}
					}
					else
					{
						value = XVar.Clone((XVar)Math.Round((double)(value / iNumberOfRows), 2));
					}
				}
				else
				{
					return "";
				}
			}
			if(stype == "TOTAL")
			{
				if(sFormat == Constants.FORMAT_TIME)
				{
					if(XVar.Pack(value))
					{
						s = XVar.Clone(value  %  60);
						value -= s;
						value /= 60;
						m = XVar.Clone(value  %  60);
						value -= m;
						value /= 60;
						h = XVar.Clone(value  %  24);
						value -= h;
						value /= 24;
						d = XVar.Clone(value);
						value = XVar.Clone(MVCFunctions.Concat((XVar.Pack(d != XVar.Pack(0)) ? XVar.Pack(MVCFunctions.Concat(d, "d ")) : XVar.Pack("")), MVCFunctions.mysprintf(new XVar("%02d:%02d:%02d"), (XVar)(new XVar(0, h, 1, m, 2, s)))));
					}
				}
			}
			sValue = new XVar("");
			data = XVar.Clone(new XVar(field, value));
			if(sFormat == Constants.FORMAT_CURRENCY)
			{
				sValue = XVar.Clone(CommonFunctions.str_format_currency((XVar)(value)));
			}
			else
			{
				if(sFormat == Constants.FORMAT_PERCENT)
				{
					sValue = XVar.Clone(MVCFunctions.Concat(CommonFunctions.str_format_number((XVar)(value * 100)), "%"));
				}
				else
				{
					if(sFormat == Constants.FORMAT_NUMBER)
					{
						sValue = XVar.Clone(CommonFunctions.str_format_number((XVar)(value), (XVar)(pSet.isDecimalDigits((XVar)(field)))));
					}
					else
					{
						if((XVar)(sFormat == Constants.FORMAT_CUSTOM)  && (XVar)(stype != "COUNT"))
						{
							dynamic viewControls = null;
							viewControls = XVar.Clone(new ViewControlsContainer((XVar)(pSet), (XVar)(ptype)));
							sValue = XVar.Clone(viewControls.showDBValue((XVar)(field), (XVar)(data)));
						}
						else
						{
							sValue = XVar.Clone(value);
						}
					}
				}
			}
			if(stype == "COUNT")
			{
				return value;
			}
			if(stype == "TOTAL")
			{
				return sValue;
			}
			if(stype == "AVERAGE")
			{
				return sValue;
			}
			return "";
		}
		public static XVar DisplayNoImage()
		{
			dynamic path = null;
			path = XVar.Clone(MVCFunctions.getabspath(new XVar("images/no_image.gif")));
			MVCFunctions.Header("Content-Type", "image/gif");
			MVCFunctions.printfile((XVar)(path));
			return null;
		}
		public static XVar DisplayFile()
		{
			dynamic path = null;
			path = XVar.Clone(MVCFunctions.getabspath(new XVar("images/file.gif")));
			MVCFunctions.Header("Content-Type", "image/gif");
			MVCFunctions.printfile((XVar)(path));
			return null;
		}
		public static XVar my_strrpos(dynamic _param_haystack, dynamic _param_needle)
		{
			#region pass-by-value parameters
			dynamic haystack = XVar.Clone(_param_haystack);
			dynamic needle = XVar.Clone(_param_needle);
			#endregion

			dynamic index = null;
			index = XVar.Clone(MVCFunctions.strpos((XVar)(MVCFunctions.strrev((XVar)(haystack))), (XVar)(MVCFunctions.strrev((XVar)(needle)))));
			if(XVar.Equals(XVar.Pack(index), XVar.Pack(false)))
			{
				return false;
			}
			index = XVar.Clone((MVCFunctions.strlen((XVar)(haystack)) - MVCFunctions.strlen((XVar)(needle))) - index);
			return index;
		}
		public static XVar jsreplace(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, "\\", 1, "'", 2, "\r", 3, "\n")), (XVar)(new XVar(0, "\\\\", 1, "\\'", 2, "\\r", 3, "\\n")), (XVar)(str)));
			return CommonFunctions.my_str_ireplace(new XVar("</script>"), new XVar("</scr'+'ipt>"), (XVar)(ret));
		}
		public static XVar LogInfo(dynamic _param_SQL)
		{
			#region pass-by-value parameters
			dynamic SQL = XVar.Clone(_param_SQL);
			#endregion

			return null;
		}
		public static XVar CheckImageExtension(dynamic _param_filename)
		{
			#region pass-by-value parameters
			dynamic filename = XVar.Clone(_param_filename);
			#endregion

			dynamic ext = null;
			if(MVCFunctions.strlen((XVar)(filename)) < 4)
			{
				return false;
			}
			ext = XVar.Clone(MVCFunctions.strtoupper((XVar)(MVCFunctions.substr((XVar)(filename), (XVar)(MVCFunctions.strlen((XVar)(filename)) - 4)))));
			if((XVar)((XVar)((XVar)((XVar)(ext == ".GIF")  || (XVar)(ext == ".JPG"))  || (XVar)(ext == "JPEG"))  || (XVar)(ext == ".PNG"))  || (XVar)(ext == ".BMP"))
			{
				return ext;
			}
			return false;
		}
		public static XVar html_special_decode(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(str);
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&gt;"), new XVar(">"), (XVar)(ret)));
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&lt;"), new XVar("<"), (XVar)(ret)));
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&quot;"), new XVar("\""), (XVar)(ret)));
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&#039;"), new XVar("'"), (XVar)(ret)));
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&#39;"), new XVar("'"), (XVar)(ret)));
			ret = XVar.Clone(MVCFunctions.str_replace(new XVar("&amp;"), new XVar("&"), (XVar)(ret)));
			return ret;
		}
		public static XVar whereAdd(dynamic _param_where, dynamic _param_clause)
		{
			#region pass-by-value parameters
			dynamic where = XVar.Clone(_param_where);
			dynamic clause = XVar.Clone(_param_clause);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(clause)))))
			{
				return where;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(where)))))
			{
				return clause;
			}
			return MVCFunctions.Concat("(", where, ") and (", clause, ")");
		}
		public static XVar combineSQLCriteria(dynamic _param_arrElements, dynamic _param_and = null)
		{
			#region default values
			if(_param_and as Object == null) _param_and = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic arrElements = XVar.Clone(_param_arrElements);
			dynamic var_and = XVar.Clone(_param_and);
			#endregion

			dynamic filteredCriteria = XVar.Array(), union = null;
			filteredCriteria = XVar.Clone(XVar.Array());
			union = XVar.Clone((XVar.Pack(var_and) ? XVar.Pack(" AND ") : XVar.Pack(" OR ")));
			foreach (KeyValuePair<XVar, dynamic> e in arrElements.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(e.Value))))
				{
					filteredCriteria.InitAndSetArrayItem(MVCFunctions.Concat("( ", e.Value, " )"), null);
				}
			}
			return MVCFunctions.implode((XVar)(union), (XVar)(filteredCriteria));
		}
		public static XVar sqlMoreThan(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(value == "null")
			{
				return MVCFunctions.Concat(field, " is not null");
			}
			return MVCFunctions.Concat(field, " > ", value);
		}
		public static XVar sqlLessThan(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(value == "null")
			{
				return "";
			}
			return MVCFunctions.Concat(field, " < ", value, " or ", field, " is null");
		}
		public static XVar sqlEqual(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(value == "null")
			{
				return MVCFunctions.Concat(value, " is null");
			}
			return MVCFunctions.Concat(field, " = ", value);
		}
		public static XVar AddWhere(dynamic _param_sql, dynamic _param_where)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic where = XVar.Clone(_param_where);
			#endregion

			dynamic n = null, n1 = null, n2 = null, tsql = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(where)))))
			{
				return sql;
			}
			sql = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, "\r\n", 1, "\n", 2, "\t")), new XVar(" "), (XVar)(sql)));
			tsql = XVar.Clone(MVCFunctions.strtolower((XVar)(sql)));
			n = XVar.Clone(CommonFunctions.my_strrpos((XVar)(tsql), new XVar(" where ")));
			n1 = XVar.Clone(CommonFunctions.my_strrpos((XVar)(tsql), new XVar(" group by ")));
			n2 = XVar.Clone(CommonFunctions.my_strrpos((XVar)(tsql), new XVar(" order by ")));
			if(XVar.Equals(XVar.Pack(n1), XVar.Pack(false)))
			{
				n1 = XVar.Clone(MVCFunctions.strlen((XVar)(tsql)));
			}
			if(XVar.Equals(XVar.Pack(n2), XVar.Pack(false)))
			{
				n2 = XVar.Clone(MVCFunctions.strlen((XVar)(tsql)));
			}
			if(n2 < n1)
			{
				n1 = XVar.Clone(n2);
			}
			if(XVar.Equals(XVar.Pack(n), XVar.Pack(false)))
			{
				return MVCFunctions.Concat(MVCFunctions.substr((XVar)(sql), new XVar(0), (XVar)(n1)), " where ", where, MVCFunctions.substr((XVar)(sql), (XVar)(n1)));
			}
			else
			{
				return MVCFunctions.Concat(MVCFunctions.substr((XVar)(sql), new XVar(0), (XVar)(n + MVCFunctions.strlen(new XVar(" where ")))), "(", MVCFunctions.substr((XVar)(sql), (XVar)(n + MVCFunctions.strlen(new XVar(" where "))), (XVar)((n1 - n) - MVCFunctions.strlen(new XVar(" where ")))), ") and (", where, ")", MVCFunctions.substr((XVar)(sql), (XVar)(n1)));
			}
			return null;
		}
		public static XVar KeyWhere(dynamic keys, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic connection = null, keyFields = XVar.Array(), strWhere = null;
			ProjectSettings pSet;
			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			strWhere = new XVar("");
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(table)));
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(table)));
			keyFields = XVar.Clone(pSet.getTableKeys());
			foreach (KeyValuePair<XVar, dynamic> kf in keyFields.GetEnumerator())
			{
				dynamic value = null, valueisnull = null;
				if(XVar.Pack(MVCFunctions.strlen((XVar)(strWhere))))
				{
					strWhere = MVCFunctions.Concat(strWhere, " and ");
				}
				value = XVar.Clone(GlobalVars.cipherer.MakeDBValue((XVar)(kf.Value), (XVar)(keys[kf.Value]), new XVar(""), new XVar(true)));
				if(connection.dbType == Constants.nDATABASE_Oracle)
				{
					valueisnull = XVar.Clone((XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack("null")))  || (XVar)(value == "''"));
				}
				else
				{
					valueisnull = XVar.Clone(XVar.Equals(XVar.Pack(value), XVar.Pack("null")));
				}
				if(XVar.Pack(valueisnull))
				{
					strWhere = MVCFunctions.Concat(strWhere, RunnerPage._getFieldSQL((XVar)(kf.Value), (XVar)(connection), (XVar)(pSet)), " is null");
				}
				else
				{
					strWhere = MVCFunctions.Concat(strWhere, RunnerPage._getFieldSQLDecrypt((XVar)(kf.Value), (XVar)(connection), (XVar)(pSet), (XVar)(GlobalVars.cipherer)), "=", GlobalVars.cipherer.MakeDBValue((XVar)(kf.Value), (XVar)(keys[kf.Value]), new XVar(""), new XVar(true)));
				}
			}
			return strWhere;
		}
		public static XVar GetRowCount(dynamic _param_strSQL, dynamic _param_connection)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			dynamic countdata = XVar.Array(), countstr = null, ind1 = null, ind2 = null, ind3 = null, tstr = null;
			strSQL = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, "\r\n", 1, "\n", 2, "\t")), new XVar(" "), (XVar)(strSQL)));
			tstr = XVar.Clone(MVCFunctions.strtoupper((XVar)(strSQL)));
			ind1 = XVar.Clone(MVCFunctions.strpos((XVar)(tstr), new XVar("SELECT ")));
			ind2 = XVar.Clone(CommonFunctions.my_strrpos((XVar)(tstr), new XVar(" FROM ")));
			ind3 = XVar.Clone(CommonFunctions.my_strrpos((XVar)(tstr), new XVar(" GROUP BY ")));
			if(XVar.Equals(XVar.Pack(ind3), XVar.Pack(false)))
			{
				ind3 = XVar.Clone(MVCFunctions.strpos((XVar)(tstr), new XVar(" ORDER BY ")));
				if(XVar.Equals(XVar.Pack(ind3), XVar.Pack(false)))
				{
					ind3 = XVar.Clone(MVCFunctions.strlen((XVar)(strSQL)));
				}
			}
			countstr = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(strSQL), new XVar(0), (XVar)(ind1 + 6)), " count(*) ", MVCFunctions.substr((XVar)(strSQL), (XVar)(ind2 + 1), (XVar)(ind3 - ind2))));
			countdata = XVar.Clone(connection.query((XVar)(countstr)).fetchNumeric());
			return countdata[0];
		}
		public static XVar AddTop(dynamic _param_strSQL, dynamic _param_n)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			dynamic n = XVar.Clone(_param_n);
			#endregion

			dynamic matches = XVar.Array(), pattern = null;
			pattern = new XVar("/(^\\s*select\\s+distinct\\s+)|(^\\s*select\\s+)/i");
			matches = XVar.Clone(XVar.Array());
			MVCFunctions.preg_match_all((XVar)(pattern), (XVar)(strSQL), (XVar)(matches));
			if(XVar.Pack(matches[0]))
			{
				return MVCFunctions.Concat(matches[0][0], "top ", n, " ", MVCFunctions.substr((XVar)(strSQL), (XVar)(MVCFunctions.strlen((XVar)(matches[0][0])))));
			}
			return strSQL;
		}
		public static XVar AddTopDB2(dynamic _param_strSQL, dynamic _param_n)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			dynamic n = XVar.Clone(_param_n);
			#endregion

			return MVCFunctions.Concat(strSQL, " fetch first ", n, " rows only");
		}
		public static XVar AddTopIfx(dynamic _param_strSQL, dynamic _param_n)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			dynamic n = XVar.Clone(_param_n);
			#endregion

			return MVCFunctions.Concat(MVCFunctions.substr((XVar)(strSQL), new XVar(0), new XVar(7)), "limit ", n, " ", MVCFunctions.substr((XVar)(strSQL), new XVar(7)));
		}
		public static XVar AddRowNumber(dynamic _param_strSQL, dynamic _param_n)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			dynamic n = XVar.Clone(_param_n);
			#endregion

			return MVCFunctions.Concat("select * from (", strSQL, ") where rownum<", n + 1);
		}
		public static XVar applyDBrecordLimit(dynamic _param_sql, dynamic _param_N, dynamic _param_dbType)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic N = XVar.Clone(_param_N);
			dynamic dbType = XVar.Clone(_param_dbType);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(dbType)))))
			{
				return sql;
			}
			if((XVar)((XVar)(dbType == Constants.nDATABASE_MySQL)  || (XVar)(dbType == Constants.nDATABASE_PostgreSQL))  || (XVar)(dbType == Constants.nDATABASE_SQLite3))
			{
				return MVCFunctions.Concat(sql, " LIMIT ", N);
			}
			if(dbType == Constants.nDATABASE_Oracle)
			{
				return CommonFunctions.AddRowNumber((XVar)(sql), (XVar)(N));
			}
			if((XVar)(dbType == Constants.nDATABASE_MSSQLServer)  || (XVar)(dbType == Constants.nDATABASE_Access))
			{
				return CommonFunctions.AddTop((XVar)(sql), (XVar)(N));
			}
			if(dbType == Constants.nDATABASE_Informix)
			{
				return CommonFunctions.AddTopIfx((XVar)(sql), (XVar)(N));
			}
			if(dbType == Constants.nDATABASE_DB2)
			{
				return CommonFunctions.AddTopDB2((XVar)(sql), (XVar)(N));
			}
			return sql;
		}
		public static XVar NeedQuotesNumeric(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(var_type == 203)  || (XVar)(var_type == 8))  || (XVar)(var_type == 129))  || (XVar)(var_type == 130))  || (XVar)(var_type == 7))  || (XVar)(var_type == 133))  || (XVar)(var_type == 134))  || (XVar)(var_type == 135))  || (XVar)(var_type == 201))  || (XVar)(var_type == 205))  || (XVar)(var_type == 200))  || (XVar)(var_type == 202))  || (XVar)(var_type == 72))  || (XVar)(var_type == 13))
			{
				return true;
			}
			else
			{
				return false;
			}
			return null;
		}
		public static XVar IsNumberType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(var_type == 20)  || (XVar)(var_type == 14))  || (XVar)(var_type == 5))  || (XVar)(var_type == 10))  || (XVar)(var_type == 6))  || (XVar)(var_type == 3))  || (XVar)(var_type == 131))  || (XVar)(var_type == 4))  || (XVar)(var_type == 2))  || (XVar)(var_type == 16))  || (XVar)(var_type == 21))  || (XVar)(var_type == 19))  || (XVar)(var_type == 18))  || (XVar)(var_type == 17))  || (XVar)(var_type == 139))  || (XVar)(var_type == 11))
			{
				return true;
			}
			return false;
		}
		public static XVar IsFloatType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)((XVar)((XVar)(var_type == 14)  || (XVar)(var_type == 5))  || (XVar)(var_type == 131))  || (XVar)(var_type == 4))  || (XVar)(var_type == 6))
			{
				return true;
			}
			return false;
		}
		public static XVar NeedQuotes(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			return !(XVar)(CommonFunctions.IsNumberType((XVar)(var_type)));
		}
		public static XVar IsBinaryType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)(var_type == 128)  || (XVar)(var_type == 205))  || (XVar)(var_type == 204))
			{
				return true;
			}
			return false;
		}
		public static XVar IsDateFieldType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)(var_type == 7)  || (XVar)(var_type == 133))  || (XVar)(var_type == 135))
			{
				return true;
			}
			return false;
		}
		public static XVar IsDateTimeFieldType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(var_type == 135)
			{
				return true;
			}
			return false;
		}
		public static XVar IsTimeType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(var_type == 134)
			{
				return true;
			}
			return false;
		}
		public static XVar IsCharType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)((XVar)((XVar)((XVar)((XVar)(CommonFunctions.IsTextType((XVar)(var_type)))  || (XVar)(var_type == 8))  || (XVar)(var_type == 129))  || (XVar)(var_type == 200))  || (XVar)(var_type == 202))  || (XVar)(var_type == 130))
			{
				return true;
			}
			return false;
		}
		public static XVar IsTextType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if((XVar)(var_type == 201)  || (XVar)(var_type == 203))
			{
				return true;
			}
			return false;
		}
		public static XVar IsGuid(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(var_type == 72)
			{
				return true;
			}
			return false;
		}
		public static XVar IsBigInt(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(var_type == 20)
			{
				return true;
			}
			return false;
		}
		public static XVar GetUserPermissionsStatic(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic extraPerm = null, sUserGroup = null;
			if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
			{
				return "";
			}
			extraPerm = XVar.Clone((XVar.Pack(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP) ? XVar.Pack("M") : XVar.Pack("")));
			sUserGroup = XVar.Clone(XSession.Session["GroupID"]);
			if((XVar)(table == "dbo._ABCReports")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "dbo._ABCReports")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("S", extraPerm);
			}
			if((XVar)(table == "dbo._ABCReports")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if(table == "dbo._ABCReports")
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if((XVar)(table == "dbo._ABCVotes")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "dbo._ABCVotes")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("S", extraPerm);
			}
			if((XVar)(table == "dbo._ABCVotes")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("AESP", extraPerm);
			}
			if(table == "dbo._ABCVotes")
			{
				return MVCFunctions.Concat("AESP", extraPerm);
			}
			if((XVar)(table == "dbo._ABCSecurity")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "dbo._ABCSecurity")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "dbo._ABCSecurity")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if(table == "dbo._ABCSecurity")
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Submitted")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Submitted")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("S", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Submitted")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if(table == "ABC_Voting_Submitted")
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Recirculated")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Recirculated")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("S", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Recirculated")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if(table == "ABC_Voting_Recirculated")
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_My_Voting")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_My_Voting")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_My_Voting")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if(table == "ABC_Voting_My_Voting")
			{
				return MVCFunctions.Concat("SP", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Batch_Create")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Batch_Create")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "ABC_Voting_Batch_Create")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if(table == "ABC_Voting_Batch_Create")
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "dbo.vwABCReportsVoteCount")  && (XVar)(sUserGroup == "admin"))
			{
				return MVCFunctions.Concat("AEDSPI", extraPerm);
			}
			if((XVar)(table == "dbo.vwABCReportsVoteCount")  && (XVar)(sUserGroup == "readonly"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if((XVar)(table == "dbo.vwABCReportsVoteCount")  && (XVar)(sUserGroup == "member"))
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			if(table == "dbo.vwABCReportsVoteCount")
			{
				return MVCFunctions.Concat("", extraPerm);
			}
			return "";
		}
		public static XVar IsAdmin()
		{
			return false;
			return null;
		}
		public static XVar GetUserPermissions(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic permissions = null;
			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			permissions = new XVar("");
			if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
			{
				return "";
			}
			if(XVar.Pack(MVCFunctions.is_array((XVar)(XSession.Session["securityOverrides"]))))
			{
				if(XVar.Pack(XSession.Session["securityOverrides"].KeyExists(table)))
				{
					return XSession.Session["securityOverrides"][table];
				}
			}
			permissions = XVar.Clone(CommonFunctions.GetUserPermissionsStatic((XVar)(table)));
			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("GetTablePermissions"), (XVar)(table))))
			{
				permissions = XVar.Clone(GlobalVars.globalEvents.GetTablePermissions((XVar)(permissions), (XVar)(table)));
			}
			return permissions;
		}
		public static XVar isLogged()
		{
			if(XVar.Pack(XSession.Session["UserID"]))
			{
				return true;
			}
			return false;
		}
		public static XVar guestHasPermissions()
		{
			dynamic tables = XVar.Array();
			tables = XVar.Clone(CommonFunctions.GetTablesListWithoutSecurity());
			return false;
			return null;
		}
		public static XVar AfterFBLogIn(dynamic _param_pUsername, dynamic _param_pPassword, dynamic _param_pDisplayUsername, dynamic pageObject = null)
		{
			#region default values
			if(pageObject as Object == null) pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic pUsername = XVar.Clone(_param_pUsername);
			dynamic pPassword = XVar.Clone(_param_pPassword);
			dynamic pDisplayUsername = XVar.Clone(_param_pDisplayUsername);
			#endregion

			dynamic connection = null, data = null, strUsername = null;
			connection = XVar.Clone(GlobalVars.cman.getForLogin());
			strUsername = XVar.Clone(pUsername);
			if(XVar.Pack(CommonFunctions.NeedQuotes((XVar)(GlobalVars.cUserNameFieldType))))
			{
				strUsername = XVar.Clone(connection.prepareString((XVar)(strUsername)));
			}
			else
			{
				strUsername = XVar.Clone(0 + strUsername);
			}
			GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("select * from ", connection.addTableWrappers(new XVar("dbo._ABCSecurity")), " where ", connection.addFieldWrappers((XVar)(GlobalVars.cUserNameField)), "=", strUsername, ""));
			data = XVar.Clone(connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
			if(XVar.Pack(MVCFunctions.count(data)))
			{
				CommonFunctions.DoLogin(new XVar(false), (XVar)(pUsername), (XVar)(pDisplayUsername), new XVar(""), new XVar(Constants.ACCESS_LEVEL_USER), (XVar)(pPassword), (XVar)(pageObject));
				CommonFunctions.SetAuthSessionData((XVar)(pUsername), (XVar)(data), new XVar(true), (XVar)(pPassword), (XVar)(pageObject));
			}
			return null;
		}
		public static XVar SetAuthSessionData(dynamic _param_pUsername, dynamic data, dynamic _param_fromFacebook, dynamic _param_password, dynamic pageObject = null, dynamic _param_fireEvents = null)
		{
			#region default values
			if(pageObject as Object == null) pageObject = new XVar();
			if(_param_fireEvents as Object == null) _param_fireEvents = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic pUsername = XVar.Clone(_param_pUsername);
			dynamic fromFacebook = XVar.Clone(_param_fromFacebook);
			dynamic password = XVar.Clone(_param_password);
			dynamic fireEvents = XVar.Clone(_param_fireEvents);
			#endregion

			dynamic cAdminUserID = null;
			XSession.Session["GroupID"] = data["role"];
			if(XSession.Session["GroupID"] == "admin")
			{
				XSession.Session["AccessLevel"] = Constants.ACCESS_LEVEL_ADMINGROUP;
			}
			XSession.Session["OwnerID"] = data["username"];
			XSession.Session["_dbo._ABCVotes_OwnerID"] = data["username"];
			XSession.Session["UserData"] = data;
			if((XVar)(fireEvents)  && (XVar)(GlobalVars.globalEvents.exists(new XVar("AfterSuccessfulLogin"))))
			{
				GlobalVars.globalEvents.AfterSuccessfulLogin((XVar)((XVar.Pack(pUsername != "Guest") ? XVar.Pack(pUsername) : XVar.Pack(""))), (XVar)(password), (XVar)(data), (XVar)(pageObject));
			}
			return null;
		}
		public static XVar DoLogin(dynamic _param_callAfterLoginEvent = null, dynamic _param_userID = null, dynamic _param_userName = null, dynamic _param_groupID = null, dynamic _param_accessLevel = null, dynamic _param_password = null, dynamic pageObject = null)
		{
			#region default values
			if(_param_callAfterLoginEvent as Object == null) _param_callAfterLoginEvent = new XVar(false);
			if(_param_userID as Object == null) _param_userID = new XVar("Guest");
			if(_param_userName as Object == null) _param_userName = new XVar("");
			if(_param_groupID as Object == null) _param_groupID = new XVar("<Guest>");
			if(_param_accessLevel as Object == null) _param_accessLevel = new XVar(Constants.ACCESS_LEVEL_GUEST);
			if(_param_password as Object == null) _param_password = new XVar("");
			if(pageObject as Object == null) pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic callAfterLoginEvent = XVar.Clone(_param_callAfterLoginEvent);
			dynamic userID = XVar.Clone(_param_userID);
			dynamic userName = XVar.Clone(_param_userName);
			dynamic groupID = XVar.Clone(_param_groupID);
			dynamic accessLevel = XVar.Clone(_param_accessLevel);
			dynamic password = XVar.Clone(_param_password);
			#endregion

			dynamic auditObj = null;
			if((XVar)(userID == "Guest")  && (XVar)(userName == XVar.Pack("")))
			{
				userName = new XVar("Guest");
			}
			if((XVar)(!(XVar)(CommonFunctions.GetGlobalData(new XVar("bTwoFactorAuth"), new XVar(false))))  || (XVar)(userID == "Guest"))
			{
				XSession.Session["UserID"] = userID;
				XSession.Session["UserName"] = MVCFunctions.runner_htmlspecialchars((XVar)(userName));
				XSession.Session["GroupID"] = groupID;
				XSession.Session["AccessLevel"] = accessLevel;
			}
			auditObj = XVar.Clone(CommonFunctions.GetAuditObject());
			if(XVar.Pack(auditObj))
			{
				auditObj.LogLogin((XVar)(userID));
				if(userID != "Guest")
				{
					auditObj.LoginSuccessful();
				}
			}
			if((XVar)(callAfterLoginEvent)  && (XVar)(GlobalVars.globalEvents.exists(new XVar("AfterSuccessfulLogin"))))
			{
				dynamic dummy = null;
				dummy = XVar.Clone(XVar.Array());
				GlobalVars.globalEvents.AfterSuccessfulLogin((XVar)((XVar.Pack(userID != "Guest") ? XVar.Pack(userID) : XVar.Pack(""))), (XVar)(password), (XVar)(dummy), (XVar)(pageObject));
			}
			return null;
		}
		public static XVar CheckSecurity(dynamic _param_strValue, dynamic _param_strAction, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic strValue = XVar.Clone(_param_strValue);
			dynamic strAction = XVar.Clone(_param_strAction);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic localAction = null, strPerm = null;
			ProjectSettings pSet;
			if(table == XVar.Pack(""))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			if(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMIN)
			{
				return true;
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions());
			if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("M"))), XVar.Pack(false)))
			{
				if(table == "dbo._ABCVotes")
				{
					if((XVar)((XVar)(strAction == "Edit")  || (XVar)(strAction == "Delete"))  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(pSet.getCaseSensitiveUsername((XVar)(XSession.Session[MVCFunctions.Concat("_", table, "_OwnerID")]))), XVar.Pack(pSet.getCaseSensitiveUsername((XVar)(strValue)))))))
					{
						return false;
					}
				}
			}
			localAction = XVar.Clone(MVCFunctions.strtolower((XVar)(strAction)));
			if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(localAction == "add")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("A"))), XVar.Pack(false)))))  || (XVar)((XVar)(localAction == "edit")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("E"))), XVar.Pack(false))))))  || (XVar)((XVar)(localAction == "delete")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("D"))), XVar.Pack(false))))))  || (XVar)((XVar)(localAction == "search")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false))))))  || (XVar)((XVar)(localAction == "import")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("I"))), XVar.Pack(false))))))  || (XVar)((XVar)(localAction == "export")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false))))))
			{
				return true;
			}
			else
			{
				return false;
			}
			return true;
		}
		public static XVar CheckTablePermissions(dynamic _param_strTableName, dynamic _param_permission)
		{
			#region pass-by-value parameters
			dynamic strTableName = XVar.Clone(_param_strTableName);
			dynamic permission = XVar.Clone(_param_permission);
			#endregion

			if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(CommonFunctions.GetUserPermissions((XVar)(strTableName))), (XVar)(permission))), XVar.Pack(false)))
			{
				return false;
			}
			return true;
		}
		public static XVar pagetypeToPermissions(dynamic _param_pageType)
		{
			#region pass-by-value parameters
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars._pagetypeToPermissions_dict)))
			{
				GlobalVars._pagetypeToPermissions_dict = XVar.Clone(XVar.Array());
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("S", "list");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("S", "search");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("S", "view");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("A", "add");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("E", "edit");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("P", "print");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("P", "export");
				GlobalVars._pagetypeToPermissions_dict.InitAndSetArrayItem("I", "import");
			}
			return GlobalVars._pagetypeToPermissions_dict[pageType];
		}
		public static XVar SecuritySQL(dynamic _param_strAction, dynamic _param_table = null, dynamic _param_strPerm = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_strPerm as Object == null) _param_strPerm = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic strAction = XVar.Clone(_param_strAction);
			dynamic table = XVar.Clone(_param_table);
			dynamic strPerm = XVar.Clone(_param_strPerm);
			#endregion

			dynamic ownerid = null, ret = null;
			ProjectSettings pSet;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(table)))))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			ownerid = XVar.Clone(XSession.Session[MVCFunctions.Concat("_", table, "_OwnerID")]);
			ret = new XVar("");
			if(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMIN)
			{
				return "";
			}
			ret = new XVar("");
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(strPerm)))))
			{
				strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(table)));
			}
			if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("M"))), XVar.Pack(false)))
			{
				if(table == "dbo._ABCVotes")
				{
					if((XVar)(strAction == "Edit")  || (XVar)(strAction == "Delete"))
					{
						ret = XVar.Clone(MVCFunctions.Concat(CommonFunctions.GetFullFieldName((XVar)(pSet.getTableOwnerID()), (XVar)(table), new XVar(false)), "=", CommonFunctions.make_db_value((XVar)(pSet.getTableOwnerID()), (XVar)(ownerid), new XVar(""), new XVar(""), (XVar)(table))));
					}
				}
			}
			if((XVar)((XVar)((XVar)((XVar)(strAction == "Edit")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("E"))), XVar.Pack(false)))))  || (XVar)((XVar)(strAction == "Delete")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("D"))), XVar.Pack(false))))))  || (XVar)((XVar)(strAction == "Search")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false))))))  || (XVar)((XVar)(strAction == "Export")  && (XVar)(!(XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false))))))
			{
				return ret;
			}
			else
			{
				return "1=0";
			}
			return "";
		}
		public static XVar make_db_value(dynamic _param_field, dynamic _param_value, dynamic _param_controltype = null, dynamic _param_postfilename = null, dynamic _param_table = null)
		{
			#region default values
			if(_param_controltype as Object == null) _param_controltype = new XVar("");
			if(_param_postfilename as Object == null) _param_postfilename = new XVar("");
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic controltype = XVar.Clone(_param_controltype);
			dynamic postfilename = XVar.Clone(_param_postfilename);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(field), (XVar)(value), (XVar)(controltype), (XVar)(postfilename), (XVar)(table)));
			if(XVar.Equals(XVar.Pack(ret), XVar.Pack(false)))
			{
				return ret;
			}
			return CommonFunctions.add_db_quotes((XVar)(field), (XVar)(ret), (XVar)(table));
		}
		public static XVar add_db_quotes(dynamic _param_field, dynamic _param_value, dynamic _param_table = null, dynamic _param_type = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_type as Object == null) _param_type = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic table = XVar.Clone(_param_table);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			dynamic connection = null;
			ProjectSettings pSet;
			if(table == XVar.Pack(""))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(table)));
			if(var_type == null)
			{
				var_type = XVar.Clone(pSet.getFieldType((XVar)(field)));
			}
			if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(var_type))))
			{
				return connection.addSlashesBinary((XVar)(value));
			}
			if((XVar)((XVar)((XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack("")))  || (XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack(false))))  || (XVar)(value == null))  && (XVar)(!(XVar)(CommonFunctions.IsCharType((XVar)(var_type)))))
			{
				return "null";
			}
			if(XVar.Pack(CommonFunctions.NeedQuotes((XVar)(var_type))))
			{
				if(XVar.Pack(!(XVar)(CommonFunctions.IsDateFieldType((XVar)(var_type)))))
				{
					value = XVar.Clone(connection.prepareString((XVar)(value)));
				}
				else
				{
					dynamic d = null, delim = null, m = null, matches = null, reg = null, y = null;
					y = new XVar("(\\d\\d\\d\\d)");
					m = new XVar("(0?[1-9]|1[0-2])");
					d = new XVar("(0?[1-9]|[1-2][0-9]|3[0-1])");
					delim = XVar.Clone(MVCFunctions.Concat("(-|", MVCFunctions.preg_quote((XVar)(GlobalVars.locale_info["LOCALE_SDATE"]), new XVar("/")), ")"));
					reg = XVar.Clone(MVCFunctions.Concat("/", d, delim, m, delim, y, "|", m, delim, d, delim, y, "|", y, delim, m, delim, d, "/"));
					if(XVar.Pack(!(XVar)(MVCFunctions.preg_match((XVar)(reg), (XVar)(value), (XVar)(matches)))))
					{
						return "null";
					}
					value = XVar.Clone(connection.addDateQuotes((XVar)(value)));
				}
			}
			else
			{
				if((XVar)(connection.dbType == Constants.nDATABASE_PostgreSQL)  && (XVar)(var_type == 11))
				{
					value = XVar.Clone(MVCFunctions.strtolower((XVar)(value)));
					if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(value))))  || (XVar)(value == XVar.Pack(0)))  || (XVar)(value == "0"))  || (XVar)(value == "false"))  || (XVar)(value == "f"))  || (XVar)(value == "n"))  || (XVar)(value == "no"))  || (XVar)(value == "off"))
					{
						value = new XVar("f");
					}
					else
					{
						value = new XVar("t");
					}
					value = XVar.Clone(connection.prepareString((XVar)(value)));
				}
				else
				{
					value = XVar.Clone(DB.prepareNumberValue((XVar)(value)));
				}
			}
			return value;
		}
		public static XVar prepare_for_db(dynamic _param_field, dynamic _param_value, dynamic _param_controltype = null, dynamic _param_postfilename = null, dynamic _param_table = null)
		{
			#region default values
			if(_param_controltype as Object == null) _param_controltype = new XVar("");
			if(_param_postfilename as Object == null) _param_postfilename = new XVar("");
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic controltype = XVar.Clone(_param_controltype);
			dynamic postfilename = XVar.Clone(_param_postfilename);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic connection = null, filename = null, var_type = null;
			ProjectSettings pSet;
			if(controltype == "display")
			{
				return value;
			}
			if(table == XVar.Pack(""))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(table)));
			filename = new XVar("");
			var_type = XVar.Clone(pSet.getFieldType((XVar)(field)));
			if((XVar)((XVar)(!(XVar)(controltype))  || (XVar)(controltype == "multiselect"))  && (XVar)(!(XVar)(CommonFunctions.IsTimeType((XVar)(var_type)))))
			{
				if(XVar.Pack(MVCFunctions.is_array((XVar)(value))))
				{
					value = XVar.Clone(CommonFunctions.combinevalues((XVar)(value)));
				}
				if((XVar)((XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack("")))  || (XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack(false))))  && (XVar)(!(XVar)(CommonFunctions.IsCharType((XVar)(var_type)))))
				{
					return "";
				}
				if(XVar.Pack(CommonFunctions.IsGuid((XVar)(var_type))))
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.IsGuidString(ref value))))
					{
						return "";
					}
				}
				if(XVar.Pack(CommonFunctions.IsFloatType((XVar)(var_type))))
				{
					return MVCFunctions.makeFloat((XVar)(value));
				}
				if((XVar)(CommonFunctions.IsNumberType((XVar)(var_type)))  && (XVar)(!(XVar)(MVCFunctions.IsNumeric(value))))
				{
					value = XVar.Clone(MVCFunctions.trim((XVar)(value)));
					if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(value))))))
					{
						value = new XVar("");
					}
				}
				return value;
			}
			else
			{
				dynamic time = null;
				if((XVar)(controltype == "time")  || (XVar)(CommonFunctions.IsTimeType((XVar)(var_type))))
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(value)))))
					{
						return "";
					}
					time = XVar.Clone(CommonFunctions.localtime2db((XVar)(value)));
					if(connection.dbType == Constants.nDATABASE_PostgreSQL)
					{
						dynamic timeArr = XVar.Array();
						timeArr = XVar.Clone(MVCFunctions.explode(new XVar(":"), (XVar)(time)));
						if((XVar)((XVar)(24 < timeArr[0])  || (XVar)(59 < timeArr[1]))  || (XVar)(59 < timeArr[2]))
						{
							return "";
						}
					}
					if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(var_type))))
					{
						time = XVar.Clone(MVCFunctions.Concat("2000-01-01 ", time));
					}
					return time;
				}
				else
				{
					if(MVCFunctions.substr((XVar)(controltype), new XVar(0), new XVar(4)) == "date")
					{
						dynamic dformat = null;
						dformat = XVar.Clone(MVCFunctions.substr((XVar)(controltype), new XVar(4)));
						if((XVar)((XVar)(dformat == Constants.EDIT_DATE_SIMPLE)  || (XVar)(dformat == Constants.EDIT_DATE_SIMPLE_INLINE))  || (XVar)(dformat == Constants.EDIT_DATE_SIMPLE_DP))
						{
							time = XVar.Clone(CommonFunctions.localdatetime2db((XVar)(value)));
							if(time == "null")
							{
								return "";
							}
							return time;
						}
						else
						{
							if((XVar)((XVar)(dformat == Constants.EDIT_DATE_DD)  || (XVar)(dformat == Constants.EDIT_DATE_DD_INLINE))  || (XVar)(dformat == Constants.EDIT_DATE_DD_DP))
							{
								dynamic a = XVar.Array(), d = null, m = null, y = null;
								a = XVar.Clone(MVCFunctions.explode(new XVar("-"), (XVar)(value)));
								if(MVCFunctions.count(a) < 3)
								{
									return "";
								}
								else
								{
									y = XVar.Clone(a[0]);
									m = XVar.Clone(a[1]);
									d = XVar.Clone(a[2]);
								}
								if(y < 100)
								{
									if(y < 70)
									{
										y += 2000;
									}
									else
									{
										y += 1900;
									}
								}
								return MVCFunctions.mysprintf(new XVar("%04d-%02d-%02d"), (XVar)(new XVar(0, y, 1, m, 2, d)));
							}
							else
							{
								return "";
							}
						}
					}
					else
					{
						if(MVCFunctions.substr((XVar)(controltype), new XVar(0), new XVar(8)) == "checkbox")
						{
							dynamic ret = null;
							if(value == "on")
							{
								ret = new XVar(1);
							}
							else
							{
								if(value == "none")
								{
									return "";
								}
								else
								{
									ret = new XVar(0);
								}
							}
							return ret;
						}
						else
						{
							return false;
						}
					}
				}
			}
			return null;
		}
		public static XVar DeleteUploadedFiles(dynamic _param_pSet_packed, dynamic _param_deleted_values)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic deleted_values = XVar.Clone(_param_deleted_values);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in deleted_values.GetEnumerator())
			{
				if((XVar)((XVar)(pSet.getEditFormat((XVar)(value.Key)) == Constants.EDIT_FORMAT_FILE)  || (XVar)(pSet.getPageTypeByFieldEditFormat((XVar)(value.Key), new XVar(Constants.EDIT_FORMAT_FILE)) != ""))  && (XVar)(pSet.isDeleteAssociatedFile((XVar)(value.Key))))
				{
					dynamic filesArray = XVar.Array();
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(value.Value)))))
					{
						return null;
					}
					filesArray = XVar.Clone(MVCFunctions.my_json_decode((XVar)(value.Value)));
					if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(filesArray))))  || (XVar)(MVCFunctions.count(filesArray) == 0))
					{
						filesArray = XVar.Clone(new XVar(0, new XVar("name", MVCFunctions.Concat(pSet.getUploadFolder((XVar)(value.Key)), value.Value))));
						if(XVar.Pack(pSet.getCreateThumbnail((XVar)(value.Key))))
						{
							filesArray.InitAndSetArrayItem(MVCFunctions.Concat(pSet.getUploadFolder((XVar)(value.Key)), pSet.getStrThumbnail((XVar)(value.Key)), value.Value), 0, "thumbnail");
						}
					}
					foreach (KeyValuePair<XVar, dynamic> delFile in filesArray.GetEnumerator())
					{
						dynamic filename = null, isAbs = null;
						filename = XVar.Clone(delFile.Value["name"]);
						isAbs = XVar.Clone((XVar)(pSet.isAbsolute((XVar)(value.Key)))  || (XVar)(MVCFunctions.isAbsolutePath((XVar)(filename))));
						if(XVar.Pack(!(XVar)(isAbs)))
						{
							filename = XVar.Clone(MVCFunctions.getabspath((XVar)(filename)));
						}
						MVCFunctions.runner_delete_file((XVar)(filename));
						if(delFile.Value["thumbnail"] != "")
						{
							filename = XVar.Clone(delFile.Value["thumbnail"]);
							if(XVar.Pack(!(XVar)(isAbs)))
							{
								filename = XVar.Clone(MVCFunctions.getabspath((XVar)(filename)));
							}
							MVCFunctions.runner_delete_file((XVar)(filename));
						}
					}
				}
			}
			return null;
		}
		public static XVar combinevalues(dynamic _param_arr)
		{
			#region pass-by-value parameters
			dynamic arr = XVar.Clone(_param_arr);
			#endregion

			dynamic ret = null;
			ret = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> item in arr.GetEnumerator())
			{
				dynamic val = null;
				val = XVar.Clone(item.Value);
				if(XVar.Pack(MVCFunctions.strlen((XVar)(ret))))
				{
					ret = MVCFunctions.Concat(ret, ",");
				}
				if((XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(val), new XVar(","))), XVar.Pack(false)))  && (XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(val), new XVar("\""))), XVar.Pack(false))))
				{
					ret = MVCFunctions.Concat(ret, val);
				}
				else
				{
					val = XVar.Clone(MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(val)));
					ret = MVCFunctions.Concat(ret, "\"", val, "\"");
				}
			}
			return ret;
		}
		public static XVar splitvalues(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic arr = XVar.Array(), i = null, inquot = null, start = null;
			arr = XVar.Clone(XVar.Array());
			if(str == XVar.Pack(""))
			{
				arr.InitAndSetArrayItem("", null);
				return arr;
			}
			start = new XVar(0);
			i = new XVar(0);
			inquot = new XVar(false);
			while(i <= MVCFunctions.strlen((XVar)(str)))
			{
				if((XVar)(i < MVCFunctions.strlen((XVar)(str)))  && (XVar)(MVCFunctions.substr((XVar)(str), (XVar)(i), new XVar(1)) == "\""))
				{
					inquot = XVar.Clone(!(XVar)(inquot));
				}
				else
				{
					if((XVar)(i == MVCFunctions.strlen((XVar)(str)))  || (XVar)((XVar)(!(XVar)(inquot))  && (XVar)(MVCFunctions.substr((XVar)(str), (XVar)(i), new XVar(1)) == ",")))
					{
						dynamic val = null;
						val = XVar.Clone(MVCFunctions.substr((XVar)(str), (XVar)(start), (XVar)(i - start)));
						start = XVar.Clone(i + 1);
						if((XVar)(MVCFunctions.strlen((XVar)(val)))  && (XVar)(MVCFunctions.substr((XVar)(val), new XVar(0), new XVar(1)) == "\""))
						{
							val = XVar.Clone(MVCFunctions.substr((XVar)(val), new XVar(1), (XVar)(MVCFunctions.strlen((XVar)(val)) - 2)));
							val = XVar.Clone(MVCFunctions.str_replace(new XVar("\"\""), new XVar("\""), (XVar)(val)));
						}
						if(!XVar.Equals(XVar.Pack(val), XVar.Pack(false)))
						{
							arr.InitAndSetArrayItem(val, null);
						}
					}
				}
				i++;
			}
			return arr;
		}
		public static XVar GetLookupFieldsIndexes(dynamic _param_pSet_packed, dynamic _param_field)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic displayFieldIndex = null, displayFieldName = null, linkAndDisplaySame = null, linkFieldIndex = null, linkFieldName = null, lookupTable = null, lookupType = null;
			lookupTable = XVar.Clone(pSet.getLookupTable((XVar)(field)));
			lookupType = XVar.Clone(pSet.getLookupType((XVar)(field)));
			displayFieldName = XVar.Clone(pSet.getDisplayField((XVar)(field)));
			linkFieldName = XVar.Clone(pSet.getLinkField((XVar)(field)));
			linkAndDisplaySame = XVar.Clone(linkFieldName == displayFieldName);
			if(lookupType == Constants.LT_QUERY)
			{
				dynamic lookupPSet = null;
				lookupPSet = XVar.Clone(new ProjectSettings((XVar)(lookupTable)));
				linkFieldIndex = XVar.Clone(lookupPSet.getFieldIndex((XVar)(linkFieldName)) - 1);
				if(XVar.Pack(linkAndDisplaySame))
				{
					displayFieldIndex = XVar.Clone(linkFieldIndex);
				}
				else
				{
					if(XVar.Pack(pSet.getCustomDisplay((XVar)(field))))
					{
						displayFieldIndex = XVar.Clone(lookupPSet.getCustomExpressionIndex((XVar)(pSet._table), (XVar)(field)));
					}
					else
					{
						displayFieldIndex = XVar.Clone(lookupPSet.getFieldIndex((XVar)(displayFieldName)) - 1);
					}
				}
			}
			else
			{
				linkFieldIndex = new XVar(0);
				displayFieldIndex = XVar.Clone((XVar.Pack(linkAndDisplaySame) ? XVar.Pack(0) : XVar.Pack(1)));
			}
			return new XVar("linkFieldIndex", linkFieldIndex, "displayFieldIndex", displayFieldIndex);
		}
		public static XVar getLacaleAmPmForTimePicker(dynamic _param_convention, dynamic _param_useTimePicker = null)
		{
			#region default values
			if(_param_useTimePicker as Object == null) _param_useTimePicker = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic convention = XVar.Clone(_param_convention);
			dynamic useTimePicker = XVar.Clone(_param_useTimePicker);
			#endregion

			dynamic am = null, locale = null, pm = null;
			am = new XVar("");
			pm = new XVar("");
			if(XVar.Pack(useTimePicker))
			{
				dynamic locale_convention = null;
				locale_convention = XVar.Clone((XVar.Pack(GlobalVars.locale_info["LOCALE_ITIME"]) ? XVar.Pack(24) : XVar.Pack(12)));
				if(convention == locale_convention)
				{
					am = XVar.Clone(GlobalVars.locale_info["LOCALE_S1159"]);
					pm = XVar.Clone(GlobalVars.locale_info["LOCALE_S2359"]);
					locale = XVar.Clone(GlobalVars.locale_info["LOCALE_STIMEFORMAT"]);
				}
				else
				{
					if(convention == 24)
					{
						am = new XVar("");
						pm = new XVar("");
						locale = new XVar("H:mm:ss");
					}
					else
					{
						am = new XVar("am");
						pm = new XVar("pm");
						locale = new XVar("h:mm:ss tt");
					}
				}
			}
			else
			{
				locale = XVar.Clone(GlobalVars.locale_info["LOCALE_STIMEFORMAT"]);
			}
			return new XVar("am", am, "pm", pm, "locale", locale);
		}
		public static XVar getValForTimePicker(dynamic _param_type, dynamic _param_value, dynamic _param_locale)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic value = XVar.Clone(_param_value);
			dynamic locale = XVar.Clone(_param_locale);
			#endregion

			dynamic dbtime = null, val = null;
			val = new XVar("");
			dbtime = XVar.Clone(XVar.Array());
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(var_type))))
			{
				dbtime = XVar.Clone(CommonFunctions.db2time((XVar)(value)));
				if(XVar.Pack(MVCFunctions.count(dbtime)))
				{
					val = XVar.Clone(CommonFunctions.format_datetime_custom((XVar)(dbtime), (XVar)(locale)));
				}
			}
			else
			{
				dynamic arr = XVar.Array();
				arr = XVar.Clone(CommonFunctions.parsenumbers((XVar)(value)));
				if(XVar.Pack(MVCFunctions.count(arr)))
				{
					while(MVCFunctions.count(arr) < 3)
					{
						arr.InitAndSetArrayItem(0, null);
					}
					dbtime = XVar.Clone(new XVar(0, 0, 1, 0, 2, 0, 3, arr[0], 4, arr[1], 5, arr[2]));
					val = XVar.Clone(CommonFunctions.format_datetime_custom((XVar)(dbtime), (XVar)(locale)));
				}
			}
			return new XVar("val", val, "dbTime", dbtime);
		}
		public static XVar my_stripos(dynamic _param_str, dynamic _param_needle, dynamic _param_offest)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			dynamic needle = XVar.Clone(_param_needle);
			dynamic offest = XVar.Clone(_param_offest);
			#endregion

			if((XVar)(MVCFunctions.strlen((XVar)(needle)) == 0)  || (XVar)(MVCFunctions.strlen((XVar)(str)) == 0))
			{
				return false;
			}
			return MVCFunctions.strpos((XVar)(MVCFunctions.strtolower((XVar)(str))), (XVar)(MVCFunctions.strtolower((XVar)(needle))), (XVar)(offest));
		}
		public static XVar my_str_ireplace(dynamic _param_search, dynamic _param_replace, dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic search = XVar.Clone(_param_search);
			dynamic replace = XVar.Clone(_param_replace);
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic pos = null;
			pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(str), (XVar)(search), new XVar(0)));
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				return str;
			}
			return MVCFunctions.Concat(MVCFunctions.substr((XVar)(str), new XVar(0), (XVar)(pos)), replace, MVCFunctions.substr((XVar)(str), (XVar)(pos + MVCFunctions.strlen((XVar)(search)))));
		}
		public static XVar in_assoc_array(dynamic _param_name, dynamic _param_arr)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic arr = XVar.Clone(_param_arr);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in arr.GetEnumerator())
			{
				if(value.Key == name)
				{
					return true;
				}
			}
			return false;
		}
		public static XVar xmlencode(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			str = XVar.Clone(MVCFunctions.str_replace(new XVar("&"), new XVar("&amp;"), (XVar)(str)));
			str = XVar.Clone(MVCFunctions.str_replace(new XVar("<"), new XVar("&lt;"), (XVar)(str)));
			str = XVar.Clone(MVCFunctions.str_replace(new XVar(">"), new XVar("&gt;"), (XVar)(str)));
			str = XVar.Clone(MVCFunctions.str_replace(new XVar("'"), new XVar("&apos;"), (XVar)(str)));
			return MVCFunctions.escapeEntities((XVar)(str));
		}
		public static XVar print_inline_array(dynamic arr, dynamic _param_printkey = null)
		{
			#region default values
			if(_param_printkey as Object == null) _param_printkey = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic printkey = XVar.Clone(_param_printkey);
			#endregion

			if(XVar.Pack(!(XVar)(printkey)))
			{
				foreach (KeyValuePair<XVar, dynamic> val in arr.GetEnumerator())
				{
					MVCFunctions.Echo(MVCFunctions.Concat(MVCFunctions.str_replace((XVar)(new XVar(0, "&", 1, "<", 2, "\\", 3, "\r", 4, "\n")), (XVar)(new XVar(0, "&amp;", 1, "&lt;", 2, "\\\\", 3, "\\r", 4, "\\n")), (XVar)(MVCFunctions.str_replace((XVar)(new XVar(0, "\\", 1, "\r", 2, "\n")), (XVar)(new XVar(0, "\\\\", 1, "\\r", 2, "\\n")), (XVar)(val.Value)))), "\\n"));
				}
			}
			else
			{
				foreach (KeyValuePair<XVar, dynamic> val in arr.GetEnumerator())
				{
					MVCFunctions.Echo(MVCFunctions.Concat(MVCFunctions.str_replace((XVar)(new XVar(0, "&", 1, "<", 2, "\\", 3, "\r", 4, "\n")), (XVar)(new XVar(0, "&amp;", 1, "&lt;", 2, "\\\\", 3, "\\r", 4, "\\n")), (XVar)(MVCFunctions.str_replace((XVar)(new XVar(0, "\\", 1, "\r", 2, "\n")), (XVar)(new XVar(0, "\\\\", 1, "\\r", 2, "\\n")), (XVar)(val.Key)))), "\\n"));
				}
			}
			return null;
		}
		public static XVar checkpassword(dynamic _param_pwd)
		{
			#region pass-by-value parameters
			dynamic pwd = XVar.Clone(_param_pwd);
			#endregion

			dynamic c = null, cDigit = null, cLower = null, cUnique = XVar.Array(), cUpper = null, i = null, len = null;
			len = XVar.Clone(MVCFunctions.strlen((XVar)(pwd)));
			if(len < 8)
			{
				return false;
			}
			cUnique = XVar.Clone(XVar.Array());
			cLower = XVar.Clone(cUpper = XVar.Clone(cDigit = new XVar(0)));
			i = new XVar(0);
			for(;i < len; i++)
			{
				c = XVar.Clone(MVCFunctions.substr((XVar)(pwd), (XVar)(i), new XVar(1)));
				if((XVar)("a" <= c)  && (XVar)(c <= "z"))
				{
					cLower++;
				}
				else
				{
					if((XVar)("A" <= c)  && (XVar)(c <= "Z"))
					{
						cUpper++;
					}
					else
					{
						cDigit++;
					}
				}
				cUnique.InitAndSetArrayItem(1, c);
			}
			if(MVCFunctions.count(cUnique) < 4)
			{
				return false;
			}
			if(cDigit < 2)
			{
				return false;
			}
			return true;
		}
		public static XVar GetChartXML(dynamic _param_chartname)
		{
			#region pass-by-value parameters
			dynamic chartname = XVar.Clone(_param_chartname);
			#endregion

			dynamic settings = null;
			GlobalVars.strTableName = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(chartname)));
			settings = XVar.Clone(new ProjectSettings((XVar)(GlobalVars.strTableName)));
			return settings.getChartXml();
		}
		public static XVar isSecureProtocol()
		{
			return (XVar)((XVar)(!(XVar)(MVCFunctions.GetServerVariable("HTTPS").IsEmpty()))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.GetServerVariable("HTTPS")), XVar.Pack("off"))))  || (XVar)(MVCFunctions.GetServerPort() == 443);
		}
		public static XVar GetSiteUrl()
		{
			dynamic proto = null;
			proto = new XVar("http://");
			if((XVar)(MVCFunctions.GetServerVariable("HTTPS"))  && (XVar)(MVCFunctions.GetServerVariable("HTTPS") != "off"))
			{
				proto = new XVar("https://");
			}
			return MVCFunctions.Concat(proto, MVCFunctions.GetServerVariable("HTTP_HOST"));
		}
		public static XVar GetFullSiteUrl()
		{
			return CommonFunctions.getDirectoryFromURI((XVar)(MVCFunctions.Concat(CommonFunctions.GetSiteUrl(), MVCFunctions.GetServerVariable("REQUEST_URI"))));
		}
		public static XVar GetAuditObject(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic linkAudit = null;
			return null;
			linkAudit = new XVar(false);
			if(XVar.Pack(!(XVar)(table)))
			{
				linkAudit = new XVar(true);
			}
			else
			{
				dynamic settings = null;
				settings = XVar.Clone(new ProjectSettings((XVar)(table)));
				linkAudit = XVar.Clone(settings.auditEnabled());
			}
			if(XVar.Pack(linkAudit))
			{
			}
			else
			{
				return null;
			}
			return null;
		}
		public static XVar GetLockingObject(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic settings = null;
			return null;
			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			settings = XVar.Clone(new ProjectSettings((XVar)(table)));
			if(XVar.Pack(settings.lockingEnabled()))
			{
				return new oLocking();
			}
			else
			{
				return null;
			}
			return null;
		}
		public static XVar isEnableSection508()
		{
			return CommonFunctions.GetGlobalData(new XVar("isSection508"), new XVar(false));
		}
		public static XVar getJsValidatorName(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			switch(((XVar)name).ToString())
			{
				case "Number":
					return "IsNumeric";
					break;
				case "Password":
					return "IsPassword";
					break;
				case "Email":
					return "IsEmail";
					break;
				case "Currency":
					return "IsMoney";
					break;
				case "US ZIP Code":
					return "IsZipCode";
					break;
				case "US Phone Number":
					return "IsPhoneNumber";
					break;
				case "US State":
					return "IsState";
					break;
				case "US SSN":
					return "IsSSN";
					break;
				case "Credit Card":
					return "IsCC";
					break;
				case "Time":
					return "IsTime";
					break;
				case "Regular expression":
					return "RegExp";
					break;
				default:
					return name;
					break;
			}
			return null;
		}
		public static XVar SetLangVars(dynamic _param_xt_packed, dynamic _param_prefix, dynamic _param_pageName = null, dynamic _param_extraparams = null)
		{
			#region packeted values
			XTempl _param_xt = XVar.UnPackXTempl(_param_xt_packed);
			#endregion

			#region default values
			if(_param_pageName as Object == null) _param_pageName = new XVar("");
			if(_param_extraparams as Object == null) _param_extraparams = new XVar("");
			#endregion

			#region pass-by-value parameters
			XTempl xt = XVar.Clone(_param_xt);
			dynamic prefix = XVar.Clone(_param_prefix);
			dynamic pageName = XVar.Clone(_param_pageName);
			dynamic extraparams = XVar.Clone(_param_extraparams);
			#endregion

			dynamic currentLang = null, dataAttr = null, var_var = null;
			xt.assign(new XVar("lang_label"), new XVar(true));
			if(XVar.Pack(MVCFunctions.postvalue("language")))
			{
				XSession.Session["language"] = MVCFunctions.postvalue("language");
			}
			currentLang = XVar.Clone(CommonFunctions.mlang_getcurrentlang());
			var_var = XVar.Clone(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(currentLang)), "_langattrs"));
			xt.assign((XVar)(var_var), new XVar("selected"));
			xt.assign((XVar)(MVCFunctions.Concat(currentLang, "LANGLINK_ACTIVE")), new XVar(true));
			xt.assign(new XVar("EnglishLANGLINK"), (XVar)("English" != currentLang));
			if(XVar.Pack(CommonFunctions.isEnableSection508()))
			{
				xt.assign_section(new XVar("lang_label"), new XVar("<label for=\"languageSelector\">"), new XVar("</label>"));
			}
			if(XVar.Pack(extraparams))
			{
				extraparams = XVar.Clone(MVCFunctions.Concat(extraparams, "&"));
			}
			dataAttr = XVar.Clone(MVCFunctions.Concat("data-params=\"", extraparams, "\" data-prefix=\"", prefix, "\""));
			xt.assign(new XVar("langselector_attrs"), (XVar)(MVCFunctions.Concat("id=\"languageSelector\" ", dataAttr)));
			xt.assign(new XVar("languages_block"), new XVar(true));
			return null;
		}
		public static XVar GetTableCaption(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.tableCaptions[CommonFunctions.mlang_getcurrentlang()][table];
		}
		public static XVar GetFieldByLabel(dynamic _param_table, dynamic _param_label)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic label = XVar.Clone(_param_label);
			#endregion

			dynamic currLang = null, lables = XVar.Array();
			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			if(XVar.Pack(!(XVar)(GlobalVars.field_labels.KeyExists(table))))
			{
				return "";
			}
			currLang = XVar.Clone(CommonFunctions.mlang_getcurrentlang());
			if(XVar.Pack(!(XVar)(GlobalVars.field_labels[table].KeyExists(currLang))))
			{
				return "";
			}
			lables = XVar.Clone(GlobalVars.field_labels[table][CommonFunctions.mlang_getcurrentlang()]);
			foreach (KeyValuePair<XVar, dynamic> val in lables.GetEnumerator())
			{
				if(val.Value == label)
				{
					return val.Key;
				}
			}
			return "";
		}
		public static XVar GetFieldLabel(dynamic _param_table, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.field_labels.KeyExists(table))))
			{
				return "";
			}
			return GlobalVars.field_labels[table][CommonFunctions.mlang_getcurrentlang()][field];
		}
		public static XVar GetFieldToolTip(dynamic _param_table, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.fieldToolTips.KeyExists(table))))
			{
				return "";
			}
			return GlobalVars.fieldToolTips[table][CommonFunctions.mlang_getcurrentlang()][field];
		}
		public static XVar GetFieldPlaceHolder(dynamic _param_table, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.placeHolders.KeyExists(table))))
			{
				return "";
			}
			return GlobalVars.placeHolders[table][CommonFunctions.mlang_getcurrentlang()][field];
		}
		public static XVar GetCustomLabel(dynamic _param_custom)
		{
			#region pass-by-value parameters
			dynamic custom = XVar.Clone(_param_custom);
			#endregion

			return GlobalVars.custom_labels[CommonFunctions.mlang_getcurrentlang()][custom];
		}
		public static XVar mlang_getcurrentlang()
		{
			if(XVar.Pack(MVCFunctions.postvalue("language")))
			{
				XSession.Session["language"] = MVCFunctions.postvalue("language");
			}
			if(XVar.Pack(XSession.Session["language"]))
			{
				return XSession.Session["language"];
			}
			return GlobalVars.mlang_defaultlang;
		}
		public static XVar isRTL()
		{
			dynamic cp = null;
			cp = XVar.Clone(MVCFunctions.strtolower((XVar)(GlobalVars.mlang_charsets[CommonFunctions.mlang_getcurrentlang()])));
			return (XVar)(cp == "windows-1256")  || (XVar)(cp == "windows-1255");
		}
		public static XVar mlang_getlanglist()
		{
			return MVCFunctions.array_keys((XVar)(GlobalVars.mlang_messages));
		}
		public static XVar getMountNames()
		{
			dynamic mounts = XVar.Array();
			mounts = XVar.Clone(XVar.Array());
			mounts.InitAndSetArrayItem("January", 1);
			mounts.InitAndSetArrayItem("February", 2);
			mounts.InitAndSetArrayItem("March", 3);
			mounts.InitAndSetArrayItem("April", 4);
			mounts.InitAndSetArrayItem("May", 5);
			mounts.InitAndSetArrayItem("June", 6);
			mounts.InitAndSetArrayItem("July", 7);
			mounts.InitAndSetArrayItem("August", 8);
			mounts.InitAndSetArrayItem("September", 9);
			mounts.InitAndSetArrayItem("October", 10);
			mounts.InitAndSetArrayItem("November", 11);
			mounts.InitAndSetArrayItem("December", 12);
			return mounts;
		}
		public static XVar displayDetailsOn(dynamic _param_table, dynamic _param_page)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic page = XVar.Clone(_param_page);
			#endregion

			dynamic i = null, key = null;
			if((XVar)(!(XVar)(GlobalVars.detailsTablesData.KeyExists(table)))  || (XVar)(!(XVar)(MVCFunctions.is_array((XVar)(GlobalVars.detailsTablesData[table])))))
			{
				return false;
			}
			if(page == Constants.PAGE_EDIT)
			{
				key = new XVar("previewOnEdit");
			}
			else
			{
				if(page == Constants.PAGE_ADD)
				{
					key = new XVar("previewOnAdd");
				}
				else
				{
					if(page == Constants.PAGE_VIEW)
					{
						key = new XVar("previewOnView");
					}
					else
					{
						key = new XVar("previewOnList");
					}
				}
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(GlobalVars.detailsTablesData[table]); i++)
			{
				if(XVar.Pack(GlobalVars.detailsTablesData[table][i][key]))
				{
					return true;
				}
			}
			return false;
		}
		public static XVar showDetailTable(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic oldTableName = null;
			oldTableName = XVar.Clone(GlobalVars.strTableName);
			GlobalVars.strTableName = XVar.Clone(var_params["table"]);
			if(XVar.Pack(var_params["dpObject"].isDispGrid()))
			{
				var_params["dpObject"].showPage();
			}
			GlobalVars.strTableName = XVar.Clone(oldTableName);
			return null;
		}
		public static XVar DoUpdateRecordSQL(dynamic _param_pageObject)
		{
			#region pass-by-value parameters
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			dynamic blobfields = null, blobs = null, evalues = XVar.Array(), strWhereClause = null, table = null;
			table = XVar.Clone(pageObject.pSet.getOriginalTableName());
			strWhereClause = XVar.Clone(pageObject.getKeysWhereClause(new XVar(true)));
			evalues = XVar.Clone(pageObject.getNewRecordData());
			blobfields = XVar.Clone(pageObject.getBlobFields());
			if(XVar.Pack(!(XVar)(MVCFunctions.count(evalues))))
			{
				return true;
			}
			GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("update ", pageObject.connection.addTableWrappers((XVar)(table)), " set "));
			blobs = XVar.Clone(MVCFunctions.PrepareBlobs(ref evalues, ref blobfields, (XVar)(pageObject)));
			foreach (KeyValuePair<XVar, dynamic> value in evalues.GetEnumerator())
			{
				dynamic strValue = null;
				if(XVar.Pack(MVCFunctions.in_array((XVar)(value.Key), (XVar)(blobfields))))
				{
					strValue = XVar.Clone(value.Value);
				}
				else
				{
					if(XVar.Pack(pageObject.cipherer == null))
					{
						strValue = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(value.Key), (XVar)(value.Value)));
					}
					else
					{
						strValue = XVar.Clone(pageObject.cipherer.AddDBQuotes((XVar)(value.Key), (XVar)(value.Value)));
					}
				}
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, pageObject.getTableField((XVar)(value.Key)), "=", strValue, ", ");
			}
			GlobalVars.strSQL = XVar.Clone(MVCFunctions.substr((XVar)(GlobalVars.strSQL), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(GlobalVars.strSQL)) - 2)));
			if(XVar.Equals(XVar.Pack(strWhereClause), XVar.Pack("")))
			{
				strWhereClause = new XVar(" (1=1) ");
			}
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " where ", strWhereClause);
			if(XVar.Pack(CommonFunctions.SecuritySQL(new XVar("Edit"), (XVar)(pageObject.tName))))
			{
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " and (", CommonFunctions.SecuritySQL(new XVar("Edit"), (XVar)(pageObject.tName)), ")");
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.ExecuteUpdate((XVar)(pageObject), (XVar)(GlobalVars.strSQL), (XVar)(blobs)))))
			{
				return false;
			}
			return true;
		}
		public static XVar DoInsertRecordSQL(dynamic _param_table, ref dynamic avalues, ref dynamic blobfields, dynamic pageObject)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic blobs = null, strFields = null, strValues = null;
			GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("insert into ", pageObject.connection.addTableWrappers((XVar)(table)), " "));
			strFields = new XVar("(");
			strValues = new XVar("(");
			blobs = XVar.Clone(MVCFunctions.PrepareBlobs(ref avalues, ref blobfields, (XVar)(pageObject)));
			foreach (KeyValuePair<XVar, dynamic> value in avalues.GetEnumerator())
			{
				strFields = MVCFunctions.Concat(strFields, pageObject.getTableField((XVar)(value.Key)), ", ");
				if(XVar.Pack(MVCFunctions.in_array((XVar)(value.Key), (XVar)(blobfields))))
				{
					strValues = MVCFunctions.Concat(strValues, value.Value, ", ");
				}
				else
				{
					if(XVar.Pack(pageObject.cipherer == null))
					{
						strValues = MVCFunctions.Concat(strValues, CommonFunctions.add_db_quotes((XVar)(value.Key), (XVar)(value.Value)), ", ");
					}
					else
					{
						strValues = MVCFunctions.Concat(strValues, pageObject.cipherer.AddDBQuotes((XVar)(value.Key), (XVar)(value.Value)), ", ");
					}
				}
			}
			if(MVCFunctions.substr((XVar)(strFields), new XVar(-2)) == ", ")
			{
				strFields = XVar.Clone(MVCFunctions.substr((XVar)(strFields), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(strFields)) - 2)));
			}
			if(MVCFunctions.substr((XVar)(strValues), new XVar(-2)) == ", ")
			{
				strValues = XVar.Clone(MVCFunctions.substr((XVar)(strValues), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(strValues)) - 2)));
			}
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, strFields, ") values ", strValues, ")");
			if(XVar.Pack(!(XVar)(MVCFunctions.ExecuteUpdate((XVar)(pageObject), (XVar)(GlobalVars.strSQL), (XVar)(blobs)))))
			{
				return false;
			}
			pageObject.ProcessFiles();
			return true;
		}
		public static XVar DoInsertRecordSQLOnAdd(dynamic pageObject)
		{
			dynamic avalues = XVar.Array(), blobfields = null, blobs = null, strFields = null, strValues = null, table = null;
			table = XVar.Clone(pageObject.pSet.getOriginalTableName());
			avalues = XVar.Clone(pageObject.getNewRecordData());
			blobfields = XVar.Clone(pageObject.getBlobFields());
			GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("insert into ", pageObject.connection.addTableWrappers((XVar)(table)), " "));
			strFields = new XVar("(");
			strValues = new XVar("(");
			blobs = XVar.Clone(MVCFunctions.PrepareBlobs(ref avalues, ref blobfields, (XVar)(pageObject)));
			foreach (KeyValuePair<XVar, dynamic> value in avalues.GetEnumerator())
			{
				strFields = MVCFunctions.Concat(strFields, pageObject.getTableField((XVar)(value.Key)), ", ");
				if(XVar.Pack(MVCFunctions.in_array((XVar)(value.Key), (XVar)(blobfields))))
				{
					strValues = MVCFunctions.Concat(strValues, value.Value, ", ");
				}
				else
				{
					if(XVar.Pack(pageObject.cipherer == null))
					{
						strValues = MVCFunctions.Concat(strValues, CommonFunctions.add_db_quotes((XVar)(value.Key), (XVar)(value.Value)), ", ");
					}
					else
					{
						strValues = MVCFunctions.Concat(strValues, pageObject.cipherer.AddDBQuotes((XVar)(value.Key), (XVar)(value.Value)), ", ");
					}
				}
			}
			if(MVCFunctions.substr((XVar)(strFields), new XVar(-2)) == ", ")
			{
				strFields = XVar.Clone(MVCFunctions.substr((XVar)(strFields), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(strFields)) - 2)));
			}
			if(MVCFunctions.substr((XVar)(strValues), new XVar(-2)) == ", ")
			{
				strValues = XVar.Clone(MVCFunctions.substr((XVar)(strValues), new XVar(0), (XVar)(MVCFunctions.strlen((XVar)(strValues)) - 2)));
			}
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, strFields, ") values ", strValues, ")");
			if(XVar.Pack(!(XVar)(MVCFunctions.ExecuteUpdate((XVar)(pageObject), (XVar)(GlobalVars.strSQL), (XVar)(blobs)))))
			{
				return false;
			}
			return true;
		}
		public static XVar getEventObject(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic ret = null;
			ret = new XVar(null);
			if(XVar.Pack(!(XVar)(GlobalVars.tableEvents.KeyExists(table))))
			{
				return ret;
			}
			return GlobalVars.tableEvents[table];
		}
		public static XVar tableEventExists(dynamic _param_event, dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic var_event = XVar.Clone(_param_event);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.tableEvents.KeyExists(table))))
			{
				return false;
			}
			return GlobalVars.tableEvents[table].exists((XVar)(var_event));
		}
		public static XVar add_nocache_headers()
		{
			MVCFunctions.Header("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
			MVCFunctions.Header("Pragma", "no-cache");
			MVCFunctions.Header("Expires", "Fri, 01 Jan 1990 00:00:00 GMT");
			return null;
		}
		public static XVar IsGuidString(ref dynamic str)
		{
			dynamic c = null, i = null;
			if((XVar)((XVar)(MVCFunctions.strlen((XVar)(str)) == 36)  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(0), new XVar(1)) != "{"))  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(-1)) != "}"))
			{
				str = XVar.Clone(MVCFunctions.Concat("{", str, "}"));
			}
			else
			{
				if((XVar)((XVar)(MVCFunctions.strlen((XVar)(str)) == 37)  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(0), new XVar(1)) == "{"))  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(-1)) != "}"))
				{
					str = XVar.Clone(MVCFunctions.Concat(str, "}"));
				}
				else
				{
					if((XVar)((XVar)(MVCFunctions.strlen((XVar)(str)) == 37)  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(0), new XVar(1)) != "{"))  && (XVar)(MVCFunctions.substr((XVar)(str), new XVar(-1)) == "}"))
					{
						str = XVar.Clone(MVCFunctions.Concat("{", str));
					}
				}
			}
			if(MVCFunctions.strlen((XVar)(str)) != 38)
			{
				return false;
			}
			i = new XVar(0);
			for(;i < 38; i++)
			{
				c = XVar.Clone(MVCFunctions.substr((XVar)(str), (XVar)(i), new XVar(1)));
				if(i == XVar.Pack(0))
				{
					if(c != "{")
					{
						return false;
					}
				}
				else
				{
					if(i == 37)
					{
						if(c != "}")
						{
							return false;
						}
					}
					else
					{
						if((XVar)((XVar)((XVar)(i == 9)  || (XVar)(i == 14))  || (XVar)(i == 19))  || (XVar)(i == 24))
						{
							if(c != "-")
							{
								return false;
							}
						}
						else
						{
							if((XVar)((XVar)((XVar)(c < "0")  || (XVar)("9" < c))  && (XVar)((XVar)(c < "a")  || (XVar)("f" < c)))  && (XVar)((XVar)(c < "A")  || (XVar)("F" < c)))
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}
		public static XVar IsStoredProcedure(dynamic _param_strSQL)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			#endregion

			if(6 < MVCFunctions.strlen((XVar)(strSQL)))
			{
				dynamic c = null;
				c = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(strSQL), new XVar(6), new XVar(1)))));
				if((XVar)((XVar)((XVar)(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(strSQL), new XVar(0), new XVar(6)))) == "select")  && (XVar)((XVar)(c < "0")  || (XVar)("9" < c)))  && (XVar)((XVar)(c < "a")  || (XVar)("z" < c)))  && (XVar)(c != "_"))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
			return null;
		}
		public static XVar MobileDetected()
		{
			dynamic accept = null, user_agent = null;
			user_agent = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.GetServerVariable("HTTP_USER_AGENT"))));
			accept = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.GetServerVariable("HTTP_ACCEPT"))));
			if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(accept), new XVar("text/vnd.wap.wml"))), XVar.Pack(false)))  || (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(accept), new XVar("application/vnd.wap.xhtml+xml"))), XVar.Pack(false))))
			{
				return 1;
			}
			if((XVar)(MVCFunctions.SERVERKeyExists("HTTP_X_WAP_PROFILE"))  || (XVar)(MVCFunctions.SERVERKeyExists("HTTP_PROFILE")))
			{
				return 2;
			}
			if(XVar.Pack(MVCFunctions.preg_match(new XVar(MVCFunctions.Concat("/(ipad|android|symbianos|opera mini|ipod|blackberry|", "palm os|palm|hiptop|avantgo|plucker|xiino|blazer|elaine|iris|3g_t|", "windows ce|opera mobi|windows ce; smartphone;|windows ce; iemobile|", "mini 9.5|vx1000|lge |m800|e860|u940|ux840|compal|", "wireless| mobi|ahong|lg380|lgku|lgu900|lg210|lg47|lg920|lg840|", "lg370|sam-r|mg50|s55|g83|t66|vx400|mk99|d615|d763|el370|sl900|", "mp500|samu3|samu4|vx10|xda_|samu5|samu6|samu7|samu9|a615|b832|", "m881|s920|n210|s700|c-810|_h797|mob-x|sk16d|848b|mowser|s580|", "r800|471x|v120|rim8|c500foma:|160x|x160|480x|x640|t503|w839|", "i250|sprint|w398samr810|m5252|c7100|mt126|x225|s5330|s820|", "htil-g1|fly v71|s302|-x113|novarra|k610i|-three|8325rc|8352rc|", "sanyo|vx54|c888|nx250|n120|mtk |c5588|s710|t880|c5005|i;458x|", "p404i|s210|c5100|teleca|s940|c500|s590|foma|samsu|vx8|vx9|a1000|", "_mms|myx|a700|gu1100|bc831|e300|ems100|me701|me702m-three|sd588|", "s800|8325rc|ac831|mw200|brew |d88|htc\\/|htc_touch|355x|m50|km100|", "d736|p-9521|telco|sl74|ktouch|m4u\\/|me702|8325rc|kddi|phone|lg |", "sonyericsson|samsung|240x|x320vx10|nokia|sony cmd|motorola|", "up.browser|up.link|mmp|symbian|smartphone|midp|wap|vodafone|o2|", "pocket|kindle|silk|hpwos|mobile|psp|treo)/")), (XVar)(user_agent))))
			{
				return 3;
			}
			if(XVar.Pack(MVCFunctions.in_array((XVar)(MVCFunctions.substr((XVar)(user_agent), new XVar(0), new XVar(4))), (XVar)(new XVar(0, "1207", 1, "3gso", 2, "4thp", 3, "501i", 4, "502i", 5, "503i", 6, "504i", 7, "505i", 8, "506i", 9, "6310", 10, "6590", 11, "770s", 12, "802s", 13, "a wa", 14, "abac", 15, "acer", 16, "acoo", 17, "acs-", 18, "aiko", 19, "airn", 20, "alav", 21, "alca", 22, "alco", 23, "amoi", 24, "anex", 25, "anny", 26, "anyw", 27, "aptu", 28, "arch", 29, "argo", 30, "aste", 31, "asus", 32, "attw", 33, "au-m", 34, "audi", 35, "aur ", 36, "aus ", 37, "avan", 38, "beck", 39, "bell", 40, "benq", 41, "bilb", 42, "bird", 43, "blac", 44, "blaz", 45, "brew", 46, "brvw", 47, "bumb", 48, "bw-n", 49, "bw-u", 50, "c55/", 51, "capi", 52, "ccwa", 53, "cdm-", 54, "cell", 55, "chtm", 56, "cldc", 57, "cmd-", 58, "cond", 59, "craw", 60, "dait", 61, "dall", 62, "dang", 63, "dbte", 64, "dc-s", 65, "devi", 66, "dica", 67, "dmob", 68, "doco", 69, "dopo", 70, "ds-d", 71, "ds12", 72, "el49", 73, "elai", 74, "eml2", 75, "emul", 76, "eric", 77, "erk0", 78, "esl8", 79, "ez40", 80, "ez60", 81, "ez70", 82, "ezos", 83, "ezwa", 84, "ezze", 85, "fake", 86, "fetc", 87, "fly-", 88, "fly_", 89, "g-mo", 90, "g1 u", 91, "g560", 92, "gene", 93, "gf-5", 94, "go.w", 95, "good", 96, "grad", 97, "grun", 98, "haie", 99, "hcit", 100, "hd-m", 101, "hd-p", 102, "hd-t", 103, "hei-", 104, "hiba", 105, "hipt", 106, "hita", 107, "hp i", 108, "hpip", 109, "hs-c", 110, "htc ", 111, "htc-", 112, "htc_", 113, "htca", 114, "htcg", 115, "htcp", 116, "htcs", 117, "htct", 118, "http", 119, "huaw", 120, "hutc", 121, "i-20", 122, "i-go", 123, "i-ma", 124, "i230", 125, "iac", 126, "iac-", 127, "iac/", 128, "ibro", 129, "idea", 130, "ig01", 131, "ikom", 132, "im1k", 133, "inno", 134, "ipaq", 135, "iris", 136, "jata", 137, "java", 138, "jbro", 139, "jemu", 140, "jigs", 141, "kddi", 142, "keji", 143, "kgt", 144, "kgt/", 145, "klon", 146, "kpt ", 147, "kwc-", 148, "kyoc", 149, "kyok", 150, "leno", 151, "lexi", 152, "lg g", 153, "lg-a", 154, "lg-b", 155, "lg-c", 156, "lg-d", 157, "lg-f", 158, "lg-g", 159, "lg-k", 160, "lg-l", 161, "lg-m", 162, "lg-o", 163, "lg-p", 164, "lg-s", 165, "lg-t", 166, "lg-u", 167, "lg-w", 168, "lg/k", 169, "lg/l", 170, "lg/u", 171, "lg50", 172, "lg54", 173, "lge-", 174, "lge/", 175, "libw", 176, "lynx", 177, "m-cr", 178, "m1-w", 179, "m3ga", 180, "m50/", 181, "mate", 182, "maui", 183, "maxo", 184, "mc01", 185, "mc21", 186, "mcca", 187, "medi", 188, "merc", 189, "meri", 190, "midp", 191, "mio8", 192, "mioa", 193, "mits", 194, "mmef", 195, "mo01", 196, "mo02", 197, "mobi", 198, "mode", 199, "modo", 200, "mot ", 201, "mot-", 202, "moto", 203, "motv", 204, "mozz", 205, "mt50", 206, "mtp1", 207, "mtv ", 208, "mwbp", 209, "mywa", 210, "n100", 211, "n101", 212, "n102", 213, "n202", 214, "n203", 215, "n300", 216, "n302", 217, "n500", 218, "n502", 219, "n505", 220, "n700", 221, "n701", 222, "n710", 223, "nec-", 224, "nem-", 225, "neon", 226, "netf", 227, "newg", 228, "newt", 229, "nok6", 230, "noki", 231, "nzph", 232, "o2 x", 233, "o2-x", 234, "o2im", 235, "opti", 236, "opwv", 237, "oran", 238, "owg1", 239, "p800", 240, "palm", 241, "pana", 242, "pand", 243, "pant", 244, "pdxg", 245, "pg-1", 246, "pg-2", 247, "pg-3", 248, "pg-6", 249, "pg-8", 250, "pg-c", 251, "pg13", 252, "phil", 253, "pire", 254, "play", 255, "pluc", 256, "pn-2", 257, "pock", 258, "port", 259, "pose", 260, "prox", 261, "psio", 262, "pt-g", 263, "qa-a", 264, "qc-2", 265, "qc-3", 266, "qc-5", 267, "qc-7", 268, "qc07", 269, "qc12", 270, "qc21", 271, "qc32", 272, "qc60", 273, "qci-", 274, "qtek", 275, "qwap", 276, "r380", 277, "r600", 278, "raks", 279, "rim9", 280, "rove", 281, "rozo", 282, "s55/", 283, "sage", 284, "sama", 285, "samm", 286, "sams", 287, "sany", 288, "sava", 289, "sc01", 290, "sch-", 291, "scoo", 292, "scp-", 293, "sdk/", 294, "se47", 295, "sec-", 296, "sec0", 297, "sec1", 298, "semc", 299, "send", 300, "seri", 301, "sgh-", 302, "shar", 303, "sie-", 304, "siem", 305, "sk-0", 306, "sl45", 307, "slid", 308, "smal", 309, "smar", 310, "smb3", 311, "smit", 312, "smt5", 313, "soft", 314, "sony", 315, "sp01", 316, "sph-", 317, "spv ", 318, "spv-", 319, "sy01", 320, "symb", 321, "t-mo", 322, "t218", 323, "t250", 324, "t600", 325, "t610", 326, "t618", 327, "tagt", 328, "talk", 329, "tcl-", 330, "tdg-", 331, "teli", 332, "telm", 333, "tim-", 334, "topl", 335, "tosh", 336, "treo", 337, "ts70", 338, "tsm-", 339, "tsm3", 340, "tsm5", 341, "tx-9", 342, "up.b", 343, "upg1", 344, "upsi", 345, "utst", 346, "v400", 347, "v750", 348, "veri", 349, "virg", 350, "vite", 351, "vk-v", 352, "vk40", 353, "vk50", 354, "vk52", 355, "vk53", 356, "vm40", 357, "voda", 358, "vulc", 359, "vx52", 360, "vx53", 361, "vx60", 362, "vx61", 363, "vx70", 364, "vx80", 365, "vx81", 366, "vx83", 367, "vx85", 368, "vx98", 369, "w3c ", 370, "w3c-", 371, "wap-", 372, "wapa", 373, "wapi", 374, "wapj", 375, "wapm", 376, "wapp", 377, "wapr", 378, "waps", 379, "wapt", 380, "wapu", 381, "wapv", 382, "wapy", 383, "webc", 384, "whit", 385, "wig ", 386, "winc", 387, "winw", 388, "wmlb", 389, "wonu", 390, "x700", 391, "xda-", 392, "xda2", 393, "xdag", 394, "yas-", 395, "your", 396, "zeto", 397, "zte-")))))
			{
				return 4;
			}
			return false;
		}
		public static XVar isIE8()
		{
			dynamic matches = XVar.Array();
			matches = new XVar("");
			MVCFunctions.preg_match(new XVar("/MSIE (.*?);/"), (XVar)(MVCFunctions.GetServerVariable("HTTP_USER_AGENT")), (XVar)(matches));
			return (XVar)(1 < MVCFunctions.count(matches))  && (XVar)(matches[1] <= 8);
		}
		public static XVar mobileDeviceDetected()
		{
			return false;
			return null;
		}
		public static XVar detectMobileDevice()
		{
			dynamic isMobile = null, status = null;
			return false;
			return null;
		}
		public static XVar IsMobile()
		{
			return CommonFunctions.detectMobileDevice();
		}
		public static dynamic GetPageLayout(dynamic _param_tableName, dynamic _param_pageType, dynamic _param_suffixName = null)
		{
			#region default values
			if(_param_suffixName as Object == null) _param_suffixName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic tableName = XVar.Clone(_param_tableName);
			dynamic pageType = XVar.Clone(_param_pageType);
			dynamic suffixName = XVar.Clone(_param_suffixName);
			#endregion

			dynamic layout = null, layoutName = null;
			layoutName = XVar.Clone(MVCFunctions.Concat((XVar.Pack(tableName != XVar.Pack("")) ? XVar.Pack(MVCFunctions.Concat(tableName, "_")) : XVar.Pack("")), pageType, (XVar.Pack(suffixName != XVar.Pack("")) ? XVar.Pack(MVCFunctions.Concat("_", suffixName)) : XVar.Pack(""))));
			layout = XVar.Clone(GlobalVars.page_layouts[layoutName]);
			if(XVar.Pack(layout))
			{
				if(XVar.Pack(MVCFunctions.postvalue(new XVar("pdf"))))
				{
					layout.style = XVar.Clone(layout.pdfStyle());
				}
			}
			return layout;
		}
		public static XVar isPageLayoutMobile(dynamic _param_templateFileName)
		{
			#region pass-by-value parameters
			dynamic templateFileName = XVar.Clone(_param_templateFileName);
			#endregion

			dynamic ovrd = null;
			return false;
			return null;
		}
		public static XVar extractStyle(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic pos = null, pos1 = null, quot = null;
			pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(str), new XVar("style=\""), new XVar(0)));
			quot = new XVar("\"");
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(str), new XVar("style='"), new XVar(0)));
				quot = new XVar("'");
			}
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				return null;
			}
			pos1 = XVar.Clone(MVCFunctions.strpos((XVar)(str), (XVar)(quot), (XVar)(pos + 7)));
			if(XVar.Equals(XVar.Pack(pos1), XVar.Pack(false)))
			{
				return "";
			}
			return MVCFunctions.substr((XVar)(str), (XVar)(pos + 7), (XVar)((pos1 - pos) - 7));
		}
		public static XVar injectStyle(dynamic _param_str, dynamic _param_style)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			dynamic style = XVar.Clone(_param_style);
			#endregion

			dynamic pos = null, quot = null;
			pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(str), new XVar("style=\""), new XVar(0)));
			quot = new XVar("\"");
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				pos = XVar.Clone(CommonFunctions.my_stripos((XVar)(str), new XVar("style='"), new XVar(0)));
				quot = new XVar("'");
			}
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				return MVCFunctions.Concat(str, " style=\"", style, "\"");
			}
			return MVCFunctions.Concat(MVCFunctions.substr((XVar)(str), new XVar(0), (XVar)(pos + 7)), style, ";", MVCFunctions.substr((XVar)(str), (XVar)(pos + 7)));
		}
		public static XVar isSingleSign()
		{
			if((XVar)(CommonFunctions.GetGlobalData(new XVar("ADSingleSign"), new XVar(0)))  && (XVar)(MVCFunctions.GetRemoteUser()))
			{
				return false;
			}
			return true;
		}
		public static XVar generateUserCode(dynamic _param_length)
		{
			#region pass-by-value parameters
			dynamic length = XVar.Clone(_param_length);
			#endregion

			dynamic code = null, i = null;
			code = new XVar("");
			i = new XVar(0);
			for(;i < length; i++)
			{
				code = MVCFunctions.Concat(code, MVCFunctions.rand(new XVar(0), new XVar(9)));
			}
			return code;
		}
		public static XVar generatePassword(dynamic _param_length)
		{
			#region pass-by-value parameters
			dynamic length = XVar.Clone(_param_length);
			#endregion

			dynamic i = null, j = null, password = null;
			password = new XVar("");
			i = new XVar(0);
			for(;i < length; i++)
			{
				j = XVar.Clone(MVCFunctions.rand(new XVar(0), new XVar(35)));
				if(j < 26)
				{
					password = MVCFunctions.Concat(password, MVCFunctions.chr((XVar)(MVCFunctions.ord(new XVar('a')) + j)));
				}
				else
				{
					password = MVCFunctions.Concat(password, MVCFunctions.chr((XVar)((MVCFunctions.ord(new XVar('0')) - 26) + j)));
				}
			}
			return password;
		}
		public static XVar securityCheckFileName(dynamic _param_fileName)
		{
			#region pass-by-value parameters
			dynamic fileName = XVar.Clone(_param_fileName);
			#endregion

			dynamic i = null, maliciousStrings = XVar.Array();
			maliciousStrings = XVar.Clone(new XVar(0, "../", 1, "..\\"));
			i = new XVar(0);
			for(;i < MVCFunctions.count(maliciousStrings); i++)
			{
				while(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(fileName), (XVar)(maliciousStrings[i]))), XVar.Pack(false)))
				{
					fileName = XVar.Clone(MVCFunctions.str_replace((XVar)(maliciousStrings), new XVar(""), (XVar)(fileName)));
				}
			}
			return fileName;
		}
		public static XVar getOptionsForMultiUpload(dynamic _param_pSet_packed, dynamic _param_field)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic options = XVar.Array(), uploadDir = null;
			if(XVar.Pack(pSet.isAbsolute((XVar)(field))))
			{
				uploadDir = XVar.Clone(pSet.getUploadFolder((XVar)(field)));
			}
			else
			{
				uploadDir = XVar.Clone(MVCFunctions.getabspath((XVar)(pSet.getUploadFolder((XVar)(field)))));
			}
			options = XVar.Clone(new XVar("max_file_size", pSet.getMaxFileSize((XVar)(field)), "max_totalFile_size", pSet.getMaxTotalFilesSize((XVar)(field)), "max_number_of_files", pSet.getMaxNumberOfFiles((XVar)(field)), "max_width", pSet.getMaxImageWidth((XVar)(field)), "max_height", pSet.getMaxImageHeight((XVar)(field))));
			if(XVar.Pack(pSet.getResizeOnUpload((XVar)(field))))
			{
				options.InitAndSetArrayItem(true, "resizeOnUpload");
				options.InitAndSetArrayItem(pSet.getNewImageSize((XVar)(field)), "max_width");
				options.InitAndSetArrayItem(options["max_width"], "max_height");
			}
			if(XVar.Pack(pSet.getCreateThumbnail((XVar)(field))))
			{
				options.InitAndSetArrayItem(new XVar("thumbnail", new XVar("max_width", pSet.getThumbnailSize((XVar)(field)), "max_height", pSet.getThumbnailSize((XVar)(field)), "thumbnailPrefix", pSet.getStrThumbnail((XVar)(field)))), "image_versions");
			}
			return options;
		}
		public static XVar getContentTypeByExtension(dynamic _param_ext)
		{
			#region pass-by-value parameters
			dynamic ext = XVar.Clone(_param_ext);
			#endregion

			dynamic ctype = null;
			ext = XVar.Clone(MVCFunctions.strtolower((XVar)(ext)));
			if(MVCFunctions.substr((XVar)(ext), new XVar(0), new XVar(1)) != ".")
			{
				ext = XVar.Clone(MVCFunctions.Concat(".", ext));
			}
			if(ext == ".asf")
			{
				ctype = new XVar("video/x-ms-asf");
			}
			else
			{
				if(ext == ".avi")
				{
					ctype = new XVar("video/avi");
				}
				else
				{
					if(ext == ".doc")
					{
						ctype = new XVar("application/msword");
					}
					else
					{
						if(ext == ".zip")
						{
							ctype = new XVar("application/zip");
						}
						else
						{
							if(ext == ".xls")
							{
								ctype = new XVar("application/vnd.ms-excel");
							}
							else
							{
								if(ext == ".png")
								{
									ctype = new XVar("image/png");
								}
								else
								{
									if(ext == ".gif")
									{
										ctype = new XVar("image/gif");
									}
									else
									{
										if((XVar)(ext == ".jpg")  || (XVar)(ext == "jpeg"))
										{
											ctype = new XVar("image/jpeg");
										}
										else
										{
											if(ext == ".wav")
											{
												ctype = new XVar("audio/wav");
											}
											else
											{
												if(ext == ".mp3")
												{
													ctype = new XVar("audio/mpeg");
												}
												else
												{
													if((XVar)(ext == ".mpg")  || (XVar)(ext == "mpeg"))
													{
														ctype = new XVar("video/mpeg");
													}
													else
													{
														if(ext == ".rtf")
														{
															ctype = new XVar("application/rtf");
														}
														else
														{
															if((XVar)(ext == ".htm")  || (XVar)(ext == "html"))
															{
																ctype = new XVar("text/html");
															}
															else
															{
																if(ext == ".asp")
																{
																	ctype = new XVar("text/asp");
																}
																else
																{
																	if(ext == ".flv")
																	{
																		ctype = new XVar("video/flv");
																	}
																	else
																	{
																		if(ext == ".mp4")
																		{
																			ctype = new XVar("video/mp4");
																		}
																		else
																		{
																			if(ext == ".webm")
																			{
																				ctype = new XVar("video/webm");
																			}
																			else
																			{
																				if(ext == ".pdf")
																				{
																					ctype = new XVar("application/pdf");
																				}
																				else
																				{
																					ctype = new XVar("application/octet-stream");
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return ctype;
		}
		public static XVar common_runner_sms(dynamic _param_number, dynamic _param_message, dynamic _param_parameters = null)
		{
			#region default values
			if(_param_parameters as Object == null) _param_parameters = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic number = XVar.Clone(_param_number);
			dynamic message = XVar.Clone(_param_message);
			dynamic parameters = XVar.Clone(_param_parameters);
			#endregion

			dynamic certPath = null, headers = XVar.Array(), result = XVar.Array(), url = null, var_response = XVar.Array();
			if(XVar.Pack(!(XVar)(parameters.KeyExists("To"))))
			{
				parameters.InitAndSetArrayItem(number, "To");
			}
			if(XVar.Pack(!(XVar)(parameters.KeyExists("Body"))))
			{
				parameters.InitAndSetArrayItem(message, "Body");
			}
			parameters.InitAndSetArrayItem(GlobalVars.twilioNumber, "From");
			url = XVar.Clone(MVCFunctions.Concat("https://api.twilio.com/2010-04-01/Accounts/", GlobalVars.twilioSID, "/Messages.json"));
			headers = XVar.Clone(XVar.Array());
			headers.InitAndSetArrayItem("twilio-php/5.7.3 (PHP 5.6.12)", "User-Agent");
			headers.InitAndSetArrayItem("utf-8", "Accept-Charset");
			headers.InitAndSetArrayItem("application/x-www-form-urlencoded", "Content-Type");
			headers.InitAndSetArrayItem("application/json", "Accept");
			headers.InitAndSetArrayItem(MVCFunctions.Concat("Basic ", MVCFunctions.base64_encode((XVar)(MVCFunctions.Concat("", GlobalVars.twilioSID, ":", GlobalVars.twilioAuth)))), "Authorization");
			certPath = XVar.Clone(MVCFunctions.getabspath(new XVar("include/cacert.pem")));
			result = XVar.Clone(XVar.Array());
			result.InitAndSetArrayItem(false, "success");
			var_response = XVar.Clone(MVCFunctions.runner_post_request((XVar)(url), (XVar)(parameters), (XVar)(headers), (XVar)(certPath)));
			if(XVar.Pack(!(XVar)(var_response["error"])))
			{
				result.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(var_response["content"])), "response");
				if(result["response"]["status"] == "queued")
				{
					result.InitAndSetArrayItem(true, "success");
				}
				else
				{
					result.InitAndSetArrayItem(MVCFunctions.Concat("Twilio error: ", result["response"]["message"]), "error");
				}
			}
			else
			{
				result.InitAndSetArrayItem(var_response["error"], "error");
			}
			return result;
		}
		public static XVar getLatLngByAddr(dynamic _param_addr)
		{
			#region pass-by-value parameters
			dynamic addr = XVar.Clone(_param_addr);
			#endregion

			dynamic lat = null, lng = null, result = XVar.Array(), url = null;
			switch(((XVar)CommonFunctions.getMapProvider()).ToInt())
			{
				case Constants.GOOGLE_MAPS:
					url = XVar.Clone(MVCFunctions.Concat("http://maps.googleapis.com/maps/api/geocode/json?address=", MVCFunctions.RawUrlEncode((XVar)(addr)), "&sensor=false"));
					result = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.myurl_get_contents((XVar)(url)))));
					if(result["status"] == "OK")
					{
						return result["results"][0]["geometry"]["location"];
					}
					break;
				case Constants.OPEN_STREET_MAPS:
					url = XVar.Clone(MVCFunctions.Concat("http://nominatim.openstreetmap.org/search/", MVCFunctions.RawUrlEncode((XVar)(addr)), "?format=json&addressdetails=1&limit=1"));
					result = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.myurl_get_contents((XVar)(url)))));
					if(XVar.Pack(result))
					{
						lat = XVar.Clone(result[0]["lat"]);
						if(XVar.Pack(!(XVar)(lat)))
						{
							lat = new XVar(0);
						}
						lng = XVar.Clone(result[0]["lon"]);
						if(XVar.Pack(!(XVar)(lng)))
						{
							lng = new XVar(0);
						}
						return new XVar("lat", lat, "lng", lng);
					}
					break;
				case Constants.BING_MAPS:
					if(XVar.Pack(!(XVar)(CommonFunctions.GetGlobalData(new XVar("apiGoogleMapsCode"), new XVar("")))))
					{
						return false;
					}
					url = XVar.Clone(MVCFunctions.Concat("http://dev.virtualearth.net/REST/v1/Locations?query=", MVCFunctions.RawUrlEncode((XVar)(addr)), "&output=json&key=", CommonFunctions.GetGlobalData(new XVar("apiGoogleMapsCode"), new XVar(""))));
					result = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.myurl_get_contents((XVar)(url)))));
					if(XVar.Pack(result))
					{
						lat = XVar.Clone(result["resourceSets"][0]["resources"][0]["geocodePoints"][0]["coordinates"][0]);
						if(XVar.Pack(!(XVar)(lat)))
						{
							lat = new XVar(0);
						}
						lng = XVar.Clone(result["resourceSets"][0]["resources"][0]["geocodePoints"][0]["coordinates"][1]);
						if(XVar.Pack(!(XVar)(lng)))
						{
							lng = new XVar(0);
						}
						return new XVar("lat", lat, "lng", lng);
					}
					break;
			}
			return false;
		}
		public static XVar isLoggedAsGuest()
		{
			return Security.isGuest();
		}
		public static XVar isGuestLoginAvailable()
		{
			return false;
		}
		public static XVar func_Override(dynamic _param_page)
		{
			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			#endregion

			page = XVar.Clone(MVCFunctions.str_replace(new XVar("/"), new XVar("_"), (XVar)(page)));
			if(XVar.Pack(!(XVar)(GlobalVars.globalSettings["override"].KeyExists(page))))
			{
				return Constants.otNone;
			}
			return GlobalVars.globalSettings["override"][page];
		}
		public static XVar printMFHandlerHeaders()
		{
			MVCFunctions.Header("Content-Disposition", "inline; filename=\"files.json\"");
			MVCFunctions.Header("X-Content-Type-Options", "nosniff");
			MVCFunctions.Header("Access-Control-Allow-Origin", "*");
			MVCFunctions.Header("Access-Control-Allow-Methods", "OPTIONS, HEAD, GET, POST");
			MVCFunctions.Header("Access-Control-Allow-Headers", "X-File-Name, X-File-Type, X-File-Size");
			return null;
		}
		public static XVar GetFieldType(dynamic _param_field, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if((XVar)(table != XVar.Pack(""))  || (XVar)(!(XVar)(GlobalVars.pageObject as object != null)))
			{
				dynamic newSet = null;
				if(table == XVar.Pack(""))
				{
					table = XVar.Clone(GlobalVars.strTableName);
				}
				newSet = XVar.Clone(new ProjectSettings((XVar)(table)));
				return newSet.getFieldType((XVar)(field));
			}
			else
			{
				return GlobalVars.pageObject.pSet.getFieldType((XVar)(field));
			}
			return null;
		}
		public static XVar Label(dynamic _param_field, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic result = null;
			if((XVar)(table != XVar.Pack(""))  || (XVar)(!(XVar)(GlobalVars.pageObject as object != null)))
			{
				dynamic newSet = null;
				if(table == XVar.Pack(""))
				{
					table = XVar.Clone(GlobalVars.strTableName);
				}
				newSet = XVar.Clone(new ProjectSettings((XVar)(table)));
				result = XVar.Clone(newSet.label((XVar)(field)));
			}
			else
			{
				result = XVar.Clone(GlobalVars.pageObject.pSet.label((XVar)(field)));
			}
			return (XVar.Pack(result != XVar.Pack("")) ? XVar.Pack(result) : XVar.Pack(field));
		}
		public static XVar getIconByFileType(dynamic _param_fileType, dynamic _param_sourceFileName)
		{
			#region pass-by-value parameters
			dynamic fileType = XVar.Clone(_param_fileType);
			dynamic sourceFileName = XVar.Clone(_param_sourceFileName);
			#endregion

			dynamic dotPosition = null, fileName = null;
			switch(((XVar)fileType).ToString())
			{
				case "text/html":
					fileName = new XVar("html.png");
					break;
				case "text/asp":
					fileName = new XVar("code.png");
					break;
				case "application/msword":
				case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
					fileName = new XVar("doc.png");
					break;
				case "application/vnd.ms-excel":
				case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
					fileName = new XVar("xls.png");
					break;
				case "application/rtf":
					fileName = new XVar("rtf.png");
					break;
				case "image/png":
				case "image/x-png":
					fileName = new XVar("png.png");
					break;
				case "image/gif":
					fileName = new XVar("gif.png");
					break;
				case "image/jpeg":
				case "image/pjpeg":
					fileName = new XVar("jpg.png");
					break;
				case "audio/wav":
					fileName = new XVar("wma.png");
					break;
				case "audio/mp3":
				case "audio/mpeg3":
				case "audio/mpeg":
					fileName = new XVar("mp2.png");
					break;
				case "video/mpeg":
					fileName = new XVar("mpeg.png");
					break;
				case "video/flv":
					fileName = new XVar("flv.png");
					break;
				case "video/mp4":
					fileName = new XVar("mp4.png");
					break;
				case "video/x-ms-asf":
					fileName = new XVar("asf.png");
					break;
				case "video/webm":
				case "video/x-webm":
				case "video/avi":
					fileName = new XVar("mpg.png");
					break;
				case "application/zip":
				case "application/x-zip-compressed":
					fileName = new XVar("zip.png");
					break;
				default:
					fileName = new XVar("text.png");
					dotPosition = XVar.Clone(MVCFunctions.strrpos((XVar)(sourceFileName), new XVar(".")));
					if((XVar)(!XVar.Equals(XVar.Pack(dotPosition), XVar.Pack(false)))  && (XVar)(dotPosition < MVCFunctions.strlen((XVar)(sourceFileName)) - 1))
					{
						dynamic ext = null, icons = XVar.Array();
						ext = XVar.Clone(MVCFunctions.substr((XVar)(sourceFileName), (XVar)(dotPosition + 1)));
						icons = XVar.Clone(XVar.Array());
						icons.InitAndSetArrayItem("7z", "7z");
						icons.InitAndSetArrayItem("asf", "asf");
						icons.InitAndSetArrayItem("code", "asp");
						icons.InitAndSetArrayItem("mpg", "avi");
						icons.InitAndSetArrayItem("chm", "chm");
						icons.InitAndSetArrayItem("doc", "doc");
						icons.InitAndSetArrayItem("doc", "docx");
						icons.InitAndSetArrayItem("flv", "flv");
						icons.InitAndSetArrayItem("gz", "gz");
						icons.InitAndSetArrayItem("html", "html");
						icons.InitAndSetArrayItem("mdb", "mdb");
						icons.InitAndSetArrayItem("mdb", "mdbx");
						icons.InitAndSetArrayItem("mp2", "mp3");
						icons.InitAndSetArrayItem("mp4", "mp4");
						icons.InitAndSetArrayItem("mpeg", "mpeg");
						icons.InitAndSetArrayItem("mpg", "mpg");
						icons.InitAndSetArrayItem("mov", "mov");
						icons.InitAndSetArrayItem("pdf", "pdf");
						icons.InitAndSetArrayItem("code", "php");
						icons.InitAndSetArrayItem("pps", "pps");
						icons.InitAndSetArrayItem("powerpoint", "ppt");
						icons.InitAndSetArrayItem("psd", "psd");
						icons.InitAndSetArrayItem("rar", "rar");
						icons.InitAndSetArrayItem("rtf", "rtf");
						icons.InitAndSetArrayItem("swf", "swf");
						icons.InitAndSetArrayItem("tif", "tif");
						icons.InitAndSetArrayItem("ttf", "ttf");
						icons.InitAndSetArrayItem("txt", "txt");
						icons.InitAndSetArrayItem("wav", "wav");
						icons.InitAndSetArrayItem("mpg", "webm");
						icons.InitAndSetArrayItem("wma", "wma");
						icons.InitAndSetArrayItem("emv", "wmv");
						icons.InitAndSetArrayItem("xls", "xls");
						icons.InitAndSetArrayItem("xls", "xlsx");
						icons.InitAndSetArrayItem("zip", "zip");
						if(XVar.Pack(icons.KeyExists(ext)))
						{
							fileName = XVar.Clone(MVCFunctions.Concat(icons[ext], ".png"));
						}
					}
					break;
			}
			return fileName;
		}
		public static XVar isImageType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			switch(((XVar)var_type).ToString())
			{
				case "image/png":
				case "image/x-png":
				case "image/gif":
				case "image/jpeg":
				case "image/pjpeg":
					return true;
			}
			return false;
		}
		public static XVar initArray(dynamic array, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			if(XVar.Pack(!(XVar)(array.KeyExists(key))))
			{
				array.InitAndSetArrayItem(XVar.Array(), key);
			}
			return null;
		}
		public static XVar GetKeysArray(dynamic _param_arr, dynamic _param_pageObject, dynamic _param_searchId = null)
		{
			#region default values
			if(_param_searchId as Object == null) _param_searchId = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic arr = XVar.Clone(_param_arr);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic searchId = XVar.Clone(_param_searchId);
			#endregion

			dynamic aKeys = XVar.Array(), keyfields = XVar.Array();
			keyfields = XVar.Clone(pageObject.pSet.getTableKeys());
			aKeys = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(keyfields)))
			{
				foreach (KeyValuePair<XVar, dynamic> kfield in keyfields.GetEnumerator())
				{
					if(XVar.Pack(arr.KeyExists(kfield.Value)))
					{
						aKeys.InitAndSetArrayItem(arr[kfield.Value], kfield.Value);
					}
				}
				if((XVar)(MVCFunctions.count(aKeys) == 0)  && (XVar)(searchId))
				{
					dynamic lastId = null;
					lastId = XVar.Clone(pageObject.connection.getInsertedId());
					if(XVar.Pack(0) < lastId)
					{
						aKeys.InitAndSetArrayItem(lastId, keyfields[0]);
					}
				}
			}
			return aKeys;
		}
		public static XVar GetBaseScriptsForPage(dynamic _param_isDisplayLoading, dynamic _param_additionalScripts = null, dynamic _param_customText = null)
		{
			#region default values
			if(_param_additionalScripts as Object == null) _param_additionalScripts = new XVar("");
			if(_param_customText as Object == null) _param_customText = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic isDisplayLoading = XVar.Clone(_param_isDisplayLoading);
			dynamic additionalScripts = XVar.Clone(_param_additionalScripts);
			dynamic customText = XVar.Clone(_param_customText);
			#endregion

			dynamic result = null;
			result = new XVar("");
			result = MVCFunctions.Concat(result, "<script type=\"text/javascript\">window.runnerWebRootPath=\"", MVCFunctions.urlencode((XVar)(MVCFunctions.GetWebRootPath())), "\"</script>");
			result = MVCFunctions.Concat(result, "<script type=\"text/javascript\" src=\"", MVCFunctions.GetRootPathForResources(new XVar("include/loadfirst.js")), "\"></script>");
			result = MVCFunctions.Concat(result, additionalScripts);
			result = MVCFunctions.Concat(result, "<script type=\"text/javascript\" src=\"", MVCFunctions.GetRootPathForResources((XVar)(MVCFunctions.Concat("include/lang/", CommonFunctions.getLangFileName((XVar)(CommonFunctions.mlang_getcurrentlang())), ".js"))), "\"></script>");
			if(CommonFunctions.getMapProvider() == Constants.BING_MAPS)
			{
				result = MVCFunctions.Concat(result, "<script type=\"text/javascript\" src=\"http://www.bing.com/api/maps/mapcontrol?&setMkt=", CommonFunctions.getBingMapsLang(), "\"></script>");
			}
			if(XVar.Pack(isDisplayLoading))
			{
				result = MVCFunctions.Concat(result, "<script type=\"text/javascript\">Runner.runLoading('", customText, "');</script>");
			}
			return result;
		}
		public static XVar printJSON(dynamic _param_data, dynamic _param_returnPlainJSON = null)
		{
			#region default values
			if(_param_returnPlainJSON as Object == null) _param_returnPlainJSON = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic returnPlainJSON = XVar.Clone(_param_returnPlainJSON);
			#endregion

			dynamic rJSON = null;
			rJSON = XVar.Clone(MVCFunctions.my_json_encode((XVar)(data)));
			return (XVar.Pack(returnPlainJSON) ? XVar.Pack(rJSON) : XVar.Pack(MVCFunctions.runner_htmlspecialchars((XVar)(rJSON))));
		}
		public static XVar getIntervalLimitsExprs(dynamic _param_table, dynamic _param_field, dynamic _param_idx, dynamic _param_isLowerBound)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic idx = XVar.Clone(_param_idx);
			dynamic isLowerBound = XVar.Clone(_param_isLowerBound);
			#endregion

			dynamic value = null;
			return null;
		}
		public static XVar import_error_handler(dynamic _param_errno, dynamic _param_errstr, dynamic _param_errfile, dynamic _param_errline)
		{
			#region pass-by-value parameters
			dynamic errno = XVar.Clone(_param_errno);
			dynamic errstr = XVar.Clone(_param_errstr);
			dynamic errfile = XVar.Clone(_param_errfile);
			dynamic errline = XVar.Clone(_param_errline);
			#endregion

			return null;
		}
		public static XVar PrepareForExcel(dynamic _param_ret)
		{
			#region pass-by-value parameters
			dynamic ret = XVar.Clone(_param_ret);
			#endregion

			if(MVCFunctions.substr((XVar)(ret), new XVar(0), new XVar(1)) == "=")
			{
				ret = XVar.Clone(MVCFunctions.Concat("&#61;", MVCFunctions.substr((XVar)(ret), new XVar(1))));
			}
			return ret;
		}
		public static XVar countTotals(dynamic totals, dynamic _param_totalsFields, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic totalsFields = XVar.Clone(_param_totalsFields);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic i = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(totalsFields); i++)
			{
				if(totalsFields[i]["totalsType"] == "COUNT")
				{
					totals[totalsFields[i]["fName"]]["value"] += data[totalsFields[i]["fName"]] != "";
				}
				else
				{
					if(totalsFields[i]["viewFormat"] == "Time")
					{
						dynamic time = XVar.Array();
						time = XVar.Clone(CommonFunctions.GetTotalsForTime((XVar)(data[totalsFields[i]["fName"]])));
						totals[totalsFields[i]["fName"]]["value"] += (time[2] + time[1] * 60) + time[0] * 3600;
					}
					else
					{
						totals[totalsFields[i]["fName"]]["value"] += data[totalsFields[i]["fName"]] + 0;
					}
				}
				if(totalsFields[i]["totalsType"] == "AVERAGE")
				{
					if((XVar)(!(XVar)(data[totalsFields[i]["fName"]] == null))  && (XVar)(!XVar.Equals(XVar.Pack(data[totalsFields[i]["fName"]]), XVar.Pack(""))))
					{
						totals[totalsFields[i]["fName"]]["numRows"]++;
					}
				}
			}
			return null;
		}
		public static XVar XMLNameEncode(dynamic _param_strValue)
		{
			#region pass-by-value parameters
			dynamic strValue = XVar.Clone(_param_strValue);
			#endregion

			dynamic ret = null, search = null;
			search = XVar.Clone(new XVar(0, " ", 1, "#", 2, "'", 3, "/", 4, "\\", 5, "(", 6, ")", 7, ",", 8, "["));
			ret = XVar.Clone(MVCFunctions.str_replace((XVar)(search), new XVar(""), (XVar)(strValue)));
			search = XVar.Clone(new XVar(0, "]", 1, "+", 2, "\"", 3, "-", 4, "_", 5, "|", 6, "}", 7, "{", 8, "="));
			ret = XVar.Clone(MVCFunctions.str_replace((XVar)(search), new XVar(""), (XVar)(ret)));
			return ret;
		}
		public static XVar getFileExtension(dynamic _param_fileName)
		{
			#region pass-by-value parameters
			dynamic fileName = XVar.Clone(_param_fileName);
			#endregion

			dynamic pos = null;
			pos = XVar.Clone(MVCFunctions.strrpos((XVar)(fileName), new XVar(".")));
			if(XVar.Equals(XVar.Pack(pos), XVar.Pack(false)))
			{
				return "";
			}
			return MVCFunctions.substr((XVar)(fileName), (XVar)(pos + 1));
		}
		public static dynamic getDefaultConnection()
		{
			return GlobalVars.cman.getDefault();
		}
		public static XVar isIOS()
		{
			return (XVar)((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.stripos((XVar)(MVCFunctions.GetServerVariable("HTTP_USER_AGENT")), new XVar("iPod"))), XVar.Pack(false)))  || (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.stripos((XVar)(MVCFunctions.GetServerVariable("HTTP_USER_AGENT")), new XVar("iPad"))), XVar.Pack(false))))  || (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.stripos((XVar)(MVCFunctions.GetServerVariable("HTTP_USER_AGENT")), new XVar("iPhone"))), XVar.Pack(false)));
		}
		public static XVar getMapProvider()
		{
			return CommonFunctions.GetGlobalData(new XVar("mapProvider"), new XVar(true));
		}
		public static XVar getBingMapsLang()
		{
			dynamic arrBimgMapLang = XVar.Array();
			arrBimgMapLang = XVar.Clone(XVar.Array());
			arrBimgMapLang.InitAndSetArrayItem("cs-CZ", "Czech");
			arrBimgMapLang.InitAndSetArrayItem("da-DK", "Danish");
			arrBimgMapLang.InitAndSetArrayItem("nl-NL", "Dutch");
			arrBimgMapLang.InitAndSetArrayItem("en-US", "English");
			arrBimgMapLang.InitAndSetArrayItem("fr-FR", "French");
			arrBimgMapLang.InitAndSetArrayItem("de-DE", "German");
			arrBimgMapLang.InitAndSetArrayItem("it-IT", "Italian");
			arrBimgMapLang.InitAndSetArrayItem("ja-JP", "Japanese");
			arrBimgMapLang.InitAndSetArrayItem("nb-NO", "Norwegian");
			arrBimgMapLang.InitAndSetArrayItem("pl-PL", "Polish");
			arrBimgMapLang.InitAndSetArrayItem("pt-PT", "Portugal");
			arrBimgMapLang.InitAndSetArrayItem("pt-BR", "Portuguese");
			arrBimgMapLang.InitAndSetArrayItem("ru-RU", "Russian");
			arrBimgMapLang.InitAndSetArrayItem("es-ES", "Spanish");
			arrBimgMapLang.InitAndSetArrayItem("sw-SE", "Swedish");
			arrBimgMapLang.InitAndSetArrayItem("zh-TW", "Chinese");
			arrBimgMapLang.InitAndSetArrayItem("zh-HK", "Hongkong");
			if(XVar.Pack(arrBimgMapLang.KeyExists(CommonFunctions.mlang_getcurrentlang())))
			{
				return arrBimgMapLang[CommonFunctions.mlang_getcurrentlang()];
			}
			return arrBimgMapLang["English"];
		}
		public static XVar getDefaultLanguage()
		{
			if((XVar)(MVCFunctions.strlen((XVar)(XSession.Session["language"])) == 0)  && (XVar)(MVCFunctions.GetServerVariable("HTTP_ACCEPT_LANGUAGE")))
			{
				dynamic arrLang = XVar.Array(), arrWizardLang = XVar.Array(), http_lang = null, langcode = XVar.Array();
				arrWizardLang = XVar.Clone(XVar.Array());
				arrWizardLang.InitAndSetArrayItem("English", null);
				arrLang = XVar.Clone(XVar.Array());
				arrLang.InitAndSetArrayItem("Afrikaans", "af");
				arrLang.InitAndSetArrayItem("Arabic", "ar");
				arrLang.InitAndSetArrayItem("Bosnian", "bs");
				arrLang.InitAndSetArrayItem("Bulgarian", "bg");
				arrLang.InitAndSetArrayItem("Catalan", "ca");
				arrLang.InitAndSetArrayItem("Chinese", "zh");
				arrLang.InitAndSetArrayItem("Croatian", "hr");
				arrLang.InitAndSetArrayItem("Czech", "cs");
				arrLang.InitAndSetArrayItem("Danish", "da");
				arrLang.InitAndSetArrayItem("Dutch", "nl");
				arrLang.InitAndSetArrayItem("English", "en");
				arrLang.InitAndSetArrayItem("Farsi", "fa");
				arrLang.InitAndSetArrayItem("French", "fr");
				arrLang.InitAndSetArrayItem("Georgian", "ka");
				arrLang.InitAndSetArrayItem("German", "de");
				arrLang.InitAndSetArrayItem("Greek", "el");
				arrLang.InitAndSetArrayItem("Hebrew", "he");
				arrLang.InitAndSetArrayItem("Hongkong", "hk");
				arrLang.InitAndSetArrayItem("Hungarian", "hu");
				arrLang.InitAndSetArrayItem("Indonesian", "id");
				arrLang.InitAndSetArrayItem("Italian", "it");
				arrLang.InitAndSetArrayItem("Japanese", "ja");
				arrLang.InitAndSetArrayItem("Malay", "ms");
				arrLang.InitAndSetArrayItem("Norwegian", "no");
				arrLang.InitAndSetArrayItem("Phillipines", "fl");
				arrLang.InitAndSetArrayItem("Polish", "pl");
				arrLang.InitAndSetArrayItem("Portugal", "pt");
				arrLang.InitAndSetArrayItem("Portuguese", "br");
				arrLang.InitAndSetArrayItem("Romanian", "ro");
				arrLang.InitAndSetArrayItem("Russian", "ru");
				arrLang.InitAndSetArrayItem("Slovak", "sk");
				arrLang.InitAndSetArrayItem("Spanish", "es");
				arrLang.InitAndSetArrayItem("Swedish", "sv");
				arrLang.InitAndSetArrayItem("Taiwan", "tw");
				arrLang.InitAndSetArrayItem("Thai", "th");
				arrLang.InitAndSetArrayItem("Turkish", "tr");
				arrLang.InitAndSetArrayItem("Urdu", "ur");
				arrLang.InitAndSetArrayItem("Welsh", "cy");
				http_lang = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.GetServerVariable("HTTP_ACCEPT_LANGUAGE"))));
				http_lang = XVar.Clone(MVCFunctions.str_replace(new XVar(";"), new XVar(","), (XVar)(http_lang)));
				http_lang = XVar.Clone(MVCFunctions.str_replace(new XVar("-"), new XVar(","), (XVar)(http_lang)));
				langcode = XVar.Clone(XVar.Array());
				langcode = XVar.Clone(MVCFunctions.explode(new XVar(","), (XVar)(http_lang)));
				foreach (KeyValuePair<XVar, dynamic> lang in langcode.GetEnumerator())
				{
					if(XVar.Pack(MVCFunctions.in_array((XVar)(arrLang[lang.Value]), (XVar)(arrWizardLang))))
					{
						return arrLang[lang.Value];
					}
				}
			}
			return "English";
			return null;
		}
		public static XVar xt_showchart(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic chartParams = XVar.Array(), chartPreview = null, height = null, refresh = null, settings = null, showDetails = null, width = null;
			width = new XVar(700);
			height = new XVar(530);
			chartPreview = new XVar("");
			if(XVar.Pack(var_params["chartPreview"]))
			{
				chartPreview = new XVar("&chartPreview=true");
			}
			if(XVar.Pack(var_params.KeyExists("custom1")))
			{
				width = XVar.Clone(var_params["custom1"]);
			}
			if(XVar.Pack(var_params.KeyExists("custom2")))
			{
				height = XVar.Clone(var_params["custom2"]);
			}
			if(XVar.Pack(var_params["dashResize"]))
			{
				if((XVar)(var_params["dashWidth"])  && (XVar)(var_params["dashHeight"]))
				{
					width = XVar.Clone(var_params["dashWidth"]);
					height = XVar.Clone(var_params["dashHeight"]);
				}
				else
				{
					if(XVar.Pack(var_params["dashWidth"]))
					{
						height = XVar.Clone((XVar)Math.Round((double)((height * var_params["dashWidth"]) / width)));
						width = XVar.Clone(var_params["dashWidth"]);
					}
					else
					{
						if(XVar.Pack(var_params["dashHeight"]))
						{
							width = XVar.Clone((XVar)Math.Round((double)((width * var_params["dashHeight"]) / height)));
							height = XVar.Clone(var_params["dashHeight"]);
						}
					}
				}
				width *= 0.950000;
				height *= 0.950000;
			}
			else
			{
				if(XVar.Pack(var_params["resize"]))
				{
					dynamic maxHeight = null, maxWidth = null, r = null, r2 = null;
					maxWidth = new XVar(400);
					maxHeight = new XVar(280);
					r = XVar.Clone(maxWidth / maxHeight);
					r2 = XVar.Clone(width / height);
					if((XVar)(maxWidth < width)  || (XVar)(maxHeight < height))
					{
						if(r <= r2)
						{
							height = XVar.Clone((XVar)Math.Round((double)((height * maxWidth) / width)));
							width = XVar.Clone(maxWidth);
						}
						else
						{
							width = XVar.Clone((XVar)Math.Round((double)((width * maxHeight) / height)));
							height = XVar.Clone(maxHeight);
						}
					}
				}
			}
			showDetails = XVar.Clone((XVar.Pack(var_params.KeyExists("showDetails")) ? XVar.Pack(var_params["showDetails"]) : XVar.Pack(true)));
			settings = XVar.Clone(new ProjectSettings((XVar)(CommonFunctions.GetTableByShort((XVar)(var_params["chartName"])))));
			refresh = XVar.Clone(settings.getChartRefreshTime());
			chartParams = XVar.Clone(XVar.Array());
			chartParams.InitAndSetArrayItem(width, "width");
			chartParams.InitAndSetArrayItem(height, "height");
			chartParams.InitAndSetArrayItem(showDetails, "showDetails");
			chartParams.InitAndSetArrayItem(var_params["chartName"], "chartName");
			chartParams.InitAndSetArrayItem(MVCFunctions.Concat("rnr", var_params["chartName"], var_params["id"]), "containerId");
			chartParams.InitAndSetArrayItem(var_params["ctype"], "chartType");
			chartParams.InitAndSetArrayItem(refresh, "refreshTime");
			chartParams.InitAndSetArrayItem(MVCFunctions.Concat(MVCFunctions.GetTableLink(new XVar("dchartdata")), "?chartname=", var_params["chartName"], chartPreview, "&ctype=", var_params["ctype"], "&showDetails=", showDetails), "xmlFile");
			if((XVar)(var_params.KeyExists("dash"))  && (XVar)(var_params["dash"]))
			{
				chartParams["xmlFile"] = MVCFunctions.Concat(chartParams["xmlFile"], "&dashChart=", var_params["dash"]);
				chartParams.InitAndSetArrayItem(!(XVar)(!(XVar)(var_params["dash"])), "dashChart");
			}
			chartParams.InitAndSetArrayItem(var_params["id"], "pageId");
			if((XVar)(var_params.KeyExists("dashTName"))  && (XVar)(var_params["dashTName"]))
			{
				dynamic dashElement = XVar.Array(), dashSet = null;
				chartParams.InitAndSetArrayItem(var_params["dashTName"], "dashTName");
				chartParams.InitAndSetArrayItem(var_params["dashElementName"], "dashElementName");
				chartParams.InitAndSetArrayItem(var_params["id"], "pageId");
				chartParams["xmlFile"] = MVCFunctions.Concat(chartParams["xmlFile"], "&dashTName=", var_params["dashTName"]);
				chartParams["xmlFile"] = MVCFunctions.Concat(chartParams["xmlFile"], "&dashElName=", var_params["dashElementName"]);
				chartParams["xmlFile"] = MVCFunctions.Concat(chartParams["xmlFile"], "&pageId=", var_params["id"]);
				dashSet = XVar.Clone(new ProjectSettings((XVar)(var_params["dashTName"])));
				dashElement = XVar.Clone(dashSet.getDashboardElementData((XVar)(var_params["dashElementName"])));
				if(XVar.Pack(dashElement))
				{
					if(XVar.Pack(dashElement["reload"]))
					{
						chartParams.InitAndSetArrayItem(dashElement["reload"], "refreshTime");
					}
				}
			}
			if(XVar.Pack(var_params.KeyExists("refreshTime")))
			{
				chartParams.InitAndSetArrayItem(var_params["refreshTime"], "refreshTime");
			}
			MVCFunctions.Echo(MVCFunctions.Concat("\t<style>\t@media (min-width:768px) { #", chartParams["containerId"], " {width:", width, "px;height:", height, "px; } } </style>"));
			MVCFunctions.Echo(MVCFunctions.Concat("<div class=\"bs-chart\" id=\"", chartParams["containerId"], "\"></div>"));
			if((XVar)(true)  || (XVar)(!(XVar)(var_params["singlePage"])))
			{
				chartParams.InitAndSetArrayItem(MVCFunctions.GetWebRootPath(), "webRootPath");
				MVCFunctions.Echo(MVCFunctions.Concat("<div data-runner-chart-params=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.my_json_encode((XVar)(chartParams)))), "\"></div>"));
			}
			return null;
		}
		public static XVar setHomePage(dynamic _param_url)
		{
			#region pass-by-value parameters
			dynamic url = XVar.Clone(_param_url);
			#endregion

			GlobalVars.globalSettings.InitAndSetArrayItem(2, "LandingPageType");
			GlobalVars.globalSettings.InitAndSetArrayItem(url, "LandingURL");
			return null;
		}
		public static XVar getHomePage()
		{
			if(GlobalVars.globalSettings["LandingPageType"] == 2)
			{
				return GlobalVars.globalSettings["LandingURL"];
			}
			if(GlobalVars.globalSettings["LandingPageType"] == 0)
			{
				return MVCFunctions.GetLocalLink(new XVar("menu"));
			}
			if((XVar)((XVar)(GlobalVars.globalSettings["LandingPage"] == "")  || (XVar)(GlobalVars.globalSettings["LandingPage"] == "login"))  || (XVar)(GlobalVars.globalSettings["LandingPage"] == "register"))
			{
				return MVCFunctions.GetLocalLink(new XVar("menu"));
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(GlobalVars.globalSettings["LandingTable"]))))
			{
				return MVCFunctions.GetLocalLink((XVar)(CommonFunctions.GetTableURL((XVar)(GlobalVars.globalSettings["LandingTable"]))), (XVar)(GlobalVars.globalSettings["LandingPage"]));
			}
			else
			{
				return MVCFunctions.GetLocalLink((XVar)(GlobalVars.globalSettings["LandingPage"]));
			}
			return null;
		}
		public static XVar printHomeLink(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(MVCFunctions.runner_htmlspecialchars((XVar)(CommonFunctions.getHomePage())));
			return null;
		}
		public static XVar setProjectLogo(dynamic _param_html, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic html = XVar.Clone(_param_html);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			if(MVCFunctions.strlen((XVar)(lng)) == 0)
			{
				lng = XVar.Clone(CommonFunctions.mlang_getcurrentlang());
			}
			GlobalVars.globalSettings.InitAndSetArrayItem(html, "ProjectLogo", lng);
			return null;
		}
		public static XVar getProjectLogo(dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			if(MVCFunctions.strlen((XVar)(lng)) == 0)
			{
				lng = XVar.Clone(CommonFunctions.mlang_getcurrentlang());
			}
			return GlobalVars.globalSettings["ProjectLogo"][lng];
		}
		public static XVar printProjectLogo(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(CommonFunctions.getProjectLogo((XVar)(CommonFunctions.mlang_getcurrentlang())));
			return null;
		}
		public static XVar xt_pagetitlelabel(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic record = null, settings = null;
			record = XVar.Clone((XVar.Pack(var_params.KeyExists("record")) ? XVar.Pack(var_params["record"]) : XVar.Pack(null)));
			settings = XVar.Clone((XVar.Pack(var_params.KeyExists("settings")) ? XVar.Pack(var_params["settings"]) : XVar.Pack(null)));
			if(XVar.Pack(var_params.KeyExists("custom2")))
			{
				MVCFunctions.Echo(GlobalVars.pageObject.getPageTitle((XVar)(var_params["custom2"]), (XVar)(var_params["custom1"]), (XVar)(record), (XVar)(settings)));
			}
			else
			{
				MVCFunctions.Echo(GlobalVars.pageObject.getPageTitle((XVar)(var_params["custom1"]), new XVar(""), (XVar)(record), (XVar)(settings)));
			}
			return null;
		}
		public static XVar xt_label(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(CommonFunctions.GetFieldLabel((XVar)(var_params["custom1"]), (XVar)(var_params["custom2"])));
			return null;
		}
		public static XVar xt_tooltip(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(CommonFunctions.GetFieldToolTip((XVar)(var_params["custom1"]), (XVar)(var_params["custom2"])));
			return null;
		}
		public static XVar xt_custom(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(CommonFunctions.GetCustomLabel((XVar)(var_params["custom1"])));
			return null;
		}
		public static XVar xt_caption(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			MVCFunctions.Echo(CommonFunctions.GetTableCaption((XVar)(var_params["custom1"])));
			return null;
		}
		public static XVar xt_displaytabs(dynamic _param_tabparams)
		{
			#region pass-by-value parameters
			dynamic tabparams = XVar.Clone(_param_tabparams);
			#endregion

			if((XVar)(!(XVar)(GlobalVars.pageObject as object != null))  || (XVar)(!(XVar)(tabparams.KeyExists("custom1"))))
			{
				return null;
			}
			GlobalVars.pageObject.displayTabsSections((XVar)(tabparams["custom1"]));
			return null;
		}
		public static XVar xt_buildeditcontrol(dynamic var_params)
		{
			dynamic additionalCtrlParams = null, data = null, extraParams = null, field = null, fieldNum = null, id = null, mode = null, pageObj = null, validate = null;
			pageObj = XVar.Clone(var_params["pageObj"]);
			data = XVar.Clone(pageObj.getFieldControlsData());
			field = XVar.Clone(var_params["field"]);
			if(var_params["mode"] == "edit")
			{
				mode = new XVar(Constants.MODE_EDIT);
			}
			else
			{
				if(var_params["mode"] == "add")
				{
					mode = new XVar(Constants.MODE_ADD);
				}
				else
				{
					if(var_params["mode"] == "inline_edit")
					{
						mode = new XVar(Constants.MODE_INLINE_EDIT);
					}
					else
					{
						if(var_params["mode"] == "inline_add")
						{
							mode = new XVar(Constants.MODE_INLINE_ADD);
						}
						else
						{
							mode = new XVar(Constants.MODE_SEARCH);
						}
					}
				}
			}
			fieldNum = new XVar(0);
			if(XVar.Pack(var_params["fieldNum"]))
			{
				fieldNum = XVar.Clone(var_params["fieldNum"]);
			}
			id = new XVar("");
			if(!XVar.Equals(XVar.Pack(var_params["id"]), XVar.Pack("")))
			{
				id = XVar.Clone(var_params["id"]);
			}
			validate = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(var_params["validate"])))
			{
				validate = XVar.Clone(var_params["validate"]);
			}
			additionalCtrlParams = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(var_params["additionalCtrlParams"])))
			{
				additionalCtrlParams = XVar.Clone(var_params["additionalCtrlParams"]);
			}
			extraParams = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(var_params["extraParams"])))
			{
				extraParams = XVar.Clone(var_params["extraParams"]);
			}
			pageObj.getControl((XVar)(field), (XVar)(id), (XVar)(extraParams)).buildControl((XVar)(var_params["value"]), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			return null;
		}
		public static XVar getArrayElementNC(dynamic arr, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic keys = XVar.Array(), uKey = null;
			if(XVar.Pack(arr.KeyExists(key)))
			{
				return arr[key];
			}
			keys = XVar.Clone(MVCFunctions.array_keys((XVar)(arr)));
			uKey = XVar.Clone(MVCFunctions.strtoupper((XVar)(key)));
			foreach (KeyValuePair<XVar, dynamic> k in keys.GetEnumerator())
			{
				if(MVCFunctions.strtoupper((XVar)(k.Value)) == uKey)
				{
					return arr[k.Value];
				}
			}
			return null;
		}
		public static XVar getSessionElementNC(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic ukey = null;
			if(XVar.Pack(XSession.Session.KeyExists(key)))
			{
				return XSession.Session[key];
			}
			ukey = XVar.Clone(MVCFunctions.strtoupper((XVar)(key)));
			foreach (KeyValuePair<XVar, dynamic> v in XSession.Session.GetEnumerator())
			{
				if(MVCFunctions.strtoupper((XVar)(v.Key)) == ukey)
				{
					return v.Value;
				}
			}
			return null;
		}
		public static XVar prepareLookupWhere(dynamic _param_field, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic where = null;
			where = XVar.Clone(pSet.getLookupWhere((XVar)(field)));
			if(XVar.Pack(pSet.isLookupWhereCode((XVar)(field))))
			{
				return where;
			}
			return DB.PrepareSQL((XVar)(pSet.getLookupWhere((XVar)(field))));
		}
		public static XVar verifyRecaptchaResponse(dynamic _param_response)
		{
			#region pass-by-value parameters
			dynamic var_response = XVar.Clone(_param_response);
			#endregion

			dynamic answers = XVar.Array(), captchaSettings = XVar.Array(), data = XVar.Array(), errors = XVar.Array(), message = null, req = XVar.Array(), success = null, verifyUrl = null;
			verifyUrl = new XVar("https://www.google.com/recaptcha/api/siteverify?");
			errors = XVar.Clone(XVar.Array());
			errors.InitAndSetArrayItem("Invalid security code.", "missing-input-response");
			errors.InitAndSetArrayItem("Invalid security code.", "invalid-input-response");
			errors.InitAndSetArrayItem("The secret parameter is missing", "missing-input-secret");
			errors.InitAndSetArrayItem("The secret parameter is invalid or malformed", "invalid-input-secret");
			errors.InitAndSetArrayItem("The request is invalid or malformed", "bad-request");
			captchaSettings = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("CaptchaSettings"), new XVar("")));
			data = XVar.Clone(XVar.Array());
			data.InitAndSetArrayItem(captchaSettings["secretKey"], "secret");
			data.InitAndSetArrayItem(var_response, "response");
			data.InitAndSetArrayItem(MVCFunctions.remoteAddr(), "remoteIp");
			req = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in data.GetEnumerator())
			{
				req.InitAndSetArrayItem(MVCFunctions.Concat(value.Key, "=", MVCFunctions.RawUrlEncode((XVar)(value.Value))), null);
			}
			var_response = XVar.Clone(MVCFunctions.myurl_get_contents((XVar)(MVCFunctions.Concat(verifyUrl, MVCFunctions.implode(new XVar("&"), (XVar)(req))))));
			answers = XVar.Clone(MVCFunctions.my_json_decode((XVar)(var_response)));
			message = new XVar("");
			if(var_response == XVar.Pack(""))
			{
				success = new XVar(false);
				message = new XVar("Unable to contact reCaptcha server");
			}
			else
			{
				if(XVar.Pack(!(XVar)(answers.KeyExists("success"))))
				{
					success = new XVar(false);
					message = XVar.Clone(MVCFunctions.Concat("Unable to contact reCaptcha server<br>", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.substr((XVar)(var_response), new XVar(0), new XVar(100))))));
				}
				else
				{
					dynamic code = null, i = null;
					success = XVar.Clone(answers["success"]);
					i = new XVar(0);
					for(;i < MVCFunctions.count(answers["error-codes"]); ++(i))
					{
						code = XVar.Clone(answers["error-codes"][i]);
						if(XVar.Pack(errors.KeyExists(code)))
						{
							answers.InitAndSetArrayItem(errors[code], "error-codes", i);
						}
						else
						{
							answers.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(code)), "error-codes", i);
						}
						message = XVar.Clone(MVCFunctions.implode(new XVar("<br>"), (XVar)(answers["error-codes"])));
					}
				}
			}
			return new XVar("success", success, "message", message);
		}
	}
}
