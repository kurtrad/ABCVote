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
		public XVar checkduplicates()
		{
			try
			{
				dynamic _connection = null, data = XVar.Array(), denyChecking = null, fieldControlType = null, fieldName = null, fieldSQL = null, hasDuplicates = null, pageType = null, qResult = null, returnJSON = null, sql = null, tableName = null, value = null, where = null;
				ProjectSettings pSet;
				tableName = XVar.Clone(MVCFunctions.postvalue(new XVar("tableName")));
				pageType = XVar.Clone(MVCFunctions.postvalue(new XVar("pageType")));
				fieldName = XVar.Clone(MVCFunctions.postvalue(new XVar("fieldName")));
				fieldControlType = XVar.Clone(MVCFunctions.postvalue(new XVar("fieldControlType")));
				value = XVar.Clone(MVCFunctions.postvalue(new XVar("value")));
				if(XVar.Pack(!(XVar)(CommonFunctions.checkTableName((XVar)(tableName)))))
				{
					MVCFunctions.Echo(new XVar(0));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				Assembly.GetExecutingAssembly().GetType(MVCFunctions.Concat("runnerDotNet.", MVCFunctions.Concat("", tableName, ""),
					"_Variables")).InvokeMember("Apply", BindingFlags.InvokeMethod, null, null, null);
				if((XVar)(pageType != Constants.PAGE_REGISTER)  && (XVar)((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(!(XVar)(CommonFunctions.CheckSecurity((XVar)(XSession.Session[MVCFunctions.Concat("_", GlobalVars.strTableName, "_OwnerID")]), new XVar("Search"))))))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", MVCFunctions.Concat("Error: You have not permissions to read the ", tableName, " table's data")));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				_connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(GlobalVars.strTableName), (XVar)(pageType)));
				denyChecking = XVar.Clone(pSet.allowDuplicateValues((XVar)(fieldName)));
				if(XVar.Pack(denyChecking))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", "Duplicated values are allowed"));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(GlobalVars.strTableName), (XVar)(pSet)));
				if(XVar.Pack(GlobalVars.cipherer.isFieldEncrypted((XVar)(fieldName))))
				{
					value = XVar.Clone(GlobalVars.cipherer.MakeDBValue((XVar)(fieldName), (XVar)(value), (XVar)(fieldControlType), new XVar(true)));
				}
				else
				{
					value = XVar.Clone(CommonFunctions.make_db_value((XVar)(fieldName), (XVar)(value), (XVar)(fieldControlType), new XVar(""), (XVar)(GlobalVars.strTableName)));
				}
				if(value == "null")
				{
					fieldSQL = XVar.Clone(RunnerPage._getFieldSQL((XVar)(fieldName), (XVar)(_connection), (XVar)(pSet)));
				}
				else
				{
					fieldSQL = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)(fieldName), (XVar)(_connection), (XVar)(pSet), (XVar)(GlobalVars.cipherer)));
				}
				where = XVar.Clone(MVCFunctions.Concat(fieldSQL, (XVar.Pack(value == "null") ? XVar.Pack(" is ") : XVar.Pack("=")), value));
				sql = XVar.Clone(MVCFunctions.Concat("SELECT count(*) from ", _connection.addTableWrappers((XVar)(pSet.getOriginalTableName())), " where ", where));
				qResult = XVar.Clone(_connection.query((XVar)(sql)));
				if((XVar)(!(XVar)(qResult))  || (XVar)(!(XVar)(data = XVar.Clone(qResult.fetchNumeric()))))
				{
					returnJSON = XVar.Clone(new XVar("success", false, "error", "Error: Wrong SQL query"));
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
					return MVCFunctions.GetBuferContentAndClearBufer();
				}
				hasDuplicates = XVar.Clone((XVar.Pack(data[0]) ? XVar.Pack(true) : XVar.Pack(false)));
				returnJSON = XVar.Clone(new XVar("success", true, "hasDuplicates", hasDuplicates, "error", ""));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
				return MVCFunctions.GetBuferContentAndClearBufer();
			}
			catch(RunnerRedirectException ex)
			{ return Redirect(ex.Message); }
		}
	}
}
