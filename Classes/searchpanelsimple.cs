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
	public partial class SearchPanelSimple : SearchPanel
	{
		public dynamic srchPanelAttrs = XVar.Array();
		public dynamic isDisplaySearchPanel = XVar.Pack(true);
		public dynamic isFlexibleSearch = XVar.Pack(true);
		public dynamic searchOptions = XVar.Array();
		protected static bool skipSearchPanelSimpleCtor = false;
		public SearchPanelSimple(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipSearchPanelSimpleCtor)
			{
				skipSearchPanelSimpleCtor = false;
				return;
			}
			this.isDisplaySearchPanel = XVar.Clone(this.pSet.showSearchPanel());
			if((XVar)(this.pageObj.mobileTemplateMode())  && (XVar)(!(XVar)(this.isDisplaySearchPanel)))
			{
				dynamic advSearchFields = null;
				advSearchFields = XVar.Clone(this.pSet.getAdvSearchFields());
				if(XVar.Pack(MVCFunctions.count(advSearchFields)))
				{
					this.isDisplaySearchPanel = new XVar(true);
				}
			}
			this.isFlexibleSearch = XVar.Clone(this.pSet.isFlexibleSearch());
		}
		public override XVar buildSearchPanel()
		{
			base.buildSearchPanel();
			if(XVar.Pack(this.isDisplaySearchPanel))
			{
				this.srchPanelAttrs = XVar.Clone(this.searchClauseObj.getSrchPanelAttrs());
				this.searchOptions = XVar.Clone(this.pSet.getSearchPanelOptions());
				DisplaySearchPanel();
			}
			return null;
		}
		public override XVar searchAssign()
		{
			dynamic searchGlobalParams = XVar.Array(), searchOpt_mess = null, searchPanelAttrs = XVar.Array(), searchforAttrs = null, selectClass = null;
			base.searchAssign();
			searchGlobalParams = XVar.Clone(this.searchClauseObj.getSearchGlobalParams());
			searchPanelAttrs = XVar.Clone(this.searchClauseObj.getSrchPanelAttrs());
			this.xt.assign(new XVar("showHideSearchWin_attrs"), new XVar(" title=\"Floating window\""));
			searchOpt_mess = XVar.Clone((XVar.Pack(searchPanelAttrs["srchOptShowStatus"]) ? XVar.Pack("Hide search options") : XVar.Pack("Show search options")));
			this.xt.assign(new XVar("showHideSearchPanel_attrs"), (XVar)(MVCFunctions.Concat("align=\"absmiddle\" title=\"", searchOpt_mess, "\" alt=\"", searchOpt_mess, "\"")));
			searchforAttrs = XVar.Clone(MVCFunctions.Concat("name=\"ctlSearchFor", this.id, "\" id=\"ctlSearchFor", this.id, "\""));
			if(XVar.Pack(this.isUseAjaxSuggest))
			{
				searchforAttrs = MVCFunctions.Concat(searchforAttrs, " autocomplete=off ");
			}
			searchforAttrs = MVCFunctions.Concat(searchforAttrs, " placeholder=\"", "search", "\"");
			if((XVar)(this.searchClauseObj.isUsedSrch())  || (XVar)(MVCFunctions.strlen((XVar)(searchGlobalParams["simpleSrch"]))))
			{
				dynamic valSrchFor = null;
				valSrchFor = XVar.Clone(searchGlobalParams["simpleSrch"]);
				searchforAttrs = MVCFunctions.Concat(searchforAttrs, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(valSrchFor)), "\"");
			}
			this.xt.assignbyref(new XVar("searchfor_attrs"), (XVar)(searchforAttrs));
			this.xt.assign(new XVar("searchPanelTopButtons"), (XVar)(this.isDisplaySearchPanel));
			selectClass = new XVar("");
			if(this.pageObj.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				selectClass = new XVar("form-control");
			}
			if(XVar.Pack(this.pSet.showSimpleSearchOptions()))
			{
				dynamic simpleSearchFieldCombo = null, simpleSearchTypeCombo = null;
				simpleSearchTypeCombo = XVar.Clone(MVCFunctions.Concat("<select class=\"", selectClass, "\" id=\"simpleSrchTypeCombo", this.id, "\" name=\"simpleSrchTypeCombo", this.id, "\" size=\"1\">"));
				simpleSearchTypeCombo = MVCFunctions.Concat(simpleSearchTypeCombo, this.searchControlBuilder.getSimpleSearchTypeCombo((XVar)(searchGlobalParams["simpleSrchTypeComboOpt"]), (XVar)(searchGlobalParams["simpleSrchTypeComboNot"])));
				simpleSearchTypeCombo = MVCFunctions.Concat(simpleSearchTypeCombo, "</select>");
				this.xt.assign(new XVar("simpleSearchTypeCombo"), (XVar)(simpleSearchTypeCombo));
				simpleSearchFieldCombo = XVar.Clone(MVCFunctions.Concat("<select class=\"", selectClass, "\" id=\"simpleSrchFieldsCombo", this.id, "\" name=\"simpleSrchFieldsCombo", this.id, "\" size=\"1\">"));
				simpleSearchFieldCombo = MVCFunctions.Concat(simpleSearchFieldCombo, this.searchControlBuilder.simpleSearchFieldCombo((XVar)(this.allSearchFields), (XVar)(searchGlobalParams["simpleSrchFieldsComboOpt"])));
				simpleSearchFieldCombo = MVCFunctions.Concat(simpleSearchFieldCombo, "</select>");
				this.xt.assign(new XVar("simpleSearchFieldCombo"), (XVar)(simpleSearchFieldCombo));
			}
			return null;
		}
		public virtual XVar DisplaySearchPanel()
		{
			dynamic searchRadio = XVar.Array(), showHideOpt_mess = null;
			this.xt.assign(new XVar("searchPanel"), (XVar)(this.isDisplaySearchPanel));
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			if(XVar.Pack(!(XVar)(this.isFlexibleSearch)))
			{
				this.xt.assign(new XVar("controls_block_class"), new XVar("flexibleSearchPanel"));
			}
			searchRadio = XVar.Clone(this.searchControlBuilder.getSearchRadio());
			this.xt.assign_section(new XVar("all_checkbox_label"), (XVar)(searchRadio["all_checkbox_label"][0]), (XVar)(searchRadio["all_checkbox_label"][1]));
			this.xt.assign_section(new XVar("any_checkbox_label"), (XVar)(searchRadio["any_checkbox_label"][0]), (XVar)(searchRadio["any_checkbox_label"][1]));
			this.xt.assignbyref(new XVar("all_checkbox"), (XVar)(searchRadio["all_checkbox"]));
			this.xt.assignbyref(new XVar("any_checkbox"), (XVar)(searchRadio["any_checkbox"]));
			showHideOpt_mess = XVar.Clone((XVar.Pack(this.srchPanelAttrs["ctrlTypeComboStatus"]) ? XVar.Pack("Hide options") : XVar.Pack("Show options")));
			this.xt.assign(new XVar("showHideOpt_mess"), (XVar)(showHideOpt_mess));
			this.xt.assign(new XVar("showHideCtrlsOpt_attrs"), new XVar("style=\"display: none;\""));
			if(this.searchClauseObj.getUsedCtrlsCount() <= 0)
			{
				this.xt.assign(new XVar("bottomSearchButt_attrs"), new XVar("style=\"display: none;\""));
			}
			assignSearchBlocks();
			this.pageObj.controlsMap.InitAndSetArrayItem(this.searchClauseObj.isSearchPanelByUserApiRun(), "search", "searchPanelRunByUserApi");
			return null;
		}
		public virtual XVar assignSearchBlocks()
		{
			dynamic defaultValue = null, notAddedFileds = XVar.Array(), otherFieldsBlocks = XVar.Array(), recId = null, searchPanelFieldsBlocks = XVar.Array(), srchCtrlBlocksNumber = null;
			searchPanelFieldsBlocks = XVar.Clone(XVar.Array());
			otherFieldsBlocks = XVar.Clone(XVar.Array());
			notAddedFileds = XVar.Clone(XVar.Array());
			srchCtrlBlocksNumber = new XVar(0);
			recId = XVar.Clone(this.pageObj.genId());
			foreach (KeyValuePair<XVar, dynamic> searchField in this.allSearchFields.GetEnumerator())
			{
				dynamic isSrchPanelField = null, srchFields = XVar.Array();
				this.pageObj.fillFieldToolTips((XVar)(searchField.Value));
				srchFields = XVar.Clone(this.searchClauseObj.getSearchCtrlParams((XVar)(searchField.Value)));
				isSrchPanelField = XVar.Clone(MVCFunctions.in_array((XVar)(searchField.Value), (XVar)(this.panelSearchFields)));
				if(XVar.Pack(!(XVar)(MVCFunctions.count(srchFields))))
				{
					defaultValue = XVar.Clone(this.pSet.getDefaultValue((XVar)(searchField.Value)));
					if(XVar.Pack(isSrchPanelField))
					{
						dynamic opt = null;
						opt = new XVar("");
						if(XVar.Pack(!(XVar)(this.isFlexibleSearch)))
						{
							opt = XVar.Clone(this.searchOptions[searchField.Value]);
						}
						srchFields.InitAndSetArrayItem(new XVar("opt", opt, "not", "", "value1", defaultValue, "value2", ""), null);
					}
				}
				if(XVar.Pack(MVCFunctions.count(srchFields)))
				{
					if(XVar.Pack(isSrchPanelField))
					{
						srchFields.InitAndSetArrayItem(true, MVCFunctions.count(srchFields) - 1, "immutable");
					}
					foreach (KeyValuePair<XVar, dynamic> srchField in srchFields.GetEnumerator())
					{
						dynamic block = null;
						block = XVar.Clone(this.searchControlBuilder.buildSearchCtrlBlockArr((XVar)(recId), (XVar)(searchField.Value), new XVar(0), (XVar)(srchField.Value["opt"]), (XVar)(srchField.Value["not"]), new XVar(false), (XVar)(srchField.Value["value1"]), (XVar)(srchField.Value["value2"]), (XVar)(isSrchPanelField), (XVar)(this.isFlexibleSearch), (XVar)(srchField.Value["immutable"])));
						if(XVar.Pack(isSrchPanelField))
						{
							searchPanelFieldsBlocks.InitAndSetArrayItem(block, searchField.Value, null);
						}
						else
						{
							otherFieldsBlocks.InitAndSetArrayItem(block, null);
						}
						srchCtrlBlocksNumber++;
						addSearchFieldToControlsMap((XVar)(searchField.Value), ref recId);
					}
				}
				else
				{
					notAddedFileds.InitAndSetArrayItem(searchField.Value, null);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> namedBlocks in searchPanelFieldsBlocks.GetEnumerator())
			{
				this.xt.assign_loopsection_byValue((XVar)(MVCFunctions.Concat("searchCtrlBlock_", MVCFunctions.GoodFieldName((XVar)(namedBlocks.Key)))), (XVar)(namedBlocks.Value));
			}
			if(XVar.Pack(!(XVar)(this.isFlexibleSearch)))
			{
				return null;
			}
			if((XVar)(XVar.Pack(0) < srchCtrlBlocksNumber)  && (XVar)(srchCtrlBlocksNumber < GlobalVars.gLoadSearchControls))
			{
				dynamic otherSearchControlsMaxNumber = null;
				otherSearchControlsMaxNumber = XVar.Clone((GlobalVars.gLoadSearchControls - srchCtrlBlocksNumber) + MVCFunctions.count(otherFieldsBlocks));
				foreach (KeyValuePair<XVar, dynamic> searchField in notAddedFileds.GetEnumerator())
				{
					defaultValue = XVar.Clone(this.pSet.getDefaultValue((XVar)(searchField.Value)));
					otherFieldsBlocks.InitAndSetArrayItem(this.searchControlBuilder.buildSearchCtrlBlockArr((XVar)(recId), (XVar)(searchField.Value), new XVar(0), new XVar(""), new XVar(false), new XVar(true), (XVar)(defaultValue), new XVar("")), null);
					addSearchFieldToControlsMap((XVar)(searchField.Value), ref recId);
					if(otherSearchControlsMaxNumber <= MVCFunctions.count(otherFieldsBlocks))
					{
						break;
					}
				}
			}
			this.xt.assign_loopsection(new XVar("searchCtrlBlock"), (XVar)(otherFieldsBlocks));
			return null;
		}
		public virtual XVar addSearchFieldToControlsMap(dynamic _param_fName, ref dynamic recId)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic ctrlInd = null, isFieldNeedSecCtrl = null, searchBlock = XVar.Array();
			isFieldNeedSecCtrl = XVar.Clone(this.searchControlBuilder.isNeedSecondCtrl((XVar)(fName)));
			searchBlock = XVar.Clone(new XVar("fName", fName, "recId", recId));
			ctrlInd = new XVar(0);
			searchBlock.InitAndSetArrayItem(ctrlInd, "ctrlsMap", 0);
			if(XVar.Pack(isFieldNeedSecCtrl))
			{
				searchBlock.InitAndSetArrayItem(ctrlInd + 1, "ctrlsMap", 1);
			}
			if(XVar.Pack(!(XVar)(this.isFlexibleSearch)))
			{
				searchBlock.InitAndSetArrayItem(this.searchOptions[fName], "inflexSearchOption");
			}
			this.pageObj.controlsMap.InitAndSetArrayItem(searchBlock, "search", "searchBlocks", null);
			recId = XVar.Clone(this.pageObj.genId());
			return null;
		}
		public virtual XVar refineOpenFilters(dynamic _param_openFilters)
		{
			#region pass-by-value parameters
			dynamic openFilters = XVar.Clone(_param_openFilters);
			#endregion

			dynamic openFiltersRefined = XVar.Array();
			openFiltersRefined = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> panelFiled in this.panelSearchFields.GetEnumerator())
			{
				dynamic key = null;
				key = XVar.Clone(MVCFunctions.array_search((XVar)(panelFiled.Value), (XVar)(openFilters)));
				if(!XVar.Equals(XVar.Pack(key), XVar.Pack(false)))
				{
					MVCFunctions.array_splice((XVar)(openFilters), (XVar)(key), new XVar(1));
				}
			}
			foreach (KeyValuePair<XVar, dynamic> field in openFilters.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.in_array((XVar)(field.Value), (XVar)(this.allSearchFields))))
				{
					openFiltersRefined.InitAndSetArrayItem(field.Value, null);
				}
			}
			return openFiltersRefined;
		}
	}
}
