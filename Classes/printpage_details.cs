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
	public partial class PrintPage_Details : PrintPage
	{
		public dynamic printMasterTable = XVar.Array();
		public dynamic printMasterKeys = XVar.Array();
		public dynamic multipleDetails = XVar.Pack(false);
		protected static bool skipPrintPage_DetailsCtor = false;
		public PrintPage_Details(dynamic var_params = null)
			:base((XVar)var_params)
		{
			if(skipPrintPage_DetailsCtor)
			{
				skipPrintPage_DetailsCtor = false;
				return;
			}
			#region default values
			if(var_params as Object == null) var_params = new XVar("");
			#endregion

		}
		public override XVar process()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessPrint"))))
			{
				this.eventsObject.BeforeProcessPrint(this);
			}
			commonAssign();
			setMapParams();
			this.splitByRecords = new XVar(0);
			this.allPagesMode = new XVar(true);
			buildSQL();
			calcRowCount();
			openQuery();
			fillGridPage();
			showTotals();
			doCommonAssignments();
			addCustomCss();
			displayPrintPage();
			return null;
		}
		public override XVar displayPrintPage()
		{
			if(XVar.Pack(!(XVar)(this.fetchedRecordCount)))
			{
				return null;
			}
			this.xt.bulk_assign((XVar)(this.pageBody));
			this.xt.hideAllBricksExcept((XVar)(new XVar(0, "grid")));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.assign(new XVar("printheader"), (XVar)(this.multipleDetails));
			this.xt.load_template((XVar)(this.templatefile));
			MVCFunctions.Echo("<div class='rnr-print-details'>");
			if(XVar.Pack(this.multipleDetails))
			{
				MVCFunctions.Echo("<div class='rnr-pd-title'>");
				MVCFunctions.Echo(getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))));
				MVCFunctions.Echo("</div>");
			}
			MVCFunctions.Echo("<div class='rnr-pd-grid'>");
			MVCFunctions.Echo(this.xt.fetch_loaded(new XVar("container_grid")));
			MVCFunctions.Echo("</div>");
			MVCFunctions.Echo("</div>");
			return null;
		}
		public override XVar getMasterTableSQLClause(dynamic _param_basedOnProp = null)
		{
			#region default values
			if(_param_basedOnProp as Object == null) _param_basedOnProp = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic basedOnProp = XVar.Clone(_param_basedOnProp);
			#endregion

			dynamic dKeys = XVar.Array(), where = null;
			where = new XVar("");
			dKeys = XVar.Clone(this.pSet.getDetailKeysByMasterTable((XVar)(this.printMasterTable)));
			if(XVar.Pack(!(XVar)(dKeys)))
			{
				return "1=0";
			}
			foreach (KeyValuePair<XVar, dynamic> key in dKeys.GetEnumerator())
			{
				dynamic mValue = null;
				if(key.Key != XVar.Pack(0))
				{
					where = MVCFunctions.Concat(where, " and ");
				}
				if((XVar)(this.cipherer)  && (XVar)(this.cipherer.isEncryptionByPHPEnabled()))
				{
					mValue = XVar.Clone(this.cipherer.MakeDBValue((XVar)(key.Value), (XVar)(this.printMasterKeys[key.Key])));
				}
				else
				{
					mValue = XVar.Clone(CommonFunctions.make_db_value((XVar)(key.Value), (XVar)(this.printMasterKeys[key.Key]), new XVar(""), new XVar(""), (XVar)(this.tName)));
				}
				if(MVCFunctions.strlen((XVar)(mValue)) != 0)
				{
					where = MVCFunctions.Concat(where, getFieldSQLDecrypt((XVar)(key.Value)), "=", mValue);
				}
				else
				{
					where = MVCFunctions.Concat(where, "1=0");
				}
			}
			return where;
		}
	}
}
