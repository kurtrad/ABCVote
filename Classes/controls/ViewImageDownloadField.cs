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
	public partial class ViewImageDownloadField : ViewFileField
	{
		protected dynamic isImageURL = XVar.Pack(false);
		protected dynamic showThumbnails = XVar.Pack(false);
		protected dynamic setOfThumbnails = XVar.Pack(false);
		protected dynamic useAbsolutePath = XVar.Pack(false);
		protected dynamic imageWidth;
		protected dynamic imageHeight;
		protected dynamic thumbWidth;
		protected dynamic thumbHeight;
		protected static bool skipViewImageDownloadFieldCtor = false;
		public ViewImageDownloadField(dynamic _param_field, dynamic _param_container, dynamic _param_pageobject)
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageobject)
		{
			if(skipViewImageDownloadFieldCtor)
			{
				skipViewImageDownloadFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic container = XVar.Clone(_param_container);
			dynamic pageobject = XVar.Clone(_param_pageobject);
			#endregion

			dynamic pageObject = null;
			this.isImageURL = XVar.Clone(container.pSet.isImageURL((XVar)(this.field)));
			this.showThumbnails = XVar.Clone((XVar)(container.pSet.showThumbnail((XVar)(this.field)))  && (XVar)(!(XVar)(this.isImageURL)));
			this.setOfThumbnails = XVar.Clone(container.pSet.showListOfThumbnails((XVar)(this.field)));
			this.useAbsolutePath = XVar.Clone(container.pSet.isAbsolute((XVar)(this.field)));
			this.imageWidth = XVar.Clone(container.pSet.getImageWidth((XVar)(this.field)));
			this.imageHeight = XVar.Clone(container.pSet.getImageHeight((XVar)(this.field)));
			if(XVar.Pack(this.showThumbnails))
			{
				this.thumbWidth = XVar.Clone(container.pSet.getThumbnailWidth((XVar)(this.field)));
				this.thumbHeight = XVar.Clone(container.pSet.getThumbnailHeight((XVar)(this.field)));
			}
		}
		public override XVar addJSFiles()
		{
			if(XVar.Pack(!(XVar)(this.isImageURL)))
			{
				AddJSFile(new XVar("include/sudo/jquery.sudoSlider.js"));
				AddJSFile(new XVar("include/zoombox/zoombox.js"));
				getJSControl();
			}
			return null;
		}
		public override XVar addCSSFiles()
		{
			if(XVar.Pack(!(XVar)(this.isImageURL)))
			{
				AddCSSFile(new XVar("include/sudo/style.css"));
				AddCSSFile(new XVar("include/zoombox/zoombox.css"));
			}
			return null;
		}
		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			if(data[this.field] == "")
			{
				return "";
			}
			if(XVar.Pack(!(XVar)(this.isImageURL)))
			{
				dynamic arBigThumbnails = XVar.Array(), divBigThumbnailsSize = null, divSize = null, filesArray = XVar.Array(), resultValues = XVar.Array(), zoomboxRand = null;
				this.upload_handler.tkeys = XVar.Clone(keylink);
				resultValues = XVar.Clone(XVar.Array());
				arBigThumbnails = XVar.Clone(XVar.Array());
				zoomboxRand = XVar.Clone(MVCFunctions.rand(new XVar(11111), new XVar(99999)));
				filesArray = XVar.Clone(getFilesArray((XVar)(data[this.field])));
				foreach (KeyValuePair<XVar, dynamic> imageFile in filesArray.GetEnumerator())
				{
					dynamic hasBigImage = null, hasThumbnail = null, imagePath = null, imageValue = null, userFile = XVar.Array();
					userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(imageFile.Value)));
					if((XVar)(this.container.pageType == Constants.PAGE_EXPORT)  || (XVar)(this.container.forExport != ""))
					{
						resultValues.InitAndSetArrayItem(userFile["name"], null);
						continue;
					}
					if(XVar.Pack(!(XVar)(CommonFunctions.CheckImageExtension((XVar)(imageFile.Value["name"])))))
					{
						resultValues.InitAndSetArrayItem(MVCFunctions.Concat("<a href=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">", userFile["name"], "</a>"), null);
						continue;
					}
					userFile["url"] = MVCFunctions.Concat(userFile["url"], "&nodisp=1");
					if(userFile["thumbnail_url"] != "")
					{
						userFile["thumbnail_url"] = MVCFunctions.Concat(userFile["thumbnail_url"], "&nodisp=1");
					}
					imageValue = new XVar("");
					divSize = new XVar("");
					divBigThumbnailsSize = new XVar("");
					hasThumbnail = new XVar(false);
					imagePath = XVar.Clone(getImagePath((XVar)(imageFile.Value["name"])));
					hasBigImage = XVar.Clone(MVCFunctions.myfile_exists((XVar)(imagePath)));
					if(XVar.Pack(this.showThumbnails))
					{
						dynamic thumbPath = null;
						thumbPath = XVar.Clone(getImagePath((XVar)(imageFile.Value["thumbnail"])));
						hasThumbnail = XVar.Clone(MVCFunctions.myfile_exists((XVar)(thumbPath)));
					}
					if(XVar.Pack(this.showThumbnails))
					{
						if(XVar.Pack(hasThumbnail))
						{
							dynamic src = null;
							imageValue = MVCFunctions.Concat(imageValue, "<img class=\"bs-dbimage\" border=\"0\"");
							if(XVar.Pack(this.is508))
							{
								imageValue = MVCFunctions.Concat(imageValue, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
							}
							src = XVar.Clone((XVar.Pack(userFile["thumbnail_url"] != "") ? XVar.Pack(userFile["thumbnail_url"]) : XVar.Pack(userFile["url"])));
							if((XVar)(this.thumbWidth)  || (XVar)(this.thumbHeight))
							{
								imageValue = MVCFunctions.Concat(imageValue, getSmallThumbnailStyle());
							}
							imageValue = MVCFunctions.Concat(imageValue, " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(src)), "\" />");
						}
						else
						{
							if(XVar.Pack(hasBigImage))
							{
								imageValue = MVCFunctions.Concat(imageValue, "<img class=\"bs-dbimage\" ", getImageSizeStyle(new XVar(true)), " border=\"0\"");
								if(XVar.Pack(this.is508))
								{
									imageValue = MVCFunctions.Concat(imageValue, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
								}
								imageValue = MVCFunctions.Concat(imageValue, " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">");
							}
						}
						if((XVar)(hasBigImage)  && (XVar)(imageValue != XVar.Pack("")))
						{
							dynamic href = null, linkClass = null, smallThumbnailStyle = null;
							href = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])));
							smallThumbnailStyle = new XVar("");
							linkClass = XVar.Clone((XVar.Pack(!(XVar)(this.setOfThumbnails)) ? XVar.Pack(MVCFunctions.Concat("zoombox zgallery", zoomboxRand)) : XVar.Pack("")));
							if((XVar)(this.thumbWidth)  && (XVar)(this.thumbHeight))
							{
								dynamic thumbFileUrl = null;
								thumbFileUrl = XVar.Clone((XVar.Pack(hasThumbnail) ? XVar.Pack(userFile["thumbnail_url"]) : XVar.Pack(userFile["url"])));
								smallThumbnailStyle = XVar.Clone(getSmallThumbnailStyle((XVar)(thumbFileUrl), (XVar)(hasThumbnail)));
								linkClass = MVCFunctions.Concat(linkClass, " background-picture");
							}
							if(XVar.Pack(linkClass))
							{
								linkClass = XVar.Clone(MVCFunctions.Concat("class='", linkClass, "'"));
							}
							imageValue = XVar.Clone(MVCFunctions.Concat("<a target=\"_blank\" href=\"", href, "\"", linkClass, smallThumbnailStyle, ">", imageValue, "</a>"));
							if(XVar.Pack(this.setOfThumbnails))
							{
								dynamic bigThumbnailLink = null, bigThumbnailLinkStyle = null;
								bigThumbnailLinkStyle = XVar.Clone(getBigThumbnailSizeStyles());
								bigThumbnailLink = XVar.Clone(MVCFunctions.Concat("<a style=\"display: none;\" href=\"", href, "\" ", (XVar.Pack(bigThumbnailLinkStyle) ? XVar.Pack("class=\"zoombox\"") : XVar.Pack("")), ">"));
								bigThumbnailLink = MVCFunctions.Concat(bigThumbnailLink, "<img src=\"", href, "\" border=\"0\"");
								bigThumbnailLink = MVCFunctions.Concat(bigThumbnailLink, getImageSizeStyle(new XVar(true)));
								bigThumbnailLink = MVCFunctions.Concat(bigThumbnailLink, "/></a>");
								if(XVar.Pack(!(XVar)(divBigThumbnailsSize)))
								{
									divBigThumbnailsSize = XVar.Clone(MVCFunctions.Concat("style=\"", bigThumbnailLinkStyle, "\""));
								}
								arBigThumbnails.InitAndSetArrayItem(bigThumbnailLink, null);
							}
						}
					}
					else
					{
						if(XVar.Pack(hasBigImage))
						{
							imageValue = MVCFunctions.Concat(imageValue, "<img");
							if(XVar.Pack(this.imageWidth))
							{
								divSize = XVar.Clone(MVCFunctions.Concat("width: ", this.imageWidth, "px;"));
							}
							if(XVar.Pack(this.imageHeight))
							{
								divSize = MVCFunctions.Concat(divSize, "height: ", this.imageHeight, "px;");
							}
							if(divSize != XVar.Pack(""))
							{
								divSize = XVar.Clone(MVCFunctions.Concat("style=\"", divSize, "\""));
							}
							imageValue = MVCFunctions.Concat(imageValue, " border=0");
							if(XVar.Pack(this.is508))
							{
								imageValue = MVCFunctions.Concat(imageValue, " alt=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["name"])), "\"");
							}
							imageValue = MVCFunctions.Concat(imageValue, getImageSizeStyle(new XVar(true)), " src=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userFile["url"])), "\">");
						}
					}
					if(imageValue != XVar.Pack(""))
					{
						resultValues.InitAndSetArrayItem(imageValue, null);
					}
				}
				if((XVar)(1 < MVCFunctions.count(resultValues))  || (XVar)((XVar)(MVCFunctions.count(resultValues) == 1)  && (XVar)(this.setOfThumbnails)))
				{
					dynamic divBigThumbnails = null, hiddenFields = null, i = null, presudoSlider = null;
					if((XVar)(this.container.pageType == Constants.PAGE_EXPORT)  || (XVar)(this.container.forExport != ""))
					{
						return MVCFunctions.implode(new XVar(", "), (XVar)(resultValues));
					}
					if(this.container.pageType == Constants.PAGE_PRINT)
					{
						return MVCFunctions.implode(new XVar("<br />"), (XVar)(resultValues));
					}
					i = new XVar(0);
					for(;i < MVCFunctions.count(resultValues); i++)
					{
						if(i == XVar.Pack(0))
						{
							resultValues.InitAndSetArrayItem(MVCFunctions.Concat("<li>", resultValues[i], "</li>"), i);
						}
						else
						{
							resultValues.InitAndSetArrayItem(MVCFunctions.Concat("<li style=\"display:none;\">", resultValues[i], "</li>"), i);
						}
					}
					divBigThumbnails = new XVar("");
					if(XVar.Pack(MVCFunctions.count(arBigThumbnails)))
					{
						divBigThumbnails = XVar.Clone(MVCFunctions.Concat("<div class=\"big-thumbnails\" ", divBigThumbnailsSize, ">", MVCFunctions.implode(new XVar(""), (XVar)(arBigThumbnails)), "</div>"));
					}
					if((XVar)(!(XVar)(divSize))  && (XVar)(!(XVar)(this.setOfThumbnails)))
					{
						divSize = XVar.Clone(MVCFunctions.Concat("style=\"", getBigThumbnailSizeStyles(new XVar(true)), "\""));
					}
					presudoSlider = XVar.Clone(MVCFunctions.Concat("<div class=\"presudoslider\" ", divSize, ">", divBigThumbnails, "<ul class=\"viewimage-thumblist\" style=\"list-style: none;\">", MVCFunctions.implode(new XVar(""), (XVar)(resultValues)), "</ul>", hiddenFields, "</div>"));
					return MVCFunctions.Concat("<div style=\"position:relative;\" class=\"viewImage\">", presudoSlider, "</div>");
				}
				if(MVCFunctions.count(resultValues) == 1)
				{
					return resultValues[0];
				}
				return MVCFunctions.Concat("<img src=\"", MVCFunctions.GetRootPathForResources(new XVar("images/no_image.gif")), "\" />");
			}
			else
			{
				if(0 < MVCFunctions.strlen((XVar)(data[this.field])))
				{
					dynamic value = null;
					value = new XVar("<img class=\"bs-dbimage\"");
					value = MVCFunctions.Concat(value, " border=0");
					value = MVCFunctions.Concat(value, getImageSizeStyle(new XVar(true)), " src='", data[this.field], "'>");
					return value;
				}
			}
			return "";
		}
		public override XVar getTextValue(dynamic data)
		{
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(data[this.field])))))
			{
				return "";
			}
			if(XVar.Pack(!(XVar)(this.isImageURL)))
			{
				dynamic fileNames = XVar.Array(), filesData = XVar.Array();
				fileNames = XVar.Clone(XVar.Array());
				filesData = XVar.Clone(getFilesArray((XVar)(data[this.field])));
				foreach (KeyValuePair<XVar, dynamic> imageFile in filesData.GetEnumerator())
				{
					dynamic userFile = XVar.Array();
					userFile = XVar.Clone(this.upload_handler.buildUserFile((XVar)(imageFile.Value)));
					fileNames.InitAndSetArrayItem(userFile["name"], null);
				}
				return MVCFunctions.implode(new XVar(", "), (XVar)(fileNames));
			}
			else
			{
				return data[this.field];
			}
			return null;
		}
		protected virtual XVar getImagePath(dynamic _param_imageFile)
		{
			#region pass-by-value parameters
			dynamic imageFile = XVar.Clone(_param_imageFile);
			#endregion

			if((XVar)(this.useAbsolutePath)  || (XVar)(MVCFunctions.isAbsolutePath((XVar)(imageFile))))
			{
				return imageFile;
			}
			return MVCFunctions.getabspath((XVar)(imageFile));
		}
		protected virtual XVar getSmallThumbnailStyle(dynamic _param_imageSrc = null, dynamic _param_hasThumbnail = null)
		{
			#region default values
			if(_param_imageSrc as Object == null) _param_imageSrc = new XVar(false);
			if(_param_hasThumbnail as Object == null) _param_hasThumbnail = new XVar(true);
			#endregion

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
		protected virtual XVar getBigThumbnailSizeStyles(dynamic _param_widthAutoSet = null)
		{
			#region default values
			if(_param_widthAutoSet as Object == null) _param_widthAutoSet = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic widthAutoSet = XVar.Clone(_param_widthAutoSet);
			#endregion

			dynamic bigThumbnailHeight = null, bigThumbnailSizeStyle = null, bigThumbnailWidth = null;
			bigThumbnailSizeStyle = new XVar("");
			bigThumbnailHeight = XVar.Clone(this.imageHeight);
			bigThumbnailWidth = XVar.Clone(this.imageWidth);
			if(XVar.Pack(bigThumbnailWidth))
			{
				bigThumbnailSizeStyle = MVCFunctions.Concat(bigThumbnailSizeStyle, " width: ", bigThumbnailWidth, "px;");
			}
			if(XVar.Pack(bigThumbnailHeight))
			{
				bigThumbnailSizeStyle = MVCFunctions.Concat(bigThumbnailSizeStyle, " height: ", bigThumbnailHeight, "px;");
			}
			if((XVar)((XVar)(!(XVar)(bigThumbnailWidth))  && (XVar)(bigThumbnailHeight))  && (XVar)(widthAutoSet))
			{
				bigThumbnailSizeStyle = MVCFunctions.Concat(bigThumbnailSizeStyle, " width: ", (XVar)Math.Floor((double)((4 * bigThumbnailHeight) / 3)), "px;");
			}
			return bigThumbnailSizeStyle;
		}
	}
}
