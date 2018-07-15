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
	[SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
	public partial class SessionStaticGlobalController : BaseController
	{
		public XVar combo()
		{
			try
			{
				dynamic contentType = null, library = null, queryString = null, start = null, var_end = null, yuiComponents = XVar.Array(), yuiFiles = XVar.Array();
				GlobalVars.cCharset = new XVar("utf-8");
				queryString = XVar.Clone(MVCFunctions.GetQueryString());
				if((XVar)(!(XVar)(queryString as object != null))  || (XVar)(MVCFunctions.strlen((XVar)(queryString)) == 0))
				{
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				yuiFiles = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(queryString)));
				contentType = XVar.Clone((XVar.Pack(MVCFunctions.strpos((XVar)(yuiFiles[0]), new XVar(".js"))) ? XVar.Pack("application/x-javascript") : XVar.Pack(" text/css")));
				yuiComponents = XVar.Clone(XVar.Array());
				if(contentType == "application/x-javascript")
				{
					foreach (KeyValuePair<XVar, dynamic> yuiFile in yuiFiles.GetEnumerator())
					{
						dynamic parts = XVar.Array();
						parts = XVar.Clone(MVCFunctions.explode(new XVar("/"), (XVar)(yuiFile.Value)));
						if(MVCFunctions.count(parts) == 4)
						{
							if((XVar)((XVar)(parts.KeyExists(0))  && (XVar)(parts.KeyExists(1)))  && (XVar)(parts.KeyExists(2)))
							{
								yuiComponents.InitAndSetArrayItem(MVCFunctions.Concat(parts[2], "/", parts[2]), null);
							}
							else
							{
								MVCFunctions.Echo((XVar)(MVCFunctions.Concat("<!-- Unable to determine module name! ", MVCFunctions.runner_htmlspecialchars((XVar)(yuiFile.Value)), " -->")));
								return MVCFunctions.GetBuferContentAndClearBufer();
							}
						}
						else
						{
							start = XVar.Clone(MVCFunctions.strpos((XVar)(yuiFile.Value), new XVar("/build/")));
							if(XVar.Equals(XVar.Pack(start), XVar.Pack(false)))
							{
								MVCFunctions.Echo((XVar)(MVCFunctions.Concat("<!-- Unable to determine module name! ", MVCFunctions.runner_htmlspecialchars((XVar)(yuiFile.Value)), " -->")));
								return MVCFunctions.GetBuferContentAndClearBufer();
							}
							start += MVCFunctions.strlen(new XVar("/build/"));
							var_end = XVar.Clone(MVCFunctions.strpos((XVar)(yuiFile.Value), new XVar(".js")));
							if(XVar.Equals(XVar.Pack(var_end), XVar.Pack(false)))
							{
								MVCFunctions.Echo((XVar)(MVCFunctions.Concat("<!-- Unable to determine module name! ", MVCFunctions.runner_htmlspecialchars((XVar)(yuiFile.Value)), " -->")));
								return MVCFunctions.GetBuferContentAndClearBufer();
							}
							yuiComponents.InitAndSetArrayItem(MVCFunctions.substr((XVar)(yuiFile.Value), (XVar)(start), (XVar)(var_end - start)), null);
						}
					}
					library = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath(new XVar("include/yui/yui.lib"))), new XVar("r")));
				}
				else
				{
					foreach (KeyValuePair<XVar, dynamic> yuiFile in yuiFiles.GetEnumerator())
					{
						start = XVar.Clone(MVCFunctions.strpos((XVar)(yuiFile.Value), new XVar("/build/")));
						if(XVar.Equals(XVar.Pack(start), XVar.Pack(false)))
						{
							MVCFunctions.Echo((XVar)(MVCFunctions.Concat("<!-- Unable to determine module name! ", MVCFunctions.runner_htmlspecialchars((XVar)(yuiFile.Value)), " -->")));
							return MVCFunctions.GetBuferContentAndClearBufer();
						}
						start += MVCFunctions.strlen(new XVar("/build/"));
						yuiComponents.InitAndSetArrayItem(MVCFunctions.substr((XVar)(yuiFile.Value), (XVar)(start)), null);
					}
					library = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath(new XVar("include/yui/yuicss.lib"))), new XVar("r")));
				}
				MVCFunctions.Header("Cache-Control", "max-age=315360000");
				MVCFunctions.Header("Expires", "Thu, 29 Oct 2020 20:00:00 GMT");
				MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Type: ", contentType)));
				foreach (KeyValuePair<XVar, dynamic> y in yuiComponents.GetEnumerator())
				{
					start = XVar.Clone(MVCFunctions.strpos((XVar)(library), (XVar)(MVCFunctions.Concat("begincombofile ", y.Value))));
					if(XVar.Equals(XVar.Pack(start), XVar.Pack(false)))
					{
						MVCFunctions.Echo((XVar)(MVCFunctions.Concat("Unknown file ", MVCFunctions.runner_htmlspecialchars((XVar)(y.Value)))));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
					start += MVCFunctions.strlen((XVar)(MVCFunctions.Concat("begincombofile ", y.Value)));
					var_end = XVar.Clone(MVCFunctions.strpos((XVar)(library), new XVar("endcombofile"), (XVar)(start)));
					if(XVar.Equals(XVar.Pack(var_end), XVar.Pack(false)))
					{
						MVCFunctions.Echo((XVar)(MVCFunctions.Concat("Unknown file ", MVCFunctions.runner_htmlspecialchars((XVar)(y.Value)))));
						return MVCFunctions.GetBuferContentAndClearBufer();
					}
					MVCFunctions.Echo(MVCFunctions.substr((XVar)(library), (XVar)(start), (XVar)(var_end - start)));
				}
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
