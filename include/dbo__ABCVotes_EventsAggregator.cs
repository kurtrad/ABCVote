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
	public interface IEventProviderCS_dbo__ABCVotes
	{
		// Captchas functions

		//	handlers

		XVar BeforeAdd(dynamic values, ref dynamic message, dynamic inline, dynamic pageObject);


		//	onscreen events




	}

	public interface IEventProviderVB_dbo__ABCVotes
	{
		// Captchas functions

		//	handlers

		//	onscreen events




	}

	public class eventclass_dbo__ABCVotes : EventsAggregatorBase
	{
		[Import(typeof(IEventProviderCS_dbo__ABCVotes))]
		public IEventProviderCS_dbo__ABCVotes EventProviderCS;

		[Import(typeof(IEventProviderVB_dbo__ABCVotes))]
		public IEventProviderVB_dbo__ABCVotes EventProviderVB;

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

		public eventclass_dbo__ABCVotes()
		{
			DoImport();

			// fill list of events

			events["BeforeAdd"]=true;


			//	onscreen events
		}

		// Captchas functions


		//	handlers

		
		public XVar BeforeAdd(dynamic values, ref dynamic message, dynamic inline, dynamic pageObject)
		{
			return EventProviderCS.BeforeAdd(values, ref message, inline, pageObject);
		}


		//	onscreen events





	}
}