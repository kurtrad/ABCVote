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
	public partial class ListPage_Dashboard : ListPage_Embed
	{
		public dynamic showNoData = XVar.Pack(false);
		protected static bool skipListPage_DashboardCtor = false;
		public ListPage_Dashboard(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_DashboardCtor)
			{
				skipListPage_DashboardCtor = false;
				return;
			}
			this.showAddInPopup = new XVar(true);
			this.formBricks.InitAndSetArrayItem(new XVar(0, new XVar("name", "details_block", "align", "right"), 1, new XVar("name", "newrecord_controls_block", "align", "right"), 2, new XVar("name", "record_controls_block", "align", "right"), 3, new XVar("name", "details_found", "align", "right")), "header");
			this.formBricks.InitAndSetArrayItem(new XVar(0, "pagination_block"), "footer");
			if(XVar.Pack(this.mapRefresh))
			{
				this.pageSize = XVar.Clone(-1);
			}
		}
		protected override XVar assignSessionPrefix()
		{
			this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
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
			if(XVar.Pack(inlineEditAvailable()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "inlineEditAvailable");
			}
			return null;
		}
		public override XVar isDispGrid()
		{
			return this.permis[this.tName]["search"];
		}
		public override XVar addCommonJs()
		{
			addJsForGrid();
			addControlsJSAndCSS();
			addButtonHandlers();
			return null;
		}
		public override XVar addJsForGrid()
		{
			if(XVar.Pack(this.isResizeColumns))
			{
				prepareForResizeColumns();
			}
			this.jsSettings.InitAndSetArrayItem((XVar.Pack(this.numRowsFromSQL) ? XVar.Pack(true) : XVar.Pack(false)), "tableSettings", this.tName, "showRows");
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
			logger = XVar.Clone(new paramsLogger((XVar)(MVCFunctions.Concat(this.dashTName, "_", this.dashElementName)), new XVar(Constants.CRESIZE_PARAMS_TYPE)));
			columnsData = XVar.Clone(logger.getData());
			if(XVar.Pack(columnsData))
			{
				this.pageData.InitAndSetArrayItem(columnsData, "resizableColumnsData");
			}
			return null;
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			this.xt.assign(new XVar("details_block"), new XVar(true));
			this.xt.assign(new XVar("withSelected"), (XVar)((XVar)(inlineEditAvailable())  || (XVar)(deleteAvailable())));
			this.xt.displayBrickHidden(new XVar("printpanel"));
			return null;
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if((XVar)(this.mode == Constants.LIST_DASHBOARD)  && (XVar)(hasMainDashMapElem()))
			{
				sql.InitAndSetArrayItem((XVar.Pack(this.mapRefresh) ? XVar.Pack(getWhereByMap()) : XVar.Pack("1=0")), "mandatoryWhere", null);
			}
			if(XVar.Pack(this.showNoData))
			{
				sql.InitAndSetArrayItem("1=0", "mandatoryWhere", null);
			}
			return sql;
		}
		public override XVar showPage()
		{
			dynamic bricksExcept = null;
			BeforeShowList();
			prepareGridTabs();
			if(XVar.Pack(mobileTemplateMode()))
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid_mobile", 1, "pagination", 2, "details_found"));
			}
			else
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination", 2, "message", 3, "add", 4, "recordcontrols_new", 5, "recordcontrol", 6, "details_found", 7, "reorder_records"));
			}
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			showPageAjax();
			return null;
		}
		public virtual XVar showPageAjax()
		{
			dynamic returnJSON = XVar.Array();
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			if(XVar.Pack(this.formBricks["header"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["header"]), new XVar(true)), "headerCont");
			}
			returnJSON.InitAndSetArrayItem(MVCFunctions.Concat("<span class=\"rnr-dbebrick\">", getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</span>", returnJSON["headerCont"]), "headerCont");
			if(XVar.Pack(this.formBricks["footer"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["footer"]), new XVar(true)), "footerCont");
			}
			assignFormFooterAndHeaderBricks(new XVar(false));
			this.xt.prepareContainers();
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(this.xt.fetch_loaded(new XVar("message_block")), this.xt.fetch_loaded(new XVar("reorder_records")), this.xt.fetch_loaded(new XVar("grid_block"))), "html");
			}
			else
			{
				returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			}
			returnJSON.InitAndSetArrayItem(this.flyId, "idStartFrom");
			returnJSON.InitAndSetArrayItem(true, "success");
			returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "CSSFiles");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			return null;
		}
		public override XVar fillSetCntrlMaps()
		{
			base.fillSetCntrlMaps();
			this.controlsHTMLMap.InitAndSetArrayItem(this.myPage, this.tName, this.pageType, this.id, "pageNumber");
			this.controlsHTMLMap.InitAndSetArrayItem(this.maxPages, this.tName, this.pageType, this.id, "numberOfPages");
			return null;
		}
		public override XVar fillCheckAttr(dynamic record, dynamic _param_data, dynamic _param_keyblock)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic keyblock = XVar.Clone(_param_keyblock);
			#endregion

			if((XVar)((XVar)(deleteAvailable())  || (XVar)(inlineEditAvailable()))  || (XVar)(updateSelectedAvailable()))
			{
				record.InitAndSetArrayItem(true, "checkbox");
			}
			record.InitAndSetArrayItem(MVCFunctions.Concat("name=\"selection[]\" value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(keyblock)), "\" id=\"check", this.id, "_", this.recId, "\""), "checkbox_attrs");
			return null;
		}
		public override XVar deleteAvailable()
		{
			return (XVar)(base.deleteAvailable())  && (XVar)(this.dashElementData["deleteRecord"]);
		}
		public override XVar editAvailable()
		{
			return (XVar)(base.editAvailable())  && (XVar)(this.dashElementData["popupEdit"]);
		}
		public override XVar addAvailable()
		{
			return (XVar)(base.addAvailable())  && (XVar)(this.dashElementData["popupAdd"]);
		}
		public override XVar inlineEditAvailable()
		{
			return (XVar)(base.inlineEditAvailable())  && (XVar)(this.dashElementData["inlineEdit"]);
		}
		public override XVar inlineAddAvailable()
		{
			return (XVar)(base.inlineAddAvailable())  && (XVar)(this.dashElementData["inlineAdd"]);
		}
		public override XVar updateSelectedAvailable()
		{
			return (XVar)(base.updateSelectedAvailable())  && (XVar)(this.dashElementData["updateSelected"]);
		}
		public override XVar viewAvailable()
		{
			return (XVar)(base.viewAvailable())  && (XVar)(this.dashElementData["popupView"]);
		}
		public override XVar detailsInGridAvailable()
		{
			return false;
		}
		protected virtual XVar hasDependentDashMapElem()
		{
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)((XVar)(dElem.Value["table"] == this.tName)  && (XVar)(dElem.Value["type"] == Constants.DASHBOARD_MAP))  && (XVar)(!(XVar)(dElem.Value["updateMoved"])))
				{
					return true;
				}
			}
			return false;
		}
		protected override XVar hasMainDashMapElem()
		{
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)((XVar)(dElem.Value["table"] == this.tName)  && (XVar)(dElem.Value["type"] == Constants.DASHBOARD_MAP))  && (XVar)(dElem.Value["updateMoved"]))
				{
					return true;
				}
			}
			return false;
		}
		protected override XVar hasBigMap()
		{
			return (XVar)(base.hasBigMap())  || (XVar)(hasDependentDashMapElem());
		}
		public override XVar addBigGoogleMapMarkers(dynamic data, dynamic _param_keys, dynamic _param_editLink = null)
		{
			#region default values
			if(_param_editLink as Object == null) _param_editLink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			dynamic editLink = XVar.Clone(_param_editLink);
			#endregion

			base.addBigGoogleMapMarkers((XVar)(data), (XVar)(keys), (XVar)(editLink));
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				dynamic mapId = null, markerData = XVar.Array();
				if((XVar)((XVar)((XVar)(dElem.Value["elementName"] == this.dashElementName)  || (XVar)(dElem.Value["table"] != this.tName))  || (XVar)(dElem.Value["type"] != Constants.DASHBOARD_MAP))  || (XVar)(dElem.Value["updateMoved"]))
				{
					continue;
				}
				markerData = XVar.Clone(XVar.Array());
				markerData.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[dElem.Value["latF"]]) ? XVar.Pack(data[dElem.Value["latF"]]) : XVar.Pack("")))), "lat");
				markerData.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[dElem.Value["lonF"]]) ? XVar.Pack(data[dElem.Value["lonF"]]) : XVar.Pack("")))), "lng");
				markerData.InitAndSetArrayItem((XVar.Pack(data[dElem.Value["addressF"]]) ? XVar.Pack(data[dElem.Value["addressF"]]) : XVar.Pack("")), "address");
				markerData.InitAndSetArrayItem((XVar.Pack(data[dElem.Value["descF"]]) ? XVar.Pack(data[dElem.Value["descF"]]) : XVar.Pack(markerData["address"])), "desc");
				markerData.InitAndSetArrayItem(this.dashSet.getDashMapIcon((XVar)(dElem.Value["elementName"]), (XVar)(data)), "mapIcon");
				markerData.InitAndSetArrayItem(this.recId, "recId");
				markerData.InitAndSetArrayItem(keys, "keys");
				markerData.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(data)), "masterKeys");
				mapId = XVar.Clone(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(this.dashTName)), "_", dElem.Value["elementName"], "_dashMap"));
				if(XVar.Pack(!(XVar)(this.googleMapCfg["mapsData"].KeyExists(mapId))))
				{
					this.googleMapCfg.InitAndSetArrayItem(XVar.Array(), "mapsData", mapId);
					this.googleMapCfg.InitAndSetArrayItem(true, "mapsData", mapId, "skipped");
					this.googleMapCfg.InitAndSetArrayItem(true, "mapsData", mapId, "dashMap");
					this.googleMapCfg.InitAndSetArrayItem(dElem.Value["heatMap"], "mapsData", mapId, "heatMap");
				}
				if(XVar.Pack(!(XVar)(this.googleMapCfg["mapsData"][mapId].KeyExists("markers"))))
				{
					this.googleMapCfg.InitAndSetArrayItem(XVar.Array(), "mapsData", mapId, "markers");
				}
				if((XVar)(markerData["lat"] == "")  || (XVar)(markerData["lng"] == ""))
				{
					continue;
				}
				this.googleMapCfg.InitAndSetArrayItem(markerData, "mapsData", mapId, "markers", null);
			}
			return null;
		}
		protected override XVar isInlineAreaToSet()
		{
			if(this.mode == Constants.LIST_DASHBOARD)
			{
				return true;
			}
			return base.isInlineAreaToSet();
		}
		public override XVar rulePRG()
		{
			return null;
		}
		public override XVar buildSearchPanel()
		{
			return null;
		}
		public override XVar printAvailable()
		{
			return false;
		}
		public override XVar getTabSQLComponents(dynamic _param_tab)
		{
			#region pass-by-value parameters
			dynamic tab = XVar.Clone(_param_tab);
			#endregion

			dynamic sql = null;
			this.skipMapFilter = new XVar(true);
			sql = XVar.Clone(base.getTabSQLComponents((XVar)(tab)));
			this.skipMapFilter = new XVar(false);
			return sql;
		}
	}
}
