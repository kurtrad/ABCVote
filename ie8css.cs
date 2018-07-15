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
	public partial class GlobalController : BaseController
	{
		public XVar ie8css()
		{
			try
			{
				dynamic css = null, ext = null, file = null;
				MVCFunctions.Header("Content-Type", "text/css");
				file = XVar.Clone(MVCFunctions.GetQueryString());
				ext = XVar.Clone(MVCFunctions.substr((XVar)(file), (XVar)(MVCFunctions.strrpos((XVar)(file), new XVar(".")) + 1)));
				if((XVar)(ext != "css")  || (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(file), new XVar(".."))), XVar.Pack(false))))
				{
					MVCFunctions.Echo(new XVar(""));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				css = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath((XVar)(file))), new XVar("r")));
				css = XVar.Clone(MVCFunctions.preg_replace((XVar)(new XVar(0, "/ :not\\(.*?\\)/", 1, "/ :first-child/", 2, "/ :last-child/", 3, "/ :nth-last-child\\(.*?\\)/")), new XVar(" *"), (XVar)(css)));
				css = XVar.Clone(MVCFunctions.preg_replace((XVar)(new XVar(0, "/:not\\(.*?\\)/", 1, "/:first-child/", 2, "/:last-child/", 3, "/:nth-last-child\\(.*?\\)/")), new XVar(""), (XVar)(css)));
				MVCFunctions.Echo(css);
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
