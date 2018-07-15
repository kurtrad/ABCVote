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
	public partial class DB : XClass
	{
		public static dynamic CurrentConnection()
		{
			return (XVar.Pack(GlobalVars.currentConnection) ? XVar.Pack(GlobalVars.currentConnection) : XVar.Pack(DefaultConnection()));
		}
		public static XVar CurrentConnectionId()
		{
			GlobalVars.conn = XVar.Clone(CurrentConnection());
			return GlobalVars.conn.connId;
		}
		public static XVar DefaultConnection()
		{
			return GlobalVars.cman.getDefault();
		}
		public static XVar ConnectionByTable(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return GlobalVars.cman.byTable((XVar)(table));
		}
		public static XVar ConnectionByName(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			return GlobalVars.cman.byName((XVar)(name));
		}
		public static XVar SetConnection(dynamic _param_connection)
		{
			#region pass-by-value parameters
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			if(XVar.Pack(MVCFunctions.is_string((XVar)(connection))))
			{
				GlobalVars.currentConnection = XVar.Clone(ConnectionByName((XVar)(connection)));
			}
			else
			{
				if(XVar.Pack(MVCFunctions.is_a((XVar)(connection), new XVar("Connection"))))
				{
					GlobalVars.currentConnection = XVar.Clone(connection);
				}
			}
			return null;
		}
		public static XVar LastId()
		{
			return CurrentConnection().getInsertedId();
		}
		public static dynamic Query(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			return CurrentConnection().querySilent((XVar)(sql));
		}
		public static XVar Exec(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			return CurrentConnection().execSilent((XVar)(sql));
		}
		public static XVar LastError()
		{
			return CurrentConnection().lastError();
		}
		public static XVar func_Select(dynamic _param_table, dynamic _param_userConditions = null)
		{
			#region default values
			if(_param_userConditions as Object == null) _param_userConditions = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic userConditions = XVar.Clone(_param_userConditions);
			#endregion

			dynamic queryResult = null, sql = null, tableInfo = XVar.Array(), whereSql = null;
			tableInfo = XVar.Clone(_getTableInfo((XVar)(table)));
			if(XVar.Pack(!(XVar)(tableInfo)))
			{
				return false;
			}
			whereSql = XVar.Clone(_getWhereSql((XVar)(userConditions), (XVar)(tableInfo["fields"])));
			sql = XVar.Clone(MVCFunctions.Concat("SELECT * FROM ", CurrentConnection().addTableWrappers((XVar)(tableInfo["fullName"])), whereSql));
			queryResult = XVar.Clone(CurrentConnection().querySilent((XVar)(sql)));
			return queryResult;
		}
		public static XVar Delete(dynamic _param_table, dynamic _param_userConditions = null)
		{
			#region default values
			if(_param_userConditions as Object == null) _param_userConditions = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic userConditions = XVar.Clone(_param_userConditions);
			#endregion

			dynamic ret = null, sql = null, tableInfo = XVar.Array(), whereSql = null;
			tableInfo = XVar.Clone(_getTableInfo((XVar)(table)));
			if(XVar.Pack(!(XVar)(tableInfo)))
			{
				return false;
			}
			whereSql = XVar.Clone(_getWhereSql((XVar)(userConditions), (XVar)(tableInfo["fields"])));
			if(whereSql == XVar.Pack(""))
			{
				return false;
			}
			sql = XVar.Clone(MVCFunctions.Concat("DELETE FROM ", CurrentConnection().addTableWrappers((XVar)(tableInfo["fullName"])), whereSql));
			ret = XVar.Clone(CurrentConnection().execSilent((XVar)(sql)));
			return ret;
		}
		public static XVar Insert(dynamic _param_table, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic blobs = XVar.Array(), iFields = null, iValues = null, result = null, tableInfo = XVar.Array();
			result = new XVar(false);
			tableInfo = XVar.Clone(_getTableInfo((XVar)(table)));
			if(XVar.Pack(!(XVar)(tableInfo)))
			{
				return false;
			}
			iFields = new XVar("");
			iValues = new XVar("");
			blobs = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in data.GetEnumerator())
			{
				dynamic field = XVar.Array();
				field = XVar.Clone(CommonFunctions.getArrayElementNC((XVar)(tableInfo["fields"]), (XVar)(value.Key)));
				if(XVar.Pack(field == null))
				{
					continue;
				}
				iFields = MVCFunctions.Concat(iFields, CurrentConnection().addFieldWrappers((XVar)(field["name"])), ",");
				iValues = MVCFunctions.Concat(iValues, _prepareValue((XVar)(value.Value), (XVar)(field["type"])), ",");
				if((XVar)((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Oracle)  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_DB2))  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix))
				{
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(field["type"]))))
					{
						blobs.InitAndSetArrayItem(value.Value, field["name"]);
					}
					if((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix)  && (XVar)(CommonFunctions.IsTextType((XVar)(field["type"]))))
					{
						blobs.InitAndSetArrayItem(value.Value, field["name"]);
					}
				}
			}
			if((XVar)(iFields != XVar.Pack(""))  && (XVar)(iValues != XVar.Pack("")))
			{
				dynamic sql = null;
				iFields = XVar.Clone(MVCFunctions.substr((XVar)(iFields), new XVar(0), new XVar(-1)));
				iValues = XVar.Clone(MVCFunctions.substr((XVar)(iValues), new XVar(0), new XVar(-1)));
				sql = XVar.Clone(MVCFunctions.Concat("INSERT INTO ", CurrentConnection().addTableWrappers((XVar)(tableInfo["fullName"])), " (", iFields, ") values (", iValues, ")"));
				if(0 < MVCFunctions.count(blobs))
				{
					result = XVar.Clone(_execSilentWithBlobProcessing((XVar)(blobs), (XVar)(sql), (XVar)(tableInfo["fields"])));
				}
				else
				{
					result = XVar.Clone(CurrentConnection().execSilent((XVar)(sql)));
				}
			}
			return result;
		}
		public static XVar Update(dynamic _param_table, dynamic _param_data, dynamic _param_userConditions)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic data = XVar.Clone(_param_data);
			dynamic userConditions = XVar.Clone(_param_userConditions);
			#endregion

			dynamic blobs = XVar.Array(), result = null, tableInfo = XVar.Array(), updateValues = XVar.Array(), whereSql = null;
			result = new XVar(false);
			tableInfo = XVar.Clone(_getTableInfo((XVar)(table)));
			if(XVar.Pack(!(XVar)(tableInfo)))
			{
				return false;
			}
			whereSql = XVar.Clone(_getWhereSql((XVar)(userConditions), (XVar)(tableInfo["fields"])));
			if(whereSql == XVar.Pack(""))
			{
				return false;
			}
			updateValues = XVar.Clone(XVar.Array());
			blobs = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in data.GetEnumerator())
			{
				dynamic field = XVar.Array(), prepareFieldName = null, prepareValue = null;
				field = XVar.Clone(CommonFunctions.getArrayElementNC((XVar)(tableInfo["fields"]), (XVar)(value.Key)));
				if(XVar.Pack(field == null))
				{
					continue;
				}
				prepareFieldName = XVar.Clone(CurrentConnection().addFieldWrappers((XVar)(field["name"])));
				prepareValue = XVar.Clone(_prepareValue((XVar)(value.Value), (XVar)(field["type"])));
				updateValues.InitAndSetArrayItem(MVCFunctions.Concat(prepareFieldName, "=", prepareValue), null);
				if((XVar)((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Oracle)  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_DB2))  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix))
				{
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(field["type"]))))
					{
						blobs.InitAndSetArrayItem(value.Value, field["name"]);
					}
					if((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix)  && (XVar)(CommonFunctions.IsTextType((XVar)(field["type"]))))
					{
						blobs.InitAndSetArrayItem(value.Value, field["name"]);
					}
				}
			}
			if(0 < MVCFunctions.count(updateValues))
			{
				dynamic sql = null, updateSQL = null;
				updateSQL = XVar.Clone(MVCFunctions.implode(new XVar(","), (XVar)(updateValues)));
				sql = XVar.Clone(MVCFunctions.Concat("UPDATE ", CurrentConnection().addTableWrappers((XVar)(tableInfo["fullName"])), " SET ", updateSQL, whereSql));
				if(0 < MVCFunctions.count(blobs))
				{
					result = XVar.Clone(_execSilentWithBlobProcessing((XVar)(blobs), (XVar)(sql), (XVar)(tableInfo["fields"])));
				}
				else
				{
					result = XVar.Clone(CurrentConnection().execSilent((XVar)(sql)));
				}
			}
			return result;
		}
		protected static XVar _getWhereSql(dynamic _param_userConditions, dynamic _param_founedfields)
		{
			#region pass-by-value parameters
			dynamic userConditions = XVar.Clone(_param_userConditions);
			dynamic founedfields = XVar.Clone(_param_founedfields);
			#endregion

			dynamic conditions = XVar.Array(), whereSql = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(userConditions)))))
			{
				whereSql = XVar.Clone(MVCFunctions.trim((XVar)(userConditions)));
				if(whereSql != XVar.Pack(""))
				{
					whereSql = XVar.Clone(MVCFunctions.Concat(" WHERE ", whereSql));
				}
				return whereSql;
			}
			conditions = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in userConditions.GetEnumerator())
			{
				dynamic field = XVar.Array(), wrappedField = null;
				field = XVar.Clone(CommonFunctions.getArrayElementNC((XVar)(founedfields), (XVar)(value.Key)));
				if(XVar.Pack(field == null))
				{
					continue;
				}
				wrappedField = XVar.Clone(CurrentConnection().addFieldWrappers((XVar)(field["name"])));
				if(XVar.Pack(value.Value == null))
				{
					conditions.InitAndSetArrayItem(MVCFunctions.Concat(wrappedField, " IS NULL"), null);
				}
				else
				{
					conditions.InitAndSetArrayItem(MVCFunctions.Concat(wrappedField, "=", _prepareValue((XVar)(value.Value), (XVar)(field["type"]))), null);
				}
			}
			whereSql = new XVar("");
			if(0 < MVCFunctions.count(conditions))
			{
				whereSql = MVCFunctions.Concat(whereSql, " WHERE ", MVCFunctions.implode(new XVar(" AND "), (XVar)(conditions)));
			}
			return whereSql;
		}
		protected static XVar _execSilentWithBlobProcessing(dynamic _param_blobs, dynamic _param_dalSQL, dynamic _param_tableinfo)
		{
			#region pass-by-value parameters
			dynamic blobs = XVar.Clone(_param_blobs);
			dynamic dalSQL = XVar.Clone(_param_dalSQL);
			dynamic tableinfo = XVar.Clone(_param_tableinfo);
			#endregion

			dynamic blobTypes = XVar.Array();
			blobTypes = XVar.Clone(XVar.Array());
			if(CurrentConnection().dbType == Constants.nDATABASE_Informix)
			{
				foreach (KeyValuePair<XVar, dynamic> fvalue in blobs.GetEnumerator())
				{
					blobTypes.InitAndSetArrayItem(tableinfo[fvalue.Key]["type"], fvalue.Key);
				}
			}
			CurrentConnection().execSilentWithBlobProcessing((XVar)(dalSQL), (XVar)(blobs), (XVar)(blobTypes));
			return null;
		}
		protected static XVar _prepareValue(dynamic _param_value, dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(XVar.Pack(value == null))
			{
				return "NULL";
			}
			if((XVar)((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Oracle)  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_DB2))  || (XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix))
			{
				if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(var_type))))
				{
					if(CurrentConnection().dbType == Constants.nDATABASE_Oracle)
					{
						return "EMPTY_BLOB()";
					}
					return "?";
				}
				if((XVar)(CurrentConnection().dbType == Constants.nDATABASE_Informix)  && (XVar)(CommonFunctions.IsTextType((XVar)(var_type))))
				{
					return "?";
				}
			}
			if((XVar)(CommonFunctions.IsNumberType((XVar)(var_type)))  && (XVar)(!(XVar)(MVCFunctions.IsNumeric(value))))
			{
				value = XVar.Clone(MVCFunctions.trim((XVar)(value)));
				value = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(value)));
				if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(value))))
				{
					return "NULL";
				}
			}
			if((XVar)(CommonFunctions.IsDateFieldType((XVar)(var_type)))  || (XVar)(CommonFunctions.IsTimeType((XVar)(var_type))))
			{
				if(XVar.Pack(!(XVar)(value)))
				{
					return "NULL";
				}
				if(XVar.Pack(MVCFunctions.IsNumeric(value)))
				{
					if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(var_type))))
					{
						value = XVar.Clone(MVCFunctions.Concat(MVCFunctions.getYMDdate((XVar)(value)), " ", MVCFunctions.getHISdate((XVar)(value))));
					}
					else
					{
						if(XVar.Pack(CommonFunctions.IsTimeType((XVar)(var_type))))
						{
							value = XVar.Clone(MVCFunctions.getHISdate((XVar)(value)));
						}
					}
				}
				return CurrentConnection().addDateQuotes((XVar)(value));
			}
			if(XVar.Pack(CommonFunctions.NeedQuotes((XVar)(var_type))))
			{
				return CurrentConnection().prepareString((XVar)(value));
			}
			return value;
		}
		protected static XVar _findDalTable(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic tableName = XVar.Array();
			GlobalVars.conn = XVar.Clone(CurrentConnection());
			tableName = XVar.Clone(GlobalVars.conn.getTableNameComponents((XVar)(table)));
			_fillTablesList();
			foreach (KeyValuePair<XVar, dynamic> t in GlobalVars.dalTables.GetEnumerator())
			{
				if((XVar)(t.Value["schema"] == tableName["schema"])  && (XVar)(t.Value["name"] == tableName["table"]))
				{
					return t.Value;
				}
			}
			tableName.InitAndSetArrayItem(MVCFunctions.strtoupper((XVar)(tableName["schema"])), "schema");
			tableName.InitAndSetArrayItem(MVCFunctions.strtoupper((XVar)(tableName["table"])), "table");
			foreach (KeyValuePair<XVar, dynamic> t in GlobalVars.dalTables.GetEnumerator())
			{
				if((XVar)(MVCFunctions.strtoupper((XVar)(t.Value["schema"])) == tableName["schema"])  && (XVar)(MVCFunctions.strtoupper((XVar)(t.Value["name"])) == tableName["table"]))
				{
					return t.Value;
				}
			}
			return null;
		}
		protected static XVar _getTableInfo(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic connId = null, tableDescriptor = XVar.Array(), tableInfo = XVar.Array();
			connId = XVar.Clone(CurrentConnectionId());
			if(XVar.Pack(!(XVar)(GlobalVars.tableinfo_cache.KeyExists(connId))))
			{
				GlobalVars.tableinfo_cache.InitAndSetArrayItem(XVar.Array(), connId);
			}
			tableInfo = XVar.Clone(XVar.Array());
			tableDescriptor = XVar.Clone(_findDalTable((XVar)(table)));
			if(XVar.Pack(tableDescriptor))
			{
				dynamic db_table_info = XVar.Array();
				tableInfo.InitAndSetArrayItem(db_table_info[tableDescriptor["varname"]], "fields");
				if(XVar.Pack(tableDescriptor["schema"]))
				{
					tableInfo.InitAndSetArrayItem(MVCFunctions.Concat(tableDescriptor["schema"], ".", tableDescriptor["name"]), "fullName");
				}
				else
				{
					tableInfo.InitAndSetArrayItem(tableDescriptor["name"], "fullName");
				}
			}
			else
			{
				dynamic fieldList = XVar.Array(), helpSql = null, tables = null;
				if(XVar.Pack(GlobalVars.tableinfo_cache[connId].KeyExists(table)))
				{
					return GlobalVars.tableinfo_cache[connId][table];
				}
				tables = XVar.Clone(CurrentConnection().getTableList());
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(table), (XVar)(tables)))))
				{
					GlobalVars.tableinfo_cache.InitAndSetArrayItem(false, connId, table);
					return false;
				}
				helpSql = XVar.Clone(MVCFunctions.Concat("select * from ", CurrentConnection().addTableWrappers((XVar)(table)), " where 1=0"));
				tableInfo.InitAndSetArrayItem(table, "fullName");
				tableInfo.InitAndSetArrayItem(XVar.Array(), "fields");
				fieldList = XVar.Clone(CurrentConnection().getFieldsList((XVar)(helpSql)));
				foreach (KeyValuePair<XVar, dynamic> f in fieldList.GetEnumerator())
				{
					tableInfo.InitAndSetArrayItem(new XVar("type", f.Value["type"], "name", f.Value["fieldname"]), "fields", f.Value["fieldname"]);
				}
				GlobalVars.tableinfo_cache.InitAndSetArrayItem(tableInfo, connId, table);
			}
			return tableInfo;
		}
		protected static XVar _fillTablesList()
		{
			GlobalVars.conn = XVar.Clone(CurrentConnection());
			if(XVar.Pack(GlobalVars.dalTables[GlobalVars.conn.connId]))
			{
				return null;
			}
			GlobalVars.dalTables.InitAndSetArrayItem(XVar.Array(), GlobalVars.conn.connId);
			if("azfo_at_radamakerlaptop" == GlobalVars.conn.connId)
			{
				GlobalVars.dalTables.InitAndSetArrayItem(new XVar("name", "_ABCReports", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCReports", "altvarname", "tbl_ABCReports", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"), GlobalVars.conn.connId, null);
				GlobalVars.dalTables.InitAndSetArrayItem(new XVar("name", "_ABCSecurity", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity", "altvarname", "tbl_ABCSecurity", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"), GlobalVars.conn.connId, null);
				GlobalVars.dalTables.InitAndSetArrayItem(new XVar("name", "_ABCVotes", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCVotes", "altvarname", "tbl_ABCVotes", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"), GlobalVars.conn.connId, null);
				GlobalVars.dalTables.InitAndSetArrayItem(new XVar("name", "vwABCReportsVoteCount", "varname", "azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount", "altvarname", "vwABCReportsVoteCount", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"), GlobalVars.conn.connId, null);
			}
			return null;
		}
		public static XVar PrepareSQL(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			dynamic context = null, offsetShift = null, replacements = XVar.Array(), tokens = XVar.Array();
			GlobalVars.conn = XVar.Clone(CurrentConnection());
			context = XVar.Clone(RunnerContext.current());
			tokens = XVar.Clone(scanTokenString((XVar)(sql)));
			replacements = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> match in tokens["matches"].GetEnumerator())
			{
				dynamic offset = null, repl = XVar.Array(), token = null;
				offset = XVar.Clone(tokens["offsets"][match.Key]);
				token = XVar.Clone(tokens["tokens"][match.Key]);
				repl = XVar.Clone(new XVar("offset", offset, "len", MVCFunctions.strlen((XVar)(match.Value))));
				if(XVar.Pack(GlobalVars.conn.positionQuoted((XVar)(sql), (XVar)(offset))))
				{
					repl.InitAndSetArrayItem(GlobalVars.conn.addSlashes((XVar)(context.getValue((XVar)(token)))), "insert");
				}
				else
				{
					repl.InitAndSetArrayItem(prepareNumberValue((XVar)(context.getValue((XVar)(token)))), "insert");
				}
				replacements.InitAndSetArrayItem(repl, null);
			}
			offsetShift = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> r in replacements.GetEnumerator())
			{
				sql = XVar.Clone(MVCFunctions.substr_replace((XVar)(sql), (XVar)(r.Value["insert"]), (XVar)(r.Value["offset"] + offsetShift), (XVar)(r.Value["len"])));
				offsetShift += MVCFunctions.strlen((XVar)(r.Value["insert"])) - r.Value["len"];
			}
			return sql;
		}
		public static XVar readSQLTokens(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			dynamic arr = XVar.Array();
			arr = XVar.Clone(scanTokenString((XVar)(sql)));
			return arr["tokens"];
		}
		public static XVar readMasterTokens(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			dynamic allTokens = XVar.Array(), masterTokens = XVar.Array();
			masterTokens = XVar.Clone(XVar.Array());
			allTokens = XVar.Clone(readSQLTokens((XVar)(sql)));
			foreach (KeyValuePair<XVar, dynamic> token in allTokens.GetEnumerator())
			{
				dynamic dotPos = null;
				dotPos = XVar.Clone(MVCFunctions.strpos((XVar)(token.Value), new XVar(".")));
				if((XVar)(!XVar.Equals(XVar.Pack(dotPos), XVar.Pack(false)))  && (XVar)(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(token.Value), new XVar(0), (XVar)(dotPos)))) == "master"))
				{
					masterTokens.InitAndSetArrayItem(token.Value, null);
				}
			}
			return masterTokens;
		}
		protected static XVar scanTokenString(dynamic _param_sql)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			#endregion

			dynamic matches = XVar.Array(), offsets = XVar.Array(), pattern = null, result = XVar.Array(), tokens = XVar.Array();
			tokens = XVar.Clone(XVar.Array());
			offsets = XVar.Clone(XVar.Array());
			matches = XVar.Clone(XVar.Array());
			pattern = new XVar("/(?:[^\\w\\:]|^)(\\:([a-zA-Z_]{1}[\\w\\.]*))|\\:\\{(.*?)\\}/");
			result = XVar.Clone(MVCFunctions.findMatches((XVar)(pattern), (XVar)(sql)));
			foreach (KeyValuePair<XVar, dynamic> m in result.GetEnumerator())
			{
				if(m.Value["submatches"][0] != "")
				{
					matches.InitAndSetArrayItem(m.Value["submatches"][0], null);
					tokens.InitAndSetArrayItem(m.Value["submatches"][1], null);
					offsets.InitAndSetArrayItem(m.Value["offset"] + MVCFunctions.strpos((XVar)(m.Value["match"]), (XVar)(m.Value["submatches"][0])), null);
				}
				else
				{
					matches.InitAndSetArrayItem(m.Value["match"], null);
					tokens.InitAndSetArrayItem(m.Value["submatches"][2], null);
					offsets.InitAndSetArrayItem(m.Value["offset"], null);
				}
			}
			return new XVar("tokens", tokens, "matches", matches, "offsets", offsets);
		}
		public static XVar prepareNumberValue(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic strvalue = null;
			strvalue = XVar.Clone(value);
			if(XVar.Pack(MVCFunctions.IsNumeric(strvalue)))
			{
				return MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(strvalue));
			}
			return 0;
		}
	}
}
