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
	public partial class ABC_Voting_My_VotingController : BaseController
	{
		public ActionResult add()
		{
			try
			{
				dynamic id = null, pageMode = null, pageObject = null, var_params = XVar.Array();
				XTempl xt;
				ABC_Voting_My_Voting_Variables.Apply();
				CommonFunctions.add_nocache_headers();
				CommonFunctions.InitLookupLinks();
				if(XVar.Pack(!(XVar)(AddPage.processAddPageSecurity((XVar)(GlobalVars.strTableName)))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				AddPage.handleBrokenRequest();
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("add2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["all"] = XVar.Array();
t_layout.containers["all"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "add" ));
t_layout.containers["add"] = XVar.Array();
t_layout.containers["add"].Add(new XVar("name", "addheader", "block", "addheader", "substyle", 2  ));

t_layout.containers["add"].Add(new XVar("name", "message", "block", "message_block", "substyle", 1  ));

t_layout.containers["add"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "fields" ));
t_layout.containers["fields"] = XVar.Array();
t_layout.containers["fields"].Add(new XVar("name", "addfields", "block", "", "substyle", 1  ));

t_layout.containers["fields"].Add(new XVar("name", "addbuttons", "block", "addbuttons", "substyle", 2  ));

t_layout.skins["fields"] = "fields";


t_layout.skins["add"] = "1";


t_layout.skins["all"] = "empty";

t_layout.blocks["top"].Add("all");
GlobalVars.page_layouts["ABC_Voting_My_Voting_add"] = t_layout;

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

{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("list6"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["center"] = XVar.Array();
t_layout.containers["message"] = XVar.Array();
t_layout.containers["message"].Add(new XVar("name", "message", "block", "message_block", "substyle", 1  ));

t_layout.skins["message"] = "2";

t_layout.blocks["center"].Add("message");
t_layout.containers["grid"] = XVar.Array();
t_layout.containers["grid"].Add(new XVar("name", "grid", "block", "grid_block", "substyle", 1  ));

t_layout.skins["grid"] = "grid";

t_layout.blocks["center"].Add("grid");
t_layout.containers["pagination"] = XVar.Array();
t_layout.containers["pagination"].Add(new XVar("name", "pagination", "block", "pagination_block", "substyle", 1  ));

t_layout.skins["pagination"] = "2";

t_layout.blocks["center"].Add("pagination");
t_layout.blocks["left"] = XVar.Array();
t_layout.containers["left"] = XVar.Array();
t_layout.containers["left"].Add(new XVar("name", "searchpanel", "block", "searchPanel", "substyle", 1  ));

t_layout.skins["left"] = "menu";

t_layout.blocks["left"].Add("left");
t_layout.blocks["top"] = XVar.Array();
t_layout.skins["master"] = "empty";

t_layout.blocks["top"].Add("master");
t_layout.containers["toplinks"] = XVar.Array();
t_layout.containers["toplinks"].Add(new XVar("name", "loggedas", "block", "security_block", "substyle", 1  ));

t_layout.containers["toplinks"].Add(new XVar("name", "toplinks_advsearch", "block", "asearch_link", "substyle", 1  ));

t_layout.containers["toplinks"].Add(new XVar("name", "toplinks_import", "block", "import_link", "substyle", 1  ));

t_layout.containers["toplinks"].Add(new XVar("name", "toplinks_export", "block", "export_link", "substyle", 1  ));

t_layout.containers["toplinks"].Add(new XVar("name", "printpanel", "block", "print_friendly", "substyle", 1  ));

t_layout.skins["toplinks"] = "2";

t_layout.blocks["top"].Add("toplinks");
t_layout.containers["hmenu"] = XVar.Array();
t_layout.containers["hmenu"].Add(new XVar("name", "hmenu", "block", "menu_block", "substyle", 1  ));

t_layout.skins["hmenu"] = "hmenu";

t_layout.blocks["top"].Add("hmenu");
t_layout.containers["search"] = XVar.Array();
t_layout.containers["search"].Add(new XVar("name", "search", "block", "searchform_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "search_buttons", "block", "searchformbuttons_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "search_saving_buttons", "block", "searchsaving_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "details_found", "block", "details_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "page_of", "block", "pages_block", "substyle", 1  ));

t_layout.containers["search"].Add(new XVar("name", "recsperpage", "block", "recordspp_block", "substyle", 1  ));

t_layout.skins["search"] = "1";

t_layout.blocks["top"].Add("search");
t_layout.containers["recordcontrols"] = XVar.Array();
t_layout.containers["recordcontrols"].Add(new XVar("name", "recordcontrols_new", "block", "newrecord_controls_block", "substyle", 1  ));

t_layout.containers["recordcontrols"].Add(new XVar("name", "recordcontrol", "block", "record_controls_block", "substyle", 1  ));

t_layout.skins["recordcontrols"] = "2";

t_layout.blocks["top"].Add("recordcontrols");
GlobalVars.page_layouts["ABC_Voting_My_Voting_list"] = t_layout;

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


				pageMode = XVar.Clone(AddPage.readAddModeFromRequest());
				xt = XVar.UnPackXTempl(new XTempl());
				id = XVar.Clone(MVCFunctions.postvalue(new XVar("id")));
				id = XVar.Clone((XVar.Pack(MVCFunctions.intval((XVar)(id)) == 0) ? XVar.Pack(1) : XVar.Pack(id)));
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(id, "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(pageMode, "mode");
				var_params.InitAndSetArrayItem(Constants.PAGE_ADD, "pageType");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("a")), "action");
				var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("afteradd")), "afterAdd_id");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("mastertable")), "masterTable");
				if(XVar.Pack(var_params["masterTable"]))
				{
					dynamic i = null;
					i = new XVar(1);
					var_params.InitAndSetArrayItem(XVar.Array(), "masterKeysReq");
					while(XVar.Pack(MVCFunctions.REQUESTKeyExists(MVCFunctions.Concat("masterkey", i))))
					{
						var_params.InitAndSetArrayItem(MVCFunctions.postvalue(MVCFunctions.Concat("masterkey", i)), "masterKeysReq", i);
						i++;
					}
				}
						
	
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("value_captcha_", id))), "captchaValue");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashelement")), "dashElementName");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("fromDashboard")), "fromDashboard");
				var_params.InitAndSetArrayItem((XVar.Pack(var_params["fromDashboard"]) ? XVar.Pack(var_params["fromDashboard"]) : XVar.Pack(MVCFunctions.postvalue(new XVar("dashTName")))), "dashTName");
				if(pageMode == Constants.ADD_INLINE)
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("forLookup")), "forListPageLookup");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("screenWidth")), "screenWidth");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("screenHeight")), "screenHeight");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("orientation")), "orientation");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("masterpagetype")), "masterPageType");
				}
				if((XVar)(pageMode == Constants.ADD_ONTHEFLY)  || (XVar)((XVar)(pageMode == Constants.ADD_INLINE)  && (XVar)(MVCFunctions.postvalue(new XVar("forLookup")))))
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("table")), "lookupTable");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("field")), "lookupField");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("pageType")), "lookupPageType");
					if(XVar.Pack(MVCFunctions.postvalue(new XVar("parentsExist"))))
					{
						var_params.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("parentCtrlsData")))), "parentCtrlsData");
					}
				}
				GlobalVars.pageObject = XVar.Clone(new AddPage((XVar)(var_params)));
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