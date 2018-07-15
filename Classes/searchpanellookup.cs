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
	public partial class SearchPanelLookup : SearchPanel
	{
		protected static bool skipSearchPanelLookupCtor = false;
		public SearchPanelLookup(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipSearchPanelLookupCtor)
			{
				skipSearchPanelLookupCtor = false;
				return;
			}
		}
		public override XVar searchAssign()
		{
			dynamic searchGlobalParams = XVar.Array(), searchforAttrs = null;
			base.searchAssign();
			searchforAttrs = XVar.Clone(MVCFunctions.Concat("placeholder=\"", "search", "\""));
			searchGlobalParams = XVar.Clone(this.searchClauseObj.getSearchGlobalParams());
			if(XVar.Pack(this.searchClauseObj.isUsedSrch()))
			{
				dynamic valSrchFor = null;
				valSrchFor = XVar.Clone(searchGlobalParams["simpleSrch"]);
				searchforAttrs = MVCFunctions.Concat(searchforAttrs, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(valSrchFor)), "\"");
			}
			searchforAttrs = MVCFunctions.Concat(searchforAttrs, " size=\"15\" name=\"ctlSearchFor", this.id, "\" id=\"ctlSearchFor", this.id, "\"");
			this.xt.assign(new XVar("searchfor_attrs"), (XVar)(searchforAttrs));
			return null;
		}
	}
}
