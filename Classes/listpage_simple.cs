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
	public partial class ListPage_Simple : ListPage
	{
		protected static bool skipListPage_SimpleCtor = false;
		public ListPage_Simple(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_SimpleCtor)
			{
				skipListPage_SimpleCtor = false;
				return;
			}
			this.pSetEdit = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_SEARCH)));
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			importLinksAttrs();
			this.xt.assign(new XVar("left_block"), new XVar(true));
			addAssignTopLinks();
			addAssignPageDetails();
			this.xt.assign(new XVar("moreButtons"), (XVar)((XVar)((XVar)((XVar)(exportAvailable())  || (XVar)(printAvailable()))  || (XVar)(importAvailable()))  || (XVar)(advSearchAvailable())));
			this.xt.assign(new XVar("withSelected"), (XVar)((XVar)((XVar)((XVar)(exportAvailable())  || (XVar)(printAvailable()))  || (XVar)(inlineEditAvailable()))  || (XVar)(deleteAvailable())));
			if(XVar.Pack(exportAvailable()))
			{
				this.xt.assign(new XVar("exportselected_link"), new XVar(true));
				this.xt.assign(new XVar("exportselectedlink_span"), (XVar)(buttonShowHideStyle()));
				this.xt.assign(new XVar("exportselectedlink_attrs"), (XVar)(getPrintExportLinkAttrs(new XVar("export"))));
			}
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				this.xt.assign(new XVar("field_hide_panel"), new XVar(true));
			}
			if(XVar.Pack(printAvailable()))
			{
				dynamic i = null;
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.displayBrickHidden(new XVar("printpanel"));
				}
				this.xt.assign(new XVar("print_friendly"), new XVar(true));
				this.xt.assign(new XVar("print_friendly_all"), new XVar(true));
				this.xt.assign(new XVar("print_recspp"), (XVar)(this.pSet.getPrinterSplitRecords()));
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
				{
					if((XVar)(this.permis[this.allDetailsTablesArr[i]["dDataSourceTable"]]["add"])  || (XVar)(this.permis[this.allDetailsTablesArr[i]["dDataSourceTable"]]["search"]))
					{
						this.xt.assign(new XVar("print_details"), new XVar(true));
						this.xt.assign((XVar)(MVCFunctions.Concat("print_details_", MVCFunctions.GoodFieldName((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])))), new XVar(true));
					}
				}
				this.xt.assign(new XVar("printselected_link"), new XVar(true));
				this.xt.assign(new XVar("printselectedlink_attrs"), (XVar)(getPrintExportLinkAttrs(new XVar("print"))));
				this.xt.assign(new XVar("printselectedlink_span"), (XVar)(buttonShowHideStyle()));
			}
			this.xt.assign(new XVar("advsearchlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"advButton", this.id, "\"")));
			this.xt.assign(new XVar("menu_block"), (XVar)((XVar)(isShowMenu())  || (XVar)(isAdminTable())));
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.xt.assign(new XVar("morelinkmobile_block"), new XVar(true));
				this.xt.assign(new XVar("tableinfomobile_block"), new XVar(true));
				this.xt.displayBrickHidden(new XVar("vmsearch2"));
			}
			setupBreadcrumbs();
			this.xt.assign(new XVar("grid_classes"), new XVar("table-bordered table-striped"));
			return null;
		}
		protected override XVar setGridUserParams()
		{
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				dynamic fieldsClasses = XVar.Array(), hideColumns = XVar.Array();
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isAllowShowHideFields");
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.displayBrickHidden(new XVar("bsfieldhidepanel"));
				}
				hideColumns = XVar.Clone(getColumnsToHide());
				this.jsSettings.InitAndSetArrayItem(hideColumns, "tableSettings", this.tName, "hideColumns");
				fieldsClasses = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> fields in hideColumns.GetEnumerator())
				{
					foreach (KeyValuePair<XVar, dynamic> f in fields.Value.GetEnumerator())
					{
						fieldsClasses[f.Value] = MVCFunctions.Concat(fieldsClasses[f.Value], " bs-hidden-column", fields.Key);
					}
					foreach (KeyValuePair<XVar, dynamic> c in fieldsClasses.GetEnumerator())
					{
						this.hiddenColumnClasses.InitAndSetArrayItem(c.Value, c.Key);
					}
				}
			}
			if(XVar.Pack(this.pSet.isAllowFieldsReordering()))
			{
				dynamic columnOrder = null, logger = null;
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isAllowFieldsReordering");
				logger = XVar.Clone(new paramsLogger((XVar)(this.tName), new XVar(Constants.FORDER_PARAMS_TYPE)));
				columnOrder = XVar.Clone(logger.getData());
				if(XVar.Pack(columnOrder))
				{
					this.jsSettings.InitAndSetArrayItem(columnOrder, "tableSettings", this.tName, "columnOrder");
				}
			}
			return null;
		}
		protected virtual XVar setupBreadcrumbs()
		{
			if(XVar.Pack(isAdminTable()))
			{
				prepareBreadcrumbs(new XVar("adminarea"));
			}
			else
			{
				prepareBreadcrumbs(new XVar("main"));
			}
			return null;
		}
		public virtual XVar addAssignTopLinks()
		{
			if((XVar)(!(XVar)(isDispGrid()))  && (XVar)(!(XVar)(this.pSetEdit.ajaxBasedListPage())))
			{
				return null;
			}
			if(XVar.Pack(printAvailable()))
			{
				dynamic needShowLinkForAdd = null;
				this.xt.assign(new XVar("prints_block"), new XVar(true));
				this.xt.assign(new XVar("print_link"), (XVar)(this.rowsFound));
				this.xt.assign(new XVar("printlink_attrs"), (XVar)(MVCFunctions.Concat("id='print_", this.id, "' name='print_", this.id, "'", (XVar.Pack((XVar)(!(XVar)(this.rowsFound))  && (XVar)(needShowLinkForAdd)) ? XVar.Pack(" style='display:none;'") : XVar.Pack("")))));
				this.xt.assign(new XVar("printall_link"), new XVar(true));
				this.xt.assign(new XVar("printalllink_attrs"), (XVar)(MVCFunctions.Concat("id='printAll_", this.id, "' name='printAll_", this.id, "'", (XVar.Pack(!(XVar)(this.rowsFound)) ? XVar.Pack(" style='display:none;'") : XVar.Pack("")))));
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.displayBrickHidden(new XVar("toplinks_print"));
				}
			}
			if(XVar.Pack(exportAvailable()))
			{
				this.xt.assign(new XVar("export_link"), new XVar(true));
				this.xt.assign(new XVar("exportlink_attrs"), (XVar)(MVCFunctions.Concat("id='export_", this.id, "'", (XVar.Pack(!(XVar)(this.rowsFound)) ? XVar.Pack(" style='display:none;'") : XVar.Pack("")))));
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.displayBrickHidden(new XVar("toplinks_export"));
				}
			}
			return null;
		}
		public virtual XVar addAssignPageDetails()
		{
			dynamic searchPermis = null;
			searchPermis = XVar.Clone(this.permis[this.tName]["search"]);
			if((XVar)((XVar)(!(XVar)(this.rowsFound))  && (XVar)(!(XVar)(inlineAddAvailable())))  && (XVar)(!(XVar)(this.showAddInPopup)))
			{
				return null;
			}
			this.xt.assign(new XVar("details_block"), (XVar)(searchPermis));
			if(XVar.Pack(!(XVar)(this.rowsFound)))
			{
				this.xt.displayBrickHidden(new XVar("details_found"));
				this.xt.displayBrickHidden(new XVar("vdetails_found"));
			}
			this.xt.assign(new XVar("pages_block"), (XVar)(searchPermis));
			if(XVar.Pack(!(XVar)(this.rowsFound)))
			{
				this.xt.displayBrickHidden(new XVar("page_of"));
				this.xt.displayBrickHidden(new XVar("vpage_of"));
			}
			this.xt.assign(new XVar("pages_attrs"), (XVar)(MVCFunctions.Concat("id=\"pageOf", this.id, "\" name=\"pageOf", this.id, "\"")));
			if((XVar)(searchPermis)  && (XVar)(MVCFunctions.count(this.arrRecsPerPage)))
			{
				this.xt.assign(new XVar("recordspp_block"), new XVar(true));
				createPerPage();
				if(XVar.Pack(!(XVar)(this.rowsFound)))
				{
					this.xt.displayBrickHidden(new XVar("recsperpage"));
					this.xt.displayBrickHidden(new XVar("vrecsperpage"));
				}
			}
			return null;
		}
		public override XVar addCommonHtml()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage((XVar)(this.isDisplayLoading)));
			base.addCommonHtml();
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			return null;
		}
		public override XVar buildSearchPanel()
		{
			dynamic allSearchFields = XVar.Array(), i = null, panelSearchFields = XVar.Array(), var_params = XVar.Array();
			if(XVar.Pack(!(XVar)(this.permis[this.tName]["search"])))
			{
				return null;
			}
			this.xt.enable_section(new XVar("searchPanel"));
			var_params = XVar.Clone(XVar.Array());
			var_params.InitAndSetArrayItem(this, "pageObj");
			var_params.InitAndSetArrayItem(this.panelSearchFields, "panelSearchFields");
			panelSearchFields = XVar.Clone(XVar.Array());
			allSearchFields = XVar.Clone(this.pSet.getAllSearchFields());
			i = new XVar(0);
			for(;i < MVCFunctions.count(allSearchFields); i++)
			{
				if(XVar.Pack(!(XVar)(matchWithDetailKeys((XVar)(allSearchFields[i])))))
				{
					panelSearchFields.InitAndSetArrayItem(allSearchFields[i], null);
				}
			}
			var_params.InitAndSetArrayItem(panelSearchFields, "allSearchFields");
			this.searchPanel = XVar.Clone(new SearchPanelSimple((XVar)(var_params)));
			this.searchPanel.buildSearchPanel();
			return null;
		}
		public override XVar prepareForResizeColumns()
		{
			dynamic columnsData = null, logger = null;
			base.prepareForResizeColumns();
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				return null;
			}
			logger = XVar.Clone(new paramsLogger((XVar)(this.tName), new XVar(Constants.CRESIZE_PARAMS_TYPE)));
			columnsData = XVar.Clone(logger.getData());
			if(XVar.Pack(columnsData))
			{
				this.pageData.InitAndSetArrayItem(columnsData, "resizableColumnsData");
			}
			return null;
		}
		protected override XVar getColumnsToHide()
		{
			return getCombinedHiddenColumns();
		}
		protected override XVar prepareEmptyFPMarkup()
		{
			if((XVar)((XVar)((XVar)(this.listAjax)  && (XVar)(this.pSetEdit.isSearchRequiredForFiltering()))  && (XVar)(!(XVar)(isRequiredSearchRunning())))  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				this.xt.enable_section(new XVar("filterPanel"));
				this.xt.displayBrickHidden(new XVar("filterpanel"));
			}
			return null;
		}
	}
}
