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
	[Export(typeof(IGlobalEventProviderCS))]
	public class CommonEvents : IGlobalEventProviderCS
	{

	// Captchas functions

		// handlers

		// onscreen events

		// global captchas, buttons

		// table maps, buttons




		public XVar AfterTableInit(dynamic context)
		{
			var table = context["table"];
			var query = context["query"];
			if(table == "ABC_Voting_My_Voting")
			{

	//var strUpdate = string.Format("record NOT IN (Select record From _ABCVotes Where [_ABCVotes].[record] =[_ABCReports].[record] and committee_member = '{0}')", XSession.Session["UserID"]);

	//var strUpdate = string.Format("record NOT IN (Select record From _ABCVotes Where [_ABCVotes].[record] =[_ABCReports].[record] and committee_member = '{0}') AND EXISTS (Select * from _ABCSecurity where [_ABCSecurity].[username] = '{0}' and [_ABCReports].[votingCycle] between [_ABCSecurity].[byear] and [_ABCSecurity].[eyear])", XSession.Session["UserID"]);
	
var strUpdate = string.Format("record NOT IN (Select record From _ABCVotes Where [_ABCVotes].[record] =[_ABCReports].[record] and committee_member = '{0}') AND EXISTS (Select * from _ABCSecurity where [_ABCSecurity].[username] = '{0}' and [_ABCReports].[votingCycle] between [_ABCSecurity].[byear] and [_ABCSecurity].[eyear]) and (SELECT COUNT(id) FROM _ABCVotes Where [_ABCVotes].[record] = [_ABCReports].[record]) < 8", XSession.Session["UserID"]);
	
	query.addWhere(strUpdate);


// Place event code here.
// Use 
// Place event code here.
// Use 
			}
			context["query"] = query;
			return null;
		}

		public XVar GetTablePermissions(dynamic permissions, dynamic table = null)
		{
			return permissions;
		}

		public XVar IsRecordEditable(dynamic values, dynamic isEditable, dynamic table = null)
		{
			return isEditable;
		}
	}
}