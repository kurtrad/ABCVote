using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using runnerDotNet;
namespace runnerDotNet
{
	public partial class CommonFunctions
	{
        public static void LoadLanguages()
        {
            switch(((XVar)CommonFunctions.mlang_getcurrentlang()).ToString())
            {
		        case "English":
			        CommonFunctions.LoadLanguage_English();
		            break;
                default:
                    break;
            }
        }

		public static XVar mlang_message(XVar tag)
		{
			return GlobalVars.mlang_messages[CommonFunctions.mlang_getcurrentlang()][tag];
		}
	}
}
