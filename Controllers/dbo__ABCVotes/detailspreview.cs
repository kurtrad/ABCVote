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
		public ActionResult detailspreview()
		{
			try
			{
				dynamic masterKeys = XVar.Array(), mastertable = null, mode = null, pageObject = null, query = null, recordsCounter = null, returnJSON = XVar.Array(), rowcount = null, sessionPrefix = null, var_params = XVar.Array(), whereClauses = XVar.Array();
				ProjectSettings pSet;
				XTempl xt;
				dbo__ABCVotes_Variables.Apply();
				MVCFunctions.Header("Expires", "Thu, 01 Jan 1970 00:00:01 GMT");
				mode = XVar.Clone(MVCFunctions.postvalue(new XVar("mode")));
				if(XVar.Pack(!(XVar)(CommonFunctions.isLogged())))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				if(XVar.Pack(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search")))))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName)));
				xt = XVar.UnPackXTempl(new XTempl());
{

TLayout t_layout = null;

t_layout = new TLayout(new XVar("detailspreview"), new XVar("AvenueAvenue"), new XVar("MobileAvenue"));
t_layout.version = 2;
t_layout.blocks["bare"] = XVar.Array();
t_layout.containers["dcount"] = XVar.Array();
t_layout.containers["dcount"].Add(new XVar("name", "detailspreviewheader", "block", "", "substyle", 1  ));

t_layout.containers["dcount"].Add(new XVar("name", "detailspreviewdetailsfount", "block", "", "substyle", 1  ));

t_layout.containers["dcount"].Add(new XVar("name", "detailspreviewdispfirst", "block", "display_first", "substyle", 1  ));

t_layout.skins["dcount"] = "empty";

t_layout.blocks["bare"].Add("dcount");
t_layout.containers["detailspreviewgrid"] = XVar.Array();
t_layout.containers["detailspreviewgrid"].Add(new XVar("name", "detailspreviewfields", "block", "details_data", "substyle", 1  ));

t_layout.skins["detailspreviewgrid"] = "grid";

t_layout.blocks["bare"].Add("detailspreviewgrid");
GlobalVars.page_layouts["dbo__ABCVotes_detailspreview"] = t_layout;

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

				recordsCounter = new XVar(0);
				mastertable = XVar.Clone(MVCFunctions.postvalue(new XVar("mastertable")));
				masterKeys = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("masterKeys")))));
				sessionPrefix = new XVar("_detailsPreview");
				if(mastertable != XVar.Pack(""))
				{
					dynamic i = null;
					XSession.Session[MVCFunctions.Concat(sessionPrefix, "_mastertable")] = mastertable;
					i = new XVar(1);
					if((XVar)(MVCFunctions.is_array((XVar)(masterKeys)))  && (XVar)(0 < MVCFunctions.count(masterKeys)))
					{
						while(XVar.Pack(masterKeys.KeyExists(MVCFunctions.Concat("masterkey", i))))
						{
							XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey", i)] = masterKeys[MVCFunctions.Concat("masterkey", i)];
							i++;
						}
					}
					if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(sessionPrefix, "_masterkey", i))))
					{
						XSession.Session.Remove(MVCFunctions.Concat(sessionPrefix, "_masterkey", i));
					}
				}
				else
				{
					mastertable = XVar.Clone(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_mastertable")]);
				}
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(1, "id");
				var_params.InitAndSetArrayItem(xt, "xt");
				var_params.InitAndSetArrayItem(GlobalVars.strTableName, "tName");
				var_params.InitAndSetArrayItem("detailspreview", "pageType");
				GlobalVars.pageObject = XVar.Clone(new DetailsPreview((XVar)(var_params)));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), new XVar(Constants.PAGE_LIST)));
				whereClauses = XVar.Clone(XVar.Array());
				if(mastertable == "dbo._ABCReports")
				{
					dynamic formattedValue = null;
					formattedValue = XVar.Clone(CommonFunctions.make_db_value(new XVar("record"), (XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey1")])));
					if(formattedValue == "null")
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), " is null"), null);
					}
					else
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), "=", formattedValue), null);
					}
				}
				if(mastertable == "ABC_Voting_Submitted")
				{
					dynamic formattedValue = null;
					formattedValue = XVar.Clone(CommonFunctions.make_db_value(new XVar("record"), (XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey1")])));
					if(formattedValue == "null")
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), " is null"), null);
					}
					else
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), "=", formattedValue), null);
					}
				}
				if(mastertable == "ABC_Voting_Recirculated")
				{
					dynamic formattedValue = null;
					formattedValue = XVar.Clone(CommonFunctions.make_db_value(new XVar("record"), (XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey1")])));
					if(formattedValue == "null")
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), " is null"), null);
					}
					else
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), "=", formattedValue), null);
					}
				}
				if(mastertable == "ABC_Voting_My_Voting")
				{
					dynamic formattedValue = null;
					formattedValue = XVar.Clone(CommonFunctions.make_db_value(new XVar("record"), (XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey1")])));
					if(formattedValue == "null")
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), " is null"), null);
					}
					else
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), "=", formattedValue), null);
					}
				}
				if(mastertable == "dbo.vwABCReportsVoteCount")
				{
					dynamic formattedValue = null;
					formattedValue = XVar.Clone(CommonFunctions.make_db_value(new XVar("record"), (XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_masterkey1")])));
					if(formattedValue == "null")
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), " is null"), null);
					}
					else
					{
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(GlobalVars.pageObject.getFieldSQLDecrypt(new XVar("record")), "=", formattedValue), null);
					}
				}
				whereClauses.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(GlobalVars.strTableName)), null);
				query = XVar.Clone(pSet.getSQLQuery());
				GlobalVars.strSQL = XVar.Clone(query.buildSQL_default((XVar)(whereClauses)));
				rowcount = XVar.Clone(GlobalVars.pageObject.connection.getFetchedRowsNumber((XVar)(GlobalVars.strSQL)));
				GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, GlobalVars.pageObject.getOrderByClause());
				xt.assign(new XVar("row_count"), (XVar)(rowcount));
				if(XVar.Pack(rowcount))
				{
					dynamic b = null, data = XVar.Array(), display_count = null, format = null, keylink = null, qResult = null, row = XVar.Array(), rowinfo = XVar.Array(), rowinfo2 = XVar.Array(), value = null, var_class = null, viewContainer = null;
					xt.assign(new XVar("details_data"), new XVar(true));

					display_count = new XVar(10);
					if(mode == "inline")
					{
						display_count *= 2;
					}
					if(display_count + 2 < rowcount)
					{
						xt.assign(new XVar("display_first"), new XVar(true));
						xt.assign(new XVar("display_count"), (XVar)(display_count));
					}
					else
					{
						display_count = XVar.Clone(rowcount);
					}
					rowinfo = XVar.Clone(XVar.Array());
					viewContainer = XVar.Clone(new ViewControlsContainer((XVar)(pSet), new XVar(Constants.PAGE_LIST)));
					viewContainer.isDetailsPreview = new XVar(true);
					b = new XVar(true);
					qResult = XVar.Clone(GlobalVars.pageObject.connection.query((XVar)(GlobalVars.strSQL)));
					data = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
					while((XVar)(data)  && (XVar)(recordsCounter < display_count))
					{
						recordsCounter++;
						row = XVar.Clone(XVar.Array());
						keylink = new XVar("");
						keylink = MVCFunctions.Concat(keylink, "&key1=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data["id"])))));
						keylink = MVCFunctions.Concat(keylink, "&key2=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data["record"])))));

						viewContainer.recId = XVar.Clone(recordsCounter);
						value = XVar.Clone(viewContainer.showDBValue(new XVar("committee_member"), (XVar)(data), (XVar)(keylink)));
						row.InitAndSetArrayItem(value, "committee_member_value");
						format = XVar.Clone(pSet.getViewFormat(new XVar("committee_member")));
						var_class = new XVar("rnr-field-text");
						if(format == Constants.FORMAT_FILE)
						{
							var_class = new XVar(" rnr-field-file");
						}
						if(format == Constants.FORMAT_AUDIO)
						{
							var_class = new XVar(" rnr-field-audio");
						}
						if(format == Constants.FORMAT_CHECKBOX)
						{
							var_class = new XVar(" rnr-field-checkbox");
						}
						if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(pSet.getFieldType(new XVar("committee_member"))))))
						{
							var_class = new XVar(" rnr-field-number");
						}
						row.InitAndSetArrayItem(var_class, "committee_member_class");
						viewContainer.recId = XVar.Clone(recordsCounter);
						value = XVar.Clone(viewContainer.showDBValue(new XVar("date_voted"), (XVar)(data), (XVar)(keylink)));
						row.InitAndSetArrayItem(value, "date_voted_value");
						format = XVar.Clone(pSet.getViewFormat(new XVar("date_voted")));
						var_class = new XVar("rnr-field-text");
						if(format == Constants.FORMAT_FILE)
						{
							var_class = new XVar(" rnr-field-file");
						}
						if(format == Constants.FORMAT_AUDIO)
						{
							var_class = new XVar(" rnr-field-audio");
						}
						if(format == Constants.FORMAT_CHECKBOX)
						{
							var_class = new XVar(" rnr-field-checkbox");
						}
						if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(pSet.getFieldType(new XVar("date_voted"))))))
						{
							var_class = new XVar(" rnr-field-number");
						}
						row.InitAndSetArrayItem(var_class, "date_voted_class");
						viewContainer.recId = XVar.Clone(recordsCounter);
						value = XVar.Clone(viewContainer.showDBValue(new XVar("vote"), (XVar)(data), (XVar)(keylink)));
						row.InitAndSetArrayItem(value, "vote_value");
						format = XVar.Clone(pSet.getViewFormat(new XVar("vote")));
						var_class = new XVar("rnr-field-text");
						if(format == Constants.FORMAT_FILE)
						{
							var_class = new XVar(" rnr-field-file");
						}
						if(format == Constants.FORMAT_AUDIO)
						{
							var_class = new XVar(" rnr-field-audio");
						}
						if(format == Constants.FORMAT_CHECKBOX)
						{
							var_class = new XVar(" rnr-field-checkbox");
						}
						if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(pSet.getFieldType(new XVar("vote"))))))
						{
							var_class = new XVar(" rnr-field-number");
						}
						row.InitAndSetArrayItem(var_class, "vote_class");
						viewContainer.recId = XVar.Clone(recordsCounter);
						value = XVar.Clone(viewContainer.showDBValue(new XVar("comment"), (XVar)(data), (XVar)(keylink)));
						row.InitAndSetArrayItem(value, "comment_value");
						format = XVar.Clone(pSet.getViewFormat(new XVar("comment")));
						var_class = new XVar("rnr-field-text");
						if(format == Constants.FORMAT_FILE)
						{
							var_class = new XVar(" rnr-field-file");
						}
						if(format == Constants.FORMAT_AUDIO)
						{
							var_class = new XVar(" rnr-field-audio");
						}
						if(format == Constants.FORMAT_CHECKBOX)
						{
							var_class = new XVar(" rnr-field-checkbox");
						}
						if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(pSet.getFieldType(new XVar("comment"))))))
						{
							var_class = new XVar(" rnr-field-number");
						}
						row.InitAndSetArrayItem(var_class, "comment_class");
						rowinfo.InitAndSetArrayItem(row, null);
						if(XVar.Pack(b))
						{
							rowinfo2.InitAndSetArrayItem(row, null);
							b = new XVar(false);
						}
						data = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())));
					}
					xt.assign_loopsection(new XVar("details_row"), (XVar)(rowinfo));
					xt.assign_loopsection(new XVar("details_row_header"), (XVar)(rowinfo2));
				}
				returnJSON = XVar.Clone(new XVar("success", true));
				xt.load_template((XVar)(MVCFunctions.GetTemplateName(new XVar("dbo__ABCVotes"), new XVar("detailspreview"))));
				returnJSON.InitAndSetArrayItem(xt.fetch_loaded(), "body");
				if(mode != "inline")
				{
					dynamic layout = null;
					returnJSON.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("counter")), "counter");
					layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(MVCFunctions.GoodFieldName((XVar)(GlobalVars.strTableName))), new XVar("detailspreview")));
					if(XVar.Pack(layout))
					{
						foreach (KeyValuePair<XVar, dynamic> css in layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)((XVar)(CommonFunctions.mobileDeviceDetected())  && (XVar)(layout.version != Constants.BOOTSTRAP_LAYOUT))).GetEnumerator())
						{
							returnJSON.InitAndSetArrayItem(css.Value, "CSSFiles", null);
						}
					}
				}
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				MVCFunctions.Echo(new XVar(""));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
