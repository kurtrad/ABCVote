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
	public partial class dbo__ABCVotesController : BaseController
	{
		public ActionResult search()
		{
			try
			{
				dynamic accessGranted = null, chrt_array = XVar.Array(), cname = null, id = null, layoutVersion = null, mode = null, pageObject = null, rname = null, templatefile = null, var_params = XVar.Array();
				XTempl xt;
				dbo__ABCVotes_Variables.Apply();
				CommonFunctions.add_nocache_headers();
				Security.processLogoutRequest();
				if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
				{
					Security.saveRedirectURL();
					CommonFunctions.redirectToLogin();
				}
				cname = XVar.Clone(MVCFunctions.postvalue(new XVar("cname")));
				rname = XVar.Clone(MVCFunctions.postvalue(new XVar("rname")));
				accessGranted = XVar.Clone(CommonFunctions.CheckTablePermissions((XVar)(GlobalVars.strTableName), new XVar("S")));
				if(XVar.Pack(!(XVar)(accessGranted)))
				{
					MVCFunctions.HeaderRedirect(new XVar("menu"));
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("search2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["search"] = XVar.Array();
t_layout.containers["search"].Add(new XVar("name", "srchheader", "block", "searchheader", "substyle", 2  ));

t_layout.containers["search"].Add(new XVar("name", "srchconditions", "block", "conditions_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "fields" ));
t_layout.containers["fields"] = XVar.Array();
t_layout.containers["fields"].Add(new XVar("name", "srchfields", "block", "", "substyle", 1  ));

t_layout.containers["fields"].Add(new XVar("name", "srchbuttons", "block", "searchbuttons", "substyle", 2  ));

t_layout.skins["fields"] = "fields";


t_layout.skins["search"] = "1";

t_layout.blocks["top"].Add("search");
GlobalVars.page_layouts["dbo__ABCVotes_search"] = t_layout;

t_layout.skinsparams = XVar.Array();
t_layout.skinsparams["empty"] = XVar.Array();
t_layout.skinsparams["empty"]["button"] = "button2";
t_layout.skinsparams["menu"] = XVar.Array();
t_layout.skinsparams["menu"]["button"] = "button1";
t_layout.skinsparams["hmenu"] = XVar.Array();
t_layout.skinsparams["hmenu"]["button"] = "button1";
t_layout.skinsparams["undermenu"] = XVar.Array();
t_layout.skinsparams["undermenu"]["button"] = "button1";
t_layout.skinsparams["fields"] = XVar.Array();
t_layout.skinsparams["fields"]["button"] = "button1";
t_layout.skinsparams["form"] = XVar.Array();
t_layout.skinsparams["form"]["button"] = "button1";
t_layout.skinsparams["1"] = XVar.Array();
t_layout.skinsparams["1"]["button"] = "button1";
t_layout.skinsparams["2"] = XVar.Array();
t_layout.skinsparams["2"]["button"] = "button1";
t_layout.skinsparams["3"] = XVar.Array();
t_layout.skinsparams["3"]["button"] = "button1";

}

				xt = XVar.UnPackXTempl(new XTempl());
				id = XVar.Clone(MVCFunctions.postvalue(new XVar("id")));
				id = XVar.Clone((XVar.Pack(id) ? XVar.Pack(id) : XVar.Pack(1)));
				mode = new XVar(Constants.SEARCH_SIMPLE);
				if(MVCFunctions.postvalue(new XVar("mode")) == "dashsearch")
				{
					mode = new XVar(Constants.SEARCH_DASHBOARD);
				}
				else
				{
					if(MVCFunctions.postvalue(new XVar("mode")) == "inlineLoadCtrl")
					{
						mode = new XVar(Constants.SEARCH_LOAD_CONTROL);
						layoutVersion = XVar.Clone(MVCFunctions.postvalue(new XVar("layoutVersion")));
					}
				}
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(id, "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(mode, "mode");
				var_params.InitAndSetArrayItem(cname, "chartName");
				var_params.InitAndSetArrayItem(rname, "reportName");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(Constants.PAGE_SEARCH, "pageType");
				var_params.InitAndSetArrayItem(templatefile, "templatefile");
				var_params.InitAndSetArrayItem("dbo__ABCVotes", "shortTableName");
				var_params.InitAndSetArrayItem(layoutVersion, "layoutVersion");
				var_params.InitAndSetArrayItem((XVar.Pack(MVCFunctions.postvalue(new XVar("searchControllerId"))) ? XVar.Pack(MVCFunctions.postvalue(new XVar("searchControllerId"))) : XVar.Pack(id)), "searchControllerId");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("ctrlField")), "ctrlField");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("isNeedSettings")), "needSettings");
				if(mode == Constants.SEARCH_DASHBOARD)
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("table")), "dashTName");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashelement")), "dashElementName");
				}
				var_params.InitAndSetArrayItem(SearchPage.getExtraPageParams(), "extraPageParams");
				GlobalVars.pageObject = XVar.Clone(new SearchPage((XVar)(var_params)));
				if(mode == Constants.SEARCH_LOAD_CONTROL)
				{
					GlobalVars.pageObject.displaySearchControl();
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				GlobalVars.pageObject.init();
				GlobalVars.pageObject.process();
				if(mode == Constants.SEARCH_DASHBOARD)
				{
					MVCFunctions.Echo(new XVar(""));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				ViewBag.xt = xt;
				return View(xt.GetViewPath());
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
