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
	public partial class Security : XClass
	{
		public static XVar processPageSecurity(dynamic _param_table, dynamic _param_permission, dynamic _param_ajaxMode = null, dynamic _param_message = null)
		{
			#region default values
			if(_param_ajaxMode as Object == null) _param_ajaxMode = new XVar(false);
			if(_param_message as Object == null) _param_message = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic permission = XVar.Clone(_param_permission);
			dynamic ajaxMode = XVar.Clone(_param_ajaxMode);
			dynamic message = XVar.Clone(_param_message);
			#endregion

			if(XVar.Pack(checkPagePermissions((XVar)(table), (XVar)(permission))))
			{
				return true;
			}
			if(XVar.Pack(ajaxMode))
			{
				sendPermissionError((XVar)(message));
				return false;
			}
			if((XVar)(CommonFunctions.isLogged())  && (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())))
			{
				MVCFunctions.HeaderRedirect(new XVar("menu"));
				return false;
			}
			CommonFunctions.redirectToLogin();
			return false;
		}
		public static XVar processAdminPageSecurity(dynamic _param_ajaxMode = null)
		{
			#region default values
			if(_param_ajaxMode as Object == null) _param_ajaxMode = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic ajaxMode = XVar.Clone(_param_ajaxMode);
			#endregion

			processLogoutRequest();
			if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(CommonFunctions.isLoggedAsGuest()))
			{
				tryRelogin();
			}
			if(XVar.Pack(CommonFunctions.IsAdmin()))
			{
				return true;
			}
			if(XVar.Pack(ajaxMode))
			{
				sendPermissionError();
				return false;
			}
			if((XVar)(CommonFunctions.isLogged())  && (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())))
			{
				MVCFunctions.HeaderRedirect(new XVar("menu"));
				return false;
			}
			saveRedirectURL();
			CommonFunctions.redirectToLogin();
			return false;
		}
		public static XVar saveRedirectURL()
		{
			dynamic query = null, url = null;
			url = XVar.Clone(MVCFunctions.GetScriptName());
			query = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> value in MVCFunctions.EnumeratePOST())
			{
				if((XVar)(value.Key == "a")  && (XVar)(value.Value == "logout"))
				{
					continue;
				}
				if(query != XVar.Pack(""))
				{
					query = MVCFunctions.Concat(query, "&");
				}
				if(XVar.Pack(MVCFunctions.is_array((XVar)(value.Value))))
				{
					query = MVCFunctions.Concat(query, MVCFunctions.RawUrlEncode((XVar)(MVCFunctions.Concat(value.Key, "[]"))), "=");
					query = MVCFunctions.Concat(query, MVCFunctions.implode((XVar)(MVCFunctions.Concat(MVCFunctions.RawUrlEncode((XVar)(MVCFunctions.Concat(value.Key, "[]"))), "=")), (XVar)(value.Value)));
				}
				else
				{
					query = MVCFunctions.Concat(query, MVCFunctions.RawUrlEncode((XVar)(value.Key)));
					if(XVar.Pack(MVCFunctions.strlen((XVar)(value.Value))))
					{
						query = MVCFunctions.Concat(query, "=", MVCFunctions.RawUrlEncode((XVar)(value.Value)));
					}
				}
			}
			if(query != XVar.Pack(""))
			{
				url = MVCFunctions.Concat(url, "?", query);
			}
			XSession.Session["MyURL"] = url;
			return null;
		}
		public static XVar checkPagePermissions(dynamic _param_table, dynamic _param_permission)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic permission = XVar.Clone(_param_permission);
			#endregion

			dynamic ret = null;
			processLogoutRequest();
			saveRedirectURL();
			ret = XVar.Clone(checkUserPermissions((XVar)(table), (XVar)(permission)));
			XSession.Session["MyUrlAccess"] = ret;
			return ret;
		}
		protected static XVar createLoginPageObject()
		{
			dynamic loginPageObject = null, loginParams = XVar.Array(), loginXt = null;
			loginXt = XVar.Clone(new XTempl());
			loginParams = XVar.Clone(new XVar("pageType", Constants.PAGE_LOGIN));
			loginParams.InitAndSetArrayItem(loginXt, "xt");
			loginParams.InitAndSetArrayItem(Constants.NOT_TABLE_BASED_TNAME, "tName");
			loginParams.InitAndSetArrayItem(false, "needSearchClauseObj");
			loginPageObject = XVar.Clone(new LoginPage((XVar)(loginParams)));
			loginPageObject.init();
			return loginPageObject;
		}
		public static XVar tryRelogin()
		{
			dynamic loginPageObject = null, password = null, username = null;
			username = XVar.Clone(MVCFunctions.GetCookie("username"));
			password = XVar.Clone(MVCFunctions.GetCookie("password"));
			if((XVar)(username == XVar.Pack(""))  || (XVar)(password == XVar.Pack("")))
			{
				return false;
			}
			loginPageObject = XVar.Clone(createLoginPageObject());
			if(XVar.Pack(loginPageObject.twoFactAuth))
			{
				return false;
			}
			return loginPageObject.LogIn((XVar)(username), (XVar)(password));
		}
		public static XVar checkUserPermissions(dynamic _param_table, dynamic _param_permission)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic permission = XVar.Clone(_param_permission);
			#endregion

			if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(CommonFunctions.isLoggedAsGuest()))
			{
				tryRelogin();
			}
			if(table == Constants.ADMIN_USERS)
			{
				return CommonFunctions.IsAdmin();
			}
			return CommonFunctions.CheckTablePermissions((XVar)(table), (XVar)(permission));
		}
		public static XVar processLogoutRequest()
		{
			dynamic loginPageObject = null;
			if((XVar)((XVar)(MVCFunctions.postvalue(new XVar("a")) != "logout")  || (XVar)(!(XVar)(CommonFunctions.isLogged())))  || (XVar)(CommonFunctions.isLoggedAsGuest()))
			{
				return false;
			}
			loginPageObject = XVar.Clone(createLoginPageObject());
			loginPageObject.Logout();
			doGuestLogin();
			GlobalVars.logoutPerformed = new XVar(true);
			return true;
		}
		public static XVar sendPermissionError(dynamic _param_message = null)
		{
			#region default values
			if(_param_message as Object == null) _param_message = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic message = XVar.Clone(_param_message);
			#endregion

			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(new XVar("success", false, "message", MVCFunctions.Concat("You don't have permissions to access this table", message)))));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public static XVar redirectToList(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic settings = null;
			settings = XVar.Clone(new ProjectSettings((XVar)(table)));
			if(XVar.Pack(settings.hasListPage()))
			{
				MVCFunctions.HeaderRedirect((XVar)(settings.getShortTableName()), new XVar("list"), new XVar("a=return"));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			MVCFunctions.HeaderRedirect(new XVar("menu"));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public static XVar clearSecuritySession()
		{
			dynamic toClear = XVar.Array();
			XSession.Session.Abandon();
			MVCFunctions.SetCookie(new XVar("username"), new XVar(""), (XVar)(MVCFunctions.time() - (365 * 1440) * 60));
			MVCFunctions.SetCookie(new XVar("password"), new XVar(""), (XVar)(MVCFunctions.time() - (365 * 1440) * 60));
			MVCFunctions.RemoveCookie("username");
			MVCFunctions.RemoveCookie("password");
			XSession.Session.Remove("UserID");
			XSession.Session.Remove("UserName");
			XSession.Session.Remove("AccessLevel");
			XSession.Session.Remove("fromFacebook");
			XSession.Session.Remove("UserRights");
			XSession.Session.Remove("LastReadRights");
			XSession.Session.Remove("GroupID");
			XSession.Session.Remove("OwnerID");
			XSession.Session.Remove("securityOverrides");
			toClear = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> v in XSession.Session.GetEnumerator())
			{
				if(MVCFunctions.substr((XVar)(v.Key), new XVar(-8)) == "_OwnerID")
				{
					toClear.InitAndSetArrayItem(v.Key, null);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> v in toClear.GetEnumerator())
			{
				XSession.Session.Remove(v.Key);
			}
			return null;
		}
		public static XVar doGuestLogin()
		{
			dynamic allowGuest = null;
			return null;
		}
		public static XVar getUserGroup()
		{
			dynamic userGroups = XVar.Array();
			userGroups = XVar.Clone(getUserGroups());
			foreach (KeyValuePair<XVar, dynamic> v in userGroups.GetEnumerator())
			{
				return v.Key;
			}
			return "";
		}
		public static XVar getUserGroupIds()
		{
			dynamic groups = XVar.Array();
			if((XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_NONE)  || (XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_HARDCODED))
			{
				return XVar.Array();
			}
			if(XVar.Pack(!(XVar)(GlobalVars.globalSettings["isDynamicPerm"])))
			{
				if(XVar.Pack(XSession.Session["GroupID"]))
				{
					return new XVar(XSession.Session["GroupID"], true);
				}
				return XVar.Array();
			}
			groups = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> g in XSession.Session["UserRights"][XSession.Session["UserID"]][".Groups"].GetEnumerator())
			{
				groups.InitAndSetArrayItem(true, g.Value);
			}
			return groups;
		}
		public static XVar getUserGroups()
		{
			dynamic data = XVar.Array(), grConnection = null, groupIds = XVar.Array(), groupNames = XVar.Array(), qResult = null, sql = null;
			if((XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_NONE)  || (XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_HARDCODED))
			{
				return XVar.Array();
			}
			if((XVar)(!(XVar)(GlobalVars.globalSettings["isDynamicPerm"]))  || (XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_AD))
			{
				return getUserGroupIds();
			}
			groupIds = XVar.Clone(getUserGroupIds());
			groupNames = XVar.Clone(XVar.Array());
			grConnection = XVar.Clone(GlobalVars.cman.getForUserGroups());
			sql = XVar.Clone(MVCFunctions.Concat("select ", grConnection.addFieldWrappers(new XVar("")), " from ", grConnection.addTableWrappers(new XVar("uggroups")), " WHERE ", grConnection.addFieldWrappers(new XVar("")), " in ( ", MVCFunctions.implode(new XVar(","), (XVar)(MVCFunctions.array_keys((XVar)(groupIds)))), ")"));
			qResult = XVar.Clone(grConnection.query((XVar)(sql)));
			while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				groupNames.InitAndSetArrayItem(true, data[0]);
			}
			if(XVar.Pack(groupIds[-1]))
			{
				groupNames.InitAndSetArrayItem(true, "<Admin>");
			}
			return groupNames;
		}
		public static XVar getUserName()
		{
			return XSession.Session["UserID"];
		}
		public static XVar getDisplayName()
		{
			return XSession.Session["UserName"];
		}
		public static XVar setDisplayName(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			XSession.Session["UserName"] = str;
			return null;
		}
		public static XVar isGuest()
		{
			if((XVar)(XSession.Session["UserID"] == "Guest")  && (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_GUEST))
			{
				return true;
			}
			return false;
		}
		public static XVar isAdmin()
		{
			if((XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_NONE)  || (XVar)(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_HARDCODED))
			{
				return false;
			}
			if(XVar.Pack(GlobalVars.globalSettings["isDynamicPerm"]))
			{
				return XSession.Session["UserRights"][XSession.Session["UserID"]][".IsAdmin"];
			}
			if(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_TABLE)
			{
				return Constants.ACCESS_LEVEL_ADMIN == XSession.Session["AccessLevel"];
			}
			return false;
		}
		public static XVar isLoggedIn()
		{
			return (XVar)(XSession.Session["UserID"] != "")  && (XVar)(!(XVar)(isGuest()));
		}
		public static XVar loginAs(dynamic _param_username, dynamic _param_fireEvents = null)
		{
			#region default values
			if(_param_fireEvents as Object == null) _param_fireEvents = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			dynamic fireEvents = XVar.Clone(_param_fireEvents);
			#endregion

			dynamic loginPageObject = null;
			loginPageObject = XVar.Clone(createLoginPageObject());
			return loginPageObject.LogIn((XVar)(username), new XVar(""), new XVar(true), (XVar)(fireEvents));
		}
		public static XVar checkUsernamePassword(dynamic _param_username, dynamic _param_password, dynamic _param_fireEvents = null)
		{
			#region default values
			if(_param_fireEvents as Object == null) _param_fireEvents = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			dynamic password = XVar.Clone(_param_password);
			dynamic fireEvents = XVar.Clone(_param_fireEvents);
			#endregion

			dynamic loginPageObject = null;
			loginPageObject = XVar.Clone(createLoginPageObject());
			if(XVar.Pack(loginPageObject.checkUsernamePassword((XVar)(username), (XVar)(password))))
			{
				return true;
			}
			if(XVar.Pack(fireEvents))
			{
				loginPageObject.doAfterUnsuccessfulLog((XVar)(username));
				loginPageObject.callAfterUnsuccessfulLoginEvent();
			}
			return false;
		}
		public static XVar getUserData(dynamic _param_username, dynamic _param_password = null)
		{
			#region default values
			if(_param_password as Object == null) _param_password = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			dynamic password = XVar.Clone(_param_password);
			#endregion

			dynamic loginPageObject = null;
			loginPageObject = XVar.Clone(createLoginPageObject());
			return loginPageObject.getUserData((XVar)(username), (XVar)(password), (XVar)(XVar.Pack("") == password));
		}
		public static XVar currentUserData()
		{
			return XSession.Session["UserData"];
		}
		public static XVar logout()
		{
			dynamic loginPageObject = null;
			loginPageObject = XVar.Clone(createLoginPageObject());
			loginPageObject.Logout();
			return null;
		}
		public static XVar getPermissions(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return XVar.Array();
			}
			return permMask2Array((XVar)(CommonFunctions.GetUserPermissions((XVar)(table))));
		}
		public static XVar setPermissions(dynamic _param_table, dynamic _param_rights)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic rights = XVar.Clone(_param_rights);
			#endregion

			dynamic strPerm = null;
			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return null;
			}
			strPerm = XVar.Clone(permArray2Mask((XVar)(rights)));
			if(XVar.Pack(!(XVar)(XSession.Session.KeyExists("securityOverrides"))))
			{
				XSession.Session["securityOverrides"] = XVar.Array();
			}
			XSession.Session.InitAndSetArrayItem(strPerm, "securityOverrides", table);
			return null;
		}
		private static XVar permMask2Array(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic c = null, i = null, ret = XVar.Array();
			ret = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.strlen((XVar)(str)); ++(i))
			{
				c = XVar.Clone(MVCFunctions.substr((XVar)(str), (XVar)(i), new XVar(1)));
				if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(c == "A")  || (XVar)(c == "D"))  || (XVar)(c == "E"))  || (XVar)(c == "S"))  || (XVar)(c == "P"))  || (XVar)(c == "I"))  || (XVar)(c == "M"))
				{
					ret.InitAndSetArrayItem(true, c);
				}
			}
			return ret;
		}
		private static XVar permArray2Mask(dynamic _param_rights)
		{
			#region pass-by-value parameters
			dynamic rights = XVar.Clone(_param_rights);
			#endregion

			dynamic str = null;
			str = new XVar("");
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(rights)))))
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(rights))))
				{
					rights = XVar.Clone(permMask2Array((XVar)(rights)));
				}
				else
				{
					return "";
				}
			}
			foreach (KeyValuePair<XVar, dynamic> v in rights.GetEnumerator())
			{
				if((XVar)(v.Value)  && (XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(v.Key == "A")  || (XVar)(v.Key == "D"))  || (XVar)(v.Key == "E"))  || (XVar)(v.Key == "S"))  || (XVar)(v.Key == "P"))  || (XVar)(v.Key == "I"))  || (XVar)(v.Key == "M")))
				{
					str = MVCFunctions.Concat(str, v.Key);
				}
			}
			return str;
		}
		public static XVar getOwnerId(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return null;
			}
			return XSession.Session[MVCFunctions.Concat("_", table, "_OwnerID")];
		}
		public static XVar setOwnerId(dynamic _param_table, dynamic _param_ownerid)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic ownerid = XVar.Clone(_param_ownerid);
			#endregion

			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return null;
			}
			XSession.Session[MVCFunctions.Concat("_", table, "_OwnerID")] = ownerid;
			return null;
		}
	}
}
