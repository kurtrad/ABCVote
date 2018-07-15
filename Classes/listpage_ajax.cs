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
	public partial class ListPage_Ajax : ListPage_Simple
	{
		protected static bool skipListPage_AjaxCtor = false;
		private bool skipListPage_SimpleCtorSurrogate = new Func<bool>(() => skipListPage_SimpleCtor = true).Invoke();
		public ListPage_Ajax(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipListPage_AjaxCtor)
			{
				skipListPage_AjaxCtor = false;
				return;
			}
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			this.xt.assign(new XVar("filterPanelStateClass"), new XVar("filter-ajaxReloaded"));
			return null;
		}
		public override XVar addCommonHtml()
		{
			return true;
		}
		public override XVar addCommonJs()
		{
			addJsForGrid();
			return null;
		}
		public override XVar showPage()
		{
			dynamic bricksExcept = XVar.Array(), returnJSON = XVar.Array();
			BeforeShowList();
			bricksExcept = XVar.Clone(new XVar(0, "details_found", 1, "page_of", 2, "recsperpage", 3, "vrecsperpage", 4, "vdetails_found", 5, "vpage_of", 6, "grid"));
			bricksExcept.InitAndSetArrayItem("pagination", null);
			bricksExcept.InitAndSetArrayItem("reorder_records", null);
			bricksExcept.InitAndSetArrayItem("search_saving_buttons", null);
			bricksExcept.InitAndSetArrayItem("filterpanel", null);
			bricksExcept.InitAndSetArrayItem("message", null);
			bricksExcept.InitAndSetArrayItem("recordcontrol", null);
			bricksExcept.InitAndSetArrayItem("bsgrid_tabs", null);
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				bricksExcept.InitAndSetArrayItem("morebutton", null);
			}
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			returnJSON = XVar.Clone(new XVar("success", true, "idStartFrom", this.flyId));
			addControlsJSAndCSS();
			fillSetCntrlMaps();
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			returnJSON.InitAndSetArrayItem(this.viewControlsMap, "viewControlsMap");
			returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			this.xt.assign(new XVar("header"), new XVar(""));
			this.xt.assign(new XVar("footer"), new XVar(""));
			returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			returnJSON.InitAndSetArrayItem(MVCFunctions.Concat(this.row_css_rules, this.cell_css_rules, "\n", this.mobile_css_rules), "cellStyles");
			returnJSON.InitAndSetArrayItem(this.numRowsFromSQL, "numberOfRecs");
			returnJSON.InitAndSetArrayItem(this.pageSize, "recPerPage");
			if(this.deleteMessage != "")
			{
				returnJSON.InitAndSetArrayItem(true, "usermessage");
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public override XVar rulePRG()
		{
			return null;
		}
	}
}
