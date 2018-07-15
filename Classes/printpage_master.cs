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
	public partial class PrintPage_Master : PrintPage
	{
		protected static bool skipPrintPage_MasterCtor = false;
		public PrintPage_Master(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipPrintPage_MasterCtor)
			{
				skipPrintPage_MasterCtor = false;
				return;
			}
			this.pageType = new XVar("masterprint");
			this.masterPageType = XVar.Clone(var_params["masterPageType"]);
			if(this.masterPageType == "report")
			{
				this.pageType = new XVar("masterrprint");
			}
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			return null;
		}
		public virtual XVar getMasterHeading()
		{
			this.xt.assign(new XVar("masterlist_title"), new XVar(true));
			return this.xt.fetch_loaded(new XVar("masterlist_title"));
		}
		public virtual XVar preparePage()
		{
			dynamic fields = XVar.Array(), i = null, keylink = null, pageTypeTitle = null, tKeys = XVar.Array();
			if((XVar)(!(XVar)(this.masterRecordData))  || (XVar)(!(XVar)(MVCFunctions.count(this.masterRecordData))))
			{
				return null;
			}
			pageTypeTitle = XVar.Clone(this.pageType);
			if(this.masterPageType == "report")
			{
				pageTypeTitle = new XVar("masterprint");
			}
			this.xt.assign(new XVar("pagetitlelabel"), (XVar)(getPageTitle((XVar)(pageTypeTitle), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(this.masterRecordData))));
			tKeys = XVar.Clone(this.pSet.getTableKeys());
			keylink = new XVar("");
			i = new XVar(0);
			for(;i < MVCFunctions.count(tKeys); i++)
			{
				keylink = MVCFunctions.Concat(keylink, "&key", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(this.masterRecordData[tKeys[i]])))));
			}
			fields = XVar.Clone(this.pSet.getMasterListFields());
			fields = XVar.Clone(MVCFunctions.array_merge((XVar)(fields), (XVar)(tKeys)));
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				dynamic fieldClassStr = null;
				fieldClassStr = XVar.Clone(fieldClass((XVar)(f.Value)));
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(f.Value)), "_mastervalue")), (XVar)(MVCFunctions.Concat("<span class='", fieldClassStr, "'>", showDBValue((XVar)(f.Value), (XVar)(this.masterRecordData), (XVar)(keylink)), "</span>")));
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(f.Value)), "_class")), (XVar)(fieldClassStr));
			}
			if(XVar.Pack(this.pageLayout))
			{
				this.xt.assign(new XVar("pageattrs"), (XVar)(MVCFunctions.Concat("class=\"", this.pageLayout.style, " page-", this.pageLayout.name, "\"")));
			}
			this.xt.load_template((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(this.pageType))));
			return null;
		}
		public virtual XVar showMaster(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			if((XVar)(!(XVar)(this.masterRecordData))  || (XVar)(!(XVar)(MVCFunctions.count(this.masterRecordData))))
			{
				return null;
			}
			this.xt.assign(new XVar("masterlist_title"), new XVar(false));
			this.xt.display_loaded();
			return null;
		}
	}
}
