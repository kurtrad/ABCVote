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
	public partial class SearchPanel : XClass
	{
		public dynamic tName = XVar.Pack("");
		public ProjectSettings pSet = null;
		public dynamic dispNoneStyle = XVar.Pack("style=\"display: none;\"");
		public dynamic pageObj = XVar.Pack(null);
		public dynamic searchClauseObj = XVar.Pack(null);
		public dynamic searchControlBuilder = XVar.Pack(null);
		public dynamic id = XVar.Pack(1);
		public dynamic panelState = XVar.Array();
		public dynamic panelSearchFields = XVar.Array();
		public dynamic allSearchFields = XVar.Array();
		public dynamic isUseAjaxSuggest = XVar.Pack(false);
		public dynamic searchPerm = XVar.Pack(true);
		public XTempl xt;
		public SearchPanel(dynamic var_params)
		{
			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			this.searchClauseObj = this.pageObj.searchClauseObj;
			this.id = XVar.Clone(this.pageObj.id);
			this.pSet = XVar.UnPackProjectSettings(this.pageObj.pSetSearch);
			this.tName = XVar.Clone(this.pageObj.searchTableName);
			this.xt = XVar.UnPackXTempl(this.pageObj.xt);
			this.panelState = XVar.Clone(this.searchClauseObj.getSrchPanelAttrs());
			this.isUseAjaxSuggest = XVar.Clone(this.pSet.isUseAjaxSuggest());
			if(XVar.Pack(this.pageObj.mobileTemplateMode()))
			{
				this.isUseAjaxSuggest = new XVar(false);
			}
			this.searchControlBuilder = XVar.Clone(new PanelSearchControl((XVar)(this.id), (XVar)(this.tName), (XVar)(this.searchClauseObj), (XVar)(this.pageObj)));
			if(XVar.Pack(!(XVar)(var_params.KeyExists("panelSearchFields"))))
			{
				this.panelSearchFields = XVar.Clone(this.pSet.getPanelSearchFields());
			}
			if(XVar.Pack(!(XVar)(var_params.KeyExists("allSearchFields"))))
			{
				this.allSearchFields = XVar.Clone(this.pSet.getAllSearchFields());
			}
		}
		public virtual XVar getSearchPerm(dynamic _param_tName = null)
		{
			#region default values
			if(_param_tName as Object == null) _param_tName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			#endregion

			dynamic strPerm = null;
			tName = XVar.Clone((XVar.Pack(tName) ? XVar.Pack(tName) : XVar.Pack(this.tName)));
			if(XVar.Pack(!(XVar)(GlobalVars.isGroupSecurity)))
			{
				return true;
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(tName)));
			return !XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false));
		}
		public virtual XVar buildSearchPanel()
		{
			searchAssign();
			return null;
		}
		public virtual XVar searchAssign()
		{
			dynamic searchPerm = XVar.Array(), showallbutton_attrs = null, srchButtTitle = null;
			this.xt.assign(new XVar("asearch_link"), (XVar)(this.searchPerm));
			if((XVar)(CommonFunctions.isEnableSection508())  && (XVar)(this.searchPerm))
			{
				searchPerm = XVar.Clone(XVar.Array());
				searchPerm.InitAndSetArrayItem("<a name=\"skipsearch\"></a>", "begin");
			}
			else
			{
				searchPerm = XVar.Clone(this.searchPerm);
			}
			this.xt.assign(new XVar("searchform_block"), (XVar)(searchPerm));
			if(XVar.Pack(this.pageObj.mobileTemplateMode()))
			{
				this.xt.assign(new XVar("searchformmobile_block"), (XVar)(searchPerm));
			}
			this.xt.assign(new XVar("searchformbuttons_block"), (XVar)(searchPerm));
			this.xt.assign(new XVar("searchform_text"), new XVar(true));
			this.xt.assign(new XVar("searchform_search"), new XVar(true));
			srchButtTitle = new XVar("Search");
			this.xt.assign(new XVar("searchbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"searchButtTop", this.id, "\" title=\"", srchButtTitle, "\"")));
			this.xt.assign(new XVar("clear_searchbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"clearSearch", this.id, "\"")));
			if(XVar.Pack(this.pageObj.mobileTemplateMode()))
			{
				this.xt.assign(new XVar("searchform_showall_mobile"), new XVar(true));
				this.xt.assign(new XVar("searchform_clear_search_mobile"), new XVar(true));
			}
			else
			{
				if(XVar.Pack(!(XVar)(this.pSet.noRecordsOnFirstPage())))
				{
					this.xt.assign(new XVar("searchform_showall"), new XVar(true));
					this.xt.assign(new XVar("searchform_clear_search"), new XVar(true));
				}
			}
			showallbutton_attrs = XVar.Clone(MVCFunctions.Concat("id=\"showAll", this.id, "\""));
			if(XVar.Pack(!(XVar)(this.searchClauseObj.isShowAll())))
			{
				showallbutton_attrs = MVCFunctions.Concat(showallbutton_attrs, " ", this.dispNoneStyle);
				this.xt.assign(new XVar("showAllCont_attrs"), (XVar)(this.dispNoneStyle));
				this.xt.assign(new XVar("clearSearchCont_attrs"), (XVar)(this.dispNoneStyle));
			}
			this.xt.assign(new XVar("showallbutton_attrs"), (XVar)(showallbutton_attrs));
			return null;
		}
	}
}
