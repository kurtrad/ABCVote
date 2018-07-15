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
		public XVar buttonhandler()
		{
			try
			{
				dynamic buttId = null, eventId = null, table = null, var_params = XVar.Array();
				var_params = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.postvalue(new XVar("params")))));
				buttId = XVar.Clone(var_params["buttId"]);
				eventId = XVar.Clone(MVCFunctions.postvalue(new XVar("event")));
				table = XVar.Clone(var_params["table"]);
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
