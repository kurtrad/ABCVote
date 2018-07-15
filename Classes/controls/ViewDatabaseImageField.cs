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
	public partial class ViewDatabaseImageField : ViewControl
	{
		protected dynamic showThumbnails = XVar.Pack(false);
		protected dynamic thumbWidth;
		protected dynamic thumbHeight;
		protected static bool skipViewDatabaseImageFieldCtor = false;
		public ViewDatabaseImageField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject)
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject)
		{
			if(skipViewDatabaseImageFieldCtor)
			{
				skipViewDatabaseImageFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic container = XVar.Clone(_param_container);
			dynamic pageobject = XVar.Clone(_param_pageobject);
			#endregion

			dynamic pageObject = null;
			this.showThumbnails = XVar.Clone(container.pSet.showThumbnail((XVar)(this.field)));
			if(XVar.Pack(this.showThumbnails))
			{
				this.thumbWidth = XVar.Clone(container.pSet.getThumbnailWidth((XVar)(this.field)));
				this.thumbHeight = XVar.Clone(container.pSet.getThumbnailHeight((XVar)(this.field)));
			}
		}
		public override XVar addJSFiles()
		{
			AddJSFile(new XVar("include/zoombox/zoombox.js"));
			getJSControl();
			return null;
		}
		public override XVar addCSSFiles()
		{
			AddCSSFile(new XVar("include/zoombox/zoombox.css"));
			return null;
		}
		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic fileName = null, fileNameF = null, value = null;
			if(XVar.Pack(!(XVar)(data[this.field])))
			{
				return "";
			}
			value = new XVar("");
			fileName = new XVar("file.jpg");
			fileNameF = XVar.Clone(this.container.pSet.getFilenameField((XVar)(this.field)));
			if((XVar)(fileNameF)  && (XVar)(data[fileNameF]))
			{
				fileName = XVar.Clone(data[fileNameF]);
			}
			if(XVar.Pack(this.showThumbnails))
			{
				dynamic hrefBegin = null, hrefEnd = null, linkClass = null, smallThumbnailStyle = null, thumbPref = null;
				thumbPref = XVar.Clone(this.container.pSet.getStrThumbnail((XVar)(this.field)));
				hrefBegin = XVar.Clone(MVCFunctions.GetTableLink(new XVar("mfhandler"), new XVar(""), (XVar)(MVCFunctions.Concat("filename=", fileName, "&table=", MVCFunctions.RawUrlEncode((XVar)(this.container.pSet._table))))));
				hrefEnd = XVar.Clone(MVCFunctions.Concat("&nodisp=1&pageType=", this.container.pageType, keylink, "&rndVal=", MVCFunctions.rand(new XVar(0), new XVar(32768))));
				linkClass = new XVar("zoombox");
				if((XVar)(this.thumbWidth)  && (XVar)(this.thumbHeight))
				{
					dynamic hasThumbnail = null, thumbFileUrl = null;
					hasThumbnail = XVar.Clone((XVar)(thumbPref != XVar.Pack(""))  && (XVar)(MVCFunctions.strlen((XVar)(data[thumbPref]))));
					thumbFileUrl = XVar.Clone(MVCFunctions.Concat(hrefBegin, "&field=", (XVar.Pack(hasThumbnail) ? XVar.Pack(MVCFunctions.RawUrlEncode((XVar)(thumbPref))) : XVar.Pack(MVCFunctions.RawUrlEncode((XVar)(this.field)))), hrefEnd));
					smallThumbnailStyle = XVar.Clone(getSmallThumbnailStyle((XVar)(thumbFileUrl), (XVar)(hasThumbnail)));
					linkClass = MVCFunctions.Concat(linkClass, " background-picture");
				}
				value = MVCFunctions.Concat(value, "<a target=_blank href='", hrefBegin, "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), hrefEnd, "' class='", linkClass, "' ", smallThumbnailStyle, ">");
				value = MVCFunctions.Concat(value, "<img border=0 ");
				if(XVar.Pack(this.is508))
				{
					value = MVCFunctions.Concat(value, " alt=\"Image from DB\"");
				}
				value = MVCFunctions.Concat(value, " src='", hrefBegin, "&field=", MVCFunctions.RawUrlEncode((XVar)(thumbPref)), hrefEnd, "'>");
				value = MVCFunctions.Concat(value, "</a>");
			}
			else
			{
				value = new XVar("<img class=\"bs-dbimage\" ");
				if(XVar.Pack(this.is508))
				{
					value = MVCFunctions.Concat(value, " alt=\"Image from DB\"");
				}
				value = MVCFunctions.Concat(value, " border=0");
				value = MVCFunctions.Concat(value, getImageSizeStyle(new XVar(true)), " src='", MVCFunctions.GetTableLink(new XVar("mfhandler"), new XVar(""), (XVar)(MVCFunctions.Concat("filename=", fileName, "&table=", MVCFunctions.RawUrlEncode((XVar)(this.container.pSet._table)), "&field=", MVCFunctions.RawUrlEncode((XVar)(this.field)), "&nodisp=1", "&pageType=", this.container.pageType, keylink, "&rndVal=", MVCFunctions.rand(new XVar(0), new XVar(32768))))), "'>");
			}
			return value;
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic fileNameField = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(data[this.field])))))
			{
				return "";
			}
			fileNameField = XVar.Clone(this.container.pSet.getFilenameField((XVar)(this.field)));
			if((XVar)(fileNameField)  && (XVar)(data[fileNameField]))
			{
				return data[fileNameField];
			}
			return "<<Image>>";
		}
		public override XVar getExportValue(dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return "LONG BINARY DATA - CANNOT BE DISPLAYED";
		}
		protected virtual XVar getSmallThumbnailStyle(dynamic _param_imageSrc, dynamic _param_hasThumbnail)
		{
			#region pass-by-value parameters
			dynamic imageSrc = XVar.Clone(_param_imageSrc);
			dynamic hasThumbnail = XVar.Clone(_param_hasThumbnail);
			#endregion

			dynamic styles = XVar.Array();
			styles = XVar.Clone(XVar.Array());
			if(XVar.Pack(imageSrc))
			{
				imageSrc = XVar.Clone(MVCFunctions.str_replace(new XVar("="), new XVar("&#61;"), (XVar)(imageSrc)));
				styles.InitAndSetArrayItem(MVCFunctions.Concat(" background-image: url(", imageSrc, ");"), null);
				if(XVar.Pack(!(XVar)(hasThumbnail)))
				{
					styles.InitAndSetArrayItem(MVCFunctions.Concat(" background-size: ", this.thumbWidth, "px ", this.thumbHeight, "px ;"), null);
				}
			}
			if(XVar.Pack(this.thumbWidth))
			{
				styles.InitAndSetArrayItem(MVCFunctions.Concat(" width: ", this.thumbWidth, "px;"), null);
			}
			if(XVar.Pack(this.thumbHeight))
			{
				styles.InitAndSetArrayItem(MVCFunctions.Concat(" height: ", this.thumbHeight, "px"), null);
			}
			return MVCFunctions.Concat(" style=\"", MVCFunctions.implode(new XVar(""), (XVar)(styles)), "\"");
		}
	}
}
