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
		public ActionResult print()
		{
			try
			{
				dynamic id = null, pageObject = null, strtablename = null, var_params = XVar.Array();
				XTempl xt;
				ABC_Voting_Submitted1_Variables.Apply();
				CommonFunctions.add_nocache_headers();
				if(XVar.Pack(!(XVar)(Security.processPageSecurity((XVar)(strtablename), new XVar("P")))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("print"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["center"] = XVar.Array();
t_layout.containers["pageheader"] = XVar.Array();
t_layout.containers["pageheader"].Add(new XVar("name", "printheader", "block", "printheader", "substyle", 1  ));

t_layout.containers["pageheader"].Add(new XVar("name", "page_of_print", "block", "page_number", "substyle", 1  ));

t_layout.skins["pageheader"] = "empty";

t_layout.blocks["center"].Add("pageheader");
t_layout.containers["grid"] = XVar.Array();
t_layout.containers["grid"].Add(new XVar("name", "printgridnext", "block", "grid_block", "substyle", 1  ));

t_layout.skins["grid"] = "grid";

t_layout.blocks["center"].Add("grid");
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["pdf"] = XVar.Array();
t_layout.containers["pdf"].Add(new XVar("name", "printbuttons", "block", "printbuttons", "substyle", 1  ));

t_layout.skins["pdf"] = "empty";

t_layout.blocks["top"].Add("pdf");
t_layout.skins["master"] = "empty";

t_layout.blocks["top"].Add("master");
GlobalVars.page_layouts["ABC_Voting_Submitted1_print"] = t_layout;

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

t_layout = new TLayout(new XVar("print"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["center"] = XVar.Array();
t_layout.containers["pageheader"] = XVar.Array();
t_layout.containers["pageheader"].Add(new XVar("name", "printheader", "block", "printheader", "substyle", 1  ));

t_layout.containers["pageheader"].Add(new XVar("name", "page_of_print", "block", "page_number", "substyle", 1  ));

t_layout.skins["pageheader"] = "empty";

t_layout.blocks["center"].Add("pageheader");
t_layout.containers["grid"] = XVar.Array();
t_layout.containers["grid"].Add(new XVar("name", "printgridnext", "block", "grid_block", "substyle", 1  ));

t_layout.skins["grid"] = "grid";

t_layout.blocks["center"].Add("grid");
t_layout.blocks["top"] = XVar.Array();
t_layout.containers["pdf"] = XVar.Array();
t_layout.containers["pdf"].Add(new XVar("name", "printbuttons", "block", "printbuttons", "substyle", 1  ));

t_layout.skins["pdf"] = "empty";

t_layout.blocks["top"].Add("pdf");
t_layout.containers["master"] = XVar.Array();
t_layout.containers["master"].Add(new XVar("name", "masterinfoprint", "block", "mastertable_block", "substyle", 1  ));

t_layout.skins["master"] = "empty";

t_layout.blocks["top"].Add("master");
GlobalVars.page_layouts["dbo__ABCVotes_print"] = t_layout;

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
				var_params.InitAndSetArrayItem(Constants.PAGE_PRINT, "pageType");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("selection")), "selection");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("all")), "allPagesMode");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("pdf")), "pdfMode");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("htmlPdfContent")), "pdfContent");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("width")), "pdfWidth");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("details")), "detailTables");
				var_params.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("records")), "splitByRecords");
				GlobalVars.pageObject = XVar.Clone(new PrintPage((XVar)(var_params)));
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
