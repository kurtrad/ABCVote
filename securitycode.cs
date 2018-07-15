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
	public partial class GlobalController : BaseController
	{
		//Modify the randstring variable below to customize your security code.
		//- Use 'num' for numbers only
		//- Use 'alphanum' for numbers and letters
		//- Use 'secure' for numbers, letter and special characters
		//Modify the number in the string to reflect how many characters you want your security code to be

		public string securitycode()
		{
			var res = randString("alphanum", 6);
			XSession.Session["captcha_"+Request.QueryString["id"]] = res;
			return "&securitycode=" + res + "&";
		}

		public string randString(string mode, int len)
		{
			var randStr = "";
			var randNum = 0;
			var useList = "";
			var alpha = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
			var secure = "!,@,$,%,&,*,-,_,=,+,?,~";
			if (mode == "alpha")
			{
				randNum = 52;
				useList = alpha;
			}
			else if (mode == "alphanum")
			{
				randNum = 62;
				useList = alpha + ",1,2,3,4,5,6,7,8,9";
			}
			else if (mode == "secure")
			{
				randNum = 73;
				useList = alpha + ",0,1,2,3,4,5,6,7,8,9," + secure;
			}
			else
			{
				randNum = 10;
				useList = "0,1,2,3,4,5,6,7,8,9";
			}

			var arr = useList.Split(',');
			randNum = arr.Length;
			Random rnd = new Random();
			for (int i = 0; i < len; i++)
			{
				randStr = randStr + arr[rnd.Next(randNum - 1)];
			}
			return randStr;
		}
	}
}