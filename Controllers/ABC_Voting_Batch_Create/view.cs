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
	public partial class ABC_Voting_Batch_CreateController : BaseController
	{
		public ActionResult view()
		{
			try
			{
				dynamic keys = XVar.Array(), pageMode = null, pageObject = null, var_params = XVar.Array();
				XTempl xt;
				ABC_Voting_Batch_Create_Variables.Apply();
				CommonFunctions.add_nocache_headers();
				if(XVar.Pack(!(XVar)(ViewPage.processEditPageSecurity((XVar)(GlobalVars.strTableName)))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("view2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["all"] = XVar.Array();
t_layout.containers["all"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "main" ));
t_layout.containers["main"] = XVar.Array();
t_layout.containers["main"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "view" ));
t_layout.containers["view"] = XVar.Array();
t_layout.containers["view"].Add(new XVar("name", "viewheader", "block", "viewheader", "substyle", 2  ));

t_layout.containers["view"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "fields" ));
t_layout.containers["fields"] = XVar.Array();
t_layout.containers["fields"].Add(new XVar("name", "viewfields", "block", "", "substyle", 1  ));

t_layout.containers["fields"].Add(new XVar("name", "viewbuttons", "block", "viewbuttons", "substyle", 2  ));

t_layout.skins["fields"] = "fields";


t_layout.skins["view"] = "1";


t_layout.skins["main"] = "empty";


t_layout.skins["all"] = "empty";

t_layout.blocks["top"].Add("all");
GlobalVars.page_layouts["ABC_Voting_Batch_Create_view"] = t_layout;

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

				pageMode = XVar.Clone(ViewPage.readViewModeFromRequest());
				xt = XVar.UnPackXTempl(new XTempl());
				keys = XVar.Clone(XVar.Array());
				keys.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("editid1")), "id");
				keys.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("editid2")), "record");
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("id")), "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(keys, "keys");
				var_params.InitAndSetArrayItem(pageMode, "mode");
				var_params.InitAndSetArrayItem(Constants.PAGE_VIEW, "pageType");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.postvalue(new XVar("mvcPDF"))), XVar.Pack("")), "pdfMode");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("mastertable")), "masterTable");
				if(pageMode == Constants.VIEW_DASHBOARD)
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashelement")), "dashElementName");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("table")), "dashTName");
					if(XVar.Pack(MVCFunctions.postvalue(new XVar("mapRefresh"))))
					{
						var_params.InitAndSetArrayItem(true, "mapRefresh");
						var_params.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("vpCoordinates")))), "vpCoordinates");
					}
				}
				if(pageMode == Constants.VIEW_POPUP)
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashelement")), "dashElementName");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashTName")), "dashTName");
				}
				if(XVar.Pack(var_params["masterTable"]))
				{
					var_params.InitAndSetArrayItem(ViewPage.processMasterKeys(), "masterKeysReq");
				}
				GlobalVars.pageObject = XVar.Clone(new ViewPage((XVar)(var_params)));
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
