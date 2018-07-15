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
	public partial class AddPage : RunnerPage
	{
		public dynamic messageType = XVar.Pack(Constants.MESSAGE_ERROR);
		protected dynamic auditObj = XVar.Pack(null);
		protected dynamic addFields = XVar.Array();
		protected dynamic readAddValues = XVar.Pack(false);
		protected dynamic insertedSuccessfully = XVar.Pack(false);
		protected dynamic defvalues = XVar.Array();
		protected dynamic newRecordData = XVar.Array();
		protected dynamic newRecordBlobFields = XVar.Array();
		public dynamic afterAdd_id = XVar.Pack("");
		public dynamic action = XVar.Pack("");
		public dynamic screenWidth = XVar.Pack(0);
		public dynamic screenHeight = XVar.Pack(0);
		public dynamic orientation = XVar.Pack("");
		public dynamic forListPageLookup = XVar.Pack(false);
		public dynamic lookupTable = XVar.Pack("");
		public dynamic lookupField = XVar.Pack("");
		public dynamic lookupPageType = XVar.Pack("");
		public dynamic parentCtrlsData;
		protected dynamic afterAddAction = XVar.Pack(null);
		public dynamic fromDashboard = XVar.Pack("");
		protected static bool skipAddPageCtor = false;
		public AddPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipAddPageCtor)
			{
				skipAddPageCtor = false;
				return;
			}
			this.addFields = XVar.Clone(getPageFields());
			this.auditObj = XVar.Clone(CommonFunctions.GetAuditObject((XVar)(this.tName)));
			this.formBricks.InitAndSetArrayItem("addheader", "header");
			this.formBricks.InitAndSetArrayItem("addbuttons", "footer");
			assignFormFooterAndHeaderBricks(new XVar(true));
			addPageSettings();
		}
		protected virtual XVar addPageSettings()
		{
			dynamic afterAddAction = null;
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_recordAdded")]))
			{
				setProxyValue((XVar)(MVCFunctions.Concat(this.shortTableName, "_recordAdded")), new XVar(true));
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_recordAdded"));
			}
			else
			{
				setProxyValue((XVar)(MVCFunctions.Concat(this.shortTableName, "_recordAdded")), new XVar(false));
			}
			if((XVar)(this.mode != Constants.ADD_SIMPLE)  && (XVar)(this.mode != Constants.ADD_POPUP))
			{
				return null;
			}
			afterAddAction = XVar.Clone(getAfterAddAction());
			this.jsSettings.InitAndSetArrayItem(afterAddAction, "tableSettings", this.tName, "afterAddAction");
			if((XVar)(afterAddAction == Constants.AA_TO_DETAIL_LIST)  || (XVar)(afterAddAction == Constants.AA_TO_DETAIL_ADD))
			{
				this.jsSettings.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(this.pSet.getAADetailTable())), "tableSettings", this.tName, "afterAddActionDetTable");
			}
			return null;
		}
		protected virtual XVar getAfterAddAction()
		{
			dynamic action = null;
			if((XVar)(true)  && (XVar)(!(XVar)(this.afterAddAction == null)))
			{
				return this.afterAddAction;
			}
			action = XVar.Clone(this.pSet.getAfterAddAction());
			if((XVar)((XVar)((XVar)(this.mode == Constants.ADD_POPUP)  && (XVar)(this.pSet.checkClosePopupAfterAdd()))  || (XVar)((XVar)(action == Constants.AA_TO_VIEW)  && (XVar)(!(XVar)(viewAvailable()))))  || (XVar)((XVar)(action == Constants.AA_TO_EDIT)  && (XVar)(!(XVar)(editAvailable()))))
			{
				action = new XVar(Constants.AA_TO_LIST);
			}
			if((XVar)(action == Constants.AA_TO_DETAIL_LIST)  || (XVar)(action == Constants.AA_TO_DETAIL_ADD))
			{
				dynamic dPermissions = XVar.Array(), dPset = null, dTName = null, listPageAllowed = null;
				dTName = XVar.Clone(this.pSet.getAADetailTable());
				dPset = XVar.Clone(new ProjectSettings((XVar)(dTName)));
				dPermissions = XVar.Clone(getPermissions((XVar)(dTName)));
				listPageAllowed = XVar.Clone((XVar)(dPset.hasListPage())  && (XVar)(dPermissions["search"]));
				if((XVar)((XVar)(!(XVar)(dTName))  || (XVar)((XVar)(action == Constants.AA_TO_DETAIL_LIST)  && (XVar)(!(XVar)(listPageAllowed))))  || (XVar)((XVar)(action == Constants.AA_TO_DETAIL_ADD)  && (XVar)((XVar)(!(XVar)(dPset.hasAddPage()))  || (XVar)((XVar)(!(XVar)(dPermissions["add"]))  && (XVar)(!(XVar)(listPageAllowed))))))
				{
					action = new XVar(Constants.AA_TO_LIST);
				}
			}
			this.afterAddAction = XVar.Clone(action);
			return this.afterAddAction;
		}
		protected override XVar assignSessionPrefix()
		{
			if((XVar)((XVar)(this.mode == Constants.ADD_DASHBOARD)  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))  || (XVar)((XVar)((XVar)((XVar)(this.mode == Constants.ADD_POPUP)  || (XVar)(this.mode == Constants.ADD_INLINE))  || (XVar)(this.fromDashboard != ""))  && (XVar)(this.dashTName)))
			{
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
				return null;
			}
			base.assignSessionPrefix();
			if(this.mode == Constants.ADD_ONTHEFLY)
			{
				this.sessionPrefix = MVCFunctions.Concat(this.sessionPrefix, "_add");
			}
			return null;
		}
		public override XVar setTemplateFile()
		{
			if(this.mode == Constants.ADD_INLINE)
			{
				this.templatefile = XVar.Clone(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), new XVar("inline_add")));
			}
			base.setTemplateFile();
			return null;
		}
		protected override XVar getPageFields()
		{
			if(this.mode == Constants.ADD_INLINE)
			{
				if((XVar)((XVar)(this.masterTable)  && (XVar)(!(XVar)(inlineAddAvailable())))  && (XVar)(this.masterPageType == Constants.PAGE_ADD))
				{
					return this.pSet.getAddFields();
				}
				return this.pSet.getInlineAddFields();
			}
			return this.pSet.getAddFields();
		}
		public static XVar handleBrokenRequest()
		{
			if((XVar)(MVCFunctions.POSTSize() != 0)  || (XVar)(!(XVar)(MVCFunctions.postvalue(new XVar("submit")))))
			{
				return null;
			}
			if(XVar.Pack(MVCFunctions.postvalue(new XVar("inline"))))
			{
				dynamic returnJSON = XVar.Array();
				returnJSON = XVar.Clone(XVar.Array());
				returnJSON.InitAndSetArrayItem(false, "success");
				returnJSON.InitAndSetArrayItem("Error occurred", "message");
				returnJSON.InitAndSetArrayItem(true, "fatalError");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			if(XVar.Pack(MVCFunctions.postvalue(new XVar("fly"))))
			{
				MVCFunctions.Echo(-1);
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			XSession.Session["message_add"] = MVCFunctions.Concat("<< ", "Error occurred", " >>");
			return null;
		}
		public virtual XVar redirectAfterAdd()
		{
			dynamic data = XVar.Array();
			if((XVar)(XSession.Session["after_add_data"].KeyExists(this.afterAdd_id))  && (XVar)(XSession.Session["after_add_data"][this.afterAdd_id]))
			{
				data = XVar.Clone(XSession.Session["after_add_data"][this.afterAdd_id]);
				this.keys = XVar.Clone(data["keys"]);
				this.newRecordData = XVar.Clone(data["avalues"]);
			}
			if((XVar)((XVar)(this.eventsObject.exists(new XVar("AfterAdd")))  && (XVar)(XSession.Session["after_add_data"].KeyExists(this.afterAdd_id)))  && (XVar)(XSession.Session["after_add_data"][this.afterAdd_id]))
			{
				this.eventsObject.AfterAdd((XVar)(data["avalues"]), (XVar)(data["keys"]), (XVar)(data["inlineadd"]), this);
			}
			XSession.Session["after_add_data"].Remove(this.afterAdd_id);
			foreach (KeyValuePair<XVar, dynamic> v in (XVar.Pack(MVCFunctions.is_array((XVar)(XSession.Session["after_add_data"]))) ? XVar.Pack(XSession.Session["after_add_data"]) : XVar.Pack(XVar.Array())).GetEnumerator())
			{
				if((XVar)(!(XVar)(MVCFunctions.is_array((XVar)(v.Value))))  || (XVar)(!(XVar)(v.Value.KeyExists("time"))))
				{
					XSession.Session["after_add_data"].Remove(v.Key);
					continue;
				}
				if(v.Value["time"] < MVCFunctions.time() - 3600)
				{
					XSession.Session["after_add_data"].Remove(v.Key);
				}
			}
			afterAddActionRedirect();
			return null;
		}
		public virtual XVar process()
		{
			if(XVar.Pack(MVCFunctions.strlen((XVar)(this.afterAdd_id))))
			{
				redirectAfterAdd();
				return null;
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessAdd"))))
			{
				this.eventsObject.BeforeProcessAdd(this);
			}
			if(this.action == "added")
			{
				processDataInput();
				this.readAddValues = XVar.Clone(!(XVar)(this.insertedSuccessfully));
				if((XVar)((XVar)(this.mode != Constants.ADD_SIMPLE)  && (XVar)(this.mode != Constants.ADD_DASHBOARD))  && (XVar)(this.mode != Constants.ADD_MASTER_DASH))
				{
					reportSaveStatus();
					return null;
				}
				if(XVar.Pack(this.insertedSuccessfully))
				{
					if(XVar.Pack(afterAddActionRedirect()))
					{
						return null;
					}
				}
			}
			if(XVar.Pack(captchaExists()))
			{
				displayCaptcha();
			}
			prgReadMessage();
			prepareDefvalues();
			if(XVar.Pack(this.eventsObject.exists(new XVar("ProcessValuesAdd"))))
			{
				this.eventsObject.ProcessValuesAdd((XVar)(this.defvalues), this);
			}
			prepareReadonlyFields();
			prepareEditControls();
			prepareButtons();
			prepareSteps();
			fillCntrlTabGroups();
			prepareDetailsTables();
			if((XVar)(this.mode == Constants.ADD_SIMPLE)  || (XVar)(this.mode == Constants.ADD_ONTHEFLY))
			{
				addButtonHandlers();
			}
			addCommonJs();
			fillSetCntrlMaps();
			doCommonAssignments();
			displayAddPage();
			return null;
		}
		public override XVar addCommonJs()
		{
			base.addCommonJs();
			if(XVar.Pack(this.allDetailsTablesArr))
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			return null;
		}
		protected virtual XVar processDataInput()
		{
			dynamic performingRegularAdd = null;
			if(this.action != "added")
			{
				return null;
			}
			buildNewRecordData();
			if(XVar.Pack(!(XVar)(checkCaptcha())))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(recheckUserPermissions())))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(callBeforeAddEvent())))
			{
				return null;
			}
			setUpdatedLatLng((XVar)(this.newRecordData));
			if(XVar.Pack(!(XVar)(checkDeniedDuplicatedValues())))
			{
				return null;
			}
			performingRegularAdd = XVar.Clone(callCustomAddEvent());
			if(XVar.Pack(performingRegularAdd))
			{
				this.insertedSuccessfully = XVar.Clone(MVCFunctions.DoInsertRecordOnAdd(this));
			}
			if(XVar.Pack(!(XVar)(this.insertedSuccessfully)))
			{
				return false;
			}
			if(getAfterAddAction() == Constants.AA_TO_ADD)
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_recordAdded")] = true;
			}
			ProcessFiles();
			if(XVar.Pack(performingRegularAdd))
			{
				prepareTableKeysAfterInsert();
			}
			mergeNewRecordData();
			if(XVar.Pack(this.auditObj))
			{
				this.auditObj.LogAdd((XVar)(this.tName), (XVar)(this.newRecordData), (XVar)(this.keys));
			}
			callAfterSuccessfulSave();
			callAfterAddEvent();
			this.messageType = new XVar(Constants.MESSAGE_INFO);
			setSuccessfulUpdateMessage();
			return null;
		}
		protected virtual XVar buildNewRecordData()
		{
			dynamic afilename_values = XVar.Array(), avalues = XVar.Array(), blobfields = null, masterTables = XVar.Array(), securityType = null;
			avalues = XVar.Clone(XVar.Array());
			blobfields = XVar.Clone(XVar.Array());
			afilename_values = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in this.addFields.GetEnumerator())
			{
				dynamic control = null;
				control = XVar.Clone(getControl((XVar)(f.Value), (XVar)(this.id)));
				control.readWebValue((XVar)(avalues), (XVar)(blobfields), new XVar(null), new XVar(null), (XVar)(afilename_values));
			}
			securityType = XVar.Clone(this.pSet.getAdvancedSecurityType());
			if((XVar)(!(XVar)(isAdminTable()))  && (XVar)((XVar)(securityType == Constants.ADVSECURITY_EDIT_OWN)  || (XVar)(securityType == Constants.ADVSECURITY_VIEW_OWN)))
			{
				dynamic tableOwnerIdField = null;
				tableOwnerIdField = XVar.Clone(this.pSet.getTableOwnerIdField());
				if(XVar.Pack(checkIfToAddOwnerIdValue((XVar)(tableOwnerIdField), (XVar)(avalues[tableOwnerIdField]))))
				{
					avalues.InitAndSetArrayItem(CommonFunctions.prepare_for_db((XVar)(tableOwnerIdField), (XVar)(XSession.Session[MVCFunctions.Concat("_", this.tName, "_OwnerID")])), tableOwnerIdField);
				}
			}
			masterTables = XVar.Clone(this.pSet.getMasterTablesArr((XVar)(this.tName)));
			foreach (KeyValuePair<XVar, dynamic> mTableData in masterTables.GetEnumerator())
			{
				if(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")] == mTableData.Value["mDataSourceTable"])
				{
					foreach (KeyValuePair<XVar, dynamic> dk in mTableData.Value["detailKeys"].GetEnumerator())
					{
						dynamic masterkeyIdx = null;
						masterkeyIdx = XVar.Clone(MVCFunctions.Concat("masterkey", dk.Key + 1));
						if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.postvalue((XVar)(masterkeyIdx))))))
						{
							XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", masterkeyIdx)] = MVCFunctions.postvalue((XVar)(masterkeyIdx));
						}
						if((XVar)(!(XVar)(avalues.KeyExists(dk.Value)))  || (XVar)(avalues[dk.Value] == ""))
						{
							avalues.InitAndSetArrayItem(CommonFunctions.prepare_for_db((XVar)(dk.Value), (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", masterkeyIdx)])), dk.Value);
						}
					}
				}
			}
			addLookupFilterFieldValue((XVar)(avalues), (XVar)(avalues));
			foreach (KeyValuePair<XVar, dynamic> value in afilename_values.GetEnumerator())
			{
				avalues.InitAndSetArrayItem(value.Value, value.Key);
			}
			this.newRecordData = XVar.Clone(avalues);
			this.newRecordBlobFields = XVar.Clone(blobfields);
			return null;
		}
		protected virtual XVar addLookupFilterFieldValue(dynamic _param_recordData, dynamic values)
		{
			#region pass-by-value parameters
			dynamic recordData = XVar.Clone(_param_recordData);
			#endregion

			dynamic lookupPSet = null;
			lookupPSet = XVar.Clone(CommonFunctions.getLookupMainTableSettings((XVar)(this.tName), (XVar)(this.lookupTable), (XVar)(this.lookupField)));
			if(XVar.Pack(!(XVar)(lookupPSet)))
			{
				return null;
			}
			if(XVar.Pack(lookupPSet.useCategory((XVar)(this.lookupField))))
			{
				foreach (KeyValuePair<XVar, dynamic> cData in lookupPSet.getParentFieldsData((XVar)(this.lookupField)).GetEnumerator())
				{
					if((XVar)(this.parentCtrlsData.KeyExists(cData.Value["main"]))  && (XVar)(!(XVar)(recordData.KeyExists(cData.Value["lookup"]))))
					{
						values.InitAndSetArrayItem(this.parentCtrlsData[cData.Value["main"]], cData.Value["lookup"]);
					}
				}
			}
			return null;
		}
		public virtual XVar captchaExists()
		{
			if((XVar)(this.mode == Constants.ADD_ONTHEFLY)  || (XVar)(this.mode == Constants.ADD_INLINE))
			{
				return false;
			}
			return this.pSet.isCaptchaEnabledOnAdd();
		}
		public override XVar getCaptchaFieldName()
		{
			return this.pSet.captchaAddFieldName();
		}
		protected virtual XVar recheckUserPermissions()
		{
			if(XVar.Pack(CommonFunctions.CheckTablePermissions((XVar)(this.tName), new XVar("A"))))
			{
				return true;
			}
			if((XVar)(CommonFunctions.isLoggedAsGuest())  || (XVar)(!(XVar)(CommonFunctions.isLogged())))
			{
				setMessage((XVar)(MVCFunctions.Concat("Your session has expired.", "<a href='#' id='loginButtonContinue", this.id, "'>", "Login", "</a>", " to save data.")));
			}
			else
			{
				setMessage(new XVar("You have no permissions to complete this action."));
			}
			return false;
		}
		protected virtual XVar callBeforeAddEvent()
		{
			dynamic ret = null, usermessage = null;
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("BeforeAdd")))))
			{
				return true;
			}
			usermessage = new XVar("");
			ret = XVar.Clone(this.eventsObject.BeforeAdd((XVar)(this.newRecordData), ref usermessage, (XVar)(this.mode == Constants.ADD_INLINE), this));
			if(usermessage != XVar.Pack(""))
			{
				setMessage((XVar)(usermessage));
			}
			return ret;
		}
		public virtual XVar checkDeniedDuplicatedValues()
		{
			dynamic ret = null, usermessage = null;
			usermessage = new XVar("");
			ret = XVar.Clone(hasDeniedDuplicateValues((XVar)(this.newRecordData), ref usermessage));
			if(XVar.Pack(ret))
			{
				setMessage((XVar)(usermessage));
			}
			return !(XVar)(ret);
		}
		protected virtual XVar callCustomAddEvent()
		{
			dynamic customAddError = null, keyFields = XVar.Array(), keys = XVar.Array(), ret = null;
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("CustomAdd")))))
			{
				return true;
			}
			keys = XVar.Clone(XVar.Array());
			customAddError = new XVar("");
			ret = XVar.Clone(this.eventsObject.CustomAdd((XVar)(this.newRecordData), (XVar)(keys), (XVar)(customAddError), (XVar)(this.mode == Constants.ADD_INLINE), this));
			if(0 < MVCFunctions.strlen((XVar)(customAddError)))
			{
				this.insertedSuccessfully = new XVar(false);
				setMessage((XVar)(customAddError));
				this.keys = XVar.Clone(XVar.Array());
				return false;
			}
			if(XVar.Pack(ret))
			{
				return true;
			}
			this.insertedSuccessfully = new XVar(true);
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			if(XVar.Pack(MVCFunctions.is_array((XVar)(keys))))
			{
				dynamic allKeysFilled = null;
				allKeysFilled = new XVar(true);
				foreach (KeyValuePair<XVar, dynamic> kf in keyFields.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(keys[kf.Value])))))
					{
						allKeysFilled = new XVar(false);
						break;
					}
				}
				if(XVar.Pack(allKeysFilled))
				{
					this.keys = XVar.Clone(keys);
				}
				else
				{
					prepareTableKeysAfterInsert();
				}
			}
			else
			{
				if((XVar)(keys)  && (XVar)(MVCFunctions.count(keyFields) == 1))
				{
					this.keys = XVar.Clone(new XVar(keyFields[0], keys));
				}
				else
				{
					prepareTableKeysAfterInsert();
				}
			}
			return false;
		}
		protected virtual XVar mergeNewRecordData()
		{
			foreach (KeyValuePair<XVar, dynamic> keyValue in this.keys.GetEnumerator())
			{
				this.newRecordData.InitAndSetArrayItem(keyValue.Value, keyValue.Key);
			}
			return null;
		}
		protected virtual XVar callAfterSuccessfulSave()
		{
			foreach (KeyValuePair<XVar, dynamic> f in this.addFields.GetEnumerator())
			{
				getControl((XVar)(f.Value), (XVar)(this.id)).afterSuccessfulSave();
			}
			return null;
		}
		protected virtual XVar callAfterAddEvent()
		{
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("AfterAdd")))))
			{
				return null;
			}
			if(this.mode != Constants.ADD_MASTER)
			{
				this.eventsObject.AfterAdd((XVar)(this.newRecordData), (XVar)(this.keys), (XVar)(this.mode == Constants.ADD_INLINE), this);
				return null;
			}
			this.afterAdd_id = XVar.Clone(CommonFunctions.generatePassword(new XVar(20)));
			XSession.Session.InitAndSetArrayItem(new XVar("avalues", this.newRecordData, "keys", this.keys, "inlineadd", this.mode == Constants.ADD_INLINE, "time", MVCFunctions.time()), "after_add_data", this.afterAdd_id);
			return null;
		}
		protected virtual XVar setSuccessfulUpdateMessage()
		{
			dynamic infoMessage = null, k = null, keyParams = XVar.Array(), keylink = null, keysArray = XVar.Array();
			if(XVar.Pack(isMessageSet()))
			{
				return null;
			}
			if(this.mode == Constants.ADD_INLINE)
			{
				infoMessage = XVar.Clone(MVCFunctions.Concat("", "Record was added", ""));
			}
			else
			{
				infoMessage = XVar.Clone(MVCFunctions.Concat("<strong><<< ", "Record was added", " >>></strong>"));
			}
			if((XVar)((XVar)(this.mode != Constants.ADD_SIMPLE)  && (XVar)(this.mode != Constants.ADD_MASTER))  || (XVar)(!(XVar)(MVCFunctions.count(this.keys))))
			{
				setMessage((XVar)(infoMessage));
				return null;
			}
			k = new XVar(0);
			keyParams = XVar.Clone(XVar.Array());
			keysArray = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> val in this.keys.GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("editid", ++(k), "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(val.Value))))), null);
				keysArray.InitAndSetArrayItem(val.Value, null);
			}
			keylink = XVar.Clone(MVCFunctions.implode(new XVar("&"), (XVar)(keyParams)));
			if((XVar)((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(0 < MVCFunctions.count(keysArray)))  && (XVar)(this.mode == Constants.ADD_SIMPLE))
			{
				XSession.Session["successKeys"] = keysArray;
			}
			else
			{
				infoMessage = MVCFunctions.Concat(infoMessage, "<br>");
				if(XVar.Pack(editAvailable()))
				{
					infoMessage = MVCFunctions.Concat(infoMessage, "&nbsp;<a href='", MVCFunctions.GetTableLink((XVar)(this.pSet.getShortTableName()), new XVar("edit"), (XVar)(keylink)), "'>", "Edit", "</a>&nbsp;");
				}
				if(XVar.Pack(viewAvailable()))
				{
					infoMessage = MVCFunctions.Concat(infoMessage, "&nbsp;<a href='", MVCFunctions.GetTableLink((XVar)(this.pSet.getShortTableName()), new XVar("view"), (XVar)(keylink)), "'>", "View", "</a>&nbsp;");
				}
			}
			setMessage((XVar)(infoMessage));
			return null;
		}
		protected virtual XVar reportSaveStatus()
		{
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(getSaveStatusJSON())));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar getSaveStatusJSON()
		{
			dynamic addedData = XVar.Array(), allFields = XVar.Array(), data = XVar.Array(), dmapIconsData = null, fieldsIconsData = null, haveData = null, jsKeys = XVar.Array(), keyFields = XVar.Array(), keyParams = XVar.Array(), keylink = null, linkAndDispVals = XVar.Array(), returnJSON = XVar.Array(), showDetailKeys = null, showFields = XVar.Array(), showRawValues = XVar.Array(), showValues = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			if((XVar)(this.action != "added")  || (XVar)(this.mode == Constants.ADD_SIMPLE))
			{
				return returnJSON;
			}
			returnJSON.InitAndSetArrayItem(this.insertedSuccessfully, "success");
			returnJSON.InitAndSetArrayItem(this.message, "message");
			if(XVar.Pack(!(XVar)(this.isCaptchaOk)))
			{
				returnJSON.InitAndSetArrayItem(getCaptchaFieldName(), "wrongCaptchaFieldName");
			}
			else
			{
				if((XVar)((XVar)((XVar)(this.mode == Constants.ADD_POPUP)  || (XVar)(this.mode == Constants.ADD_MASTER))  || (XVar)(this.mode == Constants.ADD_MASTER_POPUP))  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))
				{
					GlobalVars.sessPrefix = XVar.Clone(MVCFunctions.Concat(this.tName, "_", this.pageType));
					if((XVar)((XVar)(XSession.Session.KeyExists("count_passes_captcha"))  || (XVar)(0 < XSession.Session["count_passes_captcha"]))  || (XVar)(XSession.Session["count_passes_captcha"] < 5))
					{
						dynamic respJSON = XVar.Array();
						respJSON.InitAndSetArrayItem(true, "hideCaptcha");
					}
				}
			}
			if(XVar.Pack(!(XVar)(this.insertedSuccessfully)))
			{
				return returnJSON;
			}
			jsKeys = XVar.Clone(XVar.Array());
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			foreach (KeyValuePair<XVar, dynamic> f in keyFields.GetEnumerator())
			{
				jsKeys.InitAndSetArrayItem(this.keys[f.Value], f.Key);
			}
			if(this.mode == Constants.ADD_ONTHEFLY)
			{
				dynamic respData = null;
				addedData = XVar.Clone(GetAddedDataLookupQuery(new XVar(false)));
				data = addedData[0];
				if(XVar.Pack(MVCFunctions.count(data)))
				{
					respData = XVar.Clone(new XVar(addedData[1]["linkField"], data[addedData[1]["linkFieldIndex"]], addedData[1]["displayField"], data[addedData[1]["displayFieldIndex"]]));
				}
				else
				{
					respData = XVar.Clone(new XVar(addedData[1]["linkField"], this.newRecordData[addedData[1]["linkField"]], addedData[1]["displayField"], this.newRecordData[addedData[1]["displayField"]]));
				}
				returnJSON.InitAndSetArrayItem(jsKeys, "keys");
				returnJSON.InitAndSetArrayItem(respData, "vals");
				returnJSON.InitAndSetArrayItem(keyFields, "keyFields");
				return returnJSON;
			}
			data = XVar.Clone(XVar.Array());
			haveData = new XVar(true);
			linkAndDispVals = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(this.keys)))
			{
				dynamic where = null;
				where = XVar.Clone(keysSQLExpression((XVar)(this.keys)));
				if((XVar)(this.mode == Constants.ADD_INLINE)  && (XVar)(this.forListPageLookup))
				{
					addedData = XVar.Clone(GetAddedDataLookupQuery(new XVar(true)));
					data = addedData[0];
					linkAndDispVals = XVar.Clone(new XVar("linkField", addedData[0][addedData[1]["linkField"]], "displayField", addedData[0][addedData[1]["displayField"]]));
				}
				else
				{
					GlobalVars.strSQL = XVar.Clone(this.gQuery.buildSQL_default((XVar)(new XVar(0, CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.tName)), 1, keysSQLExpression((XVar)(this.keys))))));
					data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
				}
			}
			if(XVar.Pack(!(XVar)(data)))
			{
				data = XVar.Clone(this.newRecordData);
				haveData = new XVar(false);
			}
			showDetailKeys = XVar.Clone(getShowDetailKeys((XVar)(data)));
			keyParams = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> kf in this.pSet.getTableKeys().GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("key", kf.Key + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[kf.Value]))))), null);
			}
			keylink = XVar.Clone(MVCFunctions.Concat("&", MVCFunctions.implode(new XVar("&"), (XVar)(keyParams))));
			showValues = XVar.Clone(XVar.Array());
			showFields = XVar.Clone(XVar.Array());
			showRawValues = XVar.Clone(XVar.Array());
			allFields = XVar.Clone(this.pSet.getFieldsList());
			foreach (KeyValuePair<XVar, dynamic> f in allFields.GetEnumerator())
			{
				if((XVar)((XVar)((XVar)((XVar)(this.mode == Constants.ADD_MASTER)  || (XVar)(this.mode == Constants.ADD_MASTER_POPUP))  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))  && (XVar)(this.pSet.appearOnAddPage((XVar)(f.Value))))  || (XVar)(this.pSet.appearOnListPage((XVar)(f.Value))))
				{
					showValues.InitAndSetArrayItem(showDBValue((XVar)(f.Value), (XVar)(data), (XVar)(keylink)), f.Value);
					showFields.InitAndSetArrayItem(f.Value, null);
				}
				if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(this.pSet.getFieldType((XVar)(f.Value))))))
				{
					showRawValues.InitAndSetArrayItem("", f.Value);
				}
				else
				{
					showRawValues.InitAndSetArrayItem(MVCFunctions.runner_substr((XVar)(data[f.Value]), new XVar(0), new XVar(100)), f.Value);
				}
			}
			returnJSON.InitAndSetArrayItem(jsKeys, "keys");
			returnJSON.InitAndSetArrayItem(showValues, "vals");
			returnJSON.InitAndSetArrayItem(showFields, "fields");
			returnJSON.InitAndSetArrayItem(showDetailKeys, "detKeys");
			dmapIconsData = XVar.Clone(getDashMapsIconsData((XVar)(data)));
			if(XVar.Pack(MVCFunctions.count(dmapIconsData)))
			{
				returnJSON.InitAndSetArrayItem(dmapIconsData, "mapIconsData");
			}
			fieldsIconsData = XVar.Clone(getFieldMapIconsData((XVar)(data)));
			if(XVar.Pack(MVCFunctions.count(fieldsIconsData)))
			{
				returnJSON.InitAndSetArrayItem(fieldsIconsData, "fieldsMapIconsData");
			}
			if((XVar)((XVar)(this.mode == Constants.ADD_INLINE)  || (XVar)(this.mode == Constants.ADD_POPUP))  || (XVar)(this.mode == Constants.ADD_DASHBOARD))
			{
				returnJSON.InitAndSetArrayItem(!(XVar)(haveData), "noKeys");
				returnJSON.InitAndSetArrayItem(keyFields, "keyFields");
				returnJSON.InitAndSetArrayItem(showRawValues, "rawVals");
				returnJSON.InitAndSetArrayItem(buildDetailGridLinks((XVar)(showDetailKeys)), "hrefs");
				if(XVar.Pack(MVCFunctions.count(linkAndDispVals)))
				{
					returnJSON.InitAndSetArrayItem(linkAndDispVals["linkField"], "linkValue");
					returnJSON.InitAndSetArrayItem(linkAndDispVals["displayField"], "displayValue");
				}
				if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
				{
					if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.IsRecordEditable((XVar)(showRawValues), new XVar(true), (XVar)(this.tName)))))
					{
						returnJSON.InitAndSetArrayItem(true, "nonEditable");
					}
				}
				return returnJSON;
			}
			if((XVar)((XVar)(this.mode == Constants.ADD_MASTER)  || (XVar)(this.mode == Constants.ADD_MASTER_POPUP))  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))
			{
				XSession.Session["message_add"] = (XVar.Pack(this.message) ? XVar.Pack(this.message) : XVar.Pack(""));
				returnJSON.InitAndSetArrayItem(this.afterAdd_id, "afterAddId");
				returnJSON.InitAndSetArrayItem(getDetailTablesMasterKeys(), "mKeys");
				if((XVar)(this.mode == Constants.ADD_MASTER_POPUP)  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))
				{
					returnJSON.InitAndSetArrayItem(!(XVar)(haveData), "noKeys");
					returnJSON.InitAndSetArrayItem(keyFields, "keyFields");
					returnJSON.InitAndSetArrayItem(showRawValues, "rawVals");
					returnJSON.InitAndSetArrayItem(buildDetailGridLinks((XVar)(showDetailKeys)), "hrefs");
					if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
					{
						if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.IsRecordEditable((XVar)(showRawValues), new XVar(true), (XVar)(this.tName)))))
						{
							returnJSON.InitAndSetArrayItem(true, "nonEditable");
						}
					}
				}
				return returnJSON;
			}
			return null;
		}
		protected virtual XVar getShowDetailKeys(dynamic data)
		{
			dynamic showDetailKeys = XVar.Array();
			showDetailKeys = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> dt in this.pSet.getDetailTablesArr().GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> dk in dt.Value["masterKeys"].GetEnumerator())
				{
					showDetailKeys.InitAndSetArrayItem(data[dk.Value], dt.Value["dDataSourceTable"], MVCFunctions.Concat("masterkey", dk.Key + 1));
				}
			}
			if(getAfterAddAction() == Constants.AA_TO_DETAIL_ADD)
			{
				dynamic AAdTName = null, dTUrl = null;
				AAdTName = XVar.Clone(this.pSet.getAADetailTable());
				dTUrl = XVar.Clone(CommonFunctions.GetTableURL((XVar)(AAdTName)));
				if(XVar.Pack(!(XVar)(showDetailKeys.KeyExists(dTUrl))))
				{
					showDetailKeys.InitAndSetArrayItem(showDetailKeys[AAdTName], dTUrl);
				}
			}
			return showDetailKeys;
		}
		protected virtual XVar getDetailTablesMasterKeys()
		{
			dynamic data = null, dpParams = XVar.Array(), i = null, mKeysData = XVar.Array();
			if((XVar)(!(XVar)(this.isShowDetailTables))  || (XVar)(mobileTemplateMode()))
			{
				return XVar.Array();
			}
			dpParams = XVar.Clone(getDetailsParams((XVar)(this.id)));
			data = XVar.Clone(getAddedRecordFullData());
			mKeysData = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(dpParams["ids"]); i++)
			{
				mKeysData.InitAndSetArrayItem(getMasterKeysData((XVar)(dpParams["strTableNames"][i]), (XVar)(data)), dpParams["strTableNames"][i]);
			}
			return mKeysData;
		}
		protected virtual XVar getAddedRecordFullData()
		{
			dynamic data = null;
			if(XVar.Pack(MVCFunctions.count(this.keys)))
			{
				GlobalVars.strSQL = XVar.Clone(this.gQuery.buildSQL_default((XVar)(new XVar(0, CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.tName)), 1, keysSQLExpression((XVar)(this.keys))))));
				data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
			}
			if(XVar.Pack(!(XVar)(data)))
			{
				data = XVar.Clone(this.newRecordData);
			}
			return data;
		}
		protected virtual XVar getMasterKeysData(dynamic _param_dTableName, dynamic data)
		{
			#region pass-by-value parameters
			dynamic dTableName = XVar.Clone(_param_dTableName);
			#endregion

			dynamic mKeyId = null, mKeys = XVar.Array(), mKeysData = XVar.Array();
			mKeyId = new XVar(1);
			mKeysData = XVar.Clone(XVar.Array());
			mKeys = XVar.Clone(this.pSet.getMasterKeysByDetailTable((XVar)(dTableName)));
			foreach (KeyValuePair<XVar, dynamic> mk in mKeys.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(data[mk.Value]))))
				{
					mKeysData.InitAndSetArrayItem(data[mk.Value], MVCFunctions.Concat("masterkey", mKeyId++));
				}
				else
				{
					mKeysData.InitAndSetArrayItem("", MVCFunctions.Concat("masterkey", mKeyId++));
				}
			}
			return mKeysData;
		}
		protected virtual XVar afterAddActionRedirect()
		{
			dynamic dTName = null;
			if(this.mode != Constants.ADD_SIMPLE)
			{
				return false;
			}
			switch(((XVar)getAfterAddAction()).ToInt())
			{
				case Constants.AA_TO_ADD:
					if(XVar.Pack(this.insertedSuccessfully))
					{
						return prgRedirect();
					}
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_ADD));
					return true;
				case Constants.AA_TO_LIST:
					if(XVar.Pack(this.pSet.hasListPage()))
					{
						MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_LIST), new XVar("a=return"));
					}
					else
					{
						MVCFunctions.HeaderRedirect(new XVar("menu"));
					}
					return true;
				case Constants.AA_TO_VIEW:
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_VIEW), (XVar)(getKeyParams()));
					return true;
				case Constants.AA_TO_EDIT:
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_EDIT), (XVar)(getKeyParams()));
					return true;
				case Constants.AA_TO_DETAIL_LIST:
					dTName = XVar.Clone(this.pSet.getAADetailTable());
					MVCFunctions.HeaderRedirect((XVar)(CommonFunctions.GetTableURL((XVar)(dTName))), new XVar(Constants.PAGE_LIST), (XVar)(MVCFunctions.Concat(MVCFunctions.implode(new XVar("&"), (XVar)(getNewRecordMasterKeys((XVar)(dTName)))), "&mastertable=", this.tName)));
					return true;
				case Constants.AA_TO_DETAIL_ADD:
					XSession.Session["message_add"] = (XVar.Pack(this.message) ? XVar.Pack(this.message) : XVar.Pack(""));
					dTName = XVar.Clone(this.pSet.getAADetailTable());
					MVCFunctions.HeaderRedirect((XVar)(CommonFunctions.GetTableURL((XVar)(dTName))), new XVar(Constants.PAGE_ADD), (XVar)(MVCFunctions.Concat(MVCFunctions.implode(new XVar("&"), (XVar)(getNewRecordMasterKeys((XVar)(dTName)))), "&mastertable=", this.tName)));
					return true;
				default:
					return false;
			}
			return null;
		}
		public virtual XVar getNewRecordMasterKeys(dynamic _param_dTName)
		{
			#region pass-by-value parameters
			dynamic dTName = XVar.Clone(_param_dTName);
			#endregion

			dynamic data = XVar.Array(), mKeys = XVar.Array();
			data = XVar.Clone(getNewRecordData());
			mKeys = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> mk in this.pSet.getMasterKeysByDetailTable((XVar)(dTName)).GetEnumerator())
			{
				mKeys.InitAndSetArrayItem(MVCFunctions.Concat("masterkey", mk.Key + 1, "=", data[mk.Value]), null);
			}
			return mKeys;
		}
		protected virtual XVar prgRedirect()
		{
			if(XVar.Pack(this.stopPRG))
			{
				return false;
			}
			if((XVar)((XVar)(!(XVar)(this.insertedSuccessfully))  || (XVar)(this.mode != Constants.ADD_SIMPLE))  || (XVar)(!(XVar)(MVCFunctions.no_output_done())))
			{
				return false;
			}
			XSession.Session["message_add"] = (XVar.Pack(this.message) ? XVar.Pack(this.message) : XVar.Pack(""));
			XSession.Session["message_add_type"] = this.messageType;
			MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), (XVar)(this.pageType));
			return true;
		}
		protected virtual XVar prgReadMessage()
		{
			if((XVar)(this.mode == Constants.ADD_SIMPLE)  && (XVar)(XSession.Session.KeyExists("message_add")))
			{
				this.message = XVar.Clone(XSession.Session["message_add"]);
				this.messageType = XVar.Clone(XSession.Session["message_add_type"]);
				XSession.Session.Remove("message_add");
			}
			return null;
		}
		public override XVar getCurrentRecord()
		{
			dynamic data = XVar.Array();
			data = XVar.Clone(XVar.Array());
			if((XVar)(this.masterTable)  && (XVar)(0 < MVCFunctions.count(this.masterKeysReq)))
			{
				foreach (KeyValuePair<XVar, dynamic> detKey in this.detailKeysByM.GetEnumerator())
				{
					data.InitAndSetArrayItem(this.masterKeysReq[detKey.Key + 1], detKey.Value);
				}
			}
			return data;
		}
		protected virtual XVar prepareDefvalues()
		{
			dynamic masterTables = XVar.Array(), securityType = null;
			if((XVar)((XVar)(MVCFunctions.REQUESTKeyExists("copyid1"))  || (XVar)(MVCFunctions.REQUESTKeyExists("editid1")))  && (XVar)(this.mode != Constants.ADD_DASHBOARD))
			{
				dynamic copykeys = XVar.Array(), keyFields = XVar.Array(), prefix = null;
				copykeys = XVar.Clone(XVar.Array());
				keyFields = XVar.Clone(this.pSet.getTableKeys());
				prefix = XVar.Clone((XVar.Pack(MVCFunctions.REQUESTKeyExists("copyid1")) ? XVar.Pack("copyid") : XVar.Pack("editid")));
				foreach (KeyValuePair<XVar, dynamic> k in keyFields.GetEnumerator())
				{
					copykeys.InitAndSetArrayItem(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat(prefix, k.Key + 1))), k.Value);
				}
				GlobalVars.strSQL = XVar.Clone(this.gQuery.buildSQL_default((XVar)(new XVar(0, CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.tName)), 1, keysSQLExpression((XVar)(copykeys))))));
				this.defvalues = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
				if(XVar.Pack(!(XVar)(this.defvalues)))
				{
					this.defvalues = XVar.Clone(XVar.Array());
				}
				foreach (KeyValuePair<XVar, dynamic> k in keyFields.GetEnumerator())
				{
					this.defvalues.InitAndSetArrayItem("", k.Value);
				}
				foreach (KeyValuePair<XVar, dynamic> f in this.addFields.GetEnumerator())
				{
					if(this.pSet.getEditFormat((XVar)(f.Value)) == Constants.EDIT_FORMAT_FILE)
					{
						this.defvalues.InitAndSetArrayItem(getControl((XVar)(f.Value), (XVar)(this.id)).getFieldValueCopy((XVar)(this.defvalues[f.Value])), f.Value);
					}
				}
				if(XVar.Pack(this.eventsObject.exists(new XVar("CopyOnLoad"))))
				{
					dynamic strWhere = null;
					this.eventsObject.CopyOnLoad((XVar)(this.defvalues), (XVar)(strWhere), this);
				}
			}
			else
			{
				foreach (KeyValuePair<XVar, dynamic> f in this.addFields.GetEnumerator())
				{
					dynamic defaultValue = null;
					defaultValue = XVar.Clone(MVCFunctions.GetDefaultValue((XVar)(f.Value), new XVar(Constants.PAGE_ADD)));
					if(XVar.Pack(MVCFunctions.strlen((XVar)(defaultValue))))
					{
						this.defvalues.InitAndSetArrayItem(defaultValue, f.Value);
					}
				}
			}
			securityType = XVar.Clone(this.pSet.getAdvancedSecurityType());
			if((XVar)(!(XVar)(isAdminTable()))  && (XVar)((XVar)(securityType == Constants.ADVSECURITY_EDIT_OWN)  || (XVar)(securityType == Constants.ADVSECURITY_VIEW_OWN)))
			{
				dynamic tableOwnerIdField = null;
				tableOwnerIdField = XVar.Clone(this.pSet.getTableOwnerIdField());
				if(XVar.Pack(checkIfToAddOwnerIdValue((XVar)(tableOwnerIdField), new XVar(""))))
				{
					this.defvalues.InitAndSetArrayItem(CommonFunctions.prepare_for_db((XVar)(tableOwnerIdField), (XVar)(XSession.Session[MVCFunctions.Concat("_", this.tName, "_OwnerID")])), tableOwnerIdField);
				}
			}
			masterTables = XVar.Clone(this.pSet.getMasterTablesArr((XVar)(this.tName)));
			foreach (KeyValuePair<XVar, dynamic> mTableData in masterTables.GetEnumerator())
			{
				if(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")] == mTableData.Value["mDataSourceTable"])
				{
					foreach (KeyValuePair<XVar, dynamic> dk in mTableData.Value["detailKeys"].GetEnumerator())
					{
						dynamic masterkeyIdx = null;
						masterkeyIdx = XVar.Clone(MVCFunctions.Concat("masterkey", dk.Key + 1));
						if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.postvalue((XVar)(masterkeyIdx))))))
						{
							XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", masterkeyIdx)] = MVCFunctions.postvalue((XVar)(masterkeyIdx));
						}
						if(this.masterPageType != Constants.PAGE_ADD)
						{
							this.defvalues.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", masterkeyIdx)], dk.Value);
						}
					}
				}
			}
			addLookupFilterFieldValue((XVar)(this.newRecordData), (XVar)(this.defvalues));
			if(XVar.Pack(this.readAddValues))
			{
				foreach (KeyValuePair<XVar, dynamic> fName in this.addFields.GetEnumerator())
				{
					dynamic editFormat = null;
					editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(fName.Value)));
					if((XVar)((XVar)(editFormat != Constants.EDIT_FORMAT_DATABASE_FILE)  && (XVar)(editFormat != Constants.EDIT_FORMAT_DATABASE_IMAGE))  && (XVar)(editFormat != Constants.EDIT_FORMAT_FILE))
					{
						this.defvalues.InitAndSetArrayItem(this.newRecordData[fName.Value], fName.Value);
					}
				}
			}
			return null;
		}
		protected virtual XVar prepareReadonlyFields()
		{
			foreach (KeyValuePair<XVar, dynamic> f in this.addFields.GetEnumerator())
			{
				if(this.pSet.getEditFormat((XVar)(f.Value)) == Constants.EDIT_FORMAT_READONLY)
				{
					this.readOnlyFields.InitAndSetArrayItem(showDBValue((XVar)(f.Value), (XVar)(this.defvalues)), f.Value);
				}
			}
			return null;
		}
		protected virtual XVar prepareButtons()
		{
			dynamic addStyle = null;
			if(this.mode == Constants.ADD_INLINE)
			{
				return null;
			}
			this.xt.assign(new XVar("save_button"), new XVar(true));
			addStyle = new XVar("");
			if(XVar.Pack(isMultistepped()))
			{
				addStyle = new XVar(" style=\"display: none;\"");
			}
			this.xt.assign(new XVar("savebutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"saveButton", this.id, "\"", addStyle)));
			if(this.mode == Constants.ADD_SIMPLE)
			{
				if(XVar.Pack(this.pSet.hasListPage()))
				{
					this.xt.assign(new XVar("back_button"), new XVar(true));
					this.xt.assign(new XVar("backbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"backButton", this.id, "\"")));
				}
				else
				{
					if(XVar.Pack(isShowMenu()))
					{
						this.xt.assign(new XVar("back_button"), new XVar(true));
						this.xt.assign(new XVar("backbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"backToMenuButton", this.id, "\"")));
					}
				}
			}
			if(this.mode == Constants.ADD_DASHBOARD)
			{
				this.xt.assign(new XVar("reset_button"), new XVar(true));
				return null;
			}
			if((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(XSession.Session.KeyExists("successKeys")))
			{
				dynamic dataKeysAttr = null;
				dataKeysAttr = XVar.Clone(MVCFunctions.Concat("data-keys=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.my_json_encode((XVar)(XSession.Session["successKeys"])))), "\""));
				XSession.Session.Remove("successKeys");
				if(XVar.Pack(viewAvailable()))
				{
					this.xt.assign(new XVar("view_page_button"), new XVar(true));
					this.xt.assign(new XVar("view_page_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"viewPageButton", this.id, "\" ", dataKeysAttr)));
				}
				if(XVar.Pack(editAvailable()))
				{
					this.xt.assign(new XVar("edit_page_button"), new XVar(true));
					this.xt.assign(new XVar("edit_page_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"editPageButton", this.id, "\" ", dataKeysAttr)));
				}
			}
			if((XVar)((XVar)(this.mode != Constants.ADD_ONTHEFLY)  && (XVar)(this.mode != Constants.ADD_POPUP))  && (XVar)(this.mode != Constants.ADD_MASTER_DASH))
			{
				if(XVar.Pack(this.pSet.hasListPage()))
				{
					this.xt.assign(new XVar("back_button"), new XVar(true));
				}
				else
				{
					if(XVar.Pack(isShowMenu()))
					{
						this.xt.assign(new XVar("backToMenu_button"), new XVar(true));
					}
				}
			}
			else
			{
				this.xt.assign(new XVar("cancel_button"), new XVar(true));
			}
			return null;
		}
		protected virtual XVar prepareEditControls()
		{
			dynamic control = null, controlFields = XVar.Array();
			controlFields = XVar.Clone(this.addFields);
			if(this.mode == Constants.ADD_INLINE)
			{
				controlFields = XVar.Clone(removeHiddenColumnsFromInlineFields((XVar)(controlFields), (XVar)(this.screenWidth), (XVar)(this.screenHeight), (XVar)(this.orientation)));
			}
			control = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fName in controlFields.GetEnumerator())
			{
				dynamic controls = XVar.Array(), gfName = null, isDetKeyField = null, parameters = XVar.Array(), preload = XVar.Array();
				gfName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fName.Value)));
				isDetKeyField = XVar.Clone(MVCFunctions.in_array((XVar)(fName.Value), (XVar)(this.detailKeysByM)));
				controls = XVar.Clone(XVar.Array());
				controls.InitAndSetArrayItem(XVar.Array(), "controls");
				controls.InitAndSetArrayItem(this.id, "controls", "id");
				controls.InitAndSetArrayItem(0, "controls", "ctrlInd");
				controls.InitAndSetArrayItem(fName.Value, "controls", "fieldName");
				if(XVar.Pack(MVCFunctions.in_array((XVar)(fName.Value), (XVar)(this.errorFields))))
				{
					controls.InitAndSetArrayItem(true, "controls", "isInvalid");
				}
				parameters = XVar.Clone(XVar.Array());
				parameters.InitAndSetArrayItem(this.id, "id");
				parameters.InitAndSetArrayItem(Constants.PAGE_ADD, "ptype");
				parameters.InitAndSetArrayItem(fName.Value, "field");
				parameters.InitAndSetArrayItem(this.defvalues[fName.Value], "value");
				parameters.InitAndSetArrayItem(this, "pageObj");
				if(XVar.Pack(!(XVar)(isDetKeyField)))
				{
					parameters.InitAndSetArrayItem(this.pSet.getValidation((XVar)(fName.Value)), "validate");
					if(XVar.Pack(this.pSet.isUseRTE((XVar)(fName.Value))))
					{
						XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", fName.Value, "_rte")] = this.defvalues[fName.Value];
					}
				}
				if(XVar.Pack(this.pSet.isUseRTE((XVar)(fName.Value))))
				{
					parameters.InitAndSetArrayItem("add", "mode");
					controls.InitAndSetArrayItem("add", "controls", "mode");
				}
				else
				{
					dynamic controlMode = null;
					controlMode = XVar.Clone((XVar.Pack(this.mode == Constants.ADD_INLINE) ? XVar.Pack("inline_add") : XVar.Pack("add")));
					parameters.InitAndSetArrayItem(controlMode, "mode");
					controls.InitAndSetArrayItem(controlMode, "controls", "mode");
				}
				if((XVar)(isDetKeyField)  && (XVar)(fName.Value))
				{
					controls.InitAndSetArrayItem(this.defvalues[fName.Value], "controls", "value");
					parameters.InitAndSetArrayItem(XVar.Array(), "extraParams");
					parameters.InitAndSetArrayItem(true, "extraParams", "getDetKeyReadOnlyCtrl");
					this.readOnlyFields.InitAndSetArrayItem(showDBValue((XVar)(fName.Value), (XVar)(this.defvalues)), fName.Value);
				}
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					dynamic firstElementId = null;
					firstElementId = XVar.Clone(getControl((XVar)(fName.Value), (XVar)(this.id)).getFirstElementId());
					if(XVar.Pack(firstElementId))
					{
						this.xt.assign((XVar)(MVCFunctions.Concat("labelfor_", MVCFunctions.GoodFieldName((XVar)(fName.Value)))), (XVar)(firstElementId));
					}
				}
				this.xt.assign_function((XVar)(MVCFunctions.Concat(gfName, "_editcontrol")), new XVar("xt_buildeditcontrol"), (XVar)(parameters));
				preload = XVar.Clone(fillPreload((XVar)(fName.Value), (XVar)(controlFields), (XVar)(this.defvalues)));
				if(!XVar.Equals(XVar.Pack(preload), XVar.Pack(false)))
				{
					controls.InitAndSetArrayItem(preload, "controls", "preloadData");
					if((XVar)(!(XVar)(this.defvalues[fName.Value]))  && (XVar)(0 < MVCFunctions.count(preload["vals"])))
					{
						this.defvalues.InitAndSetArrayItem(preload["vals"][0], fName.Value);
					}
				}
				fillControlsMap((XVar)(controls));
				fillFieldToolTips((XVar)(fName.Value));
				fillControlFlags((XVar)(fName.Value));
				if(this.pSet.getEditFormat((XVar)(fName.Value)) == "Time")
				{
					fillTimePickSettings((XVar)(fName.Value), (XVar)(this.defvalues[fName.Value]));
				}
			}
			return null;
		}
		protected virtual XVar prepareDetailsTables()
		{
			dynamic d = null, dpParams = XVar.Array();
			if((XVar)((XVar)(!(XVar)(this.isShowDetailTables))  || (XVar)((XVar)((XVar)((XVar)(this.mode != Constants.ADD_SIMPLE)  && (XVar)(this.mode != Constants.ADD_POPUP))  && (XVar)(this.mode != Constants.ADD_DASHBOARD))  && (XVar)(this.mode != Constants.ADD_MASTER_DASH)))  || (XVar)(mobileTemplateMode()))
			{
				return null;
			}
			dpParams = XVar.Clone(getDetailsParams((XVar)(this.id)));
			this.jsSettings.InitAndSetArrayItem(0 < MVCFunctions.count(dpParams), "tableSettings", this.tName, "isShowDetails");
			this.jsSettings.InitAndSetArrayItem(new XVar("tableNames", dpParams["strTableNames"], "ids", dpParams["ids"]), "tableSettings", this.tName, "dpParams");
			if(XVar.Pack(!(XVar)(MVCFunctions.count(dpParams["ids"]))))
			{
				return null;
			}
			this.xt.assign(new XVar("detail_tables"), new XVar(true));
			d = new XVar(0);
			for(;d < MVCFunctions.count(dpParams["ids"]); d++)
			{
				setDetailPreview(new XVar("list"), (XVar)(dpParams["strTableNames"][d]), (XVar)(dpParams["ids"][d]), (XVar)(this.defvalues));
				displayDetailsButtons((XVar)(dpParams["type"][d]), (XVar)(dpParams["strTableNames"][d]), (XVar)(dpParams["ids"][d]));
			}
			return null;
		}
		protected virtual XVar displayDetailsButtons(dynamic _param_dpType, dynamic _param_dpTableName, dynamic _param_dpId)
		{
			#region pass-by-value parameters
			dynamic dpType = XVar.Clone(_param_dpType);
			dynamic dpTableName = XVar.Clone(_param_dpTableName);
			dynamic dpId = XVar.Clone(_param_dpId);
			#endregion

			dynamic listPageObject = null;
			if(XVar.Pack(!(XVar)(CommonFunctions.CheckTablePermissions((XVar)(dpTableName), new XVar("S")))))
			{
				return null;
			}
			listPageObject = XVar.Clone(getDetailsPageObject((XVar)(dpTableName), (XVar)(dpId)));
			if((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(listPageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				listPageObject.assignButtonsOnMasterEdit((XVar)(this.xt));
			}
			return null;
		}
		protected virtual XVar doCommonAssignments()
		{
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				if(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.ADD_SIMPLE)))
				{
					headerCommonAssign();
				}
				else
				{
					this.xt.assign(new XVar("menu_chiddenattr"), new XVar("data-hidden"));
				}
			}
			this.xt.assign(new XVar("message_block"), new XVar(true));
			if(XVar.Pack(isMessageSet()))
			{
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					dynamic mesClass = null;
					mesClass = XVar.Clone((XVar.Pack(this.messageType == Constants.MESSAGE_ERROR) ? XVar.Pack("message rnr-error") : XVar.Pack("message")));
					this.xt.assign(new XVar("message"), (XVar)(MVCFunctions.Concat("<div class='", mesClass, "' >", this.message, "</div>")));
				}
				else
				{
					this.xt.assign(new XVar("message"), (XVar)(this.message));
					this.xt.assign(new XVar("message_class"), (XVar)((XVar.Pack(this.messageType == Constants.MESSAGE_ERROR) ? XVar.Pack("alert alert-danger") : XVar.Pack("alert alert-success"))));
				}
			}
			else
			{
				this.xt.displayBrickHidden(new XVar("message"));
			}
			if(this.mode != Constants.ADD_INLINE)
			{
				assignAddFieldsBlocksAndLabels();
			}
			if(this.mode == Constants.ADD_SIMPLE)
			{
				assignBody();
				this.xt.assign(new XVar("flybody"), new XVar(true));
			}
			if((XVar)((XVar)((XVar)(this.mode == Constants.ADD_ONTHEFLY)  || (XVar)(this.mode == Constants.ADD_POPUP))  || (XVar)(this.mode == Constants.ADD_DASHBOARD))  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))
			{
				this.xt.assign(new XVar("body"), new XVar(true));
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("flybody"), (XVar)(this.body));
			}
			return null;
		}
		public virtual XVar assignAddFieldsBlocksAndLabels()
		{
			foreach (KeyValuePair<XVar, dynamic> fName in this.addFields.GetEnumerator())
			{
				dynamic gfName = null;
				gfName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fName.Value)));
				this.xt.assign((XVar)(MVCFunctions.Concat(gfName, "_fieldblock")), new XVar(true));
				this.xt.assign((XVar)(MVCFunctions.Concat(gfName, "_tabfieldblock")), new XVar(true));
				if((XVar)(this.is508)  && (XVar)(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT))
				{
					this.xt.assign_section((XVar)(MVCFunctions.Concat(gfName, "_label")), (XVar)(MVCFunctions.Concat("<label for=\"", getInputElementId((XVar)(fName.Value)), "\">")), new XVar("</label>"));
				}
			}
			return null;
		}
		protected virtual XVar displayAddPage()
		{
			dynamic templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowAdd"))))
			{
				this.eventsObject.BeforeShowAdd((XVar)(this.xt), ref templatefile, this);
			}
			displayMasterTableInfo();
			if(this.mode == Constants.ADD_SIMPLE)
			{
				display((XVar)(templatefile));
				return null;
			}
			if((XVar)((XVar)((XVar)(this.mode == Constants.ADD_ONTHEFLY)  || (XVar)(this.mode == Constants.ADD_POPUP))  || (XVar)(this.mode == Constants.ADD_DASHBOARD))  || (XVar)(this.mode == Constants.ADD_MASTER_DASH))
			{
				displayAJAX((XVar)(templatefile), (XVar)(this.id + 1));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			if(this.mode == Constants.ADD_INLINE)
			{
				dynamic returnJSON = XVar.Array();
				returnJSON = XVar.Clone(XVar.Array());
				this.xt.load_template((XVar)(templatefile));
				returnJSON.InitAndSetArrayItem(XVar.Array(), "html");
				foreach (KeyValuePair<XVar, dynamic> fName in this.addFields.GetEnumerator())
				{
					returnJSON.InitAndSetArrayItem(this.xt.fetchVar((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fName.Value)), "_editcontrol"))), "html", fName.Value);
				}
				returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
				returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
				returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
				returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
				returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
				returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "additionalCSS");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			return null;
		}
		protected override XVar getExtraAjaxPageParams()
		{
			return getSaveStatusJSON();
		}
		protected virtual XVar GetAddedDataLookupQuery(dynamic _param_forLookup)
		{
			#region pass-by-value parameters
			dynamic forLookup = XVar.Clone(_param_forLookup);
			#endregion

			dynamic LookupSQL = null, data = XVar.Array(), dispfield = null, linkFieldName = null, lookupIndexes = XVar.Array(), lookupMainSettings = null, lookupQueryObj = null, mainTable = null;
			lookupMainSettings = XVar.Clone(CommonFunctions.getLookupMainTableSettings((XVar)(this.tName), (XVar)(this.lookupTable), (XVar)(this.lookupField), (XVar)(this.lookupPageType)));
			if(XVar.Pack(!(XVar)(lookupMainSettings)))
			{
				return XVar.Array();
			}
			LookupSQL = new XVar("");
			mainTable = XVar.Clone(lookupMainSettings.getTableName());
			linkFieldName = XVar.Clone(lookupMainSettings.getLinkField((XVar)(this.lookupField)));
			dispfield = XVar.Clone(lookupMainSettings.getDisplayField((XVar)(this.lookupField)));
			lookupQueryObj = XVar.Clone(this.pSet.getSQLQuery().CloneObject());
			if(XVar.Pack(lookupMainSettings.getCustomDisplay((XVar)(this.lookupField))))
			{
				lookupQueryObj.AddCustomExpression((XVar)(dispfield), (XVar)(this.pSet), (XVar)(mainTable), (XVar)(this.lookupField));
			}
			data = XVar.Clone(XVar.Array());
			lookupIndexes = XVar.Clone(new XVar("linkFieldIndex", 0, "displayFieldIndex", 0));
			if(XVar.Pack(MVCFunctions.count(this.keys)))
			{
				LookupSQL = XVar.Clone(lookupQueryObj.buildSQL_default((XVar)(CommonFunctions.KeyWhere((XVar)(this.keys)))));
				lookupIndexes = XVar.Clone(CommonFunctions.GetLookupFieldsIndexes((XVar)(lookupMainSettings), (XVar)(this.lookupField)));
				CommonFunctions.LogInfo((XVar)(LookupSQL));
				if(XVar.Pack(forLookup))
				{
					data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.query((XVar)(LookupSQL)).fetchAssoc())));
				}
				else
				{
					if(XVar.Pack(LookupSQL))
					{
						data = XVar.Clone(this.connection.query((XVar)(LookupSQL)).fetchNumeric());
						data.InitAndSetArrayItem(this.cipherer.DecryptField((XVar)(linkFieldName), (XVar)(data[lookupIndexes["linkFieldIndex"]])), lookupIndexes["linkFieldIndex"]);
						data.InitAndSetArrayItem(this.cipherer.DecryptField((XVar)(dispfield), (XVar)(data[lookupIndexes["displayFieldIndex"]])), lookupIndexes["displayFieldIndex"]);
					}
				}
			}
			return new XVar(0, data, 1, new XVar("linkField", linkFieldName, "displayField", dispfield, "linkFieldIndex", lookupIndexes["linkFieldIndex"], "displayFieldIndex", lookupIndexes["displayFieldIndex"]));
		}
		public virtual XVar checkIfToAddOwnerIdValue(dynamic _param_ownerField, dynamic _param_currentValue)
		{
			#region pass-by-value parameters
			dynamic ownerField = XVar.Clone(_param_ownerField);
			dynamic currentValue = XVar.Clone(_param_currentValue);
			#endregion

			return (XVar)((XVar)(this.pSet.getOriginalTableName() == this.pSet.getOwnerTable((XVar)(ownerField)))  && (XVar)(!(XVar)(isAutoincPrimaryKey((XVar)(ownerField)))))  && (XVar)((XVar)(!(XVar)(CommonFunctions.CheckTablePermissions((XVar)(this.tName), new XVar("M"))))  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(currentValue)))));
		}
		protected virtual XVar isAutoincPrimaryKey(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic keyFields = null;
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			return (XVar)((XVar)(MVCFunctions.count(keyFields) == 1)  && (XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(keyFields))))  && (XVar)(this.pSet.isAutoincField((XVar)(field)));
		}
		protected virtual XVar prepareTableKeysAfterInsert()
		{
			dynamic keyFields = XVar.Array(), table = null;
			table = XVar.Clone(this.pSet.getOriginalTableName());
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			foreach (KeyValuePair<XVar, dynamic> k in keyFields.GetEnumerator())
			{
				if(XVar.Pack(this.newRecordData.KeyExists(k.Value)))
				{
					this.keys.InitAndSetArrayItem(this.newRecordData[k.Value], k.Value);
				}
				else
				{
					if(XVar.Pack(this.pSet.isAutoincField((XVar)(k.Value))))
					{
						dynamic oraSequenceName = null;
						oraSequenceName = XVar.Clone(this.pSet.getOraSequenceName((XVar)(k.Value)));
						this.keys.InitAndSetArrayItem(this.connection.getInsertedId((XVar)(k.Value), (XVar)(table), (XVar)(oraSequenceName)), k.Value);
					}
				}
			}
			return null;
		}
		public virtual XVar setMessage(dynamic _param_message)
		{
			#region pass-by-value parameters
			dynamic message = XVar.Clone(_param_message);
			#endregion

			this.message = XVar.Clone(message);
			return null;
		}
		protected virtual XVar isMessageSet()
		{
			return 0 < MVCFunctions.strlen((XVar)(this.message));
		}
		public virtual XVar setDatabaseError(dynamic _param_message)
		{
			#region pass-by-value parameters
			dynamic message = XVar.Clone(_param_message);
			#endregion

			if(this.mode != Constants.ADD_INLINE)
			{
				this.message = XVar.Clone(MVCFunctions.Concat("<strong>&lt;&lt;&lt; ", "Record was NOT added", "</strong> &gt;&gt;&gt;<br><br>", message));
			}
			else
			{
				this.message = XVar.Clone(MVCFunctions.Concat("Record was NOT added", ". ", message));
			}
			this.messageType = new XVar(Constants.MESSAGE_ERROR);
			return null;
		}
		public virtual XVar getNewRecordData()
		{
			return this.newRecordData;
		}
		public virtual XVar getBlobFields()
		{
			return this.newRecordBlobFields;
		}
		protected override XVar checkFieldOnPage(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if(this.mode == Constants.ADD_INLINE)
			{
				return this.pSet.appearOnInlineAdd((XVar)(fName));
			}
			return this.pSet.appearOnAddPage((XVar)(fName));
		}
		public static XVar processAddPageSecurity(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic pageMode = null;
			if(XVar.Pack(Security.checkPagePermissions((XVar)(table), new XVar("A"))))
			{
				return true;
			}
			if(MVCFunctions.postvalue(new XVar("a")) == "added")
			{
				return true;
			}
			pageMode = XVar.Clone(readAddModeFromRequest());
			if(pageMode != Constants.ADD_SIMPLE)
			{
				Security.sendPermissionError();
				return false;
			}
			if((XVar)(CommonFunctions.isLogged())  && (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())))
			{
				Security.redirectToList((XVar)(table));
				return false;
			}
			CommonFunctions.redirectToLogin();
			return false;
		}
		public static XVar readAddModeFromRequest()
		{
			dynamic editType = null;
			editType = XVar.Clone(MVCFunctions.postvalue(new XVar("editType")));
			if(editType == "inline")
			{
				return Constants.ADD_INLINE;
			}
			else
			{
				if(editType == Constants.ADD_POPUP)
				{
					return Constants.ADD_POPUP;
				}
				else
				{
					if(editType == Constants.ADD_MASTER)
					{
						return Constants.ADD_MASTER;
					}
					else
					{
						if(editType == Constants.ADD_MASTER_POPUP)
						{
							return Constants.ADD_MASTER_POPUP;
						}
						else
						{
							if(editType == Constants.ADD_MASTER_DASH)
							{
								return Constants.ADD_MASTER_DASH;
							}
							else
							{
								if(editType == Constants.ADD_ONTHEFLY)
								{
									return Constants.ADD_ONTHEFLY;
								}
								else
								{
									if(MVCFunctions.postvalue(new XVar("mode")) == "dashrecord")
									{
										return Constants.ADD_DASHBOARD;
									}
									else
									{
										return Constants.ADD_SIMPLE;
									}
								}
							}
						}
					}
				}
			}
			return null;
		}
		public override XVar isMultistepped()
		{
			return this.pSet.isAddMultistep();
		}
		public override XVar editAvailable()
		{
			return (XVar)(!(XVar)(this.dashElementData))  && (XVar)(base.editAvailable());
		}
		public override XVar viewAvailable()
		{
			return (XVar)(!(XVar)(this.dashElementData))  && (XVar)(base.viewAvailable());
		}
		public override XVar getLayoutVersion()
		{
			dynamic layout = null;
			if(this.mode != Constants.ADD_INLINE)
			{
				return base.getLayoutVersion();
			}
			layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), new XVar(Constants.PAGE_LIST)));
			if(XVar.Pack(layout))
			{
				return layout.version;
			}
			return 2;
		}
		public virtual XVar setMessageType(dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			this.messageType = XVar.Clone(var_type);
			return null;
		}
	}
}
