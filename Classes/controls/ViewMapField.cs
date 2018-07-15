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
	public partial class ViewMapField : ViewControl
	{
		protected static bool skipViewMapFieldCtor = false;
		public ViewMapField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject) // proxy constructor
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject) {}

		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic icon = null, location = null, mapData = XVar.Array(), src = null;
			if(XVar.Pack(!(XVar)(this.pageObject)))
			{
				return MVCFunctions.runner_htmlspecialchars((XVar)(data[this.field]));
			}
			else
			{
				if((XVar)(this.pageObject.pageType == Constants.PAGE_EXPORT)  || (XVar)((XVar)(this.pageObject.pageType == Constants.PAGE_RPRINT)  && (XVar)(this.container.forExport == "excel")))
				{
					return MVCFunctions.runner_htmlspecialchars((XVar)(data[this.field]));
				}
			}
			if(this.pageObject.pageType != Constants.PAGE_LIST)
			{
				mapData = XVar.Clone(this.pageObject.addGoogleMapData((XVar)(this.field), (XVar)(data)));
			}
			if((XVar)((XVar)((XVar)((XVar)(this.pageObject.pageType != Constants.PAGE_PRINT)  && (XVar)(this.pageObject.pageType != Constants.PAGE_MASTER_INFO_PRINT))  && (XVar)(this.pageObject.pageType != Constants.PAGE_RPRINT))  && (XVar)(this.pageObject.pageType != Constants.PAGE_REPORT))  && (XVar)(!(XVar)((XVar)(this.pageObject.mode == Constants.VIEW_SIMPLE)  && (XVar)(this.pageObject.pdfMode))))
			{
				return MVCFunctions.Concat("<div id=\"littleMap_", MVCFunctions.GoodFieldName((XVar)(this.field)), "_", this.pageObject.recId, "\" style=\"width:", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("300") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["width"])), "px; ", "height: ", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("225") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["height"])), "px; ", "\" data-gridlink class=\"littleMap\"></div>");
			}
			if((XVar)(mapData["markers"][0]["lat"] == "")  && (XVar)(mapData["markers"][0]["lng"] == ""))
			{
				dynamic locationByAddress = XVar.Array();
				switch(((XVar)CommonFunctions.getMapProvider()).ToInt())
				{
					case Constants.GOOGLE_MAPS:
						location = XVar.Clone(mapData["markers"][0]["address"]);
						break;
					case Constants.OPEN_STREET_MAPS:
						locationByAddress = XVar.Clone(CommonFunctions.getLatLngByAddr((XVar)(mapData["markers"][0]["address"])));
						location = XVar.Clone(MVCFunctions.Concat(locationByAddress["lat"], ",", locationByAddress["lng"]));
						break;
					case Constants.BING_MAPS:
						locationByAddress = XVar.Clone(CommonFunctions.getLatLngByAddr((XVar)(mapData["markers"][0]["address"])));
						location = XVar.Clone(MVCFunctions.Concat(locationByAddress["lat"], ",", locationByAddress["lng"]));
						break;
				}
			}
			else
			{
				location = XVar.Clone(MVCFunctions.Concat(mapData["markers"][0]["lat"], ",", mapData["markers"][0]["lng"]));
			}
			icon = XVar.Clone(mapData["markers"][0]["mapIcon"]);
			src = XVar.Clone(getStaticMapURL((XVar)(location), (XVar)(mapData["zoom"]), (XVar)(icon)));
			return MVCFunctions.Concat("<img border=\"0\" alt=\"\" src=\"", src, "\">");
		}
		public virtual XVar getStaticMapURL(dynamic _param_location, dynamic _param_zoom, dynamic _param_icon)
		{
			#region pass-by-value parameters
			dynamic location = XVar.Clone(_param_location);
			dynamic zoom = XVar.Clone(_param_zoom);
			dynamic icon = XVar.Clone(_param_icon);
			#endregion

			dynamic markerLocation = null, src = null, src2 = null;
			markerLocation = XVar.Clone(location);
			switch(((XVar)CommonFunctions.getMapProvider()).ToInt())
			{
				case Constants.GOOGLE_MAPS:
					src = new XVar("http://maps.googleapis.com/maps/api/staticmap");
					src2 = XVar.Clone(MVCFunctions.Concat("&sensor=false&key=", this.pageObject.googleMapCfg["APIcode"]));
					if((XVar)(icon)  && (XVar)(GlobalVars.showCustomMarkerOnPrint))
					{
						dynamic here = null, pos = null;
						here = XVar.Clone(MVCFunctions.Concat("http://", MVCFunctions.GetServerVariable("HTTP_HOST"), MVCFunctions.GetServerVariable("REQUEST_URI")));
						pos = XVar.Clone(MVCFunctions.strrpos((XVar)(here), new XVar("/")));
						here = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(here), new XVar(0), (XVar)(pos)), "/images/menuicons/", icon));
						markerLocation = XVar.Clone(MVCFunctions.Concat("icon:", here, "|", location));
					}
					break;
				case Constants.OPEN_STREET_MAPS:
					src = new XVar("http://staticmap.openstreetmap.de/staticmap.php");
					src2 = new XVar(",ol-marker");
					break;
				case Constants.BING_MAPS:
					if(XVar.Pack(!(XVar)(CommonFunctions.GetGlobalData(new XVar("apiGoogleMapsCode"), new XVar("")))))
					{
						return null;
					}
					return MVCFunctions.Concat("http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/", location, "/", zoom, "?\r\n\t\t\t\tmapSize=", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("300") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["width"])), ",", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("225") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["height"])), "\r\n\t\t\t\t&pp=", location, ";63;&key=", CommonFunctions.GetGlobalData(new XVar("apiGoogleMapsCode"), new XVar("")));
					break;
			}
			src = MVCFunctions.Concat(src, "?center=", location, "&zoom=", zoom, "&size=", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("300") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["width"])), "x", (XVar.Pack(!(XVar)(this.pageObject.googleMapCfg.KeyExists("fieldsAsMap"))) ? XVar.Pack("225") : XVar.Pack(this.pageObject.googleMapCfg["fieldsAsMap"][this.field]["height"])), "&maptype=mobile&markers=", markerLocation, src2);
			return src;
		}
	}
}
