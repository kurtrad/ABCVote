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
	public partial class ListPage : RunnerPage
	{
		public dynamic gPageSize = XVar.Pack(0);
		protected dynamic orderClause = XVar.Pack(null);
		public dynamic exportTo = XVar.Pack(false);
		public dynamic deleteRecs = XVar.Pack(false);
		public dynamic listFields = XVar.Array();
		protected dynamic recSet = XVar.Pack(null);
		public dynamic nSecOptions = XVar.Pack(0);
		protected dynamic recNo = XVar.Pack(1);
		protected dynamic rowId = XVar.Pack(0);
		protected dynamic selectedRecs = XVar.Array();
		protected dynamic recordsDeleted = XVar.Pack(0);
		public dynamic origTName = XVar.Pack("");
		public dynamic panelSearchFields = XVar.Array();
		public dynamic arrKeyFields = XVar.Array();
		public dynamic audit = XVar.Pack(null);
		public dynamic noRecordsFirstPage = XVar.Pack(false);
		public dynamic mainTableOwnerID = XVar.Pack("");
		public dynamic printFriendly = XVar.Pack(false);
		public dynamic createLoginPage = XVar.Pack(false);
		protected dynamic searchPanel = XVar.Pack(null);
		protected dynamic arrFieldSpanVal = XVar.Array();
		protected dynamic lockDelRec;
		public dynamic firstTime = XVar.Pack(0);
		protected dynamic gMapFields = XVar.Array();
		public dynamic nLoginMethod;
		protected dynamic recordFieldTypes = XVar.Array();
		protected dynamic hiddenColumnClasses = XVar.Array();
		protected dynamic showAddInPopup = XVar.Pack(false);
		protected dynamic showEditInPopup = XVar.Pack(false);
		protected dynamic showViewInPopup = XVar.Pack(false);
		protected dynamic fieldClasses = XVar.Array();
		public dynamic sortBy;
		protected dynamic addedDetailsCountSubqueries = XVar.Array();
		protected static bool skipListPageCtor = false;
		public ListPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPageCtor)
			{
				skipListPageCtor = false;
				return;
			}
			dynamic i = null;
			this.showAddInPopup = XVar.Clone(this.pSet.isShowAddInPopup());
			this.showEditInPopup = XVar.Clone(this.pSet.isShowEditInPopup());
			this.showViewInPopup = XVar.Clone(this.pSet.isShowViewInPopup());
			this.pSet.setPage(new XVar(Constants.PAGE_SEARCH));
			beforeProcessEvent();
			setLangParams();
			if(XVar.Pack(this.searchClauseObj))
			{
				this.jsSettings.InitAndSetArrayItem(this.searchClauseObj.simpleSearchActive, "tableSettings", this.tName, "simpleSearchActive");
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				this.permis.InitAndSetArrayItem(getPermissions((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])), this.allDetailsTablesArr[i]["dDataSourceTable"]);
				this.detailKeysByD.InitAndSetArrayItem(this.pSet.getDetailKeysByDetailTable((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])), i);
			}
			genId();
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "totalFields");
			foreach (KeyValuePair<XVar, dynamic> tField in this.totalsFields.GetEnumerator())
			{
				dynamic totalFieldData = XVar.Array();
				totalFieldData = XVar.Clone(new XVar("type", tField.Value["totalsType"], "fName", tField.Value["fName"], "format", tField.Value["viewFormat"]));
				if(Constants.FORMAT_NUMBER == tField.Value["viewFormat"])
				{
					totalFieldData.InitAndSetArrayItem(this.pSet.isDecimalDigits((XVar)(tField.Value["fName"])), "numberOfDigits");
				}
				this.jsSettings.InitAndSetArrayItem(totalFieldData, "tableSettings", this.tName, "totalFields", null);
				if(tField.Value["totalsType"] == "COUNT")
				{
					outputFieldValue((XVar)(tField.Value["fName"]), new XVar(1));
				}
				else
				{
					outputFieldValue((XVar)(tField.Value["fName"]), new XVar(2));
				}
			}
			this.jsSettings.InitAndSetArrayItem((XVar)((XVar)(this.listGridLayout == Constants.gltHORIZONTAL)  || (XVar)(this.listGridLayout == Constants.gltFLEXIBLE))  && (XVar)(this.isScrollGridBody), "tableSettings", this.tName, "scrollGridBody");
			this.jsSettings.InitAndSetArrayItem(this.permis[this.tName], "tableSettings", this.tName, "permissions");
			if(this.pSet.getAdvancedSecurityType() == Constants.ADVSECURITY_EDIT_OWN)
			{
				this.jsSettings.InitAndSetArrayItem(this.permis[this.tName], "tableSettings", this.tName, "isEditOwn");
			}
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isInlineEdit"), "tableSettings", "inlineEdit");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isInlineAdd"), "tableSettings", "inlineAdd");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "copy"), "tableSettings", "copy");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "view"), "tableSettings", "view");
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "listFields");
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				this.jsSettings.InitAndSetArrayItem(this.listFields[i]["fName"], "tableSettings", this.tName, "listFields", null);
				if(this.listFields[i]["viewFormat"] == Constants.FORMAT_MAP)
				{
					this.gMapFields.InitAndSetArrayItem(i, null);
				}
			}
			processClickAction();
			addPopupLayoutNamesToSettings();
			createOrderByObject();
			if((XVar)(this.listAjax)  && (XVar)(this.mode != Constants.LIST_DETAILS))
			{
				this.pageData.InitAndSetArrayItem(getUrlParams(), "urlParams");
			}
		}
		protected virtual XVar createOrderByObject()
		{
			this.orderClause = XVar.Clone(OrderClause.createFromPage(this, new XVar(true)));
			return null;
		}
		public virtual XVar processClickAction()
		{
			dynamic clickActionJSON = null;
			clickActionJSON = XVar.Clone(this.pSet.getClickActionJSONString());
			if(clickActionJSON != XVar.Pack(""))
			{
				dynamic clickAction = XVar.Array();
				clickAction = XVar.Clone(MVCFunctions.my_json_decode((XVar)(clickActionJSON)));
				foreach (KeyValuePair<XVar, dynamic> fSetting in clickAction["fields"].GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(checkAllowClickAction((XVar)(fSetting.Value)))))
					{
						clickAction.InitAndSetArrayItem("noaction", "fields", fSetting.Key, "action");
					}
				}
				if(XVar.Pack(!(XVar)(checkAllowClickAction((XVar)(clickAction["row"])))))
				{
					clickAction.InitAndSetArrayItem("noaction", "row", "action");
				}
				this.jsSettings.InitAndSetArrayItem(clickAction, "tableSettings", this.tName, "clickActions");
			}
			return null;
		}
		public virtual XVar checkAllowClickAction(dynamic _param_actionSet)
		{
			#region pass-by-value parameters
			dynamic actionSet = XVar.Clone(_param_actionSet);
			#endregion

			dynamic isActionAllowed = null;
			isActionAllowed = new XVar(true);
			if(actionSet["action"] == "open")
			{
				switch(((XVar)actionSet["openData"]["page"]).ToString())
				{
					case "add":
						isActionAllowed = XVar.Clone(addAvailable());
						break;
					case "edit":
						isActionAllowed = XVar.Clone(editAvailable());
						break;
					case "view":
						isActionAllowed = XVar.Clone(viewAvailable());
						break;
					case "print":
						isActionAllowed = XVar.Clone(printAvailable());
						break;
					default:
						isActionAllowed = new XVar(true);
						break;
				}
			}
			return isActionAllowed;
		}
		public virtual XVar addOrderUrlParam()
		{
			dynamic urlParams = null;
			urlParams = XVar.Clone(this.orderClause.getOrderUrlParams());
			if(0 < MVCFunctions.strlen((XVar)(urlParams)))
			{
				if(XVar.Pack(!(XVar)(this.pageData.KeyExists("urlParams"))))
				{
					this.pageData.InitAndSetArrayItem(XVar.Array(), "urlParams");
				}
				this.pageData.InitAndSetArrayItem(urlParams, "urlParams", "orderby");
			}
			return null;
		}
		public virtual XVar getUrlParams()
		{
			dynamic i = null, tabId = null, var_params = XVar.Array();
			var_params = XVar.Clone(XVar.Array());
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_qs")]))
			{
				var_params.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_qs")])), "qs");
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_q")]))
			{
				var_params.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_q")])), "q");
			}
			if((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch")])  && (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch")] != "and"))
			{
				var_params.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch")], "criteria");
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_filters")]))
			{
				var_params.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_filters")])), "f");
			}
			if((XVar)((XVar)((XVar)((XVar)(!(XVar)(MVCFunctions.postvalue(new XVar("qs"))))  && (XVar)(!(XVar)(MVCFunctions.postvalue(new XVar("q")))))  && (XVar)(!(XVar)(MVCFunctions.REQUESTKeyExists("f"))))  && (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")]))  && (XVar)(1 < XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")]))
			{
				var_params.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")], "goto");
			}
			if((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")])  && (XVar)(this.gPageSize != XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")]))
			{
				var_params.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")], "pagesize");
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")]))
			{
				var_params.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")], "mastertable");
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")]))
			{
				var_params.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")], "mastertable");
			}
			if(XVar.Pack(MVCFunctions.count(this.masterKeysReq)))
			{
				i = new XVar(1);
				for(;i <= MVCFunctions.count(this.masterKeysReq); i++)
				{
					var_params.InitAndSetArrayItem(this.masterKeysReq[i], MVCFunctions.Concat("masterkey", i));
				}
			}
			else
			{
				if(XVar.Pack(MVCFunctions.count(this.detailKeysByM)))
				{
					i = new XVar(1);
					for(;i <= MVCFunctions.count(this.detailKeysByM); i++)
					{
						var_params.InitAndSetArrayItem(this.masterKeysReq[i], MVCFunctions.Concat("masterkey", i));
					}
				}
			}
			if(XVar.Pack(this.searchClauseObj))
			{
				if(XVar.Pack(this.searchClauseObj.savedSearchIsRun))
				{
					var_params.InitAndSetArrayItem(true, "savedSearch");
				}
			}
			tabId = XVar.Clone(getCurrentTabId());
			if(XVar.Pack(MVCFunctions.strlen((XVar)(tabId))))
			{
				var_params.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(tabId)), "tab");
			}
			return var_params;
		}
		protected virtual XVar addPopupLayoutNamesToSettings()
		{
			dynamic layoutNames = null;
			layoutNames = XVar.Clone(this.pSet.getPopupPagesLayoutNames());
			if(XVar.Pack(MVCFunctions.count(layoutNames)))
			{
				this.jsSettings.InitAndSetArrayItem(this.pSet.getPopupPagesLayoutNames(), "tableSettings", this.tName, "popupPagesLayoutNames");
			}
			return null;
		}
		public virtual XVar addCommonHtml()
		{
			if((XVar)(!(XVar)(isDashboardElement()))  && (XVar)(!(XVar)(mobileTemplateMode())))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div id=\"search_suggest\" class=\"search_suggest\"></div>");
			}
			if(XVar.Pack(this.is508))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<a href=\"#skipdata\" title=\"", "Skip to table data", "\" class=\"", makeClassName(new XVar("s508")), "\">", "Skip to table data", "</a>");
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<a href=\"#skipmenu\" title=\"", "Skip to menu", "\" class=\"", makeClassName(new XVar("s508")), "\">", "Skip to menu", "</a>");
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<a href=\"#skipsearch\" title=\"", "Skip to search", "\" class=\"", makeClassName(new XVar("s508")), "\">", "Skip to search", "</a>");
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<a href=\"templates/helpshortcut.htm\" title=\"", "Hotkeys reference", "\" class=\"", makeClassName(new XVar("s508")), "\">", "Hotkeys reference", "</a>");
			}
			displayMasterTableInfo();
			return null;
		}
		public override XVar addCommonJs()
		{
			dynamic addPSet = null, editPSet = null, i = null;
			base.addCommonJs();
			addJsForGrid();
			addButtonHandlers();
			if(XVar.Pack(this.allDetailsTablesArr))
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			return null;
		}
		public virtual XVar addJsForGrid()
		{
			if(XVar.Pack(this.isResizeColumns))
			{
				prepareForResizeColumns();
			}
			if((XVar)(this.pSet.isAllowFieldsReordering())  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				AddJSFile(new XVar("include/jquery.dragtable.js"));
			}
			this.jsSettings.InitAndSetArrayItem((XVar.Pack(this.numRowsFromSQL) ? XVar.Pack(true) : XVar.Pack(false)), "tableSettings", this.tName, "showRows");
			initGmaps();
			return null;
		}
		public virtual XVar prepareForResizeColumns()
		{
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				AddJSFile(new XVar("include/colresizable.js"));
				return null;
			}
			if(this.mode != Constants.LIST_AJAX)
			{
				if(XVar.Equals(XVar.Pack(this.debugJSMode), XVar.Pack(true)))
				{
					AddJSFile(new XVar("include/runnerJS/RunnerResizeGrid.js"));
				}
			}
			return null;
		}
		public override XVar processMasterKeyValue()
		{
			base.processMasterKeyValue();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_search")] = 0;
			if(XVar.Pack(this.firstTime))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			return null;
		}
		public virtual XVar beforeProcessEvent()
		{
			if(XVar.Pack(eventExists(new XVar("BeforeProcessList"))))
			{
				this.eventsObject.BeforeProcessList(this);
			}
			return null;
		}
		public override XVar setSessionVariables()
		{
			base.setSessionVariables();
			if(XVar.Pack(this.searchClauseObj))
			{
				if(XVar.Pack(this.searchClauseObj.isSearchFunctionalityActivated()))
				{
					XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
				}
				else
				{
					XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_advsearch"));
				}
				if(XVar.Pack(!(XVar)(this.searchClauseObj.filtersActivated)))
				{
					XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_filters"));
				}
			}
			else
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_advsearch"));
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_filters"));
			}
			if(XVar.Pack(MVCFunctions.postvalue("goto")))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = MVCFunctions.postvalue("goto");
			}
			this.myPage = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")]);
			if(XVar.Pack(!(XVar)(this.myPage)))
			{
				this.myPage = new XVar(1);
			}
			if(XVar.Pack(!(XVar)(this.pageSize)))
			{
				this.pageSize = XVar.Clone(this.gPageSize);
			}
			return null;
		}
		protected virtual XVar assignColumnHeaderClasses()
		{
			dynamic field = null, fieldClassStr = null, goodName = null, i = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				field = XVar.Clone(this.listFields[i]["fName"]);
				goodName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
				fieldClassStr = XVar.Clone(fieldClass((XVar)(field)));
				if(XVar.Pack(this.hiddenColumnClasses.KeyExists(goodName)))
				{
					fieldClassStr = MVCFunctions.Concat(fieldClassStr, " ", this.hiddenColumnClasses[goodName]);
				}
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field)), "_class")), (XVar)(fieldClassStr));
			}
			return null;
		}
		public virtual XVar isReoderByHeaderClickingEnabled()
		{
			return (XVar)(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)  || (XVar)(this.pSet.hasReorderingByHeader());
		}
		public virtual XVar deleteRecords()
		{
			dynamic i = null, message_class = null, selectedKeys = XVar.Array();
			if(MVCFunctions.postvalue("a") != "delete")
			{
				return null;
			}
			message_class = new XVar("alert-warning");
			this.deleteMessage = new XVar("");
			if(XVar.Pack(MVCFunctions.postvalue("mdelete")))
			{
				foreach (KeyValuePair<XVar, dynamic> ind in MVCFunctions.EnumeratePOST("mdelete"))
				{
					selectedKeys = XVar.Clone(XVar.Array());
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.arrKeyFields); i++)
					{
						selectedKeys.InitAndSetArrayItem(MVCFunctions.postvalue(MVCFunctions.Concat("mdelete", i + 1))[MVCFunctions.mdeleteIndex((XVar)(ind.Value))], this.arrKeyFields[i]);
					}
					this.selectedRecs.InitAndSetArrayItem(selectedKeys, null);
				}
			}
			else
			{
				if(XVar.Pack(MVCFunctions.postvalue("selection")))
				{
					foreach (KeyValuePair<XVar, dynamic> keyblock in MVCFunctions.EnumeratePOST("selection"))
					{
						dynamic arr = XVar.Array();
						arr = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(keyblock.Value)));
						if(MVCFunctions.count(arr) < MVCFunctions.count(this.arrKeyFields))
						{
							continue;
						}
						selectedKeys = XVar.Clone(XVar.Array());
						i = new XVar(0);
						for(;i < MVCFunctions.count(this.arrKeyFields); i++)
						{
							selectedKeys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(arr[i])), this.arrKeyFields[i]);
						}
						this.selectedRecs.InitAndSetArrayItem(selectedKeys, null);
					}
				}
			}
			this.recordsDeleted = new XVar(0);
			this.lockDelRec = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> keys in this.selectedRecs.GetEnumerator())
			{
				dynamic deleted_values = XVar.Array(), deletedqResult = null, lockRecord = null, mandatoryWhere = XVar.Array(), retval = null, selectSQL = null, sqlParts = null, tdeleteMessage = null, where = null;
				where = XVar.Clone(CommonFunctions.KeyWhere((XVar)(keys.Value)));
				mandatoryWhere = XVar.Clone(XVar.Array());
				mandatoryWhere.InitAndSetArrayItem(where, null);
				if((XVar)((XVar)(this.nSecOptions != Constants.ADVSECURITY_ALL)  && (XVar)(this.nLoginMethod == Constants.SECURITY_TABLE))  && (XVar)(this.createLoginPage))
				{
					where = XVar.Clone(CommonFunctions.whereAdd((XVar)(where), (XVar)(CommonFunctions.SecuritySQL(new XVar("Delete"), (XVar)(this.tName)))));
					mandatoryWhere.InitAndSetArrayItem(SecuritySQL(new XVar("Delete"), (XVar)(this.tName)), null);
				}
				sqlParts = XVar.Clone(this.gQuery.getSqlComponents());
				selectSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sqlParts), (XVar)(mandatoryWhere)));
				GlobalVars.strSQL = XVar.Clone(MVCFunctions.Concat("delete from ", this.connection.addTableWrappers((XVar)(this.origTName)), " where ", where));
				retval = new XVar(true);
				deletedqResult = XVar.Clone(this.connection.query((XVar)(SQLQuery.buildSQL((XVar)(sqlParts), (XVar)(mandatoryWhere)))));
				deleted_values = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(deletedqResult.fetchAssoc())));
				if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
				{
					if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.IsRecordEditable((XVar)(deleted_values), new XVar(true), (XVar)(this.tName)))))
					{
						continue;
					}
				}
				if(XVar.Pack(eventExists(new XVar("BeforeDelete"))))
				{
					RunnerContext.pushRecordContext((XVar)(deleted_values), this);
					tdeleteMessage = XVar.Clone(this.deleteMessage);
					retval = XVar.Clone(this.eventsObject.BeforeDelete((XVar)(where), (XVar)(deleted_values), ref tdeleteMessage, this));
					this.deleteMessage = XVar.Clone(tdeleteMessage);
					RunnerContext.pop();
				}
				lockRecord = new XVar(false);
				if(XVar.Pack(this.lockingObj))
				{
					dynamic lockWhere = XVar.Array();
					lockWhere = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> keysvalue in keys.Value.GetEnumerator())
					{
						lockWhere.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(keysvalue.Value)), null);
					}
					if(XVar.Pack(this.lockingObj.isLocked((XVar)(this.origTName), (XVar)(MVCFunctions.implode(new XVar("&"), (XVar)(lockWhere))), new XVar("1"))))
					{
						lockRecord = new XVar(true);
						this.lockDelRec.InitAndSetArrayItem(keys.Value, null);
					}
					if(this.mode == Constants.LIST_SIMPLE)
					{
						XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_lockDelRec")] = this.lockDelRec;
					}
				}
				if((XVar)((XVar)(!(XVar)(lockRecord))  && (XVar)(MVCFunctions.postvalue("a") == "delete"))  && (XVar)(retval))
				{
					this.recordsDeleted++;
					CommonFunctions.DeleteUploadedFiles((XVar)(this.pSet), (XVar)(deleted_values));
					CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
					this.connection.exec((XVar)(GlobalVars.strSQL));
					if((XVar)(this.audit)  && (XVar)(deleted_values))
					{
						dynamic deleted_audit_values = XVar.Array(), fieldsList = XVar.Array();
						fieldsList = XVar.Clone(this.pSet.getFieldsList());
						i = new XVar(0);
						foreach (KeyValuePair<XVar, dynamic> value in deleted_values.GetEnumerator())
						{
							if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(this.pSet.getFieldType((XVar)(fieldsList[i]))))))
							{
								deleted_audit_values.InitAndSetArrayItem(value.Value, fieldsList[i]);
							}
							else
							{
								deleted_audit_values.InitAndSetArrayItem(value.Value, value.Key);
							}
							i++;
						}
						this.audit.LogDelete((XVar)(this.tName), (XVar)(deleted_audit_values), (XVar)(keys.Value));
					}
					if(XVar.Pack(eventExists(new XVar("AfterDelete"))))
					{
						RunnerContext.pushRecordContext((XVar)(deleted_values), this);
						tdeleteMessage = XVar.Clone(this.deleteMessage);
						this.eventsObject.AfterDelete((XVar)(where), (XVar)(deleted_values), ref tdeleteMessage, this);
						this.deleteMessage = XVar.Clone(tdeleteMessage);
						RunnerContext.pop();
						message_class = new XVar("alert-info");
					}
				}
				if(XVar.Pack(MVCFunctions.strlen((XVar)(this.deleteMessage))))
				{
					this.xt.assignbyref(new XVar("message"), (XVar)(this.deleteMessage));
					this.xt.assignbyref(new XVar("message_class"), (XVar)(message_class));
					this.xt.assign(new XVar("message_block"), new XVar(true));
				}
			}
			if((XVar)(MVCFunctions.count(this.selectedRecs))  && (XVar)(eventExists(new XVar("AfterMassDelete"))))
			{
				this.eventsObject.AfterMassDelete((XVar)(this.recordsDeleted), this);
			}
			return null;
		}
		public virtual XVar rulePRG()
		{
			if(XVar.Pack(this.stopPRG))
			{
				return false;
			}
			if((XVar)((XVar)(MVCFunctions.no_output_done())  && (XVar)(MVCFunctions.count(this.selectedRecs)))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(this.deleteMessage)))))
			{
				MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), (XVar)(getPageType()), new XVar("a=return"));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			return null;
		}
		public virtual XVar BeforeShowList()
		{
			if(XVar.Pack(eventExists(new XVar("BeforeShowList"))))
			{
				dynamic templatefile = null;
				templatefile = XVar.Clone(this.templatefile);
				this.eventsObject.BeforeShowList((XVar)(this.xt), ref templatefile, this);
				this.templatefile = XVar.Clone(templatefile);
			}
			return null;
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			this.xt.assign(new XVar("newrecord_controls_block"), (XVar)((XVar)(inlineAddAvailable())  || (XVar)(addAvailable())));
			if((XVar)((XVar)(isDispGrid())  || (XVar)(this.listAjax))  && (XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(printAvailable())  || (XVar)(exportAvailable()))  || (XVar)(deleteAvailable()))  || (XVar)(updateSelectedAvailable()))  || (XVar)(inlineEditAvailable()))  || (XVar)(inlineAddAvailable()))  || (XVar)((XVar)(this.showAddInPopup)  && (XVar)(addAvailable()))))
			{
				this.xt.assign(new XVar("record_controls_block"), new XVar(true));
			}
			if(this.numRowsFromSQL == 0)
			{
				this.xt.displayBrickHidden(new XVar("recordcontrol"));
			}
			if(XVar.Pack(addAvailable()))
			{
				this.xt.assign(new XVar("add_link"), new XVar(true));
				this.xt.assign(new XVar("addlink_attrs"), (XVar)(MVCFunctions.Concat("href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("add")), "' id=\"addButton", this.id, "\"")));
				if(XVar.Pack(this.dashTName))
				{
					this.xt.assign(new XVar("addlink_getparams"), (XVar)(MVCFunctions.Concat("?fromDashboard=", this.dashTName)));
				}
			}
			inlineAddLinksAttrs();
			if((XVar)(isShowMenu())  || (XVar)(isAdminTable()))
			{
				this.xt.assign(new XVar("quickjump_attrs"), (XVar)(MVCFunctions.Concat("class=\"", makeClassName(new XVar("quickjump")), "\"")));
			}
			foreach (KeyValuePair<XVar, dynamic> mapId in this.googleMapCfg["mainMapIds"].GetEnumerator())
			{
				this.xt.assign_event((XVar)(mapId.Value), this, new XVar("createMapDiv"), (XVar)(new XVar("mapId", mapId.Value, "width", this.googleMapCfg["mapsData"][mapId.Value]["width"], "height", this.googleMapCfg["mapsData"][mapId.Value]["height"])));
			}
			assignSortByDropdown();
			addAssignForGrid();
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			selectAllLinkAttrs();
			editSelectedLinkAttrs();
			updateSelectedLinkAttrs();
			saveAllLinkAttrs();
			cancelAllLinkAttrs();
			assignDetailsTablesBadgeColors();
			return null;
		}
		public virtual XVar addAssignForGrid()
		{
			dynamic colsOnPage = null, gridTableStyle = null, i = null, record_footer = XVar.Array(), record_header = XVar.Array(), rfooter = XVar.Array(), rheader = XVar.Array();
			if(XVar.Pack(!(XVar)(isDispGrid())))
			{
				return null;
			}
			if(XVar.Pack(this.is508))
			{
				if((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(this.listGridLayout == Constants.gltVERTICAL))
				{
					this.xt.assign_section(new XVar("grid_header"), new XVar("<div style=\"display:none\">Table data</div>"), new XVar(""));
				}
				else
				{
					this.xt.assign_section(new XVar("grid_header"), new XVar("<caption style=\"display:none\">Table data</caption>"), new XVar(""));
				}
			}
			this.xt.assign(new XVar("endrecordblock_attrs"), new XVar("colid=\"endrecord\""));
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.listFields[i]["fName"])), "_fieldheadercolumn")), new XVar(true));
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.listFields[i]["fName"])), "_fieldcolumn")), new XVar(true));
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.listFields[i]["fName"])), "_fieldfootercolumn")), new XVar(true));
			}
			colsOnPage = XVar.Clone(this.recsPerRowList);
			record_header = XVar.Clone(new XVar("data", XVar.Array()));
			record_footer = XVar.Clone(new XVar("data", XVar.Array()));
			if((XVar)((XVar)(this.recordsOnPage < colsOnPage)  && (XVar)(this.recordsOnPage))  && (XVar)(this.listGridLayout != Constants.gltVERTICAL))
			{
				colsOnPage = XVar.Clone(this.recordsOnPage);
			}
			i = new XVar(0);
			for(;i < colsOnPage; i++)
			{
				rheader = XVar.Clone(XVar.Array());
				rfooter = XVar.Clone(XVar.Array());
				if(i < colsOnPage - 1)
				{
					rheader.InitAndSetArrayItem(true, "endrecordheader_block");
					rfooter.InitAndSetArrayItem(true, "endrecordfooter_block");
				}
				record_header.InitAndSetArrayItem(rheader, "data", null);
				record_footer.InitAndSetArrayItem(rfooter, "data", null);
			}
			this.xt.assignbyref(new XVar("record_header"), (XVar)(record_header));
			this.xt.assignbyref(new XVar("record_footer"), (XVar)(record_footer));
			this.xt.assign(new XVar("grid_header"), new XVar(true));
			this.xt.assign(new XVar("grid_footer"), new XVar(true));
			if(XVar.Pack(!(XVar)(this.numRowsFromSQL)))
			{
				this.xt.assign(new XVar("gridHeader_class"), (XVar)(MVCFunctions.Concat(" ", makeClassName(new XVar("hiddenelem")))));
				this.xt.assign(new XVar("gridFooter_class"), (XVar)(MVCFunctions.Concat(" ", makeClassName(new XVar("hiddenelem")))));
			}
			gridTableStyle = new XVar("");
			gridTableStyle = new XVar("style=\"");
			gridTableStyle = MVCFunctions.Concat(gridTableStyle, (XVar.Pack(0 < this.recordsOnPage) ? XVar.Pack("\"") : XVar.Pack("width: 50%;\"")));
			this.xt.assign(new XVar("gridTable_attrs"), (XVar)(gridTableStyle));
			checkboxColumnAttrs();
			if(XVar.Pack(editAvailable()))
			{
				this.xt.assign(new XVar("edit_column"), new XVar(true));
				this.xt.assign(new XVar("edit_headercolumn"), new XVar(true));
				this.xt.assign(new XVar("edit_footercolumn"), new XVar(true));
			}
			if(XVar.Pack(inlineEditAvailable()))
			{
				this.xt.assign(new XVar("inlineedit_column"), new XVar(true));
				this.xt.assign(new XVar("inlineedit_headercolumn"), new XVar(true));
				this.xt.assign(new XVar("inlineedit_footercolumn"), new XVar(true));
			}
			if(XVar.Pack(copyAvailable()))
			{
				this.xt.assign(new XVar("copy_column"), new XVar(true));
			}
			if(XVar.Pack(displayViewLink()))
			{
				this.xt.assign(new XVar("view_column"), new XVar(true));
			}
			assignListIconsColumn();
			if(XVar.Pack(detailsInGridAvailable()))
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.tName)), "_dtable_column")), new XVar(true));
			}
			deleteSelectedLink();
			return null;
		}
		public virtual XVar createMapDiv(dynamic var_params)
		{
			dynamic div = null;
			div = XVar.Clone(MVCFunctions.Concat("<div id=\"", var_params["mapId"], "\" style=\"width: ", var_params["width"], "px; height: ", var_params["height"], "px;\"></div>"));
			MVCFunctions.Echo(div);
			return null;
		}
		public virtual XVar importLinksAttrs()
		{
			this.xt.assign(new XVar("import_link"), (XVar)(this.permis[this.tName]["import"]));
			this.xt.assign(new XVar("importlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"import_", this.id, "\" name=\"import_", this.id, "\"")));
			return null;
		}
		public virtual XVar getInlineAddLinksAttrs()
		{
			return MVCFunctions.Concat("name=\"inlineAdd_", this.id, "\" href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("add")), "' id=\"inlineAdd", this.id, "\"");
		}
		public virtual XVar getAddLinksAttrs()
		{
			return MVCFunctions.Concat("href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("add")), "' id=\"addButton", this.id, "\"");
		}
		public virtual XVar inlineAddLinksAttrs()
		{
			if(XVar.Pack(!(XVar)(inlineAddAvailable())))
			{
				return null;
			}
			this.xt.assign(new XVar("inlineadd_link"), new XVar(true));
			this.xt.assign(new XVar("inlineaddlink_attrs"), (XVar)(getInlineAddLinksAttrs()));
			return null;
		}
		public virtual XVar selectAllLinkAttrs()
		{
			this.xt.assign(new XVar("selectall_link"), (XVar)((XVar)((XVar)(deleteAvailable())  || (XVar)(this.permis[this.tName]["export"]))  || (XVar)(this.permis[this.tName]["edit"])));
			this.xt.assign(new XVar("selectalllink_span"), (XVar)(buttonShowHideStyle()));
			this.xt.assign(new XVar("selectalllink_attrs"), (XVar)(MVCFunctions.Concat("name=\"select_all", this.id, "\" \r\n\t\t\tid=\"select_all", this.id, "\" \r\n\t\t\thref=\"#\"")));
			return null;
		}
		public virtual XVar checkboxColumnAttrs()
		{
			dynamic showColumn = null;
			showColumn = XVar.Clone((XVar)((XVar)((XVar)((XVar)(deleteAvailable())  || (XVar)(exportAvailable()))  || (XVar)(inlineEditAvailable()))  || (XVar)(printAvailable()))  || (XVar)(updateSelectedAvailable()));
			this.xt.assign(new XVar("checkbox_column"), (XVar)(showColumn));
			this.xt.assign(new XVar("checkbox_header"), new XVar(true));
			this.xt.assign(new XVar("checkboxheader_attrs"), (XVar)(MVCFunctions.Concat("id=\"chooseAll_", this.id, "\" class=\"chooseAll", this.id, "\"")));
			return null;
		}
		public virtual XVar getPrintExportLinkAttrs(dynamic _param_page)
		{
			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			#endregion

			if(XVar.Pack(!(XVar)(page)))
			{
				return "";
			}
			return MVCFunctions.Concat("name=\"", page, "_selected", this.id, "\" \r\n\t\t\t\tid=\"", page, "_selected", this.id, "\"\r\n\t\t\t\thref = '", MVCFunctions.GetTableLink((XVar)(this.shortTableName), (XVar)(page)), "'");
		}
		public virtual XVar buttonShowHideStyle(dynamic _param_link = null)
		{
			#region default values
			if(_param_link as Object == null) _param_link = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic link = XVar.Clone(_param_link);
			#endregion

			if((XVar)(link == "saveall")  || (XVar)(link == "cancelall"))
			{
				return " style=\"display:none;\" ";
			}
			return (XVar.Pack(0 < this.numRowsFromSQL) ? XVar.Pack("") : XVar.Pack(" style=\"display:none;\" "));
		}
		public virtual XVar editSelectedLinkAttrs()
		{
			if(XVar.Pack(!(XVar)(inlineEditAvailable())))
			{
				return null;
			}
			this.xt.assign(new XVar("editselected_link"), new XVar(true));
			this.xt.assign(new XVar("editselectedlink_span"), (XVar)(buttonShowHideStyle()));
			this.xt.assign(new XVar("editselectedlink_attrs"), (XVar)(MVCFunctions.Concat("\r\n\t\t\t\t\thref='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit")), "' \r\n\t\t\t\t\tname=\"edit_selected", this.id, "\" \r\n\t\t\t\t\tid=\"edit_selected", this.id, "\"")));
			return null;
		}
		public virtual XVar updateSelectedLinkAttrs()
		{
			if(XVar.Pack(!(XVar)(updateSelectedAvailable())))
			{
				return null;
			}
			this.xt.assign(new XVar("updateselected_link"), new XVar(true));
			this.xt.assign(new XVar("updateselectedlink_attrs"), (XVar)(MVCFunctions.Concat(getUpdateSelectedAttrs(), buttonShowHideStyle())));
			return null;
		}
		protected virtual XVar getUpdateSelectedAttrs()
		{
			return MVCFunctions.Concat("href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit")), "' name=\"update_selected", this.id, "\" id=\"update_selected", this.id, "\"");
		}
		public virtual XVar saveAllLinkAttrs()
		{
			this.xt.assign(new XVar("saveall_link"), (XVar)((XVar)(this.permis[this.tName]["edit"])  || (XVar)(this.permis[this.tName]["add"])));
			this.xt.assign(new XVar("savealllink_span"), (XVar)(buttonShowHideStyle(new XVar("saveall"))));
			this.xt.assign(new XVar("savealllink_attrs"), (XVar)(MVCFunctions.Concat("name=\"saveall_edited", this.id, "\" id=\"saveall_edited", this.id, "\"")));
			return null;
		}
		public virtual XVar cancelAllLinkAttrs()
		{
			this.xt.assign(new XVar("cancelall_link"), (XVar)((XVar)(this.permis[this.tName]["edit"])  || (XVar)(this.permis[this.tName]["add"])));
			this.xt.assign(new XVar("cancelalllink_span"), (XVar)(buttonShowHideStyle(new XVar("cancelall"))));
			this.xt.assign(new XVar("cancelalllink_attrs"), (XVar)(MVCFunctions.Concat("name=\"revertall_edited", this.id, "\" id=\"revertall_edited", this.id, "\"")));
			return null;
		}
		public virtual XVar deleteSelectedLink()
		{
			if(XVar.Pack(!(XVar)(deleteAvailable())))
			{
				return null;
			}
			this.xt.assign(new XVar("deleteselected_link"), new XVar(true));
			this.xt.assign(new XVar("deleteselectedlink_span"), (XVar)(buttonShowHideStyle()));
			this.xt.assign(new XVar("deleteselectedlink_attrs"), (XVar)(getDeleteLinksAttrs()));
			return null;
		}
		public virtual XVar getDeleteLinksAttrs()
		{
			return MVCFunctions.Concat("id=\"delete_selected", this.id, "\" name=\"delete_selected", this.id, "\" ");
		}
		public virtual XVar getEditLinksAttrs()
		{
			return MVCFunctions.Concat("id=\"edit_selected", this.id, "\" name=\"edit_selected", this.id, "\" ");
		}
		public virtual XVar getFormInputsHTML()
		{
			return "";
		}
		public virtual XVar getFormTargetHTML()
		{
			return "";
		}
		protected virtual XVar seekPageInRecSet(dynamic _param_strSQL)
		{
			#region pass-by-value parameters
			dynamic strSQL = XVar.Clone(_param_strSQL);
			#endregion

			dynamic listarray = null;
			listarray = new XVar(false);
			if(XVar.Pack(eventExists(new XVar("ListQuery"))))
			{
				dynamic orderData = XVar.Array();
				orderData = XVar.Clone(this.orderClause.getListQueryData());
				listarray = XVar.Clone(this.eventsObject.ListQuery((XVar)(this.searchClauseObj), (XVar)(orderData["fieldsForSort"]), (XVar)(orderData["howToSortData"]), (XVar)(this.masterTable), (XVar)(this.masterKeysReq), new XVar(null), (XVar)(this.pageSize), (XVar)(this.myPage), this));
			}
			if(!XVar.Equals(XVar.Pack(listarray), XVar.Pack(false)))
			{
				this.recSet = XVar.Clone(listarray);
			}
			else
			{
				this.recSet = XVar.Clone(this.connection.queryPage((XVar)(strSQL), (XVar)(this.myPage), (XVar)(this.pageSize), (XVar)(1 < this.maxPages)));
			}
			return null;
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic detailsSubqueries = null, sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ROW_NUMBER() over () as DB2_ROW_NUMBER ");
			}
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Search"), (XVar)(this.tName)), "mandatoryWhere", null);
			if((XVar)((XVar)(this.mode != Constants.LIST_DETAILS)  && (XVar)(this.noRecordsFirstPage))  && (XVar)(!(XVar)(isSearchFunctionalityActivated())))
			{
				sql.InitAndSetArrayItem("1=0", "mandatoryWhere", null);
			}
			detailsSubqueries = XVar.Clone(getMasterDetailSubQuery());
			if(XVar.Pack(detailsSubqueries))
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ", MVCFunctions.implode(new XVar(", "), (XVar)(detailsSubqueries)));
			}
			return sql;
		}
		public override XVar getOrderByClause()
		{
			return this.orderClause.getOrderByExpression();
		}
		public virtual XVar buildSQL()
		{
			dynamic countSQL = null, orderbyModifiedInEvent = null, rowcount = null, sql = XVar.Array(), sqlModifiedInEvent = null, strOrderBy = null, strSQLbak = null, whereModifiedInEvent = null;
			sql = XVar.Clone(getSubsetSQLComponents());
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			countSQL = XVar.Clone(GlobalVars.strSQL);
			strOrderBy = XVar.Clone(getOrderByClause());
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", MVCFunctions.trim((XVar)(strOrderBy)));
			strSQLbak = XVar.Clone(GlobalVars.strSQL);
			sqlModifiedInEvent = new XVar(false);
			whereModifiedInEvent = new XVar(false);
			orderbyModifiedInEvent = new XVar(false);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryList"))))
			{
				dynamic strWhereBak = null, tstrOrderBy = null, tstrWhereClause = null;
				tstrWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				strWhereBak = XVar.Clone(tstrWhereClause);
				tstrOrderBy = XVar.Clone(strOrderBy);
				this.eventsObject.BeforeQueryList((XVar)(GlobalVars.strSQL), ref tstrWhereClause, ref tstrOrderBy, this);
				whereModifiedInEvent = XVar.Clone(tstrWhereClause != strWhereBak);
				orderbyModifiedInEvent = XVar.Clone(tstrOrderBy != strOrderBy);
				sqlModifiedInEvent = XVar.Clone(GlobalVars.strSQL != strSQLbak);
				strOrderBy = XVar.Clone(tstrOrderBy);
				if(XVar.Pack(sqlModifiedInEvent))
				{
					this.numRowsFromSQL = XVar.Clone(CommonFunctions.GetRowCount((XVar)(GlobalVars.strSQL), (XVar)(this.connection)));
				}
				else
				{
					if(XVar.Pack(whereModifiedInEvent))
					{
						GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, tstrWhereClause)), (XVar)(sql["mandatoryHaving"])));
						countSQL = XVar.Clone(GlobalVars.strSQL);
						GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", MVCFunctions.trim((XVar)(strOrderBy)));
					}
					else
					{
						if(XVar.Pack(orderbyModifiedInEvent))
						{
							GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
							countSQL = XVar.Clone(GlobalVars.strSQL);
							GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", MVCFunctions.trim((XVar)(strOrderBy)));
						}
					}
				}
			}
			rowcount = new XVar(false);
			if(XVar.Pack(eventExists(new XVar("ListGetRowCount"))))
			{
				rowcount = XVar.Clone(this.eventsObject.ListGetRowCount((XVar)(this.searchClauseObj), (XVar)(this.masterTable), (XVar)(this.masterKeysReq), new XVar(null), this));
				if(!XVar.Equals(XVar.Pack(rowcount), XVar.Pack(false)))
				{
					this.numRowsFromSQL = XVar.Clone(rowcount);
				}
			}
			if((XVar)(XVar.Equals(XVar.Pack(rowcount), XVar.Pack(false)))  && (XVar)(!(XVar)(sqlModifiedInEvent)))
			{
				this.numRowsFromSQL = XVar.Clone(this.connection.getFetchedRowsNumber((XVar)(countSQL)));
			}
			this.numRowsFromSQL = XVar.Clone(limitRowCount((XVar)(this.numRowsFromSQL)));
			if(XVar.Pack(useDetailsCountBySubquery()))
			{
				sql = XVar.Clone(getSubsetSQLComponents());
				GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", MVCFunctions.trim((XVar)(getOrderByClause())));
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			this.querySQL = XVar.Clone(GlobalVars.strSQL);
			return null;
		}
		protected override XVar getRowCountByTab(dynamic _param_tab)
		{
			#region pass-by-value parameters
			dynamic tab = XVar.Clone(_param_tab);
			#endregion

			dynamic countSQL = null, sql = XVar.Array();
			sql = XVar.Clone(getTabSQLComponents((XVar)(tab)));
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			countSQL = XVar.Clone(GlobalVars.strSQL);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryList"))))
			{
				dynamic sqlModifiedInEvent = null, strSQLbak = null, strWhereBak = null, tstrOrderBy = null, tstrWhereClause = null, whereModifiedInEvent = null;
				strSQLbak = XVar.Clone(GlobalVars.strSQL);
				sqlModifiedInEvent = new XVar(false);
				whereModifiedInEvent = new XVar(false);
				tstrWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				strWhereBak = XVar.Clone(tstrWhereClause);
				tstrOrderBy = new XVar("");
				this.eventsObject.BeforeQueryList((XVar)(GlobalVars.strSQL), ref tstrWhereClause, ref tstrOrderBy, this);
				whereModifiedInEvent = XVar.Clone(tstrWhereClause != strWhereBak);
				sqlModifiedInEvent = XVar.Clone(GlobalVars.strSQL != strSQLbak);
				if(XVar.Pack(sqlModifiedInEvent))
				{
					return limitRowCount((XVar)(CommonFunctions.GetRowCount((XVar)(GlobalVars.strSQL), (XVar)(this.connection))));
				}
				if(XVar.Pack(whereModifiedInEvent))
				{
					countSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, tstrWhereClause)), (XVar)(sql["mandatoryHaving"])));
				}
			}
			return limitRowCount((XVar)(this.connection.getFetchedRowsNumber((XVar)(countSQL))));
		}
		protected virtual XVar getMasterDetailSubQuery()
		{
			dynamic countsql = null, dataSourceTName = null, detailData = XVar.Array(), detailsQuery = null, detailsSettings = null, detailsSqlWhere = null, i = null, masterWhere = null, ret = XVar.Array(), securityClause = null, subQ = null;
			if(this.numRowsFromSQL == 0)
			{
				return XVar.Array();
			}
			ret = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				detailData = XVar.Clone(this.allDetailsTablesArr[i]);
				if((XVar)(!(XVar)(detailData["dispChildCount"]))  && (XVar)(!(XVar)(detailData["hideChild"])))
				{
					continue;
				}
				dataSourceTName = XVar.Clone(detailData["dDataSourceTable"]);
				if(XVar.Pack(!(XVar)(isDetailTableSubquerySupported((XVar)(dataSourceTName), (XVar)(i)))))
				{
					continue;
				}
				detailsSettings = XVar.Clone(this.pSet.getTable((XVar)(dataSourceTName)));
				detailsQuery = XVar.Clone(detailsSettings.getSQLQuery());
				detailsSqlWhere = XVar.Clone(detailsQuery.WhereToSql());
				masterWhere = new XVar("");
				foreach (KeyValuePair<XVar, dynamic> val in this.masterKeysByD[i].GetEnumerator())
				{
					if(XVar.Pack(masterWhere))
					{
						masterWhere = MVCFunctions.Concat(masterWhere, " and ");
					}
					masterWhere = MVCFunctions.Concat(masterWhere, this.cipherer.GetFieldName((XVar)(MVCFunctions.Concat(this.connection.addTableWrappers(new XVar("subQuery_cnt")), ".", this.connection.addFieldWrappers((XVar)(this.detailKeysByD[i][val.Key])))), (XVar)(this.masterKeysByD[i][val.Key])), "=", this.cipherer.GetFieldName((XVar)(MVCFunctions.Concat(this.connection.addTableWrappers((XVar)(this.origTName)), ".", this.connection.addFieldWrappers((XVar)(this.masterKeysByD[i][val.Key])))), (XVar)(this.masterKeysByD[i][val.Key])));
				}
				subQ = new XVar("");
				foreach (KeyValuePair<XVar, dynamic> k in this.detailKeysByD[i].GetEnumerator())
				{
					if(XVar.Pack(MVCFunctions.strlen((XVar)(subQ))))
					{
						subQ = MVCFunctions.Concat(subQ, ",");
					}
					subQ = MVCFunctions.Concat(subQ, RunnerPage._getFieldSQL((XVar)(k.Value), (XVar)(this.connection), (XVar)(detailsSettings)));
				}
				subQ = XVar.Clone(MVCFunctions.Concat("SELECT ", subQ, " ", detailsQuery.FromToSql()));
				securityClause = XVar.Clone(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(dataSourceTName)));
				if(XVar.Pack(MVCFunctions.strlen((XVar)(securityClause))))
				{
					subQ = MVCFunctions.Concat(subQ, " WHERE ", CommonFunctions.whereAdd((XVar)(detailsSqlWhere), (XVar)(securityClause)));
				}
				else
				{
					if(XVar.Pack(MVCFunctions.strlen((XVar)(detailsSqlWhere))))
					{
						subQ = MVCFunctions.Concat(subQ, " WHERE ", CommonFunctions.whereAdd(new XVar(""), (XVar)(detailsSqlWhere)));
					}
				}
				subQ = MVCFunctions.Concat(subQ, " ", detailsQuery.GroupByHavingToSql());
				countsql = XVar.Clone(MVCFunctions.Concat("SELECT count(*) FROM (", subQ, ") ", this.connection.addTableWrappers(new XVar("subQuery_cnt")), " WHERE ", masterWhere));
				ret.InitAndSetArrayItem(MVCFunctions.Concat("(", countsql, ") as ", this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat(dataSourceTName, "_cnt")))), null);
				this.addedDetailsCountSubqueries.InitAndSetArrayItem(true, dataSourceTName);
			}
			return ret;
		}
		protected override XVar isDetailTableSubquerySupported(dynamic _param_dDataSourceTName, dynamic _param_dTableIndex)
		{
			#region pass-by-value parameters
			dynamic dDataSourceTName = XVar.Clone(_param_dDataSourceTName);
			dynamic dTableIndex = XVar.Clone(_param_dTableIndex);
			#endregion

			return (XVar)((XVar)((XVar)((XVar)(GlobalVars.bSubqueriesSupported)  && (XVar)(this.connection.checkDBSubqueriesSupport()))  && (XVar)(GlobalVars.cman.checkTablesSubqueriesSupport((XVar)(this.tName), (XVar)(dDataSourceTName))))  && (XVar)(checkfDMLinkFieldsOfTheSameType((XVar)(dDataSourceTName), (XVar)(dTableIndex))))  && (XVar)(!(XVar)(this.gQuery.HasJoinInFromClause()));
		}
		protected virtual XVar useDetailsCountBySubquery()
		{
			dynamic manyPages = null;
			manyPages = new XVar(false);
			if(0 < this.numRowsFromSQL)
			{
				manyPages = XVar.Clone(this.pageSize / this.numRowsFromSQL < 0.100000);
			}
			return (XVar)((XVar)((XVar)(0 < MVCFunctions.count(this.allDetailsTablesArr))  && (XVar)((XVar)(MVCFunctions.max((XVar)(this.pageSize), (XVar)(this.numRowsFromSQL)) <= 20)  || (XVar)(manyPages)))  && (XVar)(!(XVar)(eventExists(new XVar("BeforeQueryList")))))  && (XVar)(!(XVar)(eventExists(new XVar("ListGetRowCount"))));
		}
		protected override XVar isDetailTableSubqueryApplied(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return this.addedDetailsCountSubqueries[table];
		}
		protected virtual XVar checkfDMLinkFieldsOfTheSameType(dynamic _param_dDataSourceTName, dynamic _param_dTableIndex)
		{
			#region pass-by-value parameters
			dynamic dDataSourceTName = XVar.Clone(_param_dDataSourceTName);
			dynamic dTableIndex = XVar.Clone(_param_dTableIndex);
			#endregion

			if(this.allDetailsTablesArr[dTableIndex]["dDataSourceTable"] != dDataSourceTName)
			{
				return false;
			}
			if(this.connection.dbType == Constants.nDATABASE_MySQL)
			{
				return true;
			}
			foreach (KeyValuePair<XVar, dynamic> val in this.masterKeysByD[dTableIndex].GetEnumerator())
			{
				dynamic detailLinkFieldType = null, masterLinkFieldType = null;
				masterLinkFieldType = XVar.Clone(this.pSet.getFieldType((XVar)(this.masterKeysByD[dTableIndex][val.Key])));
				detailLinkFieldType = XVar.Clone(this.pSet.getFieldType((XVar)(this.detailKeysByD[dTableIndex][val.Key])));
				if(masterLinkFieldType != detailLinkFieldType)
				{
					return false;
				}
			}
			return true;
		}
		protected virtual XVar isInlineAreaToSet()
		{
			return (XVar)(inlineAddAvailable())  || (XVar)((XVar)(addAvailable())  && (XVar)(this.showAddInPopup));
		}
		public virtual XVar prepareInlineAddArea(dynamic rowInfoArr)
		{
			dynamic copylink = null, dDataSourceTable = null, dShortTable = null, editlink = null, field = null, gFieldName = null, hideDPLink = null, htmlAttributes = XVar.Array(), i = null, rec = XVar.Array(), record = XVar.Array(), row = XVar.Array();
			rowInfoArr.InitAndSetArrayItem(XVar.Array(), "data");
			if(XVar.Pack(!(XVar)(isInlineAreaToSet())))
			{
				return null;
			}
			editlink = new XVar("");
			copylink = new XVar("");
			row = XVar.Clone(XVar.Array());
			row.InitAndSetArrayItem(MVCFunctions.Concat("gridRowAdd ", makeClassName(new XVar("hiddenelem"))), "rowclass");
			row.InitAndSetArrayItem(MVCFunctions.Concat("gridRowSepAdd ", makeClassName(new XVar("hiddenelem"))), "rsclass");
			if(this.listGridLayout == Constants.gltVERTICAL)
			{
				row["rowattrs"] = MVCFunctions.Concat(row["rowattrs"], "vertical=\"1\"");
			}
			record = XVar.Clone(XVar.Array());
			record.InitAndSetArrayItem(true, "edit_link");
			record.InitAndSetArrayItem(true, "inlineedit_link");
			record.InitAndSetArrayItem(true, "view_link");
			record.InitAndSetArrayItem(true, "copy_link");
			record.InitAndSetArrayItem(true, "checkbox");
			record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"editLink_add", this.id, "\" data-gridlink"), "editlink_attrs");
			record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"copyLink_add", this.id, "\" name=\"copyLink_add", this.id, "\" data-gridlink"), "copylink_attrs");
			record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"viewLink_add", this.id, "\" name=\"viewLink_add", this.id, "\" data-gridlink"), "viewlink_attrs");
			record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"check_add", this.id, "\" name=\"selection[]\""), "checkbox_attrs");
			if(XVar.Pack(inlineEditAvailable()))
			{
				record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"inlineEdit_add", this.id, "\" "), "inlineeditlink_attrs");
			}
			record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"ieditbuttonsholder", this.id, "\" "), "ieditbuttonsholder_attrs");
			if(XVar.Pack(detailsInGridAvailable()))
			{
				record.InitAndSetArrayItem(true, "dtables_link");
			}
			hideDPLink = new XVar(false);
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				dDataSourceTable = XVar.Clone(this.allDetailsTablesArr[i]["dDataSourceTable"]);
				dShortTable = XVar.Clone(this.allDetailsTablesArr[i]["dShortTable"]);
				record.InitAndSetArrayItem((XVar)(this.permis[dDataSourceTable]["add"])  || (XVar)(this.permis[dDataSourceTable]["search"]), MVCFunctions.Concat(dShortTable, "_dtable_link"));
				if(XVar.Pack(this.allDetailsTablesArr[i]["dispChildCount"]))
				{
					record.InitAndSetArrayItem(MVCFunctions.Concat(" id='cntDet_", dShortTable, "_'"), MVCFunctions.Concat(dShortTable, "_childnumber_attr"));
					record.InitAndSetArrayItem(MVCFunctions.Concat(" href=\"#\" id=\"details_add", this.id, "_", dShortTable, "\" "), MVCFunctions.Concat(dShortTable, "_link_attrs"));
					record.InitAndSetArrayItem(true, MVCFunctions.Concat(dShortTable, "_childcount"));
				}
				htmlAttributes = XVar.Clone(XVar.Array());
				record.InitAndSetArrayItem("", MVCFunctions.Concat(dShortTable, "_dtablelink_attrs"));
				htmlAttributes.InitAndSetArrayItem(MVCFunctions.Concat(MVCFunctions.GetTableLink((XVar)(dShortTable), new XVar("list")), "?"), "href");
				htmlAttributes.InitAndSetArrayItem(MVCFunctions.Concat(dShortTable, "_preview", this.id), "id");
				htmlAttributes.InitAndSetArrayItem("display:none;", "style");
				if(this.allDetailsTablesArr[i]["previewOnList"] == Constants.DP_INLINE)
				{
					htmlAttributes.InitAndSetArrayItem(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(dDataSourceTable)))), "caption");
					htmlAttributes.InitAndSetArrayItem(this.allDetailsTablesArr[i]["dType"], "data-pagetype");
				}
				foreach (KeyValuePair<XVar, dynamic> value in htmlAttributes.GetEnumerator())
				{
					record[MVCFunctions.Concat(dShortTable, "_dtablelink_attrs")] = MVCFunctions.Concat(record[MVCFunctions.Concat(dShortTable, "_dtablelink_attrs")], value.Key, "=\"", value.Value, "\" ");
				}
				if((XVar)(this.allDetailsTablesArr[i]["hideChild"])  && (XVar)(!(XVar)(hideDPLink)))
				{
					hideDPLink = new XVar(true);
				}
			}
			record.InitAndSetArrayItem(MVCFunctions.Concat(" href=\"#\" id=\"details_add", this.id, "\" "), "dtables_link_attrs");
			if(XVar.Pack(hideDPLink))
			{
				record["dtables_link_attrs"] = MVCFunctions.Concat(record["dtables_link_attrs"], " class=\"", makeClassName(new XVar("hiddenelem")), "\"");
			}
			addSpansForGridCells(new XVar("add"), (XVar)(record));
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				field = XVar.Clone(this.listFields[i]["fName"]);
				gFieldName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
				record[MVCFunctions.Concat(gFieldName, "_class")] = MVCFunctions.Concat(record[MVCFunctions.Concat(gFieldName, "_class")], fieldClass((XVar)(field)));
				addHiddenColumnClasses((XVar)(record), (XVar)(field));
			}
			if(1 < this.colsOnPage)
			{
				record.InitAndSetArrayItem(true, "endrecord_block");
			}
			record.InitAndSetArrayItem(true, "grid_recordheader");
			record.InitAndSetArrayItem(true, "grid_vrecord");
			row.InitAndSetArrayItem(new XVar("data", XVar.Array()), "grid_record");
			setRowsGridRecord((XVar)(row), (XVar)(record));
			i = new XVar(1);
			for(;i < this.colsOnPage; i++)
			{
				rec = XVar.Clone(XVar.Array());
				if(i < this.colsOnPage - 1)
				{
					rec.InitAndSetArrayItem(true, "endrecord_block");
				}
				if(XVar.Pack(row["grid_record"]["data"]))
				{
					row.InitAndSetArrayItem(rec, "grid_record", "data", null);
				}
			}
			row.InitAndSetArrayItem(true, "grid_rowspace");
			row.InitAndSetArrayItem(new XVar("data", XVar.Array()), "grid_recordspace");
			i = new XVar(0);
			for(;i < this.colsOnPage * 2 - 1; i++)
			{
				row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
			}
			rowInfoArr.InitAndSetArrayItem(row, "data", null);
			return null;
		}
		public virtual XVar beforeProccessRow()
		{
			dynamic data = null;
			if(XVar.Pack(eventExists(new XVar("ListFetchArray"))))
			{
				data = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(this.recSet), this));
			}
			else
			{
				data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.recSet.fetchAssoc())));
			}
			while(XVar.Pack(data))
			{
				if(XVar.Pack(eventExists(new XVar("BeforeProcessRowList"))))
				{
					dynamic result = null;
					RunnerContext.pushRecordContext((XVar)(data), this);
					result = XVar.Clone(this.eventsObject.BeforeProcessRowList((XVar)(data), this));
					RunnerContext.pop();
					if(XVar.Pack(!(XVar)(result)))
					{
						if(XVar.Pack(eventExists(new XVar("ListFetchArray"))))
						{
							data = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(this.recSet), this));
						}
						else
						{
							data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.recSet.fetchAssoc())));
						}
						continue;
					}
				}
				return data;
			}
			return null;
		}
		public virtual XVar assignListIconsColumn()
		{
			if((XVar)((XVar)((XVar)(inlineEditAvailable())  || (XVar)(inlineAddAvailable()))  || (XVar)(displayViewLink()))  || (XVar)(editAvailable()))
			{
				this.xt.assign(new XVar("listIcons_column"), new XVar(true));
			}
			return null;
		}
		public virtual XVar fillGridData()
		{
			dynamic col = null, copylink = null, currentPageSize = null, data = XVar.Array(), editlink = null, field = null, gridRowInd = null, i = null, isEditable = null, keyblock = null, keylink = null, keys = XVar.Array(), lockRecIds = XVar.Array(), record = XVar.Array(), row = XVar.Array(), rowinfo = XVar.Array(), tKeys = XVar.Array(), totals = null;
			totals = XVar.Clone(XVar.Array());
			rowinfo = XVar.Clone(XVar.Array());
			prepareInlineAddArea((XVar)(rowinfo));
			setDetailsBadgeStyles();
			data = XVar.Clone(beforeProccessRow());
			lockRecIds = XVar.Clone(XVar.Array());
			tKeys = XVar.Clone(this.pSet.getTableKeys());
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows");
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				this.recordFieldTypes.InitAndSetArrayItem(this.pSet.getFieldType((XVar)(this.listFields[i]["fName"])), this.listFields[i]["fName"]);
			}
			currentPageSize = XVar.Clone(this.pageSize);
			if((XVar)(this.pSet.getRecordsLimit())  && (XVar)(this.maxPages == this.myPage))
			{
				currentPageSize = XVar.Clone(this.pSet.getRecordsLimit() - (this.myPage - 1) * this.pageSize);
			}
			while((XVar)(data)  && (XVar)((XVar)(this.recNo <= currentPageSize)  || (XVar)(currentPageSize == -1)))
			{
				row = XVar.Clone(XVar.Array());
				row.InitAndSetArrayItem(XVar.Array(), "grid_record");
				row.InitAndSetArrayItem(XVar.Array(), "grid_record", "data");
				this.rowId++;
				col = new XVar(1);
				for(;(XVar)((XVar)(data)  && (XVar)((XVar)(this.recNo <= currentPageSize)  || (XVar)(currentPageSize == -1)))  && (XVar)(col <= this.colsOnPage); col++)
				{
					countTotals((XVar)(totals), (XVar)(data));
					record = XVar.Clone(XVar.Array());
					genId();
					row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"gridRow", this.recId, "\""), "rowattrs");
					gridRowInd = XVar.Clone(MVCFunctions.count(this.controlsMap["gridRows"]));
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd);
					this.controlsMap.InitAndSetArrayItem(this.recId, "gridRows", gridRowInd, "id");
					this.controlsMap.InitAndSetArrayItem(gridRowInd, "gridRows", gridRowInd, "rowInd");
					this.controlsMap.InitAndSetArrayItem((this.recId + this.colsOnPage) - col, "gridRows", gridRowInd, "contextRowId");
					isEditable = XVar.Clone((XVar)((XVar)(this.permis[this.tName]["edit"])  && (XVar)(CommonFunctions.CheckSecurity((XVar)(data[this.mainTableOwnerID]), new XVar("Edit"), (XVar)(this.tName))))  || (XVar)((XVar)(this.permis[this.tName]["delete"])  && (XVar)(CommonFunctions.CheckSecurity((XVar)(data[this.mainTableOwnerID]), new XVar("Delete"), (XVar)(this.tName)))));
					if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
					{
						isEditable = XVar.Clone(GlobalVars.globalEvents.IsRecordEditable((XVar)(data), (XVar)(isEditable), (XVar)(this.tName)));
					}
					this.controlsMap.InitAndSetArrayItem(isEditable, "gridRows", gridRowInd, "isEditOwnRow");
					this.controlsMap.InitAndSetArrayItem(this.listGridLayout, "gridRows", gridRowInd, "gridLayout");
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd, "keyFields");
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd, "keys");
					i = new XVar(0);
					for(;i < MVCFunctions.count(tKeys); i++)
					{
						this.controlsMap.InitAndSetArrayItem(tKeys[i], "gridRows", gridRowInd, "keyFields", i);
						this.controlsMap.InitAndSetArrayItem(data[tKeys[i]], "gridRows", gridRowInd, "keys", i);
					}
					record.InitAndSetArrayItem(isEditable, "edit_link");
					record.InitAndSetArrayItem(isEditable, "inlineedit_link");
					record.InitAndSetArrayItem(this.permis[this.tName]["search"], "view_link");
					record.InitAndSetArrayItem(this.permis[this.tName]["add"], "copy_link");
					if(XVar.Pack(this.lockingObj))
					{
						dynamic lockDelRec = null;
						if((XVar)((XVar)(this.mode == Constants.LIST_SIMPLE)  && (XVar)(!(XVar)(MVCFunctions.count(this.lockDelRec))))  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_lockDelRec"))))
						{
							this.lockDelRec = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_lockDelRec")]);
							XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_lockDelRec"));
						}
						i = new XVar(0);
						for(;i < MVCFunctions.count(this.lockDelRec); i++)
						{
							lockDelRec = new XVar(true);
							foreach (KeyValuePair<XVar, dynamic> val in this.lockDelRec[i].GetEnumerator())
							{
								if(data[val.Key] != val.Value)
								{
									lockDelRec = new XVar(false);
									break;
								}
							}
							if(XVar.Pack(lockDelRec))
							{
								lockRecIds.InitAndSetArrayItem(this.recId, null);
								break;
							}
						}
					}
					proccessDetailGridInfo((XVar)(record), (XVar)(data), (XVar)(gridRowInd));
					keyblock = new XVar("");
					editlink = new XVar("");
					copylink = new XVar("");
					keylink = new XVar("");
					keys = XVar.Clone(XVar.Array());
					i = new XVar(0);
					for(;i < MVCFunctions.count(tKeys); i++)
					{
						if(i != XVar.Pack(0))
						{
							keyblock = MVCFunctions.Concat(keyblock, "&");
							editlink = MVCFunctions.Concat(editlink, "&");
							copylink = MVCFunctions.Concat(copylink, "&");
						}
						keyblock = MVCFunctions.Concat(keyblock, MVCFunctions.RawUrlEncode((XVar)(data[tKeys[i]])));
						editlink = MVCFunctions.Concat(editlink, "editid", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[tKeys[i]])))));
						copylink = MVCFunctions.Concat(copylink, "copyid", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[tKeys[i]])))));
						keylink = MVCFunctions.Concat(keylink, "&key", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[tKeys[i]])))));
						keys.InitAndSetArrayItem(data[tKeys[i]], i);
					}
					this.recIds.InitAndSetArrayItem(this.recId, null);
					record.InitAndSetArrayItem(MVCFunctions.Concat("data-record-id=\"", this.recId, "\""), "recordattrs");
					record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"editLink", this.recId, "\" name=\"editLink", this.recId, "\" href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit"), (XVar)(editlink)), "' data-gridlink"), "editlink_attrs");
					record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"copyLink", this.recId, "\" name=\"copyLink", this.recId, "\" href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("add"), (XVar)(copylink)), "' data-gridlink"), "copylink_attrs");
					record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"viewLink", this.recId, "\" name=\"viewLink", this.recId, "\" href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("view"), (XVar)(editlink)), "' data-gridlink"), "viewlink_attrs");
					record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"iEditLink", this.recId, "\" name=\"iEditLink", this.recId, "\" href='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit"), (XVar)(editlink)), "' data-gridlink"), "inlineeditlink_attrs");
					record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"ieditbuttonsholder", this.recId, "\" "), "ieditbuttonsholder_attrs");
					if(XVar.Pack(mobileTemplateMode()))
					{
						if(XVar.Pack(displayViewLink()))
						{
							record["recordattrs"] = MVCFunctions.Concat(record["recordattrs"], " data-viewlink='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("view"), (XVar)(editlink)), "'");
						}
						if((XVar)(editAvailable())  && (XVar)(isEditable))
						{
							record["recordattrs"] = MVCFunctions.Concat(record["recordattrs"], " data-editlink='", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit"), (XVar)(editlink)), "'");
						}
					}
					fillCheckAttr((XVar)(record), (XVar)(data), (XVar)(keyblock));
					if(XVar.Pack(detailsInGridAvailable()))
					{
						record.InitAndSetArrayItem(true, "dtables_link");
					}
					if(XVar.Pack(hasBigMap()))
					{
						addBigGoogleMapMarkers((XVar)(data), (XVar)(keys), (XVar)(editlink));
					}
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.listFields); i++)
					{
						if(XVar.Pack(checkFieldHasMapData((XVar)(i))))
						{
							addGoogleMapData((XVar)(this.listFields[i]["fName"]), (XVar)(data), (XVar)(keys), (XVar)(editlink));
						}
						record.InitAndSetArrayItem(proccessRecordValue((XVar)(data), (XVar)(keylink), (XVar)(this.listFields[i])), this.listFields[i]["valueFieldName"]);
					}
					addSpansForGridCells(new XVar("edit"), (XVar)(record), (XVar)(data));
					if(XVar.Pack(eventExists(new XVar("BeforeMoveNextList"))))
					{
						RunnerContext.pushRecordContext((XVar)(data), this);
						this.eventsObject.BeforeMoveNextList((XVar)(data), (XVar)(row), (XVar)(record), this);
						RunnerContext.pop();
					}
					spreadRowStyles((XVar)(data), (XVar)(row), (XVar)(record));
					setRowCssRules((XVar)(record));
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.listFields); i++)
					{
						field = XVar.Clone(this.listFields[i]["fName"]);
						setRowClassNames((XVar)(record), (XVar)(field));
						addHiddenColumnClasses((XVar)(record), (XVar)(field));
					}
					if(col < this.colsOnPage)
					{
						record.InitAndSetArrayItem(true, "endrecord_block");
					}
					record.InitAndSetArrayItem(true, "grid_recordheader");
					record.InitAndSetArrayItem(true, "grid_vrecord");
					setRowsGridRecord((XVar)(row), (XVar)(record));
					data = XVar.Clone(beforeProccessRow());
					this.recNo++;
				}
				if(col <= this.colsOnPage)
				{
					dynamic gInd = null;
					gInd = new XVar(0);
					for(;gInd < col - 1; gInd++)
					{
						this.controlsMap.InitAndSetArrayItem(this.recId, "gridRows", gridRowInd - gInd, "contextRowId");
					}
				}
				while(col <= this.colsOnPage)
				{
					record = XVar.Clone(XVar.Array());
					if(col < this.colsOnPage)
					{
						record.InitAndSetArrayItem(true, "endrecord_block");
					}
					if(XVar.Pack(row["grid_record"]["data"]))
					{
						row.InitAndSetArrayItem(record, "grid_record", "data", null);
					}
					col++;
				}
				row.InitAndSetArrayItem(true, "grid_rowspace");
				row.InitAndSetArrayItem(new XVar("data", XVar.Array()), "grid_recordspace");
				i = new XVar(0);
				for(;i < this.colsOnPage * 2 - 1; i++)
				{
					row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
				}
				rowinfo.InitAndSetArrayItem(row, "data", null);
			}
			if(XVar.Pack(this.lockingObj))
			{
				this.jsSettings.InitAndSetArrayItem(lockRecIds, "tableSettings", this.tName, "lockRecIds");
			}
			if(XVar.Pack(MVCFunctions.count(rowinfo["data"])))
			{
				rowinfo.InitAndSetArrayItem(false, "data", MVCFunctions.count(rowinfo["data"]) - 1, "grid_rowspace");
				if((XVar)(this.listGridLayout == Constants.gltVERTICAL)  && (XVar)(this.is508))
				{
					if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						rowinfo.InitAndSetArrayItem("<div style=\"display:none\">Table data</div>", "begin");
					}
					else
					{
						rowinfo.InitAndSetArrayItem("<caption style=\"display:none\">Table data</caption>", "begin");
					}
				}
				this.xt.assignbyref(new XVar("grid_row"), (XVar)(rowinfo));
			}
			buildTotals((XVar)(totals));
			return null;
		}
		protected virtual XVar hasBigMap()
		{
			return this.googleMapCfg["isUseMainMaps"];
		}
		protected virtual XVar checkFieldHasMapData(dynamic _param_fIndex)
		{
			#region pass-by-value parameters
			dynamic fIndex = XVar.Clone(_param_fIndex);
			#endregion

			return MVCFunctions.in_array((XVar)(fIndex), (XVar)(this.gMapFields));
		}
		protected virtual XVar addHiddenColumnClasses(dynamic record, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic gFieldName = null;
			gFieldName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
			if(XVar.Pack(this.hiddenColumnClasses.KeyExists(gFieldName)))
			{
				record[MVCFunctions.Concat(gFieldName, "_class")] = MVCFunctions.Concat(record[MVCFunctions.Concat(gFieldName, "_class")], " ", this.hiddenColumnClasses[gFieldName]);
				if(this.listGridLayout != Constants.gltHORIZONTAL)
				{
					record.InitAndSetArrayItem(this.hiddenColumnClasses[gFieldName], MVCFunctions.Concat(gFieldName, "_label_class"));
				}
			}
			return null;
		}
		public override XVar fieldClass(dynamic _param_f)
		{
			#region pass-by-value parameters
			dynamic f = XVar.Clone(_param_f);
			#endregion

			if(XVar.Pack(!(XVar)(this.fieldClasses.KeyExists(f))))
			{
				this.fieldClasses.InitAndSetArrayItem(calcFieldClass((XVar)(f)), f);
			}
			return this.fieldClasses[f];
		}
		public virtual XVar calcFieldClass(dynamic _param_f)
		{
			#region pass-by-value parameters
			dynamic f = XVar.Clone(_param_f);
			#endregion

			dynamic format = null;
			if(this.pSet.getEditFormat((XVar)(f)) == Constants.FORMAT_LOOKUP_WIZARD)
			{
				return "";
			}
			format = XVar.Clone(this.pSet.getViewFormat((XVar)(f)));
			if(format == Constants.FORMAT_FILE)
			{
				return " rnr-field-file";
			}
			if((XVar)(this.listGridLayout == Constants.gltVERTICAL)  || (XVar)(this.listGridLayout == Constants.gltCOLUMNS))
			{
				return "";
			}
			if(format == Constants.FORMAT_AUDIO)
			{
				return " rnr-field-audio";
			}
			if(format == Constants.FORMAT_CHECKBOX)
			{
				return " rnr-field-checkbox";
			}
			if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(this.pSet.getFieldType((XVar)(f))))))
			{
				return " rnr-field-number";
			}
			return "rnr-field-text";
		}
		protected virtual XVar setRowHoverCssRule(dynamic _param_rowHoverCssRule, dynamic _param_fieldName = null)
		{
			#region default values
			if(_param_fieldName as Object == null) _param_fieldName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic rowHoverCssRule = XVar.Clone(_param_rowHoverCssRule);
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			if((XVar)(this.listGridLayout != Constants.gltHORIZONTAL)  && (XVar)(this.listGridLayout != Constants.gltFLEXIBLE))
			{
				return null;
			}
			if(XVar.Pack(fieldName))
			{
				dynamic className = null;
				className = XVar.Clone(MVCFunctions.Concat("rnr-style", this.recId, "-", fieldName));
				this.row_css_rules = MVCFunctions.Concat(this.row_css_rules, " tr:hover > td.", className, ".", className, "{", getCustomCSSRule((XVar)(rowHoverCssRule)), "}\n");
				return className;
			}
			else
			{
				this.row_css_rules = XVar.Clone(MVCFunctions.Concat(" tr[id=\"gridRow", this.recId, "\"]:hover > td:not(.rnr-cs){", getCustomCSSRule((XVar)(rowHoverCssRule)), "}\n", this.row_css_rules));
				return "";
			}
			return null;
		}
		public virtual XVar buildTotals(dynamic totals)
		{
			if(XVar.Pack(MVCFunctions.count(this.totalsFields)))
			{
				dynamic j = null, record = XVar.Array(), totals_records = XVar.Array();
				this.xt.assign(new XVar("totals_row"), new XVar(true));
				totals_records = XVar.Clone(new XVar("data", XVar.Array()));
				j = new XVar(0);
				for(;j < this.colsOnPage; j++)
				{
					record = XVar.Clone(XVar.Array());
					if(j == XVar.Pack(0))
					{
						dynamic i = null, total = null;
						i = new XVar(0);
						for(;i < MVCFunctions.count(this.totalsFields); i++)
						{
							total = XVar.Clone(CommonFunctions.GetTotals((XVar)(this.totalsFields[i]["fName"]), (XVar)(totals[this.totalsFields[i]["fName"]]), (XVar)(this.totalsFields[i]["totalsType"]), (XVar)(this.totalsFields[i]["numRows"]), (XVar)(this.totalsFields[i]["viewFormat"]), new XVar(Constants.PAGE_LIST), (XVar)(this.pSet)));
							total = XVar.Clone(MVCFunctions.Concat("<span id=\"total", this.id, "_", MVCFunctions.GoodFieldName((XVar)(this.totalsFields[i]["fName"])), "\">", total, "</span>"));
							this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.totalsFields[i]["fName"])), "_total")), (XVar)(total));
							record.InitAndSetArrayItem(true, MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.totalsFields[i]["fName"])), "_showtotal"));
						}
					}
					if(j < this.colsOnPage - 1)
					{
						record.InitAndSetArrayItem(true, "endrecordtotals_block");
					}
					totals_records.InitAndSetArrayItem(record, "data", null);
				}
				this.xt.assignbyref(new XVar("totals_record"), (XVar)(totals_records));
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.assign(new XVar("totals_attr"), new XVar("style='display:none;'"));
				}
			}
			return null;
		}
		public virtual XVar outputFieldValue(dynamic _param_field, dynamic _param_state)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic state = XVar.Clone(_param_state);
			#endregion

			this.arrFieldSpanVal.InitAndSetArrayItem(state, field);
			return null;
		}
		public virtual XVar addSpanVal(dynamic _param_fName, dynamic data)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic var_type = null;
			var_type = XVar.Clone(this.pSet.getFieldType((XVar)(fName)));
			if((XVar)(!(XVar)(CommonFunctions.IsBinaryType((XVar)(var_type))))  && (XVar)((XVar)((XVar)((XVar)(this.arrFieldSpanVal[fName] == 2)  || (XVar)(this.arrFieldSpanVal[fName] == 1))  || (XVar)(this.pSet.hasAjaxSnippet()))  || (XVar)(this.pSet.hasButtonsAdded())))
			{
				return MVCFunctions.Concat("val=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[fName])), "\" ");
			}
			return null;
		}
		public virtual XVar addSpansForGridCells(dynamic _param_type, dynamic record, dynamic _param_data = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic i = null, span = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.listFields); i++)
			{
				span = XVar.Clone(MVCFunctions.Concat("<span id=\"", var_type, (XVar.Pack(var_type == "edit") ? XVar.Pack(this.recId) : XVar.Pack(this.id)), "_", this.listFields[i]["goodFieldName"], "\" "));
				if(var_type == "edit")
				{
					span = MVCFunctions.Concat(span, addSpanVal((XVar)(this.listFields[i]["fName"]), (XVar)(data)), ">");
					span = MVCFunctions.Concat(span, record[this.listFields[i]["valueFieldName"]]);
				}
				else
				{
					span = MVCFunctions.Concat(span, ">");
				}
				span = MVCFunctions.Concat(span, "</span>");
				record.InitAndSetArrayItem(span, this.listFields[i]["valueFieldName"]);
			}
			return null;
		}
		public virtual XVar proccessRecordValue(dynamic data, dynamic keylink, dynamic _param_listFieldInfo)
		{
			#region pass-by-value parameters
			dynamic listFieldInfo = XVar.Clone(_param_listFieldInfo);
			#endregion

			dynamic dbVal = null;
			dbVal = XVar.Clone(showDBValue((XVar)(listFieldInfo["fName"]), (XVar)(data), (XVar)(keylink)));
			return addCenterLink(ref dbVal, (XVar)(listFieldInfo["fName"]));
		}
		public virtual XVar isDispGrid()
		{
			if((XVar)(this.permis[this.tName]["search"])  && (XVar)(this.rowsFound))
			{
				return true;
			}
			if((XVar)(inlineAddAvailable())  || (XVar)((XVar)(addAvailable())  && (XVar)(this.showAddInPopup)))
			{
				return true;
			}
			return false;
		}
		public virtual XVar fillCheckAttr(dynamic record, dynamic _param_data, dynamic _param_keyblock)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic keyblock = XVar.Clone(_param_keyblock);
			#endregion

			if((XVar)((XVar)((XVar)(exportAvailable())  || (XVar)(printAvailable()))  || (XVar)(deleteAvailable()))  || (XVar)(inlineEditAvailable()))
			{
				record.InitAndSetArrayItem(true, "checkbox");
				record.InitAndSetArrayItem(MVCFunctions.Concat("name=\"selection[]\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(keyblock)), "\" id=\"check", this.id, "_", this.recId, "\""), "checkbox_attrs");
			}
			return null;
		}
		public virtual XVar prepareForBuildPage()
		{
			if((XVar)(this.mode == Constants.LIST_DASHDETAILS)  || (XVar)((XVar)(this.mode == Constants.LIST_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_LIST)  || (XVar)(this.masterPageType == Constants.PAGE_REPORT))))
			{
				updateDetailsTabTitles();
			}
			buildMobileCssRules();
			buildOrderParams();
			deleteRecords();
			rulePRG();
			processGridTabs();
			buildSQL();
			buildPagination();
			seekPageInRecSet((XVar)(this.querySQL));
			setGoogleMapsParams((XVar)(this.listFields));
			setGridUserParams();
			assignColumnHeaderClasses();
			fillGridData();
			if(this.mode != Constants.LIST_MASTER)
			{
				buildSearchPanel();
			}
			addCommonJs();
			addCommonHtml();
			commonAssign();
			addCustomCss();
			return null;
		}
		public virtual XVar buildOrderParams()
		{
			assignColumnHeaders();
			if(XVar.Pack(!(XVar)(isPageSortable())))
			{
				return null;
			}
			addOrderUrlParam();
			return null;
		}
		public virtual XVar assignColumnHeaders()
		{
			dynamic orderFields = XVar.Array();
			foreach (KeyValuePair<XVar, dynamic> f in this.listFields.GetEnumerator())
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(f.Value["fName"])), "_fieldheader")), new XVar(true));
			}
			if(XVar.Pack(!(XVar)(isReoderByHeaderClickingEnabled())))
			{
				return null;
			}
			orderFields = this.orderClause.getOrderFields();
			foreach (KeyValuePair<XVar, dynamic> of in orderFields.GetEnumerator())
			{
				if(XVar.Pack(of.Value["hidden"]))
				{
					continue;
				}
				this.xt.assign_section((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(of.Value["column"])), "_fieldheader")), new XVar(""), (XVar)(MVCFunctions.Concat("<span data-icon=\"", (XVar.Pack(of.Value["dir"] == "ASC") ? XVar.Pack("sortasc") : XVar.Pack("sortdesc")), "\"></span>")));
			}
			foreach (KeyValuePair<XVar, dynamic> f in this.listFields.GetEnumerator())
			{
				dynamic attrs = XVar.Array(), dir = null, gf = null, multisort = XVar.Array();
				gf = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(f.Value["fName"])));
				dir = new XVar("a");
				multisort = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> of in orderFields.GetEnumerator())
				{
					if(XVar.Pack(of.Value["hidden"]))
					{
						continue;
					}
					if(of.Value["column"] == f.Value["fName"])
					{
						dir = XVar.Clone((XVar.Pack(of.Value["dir"] == "ASC") ? XVar.Pack("d") : XVar.Pack("a")));
					}
					else
					{
						multisort.InitAndSetArrayItem(MVCFunctions.Concat((XVar.Pack(of.Value["dir"] == "ASC") ? XVar.Pack("a") : XVar.Pack("d")), MVCFunctions.GoodFieldName((XVar)(of.Value["column"]))), null);
					}
				}
				attrs = XVar.Clone(XVar.Array());
				attrs.InitAndSetArrayItem(MVCFunctions.Concat("data-href=\"", MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("list"), (XVar)(MVCFunctions.Concat("orderby=", dir, gf))), "\""), null);
				attrs.InitAndSetArrayItem(MVCFunctions.Concat("data-order=\"", dir, gf, "\""), null);
				attrs.InitAndSetArrayItem(MVCFunctions.Concat("id=\"order_", gf, "_", this.id, "\""), null);
				attrs.InitAndSetArrayItem(MVCFunctions.Concat("name=\"order_", gf, "_", this.id, "\""), null);
				attrs.InitAndSetArrayItem(MVCFunctions.Concat("data-multisort=\"", MVCFunctions.implode(new XVar(";"), (XVar)(multisort)), "\""), null);
				attrs.InitAndSetArrayItem("class=\"rnr-orderlink\"", null);
				this.xt.assign((XVar)(MVCFunctions.Concat(gf, "_orderlinkattrs")), (XVar)(MVCFunctions.implode(new XVar(" "), (XVar)(attrs))));
			}
			return null;
		}
		protected virtual XVar orderFieldLabelString(dynamic _param_fName, dynamic _param_desc, dynamic _param_showLabelOnly)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic desc = XVar.Clone(_param_desc);
			dynamic showLabelOnly = XVar.Clone(_param_showLabelOnly);
			#endregion

			fName = XVar.Clone(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(fName)))));
			if(XVar.Pack(showLabelOnly))
			{
				return fName;
			}
			return MVCFunctions.Concat(fName, " ", (XVar.Pack(desc) ? XVar.Pack("High to Low") : XVar.Pack("Low to High")));
		}
		protected virtual XVar assignSortByDropdown()
		{
			dynamic markup = null, options = XVar.Array(), sortByIdx = null, sortSettings = XVar.Array();
			if(XVar.Pack(!(XVar)(this.pSet.hasSortByDropdown())))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(this.rowsFound)))
			{
				if(XVar.Pack(this.listAjax))
				{
					this.xt.assign(new XVar("reorder_records"), new XVar(true));
					this.xt.displayBrickHidden(new XVar("reorder_records"));
				}
				return null;
			}
			sortSettings = XVar.Clone(this.orderClause.getSortBySettings());
			sortByIdx = XVar.Clone(this.orderClause.getSortByControlIdx());
			options = XVar.Clone(XVar.Array());
			if(sortByIdx == -1)
			{
				options.InitAndSetArrayItem("<option selected> </option>", null);
			}
			foreach (KeyValuePair<XVar, dynamic> sData in sortSettings.GetEnumerator())
			{
				dynamic label = null, selected = null;
				label = XVar.Clone(sData.Value["label"]);
				if(XVar.Pack(!(XVar)(label)))
				{
					dynamic labelParts = XVar.Array();
					labelParts = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> fData in sData.Value["fields"].GetEnumerator())
					{
						labelParts.InitAndSetArrayItem(orderFieldLabelString((XVar)(fData.Value["field"]), (XVar)(fData.Value["desc"]), (XVar)(fData.Value["labelOnly"])), null);
					}
					label = XVar.Clone(MVCFunctions.implode(new XVar("; "), (XVar)(labelParts)));
				}
				selected = XVar.Clone((XVar.Pack(sortByIdx == sData.Key) ? XVar.Pack(" selected") : XVar.Pack("")));
				options.InitAndSetArrayItem(MVCFunctions.Concat("<option value=\"", sData.Key + 1, "\" ", selected, ">", label, "</option>"), null);
			}
			if(XVar.Pack(MVCFunctions.count(options)))
			{
				markup = XVar.Clone(MVCFunctions.Concat("<select id=\"sortBy", this.id, "\" class=\"form-control\">", MVCFunctions.implode(new XVar(""), (XVar)(options)), "</select>"));
			}
			this.xt.assign(new XVar("reorder_records"), new XVar(true));
			this.xt.assign(new XVar("sortByDropdown"), (XVar)(markup));
			return null;
		}
		public virtual XVar showPage()
		{
			BeforeShowList();
			display((XVar)(this.templatefile));
			return null;
		}
		public static XVar createListPage(dynamic _param_strTableName, dynamic _param_options)
		{
			#region pass-by-value parameters
			dynamic strTableName = XVar.Clone(_param_strTableName);
			dynamic options = XVar.Clone(_param_options);
			#endregion

			dynamic allfields = XVar.Array(), pageObject = null, var_params = XVar.Array();
			GlobalVars.gSettings = XVar.Clone(new ProjectSettings((XVar)(strTableName), (XVar)(options["pageType"])));
			GlobalVars.gQuery = XVar.Clone(GlobalVars.gSettings.getSQLQuery());
			var_params = XVar.Clone(options);
			var_params.InitAndSetArrayItem(strTableName, "tName");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getOriginalTableName(), "origTName");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getInitialPageSize(), "gPageSize");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getAdvancedSecurityType(), "nSecOptions");
			var_params.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("nLoginMethod"), new XVar(0)), "nLoginMethod");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getRecordsPerRowList(), "recsPerRowList");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getTableOwnerIdField(), "mainTableOwnerID");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.hasExportPage(), "exportTo");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.hasPrintPage(), "printFriendly");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.hasDelete(), "deleteRecs");
			var_params.InitAndSetArrayItem(GlobalVars.isGroupSecurity, "isGroupSecurity");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getTableKeys(), "arrKeyFields");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getPanelSearchFields(), "panelSearchFields");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getListGridLayout(), "listGridLayout");
			var_params.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("createLoginPage"), new XVar(false)), "createLoginPage");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.noRecordsOnFirstPage(), "noRecordsFirstPage");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getTotalsFields(), "totalsFields");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.ajaxBasedListPage(), "listAjax");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getRecordsPerPageArray(), "arrRecsPerPage");
			var_params.InitAndSetArrayItem(GlobalVars.gSettings.getScrollGridBody(), "isScrollGridBody");
			var_params.InitAndSetArrayItem((XVar)(GlobalVars.gSettings.isViewPagePDF())  || (XVar)(GlobalVars.gSettings.isPrinterPagePDF()), "viewPDF");
			var_params.InitAndSetArrayItem(CommonFunctions.GetAuditObject((XVar)(strTableName)), "audit");
			var_params.InitAndSetArrayItem(XVar.Array(), "listFields");
			allfields = XVar.Clone(GlobalVars.gSettings.getListFields());
			foreach (KeyValuePair<XVar, dynamic> f in allfields.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(GlobalVars.gSettings.appearOnListPage((XVar)(f.Value)))))
				{
					continue;
				}
				var_params.InitAndSetArrayItem(new XVar("fName", f.Value, "goodFieldName", MVCFunctions.GoodFieldName((XVar)(f.Value)), "valueFieldName", MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(f.Value)), "_value"), "viewFormat", GlobalVars.gSettings.getViewFormat((XVar)(f.Value)), "editFormat", GlobalVars.gSettings.getEditFormat((XVar)(f.Value))), "listFields", null);
			}
			if(var_params["mode"] == Constants.LIST_SIMPLE)
			{
				pageObject = XVar.Clone(new ListPage_Simple((XVar)(var_params)));
			}
			else
			{
				if(var_params["mode"] == Constants.LIST_MASTER)
				{
					pageObject = XVar.Clone(new ListPage_Master((XVar)(var_params)));
				}
				else
				{
					if(var_params["mode"] == Constants.LIST_AJAX)
					{
						pageObject = XVar.Clone(new ListPage_Ajax((XVar)(var_params)));
					}
					else
					{
						if(var_params["mode"] == Constants.LIST_LOOKUP)
						{
							pageObject = XVar.Clone(new ListPage_Lookup((XVar)(var_params)));
						}
						else
						{
							if((XVar)(var_params["mode"] == Constants.LIST_DETAILS)  && (XVar)(var_params["masterPageType"] == Constants.PAGE_LIST))
							{
								pageObject = XVar.Clone(new ListPage_DPList((XVar)(var_params)));
							}
							else
							{
								if(var_params["mode"] == Constants.LIST_DETAILS)
								{
									pageObject = XVar.Clone(new ListPage_DPInline((XVar)(var_params)));
								}
								else
								{
									if(var_params["mode"] == Constants.LIST_DASHDETAILS)
									{
										pageObject = XVar.Clone(new ListPage_DPDash((XVar)(var_params)));
									}
									else
									{
										if(var_params["mode"] == Constants.RIGHTS_PAGE)
										{
											pageObject = XVar.Clone(new RightsPage((XVar)(var_params)));
										}
										else
										{
											if(var_params["mode"] == Constants.MEMBERS_PAGE)
											{
												pageObject = XVar.Clone(new MembersPage((XVar)(var_params)));
											}
											else
											{
												if(var_params["mode"] == Constants.LIST_DASHBOARD)
												{
													pageObject = XVar.Clone(new ListPage_Dashboard((XVar)(var_params)));
												}
												else
												{
													if(var_params["mode"] == Constants.MAP_DASHBOARD)
													{
														pageObject = XVar.Clone(new MapPage_Dashboard((XVar)(var_params)));
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
			pageObject.init();
			return pageObject;
		}
		protected virtual XVar setRowsGridRecord(dynamic row, dynamic _param_record)
		{
			#region pass-by-value parameters
			dynamic record = XVar.Clone(_param_record);
			#endregion

			if((XVar)(this.listGridLayout == Constants.gltVERTICAL)  || (XVar)(this.recsPerRowList != 1))
			{
				row.InitAndSetArrayItem(record, "grid_record", "data", null);
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> value in record.GetEnumerator())
			{
				row.InitAndSetArrayItem(value.Value, value.Key);
			}
			row.InitAndSetArrayItem(true, "grid_record");
			return null;
		}
		protected virtual XVar buildMobileCssRules()
		{
			dynamic columnsToHide = XVar.Array(), cssBlocks = XVar.Array(), devices = XVar.Array();
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				return null;
			}
			cssBlocks = XVar.Clone(XVar.Array());
			columnsToHide = XVar.Clone(getColumnsToHide());
			devices = XVar.Clone(new XVar(0, Constants.TABLET_7_IN, 1, Constants.SMARTPHONE_PORTRAIT, 2, Constants.SMARTPHONE_LANDSCAPE, 3, Constants.TABLET_10_IN, 4, Constants.DESKTOP));
			foreach (KeyValuePair<XVar, dynamic> f in this.listFields.GetEnumerator())
			{
				dynamic field = null, fieldMentioned = null, gFieldName = null;
				gFieldName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(f.Value["fName"])));
				fieldMentioned = new XVar(false);
				field = XVar.Clone(f.Value["fName"]);
				foreach (KeyValuePair<XVar, dynamic> d in devices.GetEnumerator())
				{
					if(XVar.Pack(MVCFunctions.in_array((XVar)(gFieldName), (XVar)(columnsToHide[d.Value]))))
					{
						this.hiddenColumnClasses.InitAndSetArrayItem(MVCFunctions.Concat("column", MVCFunctions.GoodFieldName((XVar)(field))), gFieldName);
						cssBlocks[d.Value] = MVCFunctions.Concat(cssBlocks[d.Value], ".", this.hiddenColumnClasses[gFieldName], ":not([data-forced-visible-column]) { display: none !important;; }\n");
						fieldMentioned = new XVar(true);
					}
				}
			}
			this.mobile_css_rules = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> d in devices.GetEnumerator())
			{
				if(XVar.Pack(cssBlocks[d.Value]))
				{
					this.mobile_css_rules = MVCFunctions.Concat(this.mobile_css_rules, ProjectSettings.getDeviceMediaClause((XVar)(d.Value)), "\n{\n", cssBlocks[d.Value], "\n}\n");
				}
			}
			return null;
		}
		public virtual XVar getNotListBlobFieldsIndices()
		{
			dynamic allFields = XVar.Array(), blobIndices = XVar.Array(), indices = XVar.Array();
			allFields = XVar.Clone(this.pSet.getFieldsList());
			blobIndices = XVar.Clone(this.pSet.getBinaryFieldsIndices());
			indices = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> idx in blobIndices.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.pSet.appearOnListPage((XVar)(allFields[idx.Value - 1])))))
				{
					indices.InitAndSetArrayItem(idx.Value, null);
				}
			}
			return indices;
		}
		public static XVar readListModeFromRequest()
		{
			dynamic postedMode = null;
			postedMode = XVar.Clone(MVCFunctions.postvalue(new XVar("mode")));
			if(postedMode == "ajax")
			{
				return Constants.LIST_AJAX;
			}
			else
			{
				if(postedMode == "lookup")
				{
					return Constants.LIST_LOOKUP;
				}
				else
				{
					if(postedMode == "listdetails")
					{
						return Constants.LIST_DETAILS;
					}
					else
					{
						if(postedMode == "dashdetails")
						{
							return Constants.LIST_DASHDETAILS;
						}
						else
						{
							if(postedMode == "dashlist")
							{
								return Constants.LIST_DASHBOARD;
							}
							else
							{
								if(postedMode == "dashmap")
								{
									return Constants.MAP_DASHBOARD;
								}
							}
						}
					}
				}
			}
			return Constants.LIST_SIMPLE;
		}
		protected static XVar readMainTableSettingsFromRequest(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic mainTableShortName = null;
			mainTableShortName = XVar.Clone(CommonFunctions.GetTableURL((XVar)(MVCFunctions.postvalue(new XVar("table")))));
			return CommonFunctions.getLookupMainTableSettings((XVar)(table), (XVar)(mainTableShortName), (XVar)(MVCFunctions.postvalue(new XVar("field"))));
		}
		protected static XVar checkLookupPermissions(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic lookupMainSettings = null, mainTable = null;
			lookupMainSettings = XVar.Clone(readMainTableSettingsFromRequest((XVar)(table)));
			if(XVar.Pack(!(XVar)(lookupMainSettings)))
			{
				return false;
			}
			mainTable = XVar.Clone(lookupMainSettings.getTableName());
			if((XVar)((XVar)(CommonFunctions.CheckTablePermissions((XVar)(mainTable), new XVar("S")))  || (XVar)(CommonFunctions.CheckTablePermissions((XVar)(mainTable), new XVar("E"))))  || (XVar)(CommonFunctions.CheckTablePermissions((XVar)(mainTable), new XVar("A"))))
			{
				return true;
			}
			return false;
		}
		public static XVar processListPageSecurity(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic mode = null;
			if(XVar.Pack(Security.checkPagePermissions((XVar)(table), new XVar("S"))))
			{
				return true;
			}
			mode = XVar.Clone(readListModeFromRequest());
			if((XVar)(mode == Constants.LIST_LOOKUP)  && (XVar)(checkLookupPermissions((XVar)(table))))
			{
				return true;
			}
			if(mode != Constants.LIST_SIMPLE)
			{
				Security.sendPermissionError();
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
		public override XVar hideField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldheadercolumn")), new XVar(false));
			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldcolumn")), new XVar(false));
			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldfootercolumn")), new XVar(false));
			return null;
		}
		public override XVar showField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldheadercolumn")), new XVar(true));
			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldcolumn")), new XVar(true));
			this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(fieldName)), "_fieldfootercolumn")), new XVar(true));
			return null;
		}
		public virtual XVar isPageSortable()
		{
			return true;
		}
		public static XVar processSaveParams(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(XVar.Pack(MVCFunctions.postvalue(new XVar("saveParam"))))
			{
				dynamic paramData = null, paramType = null, paramsLogger = null;
				paramType = XVar.Clone(MVCFunctions.intval((XVar)(MVCFunctions.postvalue(new XVar("saveParam")))));
				paramData = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("data")))));
				if(XVar.Pack(MVCFunctions.postvalue(new XVar("onDashboard"))))
				{
					paramsLogger = XVar.Clone(new paramsLogger((XVar)(MVCFunctions.postvalue(new XVar("dashElementId"))), (XVar)(paramType)));
				}
				else
				{
					paramsLogger = XVar.Clone(new paramsLogger((XVar)(table), (XVar)(paramType)));
				}
				if(paramType == Constants.SHFIELDS_PARAMS_TYPE)
				{
					dynamic macroDeviceClass = null, ps = null;
					macroDeviceClass = XVar.Clone(RunnerPage.deviceClassToMacro((XVar)(MVCFunctions.postvalue(new XVar("deviceClass")))));
					ps = XVar.Clone(new ProjectSettings((XVar)(table)));
					if(XVar.Pack(!(XVar)(ps.columnsByDeviceEnabled())))
					{
						macroDeviceClass = new XVar(0);
					}
					paramsLogger.saveShowHideData((XVar)(macroDeviceClass), (XVar)(paramData));
				}
				else
				{
					paramsLogger.save((XVar)(paramData));
				}
				return true;
			}
			return false;
		}
		protected virtual XVar setGridUserParams()
		{
			return null;
		}
		protected virtual XVar displayViewLink()
		{
			return viewAvailable();
		}
		public override XVar gridTabsAvailable()
		{
			if((XVar)(this.mode == Constants.LIST_DETAILS)  && (XVar)(this.masterPageType == Constants.PAGE_ADD))
			{
				return false;
			}
			return true;
		}
		protected virtual XVar hasMainDashMapElem()
		{
			return false;
		}
		public override XVar displayTabsInPage()
		{
			return (XVar)(simpleMode())  || (XVar)((XVar)(this.mode == Constants.LIST_DETAILS)  && (XVar)((XVar)(this.masterPageType == Constants.PAGE_VIEW)  || (XVar)(this.masterPageType == Constants.PAGE_EDIT)));
		}
	}
}
