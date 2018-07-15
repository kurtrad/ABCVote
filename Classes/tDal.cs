using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web;

namespace runnerDotNet
{
    public class tDAL : XClass
    {

        public static XVar CustomQuery(XVar dalSQL)
        {
	    var connection = CommonFunctions.getDefaultConnection();
            dynamic data = connection.query(dalSQL);
            if (data!=null)
                return data.getQueryHandle();
            else
                return null;
        }

        public static XVar UsersTableName()
        {
			var connection = GlobalVars.cman.byId( "azfo_at_radamakerlaptop" );
			return connection.addTableWrappers("dbo._ABCSecurity");
        }

        public static XVar DBLookup(XVar sql)
		{
			var connection = CommonFunctions.getDefaultConnection();
			XVar data = connection.query(sql).fetchAssoc();
			if (data)
				return data.GetEnumerator().First().Value; // return first value

			return null;
		}

	    public XVar lstTables = XVar.Array();

        public XVar tObjects = XVar.Array();

        public void FillTablesList()
        {
            if(this.lstTables.Count() > 0)
            {
                return;
            }
			
			this.lstTables.Add(new XVar("name", "_ABCReports", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCReports", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"));
			this.lstTables.Add(new XVar("name", "_ABCSecurity", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCSecurity", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"));
			this.lstTables.Add(new XVar("name", "_ABCVotes", "varname", "azfo_at_radamakerlaptop_dbo_tbl_ABCVotes", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"));
			this.lstTables.Add(new XVar("name", "vwABCReportsVoteCount", "varname", "azfo_at_radamakerlaptop_dbo_vwABCReportsVoteCount", "connId", "azfo_at_radamakerlaptop", "schema", "dbo", "connName", "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"));
        }

        /// <summary>
        /// Returns table object by table name
        /// </summary>
        /// <param name="strTable">Table name</param>
        /// <returns></returns>
        public dynamic Table( XVar strTable, string schema = "", string connName = "" )
	    {
		    this.FillTablesList();
		    foreach(var tbl in this.lstTables.GetEnumerator())
		    {
			    if(MVCFunctions.strtoupper(strTable) == MVCFunctions.strtoupper(tbl.Value["name"]) &&
					( schema == "" || MVCFunctions.strtoupper(schema) == MVCFunctions.strtoupper(tbl.Value["schema"]) ) &&
					( connName == "" || MVCFunctions.strtoupper(connName) == MVCFunctions.strtoupper(tbl.Value["connName"]) ) )
			    {
				    this.CreateClass(tbl.Value);
				    return this.tObjects[tbl.Value["varname"]];
			    }
		    }
            //	check table names without dbo. and other prefixes
		    foreach(var tbl in this.lstTables.GetEnumerator())
		    {
			    if(MVCFunctions.strtoupper(tDAL.cutprefix(strTable)) == MVCFunctions.strtoupper(tDAL.cutprefix(tbl.Value["name"])))
			    {
				    this.CreateClass(tbl.Value);
				    return this.tObjects[tbl.Value["varname"]];
			    }
		    }
		    return null;
	    }

        public void CreateClass(XVar tbl)
	    {
		    if(this.tObjects[tbl["varname"]])
            {
			    return;
            }
            //	load table info

            XVar classname = "runnerDotNet.dalTable_" + tbl["varname"].ToString();
            Type tableType = Type.GetType(classname);
		    tableType.GetMethod("Init", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
        
            //	create class and object
			
			// TODO: init connection somewhere?

            this.tObjects[tbl["varname"]] = (XVar)Activator.CreateInstance(tableType);
			((tDALTable)this.tObjects[tbl["varname"]]).setConnection( tbl["connId"] );
	    }

        /// <summary>
        /// Returns list of table names
        /// </summary>
        /// <returns></returns>
        public XVar GetTablesList()
	    {
		    this.FillTablesList();
		    XVar res = XVar.Array();
		    foreach(var tbl in this.lstTables.GetEnumerator())
		    {
			    res.Add(tbl.Value["name"]);
            }
		    return res;
        }

        /// <summary>
        /// Get list of table fields by table name
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public XVar GetFieldsList(XVar table)
	    {
		    tDALTable tbl = this.Table(table);
		    return tbl.GetFieldsList();
	    }

        /// <summary>
        /// Get field type by table name and field name
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public XVar GetFieldType(XVar table, XVar field)
	    {
		    tDALTable tbl = this.Table(table);
		    return tbl.GetFieldType(field);
	    }

        /// <summary>
        /// Get table key fields by table name
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public XVar GetDBTableKeys(XVar table)
	    {
		    tDALTable tbl = this.Table(table);
		    return tbl.GetDBTableKeys();
	    }


        public static XVar cutprefix(XVar table)
        {
	        XVar pos = MVCFunctions.strpos(table, ".");
	        if(pos.Equals(false))
            {
		        return table;
            }
	        return MVCFunctions.substr(table, pos + 1);
        }
    }

    /// <summary>
    /// Data Access Layer table class
    /// </summary>
    public class tDALTable : XClass
    {
	    public XVar m_TableName;
	    public XVar m_infoKey;
	    public XVar Param = XVar.Array();
	    public XVar Value = XVar.Array();
		public Connection _connection;

		public void setConnection(XVar connId)
		{
			_connection = GlobalVars.cman.byId(connId);
		}
		
        public XVar GetDBTableKeys()
	    {
		    if(!GlobalVars.dal_info.KeyExists(this.m_infoKey) || ! MVCFunctions.is_array(GlobalVars.dal_info[this.m_infoKey]))
		    {
			    return XVar.Array();
		    }
		    XVar ret = XVar.Array();
		    foreach(var f in GlobalVars.dal_info[this.m_infoKey].GetEnumerator())
		    {
			    if(f.Value["key"] != null)
                {
				    ret.Add(f.Key);
                }
		    }
		    return ret;
	    }

        public XVar GetFieldsList()
	    {
		    return GlobalVars.dal_info[this.m_infoKey].ArrayKeys();
	    }

        public XVar GetFieldType(XVar field)
	    {
		    if(!GlobalVars.dal_info.KeyExists(this.m_infoKey))
            {
                return 200;
            }
		    return GlobalVars.dal_info[this.m_infoKey][field]["type"];
	    }

        public XVar PrepareValue(XVar value, XVar type)
	    {
		
			if(_connection.dbType == Constants.nDATABASE_Oracle)
			{
				if(CommonFunctions.IsBinaryType(type))
				{
					return "EMPTY_BLOB()";
				}
			}
	
		    if(CommonFunctions.IsDateFieldType(type))
            {
				return _connection.addDateQuotes(value);
            }
		    if (CommonFunctions.NeedQuotes(type))
			    return _connection.prepareString(value);
		    else
			{
				if(XVar.Pack(MVCFunctions.IsNumeric(value)))
				{
					return MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(value));
				}
				else
				{
					return 0;
				}
			}
	    }

        /// <summary>
        /// Get table name
        /// </summary>
        /// <returns></returns>
        public XVar TableName()
	    {
		    return _connection.addTableWrappers(this.m_TableName);
	    } 

        public void Execute_Query(XVar blobs, XVar dalSQL, XVar tableinfo)
	    {
			XVar blobTypes = XVar.Array();
			_connection.execWithBlobProcessing( dalSQL, blobs, blobTypes );
	    }

        /// <summary>
        /// Add new record to the table
        /// </summary>
        public void Add() 
	    {
		    XVar insertFields = "";
		    XVar insertValues = "";
		    XVar tableinfo = GlobalVars.dal_info[this.m_infoKey];
		    XVar blobs = XVar.Array();
    //	prepare parameters		
		    foreach(var f in tableinfo.GetEnumerator())
		    {
                var fieldInfo = this.GetType().GetField(f.Value["varname"]);
			    if(fieldInfo != null && fieldInfo.GetValue(this) != null)
			    {
				    this.Value[f.Key] = fieldInfo.GetValue(this);
			    }
			    foreach(var fieldValue in this.Value.GetEnumerator())
			    {
				    if (MVCFunctions.strtoupper(fieldValue.Key) != MVCFunctions.strtoupper(f.Key))
					    continue;
						
				    insertFields = MVCFunctions.Concat(insertFields, _connection.addFieldWrappers(f.Key), ",");
				    insertValues = MVCFunctions.Concat(insertValues, this.PrepareValue(fieldValue.Value, f.Value["type"]), ",");
					if(_connection.dbType == Constants.nDATABASE_Oracle)
					{
						if(CommonFunctions.IsBinaryType(f.Value["type"]))
						{
							blobs[fieldValue.Key] = fieldValue.Value;
						}
					}
				    break;
			    }
		    }
    //	prepare and exec SQL
		    if (insertFields != "" && insertValues != "")
		    {
			    insertFields = MVCFunctions.substr(insertFields, 0, -1);
			    insertValues = MVCFunctions.substr(insertValues, 0, -1);
			
				
			    XVar dalSQL = MVCFunctions.Concat("insert into ", _connection.addTableWrappers(this.m_TableName), " (", insertFields, ") values (", insertValues, ")");
			    this.Execute_Query(blobs, dalSQL, tableinfo);
				
		    }
    //	cleanup		
	        this.Reset();
	    }

        /// <summary>
        /// Query all records from the table
        /// </summary>
        /// <returns></returns>
        public XVar QueryAll()
	    {
		    XVar dalSQL = MVCFunctions.Concat("select * from ", _connection.addTableWrappers(this.m_TableName));
		    return _connection.query(dalSQL).getQueryHandle();
	    }

        /// <summary>
        /// Do a custom query on the table
        /// </summary>
        /// <param name="swhere"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public XVar Query(XVar swhere = null, XVar orderby = null)
	    {
		    if (swhere)
            {
			    swhere = MVCFunctions.Concat(" where ", swhere);
            }
		    if (orderby)
            {
			    orderby = MVCFunctions.Concat(" order by ", orderby);
            }
		    XVar dalSQL = MVCFunctions.Concat("select * from ", _connection.addTableWrappers(this.m_TableName), swhere, orderby);
		    return _connection.query(dalSQL).getQueryHandle();
	    }

        /// <summary>
        /// Delete a record from the table
        /// </summary>
        /// <returns></returns>
        public void Delete()
	    {
		    XVar deleteFields = "";
		    XVar tableinfo = GlobalVars.dal_info[this.m_infoKey];
    //	prepare parameters		
		    foreach(var f in tableinfo.GetEnumerator())
		    {
                var fieldInfo = this.GetType().GetField(f.Value["varname"]);
			    if(fieldInfo != null && fieldInfo.GetValue(this) != null)
			    {
				    this.Param[f.Key] = fieldInfo.GetValue(this);
			    }
			    foreach(var paramValue in this.Param.GetEnumerator())
			    {
				    if (MVCFunctions.strtoupper(paramValue.Key) != MVCFunctions.strtoupper(f.Key))
                    {
					    continue;
                    }
				    deleteFields = MVCFunctions.Concat(deleteFields, _connection.addFieldWrappers(f.Key), "=", this.PrepareValue(paramValue.Value, f.Value["type"]), " and ");
				    break;
			    }
		    }
    //	do delete
		    if (deleteFields)
		    {
			    deleteFields = MVCFunctions.substr(deleteFields,0,-5);
			    XVar dalSQL = MVCFunctions.Concat("delete from ", _connection.addTableWrappers(this.m_TableName), " where ", deleteFields);
			    _connection.exec(dalSQL);
		    }
	
    //	cleanup
	        this.Reset();
	    }

        /// <summary>
        /// Reset table object
        /// </summary>
        public void Reset()
	    {
		    this.Value = XVar.Array();
		    this.Param = XVar.Array();
		    XVar tableinfo = GlobalVars.dal_info[this.m_infoKey];
    //	prepare parameters		
		    foreach(var f in tableinfo.GetEnumerator())
		    {
                var fieldInfo = this.GetType().GetField(f.Value["varname"]);
			    if(fieldInfo != null)
			    {
				    fieldInfo.SetValue(this, null);
			    }
		    }
	    }

        /// <summary>
        /// Update record in the table
        /// </summary>
        public void Update()
	    {
		    XVar tableinfo = GlobalVars.dal_info[this.m_infoKey];
		    XVar updateParam = "";
		    XVar updateValue = "";
		    XVar blobs = XVar.Array();

		    foreach(var f in tableinfo.GetEnumerator())
		    {
                var fieldInfo = this.GetType().GetField(f.Value["varname"]);
                if(fieldInfo != null && fieldInfo.GetValue(this) != null)
                {
                    if(f.Value["key"] != null)
                    {
                        this.Param[ f.Value["varname"] ] = fieldInfo.GetValue(this);
                    }
                    else
                    {
                        this.Value[ f.Value["varname"] ] = fieldInfo.GetValue(this);
                    }
                }
			    if(f.Value["key"] == null && !MVCFunctions.array_change_key_case(this.Param, Constants.CASE_UPPER).KeyExists(MVCFunctions.strtoupper(f.Key)))
			    {
				    foreach(var fieldValue in this.Value.GetEnumerator())
				    {
					    if (MVCFunctions.strtoupper(fieldValue.Key) != MVCFunctions.strtoupper(f.Key))
                        {
						    continue;
                        }
					    updateValue = MVCFunctions.Concat(updateValue, _connection.addFieldWrappers(f.Key), "=", this.PrepareValue(fieldValue.Value, f.Value["type"]), ", ");
						
						if(_connection.dbType == Constants.nDATABASE_Oracle)
						{
							if(CommonFunctions.IsBinaryType(f.Value["type"]))
							{
								blobs[f.Key] = fieldValue.Value;
							}
						}
					    break;
				    }
			    }
			    else
			    {
				    foreach(var fieldValue in this.Param.GetEnumerator())
				    {
					    if (MVCFunctions.strtoupper(fieldValue.Key) != MVCFunctions.strtoupper(f.Key))
                        {
						    continue;
                        }
					    updateParam = MVCFunctions.Concat(updateParam, _connection.addFieldWrappers(f.Key), "=", this.PrepareValue(fieldValue.Value, f.Value["type"]) , " and ");
					    break;
				    }
			    }
		    }

    //	construct SQL and do update	
		    if (updateParam)
            {
			    updateParam = MVCFunctions.substr(updateParam,0,-5);
            }
		    if (updateValue)
            {
			    updateValue = MVCFunctions.substr(updateValue,0,-2);
            }
		    if (updateValue && updateParam)
		    {
			    XVar dalSQL = MVCFunctions.Concat("update ", _connection.addTableWrappers(this.m_TableName), " set ", updateValue, " where ", updateParam);
			    this.Execute_Query(blobs, dalSQL, tableinfo);
		    }

    //	cleanup
		    this.Reset();
	    }

        public XVar FetchByID()
	    {
		    XVar tableinfo = GlobalVars.dal_info[this.m_infoKey];

		    XVar dal_where = "";
		    foreach(var f in tableinfo.GetEnumerator())
		    {
                var fieldInfo = this.GetType().GetField(f.Value["varname"]);
                if(fieldInfo != null && fieldInfo.GetValue(this) != null)
                {
                    this.Value[ f.Value["varname"] ] = fieldInfo.GetValue(this);
                }
			    foreach(var fieldValue in this.Param.GetEnumerator())
			    {
				    if (MVCFunctions.strtoupper(fieldValue.Key) != MVCFunctions.strtoupper(f.Key))
                    {
				        continue;
                    }
				    dal_where =  MVCFunctions.Concat(_connection.addFieldWrappers(f.Key), "=", this.PrepareValue(fieldValue.Value, f.Value["type"]), " and ");
				    break;
			    }
		    }
		    // cleanup
		    this.Reset();
		    // construct and run SQL
		    if (dal_where)
            {
			    dal_where =  MVCFunctions.Concat(" where ", MVCFunctions.substr(dal_where,0,-5));
            }
		    XVar dalSQL =  MVCFunctions.Concat("select * from ", _connection.addTableWrappers(this.m_TableName), dal_where);
		    return _connection.query(dalSQL).getQueryHandle();
	    }
    }
}