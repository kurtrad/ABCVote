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
	public partial class ListPage_DPList : ListPage_DPInline
	{
		protected static bool skipListPage_DPListCtor = false;
		public ListPage_DPList(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_DPListCtor)
			{
				skipListPage_DPListCtor = false;
				return;
			}
			this.formBricks.InitAndSetArrayItem(new XVar(0, "pagination_block"), "footer");
		}
		public override XVar showPage()
		{
			dynamic bricksExcept = null;
			BeforeShowList();
			if(XVar.Pack(mobileTemplateMode()))
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid_mobile", 1, "pagination", 2, "details_found"));
			}
			else
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination", 2, "message", 3, "reorder_records"));
			}
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			showPageAjax();
			return null;
		}
		public virtual XVar showPageAjax()
		{
			dynamic proceedLink = null, returnJSON = XVar.Array();
			returnJSON = XVar.Clone(XVar.Array());
			proceedLink = XVar.Clone(getProceedLink());
			if((XVar)((XVar)((XVar)((XVar)((XVar)(!(XVar)(this.numRowsFromSQL))  && (XVar)(!(XVar)(addAvailable())))  && (XVar)(!(XVar)(inlineAddAvailable())))  && (XVar)(!(XVar)(this.recordsDeleted)))  && (XVar)(proceedLink == XVar.Pack("")))  && (XVar)(getGridTabsCount() == 0))
			{
				returnJSON.InitAndSetArrayItem(false, "success");
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				return null;
			}
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(proceedLink, getHeaderControlsBlocks()), "headerCont");
			if(XVar.Pack(this.formBricks["footer"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["footer"]), new XVar(true)), "footerCont");
			}
			assignFormFooterAndHeaderBricks(new XVar(false));
			this.xt.prepareContainers();
			returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			returnJSON.InitAndSetArrayItem(this.flyId, "idStartFrom");
			returnJSON.InitAndSetArrayItem(true, "success");
			returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			returnJSON.InitAndSetArrayItem(grabAllCSSFiles(), "CSSFiles");
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			return null;
		}
		protected override XVar getBSButtonsClass()
		{
			return "btn btn-sm";
		}
		protected override XVar assignSessionPrefix()
		{
			this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.tName, "_preview"));
			return null;
		}
		public override XVar showNoRecordsMessage()
		{
			return null;
		}
	}
}
