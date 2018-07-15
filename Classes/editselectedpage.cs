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
	public partial class EditSelectedPage : EditPage
	{
		public dynamic rowIds = XVar.Array();
		public dynamic parsedSelection = XVar.Array();
		public dynamic updSelectedFields = XVar.Pack(null);
		public dynamic selectedFields = XVar.Pack(null);
		public dynamic nSelected = XVar.Pack(0);
		public dynamic nUpdated = XVar.Pack(0);
		public dynamic recordBeingUpdated;
		public dynamic currentWhereExpr;
		public dynamic recordCount = XVar.Pack(0);
		public dynamic messages = XVar.Array();
		protected dynamic inlineReportData = XVar.Array();
		protected static bool skipEditSelectedPageCtor = false;
		public EditSelectedPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipEditSelectedPageCtor)
			{
				skipEditSelectedPageCtor = false;
				return;
			}
			dynamic keyFields = XVar.Array();
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			foreach (KeyValuePair<XVar, dynamic> s in this.selection.GetEnumerator())
			{
				dynamic arr = XVar.Array(), parsed = XVar.Array();
				arr = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(s.Value)));
				if(MVCFunctions.count(arr) != MVCFunctions.count(this.pSet.getTableKeys()))
				{
					continue;
				}
				foreach (KeyValuePair<XVar, dynamic> v in arr.GetEnumerator())
				{
					parsed.InitAndSetArrayItem(v.Value, keyFields[v.Key]);
				}
				this.parsedSelection.InitAndSetArrayItem(parsed, null);
			}
		}
		protected override XVar getLockingObject()
		{
			return null;
		}
		protected override XVar getPageFields()
		{
			if(XVar.Equals(XVar.Pack(this.updSelectedFields), XVar.Pack(null)))
			{
				dynamic denyDuplicateFields = XVar.Array(), updateFields = XVar.Array();
				this.updSelectedFields = XVar.Clone(MVCFunctions.array_diff((XVar)(this.pSet.getUpdateSelectedFields()), (XVar)(this.pSet.getTableKeys())));
				denyDuplicateFields = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> f in this.updSelectedFields.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(this.pSet.allowDuplicateValues((XVar)(f.Value)))))
					{
						denyDuplicateFields.InitAndSetArrayItem(f.Value, null);
					}
				}
				this.updSelectedFields = XVar.Clone(MVCFunctions.array_diff((XVar)(this.updSelectedFields), (XVar)(denyDuplicateFields)));
				updateFields = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> f in this.updSelectedFields.GetEnumerator())
				{
					dynamic editFormat = null;
					editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(f.Value)));
					if(editFormat != Constants.EDIT_FORMAT_FILE)
					{
						updateFields.InitAndSetArrayItem(f.Value, null);
					}
				}
				this.updSelectedFields = XVar.Clone(updateFields);
			}
			return this.updSelectedFields;
		}
		public override XVar setKeys(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			this.keys = XVar.Clone(keys);
			return null;
		}
		protected override XVar getAfterEditAction()
		{
			dynamic action = null;
			if((XVar)(true)  && (XVar)(!(XVar)(this.afterEditAction == null)))
			{
				return this.afterEditAction;
			}
			action = XVar.Clone(this.pSet.getAfterEditAction());
			if(action != Constants.AE_TO_LIST)
			{
				action = new XVar(Constants.AE_TO_EDIT);
			}
			if((XVar)(isPopupMode())  && (XVar)(this.pSet.checkClosePopupAfterEdit()))
			{
				action = new XVar(Constants.AE_TO_LIST);
			}
			this.afterEditAction = XVar.Clone(action);
			return this.afterEditAction;
		}
		public override XVar process()
		{
			if(this.action == "edited")
			{
				processDataInput();
				this.readEditValues = XVar.Clone(!(XVar)(this.updatedSuccessfully));
				if(XVar.Pack(isPopupMode()))
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
				this.cachedRecord = new XVar(null);
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
			setPageTitle((XVar)(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))))));
			prepareReadonlyFields();
			doCommonAssignments();
			prepareButtons();
			prepareSteps();
			prepareEditControls();
			fillCntrlTabGroups();
			prepareJsSettings();
			addButtonHandlers();
			addCommonJs();
			fillSetCntrlMaps();
			displayEditPage();
			return null;
		}
		protected override XVar prepareJsSettings()
		{
			this.jsSettings.InitAndSetArrayItem(getSelection(), "tableSettings", this.tName, "selection");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getTableKeys(), "tableSettings", this.tName, "keyFields");
			this.jsSettings.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(getCurrentRecordInternal())), "tableSettings", this.tName, "masterKeys");
			this.jsSettings.InitAndSetArrayItem(getCaptchaFieldName(), "tableSettings", this.tName, "captchaEditFieldName");
			return null;
		}
		protected override XVar doCommonAssignments()
		{
			this.message = XVar.Clone(getMessages());
			base.doCommonAssignments();
			return null;
		}
		protected override XVar prepareDetailsTables()
		{
			return null;
		}
		protected override XVar prepareNextPrevButtons()
		{
			return null;
		}
		protected override XVar prepareButtons()
		{
			dynamic label = null;
			base.prepareButtons();
			this.xt.assign(new XVar("save_button"), new XVar(false));
			this.xt.assign(new XVar("view_page_button"), new XVar(false));
			this.xt.assign(new XVar("updsel_button"), new XVar(true));
			this.xt.assign(new XVar("updselbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"saveButton", this.id, "\"")));
			label = XVar.Clone(MVCFunctions.str_replace(new XVar("%n%"), (XVar)(this.nSelected), new XVar("Update %n% records")));
			this.xt.assign(new XVar("update_selected"), (XVar)(label));
			return null;
		}
		protected override XVar lockRecord()
		{
			return true;
		}
		protected override XVar reportInlineSaveStatus()
		{
			dynamic returnJSON = XVar.Array();
			returnJSON = XVar.Clone(this.inlineReportData);
			returnJSON.InitAndSetArrayItem(this.updatedSuccessfully, "success");
			if(XVar.Pack(!(XVar)(this.isCaptchaOk)))
			{
				returnJSON.InitAndSetArrayItem(getCaptchaFieldName(), "wrongCaptchaFieldName");
			}
			returnJSON.InitAndSetArrayItem(getMessages(), "message");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar getRowSaveStatusJSON(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			dynamic data = XVar.Array(), dmapIconsData = null, fields = XVar.Array(), fieldsIconsData = null, i = null, keyParams = XVar.Array(), keylink = null, rawValues = XVar.Array(), returnJSON = XVar.Array(), values = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			if((XVar)(this.action != "edited")  || (XVar)(isSimpleMode()))
			{
				return returnJSON;
			}
			returnJSON.InitAndSetArrayItem(MVCFunctions.array_keys((XVar)(this.newRecordData)), "fNamesSelected");
			returnJSON.InitAndSetArrayItem(getMessages(), "message");
			returnJSON.InitAndSetArrayItem(this.lockingMessageText, "lockMessage");
			if(XVar.Pack(!(XVar)(this.isCaptchaOk)))
			{
				returnJSON.InitAndSetArrayItem(getCaptchaFieldName(), "wrongCaptchaFieldName");
			}
			data = XVar.Clone(getRecordByKeys((XVar)(keys)));
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
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("key", k.Key + 1, "=", MVCFunctions.RawUrlDecode((XVar)(keys[k.Value]))), null);
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
		protected override XVar afterEditActionRedirect()
		{
			if(XVar.Pack(isPopupMode()))
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
				default:
					return false;
			}
			return null;
		}
		protected override XVar getPrevKeys()
		{
			return XVar.Array();
		}
		protected override XVar getNextKeys()
		{
			return XVar.Array();
		}
		protected override XVar prgRedirect()
		{
			if(XVar.Pack(this.stopPRG))
			{
				return false;
			}
			if((XVar)((XVar)(!(XVar)(this.updatedSuccessfully))  || (XVar)(!(XVar)(isSimpleMode())))  || (XVar)(!(XVar)(MVCFunctions.no_output_done())))
			{
				return false;
			}
			XSession.Session["edit_seletion"] = this.selection;
			XSession.Session["message_edit"] = getMessages();
			XSession.Session["message_edit_type"] = this.messageType;
			MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), (XVar)(getPageType()));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return true;
		}
		protected override XVar prgReadMessage()
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
		protected virtual XVar getSingleRecordWhereClause(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			dynamic strWhereClause = null;
			strWhereClause = XVar.Clone(CommonFunctions.KeyWhere((XVar)(keys)));
			if(this.pSet.getAdvancedSecurityType() != Constants.ADVSECURITY_ALL)
			{
				strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(CommonFunctions.SecuritySQL(new XVar("Edit"), (XVar)(this.tName)))));
			}
			return strWhereClause;
		}
		public virtual XVar getSelectedWhereClause()
		{
			dynamic keyFields = XVar.Array(), strWhereClause = null;
			strWhereClause = new XVar("");
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			if(MVCFunctions.count(this.pSet.getTableKeys()) == 1)
			{
				dynamic selectionDecrypted = XVar.Array();
				selectionDecrypted = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> s in this.selection.GetEnumerator())
				{
					selectionDecrypted.InitAndSetArrayItem(this.cipherer.MakeDBValue((XVar)(keyFields[0]), (XVar)(s.Value)), null);
				}
				strWhereClause = XVar.Clone(MVCFunctions.Concat(getFieldSQLDecrypt((XVar)(keyFields[0])), " in (", MVCFunctions.implode(new XVar(","), (XVar)(selectionDecrypted)), ")"));
			}
			else
			{
				dynamic components = XVar.Array();
				components = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> s in this.parsedSelection.GetEnumerator())
				{
					components.InitAndSetArrayItem(CommonFunctions.KeyWhere((XVar)(s.Value)), null);
				}
				strWhereClause = XVar.Clone(MVCFunctions.implode(new XVar(" or "), (XVar)(components)));
			}
			if(this.pSet.getAdvancedSecurityType() != Constants.ADVSECURITY_ALL)
			{
				strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(strWhereClause), (XVar)(CommonFunctions.SecuritySQL(new XVar("Edit"), (XVar)(this.tName)))));
			}
			return strWhereClause;
		}
		public override XVar getKeysWhereClause(dynamic _param_useOldKeys)
		{
			#region pass-by-value parameters
			dynamic useOldKeys = XVar.Clone(_param_useOldKeys);
			#endregion

			return this.currentWhereExpr;
		}
		public override XVar getCurrentRecordInternal()
		{
			dynamic diffValues = XVar.Array(), fetchedArray = XVar.Array(), fields = XVar.Array(), rs = null, strSQLbak = null, strWhereClause = null, strWhereClauseBak = null;
			if(XVar.Pack(!(XVar)(this.cachedRecord == null)))
			{
				return this.cachedRecord;
			}
			this.nSelected = new XVar(0);
			strWhereClause = XVar.Clone(getSelectedWhereClause());
			GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(strWhereClause)));
			strSQLbak = XVar.Clone(GlobalVars.strSQL);
			strWhereClauseBak = XVar.Clone(strWhereClause);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryEdit"))))
			{
				this.eventsObject.BeforeQueryEdit((XVar)(GlobalVars.strSQL), ref strWhereClause, this);
			}
			if((XVar)(strSQLbak == GlobalVars.strSQL)  && (XVar)(strWhereClauseBak != strWhereClause))
			{
				dynamic keysSet = null;
				GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(strWhereClause)));
				if(XVar.Pack(!(XVar)(keysSet)))
				{
					dynamic orderClause = null;
					GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
				}
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			fields = XVar.Clone(getPageFields());
			rs = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)));
			diffValues = XVar.Clone(XVar.Array());
			while(XVar.Pack(fetchedArray = XVar.Clone(rs.fetchAssoc())))
			{
				fetchedArray = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
				if(XVar.Pack(!(XVar)(this.cachedRecord)))
				{
					this.cachedRecord = XVar.Clone(fetchedArray);
				}
				else
				{
					foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
					{
						dynamic editFormat = null;
						if(this.cachedRecord[f.Value] != fetchedArray[f.Value])
						{
							diffValues.InitAndSetArrayItem(true, f.Value);
						}
					}
				}
				++(this.nSelected);
			}
			foreach (KeyValuePair<XVar, dynamic> v in diffValues.GetEnumerator())
			{
				this.cachedRecord.Remove(v.Key);
			}
			if(this.action != "edited")
			{
				foreach (KeyValuePair<XVar, dynamic> fName in getPageFields().GetEnumerator())
				{
					dynamic aValue = null;
					aValue = XVar.Clone(this.pSet.getAutoUpdateValue((XVar)(fName.Value)));
					if(!XVar.Equals(XVar.Pack(aValue), XVar.Pack("")))
					{
						this.cachedRecord.InitAndSetArrayItem(this.pSet.getAutoUpdateValue((XVar)(fName.Value)), fName.Value);
					}
				}
			}
			this.cachedRecord.InitAndSetArrayItem("", "...");
			return this.cachedRecord;
		}
		protected override XVar readRecord()
		{
			getCurrentRecordInternal();
			return true;
		}
		public override XVar fillControlFlags(dynamic _param_field, dynamic _param_required = null)
		{
			#region default values
			if(_param_required as Object == null) _param_required = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic required = XVar.Clone(_param_required);
			#endregion

			dynamic checkbox = null, data = null, gf = null, label = XVar.Array();
			gf = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
			data = XVar.Clone(getCurrentRecordInternal());
			checkbox = XVar.Clone(MVCFunctions.Concat("<input type=checkbox class=\"bs-updselbox\" id=updsel_", gf, this.id, " data-field=\"", MVCFunctions.runner_htmlspecialchars((XVar)(field)), "\">"));
			label = XVar.Clone(XVar.Array());
			label.InitAndSetArrayItem(checkbox, "begin");
			if((XVar)(required)  || (XVar)(this.pSet.isRequired((XVar)(field))))
			{
				label.InitAndSetArrayItem("&nbsp;<span class=\"icon-required\"></span>", "end");
			}
			this.xt.assign((XVar)(MVCFunctions.Concat(gf, "_label")), (XVar)(label));
			return null;
		}
		protected override XVar buildNewRecordData()
		{
			dynamic blobfields = null, efilename_values = null, evalues = null, keys = null, newFields = XVar.Array();
			evalues = XVar.Clone(XVar.Array());
			efilename_values = XVar.Clone(XVar.Array());
			blobfields = XVar.Clone(XVar.Array());
			keys = XVar.Clone(this.keys);
			newFields = XVar.Clone(MVCFunctions.array_intersect((XVar)(getPageFields()), (XVar)(this.selectedFields)));
			foreach (KeyValuePair<XVar, dynamic> f in newFields.GetEnumerator())
			{
				dynamic control = null;
				control = XVar.Clone(getControl((XVar)(f.Value), (XVar)(this.id)));
				control.readWebValue((XVar)(evalues), (XVar)(blobfields), new XVar(null), new XVar(null), (XVar)(efilename_values));
			}
			this.newRecordData = XVar.Clone(evalues);
			this.newRecordBlobFields = XVar.Clone(blobfields);
			return null;
		}
		protected virtual XVar getNewRecordCopy(dynamic _param_newRecordData)
		{
			#region pass-by-value parameters
			dynamic newRecordData = XVar.Clone(_param_newRecordData);
			#endregion

			dynamic newRecordCopy = XVar.Array();
			newRecordCopy = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> data in newRecordData.GetEnumerator())
			{
				newRecordCopy.InitAndSetArrayItem(data.Value, data.Key);
			}
			return newRecordCopy;
			return null;
		}
		public override XVar processDataInput()
		{
			dynamic newRecordData = null;
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
				setMessage((XVar)(this.message));
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> value in this.newRecordData.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.pSet.allowDuplicateValues((XVar)(value.Key)))))
				{
					this.errorFields.InitAndSetArrayItem(value.Key, null);
					setMessage((XVar)(MVCFunctions.Concat(this.pSet.label((XVar)(value.Key)), " ", CommonFunctions.mlang_message(new XVar("INLINE_DENY_DUPLICATES")))));
					return false;
				}
			}
			newRecordData = XVar.Clone(getNewRecordCopy((XVar)(this.newRecordData)));
			foreach (KeyValuePair<XVar, dynamic> s in this.parsedSelection.GetEnumerator())
			{
				dynamic fetchedArray = null, newRecordDataTemp = null;
				newRecordDataTemp = XVar.Clone(newRecordData);
				this.newRecordData = XVar.Clone(getNewRecordCopy((XVar)(newRecordDataTemp)));
				this.currentWhereExpr = XVar.Clone(getSingleRecordWhereClause((XVar)(s.Value)));
				GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(this.currentWhereExpr)));
				CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
				fetchedArray = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
				if(XVar.Pack(!(XVar)(fetchedArray)))
				{
					continue;
				}
				fetchedArray = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
				if(XVar.Pack(!(XVar)(isRecordEditable((XVar)(fetchedArray)))))
				{
					continue;
				}
				setUpdatedLatLng((XVar)(getNewRecordData()), (XVar)(fetchedArray));
				this.oldKeys = XVar.Clone(s.Value);
				this.recordBeingUpdated = XVar.Clone(fetchedArray);
				if(XVar.Pack(!(XVar)(callBeforeEditEvent())))
				{
					continue;
				}
				if(XVar.Pack(callCustomEditEvent()))
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.DoUpdateRecord(this))))
					{
						continue;
					}
				}
				++(this.nUpdated);
				mergeNewRecordData();
				auditLogEdit((XVar)(s.Value));
				callAfterEditEvent();
				if(XVar.Pack(isPopupMode()))
				{
					this.inlineReportData.InitAndSetArrayItem(getRowSaveStatusJSON((XVar)(s.Value)), this.rowIds[s.Key]);
				}
			}
			this.updatedSuccessfully = XVar.Clone(0 < this.nUpdated);
			if(XVar.Pack(!(XVar)(this.updatedSuccessfully)))
			{
				return false;
			}
			this.messageType = new XVar(Constants.MESSAGE_INFO);
			setSuccessfulEditMessage();
			callAfterSuccessfulSave();
			return true;
		}
		protected virtual XVar getRecordByKeys(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			dynamic fetchedArray = null, strSQLbak = null, strWhereClause = null, strWhereClauseBak = null;
			strWhereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(getSelectedWhereClause()), (XVar)(getSingleRecordWhereClause((XVar)(keys)))));
			GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(strWhereClause)));
			strSQLbak = XVar.Clone(GlobalVars.strSQL);
			strWhereClauseBak = XVar.Clone(strWhereClause);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryEdit"))))
			{
				this.eventsObject.BeforeQueryEdit((XVar)(GlobalVars.strSQL), ref strWhereClause, this);
			}
			if((XVar)(strSQLbak == GlobalVars.strSQL)  && (XVar)(strWhereClauseBak != strWhereClause))
			{
				dynamic keysSet = null;
				GlobalVars.strSQL = XVar.Clone(this.gQuery.gSQLWhere((XVar)(strWhereClause)));
				if(XVar.Pack(!(XVar)(keysSet)))
				{
					dynamic orderClause = null;
					GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
				}
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			fetchedArray = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
			return this.cipherer.DecryptFetchedArray((XVar)(fetchedArray));
		}
		protected override XVar setSuccessfulEditMessage()
		{
			dynamic message = null;
			message = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, "%succeed%", 1, "%total%")), (XVar)(new XVar(0, MVCFunctions.Concat("<strong>", this.nUpdated, "</strong>"), 1, MVCFunctions.Concat("<strong>", this.nSelected, "</strong>"))), new XVar("%succeed% out of %total% records updated successfully.")));
			setMessage((XVar)(message));
			if(this.nUpdated != this.nSelected)
			{
				message = XVar.Clone(MVCFunctions.str_replace(new XVar("%failed%"), (XVar)(MVCFunctions.Concat("<strong>", this.nSelected - this.nUpdated, "</strong>")), new XVar("%failed% records failed.")));
				setMessage((XVar)(message));
			}
			return null;
		}
		protected override XVar mergeNewRecordData()
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
		protected override XVar callAfterEditEvent()
		{
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("AfterEdit")))))
			{
				return null;
			}
			this.eventsObject.AfterEdit((XVar)(this.newRecordData), (XVar)(getKeysWhereClause(new XVar(false))), (XVar)(getOldRecordData()), (XVar)(this.keys), (XVar)(this.mode == Constants.EDIT_INLINE), this);
			return null;
		}
		protected override XVar callAfterSuccessfulSave()
		{
			foreach (KeyValuePair<XVar, dynamic> f in this.editFields.GetEnumerator())
			{
				getControl((XVar)(f.Value), (XVar)(this.id)).afterSuccessfulSave();
			}
			return null;
		}
		protected override XVar callCustomEditEvent()
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
				if(0 == MVCFunctions.strlen((XVar)(usermessage)))
				{
					++(this.nUpdated);
				}
				else
				{
					setMessage((XVar)(usermessage));
				}
			}
			return ret;
		}
		public override XVar captchaExists()
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
		protected override XVar recheckUserPermissions()
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
		protected override XVar SecurityRedirect()
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
		protected override XVar isRecordEditable(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
			{
				if(XVar.Pack(!(XVar)(GlobalVars.globalEvents.IsRecordEditable((XVar)(data), new XVar(true), (XVar)(this.tName)))))
				{
					return false;
				}
			}
			return true;
		}
		protected override XVar checkFieldOnPage(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			return this.pSet.appearOnUpdateSelected((XVar)(fName));
		}
		public override XVar getOldRecordData()
		{
			return this.recordBeingUpdated;
		}
		public override XVar setMessage(dynamic _param_message)
		{
			#region pass-by-value parameters
			dynamic message = XVar.Clone(_param_message);
			#endregion

			this.messages.InitAndSetArrayItem(message, null);
			return null;
		}
		public virtual XVar getMessages()
		{
			return MVCFunctions.implode(new XVar("<br>"), (XVar)(this.messages));
		}
		protected override XVar isPopupMode()
		{
			return this.mode == Constants.EDIT_SELECTED_POPUP;
		}
		protected override XVar isSimpleMode()
		{
			return this.mode == Constants.EDIT_SELECTED_SIMPLE;
		}
	}
}
