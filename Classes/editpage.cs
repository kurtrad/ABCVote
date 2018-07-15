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
	public partial class EditPage : RunnerPage
	{
		protected dynamic cachedRecord = XVar.Pack(null);
		public dynamic oldKeys = XVar.Array();
		protected dynamic keysChanged = XVar.Pack(false);
		public dynamic jsKeys = XVar.Array();
		public dynamic keyFields = XVar.Array();
		public dynamic editFields = XVar.Array();
		public dynamic readEditValues = XVar.Pack(false);
		protected dynamic controlsDisabled = XVar.Pack(false);
		public dynamic action = XVar.Pack("");
		public dynamic lockingAction = XVar.Pack("");
		public dynamic lockingSid = XVar.Pack(null);
		public dynamic lockingKeys = XVar.Pack(null);
		public dynamic lockingStart = XVar.Pack(null);
		protected dynamic lockingMessageStyle = XVar.Pack("display:none;");
		protected dynamic lockingMessageText = XVar.Pack("");
		public dynamic messageType = XVar.Pack(Constants.MESSAGE_ERROR);
		protected dynamic auditObj = XVar.Pack(null);
		protected dynamic oldRecordData = XVar.Pack(null);
		protected dynamic newRecordData = XVar.Array();
		protected dynamic newRecordBlobFields = XVar.Array();
		protected dynamic updatedSuccessfully = XVar.Pack(false);
		public dynamic screenWidth = XVar.Pack(0);
		public dynamic screenHeight = XVar.Pack(0);
		public dynamic orientation = XVar.Pack("");
		protected dynamic afterEditAction = XVar.Pack(null);
		protected dynamic prevKeys = XVar.Pack(null);
		protected dynamic nextKeys = XVar.Pack(null);
		protected dynamic recordValuesToEdit = XVar.Pack(null);
		protected static bool skipEditPageCtor = false;
		public EditPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipEditPageCtor)
			{
				skipEditPageCtor = false;
				return;
			}
			setKeysForJs();
			this.auditObj = XVar.Clone(CommonFunctions.GetAuditObject((XVar)(this.tName)));
			this.editFields = XVar.Clone(getPageFields());
			this.formBricks.InitAndSetArrayItem("editheader", "header");
			this.formBricks.InitAndSetArrayItem(new XVar(0, "editbuttons", 1, "righteditbuttons"), "footer");
			assignFormFooterAndHeaderBricks(new XVar(true));
			addPageSettings();
		}
		protected virtual XVar addPageSettings()
		{
			dynamic afterEditAction = null;
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_recordUpdated")]))
			{
				setProxyValue((XVar)(MVCFunctions.Concat(this.shortTableName, "_recordUpdated")), new XVar(true));
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_recordUpdated"));
			}
			else
			{
				setProxyValue((XVar)(MVCFunctions.Concat(this.shortTableName, "_recordUpdated")), new XVar(false));
			}
			if((XVar)(!(XVar)(isPopupMode()))  && (XVar)(!(XVar)(isSimpleMode())))
			{
				return null;
			}
			afterEditAction = XVar.Clone(getAfterEditAction());
			this.jsSettings.InitAndSetArrayItem(afterEditAction, "tableSettings", this.tName, "afterEditAction");
			if(afterEditAction == Constants.AE_TO_DETAIL_LIST)
			{
				this.jsSettings.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(this.pSet.getAEDetailTable())), "tableSettings", this.tName, "afterEditActionDetTable");
			}
			if(this.mode == Constants.EDIT_POPUP)
			{
				if(afterEditAction == Constants.AE_TO_NEXT_EDIT)
				{
					this.jsSettings.InitAndSetArrayItem(getNextKeys(), "tableSettings", this.tName, "nextKeys");
				}
				if(afterEditAction == Constants.AE_TO_PREV_EDIT)
				{
					this.jsSettings.InitAndSetArrayItem(getPrevKeys(), "tableSettings", this.tName, "prevKeys");
				}
			}
			return null;
		}
		protected virtual XVar getAfterEditAction()
		{
			dynamic action = null;
			if((XVar)(true)  && (XVar)(!(XVar)(this.afterEditAction == null)))
			{
				return this.afterEditAction;
			}
			action = XVar.Clone(this.pSet.getAfterEditAction());
			if((XVar)((XVar)((XVar)((XVar)(isPopupMode())  && (XVar)(this.pSet.checkClosePopupAfterEdit()))  || (XVar)((XVar)(action == Constants.AE_TO_VIEW)  && (XVar)(!(XVar)(viewAvailable()))))  || (XVar)((XVar)(action == Constants.AE_TO_NEXT_EDIT)  && (XVar)(!(XVar)(MVCFunctions.count(getNextKeys())))))  || (XVar)((XVar)(action == Constants.AE_TO_PREV_EDIT)  && (XVar)(!(XVar)(MVCFunctions.count(getPrevKeys())))))
			{
				action = new XVar(Constants.AE_TO_LIST);
			}
			if(action == Constants.AE_TO_DETAIL_LIST)
			{
				dynamic dPermissions = XVar.Array(), dPset = null, dTName = null;
				dTName = XVar.Clone(this.pSet.getAEDetailTable());
				dPset = XVar.Clone(new ProjectSettings((XVar)(dTName)));
				dPermissions = XVar.Clone(getPermissions((XVar)(dTName)));
				if((XVar)(!(XVar)(dTName))  || (XVar)((XVar)(action == Constants.AE_TO_DETAIL_LIST)  && (XVar)((XVar)(!(XVar)(dPset.hasListPage()))  || (XVar)(!(XVar)(dPermissions["search"])))))
				{
					action = new XVar(Constants.AE_TO_LIST);
				}
			}
			this.afterEditAction = XVar.Clone(action);
			return this.afterEditAction;
		}
		protected override XVar assignSessionPrefix()
		{
			if((XVar)(this.mode == Constants.EDIT_DASHBOARD)  || (XVar)((XVar)((XVar)(isPopupMode())  || (XVar)(this.mode == Constants.EDIT_INLINE))  && (XVar)(this.dashTName)))
			{
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
				return null;
			}
			base.assignSessionPrefix();
			return null;
		}
		public override XVar setSessionVariables()
		{
			base.setSessionVariables();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
			return null;
		}
		protected override XVar getPageFields()
		{
			if(this.mode == Constants.EDIT_INLINE)
			{
				return this.pSet.getInlineEditFields();
			}
			return this.pSet.getEditFields();
		}
		public virtual XVar setKeys(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			this.cachedRecord = new XVar(null);
			this.recordValuesToEdit = new XVar(null);
			this.keys = XVar.Clone(keys);
			setKeysForJs();
			return null;
		}
		public virtual XVar setKeysForJs()
		{
			dynamic i = null;
			i = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> value in this.keys.GetEnumerator())
			{
				this.jsKeys.InitAndSetArrayItem(value.Value, i++);
			}
			return null;
		}
		public virtual XVar isLockingRequest()
		{
			return (XVar)(this.lockingObj)  && (XVar)(this.lockingAction != "");
		}
		public virtual XVar doLockingAction()
		{
			dynamic arrkeys = XVar.Array();
			arrkeys = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(MVCFunctions.urldecode((XVar)(this.lockingKeys)))));
			foreach (KeyValuePair<XVar, dynamic> ind in MVCFunctions.array_keys((XVar)(arrkeys)).GetEnumerator())
			{
				arrkeys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(arrkeys[ind.Value])), ind.Value);
			}
			if(this.lockingAction == "unlock")
			{
				this.lockingObj.UnlockRecord((XVar)(this.tName), (XVar)(arrkeys), (XVar)(this.lockingSid));
			}
			else
			{
				if((XVar)(this.lockingAction == "lockadmin")  && (XVar)((XVar)(CommonFunctions.IsAdmin())  || (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP)))
				{
					this.lockingObj.UnlockAdmin((XVar)(this.tName), (XVar)(arrkeys), (XVar)(this.lockingStart == "yes"));
					if(this.lockingStart == "no")
					{
						MVCFunctions.Echo("unlock");
					}
					else
					{
						if(this.lockingStart == "yes")
						{
							MVCFunctions.Echo("lock");
						}
					}
				}
				else
				{
					if(this.lockingAction == "confirm")
					{
						dynamic lockMessage = null;
						lockMessage = new XVar("");
						if(XVar.Pack(!(XVar)(this.lockingObj.ConfirmLock((XVar)(this.tName), (XVar)(arrkeys), ref lockMessage))))
						{
							MVCFunctions.Echo(lockMessage);
						}
					}
				}
			}
			return null;
		}
		public override XVar setTemplateFile()
		{
			if(this.mode == Constants.EDIT_INLINE)
			{
				this.templatefile = XVar.Clone(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), new XVar("inline_edit")));
			}
			base.setTemplateFile();
			return null;
		}
		public override XVar init()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessEdit"))))
			{
				this.eventsObject.BeforeProcessEdit(this);
			}
			base.init();
			return null;
		}
		public virtual XVar process()
		{
			if(this.action == "edited")
			{
				processDataInput();
				this.readEditValues = XVar.Clone(!(XVar)(this.updatedSuccessfully));
				if((XVar)(this.mode == Constants.EDIT_INLINE)  || (XVar)(isPopupMode()))
				{
					reportInlineSaveStatus();
					return null;
				}
				if(XVar.Pack(this.updatedSuccessfully))
				{
					if(XVar.Pack(afterEditActionRedirect()))
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
			if(XVar.Pack(!(XVar)(readRecord())))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(isRecordEditable(new XVar(false)))))
			{
				return SecurityRedirect();
			}
			if(XVar.Pack(!(XVar)(lockRecord())))
			{
				return null;
			}
			prepareReadonlyFields();
			doCommonAssignments();
			prepareButtons();
			prepareSteps();
			prepareEditControls();
			fillCntrlTabGroups();
			prepareJsSettings();
			prepareDetailsTables();
			if(this.mode != Constants.EDIT_INLINE)
			{
				addButtonHandlers();
			}
			addCommonJs();
			fillSetCntrlMaps();
			displayEditPage();
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
		protected virtual XVar prepareJsSettings()
		{
			this.jsSettings.InitAndSetArrayItem(this.jsKeys, "tableSettings", this.tName, "keys");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getTableKeys(), "tableSettings", this.tName, "keyFields");
			this.jsSettings.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(getCurrentRecordInternal())), "tableSettings", this.tName, "masterKeys");
			if(XVar.Pack(this.lockingObj))
			{
				this.jsSettings.InitAndSetArrayItem(MVCFunctions.implode(new XVar("&"), (XVar)(this.keys)), "tableSettings", this.tName, "sKeys");
				this.jsSettings.InitAndSetArrayItem(!(XVar)(this.controlsDisabled), "tableSettings", this.tName, "enableCtrls");
				this.jsSettings.InitAndSetArrayItem(this.lockingObj.ConfirmTime, "tableSettings", this.tName, "confirmTime");
			}
			return null;
		}
		protected virtual XVar doCommonAssignments()
		{
			dynamic data = XVar.Array();
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				if(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.EDIT_SIMPLE)))
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
			data = XVar.Clone(getCurrentRecordInternal());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				dynamic viewFormat = null;
				viewFormat = XVar.Clone(this.pSet.getViewFormat((XVar)(k.Value)));
				if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(viewFormat == Constants.FORMAT_HTML)  || (XVar)(viewFormat == Constants.FORMAT_FILE_IMAGE))  || (XVar)(viewFormat == Constants.FORMAT_FILE))  || (XVar)(viewFormat == Constants.FORMAT_HYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_HYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_EMAILHYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_CHECKBOX))
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("show_key", k.Key + 1)), (XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(data[k.Value]))));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("show_key", k.Key + 1)), (XVar)(showDBValue((XVar)(k.Value), (XVar)(data))));
				}
			}
			assignEditFieldsBlocksAndLabels();
			if(XVar.Pack(isSimpleMode()))
			{
				assignBody();
				this.xt.assign(new XVar("flybody"), new XVar(true));
			}
			return null;
		}
		protected virtual XVar displayEditPage()
		{
			dynamic templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowEdit"))))
			{
				this.eventsObject.BeforeShowEdit((XVar)(this.xt), ref templatefile, (XVar)(getCurrentRecordInternal()), this);
			}
			displayMasterTableInfo();
			if(XVar.Pack(isSimpleMode()))
			{
				display((XVar)(templatefile));
				return null;
			}
			if((XVar)(isPopupMode())  || (XVar)(this.mode == Constants.EDIT_DASHBOARD))
			{
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("body"), (XVar)(this.body));
				displayAJAX((XVar)(templatefile), (XVar)(this.flyId + 1));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			if(this.mode == Constants.EDIT_INLINE)
			{
				dynamic returnJSON = XVar.Array();
				returnJSON = XVar.Clone(XVar.Array());
				this.xt.load_template((XVar)(templatefile));
				returnJSON.InitAndSetArrayItem(XVar.Array(), "html");
				foreach (KeyValuePair<XVar, dynamic> f in this.editFields.GetEnumerator())
				{
					returnJSON.InitAndSetArrayItem(this.xt.fetchVar((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(f.Value)), "_editcontrol"))), "html", f.Value);
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
		protected virtual XVar prepareDetailsTables()
		{
			dynamic d = null, dpParams = XVar.Array(), dpTablesParams = XVar.Array();
			if((XVar)(!(XVar)(this.isShowDetailTables))  || (XVar)(this.mode == Constants.EDIT_INLINE))
			{
				return null;
			}
			dpParams = XVar.Clone(getDetailsParams((XVar)(this.id)));
			this.jsSettings.InitAndSetArrayItem(new XVar("tableNames", dpParams["strTableNames"], "ids", dpParams["ids"]), "tableSettings", this.tName, "dpParams");
			if(XVar.Pack(!(XVar)(MVCFunctions.count(dpParams["ids"]))))
			{
				return null;
			}
			if(this.mode == Constants.EDIT_DASHBOARD)
			{
				dpTablesParams = XVar.Clone(XVar.Array());
			}
			this.xt.assign(new XVar("detail_tables"), new XVar(true));
			this.flyId = XVar.Clone(dpParams["ids"][MVCFunctions.count(dpParams["ids"]) - 1] + 1);
			d = new XVar(0);
			for(;d < MVCFunctions.count(dpParams["ids"]); d++)
			{
				if(this.mode != Constants.EDIT_DASHBOARD)
				{
					setDetailPreview((XVar)(dpParams["type"][d]), (XVar)(dpParams["strTableNames"][d]), (XVar)(dpParams["ids"][d]), (XVar)(getCurrentRecordInternal()));
					displayDetailsButtons((XVar)(dpParams["type"][d]), (XVar)(dpParams["strTableNames"][d]), (XVar)(dpParams["ids"][d]));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("details_", dpParams["shorTNames"][d])), new XVar(true));
					dpTablesParams.InitAndSetArrayItem(new XVar("tName", dpParams["strTableNames"][d], "id", dpParams["ids"][d], "pType", dpParams["type"][d]), null);
					this.xt.assign((XVar)(MVCFunctions.Concat("displayDetailTable_", MVCFunctions.GoodFieldName((XVar)(dpParams["strTableNames"][d])))), (XVar)(MVCFunctions.Concat("<div id='dp_", MVCFunctions.GoodFieldName((XVar)(this.tName)), "_", this.pageType, "_", dpParams["ids"][d], "'></div>")));
				}
			}
			if(this.mode == Constants.EDIT_DASHBOARD)
			{
				this.controlsMap.InitAndSetArrayItem(dpTablesParams, "dpTablesParams");
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
			if((XVar)(dpType == Constants.PAGE_CHART)  || (XVar)(dpType == Constants.PAGE_REPORT))
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
		protected virtual XVar prepareButtons()
		{
			dynamic addStyle = null;
			if(this.mode == Constants.EDIT_INLINE)
			{
				return null;
			}
			prepareNextPrevButtons();
			if(XVar.Pack(isSimpleMode()))
			{
				if(XVar.Pack(this.pSet.hasListPage()))
				{
					this.xt.assign(new XVar("back_button"), new XVar(true));
					this.xt.assign(new XVar("backbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"backButton", this.id, "\"")));
					this.xt.assign(new XVar("mbackbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"extraBackButton", this.id, "\"")));
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
			if(XVar.Pack(isPopupMode()))
			{
				this.xt.assign(new XVar("close_button"), new XVar(true));
				this.xt.assign(new XVar("closebutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"closeButton", this.id, "\"")));
			}
			this.xt.assign(new XVar("save_button"), new XVar(true));
			addStyle = new XVar("");
			if(XVar.Pack(isMultistepped()))
			{
				addStyle = new XVar(" style=\"display: none;\"");
			}
			if(XVar.Pack(this.controlsDisabled))
			{
				this.xt.assign(new XVar("savebutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"saveButton", this.id, "\" type=\"disabled\"", addStyle)));
			}
			else
			{
				this.xt.assign(new XVar("savebutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"saveButton", this.id, "\"", addStyle)));
			}
			this.xt.assign(new XVar("resetbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"resetButton", this.id, "\"")));
			this.xt.assign(new XVar("reset_button"), new XVar(true));
			if(this.mode == Constants.EDIT_DASHBOARD)
			{
				return null;
			}
			if(XVar.Pack(viewAvailable()))
			{
				this.xt.assign(new XVar("view_page_button"), new XVar(true));
				this.xt.assign(new XVar("view_page_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"viewPageButton", this.id, "\"")));
			}
			return null;
		}
		protected virtual XVar prepareNextPrevButtons()
		{
			dynamic nextPrev = XVar.Array();
			if(XVar.Pack(!(XVar)(this.pSet.useMoveNext())))
			{
				return null;
			}
			nextPrev = XVar.Clone(getNextPrevRecordKeys((XVar)(getCurrentRecordInternal())));
			assignPrevNextButtons((XVar)(0 < MVCFunctions.count(nextPrev["next"])), (XVar)(0 < MVCFunctions.count(nextPrev["prev"])), (XVar)((XVar)(this.mode == Constants.EDIT_DASHBOARD)  && (XVar)((XVar)(hasTableDashGridElement())  || (XVar)(hasDashMapElement()))));
			this.jsSettings.InitAndSetArrayItem(nextPrev["prev"], "tableSettings", this.tName, "prevKeys");
			this.jsSettings.InitAndSetArrayItem(nextPrev["next"], "tableSettings", this.tName, "nextKeys");
			return null;
		}
		protected virtual XVar readRecord()
		{
			if(XVar.Pack(getCurrentRecordInternal()))
			{
				return true;
			}
			if(XVar.Pack(isSimpleMode()))
			{
				MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), new XVar("list"), new XVar("a=return"));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return false;
		}
		protected virtual XVar prepareReadonlyFields()
		{
			dynamic data = null, fields = XVar.Array(), keyParams = XVar.Array(), keylink = null;
			fields = XVar.Clone(this.pSet.getFieldsList());
			data = XVar.Clone(getCurrentRecordInternal());
			keyParams = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("key", k.Key + 1, "=", MVCFunctions.RawUrlDecode((XVar)(this.keys[k.Value]))), null);
			}
			keylink = XVar.Clone(MVCFunctions.Concat("&", MVCFunctions.implode(new XVar("&"), (XVar)(keyParams))));
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				if((XVar)(this.pSet.getEditFormat((XVar)(f.Value)) == Constants.EDIT_FORMAT_READONLY)  && (XVar)((XVar)(this.pSet.appearOnEditPage((XVar)(f.Value)))  || (XVar)(this.pSet.appearOnInlineEdit((XVar)(f.Value)))))
				{
					this.readOnlyFields.InitAndSetArrayItem(showDBValue((XVar)(f.Value), (XVar)(data), (XVar)(keylink)), f.Value);
				}
			}
			return null;
		}
		protected virtual XVar lockRecord()
		{
			if(XVar.Pack(!(XVar)(this.lockingObj)))
			{
				return true;
			}
			if(XVar.Pack(this.lockingObj.LockRecord((XVar)(this.tName), (XVar)(this.keys))))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div class=\"rnr-locking\" style=\"", this.lockingMessageStyle, "\">", this.lockingMessageText, "</div>");
				return true;
			}
			if(this.mode == Constants.EDIT_INLINE)
			{
				dynamic lockmessage = null, returnJSON = XVar.Array();
				if((XVar)(CommonFunctions.IsAdmin())  || (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP))
				{
					lockmessage = XVar.Clone(this.lockingObj.GetLockInfo((XVar)(this.tName), (XVar)(this.keys), new XVar(false), (XVar)(this.id)));
				}
				else
				{
					lockmessage = XVar.Clone(this.lockingObj.LockUser);
				}
				returnJSON = XVar.Clone(XVar.Array());
				returnJSON.InitAndSetArrayItem(false, "success");
				returnJSON.InitAndSetArrayItem(lockmessage, "message");
				returnJSON.InitAndSetArrayItem(false, "enableCtrls");
				returnJSON.InitAndSetArrayItem(this.lockingObj.ConfirmTime, "confirmTime");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			this.controlsDisabled = new XVar(true);
			this.lockingMessageStyle = new XVar("style='display:block;'");
			this.lockingMessageText = XVar.Clone(this.lockingObj.LockUser);
			if((XVar)(CommonFunctions.IsAdmin())  || (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP))
			{
				dynamic ribbonMessage = null;
				ribbonMessage = XVar.Clone(this.lockingObj.GetLockInfo((XVar)(this.tName), (XVar)(this.keys), new XVar(true), (XVar)(this.id)));
				if(ribbonMessage != XVar.Pack(""))
				{
					this.lockingMessageText = XVar.Clone(ribbonMessage);
				}
			}
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div class=\"rnr-locking\" style=\"", this.lockingMessageStyle, "\">", this.lockingMessageText, "</div>");
			return true;
		}
		protected virtual XVar reportInlineSaveStatus()
		{
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(getSaveStatusJSON())));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar getSaveStatusJSON()
		{
			dynamic data = XVar.Array(), dmapIconsData = null, fields = XVar.Array(), fieldsIconsData = null, i = null, keyParams = XVar.Array(), keylink = null, rawValues = XVar.Array(), returnJSON = XVar.Array(), values = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			if((XVar)(this.action != "edited")  || (XVar)(isSimpleMode()))
			{
				return returnJSON;
			}
			returnJSON.InitAndSetArrayItem(this.updatedSuccessfully, "success");
			returnJSON.InitAndSetArrayItem(this.message, "message");
			returnJSON.InitAndSetArrayItem(this.lockingMessageText, "lockMessage");
			if(XVar.Pack(!(XVar)(this.isCaptchaOk)))
			{
				returnJSON.InitAndSetArrayItem(getCaptchaFieldName(), "wrongCaptchaFieldName");
			}
			if(XVar.Pack(!(XVar)(this.updatedSuccessfully)))
			{
				return returnJSON;
			}
			data = XVar.Clone(getCurrentRecordInternal());
			if(XVar.Pack(!(XVar)(data)))
			{
				data = XVar.Clone(this.newRecordData);
			}
			returnJSON.InitAndSetArrayItem(XVar.Array(), "detKeys");
			foreach (KeyValuePair<XVar, dynamic> dt in this.pSet.getDetailTablesArr().GetEnumerator())
			{
				dynamic dkeys = XVar.Array();
				dkeys = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> mk in dt.Value["masterKeys"].GetEnumerator())
				{
					dkeys.InitAndSetArrayItem(data[mk.Value], MVCFunctions.Concat("masterkey", mk.Key + 1));
				}
				returnJSON.InitAndSetArrayItem(dkeys, "detKeys", dt.Value["dDataSourceTable"]);
			}
			keyParams = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("key", k.Key + 1, "=", MVCFunctions.RawUrlDecode((XVar)(this.keys[k.Value]))), null);
			}
			keylink = XVar.Clone(MVCFunctions.Concat("&", MVCFunctions.implode(new XVar("&"), (XVar)(keyParams))));
			values = XVar.Clone(XVar.Array());
			rawValues = XVar.Clone(XVar.Array());
			fields = XVar.Clone(this.pSet.getFieldsList());
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				dynamic value = null;
				value = XVar.Clone(showDBValue((XVar)(f.Value), (XVar)(data), (XVar)(keylink)));
				values.InitAndSetArrayItem(value, f.Value);
				if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(this.pSet.getFieldType((XVar)(f.Value))))))
				{
					rawValues.InitAndSetArrayItem("", f.Value);
				}
				else
				{
					rawValues.InitAndSetArrayItem(MVCFunctions.runner_substr((XVar)(data[f.Value]), new XVar(0), new XVar(100)), f.Value);
				}
			}
			returnJSON.InitAndSetArrayItem(this.jsKeys, "keys");
			returnJSON.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(data)), "masterKeys");
			returnJSON.InitAndSetArrayItem(this.pSet.getTableKeys(), "keyFields");
			returnJSON.InitAndSetArrayItem(XVar.Array(), "oldKeys");
			i = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> value in this.oldKeys.GetEnumerator())
			{
				returnJSON.InitAndSetArrayItem(value.Value, "oldKeys", i++);
			}
			returnJSON.InitAndSetArrayItem(values, "vals");
			returnJSON.InitAndSetArrayItem(fields, "fields");
			returnJSON.InitAndSetArrayItem(rawValues, "rawVals");
			returnJSON.InitAndSetArrayItem(buildDetailGridLinks((XVar)(returnJSON["detKeys"])), "hrefs");
			if(XVar.Pack(!(XVar)(isRecordEditable(new XVar(false)))))
			{
				returnJSON.InitAndSetArrayItem(true, "nonEditable");
			}
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
			return returnJSON;
		}
		protected virtual XVar afterEditActionRedirect()
		{
			dynamic dTName = null, nextKeys = null, prevKeys = null;
			if(XVar.Pack(!(XVar)(isSimpleMode())))
			{
				return false;
			}
			switch(((XVar)getAfterEditAction()).ToInt())
			{
				case Constants.AE_TO_EDIT:
					return prgRedirect();
				case Constants.AE_TO_LIST:
					if(XVar.Pack(this.pSet.hasListPage()))
					{
						MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_LIST), new XVar("a=return"));
					}
					else
					{
						MVCFunctions.HeaderRedirect(new XVar("menu"));
					}
					return true;
				case Constants.AE_TO_VIEW:
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_VIEW), (XVar)(getKeyParams()));
					return true;
				case Constants.AE_TO_PREV_EDIT:
					XSession.Session["message_edit"] = MVCFunctions.Concat(this.message, "");
					prevKeys = XVar.Clone(getPrevKeys());
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_EDIT), (XVar)(getKeyParams((XVar)(prevKeys))));
					return true;
				case Constants.AE_TO_NEXT_EDIT:
					XSession.Session["message_edit"] = MVCFunctions.Concat(this.message, "");
					nextKeys = XVar.Clone(getNextKeys());
					MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), new XVar(Constants.PAGE_EDIT), (XVar)(getKeyParams((XVar)(nextKeys))));
					return true;
				case Constants.AE_TO_DETAIL_LIST:
					dTName = XVar.Clone(this.pSet.getAEDetailTable());
					MVCFunctions.HeaderRedirect((XVar)(CommonFunctions.GetTableURL((XVar)(dTName))), new XVar(Constants.PAGE_LIST), (XVar)(MVCFunctions.Concat(MVCFunctions.implode(new XVar("&"), (XVar)(getNewRecordMasterKeys((XVar)(dTName)))), "&mastertable=", this.tName)));
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
			data = XVar.Clone(getCurrentRecordInternal());
			mKeys = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> mk in this.pSet.getMasterKeysByDetailTable((XVar)(dTName)).GetEnumerator())
			{
				mKeys.InitAndSetArrayItem(MVCFunctions.Concat("masterkey", mk.Key + 1, "=", data[mk.Value]), null);
			}
			return mKeys;
		}
		protected virtual XVar getPrevKeys()
		{
			dynamic keys = XVar.Array();
			if((XVar)(true)  && (XVar)(!(XVar)(this.prevKeys == null)))
			{
				return this.prevKeys;
			}
			keys = XVar.Clone(getNextPrevRecordKeys((XVar)(getCurrentRecordInternal()), new XVar(Constants.PREV_RECORD)));
			this.prevKeys = XVar.Clone(keys["prev"]);
			return this.prevKeys;
		}
		protected virtual XVar getNextKeys()
		{
			dynamic keys = XVar.Array();
			if((XVar)(true)  && (XVar)(!(XVar)(this.nextKeys == null)))
			{
				return this.nextKeys;
			}
			keys = XVar.Clone(getNextPrevRecordKeys((XVar)(getCurrentRecordInternal()), new XVar(Constants.NEXT_RECORD)));
			this.nextKeys = XVar.Clone(keys["next"]);
			return this.nextKeys;
		}
		protected virtual XVar prgRedirect()
		{
			if(XVar.Pack(this.stopPRG))
			{
				return false;
			}
			if((XVar)((XVar)(!(XVar)(this.updatedSuccessfully))  || (XVar)(!(XVar)(isSimpleMode())))  || (XVar)(!(XVar)(MVCFunctions.no_output_done())))
			{
				return false;
			}
			XSession.Session["message_edit"] = MVCFunctions.Concat(this.message, "");
			XSession.Session["message_edit_type"] = this.messageType;
			MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), (XVar)(getPageType()), (XVar)(getKeyParams()));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return true;
		}
		protected virtual XVar prgReadMessage()
		{
			if((XVar)(!(XVar)(isSimpleMode()))  || (XVar)(!(XVar)(XSession.Session.KeyExists("message_edit"))))
			{
				return null;
			}
			setMessage((XVar)(XSession.Session["message_edit"]));
			this.messageType = XVar.Clone(XSession.Session["message_edit_type"]);
			XSession.Session.Remove("message_edit");
			return null;
		}
		public override XVar getCurrentRecord()
		{
			dynamic data = XVar.Array(), newData = XVar.Array();
			data = XVar.Clone(getCurrentRecordInternal());
			newData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> val in data.GetEnumerator())
			{
				dynamic editFormat = null;
				editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(val.Key)));
				if((XVar)(editFormat == Constants.EDIT_FORMAT_DATABASE_FILE)  || (XVar)(editFormat == Constants.EDIT_FORMAT_DATABASE_IMAGE))
				{
					if(XVar.Pack(data[val.Key]))
					{
						newData.InitAndSetArrayItem(true, val.Key);
					}
					else
					{
						newData.InitAndSetArrayItem(false, val.Key);
					}
				}
			}
			foreach (KeyValuePair<XVar, dynamic> val in newData.GetEnumerator())
			{
				data.InitAndSetArrayItem(val.Value, val.Key);
			}
			return data;
		}
		public virtual XVar getKeysWhereClause(dynamic _param_useOldKeys)
		{
			#region pass-by-value parameters
			dynamic useOldKeys = XVar.Clone(_param_useOldKeys);
			#endregion

			dynamic strWhereClause = null;
			strWhereClause = new XVar("");
			if(XVar.Pack(useOldKeys))
			{
				strWhereClause = XVar.Clone(CommonFunctions.KeyWhere((XVar)(this.oldKeys)));
			}
			else
			{
				strWhereClause = XVar.Clone(CommonFunctions.KeyWhere((XVar)(this.keys)));
			}
			if(this.pSet.getAdvancedSecurityType() != Constants.ADVSECURITY_ALL)
			{
				strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(CommonFunctions.SecuritySQL(new XVar("Edit"), (XVar)(this.tName)))));
			}
			return strWhereClause;
		}
		public virtual XVar getCurrentRecordInternal()
		{
			dynamic fetchedArray = null, keysSet = null, orderClause = null, sql = XVar.Array();
			if(XVar.Pack(!(XVar)(this.cachedRecord == null)))
			{
				return this.cachedRecord;
			}
			sql = XVar.Clone(getSubsetSQLComponents());
			orderClause = XVar.Clone(getOrderByClause());
			keysSet = XVar.Clone(checkKeysSet());
			if(XVar.Pack(keysSet))
			{
				sql.InitAndSetArrayItem(XVar.Array(), "optionalWhere");
				sql.InitAndSetArrayItem(XVar.Array(), "optionalHaving");
				sql.InitAndSetArrayItem(XVar.Array(), "mandatoryHaving");
				sql.InitAndSetArrayItem(XVar.Array(), "mandatoryWhere");
				sql.InitAndSetArrayItem(getKeysWhereClause(new XVar(false)), "mandatoryWhere", null);
				sql.InitAndSetArrayItem(SecuritySQL(new XVar("Edit"), (XVar)(this.tName)), "mandatoryWhere", null);
			}
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			if(XVar.Pack(!(XVar)(keysSet)))
			{
				GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryEdit"))))
			{
				dynamic strSQLbak = null, strWhereClause = null, strWhereClauseBak = null;
				strWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")));
				strSQLbak = XVar.Clone(GlobalVars.strSQL);
				strWhereClauseBak = XVar.Clone(strWhereClause);
				this.eventsObject.BeforeQueryEdit((XVar)(GlobalVars.strSQL), ref strWhereClause, this);
				if((XVar)(strSQLbak == GlobalVars.strSQL)  && (XVar)(strWhereClauseBak != strWhereClause))
				{
					GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, strWhereClause))));
					if(XVar.Pack(!(XVar)(keysSet)))
					{
						GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
					}
				}
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			fetchedArray = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
			this.cachedRecord = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
			if(XVar.Pack(!(XVar)(keysSet)))
			{
				this.keys = XVar.Clone(getKeysFromData((XVar)(this.cachedRecord)));
				setKeysForJs();
			}
			if((XVar)(!(XVar)(this.cachedRecord))  && (XVar)(this.mode == Constants.EDIT_SIMPLE))
			{
				return this.cachedRecord;
			}
			foreach (KeyValuePair<XVar, dynamic> fName in getPageFields().GetEnumerator())
			{
				if((XVar)(MVCFunctions.postvalue("a") != "edited")  && (XVar)(!XVar.Equals(XVar.Pack(this.pSet.getAutoUpdateValue((XVar)(fName.Value))), XVar.Pack(""))))
				{
					this.cachedRecord.InitAndSetArrayItem(this.pSet.getAutoUpdateValue((XVar)(fName.Value)), fName.Value);
				}
			}
			if(XVar.Pack(this.readEditValues))
			{
				foreach (KeyValuePair<XVar, dynamic> fName in getPageFields().GetEnumerator())
				{
					dynamic editFormat = null;
					editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(fName.Value)));
					if((XVar)((XVar)((XVar)(editFormat == Constants.EDIT_FORMAT_DATABASE_FILE)  && (XVar)(editFormat != Constants.EDIT_FORMAT_DATABASE_IMAGE))  && (XVar)(editFormat != Constants.EDIT_FORMAT_FILE))  && (XVar)(!(XVar)(this.pSet.isReadonly((XVar)(fName.Value)))))
					{
						this.cachedRecord.InitAndSetArrayItem(this.newRecordData[fName.Value], fName.Value);
					}
				}
			}
			return this.cachedRecord;
		}
		protected virtual XVar checkKeysSet()
		{
			foreach (KeyValuePair<XVar, dynamic> kValue in this.keys.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(kValue.Value))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar prepareEditControls()
		{
			dynamic control = null, data = XVar.Array();
			if(this.mode == Constants.EDIT_INLINE)
			{
				this.editFields = XVar.Clone(removeHiddenColumnsFromInlineFields((XVar)(this.editFields), (XVar)(this.screenWidth), (XVar)(this.screenHeight), (XVar)(this.orientation)));
			}
			data = XVar.Clone(getFieldControlsData());
			if(XVar.Pack(this.readEditValues))
			{
				foreach (KeyValuePair<XVar, dynamic> f in this.editFields.GetEnumerator())
				{
					dynamic editFormat = null;
					if(XVar.Pack(!(XVar)(this.newRecordData.KeyExists(f.Value))))
					{
						continue;
					}
					editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(f.Value)));
					if((XVar)((XVar)(editFormat != Constants.EDIT_FORMAT_DATABASE_FILE)  && (XVar)(editFormat != Constants.EDIT_FORMAT_DATABASE_IMAGE))  && (XVar)(editFormat != Constants.EDIT_FORMAT_READONLY))
					{
						data.InitAndSetArrayItem(this.newRecordData[f.Value], f.Value);
					}
				}
			}
			control = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fName in this.editFields.GetEnumerator())
			{
				dynamic controlMode = null, controls = XVar.Array(), gfName = null, isDetKeyField = null, parameters = XVar.Array(), preload = null;
				gfName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fName.Value)));
				isDetKeyField = XVar.Clone(MVCFunctions.in_array((XVar)(fName.Value), (XVar)(this.detailKeysByM)));
				controls = XVar.Clone(XVar.Array());
				controls.InitAndSetArrayItem(XVar.Array(), "controls");
				controls.InitAndSetArrayItem(0, "controls", "ctrlInd");
				controls.InitAndSetArrayItem(this.id, "controls", "id");
				controls.InitAndSetArrayItem(fName.Value, "controls", "fieldName");
				if(XVar.Pack(MVCFunctions.in_array((XVar)(fName.Value), (XVar)(this.errorFields))))
				{
					controls.InitAndSetArrayItem(true, "controls", "isInvalid");
				}
				parameters = XVar.Clone(XVar.Array());
				parameters.InitAndSetArrayItem(this.id, "id");
				parameters.InitAndSetArrayItem(Constants.PAGE_EDIT, "ptype");
				parameters.InitAndSetArrayItem(fName.Value, "field");
				parameters.InitAndSetArrayItem(this, "pageObj");
				parameters.InitAndSetArrayItem(data[fName.Value], "value");
				if(XVar.Pack(!(XVar)(isDetKeyField)))
				{
					dynamic additionalCtrlParams = XVar.Array();
					if((XVar)(CommonFunctions.IsFloatType((XVar)(this.pSet.getFieldType((XVar)(fName.Value)))))  && (XVar)(!(XVar)(data[fName.Value] == null)))
					{
						if(this.pSet.getHTML5InputType((XVar)(fName.Value)) == "number")
						{
							parameters.InitAndSetArrayItem(MVCFunctions.formatNumberForHTML5((XVar)(data[fName.Value])), "value");
						}
						else
						{
							parameters.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar("."), (XVar)(GlobalVars.locale_info["LOCALE_SDECIMAL"]), (XVar)(data[fName.Value])), "value");
						}
					}
					parameters.InitAndSetArrayItem(this.pSet.getValidation((XVar)(fName.Value)), "validate");
					additionalCtrlParams = XVar.Clone(XVar.Array());
					additionalCtrlParams.InitAndSetArrayItem(this.controlsDisabled, "disabled");
					parameters.InitAndSetArrayItem(additionalCtrlParams, "additionalCtrlParams");
				}
				controlMode = XVar.Clone((XVar.Pack(this.mode == Constants.EDIT_INLINE) ? XVar.Pack("inline_edit") : XVar.Pack("edit")));
				parameters.InitAndSetArrayItem(controlMode, "mode");
				controls.InitAndSetArrayItem(controlMode, "controls", "mode");
				if((XVar)(this.pSet.isUseRTE((XVar)(fName.Value)))  && (XVar)(this.pSet.isAutoUpdatable((XVar)(fName.Value))))
				{
					XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_", fName.Value, "_rte")] = MVCFunctions.GetAutoUpdateValue((XVar)(fName.Value), new XVar(Constants.PAGE_EDIT));
					parameters.InitAndSetArrayItem("add", "mode");
				}
				if(XVar.Pack(isDetKeyField))
				{
					controls.InitAndSetArrayItem(data[fName.Value], "controls", "value");
					parameters.InitAndSetArrayItem(XVar.Array(), "extraParams");
					parameters.InitAndSetArrayItem(true, "extraParams", "getDetKeyReadOnlyCtrl");
					this.readOnlyFields.InitAndSetArrayItem(showDBValue((XVar)(fName.Value), (XVar)(data)), fName.Value);
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
				preload = XVar.Clone(fillPreload((XVar)(fName.Value), (XVar)(this.editFields), (XVar)(data)));
				if(!XVar.Equals(XVar.Pack(preload), XVar.Pack(false)))
				{
					controls.InitAndSetArrayItem(preload, "controls", "preloadData");
				}
				fillControlsMap((XVar)(controls));
				fillFieldToolTips((XVar)(fName.Value));
				fillControlFlags((XVar)(fName.Value));
				if(this.pSet.getEditFormat((XVar)(fName.Value)) == "Time")
				{
					fillTimePickSettings((XVar)(fName.Value), (XVar)(data[fName.Value]));
				}
				if(this.pSet.getViewFormat((XVar)(fName.Value)) == Constants.FORMAT_MAP)
				{
					this.googleMapCfg.InitAndSetArrayItem(true, "isUseGoogleMap");
				}
			}
			return null;
		}
		public virtual XVar assignEditFieldsBlocksAndLabels()
		{
			dynamic editFields = XVar.Array();
			editFields = XVar.Clone(getPageFields());
			foreach (KeyValuePair<XVar, dynamic> fName in editFields.GetEnumerator())
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
		public static XVar readEditModeFromRequest()
		{
			if(MVCFunctions.postvalue(new XVar("editType")) == "inline")
			{
				return Constants.EDIT_INLINE;
			}
			else
			{
				if(MVCFunctions.postvalue(new XVar("editType")) == Constants.EDIT_POPUP)
				{
					return Constants.EDIT_POPUP;
				}
				else
				{
					if(MVCFunctions.postvalue(new XVar("mode")) == "dashrecord")
					{
						return Constants.EDIT_DASHBOARD;
					}
					else
					{
						return Constants.EDIT_SIMPLE;
					}
				}
			}
			return null;
		}
		public static XVar processEditPageSecurity(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic i = null, keyParams = XVar.Array(), pageMode = null;
			if(XVar.Pack(Security.checkPagePermissions((XVar)(table), new XVar("E"))))
			{
				return true;
			}
			if(MVCFunctions.postvalue(new XVar("a")) == "edited")
			{
				return true;
			}
			pageMode = XVar.Clone(readEditModeFromRequest());
			if(pageMode != Constants.EDIT_SIMPLE)
			{
				dynamic messageLink = null;
				messageLink = new XVar("");
				if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(CommonFunctions.isLoggedAsGuest()))
				{
					messageLink = XVar.Clone(MVCFunctions.Concat(" <a href='#' id='loginButtonContinue'>", "Login", "</a>"));
				}
				Security.sendPermissionError((XVar)(messageLink));
				return false;
			}
			if((XVar)(CommonFunctions.isLogged())  && (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())))
			{
				Security.redirectToList((XVar)(table));
				return false;
			}
			keyParams = XVar.Clone(XVar.Array());
			i = new XVar(1);
			while(XVar.Pack(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("editid", i)))))
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("editid", i, "=", MVCFunctions.RawUrlEncode((XVar)(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("editid", i)))))), null);
				i++;
			}
			XSession.Session["MyURL"] = MVCFunctions.Concat(MVCFunctions.GetScriptName(), "?", MVCFunctions.implode(new XVar("&"), (XVar)(keyParams)));
			CommonFunctions.redirectToLogin();
			return false;
		}
		public static XVar handleBrokenRequest()
		{
			if((XVar)(MVCFunctions.POSTSize() != 0)  || (XVar)(!(XVar)(MVCFunctions.postvalue(new XVar("submit")))))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.postvalue(new XVar("editid1")))))
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
			else
			{
				if(XVar.Pack(MVCFunctions.postvalue(new XVar("fly"))))
				{
					MVCFunctions.Echo(-1);
					MVCFunctions.ob_flush();
					HttpContext.Current.Response.End();
					throw new RunnerInlineOutputException();
				}
				else
				{
					XSession.Session["message_edit"] = MVCFunctions.Concat("<< ", "Error occurred", " >>");
				}
			}
			return null;
		}
		protected virtual XVar buildNewRecordData()
		{
			dynamic blobfields = null, efilename_values = XVar.Array(), evalues = XVar.Array(), keys = XVar.Array();
			evalues = XVar.Clone(XVar.Array());
			efilename_values = XVar.Clone(XVar.Array());
			blobfields = XVar.Clone(XVar.Array());
			keys = XVar.Clone(this.keys);
			foreach (KeyValuePair<XVar, dynamic> f in this.editFields.GetEnumerator())
			{
				dynamic control = null;
				control = XVar.Clone(getControl((XVar)(f.Value), (XVar)(this.id)));
				control.readWebValue((XVar)(evalues), (XVar)(blobfields), new XVar(null), new XVar(null), (XVar)(efilename_values));
				if((XVar)(keys.KeyExists(f.Value))  && (XVar)(evalues.KeyExists(f.Value)))
				{
					if(keys[f.Value] != control.getWebValue())
					{
						keys.InitAndSetArrayItem(control.getWebValue(), f.Value);
						this.keysChanged = new XVar(true);
					}
				}
			}
			if(XVar.Pack(this.keysChanged))
			{
				setKeys((XVar)(keys));
			}
			foreach (KeyValuePair<XVar, dynamic> value in efilename_values.GetEnumerator())
			{
				evalues.InitAndSetArrayItem(value.Value, value.Key);
			}
			this.newRecordData = XVar.Clone(evalues);
			this.newRecordBlobFields = XVar.Clone(blobfields);
			return null;
		}
		public virtual XVar processDataInput()
		{
			this.oldKeys = XVar.Clone(this.keys);
			buildNewRecordData();
			if(XVar.Pack(!(XVar)(recheckUserPermissions())))
			{
				this.oldRecordData = XVar.Clone(this.newRecordData);
				this.cachedRecord = XVar.Clone(this.newRecordData);
				this.recordValuesToEdit = new XVar(null);
				return false;
			}
			if(XVar.Pack(!(XVar)(checkCaptcha())))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(isRecordEditable(new XVar(true)))))
			{
				return SecurityRedirect();
			}
			if(XVar.Pack(!(XVar)(callBeforeEditEvent())))
			{
				return false;
			}
			setUpdatedLatLng((XVar)(getNewRecordData()), (XVar)(getOldRecordData()));
			if(XVar.Pack(!(XVar)(checkDeniedDuplicatedValues())))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(confirmLockingBeforeSaving())))
			{
				return false;
			}
			if(XVar.Pack(callCustomEditEvent()))
			{
				this.updatedSuccessfully = XVar.Clone(MVCFunctions.DoUpdateRecord(this));
			}
			unlockNewRecord();
			if(XVar.Pack(!(XVar)(this.updatedSuccessfully)))
			{
				setKeys((XVar)(this.oldKeys));
				return false;
			}
			if(XVar.Pack(MVCFunctions.in_array((XVar)(getAfterEditAction()), (XVar)(new XVar(0, Constants.AE_TO_EDIT, 1, Constants.AE_TO_PREV_EDIT, 2, Constants.AE_TO_NEXT_EDIT)))))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_recordUpdated")] = true;
			}
			ProcessFiles();
			this.messageType = new XVar(Constants.MESSAGE_INFO);
			setSuccessfulEditMessage();
			callAfterSuccessfulSave();
			unlockOldRecord();
			mergeNewRecordData();
			auditLogEdit();
			callAfterEditEvent();
			setKeys((XVar)(this.keys));
			return true;
		}
		protected virtual XVar setSuccessfulEditMessage()
		{
			if(XVar.Pack(isMessageSet()))
			{
				return null;
			}
			setMessage(new XVar(MVCFunctions.Concat("<strong>&lt;&lt;&lt; ", "Record updated", " &gt;&gt;&gt;</strong>")));
			return null;
		}
		protected virtual XVar checkDeniedDuplicatedValues()
		{
			dynamic oldData = XVar.Array();
			oldData = XVar.Clone(getOldRecordData());
			foreach (KeyValuePair<XVar, dynamic> value in this.newRecordData.GetEnumerator())
			{
				if(XVar.Pack(this.pSet.allowDuplicateValues((XVar)(value.Key))))
				{
					continue;
				}
				if(oldData[value.Key] == value.Value)
				{
					continue;
				}
				if(XVar.Pack(!(XVar)(hasDuplicateValue((XVar)(value.Key), (XVar)(value.Value)))))
				{
					continue;
				}
				this.errorFields.InitAndSetArrayItem(value.Key, null);
				setMessage((XVar)(getDenyDuplicatedMessage((XVar)(value.Key), (XVar)(value.Value))));
				return false;
			}
			return true;
		}
		protected virtual XVar auditLogEdit(dynamic _param_keys = null)
		{
			#region default values
			if(_param_keys as Object == null) _param_keys = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.count(keys))))
			{
				keys = XVar.Clone(this.keys);
			}
			if(XVar.Pack(this.auditObj))
			{
				this.auditObj.LogEdit((XVar)(this.tName), (XVar)(this.newRecordData), (XVar)(getOldRecordData()), (XVar)(keys));
			}
			return null;
		}
		protected virtual XVar mergeNewRecordData()
		{
			if((XVar)(!(XVar)(this.auditObj))  && (XVar)(!(XVar)(this.eventsObject.exists(new XVar("AfterEdit")))))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> v in getOldRecordData().GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.newRecordData.KeyExists(v.Key))))
				{
					this.newRecordData.InitAndSetArrayItem(v.Value, v.Key);
				}
			}
			return null;
		}
		protected virtual XVar callAfterEditEvent()
		{
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("AfterEdit")))))
			{
				return null;
			}
			this.eventsObject.AfterEdit((XVar)(this.newRecordData), (XVar)(getKeysWhereClause(new XVar(false))), (XVar)(getOldRecordData()), (XVar)(this.keys), (XVar)(this.mode == Constants.EDIT_INLINE), this);
			return null;
		}
		protected virtual XVar unlockOldRecord()
		{
			if((XVar)(this.lockingObj)  && (XVar)(this.keysChanged))
			{
				this.lockingObj.UnlockRecord((XVar)(this.tName), (XVar)(this.oldKeys), new XVar(""));
			}
			return null;
		}
		protected virtual XVar unlockNewRecord()
		{
			if(XVar.Pack(this.lockingObj))
			{
				this.lockingObj.UnlockRecord((XVar)(this.tName), (XVar)(this.keys), new XVar(""));
			}
			return null;
		}
		protected virtual XVar callAfterSuccessfulSave()
		{
			foreach (KeyValuePair<XVar, dynamic> f in this.editFields.GetEnumerator())
			{
				getControl((XVar)(f.Value), (XVar)(this.id)).afterSuccessfulSave();
			}
			return null;
		}
		protected virtual XVar callBeforeEditEvent()
		{
			dynamic ret = null, usermessage = null;
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("BeforeEdit")))))
			{
				return true;
			}
			usermessage = new XVar("");
			ret = XVar.Clone(this.eventsObject.BeforeEdit((XVar)(this.newRecordData), (XVar)(getKeysWhereClause(new XVar(true))), (XVar)(getOldRecordData()), (XVar)(this.oldKeys), ref usermessage, (XVar)(this.mode == Constants.EDIT_INLINE), this));
			if(usermessage != XVar.Pack(""))
			{
				setMessage((XVar)(usermessage));
			}
			return ret;
		}
		protected virtual XVar callCustomEditEvent()
		{
			dynamic ret = null, usermessage = null;
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("CustomEdit")))))
			{
				return true;
			}
			usermessage = new XVar("");
			ret = XVar.Clone(this.eventsObject.CustomEdit((XVar)(this.newRecordData), (XVar)(getKeysWhereClause(new XVar(true))), (XVar)(getOldRecordData()), (XVar)(this.oldKeys), (XVar)(usermessage), (XVar)(this.mode == Constants.EDIT_INLINE), this));
			if(XVar.Pack(!(XVar)(ret)))
			{
				setMessage((XVar)(usermessage));
				this.updatedSuccessfully = XVar.Clone(0 == MVCFunctions.strlen((XVar)(this.message)));
			}
			return ret;
		}
		public virtual XVar captchaExists()
		{
			if((XVar)((XVar)(this.mode == Constants.ADD_ONTHEFLY)  || (XVar)(this.mode == Constants.ADD_INLINE))  || (XVar)(this.mode == Constants.EDIT_INLINE))
			{
				return false;
			}
			return this.pSet.isCaptchaEnabledOnEdit();
		}
		public override XVar getCaptchaFieldName()
		{
			return this.pSet.captchaEditFieldName();
		}
		protected virtual XVar recheckUserPermissions()
		{
			if(XVar.Pack(CommonFunctions.CheckTablePermissions((XVar)(this.tName), new XVar("E"))))
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
		protected virtual XVar confirmLockingBeforeSaving()
		{
			dynamic lockConfirmed = null, lockmessage = null;
			if(XVar.Pack(!(XVar)(this.lockingObj)))
			{
				return true;
			}
			lockmessage = new XVar("");
			if(XVar.Pack(this.keysChanged))
			{
				lockConfirmed = XVar.Clone(this.lockingObj.ConfirmLock((XVar)(this.tName), (XVar)(this.oldKeys), ref lockmessage));
				if(XVar.Pack(lockConfirmed))
				{
					lockConfirmed = XVar.Clone(this.lockingObj.LockRecord((XVar)(this.tName), (XVar)(this.keys)));
				}
			}
			else
			{
				lockConfirmed = XVar.Clone(this.lockingObj.ConfirmLock((XVar)(this.tName), (XVar)(this.oldKeys), ref lockmessage));
			}
			if(XVar.Pack(!(XVar)(lockConfirmed)))
			{
				this.lockingMessageStyle = new XVar("display:block");
				if(this.mode == Constants.EDIT_INLINE)
				{
					dynamic returnJSON = XVar.Array();
					if((XVar)(CommonFunctions.IsAdmin())  || (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP))
					{
						lockmessage = XVar.Clone(this.lockingObj.GetLockInfo((XVar)(this.tName), (XVar)(this.oldKeys), new XVar(false), (XVar)(this.id)));
					}
					returnJSON.InitAndSetArrayItem(false, "success");
					returnJSON.InitAndSetArrayItem(lockmessage, "message");
					returnJSON.InitAndSetArrayItem(false, "enableCtrls");
					returnJSON.InitAndSetArrayItem(this.lockingObj.ConfirmTime, "confirmTime");
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					MVCFunctions.ob_flush();
					HttpContext.Current.Response.End();
					throw new RunnerInlineOutputException();
				}
				else
				{
					if((XVar)(CommonFunctions.IsAdmin())  || (XVar)(XSession.Session["AccessLevel"] == Constants.ACCESS_LEVEL_ADMINGROUP))
					{
						dynamic id = null;
						this.lockingMessageText = XVar.Clone(this.lockingObj.GetLockInfo((XVar)(this.tName), (XVar)(this.oldKeys), new XVar(true), (XVar)(id)));
					}
					else
					{
						this.lockingMessageText = XVar.Clone(lockmessage);
					}
				}
				this.readEditValues = new XVar(true);
				return false;
			}
			return true;
		}
		protected virtual XVar SecurityRedirect()
		{
			if(this.mode == Constants.EDIT_INLINE)
			{
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(new XVar("success", false, "message", "The record is not editable"))));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			Security.redirectToList((XVar)(this.tName));
			return null;
		}
		protected virtual XVar isRecordEditable(dynamic _param_useOldData)
		{
			#region pass-by-value parameters
			dynamic useOldData = XVar.Clone(_param_useOldData);
			#endregion

			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
			{
				if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.IsRecordEditable((XVar)((XVar.Pack(useOldData) ? XVar.Pack(getOldRecordData()) : XVar.Pack(getCurrentRecordInternal()))), new XVar(true), (XVar)(this.tName)))))
				{
					return false;
				}
			}
			return true;
		}
		public virtual XVar getOldRecordData()
		{
			if(XVar.Equals(XVar.Pack(this.oldRecordData), XVar.Pack(null)))
			{
				dynamic fetchedArray = null;
				GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(getKeysWhereClause(new XVar(true)))));
				CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
				fetchedArray = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
				this.oldRecordData = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
			}
			return this.oldRecordData;
		}
		public virtual XVar getBlobFields()
		{
			return this.newRecordBlobFields;
		}
		public virtual XVar getNewRecordData()
		{
			return this.newRecordData;
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

			if(this.mode != Constants.EDIT_INLINE)
			{
				this.message = XVar.Clone(MVCFunctions.Concat("<strong>&lt;&lt;&lt; ", "Record was NOT edited", "</strong> &gt;&gt;&gt;<br><br>", message));
			}
			else
			{
				this.message = XVar.Clone(MVCFunctions.Concat("Record was NOT edited", ". ", message));
			}
			this.messageType = new XVar(Constants.MESSAGE_ERROR);
			return null;
		}
		protected override XVar checkFieldOnPage(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if(this.mode == Constants.EDIT_INLINE)
			{
				return this.pSet.appearOnInlineEdit((XVar)(fName));
			}
			return this.pSet.appearOnEditPage((XVar)(fName));
		}
		public override XVar getFieldControlsData()
		{
			dynamic editValues = null;
			if(XVar.Pack(this.recordValuesToEdit))
			{
				return this.recordValuesToEdit;
			}
			editValues = XVar.Clone(getCurrentRecordInternal());
			if(XVar.Pack(this.eventsObject.exists(new XVar("ProcessValuesEdit"))))
			{
				this.eventsObject.ProcessValuesEdit((XVar)(editValues), this);
			}
			this.recordValuesToEdit = XVar.Clone(editValues);
			return this.recordValuesToEdit;
		}
		public override XVar isMultistepped()
		{
			return this.pSet.isEditMultistep();
		}
		public override XVar viewAvailable()
		{
			if(XVar.Pack(this.dashElementData))
			{
				return (XVar)(base.viewAvailable())  && (XVar)(this.dashElementData["details"][this.tName]["view"]);
			}
			return base.viewAvailable();
		}
		public override XVar getLayoutVersion()
		{
			dynamic layout = null;
			if(this.mode != Constants.EDIT_INLINE)
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
		protected virtual XVar isPopupMode()
		{
			return this.mode == Constants.EDIT_POPUP;
		}
		protected virtual XVar isSimpleMode()
		{
			return this.mode == Constants.EDIT_SIMPLE;
		}
		public static XVar processMasterKeys()
		{
			dynamic i = null, options = XVar.Array();
			i = new XVar(1);
			options = XVar.Clone(XVar.Array());
			while(XVar.Pack(MVCFunctions.REQUESTKeyExists(MVCFunctions.Concat("masterkey", i))))
			{
				options.InitAndSetArrayItem(MVCFunctions.postvalue(MVCFunctions.Concat("masterkey", i)), i);
				i++;
			}
			return options;
		}
		public static XVar EditPageFactory(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			if((XVar)(!(XVar)(var_params["selection"]))  || (XVar)(!(XVar)(MVCFunctions.is_array((XVar)(var_params["selection"])))))
			{
				var_params.InitAndSetArrayItem(XSession.Session["edit_seletion"], "selection");
				XSession.Session.Remove("edit_seletion");
			}
			if((XVar)(var_params["selection"])  && (XVar)(MVCFunctions.is_array((XVar)(var_params["selection"]))))
			{
				if(0 < MVCFunctions.count(var_params["selection"]))
				{
					if(var_params["mode"] == Constants.EDIT_SIMPLE)
					{
						var_params.InitAndSetArrayItem(Constants.EDIT_SELECTED_SIMPLE, "mode");
					}
					if(var_params["mode"] == Constants.EDIT_POPUP)
					{
						var_params.InitAndSetArrayItem(Constants.EDIT_SELECTED_POPUP, "mode");
					}
					return new EditSelectedPage((XVar)(var_params));
				}
			}
			return new EditPage((XVar)(var_params));
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ROW_NUMBER() over () as DB2_ROW_NUMBER ");
			}
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Edit"), (XVar)(this.tName)), "mandatoryWhere", null);
			if((XVar)(this.mode == Constants.EDIT_DASHBOARD)  && (XVar)(this.mapRefresh))
			{
				sql.InitAndSetArrayItem(getWhereByMap(), "mandatoryWhere", null);
			}
			return sql;
		}
	}
}
