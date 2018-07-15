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
	public partial class ListPage_Lookup : ListPage_Embed
	{
		public dynamic linkField = XVar.Pack("");
		public dynamic lookupSelectField = XVar.Pack("");
		public dynamic customField = XVar.Pack("");
		public dynamic dispField = XVar.Pack("");
		public dynamic dispFieldAlias = XVar.Pack("");
		public dynamic lookupValuesArr = XVar.Array();
		public dynamic parentCtrlsData;
		public dynamic mainPageType;
		protected dynamic mainPSet;
		public dynamic mainRecordData;
		public dynamic mainRecordMasterTable;
		public dynamic mainContext;
		protected static bool skipListPage_LookupCtor = false;
		public ListPage_Lookup(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_LookupCtor)
			{
				skipListPage_LookupCtor = false;
				return;
			}
			initLookupParams();
			this.permis.InitAndSetArrayItem(1, this.tName, "search");
			this.jsSettings.InitAndSetArrayItem(this.permis[this.tName], "tableSettings", this.tName, "permissions");
			this.isUseAjaxSuggest = new XVar(false);
		}
		protected override XVar assignSessionPrefix()
		{
			this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.tName, "_lookup_", this.mainTable, "_", this.mainField));
			return null;
		}
		public virtual XVar initLookupParams()
		{
			if((XVar)(this.mainPageType != Constants.PAGE_ADD)  && (XVar)(this.mainPageType != Constants.PAGE_EDIT))
			{
				this.mainPageType = new XVar(Constants.PAGE_SEARCH);
			}
			this.mainPSet = XVar.Clone(new ProjectSettings((XVar)(this.mainTable), (XVar)(this.mainPageType)));
			this.linkField = XVar.Clone(this.mainPSet.getLinkField((XVar)(this.mainField)));
			this.dispField = XVar.Clone(this.mainPSet.getDisplayField((XVar)(this.mainField)));
			if(XVar.Pack(this.mainPSet.getCustomDisplay((XVar)(this.mainField))))
			{
				this.dispFieldAlias = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("dispFieldAlias"), new XVar("rrdf1")));
				this.customField = XVar.Clone(this.linkField);
			}
			outputFieldValue((XVar)(this.linkField), new XVar(2));
			outputFieldValue((XVar)(this.dispField), new XVar(2));
			if((XVar)(this.dispFieldAlias)  && (XVar)(this.pSet.appearOnListPage((XVar)(this.dispField))))
			{
				this.lookupSelectField = XVar.Clone(this.dispField);
			}
			else
			{
				if(XVar.Pack(this.pSet.appearOnListPage((XVar)(this.dispField))))
				{
					this.lookupSelectField = XVar.Clone(this.dispField);
				}
				else
				{
					this.lookupSelectField = XVar.Clone(this.listFields[0]["fName"]);
				}
			}
			this.mainContext = XVar.Clone(getMainContext());
			return null;
		}
		public override XVar displayMasterTableInfo()
		{
			return null;
		}
		public override XVar processMasterKeyValue()
		{
			return null;
		}
		protected virtual XVar getMainContext()
		{
			dynamic contextParams = XVar.Array();
			contextParams = XVar.Clone(XVar.Array());
			contextParams.InitAndSetArrayItem(this.mainRecordData, "data");
			if((XVar)(this.mainRecordMasterTable)  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.mainRecordMasterTable, "_masterRecordData"))))
			{
				contextParams.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.mainRecordMasterTable, "_masterRecordData")], "masterData");
			}
			return contextParams;
		}
		public virtual XVar clearLookupSessionData()
		{
			if(XVar.Pack(this.firstTime))
			{
				dynamic sessLookUpUnset = XVar.Array();
				sessLookUpUnset = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> value in XSession.Session.GetEnumerator())
				{
					if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(value.Key), new XVar("_lookup_"))), XVar.Pack(false)))
					{
						sessLookUpUnset.InitAndSetArrayItem(value.Key, null);
					}
				}
				foreach (KeyValuePair<XVar, dynamic> key in sessLookUpUnset.GetEnumerator())
				{
					XSession.Session.Remove(key.Value);
				}
			}
			return null;
		}
		public override XVar addCommonJs()
		{
			this.controlsMap.InitAndSetArrayItem(this.dispFieldAlias, "dispFieldAlias");
			addControlsJSAndCSS();
			addButtonHandlers();
			return null;
		}
		public override XVar addSpanVal(dynamic _param_fName, dynamic data)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if((XVar)(this.dispFieldAlias)  && (XVar)(this.arrFieldSpanVal[fName] == 2))
			{
				return MVCFunctions.Concat("val=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.dispFieldAlias])), "\" ");
			}
			else
			{
				return base.addSpanVal((XVar)(fName), (XVar)(data));
			}
			return null;
		}
		public override XVar getOrderByClause()
		{
			dynamic orderByField = null, strOrder = null;
			orderByField = XVar.Clone(this.mainPSet.getLookupOrderBy((XVar)(this.mainField)));
			if(XVar.Pack(MVCFunctions.strlen((XVar)(orderByField))))
			{
				strOrder = XVar.Clone(MVCFunctions.Concat(" ORDER BY ", getFieldSQL((XVar)(orderByField))));
				if(XVar.Pack(this.mainPSet.isLookupDesc((XVar)(this.mainField))))
				{
					strOrder = MVCFunctions.Concat(strOrder, " DESC");
				}
			}
			else
			{
				strOrder = XVar.Clone(base.getOrderByClause());
			}
			return strOrder;
		}
		protected virtual XVar getDependentDropdownFilter()
		{
			dynamic parentWhereParts = XVar.Array();
			if(XVar.Pack(!(XVar)(this.mainPSet.useCategory((XVar)(this.mainField)))))
			{
				return "";
			}
			if((XVar)(this.mainPageType != Constants.PAGE_SEARCH)  && (XVar)(!(XVar)(MVCFunctions.count(this.parentCtrlsData))))
			{
				return "1=0";
			}
			parentWhereParts = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> cData in this.mainPSet.getParentFieldsData((XVar)(this.mainField)).GetEnumerator())
			{
				dynamic arWhereClause = XVar.Array(), parentFieldName = null, parentFieldValues = XVar.Array();
				if(XVar.Pack(!(XVar)(this.parentCtrlsData.KeyExists(cData.Value["main"]))))
				{
					continue;
				}
				parentFieldName = XVar.Clone(cData.Value["lookup"]);
				parentFieldValues = XVar.Clone(CommonFunctions.splitvalues((XVar)(this.parentCtrlsData[cData.Value["main"]])));
				arWhereClause = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> value in parentFieldValues.GetEnumerator())
				{
					dynamic lookupValue = null;
					if(this.cipherer != null)
					{
						lookupValue = XVar.Clone(this.cipherer.MakeDBValue((XVar)(parentFieldName), (XVar)(value.Value)));
					}
					else
					{
						lookupValue = XVar.Clone(CommonFunctions.make_db_value((XVar)(parentFieldName), (XVar)(value.Value)));
					}
					arWhereClause.InitAndSetArrayItem(MVCFunctions.Concat(getFieldSQLDecrypt((XVar)(parentFieldName)), "=", lookupValue), null);
				}
				if(XVar.Pack(MVCFunctions.count(arWhereClause)))
				{
					parentWhereParts.InitAndSetArrayItem(MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" OR "), (XVar)(arWhereClause)), ")"), null);
				}
			}
			return MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" AND "), (XVar)(parentWhereParts)), ")");
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if(XVar.Pack(this.dispFieldAlias))
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ", this.dispField, " ");
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], "as ", this.connection.addFieldWrappers((XVar)(this.dispFieldAlias)));
			}
			sql.InitAndSetArrayItem(getLookupWizardWhere(), "mandatoryWhere", null);
			sql.InitAndSetArrayItem(getDependentDropdownFilter(), "mandatoryWhere", null);
			return sql;
		}
		protected virtual XVar getLookupWizardWhere()
		{
			dynamic where = null;
			RunnerContext.push((XVar)(new RunnerContextItem(new XVar(Constants.CONTEXT_ROW), (XVar)(this.mainContext))));
			where = XVar.Clone(CommonFunctions.prepareLookupWhere((XVar)(this.mainField), (XVar)(this.mainPSet)));
			RunnerContext.pop();
			return where;
		}
		public override XVar buildSearchPanel()
		{
			dynamic var_params = XVar.Array();
			if(XVar.Pack(!(XVar)(this.permis[this.tName]["search"])))
			{
				return null;
			}
			var_params = XVar.Clone(XVar.Array());
			var_params.InitAndSetArrayItem(this, "pageObj");
			var_params.InitAndSetArrayItem(this.panelSearchFields, "panelSearchFields");
			this.searchPanel = XVar.Clone(new SearchPanelLookup((XVar)(var_params)));
			this.searchPanel.buildSearchPanel();
			return null;
		}
		public virtual XVar addLookupVals()
		{
			this.controlsMap.InitAndSetArrayItem(this.lookupValuesArr, "lookupVals");
			return null;
		}
		public override XVar fillGridData()
		{
			base.fillGridData();
			addLookupVals();
			return null;
		}
		public override XVar fillCheckAttr(dynamic record, dynamic _param_data, dynamic _param_keyblock)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic keyblock = XVar.Clone(_param_keyblock);
			#endregion

			dynamic checkbox_attrs = null;
			checkbox_attrs = XVar.Clone(MVCFunctions.Concat("name=\"selection[]\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.linkField])), "\" id=\"check", this.recId, "\""));
			record.InitAndSetArrayItem(new XVar("begin", MVCFunctions.Concat("<input type='checkbox' ", checkbox_attrs, ">"), "data", XVar.Array()), "checkbox");
			return null;
		}
		public override XVar addSpansForGridCells(dynamic _param_type, dynamic record, dynamic _param_data = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(var_type == "add")
			{
				base.addSpansForGridCells((XVar)(var_type), (XVar)(record), (XVar)(data));
				return null;
			}
			if(XVar.Pack(!(XVar)(data == null)))
			{
				dynamic dispVal = null;
				if(XVar.Pack(this.dispFieldAlias))
				{
					dispVal = XVar.Clone(data[this.dispFieldAlias]);
				}
				else
				{
					dispVal = XVar.Clone(data[this.dispField]);
				}
				this.lookupValuesArr.InitAndSetArrayItem(new XVar("linkVal", data[this.linkField], "dispVal", dispVal), null);
			}
			return null;
		}
		public override XVar proccessRecordValue(dynamic data, dynamic keylink, dynamic _param_listFieldInfo)
		{
			#region pass-by-value parameters
			dynamic listFieldInfo = XVar.Clone(_param_listFieldInfo);
			#endregion

			dynamic value = null;
			value = XVar.Clone(base.proccessRecordValue((XVar)(data), (XVar)(keylink), (XVar)(listFieldInfo)));
			if(this.lookupSelectField == listFieldInfo["fName"])
			{
				value = XVar.Clone(MVCFunctions.Concat("<a href=\"#\" data-ind=\"", MVCFunctions.count(this.lookupValuesArr), "\" type=\"lookupSelect", this.id, "\">", value, "</a>"));
			}
			return value;
		}
		public override XVar showPage()
		{
			dynamic bricksExcept = XVar.Array();
			BeforeShowList();
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.xt.assign(new XVar("cancelbutton_block"), new XVar(true));
				this.xt.assign(new XVar("searchform_block"), new XVar(true));
				this.xt.assign(new XVar("searchform_showall"), new XVar(true));
				bricksExcept = XVar.Clone(new XVar(0, "grid_mobile", 1, "message", 2, "pagination", 3, "vmsearch2", 4, "cancelbutton_mobile"));
			}
			else
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "message", 2, "pagination", 3, "vsearch1", 4, "vsearch2", 5, "search", 6, "recordcontrols_new", 7, "bsgrid_tabs"));
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					bricksExcept.InitAndSetArrayItem("add", null);
					bricksExcept.InitAndSetArrayItem("reorder_records", null);
				}
			}
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			showPageAjax();
			return null;
		}
		public virtual XVar showPageAjax()
		{
			dynamic lookupSearchControls = null, returnJSON = XVar.Array();
			lookupSearchControls = XVar.Clone(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("searchform_text")), this.xt.fetch_loaded(new XVar("searchform_search")), this.xt.fetch_loaded(new XVar("searchform_showall"))));
			this.xt.assign(new XVar("lookupSearchControls"), (XVar)(lookupSearchControls));
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			returnJSON.InitAndSetArrayItem(MVCFunctions.Concat("<h2>", getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</h2>"), "headerCont");
			returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			returnJSON.InitAndSetArrayItem(this.flyId, "idStartFrom");
			returnJSON.InitAndSetArrayItem(true, "success");
			returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "CSSFiles");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			return null;
		}
		public override XVar SecuritySQL(dynamic _param_strAction, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic strAction = XVar.Clone(_param_strAction);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic strPerm = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(table)))))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(table)));
			if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)))
			{
				strPerm = MVCFunctions.Concat(strPerm, "S");
			}
			return CommonFunctions.SecuritySQL((XVar)(strAction), (XVar)(table), (XVar)(strPerm));
		}
		public override XVar displayTabsInPage()
		{
			return true;
		}
		public override XVar buildTotals(dynamic totals)
		{
			return null;
		}
		public override XVar getMasterTableSQLClause(dynamic _param_basedOnProp = null)
		{
			#region default values
			if(_param_basedOnProp as Object == null) _param_basedOnProp = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic basedOnProp = XVar.Clone(_param_basedOnProp);
			#endregion

			return "";
		}
		public override XVar deleteAvailable()
		{
			return false;
		}
		public override XVar importAvailable()
		{
			return false;
		}
		public override XVar editAvailable()
		{
			return false;
		}
		public override XVar addAvailable()
		{
			return false;
		}
		public override XVar copyAvailable()
		{
			return false;
		}
		public override XVar inlineAddAvailable()
		{
			return (XVar)(base.inlineAddAvailable())  && (XVar)(this.mainPSet.isAllowToAdd((XVar)(this.mainField)));
		}
		public override XVar inlineEditAvailable()
		{
			return false;
		}
		public override XVar viewAvailable()
		{
			return false;
		}
		public override XVar exportAvailable()
		{
			return false;
		}
		public override XVar printAvailable()
		{
			return false;
		}
		public override XVar advSearchAvailable()
		{
			return false;
		}
		public override XVar detailsInGridAvailable()
		{
			return false;
		}
		public override XVar updateSelectedAvailable()
		{
			return false;
		}
	}
}
