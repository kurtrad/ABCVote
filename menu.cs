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
	public partial class GlobalController : BaseController
	{
		public ActionResult menu()
		{
			try
			{
				dynamic id = null, pageObject = null, redirect = null, var_params = XVar.Array();
				XTempl xt;
				Security.processLogoutRequest();
				if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
				{
					MVCFunctions.HeaderRedirect(new XVar("login"));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if((XVar)(XSession.Session["MyURL"] == "")  || (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())))
				{
					Security.saveRedirectURL();
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("menu2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["menu"] = XVar.Array();
t_layout.containers["menu"].Add(new XVar("name", "login_menu", "block", "loggedas_block", "substyle", 2  ));

t_layout.containers["menu"].Add(new XVar("name", "vmenu", "block", "menu_block", "substyle", 1  ));

t_layout.skins["menu"] = "1";

t_layout.blocks["top"].Add("menu");
GlobalVars.page_layouts["menu"] = t_layout;

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

				GlobalVars.tableEvents.InitAndSetArrayItem(new eventclass_dbo__ABCVotes(), "dbo._ABCVotes");
				GlobalVars.tableEvents.InitAndSetArrayItem(new eventclass_ABC_Voting_My_Voting(), "ABC_Voting_My_Voting");
				xt = XVar.UnPackXTempl(new XTempl());
				id = XVar.Clone((XVar.Pack(!XVar.Equals(XVar.Pack(MVCFunctions.postvalue(new XVar("id"))), XVar.Pack(""))) ? XVar.Pack(MVCFunctions.postvalue(new XVar("id"))) : XVar.Pack(1)));
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(id, "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(Constants.NOT_TABLE_BASED_TNAME, "tName");
				var_params.InitAndSetArrayItem(Constants.PAGE_MENU, "pageType");
				var_params.InitAndSetArrayItem("menu.cshtml", "templatefile");
				var_params.InitAndSetArrayItem(GlobalVars.isGroupSecurity, "isGroupSecurity");
				var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				GlobalVars.pageObject = XVar.Clone(new RunnerPage((XVar)(var_params)));
				GlobalVars.pageObject.init();
				GlobalVars.pageObject.commonAssign();
				if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("BeforeProcessMenu"))))
				{
					GlobalVars.globalEvents.BeforeProcessMenu((XVar)(GlobalVars.pageObject));
				}
				GlobalVars.pageObject.body["begin"] = MVCFunctions.Concat(GlobalVars.pageObject.body["begin"], CommonFunctions.GetBaseScriptsForPage(new XVar(false)));
				GlobalVars.pageObject.addCommonJs();
				GlobalVars.pageObject.fillSetCntrlMaps();
				GlobalVars.pageObject.setLangParams();
				xt.assign(new XVar("id"), (XVar)(id));
				xt.assign(new XVar("username"), (XVar)(XSession.Session["UserName"]));
				xt.assign(new XVar("changepwd_link"), (XVar)((XVar)(XSession.Session["AccessLevel"] != Constants.ACCESS_LEVEL_GUEST)  && (XVar)(XSession.Session["fromFacebook"] == false)));
				xt.assign(new XVar("changepwdlink_attrs"), (XVar)(MVCFunctions.Concat("onclick=\"window.location.href='", MVCFunctions.GetTableLink(new XVar("changepwd")), "';return false;\"")));
				xt.assign(new XVar("logoutlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"logoutButton", id, "\"")));
				xt.assign(new XVar("guestloginlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"loginButton", id, "\"")));
				xt.assign(new XVar("loggedas_block"), (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())));
				xt.assign(new XVar("loggedas_message"), (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest())));
				xt.assign(new XVar("logout_link"), new XVar(true));
				xt.assign(new XVar("guestloginbutton"), (XVar)(CommonFunctions.isLoggedAsGuest()));
				xt.assign(new XVar("logoutbutton"), (XVar)((XVar)(CommonFunctions.isSingleSign())  && (XVar)(!(XVar)(CommonFunctions.isLoggedAsGuest()))));
				redirect = XVar.Clone(GlobalVars.pageObject.getRedirectForMenuPage());
				if(XVar.Pack(redirect))
				{
					MVCFunctions.HeaderRedirect((XVar)(MVCFunctions.Concat("", redirect)));
					MVCFunctions.Echo(new XVar(""));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				xt.assign(new XVar("menu_block"), new XVar(true));
				if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("BeforeShowMenu"))))
				{
					new Func<XVar>(() => { var GlobalVars_pageObject_templatefile_byref = GlobalVars.pageObject.templatefile; var result = GlobalVars.globalEvents.BeforeShowMenu((XVar)(xt), ref GlobalVars_pageObject_templatefile_byref, (XVar)(GlobalVars.pageObject)); GlobalVars.pageObject.templatefile = GlobalVars_pageObject_templatefile_byref; return result; }).Invoke();
				}
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "<script>");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "window.controlsMap = ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pageObject.controlsHTMLMap)), ";");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "window.viewControlsMap = ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pageObject.viewControlsHTMLMap)), ";");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "Runner.applyPagesData( ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pagesData)), " );");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "window.settings = ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pageObject.jsSettings)), ";</script>");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "<script type=\"text/javascript\" src=\"", MVCFunctions.GetRootPathForResources(new XVar("include/runnerJS/RunnerAll.js")), "\"></script>");
				GlobalVars.pageObject.body["end"] = MVCFunctions.Concat(GlobalVars.pageObject.body["end"], "<script>", GlobalVars.pageObject.PrepareJS(), "</script>");
				xt.assignbyref(new XVar("body"), (XVar)(GlobalVars.pageObject.body));
				GlobalVars.pageObject.display((XVar)(GlobalVars.pageObject.templatefile));
				ViewBag.xt = xt;
				return View(xt.GetViewPath());
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
