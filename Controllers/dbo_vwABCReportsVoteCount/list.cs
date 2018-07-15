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
	public partial class dbo_vwABCReportsVoteCountController : BaseController
	{
		public ActionResult list()
		{
			try
			{
				dynamic i = null, mode = null, options = XVar.Array(), pageObject = null;
				XTempl xt;
				dbo_vwABCReportsVoteCount_Variables.Apply();
				CommonFunctions.add_nocache_headers();
				CommonFunctions.InitLookupLinks();
				if(XVar.Pack(!(XVar)(ListPage.processListPageSecurity((XVar)(GlobalVars.strTableName)))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(ListPage.processSaveParams((XVar)(GlobalVars.strTableName))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
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
t_layout.containers["recordcontrols"].Add(new XVar("name", "recordcontrol", "block", "record_controls_block", "substyle", 1  ));

t_layout.skins["recordcontrols"] = "2";

t_layout.blocks["top"].Add("recordcontrols");
GlobalVars.page_layouts["dbo_vwABCReportsVoteCount_list"] = t_layout;

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

				options = XVar.Clone(XVar.Array());
				mode = XVar.Clone(ListPage.readListModeFromRequest());
				if(mode == Constants.LIST_SIMPLE)
				{
				}
				else
				{
					if(mode == Constants.LIST_AJAX)
					{
					}
					else
					{
						if(mode == Constants.LIST_LOOKUP)
						{
							options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("table")), "mainTable");
							options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("field")), "mainField");
							options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("pageType")), "mainPageType");
							options.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("data")))), "mainRecordData");
							options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("mainRecordMasterTable")), "mainRecordMasterTable");
							if(XVar.Pack(MVCFunctions.postvalue(new XVar("parentsExist"))))
							{
								options.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("parentCtrlsData")))), "parentCtrlsData");
							}
						}
						else
						{
							if(mode == Constants.LIST_DETAILS)
							{
							}
							else
							{
								if(mode == Constants.LIST_DASHDETAILS)
								{
								}
								else
								{
									if(mode == Constants.LIST_DASHBOARD)
									{
									}
									else
									{
										if(mode == Constants.MAP_DASHBOARD)
										{
										}
									}
								}
							}
						}
					}
				}
				xt = XVar.UnPackXTempl(new XTempl((XVar)(mode != Constants.LIST_SIMPLE)));
				options.InitAndSetArrayItem(Constants.PAGE_LIST, "pageType");
				options.InitAndSetArrayItem((XVar.Pack(MVCFunctions.postvalue(new XVar("id"))) ? XVar.Pack(MVCFunctions.postvalue(new XVar("id"))) : XVar.Pack(1)), "id");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("recordId")) + 0, "flyId");
				options.InitAndSetArrayItem(mode, "mode");
				options.InitAndSetArrayItem(xt, "xt");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("masterpagetype")), "masterPageType");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("mastertable")), "masterTable");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("masterid")), "masterId");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("firsttime")), "firstTime");
				options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("sortby")), "sortBy");
				if((XVar)((XVar)(mode == Constants.LIST_DASHBOARD)  && (XVar)(MVCFunctions.postvalue(new XVar("nodata"))))  && (XVar)(MVCFunctions.strlen((XVar)(options["masterTable"]))))
				{
					options.InitAndSetArrayItem(true, "showNoData");
				}
				if(mode != Constants.LIST_LOOKUP)
				{
					options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("dashelement")), "dashElementName");
					options.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("table")), "dashTName");
				}
				if(XVar.Pack(MVCFunctions.postvalue(new XVar("mapRefresh"))))
				{
					options.InitAndSetArrayItem(true, "mapRefresh");
					options.InitAndSetArrayItem(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("vpCoordinates")))), "vpCoordinates");
				}
				i = new XVar(1);
				while(XVar.Pack(MVCFunctions.REQUESTKeyExists(MVCFunctions.Concat("masterkey", i))))
				{
					if(i == 1)
					{
						options.InitAndSetArrayItem(XVar.Array(), "masterKeysReq");
					}
					options.InitAndSetArrayItem(MVCFunctions.postvalue(MVCFunctions.Concat("masterkey", i)), "masterKeysReq", i);
					i++;
				}
				GlobalVars.pageObject = XVar.Clone(ListPage.createListPage((XVar)(GlobalVars.strTableName), (XVar)(options)));
				if(XVar.Pack(GlobalVars.pageObject.processSaveSearch()))
				{
					MVCFunctions.Echo(new XVar(""));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				GlobalVars.gQuery.ReplaceFieldsWithDummies((XVar)(GlobalVars.pageObject.getNotListBlobFieldsIndices()));
				if((XVar)((XVar)(mode != Constants.LIST_DETAILS)  && (XVar)(mode != Constants.MAP_DASHBOARD))  && (XVar)(mode != Constants.LIST_DASHBOARD))
				{
					dynamic mapSettings = XVar.Array();
				}
				XSession.Session.Remove("message_add");
				XSession.Session.Remove("message_edit");
				GlobalVars.pageObject.prepareForBuildPage();
				GlobalVars.pageObject.showPage();
				if(mode != Constants.LIST_SIMPLE)
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
