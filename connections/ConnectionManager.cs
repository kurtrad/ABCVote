using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using runnerDotNet;

namespace runnerDotNet
{
	public class ConnectionManager : XClass
	{
		private Dictionary<string, Connection> cache = new Dictionary<string, Connection>();
		
		private XVar _connectionsData = XVar.Array();
		private XVar _connectionsIdByName = XVar.Array();
		
		protected Dictionary<string, string> _tablesConnectionIds = new Dictionary<string, string>();
		
		public ConnectionManager()
		{
			_setConnectionsData();
			_setTablesConnectionIds();
		}
		
		public Connection byTable(XVar tName)
		{
			var connId = _tablesConnectionIds.ContainsKey(tName) ? _tablesConnectionIds[tName] : "";
			return byId(connId);
		}
		
		public Connection byId(XVar connId)
		{
			if(!cache.ContainsKey(connId))
				cache[connId] = getConnection(connId);

			return cache[connId];
		}

		public Connection byName(XVar connName)
		{
			XVar connId = _connectionsIdByName[ connName ];
			
			if( !cache.ContainsKey( connId ) )
				cache[ connId ] = getConnection( connId );

			return cache[ connId ];
		}
		
		public Connection getDefault()
		{
			return byId("azfo_at_radamakerlaptop");
		}
		
		public Connection getForLogin()
		{
			return byId( "azfo_at_radamakerlaptop" );
		}
	
		public Connection getForAudit()
		{
			return getDefault();
		}
	
		public Connection getForLocking()
		{
			return getDefault();
		}
	
		public Connection getForUserGroups()
		{
			return getDefault();
		}

		public Connection getForSavedSearches()
		{
			return getDefault();
		}

		public Connection getForWebReports()
		{
			return getDefault();
		}
		
		private Connection getConnection(XVar connId)
		{
			if(connId == "")
				return getDefault();
			
			XVar data = _connectionsData[connId];	
			
			switch(data["connStringType"].ToString())
			{
				case "mssql":
						return new MSSQLConnection(data);
				case "msaccess":
				case "odbc":
				case "odbcdsn":
				case "custom":
				case "file":
				{
					String firstClause = GlobalVars.ConnectionStrings[connId].ToString().Substring(0,9).ToUpper();
					if(  firstClause == "PROVIDER=" )
						return new OLEDBConnection(data);
					else
						return new ODBCConnection(data);
				}
				default:
					return null;
			}
		}
		
		/**
		 * Set the data representing the project's 
		 * db connection properties
		 */	 
		protected void _setConnectionsData()
		{
			// content of this function can be modified on demo account
			// variable names data and connectionsData are important
			
			var connectionsData = XVar.Array();
			XVar data;
			data = XVar.Array();
			data["dbType"] = "2";
			data["connStringType"] = "mssql";
			data["connId"] = "azfo_at_radamakerlaptop";
			data["connName"] = "azfo at DESKTOP-L3H49HJ\\SQLEXPRESS";
			data["leftWrap"] = "[";
			data["rightWrap"] = "]";
			
			_connectionsIdByName["azfo at DESKTOP-L3H49HJ\\SQLEXPRESS"] = "azfo_at_radamakerlaptop";
			
			data["EncryptInfo"] = XVar.Array();
				data["EncryptInfo"]["mode"] = 0;
			data["EncryptInfo"]["alg"]  = 128;
			data["EncryptInfo"]["key"]  = "";
					data["EncryptInfo"]["slqserverkey"] = "";
			data["EncryptInfo"]["slqservercert"] = "";

			connectionsData["azfo_at_radamakerlaptop"] = data;
			_connectionsData = connectionsData;
		}
		
		/**
		 * Set the data representing the correspondence between 
		 * the project's table names and db connections
		 */	 
		protected void _setTablesConnectionIds()
		{
			_tablesConnectionIds = new Dictionary<string, string>();
			_tablesConnectionIds["dbo._ABCReports"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["dbo._ABCVotes"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["dbo._ABCSecurity"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["ABC_Voting_Submitted"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["ABC_Voting_Recirculated"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["ABC_Voting_My_Voting"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["ABC_Voting_Batch_Create"] = "azfo_at_radamakerlaptop";
			_tablesConnectionIds["dbo.vwABCReportsVoteCount"] = "azfo_at_radamakerlaptop";
		}
		
		public XVar checkTablesSubqueriesSupport(XVar dataSourceTName1, XVar dataSourceTName2)
		{
			var connId1 = _tablesConnectionIds[dataSourceTName1];
			var connId2 = _tablesConnectionIds[dataSourceTName2];

			if(connId1 != connId2)
				return false;

			if(_connectionsData[connId1]["dbType"] == Constants.nDATABASE_Access && dataSourceTName1 == dataSourceTName2)
				return false;

			return true;
		}
		
		public XVar CloseConnections() 
		{
			foreach(var con in cache.Values)
			{
				con.close();
			}
			return null;
		}
	}
}