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
	public partial class LookupField : EditControl
	{
		public dynamic lookupTable = XVar.Pack("");
		public dynamic lookupType = XVar.Pack(0);
		public dynamic LCType = XVar.Pack(0);
		public dynamic ciphererLookup = XVar.Pack(null);
		public dynamic displayFieldName = XVar.Pack("");
		public dynamic linkFieldName = XVar.Pack("");
		public dynamic linkAndDisplaySame = XVar.Pack(false);
		public dynamic linkFieldIndex = XVar.Pack(0);
		public dynamic displayFieldIndex = XVar.Pack(0);
		public dynamic lookupSize = XVar.Pack(1);
		public dynamic multiple = XVar.Pack("");
		public dynamic postfix = XVar.Pack("");
		public dynamic alt = XVar.Pack("");
		public dynamic clookupfield = XVar.Pack("");
		public dynamic openlookup = XVar.Pack("");
		public dynamic bUseCategory = XVar.Pack(false);
		public dynamic horizontalLookup = XVar.Pack(false);
		public dynamic addNewItem = XVar.Pack(false);
		public dynamic isLinkFieldEncrypted = XVar.Pack(false);
		public dynamic isDisplayFieldEncrypted = XVar.Pack(false);
		public dynamic lookupPageType = XVar.Pack("");
		public dynamic lookupPSet = XVar.Pack(null);
		public dynamic multiselect = XVar.Pack(false);
		public dynamic lwLinkField = XVar.Pack("");
		public dynamic lwDisplayFieldWrapped = XVar.Pack("");
		public dynamic customDisplay = XVar.Pack("");
		public dynamic tName = XVar.Pack("");
		protected dynamic lookupTableAliases = XVar.Array();
		protected dynamic linkFieldAliases = XVar.Array();
		protected dynamic displayFieldAliases = XVar.Array();
		protected dynamic searchByDisplayedFieldIsAllowed = XVar.Pack(null);
		protected dynamic lookupConnection;
		protected static bool skipLookupFieldCtor = false;
		public LookupField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipLookupFieldCtor)
			{
				skipLookupFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.tName = XVar.Clone(this.pageObject.tName);
			if(XVar.Pack(this.pageObject.tableBasedSearchPanelAdded))
			{
				this.tName = XVar.Clone(this.pageObject.searchTableName);
			}
			this.format = new XVar(Constants.EDIT_FORMAT_TEXT_FIELD);
			if((XVar)(pageObject.pageType == Constants.PAGE_LIST)  || (XVar)(!(XVar)(pageObject.isPageTableBased())))
			{
				this.lookupPageType = new XVar(Constants.PAGE_SEARCH);
			}
			else
			{
				this.lookupPageType = XVar.Clone(pageObject.pageType);
			}
			this.lookupTable = XVar.Clone(this.pageObject.pSetEdit.getLookupTable((XVar)(this.field)));
			this.lookupType = XVar.Clone(this.pageObject.pSetEdit.getLookupType((XVar)(this.field)));
			setLookupConnection();
			if(this.lookupType == Constants.LT_QUERY)
			{
				this.lookupPSet = XVar.Clone(new ProjectSettings((XVar)(this.lookupTable)));
			}
			this.displayFieldName = XVar.Clone(this.pageObject.pSetEdit.getDisplayField((XVar)(this.field)));
			this.linkFieldName = XVar.Clone(this.pageObject.pSetEdit.getLinkField((XVar)(this.field)));
			this.linkAndDisplaySame = XVar.Clone(this.displayFieldName == this.linkFieldName);
			if(this.lookupType == Constants.LT_QUERY)
			{
				this.ciphererLookup = XVar.Clone(new RunnerCipherer((XVar)(this.lookupTable)));
			}
			this.LCType = XVar.Clone(this.pageObject.pSetEdit.lookupControlType((XVar)(this.field)));
			this.multiselect = XVar.Clone(this.pageObject.pSetEdit.multiSelect((XVar)(this.field)));
			this.customDisplay = XVar.Clone(this.pageObject.pSetEdit.getCustomDisplay((XVar)(this.field)));
			this.lwLinkField = XVar.Clone(this.lookupConnection.addFieldWrappers((XVar)(this.linkFieldName)));
			this.lwDisplayFieldWrapped = XVar.Clone(RunnerPage.sqlFormattedDisplayField((XVar)(this.field), (XVar)(this.lookupConnection), (XVar)(this.pageObject.pSetEdit)));
			this.lookupSize = XVar.Clone(this.pageObject.pSetEdit.selectSize((XVar)(this.field)));
			this.bUseCategory = XVar.Clone(this.pageObject.pSetEdit.useCategory((XVar)(this.field)));
		}
		protected virtual XVar setLookupConnection()
		{
			dynamic connId = null;
			if(this.lookupType == Constants.LT_QUERY)
			{
				this.lookupConnection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.lookupTable)));
				return null;
			}
			connId = XVar.Clone(this.pageObject.pSetEdit.getNotProjectLookupTableConnId((XVar)(this.field)));
			this.lookupConnection = XVar.Clone((XVar.Pack(MVCFunctions.strlen((XVar)(connId))) ? XVar.Pack(GlobalVars.cman.byId((XVar)(connId))) : XVar.Pack(GlobalVars.cman.getDefault())));
			return null;
		}
		public override XVar makeWidthStyle(dynamic _param_widthPx)
		{
			#region pass-by-value parameters
			dynamic widthPx = XVar.Clone(_param_widthPx);
			#endregion

			if(!XVar.Equals(XVar.Pack(this.LCType), XVar.Pack(Constants.LCT_DROPDOWN)))
			{
				return base.makeWidthStyle((XVar)(widthPx));
			}
			if(XVar.Pack(0) == widthPx)
			{
				return "";
			}
			return MVCFunctions.Concat("width: ", widthPx + 7, "px");
		}
		public override XVar addJSFiles()
		{
			if((XVar)(this.multiselect)  && (XVar)((XVar)((XVar)((XVar)(this.LCType == Constants.LCT_DROPDOWN)  && (XVar)(this.lookupSize == 1))  || (XVar)(this.LCType == Constants.LCT_AJAX))  || (XVar)(this.LCType == Constants.LCT_LIST)))
			{
				this.pageObject.AddJSFile(new XVar("include/chosen/chosen.jquery.js"));
			}
			return null;
		}
		public override XVar addCSSFiles()
		{
			if((XVar)(this.multiselect)  && (XVar)((XVar)((XVar)((XVar)(this.LCType == Constants.LCT_DROPDOWN)  && (XVar)(this.lookupSize == 1))  || (XVar)(this.LCType == Constants.LCT_AJAX))  || (XVar)(this.LCType == Constants.LCT_LIST)))
			{
				if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					this.pageObject.AddCSSFile(new XVar("include/chosen/bootstrap-chosen.css"));
				}
				else
				{
					this.pageObject.AddCSSFile(new XVar("include/chosen/chosen.css"));
				}
			}
			return null;
		}
		public virtual XVar parentBuildControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			return null;
		}
		public override XVar buildControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic avalue = null, searchOption = null, suffix = null;
			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			this.alt = XVar.Clone((XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508)) ? XVar.Pack(MVCFunctions.Concat(" alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(this.strLabel)), "\" ")) : XVar.Pack("")));
			suffix = XVar.Clone(MVCFunctions.Concat("_", MVCFunctions.GoodFieldName((XVar)(this.field)), "_", this.id));
			this.clookupfield = XVar.Clone(MVCFunctions.Concat("display_value", (XVar.Pack(fieldNum) ? XVar.Pack(fieldNum) : XVar.Pack("")), suffix));
			this.openlookup = XVar.Clone(MVCFunctions.Concat("open_lookup", suffix));
			this.cfield = XVar.Clone(MVCFunctions.Concat("value", suffix));
			this.ctype = XVar.Clone(MVCFunctions.Concat("type", suffix));
			if(XVar.Pack(fieldNum))
			{
				this.cfield = XVar.Clone(MVCFunctions.Concat("value", fieldNum, suffix));
				this.ctype = XVar.Clone(MVCFunctions.Concat("type", fieldNum, suffix));
			}
			if(XVar.Pack(this.ciphererLookup))
			{
				this.isLinkFieldEncrypted = XVar.Clone(this.ciphererLookup.isFieldPHPEncrypted((XVar)(this.linkFieldName)));
			}
			this.horizontalLookup = XVar.Clone(this.pageObject.pSetEdit.isHorizontalLookup((XVar)(this.field)));
			addMainFieldsSettings();
			this.addNewItem = XVar.Clone(isAllowToAdd((XVar)(mode)));
			this.multiple = XVar.Clone((XVar.Pack(this.multiselect) ? XVar.Pack(" multiple") : XVar.Pack("")));
			this.postfix = XVar.Clone((XVar.Pack(this.multiselect) ? XVar.Pack("[]") : XVar.Pack("")));
			if(XVar.Pack(this.multiselect))
			{
				avalue = XVar.Clone(CommonFunctions.splitvalues((XVar)(value)));
			}
			else
			{
				avalue = XVar.Clone(new XVar(0, value));
			}
			searchOption = XVar.Clone(additionalCtrlParams["option"]);
			if(this.lookupType == Constants.LT_LISTOFVALUES)
			{
				buildListOfValues((XVar)(avalue), (XVar)(value), (XVar)(mode), (XVar)(searchOption));
			}
			else
			{
				if(XVar.Pack(this.ciphererLookup))
				{
					this.isDisplayFieldEncrypted = XVar.Clone(this.ciphererLookup.isFieldPHPEncrypted((XVar)(this.displayFieldName)));
				}
				if((XVar)(this.LCType == Constants.LCT_AJAX)  || (XVar)(this.LCType == Constants.LCT_LIST))
				{
					buildAJAXLookup((XVar)(avalue), (XVar)(value), (XVar)(mode), (XVar)(searchOption));
				}
				else
				{
					buildClassicLookup((XVar)(avalue), (XVar)(value), (XVar)(mode), (XVar)(searchOption));
				}
			}
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		protected virtual XVar isAllowToAdd(dynamic _param_mode)
		{
			#region pass-by-value parameters
			dynamic mode = XVar.Clone(_param_mode);
			#endregion

			dynamic addNewItem = null, strPerm = null;
			addNewItem = new XVar(false);
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(this.lookupTable)));
			if((XVar)((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("A"))), XVar.Pack(false)))  && (XVar)(this.LCType != Constants.LCT_LIST))  && (XVar)(mode != Constants.MODE_SEARCH))
			{
				dynamic advancedadd = null;
				addNewItem = XVar.Clone(this.pageObject.pSetEdit.isAllowToAdd((XVar)(this.field)));
				advancedadd = XVar.Clone(!(XVar)(this.pageObject.pSetEdit.isSimpleAdd((XVar)(this.field))));
				if((XVar)(!(XVar)(advancedadd))  || (XVar)(this.pageObject.pageType == Constants.PAGE_REGISTER))
				{
					addNewItem = new XVar(false);
				}
			}
			return addNewItem;
		}
		protected virtual XVar addMainFieldsSettings()
		{
			dynamic mainFields = XVar.Array(), mainMasterFields = XVar.Array(), where = null;
			if(XVar.Pack(this.pageObject.pSetEdit.isLookupWhereCode((XVar)(this.field))))
			{
				return null;
			}
			mainMasterFields = XVar.Clone(XVar.Array());
			mainFields = XVar.Clone(XVar.Array());
			where = XVar.Clone(this.pageObject.pSetEdit.getLookupWhere((XVar)(this.field)));
			foreach (KeyValuePair<XVar, dynamic> token in DB.readSQLTokens((XVar)(where)).GetEnumerator())
			{
				dynamic dotPos = null, field = null, prefix = null;
				prefix = new XVar("");
				field = XVar.Clone(token.Value);
				dotPos = XVar.Clone(MVCFunctions.strpos((XVar)(token.Value), new XVar(".")));
				if(!XVar.Equals(XVar.Pack(dotPos), XVar.Pack(false)))
				{
					prefix = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(token.Value), new XVar(0), (XVar)(dotPos)))));
					field = XVar.Clone(MVCFunctions.substr((XVar)(token.Value), (XVar)(dotPos + 1)));
				}
				if(prefix == "master")
				{
					mainMasterFields.InitAndSetArrayItem(field, null);
				}
				else
				{
					if(XVar.Pack(!(XVar)(prefix)))
					{
						mainFields.InitAndSetArrayItem(field, null);
					}
				}
			}
			addJSSetting(new XVar("mainFields"), (XVar)(mainFields));
			addJSSetting(new XVar("mainMasterFields"), (XVar)(mainMasterFields));
			return null;
		}
		public virtual XVar fillLookupFieldsIndexes()
		{
			dynamic lookupIndexes = XVar.Array();
			lookupIndexes = XVar.Clone(CommonFunctions.GetLookupFieldsIndexes((XVar)(this.pageObject.pSetEdit), (XVar)(this.field)));
			this.linkFieldIndex = XVar.Clone(lookupIndexes["linkFieldIndex"]);
			this.displayFieldIndex = XVar.Clone(lookupIndexes["displayFieldIndex"]);
			return null;
		}
		public virtual XVar buildListOfValues(dynamic _param_avalue, dynamic _param_value, dynamic _param_mode, dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic avalue = XVar.Clone(_param_avalue);
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			dynamic arr = XVar.Array(), dataAttr = null, dropDownHasSimpleBox = null, i = null, optionContains = null, res = null, selectClass = null, spacer = null;
			arr = XVar.Clone(this.pageObject.pSetEdit.getLookupValues((XVar)(this.field)));
			dropDownHasSimpleBox = XVar.Clone((XVar)((XVar)(this.LCType == Constants.LCT_DROPDOWN)  && (XVar)(!(XVar)(this.multiselect)))  && (XVar)(mode == Constants.MODE_SEARCH));
			optionContains = XVar.Clone((XVar)(dropDownHasSimpleBox)  && (XVar)(isSearchOpitonForSimpleBox((XVar)(searchOption))));
			if(XVar.Pack(this.multiselect))
			{
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"multiselect\">"));
			}
			switch(((XVar)this.LCType).ToInt())
			{
				case Constants.LCT_DROPDOWN:
					dataAttr = XVar.Clone(selectClass = new XVar(""));
					if(XVar.Pack(dropDownHasSimpleBox))
					{
						dynamic simpleBoxClass = null, simpleBoxStyle = null;
						dataAttr = new XVar(" data-usesuggests=\"true\"");
						selectClass = XVar.Clone((XVar.Pack(optionContains) ? XVar.Pack(" class=\"rnr-hiddenControlSubelem\" ") : XVar.Pack("")));
						simpleBoxClass = XVar.Clone((XVar.Pack(optionContains) ? XVar.Pack("") : XVar.Pack(" class=\"rnr-hiddenControlSubelem\" ")));
						simpleBoxStyle = XVar.Clone(getWidthStyleForAdditionalControl());
						MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "_simpleSearchBox\" type=\"text\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\" autocomplete=\"off\"", simpleBoxClass, " ", simpleBoxStyle, ">"));
					}
					MVCFunctions.Echo(MVCFunctions.Concat("<select id=\"", this.cfield, "\" size=\"", this.lookupSize, "\" ", dataAttr, selectClass, " name=\"", this.cfield, this.postfix, "\" ", this.multiple, " ", this.inputStyle, ">"));
					if(XVar.Pack(!(XVar)(this.multiselect)))
					{
						MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"\">", "Please select", "</option>"));
					}
					else
					{
						if(mode == Constants.MODE_SEARCH)
						{
							MVCFunctions.Echo("<option value=\"\"> </option>");
						}
					}
					foreach (KeyValuePair<XVar, dynamic> opt in arr.GetEnumerator())
					{
						res = XVar.Clone(MVCFunctions.array_search((XVar)(opt.Value), (XVar)(avalue)));
						if(!XVar.Equals(XVar.Pack(res), XVar.Pack(false)))
						{
							MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "\" selected>", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "</option>"));
						}
						else
						{
							MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "\">", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "</option>"));
						}
					}
					MVCFunctions.Echo("</select>");
					break;
				case Constants.LCT_CBLIST:
					MVCFunctions.Echo(MVCFunctions.Concat("<div data-lookup-options class=\"", (XVar.Pack(this.horizontalLookup) ? XVar.Pack("rnr-horizontal-lookup") : XVar.Pack("rnr-vertical-lookup")), "\">"));
					spacer = new XVar("<br/>");
					if(XVar.Pack(this.horizontalLookup))
					{
						spacer = new XVar("&nbsp;&nbsp;");
					}
					i = new XVar(0);
					foreach (KeyValuePair<XVar, dynamic> opt in arr.GetEnumerator())
					{
						MVCFunctions.Echo("<span class=\"checkbox\"><label>");
						MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "_", i, "\" class=\"rnr-checkbox\" type=\"checkbox\" ", this.alt, " name=\"", this.cfield, this.postfix, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "\""));
						res = XVar.Clone(MVCFunctions.array_search((XVar)(opt.Value), (XVar)(avalue)));
						if(!XVar.Equals(XVar.Pack(res), XVar.Pack(false)))
						{
							MVCFunctions.Echo(" checked=\"checked\" ");
						}
						MVCFunctions.Echo("/>");
						MVCFunctions.Echo(MVCFunctions.Concat("&nbsp;<b class=\"rnr-checkbox-label\" id=\"data_", this.cfield, "_", i, "\">", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "</b>", spacer));
						MVCFunctions.Echo("</label></span>");
						i++;
					}
					MVCFunctions.Echo("</div>");
					break;
				case Constants.LCT_RADIO:
					MVCFunctions.Echo(MVCFunctions.Concat("<div data-lookup-options class=\"", (XVar.Pack(this.horizontalLookup) ? XVar.Pack("rnr-horizontal-lookup") : XVar.Pack("rnr-vertical-lookup")), "\">"));
					MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"hidden\" name=\"", this.cfield, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\">"));
					i = new XVar(0);
					foreach (KeyValuePair<XVar, dynamic> opt in arr.GetEnumerator())
					{
						dynamic var_checked = null;
						var_checked = new XVar("");
						if(opt.Value == value)
						{
							var_checked = new XVar(" checked=\"checked\" ");
						}
						MVCFunctions.Echo("<span class=\"checkbox\"><label>");
						MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"Radio\" class=\"rnr-radio-button\" id=\"radio_", this.cfieldname, "_", i, "\" ", this.alt, " name=\"radio_", this.cfieldname, "\" ", var_checked, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "\">", " <span id=\"label_radio_", this.cfieldname, "_", i, "\" class=\"rnr-radio-label\">", MVCFunctions.runner_htmlspecialchars((XVar)(opt.Value)), "</span >"));
						MVCFunctions.Echo("</label></span>");
						i++;
					}
					MVCFunctions.Echo("</div>");
					break;
			}
			return null;
		}
		public virtual XVar buildAJAXLookup(dynamic _param_avalue, dynamic _param_value, dynamic _param_mode, dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic avalue = XVar.Clone(_param_avalue);
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			dynamic data = XVar.Array(), dataAttr = null, inputParams = null, inputTag = null, listOptionContains = null, listSearchHasSimpleBox = null, lookupSQL = null, lookup_value = null, optionContains = null, qResult = null;
			if(XVar.Pack(this.multiselect))
			{
				buildMultiselectAJAXLookup((XVar)(avalue), (XVar)(value), (XVar)(mode), (XVar)(searchOption));
				return null;
			}
			listSearchHasSimpleBox = XVar.Clone((XVar)(mode == Constants.MODE_SEARCH)  && (XVar)(isAdditionalControlRequired()));
			optionContains = XVar.Clone(isSearchOpitonForSimpleBox((XVar)(searchOption)));
			listOptionContains = XVar.Clone((XVar)(listSearchHasSimpleBox)  && (XVar)(optionContains));
			dataAttr = new XVar("");
			if(this.LCType == Constants.LCT_LIST)
			{
				dataAttr = XVar.Clone((XVar.Pack(listSearchHasSimpleBox) ? XVar.Pack(" data-usesuggests=\"true\"") : XVar.Pack("")));
			}
			else
			{
				if((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(optionContains))
				{
					dataAttr = new XVar(" data-simple-search-mode=\"true\" ");
				}
			}
			if(XVar.Pack(this.bUseCategory))
			{
				dynamic valueAttr = null;
				valueAttr = new XVar("");
				if((XVar)((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(optionContains))  || (XVar)((XVar)(this.LCType == Constants.LCT_LIST)  && (XVar)(listOptionContains)))
				{
					valueAttr = XVar.Clone(MVCFunctions.Concat(" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\""));
				}
				inputParams = XVar.Clone(MVCFunctions.Concat("\" autocomplete=\"off\" id=\"", this.clookupfield, "\" ", valueAttr, " name=\"", this.clookupfield, "\" ", this.inputStyle));
				inputParams = MVCFunctions.Concat(inputParams, (XVar.Pack((XVar)(this.LCType == Constants.LCT_LIST)  && (XVar)(!(XVar)(listOptionContains))) ? XVar.Pack("readonly") : XVar.Pack("")));
				MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"text\" ", inputParams, ">"));
				MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"hidden\" id=\"", this.cfield, "\" ", valueAttr, " name=\"", this.cfield, "\"", dataAttr, ">"));
				MVCFunctions.Echo(getLookupLinks((XVar)(listOptionContains)));
				return null;
			}
			lookup_value = new XVar("");
			lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(value), new XVar(false), new XVar(true)));
			fillLookupFieldsIndexes();
			qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
			if(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				if(XVar.Pack(this.isDisplayFieldEncrypted))
				{
					lookup_value = XVar.Clone(this.ciphererLookup.DecryptField((XVar)(this.displayFieldName), (XVar)(data[this.displayFieldIndex])));
				}
				else
				{
					lookup_value = XVar.Clone(data[this.displayFieldIndex]);
				}
			}
			else
			{
				if(XVar.Pack(this.pageObject.pSetEdit.isLookupWhereSet((XVar)(this.field))))
				{
					lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(value), new XVar(false), new XVar(true), new XVar(false)));
					qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
					if(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
					{
						if(XVar.Pack(this.isDisplayFieldEncrypted))
						{
							lookup_value = XVar.Clone(this.ciphererLookup.DecryptField((XVar)(this.displayFieldName), (XVar)(data[this.displayFieldIndex])));
						}
						else
						{
							lookup_value = XVar.Clone(data[this.displayFieldIndex]);
						}
					}
				}
			}
			if((XVar)((XVar)((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(lookup_value)))))  && (XVar)((XVar)(this.pageObject.pSetEdit.isFreeInput((XVar)(this.field)))  || (XVar)(this.lookupPageType == Constants.PAGE_SEARCH)))  || (XVar)((XVar)(this.LCType == Constants.LCT_LIST)  && (XVar)(listOptionContains)))
			{
				lookup_value = XVar.Clone(value);
			}
			inputParams = XVar.Clone(MVCFunctions.Concat("autocomplete=\"off\" id=\"", this.clookupfield, "\" name=\"", this.clookupfield, "\" ", this.inputStyle, this.alt));
			inputParams = MVCFunctions.Concat(inputParams, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(lookup_value)), "\"");
			if((XVar)(this.LCType == Constants.LCT_LIST)  && (XVar)(!(XVar)(listOptionContains)))
			{
				inputParams = MVCFunctions.Concat(inputParams, " readonly ");
			}
			if((XVar)(this.LCType == Constants.LCT_LIST)  && (XVar)(!(XVar)(this.pageObject.pSetEdit.isRequired((XVar)(this.field)))))
			{
				inputParams = MVCFunctions.Concat(inputParams, " class=\"clearable\" ");
			}
			inputTag = XVar.Clone(MVCFunctions.Concat("<input type=\"text\" ", inputParams, ">"));
			if(this.LCType == Constants.LCT_LIST)
			{
				MVCFunctions.Echo(MVCFunctions.Concat("<span class=\"bs-list-lookup\">", inputTag, "</span>"));
			}
			else
			{
				MVCFunctions.Echo(inputTag);
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<input type=\"hidden\" id=\"", this.cfield, "\" name=\"", this.cfield, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\"", dataAttr, ">"));
			MVCFunctions.Echo(getLookupLinks((XVar)(listOptionContains)));
			return null;
		}
		protected virtual XVar buildMultiselectAJAXLookup(dynamic _param_avalue, dynamic _param_value, dynamic _param_mode, dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic avalue = XVar.Clone(_param_avalue);
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			MVCFunctions.Echo(MVCFunctions.Concat("<select ", this.multiple, " id=\"", this.cfield, "\" name=\"", this.cfield, this.postfix, "\" ", this.inputStyle, this.alt, ">"));
			if((XVar)(!(XVar)(this.bUseCategory))  && (XVar)(MVCFunctions.strlen((XVar)(value))))
			{
				buildMultiselectAJAXLookupRows((XVar)(avalue), (XVar)(value), (XVar)(mode), (XVar)(searchOption));
			}
			MVCFunctions.Echo("</select>");
			MVCFunctions.Echo(getLookupLinks());
			return null;
		}
		protected virtual XVar buildMultiselectAJAXLookupRows(dynamic _param_avalue, dynamic _param_value, dynamic _param_mode, dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic avalue = XVar.Clone(_param_avalue);
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			dynamic data = XVar.Array(), lookupSQL = null, options = null, qResult = null;
			fillLookupFieldsIndexes();
			if((XVar)(this.linkAndDisplaySame)  || (XVar)(this.lookupPageType == Constants.PAGE_SEARCH))
			{
				foreach (KeyValuePair<XVar, dynamic> mValue in avalue.GetEnumerator())
				{
					data = XVar.Clone(XVar.Array());
					data.InitAndSetArrayItem(mValue.Value, this.linkFieldIndex);
					data.InitAndSetArrayItem(mValue.Value, this.displayFieldIndex);
					buildLookupRow((XVar)(mode), (XVar)(data), new XVar(" selected"), (XVar)(mValue.Key));
				}
				return null;
			}
			lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(value), new XVar(false), new XVar(true)));
			qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
			options = new XVar(0);
			while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				decryptDataRow((XVar)(data));
				if(!XVar.Equals(XVar.Pack(MVCFunctions.array_search((XVar)(data[this.linkFieldIndex]), (XVar)(avalue))), XVar.Pack(false)))
				{
					buildLookupRow((XVar)(mode), (XVar)(data), new XVar(" selected"), (XVar)(options));
					options++;
				}
			}
			if((XVar)((XVar)((XVar)(options == XVar.Pack(0))  && (XVar)(MVCFunctions.strlen((XVar)(value))))  && (XVar)(mode == Constants.MODE_EDIT))  && (XVar)(this.pageObject.pSetEdit.isLookupWhereSet((XVar)(this.field))))
			{
				lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(value), new XVar(false), new XVar(true), new XVar(false), new XVar(true)));
				qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
				if(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
				{
					decryptDataRow((XVar)(data));
					buildLookupRow((XVar)(mode), (XVar)(data), new XVar(" selected"), (XVar)(options));
				}
			}
			return null;
		}
		public virtual XVar buildClassicLookup(dynamic _param_avalue, dynamic _param_value, dynamic _param_mode, dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic avalue = XVar.Clone(_param_avalue);
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			dynamic data = XVar.Array(), dataAttr = null, dropDownHasSimpleBox = null, footer = null, found = null, i = null, isLookupUnique = null, lookupSQL = null, optionContains = null, qResult = null, res = null, selectClass = null, simpleBoxClass = null, simpleBoxStyle = null, uniqueArray = XVar.Array(), var_checked = null;
			dropDownHasSimpleBox = XVar.Clone((XVar)((XVar)(this.LCType == Constants.LCT_DROPDOWN)  && (XVar)(mode == Constants.MODE_SEARCH))  && (XVar)(isAdditionalControlRequired()));
			optionContains = XVar.Clone((XVar)(dropDownHasSimpleBox)  && (XVar)(isSearchOpitonForSimpleBox((XVar)(searchOption))));
			if(XVar.Pack(this.multiselect))
			{
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"multiselect\">"));
			}
			if(XVar.Pack(this.bUseCategory))
			{
				switch(((XVar)this.LCType).ToInt())
				{
					case Constants.LCT_CBLIST:
						MVCFunctions.Echo("<div data-lookup-options>");
						MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"checkbox\" name=\"", this.cfield, "\" style=\"display:none;\">"));
						MVCFunctions.Echo("</div>");
						break;
					case Constants.LCT_RADIO:
						MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"hidden\" name=\"", this.cfield, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\">"));
						MVCFunctions.Echo("<div data-lookup-options>");
						MVCFunctions.Echo("</div>");
						break;
					case Constants.LCT_DROPDOWN:
						dataAttr = new XVar("");
						selectClass = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("form-control") : XVar.Pack("")));
						simpleBoxClass = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("form-control") : XVar.Pack("")));
						if(XVar.Pack(dropDownHasSimpleBox))
						{
							dataAttr = new XVar(" data-usesuggests=\"true\"");
							selectClass = MVCFunctions.Concat(selectClass, (XVar.Pack(optionContains) ? XVar.Pack(" rnr-hiddenControlSubelem") : XVar.Pack("")));
							simpleBoxClass = MVCFunctions.Concat(simpleBoxClass, (XVar.Pack(optionContains) ? XVar.Pack("") : XVar.Pack(" rnr-hiddenControlSubelem")));
							simpleBoxStyle = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("") : XVar.Pack(getWidthStyleForAdditionalControl())));
							MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "_simpleSearchBox\" type=\"text\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\" autocomplete=\"off\" class=\"", simpleBoxClass, "\" ", simpleBoxStyle, ">"));
						}
						MVCFunctions.Echo(MVCFunctions.Concat("<select size=\"", this.lookupSize, "\" id=\"", this.cfield, "\" name=\"", this.cfield, this.postfix, "\" class=\"", selectClass, "\" ", dataAttr, this.multiple, " ", this.inputStyle, ">"));
						MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"\">", "Please select", "</option>"));
						MVCFunctions.Echo("</select>");
						break;
				}
				MVCFunctions.Echo(getLookupLinks());
				return null;
			}
			lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), new XVar(""), new XVar(false)));
			qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
			fillLookupFieldsIndexes();
			if(this.LCType == Constants.LCT_DROPDOWN)
			{
				dataAttr = new XVar("");
				selectClass = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("form-control") : XVar.Pack("")));
				simpleBoxClass = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("form-control") : XVar.Pack("")));
				if(XVar.Pack(dropDownHasSimpleBox))
				{
					dataAttr = new XVar(" data-usesuggests=\"true\"");
					selectClass = MVCFunctions.Concat(selectClass, (XVar.Pack(optionContains) ? XVar.Pack(" rnr-hiddenControlSubelem") : XVar.Pack("")));
					simpleBoxClass = MVCFunctions.Concat(simpleBoxClass, (XVar.Pack(optionContains) ? XVar.Pack("") : XVar.Pack(" rnr-hiddenControlSubelem")));
					simpleBoxStyle = XVar.Clone((XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack("") : XVar.Pack(getWidthStyleForAdditionalControl())));
					MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "_simpleSearchBox\" type=\"text\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\" autocomplete=\"off\" class=\"", simpleBoxClass, "\" ", simpleBoxStyle, ">"));
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<select size=\"", this.lookupSize, "\" id=\"", this.cfield, "\" ", this.alt, " name=\"", this.cfield, this.postfix, "\"", dataAttr, " class=\"", selectClass, "\" ", this.multiple, " ", this.inputStyle, ">"));
				if(XVar.Pack(!(XVar)(this.multiselect)))
				{
					MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"\">", "Please select", "</option>"));
				}
				else
				{
					if(mode == Constants.MODE_SEARCH)
					{
						MVCFunctions.Echo("<option value=\"\"> </option>");
					}
				}
			}
			else
			{
				if(this.LCType == Constants.LCT_RADIO)
				{
					MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"hidden\" name=\"", this.cfield, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(value)), "\">"));
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<div data-lookup-options class=\"", (XVar.Pack(this.horizontalLookup) ? XVar.Pack("rnr-horizontal-lookup") : XVar.Pack("rnr-vertical-lookup")), "\">"));
			}
			found = new XVar(false);
			i = new XVar(0);
			isLookupUnique = XVar.Clone((XVar)(this.lookupType == Constants.LT_QUERY)  && (XVar)(this.pageObject.pSetEdit.isLookupUnique((XVar)(this.field))));
			uniqueArray = XVar.Clone(XVar.Array());
			while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				if(XVar.Pack(isLookupUnique))
				{
					if(XVar.Pack(MVCFunctions.in_array((XVar)(data[this.linkFieldIndex]), (XVar)(uniqueArray))))
					{
						continue;
					}
					uniqueArray.InitAndSetArrayItem(data[this.linkFieldIndex], null);
				}
				decryptDataRow((XVar)(data));
				res = XVar.Clone(MVCFunctions.array_search((XVar)(data[this.linkFieldIndex]), (XVar)(avalue)));
				var_checked = new XVar("");
				if((XVar)(!XVar.Equals(XVar.Pack(res), XVar.Pack(null)))  && (XVar)(!XVar.Equals(XVar.Pack(res), XVar.Pack(false))))
				{
					found = new XVar(true);
					var_checked = XVar.Clone((XVar.Pack((XVar)(this.LCType == Constants.LCT_CBLIST)  || (XVar)(this.LCType == Constants.LCT_RADIO)) ? XVar.Pack(" checked=\"checked\"") : XVar.Pack(" selected")));
				}
				buildLookupRow((XVar)(mode), (XVar)(data), (XVar)(var_checked), (XVar)(i));
				i++;
			}
			if((XVar)((XVar)((XVar)(!(XVar)(found))  && (XVar)(MVCFunctions.strlen((XVar)(value))))  && (XVar)(mode == Constants.MODE_EDIT))  && (XVar)(this.pageObject.pSetEdit.isLookupWhereSet((XVar)(this.field))))
			{
				lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(value), new XVar(false), new XVar(true), new XVar(false), new XVar(true)));
				fillLookupFieldsIndexes();
				qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
				if(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
				{
					decryptDataRow((XVar)(data));
					var_checked = XVar.Clone((XVar.Pack((XVar)(this.LCType == Constants.LCT_CBLIST)  || (XVar)(this.LCType == Constants.LCT_RADIO)) ? XVar.Pack(" checked=\"checked\"") : XVar.Pack(" selected")));
					buildLookupRow((XVar)(mode), (XVar)(data), (XVar)(var_checked), (XVar)(i));
				}
			}
			footer = XVar.Clone((XVar.Pack(this.LCType == Constants.LCT_DROPDOWN) ? XVar.Pack("</select>") : XVar.Pack("</div>")));
			MVCFunctions.Echo(footer);
			MVCFunctions.Echo(getLookupLinks());
			return null;
		}
		public virtual XVar decryptDataRow(dynamic data)
		{
			if(XVar.Pack(this.isLinkFieldEncrypted))
			{
				data.InitAndSetArrayItem(this.ciphererLookup.DecryptField((XVar)(this.linkFieldName), (XVar)(data[this.linkFieldIndex])), this.linkFieldIndex);
			}
			if(XVar.Pack(this.isDisplayFieldEncrypted))
			{
				data.InitAndSetArrayItem(this.ciphererLookup.DecryptField((XVar)(this.displayFieldName), (XVar)(data[this.displayFieldIndex])), this.displayFieldIndex);
			}
			return null;
		}
		public virtual XVar buildLookupRow(dynamic _param_mode, dynamic _param_data, dynamic _param_checked, dynamic _param_i)
		{
			#region pass-by-value parameters
			dynamic mode = XVar.Clone(_param_mode);
			dynamic data = XVar.Clone(_param_data);
			dynamic var_checked = XVar.Clone(_param_checked);
			dynamic i = XVar.Clone(_param_i);
			#endregion

			switch(((XVar)this.LCType).ToInt())
			{
				case Constants.LCT_DROPDOWN:
				case Constants.LCT_LIST:
				case Constants.LCT_AJAX:
					MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.linkFieldIndex])), "\"", var_checked, ">", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.displayFieldIndex])), "</option>"));
					break;
				case Constants.LCT_CBLIST:
					MVCFunctions.Echo(MVCFunctions.Concat("<span class=\"checkbox\"><label>", "<input id=\"", this.cfield, "_", i, "\" class=\"rnr-checkbox\" type=\"checkbox\" ", this.alt, " name=\"", this.cfield, this.postfix, "\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.linkFieldIndex])), "\"", var_checked, "/>&nbsp;", "<b class=\"rnr-checkbox-label\" id=\"data_", this.cfield, "_", i, "\">", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.displayFieldIndex])), "</b>", "</label></span>"));
					break;
				case Constants.LCT_RADIO:
					MVCFunctions.Echo(MVCFunctions.Concat("<span class=\"radio\"><label>", "<input type=\"Radio\" class=\"rnr-radio-button\" id=\"radio_", this.cfieldname, "_", i, "\" ", this.alt, " name=\"radio_", this.cfieldname, "\" ", var_checked, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.linkFieldIndex])), "\">", " <span id=\"label_radio_", this.cfieldname, "_", i, "\" class=\"rnr-radio-label\">", MVCFunctions.runner_htmlspecialchars((XVar)(data[this.displayFieldIndex])), "</span>", "</label></span>"));
					break;
			}
			return null;
		}
		public override XVar getFirstElementId()
		{
			switch(((XVar)this.LCType).ToInt())
			{
				case Constants.LCT_AJAX:
					return MVCFunctions.Concat("display_value_", this.goodFieldName, "_", this.id);
					break;
				default:
					return this.cfield;
					break;
			}
			return null;
		}
		public virtual XVar isSearchOpitonForSimpleBox(dynamic _param_searchOption)
		{
			#region pass-by-value parameters
			dynamic searchOption = XVar.Clone(_param_searchOption);
			#endregion

			dynamic userSearchOptions = null;
			if((XVar)(searchOption == "Contains")  || (XVar)(searchOption == "Starts with"))
			{
				return true;
			}
			if(searchOption != XVar.Pack(""))
			{
				return false;
			}
			userSearchOptions = XVar.Clone(this.pageObject.pSetEdit.getSearchOptionsList((XVar)(this.field)));
			return (XVar)((XVar)(!(XVar)(MVCFunctions.count(userSearchOptions)))  || (XVar)(MVCFunctions.in_array(new XVar("Contains"), (XVar)(userSearchOptions))))  || (XVar)(MVCFunctions.in_array(new XVar("Starts with"), (XVar)(userSearchOptions)));
		}
		protected virtual XVar isAdditionalControlRequired()
		{
			dynamic hostPageType = null, userSearchOptions = null;
			if(XVar.Pack(this.multiselect))
			{
				return false;
			}
			hostPageType = XVar.Clone(this.pageObject.pSetEdit.getTableType());
			if((XVar)(hostPageType == "report")  || (XVar)(hostPageType == "chart"))
			{
				return false;
			}
			userSearchOptions = XVar.Clone(this.pageObject.pSetEdit.getSearchOptionsList((XVar)(this.field)));
			if((XVar)((XVar)(MVCFunctions.count(userSearchOptions))  && (XVar)(!(XVar)(MVCFunctions.in_array(new XVar("Contains"), (XVar)(userSearchOptions)))))  && (XVar)(!(XVar)(MVCFunctions.in_array(new XVar("Starts with"), (XVar)(userSearchOptions)))))
			{
				return false;
			}
			if((XVar)(this.lookupType == Constants.LT_LISTOFVALUES)  || (XVar)(this.linkAndDisplaySame))
			{
				return true;
			}
			if(this.connection.connId != this.lookupConnection.connId)
			{
				return false;
			}
			if((XVar)(!(XVar)(this.connection.checkIfJoinSubqueriesOptimized()))  && (XVar)(this.LCType == Constants.LCT_LIST))
			{
				return false;
			}
			return (XVar)(isLookupSQLquerySimple())  && (XVar)(isMainTableSQLquerySimple());
		}
		protected virtual XVar getWidthStyleForAdditionalControl()
		{
			dynamic style = null, width = null;
			width = XVar.Clone((XVar.Pack(this.searchPanelControl) ? XVar.Pack(150) : XVar.Pack(this.pageObject.pSetEdit.getControlWidth((XVar)(this.field)))));
			style = XVar.Clone(base.makeWidthStyle((XVar)(width)));
			return MVCFunctions.Concat("style=\"", style, "\"");
		}
		protected virtual XVar isLookupSQLquerySimple()
		{
			dynamic lookupSqlQuery = null;
			if((XVar)((XVar)(this.lookupConnection.dbType == Constants.nDATABASE_DB2)  || (XVar)(this.lookupConnection.dbType == Constants.nDATABASE_Informix))  || (XVar)(this.lookupConnection.dbType == Constants.nDATABASE_SQLite3))
			{
				return false;
			}
			if((XVar)(this.lookupType == Constants.LT_LOOKUPTABLE)  || (XVar)(this.lookupType == Constants.LT_LISTOFVALUES))
			{
				return true;
			}
			if(XVar.Pack(this.lookupPSet.hasEncryptedFields()))
			{
				return false;
			}
			lookupSqlQuery = XVar.Clone(this.lookupPSet.getSQLQuery());
			if((XVar)((XVar)(lookupSqlQuery.HasGroupBy())  || (XVar)(lookupSqlQuery.HavingToSql() != ""))  || (XVar)(lookupSqlQuery.HasSubQueryInFromClause()))
			{
				return false;
			}
			if(this.lookupConnection.dbType != Constants.nDATABASE_MySQL)
			{
				dynamic linkFieldType = null;
				linkFieldType = XVar.Clone(this.lookupPSet.getFieldType((XVar)(this.linkFieldName)));
				if(XVar.Pack(!(XVar)((XVar)((XVar)((XVar)(CommonFunctions.IsNumberType((XVar)(this.var_type)))  && (XVar)(CommonFunctions.IsNumberType((XVar)(linkFieldType))))  || (XVar)((XVar)(CommonFunctions.IsCharType((XVar)(this.var_type)))  && (XVar)(CommonFunctions.IsCharType((XVar)(linkFieldType)))))  || (XVar)((XVar)(CommonFunctions.IsDateFieldType((XVar)(this.var_type)))  && (XVar)(CommonFunctions.IsDateFieldType((XVar)(linkFieldType)))))))
				{
					return false;
				}
			}
			return true;
		}
		protected virtual XVar isMainTableSQLquerySimple()
		{
			dynamic sqlQuery = null;
			if((XVar)((XVar)((XVar)(this.connection.dbType != Constants.nDATABASE_MySQL)  && (XVar)(this.connection.dbType != Constants.nDATABASE_MSSQLServer))  && (XVar)(this.connection.dbType != Constants.nDATABASE_Oracle))  && (XVar)(this.connection.dbType != Constants.nDATABASE_PostgreSQL))
			{
				return false;
			}
			if(XVar.Pack(this.pageObject.pSetEdit.hasEncryptedFields()))
			{
				return false;
			}
			sqlQuery = XVar.Clone(this.pageObject.pSetEdit.getSQLQueryByField((XVar)(this.field)));
			if((XVar)((XVar)(sqlQuery.HasGroupBy())  || (XVar)(sqlQuery.HavingToSql() != ""))  || (XVar)(sqlQuery.HasSubQueryInFromClause()))
			{
				return false;
			}
			return true;
		}
		protected virtual XVar isSearchByDispalyedFieldAllowed()
		{
			dynamic hostPageType = null;
			if(XVar.Pack(!(XVar)(this.searchByDisplayedFieldIsAllowed == null)))
			{
				return this.searchByDisplayedFieldIsAllowed;
			}
			if(this.connection.connId != this.lookupConnection.connId)
			{
				this.searchByDisplayedFieldIsAllowed = new XVar(false);
				return this.searchByDisplayedFieldIsAllowed;
			}
			if((XVar)(!(XVar)(this.connection.checkIfJoinSubqueriesOptimized()))  && (XVar)((XVar)(this.LCType == Constants.LCT_LIST)  || (XVar)(this.LCType == Constants.LCT_AJAX)))
			{
				this.searchByDisplayedFieldIsAllowed = new XVar(false);
				return this.searchByDisplayedFieldIsAllowed;
			}
			hostPageType = XVar.Clone(this.pageObject.pSetEdit.getTableType());
			this.searchByDisplayedFieldIsAllowed = XVar.Clone((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(hostPageType != "report")  && (XVar)(hostPageType != "chart"))  && (XVar)(!(XVar)(this.linkAndDisplaySame)))  && (XVar)(!(XVar)(this.multiselect)))  && (XVar)((XVar)((XVar)(this.LCType == Constants.LCT_LIST)  || (XVar)(this.LCType == Constants.LCT_DROPDOWN))  || (XVar)(this.LCType == Constants.LCT_AJAX)))  && (XVar)(this.lookupType != Constants.LT_LISTOFVALUES))  && (XVar)(isLookupSQLquerySimple()))  && (XVar)(isMainTableSQLquerySimple()));
			return this.searchByDisplayedFieldIsAllowed;
		}
		public override XVar getSearchWhere(dynamic _param_searchFor, dynamic _param_strSearchOption, dynamic _param_searchFor2, dynamic _param_etype)
		{
			#region pass-by-value parameters
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic searchFor2 = XVar.Clone(_param_searchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			#endregion

			dynamic displayFieldSearchClause = null, searchIsCaseInsensitive = null;
			if((XVar)(!(XVar)(isSearchByDispalyedFieldAllowed()))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Starts with")))  && (XVar)(!XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Contains")))))
			{
				return SQLWhere((XVar)(searchFor), (XVar)(strSearchOption), (XVar)(searchFor2), (XVar)(etype), new XVar(false));
			}
			searchIsCaseInsensitive = XVar.Clone(this.pageObject.pSetEdit.getNCSearch());
			searchFor = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(searchFor)));
			searchFor = XVar.Clone(this.connection.prepareString((XVar)((XVar.Pack(strSearchOption == "Contains") ? XVar.Pack(MVCFunctions.Concat("%", searchFor, "%")) : XVar.Pack(MVCFunctions.Concat(searchFor, "%"))))));
			if(XVar.Pack(searchIsCaseInsensitive))
			{
				displayFieldSearchClause = XVar.Clone(MVCFunctions.Concat(this.connection.upper((XVar)(this.lwDisplayFieldWrapped)), " ", this.var_like, " ", this.connection.upper((XVar)(searchFor))));
			}
			else
			{
				displayFieldSearchClause = XVar.Clone(MVCFunctions.Concat(this.lwDisplayFieldWrapped, " ", this.var_like, " ", searchFor));
			}
			return MVCFunctions.Concat(getFieldSQLDecrypt(), " in (", getSearchSubquerySQL((XVar)(displayFieldSearchClause)), ")");
		}
		public override XVar checkIfDisplayFieldSearch(dynamic _param_strSearchOption)
		{
			#region pass-by-value parameters
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			#endregion

			return (XVar)(isSearchByDispalyedFieldAllowed())  && (XVar)((XVar)(XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Starts with")))  || (XVar)(XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Contains"))));
		}
		public override XVar getSuggestWhere(dynamic _param_strSearchOption, dynamic _param_searchFor, dynamic _param_isAggregateField = null)
		{
			#region default values
			if(_param_isAggregateField as Object == null) _param_isAggregateField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic isAggregateField = XVar.Clone(_param_isAggregateField);
			#endregion

			dynamic displayFieldName = null, likeCondition = null, searchForPrepared = null, searchIsCaseInsensitive = null;
			if((XVar)(!(XVar)(isSearchByDispalyedFieldAllowed()))  || (XVar)((XVar)(!XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Starts with")))  && (XVar)(!XVar.Equals(XVar.Pack(strSearchOption), XVar.Pack("Contains")))))
			{
				return SQLWhere((XVar)(searchFor), (XVar)(strSearchOption), new XVar(""), new XVar(""), new XVar(false));
			}
			initializeLookupTableAliases();
			searchIsCaseInsensitive = XVar.Clone(this.pageObject.pSetEdit.getNCSearch());
			searchFor = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(searchFor)));
			searchFor = XVar.Clone(this.connection.prepareString((XVar)((XVar.Pack(strSearchOption == "Contains") ? XVar.Pack(MVCFunctions.Concat("%", searchFor, "%")) : XVar.Pack(MVCFunctions.Concat(searchFor, "%"))))));
			searchForPrepared = XVar.Clone((XVar.Pack(searchIsCaseInsensitive) ? XVar.Pack(this.connection.upper((XVar)(searchFor))) : XVar.Pack(searchFor)));
			displayFieldName = XVar.Clone(MVCFunctions.Concat(this.lookupTableAliases[this.id], ".", this.displayFieldAliases[this.id]));
			if(XVar.Pack(searchIsCaseInsensitive))
			{
				likeCondition = XVar.Clone(MVCFunctions.Concat(this.connection.upper((XVar)(displayFieldName)), " ", this.var_like, " ", searchForPrepared));
			}
			else
			{
				likeCondition = XVar.Clone(MVCFunctions.Concat(displayFieldName, " ", this.var_like, " ", searchForPrepared));
			}
			return likeCondition;
		}
		public override XVar getSuggestHaving(dynamic _param_searchOpt, dynamic _param_searchFor, dynamic _param_isAggregateField = null)
		{
			#region default values
			if(_param_isAggregateField as Object == null) _param_isAggregateField = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic isAggregateField = XVar.Clone(_param_isAggregateField);
			#endregion

			if(XVar.Pack(!(XVar)(isSearchByDispalyedFieldAllowed())))
			{
				return base.getSuggestHaving((XVar)(searchOpt), (XVar)(searchFor), (XVar)(isAggregateField));
			}
			initializeLookupTableAliases();
			return (XVar.Pack(isAggregateField) ? XVar.Pack(MVCFunctions.Concat(this.lookupTableAliases[this.id], ".", this.linkFieldAliases[this.id], " is not NULL")) : XVar.Pack(""));
		}
		public override XVar getSelectColumnsAndJoinFromPart(dynamic _param_searchFor, dynamic _param_searchOpt, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			if((XVar)(!(XVar)(isSuggest))  || (XVar)(!(XVar)(isSearchByDispalyedFieldAllowed())))
			{
				return base.getSelectColumnsAndJoinFromPart((XVar)(searchFor), (XVar)(searchOpt), (XVar)(isSuggest));
			}
			initializeLookupTableAliases();
			return new XVar("selectColumns", getSelectColumns((XVar)(isSuggest)), "joinFromPart", getFromClauseJoinPart((XVar)(searchFor), (XVar)(searchOpt), (XVar)(isSuggest)));
		}
		protected virtual XVar initializeLookupTableAliases()
		{
			if(XVar.Pack(this.lookupTableAliases.KeyExists(this.id)))
			{
				return null;
			}
			this.lookupTableAliases.InitAndSetArrayItem(this.connection.addTableWrappers((XVar)(MVCFunctions.Concat("lt", CommonFunctions.generatePassword(new XVar(14))))), this.id);
			this.linkFieldAliases.InitAndSetArrayItem(this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat("lf", CommonFunctions.generatePassword(new XVar(14))))), this.id);
			this.displayFieldAliases.InitAndSetArrayItem(this.connection.addFieldWrappers((XVar)(MVCFunctions.Concat("df", CommonFunctions.generatePassword(new XVar(14))))), this.id);
			return null;
		}
		protected virtual XVar getFromClauseJoinPart(dynamic _param_searchFor, dynamic _param_searchOpt, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			dynamic displayFieldName = null, linkFieldName = null, lookUpFieldName = null, lookupFromExpression = null, subquery = null, subqueryColumns = null;
			if(XVar.Pack(!(XVar)(isSearchByDispalyedFieldAllowed())))
			{
				return "";
			}
			lookUpFieldName = XVar.Clone(RunnerPage._getFieldSQL((XVar)(this.field), (XVar)(this.connection), (XVar)(this.pageObject.pSetEdit)));
			if(this.lookupType != Constants.LT_LOOKUPTABLE)
			{
				linkFieldName = XVar.Clone(RunnerPage._getFieldSQL((XVar)(this.linkFieldName), (XVar)(this.connection), (XVar)(this.lookupPSet)));
				if(XVar.Pack(!(XVar)(this.customDisplay)))
				{
					displayFieldName = XVar.Clone(RunnerPage._getFieldSQL((XVar)(this.displayFieldName), (XVar)(this.connection), (XVar)(this.lookupPSet)));
				}
				else
				{
					displayFieldName = XVar.Clone(this.displayFieldName);
				}
				lookupFromExpression = XVar.Clone(this.lookupPSet.getSQLQuery().FromToSql());
			}
			else
			{
				linkFieldName = XVar.Clone(this.lwLinkField);
				displayFieldName = XVar.Clone(this.lwDisplayFieldWrapped);
				lookupFromExpression = XVar.Clone(MVCFunctions.Concat(" from ", this.connection.addTableWrappers((XVar)(this.lookupTable))));
			}
			subqueryColumns = XVar.Clone(MVCFunctions.Concat(linkFieldName, " as ", this.linkFieldAliases[this.id], ", ", displayFieldName, " as", this.displayFieldAliases[this.id]));
			subquery = XVar.Clone(MVCFunctions.Concat("select ", subqueryColumns, lookupFromExpression));
			return MVCFunctions.Concat(" left join (", subquery, ") ", this.lookupTableAliases[this.id], " on ", this.lookupTableAliases[this.id], ".", this.linkFieldAliases[this.id], "=", lookUpFieldName);
		}
		protected virtual XVar getSelectColumns(dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			if(XVar.Pack(isSuggest))
			{
				return MVCFunctions.Concat(this.lookupTableAliases[this.id], ".", this.displayFieldAliases[this.id]);
			}
			return "";
		}
		public override XVar SQLWhere(dynamic _param_SearchFor, dynamic _param_strSearchOption, dynamic _param_SearchFor2, dynamic _param_etype, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic SearchFor = XVar.Clone(_param_SearchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic SearchFor2 = XVar.Clone(_param_SearchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			dynamic baseResult = null, gstrField = null, ret = null;
			if(this.lookupType == Constants.LT_LISTOFVALUES)
			{
				return base.SQLWhere((XVar)(SearchFor), (XVar)(strSearchOption), (XVar)(SearchFor2), (XVar)(etype), (XVar)(isSuggest));
			}
			baseResult = XVar.Clone(baseSQLWhere((XVar)(strSearchOption)));
			if(XVar.Equals(XVar.Pack(baseResult), XVar.Pack(false)))
			{
				return "";
			}
			if(!XVar.Equals(XVar.Pack(baseResult), XVar.Pack("")))
			{
				return baseResult;
			}
			if(this.connection.dbType != Constants.nDATABASE_MySQL)
			{
				this.btexttype = XVar.Clone(CommonFunctions.IsTextType((XVar)(this.var_type)));
			}
			if((XVar)(this.multiselect)  && (XVar)(strSearchOption != "Equals"))
			{
				SearchFor = XVar.Clone(CommonFunctions.splitvalues((XVar)(SearchFor)));
			}
			else
			{
				SearchFor = XVar.Clone(new XVar(0, SearchFor));
			}
			gstrField = XVar.Clone(getFieldSQLDecrypt());
			if((XVar)((XVar)(strSearchOption == "Starts with")  || (XVar)(strSearchOption == "Contains"))  && (XVar)((XVar)(!(XVar)(CommonFunctions.IsCharType((XVar)(this.var_type))))  || (XVar)(this.btexttype)))
			{
				gstrField = XVar.Clone(this.connection.field2char((XVar)(gstrField), (XVar)(this.var_type)));
			}
			ret = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> searchItem in SearchFor.GetEnumerator())
			{
				dynamic condition = null, searchIsCaseInsensitive = null, value = null;
				value = XVar.Clone(searchItem.Value);
				if((XVar)((XVar)(value == "null")  || (XVar)(value == "Null"))  || (XVar)(value == XVar.Pack("")))
				{
					continue;
				}
				if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(ret))))))
				{
					ret = MVCFunctions.Concat(ret, " or ");
				}
				if((XVar)((XVar)(strSearchOption == "Starts with")  || (XVar)(strSearchOption == "Contains"))  && (XVar)(!(XVar)(this.multiselect)))
				{
					value = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(value)));
					if(strSearchOption == "Starts with")
					{
						value = MVCFunctions.Concat(value, "%");
					}
					if(strSearchOption == "Contains")
					{
						value = XVar.Clone(MVCFunctions.Concat("%", value, "%"));
					}
				}
				if((XVar)(strSearchOption != "Starts with")  && (XVar)(strSearchOption != "Contains"))
				{
					value = XVar.Clone(CommonFunctions.make_db_value((XVar)(this.field), (XVar)(value)));
				}
				searchIsCaseInsensitive = XVar.Clone(this.pageObject.pSetEdit.getNCSearch());
				if((XVar)(strSearchOption == "Equals")  && (XVar)(!(XVar)((XVar)(value == "null")  || (XVar)(value == "Null"))))
				{
					condition = XVar.Clone(MVCFunctions.Concat(gstrField, "=", value));
				}
				else
				{
					if((XVar)((XVar)(strSearchOption == "Starts with")  || (XVar)(strSearchOption == "Contains"))  && (XVar)(!(XVar)(this.multiselect)))
					{
						condition = XVar.Clone(MVCFunctions.Concat(gstrField, " ", this.var_like, " ", this.connection.prepareString((XVar)(value))));
					}
					else
					{
						if(strSearchOption == "More than")
						{
							condition = XVar.Clone(MVCFunctions.Concat(gstrField, " > ", value));
						}
						else
						{
							if(strSearchOption == "Less than")
							{
								condition = XVar.Clone(MVCFunctions.Concat(gstrField, "<", value));
							}
							else
							{
								dynamic value1 = null;
								if(strSearchOption == "Equal or more than")
								{
									condition = XVar.Clone(MVCFunctions.Concat(gstrField, ">=", value1));
								}
								else
								{
									if(strSearchOption == "Equal or less than")
									{
										condition = XVar.Clone(MVCFunctions.Concat(gstrField, "<=", value1));
									}
									else
									{
										if(strSearchOption == "Between")
										{
											dynamic value2 = null;
											value2 = XVar.Clone(this.connection.prepareString((XVar)(SearchFor2)));
											if((XVar)((XVar)((XVar)(this.lookupType == Constants.LT_QUERY)  && (XVar)(CommonFunctions.IsCharType((XVar)(this.var_type))))  && (XVar)(!(XVar)(this.btexttype)))  && (XVar)(searchIsCaseInsensitive))
											{
												value2 = XVar.Clone(this.connection.upper((XVar)(value2)));
											}
											condition = XVar.Clone(MVCFunctions.Concat(gstrField, ">=", value, " and "));
											if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(this.var_type))))
											{
												dynamic timeArr = XVar.Array();
												timeArr = XVar.Clone(CommonFunctions.db2time((XVar)(SearchFor2)));
												if((XVar)((XVar)(timeArr[3] == 0)  && (XVar)(timeArr[4] == 0))  && (XVar)(timeArr[5] == 0))
												{
													timeArr = XVar.Clone(CommonFunctions.adddays((XVar)(timeArr), new XVar(1)));
													SearchFor2 = XVar.Clone(MVCFunctions.Concat(timeArr[0], "-", timeArr[1], "-", timeArr[2]));
													SearchFor2 = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(this.field), (XVar)(SearchFor2), (XVar)(this.tName)));
													condition = MVCFunctions.Concat(condition, gstrField, "<", SearchFor2);
												}
												else
												{
													condition = MVCFunctions.Concat(condition, gstrField, "<=", value2);
												}
											}
											else
											{
												condition = MVCFunctions.Concat(condition, gstrField, "<=", value2);
											}
										}
										else
										{
											if(XVar.Pack(this.multiselect))
											{
												dynamic fullFieldName = null;
												if((XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(value), new XVar(","))), XVar.Pack(false)))  || (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(value), new XVar("\""))), XVar.Pack(false))))
												{
													value = XVar.Clone(MVCFunctions.Concat("\"", MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(value)), "\""));
												}
												fullFieldName = XVar.Clone(gstrField);
												value = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(value)));
												ret = MVCFunctions.Concat(ret, fullFieldName, " = ", this.connection.prepareString((XVar)(value)));
												ret = MVCFunctions.Concat(ret, " or ", fullFieldName, " ", this.var_like, " ", this.connection.prepareString((XVar)(MVCFunctions.Concat("%,", value, ",%"))));
												ret = MVCFunctions.Concat(ret, " or ", fullFieldName, " ", this.var_like, " ", this.connection.prepareString((XVar)(MVCFunctions.Concat("%,", value))));
												ret = MVCFunctions.Concat(ret, " or ", fullFieldName, " ", this.var_like, " ", this.connection.prepareString((XVar)(MVCFunctions.Concat(value, ",%"))));
											}
										}
									}
								}
							}
						}
					}
				}
				if((XVar)(condition != XVar.Pack(""))  && (XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(isSuggest)  || (XVar)(strSearchOption == "Contains"))  || (XVar)(strSearchOption == "Equals"))  || (XVar)(strSearchOption == "Starts with"))  || (XVar)(strSearchOption == "More than"))  || (XVar)(strSearchOption == "Less than"))  || (XVar)(strSearchOption == "Equal or more than"))  || (XVar)(strSearchOption == "Equal or less than"))  || (XVar)(strSearchOption == "Between")))
				{
					if((XVar)(this.linkAndDisplaySame)  || (XVar)((XVar)(strSearchOption != "Contains")  && (XVar)(strSearchOption != "Starts with")))
					{
						ret = MVCFunctions.Concat(ret, " ", condition);
					}
					else
					{
						return "";
					}
				}
			}
			ret = XVar.Clone(MVCFunctions.trim((XVar)(ret)));
			if(XVar.Pack(MVCFunctions.strlen((XVar)(ret))))
			{
				ret = XVar.Clone(MVCFunctions.Concat("(", ret, ")"));
			}
			return ret;
		}
		public override XVar getSearchOptions(dynamic _param_selOpt, dynamic _param_not, dynamic _param_both)
		{
			#region pass-by-value parameters
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			dynamic optionsArray = XVar.Array();
			optionsArray = XVar.Clone(XVar.Array());
			if(XVar.Pack(this.multiselect))
			{
				optionsArray.InitAndSetArrayItem(Constants.CONTAINS, null);
			}
			else
			{
				if(this.lookupType == Constants.LT_QUERY)
				{
					this.ciphererLookup = XVar.Clone(new RunnerCipherer((XVar)(this.lookupTable)));
				}
				if(XVar.Pack(this.ciphererLookup))
				{
					this.isDisplayFieldEncrypted = XVar.Clone(this.ciphererLookup.isFieldPHPEncrypted((XVar)(this.displayFieldName)));
				}
				if((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(!(XVar)(this.isDisplayFieldEncrypted)))
				{
					if((XVar)(isSearchByDispalyedFieldAllowed())  || (XVar)(this.linkAndDisplaySame))
					{
						optionsArray.InitAndSetArrayItem(Constants.CONTAINS, null);
						optionsArray.InitAndSetArrayItem(Constants.STARTS_WITH, null);
					}
					optionsArray.InitAndSetArrayItem(Constants.MORE_THAN, null);
					optionsArray.InitAndSetArrayItem(Constants.LESS_THAN, null);
					optionsArray.InitAndSetArrayItem(Constants.BETWEEN, null);
				}
				if((XVar)((XVar)(this.LCType == Constants.LCT_LIST)  || (XVar)(this.LCType == Constants.LCT_DROPDOWN))  && (XVar)(isAdditionalControlRequired()))
				{
					optionsArray.InitAndSetArrayItem(Constants.CONTAINS, null);
					optionsArray.InitAndSetArrayItem(Constants.STARTS_WITH, null);
				}
			}
			optionsArray.InitAndSetArrayItem(Constants.EQUALS, null);
			optionsArray.InitAndSetArrayItem(Constants.EMPTY_SEARCH, null);
			if(XVar.Pack(both))
			{
				if(XVar.Pack(this.multiselect))
				{
					optionsArray.InitAndSetArrayItem(Constants.NOT_CONTAINS, null);
				}
				else
				{
					if((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(!(XVar)(this.isDisplayFieldEncrypted)))
					{
						if((XVar)(isSearchByDispalyedFieldAllowed())  || (XVar)(this.linkAndDisplaySame))
						{
							optionsArray.InitAndSetArrayItem(Constants.NOT_CONTAINS, null);
							optionsArray.InitAndSetArrayItem(Constants.NOT_STARTS_WITH, null);
						}
						optionsArray.InitAndSetArrayItem(Constants.NOT_MORE_THAN, null);
						optionsArray.InitAndSetArrayItem(Constants.NOT_LESS_THAN, null);
						optionsArray.InitAndSetArrayItem(Constants.NOT_BETWEEN, null);
					}
					if((XVar)((XVar)(this.LCType == Constants.LCT_LIST)  || (XVar)(this.LCType == Constants.LCT_DROPDOWN))  && (XVar)(isAdditionalControlRequired()))
					{
						optionsArray.InitAndSetArrayItem(Constants.NOT_CONTAINS, null);
						optionsArray.InitAndSetArrayItem(Constants.NOT_STARTS_WITH, null);
					}
				}
				optionsArray.InitAndSetArrayItem(Constants.NOT_EQUALS, null);
				optionsArray.InitAndSetArrayItem(Constants.NOT_EMPTY, null);
			}
			return buildSearchOptions((XVar)(optionsArray), (XVar)(selOpt), (XVar)(var_not), (XVar)(both));
		}
		public override XVar suggestValue(dynamic _param_value, dynamic _param_searchFor, dynamic var_response, dynamic row)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			#endregion

			dynamic data = XVar.Array(), lookupSQL = null, qResult = null;
			if((XVar)((XVar)(!(XVar)(CommonFunctions.GetGlobalData(new XVar("handleSearchSuggestInLookup"), new XVar(true))))  || (XVar)(this.lookupType == Constants.LT_LISTOFVALUES))  || (XVar)(isSearchByDispalyedFieldAllowed()))
			{
				base.suggestValue((XVar)(value), (XVar)(searchFor), (XVar)(var_response), (XVar)(row));
				return null;
			}
			lookupSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(MVCFunctions.substr((XVar)(value), new XVar(1))), new XVar(false), new XVar(true), new XVar(true), new XVar(true)));
			fillLookupFieldsIndexes();
			qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
			if(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				dynamic lookup_value = null;
				if(XVar.Pack(this.isDisplayFieldEncrypted))
				{
					lookup_value = XVar.Clone(MVCFunctions.Concat("_", this.ciphererLookup.DecryptField((XVar)(this.displayFieldName), (XVar)(data[this.displayFieldIndex]))));
				}
				else
				{
					lookup_value = XVar.Clone(MVCFunctions.Concat("_", data[this.displayFieldIndex]));
				}
				base.suggestValue((XVar)(lookup_value), (XVar)(searchFor), (XVar)(var_response), (XVar)(row));
			}
			return null;
		}
		protected virtual XVar getSearchSubquerySQL(dynamic _param_dispFieldSearchClause)
		{
			#region pass-by-value parameters
			dynamic dispFieldSearchClause = XVar.Clone(_param_dispFieldSearchClause);
			#endregion

			ProjectSettings pSet;
			if((XVar)(this.lookupType != Constants.LT_LOOKUPTABLE)  && (XVar)(this.lookupType != Constants.LT_QUERY))
			{
				return "";
			}
			pSet = XVar.UnPackProjectSettings(this.pageObject.pSetEdit);
			if(this.lookupType == Constants.LT_QUERY)
			{
				dynamic lookupQueryObj = null;
				fillLookupFieldsIndexes();
				lookupQueryObj = XVar.Clone(this.lookupPSet.getSQLQuery().CloneObject());
				lookupQueryObj.deleteAllFieldsExcept((XVar)(this.linkFieldIndex));
				return lookupQueryObj.buildSQL_default((XVar)(dispFieldSearchClause));
			}
			return MVCFunctions.Concat("SELECT ", this.lwLinkField, " FROM ", this.lookupConnection.addTableWrappers((XVar)(this.lookupTable)), " WHERE ", dispFieldSearchClause);
		}
		protected virtual XVar getSQLWhereParts(dynamic _param_doValueFilter, dynamic _param_childVal, dynamic _param_doWhereFilter, dynamic _param_doCategoryFilter, dynamic _param_parentValuesData, dynamic _param_readyCategoryWhere, dynamic _param_oneRecordMode)
		{
			#region pass-by-value parameters
			dynamic doValueFilter = XVar.Clone(_param_doValueFilter);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doWhereFilter = XVar.Clone(_param_doWhereFilter);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic readyCategoryWhere = XVar.Clone(_param_readyCategoryWhere);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			#endregion

			dynamic mandatoryWhere = XVar.Array();
			mandatoryWhere = XVar.Clone(XVar.Array());
			mandatoryWhere.InitAndSetArrayItem((XVar.Pack(doValueFilter) ? XVar.Pack(getChildWhere((XVar)(childVal))) : XVar.Pack("")), null);
			if(XVar.Pack(doWhereFilter))
			{
				mandatoryWhere.InitAndSetArrayItem(CommonFunctions.prepareLookupWhere((XVar)(this.field), (XVar)(this.pageObject.pSetEdit)), null);
				if(this.lookupType == Constants.LT_QUERY)
				{
					if(this.lookupPSet.getAdvancedSecurityType() == Constants.ADVSECURITY_VIEW_OWN)
					{
						mandatoryWhere.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.lookupTable)), null);
					}
				}
			}
			if(XVar.Pack(doCategoryFilter))
			{
				if(XVar.Pack(readyCategoryWhere))
				{
					mandatoryWhere.InitAndSetArrayItem(readyCategoryWhere, null);
				}
				else
				{
					if(0 < MVCFunctions.count(parentValuesData))
					{
						mandatoryWhere.InitAndSetArrayItem(getCategoryWhere((XVar)(parentValuesData)), null);
					}
				}
			}
			if((XVar)(oneRecordMode)  && (XVar)(this.lookupConnection.dbType == Constants.nDATABASE_Oracle))
			{
				mandatoryWhere.InitAndSetArrayItem("rownum < 2", null);
			}
			return mandatoryWhere;
		}
		protected virtual XVar getProjectTableLookupSQL(dynamic _param_parentValuesData, dynamic _param_childVal, dynamic _param_doCategoryFilter, dynamic _param_doValueFilter, dynamic _param_doWhereFilter, dynamic _param_oneRecordMode, dynamic _param_readyCategoryWhere)
		{
			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic doValueFilter = XVar.Clone(_param_doValueFilter);
			dynamic doWhereFilter = XVar.Clone(_param_doWhereFilter);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			dynamic readyCategoryWhere = XVar.Clone(_param_readyCategoryWhere);
			#endregion

			dynamic lookupQueryObj = null, mandatoryWhere = null, orderByClause = null, sqlParts = null, strOrderBy = null;
			if(this.lookupType != Constants.LT_QUERY)
			{
				return "";
			}
			strOrderBy = XVar.Clone(this.pageObject.pSetEdit.getLookupOrderBy((XVar)(this.field)));
			orderByClause = new XVar("");
			if(XVar.Pack(MVCFunctions.strlen((XVar)(strOrderBy))))
			{
				strOrderBy = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)(strOrderBy), (XVar)(this.lookupConnection), (XVar)(this.lookupPSet), (XVar)(this.ciphererLookup)));
				if(XVar.Pack(this.pageObject.pSetEdit.isLookupDesc((XVar)(this.field))))
				{
					strOrderBy = MVCFunctions.Concat(strOrderBy, " DESC");
				}
				orderByClause = XVar.Clone(MVCFunctions.Concat(" ORDER BY ", strOrderBy));
			}
			lookupQueryObj = XVar.Clone(this.lookupPSet.getSQLQuery());
			lookupQueryObj.ReplaceFieldsWithDummies((XVar)(this.lookupPSet.getBinaryFieldsIndices()));
			if(XVar.Pack(this.customDisplay))
			{
				lookupQueryObj.AddCustomExpression((XVar)(this.displayFieldName), (XVar)(this.lookupPSet), (XVar)(this.tName), (XVar)(this.field));
			}
			sqlParts = XVar.Clone(lookupQueryObj.getSqlComponents((XVar)(oneRecordMode)));
			mandatoryWhere = XVar.Clone(getSQLWhereParts((XVar)(doValueFilter), (XVar)(childVal), (XVar)(doWhereFilter), (XVar)(doCategoryFilter), (XVar)(parentValuesData), (XVar)(readyCategoryWhere), (XVar)(oneRecordMode)));
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sqlParts), (XVar)(mandatoryWhere)));
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, " ", MVCFunctions.trim((XVar)(orderByClause)));
			if(XVar.Pack(oneRecordMode))
			{
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, getOneRecordModeTailPart());
			}
			return GlobalVars.strSQL;
		}
		protected virtual XVar getNotProjectTableLookupSQL(dynamic _param_parentValuesData, dynamic _param_childVal, dynamic _param_doCategoryFilter, dynamic _param_doValueFilter, dynamic _param_doWhereFilter, dynamic _param_oneRecordMode, dynamic _param_readyCategoryWhere)
		{
			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic doValueFilter = XVar.Clone(_param_doValueFilter);
			dynamic doWhereFilter = XVar.Clone(_param_doWhereFilter);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			dynamic readyCategoryWhere = XVar.Clone(_param_readyCategoryWhere);
			#endregion

			dynamic LookupSQL = null, bUnique = null, mandatoryWhere = null, strOrderBy = null, strUniqueOrderBy = null, strWhere = null;
			ProjectSettings pSet;
			if(this.lookupType != Constants.LT_LOOKUPTABLE)
			{
				return "";
			}
			pSet = XVar.UnPackProjectSettings(this.pageObject.pSetEdit);
			strOrderBy = XVar.Clone(pSet.getLookupOrderBy((XVar)(this.field)));
			if(this.lookupConnection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				strUniqueOrderBy = XVar.Clone(strOrderBy);
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(strOrderBy))))
			{
				strOrderBy = XVar.Clone(this.lookupConnection.addFieldWrappers((XVar)(strOrderBy)));
				if(XVar.Pack(pSet.isLookupDesc((XVar)(this.field))))
				{
					strOrderBy = MVCFunctions.Concat(strOrderBy, " DESC");
				}
			}
			mandatoryWhere = XVar.Clone(getSQLWhereParts((XVar)(doValueFilter), (XVar)(childVal), (XVar)(doWhereFilter), (XVar)(doCategoryFilter), (XVar)(parentValuesData), (XVar)(readyCategoryWhere), (XVar)(oneRecordMode)));
			strWhere = XVar.Clone(CommonFunctions.combineSQLCriteria((XVar)(mandatoryWhere)));
			LookupSQL = new XVar("SELECT ");
			if((XVar)(this.lookupConnection.dbType == Constants.nDATABASE_MSSQLServer)  || (XVar)(this.lookupConnection.dbType == Constants.nDATABASE_Access))
			{
				if(XVar.Pack(oneRecordMode))
				{
					LookupSQL = MVCFunctions.Concat(LookupSQL, "top 1 ");
				}
			}
			bUnique = XVar.Clone(pSet.isLookupUnique((XVar)(this.field)));
			if((XVar)(bUnique)  && (XVar)(!(XVar)(oneRecordMode)))
			{
				LookupSQL = MVCFunctions.Concat(LookupSQL, "DISTINCT ");
			}
			LookupSQL = MVCFunctions.Concat(LookupSQL, this.lwLinkField);
			if(XVar.Pack(!(XVar)(this.linkAndDisplaySame)))
			{
				LookupSQL = MVCFunctions.Concat(LookupSQL, ",", RunnerPage.sqlFormattedDisplayField((XVar)(this.field), (XVar)(this.lookupConnection), (XVar)(pSet)));
			}
			if(this.lookupConnection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				if((XVar)((XVar)(strUniqueOrderBy)  && (XVar)(bUnique))  && (XVar)(!(XVar)(oneRecordMode)))
				{
					LookupSQL = MVCFunctions.Concat(LookupSQL, ",", this.lookupConnection.addFieldWrappers((XVar)(strUniqueOrderBy)));
				}
			}
			LookupSQL = MVCFunctions.Concat(LookupSQL, " FROM ", this.lookupConnection.addTableWrappers((XVar)(this.lookupTable)));
			if(XVar.Pack(MVCFunctions.strlen((XVar)(strWhere))))
			{
				LookupSQL = MVCFunctions.Concat(LookupSQL, " WHERE ", strWhere);
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(strOrderBy))))
			{
				LookupSQL = MVCFunctions.Concat(LookupSQL, " ORDER BY ", this.lookupConnection.addTableWrappers((XVar)(this.lookupTable)), ".", strOrderBy);
			}
			if(XVar.Pack(oneRecordMode))
			{
				LookupSQL = MVCFunctions.Concat(LookupSQL, getOneRecordModeTailPart());
			}
			return LookupSQL;
		}
		protected virtual XVar getOneRecordModeTailPart()
		{
			if(this.lookupConnection.dbType == Constants.nDATABASE_MySQL)
			{
				return " limit 0,1";
			}
			if(this.lookupConnection.dbType == Constants.nDATABASE_PostgreSQL)
			{
				return " limit 1";
			}
			if(this.lookupConnection.dbType == Constants.nDATABASE_DB2)
			{
				return " fetch first 1 rows only";
			}
			return "";
		}
		public virtual XVar getLookupSQL(dynamic _param_parentValuesData, dynamic _param_childVal = null, dynamic _param_doCategoryFilter = null, dynamic _param_doValueFilter = null, dynamic _param_doWhereFilter = null, dynamic _param_oneRecordMode = null, dynamic _param_readyCategoryWhere = null)
		{
			#region default values
			if(_param_childVal as Object == null) _param_childVal = new XVar("");
			if(_param_doCategoryFilter as Object == null) _param_doCategoryFilter = new XVar(true);
			if(_param_doValueFilter as Object == null) _param_doValueFilter = new XVar(false);
			if(_param_doWhereFilter as Object == null) _param_doWhereFilter = new XVar(true);
			if(_param_oneRecordMode as Object == null) _param_oneRecordMode = new XVar(false);
			if(_param_readyCategoryWhere as Object == null) _param_readyCategoryWhere = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic doValueFilter = XVar.Clone(_param_doValueFilter);
			dynamic doWhereFilter = XVar.Clone(_param_doWhereFilter);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			dynamic readyCategoryWhere = XVar.Clone(_param_readyCategoryWhere);
			#endregion

			if(this.lookupType == Constants.LT_QUERY)
			{
				return getProjectTableLookupSQL((XVar)(parentValuesData), (XVar)(childVal), (XVar)(doCategoryFilter), (XVar)(doValueFilter), (XVar)(doWhereFilter), (XVar)(oneRecordMode), (XVar)(readyCategoryWhere));
			}
			if(this.lookupType == Constants.LT_LOOKUPTABLE)
			{
				return getNotProjectTableLookupSQL((XVar)(parentValuesData), (XVar)(childVal), (XVar)(doCategoryFilter), (XVar)(doValueFilter), (XVar)(doWhereFilter), (XVar)(oneRecordMode), (XVar)(readyCategoryWhere));
			}
			return "";
		}
		public virtual XVar getCategoryCondition(dynamic _param_mainControlName, dynamic _param_filterFieldName, dynamic _param_mainControlVal)
		{
			#region pass-by-value parameters
			dynamic mainControlName = XVar.Clone(_param_mainControlName);
			dynamic filterFieldName = XVar.Clone(_param_filterFieldName);
			dynamic mainControlVal = XVar.Clone(_param_mainControlVal);
			#endregion

			dynamic categoryWhere = XVar.Array(), parentVals = XVar.Array(), parentValsPlain = XVar.Array();
			parentValsPlain = XVar.Clone((XVar.Pack(this.pageObject.pSetEdit.multiSelect((XVar)(mainControlName))) ? XVar.Pack(CommonFunctions.splitvalues((XVar)(mainControlVal))) : XVar.Pack(new XVar(0, mainControlVal))));
			parentVals = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> arElement in parentValsPlain.GetEnumerator())
			{
				if(this.lookupType == Constants.LT_QUERY)
				{
					parentVals.InitAndSetArrayItem(this.ciphererLookup.MakeDBValue((XVar)(filterFieldName), (XVar)(arElement.Value), new XVar(""), new XVar(true)), arElement.Key);
				}
				else
				{
					parentVals.InitAndSetArrayItem(CommonFunctions.make_db_value((XVar)(mainControlName), (XVar)(arElement.Value), new XVar(""), new XVar(""), (XVar)(this.tName)), arElement.Key);
				}
			}
			categoryWhere = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> arValue in parentVals.GetEnumerator())
			{
				dynamic condition = null;
				condition = XVar.Clone((XVar.Pack(XVar.Equals(XVar.Pack(arValue.Value), XVar.Pack("null"))) ? XVar.Pack(" is null") : XVar.Pack(MVCFunctions.Concat("=", arValue.Value))));
				if(this.lookupType == Constants.LT_QUERY)
				{
					categoryWhere.InitAndSetArrayItem(MVCFunctions.Concat(this.ciphererLookup.GetFieldName((XVar)(RunnerPage._getFieldSQL((XVar)(filterFieldName), (XVar)(this.lookupConnection), (XVar)(this.lookupPSet))), (XVar)(filterFieldName)), condition), null);
				}
				else
				{
					categoryWhere.InitAndSetArrayItem(MVCFunctions.Concat(this.lookupConnection.addFieldWrappers((XVar)(filterFieldName)), condition), null);
				}
			}
			return (XVar.Pack(MVCFunctions.count(categoryWhere) == 1) ? XVar.Pack(categoryWhere[0]) : XVar.Pack(MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" OR "), (XVar)(categoryWhere)), ")")));
		}
		protected virtual XVar getCategoryWhere(dynamic _param_parentValueData)
		{
			#region pass-by-value parameters
			dynamic parentValueData = XVar.Clone(_param_parentValueData);
			#endregion

			dynamic whereParts = XVar.Array();
			if(XVar.Pack(!(XVar)(this.bUseCategory)))
			{
				return "";
			}
			whereParts = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> cdata in this.pageObject.pSetEdit.getParentFieldsData((XVar)(this.field)).GetEnumerator())
			{
				dynamic filterFieldName = null, mainControlName = null, mainControlVal = null;
				mainControlName = XVar.Clone(cdata.Value["main"]);
				filterFieldName = XVar.Clone(cdata.Value["lookup"]);
				mainControlVal = XVar.Clone(parentValueData[mainControlName]);
				if((XVar)(this.pageObject.pSetEdit.multiSelect((XVar)(mainControlName)))  || (XVar)(MVCFunctions.strlen((XVar)(mainControlVal))))
				{
					whereParts.InitAndSetArrayItem(getCategoryCondition((XVar)(mainControlName), (XVar)(filterFieldName), (XVar)(mainControlVal)), null);
				}
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(whereParts))))
			{
				return "";
			}
			return MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" AND "), (XVar)(whereParts)), ")");
		}
		protected virtual XVar getChildWhere(dynamic _param_childVal)
		{
			#region pass-by-value parameters
			dynamic childVal = XVar.Clone(_param_childVal);
			#endregion

			dynamic childValues = XVar.Array(), childWheres = XVar.Array(), fullLinkFieldName = null;
			if(this.lookupType == Constants.LT_QUERY)
			{
				fullLinkFieldName = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)(this.linkFieldName), (XVar)(this.lookupConnection), (XVar)(this.lookupPSet), (XVar)(this.ciphererLookup)));
			}
			else
			{
				fullLinkFieldName = XVar.Clone(this.lwLinkField);
			}
			childValues = XVar.Clone((XVar.Pack(this.multiselect) ? XVar.Pack(CommonFunctions.splitvalues((XVar)(childVal))) : XVar.Pack(new XVar(0, childVal))));
			childWheres = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> childValue in childValues.GetEnumerator())
			{
				dynamic dbValue = null;
				if(this.lookupType == Constants.LT_QUERY)
				{
					dbValue = XVar.Clone(this.ciphererLookup.MakeDBValue((XVar)(this.linkFieldName), (XVar)(childValue.Value), new XVar(""), new XVar(true)));
				}
				else
				{
					dbValue = XVar.Clone(CommonFunctions.make_db_value((XVar)(this.field), (XVar)(childValue.Value), new XVar(""), new XVar(""), (XVar)(this.tName)));
				}
				childWheres.InitAndSetArrayItem(MVCFunctions.Concat(fullLinkFieldName, (XVar.Pack(XVar.Equals(XVar.Pack(dbValue), XVar.Pack("null"))) ? XVar.Pack(" is null") : XVar.Pack(MVCFunctions.Concat("=", dbValue)))), null);
			}
			return MVCFunctions.implode(new XVar(" OR "), (XVar)(childWheres));
		}
		protected virtual XVar needCategoryFiltering(dynamic _param_parentValuesData)
		{
			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> cData in this.pageObject.pSetEdit.getParentFieldsData((XVar)(this.field)).GetEnumerator())
			{
				dynamic parentVals = XVar.Array(), parentValue = null, strCategoryControl = null;
				strCategoryControl = XVar.Clone(cData.Value["main"]);
				if(XVar.Pack(!(XVar)(parentValuesData.KeyExists(cData.Value["main"]))))
				{
					continue;
				}
				parentValue = XVar.Clone(parentValuesData[cData.Value["main"]]);
				parentVals = XVar.Clone((XVar.Pack(this.pageObject.pSetEdit.multiSelect((XVar)(strCategoryControl))) ? XVar.Pack(CommonFunctions.splitvalues((XVar)(parentValue))) : XVar.Pack(new XVar(0, parentValue))));
				foreach (KeyValuePair<XVar, dynamic> parentVal in parentVals.GetEnumerator())
				{
					dynamic tempParentVal = null;
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(parentVal.Value)))))
					{
						continue;
					}
					if(this.lookupType == Constants.LT_QUERY)
					{
						tempParentVal = XVar.Clone(this.ciphererLookup.MakeDBValue((XVar)(cData.Value["main"]), (XVar)(parentVal.Value), new XVar(""), new XVar(true)));
					}
					else
					{
						tempParentVal = XVar.Clone(CommonFunctions.make_db_value((XVar)(this.field), (XVar)(parentVal.Value)));
					}
					if(!XVar.Equals(XVar.Pack(tempParentVal), XVar.Pack("null")))
					{
						return true;
					}
				}
			}
			return false;
		}
		public override XVar loadLookupContent(dynamic _param_parentValuesData, dynamic _param_childVal = null, dynamic _param_doCategoryFilter = null, dynamic _param_initialLoad = null)
		{
			#region default values
			if(_param_childVal as Object == null) _param_childVal = new XVar("");
			if(_param_doCategoryFilter as Object == null) _param_doCategoryFilter = new XVar(true);
			if(_param_initialLoad as Object == null) _param_initialLoad = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic initialLoad = XVar.Clone(_param_initialLoad);
			#endregion

			dynamic lookupSQL = null;
			if((XVar)(this.bUseCategory)  && (XVar)(doCategoryFilter))
			{
				if(XVar.Pack(!(XVar)(needCategoryFiltering((XVar)(parentValuesData)))))
				{
					return XVar.Array();
				}
			}
			lookupSQL = XVar.Clone(getLookupSQL((XVar)(parentValuesData), (XVar)(childVal), (XVar)(doCategoryFilter), (XVar)((XVar)(this.LCType == Constants.LCT_AJAX)  && (XVar)(initialLoad))));
			return getLookupContentDataBySql((XVar)(lookupSQL), (XVar)(childVal));
		}
		public virtual XVar getLookupContentDataBySql(dynamic _param_lookupSQL, dynamic _param_childVal = null)
		{
			#region default values
			if(_param_childVal as Object == null) _param_childVal = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic lookupSQL = XVar.Clone(_param_lookupSQL);
			dynamic childVal = XVar.Clone(_param_childVal);
			#endregion

			dynamic data = XVar.Array(), lookupIndexes = XVar.Array(), qResult = null, responce = null, var_response = XVar.Array();
			responce = XVar.Clone(XVar.Array());
			lookupIndexes = XVar.Clone(CommonFunctions.GetLookupFieldsIndexes((XVar)(this.pageObject.pSetEdit), (XVar)(this.field)));
			qResult = XVar.Clone(this.lookupConnection.query((XVar)(lookupSQL)));
			if((XVar)(!XVar.Equals(XVar.Pack(this.LCType), XVar.Pack(Constants.LCT_AJAX)))  || (XVar)(this.multiselect))
			{
				dynamic isUnique = null, uniqueArray = XVar.Array();
				isUnique = XVar.Clone(this.pageObject.pSetEdit.isLookupUnique((XVar)(this.field)));
				uniqueArray = XVar.Clone(XVar.Array());
				while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
				{
					if((XVar)(this.lookupType == Constants.LT_QUERY)  && (XVar)(isUnique))
					{
						if(XVar.Pack(MVCFunctions.in_array((XVar)(data[lookupIndexes["displayFieldIndex"]]), (XVar)(uniqueArray))))
						{
							continue;
						}
						uniqueArray.InitAndSetArrayItem(data[lookupIndexes["displayFieldIndex"]], null);
					}
					var_response.InitAndSetArrayItem(data[lookupIndexes["linkFieldIndex"]], null);
					var_response.InitAndSetArrayItem(data[lookupIndexes["displayFieldIndex"]], null);
				}
			}
			else
			{
				data = XVar.Clone(qResult.fetchNumeric());
				if((XVar)(data)  && (XVar)((XVar)((XVar)(!(XVar)(this.multiselect))  && (XVar)(MVCFunctions.strlen((XVar)(childVal))))  || (XVar)(!(XVar)(qResult.fetchNumeric()))))
				{
					var_response.InitAndSetArrayItem(data[lookupIndexes["linkFieldIndex"]], null);
					var_response.InitAndSetArrayItem(data[lookupIndexes["displayFieldIndex"]], null);
				}
			}
			return var_response;
		}
		public override XVar getLookupContentToReload(dynamic _param_isExistParent, dynamic _param_mode, dynamic _param_parentCtrlsData)
		{
			#region pass-by-value parameters
			dynamic isExistParent = XVar.Clone(_param_isExistParent);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic parentCtrlsData = XVar.Clone(_param_parentCtrlsData);
			#endregion

			if(XVar.Pack(isExistParent))
			{
				dynamic hasEmptyParent = null;
				hasEmptyParent = new XVar(false);
				foreach (KeyValuePair<XVar, dynamic> value in parentCtrlsData.GetEnumerator())
				{
					if(XVar.Equals(XVar.Pack(value.Value), XVar.Pack("")))
					{
						hasEmptyParent = new XVar(true);
						break;
					}
				}
				if(XVar.Pack(!(XVar)(hasEmptyParent)))
				{
					return loadLookupContent((XVar)(parentCtrlsData), new XVar(""), new XVar(true), new XVar(false));
				}
				if(mode == Constants.MODE_SEARCH)
				{
					return loadLookupContent((XVar)(parentCtrlsData), new XVar(""), new XVar(false), new XVar(false));
				}
				if((XVar)((XVar)((XVar)(mode == Constants.MODE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_EDIT))  || (XVar)(mode == Constants.MODE_INLINE_ADD))  || (XVar)(mode == Constants.MODE_ADD))
				{
					return "";
				}
				return loadLookupContent((XVar)(parentCtrlsData), new XVar(""), new XVar(true), new XVar(false));
			}
			else
			{
				if((XVar)((XVar)((XVar)((XVar)(mode == Constants.MODE_SEARCH)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  || (XVar)(mode == Constants.MODE_ADD))  || (XVar)(mode == Constants.MODE_EDIT))  || (XVar)(mode == Constants.MODE_INLINE_EDIT))
				{
					return loadLookupContent((XVar)(XVar.Array()), new XVar(""), new XVar(false), new XVar(false));
				}
				return loadLookupContent((XVar)(XVar.Array()), new XVar(""), new XVar(true), new XVar(false));
			}
			return "";
		}
		public virtual XVar getAutoFillData(dynamic _param_linkFieldVal)
		{
			#region pass-by-value parameters
			dynamic linkFieldVal = XVar.Clone(_param_linkFieldVal);
			#endregion

			dynamic data = XVar.Array(), row = XVar.Array();
			data = XVar.Clone(XVar.Array());
			if(this.lookupType == Constants.LT_QUERY)
			{
				GlobalVars.strSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(linkFieldVal), new XVar(false), new XVar(true), new XVar(true)));
				row = XVar.Clone(this.lookupConnection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
				data = XVar.Clone(this.ciphererLookup.DecryptFetchedArray((XVar)(row)));
			}
			else
			{
				dynamic autoCompleteFields = XVar.Array();
				GlobalVars.strSQL = XVar.Clone(getLookupSQL((XVar)(XVar.Array()), (XVar)(linkFieldVal), new XVar(false), new XVar(true), new XVar(true)));
				row = XVar.Clone(this.lookupConnection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
				autoCompleteFields = XVar.Clone(this.pageObject.pSetEdit.getAutoCompleteFields((XVar)(this.field)));
				foreach (KeyValuePair<XVar, dynamic> aData in autoCompleteFields.GetEnumerator())
				{
					data.InitAndSetArrayItem(row[aData.Value["lookupF"]], aData.Value["lookupF"]);
				}
			}
			this.lookupConnection.close();
			if(XVar.Pack(!(XVar)(MVCFunctions.count(data))))
			{
				data = XVar.Clone(new XVar(this.field, ""));
			}
			return data;
		}
		public override XVar getConnection()
		{
			return this.lookupConnection;
		}
		public override XVar getInputStyle(dynamic _param_mode)
		{
			#region pass-by-value parameters
			dynamic mode = XVar.Clone(_param_mode);
			#endregion

			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				return "class='form-control'";
			}
			else
			{
				return base.getInputStyle((XVar)(mode));
			}
			return null;
		}
		protected virtual XVar getLookupLinks(dynamic _param_hiddenSelect = null)
		{
			#region default values
			if(_param_hiddenSelect as Object == null) _param_hiddenSelect = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic hiddenSelect = XVar.Clone(_param_hiddenSelect);
			#endregion

			dynamic links = XVar.Array();
			links = XVar.Clone(XVar.Array());
			if(this.LCType == Constants.LCT_LIST)
			{
				dynamic visibility = null;
				visibility = XVar.Clone((XVar.Pack(hiddenSelect) ? XVar.Pack(" style=\"visibility: hidden;\"") : XVar.Pack("")));
				links.InitAndSetArrayItem(MVCFunctions.Concat("<a href=\"#\" id=\"", this.openlookup, "\"", visibility, ">", "Select", "</a>"), null);
			}
			if(XVar.Pack(this.addNewItem))
			{
				links.InitAndSetArrayItem(MVCFunctions.Concat("<a href=\"#\" id=\"addnew_", this.cfield, "\">", "Add new", "</a>"), null);
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(links))))
			{
				return "";
			}
			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				return MVCFunctions.Concat("<div class=\"bs-lookup-links\">", MVCFunctions.implode(new XVar(""), (XVar)(links)), "</div>");
			}
			return MVCFunctions.Concat("&nbsp;", MVCFunctions.implode(new XVar("&nbsp;"), (XVar)(links)));
		}
	}
}
