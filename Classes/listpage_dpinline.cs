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
	public partial class ListPage_DPInline : ListPage_Embed
	{
		public dynamic dpParams = XVar.Pack("");
		public dynamic dpMasterKey = XVar.Array();
		public dynamic masterShortTable = XVar.Pack("");
		public dynamic masterFormName = XVar.Pack("");
		public dynamic masterId = XVar.Pack("");
		protected static bool skipListPage_DPInlineCtor = false;
		public ListPage_DPInline(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_DPInlineCtor)
			{
				skipListPage_DPInlineCtor = false;
				return;
			}
			this.showAddInPopup = new XVar(true);
			this.showEditInPopup = new XVar(true);
			this.showViewInPopup = new XVar(true);
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.pageSize = XVar.Clone(-1);
			}
			initDPInlineParams();
			this.searchClauseObj.clearSearch();
			this.jsSettings.InitAndSetArrayItem(this.masterPageType, "tableSettings", this.tName, "masterPageType");
			this.jsSettings.InitAndSetArrayItem(this.masterTable, "tableSettings", this.tName, "masterTable");
			this.jsSettings.InitAndSetArrayItem(this.firstTime, "tableSettings", this.tName, "firstTime");
			this.jsSettings.InitAndSetArrayItem(getStrMasterKey(), "tableSettings", this.tName, "strKey");
		}
		public override XVar importLinksAttrs()
		{
			return null;
		}
		public override XVar displayMasterTableInfo()
		{
			return null;
		}
		public override XVar processMasterKeyValue()
		{
			dynamic i = null, masterKeys = XVar.Array();
			base.processMasterKeyValue();
			i = new XVar(1);
			for(;i <= MVCFunctions.count(this.masterKeysReq); i++)
			{
				this.dpMasterKey.InitAndSetArrayItem(this.masterKeysReq[i], null);
			}
			masterKeys = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.dpMasterKey); i++)
			{
				masterKeys.InitAndSetArrayItem(this.dpMasterKey[i], MVCFunctions.Concat("masterkey", i + 1));
			}
			this.controlsMap.InitAndSetArrayItem(masterKeys, "masterKeys");
			return null;
		}
		public virtual XVar initDPInlineParams()
		{
			dynamic i = null, strkey = null;
			strkey = new XVar("");
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.dpMasterKey); i++)
			{
				strkey = MVCFunctions.Concat(strkey, "&masterkey", i + 1, "=", MVCFunctions.RawUrlEncode((XVar)(this.dpMasterKey[i])));
			}
			this.dpParams = XVar.Clone(MVCFunctions.Concat("mode=listdetails&id=", this.id, "&mastertable=", MVCFunctions.RawUrlEncode((XVar)(this.masterTable)), strkey, (XVar.Pack(this.masterId) ? XVar.Pack(MVCFunctions.Concat("&masterid=", this.masterId)) : XVar.Pack("")), (XVar.Pack((XVar)(this.masterPageType == Constants.PAGE_EDIT)  || (XVar)(this.masterPageType == Constants.PAGE_VIEW)) ? XVar.Pack(MVCFunctions.Concat("&masterpagetype=", this.masterPageType)) : XVar.Pack(""))));
			return null;
		}
		public virtual XVar getStrMasterKey()
		{
			dynamic i = null, strkey = XVar.Array();
			strkey = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.dpMasterKey); i++)
			{
				strkey.InitAndSetArrayItem(this.dpMasterKey[i], i);
			}
			return strkey;
		}
		public override XVar isReoderByHeaderClickingEnabled()
		{
			return (XVar)(base.isReoderByHeaderClickingEnabled())  && (XVar)(this.masterPageType != Constants.PAGE_ADD);
		}
		public override XVar inlineAddLinksAttrs()
		{
			if(this.masterPageType != Constants.PAGE_VIEW)
			{
				base.inlineAddLinksAttrs();
			}
			return null;
		}
		public override XVar commonAssign()
		{
			dynamic i = null, permis = null;
			base.commonAssign();
			this.xt.assign(new XVar("left_block"), new XVar(false));
			if((XVar)((XVar)(this.masterPageType == Constants.PAGE_ADD)  || (XVar)(this.masterPageType == Constants.PAGE_VIEW))  || (XVar)(this.mode == Constants.LIST_DASHDETAILS))
			{
				this.xt.assign(new XVar("selectall_link"), new XVar(false));
				this.xt.assign(new XVar("checkbox_column"), new XVar(false));
				this.xt.assign(new XVar("checkbox_header"), new XVar(false));
				this.xt.assign(new XVar("editselected_link"), new XVar(false));
				this.xt.assign(new XVar("delete_link"), new XVar(false));
				this.xt.assign(new XVar("saveall_link"), new XVar(false));
				this.xt.assign(new XVar("withSelectedClass"), new XVar("rnr-hiddenelem"));
				if(this.masterPageType == Constants.PAGE_VIEW)
				{
					this.xt.assign(new XVar("record_controls_block"), new XVar(false));
				}
			}
			else
			{
				selectAllLinkAttrs();
				if(XVar.Pack(!(XVar)(mobileTemplateMode())))
				{
					checkboxColumnAttrs();
				}
				editSelectedLinkAttrs();
				saveAllLinkAttrs();
				deleteSelectedLink();
				if(this.masterPageType != Constants.PAGE_EDIT)
				{
					dynamic searchPermis = null;
					searchPermis = XVar.Clone(this.permis[this.tName]["search"]);
					this.xt.assign(new XVar("record_controls_block"), (XVar)((XVar)((XVar)(this.permis[this.tName]["edit"])  && (XVar)(this.pSet.hasInlineEdit()))  || (XVar)((XVar)(this.permis[this.tName]["delete"])  && (XVar)(this.pSet.hasDelete()))));
					this.xt.assign(new XVar("details_block"), (XVar)((XVar)(searchPermis)  && (XVar)(this.rowsFound)));
					this.xt.assign(new XVar("details_attrs"), (XVar)(MVCFunctions.Concat("id=\"detFound", this.id, "\" name=\"detFound", this.id, "\"")));
					this.xt.assign(new XVar("pages_block"), (XVar)((XVar)(searchPermis)  && (XVar)(this.rowsFound)));
				}
			}
			this.xt.assign(new XVar("withSelected"), (XVar)((XVar)((XVar)(this.permis[this.tName]["export"])  || (XVar)(this.permis[this.tName]["edit"]))  || (XVar)(this.permis[this.tName]["delete"])));
			if(this.numRowsFromSQL == 0)
			{
				this.xt.displayBrickHidden(new XVar("recordcontrol"));
			}
			if(this.masterPageType != Constants.PAGE_VIEW)
			{
				this.xt.assign(new XVar("inlineedit_column"), (XVar)((XVar)(inlineEditAvailable())  && (XVar)(this.permis[this.tName]["edit"])));
				assignListIconsColumn();
				cancelAllLinkAttrs();
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				permis = XVar.Clone((XVar)((XVar)(this.isGroupSecurity)  && (XVar)((XVar)(this.permis[this.allDetailsTablesArr[i]["dDataSourceTable"]]["add"])  || (XVar)(this.permis[this.allDetailsTablesArr[i]["dDataSourceTable"]]["search"])))  || (XVar)(!(XVar)(this.isGroupSecurity)));
				if(XVar.Pack(permis))
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.tName)), "_dtable_column")), (XVar)(permis));
					break;
				}
			}
			return null;
		}
		protected virtual XVar prepareTemplate()
		{
			dynamic bricksExcept = XVar.Array();
			bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "grid_mobile", 2, "pagination", 3, "reorder_records", 4, "bsgrid_tabs"));
			if(this.masterPageType == Constants.PAGE_LIST)
			{
				bricksExcept.InitAndSetArrayItem("details_found", null);
				bricksExcept.InitAndSetArrayItem("page_of", null);
			}
			if((XVar)((XVar)(this.masterPageType == Constants.PAGE_EDIT)  || (XVar)(this.masterPageType == Constants.PAGE_ADD))  || (XVar)(this.masterPageType == Constants.PAGE_LIST))
			{
				if((XVar)(this.pSet.hasInlineEdit())  || (XVar)((XVar)(this.pSet.hasDelete())  && (XVar)(this.masterPageType != Constants.PAGE_ADD)))
				{
					if((XVar)(this.permis[this.tName]["edit"])  || (XVar)(this.permis[this.tName]["delete"]))
					{
						bricksExcept.InitAndSetArrayItem("recordcontrol", null);
					}
				}
				if((XVar)(this.pSet.hasInlineAdd())  && (XVar)(this.permis[this.tName]["add"]))
				{
					bricksExcept.InitAndSetArrayItem("recordcontrols_new", null);
				}
			}
			bricksExcept.InitAndSetArrayItem("message", null);
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			return null;
		}
		protected virtual XVar getBSButtonsClass()
		{
			return "btn btn-xs btn-info";
		}
		protected virtual XVar getHeaderControlsBlocks()
		{
			dynamic bs_button_class = null, buttons = null, caption = null, controlsBlocks = null;
			controlsBlocks = new XVar("");
			buttons = new XVar("");
			bs_button_class = XVar.Clone(getBSButtonsClass());
			if((XVar)(inlineAddAvailable())  && (XVar)(this.xt.getVar(new XVar("inlineadd_link"))))
			{
				dynamic inlineaddlink_attrs = null;
				inlineaddlink_attrs = XVar.Clone(this.xt.getVar(new XVar("inlineaddlink_attrs")));
				if(XVar.Pack(addAvailable()))
				{
					caption = new XVar("Inline Add");
				}
				else
				{
					caption = new XVar("Add");
				}
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					controlsBlocks = XVar.Clone(MVCFunctions.Concat("<span class=\"rnr-dbebrick \">", "<div class=\"style1 rnr-bl rnr-b-recordcontrols_new\">", "<a class=\"rnr-button\" href=\"#\" ", inlineaddlink_attrs, ">", caption, "</a> ", "</div>", "</span>"));
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" href=\"#\" ", inlineaddlink_attrs, ">", caption, "</a> ");
				}
			}
			if((XVar)(addAvailable())  && (XVar)(this.xt.getVar(new XVar("add_link"))))
			{
				dynamic addlink_attrs = null;
				addlink_attrs = XVar.Clone(this.xt.getVar(new XVar("addlink_attrs")));
				if(XVar.Pack(inlineAddAvailable()))
				{
					caption = new XVar("Add new");
				}
				else
				{
					caption = new XVar("Add");
				}
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					controlsBlocks = XVar.Clone(MVCFunctions.Concat("<span class=\"rnr-dbebrick \">", "<div class=\"style1 rnr-bl rnr-b-recordcontrols_new\">", "<a class=\"rnr-button\" href=\"#\" ", addlink_attrs, ">", caption, "</a> ", "</div>", "</span>"));
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" href=\"#\" ", addlink_attrs, ">", caption, "</a> ");
				}
			}
			if((XVar)(inlineEditAvailable())  && (XVar)(this.xt.getVar(new XVar("editselected_link"))))
			{
				dynamic editselectedlink_attrs = null, editselectedlink_span = null;
				editselectedlink_attrs = XVar.Clone(this.xt.getVar(new XVar("editselectedlink_attrs")));
				editselectedlink_span = XVar.Clone(this.xt.getVar(new XVar("editselectedlink_span")));
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"rnr-button\" href=\"#\" ", editselectedlink_attrs, " ", editselectedlink_span, ">", "Edit", "</a> ");
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, " \" disabled href=\"#\" ", editselectedlink_attrs, " ", editselectedlink_span, ">", "Edit", "</a> ");
				}
			}
			if((XVar)((XVar)(updateSelectedAvailable())  && (XVar)(this.xt.getVar(new XVar("updateselected_link"))))  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				dynamic updateselectedlink_attrs = null;
				updateselectedlink_attrs = XVar.Clone(this.xt.getVar(new XVar("updateselectedlink_attrs")));
				buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" disabled href=\"#\" ", updateselectedlink_attrs, ">", "Update selected", "</a> ");
			}
			if(XVar.Pack(this.xt.getVar(new XVar("saveall_link"))))
			{
				dynamic savealllink_attrs = null, savealllink_span = null;
				savealllink_attrs = XVar.Clone(this.xt.getVar(new XVar("savealllink_attrs")));
				savealllink_span = XVar.Clone(this.xt.getVar(new XVar("savealllink_span")));
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"rnr-button\" href=\"#\" ", savealllink_attrs, " ", savealllink_span, ">", "Save all", "</a> ");
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" href=\"#\" ", savealllink_attrs, " ", savealllink_span, ">", "Save all", "</a> ");
				}
			}
			if(XVar.Pack(this.xt.getVar(new XVar("cancelall_link"))))
			{
				dynamic cancelalllink_attrs = null, cancelalllink_span = null;
				cancelalllink_attrs = XVar.Clone(this.xt.getVar(new XVar("cancelalllink_attrs")));
				cancelalllink_span = XVar.Clone(this.xt.getVar(new XVar("cancelalllink_span")));
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"rnr-button\" href=\"#\" ", cancelalllink_attrs, " ", cancelalllink_span, ">", "Cancel", "</a> ");
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" href=\"#\" ", cancelalllink_attrs, " ", cancelalllink_span, ">", "Cancel", "</a> ");
				}
			}
			if((XVar)(deleteAvailable())  && (XVar)(this.xt.getVar(new XVar("deleteselected_link"))))
			{
				dynamic deleteselectedlink_attrs = null, deleteselectedlink_span = null;
				deleteselectedlink_attrs = XVar.Clone(this.xt.getVar(new XVar("deleteselectedlink_attrs")));
				deleteselectedlink_span = XVar.Clone(this.xt.getVar(new XVar("deleteselectedlink_span")));
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"rnr-button \" href=\"#\" ", deleteselectedlink_attrs, " ", deleteselectedlink_span, ">", "Delete", "</a> ");
				}
				else
				{
					buttons = MVCFunctions.Concat(buttons, "<a class=\"", bs_button_class, "\" disabled href=\"#\" ", deleteselectedlink_attrs, " ", deleteselectedlink_span, ">", "Delete", "</a> ");
				}
			}
			if(XVar.Pack(buttons))
			{
				if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
				{
					controlsBlocks = MVCFunctions.Concat(controlsBlocks, "<span class=\"rnr-dbebrick \">", "<div class=\"style1 rnr-bl rnr-b-recordcontrol \">", buttons, "</div>", "</span>");
				}
				else
				{
					controlsBlocks = MVCFunctions.Concat(controlsBlocks, "<span class=\"rnr-dbebrick \">", buttons, "</span>");
				}
			}
			return MVCFunctions.Concat(controlsBlocks, "<div class=\"rnr-dbefiller\"></div>");
		}
		public override XVar showPage()
		{
			dynamic contents = null, var_response = XVar.Array();
			BeforeShowList();
			var_response = XVar.Clone(XVar.Array());
			if((XVar)(!(XVar)(isDispGrid()))  && (XVar)(0 == getGridTabsCount()))
			{
				var_response.InitAndSetArrayItem(true, "noData");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(var_response)));
				return null;
			}
			prepareTemplate();
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				contents = XVar.Clone(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("grid_tabs")), this.xt.fetch_loaded(new XVar("message_block")), this.xt.fetch_loaded(new XVar("grid_block")), this.xt.fetch_loaded(new XVar("pagination_block"))));
			}
			else
			{
				contents = XVar.Clone(this.xt.fetch_loaded(new XVar("body")));
			}
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			var_response.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			var_response.InitAndSetArrayItem(this.jsSettings, "settings");
			var_response.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			var_response.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			if((XVar)((XVar)(this.masterPageType == Constants.PAGE_EDIT)  && (XVar)(this.dashTName))  && (XVar)(this.dashElementName))
			{
				var_response.InitAndSetArrayItem(getHeaderControlsBlocks(), "headerButtonsBlock");
			}
			var_response.InitAndSetArrayItem(contents, "html");
			var_response.InitAndSetArrayItem(true, "success");
			var_response.InitAndSetArrayItem(this.id, "id");
			var_response.InitAndSetArrayItem(this.flyId, "idStartFrom");
			var_response.InitAndSetArrayItem(this.recordsDeleted, "delRecs");
			if(this.deleteMessage != "")
			{
				var_response.InitAndSetArrayItem(true, "delMess");
			}
			var_response.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			var_response.InitAndSetArrayItem(grabAllCSSFiles(), "additionalCSS");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(var_response)));
			return null;
		}
		public override XVar showPageDp(dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic contents = null, layout = null, pageSkinStyle = null;
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				return showGridOnly();
			}
			BeforeShowList();
			prepareTemplate();
			contents = XVar.Clone(this.xt.fetch_loaded(new XVar("body")));
			layout = GlobalVars.page_layouts[MVCFunctions.Concat(this.shortTableName, "_", this.pageType)];
			pageSkinStyle = XVar.Clone(MVCFunctions.Concat(layout.style, " page-", layout.name));
			this.xt.assign(new XVar("dpShowHide"), new XVar(true));
			this.xt.assign(new XVar("dpMinus"), new XVar(true));
			this.xt.assign(new XVar("dpShowHide_attrs"), (XVar)(MVCFunctions.Concat("id=\"dpShowHide", this.id, "\"")));
			this.xt.assign(new XVar("dpMinus_attrs"), (XVar)(MVCFunctions.Concat("id=\"dpMinus", this.id, "\"")));
			this.xt.assign(new XVar("dt_attrs"), (XVar)(MVCFunctions.Concat("name=\"dt", this.id, "\"")));
			if(XVar.Pack(CommonFunctions.GetGlobalData(new XVar("printDetailTableName"), new XVar(false))))
			{
				this.xt.assign(new XVar("dpShowHide"), new XVar(false));
				this.xt.assign(new XVar("dpMinus"), new XVar(false));
			}
			if(MVCFunctions.postvalue(new XVar("pdf")) == 1)
			{
				this.xt.assign(new XVar("dpMinus"), new XVar(false));
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"detailPreview", this.id, "\" class=\"", pageSkinStyle, " rnr-pagewrapper dpStyle\">", contents, "</div>"));
			return null;
		}
		public virtual XVar showGridOnly()
		{
			dynamic contents = null;
			prepareTemplate();
			contents = XVar.Clone(this.xt.fetch_loaded(new XVar("grid_block")));
			if(this.masterPageType != Constants.PAGE_ADD)
			{
				contents = XVar.Clone(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("grid_tabs")), this.xt.fetch_loaded(new XVar("message")), this.xt.fetch_loaded(new XVar("reorder_records")), contents));
			}
			contents = MVCFunctions.Concat(contents, this.xt.fetch_loaded(new XVar("pagination_block")));
			MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"detailPreview", this.id, "\">", contents, "</div>"));
			return null;
		}
		public override XVar buildSearchPanel()
		{
			return null;
		}
		public override XVar isPageSortable()
		{
			return this.masterPageType != Constants.PAGE_ADD;
		}
		public override XVar rulePRG()
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

			if(this.masterPageType == Constants.PAGE_ADD)
			{
				return "1=0";
			}
			return base.getMasterTableSQLClause();
		}
		public override XVar assignButtonsOnMasterEdit(dynamic _param_masterXt)
		{
			#region pass-by-value parameters
			dynamic masterXt = XVar.Clone(_param_masterXt);
			#endregion

			if(XVar.Pack(inlineAddAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("details_inlineadd_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("details_inlineadd_", this.shortTableName, "_attrs")), (XVar)(getInlineAddLinksAttrs()));
			}
			if(XVar.Pack(addAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("details_add_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("details_add_", this.shortTableName, "_attrs")), (XVar)(getAddLinksAttrs()));
			}
			if(XVar.Pack(deleteAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("details_delete_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("details_delete_", this.shortTableName, "_attrs")), (XVar)(getDeleteLinksAttrs()));
			}
			if(XVar.Pack(inlineEditAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("details_edit_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("details_edit_", this.shortTableName, "_attrs")), (XVar)(getEditLinksAttrs()));
			}
			if(XVar.Pack(updateSelectedAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("details_updateselected_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("details_updateselected_", this.shortTableName, "_attrs")), (XVar)(getUpdateSelectedAttrs()));
			}
			if((XVar)(inlineAddAvailable())  || (XVar)(inlineEditAvailable()))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("cancelall_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("cancelalllink_", this.shortTableName, "_span")), (XVar)(buttonShowHideStyle(new XVar("cancelall"))));
				masterXt.assign((XVar)(MVCFunctions.Concat("cancelalllink_", this.shortTableName, "_attrs")), (XVar)(MVCFunctions.Concat("name=\"revertall_edited", this.id, "\" id=\"revertall_edited", this.id, "\"")));
			}
			if((XVar)(this.masterPageType == Constants.PAGE_EDIT)  && (XVar)((XVar)(inlineAddAvailable())  || (XVar)(inlineEditAvailable())))
			{
				masterXt.assign((XVar)(MVCFunctions.Concat("saveall_", this.shortTableName, "_link")), new XVar(true));
				masterXt.assign((XVar)(MVCFunctions.Concat("savealllink_", this.shortTableName, "_span")), (XVar)(buttonShowHideStyle(new XVar("saveall"))));
				masterXt.assign((XVar)(MVCFunctions.Concat("savealllink_", this.shortTableName, "_attrs")), (XVar)(MVCFunctions.Concat("name=\"saveall_edited", this.id, "\" id=\"saveall_edited", this.id, "\"")));
			}
			return null;
		}
		protected override XVar fillTableSettings(dynamic _param_table = null, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			base.fillTableSettings((XVar)(table), (XVar)(pSet));
			if(XVar.Pack(addAvailable()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "showAddInPopup");
			}
			if((XVar)(editAvailable())  || (XVar)(updateSelectedAvailable()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "showEditInPopup");
			}
			if(XVar.Pack(viewAvailable()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "showViewInPopup");
			}
			return null;
		}
		public override XVar deleteAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.deleteAvailable());
		}
		public override XVar importAvailable()
		{
			return false;
		}
		public override XVar editAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.editAvailable());
		}
		public override XVar addAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.addAvailable());
		}
		public override XVar copyAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.copyAvailable());
		}
		public override XVar inlineEditAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.inlineEditAvailable());
		}
		public override XVar inlineAddAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(base.inlineAddAvailable()))  || (XVar)((XVar)(this.masterPageType == Constants.PAGE_ADD)  && (XVar)(base.addAvailable()));
		}
		protected override XVar displayViewLink()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(viewAvailable());
		}
		public override XVar updateSelectedAvailable()
		{
			return (XVar)((XVar)(this.masterPageType != Constants.PAGE_VIEW)  && (XVar)(this.masterPageType != Constants.PAGE_ADD))  && (XVar)(base.updateSelectedAvailable());
		}
		public override XVar isDispGrid()
		{
			if((XVar)(inlineAddAvailable())  || (XVar)(addAvailable()))
			{
				return true;
			}
			return base.isDispGrid();
		}
		public override XVar shouldDisplayDetailsPage()
		{
			return (XVar)(isDispGrid())  || (XVar)(0 != getGridTabsCount());
		}
	}
}
