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
	public partial class ABC_Voting_Submitted1Controller : BaseController
	{
		public ActionResult import()
		{
			try
			{
				dynamic id = null, pageObject = null, strtablename = null, var_params = XVar.Array();
				XTempl xt;
				ABC_Voting_Submitted1_Variables.Apply();
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				Server.ScriptTimeout = 600;
				if(XVar.Pack(!(XVar)(Security.processPageSecurity((XVar)(strtablename), new XVar("I")))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("import2"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["import"] = XVar.Array();
t_layout.containers["import"].Add(new XVar("name", "importheader", "block", "", "substyle", 2  ));

t_layout.containers["import"].Add(new XVar("name", "errormessage", "block", "", "substyle", 1  ));

t_layout.containers["import"].Add(new XVar("name", "wrapper", "block", "", "substyle", 1 , "container", "importfields" ));
t_layout.containers["importfields"] = XVar.Array();
t_layout.containers["importfields"].Add(new XVar("name", "importheader_text", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "importfields", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "import_rawtext_control", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "import_preview", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "import_process", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "import_results", "block", "", "substyle", 1  ));

t_layout.containers["importfields"].Add(new XVar("name", "importbuttons", "block", "", "substyle", 2  ));

t_layout.skins["importfields"] = "fields";


t_layout.skins["import"] = "1";

t_layout.blocks["top"].Add("import");
GlobalVars.page_layouts["ABC_Voting_Submitted1_import"] = t_layout;

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
				id = XVar.Clone((XVar.Pack(id != XVar.Pack("")) ? XVar.Pack(id) : XVar.Pack(1)));
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(id, "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("a")), "action");
				var_params.InitAndSetArrayItem(Constants.PAGE_IMPORT, "pageType");
				var_params.InitAndSetArrayItem(false, "needSearchClauseObj");
				var_params.InitAndSetArrayItem(GlobalVars.strOriginalTableName, "strOriginalTableName");
				if(var_params["action"] == "importPreview")
				{
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("importType")), "importType");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("importText")), "importText");
					var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("useXHR")), "useXHR");
				}
				else
				{
					if(var_params["action"] == "importData")
					{
						var_params.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("importData")))), "importData");
					}
				}
				GlobalVars.pageObject = XVar.Clone(new ImportPage((XVar)(var_params)));
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
