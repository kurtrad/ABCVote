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
		public ActionResult login()
		{
			try
			{
				dynamic pageObject = null, var_params = XVar.Array();
				XTempl xt;
				CommonFunctions.add_nocache_headers();
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("login2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["login"] = XVar.Array();
t_layout.containers["login"].Add(new XVar("name", "loginheader", "block", "loginheader", "substyle", 2  ));

t_layout.containers["login"].Add(new XVar("name", "message", "block", "message_block", "substyle", 1  ));

t_layout.containers["login"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "fields" ));
t_layout.containers["fields"] = XVar.Array();
t_layout.containers["fields"].Add(new XVar("name", "loginfields", "block", "", "substyle", 1  ));

t_layout.containers["fields"].Add(new XVar("name", "loginbuttons", "block", "loginbuttons", "substyle", 2  ));

t_layout.skins["fields"] = "fields";


t_layout.skins["login"] = "1";

t_layout.blocks["top"].Add("login");
GlobalVars.page_layouts["login"] = t_layout;

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
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("id")), "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(Constants.PAGE_LOGIN, "pageType");
				var_params.InitAndSetArrayItem(Constants.NOT_TABLE_BASED_TNAME, "tName");
				var_params.InitAndSetArrayItem("login.cshtml", "templatefile");
				var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("value_captcha_1")), "captchaValue");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("notRedirect")), "notRedirect");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("remember_password")), "rememberPassword");
				var_params.InitAndSetArrayItem(LoginPage.readLoginModeFromRequest(), "mode");
				var_params.InitAndSetArrayItem(LoginPage.readActionFromRequest(), "action");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("message")), "message");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("username")), "var_pUsername");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("password")), "var_pPassword");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("return")), "redirectAfterLogin");
				GlobalVars.pageObject = XVar.Clone(new LoginPage((XVar)(var_params)));
				GlobalVars.pageObject.init();
				GlobalVars.pageObject.process();
				ViewBag.xt = xt;
				return View(xt.GetViewPath());
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
