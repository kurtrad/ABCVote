using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Dynamic;
using System.ComponentModel.Composition;
using System.Web;
using runnerDotNet;

namespace runnerDotNet
{
	[Export(typeof(IEventProviderCS_dbo__ABCVotes))]
	public class eventclassCS_dbo__ABCVotes : IEventProviderCS_dbo__ABCVotes
	{
		// Captchas functions


		//	handlers
// Before record added
		public XVar BeforeAdd(dynamic values, ref dynamic message, dynamic inline, dynamic pageObject)
		{

//**********  Check if specific record exists  ************
			
			string recordValue = values["record"];
			string committeeMemberValue = values["committee_member"];
			inline=true;

			string strSQLExists = string.Format("select * from dbo._ABCVotes where [record]= '{0}' and [committee_member] = '{1}'", recordValue, committeeMemberValue);
			XVar rsExists = CommonFunctions.db_query(strSQLExists, null);
			XVar data = CommonFunctions.db_fetch_array(rsExists);
			if (data)
			{
				 // if record exists do something
				 message = string.Format("voting for committee member {0} already exists", committeeMemberValue);
		
				 return false;
			}
			else
			{
				 // if dont exist do something else
				 return true;
			}


// Place event code here.
// Use 

return true;
return null;

		} // BeforeAdd


		//	onscreen events





	}
}