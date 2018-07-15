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
		public ActionResult export()
		{
			try
			{
				dynamic pageObject = null, strtablename = null, var_params = XVar.Array();
				XTempl xt;
				ABC_Voting_My_Voting_Variables.Apply();
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				if(XVar.Pack(!(XVar)(Security.processPageSecurity((XVar)(strtablename), new XVar("P")))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("export2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["export"] = XVar.Array();
t_layout.containers["export"].Add(new XVar("name", "exportheader", "block", "", "substyle", 2  ));

t_layout.containers["export"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "range" ));
t_layout.containers["range"] = XVar.Array();
t_layout.containers["range"].Add(new XVar("name", "exprange", "block", "range_block", "substyle", 1  ));

t_layout.skins["range"] = "fields";


t_layout.containers["export"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "format" ));
t_layout.containers["format"] = XVar.Array();
t_layout.containers["format"].Add(new XVar("name", "expformat", "block", "exportformat", "substyle", 1  ));

t_layout.skins["format"] = "fields";


t_layout.containers["export"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "fields" ));
t_layout.containers["fields"] = XVar.Array();
t_layout.containers["fields"].Add(new XVar("name", "expoutput", "block", "", "substyle", 1  ));

t_layout.skins["fields"] = "fields";


t_layout.containers["export"].Add(new XVar("name", "expbuttons", "block", "", "substyle", 2  ));

t_layout.skins["export"] = "1";

t_layout.blocks["top"].Add("export");
GlobalVars.page_layouts["ABC_Voting_My_Voting_export"] = t_layout;

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
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(Constants.PAGE_EXPORT, "pageType");
				if((XVar)(!(XVar)(GlobalVars.eventObj.exists(new XVar("ListGetRowCount"))))  && (XVar)(!(XVar)(GlobalVars.eventObj.exists(new XVar("ListQuery")))))
				{
					var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				}
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("exportFields")), "selectedFields");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("type")), "exportType");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("a")), "action");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("records")), "records");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("selection")), "selection");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("delimiter")), "csvDelimiter");
				if(MVCFunctions.postvalue(new XVar("txtformatting")) == "raw")
				{
					var_params.InitAndSetArrayItem(true, "useRawValues");
				}
				var_params.InitAndSetArrayItem(ExportPage.readModeFromRequest(), "mode");
				GlobalVars.pageObject = XVar.Clone(new ExportPage((XVar)(var_params)));
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
