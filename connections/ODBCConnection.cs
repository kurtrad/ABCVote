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
	public class ODBCConnection : Connection
	{
		public ODBCConnection(XVar parameters) : base(parameters)
		{ }


		protected override ConnectionsPool GetConnectionsPool(string connStr)
		{
			return new ODBCConnectionPool(connStr);
		}

		protected override DbCommand GetCommand()
		{
			return new System.Data.Odbc.OdbcCommand();
		}

		public override XVar queryPage(XVar strSQL, XVar pageStart, XVar pageSize, XVar applyLimit)
		{
			if( applyLimit )
				strSQL = CommonFunctions.applyDBrecordLimit((XVar)strSQL, (XVar)pageStart * pageSize, (XVar)(this.dbType));

			var qResult = query( strSQL );
			qResult.seekPage( pageSize, pageStart );

			return qResult;
		}
	}
}