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
	public partial class searchParamsLogger : paramsLogger
	{
		protected dynamic savedSearchesParams = XVar.Array();
		protected static bool skipsearchParamsLoggerCtor = false;
		public searchParamsLogger(dynamic _param_tableNameId, dynamic _param_type = null)
			:base((XVar)_param_tableNameId, (XVar)Constants.SSEARCH_PARAMS_TYPE)
		{
			if(skipsearchParamsLoggerCtor)
			{
				skipsearchParamsLoggerCtor = false;
				return;
			}
			#region default values
			if(_param_type as Object == null) _param_type = new XVar(Constants.SSEARCH_PARAMS_TYPE);
			#endregion

			#region pass-by-value parameters
			dynamic tableNameId = XVar.Clone(_param_tableNameId);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			this.var_type = new XVar(Constants.SSEARCH_PARAMS_TYPE);
		}
		protected override XVar assignDbFieldsAndTableNames()
		{
			base.assignDbFieldsAndTableNames();
			this.dbNameFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("NAME")));
			return null;
		}
		public virtual XVar saveSearch(dynamic _param_searchName, dynamic _param_searchParams)
		{
			#region pass-by-value parameters
			dynamic searchName = XVar.Clone(_param_searchName);
			dynamic searchParams = XVar.Clone(_param_searchParams);
			#endregion

			dynamic column = null, savedSearchNames = null, value = null;
			savedSearchNames = XVar.Clone(getSavedSeachesParams());
			if(XVar.Pack(savedSearchNames.KeyExists(searchName)))
			{
				updateSearch((XVar)(searchName), (XVar)(searchParams));
				return null;
			}
			column = XVar.Clone(MVCFunctions.Concat(this.dbNameFieldName, ", "));
			value = XVar.Clone(MVCFunctions.Concat(this.connection.prepareString((XVar)(searchName)), ", "));
			save((XVar)(searchParams), (XVar)(column), (XVar)(value));
			return null;
		}
		public virtual XVar updateSearch(dynamic _param_searchName, dynamic _param_searchParams)
		{
			#region pass-by-value parameters
			dynamic searchName = XVar.Clone(_param_searchName);
			dynamic searchParams = XVar.Clone(_param_searchParams);
			#endregion

			dynamic where = null;
			where = XVar.Clone(MVCFunctions.Concat(this.dbNameFieldName, "=", this.connection.prepareString((XVar)(searchName))));
			update((XVar)(searchParams), (XVar)(where));
			return null;
		}
		public virtual XVar deleteSearch(dynamic _param_searchName)
		{
			#region pass-by-value parameters
			dynamic searchName = XVar.Clone(_param_searchName);
			#endregion

			dynamic where = null;
			where = XVar.Clone(MVCFunctions.Concat(this.dbNameFieldName, "=", this.connection.prepareString((XVar)(searchName))));
			delete((XVar)(where));
			return null;
		}
		public virtual XVar getSavedSeachesParams()
		{
			dynamic data = XVar.Array(), names = XVar.Array(), qResult = null, sql = null, where = null;
			if(XVar.Pack(MVCFunctions.count(this.savedSearchesParams)))
			{
				return this.savedSearchesParams;
			}
			where = XVar.Clone(getCommonWhere());
			sql = XVar.Clone(MVCFunctions.Concat("SELECT ", this.dbNameFieldName, ", ", this.dbDataFieldName, ", ", this.dbTypeFieldName, " from ", this.dbParamsTableName, " where ", where, " ORDER BY ", this.dbNameFieldName));
			qResult = XVar.Clone(this.connection.querySilent((XVar)(sql)));
			if(XVar.Pack(!(XVar)(qResult)))
			{
				return XVar.Array();
			}
			names = XVar.Clone(XVar.Array());
			while(XVar.Pack(data = XVar.Clone(qResult.fetchAssoc())))
			{
				if((XVar)(!(XVar)(data["TYPE"]))  || (XVar)(data["TYPE"] == 1))
				{
					if(MVCFunctions.substr((XVar)(data["SEARCH"]), new XVar(0), new XVar(2)) != "{\"")
					{
						names.InitAndSetArrayItem(MVCFunctions.runner_unserialize_array((XVar)(data["SEARCH"])), data["NAME"]);
					}
					else
					{
						names.InitAndSetArrayItem(decode((XVar)(data["SEARCH"])), data["NAME"]);
					}
				}
			}
			this.savedSearchesParams = XVar.Clone(names);
			return names;
		}
	}
}
