using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Dynamic;
using System.ComponentModel.Composition;
using runnerDotNet;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Web;
using System.Reflection;

namespace runnerDotNet
{
	public interface IEventProviderCS_ABC_Voting_My_Voting
	{
		// Captchas functions

		//	handlers

		//	onscreen events




	}

	public interface IEventProviderVB_ABC_Voting_My_Voting
	{
		// Captchas functions

		//	handlers

		//	onscreen events




	}

	public class eventclass_ABC_Voting_My_Voting : EventsAggregatorBase
	{
		[Import(typeof(IEventProviderCS_ABC_Voting_My_Voting))]
		public IEventProviderCS_ABC_Voting_My_Voting EventProviderCS;

		[Import(typeof(IEventProviderVB_ABC_Voting_My_Voting))]
		public IEventProviderVB_ABC_Voting_My_Voting EventProviderVB;

		public void callCAPTCHA(dynamic pageObject)
		{
			RunnerPage page = XVar.UnPackRunnerPage(pageObject);

			if(page.pageType == "add")
			{
			}
			else if(page.pageType == "edit")
			{
			}
		}

		public eventclass_ABC_Voting_My_Voting()
		{
			DoImport();

			// fill list of events

			events["AfterTableInit"]=true;


			//	onscreen events
		}

		// Captchas functions


		//	handlers

		//	onscreen events





	}
}