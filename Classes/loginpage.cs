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
	public partial class LoginPage : RunnerPage
	{
		public dynamic auditObj = XVar.Pack(null);
		public dynamic notRedirect = XVar.Pack(false);
		public dynamic rememberPassword = XVar.Pack(0);
		public dynamic var_pUsername = XVar.Pack("");
		public dynamic var_pPassword = XVar.Pack("");
		public dynamic action = XVar.Pack("");
		public dynamic redirectAfterLogin = XVar.Pack(false);
		protected dynamic myurl = XVar.Pack("");
		public dynamic fromFacebook = XVar.Pack(false);
		public dynamic fbConnected;
		protected dynamic loggedByCredentials = XVar.Pack(false);
		public dynamic messageType = XVar.Pack(Constants.MESSAGE_ERROR);
		public dynamic twoFactAuth = XVar.Pack(false);
		public dynamic SMSCode;
		protected dynamic SMSCodeSent;
		protected dynamic phoneNumber;
		protected dynamic pnoneNotInQuery;
		protected dynamic skipSecondStep;
		protected static bool skipLoginPageCtor = false;
		public LoginPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipLoginPageCtor)
			{
				skipLoginPageCtor = false;
				return;
			}
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings(new XVar("dbo._ABCSecurity"), (XVar)(this.pageType)));
			this.pSetEdit = XVar.UnPackProjectSettings(this.pSet);
			this.pSetSearch = XVar.Clone(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_SEARCH)));
			this.auditObj = XVar.Clone(CommonFunctions.GetAuditObject());
			this.formBricks.InitAndSetArrayItem("loginheader", "header");
			this.formBricks.InitAndSetArrayItem(new XVar(0, "loginbuttons", 1, "facebookbutton"), "footer");
			assignFormFooterAndHeaderBricks(new XVar(true));
			initMyURL();
			initCredentials();
			this.body = XVar.Clone(new XVar("begin", "", "end", ""));
		}
		protected override XVar setTableConnection()
		{
			this.connection = XVar.Clone(GlobalVars.cman.getForLogin());
			return null;
		}
		protected override XVar assignCipherer()
		{
			this.cipherer = XVar.Clone(RunnerCipherer.getForLogin());
			return null;
		}
		public override XVar setSessionVariables()
		{
			base.setSessionVariables();
			XSession.Session["fromFacebook"] = false;
			return null;
		}
		public virtual XVar process()
		{
			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("BeforeProcessLogin"))))
			{
				GlobalVars.globalEvents.BeforeProcessLogin(this);
			}
			if((XVar)(this.action == "resendCode")  && (XVar)(isFirstAuthStepSuccessful()))
			{
				this.SMSCodeSent = XVar.Clone(sendCodeBySMS());
				reportFirstAuthStepResult(new XVar(true));
				return null;
			}
			if((XVar)(this.action == "verifyCode")  && (XVar)(isFirstAuthStepSuccessful()))
			{
				dynamic verified = null;
				verified = XVar.Clone(doVerifySMSCode((XVar)(this.SMSCode)));
				reportSecondAuthStepResult((XVar)(verified));
				return null;
			}
			if(this.action == "logout")
			{
				Logout(new XVar(true));
				return null;
			}
			refineMessage();
			if(XVar.Pack(isActionSubmit()))
			{
				if(XVar.Pack(isLoginAccessAllowed()))
				{
					setCredentialsCookie((XVar)(this.var_pUsername), (XVar)(this.var_pPassword));
					this.loggedByCredentials = XVar.Clone(doLoginRoutine());
				}
				if((XVar)(this.twoFactAuth)  && (XVar)(this.mode != Constants.LOGIN_EMBEDED))
				{
					reportFirstAuthStepResult((XVar)(this.loggedByCredentials));
					return null;
				}
				if((XVar)(this.loggedByCredentials)  && (XVar)(this.mode == Constants.LOGIN_SIMPLE))
				{
					redirectAfterSuccessfulLogin();
					return null;
				}
				if((XVar)(this.mode == Constants.LOGIN_POPUP)  || (XVar)((XVar)(this.mode == Constants.LOGIN_EMBEDED)  && (XVar)(!(XVar)(this.twoFactAuth))))
				{
					reportLogStatus((XVar)(this.loggedByCredentials));
					return null;
				}
			}
			XSession.Session["MyURL"] = this.myurl;
			if((XVar)(this.mode != Constants.LOGIN_EMBEDED)  && (XVar)(captchaExists()))
			{
				displayCaptcha();
			}
			addCommonJs();
			addButtonHandlers();
			fillSetCntrlMaps();
			doCommonAssignments();
			showPage();
			return null;
		}
		protected virtual XVar isActionSubmit()
		{
			return this.action == "Login";
		}
		protected virtual XVar doLoginRoutine()
		{
			dynamic logged = null;
			if(XVar.Pack(!(XVar)(callBeforeLoginEvent())))
			{
				return false;
			}
			logged = XVar.Clone(LogIn((XVar)(this.var_pUsername), (XVar)(this.var_pPassword)));
			if(XVar.Pack(!(XVar)(logged)))
			{
				callAfterUnsuccessfulLoginEvent();
				return false;
			}
			if(XVar.Pack(this.twoFactAuth))
			{
				this.SMSCodeSent = XVar.Clone(sendCodeBySMS());
				if((XVar)(this.SMSCodeSent)  && (XVar)(this.skipSecondStep))
				{
					doVerifySMSCode(new XVar(""));
				}
			}
			else
			{
				callAfterSuccessfulLoginEvent();
			}
			return true;
		}
		protected virtual XVar isFirstAuthStepSuccessful()
		{
			if(XVar.Pack(!(XVar)(this.twoFactAuth)))
			{
				return false;
			}
			if(XVar.Pack(XSession.Session.KeyExists("firsAuthStepData")))
			{
				return true;
			}
			return false;
		}
		protected virtual XVar reportFirstAuthStepResult(dynamic _param_logged)
		{
			#region pass-by-value parameters
			dynamic logged = XVar.Clone(_param_logged);
			#endregion

			dynamic returnJSON = XVar.Array();
			if(XVar.Pack(this.skipSecondStep))
			{
				reportSecondAuthStepResult(new XVar(true));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(this.message, "message");
			returnJSON.InitAndSetArrayItem((XVar)(logged)  && (XVar)(this.SMSCodeSent), "success");
			returnJSON.InitAndSetArrayItem(logged, "logged");
			if(XVar.Pack(logged))
			{
				dynamic parts = XVar.Array();
				parts = XVar.Clone(getSecondStepMarkupBlocks((XVar)(this.SMSCodeSent)));
				returnJSON.InitAndSetArrayItem(parts["loginfields"], "loginfields");
				returnJSON.InitAndSetArrayItem(parts["loginbuttons"], "loginbuttons");
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar doAssignForSecondAuthStep(dynamic _param_codeSent)
		{
			#region pass-by-value parameters
			dynamic codeSent = XVar.Clone(_param_codeSent);
			#endregion

			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.xt.assign(new XVar("user_code"), new XVar(true));
			if(XVar.Pack(!(XVar)(codeSent)))
			{
				this.xt.assign(new XVar("userCodeFieldClass"), new XVar("rnr-hiddenblock"));
			}
			this.xt.assign(new XVar("user_code_buttons"), new XVar(true));
			if(XVar.Pack(!(XVar)(codeSent)))
			{
				this.xt.assign(new XVar("verifyButtonClass"), new XVar("rnr-invisible-button"));
			}
			if(XVar.Pack(this.pnoneNotInQuery))
			{
				this.xt.assign(new XVar("resendButtonClass"), new XVar("rnr-invisible-button"));
			}
			return null;
		}
		protected virtual XVar getSecondStepMarkupBlocks(dynamic _param_codeSent)
		{
			#region pass-by-value parameters
			dynamic codeSent = XVar.Clone(_param_codeSent);
			#endregion

			dynamic parts = XVar.Array();
			doAssignForSecondAuthStep((XVar)(codeSent));
			parts = XVar.Clone(XVar.Array());
			parts.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("user_code")), "loginfields");
			parts.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("user_code_buttons")), "loginbuttons");
			return parts;
		}
		protected virtual XVar getMaskedPhone()
		{
			dynamic astrixStringLength = null, number = null, smsMaskLength = null;
			number = XVar.Clone(getPhoneNumber());
			smsMaskLength = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("smsMaskLength"), new XVar(4)));
			astrixStringLength = XVar.Clone(MVCFunctions.strlen((XVar)(number)) - smsMaskLength);
			number = XVar.Clone(MVCFunctions.Concat(MVCFunctions.preg_replace(new XVar("/[^+]/"), new XVar("*"), (XVar)(MVCFunctions.substr((XVar)(number), new XVar(0), (XVar)(astrixStringLength)))), MVCFunctions.substr((XVar)(number), (XVar)(astrixStringLength))));
			return number;
		}
		protected virtual XVar checkPhoneFieldInQuery()
		{
			dynamic phoneField = null;
			phoneField = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("strPhoneField"), new XVar("")));
			if(XVar.Pack(!(XVar)(XSession.Session["firsAuthStepData"].KeyExists(phoneField))))
			{
				this.pnoneNotInQuery = new XVar(true);
				this.message = XVar.Clone(MVCFunctions.Concat("Two factor authorization is not possible.", " Add <b>", phoneField, "</b> field to the <b>", GlobalVars.cLoginTable, "</b> table SQL query."));
				return false;
			}
			return true;
		}
		protected virtual XVar sendCodeBySMS()
		{
			dynamic SMSCode = null, number = null, ret = XVar.Array(), smsText = null;
			if(XVar.Pack(!(XVar)(checkPhoneFieldInQuery())))
			{
				return false;
			}
			number = XVar.Clone(getPhoneNumber());
			if(MVCFunctions.strlen((XVar)(number)) < 5)
			{
				this.skipSecondStep = new XVar(true);
				if(this.mode == Constants.LOGIN_EMBEDED)
				{
					this.pageData.InitAndSetArrayItem(getLoggedInRedirectUrl(), "twoStepEmbedRedirect");
				}
				return true;
			}
			SMSCode = XVar.Clone(CommonFunctions.generateUserCode((XVar)(CommonFunctions.GetGlobalData(new XVar("smsCodeLength"), new XVar(6)))));
			XSession.Session["smsCode"] = SMSCode;
			smsText = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat("email/", CommonFunctions.mlang_getcurrentlang(), "/twofactorauth.txt")))), new XVar("r")));
			smsText = XVar.Clone(MVCFunctions.str_replace(new XVar("%code%"), (XVar)(SMSCode), (XVar)(smsText)));
			ret = XVar.Clone(MVCFunctions.runner_sms((XVar)(number), (XVar)(smsText)));
			if(XVar.Pack(!(XVar)(ret["success"])))
			{
				this.message = XVar.Clone(MVCFunctions.Concat("Error sending message", " ", ret["error"]));
			}
			else
			{
				this.messageType = new XVar(Constants.MESSAGE_INFO);
				this.message = XVar.Clone(MVCFunctions.str_replace(new XVar("%phone%"), (XVar)(getMaskedPhone()), new XVar("A text message with your code has been sent to: %phone%")));
			}
			return ret["success"];
		}
		protected virtual XVar doVerifySMSCode(dynamic _param_code)
		{
			#region pass-by-value parameters
			dynamic code = XVar.Clone(_param_code);
			#endregion

			dynamic data = XVar.Array(), verified = null;
			if(XVar.Pack(!(XVar)(this.twoFactAuth)))
			{
				return null;
			}
			if(XVar.Pack(this.skipSecondStep))
			{
				verified = new XVar(true);
			}
			else
			{
				verified = XVar.Clone(verifySMSCode((XVar)(code)));
			}
			data = XVar.Clone(XSession.Session["firsAuthStepData"]);
			this.var_pUsername = XVar.Clone(data[GlobalVars.cUserNameField]);
			this.var_pPassword = XVar.Clone(data[GlobalVars.cPasswordField]);
			if(XVar.Pack(verified))
			{
				dynamic userName = null;
				XSession.Session.Remove("smsCode");
				XSession.Session.Remove("firsAuthStepData");
				XSession.Session["UserID"] = this.var_pUsername;
				userName = XVar.Clone((XVar.Pack(data[GlobalVars.cDisplayNameField] != "") ? XVar.Pack(data[GlobalVars.cDisplayNameField]) : XVar.Pack(this.var_pUsername)));
				XSession.Session["UserName"] = MVCFunctions.runner_htmlspecialchars((XVar)(userName));
				XSession.Session["AccessLevel"] = Constants.ACCESS_LEVEL_USER;
				CommonFunctions.SetAuthSessionData((XVar)(this.var_pUsername), (XVar)(data), new XVar(false), (XVar)(this.var_pPassword), this, new XVar(true));
			}
			else
			{
				callAfterUnsuccessfulLoginEvent();
			}
			return verified;
		}
		protected virtual XVar verifySMSCode(dynamic _param_code)
		{
			#region pass-by-value parameters
			dynamic code = XVar.Clone(_param_code);
			#endregion

			return code == XSession.Session["smsCode"];
		}
		protected virtual XVar reportSecondAuthStepResult(dynamic _param_verified)
		{
			#region pass-by-value parameters
			dynamic verified = XVar.Clone(_param_verified);
			#endregion

			dynamic returnJSON = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(verified, "success");
			returnJSON.InitAndSetArrayItem(this.skipSecondStep, "secondStepSkipped");
			if(XVar.Pack(!(XVar)(verified)))
			{
				returnJSON.InitAndSetArrayItem("Wrong code", "message");
			}
			else
			{
				if(this.mode == Constants.LOGIN_POPUP)
				{
					returnJSON.InitAndSetArrayItem("You have successfully logged in.", "message");
				}
				returnJSON.InitAndSetArrayItem(getLoggedInRedirectUrl(), "redirect");
				if(XVar.Pack(this.notRedirect))
				{
					this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<input type=hidden id='notRedirect' value=1>");
					this.xt.assign(new XVar("continuebutton_attrs"), new XVar("href=\"#\" id=\"continueButton\""));
					this.xt.assign(new XVar("continue_button"), new XVar(true));
					returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("continue_button")), "loginbuttons");
				}
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar getLoggedInRedirectUrl()
		{
			if(XVar.Pack(this.myurl))
			{
				return MVCFunctions.Concat(this.myurl, (XVar.Pack(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(this.myurl), new XVar("?"))), XVar.Pack(false))) ? XVar.Pack("&a=login") : XVar.Pack("?a=login")));
			}
			return MVCFunctions.GetTableLink(new XVar("menu"));
		}
		protected virtual XVar getPhoneNumber()
		{
			dynamic data = XVar.Array(), number = XVar.Array();
			if(XVar.Pack(this.phoneNumber))
			{
				return this.phoneNumber;
			}
			if(XVar.Pack(XSession.Session.KeyExists("firsAuthStepData")))
			{
				data = XVar.Clone(XSession.Session["firsAuthStepData"]);
			}
			else
			{
				data = XVar.Clone(getUserData((XVar)(this.var_pUsername), (XVar)(this.var_pPassword)));
			}
			if(XVar.Pack(!(XVar)(data)))
			{
				return "";
			}
			number = XVar.Clone(data[CommonFunctions.GetGlobalData(new XVar("strPhoneField"), new XVar(""))]);
			number = XVar.Clone(MVCFunctions.preg_replace(new XVar("/[^\\+\\d]/"), new XVar(""), (XVar)(number)));
			if((XVar)(number[0] == "+")  && (XVar)(10 < MVCFunctions.strlen((XVar)(number))))
			{
				this.phoneNumber = XVar.Clone(number);
			}
			else
			{
				this.phoneNumber = XVar.Clone(MVCFunctions.Concat(CommonFunctions.GetGlobalData(new XVar("strCounryCode"), new XVar("")), number));
			}
			return this.phoneNumber;
		}
		protected virtual XVar refineMessage()
		{
			if(this.message == "expired")
			{
				this.message = XVar.Clone(MVCFunctions.Concat("Your session has expired.", "Please login again."));
			}
			else
			{
				if(this.message == "invalidlogin")
				{
					this.message = new XVar("Invalid Login");
				}
				else
				{
					if((XVar)(this.message == "loginblocked")  && (XVar)(MVCFunctions.strlen((XVar)(XSession.Session["loginBlockMessage"]))))
					{
						this.message = XVar.Clone(XSession.Session["loginBlockMessage"]);
					}
				}
			}
			if((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(this.message))
			{
				this.xt.assign(new XVar("message_class"), new XVar("alert-danger"));
			}
			XSession.Session.Remove("loginBlockMessage");
			return null;
		}
		protected virtual XVar initMyURL()
		{
			this.myurl = XVar.Clone(XSession.Session["MyURL"]);
			if((XVar)((XVar)((XVar)(this.redirectAfterLogin)  || (XVar)(this.mode == Constants.LOGIN_POPUP))  || (XVar)(this.action == "Login"))  || (XVar)(this.twoFactAuth))
			{
			}
			else
			{
				this.myurl = new XVar("");
			}
			return null;
		}
		protected virtual XVar callAfterSuccessfulLoginEvent()
		{
			return null;
		}
		protected virtual XVar redirectAfterSuccessfulLogin()
		{
			XSession.Session.Remove("MyURL");
			if(XVar.Pack(this.myurl))
			{
				MVCFunctions.HeaderRedirect((XVar)(MVCFunctions.Concat("", this.myurl)));
			}
			else
			{
				MVCFunctions.HeaderRedirect(new XVar("menu"));
			}
			return null;
		}
		public virtual XVar callAfterUnsuccessfulLoginEvent()
		{
			dynamic message = null;
			message = new XVar("");
			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("AfterUnsuccessfulLogin"))))
			{
				GlobalVars.globalEvents.AfterUnsuccessfulLogin((XVar)(this.var_pUsername), (XVar)(this.var_pPassword), ref message, this);
			}
			if((XVar)(message == XVar.Pack(""))  && (XVar)(!(XVar)(this.message)))
			{
				this.message = new XVar("Invalid Login");
			}
			else
			{
				if(XVar.Pack(message))
				{
					this.message = XVar.Clone(message);
				}
			}
			return null;
		}
		protected virtual XVar reportLogStatus(dynamic _param_logged)
		{
			#region pass-by-value parameters
			dynamic logged = XVar.Clone(_param_logged);
			#endregion

			dynamic returnJSON = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(logged, "success");
			if(XVar.Pack(this.message))
			{
				returnJSON.InitAndSetArrayItem(this.message, "message");
			}
			else
			{
				if(XVar.Pack(logged))
				{
					returnJSON.InitAndSetArrayItem(getLoggedInRedirectUrl(), "redirect");
				}
			}
			if(this.mode == Constants.LOGIN_EMBEDED)
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(XSession.Session["loginBlockMessage"]))))
				{
					returnJSON.InitAndSetArrayItem("loginblocked", "messageParam");
				}
				else
				{
					returnJSON.InitAndSetArrayItem("invalidlogin", "messageParam");
				}
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar isLoginAccessAllowed()
		{
			if(XVar.Pack(!(XVar)(this.auditObj)))
			{
				return true;
			}
			if(XVar.Pack(!(XVar)(this.auditObj.LoginAccess())))
			{
				return true;
			}
			this.message = XVar.Clone(MVCFunctions.mysprintf(new XVar("Access denied for %s minutes"), (XVar)(new XVar(0, this.auditObj.LoginAccess()))));
			XSession.Session["loginBlockMessage"] = this.message;
			return false;
		}
		protected virtual XVar callBeforeLoginEvent()
		{
			dynamic message = null, ret = null;
			if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.exists(new XVar("BeforeLogin")))))
			{
				return true;
			}
			message = new XVar("");
			ret = XVar.Clone(GlobalVars.globalEvents.BeforeLogin(ref this.var_pUsername, ref this.var_pPassword, ref message, this));
			if(XVar.Pack(message))
			{
				this.message = XVar.Clone(message);
			}
			if(XVar.Pack(!(XVar)(ret)))
			{
				callAfterUnsuccessfulLoginEvent();
			}
			return ret;
		}
		public virtual XVar doAfterUnsuccessfulLog(dynamic _param_username)
		{
			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			#endregion

			if(XVar.Pack(this.auditObj))
			{
				this.auditObj.LogLoginFailed((XVar)(username));
				this.auditObj.LoginUnsuccessful((XVar)(username));
			}
			return null;
		}
		public virtual XVar checkUsernamePassword(dynamic _param_username, dynamic _param_password)
		{
			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			dynamic password = XVar.Clone(_param_password);
			#endregion

			if(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_NONE)
			{
				return false;
			}
			if(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_HARDCODED)
			{
				return (XVar)(password == GlobalVars.globalSettings["Password"])  && (XVar)((XVar)(username == GlobalVars.globalSettings["UserName"])  || (XVar)((XVar)(GlobalVars.caseInsensitiveUsername)  && (XVar)(MVCFunctions.strtoupper((XVar)(username)) == MVCFunctions.strtoupper((XVar)(GlobalVars.globalSettings["UserName"])))));
			}
			if(GlobalVars.globalSettings["nLoginMethod"] == Constants.SECURITY_TABLE)
			{
				dynamic data = null;
				data = XVar.Clone(getUserData((XVar)(username), (XVar)(password)));
				return (XVar.Pack(data) ? XVar.Pack(true) : XVar.Pack(false));
			}
			return false;
		}
		public virtual XVar getUserData(dynamic _param_username, dynamic _param_password, dynamic _param_skipPasswordCheck = null)
		{
			#region default values
			if(_param_skipPasswordCheck as Object == null) _param_skipPasswordCheck = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic username = XVar.Clone(_param_username);
			dynamic password = XVar.Clone(_param_password);
			dynamic skipPasswordCheck = XVar.Clone(_param_skipPasswordCheck);
			#endregion

			dynamic bcrypted = null, data = null, loginSet = null, originalPassword = null;
			if(GlobalVars.globalSettings["nLoginMethod"] != Constants.SECURITY_TABLE)
			{
				return false;
			}
			loginSet = XVar.Clone(ProjectSettings.getForLogin());
			GlobalVars.cipherer = XVar.Clone(RunnerCipherer.getForLogin((XVar)(loginSet)));
			bcrypted = XVar.Clone((XVar)(GlobalVars.globalSettings["bEncryptPasswords"])  && (XVar)(GlobalVars.globalSettings["nEncryptPasswordMethod"] == 0));
			originalPassword = XVar.Clone(password);
			GlobalVars.strSQL = XVar.Clone(getSelectSQL((XVar)((XVar)(skipPasswordCheck)  || (XVar)(bcrypted)), (XVar)(username), (XVar)(password), (XVar)(loginSet), (XVar)(GlobalVars.cipherer)));
			data = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
			if((XVar)((XVar)(data)  && (XVar)(skipPasswordCheck))  || (XVar)(verifyUserFetchedData((XVar)(bcrypted), (XVar)(data), (XVar)(username), (XVar)(password), (XVar)(originalPassword))))
			{
				return data;
			}
			return false;
		}
		public virtual XVar LogIn(dynamic _param_pUsername, dynamic _param_pPassword, dynamic _param_skipPasswordCheck = null, dynamic _param_fireEvents = null)
		{
			#region default values
			if(_param_skipPasswordCheck as Object == null) _param_skipPasswordCheck = new XVar(false);
			if(_param_fireEvents as Object == null) _param_fireEvents = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic pUsername = XVar.Clone(_param_pUsername);
			dynamic pPassword = XVar.Clone(_param_pPassword);
			dynamic skipPasswordCheck = XVar.Clone(_param_skipPasswordCheck);
			dynamic fireEvents = XVar.Clone(_param_fireEvents);
			#endregion

			dynamic data = XVar.Array(), strPassword = null, strUsername = null;
			if(XVar.Pack(!(XVar)(checkCaptcha())))
			{
				return false;
			}
			strUsername = XVar.Clone(pUsername);
			strPassword = XVar.Clone(pPassword);
			data = XVar.Clone(getUserData((XVar)(pUsername), (XVar)(pPassword), (XVar)(skipPasswordCheck)));
			if(XVar.Pack(data))
			{
				dynamic pDisplayUsername = null;
				pDisplayUsername = XVar.Clone((XVar.Pack(data[GlobalVars.cDisplayNameField] != "") ? XVar.Pack(data[GlobalVars.cDisplayNameField]) : XVar.Pack(strUsername)));
				CommonFunctions.DoLogin(new XVar(false), (XVar)(pUsername), (XVar)(pDisplayUsername), new XVar(""), new XVar(Constants.ACCESS_LEVEL_USER), (XVar)(pPassword), this);
				if(XVar.Pack(!(XVar)(this.twoFactAuth)))
				{
					CommonFunctions.SetAuthSessionData((XVar)(pUsername), (XVar)(data), new XVar(false), (XVar)(pPassword), this, (XVar)(fireEvents));
				}
				else
				{
					XSession.Session["firsAuthStepData"] = data;
				}
				return true;
			}
			if(XVar.Pack(fireEvents))
			{
				doAfterUnsuccessfulLog((XVar)(pUsername));
			}
			return false;
			return null;
		}
		protected virtual XVar verifyUserFetchedData(dynamic _param_bcrypted, dynamic _param_data, dynamic _param_strUsername, dynamic _param_processedPass, dynamic _param_rawPass)
		{
			#region pass-by-value parameters
			dynamic bcrypted = XVar.Clone(_param_bcrypted);
			dynamic data = XVar.Clone(_param_data);
			dynamic strUsername = XVar.Clone(_param_strUsername);
			dynamic processedPass = XVar.Clone(_param_processedPass);
			dynamic rawPass = XVar.Clone(_param_rawPass);
			#endregion

			if(XVar.Pack(!(XVar)(data)))
			{
				return false;
			}
			if(XVar.Pack(bcrypted))
			{
				return MVCFunctions.passwordVerify((XVar)(rawPass), (XVar)(data[GlobalVars.cPasswordField]));
			}
			return (XVar)(this.pSet.getCaseSensitiveUsername((XVar)(data[GlobalVars.cUserNameField])) == this.pSet.getCaseSensitiveUsername((XVar)(strUsername)))  && (XVar)(data[GlobalVars.cPasswordField] == processedPass);
		}
		protected virtual XVar getSelectSQL(dynamic _param_skipPasswordCheck, dynamic _param_strUsername, dynamic _param_strPassword, dynamic _param_loginSet, dynamic _param_cipherer)
		{
			#region pass-by-value parameters
			dynamic skipPasswordCheck = XVar.Clone(_param_skipPasswordCheck);
			dynamic strUsername = XVar.Clone(_param_strUsername);
			dynamic strPassword = XVar.Clone(_param_strPassword);
			dynamic loginSet = XVar.Clone(_param_loginSet);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			dynamic activateWhere = null, passWhere = null;
			passWhere = new XVar("");
			activateWhere = new XVar("");
			if(XVar.Pack(!(XVar)(skipPasswordCheck)))
			{
				strPassword = XVar.Clone(getSqlPreparedLoginTableValue((XVar)(strPassword), (XVar)(GlobalVars.cPasswordField), (XVar)(GlobalVars.cPasswordFieldType), (XVar)(cipherer)));
				if(XVar.Pack(loginSet))
				{
					passWhere = XVar.Clone(MVCFunctions.Concat(" and ", getFieldSQLDecrypt((XVar)(GlobalVars.cPasswordField)), "=", strPassword));
				}
				else
				{
					passWhere = XVar.Clone(MVCFunctions.Concat(" and ", this.connection.addFieldWrappers((XVar)(GlobalVars.cPasswordField)), "=", strPassword));
				}
			}
			strUsername = XVar.Clone(getSqlPreparedLoginTableValue((XVar)(strUsername), (XVar)(GlobalVars.cUserNameField), (XVar)(GlobalVars.cUserNameFieldType), (XVar)(cipherer)));
			if(XVar.Pack(loginSet))
			{
				dynamic tempSQLQuery = null, where = null;
				if(XVar.Pack(!(XVar)(this.pSet.isCaseInsensitiveUsername())))
				{
					where = XVar.Clone(MVCFunctions.Concat(getFieldSQLDecrypt((XVar)(GlobalVars.cUserNameField)), "=", strUsername, passWhere));
				}
				else
				{
					where = XVar.Clone(MVCFunctions.Concat(this.connection.upper((XVar)(getFieldSQLDecrypt((XVar)(GlobalVars.cUserNameField)))), "=", this.pSet.getCaseSensitiveUsername((XVar)(strUsername)), passWhere));
				}
				where = MVCFunctions.Concat(where, activateWhere);
				tempSQLQuery = XVar.Clone(loginSet.getTableData(new XVar(".sqlquery")));
				return tempSQLQuery.buildSQL_default((XVar)(new XVar(0, where)));
			}
			return MVCFunctions.Concat("select * from ", this.connection.addTableWrappers(new XVar("dbo._ABCSecurity")), " where ", this.connection.addFieldWrappers((XVar)(GlobalVars.cUserNameField)), "=", strUsername, passWhere, activateWhere);
		}
		public virtual XVar Logout(dynamic _param_redirectToLogin = null)
		{
			#region default values
			if(_param_redirectToLogin as Object == null) _param_redirectToLogin = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic redirectToLogin = XVar.Clone(_param_redirectToLogin);
			#endregion

			dynamic cookieName = null, username = null;
			if(XVar.Pack(this.auditObj))
			{
				this.auditObj.LogLogout();
			}
			username = XVar.Clone((XVar.Pack(XSession.Session["UserID"] != "Guest") ? XVar.Pack(XSession.Session["UserID"]) : XVar.Pack("")));
			XSession.Session.Remove("MyURL");
			Security.clearSecuritySession();
			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("AfterLogout"))))
			{
				GlobalVars.globalEvents.AfterLogout((XVar)(username));
			}
			if(XVar.Pack(redirectToLogin))
			{
				MVCFunctions.HeaderRedirect((XVar)(MVCFunctions.Concat("", MVCFunctions.GetTableLink(new XVar("login")))));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			return null;
		}
		public virtual XVar LogoutAndRedirect(dynamic _param_url = null)
		{
			#region default values
			if(_param_url as Object == null) _param_url = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic url = XVar.Clone(_param_url);
			#endregion

			Logout();
			if(url == XVar.Pack(""))
			{
				url = XVar.Clone(MVCFunctions.GetTableLink(new XVar("menu")));
			}
			MVCFunctions.HeaderRedirect((XVar)(MVCFunctions.Concat("", url)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public virtual XVar captchaExists()
		{
			dynamic captchaSettings = XVar.Array();
			captchaSettings = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("CaptchaSettings"), new XVar(false)));
			return captchaSettings["isEnabledOnLogin"];
		}
		public override XVar getCaptchaId()
		{
			return "login";
		}
		public virtual XVar setDatabaseError(dynamic _param_messageText)
		{
			#region pass-by-value parameters
			dynamic messageText = XVar.Clone(_param_messageText);
			#endregion

			this.message = XVar.Clone(messageText);
			return null;
		}
		public virtual XVar setCredentialsCookie(dynamic _param_pUsername, dynamic _param_pPassword)
		{
			#region pass-by-value parameters
			dynamic pUsername = XVar.Clone(_param_pUsername);
			dynamic pPassword = XVar.Clone(_param_pPassword);
			#endregion

			MVCFunctions.SetCookie(new XVar("username"), (XVar)((XVar.Pack(this.rememberPassword) ? XVar.Pack(pUsername) : XVar.Pack(""))), (XVar)(MVCFunctions.time() + (365 * 1440) * 60));
			MVCFunctions.SetCookie(new XVar("password"), (XVar)((XVar.Pack(this.rememberPassword) ? XVar.Pack(pPassword) : XVar.Pack(""))), (XVar)(MVCFunctions.time() + (365 * 1440) * 60));
			return null;
		}
		public override XVar setLangParams()
		{
			return null;
		}
		protected override XVar assignBody()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage(new XVar(false)));
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<form method=\"post\" action='", MVCFunctions.GetTableLink(new XVar("login")), "' id=\"form", this.id, "\" name=\"form", this.id, "\">\r\n\t\t\t\t\t\t\t\t<input type=\"hidden\" name=\"btnSubmit\" value=\"Login\">");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "</form>");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "<script>");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "window.controlsMap = ", MVCFunctions.my_json_encode((XVar)(this.controlsHTMLMap)), ";");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "window.viewControlsMap = ", MVCFunctions.my_json_encode((XVar)(this.viewControlsHTMLMap)), ";");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "Runner.applyPagesData( ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pagesData)), " );");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "window.settings = ", MVCFunctions.my_json_encode((XVar)(this.jsSettings)), ";</script>");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "<script type=\"text/javascript\" src=\"", MVCFunctions.GetRootPathForResources(new XVar("include/runnerJS/RunnerAll.js")), "\"></script>");
			this.body["end"] = MVCFunctions.Concat(this.body["end"], "<script>", PrepareJS(), "</script>");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public virtual XVar doCommonAssignments()
		{
			dynamic rememberPassword = null, rememberbox_checked = null;
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.xt.assign(new XVar("loginlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"submitLogin", this.id, "\"")));
			if((XVar)((XVar)((XVar)(this.loggedByCredentials)  && (XVar)(this.mode == Constants.LOGIN_EMBEDED))  && (XVar)(this.twoFactAuth))  && (XVar)(this.skipSecondStep))
			{
				return null;
			}
			setLangParams();
			rememberbox_checked = new XVar("");
			if((XVar)((XVar)(rememberPassword)  || (XVar)(MVCFunctions.GetCookie("username")))  || (XVar)(MVCFunctions.GetCookie("password")))
			{
				rememberbox_checked = new XVar(" checked");
			}
			this.xt.assign(new XVar("rememberbox_attrs"), (XVar)(MVCFunctions.Concat((XVar.Pack(this.is508) ? XVar.Pack("id=\"remember_password\" ") : XVar.Pack("")), "name=\"remember_password\" value=\"1\"", rememberbox_checked)));
			this.xt.assign(new XVar("guestlink_block"), (XVar)((XVar)((XVar)(this.mode == Constants.LOGIN_SIMPLE)  && (XVar)(CommonFunctions.guestHasPermissions()))  && (XVar)(CommonFunctions.isGuestLoginAvailable())));
			this.xt.assign(new XVar("username_label"), new XVar(true));
			this.xt.assign(new XVar("password_label"), new XVar(true));
			this.xt.assign(new XVar("remember_password_label"), new XVar(true));
			if((XVar)(this.is508)  && (XVar)(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT))
			{
				this.xt.assign_section(new XVar("username_label"), new XVar("<label for=\"username\">"), new XVar("</label>"));
				this.xt.assign_section(new XVar("password_label"), new XVar("<label for=\"password\">"), new XVar("</label>"));
				this.xt.assign_section(new XVar("remember_password_label"), new XVar("<label for=\"remember_password\">"), new XVar("</label>"));
			}
			if((XVar)((XVar)(this.message)  || (XVar)(this.mode == Constants.LOGIN_POPUP))  || (XVar)(this.twoFactAuth))
			{
				this.xt.assign(new XVar("message_block"), new XVar(true));
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					this.xt.assign(new XVar("message"), (XVar)(this.message));
				}
				else
				{
					this.xt.assign(new XVar("message"), (XVar)(MVCFunctions.Concat("<div id='login_message' class='message rnr-error'>", this.message, "</div>")));
				}
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					this.xt.assign(new XVar("message_class"), (XVar)((XVar.Pack(this.messageType == Constants.MESSAGE_INFO) ? XVar.Pack("alert-success") : XVar.Pack("alert-danger"))));
				}
				if(XVar.Pack(!(XVar)(this.message)))
				{
					this.xt.displayBrickHidden(new XVar("message"));
				}
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(this.var_pUsername))))
			{
				this.xt.assign(new XVar("username_attrs"), (XVar)(MVCFunctions.Concat((XVar.Pack(this.is508) ? XVar.Pack("id=\"username\" ") : XVar.Pack("")), "value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(this.var_pUsername)), "\"")));
			}
			else
			{
				if(XVar.Pack(!(XVar)(this.twoFactAuth)))
				{
					this.xt.assign(new XVar("username_attrs"), (XVar)(MVCFunctions.Concat((XVar.Pack(this.is508) ? XVar.Pack("id=\"username\" ") : XVar.Pack("")), "value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.GetCookie("username"))), "\"")));
				}
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(this.var_pPassword))))
			{
				this.xt.assign(new XVar("password_attrs"), (XVar)(MVCFunctions.Concat((XVar.Pack(this.is508) ? XVar.Pack(" id=\"password\"") : XVar.Pack("")), " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(this.var_pPassword)), "\"")));
			}
			else
			{
				if(XVar.Pack(!(XVar)(this.twoFactAuth)))
				{
					this.xt.assign(new XVar("password_attrs"), (XVar)(MVCFunctions.Concat((XVar.Pack(this.is508) ? XVar.Pack(" id=\"password\"") : XVar.Pack("")), " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.GetCookie("password"))), "\"")));
				}
			}
			if((XVar)(this.myurl)  && (XVar)(XSession.Session["MyUrlAccess"]))
			{
				this.xt.assign(new XVar("guestlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", this.myurl, "\"")));
			}
			else
			{
				this.xt.assign(new XVar("guestlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", MVCFunctions.GetTableLink(new XVar("menu")), "\"")));
			}
			if((XVar)((XVar)(this.loggedByCredentials)  && (XVar)(this.mode == Constants.LOGIN_EMBEDED))  && (XVar)(this.twoFactAuth))
			{
				doAssignForSecondAuthStep((XVar)(!(XVar)(this.pnoneNotInQuery)));
				this.xt.displayBrickHidden(new XVar("bsloginregister"));
				this.xt.displayBrickHidden(new XVar("mesreg"));
				this.xt.displayBrickHidden(new XVar("mesforgot"));
			}
			else
			{
				this.xt.assign(new XVar("main_loginfields"), new XVar(true));
				this.xt.assign(new XVar("signin_button"), new XVar(true));
			}
			if((XVar)(this.mode == Constants.LOGIN_POPUP)  || (XVar)((XVar)(this.mode == Constants.LOGIN_EMBEDED)  && (XVar)(this.twoFactAuth)))
			{
				if(XVar.Pack(this.notRedirect))
				{
					this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<input type=hidden id='notRedirect' value=1>");
					this.xt.assign(new XVar("continuebutton_attrs"), new XVar("href=\"#\" style=\"display:none\" id=\"continueButton\""));
					this.xt.assign(new XVar("continue_button"), new XVar(true));
				}
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("body"), (XVar)(this.body));
				this.xt.assign(new XVar("registerlink_attrs"), (XVar)(MVCFunctions.Concat("name=\"RegisterPage\" data-table=\"", MVCFunctions.runner_htmlspecialchars((XVar)(GlobalVars.cLoginTable)), "\"")));
				this.xt.assign(new XVar("forgotpasswordlink_attrs"), new XVar("name=\"ForgotPasswordPage\""));
			}
			return null;
		}
		public virtual XVar showPage()
		{
			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("BeforeShowLogin"))))
			{
				GlobalVars.globalEvents.BeforeShowLogin((XVar)(this.xt), ref this.templatefile, this);
			}
			if((XVar)(this.mode == Constants.LOGIN_POPUP)  || (XVar)((XVar)(this.mode == Constants.LOGIN_EMBEDED)  && (XVar)(this.twoFactAuth)))
			{
				displayAJAX((XVar)(this.templatefile), (XVar)(this.id + 1));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			if(this.mode == Constants.LOGIN_SIMPLE)
			{
				assignBody();
			}
			display((XVar)(this.templatefile));
			return null;
		}
		protected virtual XVar initCredentials()
		{
			return null;
		}
		public static XVar readLoginModeFromRequest()
		{
			dynamic pageMode = null;
			pageMode = XVar.Clone(MVCFunctions.postvalue(new XVar("mode")));
			if(pageMode == "popup")
			{
				return Constants.LOGIN_POPUP;
			}
			if(pageMode == "embeded")
			{
				return Constants.LOGIN_EMBEDED;
			}
			return Constants.LOGIN_SIMPLE;
		}
		public static XVar readActionFromRequest()
		{
			dynamic action = null;
			action = XVar.Clone(MVCFunctions.postvalue(new XVar("a")));
			if(XVar.Pack(action))
			{
				return action;
			}
			return MVCFunctions.postvalue("btnSubmit");
		}
	}
}
