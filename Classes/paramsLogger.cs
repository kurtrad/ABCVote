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
	public partial class paramsLogger : XClass
	{
		protected dynamic paramsTableName = XVar.Pack("");
		protected dynamic var_type;
		protected dynamic userID = XVar.Pack("");
		protected dynamic cookie = XVar.Pack("");
		protected dynamic tableNameId;
		protected dynamic dbParamsTableName;
		protected dynamic dbTypeFieldName;
		protected dynamic dbDataFieldName;
		protected dynamic dbTableNameFieldName;
		protected dynamic dbCookieFieldName;
		protected dynamic dbUserNameFieldName;
		protected dynamic dbNameFieldName;
		protected dynamic connection;
		public paramsLogger(dynamic _param_tableNameId, dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic tableNameId = XVar.Clone(_param_tableNameId);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			this.var_type = XVar.Clone(var_type);
			this.tableNameId = XVar.Clone(tableNameId);
			this.connection = XVar.Clone(GlobalVars.cman.getForSavedSearches());
			assignDbFieldsAndTableNames();
			assignUserId();
			assignCookieParams();
		}
		protected virtual XVar assignDbFieldsAndTableNames()
		{
			this.dbParamsTableName = XVar.Clone(this.connection.addTableWrappers((XVar)(this.paramsTableName)));
			this.dbTypeFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("TYPE")));
			this.dbDataFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("SEARCH")));
			this.dbTableNameFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("TABLENAME")));
			this.dbCookieFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("COOKIE")));
			this.dbUserNameFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("USERNAME")));
			this.dbNameFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("NAME")));
			return null;
		}
		protected virtual XVar assignUserId()
		{
			if((XVar)(XSession.Session.KeyExists("UserID"))  && (XVar)(XSession.Session["UserID"] != "Guest"))
			{
				this.userID = XVar.Clone(XSession.Session["UserID"]);
			}
			return null;
		}
		protected virtual XVar assignCookieParams()
		{
			if((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(MVCFunctions.GetCookie("paramsLogger")))))  && (XVar)(!(XVar)(this.userID)))
			{
				MVCFunctions.SetCookie(new XVar("paramsLogger"), (XVar)(CommonFunctions.generatePassword(new XVar(24))), (XVar)(MVCFunctions.time() + (5 * 365) * 86400));
			}
			this.cookie = XVar.Clone(MVCFunctions.GetCookie("paramsLogger"));
			return null;
		}
		protected virtual XVar getCommonWhere()
		{
			dynamic addWhere = null, typeWhere = null, wheres = XVar.Array();
			wheres = XVar.Clone(XVar.Array());
			if(XVar.Pack(this.userID))
			{
				wheres.InitAndSetArrayItem(MVCFunctions.Concat(this.dbUserNameFieldName, "=", this.connection.prepareString((XVar)(this.userID))), null);
			}
			if(XVar.Pack(this.cookie))
			{
				wheres.InitAndSetArrayItem(MVCFunctions.Concat(this.dbCookieFieldName, "=", this.connection.prepareString((XVar)(this.cookie))), null);
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(wheres))))
			{
				return "1=0";
			}
			typeWhere = new XVar("1=1");
			if(!XVar.Equals(XVar.Pack(this.var_type), XVar.Pack(Constants.SSEARCH_PARAMS_TYPE)))
			{
				typeWhere = XVar.Clone(MVCFunctions.Concat(this.dbTypeFieldName, "=", this.var_type));
			}
			addWhere = XVar.Clone(CommonFunctions.whereAdd((XVar)(typeWhere), (XVar)(MVCFunctions.implode(new XVar(" OR "), (XVar)(wheres)))));
			return CommonFunctions.whereAdd((XVar)(MVCFunctions.Concat(this.dbTableNameFieldName, "=", this.connection.prepareString((XVar)(this.tableNameId)))), (XVar)(addWhere));
		}
		public virtual XVar save(dynamic _param_data, dynamic _param_addColumnsList = null, dynamic _param_addValuesList = null)
		{
			#region default values
			if(_param_addColumnsList as Object == null) _param_addColumnsList = new XVar("");
			if(_param_addValuesList as Object == null) _param_addValuesList = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic addColumnsList = XVar.Clone(_param_addColumnsList);
			dynamic addValuesList = XVar.Clone(_param_addValuesList);
			#endregion

			dynamic columnsList = null, issetData = null, sql = null, valuesList = null;
			issetData = XVar.Clone(MVCFunctions.strlen((XVar)(readData())) != 0);
			if((XVar)(issetData)  && (XVar)(this.var_type != Constants.SSEARCH_PARAMS_TYPE))
			{
				update((XVar)(data));
				return null;
			}
			columnsList = XVar.Clone(MVCFunctions.Concat(addColumnsList, MVCFunctions.implode(new XVar(","), (XVar)(new XVar(0, this.dbDataFieldName, 1, this.dbTableNameFieldName)))));
			valuesList = XVar.Clone(MVCFunctions.Concat(addValuesList, this.connection.prepareString((XVar)(MVCFunctions.my_json_encode((XVar)(data)))), ", ", this.connection.prepareString((XVar)(this.tableNameId))));
			if(XVar.Pack(this.userID))
			{
				columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbUserNameFieldName);
				valuesList = MVCFunctions.Concat(valuesList, ", ", this.connection.prepareString((XVar)(this.userID)));
			}
			else
			{
				if(XVar.Pack(this.cookie))
				{
					columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbCookieFieldName);
					valuesList = MVCFunctions.Concat(valuesList, ", ", this.connection.prepareString((XVar)(this.cookie)));
				}
			}
			if(this.var_type != Constants.SSEARCH_PARAMS_TYPE)
			{
				columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbTypeFieldName);
				valuesList = MVCFunctions.Concat(valuesList, ", ", this.var_type);
			}
			sql = XVar.Clone(MVCFunctions.Concat("INSERT INTO ", this.dbParamsTableName, " (", columnsList, ") values (", valuesList, ")"));
			this.connection.execSilent((XVar)(sql));
			return null;
		}
		public virtual XVar saveShowHideData(dynamic _param_deviceClass, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic deviceClass = XVar.Clone(_param_deviceClass);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic columnsList = null, sql = null, valuesList = null;
			if(XVar.Pack(getShowHideData((XVar)(deviceClass))))
			{
				update((XVar)(data), (XVar)(MVCFunctions.Concat(this.dbNameFieldName, "=", this.connection.prepareString((XVar)(deviceClass)))));
				return null;
			}
			columnsList = XVar.Clone(MVCFunctions.implode(new XVar(","), (XVar)(new XVar(0, this.dbNameFieldName, 1, this.dbDataFieldName, 2, this.dbTableNameFieldName))));
			valuesList = XVar.Clone(MVCFunctions.Concat(this.connection.prepareString((XVar)(deviceClass)), ", ", this.connection.prepareString((XVar)(MVCFunctions.my_json_encode((XVar)(data)))), ", ", this.connection.prepareString((XVar)(this.tableNameId))));
			if(XVar.Pack(this.userID))
			{
				columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbUserNameFieldName);
				valuesList = MVCFunctions.Concat(valuesList, ", ", this.connection.prepareString((XVar)(this.userID)));
			}
			else
			{
				if(XVar.Pack(this.cookie))
				{
					columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbCookieFieldName);
					valuesList = MVCFunctions.Concat(valuesList, ", ", this.connection.prepareString((XVar)(this.cookie)));
				}
			}
			columnsList = MVCFunctions.Concat(columnsList, ", ", this.dbTypeFieldName);
			valuesList = MVCFunctions.Concat(valuesList, ", ", this.var_type);
			sql = XVar.Clone(MVCFunctions.Concat("INSERT INTO ", this.dbParamsTableName, " (", columnsList, ") values (", valuesList, ")"));
			this.connection.execSilent((XVar)(sql));
			return null;
		}
		protected virtual XVar update(dynamic _param_data, dynamic _param_addWhere = null)
		{
			#region default values
			if(_param_addWhere as Object == null) _param_addWhere = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic addWhere = XVar.Clone(_param_addWhere);
			#endregion

			dynamic sql = null;
			if(addWhere != XVar.Pack(""))
			{
				addWhere = MVCFunctions.Concat(addWhere, " AND ");
			}
			sql = XVar.Clone(MVCFunctions.Concat("UPDATE ", this.dbParamsTableName, " SET ", this.dbDataFieldName, "=", this.connection.prepareString((XVar)(MVCFunctions.my_json_encode((XVar)(data)))), " WHERE ", addWhere, getCommonWhere()));
			this.connection.execSilent((XVar)(sql));
			return null;
		}
		protected virtual XVar delete(dynamic _param_addWhere = null)
		{
			#region default values
			if(_param_addWhere as Object == null) _param_addWhere = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic addWhere = XVar.Clone(_param_addWhere);
			#endregion

			dynamic sql = null;
			if(addWhere != XVar.Pack(""))
			{
				addWhere = MVCFunctions.Concat(addWhere, " AND ");
			}
			sql = XVar.Clone(MVCFunctions.Concat("DELETE FROM ", this.dbParamsTableName, " WHERE ", addWhere, getCommonWhere()));
			this.connection.execSilent((XVar)(sql));
			return null;
		}
		public virtual XVar getData()
		{
			return decode((XVar)(readData()));
		}
		public virtual XVar decode(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic parsed = null;
			parsed = XVar.Clone(MVCFunctions.my_json_decode((XVar)(data)));
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(parsed)))))
			{
				parsed = XVar.Clone(MVCFunctions.runner_unserialize_array((XVar)(data)));
			}
			return parsed;
		}
		protected virtual XVar queryData(dynamic _param_addWhere = null)
		{
			#region default values
			if(_param_addWhere as Object == null) _param_addWhere = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic addWhere = XVar.Clone(_param_addWhere);
			#endregion

			dynamic sql = null;
			if(addWhere != XVar.Pack(""))
			{
				addWhere = MVCFunctions.Concat(addWhere, " AND ");
			}
			sql = XVar.Clone(MVCFunctions.Concat("SELECT ", this.dbNameFieldName, ",", this.dbDataFieldName, " from ", this.dbParamsTableName, " where ", addWhere, getCommonWhere()));
			return this.connection.querySilent((XVar)(sql));
		}
		protected virtual XVar readData()
		{
			dynamic data = XVar.Array(), qResult = null;
			qResult = XVar.Clone(queryData());
			if(XVar.Pack(!(XVar)(qResult)))
			{
				return "";
			}
			data = XVar.Clone(qResult.fetchAssoc());
			if(XVar.Pack(!(XVar)(data.KeyExists("SEARCH"))))
			{
				return false;
			}
			return data["SEARCH"];
		}
		public virtual XVar getShowHideData(dynamic _param_deviceClass = null)
		{
			#region default values
			if(_param_deviceClass as Object == null) _param_deviceClass = new XVar(-1);
			#endregion

			#region pass-by-value parameters
			dynamic deviceClass = XVar.Clone(_param_deviceClass);
			#endregion

			dynamic data = XVar.Array(), qResult = null, ret = XVar.Array(), where = null;
			where = new XVar("");
			if(XVar.Pack(0) <= deviceClass)
			{
				where = XVar.Clone(MVCFunctions.Concat(this.dbNameFieldName, "=", this.connection.prepareString((XVar)(deviceClass))));
			}
			qResult = XVar.Clone(queryData((XVar)(where)));
			if(XVar.Pack(!(XVar)(qResult)))
			{
				return XVar.Array();
			}
			ret = XVar.Clone(XVar.Array());
			while(XVar.Pack(data = XVar.Clone(qResult.fetchAssoc())))
			{
				ret.InitAndSetArrayItem(decode((XVar)(data["SEARCH"])), data["NAME"]);
			}
			return ret;
		}
	}
}
