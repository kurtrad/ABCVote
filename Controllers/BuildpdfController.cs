using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace runnerDotNet
{
    public partial class GlobalController : BaseController
    {
        public ActionResult Buildpdf()
        {
			// split /controller/action and ?param=some
			var str = Request.QueryString["url"];
			var quotePos = str.IndexOf("?");
			var qstr = quotePos >= 0 ? str.Substring(0, quotePos) : str;
			var rstr = quotePos >= 0 ? str.Substring(quotePos + 1, str.Length - quotePos - 1) : "";

			// get controller and action from route
			var request = new HttpRequest(null, qstr, rstr);
			var response = new HttpResponse(new StringWriter());
			var httpContext = new HttpContext(request, response);
			var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

			// replace GET string 
			HttpContext.RewritePath(MVCFunctions.GetTableLink(routeData.Values["controller"].ToString(), routeData.Values["action"].ToString(), rstr));

			// make actual request
			System.IO.TextWriter writer = new System.IO.StringWriter();
			var html = new HtmlHelper(new ViewContext(ControllerContext, new FakeView(), new ViewDataDictionary(), new TempDataDictionary(), writer), new System.Web.Mvc.ViewPage());
			System.Web.Mvc.Html.ChildActionExtensions.RenderAction(html, routeData.Values["action"].ToString(), routeData.Values["controller"].ToString());
			var res = writer.ToString();

			// remove "pdf" link
			res = RemovePdfLink(res);
			// replace images, add css and return result
			res = getImage(res);
			res = BuildCSSForInlineStyles(res);
			byte[] bytes = WKHtmlToPdf(res);

			return File(bytes, "application/pdf");
		}
		
		private string RemovePdfLink(string html)
		{
			// fast search. no regex

			int pos1 = html.IndexOf("rnr-b-printpdf");
			
			if (pos1 == -1)
				pos1 = html.IndexOf("pdflink");
			
			if (pos1 == -1)
				return html;
				
			int pos2 = html.LastIndexOf("<div", pos1, StringComparison.CurrentCultureIgnoreCase);
			if (pos2 == -1)
				return html;

			int pos3 = html.IndexOf("</div>", pos1, StringComparison.CurrentCultureIgnoreCase);
			if (pos3 == -1)
				return html;

			return html.Substring(0, pos2) + html.Substring(pos3 + 6);
		}

        private string BuildCSSForInlineStyles(string html)
        {
            WebClient wc = new WebClient();
            StringBuilder styles = new StringBuilder();
			string pattern = @"<link rel=""stylesheet""\s*(?:type=""text/css"")?\s*href=""(?'link'[^""]*)""\s*(?:type=""text/css"")?\s*/>";
            foreach (Match match in Regex.Matches(html, pattern, RegexOptions.IgnoreCase))
            {
                string styleblock = match.Groups["link"].Value;
                if (styleblock.IndexOf("http") != 0)
                {
                    styleblock = Request.Url.Scheme + "://" + Request.Url.Authority + styleblock;
                }
                styles.Append(wc.DownloadString(styleblock));
            }
            // Remove the style block(s)
            html = Regex.Replace(html, pattern, "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<script([^<]*)</script>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<meta([^<]*)/>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "</head>", string.Format("<style type=\"text/css\">{0}</style></head>", styles.ToString()));
            return html;
        }

        public string getImage(string input)
        {
            if (input == null)
                return string.Empty;
            string tempInput = input;
            string pattern = @"<img(.|\n)+?>";
            string src = string.Empty;

            //Change the relative URL's to absolute URL's for an image, if any in the HTML code.
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.RightToLeft))
            {
                if (m.Success)
                {
                    string tempM = m.Value;
                    string pattern1 = "src=[\'|\"](.+?)[\'|\"]";
                    Regex reImg = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match mImg = reImg.Match(m.Value);

                    if (mImg.Success)
                    {
                        src = Regex.Replace(mImg.Value, @"src=", "", RegexOptions.IgnoreCase).Replace("\"", "").Replace("\'", "");

                        if (src.ToLower().Contains("http://") == false)
                        {
                            //Insert new URL in img tag
                            src = "src=\"" + Request.Url.Scheme + "://" +
                                Request.Url.Authority + src + "\"";
                            try
                            {
                                tempM = tempM.Remove(mImg.Index, mImg.Length);
                                tempM = tempM.Insert(mImg.Index, src);

                                //insert new url img tag in whole html code
                                tempInput = tempInput.Remove(m.Index, m.Length);
                                tempInput = tempInput.Insert(m.Index, tempM);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }
            return tempInput;
        }

		public byte[] WKHtmlToPdf(string content)
		{
			var prefix = HttpContext.Request.PhysicalApplicationPath;
			var tempdir = prefix + "temp\\";

			string inputFileName = "";

			try
			{
				if (!Directory.Exists(tempdir))
					Directory.CreateDirectory(tempdir);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("\\temp directory is not exist. Please create it or give server permissions to create directories in root");
			}

			try
			{
				inputFileName = tempdir + Guid.NewGuid().ToString() + ".html";
				System.IO.File.WriteAllText(inputFileName, content, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Cannot create temp file. Please check server has permissions to create files in \\temp directory");
			}

			var wkhtmlDir = prefix + "bin\\wkhtmltopdf\\";
			var wkhtml = prefix + "bin\\wkhtmltopdf\\wkhtmltopdf.exe";
			var p = new System.Diagnostics.Process();

			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.FileName = wkhtml;
			p.StartInfo.WorkingDirectory = wkhtmlDir;

			string switches = "";
			switches += "--print-media-type ";
			//switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
			//switches += "--page-size Letter ";
			p.StartInfo.Arguments = switches + " \"" + inputFileName + "\" - ";
			p.Start();

			//read output
			byte[] buffer = new byte[32768];
			byte[] file;
			using (var ms = new MemoryStream())
			{
				while (true)
				{
					int read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

					if (read <= 0)
					{
						break;
					}
					ms.Write(buffer, 0, read);
				}
				file = ms.ToArray();
			}

			// wait or exit
			p.WaitForExit(1000);

			// read the exit code, close process
			int returnCode = p.ExitCode;
			p.Close();

			if (System.IO.File.Exists(inputFileName))
				System.IO.File.Delete(inputFileName);

			if (returnCode != 0)
				throw new ApplicationException("wkhtmltopdf return code=" + returnCode);

			return returnCode == 0 ? file : null;
		}
    }
}