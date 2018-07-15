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
	public partial class RunnerPage : XClass
	{
		public dynamic id = XVar.Pack(1);
		protected dynamic isUseToolTips = XVar.Pack(false);
		protected dynamic isUseAjaxSuggest = XVar.Pack(true);
		public dynamic pageType = XVar.Pack("");
		public dynamic mode = XVar.Pack(0);
		public dynamic isDisplayLoading = XVar.Pack(false);
		public dynamic strOriginalTableName = XVar.Pack("");
		protected dynamic strCaption = XVar.Pack("");
		public dynamic shortTableName = XVar.Pack("");
		public dynamic sessionPrefix = XVar.Pack("");
		public dynamic tName = XVar.Pack("");
		public dynamic gstrOrderBy = XVar.Pack("");
		public XTempl xt = null;
		public dynamic searchClauseObj = XVar.Pack(null);
		public dynamic needSearchClauseObj = XVar.Pack(true);
		public dynamic flyId = XVar.Pack(1);
		public dynamic includes_js = XVar.Array();
		public dynamic includes_jsreq = XVar.Array();
		public dynamic includes_css = XVar.Array();
		public dynamic recId = XVar.Pack(0);
		public dynamic googleMapCfg = XVar.Array();
		public dynamic reCaptchaCfg = XVar.Array();
		public dynamic captchaValue = XVar.Pack("");
		public dynamic isCaptchaOk = XVar.Pack(true);
		public dynamic captchaPassesCount = XVar.Pack(5);
		public dynamic permis = XVar.Array();
		public dynamic isGroupSecurity = XVar.Pack(true);
		protected dynamic debugJSMode = XVar.Pack(false);
		protected dynamic recIds = XVar.Array();
		public dynamic listAjax = XVar.Pack(false);
		public dynamic body = XVar.Array();
		public dynamic masterTable = XVar.Pack("");
		public dynamic masterRecordData = XVar.Array();
		protected dynamic detailsLinksOnList;
		public dynamic allDetailsTablesArr = XVar.Array();
		public dynamic jsSettings = XVar.Array();
		public dynamic controlsHTMLMap = XVar.Array();
		public dynamic viewControlsHTMLMap = XVar.Array();
		public dynamic controlsMap = XVar.Array();
		public dynamic pageData = XVar.Array();
		public dynamic viewControlsMap = XVar.Array();
		public dynamic settingsMap = XVar.Array();
		public dynamic arrRecsPerPage = XVar.Array();
		public dynamic pageSize = XVar.Pack(0);
		protected dynamic tableType = XVar.Pack("");
		public dynamic eventsObject;
		public dynamic masterKeysReq = XVar.Array();
		public dynamic detailKeysByM = XVar.Array();
		public dynamic lockingObj = XVar.Pack(null);
		protected dynamic isUseVideo = XVar.Pack(false);
		protected dynamic isResizeColumns = XVar.Pack(false);
		protected dynamic isUseCK = XVar.Pack(false);
		public dynamic isShowDetailTables = XVar.Pack(false);
		public dynamic filesToSave = XVar.Array();
		public dynamic filesToMove = XVar.Array();
		public dynamic filesToDelete = XVar.Array();
		protected dynamic masterKeysByD = XVar.Array();
		public dynamic isDynamicPerm = XVar.Pack(false);
		protected dynamic isAddWebRep = XVar.Pack(true);
		protected dynamic is508 = XVar.Pack(false);
		public dynamic cipherer = XVar.Pack(null);
		public ProjectSettings pSet = null;
		public ProjectSettings pSetEdit = null;
		protected dynamic numRowsFromSQL = XVar.Pack(0);
		protected dynamic myPage = XVar.Pack(0);
		protected dynamic mapProvider = XVar.Pack(0);
		protected dynamic recordsOnPage = XVar.Pack(0);
		public dynamic recsPerRowList = XVar.Pack(0);
		public dynamic recsPerRowPrint = XVar.Pack(0);
		public dynamic listGridLayout = XVar.Pack(false);
		public dynamic printGridLayout = XVar.Pack(false);
		public dynamic gridTabs = XVar.Array();
		protected dynamic fieldCssRules = XVar.Array();
		protected dynamic cell_css_rules = XVar.Pack("");
		protected dynamic row_css_rules = XVar.Pack("");
		protected dynamic mobile_css_rules = XVar.Pack("");
		protected dynamic colsOnPage = XVar.Pack(1);
		public dynamic totalsFields = XVar.Array();
		public dynamic rowsFound = XVar.Pack(false);
		protected dynamic deleteMessage = XVar.Pack("");
		protected dynamic maxPages = XVar.Pack(1);
		public dynamic templatefile = XVar.Pack("");
		public dynamic menuNodes = XVar.Array();
		protected SQLQuery gQuery = null;
		protected dynamic isControlsMapFilled = XVar.Pack(false);
		protected dynamic controls = XVar.Pack(null);
		public dynamic viewControls = XVar.Pack(null);
		public dynamic readOnlyFields = XVar.Array();
		protected dynamic searchPanelActivated = XVar.Pack(false);
		public dynamic pSetSearch = XVar.Pack(null);
		public dynamic searchTableName = XVar.Pack("");
		protected dynamic pageLayout = XVar.Pack(null);
		protected dynamic warnLeavingPages = XVar.Pack(null);
		public dynamic tableBasedSearchPanelAdded = XVar.Pack(false);
		public dynamic mainTable = XVar.Pack("");
		public dynamic mainField = XVar.Pack("");
		protected dynamic _cachedWhereComponents = XVar.Pack(null);
		protected dynamic timeRegexp;
		protected dynamic dispNoneStyle = XVar.Pack("style=\"display: none;\"");
		protected dynamic detailKeysByD = XVar.Array();
		public dynamic searchLogger = XVar.Pack(null);
		public dynamic searchSavingEnabled = XVar.Pack(false);
		public dynamic pageHasSavedSearches = XVar.Pack(false);
		protected dynamic formBricks = XVar.Array();
		public dynamic connection = XVar.Pack(null);
		public dynamic dashTName = XVar.Pack("");
		public dynamic dashElementName = XVar.Pack("");
		protected dynamic dashSet;
		protected dynamic dashElementData = XVar.Array();
		public dynamic fbObj = XVar.Pack(null);
		public dynamic pdfMode = XVar.Pack("");
		public dynamic initialStep = XVar.Pack(0);
		public dynamic format = XVar.Pack("");
		public dynamic message = XVar.Pack("");
		public dynamic viewPdfEnabled = XVar.Pack(false);
		public dynamic mapRefresh = XVar.Pack(false);
		public dynamic vpCoordinates = XVar.Array();
		public dynamic querySQL = XVar.Pack("");
		public dynamic masterPageType = XVar.Pack("");
		protected dynamic data = XVar.Pack(null);
		public dynamic errorFields = XVar.Array();
		protected dynamic menuRoots = XVar.Array();
		protected dynamic detailsTableObjects = XVar.Array();
		public dynamic isScrollGridBody = XVar.Pack(false);
		public dynamic stopPRG = XVar.Pack(false);
		public dynamic pageTitle = XVar.Pack(null);
		public dynamic pushContext = XVar.Pack(true);
		public dynamic standaloneContext = XVar.Pack(null);
		public dynamic keys = XVar.Array();
		public dynamic selection = XVar.Array();
		public dynamic tabChangeling = XVar.Pack(null);
		public dynamic skipMapFilter = XVar.Pack(false);
		public dynamic changeDetailsTabTitles = XVar.Pack(true);
		public RunnerPage(dynamic var_params)
		{
			dynamic ajaxSuggestDefault = null, captchaSettings = XVar.Array(), globalPopupPagesLayoutNames = null;
			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			if(XVar.Pack(this.pushContext))
			{
				RunnerContext.pushPageContext(this);
			}
			else
			{
				dynamic pageObj = null;
				this.standaloneContext = XVar.Clone(new RunnerContextItem(new XVar(Constants.CONTEXT_PAGE), (XVar)(new XVar("pageObj", pageObj))));
			}
			if(XVar.Pack(!(XVar)(this.id)))
			{
				this.id = new XVar(1);
			}
			GlobalVars.pagesData.InitAndSetArrayItem(this.pageData, this.id);
			this.pageData.InitAndSetArrayItem(XVar.Array(), "proxy");
			if(XVar.Pack(this.xt))
			{
				this.xt.pageId = XVar.Clone(this.id);
			}
			this.xt.assign(new XVar("pageid"), (XVar)(this.id));
			setTableConnection();
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.tName), (XVar)(this.pageType)));
			this.pSetEdit = XVar.UnPackProjectSettings(this.pSet);
			this.pSetSearch = XVar.Clone(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_SEARCH)));
			this.searchTableName = XVar.Clone(this.tName);
			if(XVar.Pack(this.dashTName))
			{
				this.dashSet = XVar.Clone(new ProjectSettings((XVar)(this.dashTName)));
				if(XVar.Pack(isDashboardElement()))
				{
					this.dashElementData = XVar.Clone(this.dashSet.getDashboardElementData((XVar)(this.dashElementName)));
				}
			}
			assignCipherer();
			this.controls = XVar.Clone(new EditControlsContainer(this, (XVar)(this.pSetEdit), (XVar)(this.pageType)));
			this.viewControls = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), (XVar)(this.pageType), this));
			this.gQuery = XVar.UnPackSQLQuery(this.pSet.getSQLQuery());
			this.googleMapCfg = XVar.Clone(new XVar("isUseMainMaps", false, "isUseFieldsMaps", false, "isUseGoogleMap", false, "APIcode", CommonFunctions.GetGlobalData(new XVar("apiGoogleMapsCode"), new XVar("")), "mainMapIds", XVar.Array(), "fieldMapsIds", XVar.Array(), "mapsData", XVar.Array()));
			captchaSettings = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("CaptchaSettings"), new XVar("")));
			this.captchaPassesCount = XVar.Clone(captchaSettings["captchaPassesCount"]);
			if(captchaSettings["type"] == Constants.RE_CAPTCHA)
			{
				AddJSFile(new XVar("include/runnerJS/ReCaptcha.js"));
				this.reCaptchaCfg = XVar.Clone(new XVar("siteKey", captchaSettings["siteKey"], "inputCaptchaId", ""));
			}
			this.debugJSMode = new XVar(false);
			if(this.flyId < this.id + 1)
			{
				this.flyId = XVar.Clone(this.id + 1);
			}
			if(XVar.Pack(this.tName))
			{
				this.permis.InitAndSetArrayItem(getPermissions(), this.tName);
				this.eventsObject = CommonFunctions.getEventObject((XVar)(this.tName));
			}
			if(XVar.Pack(!(XVar)(this.sessionPrefix)))
			{
				assignSessionPrefix();
			}
			this.isDisplayLoading = XVar.Clone(this.pSet.displayLoading());
			this.settingsMap.InitAndSetArrayItem(XVar.Array(), "globalSettings");
			this.settingsMap.InitAndSetArrayItem(XVar.Array(), "globalSettings", "shortTNames");
			this.searchPanelActivated = XVar.Clone(checkIfSearchPanelActivated((XVar)(mobileTemplateMode())));
			setParamsForSearchPanel();
			this.searchSavingEnabled = XVar.Clone((XVar)(isSearchSavingEnabled())  && (XVar)(this.needSearchClauseObj));
			if(this.mode != Constants.LIST_MASTER)
			{
				setSessionVariables();
			}
			this.lockingObj = XVar.Clone(getLockingObject());
			this.warnLeavingPages = XVar.Clone(this.pSet.warnLeavingPages());
			this.is508 = XVar.Clone(CommonFunctions.isEnableSection508());
			this.mapProvider = XVar.Clone(CommonFunctions.getMapProvider());
			this.isUseVideo = XVar.Clone(this.pSet.isUseVideo());
			this.strCaption = XVar.Clone(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))));
			this.tableType = XVar.Clone(this.pSet.getTableType());
			this.isAddWebRep = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("isAddWebRep"), new XVar(false)));
			this.detailKeysByM = XVar.Clone(getDetailKeysByMasterTable());
			this.isDynamicPerm = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("isDynamicPerm"), new XVar(false)));
			this.shortTableName = XVar.Clone(this.pSet.getShortTableName());
			this.isResizeColumns = XVar.Clone(this.pSet.isResizeColumns());
			this.isUseAjaxSuggest = XVar.Clone(this.pSetSearch.isUseAjaxSuggest());
			this.detailsLinksOnList = XVar.Clone(this.pSet.getDetailsLinksOnList());
			this.isShowDetailTables = XVar.Clone(CommonFunctions.displayDetailsOn((XVar)(this.tName), (XVar)(this.pageType)));
			if(this.mode != Constants.LIST_MASTER)
			{
				this.allDetailsTablesArr = XVar.Clone(this.pSet.getDetailTablesArr());
			}
			setTemplateFile();
			if((XVar)(this.pageType == Constants.PAGE_REGISTER)  || (XVar)(this.pageType == Constants.PAGE_CHANGEPASS))
			{
				this.pageLayout = XVar.Clone(CommonFunctions.GetPageLayout(new XVar(""), (XVar)(this.pageType)));
			}
			else
			{
				if((XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  && (XVar)(this.mode == Constants.ADD_INLINE)))  && (XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(this.mode == Constants.EDIT_INLINE))))
				{
					this.pageLayout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType)));
				}
			}
			if(XVar.Pack(this.pageLayout))
			{
				this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "pageSkinStyle");
				this.jsSettings.InitAndSetArrayItem(MVCFunctions.Concat(this.pageLayout.style, " page-", this.pageLayout.name), "tableSettings", this.tName, "pageSkinStyle", this.pageType);
				AddCSSFile((XVar)(this.pageLayout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(CommonFunctions.isPageLayoutMobile((XVar)(this.templatefile))), (XVar)(this.pdfMode != ""))));
				foreach (KeyValuePair<XVar, dynamic> tabs in getArrTabs().GetEnumerator())
				{
					dynamic layout = null;
					layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(tabs.Value["tabId"])));
					if(XVar.Pack(layout))
					{
						AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(mobileTemplateMode()), (XVar)(this.pdfMode != ""))));
					}
				}
			}
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.recsPerRowList = new XVar(1);
				this.isScrollGridBody = new XVar(false);
				this.listAjax = new XVar(false);
				this.isUseAjaxSuggest = new XVar(false);
			}
			this.jsSettings = XVar.Clone(XVar.Array());
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings");
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName);
			this.jsSettings.InitAndSetArrayItem(new XVar("proxy", ""), "tableSettings", this.tName, "proxy");
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings");
			this.settingsMap.InitAndSetArrayItem(MVCFunctions.GetWebRootPath(), "globalSettings", "webRootPath");
			this.settingsMap.InitAndSetArrayItem("aspx", "globalSettings", "ext");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.cCharset, "globalSettings", "charSet");
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.mlang_getcurrentlang(), "globalSettings", "curretLang");
			this.settingsMap.InitAndSetArrayItem(this.debugJSMode, "globalSettings", "debugMode");
			this.settingsMap.InitAndSetArrayItem(this.googleMapCfg["APIcode"], "globalSettings", "googleMapsApiCode");
			this.settingsMap.InitAndSetArrayItem(this.shortTableName, "globalSettings", "shortTNames", this.tName);
			globalPopupPagesLayoutNames = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("popupPagesLayoutNames"), (XVar)(XVar.Array())));
			if((XVar)(!(XVar)(mobileTemplateMode()))  && (XVar)(MVCFunctions.count(globalPopupPagesLayoutNames)))
			{
				this.settingsMap.InitAndSetArrayItem(globalPopupPagesLayoutNames, "globalSettings", "popupPagesLayoutNames");
			}
			this.settingsMap.InitAndSetArrayItem(mobileTemplateMode(), "globalSettings", "isMobile");
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.detectMobileDevice(), "globalSettings", "mobileDeteced");
			this.settingsMap.InitAndSetArrayItem(this.is508, "globalSettings", "s508");
			this.settingsMap.InitAndSetArrayItem(this.mapProvider, "globalSettings", "mapProvider");
			this.settingsMap.InitAndSetArrayItem(XVar.Array(), "globalSettings", "locale");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_IDATE"], "globalSettings", "locale", "dateFormat");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_LANGNAME"], "globalSettings", "locale", "langName");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_CTRYNAME"], "globalSettings", "locale", "ctryName");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_IFIRSTDAYOFWEEK"], "globalSettings", "locale", "startWeekDay");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SDATE"], "globalSettings", "locale", "dateDelimiter");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_ITIME"], "globalSettings", "locale", "is24hoursFormat");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_ITLZERO"], "globalSettings", "locale", "leadingZero");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_STIME"], "globalSettings", "locale", "timeDelimiter");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_S2359"], "globalSettings", "locale", "timePmLetter");
			this.settingsMap.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_S1159"], "globalSettings", "locale", "timeAmLetter");
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("showDetailedError"), new XVar(true)), "globalSettings", "showDetailedError");
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("customErrorMessage"), new XVar("")), "globalSettings", "customErrorMessage");
			this.settingsMap.InitAndSetArrayItem(XVar.Array(), "tableSettings");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 0, "jsName", "entityType"), "tableSettings", "entityType");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "hasEvents"), "tableSettings", "hasEvents");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "strCaption"), "tableSettings", "strCaption");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseAudio"), "tableSettings", "isUseAudio");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseVideo"), "tableSettings", "isUseVideo");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", Constants.gltHORIZONTAL, "jsName", "listGridLayout"), "tableSettings", "listGridLayout");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseHighlite"), "tableSettings", "rowHighlite");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseToolTips"), "tableSettings", "isUseToolTips");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 1, "jsName", "recsPerRowList"), "tableSettings", "recsPerRowList");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "showAddInPopup"), "tableSettings", "showAddInPopup");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "showEditInPopup"), "tableSettings", "showEditInPopup");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "showViewInPopup"), "tableSettings", "showViewInPopup");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "updateSelected"), "tableSettings", "updateSelected");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseResize"), "tableSettings", "isResizeColumns");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", Constants.DL_SINGLE, "jsName", "detailsLinksOnList"), "tableSettings", "detailsLinksOnList");
			ajaxSuggestDefault = XVar.Clone((XVar.Pack(this.tableBasedSearchPanelAdded) ? XVar.Pack(!(XVar)(this.isUseAjaxSuggest)) : XVar.Pack(true)));
			this.settingsMap.InitAndSetArrayItem(new XVar("default", ajaxSuggestDefault, "jsName", "ajaxSuggest"), "tableSettings", "isUseAjaxSuggest");
			this.controlsMap.InitAndSetArrayItem(isOldLayout(), "oldLayout");
			this.controlsMap.InitAndSetArrayItem(getLayoutVersion(), "layoutVersion");
			this.controlsMap.InitAndSetArrayItem(getLayoutName(), "layoutName");
			this.settingsMap.InitAndSetArrayItem(XVar.Array(), "fieldSettings");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "isUseTimeStamp"), "fieldSettings", "UseTimestamp");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "strName"), "fieldSettings", "strName");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "showTime"), "fieldSettings", "ShowTime");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "editFormat"), "fieldSettings", "EditFormat");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", Constants.EDIT_DATE_SIMPLE, "jsName", "dateEditType"), "fieldSettings", "DateEditType");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "RTEType"), "fieldSettings", "RTEType");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "viewFormat"), "fieldSettings", "ViewFormat");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", null, "jsName", "validation"), "fieldSettings", "validateAs");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", null, "jsName", "mask"), "fieldSettings", "strEditMask");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 10, "jsName", "lastYear"), "fieldSettings", "LastYearFactor");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 100, "jsName", "initialYear"), "fieldSettings", "InitialYearFactor");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "showListOfThumbnails"), "fieldSettings", "ShowListOfThumbnails");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 0, "jsName", "imageWidth"), "fieldSettings", "ImageWidth");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 0, "jsName", "imageHeight"), "fieldSettings", "ImageHeight");
			if(this.pageType == Constants.PAGE_VIEW)
			{
				this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "events"), "fieldSettings", "fieldViewEvents");
			}
			else
			{
				this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "events"), "fieldSettings", "fieldEvents");
			}
			this.jsSettings.InitAndSetArrayItem(this.strCaption, "tableSettings", this.tName, "strCaption");
			this.jsSettings.InitAndSetArrayItem(this.mode, "tableSettings", this.tName, "pageMode");
			if(XVar.Pack(this.listAjax))
			{
				this.jsSettings.InitAndSetArrayItem(Constants.LIST_AJAX, "tableSettings", this.tName, "pageMode");
			}
			if(XVar.Pack(this.lockingObj))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "locking");
			}
			if((XVar)(this.warnLeavingPages)  && (XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_REGISTER)  || (XVar)(this.pageType == Constants.PAGE_ADD))  || (XVar)(this.pageType == Constants.PAGE_EDIT)))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "warnOnLeaving");
			}
			if(XVar.Pack(MVCFunctions.count(this.allDetailsTablesArr)))
			{
				dynamic dPermission = XVar.Array(), dPset = null, i = null;
				if(this.pageType == Constants.PAGE_LIST)
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "detailTables");
				}
				this.jsSettings.InitAndSetArrayItem(this.isShowDetailTables, "tableSettings", this.tName, "isShowDetails");
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
				{
					dPset = XVar.Clone(new ProjectSettings((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])));
					this.settingsMap.InitAndSetArrayItem(this.allDetailsTablesArr[i]["dShortTable"], "globalSettings", "shortTNames", this.allDetailsTablesArr[i]["dDataSourceTable"]);
					dPermission = XVar.Clone(getPermissions((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])));
					this.permis.InitAndSetArrayItem(dPermission, this.allDetailsTablesArr[i]["dDataSourceTable"]);
					this.masterKeysByD.InitAndSetArrayItem(this.allDetailsTablesArr[i]["masterKeys"], i);
					if((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  || (XVar)(this.pageType == Constants.PAGE_REPORT))  || (XVar)(this.pageType == Constants.PAGE_CHART))
					{
						XSession.Session.Remove(MVCFunctions.Concat(this.allDetailsTablesArr[i]["dDataSourceTable"], "_advsearch"));
						dPermission = XVar.Clone(getPermissions((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])));
						if(XVar.Pack(dPermission["search"]))
						{
							this.jsSettings.InitAndSetArrayItem(new XVar("pageType", this.allDetailsTablesArr[i]["dType"], "dispChildCount", this.allDetailsTablesArr[i]["dispChildCount"], "hideChild", this.allDetailsTablesArr[i]["hideChild"], "listShowType", this.allDetailsTablesArr[i]["previewOnList"], "addShowType", this.allDetailsTablesArr[i]["previewOnAdd"], "editShowType", this.allDetailsTablesArr[i]["previewOnEdit"], "viewShowType", this.allDetailsTablesArr[i]["previewOnView"], "proceedLink", this.allDetailsTablesArr[i]["proceedLink"], "label", CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"]))))), "tableSettings", this.tName, "detailTables", this.allDetailsTablesArr[i]["dDataSourceTable"]);
						}
						if(this.allDetailsTablesArr[i]["previewOnList"] == Constants.DP_POPUP)
						{
							this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isUsePopUp");
						}
					}
				}
				if((XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  || (XVar)(this.pageType == Constants.PAGE_EDIT))  && (XVar)(this.isShowDetailTables))
				{
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "dControlsMap");
				}
			}
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "video");
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "toolTips");
			addLookupSettings();
			addMultiUploadSettings();
			this.controlsMap.InitAndSetArrayItem(this.searchPanelActivated, "searchPanelActivated");
			if((XVar)(this.pageType != Constants.PAGE_LIST)  || (XVar)(this.mode != Constants.LIST_DETAILS))
			{
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "controls");
				if((XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  && (XVar)(this.mode == Constants.ADD_INLINE)))  && (XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(this.mode == Constants.EDIT_INLINE))))
				{
					dynamic allSearchFields = null;
					allSearchFields = XVar.Clone(this.pSetSearch.getAllSearchFields());
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "search");
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "search", "searchBlocks");
					this.controlsMap.InitAndSetArrayItem(allSearchFields, "search", "allSearchFields");
					this.controlsMap.InitAndSetArrayItem(getSearchFieldsLabels((XVar)(allSearchFields)), "search", "allSearchFieldsLabels");
					this.controlsMap.InitAndSetArrayItem(this.pSetSearch.getPanelSearchFields(), "search", "panelSearchFields");
					this.controlsMap.InitAndSetArrayItem(this.pSetSearch.getGoogleLikeFields(), "search", "googleLikeFields");
					this.controlsMap.InitAndSetArrayItem(!(XVar)(this.pSetSearch.isFlexibleSearch()), "search", "inflexSearchPanel");
					this.controlsMap.InitAndSetArrayItem(this.pSetSearch.getSearchRequiredFields(), "search", "requiredSearchFields");
					this.controlsMap.InitAndSetArrayItem(this.pSetSearch.noRecordsOnFirstPage(), "search", "isSearchRequired");
					this.controlsMap.InitAndSetArrayItem(this.searchTableName, "search", "searchTableName");
					this.controlsMap.InitAndSetArrayItem(this.pSetSearch.getShortTableName(), "search", "shortSearchTableName");
					if(this.pageType != Constants.PAGE_SEARCH)
					{
						this.controlsMap.InitAndSetArrayItem(this.pageType, "search", "submitPageType");
					}
					else
					{
						if(XVar.Pack(MVCFunctions.postvalue(new XVar("rname"))))
						{
							this.controlsMap.InitAndSetArrayItem("dreport", "search", "submitPageType");
							this.controlsMap.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("rname")), "search", "baseParams", "rname");
							if(XVar.Pack(XSession.Session["crossLink"]))
							{
								dynamic alink = XVar.Array();
								if(MVCFunctions.substr((XVar)(XSession.Session["crossLink"]), new XVar(0), new XVar(1)) == "&")
								{
									XSession.Session["crossLink"] = MVCFunctions.substr((XVar)(XSession.Session["crossLink"]), new XVar(1));
								}
								alink = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(XSession.Session["crossLink"])));
								foreach (KeyValuePair<XVar, dynamic> param in alink.GetEnumerator())
								{
									dynamic arrtmp = XVar.Array();
									arrtmp = XVar.Clone(MVCFunctions.explode(new XVar("="), (XVar)(param.Value)));
									this.controlsMap.InitAndSetArrayItem(arrtmp[1], "search", "baseParams", arrtmp[0]);
								}
							}
						}
						else
						{
							if(XVar.Pack(MVCFunctions.postvalue(new XVar("cname"))))
							{
								this.controlsMap.InitAndSetArrayItem("dchart", "search", "submitPageType");
								this.controlsMap.InitAndSetArrayItem(MVCFunctions.postvalue(new XVar("cname")), "search", "baseParams", "cname");
							}
							else
							{
								this.controlsMap.InitAndSetArrayItem(this.tableType, "search", "submitPageType");
							}
						}
					}
				}
			}
			this.isUseToolTips = XVar.Clone((XVar)(this.isUseToolTips)  || (XVar)(this.pSet.isUseToolTips()));
			this.googleMapCfg.InitAndSetArrayItem("", "APIcode");
			processMasterKeyValue();
			if(XVar.Pack(this.masterTable))
			{
				this.jsSettings.InitAndSetArrayItem(this.masterTable, "tableSettings", this.tName, "masterTable");
			}
			if(XVar.Pack(MVCFunctions.count(this.masterKeysReq)))
			{
				this.jsSettings.InitAndSetArrayItem(this.masterKeysReq, "tableSettings", this.tName, "masterKeys");
			}
			this.gridTabs = XVar.Clone(this.pSet.getGridTabs());
			assignSearchLogger();
		}
		public virtual XVar limitRowCount(dynamic _param_rowCount, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic rowCount = XVar.Clone(_param_rowCount);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			if(XVar.Pack(pSet == null))
			{
				pSet = XVar.UnPackProjectSettings(this.pSet);
			}
			return (XVar.Pack((XVar)(pSet.getRecordsLimit())  && (XVar)(pSet.getRecordsLimit() < rowCount)) ? XVar.Pack(pSet.getRecordsLimit()) : XVar.Pack(rowCount));
		}
		public virtual XVar getGridTab(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					return tab.Value;
				}
			}
			return false;
		}
		public virtual XVar getCurrentTab()
		{
			dynamic i = null, tab = XVar.Array();
			if(XVar.Pack(!(XVar)(this.gridTabs)))
			{
				return false;
			}
			tab = XVar.Clone(getGridTab((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_currentTab")])));
			if(XVar.Pack(tab))
			{
				if(XVar.Pack(!(XVar)(tab["hidden"])))
				{
					return tab;
				}
			}
			tab = XVar.Clone(getGridTab(new XVar("")));
			if(XVar.Pack(tab))
			{
				if(XVar.Pack(!(XVar)(tab["hidden"])))
				{
					return tab;
				}
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.gridTabs); ++(i))
			{
				if(XVar.Pack(!(XVar)(this.gridTabs[i]["hidden"])))
				{
					return this.gridTabs[i];
				}
			}
			return this.gridTabs[0];
		}
		public virtual XVar getCurrentTabWhere()
		{
			dynamic currentTab = XVar.Array();
			if(XVar.Equals(XVar.Pack(this.tabChangeling), XVar.Pack(null)))
			{
				currentTab = XVar.Clone(getCurrentTab());
			}
			else
			{
				currentTab = XVar.Clone(getGridTab((XVar)(this.tabChangeling)));
			}
			if(XVar.Pack(currentTab))
			{
				return DB.PrepareSQL((XVar)(currentTab["where"]));
			}
			return "";
		}
		public virtual XVar getCurrentTabId()
		{
			dynamic currentTab = XVar.Array();
			currentTab = XVar.Clone(getCurrentTab());
			if(XVar.Pack(currentTab))
			{
				return currentTab["tabId"];
			}
			return "";
		}
		public virtual XVar prepareGridTabs()
		{
			if(XVar.Pack(!(XVar)(this.masterTable)))
			{
				foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
				{
					dynamic masterTokent = null;
					masterTokent = XVar.Clone(DB.readMasterTokens((XVar)(tab.Value["where"])));
					if(0 < MVCFunctions.count(masterTokent))
					{
						this.gridTabs.Remove(tab.Key);
					}
				}
			}
			if(XVar.Pack(gridTabsAvailable()))
			{
				dynamic i = null, tab = XVar.Array();
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.gridTabs); ++(i))
				{
					tab = XVar.Clone(this.gridTabs[i]);
					if((XVar)(tab["showRowCount"])  || (XVar)(tab["hideEmpty"]))
					{
						this.gridTabs.InitAndSetArrayItem(getRowCountByTab((XVar)(tab["tabId"])), i, "count");
						if((XVar)(tab["hideEmpty"])  && (XVar)(!(XVar)(this.gridTabs[i]["count"])))
						{
							this.gridTabs.InitAndSetArrayItem(true, i, "hidden");
						}
					}
				}
			}
			return null;
		}
		public virtual XVar getTabsHtml()
		{
			dynamic currentTab = XVar.Array(), tabs = XVar.Array();
			currentTab = XVar.Clone(getCurrentTab());
			tabs = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				dynamic countRowHtml = null, linkAttrs = XVar.Array(), tabAttrs = XVar.Array();
				linkAttrs = XVar.Clone(XVar.Array());
				linkAttrs.InitAndSetArrayItem(MVCFunctions.Concat("href=\"", MVCFunctions.GetTableLink((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(MVCFunctions.Concat("tab=", tab.Value["tabId"]))), "\""), null);
				linkAttrs.InitAndSetArrayItem(MVCFunctions.Concat("data-pageid=\"", this.id, "\""), null);
				linkAttrs.InitAndSetArrayItem(MVCFunctions.Concat("data-tabid=\"", tab.Value["tabId"], "\""), null);
				tabAttrs = XVar.Clone(XVar.Array());
				if(XVar.Equals(XVar.Pack(currentTab["tabId"]), XVar.Pack(tab.Value["tabId"])))
				{
					tabAttrs.InitAndSetArrayItem("class=\"active\"", null);
				}
				if(XVar.Pack(tab.Value["hidden"]))
				{
					tabAttrs.InitAndSetArrayItem("data-hidden", null);
				}
				countRowHtml = XVar.Clone((XVar.Pack(tab.Value["showRowCount"]) ? XVar.Pack(MVCFunctions.Concat("&nbsp;(", tab.Value["count"], ")")) : XVar.Pack("")));
				tabs.InitAndSetArrayItem(MVCFunctions.Concat("<li ", MVCFunctions.implode(new XVar(" "), (XVar)(tabAttrs)), "><a ", MVCFunctions.implode(new XVar(" "), (XVar)(linkAttrs)), ">", getTabTitle((XVar)(tab.Value["tabId"])), countRowHtml, "</a></li>"), null);
			}
			return MVCFunctions.implode(new XVar(""), (XVar)(tabs));
		}
		public virtual XVar getGridTabsCount()
		{
			dynamic tcount = null;
			tcount = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(tab.Value["hidden"])))
				{
					++(tcount);
				}
			}
			return tcount;
		}
		protected virtual XVar getRowCountByTab(dynamic _param_tab)
		{
			#region pass-by-value parameters
			dynamic tab = XVar.Clone(_param_tab);
			#endregion

			return 0;
		}
		public virtual XVar processGridTabs()
		{
			dynamic html = null, tabId = null;
			this.pageData.InitAndSetArrayItem("", "gridTabs");
			tabId = XVar.Clone(getCurrentTabId());
			prepareGridTabs();
			if(MVCFunctions.count(this.gridTabs) <= 1)
			{
				return null;
			}
			html = XVar.Clone(getTabsHtml());
			this.pageData.InitAndSetArrayItem(html, "gridTabs");
			this.pageData.InitAndSetArrayItem(getCurrentTabId(), "tabId");
			if(XVar.Pack(displayTabsInPage()))
			{
				this.xt.assign(new XVar("grid_tabs"), new XVar(true));
				this.xt.assign(new XVar("grid_tabs_content"), (XVar)(html));
			}
			return tabId != this.pageData["tabId"];
		}
		public virtual XVar addTab(dynamic _param_where, dynamic _param_title, dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic where = XVar.Clone(_param_where);
			dynamic title = XVar.Clone(_param_title);
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			if(!XVar.Equals(XVar.Pack(getGridTab((XVar)(tabId))), XVar.Pack(false)))
			{
				return false;
			}
			this.gridTabs.InitAndSetArrayItem(new XVar("tabId", tabId, "name", title, "nameType", "Text", "where", where), null);
			return true;
		}
		public virtual XVar deleteTab(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			dynamic deleteKey = null;
			deleteKey = new XVar(false);
			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					deleteKey = XVar.Clone(tab.Key);
					break;
				}
			}
			if(!XVar.Equals(XVar.Pack(deleteKey), XVar.Pack(false)))
			{
				this.gridTabs.Remove(deleteKey);
			}
			return null;
		}
		public virtual XVar setTabTitle(dynamic _param_tabId, dynamic _param_title)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			dynamic title = XVar.Clone(_param_title);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					this.gridTabs.InitAndSetArrayItem(title, tab.Key, "name");
					this.gridTabs.InitAndSetArrayItem("Text", tab.Key, "nameType");
					return true;
				}
			}
			return false;
		}
		public virtual XVar setTabWhere(dynamic _param_tabId, dynamic _param_where)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			dynamic where = XVar.Clone(_param_where);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					this.gridTabs.InitAndSetArrayItem(where, tab.Key, "where");
					return true;
				}
			}
			return false;
		}
		public virtual XVar getTabTitle(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			dynamic tab = XVar.Array();
			tab = XVar.Clone(getTabInfo((XVar)(tabId)));
			if(XVar.Pack(!(XVar)(tab)))
			{
				return false;
			}
			if(!XVar.Equals(XVar.Pack(tab["nameType"]), XVar.Pack("Text")))
			{
				return CommonFunctions.GetCustomLabel((XVar)(tab["name"]));
			}
			return tab["name"];
		}
		public virtual XVar getTabInfo(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					return tab.Value;
				}
			}
			return null;
		}
		public virtual XVar getTabFlags(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			dynamic flags = XVar.Array(), tab = XVar.Array();
			flags = XVar.Clone(XVar.Array());
			tab = XVar.Clone(getGridTab((XVar)(tabId)));
			if(XVar.Pack(!(XVar)(tab)))
			{
				return false;
			}
			flags.InitAndSetArrayItem(tab["showRowCount"], "showCount");
			flags.InitAndSetArrayItem(tab["hideEmpty"], "hideEmpty");
			return flags;
		}
		public virtual XVar setTabFlags(dynamic _param_tabId, dynamic _param_flags)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			dynamic flags = XVar.Clone(_param_flags);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> tab in this.gridTabs.GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(tab.Value["tabId"]), XVar.Pack(tabId)))
				{
					if(XVar.Pack(flags.KeyExists("showCount")))
					{
						this.gridTabs.InitAndSetArrayItem(flags["showCount"], tab.Key, "showRowCount");
					}
					if(XVar.Pack(flags.KeyExists("showCount")))
					{
						this.gridTabs.InitAndSetArrayItem(flags["hideEmpty"], tab.Key, "hideEmpty");
					}
					return true;
				}
			}
			return false;
		}
		protected virtual XVar getLockingObject()
		{
			return CommonFunctions.GetLockingObject((XVar)(this.tName));
		}
		public virtual XVar isDashboardElement()
		{
			if(this.dashElementName == "")
			{
				return false;
			}
			return true;
		}
		public virtual XVar init()
		{
			if(XVar.Pack(this.xt))
			{
				this.xt.assign(new XVar("pagetitle"), (XVar)(getPageTitle((XVar)(this.pageType), (XVar)((XVar.Pack(this.tName == Constants.NOT_TABLE_BASED_TNAME) ? XVar.Pack("") : XVar.Pack(MVCFunctions.GoodFieldName((XVar)(this.tName))))))));
			}
			buildAddedSearchPanel();
			initLogin();
			if((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  && (XVar)((XVar)(this.mode == Constants.LIST_AJAX)  || (XVar)(this.mode == Constants.LIST_SIMPLE)))  || (XVar)(this.pageType == Constants.PAGE_DASHBOARD))  || (XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_REPORT)  && (XVar)(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.REPORT_SIMPLE))))  || (XVar)((XVar)(this.pageType == Constants.PAGE_CHART)  && (XVar)(this.mode == Constants.CHART_SIMPLE))))
			{
				buildFilterPanel();
			}
			return null;
		}
		protected virtual XVar setTableConnection()
		{
			if(this.tName != Constants.NOT_TABLE_BASED_TNAME)
			{
				this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.tName)));
			}
			return null;
		}
		protected virtual XVar assignCipherer()
		{
			this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.tName), (XVar)(this.pSet)));
			return null;
		}
		public virtual XVar initLogin()
		{
			dynamic loggedAsGuest = null;
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("bTwoFactorAuth"), new XVar(false)), "globalSettings", "twoFactorAuth");
			this.settingsMap.InitAndSetArrayItem(CommonFunctions.GetGlobalData(new XVar("nLoginForm"), new XVar(0)), "globalSettings", "loginFormType");
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.settingsMap.InitAndSetArrayItem(0, "globalSettings", "loginFormType");
			}
			this.xt.assign(new XVar("security_block"), new XVar(true));
			this.xt.assign(new XVar("username"), (XVar)(XSession.Session["UserName"]));
			this.xt.assign(new XVar("logoutlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"logoutButton", this.id, "\"")));
			loggedAsGuest = XVar.Clone(CommonFunctions.isLoggedAsGuest());
			this.xt.assign(new XVar("loggedas_message"), (XVar)(!(XVar)(loggedAsGuest)));
			this.xt.assign(new XVar("guestloginbutton"), (XVar)(loggedAsGuest));
			this.xt.assign(new XVar("logoutbutton"), (XVar)((XVar)(CommonFunctions.isSingleSign())  && (XVar)(!(XVar)(loggedAsGuest))));
			if(XVar.Pack(mobileTemplateMode()))
			{
				this.xt.assign(new XVar("guestloginlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"loginButton", this.id, "\"")));
				return null;
			}
			this.xt.assign(new XVar("guestloginlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"loginButton", this.id, "\"")));
			return null;
		}
		protected virtual XVar getSqlPreparedLoginTableValue(dynamic _param_value, dynamic _param_fName, dynamic _param_fieldType, dynamic _param_cipherer = null)
		{
			#region default values
			if(_param_cipherer as Object == null) _param_cipherer = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic fName = XVar.Clone(_param_fName);
			dynamic fieldType = XVar.Clone(_param_fieldType);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			if(XVar.Pack(!(XVar)(cipherer)))
			{
				cipherer = XVar.Clone(this.cipherer);
			}
			if(XVar.Pack(cipherer.isFieldEncrypted((XVar)(fName))))
			{
				return cipherer.MakeDBValue((XVar)(fName), (XVar)(value), new XVar(""), new XVar(true));
			}
			if(XVar.Pack(CommonFunctions.NeedQuotes((XVar)(fieldType))))
			{
				return this.connection.prepareString((XVar)(value));
			}
			return 0 + value;
		}
		public virtual XVar assignAdmin()
		{
			if(XVar.Pack(isAdminTable()))
			{
				this.xt.assign(new XVar("exitadminarea_link"), new XVar(true));
				this.xt.assign(new XVar("exitaalink_attrs"), (XVar)(MVCFunctions.Concat("id=\"exitAdminArea", this.id, "\"")));
			}
			if((XVar)(this.isDynamicPerm)  && (XVar)(CommonFunctions.IsAdmin()))
			{
				this.xt.assign(new XVar("adminarea_link"), new XVar(true));
				this.xt.assign(new XVar("adminarealink_attrs"), (XVar)(MVCFunctions.Concat("id=\"adminArea", this.id, "\"")));
			}
			return null;
		}
		protected virtual XVar assignSessionPrefix()
		{
			this.sessionPrefix = XVar.Clone(this.tName);
			return null;
		}
		public virtual XVar isSearchSavingEnabled()
		{
			dynamic searchSavingEnabled = null;
			searchSavingEnabled = XVar.Clone(this.pSet.isSearchSavingEnabled());
			if(XVar.Pack(!(XVar)(searchSavingEnabled)))
			{
				return false;
			}
			return (XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  && (XVar)((XVar)(this.mode == Constants.LIST_AJAX)  || (XVar)(this.mode == Constants.LIST_SIMPLE)))  || (XVar)((XVar)(this.pageType == Constants.PAGE_REPORT)  && (XVar)(this.mode == Constants.REPORT_SIMPLE)))  || (XVar)((XVar)(this.pageType == Constants.PAGE_CHART)  && (XVar)(this.mode == Constants.CHART_SIMPLE));
		}
		protected virtual XVar assignSearchLogger()
		{
			dynamic savedSearches = null;
			if((XVar)(!(XVar)(this.searchSavingEnabled))  || (XVar)(!(XVar)(this.searchClauseObj)))
			{
				return null;
			}
			this.searchLogger = XVar.Clone(new searchParamsLogger((XVar)(this.tName)));
			this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "searchSaving");
			savedSearches = XVar.Clone(this.searchLogger.getSavedSeachesParams());
			if(XVar.Pack(MVCFunctions.count(savedSearches)))
			{
				this.pageHasSavedSearches = new XVar(true);
				this.controlsMap.InitAndSetArrayItem(savedSearches, "search", "savedSearches");
				this.controlsMap.InitAndSetArrayItem(this.searchClauseObj.savedSearchIsRun, "search", "savedSearchIsRun");
			}
			assignSearchSavingButtons();
			return null;
		}
		public virtual XVar processSaveSearch()
		{
			dynamic searchName = null;
			if((XVar)((XVar)(MVCFunctions.postvalue(new XVar("saveSearch")))  && (XVar)(MVCFunctions.postvalue(new XVar("searchName"))))  && (XVar)(!(XVar)(this.searchLogger == null)))
			{
				dynamic searchParams = null;
				searchName = XVar.Clone(MVCFunctions.postvalue(new XVar("searchName")));
				searchParams = XVar.Clone(getSearchParamsForSaving());
				this.searchLogger.saveSearch((XVar)(searchName), (XVar)(searchParams));
				this.searchClauseObj.savedSearchIsRun = new XVar(true);
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
				MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(searchParams)));
				return true;
			}
			if((XVar)((XVar)(MVCFunctions.postvalue(new XVar("deleteSearch")))  && (XVar)(MVCFunctions.postvalue(new XVar("searchName"))))  && (XVar)(!(XVar)(this.searchLogger == null)))
			{
				searchName = XVar.Clone(MVCFunctions.postvalue(new XVar("searchName")));
				this.searchLogger.deleteSearch((XVar)(searchName));
				MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(XVar.Array())));
				return true;
			}
			return false;
		}
		protected virtual XVar assignSearchSavingButtons()
		{
			this.xt.assign(new XVar("searchsaving_block"), new XVar(true));
			if((XVar)(this.searchClauseObj.isSearchFunctionalityActivated())  && (XVar)(!(XVar)(this.searchClauseObj.savedSearchIsRun)))
			{
				this.xt.assign(new XVar("saveSeachButton"), new XVar(true));
			}
			this.xt.assign(new XVar("savedSeachesButton"), new XVar(true));
			if(XVar.Pack(!(XVar)(this.pageHasSavedSearches)))
			{
				this.xt.assign(new XVar("saveSearchButtonAttrs"), (XVar)(this.dispNoneStyle));
			}
			return null;
		}
		public virtual XVar getSearchParamsForSaving()
		{
			return this.searchClauseObj.getSearchParamsForSaving();
		}
		protected virtual XVar getSearchFieldsLabels(dynamic _param_searchFields)
		{
			#region pass-by-value parameters
			dynamic searchFields = XVar.Clone(_param_searchFields);
			#endregion

			dynamic sFieldLabels = XVar.Array();
			sFieldLabels = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> sField in searchFields.GetEnumerator())
			{
				sFieldLabels.InitAndSetArrayItem(this.pSetSearch.label((XVar)(sField.Value)), sField.Value);
			}
			return sFieldLabels;
		}
		public virtual XVar spreadRowStyles(dynamic data, dynamic row, dynamic record)
		{
			spreadRowStyle((XVar)(data), (XVar)(row), (XVar)(record));
			spreadRowCssStyle((XVar)(data), (XVar)(row), (XVar)(record));
			return null;
		}
		protected virtual XVar spreadRowStyle(dynamic data, dynamic row, dynamic record)
		{
			dynamic style = null;
			if(XVar.Pack(!(XVar)(row.KeyExists("rowstyle"))))
			{
				return null;
			}
			style = XVar.Clone(CommonFunctions.extractStyle((XVar)(row["rowstyle"])));
			if(style == XVar.Pack(""))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> field in MVCFunctions.array_keys((XVar)(data)).GetEnumerator())
			{
				record.InitAndSetArrayItem(CommonFunctions.injectStyle((XVar)(record[MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field.Value)), "_style")]), (XVar)(style)), MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field.Value)), "_style"));
			}
			return null;
		}
		protected virtual XVar spreadRowCssStyle(dynamic data, dynamic row, dynamic record)
		{
			dynamic style = null;
			if(XVar.Pack(!(XVar)(row.KeyExists("style"))))
			{
				return null;
			}
			style = XVar.Clone(row["style"]);
			if(MVCFunctions.trim((XVar)(style)) == "")
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> field in MVCFunctions.array_keys((XVar)(data)).GetEnumerator())
			{
				record.InitAndSetArrayItem(MVCFunctions.Concat(style, "; ", record[MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field.Value)), "_css")]), MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field.Value)), "_css"));
			}
			return null;
		}
		protected virtual XVar setRowCssRule(dynamic _param_rowCssRule)
		{
			#region pass-by-value parameters
			dynamic rowCssRule = XVar.Clone(_param_rowCssRule);
			#endregion

			dynamic selectors = null;
			selectors = XVar.Clone(MVCFunctions.Concat(" td[data-record-id=\"", this.recId, "\"]"));
			if(this.listGridLayout == Constants.gltVERTICAL)
			{
				selectors = MVCFunctions.Concat(selectors, " td");
			}
			this.row_css_rules = MVCFunctions.Concat(this.row_css_rules, selectors, "{", getCustomCSSRule((XVar)(rowCssRule)), "}");
			return null;
		}
		protected virtual XVar getCustomCSSRule(dynamic _param_unprocessedCss)
		{
			#region pass-by-value parameters
			dynamic unprocessedCss = XVar.Clone(_param_unprocessedCss);
			#endregion

			dynamic cssRules = XVar.Array(), i = null, rules = XVar.Array();
			cssRules = XVar.Clone(XVar.Array());
			rules = XVar.Clone(MVCFunctions.explode(new XVar(";"), (XVar)(unprocessedCss)));
			i = new XVar(0);
			for(;i < MVCFunctions.count(rules); i++)
			{
				if(MVCFunctions.trim((XVar)(rules[i])) != "")
				{
					cssRules.InitAndSetArrayItem(MVCFunctions.Concat(rules[i], " !important"), null);
				}
			}
			return MVCFunctions.implode(new XVar(";"), (XVar)(cssRules));
		}
		protected virtual XVar setFieldCssRule(dynamic _param_fieldCssRule, dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldCssRule = XVar.Clone(_param_fieldCssRule);
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			dynamic className = null, selectors = null;
			if(XVar.Pack(this.fieldCssRules.KeyExists(fieldCssRule)))
			{
				return this.fieldCssRules[fieldCssRule];
			}
			className = XVar.Clone(MVCFunctions.Concat("rnr-style", this.recId, "-", fieldName));
			this.fieldCssRules.InitAndSetArrayItem(className, fieldCssRule);
			if(this.listGridLayout == Constants.gltVERTICAL)
			{
				selectors = XVar.Clone(MVCFunctions.Concat(" td[data-record-id] td.", className, ", .", className));
			}
			else
			{
				selectors = XVar.Clone(MVCFunctions.Concat(" td[data-record-id].", className, ", .", className));
			}
			this.cell_css_rules = MVCFunctions.Concat(this.cell_css_rules, selectors, "{", getCustomCSSRule((XVar)(fieldCssRule)), "}");
			return className;
		}
		public virtual XVar addCustomCss()
		{
			dynamic gbl = XVar.Array();
			if((XVar)((XVar)(!(XVar)(this.cell_css_rules))  && (XVar)(!(XVar)(this.row_css_rules)))  && (XVar)(!(XVar)(this.mobile_css_rules)))
			{
				return null;
			}
			gbl = XVar.Clone(this.xt.getVar(new XVar("grid_block")));
			if(XVar.Pack(gbl))
			{
				dynamic rules = null;
				rules = XVar.Clone(MVCFunctions.Concat(this.row_css_rules, this.cell_css_rules, "\n", this.mobile_css_rules));
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(gbl)))))
				{
					gbl = XVar.Clone(new XVar("begin", MVCFunctions.Concat("<style class=\"rnr-cells-css\" type=\"text/css\"> ", rules, " </style>")));
				}
				else
				{
					gbl.InitAndSetArrayItem(MVCFunctions.Concat(gbl["begin"], "<style class=\"rnr-cells-css\" type=\"text/css\"> ", rules, " </style>"), "begin");
				}
				this.xt.assign(new XVar("grid_block"), (XVar)(gbl));
			}
			return null;
		}
		public virtual XVar setRowCssRules(dynamic record)
		{
			if(XVar.Pack(record["css"]))
			{
				setRowCssRule((XVar)(record["css"]));
			}
			if(XVar.Pack(record["hovercss"]))
			{
				this.Invoke("setRowHoverCssRule", (XVar)(record["hovercss"]));
			}
			return null;
		}
		public virtual XVar setRowClassNames(dynamic record, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic className = null, gFieldName = null;
			gFieldName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
			record[MVCFunctions.Concat(gFieldName, "_class")] = MVCFunctions.Concat(record[MVCFunctions.Concat(gFieldName, "_class")], fieldClass((XVar)(field)));
			if(XVar.Pack(record[MVCFunctions.Concat(gFieldName, "_css")]))
			{
				className = XVar.Clone(setFieldCssRule((XVar)(record[MVCFunctions.Concat(gFieldName, "_css")]), (XVar)(gFieldName)));
				record[MVCFunctions.Concat(gFieldName, "_class")] = MVCFunctions.Concat(record[MVCFunctions.Concat(gFieldName, "_class")], " ", className);
			}
			if(XVar.Pack(record[MVCFunctions.Concat(gFieldName, "_hovercss")]))
			{
				dynamic classNameHover = null;
				classNameHover = XVar.Clone(this.Invoke("setRowHoverCssRule", (XVar)(record[MVCFunctions.Concat(gFieldName, "_hovercss")]), (XVar)(gFieldName)));
				if(!XVar.Equals(XVar.Pack(classNameHover), XVar.Pack(className)))
				{
					record[MVCFunctions.Concat(gFieldName, "_class")] = MVCFunctions.Concat(record[MVCFunctions.Concat(gFieldName, "_class")], " ", classNameHover);
				}
			}
			return null;
		}
		public virtual XVar getLayoutName()
		{
			if(XVar.Pack(this.pageLayout))
			{
				return this.pageLayout.style;
			}
			else
			{
				return "";
			}
			return null;
		}
		public virtual XVar getLayoutVersion()
		{
			if(XVar.Pack(this.pageLayout))
			{
				return this.pageLayout.version;
			}
			else
			{
				return 2;
			}
			return null;
		}
		public virtual XVar addMultiUploadSettings()
		{
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "autoUpload"), "fieldSettings", "autoUpload");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", ".+$", "jsName", "acceptFileTypes"), "fieldSettings", "acceptFileTypes");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "compatibilityMode"), "fieldSettings", "CompatibilityMode");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", null, "jsName", "maxFileSize"), "fieldSettings", "maxFileSize");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", null, "jsName", "maxTotalFilesSize"), "fieldSettings", "maxTotalFilesSize");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 1, "jsName", "maxNumberOfFiles"), "fieldSettings", "maxNumberOfFiles");
			return null;
		}
		public virtual XVar processMasterKeyValue()
		{
			dynamic i = null;
			if(XVar.Pack(MVCFunctions.count(this.masterKeysReq)))
			{
				i = new XVar(1);
				for(;i <= MVCFunctions.count(this.masterKeysReq); i++)
				{
					XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i)] = this.masterKeysReq[i];
				}
				if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i))))
				{
					XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i));
				}
			}
			else
			{
				if(XVar.Pack(MVCFunctions.count(this.detailKeysByM)))
				{
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.detailKeysByM); i++)
					{
						if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1))))
						{
							this.masterKeysReq.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1)], i + 1);
						}
					}
				}
			}
			if(XVar.Pack(this.masterTable))
			{
				XSession.Session[MVCFunctions.Concat(this.masterTable, "_masterRecordData")] = getMasterRecord();
			}
			return null;
		}
		public virtual XVar displayMasterTableInfo()
		{
			dynamic backButtonHref = null, checkDisplay = null, detailKeys = null, detailtable = null, j = null, keys = null, mParams = XVar.Array(), master = null, masterKeys = XVar.Array(), masterPage = null, masterTableData = XVar.Array(), mrData = null, tc = null, var_params = XVar.Array();
			XTempl xt;
			masterTableData = XVar.Clone(getMasterTableInfo());
			if(XVar.Pack(!(XVar)(masterTableData)))
			{
				return null;
			}
			backButtonHref = XVar.Clone(MVCFunctions.GetTableLink((XVar)(masterTableData["mShortTable"]), (XVar)(masterTableData["type"]), new XVar("a=return")));
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				this.xt.assign(new XVar("mastertable_block"), new XVar(true));
				this.xt.assign(new XVar("backtomasterlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", backButtonHref, "\"")));
				this.xt.assign(new XVar("backtomasterlink_caption"), (XVar)(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(masterTableData["mDataSourceTable"]))))));
			}
			checkDisplay = XVar.Clone(this.pageType);
			if((XVar)(this.pageType == Constants.PAGE_REPORT)  || (XVar)(this.pageType == Constants.PAGE_CHART))
			{
				checkDisplay = new XVar(Constants.PAGE_LIST);
			}
			else
			{
				if(this.pageType == Constants.PAGE_RPRINT)
				{
					checkDisplay = new XVar(Constants.PAGE_PRINT);
				}
			}
			if(XVar.Pack(!(XVar)(masterTableData["dispMasterInfo"][checkDisplay])))
			{
				return null;
			}
			if((XVar)((XVar)(this.pageType == Constants.PAGE_PRINT)  || (XVar)(this.pageType == Constants.PAGE_RPRINT))  && (XVar)(masterTableData["type"] == Constants.PAGE_CHART))
			{
				return null;
			}
			this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "hasMasterList");
			detailKeys = XVar.Clone(masterTableData["detailKeys"]);
			masterKeys = XVar.Clone(XVar.Array());
			j = new XVar(0);
			for(;j < MVCFunctions.count(detailKeys); j++)
			{
				masterKeys.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", j + 1)], null);
			}
			addMasterInfoJSAndCSS((XVar)(masterTableData["type"]), (XVar)(masterTableData["mDataSourceTable"]), (XVar)(masterTableData["mShortTable"]));
			master = XVar.Clone(XVar.Array());
			mrData = XVar.Clone(getListMasterRecordData((XVar)(masterTableData["mDataSourceTable"]), (XVar)(masterKeys)));
			var_params = XVar.Clone(new XVar("detailtable", this.tName, "keys", masterKeys, "recId", this.recId, "masterRecordData", mrData));
			keys = XVar.Clone(var_params["keys"]);
			detailtable = XVar.Clone(var_params["detailtable"]);
			xt = XVar.UnPackXTempl(new XTempl());
			xt.eventsObject = XVar.Clone(CommonFunctions.getEventObject((XVar)(this.masterTable)));
			mParams = XVar.Clone(XVar.Array());
			mParams.InitAndSetArrayItem(xt, "xt");
			mParams.InitAndSetArrayItem(var_params["recId"], "flyId");
			mParams.InitAndSetArrayItem(var_params["recId"], "id");
			mParams.InitAndSetArrayItem(mrData, "masterRecordData");
			mParams.InitAndSetArrayItem(false, "pushContext");
			mParams.InitAndSetArrayItem(masterTableData["type"], "masterPageType");
			if((XVar)(this.pageType == Constants.PAGE_PRINT)  || (XVar)(this.pageType == Constants.PAGE_RPRINT))
			{
				if(mParams["masterPageType"] == Constants.PAGE_REPORT)
				{
					mParams.InitAndSetArrayItem(Constants.PAGE_RPRINT, "pageType");
				}
				else
				{
					mParams.InitAndSetArrayItem(Constants.PAGE_PRINT, "pageType");
				}
				mParams.InitAndSetArrayItem(this.masterTable, "tName");
				mParams.InitAndSetArrayItem(Constants.PRINT_MASTER, "mode");
				masterPage = XVar.Clone(new PrintPage_Master((XVar)(mParams)));
			}
			else
			{
				if(mParams["masterPageType"] == Constants.PAGE_CHART)
				{
					mParams.InitAndSetArrayItem(this.masterTable, "tName");
					mParams.InitAndSetArrayItem(Constants.PAGE_CHART, "pageType");
					mParams.InitAndSetArrayItem(Constants.CHART_SIMPLE, "pageMode");
					masterPage = XVar.Clone(new ChartPage_Master((XVar)(mParams)));
				}
				else
				{
					mParams.InitAndSetArrayItem(mParams["masterPageType"], "pageType");
					mParams.InitAndSetArrayItem(Constants.LIST_MASTER, "mode");
					masterPage = XVar.Clone(ListPage.createListPage((XVar)(this.masterTable), (XVar)(mParams)));
				}
			}
			using(tc = XVar.Clone(new TempPageContext((XVar)(masterPage))))
			{
				masterPage.init();
				masterPage.preparePage();
				this.xt.assign(new XVar("mastertable_block"), new XVar(true));
				backButtonHref = XVar.Clone(MVCFunctions.GetTableLink((XVar)(masterTableData["mShortTable"]), (XVar)(masterTableData["type"]), new XVar("a=return")));
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					masterPage.xt.assign(new XVar("backtomasterlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", backButtonHref, "\"")));
					masterPage.xt.assign(new XVar("backtomasterlink_caption"), (XVar)(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(masterTableData["mDataSourceTable"]))))));
				}
				else
				{
					this.xt.assign(new XVar("backtomasterlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", backButtonHref, "\"")));
					this.xt.assign(new XVar("backtomasterlink_caption"), (XVar)(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(masterTableData["mDataSourceTable"]))))));
				}
				this.xt.assign(new XVar("master_heading"), (XVar)(masterPage.getMasterHeading()));
				this.xt.assign_method(new XVar("showmasterfile"), (XVar)(masterPage), new XVar("showMaster"), (XVar)(XVar.Array()));
				addMasterMapsSettings((XVar)(masterTableData["mDataSourceTable"]), (XVar)(this.recId + 1), (XVar)(mrData));
				genId();
			}
			return null;
		}
		public virtual XVar getListMasterRecordData(dynamic _param_mTName, dynamic _param_masterKeys)
		{
			#region pass-by-value parameters
			dynamic mTName = XVar.Clone(_param_mTName);
			dynamic masterKeys = XVar.Clone(_param_masterKeys);
			#endregion

			dynamic connection = null, detailtable = null, mCiph = null, mPSet = null, masterQuery = null, whereParts = XVar.Array();
			detailtable = XVar.Clone(this.tName);
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(mTName)));
			mPSet = XVar.Clone(new ProjectSettings((XVar)(mTName), new XVar(Constants.PAGE_LIST)));
			mCiph = XVar.Clone(new RunnerCipherer((XVar)(mTName)));
			whereParts = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> dt in mPSet.getDetailTablesArr().GetEnumerator())
			{
				if(dt.Value["dDataSourceTable"] == detailtable)
				{
					foreach (KeyValuePair<XVar, dynamic> mk in dt.Value["masterKeys"].GetEnumerator())
					{
						whereParts.InitAndSetArrayItem(MVCFunctions.Concat(_getFieldSQLDecrypt((XVar)(mk.Value), (XVar)(connection), (XVar)(mPSet), (XVar)(mCiph)), "=", mCiph.MakeDBValue((XVar)(mk.Value), (XVar)(masterKeys[mk.Key]), new XVar(""), new XVar(true))), null);
					}
					break;
				}
			}
			whereParts.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(mTName)), null);
			masterQuery = XVar.Clone(mPSet.getSQLQuery());
			GlobalVars.strSQL = XVar.Clone(masterQuery.buildSQL_default((XVar)(whereParts)));
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			return mCiph.DecryptFetchedArray((XVar)(connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc()));
		}
		public virtual XVar addMasterMapsSettings(dynamic _param_mTName, dynamic _param_recId, dynamic data)
		{
			#region pass-by-value parameters
			dynamic mTName = XVar.Clone(_param_mTName);
			dynamic recId = XVar.Clone(_param_recId);
			#endregion

			dynamic haveMap = null, mPSet = null;
			mPSet = XVar.Clone(new ProjectSettings((XVar)(mTName), new XVar(Constants.PAGE_LIST)));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(data))))
			{
				return null;
			}
			haveMap = new XVar(false);
			foreach (KeyValuePair<XVar, dynamic> fName in mPSet.getMasterListFields().GetEnumerator())
			{
				dynamic address = null, desc = null, fieldMapData = XVar.Array(), keys = null, lat = null, lng = null, mapData = XVar.Array(), mapId = null, viewLink = null;
				fieldMapData = XVar.Clone(mPSet.getMapData((XVar)(fName.Value)));
				if(XVar.Pack(!(XVar)(MVCFunctions.count(fieldMapData))))
				{
					continue;
				}
				mapData = XVar.Clone(XVar.Array());
				mapData.InitAndSetArrayItem(fName.Value, "fName");
				mapData.InitAndSetArrayItem((XVar.Pack(fieldMapData.KeyExists("zoom")) ? XVar.Pack(fieldMapData["zoom"]) : XVar.Pack("")), "zoom");
				mapData.InitAndSetArrayItem("FIELD_MAP", "type");
				mapData.InitAndSetArrayItem(data[fName.Value], "mapFieldValue");
				address = XVar.Clone((XVar.Pack(data[fieldMapData["address"]]) ? XVar.Pack(data[fieldMapData["address"]]) : XVar.Pack("")));
				lat = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[fieldMapData["lat"]]) ? XVar.Pack(data[fieldMapData["lat"]]) : XVar.Pack("")))));
				lng = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[fieldMapData["lng"]]) ? XVar.Pack(data[fieldMapData["lng"]]) : XVar.Pack("")))));
				desc = XVar.Clone((XVar.Pack(data[fieldMapData["desc"]]) ? XVar.Pack(data[fieldMapData["desc"]]) : XVar.Pack(address)));
				mapData.InitAndSetArrayItem(new XVar("address", address, "lat", lat, "lng", lng, "link", viewLink, "desc", desc, "keys", keys, "mapIcon", mPSet.getMapIcon((XVar)(fName.Value), (XVar)(data))), "markers", null);
				mapId = XVar.Clone(MVCFunctions.Concat("littleMap_", MVCFunctions.GoodFieldName((XVar)(fName.Value)), "_", recId));
				this.googleMapCfg.InitAndSetArrayItem(mapData, "mapsData", mapId);
				this.googleMapCfg.InitAndSetArrayItem(mapId, "fieldMapsIds", null);
				haveMap = new XVar(true);
			}
			if(XVar.Pack(haveMap))
			{
				this.googleMapCfg.InitAndSetArrayItem(true, "isUseGoogleMap");
				this.googleMapCfg.InitAndSetArrayItem(true, "isUseFieldsMaps");
			}
			return null;
		}
		protected virtual XVar addMasterInfoJSAndCSS(dynamic _param_mPageType, dynamic _param_mTableName, dynamic _param_mShortTableName)
		{
			#region pass-by-value parameters
			dynamic mPageType = XVar.Clone(_param_mPageType);
			dynamic mTableName = XVar.Clone(_param_mTableName);
			dynamic mShortTableName = XVar.Clone(_param_mShortTableName);
			#endregion

			dynamic layout = null, mastertype = null;
			if(mPageType == Constants.PAGE_CHART)
			{
				mastertype = new XVar("masterchart");
			}
			else
			{
				if(mPageType == Constants.PAGE_REPORT)
				{
					mastertype = new XVar("masterreport");
				}
				else
				{
					mastertype = new XVar("masterlist");
				}
			}
			if(mPageType != Constants.PAGE_CHART)
			{
				dynamic viewControls = null;
				viewControls = XVar.Clone(new ViewControlsContainer((XVar)(new ProjectSettings((XVar)(mTableName), (XVar)(mPageType))), (XVar)(mPageType)));
				viewControls.addControlsJSAndCSS();
				this.includes_js = XVar.Clone(MVCFunctions.array_merge((XVar)(this.includes_js), (XVar)(viewControls.includes_js)));
				this.includes_jsreq = XVar.Clone(MVCFunctions.array_merge((XVar)(this.includes_jsreq), (XVar)(viewControls.includes_jsreq)));
				this.includes_css = XVar.Clone(MVCFunctions.array_merge((XVar)(this.includes_css), (XVar)(viewControls.includes_css)));
				this.viewControlsMap.InitAndSetArrayItem(viewControls.viewControlsMap, "mViewControlsMap");
			}
			layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(mShortTableName), (XVar)(mastertype)));
			if(XVar.Pack(layout))
			{
				dynamic layoutMobile = null;
				layoutMobile = XVar.Clone(CommonFunctions.isPageLayoutMobile((XVar)(MVCFunctions.GetTemplateName((XVar)(mShortTableName), (XVar)(mastertype)))));
				AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(layoutMobile), (XVar)(this.pdfMode != ""))));
			}
			return null;
		}
		public virtual XVar getMasterRecord()
		{
			dynamic i = null, masterConnection = null, masterQuery = null, masterTablesInfoArr = XVar.Array(), settings = null, whereClauses = XVar.Array();
			if(XVar.Pack(this.masterRecordData))
			{
				return this.masterRecordData;
			}
			if(XVar.Pack(!(XVar)(this.masterTable)))
			{
				return null;
			}
			settings = XVar.Clone(new ProjectSettings((XVar)(this.masterTable), new XVar(Constants.PAGE_LIST)));
			masterConnection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.masterTable)));
			whereClauses = XVar.Clone(XVar.Array());
			masterTablesInfoArr = XVar.Clone(this.pSet.getMasterTablesArr((XVar)(this.tName)));
			i = new XVar(0);
			for(;i < MVCFunctions.count(masterTablesInfoArr); i++)
			{
				if(this.masterTable == masterTablesInfoArr[i]["mDataSourceTable"])
				{
					dynamic j = null, mKey = null, masterKeys = XVar.Array();
					masterKeys = XVar.Clone(getActiveMasterKeys());
					GlobalVars.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.masterTable)));
					j = new XVar(0);
					for(;j < MVCFunctions.count(masterTablesInfoArr[i]["masterKeys"]); j++)
					{
						mKey = XVar.Clone(masterTablesInfoArr[i]["masterKeys"][j]);
						whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(_getFieldSQL((XVar)(mKey), (XVar)(masterConnection), (XVar)(settings)), "=", GlobalVars.cipherer.MakeDBValue((XVar)(mKey), (XVar)(masterKeys[j]), new XVar(""), new XVar(true))), null);
					}
				}
			}
			if(XVar.Pack(!(XVar)(whereClauses)))
			{
				return null;
			}
			whereClauses.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(this.masterTable)), null);
			masterQuery = XVar.Clone(settings.getSQLQuery());
			GlobalVars.strSQL = XVar.Clone(masterQuery.buildSQL_default((XVar)(whereClauses)));
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			this.masterRecordData = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(masterConnection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
			return this.masterRecordData;
		}
		public virtual XVar getActiveMasterKeys()
		{
			dynamic i = null, ret = XVar.Array();
			i = new XVar(1);
			ret = XVar.Clone(XVar.Array());
			while(XVar.Pack(true))
			{
				if(XVar.Pack(this.masterKeysReq.KeyExists(i)))
				{
					ret.InitAndSetArrayItem(this.masterKeysReq[i], null);
				}
				else
				{
					if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i))))
					{
						ret.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i)], null);
					}
					else
					{
						break;
					}
				}
				++(i);
			}
			return ret;
		}
		public virtual XVar setProxyValue(dynamic _param_name, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(XVar.Pack(!(XVar)(name)))
			{
				return null;
			}
			this.pageData.InitAndSetArrayItem(value, "proxy", name);
			return null;
		}
		public virtual XVar getProxyValue(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			if(XVar.Pack(!(XVar)(name)))
			{
				return null;
			}
			return this.pageData["proxy"][name];
		}
		public virtual XVar setTemplateFile()
		{
			if(XVar.Pack(!(XVar)(this.templatefile)))
			{
				this.templatefile = XVar.Clone(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(this.pageType)));
			}
			this.xt.set_template((XVar)(this.templatefile));
			return null;
		}
		public virtual XVar getMenuNodes(dynamic _param_name = null)
		{
			#region default values
			if(_param_name as Object == null) _param_name = new XVar("main");
			#endregion

			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.count(this.menuNodes[name]))))
			{
				GlobalVars.menuNodesObject = this;
				if(name == "main")
				{
					CommonFunctions.getMenuNodes_main((XVar)(GlobalVars.menuNodesObject));
					return this.menuNodes[name];
				}
				if(name == "welcome_page")
				{
					CommonFunctions.getMenuNodes_welcome_page((XVar)(GlobalVars.menuNodesObject));
					return this.menuNodes[name];
				}
			}
			return this.menuNodes[name];
		}
		public virtual XVar menuAppearInLayout()
		{
			dynamic menuBricks = XVar.Array();
			if(XVar.Pack(!(XVar)(this.pageLayout)))
			{
				return false;
			}
			menuBricks = XVar.Clone(new XVar(0, "vmenu", 1, "vmenu_mobile", 2, "hmenu", 3, "bsmenu", 4, "quickjump"));
			foreach (KeyValuePair<XVar, dynamic> b in menuBricks.GetEnumerator())
			{
				if(XVar.Pack(this.pageLayout.isBrickSet((XVar)(b.Value))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar isShowMenu()
		{
			dynamic allowedMenuItems = null;
			if((XVar)((XVar)((XVar)((XVar)(!(XVar)(menuAppearInLayout()))  && (XVar)(this.pageType != Constants.PAGE_MENU))  && (XVar)(this.pageType != Constants.PAGE_ADD))  && (XVar)(this.pageType != Constants.PAGE_VIEW))  && (XVar)(this.pageType != Constants.PAGE_EDIT))
			{
				return false;
			}
			allowedMenuItems = XVar.Clone(getAllowedMenuItems());
			if(1 < allowedMenuItems)
			{
				return true;
			}
			foreach (KeyValuePair<XVar, dynamic> _menuSelector in GlobalVars.menuAssignments.GetEnumerator())
			{
				dynamic menuName = null, templateName = null;
				menuName = XVar.Clone(_menuSelector.Value["name"]);
				if((XVar)(_menuSelector.Value["page"] != templateName)  && (XVar)(menuName == "main"))
				{
					continue;
				}
				allowedMenuItems = XVar.Clone(getAllowedMenuItems((XVar)(menuName)));
				if(XVar.Pack(0) < allowedMenuItems)
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getAllowedMenuItems(dynamic _param_menuName = null)
		{
			#region default values
			if(_param_menuName as Object == null) _param_menuName = new XVar("main");
			#endregion

			#region pass-by-value parameters
			dynamic menuName = XVar.Clone(_param_menuName);
			#endregion

			dynamic allowedMenuItems = null, i = null, menuNodes = XVar.Array();
			menuNodes = XVar.Clone(getMenuNodes((XVar)(menuName)));
			allowedMenuItems = new XVar(0);
			i = new XVar(0);
			for(;i < MVCFunctions.count(menuNodes); i++)
			{
				if(menuNodes[i]["linkType"] == "Internal")
				{
					if(XVar.Pack(isUserHaveTablePerm((XVar)(menuNodes[i]["table"]), (XVar)(menuNodes[i]["pageType"]))))
					{
						allowedMenuItems++;
					}
				}
				else
				{
					if((XVar)(menuNodes[i]["linkType"] != "None")  || (XVar)(menuNodes[i]["type"] != "Group"))
					{
						allowedMenuItems++;
					}
				}
			}
			if((XVar)((XVar)(this.isDynamicPerm)  && (XVar)(CommonFunctions.IsAdmin()))  && (XVar)(this.pageType == Constants.PAGE_MENU))
			{
				allowedMenuItems++;
			}
			if(XVar.Pack(this.isAddWebRep))
			{
				allowedMenuItems++;
			}
			return allowedMenuItems;
		}
		public virtual XVar isUserHaveTablePerm(dynamic _param_tName, dynamic _param_pageType)
		{
			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			dynamic strPerm = null, var_type = null;
			if(pageType == "WebReports")
			{
				return true;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(tName)))))
			{
				return false;
			}
			var_type = XVar.Clone(getPermisType((XVar)(pageType)));
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(tName)));
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(var_type)))))
			{
				return false;
			}
			if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), (XVar)(var_type))), XVar.Pack(false)))
			{
				return true;
			}
			return false;
		}
		public virtual XVar getPermisType(dynamic _param_pageType)
		{
			#region pass-by-value parameters
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			dynamic var_type = null;
			var_type = new XVar("");
			if((XVar)((XVar)((XVar)((XVar)((XVar)(pageType == "List")  || (XVar)(pageType == "View"))  || (XVar)(pageType == "Search"))  || (XVar)(pageType == "Report"))  || (XVar)(pageType == "Chart"))  || (XVar)(pageType == "Dashboard"))
			{
				var_type = new XVar("S");
			}
			else
			{
				if(pageType == "Add")
				{
					var_type = new XVar("A");
				}
				else
				{
					if(pageType == "Edit")
					{
						var_type = new XVar("E");
					}
					else
					{
						if((XVar)(pageType == "Print")  || (XVar)(pageType == "Export"))
						{
							var_type = new XVar("P");
						}
						else
						{
							if(pageType == "Import")
							{
								var_type = new XVar("I");
							}
						}
					}
				}
			}
			return var_type;
		}
		public virtual XVar getRedirectForMenuPage()
		{
			dynamic i = null, menuNodes = XVar.Array(), redirect = null;
			if(XVar.Pack(isShowMenu()))
			{
				return "";
			}
			redirect = new XVar("");
			menuNodes = XVar.Clone(getMenuNodes());
			i = new XVar(0);
			for(;i < MVCFunctions.count(menuNodes); i++)
			{
				if(menuNodes[i]["linkType"] == "Internal")
				{
					if(XVar.Pack(isUserHaveTablePerm((XVar)(menuNodes[i]["table"]), (XVar)(menuNodes[i]["pageType"]))))
					{
						dynamic var_type = null;
						var_type = XVar.Clone(getPermisType((XVar)(menuNodes[i]["pageType"])));
						if(var_type == "A")
						{
							redirect = new XVar("add");
						}
						if(var_type == "E")
						{
							redirect = new XVar("edit");
						}
						else
						{
							if((XVar)(menuNodes[i]["pageType"] == "List")  && (XVar)(var_type == "S"))
							{
								redirect = new XVar("list");
							}
							else
							{
								if((XVar)(menuNodes[i]["pageType"] == "Report")  && (XVar)(var_type == "S"))
								{
									redirect = new XVar("report");
								}
								else
								{
									if((XVar)(menuNodes[i]["pageType"] == "Chart")  && (XVar)(var_type == "S"))
									{
										redirect = new XVar("chart");
									}
									else
									{
										if((XVar)(menuNodes[i]["pageType"] == "View")  && (XVar)(var_type == "S"))
										{
											redirect = new XVar("view");
										}
										else
										{
											if((XVar)(menuNodes[i]["pageType"] == "Dashboard")  && (XVar)(var_type == "S"))
											{
												redirect = new XVar("dashboard");
											}
										}
									}
								}
							}
						}
						redirect = XVar.Clone(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(menuNodes[i]["table"]))), (XVar)(redirect)));
					}
				}
			}
			if((XVar)(this.isDynamicPerm)  && (XVar)(CommonFunctions.IsAdmin()))
			{
				redirect = XVar.Clone(MVCFunctions.GetTableLink(new XVar("admin_rights"), new XVar("list")));
			}
			if(XVar.Pack(this.isAddWebRep))
			{
				redirect = XVar.Clone(MVCFunctions.GetTableLink(new XVar("webreport")));
			}
			return redirect;
		}
		public virtual XVar clearSessionKeys()
		{
			if((XVar)((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  && (XVar)(!(XVar)(MVCFunctions.POSTSize())))  && (XVar)((XVar)((XVar)(!(XVar)(MVCFunctions.GETSize()))  || (XVar)((XVar)(MVCFunctions.GETSize() == 1)  && (XVar)(MVCFunctions.GETKeyExists("menuItemId"))))  || (XVar)((XVar)(this.masterTable)  && (XVar)(this.mode != Constants.LIST_DETAILS))))  || (XVar)((XVar)((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_CHART)  || (XVar)(this.pageType == Constants.PAGE_REPORT))  || (XVar)(this.pageType == Constants.PAGE_DASHBOARD))  && (XVar)(!(XVar)(MVCFunctions.POSTSize())))  && (XVar)(!(XVar)(MVCFunctions.GETSize()))))  || (XVar)(MVCFunctions.postvalue("editType") == Constants.ADD_ONTHEFLY))
			{
				unsetAllPageSessionKeys();
			}
			if((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.LIST_DETAILS)))  || (XVar)(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.LIST_LOOKUP)))))  || (XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_REPORT)  && (XVar)(this.mode != Constants.REPORT_SIMPLE))  || (XVar)((XVar)(this.pageType == Constants.PAGE_CHART)  && (XVar)(this.mode != Constants.CHART_SIMPLE))))
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_filters"));
			}
			return null;
		}
		protected virtual XVar unsetAllPageSessionKeys(dynamic _param_sessionPrefix = null)
		{
			#region default values
			if(_param_sessionPrefix as Object == null) _param_sessionPrefix = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic sessionPrefix = XVar.Clone(_param_sessionPrefix);
			#endregion

			dynamic prefixLength = null, sess_unset = XVar.Array();
			if(XVar.Pack(!(XVar)(sessionPrefix)))
			{
				sessionPrefix = XVar.Clone(this.sessionPrefix);
			}
			prefixLength = XVar.Clone(MVCFunctions.strlen((XVar)(sessionPrefix)));
			sess_unset = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in XSession.Session.GetEnumerator())
			{
				if((XVar)(MVCFunctions.substr((XVar)(value.Key), new XVar(0), (XVar)(prefixLength + 1)) == MVCFunctions.Concat(sessionPrefix, "_"))  && (XVar)(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(MVCFunctions.substr((XVar)(value.Key), (XVar)(prefixLength + 1))), new XVar("_"))), XVar.Pack(false))))
				{
					sess_unset.InitAndSetArrayItem(value.Key, null);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> key in sess_unset.GetEnumerator())
			{
				XSession.Session.Remove(key.Value);
			}
			return null;
		}
		public virtual XVar setSessionVariables()
		{
			clearSessionKeys();
			if(this.masterTable != "")
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")] = this.masterTable;
			}
			else
			{
				this.masterTable = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")]);
			}
			if((XVar)(this.needSearchClauseObj)  && (XVar)(!(XVar)(this.searchClauseObj)))
			{
				this.searchClauseObj = XVar.Clone(getSearchObject());
			}
			if((XVar)(this.searchSavingEnabled)  && (XVar)(this.searchClauseObj))
			{
				this.searchClauseObj.storeSearchParamsForLogging();
			}
			if(XVar.Pack(MVCFunctions.postvalue("pagesize")))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")] = MVCFunctions.postvalue("pagesize");
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			this.pageSize = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")]);
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("tab")))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_currentTab")] = MVCFunctions.postvalue(new XVar("tab"));
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			return null;
		}
		public virtual XVar addLookupSettings()
		{
			this.settingsMap.InitAndSetArrayItem(new XVar("default", XVar.Array(), "jsName", "parentFields"), "fieldSettings", "parentFields");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", Constants.LCT_DROPDOWN, "jsName", "lcType"), "fieldSettings", "LCType");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "lookupTable"), "fieldSettings", "LookupTable");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", 1, "jsName", "selectSize"), "fieldSettings", "SelectSize");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "Multiselect"), "fieldSettings", "Multiselect");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "linkField"), "fieldSettings", "LinkField");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", "", "jsName", "dispField"), "fieldSettings", "DisplayField");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "freeInput"), "fieldSettings", "freeInput");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", false, "jsName", "HorizontalLookup"), "fieldSettings", "HorizontalLookup");
			this.settingsMap.InitAndSetArrayItem(new XVar("default", XVar.Array(), "jsName", "autoCompleteFields"), "fieldSettings", "autoCompleteFields");
			return null;
		}
		public virtual XVar fillGlobalSettings()
		{
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "global");
			foreach (KeyValuePair<XVar, dynamic> val in this.settingsMap["globalSettings"].GetEnumerator())
			{
				this.jsSettings.InitAndSetArrayItem(val.Value, "global", val.Key);
			}
			this.jsSettings.InitAndSetArrayItem(this.flyId, "global", "idStartFrom");
			return null;
		}
		protected virtual XVar fillTableSettings(dynamic _param_table = null, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(this.tName);
				pSet = XVar.UnPackProjectSettings(this.pSet);
			}
			foreach (KeyValuePair<XVar, dynamic> val in this.settingsMap["tableSettings"].GetEnumerator())
			{
				dynamic isDefault = null, tData = null;
				tData = XVar.Clone(pSet.getTableData((XVar)(MVCFunctions.Concat(".", val.Key))));
				isDefault = new XVar(false);
				if(XVar.Pack(MVCFunctions.is_array((XVar)(tData))))
				{
					isDefault = XVar.Clone(!(XVar)(MVCFunctions.count(tData)));
				}
				else
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(val.Value["default"])))))
					{
						isDefault = XVar.Clone(tData == val.Value["default"]);
					}
				}
				if(XVar.Pack(!(XVar)(isDefault)))
				{
					this.jsSettings.InitAndSetArrayItem(tData, "tableSettings", table, val.Value["jsName"]);
				}
			}
			this.jsSettings.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(table)), "global", "shortTNames", table);
			return null;
		}
		public virtual XVar addFieldsSettings(dynamic _param_arrFields, dynamic _param_pSet_packed, dynamic _param_pageBased, dynamic _param_pageType)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic arrFields = XVar.Clone(_param_arrFields);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic pageBased = XVar.Clone(_param_pageBased);
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> fName in arrFields.GetEnumerator())
			{
				dynamic lookupTableName = null, matchDK = null;
				if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"].KeyExists(fName.Value))))
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", fName.Value);
				}
				if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"][fName.Value].KeyExists(pageType))))
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", fName.Value, pageType);
				}
				matchDK = XVar.Clone((XVar)((XVar)((XVar)(matchWithDetailKeys((XVar)(fName.Value)))  && (XVar)(this.pageType != Constants.PAGE_SEARCH))  && (XVar)(this.pageType != Constants.PAGE_LIST))  && (XVar)(pageBased));
				foreach (KeyValuePair<XVar, dynamic> val in this.settingsMap["fieldSettings"].GetEnumerator())
				{
					dynamic fData = null, isDefault = null;
					fData = XVar.Clone(pSet.getFieldData((XVar)(fName.Value), (XVar)(val.Key)));
					if((XVar)(val.Key == "DateEditType")  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
					{
						if((XVar)((XVar)(pageType == Constants.PAGE_SEARCH)  && (XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_LIST)  || (XVar)(this.pageType == Constants.PAGE_CHART))  || (XVar)(this.pageType == Constants.PAGE_REPORT)))  || (XVar)((XVar)(this.pageType == Constants.PAGE_SEARCH)  && (XVar)(this.mode == Constants.SEARCH_LOAD_CONTROL)))
						{
							if(fData == Constants.EDIT_DATE_DD)
							{
								fData = new XVar(Constants.EDIT_DATE_SIMPLE);
							}
							else
							{
								if(fData == Constants.EDIT_DATE_DD_DP)
								{
									fData = new XVar(Constants.EDIT_DATE_SIMPLE_DP);
								}
								else
								{
									if(fData == Constants.EDIT_DATE_DD_INLINE)
									{
										fData = new XVar(Constants.EDIT_DATE_SIMPLE_INLINE);
									}
								}
							}
						}
					}
					if((XVar)(val.Key == "validateAs")  && (XVar)(!(XVar)(matchDK)))
					{
						if((XVar)((XVar)(pageType == Constants.PAGE_ADD)  || (XVar)(pageType == Constants.PAGE_EDIT))  || (XVar)(pageType == Constants.PAGE_REGISTER))
						{
							fillValidation((XVar)(fData), (XVar)(val.Value), (XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"][fName.Value][pageType]));
						}
						continue;
					}
					if(val.Key == "EditFormat")
					{
						if(XVar.Pack(matchDK))
						{
							fData = new XVar(Constants.EDIT_FORMAT_READONLY);
						}
					}
					else
					{
						if(val.Key == "RTEType")
						{
							fData = XVar.Clone(pSet.getRTEType((XVar)(fName.Value)));
							if(fData == "RTECK")
							{
								this.isUseCK = new XVar(true);
								this.jsSettings.InitAndSetArrayItem(pSet.getNCols((XVar)(fName.Value)), "tableSettings", this.tName, "fieldSettings", fName.Value, pageType, "nWidth");
								this.jsSettings.InitAndSetArrayItem(pSet.getNRows((XVar)(fName.Value)), "tableSettings", this.tName, "fieldSettings", fName.Value, pageType, "nHeight");
							}
						}
						else
						{
							if(val.Key == "autoCompleteFields")
							{
								fData = XVar.Clone(pSet.getAutoCompleteFields((XVar)(fName.Value)));
							}
							else
							{
								if(val.Key == "parentFields")
								{
									fData = XVar.Clone(pSet.getLookupParentFNames((XVar)(fName.Value)));
								}
							}
						}
					}
					isDefault = new XVar(false);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(fData))))
					{
						isDefault = XVar.Clone(!(XVar)(MVCFunctions.count(fData)));
					}
					else
					{
						if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(val.Value["default"])))))
						{
							isDefault = XVar.Clone(XVar.Equals(XVar.Pack(fData), XVar.Pack(val.Value["default"])));
						}
					}
					if((XVar)(!(XVar)(isDefault))  && (XVar)(!(XVar)(matchDK)))
					{
						this.jsSettings.InitAndSetArrayItem(fData, "tableSettings", this.tName, "fieldSettings", fName.Value, pageType, val.Value["jsName"]);
					}
					else
					{
						if((XVar)(matchDK)  && (XVar)((XVar)((XVar)((XVar)(val.Key == "EditFormat")  || (XVar)(val.Key == "strName"))  || (XVar)(val.Key == "autoCompleteFields"))  || (XVar)(val.Key == "LinkField")))
						{
							this.jsSettings.InitAndSetArrayItem(fData, "tableSettings", this.tName, "fieldSettings", fName.Value, pageType, val.Value["jsName"]);
						}
					}
				}
				this.jsSettings.InitAndSetArrayItem(this.isUseCK, "tableSettings", this.tName, "isUseCK");
				if((XVar)(MVCFunctions.count(this.googleMapCfg) != 0)  && (XVar)(this.googleMapCfg["isUseGoogleMap"]))
				{
					this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isUseGoogleMap");
					this.jsSettings.InitAndSetArrayItem(this.googleMapCfg, "tableSettings", this.tName, "googleMapCfg");
				}
				lookupTableName = XVar.Clone(pSet.getLookupTable((XVar)(fName.Value)));
				if(XVar.Pack(lookupTableName))
				{
					this.jsSettings.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(lookupTableName)), "global", "shortTNames", lookupTableName);
				}
				if(pSet.getEditFormat((XVar)(fName.Value)) == "Time")
				{
					fillTimePickSettings((XVar)(fName.Value), new XVar(""), (XVar)(pSet), (XVar)(pageType));
				}
			}
			return null;
		}
		public virtual XVar fillFieldSettings()
		{
			dynamic arrFields = null;
			arrFields = XVar.Clone(this.pSet.getFieldsList());
			addFieldsSettings((XVar)(arrFields), (XVar)(this.pSet), new XVar(true), (XVar)(this.pageType));
			addExtraFieldsToFieldSettings();
			if((XVar)(this.searchPanelActivated)  && (XVar)(this.permis[this.searchTableName]["search"]))
			{
				arrFields = XVar.Clone(this.pSetSearch.getAllSearchFields());
				addFieldsSettings((XVar)(arrFields), (XVar)(this.pSetSearch), new XVar(true), new XVar(Constants.PAGE_SEARCH));
			}
			return null;
		}
		public virtual XVar matchWithDetailKeys(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic match = null;
			match = new XVar(false);
			if(XVar.Pack(this.detailKeysByM))
			{
				dynamic j = null;
				j = new XVar(0);
				for(;j < MVCFunctions.count(this.detailKeysByM); j++)
				{
					if(this.detailKeysByM[j] == fName)
					{
						match = new XVar(true);
						break;
					}
				}
			}
			return match;
		}
		public virtual XVar fillPreload(dynamic _param_fName, dynamic _param_pageFields, dynamic _param_values, dynamic _param_controls = null)
		{
			#region default values
			if(_param_controls as Object == null) _param_controls = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageFields = XVar.Clone(_param_pageFields);
			dynamic values = XVar.Clone(_param_values);
			dynamic controls = XVar.Clone(_param_controls);
			#endregion

			dynamic vals = null;
			if((XVar)(matchWithDetailKeys((XVar)(fName)))  || (XVar)(!(XVar)(this.pSet.useCategory((XVar)(fName)))))
			{
				return false;
			}
			vals = XVar.Clone(getRawPreloadData((XVar)(fName), (XVar)(values), (XVar)(pageFields)));
			if((XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  || (XVar)(this.pageType == Constants.PAGE_EDIT))  || (XVar)(this.pageType == Constants.PAGE_REGISTER))
			{
				return getPreloadArr((XVar)(fName), (XVar)(vals));
			}
			return getSearchPreloadArr((XVar)(fName), (XVar)(vals), (XVar)(controls));
		}
		protected virtual XVar getRawPreloadData(dynamic _param_fName, dynamic _param_values, dynamic _param_pageFields)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic values = XVar.Clone(_param_values);
			dynamic pageFields = XVar.Clone(_param_pageFields);
			#endregion

			dynamic vals = XVar.Array();
			vals = XVar.Clone(XVar.Array());
			vals.InitAndSetArrayItem(values[fName], fName);
			if((XVar)((XVar)(this.pageType != Constants.PAGE_ADD)  && (XVar)(this.pageType != Constants.PAGE_EDIT))  && (XVar)(this.pageType != Constants.PAGE_REGISTER))
			{
				return vals;
			}
			foreach (KeyValuePair<XVar, dynamic> parentFName in getLookupParentFieldsNames((XVar)(fName)).GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.in_array((XVar)(parentFName.Value), (XVar)(pageFields))))
				{
					vals.InitAndSetArrayItem(values[parentFName.Value], parentFName.Value);
				}
			}
			return vals;
		}
		public virtual XVar getLookupParentFieldsNames(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if((XVar)((XVar)(this.pSet.getEditFormat((XVar)(fName)) != Constants.EDIT_FORMAT_LOOKUP_WIZARD)  || (XVar)(this.pSet.getEditFormat((XVar)(fName)) != Constants.EDIT_FORMAT_RADIO))  && (XVar)(!(XVar)(this.pSet.useCategory((XVar)(fName)))))
			{
				return XVar.Array();
			}
			return this.pSet.getLookupParentFNames((XVar)(fName));
		}
		public static XVar sqlFormattedDisplayField(dynamic _param_field, dynamic _param_connection, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic connection = XVar.Clone(_param_connection);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic displayField = null, lookupPSet = null, lookupType = null;
			displayField = XVar.Clone(pSet.getDisplayField((XVar)(field)));
			lookupType = XVar.Clone(pSet.getLookupType((XVar)(field)));
			if((XVar)(0 == MVCFunctions.strlen((XVar)(displayField)))  || (XVar)(pSet.getCustomDisplay((XVar)(field))))
			{
				return displayField;
			}
			if(lookupType != Constants.LT_QUERY)
			{
				return connection.addFieldWrappers((XVar)(displayField));
			}
			lookupPSet = XVar.Clone(new ProjectSettings((XVar)(pSet.getLookupTable((XVar)(field)))));
			return _getFieldSQL((XVar)(displayField), (XVar)(connection), (XVar)(lookupPSet));
		}
		public static XVar _getFieldSQL(dynamic _param_field, dynamic _param_connection, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic connection = XVar.Clone(_param_connection);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic fname = null;
			fname = new XVar("");
			if(XVar.Pack(pSet))
			{
				fname = XVar.Clone(DB.PrepareSQL((XVar)(pSet.getFullFieldName((XVar)(field)))));
			}
			if(XVar.Pack(!(XVar)(connection)))
			{
				connection = XVar.Clone(GlobalVars.cman.getDefault());
			}
			if(fname == XVar.Pack(""))
			{
				return connection.addFieldWrappers((XVar)(field));
			}
			if(XVar.Pack(!(XVar)(pSet.isSQLExpression((XVar)(field)))))
			{
				return MVCFunctions.Concat(connection.addTableWrappers((XVar)(pSet.getStrOriginalTableName())), ".", connection.addFieldWrappers((XVar)(fname)));
			}
			return fname;
		}
		public static XVar _getFieldSQLDecrypt(dynamic _param_field, dynamic _param_connection, dynamic _param_pSet_packed, dynamic _param_cipherer)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic connection = XVar.Clone(_param_connection);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			dynamic fname = null;
			fname = XVar.Clone(_getFieldSQL((XVar)(field), (XVar)(connection), (XVar)(pSet)));
			if((XVar)(cipherer)  && (XVar)(pSet))
			{
				if((XVar)(pSet.hasEncryptedFields())  && (XVar)(!(XVar)(cipherer.isEncryptionByPHPEnabled())))
				{
					return cipherer.GetFieldName((XVar)(fname), (XVar)(field));
				}
			}
			return fname;
		}
		public virtual XVar getFieldSQLDecrypt(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return _getFieldSQLDecrypt((XVar)(field), (XVar)(this.connection), (XVar)(this.pSet), (XVar)(this.cipherer));
		}
		public virtual XVar getFieldSQL(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return _getFieldSQL((XVar)(field), (XVar)(this.connection), (XVar)(this.pSet));
		}
		public virtual XVar getTableField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic strField = null;
			strField = XVar.Clone(this.pSet.getStrField((XVar)(field)));
			if(strField != XVar.Pack(""))
			{
				return this.connection.addFieldWrappers((XVar)(strField));
			}
			return getFieldSQL((XVar)(field));
		}
		public virtual XVar getPreloadArr(dynamic _param_fName, dynamic _param_vals)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic vals = XVar.Clone(_param_vals);
			#endregion

			dynamic categoryFieldAppear = null, fVal = null, output = null, parentFNames = XVar.Array();
			if((XVar)((XVar)(this.pageType != Constants.PAGE_ADD)  && (XVar)(this.pageType != Constants.PAGE_EDIT))  && (XVar)(this.pageType != Constants.PAGE_REGISTER))
			{
				return false;
			}
			parentFNames = XVar.Clone(getLookupParentFieldsNames((XVar)(fName)));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(parentFNames))))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(checkFieldOnPage((XVar)(fName)))))
			{
				return false;
			}
			categoryFieldAppear = new XVar(true);
			if(this.pageType == Constants.PAGE_ADD)
			{
				foreach (KeyValuePair<XVar, dynamic> pFName in parentFNames.GetEnumerator())
				{
					categoryFieldAppear = XVar.Clone(checkFieldOnPage((XVar)(pFName.Value)));
					if(XVar.Pack(categoryFieldAppear))
					{
						break;
					}
				}
			}
			output = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(this.pSet.isFreeInput((XVar)(fName)))))
			{
				dynamic parentFiltersData = XVar.Array();
				parentFiltersData = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> pFName in parentFNames.GetEnumerator())
				{
					parentFiltersData.InitAndSetArrayItem(vals[pFName.Value], pFName.Value);
				}
				output = XVar.Clone(getControl((XVar)(fName)).loadLookupContent((XVar)(parentFiltersData), (XVar)(vals[fName]), (XVar)(categoryFieldAppear)));
			}
			else
			{
				if(XVar.Pack(vals.KeyExists(fName)))
				{
					output = XVar.Clone(new XVar(0, vals[fName], 1, vals[fName]));
				}
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(output))))
			{
				return false;
			}
			fVal = new XVar("");
			if(XVar.Pack(MVCFunctions.strlen((XVar)(vals[fName]))))
			{
				fVal = XVar.Clone(vals[fName]);
			}
			if((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(this.pSet.multiSelect((XVar)(fName))))
			{
				fVal = XVar.Clone(CommonFunctions.splitvalues((XVar)(fVal)));
			}
			return new XVar("vals", output, "fVal", fVal);
		}
		protected virtual XVar checkFieldOnPage(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			return true;
		}
		public virtual XVar headerCommonAssign()
		{
			this.xt.assign(new XVar("logo_block"), new XVar(true));
			this.xt.assign(new XVar("collapse_block"), new XVar(true));
			assignAdmin();
			this.xt.assign(new XVar("changepwd_link"), (XVar)((XVar)(XSession.Session["AccessLevel"] != Constants.ACCESS_LEVEL_GUEST)  && (XVar)(XSession.Session["fromFacebook"] == false)));
			this.xt.assign(new XVar("changepwdlink_attrs"), (XVar)(MVCFunctions.Concat("href=\"", MVCFunctions.GetTableLink(new XVar("changepwd")), "\" onclick=\"window.location.href='", MVCFunctions.GetTableLink(new XVar("changepwd")), "';return false;\"")));
			return null;
		}
		public virtual XVar commonAssign()
		{
			headerCommonAssign();
			this.xt.assign(new XVar("quickjump_attrs"), (XVar)(MVCFunctions.Concat("class=\"", makeClassName(new XVar("quickjump")), "\"")));
			if(this.pdfMode == "")
			{
				this.xt.assign(new XVar("defaultCSS"), new XVar(true));
			}
			this.xt.assign(new XVar("more_list"), new XVar(true));
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				dynamic moreButtHideClass = null, multilang = null, showMoreButton = null;
				multilang = new XVar(false);
				showMoreButton = XVar.Clone((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(multilang)  || (XVar)(exportAvailable()))  || (XVar)(printAvailable()))  || (XVar)(importAvailable()))  || (XVar)(advSearchAvailable()))  || (XVar)(inlineEditAvailable()))  || (XVar)(deleteAvailable()));
				moreButtHideClass = XVar.Clone((XVar.Pack(showMoreButton) ? XVar.Pack("") : XVar.Pack("hideMoreButton")));
				this.xt.assign(new XVar("moreButtHideClass"), (XVar)(moreButtHideClass));
			}
			this.xt.displayBrickHidden(new XVar("searchpanel"));
			if(XVar.Pack(mobileTemplateMode()))
			{
				if(this.pageType != "menu")
				{
					this.xt.displayBrickHidden(new XVar("vmenu"));
				}
				this.xt.displayBrickHidden(new XVar("backbutton"));
				this.xt.displayBrickHidden(new XVar("fulltext_mobile"));
				this.xt.displayBrickHidden(new XVar("searchpanel_mobile"));
				this.xt.displayBrickHidden(new XVar("vmsearch2"));
				this.xt.displayBrickHidden(new XVar("adv_search_button"));
			}
			return null;
		}
		public virtual XVar getSearchPreloadArr(dynamic _param_fName, dynamic _param_vals, dynamic _param_controls)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic vals = XVar.Clone(_param_vals);
			dynamic controls = XVar.Clone(_param_controls);
			#endregion

			dynamic fVal = null, output = null, parentsFieldsData = XVar.Array(), searchApplied = null;
			if((XVar)((XVar)(controls == null)  || (XVar)(this.pSet.getEditFormat((XVar)(fName)) != Constants.EDIT_FORMAT_LOOKUP_WIZARD))  || (XVar)(!(XVar)(this.pSet.useCategory((XVar)(fName)))))
			{
				return false;
			}
			parentsFieldsData = XVar.Clone(XVar.Array());
			searchApplied = XVar.Clone(this.searchClauseObj.isUsedSrch());
			foreach (KeyValuePair<XVar, dynamic> cData in this.pSet.getParentFieldsData((XVar)(fName)).GetEnumerator())
			{
				if(XVar.Pack(searchApplied))
				{
					dynamic categoryFieldParams = XVar.Array();
					categoryFieldParams = XVar.Clone(this.searchClauseObj.getSearchCtrlParams((XVar)(cData.Value["main"])));
					if(XVar.Pack(MVCFunctions.count(categoryFieldParams)))
					{
						parentsFieldsData.InitAndSetArrayItem(categoryFieldParams[0]["value1"], cData.Value["main"]);
					}
				}
				else
				{
					dynamic defaultValue = null;
					defaultValue = XVar.Clone(MVCFunctions.GetDefaultValue((XVar)(cData.Value["main"]), new XVar(Constants.PAGE_SEARCH)));
					if(XVar.Pack(MVCFunctions.strlen((XVar)(defaultValue))))
					{
						parentsFieldsData.InitAndSetArrayItem(defaultValue, cData.Value["main"]);
					}
				}
			}
			output = XVar.Clone(controls.getControl((XVar)(fName)).loadLookupContent((XVar)(parentsFieldsData), (XVar)(vals[fName]), (XVar)(0 < MVCFunctions.count(parentsFieldsData))));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(output))))
			{
				return false;
			}
			fVal = XVar.Clone(vals[fName]);
			if(XVar.Pack(this.pSet.multiSelect((XVar)(fName))))
			{
				fVal = XVar.Clone(CommonFunctions.splitvalues((XVar)(fVal)));
			}
			return new XVar("vals", output, "fVal", fVal);
		}
		public virtual XVar addExtraFieldsToFieldSettings(dynamic _param_isCaptcha = null)
		{
			#region default values
			if(_param_isCaptcha as Object == null) _param_isCaptcha = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic isCaptcha = XVar.Clone(_param_isCaptcha);
			#endregion

			dynamic extraParams = XVar.Array();
			extraParams = XVar.Clone(new XVar("fields", XVar.Array()));
			if(XVar.Pack(isCaptcha))
			{
				extraParams.InitAndSetArrayItem(new XVar(0, getCaptchaFieldName()), "fields");
				extraParams.InitAndSetArrayItem("Text Field", "format");
			}
			else
			{
				if(this.pageType == Constants.PAGE_REGISTER)
				{
					extraParams.InitAndSetArrayItem(new XVar(0, "confirm"), "fields");
					extraParams.InitAndSetArrayItem("Password", "format");
				}
				else
				{
					if(this.pageType == Constants.PAGE_CHANGEPASS)
					{
						extraParams.InitAndSetArrayItem(new XVar(0, "oldpass", 1, "newpass", 2, "confirm"), "fields");
						extraParams.InitAndSetArrayItem("Password", "format");
					}
					else
					{
						if((XVar)(CommonFunctions.GetGlobalData(new XVar("nLoginMethod"), new XVar(0)) == Constants.SECURITY_AD)  && (XVar)(this.mode == Constants.MEMBERS_PAGE))
						{
							extraParams.InitAndSetArrayItem(new XVar(0, "displayname", 1, "name", 2, "category"), "fields");
							extraParams.InitAndSetArrayItem("Text Field", "format");
						}
					}
				}
			}
			foreach (KeyValuePair<XVar, dynamic> fName in extraParams["fields"].GetEnumerator())
			{
				dynamic arrSetVals = XVar.Array();
				arrSetVals = XVar.Clone(XVar.Array());
				arrSetVals.InitAndSetArrayItem(fName.Value, "strName");
				arrSetVals.InitAndSetArrayItem(extraParams["format"], "EditFormat");
				arrSetVals.InitAndSetArrayItem("IsRequired", "validation", "validationArr", null);
				this.jsSettings.InitAndSetArrayItem(arrSetVals, "tableSettings", this.tName, "fieldSettings", fName.Value, this.pageType);
			}
			return null;
		}
		public virtual XVar fillValidation(dynamic _param_fData, dynamic _param_val, dynamic arrSetVals)
		{
			#region pass-by-value parameters
			dynamic fData = XVar.Clone(_param_fData);
			dynamic val = XVar.Clone(_param_val);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.count(fData))))
			{
				return null;
			}
			if(XVar.Pack(MVCFunctions.count(fData["basicValidate"])))
			{
				arrSetVals.InitAndSetArrayItem(fData["basicValidate"], val["jsName"], "validationArr");
			}
			if(XVar.Pack(fData.KeyExists("regExp")))
			{
				arrSetVals.InitAndSetArrayItem(fData["regExp"], val["jsName"], "regExp");
			}
			if((XVar)(fData.KeyExists("customMessages"))  && (XVar)(MVCFunctions.count(fData["customMessages"])))
			{
				arrSetVals.InitAndSetArrayItem(fData["customMessages"], val["jsName"], "customMessages");
			}
			if(XVar.Pack(MVCFunctions.in_array(new XVar("IsTime"), (XVar)(fData["basicValidate"]))))
			{
				if(XVar.Pack(!(XVar)(this.timeRegexp)))
				{
					this.timeRegexp = XVar.Clone(getTimeRegexp());
				}
				arrSetVals.InitAndSetArrayItem(this.timeRegexp, val["jsName"], "regExp");
			}
			return null;
		}
		public virtual XVar getTimeRegexp()
		{
			dynamic designators = null, is24hoursFormat = null, leadingZero = null, timeDelimiter = null, timeFormat = null, timeSep = null;
			timeDelimiter = XVar.Clone(GlobalVars.locale_info["LOCALE_STIME"]);
			timeFormat = XVar.Clone(GlobalVars.locale_info["LOCALE_STIMEFORMAT"]);
			is24hoursFormat = XVar.Clone(GlobalVars.locale_info["LOCALE_ITIME"] == "1");
			leadingZero = XVar.Clone(GlobalVars.locale_info["LOCALE_ITLZERO"] == "1");
			if(GlobalVars.locale_info["LOCALE_ITIME"] == "0")
			{
				designators = XVar.Clone(MVCFunctions.Concat(MVCFunctions.preg_quote((XVar)(GlobalVars.locale_info["LOCALE_S1159"]), new XVar("")), "|", MVCFunctions.preg_quote((XVar)(GlobalVars.locale_info["LOCALE_S2359"]), new XVar(""))));
			}
			if(XVar.Pack(is24hoursFormat))
			{
				if(XVar.Pack(leadingZero))
				{
					timeFormat = XVar.Clone(MVCFunctions.str_replace(new XVar("HH"), new XVar("(?:0[0-9]|1[0-9]|2[0-3])"), (XVar)(timeFormat)));
				}
				else
				{
					timeFormat = XVar.Clone(MVCFunctions.str_replace(new XVar("H"), new XVar("(?:[1-9]|1[0-9]|2[0-3])"), (XVar)(timeFormat)));
				}
			}
			else
			{
				if(XVar.Pack(leadingZero))
				{
					timeFormat = XVar.Clone(MVCFunctions.str_replace(new XVar("hh"), new XVar("(?:0[1-9]|1[0-2])"), (XVar)(timeFormat)));
				}
				else
				{
					timeFormat = XVar.Clone(MVCFunctions.str_replace(new XVar("h"), new XVar("(?:[1-9]|1[0-2])"), (XVar)(timeFormat)));
				}
				timeFormat = XVar.Clone(MVCFunctions.str_replace(new XVar("tt"), (XVar)(MVCFunctions.Concat("[\\s]{0,2}(?:", designators, "|am|pm)[\\s]{0,2}")), (XVar)(timeFormat)));
			}
			timeSep = XVar.Clone((XVar.Pack(timeDelimiter == ":") ? XVar.Pack(":") : XVar.Pack(MVCFunctions.Concat("(?:", timeDelimiter, "|:)"))));
			timeFormat = XVar.Clone(MVCFunctions.str_replace((XVar)(MVCFunctions.Concat(timeDelimiter, "mm", timeDelimiter, "ss")), (XVar)(MVCFunctions.Concat("(?:", timeSep, "[0-5][0-9](?:", timeSep, "[0-5][0-9])?)?")), (XVar)(timeFormat)));
			timeFormat = XVar.Clone(MVCFunctions.Concat("^", MVCFunctions.str_replace(new XVar(" "), new XVar("[\\s]{0,2}"), (XVar)(timeFormat)), "$"));
			return timeFormat;
		}
		public virtual XVar fillSettings()
		{
			fillGlobalSettings();
			fillTableSettings();
			fillFieldSettings();
			return null;
		}
		public virtual XVar fillFieldToolTips(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic toolTipText = null;
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				return null;
			}
			toolTipText = XVar.Clone(CommonFunctions.GetFieldToolTip((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(fName)))));
			if(XVar.Pack(MVCFunctions.strlen((XVar)(toolTipText))))
			{
				this.controlsMap.InitAndSetArrayItem(toolTipText, "toolTips", fName);
			}
			return null;
		}
		public virtual XVar fillControlsMap(dynamic _param_arr, dynamic _param_addSet = null, dynamic _param_fName = null)
		{
			#region default values
			if(_param_addSet as Object == null) _param_addSet = new XVar(false);
			if(_param_fName as Object == null) _param_fName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic arr = XVar.Clone(_param_arr);
			dynamic addSet = XVar.Clone(_param_addSet);
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if(XVar.Pack(!(XVar)(addSet)))
			{
				foreach (KeyValuePair<XVar, dynamic> val in arr.GetEnumerator())
				{
					CommonFunctions.initArray((XVar)(this.controlsMap), (XVar)(val.Key));
					this.controlsMap.InitAndSetArrayItem(val.Value, val.Key, null);
				}
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> val in arr.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> vval in val.Value.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(fName)))
					{
						this.controlsMap.InitAndSetArrayItem(vval.Value, val.Key, MVCFunctions.count(this.controlsMap[val.Key]) - 1, vval.Key);
					}
					else
					{
						dynamic i = null;
						i = new XVar(0);
						for(;i < MVCFunctions.count(this.controlsMap[val.Key]); i++)
						{
							if(this.controlsMap[val.Key][i]["fieldName"] == fName)
							{
								this.controlsMap.InitAndSetArrayItem(vval.Value, val.Key, i, vval.Key);
								break;
							}
						}
					}
				}
			}
			return null;
		}
		public virtual XVar fillControlsHTMLMap()
		{
			this.controlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName);
			this.controlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName, this.pageType);
			this.controlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName, this.pageType, this.id);
			this.controlsMap.InitAndSetArrayItem(this.googleMapCfg, "gMaps");
			if(XVar.Pack(this.searchClauseObj))
			{
				if(XVar.Pack(!(XVar)(this.controlsMap.KeyExists("search"))))
				{
					this.controlsMap.InitAndSetArrayItem(XVar.Array(), "search");
				}
				this.controlsMap.InitAndSetArrayItem(this.searchClauseObj.isUsedSrch(), "search", "usedSrch");
			}
			foreach (KeyValuePair<XVar, dynamic> val in this.controlsMap.GetEnumerator())
			{
				this.controlsHTMLMap.InitAndSetArrayItem(val.Value, this.tName, this.pageType, this.id, val.Key);
			}
			this.viewControlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName);
			this.viewControlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName, this.pageType);
			this.viewControlsHTMLMap.InitAndSetArrayItem(XVar.Array(), this.tName, this.pageType, this.id);
			foreach (KeyValuePair<XVar, dynamic> val in this.viewControlsMap.GetEnumerator())
			{
				this.viewControlsHTMLMap.InitAndSetArrayItem(val.Value, this.tName, this.pageType, this.id, val.Key);
			}
			return null;
		}
		public virtual XVar fillSetCntrlMaps()
		{
			if(XVar.Pack(this.isControlsMapFilled))
			{
				return null;
			}
			fillSettings();
			fillControlsHTMLMap();
			this.isControlsMapFilled = new XVar(true);
			return null;
		}
		public virtual XVar fillCntrlTabGroups()
		{
			dynamic arrTabs = XVar.Array(), beginGroup = null, f = null, i = null, tabC = XVar.Array(), tabGroupName = null, tabN = XVar.Array();
			if(XVar.Pack(isMultistepped()))
			{
				this.controlsMap.InitAndSetArrayItem(this.initialStep, "initialStep");
				this.controlsMap.InitAndSetArrayItem(true, "multistep");
			}
			arrTabs = XVar.Clone(getArrTabs());
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "tabs");
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "sections");
			if(XVar.Pack(!(XVar)(arrTabs)))
			{
				return false;
			}
			beginGroup = new XVar(false);
			tabGroupName = new XVar("");
			i = new XVar(0);
			for(;i < MVCFunctions.count(arrTabs); i++)
			{
				tabC = XVar.Clone(arrTabs[i]);
				tabN = XVar.Clone((XVar.Pack(i + 1 < MVCFunctions.count(arrTabs)) ? XVar.Pack(arrTabs[i + 1]) : XVar.Pack(false)));
				if(tabC["nType"] == Constants.TAB_TYPE_TAB)
				{
					if(XVar.Pack(!(XVar)(beginGroup)))
					{
						beginGroup = new XVar(true);
						tabGroupName = XVar.Clone(tabC["tabId"]);
					}
					if(XVar.Pack(beginGroup))
					{
						if((XVar)((XVar)(!(XVar)(tabN))  || (XVar)(tabN["nType"]))  || (XVar)(tabN["tabGroup"] != tabC["tabGroup"]))
						{
							dynamic j = null, tabsAndFields = XVar.Array();
							tabsAndFields = XVar.Clone(XVar.Array());
							j = new XVar(0);
							for(;j < MVCFunctions.count(arrTabs); j++)
							{
								if(arrTabs[j]["tabGroup"] == tabC["tabGroup"])
								{
									tabsAndFields.InitAndSetArrayItem(XVar.Array(), arrTabs[j]["tabId"]);
									f = new XVar(0);
									for(;f < MVCFunctions.count(arrTabs[j]["arrFields"]); f++)
									{
										tabsAndFields.InitAndSetArrayItem(arrTabs[j]["arrFields"][f], arrTabs[j]["tabId"], null);
									}
								}
							}
							this.controlsMap.InitAndSetArrayItem(tabsAndFields, "tabs", MVCFunctions.Concat("tabGroup_", tabGroupName));
							beginGroup = new XVar(false);
							if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
							{
								dynamic selected = null, tabHeaders = XVar.Array();
								selected = new XVar("active");
								tabHeaders = XVar.Clone(XVar.Array());
								j = new XVar(0);
								for(;j < MVCFunctions.count(arrTabs); j++)
								{
									if(arrTabs[j]["tabGroup"] != tabC["tabGroup"])
									{
										continue;
									}
									tabHeaders.InitAndSetArrayItem(MVCFunctions.Concat("<li role=\"presentation\" class=\"", selected, "\"><a aria-controls=\"settings\" role=\"tab\" data-toggle=\"tab\" href=\"#", arrTabs[j]["tabId"], this.id, "\">", arrTabs[j]["tabName"], "</a></li>"), null);
									selected = new XVar("");
								}
								this.xt.assign((XVar)(MVCFunctions.Concat("tabgroup_", tabGroupName, "_header")), (XVar)(MVCFunctions.Concat("<ul class=\"nav nav-tabs\" role=\"tablist\">", MVCFunctions.implode(new XVar(""), (XVar)(tabHeaders)), "</ul>")));
							}
						}
					}
				}
				else
				{
					if(tabC["nType"] == Constants.TAB_TYPE_STEP)
					{
						this.controlsMap.InitAndSetArrayItem(XVar.Array(), "steps", i);
						f = new XVar(0);
						for(;f < MVCFunctions.count(arrTabs[i]["arrFields"]); f++)
						{
							this.controlsMap.InitAndSetArrayItem(arrTabs[i]["arrFields"][f], "steps", i, null);
						}
					}
					else
					{
						if(tabC["nType"] == Constants.TAB_TYPE_SECTION)
						{
							dynamic sectionId = null;
							this.controlsMap.InitAndSetArrayItem(XVar.Array(), "sections", MVCFunctions.Concat("section_", tabC["tabId"]));
							f = new XVar(0);
							for(;f < MVCFunctions.count(arrTabs[i]["arrFields"]); f++)
							{
								sectionId = XVar.Clone((XVar.Pack(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack(tabC["tabId"]) : XVar.Pack(MVCFunctions.Concat("section_", tabC["tabId"]))));
								this.controlsMap.InitAndSetArrayItem(arrTabs[i]["arrFields"][f], "sections", sectionId, null);
							}
							if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
							{
								dynamic bodyId = null, expanded = null, headId = null;
								headId = XVar.Clone(MVCFunctions.Concat("section_head_", tabC["tabId"], this.id));
								bodyId = XVar.Clone(MVCFunctions.Concat("section_body_", tabC["tabId"], this.id));
								expanded = new XVar("aria-expanded=\"false\"");
								if(XVar.Pack(tabC["expandSec"]))
								{
									expanded = new XVar("aria-expanded=\"true\"");
									this.xt.assign((XVar)(MVCFunctions.Concat("section_", tabC["tabId"], "_xtrclass")), new XVar("in "));
								}
								this.xt.assign((XVar)(MVCFunctions.Concat("section_", tabC["tabId"], "_headattrs")), (XVar)(MVCFunctions.Concat("id=\"", headId, "\" href=\"#", bodyId, "\" ", expanded, " aria-controls=\"", bodyId, "\"")));
								this.xt.assign((XVar)(MVCFunctions.Concat("section_", tabC["tabId"], "_attrs")), (XVar)(MVCFunctions.Concat("id=\"", bodyId, "\" aria-labelledby=\"", headId, "\"")));
								this.xt.assign((XVar)(MVCFunctions.Concat("section_", tabC["tabId"], "_label")), (XVar)(tabC["tabName"]));
							}
						}
					}
				}
			}
			return null;
		}
		public virtual XVar isAppearOnTabs(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic arrTabs = XVar.Array(), match = null;
			match = new XVar(false);
			arrTabs = XVar.Clone(getArrTabs());
			if(XVar.Pack(!(XVar)(arrTabs)))
			{
				return match;
			}
			foreach (KeyValuePair<XVar, dynamic> val in arrTabs.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.in_array((XVar)(fName), (XVar)(val.Value["arrFields"]))))
				{
					match = new XVar(true);
					break;
				}
			}
			return match;
		}
		public virtual XVar getArrTabs()
		{
			if(this.pageType == Constants.PAGE_EDIT)
			{
				return this.pSet.getEditTabs();
			}
			else
			{
				if(this.pageType == Constants.PAGE_ADD)
				{
					return this.pSet.getAddTabs();
				}
				else
				{
					if(this.pageType == Constants.PAGE_VIEW)
					{
						return this.pSet.getViewTabs();
					}
					else
					{
						if(this.pageType == Constants.PAGE_REGISTER)
						{
							return this.pSet.getRegisterTabs();
						}
						else
						{
							return XVar.Array();
						}
					}
				}
			}
			return null;
		}
		public virtual XVar fillTimePickSettings(dynamic _param_field, dynamic _param_value = null, dynamic _param_pSet_packed = null, dynamic _param_pageType = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_value as Object == null) _param_value = new XVar("");
			if(_param_pSet as Object == null) _param_pSet = null;
			if(_param_pageType as Object == null) _param_pageType = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			dynamic timeAttrs = XVar.Array();
			if(XVar.Pack(pSet == null))
			{
				pSet = XVar.UnPackProjectSettings(this.pSet);
			}
			if(pageType == XVar.Pack(""))
			{
				pageType = XVar.Clone(this.pageType);
			}
			timeAttrs = XVar.Clone(pSet.getFormatTimeAttrs((XVar)(field)));
			if((XVar)(MVCFunctions.count(timeAttrs))  && (XVar)(timeAttrs["useTimePicker"]))
			{
				dynamic convention = null, h = null, locAmPm = XVar.Array(), m = null, minutes = XVar.Array(), range = XVar.Array(), timePickSet = XVar.Array(), tpVal = XVar.Array();
				convention = XVar.Clone(timeAttrs["hours"]);
				locAmPm = XVar.Clone(CommonFunctions.getLacaleAmPmForTimePicker((XVar)(convention), new XVar(true)));
				tpVal = XVar.Clone(CommonFunctions.getValForTimePicker((XVar)(pSet.getFieldType((XVar)(field))), (XVar)(value), (XVar)(locAmPm["locale"])));
				range = XVar.Clone(XVar.Array());
				if(convention == 24)
				{
					h = new XVar(0);
					for(;h < convention; h++)
					{
						range.InitAndSetArrayItem(h, null);
					}
				}
				else
				{
					h = new XVar(1);
					for(;h <= convention; h++)
					{
						range.InitAndSetArrayItem(h, null);
					}
				}
				minutes = XVar.Clone(XVar.Array());
				m = new XVar(0);
				for(;m < 60; m += timeAttrs["minutes"])
				{
					minutes.InitAndSetArrayItem(m, null);
				}
				timePickSet = XVar.Clone(new XVar("convention", convention, "range", range, "apm", new XVar(0, locAmPm["am"], 1, locAmPm["pm"]), "rangeMin", minutes, "locale", locAmPm["locale"], "showSec", timeAttrs["showSeconds"], "minutes", timeAttrs["minutes"]));
				if(0 < MVCFunctions.count(tpVal["dbtime"]))
				{
					timePickSet.InitAndSetArrayItem(new XVar("0", tpVal["dbtime"][3], "1", tpVal["dbtime"][4], "2", tpVal["dbtime"][5]), "hover");
				}
				if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"].KeyExists(field))))
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", field);
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", field, pageType);
					this.jsSettings.InitAndSetArrayItem(timePickSet, "tableSettings", this.tName, "fieldSettings", field, pageType, "timePick");
				}
				else
				{
					if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"][field][pageType].KeyExists("timePick"))))
					{
						this.jsSettings.InitAndSetArrayItem(timePickSet, "tableSettings", this.tName, "fieldSettings", field, pageType, "timePick");
					}
				}
				fillControlsMap((XVar)(new XVar("controls", new XVar("open", (XVar.Pack(tpVal["val"]) ? XVar.Pack(true) : XVar.Pack(false))))), new XVar(true), (XVar)(field));
			}
			return null;
		}
		public virtual XVar assignBodyEnd(dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			fillSetCntrlMaps();
			MVCFunctions.Echo(MVCFunctions.Concat("<script>\r\n\t\t\twindow.controlsMap = ", MVCFunctions.my_json_encode((XVar)(this.controlsHTMLMap)), ";\r\n\t\t\twindow.viewControlsMap = ", MVCFunctions.my_json_encode((XVar)(this.viewControlsHTMLMap)), ";\r\n\t\t\twindow.settings = ", MVCFunctions.my_json_encode((XVar)(this.jsSettings)), ";\r\n\t\t\tRunner.applyPagesData( ", MVCFunctions.my_json_encode((XVar)(GlobalVars.pagesData)), " );\r\n\t\t\t</script>\r\n"));
			MVCFunctions.Echo(MVCFunctions.Concat("<script language=\"JavaScript\" src=\"", MVCFunctions.GetRootPathForResources(new XVar("include/runnerJS/RunnerAll.js")), "\"></script>\r\n"));
			MVCFunctions.Echo(MVCFunctions.Concat("<script>", PrepareJS(), "</script>"));
			return null;
		}
		public virtual XVar genId()
		{
			this.flyId++;
			this.recId = XVar.Clone(this.flyId);
			return this.flyId;
		}
		public virtual XVar getPageType()
		{
			return this.pageType;
		}
		public virtual XVar AddJSFileNoExt(dynamic _param_file)
		{
			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			#endregion

			this.includes_js.InitAndSetArrayItem(MVCFunctions.GetRootPathForResources((XVar)(file)), null);
			return null;
		}
		public virtual XVar AddJSFile(dynamic _param_file, dynamic _param_req1 = null, dynamic _param_req2 = null, dynamic _param_req3 = null)
		{
			#region default values
			if(_param_req1 as Object == null) _param_req1 = new XVar("");
			if(_param_req2 as Object == null) _param_req2 = new XVar("");
			if(_param_req3 as Object == null) _param_req3 = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			dynamic req1 = XVar.Clone(_param_req1);
			dynamic req2 = XVar.Clone(_param_req2);
			dynamic req3 = XVar.Clone(_param_req3);
			#endregion

			dynamic rootPath = null;
			rootPath = XVar.Clone(MVCFunctions.GetRootPathForResources((XVar)(file)));
			this.includes_js.InitAndSetArrayItem(rootPath, null);
			if(req1 != XVar.Pack(""))
			{
				this.includes_jsreq.InitAndSetArrayItem(new XVar(0, MVCFunctions.GetRootPathForResources((XVar)(req1))), rootPath);
			}
			if(req2 != XVar.Pack(""))
			{
				this.includes_jsreq.InitAndSetArrayItem(MVCFunctions.GetRootPathForResources((XVar)(req2)), rootPath, null);
			}
			if(req3 != XVar.Pack(""))
			{
				this.includes_jsreq.InitAndSetArrayItem(MVCFunctions.GetRootPathForResources((XVar)(req3)), rootPath, null);
			}
			return null;
		}
		public virtual XVar grabAllJsFiles()
		{
			dynamic jsFiles = XVar.Array();
			jsFiles = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> file in this.includes_js.GetEnumerator())
			{
				jsFiles.InitAndSetArrayItem(XVar.Array(), file.Value);
				if(XVar.Pack(this.includes_jsreq.KeyExists(file.Value)))
				{
					jsFiles.InitAndSetArrayItem(this.includes_jsreq[file.Value], file.Value);
				}
			}
			this.includes_js = XVar.Clone(XVar.Array());
			this.includes_jsreq = XVar.Clone(XVar.Array());
			return jsFiles;
		}
		public virtual XVar copyAllJsFiles(dynamic _param_jsFiles)
		{
			#region pass-by-value parameters
			dynamic jsFiles = XVar.Clone(_param_jsFiles);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> reqFiles in jsFiles.GetEnumerator())
			{
				this.includes_js.InitAndSetArrayItem(reqFiles.Key, null);
				if(XVar.Pack(this.includes_jsreq.KeyExists(reqFiles.Key)))
				{
					foreach (KeyValuePair<XVar, dynamic> rFile in reqFiles.Value.GetEnumerator())
					{
						if(XVar.Pack(this.includes_jsreq[reqFiles.Key].KeyExists(rFile.Value)))
						{
							continue;
						}
						this.includes_jsreq.InitAndSetArrayItem(rFile.Value, reqFiles.Key, null);
					}
				}
				else
				{
					this.includes_jsreq.InitAndSetArrayItem(reqFiles.Value, reqFiles.Key);
				}
			}
			return null;
		}
		public virtual XVar AddCSSFile(dynamic _param_file)
		{
			#region pass-by-value parameters
			dynamic file = XVar.Clone(_param_file);
			#endregion

			if((XVar)(this.pdfMode)  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				if(0 == MVCFunctions.count(this.includes_css))
				{
					this.includes_css.InitAndSetArrayItem("styles/pdf.css", null);
				}
				return null;
			}
			if(XVar.Pack(MVCFunctions.is_array((XVar)(file))))
			{
				foreach (KeyValuePair<XVar, dynamic> f in file.GetEnumerator())
				{
					this.includes_css.InitAndSetArrayItem(f.Value, null);
				}
			}
			else
			{
				this.includes_css.InitAndSetArrayItem(file, null);
			}
			return null;
		}
		public virtual XVar updatePageLayoutAndCSS(dynamic _param_tName, dynamic _param_suffix)
		{
			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic suffix = XVar.Clone(_param_suffix);
			#endregion

			this.pageLayout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(tName), (XVar)(this.pageType), (XVar)(suffix)));
			this.includes_css = XVar.Clone(XVar.Array());
			AddCSSFile((XVar)(this.pageLayout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(CommonFunctions.isPageLayoutMobile((XVar)(this.templatefile))), (XVar)(this.pdfMode != ""))));
			return null;
		}
		public virtual XVar grabAllCSSFiles()
		{
			dynamic cssFiles = null;
			cssFiles = XVar.Clone(this.includes_css);
			this.includes_css = XVar.Clone(XVar.Array());
			return cssFiles;
		}
		public virtual XVar copyAllCssFiles(dynamic _param_cssFiles)
		{
			#region pass-by-value parameters
			dynamic cssFiles = XVar.Clone(_param_cssFiles);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> file in cssFiles.GetEnumerator())
			{
				AddCSSFile((XVar)(file.Value));
			}
			return null;
		}
		public virtual XVar LoadJS_CSS()
		{
			dynamic var_out = null;
			this.includes_js = XVar.Clone(MVCFunctions.array_unique((XVar)(this.includes_js)));
			this.includes_css = XVar.Clone(MVCFunctions.array_unique((XVar)(this.includes_css)));
			var_out = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> file in this.includes_js.GetEnumerator())
			{
				var_out = MVCFunctions.Concat(var_out, "Runner.util.ScriptLoader.addJS(['", file.Value, "']");
				if(XVar.Pack(this.includes_jsreq.KeyExists(file.Value)))
				{
					foreach (KeyValuePair<XVar, dynamic> req in this.includes_jsreq[file.Value].GetEnumerator())
					{
						var_out = MVCFunctions.Concat(var_out, ",'", req.Value, "'");
					}
				}
				var_out = MVCFunctions.Concat(var_out, ");\r\n");
			}
			var_out = MVCFunctions.Concat(var_out, " Runner.util.ScriptLoader.load();");
			return var_out;
		}
		public virtual XVar setLangParams()
		{
			return null;
		}
		public static XVar addCommonJs(dynamic instance)
		{
			if(instance.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				instance.AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				instance.AddCSSFile(new XVar("include/bootstrap/css/jquery.mCustomScrollbar.css"));
			}
			if(XVar.Equals(XVar.Pack(instance.debugJSMode), XVar.Pack(true)))
			{
				instance.AddJSFile(new XVar("include/runnerJS/ControlConstants.js"));
				instance.AddJSFile(new XVar("include/runnerJS/RunnerEvent.js"));
				instance.AddJSFile(new XVar("include/runnerJS/Validate.js"), new XVar("include/runnerJS/RunnerEvent.js"));
				instance.AddJSFile(new XVar("include/runnerJS/ControlManager.js"), new XVar("include/runnerJS/Validate.js"));
				instance.AddJSFile(new XVar("include/runnerJS/button.js"), new XVar("include/runnerJS/ControlManager.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/Control.js"), new XVar("include/runnerJS/ControlManager.js"));
				instance.AddJSFile(new XVar("include/runnerJS/MockControl.js"), new XVar("include/runnerJS/ControlManager.js"));
				instance.AddJSFile(new XVar("include/runnerJS/viewControls/ViewControl.js"), new XVar("include/runnerJS/ControlManager.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/ReadOnly.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/TextAreaControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/TextFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/TimeFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/RteControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/FileControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/MultiUploadControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/DateFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/LookupWizard.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/RadioControl.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/DropDown.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/CheckBox.js"), new XVar("include/runnerJS/editControls/Control.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/CheckBoxLookup.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/TextFieldLookup.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/EditBoxLookup.js"), new XVar("include/runnerJS/editControls/TextFieldLookup.js"));
				instance.AddJSFile(new XVar("include/runnerJS/editControls/ListPageLookup.js"), new XVar("include/runnerJS/editControls/TextFieldLookup.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PageConstants.js"), new XVar("include/runnerJS/ListPageLookup.js"));
				instance.AddJSFile(new XVar("include/runnerJS/InlineEdit.js"), new XVar("include/runnerJS/pages/PageConstants.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/RunnerDefaults.js"), new XVar("include/runnerJS/pages/PageConstants.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PageManager.js"), new XVar("include/runnerJS/pages/RunnerDefaults.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PageSettings.js"), new XVar("include/runnerJS/pages/PageManager.js"));
				instance.AddJSFile(new XVar("include/runnerJS/DetPreview.js"), new XVar("include/runnerJS/pages/PageSettings.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/RunnerPage.js"), new XVar("include/runnerJS/pages/PageSettings.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/SearchPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ViewPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/LoginPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/RemindPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PrintPdf.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PrintPageCommon.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/PrintPage.js"), new XVar("include/runnerJS/pages/PrintPageCommon.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ReportPrintPage.js"), new XVar("include/runnerJS/pages/PrintPageCommon.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/EditorPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/AddPage.js"), new XVar("include/runnerJS/pages/EditorPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/AddPageFly.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/AddPageDash.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/EditPage.js"), new XVar("include/runnerJS/pages/EditorPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/EditPageDash.js"), new XVar("include/runnerJS/pages/EditPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/EditSelectedPage.js"), new XVar("include/runnerJS/pages/EditPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/DataPageWithSearch.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageCommon.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageFly.js"), new XVar("include/runnerJS/pages/ListPageCommon.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPage.js"), new XVar("include/runnerJS/pages/ListPageCommon.js"), new XVar("include/runnerJS/DetPreview.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageDash.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/DashboardMap.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/DashboardLeadingMap.js"), new XVar("include/runnerJS/pages/DashboardMap.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/DashboardGridBasedMap.js"), new XVar("include/runnerJS/pages/DashboardMap.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/DashboardPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				if(XVar.Pack(instance.mobileTemplateMode()))
				{
					instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageMobile.js"), new XVar("include/runnerJS/pages/ListPage.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageMobileDP.js"), new XVar("include/runnerJS/pages/ListPageDP.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ReportPageMobile.js"), new XVar("include/runnerJS/pages/ListPageMobile.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ChartPageMobile.js"), new XVar("include/runnerJS/pages/ListPageMobile.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ChartPageMobileDP.js"), new XVar("include/runnerJS/pages/ChartPageMobile.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/DashboardPageMobile.js"), new XVar("include/runnerJS/pages/DashboardPage.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ReportPageMobileDP.js"), new XVar("include/runnerJS/pages/ReportPageDP.js"));
				}
				else
				{
					instance.AddJSFile(new XVar("include/runnerJS/pages/ChartPage.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ChartPageDP.js"), new XVar("include/runnerJS/pages/ChartPage.js"));
					instance.AddJSFile(new XVar("include/runnerJS/pages/ChartPageDash.js"), new XVar("include/runnerJS/pages/ChartPage.js"));
				}
				instance.AddJSFile(new XVar("include/runnerJS/pages/ReportPageDP.js"), new XVar("include/runnerJS/pages/ReportPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ReportPage.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageAjax.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ListPageDP.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/CheckboxesPage.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/MembersPage.js"), new XVar("include/runnerJS/pages/CheckboxesPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/RightsPage.js"), new XVar("include/runnerJS/pages/CheckboxesPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ChangePwdPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ExportPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/ImportPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/pages/RegisterPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				instance.AddJSFile(new XVar("include/runnerJS/FilterControl.js"), new XVar("include/runnerJS/editControls/DateFieldControl.js"));
				instance.AddJSFile(new XVar("include/runnerJS/SearchForm.js"));
				instance.AddJSFile(new XVar("include/runnerJS/SearchField.js"));
				instance.AddJSFile(new XVar("include/runnerJS/SearchFormWithUI.js"), new XVar("include/runnerJS/SearchForm.js"));
				instance.AddJSFile(new XVar("include/runnerJS/SearchController.js"), new XVar("include/runnerJS/SearchFormWithUI.js"));
				instance.AddJSFile(new XVar("include/runnerJS/SearchParamsLogger.js"), new XVar("include/runnerJS/SearchController.js"));
				instance.AddJSFile(new XVar("include/runnerJS/RunnerForm.js"));
				instance.AddJSFile(new XVar("include/runnerJS/RunnerBricks.js"));
				instance.AddJSFile(new XVar("include/runnerJS/RunnerMenu.js"));
				if(XVar.Pack(instance.lockingObj))
				{
					instance.AddJSFile(new XVar("include/runnerJS/RunnerLocking.js"));
				}
				if(XVar.Pack(instance.is508))
				{
					instance.AddJSFile(new XVar("include/runnerJS/RunnerSection508.js"));
				}
				if((XVar)((XVar)(instance.pSet.isAddPageEvents())  && (XVar)(instance.pageType != Constants.PAGE_LOGIN))  && (XVar)(instance.shortTableName != ""))
				{
					instance.AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", instance.shortTableName, ".js")), new XVar("include/runnerJS/pages/PageSettings.js"), new XVar("include/runnerJS/button.js"));
				}
			}
			else
			{
				if((XVar)((XVar)(instance.pSet.isAddPageEvents())  && (XVar)(instance.pageType != Constants.PAGE_LOGIN))  && (XVar)(instance.shortTableName != ""))
				{
					instance.AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", instance.shortTableName, ".js")));
				}
			}
			if(instance.getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				instance.AddJSFile(new XVar("include/yui/yui-min.js"));
			}
			if(XVar.Pack(instance.isUseAjaxSuggest))
			{
				instance.AddJSFile(new XVar("include/ajaxsuggest.js"));
			}
			else
			{
				if(XVar.Pack(MVCFunctions.count(instance.allDetailsTablesArr)))
				{
					dynamic i = null;
					i = new XVar(0);
					for(;i < MVCFunctions.count(instance.allDetailsTablesArr); i++)
					{
						if(instance.allDetailsTablesArr[i]["previewOnList"] == Constants.DP_POPUP)
						{
							instance.AddJSFile(new XVar("include/ajaxsuggest.js"));
						}
						break;
					}
				}
			}
			if(XVar.Pack(instance.isUseCK))
			{
				instance.AddJSFile(new XVar("plugins/ckeditor/ckeditor.js"));
			}
			instance.addControlsJSAndCSS();
			return null;
		}
		public virtual XVar addCommonJs()
		{
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/bootstrap/css/jquery.mCustomScrollbar.css"));
			}
			if(XVar.Equals(XVar.Pack(this.debugJSMode), XVar.Pack(true)))
			{
				AddJSFile(new XVar("include/runnerJS/ControlConstants.js"));
				AddJSFile(new XVar("include/runnerJS/RunnerEvent.js"));
				AddJSFile(new XVar("include/runnerJS/Validate.js"), new XVar("include/runnerJS/RunnerEvent.js"));
				AddJSFile(new XVar("include/runnerJS/ControlManager.js"), new XVar("include/runnerJS/Validate.js"));
				AddJSFile(new XVar("include/runnerJS/button.js"), new XVar("include/runnerJS/ControlManager.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/Control.js"), new XVar("include/runnerJS/ControlManager.js"));
				AddJSFile(new XVar("include/runnerJS/MockControl.js"), new XVar("include/runnerJS/ControlManager.js"));
				AddJSFile(new XVar("include/runnerJS/viewControls/ViewControl.js"), new XVar("include/runnerJS/ControlManager.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/ReadOnly.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/TextAreaControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/TextFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/TimeFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/RteControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/FileControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/MultiUploadControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/DateFieldControl.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/LookupWizard.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/RadioControl.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/DropDown.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/CheckBox.js"), new XVar("include/runnerJS/editControls/Control.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/CheckBoxLookup.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/TextFieldLookup.js"), new XVar("include/runnerJS/editControls/LookupWizard.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/EditBoxLookup.js"), new XVar("include/runnerJS/editControls/TextFieldLookup.js"));
				AddJSFile(new XVar("include/runnerJS/editControls/ListPageLookup.js"), new XVar("include/runnerJS/editControls/TextFieldLookup.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PageConstants.js"), new XVar("include/runnerJS/ListPageLookup.js"));
				AddJSFile(new XVar("include/runnerJS/InlineEdit.js"), new XVar("include/runnerJS/pages/PageConstants.js"));
				AddJSFile(new XVar("include/runnerJS/pages/RunnerDefaults.js"), new XVar("include/runnerJS/pages/PageConstants.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PageManager.js"), new XVar("include/runnerJS/pages/RunnerDefaults.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PageSettings.js"), new XVar("include/runnerJS/pages/PageManager.js"));
				AddJSFile(new XVar("include/runnerJS/DetPreview.js"), new XVar("include/runnerJS/pages/PageSettings.js"));
				AddJSFile(new XVar("include/runnerJS/pages/RunnerPage.js"), new XVar("include/runnerJS/pages/PageSettings.js"));
				AddJSFile(new XVar("include/runnerJS/pages/SearchPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ViewPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/LoginPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/RemindPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PrintPdf.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PrintPageCommon.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/PrintPage.js"), new XVar("include/runnerJS/pages/PrintPageCommon.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ReportPrintPage.js"), new XVar("include/runnerJS/pages/PrintPageCommon.js"));
				AddJSFile(new XVar("include/runnerJS/pages/EditorPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/AddPage.js"), new XVar("include/runnerJS/pages/EditorPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/AddPageFly.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/AddPageDash.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/EditPage.js"), new XVar("include/runnerJS/pages/EditorPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/EditPageDash.js"), new XVar("include/runnerJS/pages/EditPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/EditSelectedPage.js"), new XVar("include/runnerJS/pages/EditPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/DataPageWithSearch.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPageCommon.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPageFly.js"), new XVar("include/runnerJS/pages/ListPageCommon.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPage.js"), new XVar("include/runnerJS/pages/ListPageCommon.js"), new XVar("include/runnerJS/DetPreview.js"), new XVar("include/runnerJS/pages/AddPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPageDash.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/DashboardMap.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/DashboardLeadingMap.js"), new XVar("include/runnerJS/pages/DashboardMap.js"));
				AddJSFile(new XVar("include/runnerJS/pages/DashboardGridBasedMap.js"), new XVar("include/runnerJS/pages/DashboardMap.js"));
				AddJSFile(new XVar("include/runnerJS/pages/DashboardPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				if(XVar.Pack(mobileTemplateMode()))
				{
					AddJSFile(new XVar("include/runnerJS/pages/ListPageMobile.js"), new XVar("include/runnerJS/pages/ListPage.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ListPageMobileDP.js"), new XVar("include/runnerJS/pages/ListPageDP.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ReportPageMobile.js"), new XVar("include/runnerJS/pages/ListPageMobile.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ChartPageMobile.js"), new XVar("include/runnerJS/pages/ListPageMobile.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ChartPageMobileDP.js"), new XVar("include/runnerJS/pages/ChartPageMobile.js"));
					AddJSFile(new XVar("include/runnerJS/pages/DashboardPageMobile.js"), new XVar("include/runnerJS/pages/DashboardPage.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ReportPageMobileDP.js"), new XVar("include/runnerJS/pages/ReportPageDP.js"));
				}
				else
				{
					AddJSFile(new XVar("include/runnerJS/pages/ChartPage.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ChartPageDP.js"), new XVar("include/runnerJS/pages/ChartPage.js"));
					AddJSFile(new XVar("include/runnerJS/pages/ChartPageDash.js"), new XVar("include/runnerJS/pages/ChartPage.js"));
				}
				AddJSFile(new XVar("include/runnerJS/pages/ReportPageDP.js"), new XVar("include/runnerJS/pages/ReportPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ReportPage.js"), new XVar("include/runnerJS/pages/DataPageWithSearch.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPageAjax.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ListPageDP.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/CheckboxesPage.js"), new XVar("include/runnerJS/pages/ListPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/MembersPage.js"), new XVar("include/runnerJS/pages/CheckboxesPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/RightsPage.js"), new XVar("include/runnerJS/pages/CheckboxesPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ChangePwdPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ExportPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/ImportPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/pages/RegisterPage.js"), new XVar("include/runnerJS/pages/RunnerPage.js"));
				AddJSFile(new XVar("include/runnerJS/FilterControl.js"), new XVar("include/runnerJS/editControls/DateFieldControl.js"));
				AddJSFile(new XVar("include/runnerJS/SearchForm.js"));
				AddJSFile(new XVar("include/runnerJS/SearchField.js"));
				AddJSFile(new XVar("include/runnerJS/SearchFormWithUI.js"), new XVar("include/runnerJS/SearchForm.js"));
				AddJSFile(new XVar("include/runnerJS/SearchController.js"), new XVar("include/runnerJS/SearchFormWithUI.js"));
				AddJSFile(new XVar("include/runnerJS/SearchParamsLogger.js"), new XVar("include/runnerJS/SearchController.js"));
				AddJSFile(new XVar("include/runnerJS/RunnerForm.js"));
				AddJSFile(new XVar("include/runnerJS/RunnerBricks.js"));
				AddJSFile(new XVar("include/runnerJS/RunnerMenu.js"));
				if(XVar.Pack(this.lockingObj))
				{
					AddJSFile(new XVar("include/runnerJS/RunnerLocking.js"));
				}
				if(XVar.Pack(this.is508))
				{
					AddJSFile(new XVar("include/runnerJS/RunnerSection508.js"));
				}
				if((XVar)((XVar)(this.pSet.isAddPageEvents())  && (XVar)(this.pageType != Constants.PAGE_LOGIN))  && (XVar)(this.shortTableName != ""))
				{
					AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", this.shortTableName, ".js")), new XVar("include/runnerJS/pages/PageSettings.js"), new XVar("include/runnerJS/button.js"));
				}
			}
			else
			{
				if((XVar)((XVar)(this.pSet.isAddPageEvents())  && (XVar)(this.pageType != Constants.PAGE_LOGIN))  && (XVar)(this.shortTableName != ""))
				{
					AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", this.shortTableName, ".js")));
				}
			}
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				AddJSFile(new XVar("include/yui/yui-min.js"));
			}
			if(XVar.Pack(this.isUseAjaxSuggest))
			{
				AddJSFile(new XVar("include/ajaxsuggest.js"));
			}
			else
			{
				if(XVar.Pack(MVCFunctions.count(this.allDetailsTablesArr)))
				{
					dynamic i = null;
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
					{
						if(this.allDetailsTablesArr[i]["previewOnList"] == Constants.DP_POPUP)
						{
							AddJSFile(new XVar("include/ajaxsuggest.js"));
						}
						break;
					}
				}
			}
			if(XVar.Pack(this.isUseCK))
			{
				AddJSFile(new XVar("plugins/ckeditor/ckeditor.js"));
			}
			addControlsJSAndCSS();
			return null;
		}
		public virtual XVar addControlsJSAndCSS()
		{
			this.controls.addControlsJSAndCSS();
			this.viewControls.addControlsJSAndCSS();
			return null;
		}
		public virtual XVar PrepareJS()
		{
			return LoadJS_CSS();
		}
		public virtual XVar addButtonHandlers()
		{
			if((XVar)(!(XVar)(this.pSet.isAddPageEvents()))  || (XVar)(this.shortTableName == ""))
			{
				return false;
			}
			if(XVar.Equals(XVar.Pack(this.debugJSMode), XVar.Pack(true)))
			{
				AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", this.shortTableName, ".js")), new XVar("include/runnerJS/pages/PageSettings.js"));
			}
			else
			{
				AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/events/pageevents_", this.shortTableName, ".js")));
			}
			return true;
		}
		public virtual XVar setGoogleMapsParams(dynamic _param_fieldsArr)
		{
			#region pass-by-value parameters
			dynamic fieldsArr = XVar.Clone(_param_fieldsArr);
			#endregion

			this.googleMapCfg.InitAndSetArrayItem(this.pSet.isUseMainMaps(), "isUseMainMaps");
			this.googleMapCfg.InitAndSetArrayItem(this.pSet.isUseFieldsMaps(), "isUseFieldsMaps");
			fillAdvancedMapData();
			if(XVar.Pack(this.googleMapCfg["isUseFieldsMaps"]))
			{
				foreach (KeyValuePair<XVar, dynamic> f in fieldsArr.GetEnumerator())
				{
					if(f.Value["viewFormat"] == Constants.FORMAT_MAP)
					{
						dynamic fieldMap = XVar.Array();
						this.googleMapCfg.InitAndSetArrayItem(XVar.Array(), "fieldsAsMap", f.Value["fName"]);
						fieldMap = XVar.Clone(this.pSet.getMapData((XVar)(f.Value["fName"])));
						this.googleMapCfg.InitAndSetArrayItem((XVar.Pack(fieldMap["width"]) ? XVar.Pack(fieldMap["width"]) : XVar.Pack(0)), "fieldsAsMap", f.Value["fName"], "width");
						this.googleMapCfg.InitAndSetArrayItem((XVar.Pack(fieldMap["height"]) ? XVar.Pack(fieldMap["height"]) : XVar.Pack(0)), "fieldsAsMap", f.Value["fName"], "height");
						this.googleMapCfg.InitAndSetArrayItem(fieldMap["address"], "fieldsAsMap", f.Value["fName"], "addressField");
						this.googleMapCfg.InitAndSetArrayItem(fieldMap["lat"], "fieldsAsMap", f.Value["fName"], "latField");
						this.googleMapCfg.InitAndSetArrayItem(fieldMap["lng"], "fieldsAsMap", f.Value["fName"], "lngField");
						this.googleMapCfg.InitAndSetArrayItem(fieldMap["desc"], "fieldsAsMap", f.Value["fName"], "descField");
						this.googleMapCfg.InitAndSetArrayItem(this.pSet.getMapIcon((XVar)(f.Value["fName"]), (XVar)(this.data)), "fieldsAsMap", f.Value["fName"], "mapIcon");
						if(XVar.Pack(fieldMap.KeyExists("zoom")))
						{
							this.googleMapCfg.InitAndSetArrayItem(fieldMap["zoom"], "fieldsAsMap", f.Value["fName"], "zoom");
						}
					}
				}
			}
			this.googleMapCfg.InitAndSetArrayItem((XVar)((XVar)(this.googleMapCfg["isUseMainMaps"])  || (XVar)(this.googleMapCfg["isUseFieldsMaps"]))  || (XVar)(mapsExists()), "isUseGoogleMap");
			this.googleMapCfg.InitAndSetArrayItem(this.tName, "tName");
			return null;
		}
		public virtual XVar fillAdvancedMapData()
		{
			dynamic advMaps = XVar.Array(), clustering = null, data = XVar.Array(), editlink = null, i = null, keys = XVar.Array(), recId = null, rs = null, tKeys = XVar.Array();
			advMaps = XVar.Clone(XVar.Array());
			clustering = new XVar(false);
			foreach (KeyValuePair<XVar, dynamic> mapData in this.googleMapCfg["mapsData"].GetEnumerator())
			{
				if(XVar.Pack(this.googleMapCfg["mapsData"][mapData.Key]["showAllMarkers"]))
				{
					advMaps.InitAndSetArrayItem(mapData.Key, null);
				}
				if((XVar)(this.googleMapCfg["mapsData"][mapData.Key]["clustering"])  && (XVar)(this.mapProvider == Constants.GOOGLE_MAPS))
				{
					clustering = new XVar(true);
				}
			}
			if(XVar.Pack(!(XVar)(advMaps)))
			{
				return null;
			}
			if(XVar.Pack(clustering))
			{
				AddJSFile(new XVar("include/markerclusterer.js"));
			}
			tKeys = XVar.Clone(this.pSet.getTableKeys());
			rs = XVar.Clone(this.connection.query((XVar)(this.querySQL)));
			recId = XVar.Clone(this.recId);
			while(XVar.Pack(data = XVar.Clone(rs.fetchAssoc())))
			{
				editlink = new XVar("");
				keys = XVar.Clone(XVar.Array());
				i = new XVar(0);
				for(;i < MVCFunctions.count(tKeys); i++)
				{
					if(i != XVar.Pack(0))
					{
						editlink = MVCFunctions.Concat(editlink, "&");
					}
					editlink = MVCFunctions.Concat(editlink, "editid", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[tKeys[i]])))));
					keys.InitAndSetArrayItem(data[tKeys[i]], i);
				}
				foreach (KeyValuePair<XVar, dynamic> mapId in advMaps.GetEnumerator())
				{
					addBigGoogleMapMarker((XVar)(mapId.Value), (XVar)(data), (XVar)(keys), (XVar)(++(recId)), (XVar)(editlink));
				}
			}
			return null;
		}
		public virtual XVar addBigGoogleMapMarkers(dynamic data, dynamic _param_keys, dynamic _param_editLink = null)
		{
			#region default values
			if(_param_editLink as Object == null) _param_editLink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			dynamic editLink = XVar.Clone(_param_editLink);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> mapId in this.googleMapCfg["mainMapIds"].GetEnumerator())
			{
				if(XVar.Pack(fetchMapMarkersInSeparateQuery((XVar)(mapId.Value))))
				{
					continue;
				}
				addBigGoogleMapMarker((XVar)(mapId.Value), (XVar)(data), (XVar)(keys), (XVar)(this.recId), (XVar)(editLink));
			}
			return null;
		}
		public virtual XVar fetchMapMarkersInSeparateQuery(dynamic _param_mapId)
		{
			#region pass-by-value parameters
			dynamic mapId = XVar.Clone(_param_mapId);
			#endregion

			return (XVar)((XVar)(this.googleMapCfg["mapsData"][mapId]["heatMap"])  || (XVar)(this.googleMapCfg["mapsData"][mapId]["clustering"]))  && (XVar)(this.mapProvider == Constants.GOOGLE_MAPS);
		}
		public virtual XVar addBigGoogleMapMarker(dynamic _param_mapId, dynamic data, dynamic _param_keys, dynamic _param_recId, dynamic _param_editLink = null)
		{
			#region default values
			if(_param_editLink as Object == null) _param_editLink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic mapId = XVar.Clone(_param_mapId);
			dynamic keys = XVar.Clone(_param_keys);
			dynamic recId = XVar.Clone(_param_recId);
			dynamic editLink = XVar.Clone(_param_editLink);
			#endregion

			dynamic addressF = null, descF = null, latF = null, lngF = null, markerArr = XVar.Array(), markerAsEditLink = null, weightF = null;
			latF = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["latField"]);
			lngF = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["lngField"]);
			addressF = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["addressField"]);
			if((XVar)((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(data[latF]))))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(data[lngF])))))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(data[addressF])))))
			{
				return null;
			}
			descF = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["descField"]);
			markerAsEditLink = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["markerAsEditLink"]);
			weightF = XVar.Clone(this.googleMapCfg["mapsData"][mapId]["weightField"]);
			markerArr = XVar.Clone(XVar.Array());
			markerArr.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[latF]) ? XVar.Pack(data[latF]) : XVar.Pack("")))), "lat");
			markerArr.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[lngF]) ? XVar.Pack(data[lngF]) : XVar.Pack("")))), "lng");
			markerArr.InitAndSetArrayItem((XVar.Pack(data[addressF]) ? XVar.Pack(data[addressF]) : XVar.Pack("")), "address");
			markerArr.InitAndSetArrayItem((XVar.Pack(data[descF]) ? XVar.Pack(data[descF]) : XVar.Pack(markerArr["address"])), "desc");
			if(XVar.Pack(weightF))
			{
				markerArr.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[weightF]) ? XVar.Pack(data[weightF]) : XVar.Pack("")))), "weight");
			}
			if((XVar)(markerAsEditLink)  && (XVar)(editAvailable()))
			{
				markerArr.InitAndSetArrayItem(MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("edit"), (XVar)(editLink)), "link");
			}
			else
			{
				if(XVar.Pack(viewAvailable()))
				{
					markerArr.InitAndSetArrayItem(MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("view"), (XVar)(editLink)), "link");
				}
			}
			markerArr.InitAndSetArrayItem(recId, "recId");
			markerArr.InitAndSetArrayItem(keys, "keys");
			if(XVar.Pack(this.googleMapCfg["mapsData"][mapId]["dashMap"]))
			{
				markerArr.InitAndSetArrayItem(this.dashSet.getDashMapIcon((XVar)(this.dashElementName), (XVar)(data)), "mapIcon");
				markerArr.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(data)), "masterKeys");
			}
			else
			{
				if(XVar.Pack(this.googleMapCfg["mapsData"][mapId]["markerField"]))
				{
					markerArr.InitAndSetArrayItem(data[this.googleMapCfg["mapsData"][mapId]["markerField"]], "mapIcon");
				}
				if((XVar)(!(XVar)(markerArr["mapIcon"]))  && (XVar)(this.googleMapCfg["mapsData"][mapId]["markerIcon"]))
				{
					markerArr.InitAndSetArrayItem(this.googleMapCfg["mapsData"][mapId]["markerIcon"], "mapIcon");
				}
			}
			this.googleMapCfg.InitAndSetArrayItem(markerArr, "mapsData", mapId, "markers", null);
			return null;
		}
		protected virtual XVar getMarkerMasterKeys(dynamic data)
		{
			dynamic dDataSourceTable = null, detailTableData = XVar.Array(), i = null, masterKeys = XVar.Array();
			masterKeys = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				detailTableData = XVar.Clone(this.allDetailsTablesArr[i]);
				dDataSourceTable = XVar.Clone(detailTableData["dDataSourceTable"]);
				if((XVar)(detailTableData["dType"] == Constants.PAGE_LIST)  && (XVar)(!(XVar)(this.permis[dDataSourceTable]["search"])))
				{
					continue;
				}
				masterKeys.InitAndSetArrayItem(XVar.Array(), dDataSourceTable);
				foreach (KeyValuePair<XVar, dynamic> m in this.masterKeysByD[i].GetEnumerator())
				{
					dynamic curM = null;
					curM = XVar.Clone(m.Value);
					if(this.pageType == Constants.PAGE_REPORT)
					{
						curM = XVar.Clone(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(curM)), "_dbvalue"));
					}
					masterKeys.InitAndSetArrayItem(data[curM], dDataSourceTable, MVCFunctions.Concat("masterkey", m.Key + 1));
				}
			}
			return masterKeys;
		}
		public virtual XVar addGoogleMapData(dynamic _param_fName, dynamic data, dynamic _param_keys = null, dynamic _param_editLink = null)
		{
			#region default values
			if(_param_keys as Object == null) _param_keys = new XVar(XVar.Array());
			if(_param_editLink as Object == null) _param_editLink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic keys = XVar.Clone(_param_keys);
			dynamic editLink = XVar.Clone(_param_editLink);
			#endregion

			dynamic address = null, desc = null, fieldMap = XVar.Array(), lat = null, lng = null, mapData = XVar.Array(), mapId = null, viewLink = null;
			fieldMap = XVar.Clone(this.pSet.getMapData((XVar)(fName)));
			mapData = XVar.Clone(XVar.Array());
			mapData.InitAndSetArrayItem(fName, "fName");
			mapData.InitAndSetArrayItem((XVar.Pack(fieldMap.KeyExists("zoom")) ? XVar.Pack(fieldMap["zoom"]) : XVar.Pack("")), "zoom");
			mapData.InitAndSetArrayItem("FIELD_MAP", "type");
			mapData.InitAndSetArrayItem(data[fName], "mapFieldValue");
			address = XVar.Clone((XVar.Pack(data[fieldMap["address"]]) ? XVar.Pack(data[fieldMap["address"]]) : XVar.Pack("")));
			lat = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[fieldMap["lat"]]) ? XVar.Pack(data[fieldMap["lat"]]) : XVar.Pack("")))));
			lng = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)((XVar.Pack(data[fieldMap["lng"]]) ? XVar.Pack(data[fieldMap["lng"]]) : XVar.Pack("")))));
			desc = XVar.Clone((XVar.Pack(data[fieldMap["desc"]]) ? XVar.Pack(data[fieldMap["desc"]]) : XVar.Pack(address)));
			viewLink = new XVar("");
			if((XVar)(this.pageType != Constants.PAGE_VIEW)  && (XVar)(viewAvailable()))
			{
				viewLink = XVar.Clone(MVCFunctions.GetTableLink((XVar)(this.shortTableName), new XVar("view"), (XVar)(editLink)));
			}
			mapData.InitAndSetArrayItem(new XVar("address", address, "lat", lat, "lng", lng, "link", viewLink, "desc", desc, "recId", this.recId, "keys", keys, "mapIcon", this.pSet.getMapIcon((XVar)(fName), (XVar)(data))), "markers", null);
			mapId = XVar.Clone(MVCFunctions.Concat("littleMap_", MVCFunctions.GoodFieldName((XVar)(fName)), "_", this.recId));
			this.googleMapCfg.InitAndSetArrayItem(mapData, "mapsData", mapId);
			this.googleMapCfg.InitAndSetArrayItem(mapId, "fieldMapsIds", null);
			return this.googleMapCfg["mapsData"][mapId];
		}
		protected virtual XVar getDashMapsIconsData(dynamic data)
		{
			dynamic mapIconsData = XVar.Array();
			mapIconsData = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(this.dashTName)))
			{
				return mapIconsData;
			}
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)(dElem.Value["table"] != this.tName)  || (XVar)(dElem.Value["type"] != Constants.DASHBOARD_MAP))
				{
					continue;
				}
				mapIconsData.InitAndSetArrayItem(this.dashSet.getDashMapIcon((XVar)(dElem.Value["elementName"]), (XVar)(data)), dElem.Value["elementName"]);
			}
			return mapIconsData;
		}
		protected virtual XVar getFieldMapIconsData(dynamic data)
		{
			dynamic iconsData = XVar.Array();
			iconsData = XVar.Clone(XVar.Array());
			if(XVar.Pack(this.pSet.isUseFieldsMaps()))
			{
				foreach (KeyValuePair<XVar, dynamic> f in this.pSet.getFieldsList().GetEnumerator())
				{
					if(this.pSet.getViewFormat((XVar)(f.Value)) == Constants.FORMAT_MAP)
					{
						iconsData.InitAndSetArrayItem(this.pSet.getMapIcon((XVar)(f.Value), (XVar)(data)), f.Value);
					}
				}
			}
			return iconsData;
		}
		public virtual XVar initGmaps()
		{
			if(XVar.Pack(!(XVar)(this.googleMapCfg["isUseGoogleMap"])))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> mapId in this.googleMapCfg["mainMapIds"].GetEnumerator())
			{
				if(XVar.Equals(XVar.Pack(this.googleMapCfg["mapsData"][mapId.Value]["showCenterLink"]), XVar.Pack(1)))
				{
					this.googleMapCfg.InitAndSetArrayItem(this.googleMapCfg["mapsData"][mapId.Value]["centerLinkText"], "centerLinkText");
					break;
				}
			}
			this.jsSettings.InitAndSetArrayItem(editAvailable(), "tableSettings", this.tName, "editAvailable");
			this.jsSettings.InitAndSetArrayItem(viewAvailable(), "tableSettings", this.tName, "viewAvailable");
			includeOSMfile();
			AddJSFile(new XVar("include/runnerJS/MapManager.js"), new XVar("include/runnerJS/ControlConstants.js"));
			AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/", getIncludeFileMapProvider())), new XVar("include/runnerJS/MapManager.js"));
			this.googleMapCfg.InitAndSetArrayItem(this.id, "id");
			if(XVar.Pack(!(XVar)(this.googleMapCfg["APIcode"])))
			{
				this.googleMapCfg.InitAndSetArrayItem("", "APIcode");
			}
			this.controlsMap.InitAndSetArrayItem(this.googleMapCfg, "gMaps");
			return null;
		}
		public virtual XVar addCenterLink(ref dynamic value, dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			if(XVar.Pack(!(XVar)(this.googleMapCfg["isUseMainMaps"])))
			{
				return value;
			}
			foreach (KeyValuePair<XVar, dynamic> mapId in this.googleMapCfg["mainMapIds"].GetEnumerator())
			{
				if((XVar)(this.googleMapCfg["mapsData"][mapId.Value]["addressField"] != fName)  || (XVar)(!(XVar)(this.googleMapCfg["mapsData"][mapId.Value]["showCenterLink"])))
				{
					continue;
				}
				if(XVar.Equals(XVar.Pack(this.googleMapCfg["mapsData"][mapId.Value]["showCenterLink"]), XVar.Pack(1)))
				{
					value = XVar.Clone(this.googleMapCfg["mapsData"][mapId.Value]["centerLinkText"]);
				}
				return MVCFunctions.Concat("<a href=\"#\" type=\"centerOnMarker", this.id, "\" recId=\"", this.recId, "\">", value, "</a>");
			}
			return value;
		}
		public virtual XVar getGeoCoordinates(dynamic _param_address)
		{
			#region pass-by-value parameters
			dynamic address = XVar.Clone(_param_address);
			#endregion

			return CommonFunctions.getLatLngByAddr((XVar)(address));
		}
		public virtual XVar glueAddressByAddressFields(dynamic _param_values)
		{
			#region pass-by-value parameters
			dynamic values = XVar.Clone(_param_values);
			#endregion

			dynamic address = null, geoData = XVar.Array();
			address = new XVar("");
			geoData = XVar.Clone(this.pSet.getGeocodingData());
			foreach (KeyValuePair<XVar, dynamic> field in geoData["addressFields"].GetEnumerator())
			{
				dynamic addressField = null;
				addressField = XVar.Clone(MVCFunctions.trim((XVar)(values[field.Value])));
				if((XVar)(values.KeyExists(field.Value))  && (XVar)(MVCFunctions.strlen((XVar)(addressField))))
				{
					address = MVCFunctions.Concat(address, addressField, " ");
				}
			}
			return MVCFunctions.trim((XVar)(address));
		}
		public virtual XVar setUpdatedLatLng(dynamic values, dynamic _param_oldvalues = null)
		{
			#region default values
			if(_param_oldvalues as Object == null) _param_oldvalues = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic oldvalues = XVar.Clone(_param_oldvalues);
			#endregion

			dynamic address = null, location = XVar.Array(), mapData = XVar.Array(), oldaddress = null;
			if(XVar.Pack(!(XVar)(this.pSet.isUpdateLatLng())))
			{
				return null;
			}
			mapData = XVar.Clone(this.pSet.getGeocodingData());
			address = XVar.Clone(glueAddressByAddressFields((XVar)(values)));
			if(address == XVar.Pack(""))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(oldvalues == null)))
			{
				oldaddress = XVar.Clone(glueAddressByAddressFields((XVar)(oldvalues)));
			}
			else
			{
				if((XVar)(MVCFunctions.trim((XVar)(values[mapData["latField"]])) != "")  && (XVar)(MVCFunctions.trim((XVar)(values[mapData["lngField"]])) != ""))
				{
					return null;
				}
			}
			if((XVar)((XVar)((XVar)(oldvalues)  && (XVar)(MVCFunctions.trim((XVar)(oldvalues[mapData["latField"]])) != ""))  && (XVar)(MVCFunctions.trim((XVar)(oldvalues[mapData["lngField"]])) != ""))  && (XVar)(address == oldaddress))
			{
				return null;
			}
			location = XVar.Clone(getGeoCoordinates((XVar)(address)));
			if(XVar.Pack(!(XVar)(location)))
			{
				return null;
			}
			values.InitAndSetArrayItem(location["lat"], mapData["latField"]);
			values.InitAndSetArrayItem(location["lng"], mapData["lngField"]);
			return null;
		}
		protected virtual XVar getWhereByMap()
		{
			dynamic tGrid = null;
			if((XVar)(!(XVar)(this.mapRefresh))  || (XVar)(!(XVar)(MVCFunctions.count(this.vpCoordinates))))
			{
				return "";
			}
			tGrid = XVar.Clone(hasTableDashGridElement());
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)((XVar)(dElem.Value["table"] == this.tName)  && (XVar)(dElem.Value["type"] == Constants.DASHBOARD_MAP))  && (XVar)((XVar)(dElem.Value["updateMoved"])  || (XVar)(!(XVar)(tGrid))))
				{
					return getLatLngWhere((XVar)(dElem.Value["latF"]), (XVar)(dElem.Value["lonF"]));
				}
			}
			return "";
		}
		protected virtual XVar getLatLngWhere(dynamic _param_latFName, dynamic _param_lngFName)
		{
			#region pass-by-value parameters
			dynamic latFName = XVar.Clone(_param_latFName);
			dynamic lngFName = XVar.Clone(_param_lngFName);
			#endregion

			dynamic e = null, latSQLName = null, lngSQLName = null, n = null, s = null, w = null;
			if(XVar.Pack(this.skipMapFilter))
			{
				return "";
			}
			if((XVar)(!(XVar)(this.mapRefresh))  || (XVar)(!(XVar)(MVCFunctions.count(this.vpCoordinates))))
			{
				return "";
			}
			latSQLName = XVar.Clone(getFieldSQLDecrypt((XVar)(latFName)));
			lngSQLName = XVar.Clone(getFieldSQLDecrypt((XVar)(lngFName)));
			s = XVar.Clone(this.cipherer.MakeDBValue((XVar)(latFName), (XVar)(this.vpCoordinates["s"]), new XVar(""), new XVar(true)));
			n = XVar.Clone(this.cipherer.MakeDBValue((XVar)(latFName), (XVar)(this.vpCoordinates["n"]), new XVar(""), new XVar(true)));
			w = XVar.Clone(this.cipherer.MakeDBValue((XVar)(lngFName), (XVar)(this.vpCoordinates["w"]), new XVar(""), new XVar(true)));
			e = XVar.Clone(this.cipherer.MakeDBValue((XVar)(lngFName), (XVar)(this.vpCoordinates["e"]), new XVar(""), new XVar(true)));
			if(this.vpCoordinates["w"] <= this.vpCoordinates["e"])
			{
				return MVCFunctions.Concat(latSQLName, ">=", s, " AND ", latSQLName, "<=", n, " AND ", lngSQLName, "<=", e, " AND ", lngSQLName, ">=", w);
			}
			else
			{
				return MVCFunctions.Concat(latSQLName, ">=", s, " AND ", latSQLName, "<=", n, " AND (", lngSQLName, "<=", e, " OR ", lngSQLName, ">=", w, ")");
			}
			return null;
		}
		protected virtual XVar getPageFields()
		{
			return this.pSet.getFieldsList();
		}
		public virtual XVar getPermissions(dynamic _param_tName = null)
		{
			#region default values
			if(_param_tName as Object == null) _param_tName = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			#endregion

			dynamic resArr = XVar.Array(), strPerm = null;
			resArr = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(tName)))
			{
				tName = XVar.Clone(this.tName);
			}
			strPerm = XVar.Clone(CommonFunctions.GetUserPermissions((XVar)(tName)));
			if(XVar.Pack(CommonFunctions.isLogged()))
			{
				resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("A"))), XVar.Pack(false)), "add");
				resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("D"))), XVar.Pack(false)), "delete");
				resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("E"))), XVar.Pack(false)), "edit");
			}
			resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("S"))), XVar.Pack(false)), "search");
			resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("P"))), XVar.Pack(false)), "export");
			resArr.InitAndSetArrayItem(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(strPerm), new XVar("I"))), XVar.Pack(false)), "import");
			return resArr;
		}
		public virtual XVar eventExists(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			if(XVar.Pack(!(XVar)(this.eventsObject)))
			{
				return false;
			}
			return this.eventsObject.exists((XVar)(name));
		}
		public virtual XVar events()
		{
			return this.eventsObject;
		}
		public virtual XVar mapsExists()
		{
			if(XVar.Pack(!(XVar)(this.eventsObject)))
			{
				return false;
			}
			return this.eventsObject.existsMap((XVar)(this.pageType));
		}
		protected virtual XVar hasTableDashGridElement()
		{
			if(XVar.Pack(!(XVar)(this.dashSet)))
			{
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)(dElem.Value["table"] == this.tName)  && (XVar)(dElem.Value["type"] == Constants.DASHBOARD_LIST))
				{
					return true;
				}
			}
			return false;
		}
		protected virtual XVar hasDashMapElement()
		{
			if(XVar.Pack(!(XVar)(this.dashSet)))
			{
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> dElem in this.dashSet.getDashboardElements().GetEnumerator())
			{
				if((XVar)(dElem.Value["table"] == this.tName)  && (XVar)(dElem.Value["type"] == Constants.DASHBOARD_MAP))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getNextPrevQueryComponents(dynamic data)
		{
			dynamic equal = null, i = null, nextOrder = XVar.Array(), nextWhere = null, of = XVar.Array(), orderClause = null, orderFields = XVar.Array(), prev = null, prevOrder = XVar.Array(), prevWhere = null, query = null, sqlColumn = null, sqlValue = null, var_next = null;
			orderClause = XVar.Clone(OrderClause.createFromPage(this, new XVar(false)));
			orderFields = orderClause.getOrderFields();
			query = XVar.Clone(this.pSet.getQueryObject());
			if((XVar)(!(XVar)(orderFields))  || (XVar)(!(XVar)(query)))
			{
				return XVar.Array();
			}
			nextWhere = new XVar("");
			prevWhere = new XVar("");
			nextOrder = XVar.Clone(XVar.Array());
			prevOrder = XVar.Clone(XVar.Array());
			i = XVar.Clone(MVCFunctions.count(orderFields) - 1);
			for(;XVar.Pack(0) <= i; --(i))
			{
				of = XVar.Clone(orderFields[i]);
				sqlColumn = XVar.Clone(this.connection.addFieldWrappers((XVar)(of["column"])));
				nextOrder.InitAndSetArrayItem(MVCFunctions.Concat(sqlColumn, " ", of["dir"]), null);
				prevOrder.InitAndSetArrayItem(MVCFunctions.Concat(sqlColumn, " ", (XVar.Pack(of["dir"] == "ASC") ? XVar.Pack("DESC") : XVar.Pack("ASC"))), null);
				sqlValue = XVar.Clone(this.cipherer.MakeDBValue((XVar)(of["column"]), (XVar)(data[of["column"]]), new XVar(""), new XVar(true)));
				equal = new XVar("");
				if(i < MVCFunctions.count(orderFields) - 1)
				{
					equal = XVar.Clone(CommonFunctions.sqlEqual((XVar)(sqlColumn), (XVar)(sqlValue)));
				}
				var_next = XVar.Clone(CommonFunctions.sqlMoreThan((XVar)(sqlColumn), (XVar)(sqlValue)));
				prev = XVar.Clone(CommonFunctions.sqlLessThan((XVar)(sqlColumn), (XVar)(sqlValue)));
				if(of["dir"] == "DESC")
				{
					prev = XVar.Clone(CommonFunctions.sqlMoreThan((XVar)(sqlColumn), (XVar)(sqlValue)));
					var_next = XVar.Clone(CommonFunctions.sqlLessThan((XVar)(sqlColumn), (XVar)(sqlValue)));
				}
				nextWhere = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, var_next, 1, SQLQuery.combineCases((XVar)(new XVar(0, equal, 1, nextWhere)), new XVar("and")))), new XVar("or")));
				prevWhere = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, prev, 1, SQLQuery.combineCases((XVar)(new XVar(0, equal, 1, prevWhere)), new XVar("and")))), new XVar("or")));
			}
			return new XVar("nextWhere", nextWhere, "prevWhere", prevWhere, "nextOrder", MVCFunctions.implode(new XVar(", "), (XVar)(MVCFunctions.array_reverse((XVar)(nextOrder)))), "prevOrder", MVCFunctions.implode(new XVar(", "), (XVar)(MVCFunctions.array_reverse((XVar)(prevOrder)))));
		}
		public virtual XVar getNextPrevRecordKeys(dynamic data, dynamic _param_what = null)
		{
			#region default values
			if(_param_what as Object == null) _param_what = new XVar(Constants.BOTH_RECORDS);
			#endregion

			#region pass-by-value parameters
			dynamic what = XVar.Clone(_param_what);
			#endregion

			dynamic baseSQL = null, keysFields = XVar.Array(), nextPrevComponents = XVar.Array(), prev = null, rs = null, sql = XVar.Array(), subQuery = null, var_next = null;
			nextPrevComponents = XVar.Clone(getNextPrevQueryComponents((XVar)(data)));
			if(XVar.Pack(!(XVar)(nextPrevComponents)))
			{
				return XVar.Array();
			}
			sql = XVar.Clone(getSubsetSQLComponents());
			subQuery = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			var_next = XVar.Clone(XVar.Array());
			prev = XVar.Clone(XVar.Array());
			keysFields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				keysFields.InitAndSetArrayItem(this.connection.addFieldWrappers((XVar)(k.Value)), null);
			}
			baseSQL = XVar.Clone(MVCFunctions.Concat("select ", MVCFunctions.implode(new XVar(", "), (XVar)(keysFields)), " from ( ", subQuery, ") a "));
			if((XVar)(what == Constants.BOTH_RECORDS)  || (XVar)(what == Constants.NEXT_RECORD))
			{
				GlobalVars.strSQL = XVar.Clone(baseSQL);
				if(XVar.Pack(MVCFunctions.strlen((XVar)(nextPrevComponents["nextWhere"]))))
				{
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, "where ", nextPrevComponents["nextWhere"]);
				}
				if(XVar.Pack(MVCFunctions.strlen((XVar)(nextPrevComponents["nextOrder"]))))
				{
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, "order by ", nextPrevComponents["nextOrder"]);
				}
				rs = XVar.Clone(this.connection.queryPage((XVar)(GlobalVars.strSQL), new XVar(1), new XVar(1), new XVar(true)));
				if(XVar.Pack(rs))
				{
					var_next = XVar.Clone(rs.fetchNumeric());
				}
			}
			if((XVar)(what == Constants.BOTH_RECORDS)  || (XVar)(what == Constants.PREV_RECORD))
			{
				GlobalVars.strSQL = XVar.Clone(baseSQL);
				if(XVar.Pack(MVCFunctions.strlen((XVar)(nextPrevComponents["prevWhere"]))))
				{
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, "where ", nextPrevComponents["prevWhere"]);
				}
				if(XVar.Pack(MVCFunctions.strlen((XVar)(nextPrevComponents["prevOrder"]))))
				{
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, "order by ", nextPrevComponents["prevOrder"]);
				}
				rs = XVar.Clone(this.connection.queryPage((XVar)(GlobalVars.strSQL), new XVar(1), new XVar(1), new XVar(true)));
				if(XVar.Pack(rs))
				{
					prev = XVar.Clone(rs.fetchNumeric());
				}
			}
			return new XVar("next", var_next, "prev", prev);
		}
		protected virtual XVar updateActualListPageNumber(dynamic _param_prevWhere, dynamic _param_sql_prev)
		{
			#region pass-by-value parameters
			dynamic prevWhere = XVar.Clone(_param_prevWhere);
			dynamic sql_prev = XVar.Clone(_param_sql_prev);
			#endregion

			if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				return null;
			}
			if(prevWhere == " 1=0 ")
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			else
			{
				dynamic currentRow = null, pageRow = XVar.Array(), pageSQL = null, pageSize = null;
				pageSQL = XVar.Clone(MVCFunctions.Concat("select count(*) from (", sql_prev, ") tcount"));
				pageRow = XVar.Clone(this.connection.query((XVar)(pageSQL)).fetchNumeric());
				currentRow = XVar.Clone(pageRow[0]);
				if(0 < this.pageSize)
				{
					pageSize = XVar.Clone(this.pageSize);
				}
				else
				{
					pageSize = XVar.Clone(this.pSet.getInitialPageSize());
				}
				this.myPage = XVar.Clone((XVar)Math.Floor((double)(currentRow / pageSize)) + 1);
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = this.myPage;
			}
			return null;
		}
		public virtual XVar getOrderByClause()
		{
			dynamic orderClause = null;
			orderClause = XVar.Clone(OrderClause.createFromPage(this));
			return orderClause.getOrderByExpression();
		}
		protected virtual XVar getKeyParams(dynamic _param_keys = null)
		{
			#region default values
			if(_param_keys as Object == null) _param_keys = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			dynamic keyParams = XVar.Array();
			if(XVar.Pack(keys == null))
			{
				keys = XVar.Clone(this.keys);
			}
			if(XVar.Pack(!(XVar)(keys)))
			{
				return "";
			}
			keyParams = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("editid", k.Key + 1, "=", MVCFunctions.RawUrlDecode((XVar)((XVar.Pack(keys.KeyExists(k.Value)) ? XVar.Pack(keys[k.Value]) : XVar.Pack(keys[k.Key]))))), null);
			}
			return MVCFunctions.implode(new XVar("&"), (XVar)(keyParams));
		}
		public virtual XVar assignPrevNextButtons(dynamic _param_showNext, dynamic _param_showPrev, dynamic _param_dashGridBased = null)
		{
			#region default values
			if(_param_dashGridBased as Object == null) _param_dashGridBased = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic showNext = XVar.Clone(_param_showNext);
			dynamic showPrev = XVar.Clone(_param_showPrev);
			dynamic dashGridBased = XVar.Clone(_param_dashGridBased);
			#endregion

			if(XVar.Pack(!(XVar)(this.pSet.useMoveNext())))
			{
				return null;
			}
			if((XVar)(showNext)  || (XVar)(dashGridBased))
			{
				this.xt.assign(new XVar("next_button"), new XVar(true));
				this.xt.assign(new XVar("nextbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"nextButton", this.id, "\"")));
				if(XVar.Pack(dashGridBased))
				{
					this.xt.assign(new XVar("nextbutton_class"), new XVar("rnr-invisible-button"));
				}
			}
			else
			{
				if(XVar.Pack(showPrev))
				{
					this.xt.assign(new XVar("next_button"), new XVar(true));
					this.xt.assign(new XVar("nextbutton_class"), new XVar("rnr-invisible-button"));
				}
				else
				{
					this.xt.assign(new XVar("next_button"), new XVar(false));
				}
			}
			if((XVar)(showPrev)  || (XVar)(dashGridBased))
			{
				this.xt.assign(new XVar("prev_button"), new XVar(true));
				this.xt.assign(new XVar("prevbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"prevButton", this.id, "\"")));
				if(XVar.Pack(dashGridBased))
				{
					this.xt.assign(new XVar("prevbutton_class"), new XVar("rnr-invisible-button"));
				}
			}
			else
			{
				if(XVar.Pack(showNext))
				{
					this.xt.assign(new XVar("prev_button"), new XVar(true));
					this.xt.assign(new XVar("prevbutton_class"), new XVar("rnr-invisible-button"));
				}
				else
				{
					this.xt.assign(new XVar("prev_button"), new XVar(false));
				}
			}
			return null;
		}
		public virtual XVar checkCaptcha()
		{
			dynamic captchaSettings = XVar.Array();
			this.isCaptchaOk = new XVar(true);
			if(XVar.Pack(!(XVar)(this.Invoke("captchaExists"))))
			{
				return true;
			}
			if(XVar.Pack(XSession.Session.KeyExists("count_passes_captcha")))
			{
				XSession.Session["count_passes_captcha"] = XSession.Session["count_passes_captcha"] + 1;
				return true;
			}
			if((XVar)(!(XVar)(XSession.Session.KeyExists(MVCFunctions.Concat("isCaptcha", getCaptchaId(), "Showed"))))  && (XVar)(this.captchaValue == ""))
			{
				return true;
			}
			captchaSettings = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("CaptchaSettings"), new XVar("")));
			if((XVar)(captchaSettings["type"] == Constants.FLASH_CAPTCHA)  && (XVar)(MVCFunctions.strtolower((XVar)(this.captchaValue)) != MVCFunctions.strtolower((XVar)(XSession.Session[MVCFunctions.Concat("captcha_", getCaptchaId())]))))
			{
				this.isCaptchaOk = new XVar(false);
				this.message = new XVar("Invalid security code.");
			}
			if(captchaSettings["type"] == Constants.RE_CAPTCHA)
			{
				dynamic verifyResponse = XVar.Array();
				verifyResponse = XVar.Clone(CommonFunctions.verifyRecaptchaResponse((XVar)(this.captchaValue)));
				if(XVar.Pack(!(XVar)(verifyResponse["success"])))
				{
					this.isCaptchaOk = new XVar(false);
					this.message = XVar.Clone(verifyResponse["message"]);
				}
			}
			if(XVar.Pack(this.isCaptchaOk))
			{
				if(captchaSettings["type"] == Constants.FLASH_CAPTCHA)
				{
					XSession.Session.Remove(MVCFunctions.Concat("captcha_", getCaptchaId()));
				}
				XSession.Session.Remove(MVCFunctions.Concat("isCaptcha", getCaptchaId(), "Showed"));
				XSession.Session["count_passes_captcha"] = 0;
			}
			return this.isCaptchaOk;
		}
		public virtual XVar displayCaptcha()
		{
			dynamic captchaFieldName = null, controls = XVar.Array();
			captchaFieldName = XVar.Clone(getCaptchaFieldName());
			if((XVar)(!(XVar)(XSession.Session.KeyExists("count_passes_captcha")))  || (XVar)(this.captchaPassesCount <= XSession.Session["count_passes_captcha"]))
			{
				this.xt.assign(new XVar("captcha_block"), new XVar(true));
				this.xt.assign(new XVar("captcha"), (XVar)(getCaptchaHtml((XVar)(captchaFieldName))));
				this.xt.assign(new XVar("captcha_field_name"), (XVar)(captchaFieldName));
				if(XVar.Pack(XSession.Session.KeyExists("count_passes_captcha")))
				{
					XSession.Session.Remove("count_passes_captcha");
				}
				XSession.Session[MVCFunctions.Concat("isCaptcha", getCaptchaId(), "Showed")] = 1;
			}
			controls = XVar.Clone(new XVar("controls", XVar.Array()));
			controls.InitAndSetArrayItem(0, "controls", "ctrlInd");
			controls.InitAndSetArrayItem(this.id, "controls", "id");
			controls.InitAndSetArrayItem(captchaFieldName, "controls", "fieldName");
			controls.InitAndSetArrayItem(this.pageType, "controls", "mode");
			if(XVar.Pack(!(XVar)(this.isCaptchaOk)))
			{
				controls.InitAndSetArrayItem(true, "controls", "isInvalid");
			}
			fillControlsMap((XVar)(controls));
			addExtraFieldsToFieldSettings(new XVar(true));
			return null;
		}
		public virtual XVar getCaptchaHtml(dynamic _param__captchaFieldName)
		{
			#region pass-by-value parameters
			dynamic _captchaFieldName = XVar.Clone(_param__captchaFieldName);
			#endregion

			dynamic captchaHTML = null, path = null, swfPath = null, typeCodeMessage = null;
			captchaHTML = new XVar("<div class=\"captcha_block\">");
			typeCodeMessage = new XVar("Type the code you see above");
			path = XVar.Clone(MVCFunctions.GetCaptchaPath());
			swfPath = XVar.Clone(MVCFunctions.GetCaptchaSwfPath());
			captchaHTML = MVCFunctions.Concat(captchaHTML, "\r\n\t\t\t<div style=\"height:65px;\">\r\n\t\t\t<object width=\"210\" height=\"65\" data=\"", swfPath, "?path=", path, "?id=", getCaptchaId(), "\" type=\"application/x-shockwave-flash\">\r\n\t\t\t\t<param value=\"", swfPath, "?path=", path, "?id=", getCaptchaId(), "\" name=\"movie\"/>\r\n\t\t\t\t<param value=\"opaque\" name=\"wmode\"/>\r\n\t\t\t\t<a href=\"http://www.macromedia.com/go/getflashplayer\"><img alt=\"Download Flash\" src=\"\"/></a>\r\n\t\t\t</object>\r\n\t\t\t</div>");
			captchaHTML = MVCFunctions.Concat(captchaHTML, "<div style=\"white-space: nowrap;\">", typeCodeMessage, ":</div>\r\n\t\t\t<span id=\"edit", this.id, "_", _captchaFieldName, "_0\">\r\n\t\t\t\t<input type=\"text\" value=\"\" class=\"captcha_value\" name=\"value_", _captchaFieldName, "_", this.id, "\" style=\"\" id=\"value_", _captchaFieldName, "_", this.id, "\"/>\r\n\t\t\t\t<font color=\"red\">*</font>\r\n\t\t\t</span>");
			captchaHTML = MVCFunctions.Concat(captchaHTML, "</div>");
			return captchaHTML;
		}
		public virtual XVar getCaptchaId()
		{
			return this.id;
		}
		public virtual XVar getCaptchaFieldName()
		{
			return "captcha";
		}
		public virtual XVar createPerPage()
		{
			dynamic allMessage = null, classString = null, i = null, rpp = null;
			if((XVar)(false)  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				return bsCreatePerPage();
			}
			classString = new XVar("");
			allMessage = new XVar("Show all");
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				classString = new XVar("class=\"form-control\"");
				allMessage = new XVar("All");
			}
			rpp = XVar.Clone(MVCFunctions.Concat("<select ", classString, " id=\"recordspp", this.id, "\">"));
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrRecsPerPage); i++)
			{
				if(this.arrRecsPerPage[i] != -1)
				{
					rpp = MVCFunctions.Concat(rpp, "<option value=\"", this.arrRecsPerPage[i], "\" ", (XVar.Pack(this.pageSize == this.arrRecsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", this.arrRecsPerPage[i], "</option>");
				}
				else
				{
					rpp = MVCFunctions.Concat(rpp, "<option value=\"-1\" ", (XVar.Pack(this.pageSize == this.arrRecsPerPage[i]) ? XVar.Pack("selected") : XVar.Pack("")), ">", allMessage, "</option>");
				}
			}
			rpp = MVCFunctions.Concat(rpp, "</select>");
			this.xt.assign(new XVar("recsPerPage"), (XVar)(rpp));
			return null;
		}
		public virtual XVar bsCreatePerPage()
		{
			dynamic i = null, rpp = null, selectedAttr = null, txtVal = null, val = null;
			txtVal = XVar.Clone(this.pageSize);
			if(this.pageSize == -1)
			{
				txtVal = new XVar("Show all");
			}
			rpp = XVar.Clone(MVCFunctions.Concat("<div class=\"dropdown btn-group\">\r\n\t\t\t<button class=\"btn btn-default dropdown-toggle\" type=\"button\" data-toggle=\"dropdown\"><span class=\"dropdown-text\">", txtVal, "</span> <span class=\"caret\"></span></button>\r\n\t\t\t<ul class=\"dropdown-menu pull-right\" role=\"menu\">"));
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.arrRecsPerPage); i++)
			{
				val = XVar.Clone(this.arrRecsPerPage[i]);
				txtVal = XVar.Clone(val);
				if(val == -1)
				{
					txtVal = new XVar("Show all");
				}
				selectedAttr = new XVar("");
				if(this.pageSize == val)
				{
					selectedAttr = new XVar("aria-selected=\"true\" class=\"active\"");
				}
				else
				{
					selectedAttr = new XVar("aria-selected=\"false\"");
				}
				rpp = MVCFunctions.Concat(rpp, "<li ", selectedAttr, "><a data-action=\"", val, "\" class=\"dropdown-item dropdown-item-button\">", txtVal, "</a></li>");
			}
			rpp = MVCFunctions.Concat(rpp, "</ul></div>");
			this.xt.assign(new XVar("recsPerPage"), (XVar)(rpp));
			return null;
		}
		public virtual XVar ProcessFiles()
		{
			foreach (KeyValuePair<XVar, dynamic> f in this.filesToDelete.GetEnumerator())
			{
				f.Value.Delete();
			}
			foreach (KeyValuePair<XVar, dynamic> f in this.filesToMove.GetEnumerator())
			{
				f.Value.Move();
			}
			foreach (KeyValuePair<XVar, dynamic> f in this.filesToSave.GetEnumerator())
			{
				f.Value.Save();
			}
			return null;
		}
		public virtual XVar countDetailsRecsNoSubQ(dynamic _param_dInd, dynamic detailid)
		{
			#region pass-by-value parameters
			dynamic dInd = XVar.Clone(_param_dInd);
			#endregion

			dynamic dDataSourceTable = null, detCipherer = null, detConnection = null, detPSet = null, detailKeys = XVar.Array(), detailsQuery = null, sql = null, whereClauses = XVar.Array();
			dDataSourceTable = XVar.Clone(this.allDetailsTablesArr[dInd]["dDataSourceTable"]);
			detPSet = XVar.Clone(this.pSet.getTable((XVar)(dDataSourceTable)));
			detCipherer = XVar.Clone(new RunnerCipherer((XVar)(dDataSourceTable), (XVar)(detPSet)));
			detConnection = XVar.Clone(GlobalVars.cman.byTable((XVar)(dDataSourceTable)));
			detailsQuery = XVar.Clone(detPSet.getSQLQuery());
			sql = XVar.Clone(detailsQuery.getSqlComponents());
			whereClauses = XVar.Clone(XVar.Array());
			whereClauses.InitAndSetArrayItem(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(dDataSourceTable)), null);
			detailKeys = XVar.Clone(detPSet.getDetailKeysByMasterTable((XVar)(this.tName)));
			foreach (KeyValuePair<XVar, dynamic> val in this.masterKeysByD[dInd].GetEnumerator())
			{
				dynamic mastervalue = null;
				mastervalue = XVar.Clone(detCipherer.MakeDBValue((XVar)(detailKeys[val.Key]), (XVar)(detailid[val.Key]), new XVar(""), new XVar(true)));
				if(mastervalue == "null")
				{
					whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(_getFieldSQL((XVar)(detailKeys[val.Key]), (XVar)(detConnection), (XVar)(detPSet)), " is NULL "), null);
				}
				else
				{
					whereClauses.InitAndSetArrayItem(MVCFunctions.Concat(_getFieldSQLDecrypt((XVar)(detailKeys[val.Key]), (XVar)(detConnection), (XVar)(detPSet), (XVar)(detCipherer)), "=", mastervalue), null);
				}
			}
			return limitRowCount((XVar)(detConnection.getFetchedRowsNumber((XVar)(SQLQuery.buildSQL((XVar)(detailsQuery.getSqlComponents()), (XVar)(whereClauses))))), (XVar)(detPSet));
		}
		public virtual XVar noRecordsMessage()
		{
			dynamic isSearchRun = null;
			isSearchRun = XVar.Clone(isSearchFunctionalityActivated());
			if((XVar)(!(XVar)(isSearchRun))  && (XVar)(getCurrentTabWhere() != ""))
			{
				isSearchRun = new XVar(true);
			}
			if((XVar)(this.pSetSearch.noRecordsOnFirstPage())  && (XVar)(!(XVar)(isSearchRun)))
			{
				return "Nothing to see. Run some search.";
			}
			if((XVar)(!(XVar)(this.rowsFound))  && (XVar)(!(XVar)(isSearchRun)))
			{
				return "No data yet.";
			}
			if((XVar)(isSearchRun)  && (XVar)(!(XVar)(this.rowsFound)))
			{
				return "No results found.";
			}
			return null;
		}
		public virtual XVar showNoRecordsMessage()
		{
			dynamic message = null;
			message = XVar.Clone(MVCFunctions.Concat((XVar.Pack(this.is508 == true) ? XVar.Pack("<a name=\"skipdata\"></a>") : XVar.Pack("")), noRecordsMessage()));
			message = XVar.Clone(MVCFunctions.Concat("<span name=\"notfound_message", this.id, "\">", message, "</span>"));
			this.xt.assign(new XVar("message"), (XVar)(message));
			this.xt.assign(new XVar("message_class"), new XVar("alert-warning"));
			this.xt.assign(new XVar("message_block"), new XVar(true));
			return null;
		}
		public virtual XVar buildPagination()
		{
			dynamic activeClass = null, advSeparator = null, nonActiveClass = null, separator = null;
			activeClass = new XVar("");
			nonActiveClass = new XVar("pag_n");
			separator = new XVar("&nbsp;");
			advSeparator = new XVar("&nbsp;:&nbsp;");
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				activeClass = new XVar("active");
				nonActiveClass = new XVar("");
				separator = new XVar("");
				advSeparator = new XVar("");
			}
			if((XVar)(this.pageSize)  && (XVar)(this.pageSize != -1))
			{
				this.maxPages = XVar.Clone((XVar)Math.Ceiling((double)(this.numRowsFromSQL / this.pageSize)));
			}
			if(this.maxPages < this.myPage)
			{
				this.myPage = XVar.Clone(this.maxPages);
			}
			if(this.myPage < 1)
			{
				this.myPage = new XVar(1);
			}
			this.recordsOnPage = XVar.Clone(this.numRowsFromSQL - (this.myPage - 1) * this.pageSize);
			if((XVar)(this.pageSize < this.recordsOnPage)  && (XVar)(this.pageSize != -1))
			{
				this.recordsOnPage = XVar.Clone(this.pageSize);
			}
			this.colsOnPage = XVar.Clone(this.recsPerRowList);
			if((XVar)(this.recordsOnPage < this.colsOnPage)  && (XVar)(this.listGridLayout != Constants.gltVERTICAL))
			{
				this.colsOnPage = XVar.Clone(this.recordsOnPage);
			}
			if(this.colsOnPage < 1)
			{
				this.colsOnPage = new XVar(1);
			}
			if((XVar)(!(XVar)(this.numRowsFromSQL))  && (XVar)(this.deleteMessage == ""))
			{
				this.rowsFound = new XVar(false);
				showNoRecordsMessage();
				if((XVar)(this.listAjax)  || (XVar)(this.mode == Constants.LIST_LOOKUP))
				{
					this.xt.assign(new XVar("pagination_block"), new XVar(true));
					this.xt.displayBrickHidden(new XVar("pagination"));
				}
			}
			else
			{
				dynamic firstDisplayed = null, lastDisplayed = null, limit = null;
				this.rowsFound = new XVar(true);
				this.xt.assign(new XVar("details_found"), new XVar(true));
				this.xt.assign(new XVar("message_block"), new XVar(false));
				if((XVar)(this.listAjax)  || (XVar)(this.mode == Constants.LIST_LOOKUP))
				{
					this.xt.assign(new XVar("message_block"), new XVar(true));
					this.xt.assign(new XVar("message_class"), new XVar("alert-warning"));
					this.xt.displayBrickHidden(new XVar("message"));
				}
				else
				{
					if(this.deleteMessage != "")
					{
						this.xt.assign(new XVar("message_block"), new XVar(true));
					}
				}
				this.xt.assign(new XVar("records_found"), (XVar)(this.numRowsFromSQL));
				this.jsSettings.InitAndSetArrayItem(this.maxPages, "tableSettings", this.tName, "maxPages");
				firstDisplayed = XVar.Clone((this.myPage - 1) * this.pageSize + 1);
				lastDisplayed = XVar.Clone(this.myPage * this.pageSize);
				if((XVar)(this.pageSize < 0)  || (XVar)(this.numRowsFromSQL < lastDisplayed))
				{
					lastDisplayed = XVar.Clone(this.numRowsFromSQL);
				}
				prepareRecordsIndicator((XVar)(firstDisplayed), (XVar)(lastDisplayed), (XVar)(this.numRowsFromSQL));
				this.xt.assign(new XVar("page"), (XVar)(this.myPage));
				this.xt.assign(new XVar("maxpages"), (XVar)(this.maxPages));
				this.xt.assign(new XVar("pagination_block"), new XVar(false));
				limit = new XVar(10);
				if(XVar.Pack(mobileTemplateMode()))
				{
					limit = new XVar(5);
				}
				if(1 < this.maxPages)
				{
					dynamic counter = null, counterend = null, counterstart = null, pageLinks = null, pagination = null;
					this.xt.assign(new XVar("pagination_block"), new XVar(true));
					pagination = new XVar("");
					counterstart = XVar.Clone(this.myPage - (limit - 1));
					if(this.myPage  %  limit != 0)
					{
						counterstart = XVar.Clone((this.myPage - this.myPage  %  limit) + 1);
					}
					counterend = XVar.Clone((counterstart + limit) - 1);
					if(this.maxPages < counterend)
					{
						counterend = XVar.Clone(this.maxPages);
					}
					if(counterstart != 1)
					{
						pagination = MVCFunctions.Concat(pagination, getPaginationLink(new XVar(1), new XVar("First")), advSeparator);
						pagination = MVCFunctions.Concat(pagination, getPaginationLink((XVar)(counterstart - 1), new XVar("Previous")), separator);
					}
					pageLinks = new XVar("");
					if(XVar.Pack(CommonFunctions.isRTL()))
					{
						counter = XVar.Clone(counterend);
						for(;counterstart <= counter; counter--)
						{
							pageLinks = MVCFunctions.Concat(pageLinks, separator, getPaginationLink((XVar)(counter), (XVar)(counter), (XVar)(counter == this.myPage)));
						}
					}
					else
					{
						counter = XVar.Clone(counterstart);
						for(;counter <= counterend; counter++)
						{
							pageLinks = MVCFunctions.Concat(pageLinks, separator, getPaginationLink((XVar)(counter), (XVar)(counter), (XVar)(counter == this.myPage)));
						}
					}
					if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
					{
						pageLinks = XVar.Clone(MVCFunctions.Concat("[", pageLinks, separator, "]"));
					}
					pagination = MVCFunctions.Concat(pagination, pageLinks);
					if(counterend != this.maxPages)
					{
						pagination = MVCFunctions.Concat(pagination, separator, getPaginationLink((XVar)(counterend + 1), new XVar("Next")), advSeparator);
						pagination = MVCFunctions.Concat(pagination, getPaginationLink((XVar)(this.maxPages), new XVar("Last")));
					}
					if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						pagination = XVar.Clone(MVCFunctions.Concat("<nav class=\"text-center\"><ul class=\"pagination\" data-function=\"pagination", this.id, "\">", pagination, "</ul></nav>"));
					}
					else
					{
						pagination = XVar.Clone(MVCFunctions.Concat("<div data-function=\"pagination", this.id, "\">", pagination, "</div>"));
					}
					this.xt.assign(new XVar("pagination"), (XVar)(pagination));
					this.xt.assign(new XVar("pagination"), (XVar)(pagination));
				}
				else
				{
					if((XVar)(this.listAjax)  || (XVar)(this.mode == Constants.LIST_LOOKUP))
					{
						this.xt.assign(new XVar("pagination_block"), new XVar(true));
						this.xt.displayBrickHidden(new XVar("pagination"));
					}
				}
			}
			return null;
		}
		public virtual XVar prepareRecordsIndicator(dynamic _param_firstDisplayed, dynamic _param_lastDisplayed, dynamic _param_totalDisplayed)
		{
			#region pass-by-value parameters
			dynamic firstDisplayed = XVar.Clone(_param_firstDisplayed);
			dynamic lastDisplayed = XVar.Clone(_param_lastDisplayed);
			dynamic totalDisplayed = XVar.Clone(_param_totalDisplayed);
			#endregion

			dynamic template = null;
			this.xt.assign(new XVar("firstrecord"), (XVar)(firstDisplayed));
			this.xt.assign(new XVar("lastrecord"), (XVar)(lastDisplayed));
			template = new XVar("Displaying %first% - %last% of %total%");
			template = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, "%first%", 1, "%last%", 2, "%total%")), (XVar)(new XVar(0, MVCFunctions.Concat("<span class=\"bs-number\">", firstDisplayed, "</span>"), 1, MVCFunctions.Concat("<span class=\"bs-number\">", lastDisplayed, "</span>"), 2, MVCFunctions.Concat("<span class=\"bs-number\">", totalDisplayed, "</span>"))), (XVar)(template)));
			this.xt.assign(new XVar("records_indicator"), (XVar)(template));
			return null;
		}
		public virtual XVar getPaginationLink(dynamic _param_pageNum, dynamic _param_linkText, dynamic _param_active = null)
		{
			#region default values
			if(_param_active as Object == null) _param_active = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic pageNum = XVar.Clone(_param_pageNum);
			dynamic linkText = XVar.Clone(_param_linkText);
			dynamic active = XVar.Clone(_param_active);
			#endregion

			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				dynamic href = null;
				href = XVar.Clone(MVCFunctions.Concat(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(this.tName))), (XVar)(this.pageType)), "?goto=", pageNum));
				return MVCFunctions.Concat("<li class=\"", (XVar.Pack(active) ? XVar.Pack("active") : XVar.Pack("")), "\"><a href=\"", href, "\" pageNum=\"", pageNum, "\" >", linkText, "</a></li>");
			}
			if(XVar.Pack(active))
			{
				return MVCFunctions.Concat("<b>", pageNum, "</b>");
			}
			return MVCFunctions.Concat("<a href=\"#\" pageNum=\"", pageNum, "\" class=\"pag_n\" style=\"TEXT-DECORATION: none;\">", linkText, "</a>");
		}
		public virtual XVar isAdminTable()
		{
			if(XVar.Pack(this.tName))
			{
				return (XVar)((XVar)(XVar.Equals(XVar.Pack(this.tName), XVar.Pack("admin_rights")))  || (XVar)(XVar.Equals(XVar.Pack(this.tName), XVar.Pack("admin_members"))))  || (XVar)(XVar.Equals(XVar.Pack(this.tName), XVar.Pack("admin_users")));
			}
			else
			{
				return false;
			}
			return null;
		}
		public virtual XVar fieldClass(dynamic _param_f)
		{
			#region pass-by-value parameters
			dynamic f = XVar.Clone(_param_f);
			#endregion

			dynamic format = null;
			if(this.pSet.getEditFormat((XVar)(f)) == Constants.FORMAT_LOOKUP_WIZARD)
			{
				return "";
			}
			format = XVar.Clone(this.pSet.getViewFormat((XVar)(f)));
			if(format == Constants.FORMAT_FILE)
			{
				return " rnr-field-file";
			}
			if(format == Constants.FORMAT_AUDIO)
			{
				return " rnr-field-audio";
			}
			if(format == Constants.FORMAT_CHECKBOX)
			{
				return " rnr-field-checkbox";
			}
			if((XVar)(format == Constants.FORMAT_NUMBER)  || (XVar)(CommonFunctions.IsNumberType((XVar)(this.pSet.getFieldType((XVar)(f))))))
			{
				return " rnr-field-number";
			}
			return "rnr-field-text";
		}
		public virtual XVar buildDetailGridLinks(dynamic data)
		{
			dynamic hrefs = XVar.Array();
			hrefs = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> detailsData in this.allDetailsTablesArr.GetEnumerator())
			{
				dynamic dShortTable = null, idx = null, masterquery = null;
				dShortTable = XVar.Clone(detailsData.Value["dShortTable"]);
				masterquery = XVar.Clone(MVCFunctions.Concat("mastertable=", MVCFunctions.RawUrlEncode((XVar)(this.tName))));
				idx = new XVar(1);
				for(;idx <= MVCFunctions.count(detailsData.Value["masterKeys"]); idx++)
				{
					masterquery = MVCFunctions.Concat(masterquery, "&masterkey", idx, "=", MVCFunctions.RawUrlEncode((XVar)(data[detailsData.Value["dDataSourceTable"]][MVCFunctions.Concat("masterkey", idx)])));
				}
				hrefs.InitAndSetArrayItem(new XVar("id", (XVar.Pack(this.pSet.getDPType((XVar)(detailsData.Value["dDataSourceTable"])) == Constants.DP_INLINE) ? XVar.Pack(MVCFunctions.Concat(dShortTable, "_preview")) : XVar.Pack(MVCFunctions.Concat("master_", dShortTable, "_"))), "href", MVCFunctions.GetTableLink((XVar)(dShortTable), (XVar)(detailsData.Value["dType"]), (XVar)(masterquery))), null);
			}
			return hrefs;
		}
		public virtual EditControl getControl(dynamic _param_field, dynamic _param_id = null, dynamic _param_extraParams = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar("");
			if(_param_extraParams as Object == null) _param_extraParams = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic id = XVar.Clone(_param_id);
			dynamic extraParams = XVar.Clone(_param_extraParams);
			#endregion

			return XVar.UnPackEditControl(this.controls.getControl((XVar)(field), (XVar)(id), (XVar)(extraParams)) ?? new XVar());
		}
		public virtual ViewControl getViewControl(dynamic _param_field, dynamic _param_format = null)
		{
			#region default values
			if(_param_format as Object == null) _param_format = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic format = XVar.Clone(_param_format);
			#endregion

			return XVar.UnPackViewControl(this.viewControls.getControl((XVar)(field), (XVar)(format)) ?? new XVar());
		}
		public virtual XVar setForExportVar(dynamic _param_forExport)
		{
			#region pass-by-value parameters
			dynamic forExport = XVar.Clone(_param_forExport);
			#endregion

			this.viewControls.setForExportVar((XVar)(forExport));
			return null;
		}
		public virtual XVar showDBValue(dynamic _param_field, dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return getViewControl((XVar)(field)).showDBValue((XVar)(data), (XVar)(keylink));
		}
		public virtual XVar getExportValue(dynamic _param_field, dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			return getViewControl((XVar)(field)).getExportValue((XVar)(data), (XVar)(keylink));
		}
		public virtual XVar hideField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			if(XVar.Pack(!(XVar)(this.xt == null)))
			{
				this.xt.hideField((XVar)(fieldName));
			}
			return null;
		}
		public virtual XVar showField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			if(XVar.Pack(!(XVar)(this.xt == null)))
			{
				this.xt.showField((XVar)(fieldName));
			}
			return null;
		}
		public virtual XVar getDetailKeysByMasterTable()
		{
			return this.pSet.getDetailKeysByMasterTable((XVar)(this.masterTable));
		}
		public virtual dynamic getPageLayout(dynamic _param_tName = null, dynamic _param_pageType = null, dynamic _param_suffix = null)
		{
			#region default values
			if(_param_tName as Object == null) _param_tName = new XVar("");
			if(_param_pageType as Object == null) _param_pageType = new XVar("");
			if(_param_suffix as Object == null) _param_suffix = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic pageType = XVar.Clone(_param_pageType);
			dynamic suffix = XVar.Clone(_param_suffix);
			#endregion

			dynamic templateName = null;
			if(XVar.Pack(!(XVar)(tName)))
			{
				tName = XVar.Clone(this.tName);
			}
			if(XVar.Pack(!(XVar)(pageType)))
			{
				pageType = XVar.Clone(this.pageType);
			}
			templateName = XVar.Clone(MVCFunctions.Concat(CommonFunctions.GetTableURL((XVar)(tName)), "_", pageType));
			if(XVar.Pack(suffix))
			{
				templateName = XVar.Clone(MVCFunctions.Concat(templateName, "_", suffix));
			}
			if((XVar)(!(XVar)(isPageTableBased()))  || (XVar)(this.pageType == Constants.PAGE_REGISTER))
			{
				templateName = XVar.Clone(pageType);
			}
			return GlobalVars.page_layouts[templateName];
		}
		public virtual XVar isPageTableBased()
		{
			if((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_MENU)  || (XVar)(this.pageType == Constants.PAGE_LOGIN))  || (XVar)(this.pageType == Constants.PAGE_REMIND))  || (XVar)(this.pageType == Constants.PAGE_CHANGEPASS))
			{
				return false;
			}
			return true;
		}
		public virtual XVar isBrickSet(dynamic _param_brickName)
		{
			#region pass-by-value parameters
			dynamic brickName = XVar.Clone(_param_brickName);
			#endregion

			dynamic layout = null;
			layout = XVar.Clone(getPageLayout());
			if(XVar.Pack(layout))
			{
				return layout.isBrickSet((XVar)(brickName));
			}
			return false;
		}
		public virtual XVar getBrickTableName(dynamic _param_brickName)
		{
			#region pass-by-value parameters
			dynamic brickName = XVar.Clone(_param_brickName);
			#endregion

			dynamic layout = null;
			layout = XVar.Clone(getPageLayout());
			if(XVar.Pack(layout))
			{
				return layout.getBrickTableName((XVar)(brickName));
			}
			return "";
		}
		public virtual XVar setParamsForSearchPanel()
		{
			dynamic seachTableName = null;
			if(XVar.Pack(!(XVar)(this.searchPanelActivated)))
			{
				return null;
			}
			this.needSearchClauseObj = new XVar(true);
			seachTableName = XVar.Clone(getBrickTableName(new XVar("searchpanel")));
			if(XVar.Pack(seachTableName))
			{
				this.pSetSearch = XVar.Clone(new ProjectSettings((XVar)(seachTableName), new XVar(Constants.PAGE_SEARCH)));
				this.searchTableName = XVar.Clone(seachTableName);
				this.settingsMap.InitAndSetArrayItem(this.pSetSearch.getShortTableName(), "globalSettings", "shortTNames", seachTableName);
				this.permis.InitAndSetArrayItem(getPermissions((XVar)(seachTableName)), this.searchTableName);
				if((XVar)(this.permis[this.searchTableName]["search"])  && (XVar)((XVar)(!(XVar)(isPageTableBased()))  || (XVar)(this.pageType == Constants.PAGE_REGISTER)))
				{
					this.tableBasedSearchPanelAdded = new XVar(true);
				}
			}
			return null;
		}
		protected virtual XVar checkIfSearchPanelActivated(dynamic _param_mobile)
		{
			#region pass-by-value parameters
			dynamic mobile = XVar.Clone(_param_mobile);
			#endregion

			if((XVar)(this.pageType == Constants.PAGE_CHART)  && (XVar)(this.mode == Constants.CHART_SIMPLE))
			{
				return true;
			}
			if((XVar)(mobile)  && (XVar)(this.pageType == Constants.PAGE_LIST))
			{
				return isBrickSet(new XVar("searchpanel_mobile"));
			}
			if((XVar)(mobile)  && (XVar)(this.pageType == Constants.PAGE_DASHBOARD))
			{
				return isBrickSet(new XVar("search_dashboard_m"));
			}
			if(this.pageType == Constants.PAGE_DASHBOARD)
			{
				return isBrickSet(new XVar("search_dashboard"));
			}
			return isBrickSet(new XVar("searchpanel"));
		}
		protected virtual XVar buildAddedSearchPanel()
		{
			if((XVar)((XVar)((XVar)((XVar)(this.pageType != Constants.PAGE_REPORT)  && (XVar)(this.pageType != Constants.PAGE_CHART))  && (XVar)(this.pageType != Constants.PAGE_LIST))  && (XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  && (XVar)(this.mode == Constants.ADD_INLINE))))  && (XVar)(!(XVar)((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(this.mode == Constants.EDIT_INLINE))))
			{
				buildSearchPanel();
			}
			return null;
		}
		public virtual XVar buildSearchPanel()
		{
			dynamic searchPanelObj = null, var_params = XVar.Array();
			if((XVar)(!(XVar)(this.searchPanelActivated))  || (XVar)(!(XVar)(this.permis[this.searchTableName]["search"])))
			{
				return null;
			}
			var_params = XVar.Clone(XVar.Array());
			var_params.InitAndSetArrayItem(this, "pageObj");
			searchPanelObj = XVar.Clone(new SearchPanelSimple((XVar)(var_params)));
			searchPanelObj.buildSearchPanel();
			return null;
		}
		public virtual XVar buildFilterPanel()
		{
			dynamic filterPanel = null, var_params = XVar.Array();
			if((XVar)(!(XVar)(this.permis[this.tName]["search"]))  || (XVar)((XVar)(this.pSetEdit.isSearchRequiredForFiltering())  && (XVar)(!(XVar)(isRequiredSearchRunning()))))
			{
				prepareEmptyFPMarkup();
				return null;
			}
			var_params = XVar.Clone(XVar.Array());
			var_params.InitAndSetArrayItem(this, "pageObj");
			filterPanel = XVar.Clone(new FilterPanel((XVar)(var_params)));
			filterPanel.buildFilterPanel();
			return null;
		}
		protected virtual XVar prepareEmptyFPMarkup()
		{
			return null;
		}
		public virtual XVar isSearchFunctionalityActivated()
		{
			if(XVar.Pack(!(XVar)(this.searchClauseObj)))
			{
				return false;
			}
			return this.searchClauseObj.isSearchFunctionalityActivated();
		}
		public virtual XVar isRequiredSearchRunning()
		{
			if(XVar.Pack(!(XVar)(this.searchClauseObj)))
			{
				return false;
			}
			return this.searchClauseObj.isRequiredSearchRunning();
		}
		public virtual XVar getFiltersWhere()
		{
			dynamic whereClause = null, whereComponents = XVar.Array();
			whereClause = new XVar("");
			whereComponents = XVar.Clone(getWhereComponents());
			foreach (KeyValuePair<XVar, dynamic> fWhere in whereComponents["filterWhere"].GetEnumerator())
			{
				whereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(whereClause), (XVar)(fWhere.Value)));
			}
			return whereClause;
		}
		public virtual XVar getFiltersHaving()
		{
			dynamic havingClause = null, whereClause = null, whereComponents = XVar.Array();
			havingClause = new XVar("");
			whereComponents = XVar.Clone(getWhereComponents());
			foreach (KeyValuePair<XVar, dynamic> fHaving in whereComponents["filterHaving"].GetEnumerator())
			{
				whereClause = XVar.Clone(CommonFunctions.whereAdd((XVar)(havingClause), (XVar)(fHaving.Value)));
			}
			return whereClause;
		}
		public virtual XVar isOldLayout()
		{
			if(XVar.Pack(!(XVar)(this.pageLayout)))
			{
				return false;
			}
			return this.pageLayout.version == 1;
		}
		public virtual XVar makeClassName(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			if(XVar.Pack(isOldLayout()))
			{
				return MVCFunctions.Concat("runner-", name);
			}
			return MVCFunctions.Concat("rnr-", name);
		}
		public virtual XVar hasDeniedDuplicateValues(dynamic _param_fieldsData, ref dynamic message)
		{
			#region pass-by-value parameters
			dynamic fieldsData = XVar.Clone(_param_fieldsData);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in fieldsData.GetEnumerator())
			{
				if(XVar.Pack(this.pSet.allowDuplicateValues((XVar)(value.Key))))
				{
					continue;
				}
				if(XVar.Pack(hasDuplicateValue((XVar)(value.Key), (XVar)(value.Value))))
				{
					this.errorFields.InitAndSetArrayItem(value.Key, null);
					if((XVar)(this.mode != Constants.EDIT_POPUP)  && (XVar)(this.mode != Constants.ADD_POPUP))
					{
						message = XVar.Clone(getDenyDuplicatedMessage((XVar)(value.Key), (XVar)(value.Value)));
					}
					return true;
				}
			}
			return false;
		}
		protected virtual XVar getDenyDuplicatedMessage(dynamic _param_fName, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic message = null, messageData = XVar.Array(), validationData = XVar.Array();
			validationData = XVar.Clone(this.pSet.getValidation((XVar)(fName)));
			messageData = XVar.Clone(validationData["customMessages"]["DenyDuplicated"]);
			if(messageData["messageType"] == "Text")
			{
				message = XVar.Clone(messageData["message"]);
			}
			else
			{
				message = XVar.Clone(CommonFunctions.GetCustomLabel((XVar)(messageData["message"])));
			}
			return MVCFunctions.Concat(this.pSet.label((XVar)(fName)), ": ", MVCFunctions.str_replace(new XVar("%value%"), (XVar)(MVCFunctions.substr((XVar)(value), new XVar(0), new XVar(10))), (XVar)(message)));
		}
		public virtual XVar hasDuplicateValue(dynamic _param_fieldName, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic data = XVar.Array(), sql = null, where = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(value)))))
			{
				return false;
			}
			if(XVar.Pack(this.cipherer.isFieldEncrypted((XVar)(fieldName))))
			{
				value = XVar.Clone(this.cipherer.MakeDBValue((XVar)(fieldName), (XVar)(value), new XVar(""), new XVar(true)));
			}
			else
			{
				value = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(fieldName), (XVar)(value)));
			}
			where = XVar.Clone(MVCFunctions.Concat(getFieldSQLDecrypt((XVar)(fieldName)), "=", value));
			sql = XVar.Clone(MVCFunctions.Concat("SELECT count(*) from ", this.connection.addTableWrappers((XVar)(this.pSet.getOriginalTableName())), " where ", where));
			data = XVar.Clone(this.connection.query((XVar)(sql)).fetchNumeric());
			if(XVar.Pack(!(XVar)(data[0])))
			{
				return false;
			}
			return true;
		}
		public virtual XVar fetchBlocksList(dynamic _param_blocks, dynamic _param_dash = null)
		{
			#region default values
			if(_param_dash as Object == null) _param_dash = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic blocks = XVar.Clone(_param_blocks);
			dynamic dash = XVar.Clone(_param_dash);
			#endregion

			dynamic brickCount = null, fetchedBlocks = null, firstRightAligned = null, hasRightAligned = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(blocks)))))
			{
				return this.xt.fetch_loaded((XVar)(blocks));
			}
			fetchedBlocks = new XVar("");
			firstRightAligned = new XVar(true);
			hasRightAligned = new XVar(false);
			brickCount = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> b in blocks.GetEnumerator())
			{
				dynamic align = null, fetched = null, name = null;
				++(brickCount);
				align = new XVar("");
				if(XVar.Pack(MVCFunctions.is_array((XVar)(b.Value))))
				{
					name = XVar.Clone(b.Value["name"]);
					align = XVar.Clone(b.Value["align"]);
				}
				else
				{
					name = XVar.Clone(b.Value);
				}
				fetched = XVar.Clone(this.xt.fetch_loaded((XVar)(name)));
				if(XVar.Pack(!(XVar)(fetched)))
				{
					continue;
				}
				if(XVar.Pack(dash))
				{
					dynamic alignClass = null;
					alignClass = new XVar("");
					if(align == "right")
					{
						alignClass = new XVar("rnr-dberight");
					}
					fetched = XVar.Clone(MVCFunctions.Concat("<span class=\"rnr-dbebrick ", alignClass, "\">", fetched, "</span>"));
					if((XVar)(align == "right")  && (XVar)(firstRightAligned))
					{
						fetched = XVar.Clone(MVCFunctions.Concat("<div class=\"rnr-dbefiller\"></div>", fetched));
						firstRightAligned = new XVar(false);
						hasRightAligned = new XVar(true);
					}
				}
				fetchedBlocks = MVCFunctions.Concat(fetchedBlocks, fetched);
			}
			if((XVar)((XVar)((XVar)(dash)  && (XVar)(fetchedBlocks != XVar.Pack("")))  && (XVar)(1 < brickCount))  && (XVar)(!(XVar)(hasRightAligned)))
			{
				fetchedBlocks = MVCFunctions.Concat(fetchedBlocks, "<div class=\"rnr-dbefiller\"></div>");
			}
			return fetchedBlocks;
		}
		protected virtual XVar needPopupSettings()
		{
			return true;
		}
		public virtual XVar displayAJAX(dynamic _param_templatefile, dynamic _param_id)
		{
			#region pass-by-value parameters
			dynamic templatefile = XVar.Clone(_param_templatefile);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic extraParams = XVar.Array(), returnJSON = XVar.Array();
			if(XVar.Pack(gridTabsAvailable()))
			{
				this.pageData.InitAndSetArrayItem(getTabsHtml(), "tabs");
				this.pageData.InitAndSetArrayItem(getCurrentTabId(), "tabId");
			}
			returnJSON = XVar.Clone(XVar.Array());
			returnJSON.InitAndSetArrayItem(true, "success");
			returnJSON.InitAndSetArrayItem(GlobalVars.pagesData, "pagesData");
			if(XVar.Pack(MVCFunctions.count(this.controlsHTMLMap)))
			{
				returnJSON.InitAndSetArrayItem(this.controlsHTMLMap, "controlsMap");
			}
			if(XVar.Pack(MVCFunctions.count(this.viewControlsHTMLMap)))
			{
				returnJSON.InitAndSetArrayItem(this.viewControlsHTMLMap, "viewControlsMap");
			}
			if(XVar.Pack(MVCFunctions.count(this.includes_css)))
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.array_unique((XVar)(this.includes_css)), "CSSFiles");
			}
			returnJSON.InitAndSetArrayItem(grabAllJsFiles(), "additionalJS");
			returnJSON.InitAndSetArrayItem(id, "idStartFrom");
			if(XVar.Pack(this.formBricks["header"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["header"])), "headerCont");
			}
			if(XVar.Pack(this.formBricks["footer"]))
			{
				returnJSON.InitAndSetArrayItem(fetchBlocksList((XVar)(this.formBricks["footer"])), "footerCont");
			}
			if(this.pageType == Constants.PAGE_CHART)
			{
				returnJSON.InitAndSetArrayItem(MVCFunctions.Concat("<span class=\"rnr-dbebrick\">", getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</span>"), "headerCont");
			}
			assignFormFooterAndHeaderBricks(new XVar(false));
			this.xt.load_template((XVar)(templatefile));
			returnJSON.InitAndSetArrayItem(this.xt.fetch_loaded(new XVar("body")), "html");
			if(XVar.Pack(needPopupSettings()))
			{
				returnJSON.InitAndSetArrayItem(this.jsSettings, "settings");
			}
			extraParams = XVar.Clone(getExtraAjaxPageParams());
			if(XVar.Pack(MVCFunctions.count(extraParams)))
			{
				foreach (KeyValuePair<XVar, dynamic> paramValue in extraParams.GetEnumerator())
				{
					returnJSON.InitAndSetArrayItem(paramValue.Value, paramValue.Key);
				}
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(returnJSON)));
			return null;
		}
		protected virtual XVar getExtraAjaxPageParams()
		{
			return XVar.Array();
		}
		public virtual XVar assignFormFooterAndHeaderBricks(dynamic _param_assignValue = null)
		{
			#region default values
			if(_param_assignValue as Object == null) _param_assignValue = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic assignValue = XVar.Clone(_param_assignValue);
			#endregion

			dynamic name = null;
			if(XVar.Pack(this.formBricks["header"]))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(this.formBricks["header"])))))
				{
					this.formBricks.InitAndSetArrayItem(new XVar(0, this.formBricks["header"]), "header");
				}
				foreach (KeyValuePair<XVar, dynamic> b in this.formBricks["header"].GetEnumerator())
				{
					name = XVar.Clone(b.Value);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(b.Value))))
					{
						name = XVar.Clone(b.Value["name"]);
					}
					this.xt.assign((XVar)(name), (XVar)(assignValue));
				}
			}
			if(XVar.Pack(this.formBricks["footer"]))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(this.formBricks["footer"])))))
				{
					this.formBricks.InitAndSetArrayItem(new XVar(0, this.formBricks["footer"]), "footer");
				}
				foreach (KeyValuePair<XVar, dynamic> b in this.formBricks["footer"].GetEnumerator())
				{
					name = XVar.Clone(b.Value);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(b.Value))))
					{
						name = XVar.Clone(b.Value["name"]);
					}
					this.xt.assign((XVar)(name), (XVar)(assignValue));
				}
			}
			return null;
		}
		public virtual XVar assignStyleFiles(dynamic _param_isPdfPage = null)
		{
			#region default values
			if(_param_isPdfPage as Object == null) _param_isPdfPage = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic isPdfPage = XVar.Clone(_param_isPdfPage);
			#endregion

			this.xt.assign_array(new XVar("styleCSSFiles"), new XVar("stylepath"), (XVar)(MVCFunctions.array_unique((XVar)(this.includes_css))));
			this.includes_css = XVar.Clone(XVar.Array());
			return null;
		}
		public virtual XVar display(dynamic _param_templatefile)
		{
			#region pass-by-value parameters
			dynamic templatefile = XVar.Clone(_param_templatefile);
			#endregion

			assignStyleFiles();
			this.xt.display((XVar)(templatefile));
			return null;
		}
		public virtual XVar getMasterTableSQLClause(dynamic _param_basedOnProp = null)
		{
			#region default values
			if(_param_basedOnProp as Object == null) _param_basedOnProp = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic basedOnProp = XVar.Clone(_param_basedOnProp);
			#endregion

			dynamic i = null, mKey = null, mValue = null, where = null;
			where = new XVar("");
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this.detailKeysByM))))
			{
				return where;
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.detailKeysByM); i++)
			{
				if(i != XVar.Pack(0))
				{
					where = MVCFunctions.Concat(where, " and ");
				}
				if(XVar.Pack(basedOnProp))
				{
					mKey = XVar.Clone(this.masterKeysReq[i + 1]);
				}
				else
				{
					mKey = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1)]);
				}
				if((XVar)(this.cipherer)  && (XVar)(this.cipherer.isEncryptionByPHPEnabled()))
				{
					mValue = XVar.Clone(this.cipherer.MakeDBValue((XVar)(this.detailKeysByM[i]), (XVar)(mKey)));
				}
				else
				{
					mValue = XVar.Clone(CommonFunctions.make_db_value((XVar)(this.detailKeysByM[i]), (XVar)(mKey), new XVar(""), new XVar(""), (XVar)(this.tName)));
				}
				if(MVCFunctions.strlen((XVar)(mValue)) != 0)
				{
					where = MVCFunctions.Concat(where, getFieldSQLDecrypt((XVar)(this.detailKeysByM[i])), "=", mValue);
				}
				else
				{
					where = MVCFunctions.Concat(where, "1=0");
				}
			}
			return where;
		}
		public virtual XVar getWhereComponents()
		{
			this._cachedWhereComponents = XVar.Clone(sGetWhereComponents((XVar)(this.gQuery), (XVar)(this.pSet), (XVar)(this.searchClauseObj), (XVar)(this.controls), (XVar)(this.connection), (XVar)(getMasterTableSQLClause()), (XVar)(SecuritySQL(new XVar("Search"), (XVar)(this.tName)))));
			return this._cachedWhereComponents;
		}
		public static XVar sGetWhereComponents(dynamic _param_query, dynamic _param_pSet_packed, dynamic _param_searchObj, dynamic _param_controls, dynamic _param_connection, dynamic _param_masterTableSQLClause = null, dynamic _param_secSQL = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_masterTableSQLClause as Object == null) _param_masterTableSQLClause = new XVar("");
			if(_param_secSQL as Object == null) _param_secSQL = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic searchObj = XVar.Clone(_param_searchObj);
			dynamic controls = XVar.Clone(_param_controls);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic masterTableSQLClause = XVar.Clone(_param_masterTableSQLClause);
			dynamic secSQL = XVar.Clone(_param_secSQL);
			#endregion

			dynamic aggregatedFields = XVar.Array(), filters = XVar.Array(), nonaggregatedFields = XVar.Array(), whereComponents = XVar.Array();
			whereComponents = XVar.Clone(XVar.Array());
			whereComponents.InitAndSetArrayItem((XVar.Pack(!XVar.Equals(XVar.Pack(secSQL), XVar.Pack(false))) ? XVar.Pack(secSQL) : XVar.Pack(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(pSet.getTableName())))), "security");
			whereComponents.InitAndSetArrayItem(masterTableSQLClause, "master");
			whereComponents.InitAndSetArrayItem(CommonFunctions.combineSQLCriteria((XVar)(new XVar(0, query.WhereToSql(), 1, masterTableSQLClause, 2, (XVar.Pack(!XVar.Equals(XVar.Pack(secSQL), XVar.Pack(false))) ? XVar.Pack(secSQL) : XVar.Pack(CommonFunctions.SecuritySQL(new XVar("Search"), (XVar)(pSet.getTableName()))))))), "commonWhere");
			whereComponents.InitAndSetArrayItem(CommonFunctions.combineSQLCriteria((XVar)(new XVar(0, query.Having().toSql((XVar)(query))))), "commonHaving");
			nonaggregatedFields = XVar.Clone(pSet.getListOfFieldsByExprType(new XVar(false)));
			aggregatedFields = XVar.Clone(pSet.getListOfFieldsByExprType(new XVar(true)));
			whereComponents.InitAndSetArrayItem(searchObj.getWhere((XVar)(nonaggregatedFields), (XVar)(controls)), "searchWhere");
			whereComponents.InitAndSetArrayItem(searchObj.getWhere((XVar)(aggregatedFields), (XVar)(controls)), "searchHaving");
			whereComponents.InitAndSetArrayItem(searchObj.getCommonJoinFromParts((XVar)(controls)), "joinFromPart");
			whereComponents.InitAndSetArrayItem((XVar)((XVar)(XVar.Equals(XVar.Pack("or"), XVar.Pack(searchObj.getCriteriaCombineType())))  && (XVar)(0 != MVCFunctions.strlen((XVar)(whereComponents["searchHaving"]))))  && (XVar)(0 != MVCFunctions.strlen((XVar)(whereComponents["searchWhere"]))), "searchUnionRequired");
			searchObj.processFiltersWhere((XVar)(connection));
			filters = XVar.Clone(searchObj.filteredFields);
			whereComponents.InitAndSetArrayItem(XVar.Array(), "filterWhere");
			foreach (KeyValuePair<XVar, dynamic> f in nonaggregatedFields.GetEnumerator())
			{
				if(XVar.Pack(filters.KeyExists(f.Value)))
				{
					whereComponents.InitAndSetArrayItem(filters[f.Value]["where"], "filterWhere", f.Value);
				}
			}
			whereComponents.InitAndSetArrayItem(XVar.Array(), "filterHaving");
			foreach (KeyValuePair<XVar, dynamic> f in aggregatedFields.GetEnumerator())
			{
				if(XVar.Pack(filters.KeyExists(f.Value)))
				{
					whereComponents.InitAndSetArrayItem(filters[f.Value]["where"], "filterHaving", f.Value);
				}
			}
			return whereComponents;
		}
		public virtual XVar SecuritySQL(dynamic _param_strAction, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic strAction = XVar.Clone(_param_strAction);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return CommonFunctions.SecuritySQL((XVar)(strAction), (XVar)(table));
		}
		public virtual XVar showPageDp(dynamic _param_params = null)
		{
			#region default values
			if(_param_params as Object == null) _param_params = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic bricksExcept = XVar.Array(), contents = null, layout = null, pageSkinStyle = null;
			layout = GlobalVars.page_layouts[MVCFunctions.Concat(this.shortTableName, "_", this.pageType)];
			pageSkinStyle = XVar.Clone(MVCFunctions.Concat(layout.style, " page-", layout.name));
			if(this.pageType == Constants.PAGE_CHART)
			{
				bricksExcept = XVar.Clone(new XVar(0, "chart", 1, "message"));
			}
			else
			{
				bricksExcept = XVar.Clone(new XVar(0, "grid", 1, "pagination", 2, "message"));
			}
			bricksExcept.InitAndSetArrayItem("bsgrid_tabs", null);
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("footer"), new XVar(false));
			this.xt.hideAllBricksExcept((XVar)(bricksExcept));
			this.xt.prepare_template((XVar)(this.templatefile));
			contents = XVar.Clone(renderPageBody());
			MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"detailPreview", this.id, "\" class=\"", pageSkinStyle, " rnr-pagewrapper dpStyle\">", contents, "</div>"));
			return null;
		}
		public virtual XVar renderPageBody()
		{
			return this.xt.fetch_loaded(new XVar("body"));
		}
		public virtual XVar proccessDetailGridInfo(dynamic record, dynamic data, dynamic _param_gridRowInd)
		{
			#region pass-by-value parameters
			dynamic gridRowInd = XVar.Clone(_param_gridRowInd);
			#endregion

			dynamic dDataSourceTable = null, dPset = null, dShortTable = null, detAddAvailabel = null, detEditAvailabel = null, detHref = null, detListAvailabel = null, detailTableData = XVar.Array(), detailid = XVar.Array(), hideDPLink = null, i = null, masterquery = null, tabNamesToHide = XVar.Array();
			hideDPLink = new XVar(true);
			tabNamesToHide = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				detailTableData = XVar.Clone(this.allDetailsTablesArr[i]);
				dDataSourceTable = XVar.Clone(detailTableData["dDataSourceTable"]);
				dPset = XVar.Clone(new ProjectSettings((XVar)(dDataSourceTable)));
				detListAvailabel = XVar.Clone((XVar)(dPset.hasListPage())  && (XVar)(this.permis[dDataSourceTable]["search"]));
				detAddAvailabel = XVar.Clone((XVar)(dPset.hasAddPage())  && (XVar)(this.permis[dDataSourceTable]["add"]));
				detEditAvailabel = XVar.Clone((XVar)(dPset.hasEditPage())  && (XVar)(this.permis[dDataSourceTable]["edit"]));
				if((XVar)((XVar)((XVar)(detailTableData["dType"] == Constants.PAGE_LIST)  && (XVar)(!(XVar)(detListAvailabel)))  && (XVar)(!(XVar)(detAddAvailabel)))  && (XVar)(!(XVar)(detEditAvailabel)))
				{
					continue;
				}
				dShortTable = XVar.Clone(detailTableData["dShortTable"]);
				masterquery = XVar.Clone(MVCFunctions.Concat("mastertable=", MVCFunctions.RawUrlEncode((XVar)(this.tName))));
				CommonFunctions.initArray((XVar)(this.controlsMap), new XVar("gridRows"));
				CommonFunctions.initArray((XVar)(this.controlsMap["gridRows"]), (XVar)(gridRowInd));
				CommonFunctions.initArray((XVar)(this.controlsMap["gridRows"][gridRowInd]), new XVar("masterKeys"));
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "gridRows", gridRowInd, "masterKeys", dDataSourceTable);
				detailid = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> m in this.masterKeysByD[i].GetEnumerator())
				{
					dynamic curM = null;
					curM = XVar.Clone(m.Value);
					if(this.pageType == Constants.PAGE_REPORT)
					{
						curM = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(curM)));
						curM = MVCFunctions.Concat(curM, "_dbvalue");
					}
					masterquery = MVCFunctions.Concat(masterquery, "&masterkey", m.Key + 1, "=", MVCFunctions.RawUrlEncode((XVar)(data[curM])));
					detailid.InitAndSetArrayItem(data[curM], null);
					this.controlsMap.InitAndSetArrayItem(data[curM], "gridRows", gridRowInd, "masterKeys", dDataSourceTable, MVCFunctions.Concat("masterkey", m.Key + 1));
				}
				if((XVar)((XVar)(detailTableData["dispChildCount"])  || (XVar)(detailTableData["hideChild"]))  && (XVar)(!(XVar)(isDetailTableSubqueryApplied((XVar)(dDataSourceTable)))))
				{
					data.InitAndSetArrayItem(countDetailsRecsNoSubQ((XVar)(i), (XVar)(detailid)), MVCFunctions.Concat(dDataSourceTable, "_cnt"));
				}
				record.InitAndSetArrayItem((XVar)((XVar)(this.permis[dDataSourceTable]["add"])  || (XVar)(this.permis[dDataSourceTable]["edit"]))  || (XVar)(this.permis[dDataSourceTable]["search"]), MVCFunctions.Concat(dShortTable, "_dtable_link"));
				if(XVar.Pack(detailTableData["dispChildCount"]))
				{
					if(data[MVCFunctions.Concat(dDataSourceTable, "_cnt")] + 0)
					{
						record.InitAndSetArrayItem(true, MVCFunctions.Concat(dShortTable, "_childcount"));
					}
					else
					{
						if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
						{
							if(this.detailsLinksOnList != Constants.DL_INDIVIDUAL)
							{
								record.InitAndSetArrayItem(true, MVCFunctions.Concat(dShortTable, "_childcount"));
							}
							record.InitAndSetArrayItem("hidden-badge", MVCFunctions.Concat(dShortTable, "_dlink_class"));
							record.InitAndSetArrayItem("hidden-detcounter", MVCFunctions.Concat(dShortTable, "_cntspan_class"));
						}
					}
					record.InitAndSetArrayItem(data[MVCFunctions.Concat(dDataSourceTable, "_cnt")], MVCFunctions.Concat(dShortTable, "_childnumber"));
					record.InitAndSetArrayItem(MVCFunctions.Concat(" id='cntDet_", dShortTable, "_", this.recId, "'"), MVCFunctions.Concat(dShortTable, "_childnumber_attr"));
					this.controlsMap.InitAndSetArrayItem(data[MVCFunctions.Concat(dDataSourceTable, "_cnt")], "gridRows", gridRowInd, "childNum");
					record.InitAndSetArrayItem(MVCFunctions.Concat(" href=\"#\" id=\"details_", this.recId, "_", dShortTable, "\" "), MVCFunctions.Concat(dShortTable, "_link_attrs"));
				}
				if(XVar.Pack(detListAvailabel))
				{
					detHref = XVar.Clone(MVCFunctions.GetTableLink((XVar)(dShortTable), (XVar)(detailTableData["dType"]), (XVar)(masterquery)));
				}
				else
				{
					if(XVar.Pack(detAddAvailabel))
					{
						detHref = XVar.Clone(MVCFunctions.GetTableLink((XVar)(dShortTable), new XVar(Constants.PAGE_ADD), (XVar)(masterquery)));
					}
					else
					{
						if(XVar.Pack(detEditAvailabel))
						{
							detHref = XVar.Clone(MVCFunctions.GetTableLink((XVar)(dShortTable), new XVar(Constants.PAGE_EDIT), (XVar)(masterquery)));
						}
					}
				}
				if(this.pSet.getDPType((XVar)(dDataSourceTable)) == Constants.DP_INLINE)
				{
					record.InitAndSetArrayItem(MVCFunctions.Concat("id = \"", dShortTable, "_preview", this.recId, "\"\r\n\t\t\t\t\tcaption = \"", MVCFunctions.runner_htmlspecialchars((XVar)(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(dDataSourceTable)))))), "\"", "href = \"", detHref, "\""), MVCFunctions.Concat(dShortTable, "_dtablelink_attrs"));
				}
				else
				{
					if(this.pSet.getDPType((XVar)(dDataSourceTable)) == Constants.DP_POPUP)
					{
						record.InitAndSetArrayItem(MVCFunctions.Concat("id=\"master_", dShortTable, "_", this.recId, "\" href=\"", detHref, "\""), MVCFunctions.Concat(dShortTable, "_dtablelink_attrs"));
					}
					else
					{
						record.InitAndSetArrayItem(MVCFunctions.Concat("href=\"", detHref, "\""), MVCFunctions.Concat(dShortTable, "_dtablelink_attrs"));
					}
				}
				if(XVar.Pack(detailTableData["hideChild"]))
				{
					if(XVar.Pack(!(XVar)(data[MVCFunctions.Concat(dDataSourceTable, "_cnt")] + 0)))
					{
						record[MVCFunctions.Concat(dShortTable, "_dtablelink_attrs")] = MVCFunctions.Concat(record[MVCFunctions.Concat(dShortTable, "_dtablelink_attrs")], " class=\"", makeClassName(new XVar("hiddenelem")), "\" data-hidden");
						tabNamesToHide.InitAndSetArrayItem(dDataSourceTable, null);
					}
					else
					{
						if((XVar)(detailTableData["previewOnList"])  && (XVar)(hideDPLink))
						{
							hideDPLink = new XVar(false);
						}
					}
				}
				else
				{
					if(XVar.Pack(hideDPLink))
					{
						hideDPLink = new XVar(false);
					}
				}
			}
			if(this.detailsLinksOnList == Constants.DL_SINGLE)
			{
				if((XVar)((XVar)(MVCFunctions.count(this.allDetailsTablesArr) == 1)  && (XVar)(!(XVar)(detListAvailabel)))  && (XVar)((XVar)(detAddAvailabel)  || (XVar)(detEditAvailabel)))
				{
					record.InitAndSetArrayItem(MVCFunctions.Concat(" href=\"", detHref, "\""), "dtables_link_attrs");
				}
				else
				{
					record.InitAndSetArrayItem(MVCFunctions.Concat(" href=\"#\" id=\"details_", this.recId, "\" "), "dtables_link_attrs");
				}
			}
			if(XVar.Pack(hideDPLink))
			{
				record["dtables_link_attrs"] = MVCFunctions.Concat(record["dtables_link_attrs"], " class=\"", makeClassName(new XVar("hiddenelem")), "\" data-hidden");
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					record.InitAndSetArrayItem(makeClassName(new XVar("hiddenelem")), "dtables_link_class");
				}
			}
			if((XVar)(this.detailsLinksOnList == Constants.DL_SINGLE)  && (XVar)(MVCFunctions.count(tabNamesToHide)))
			{
				record["dtables_link_attrs"] = MVCFunctions.Concat(record["dtables_link_attrs"], " data-hiddentabs=\"", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.my_json_encode((XVar)(tabNamesToHide)))), "\"");
				this.controlsMap.InitAndSetArrayItem(tabNamesToHide, "gridRows", gridRowInd, "hiddentabs");
			}
			return null;
		}
		public virtual XVar getProceedLink()
		{
			dynamic i = null, masterTableInfo = XVar.Array(), proceedLink = null, strkey = null;
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				return "";
			}
			masterTableInfo = getMasterTableInfo();
			if(XVar.Pack(!(XVar)(masterTableInfo["proceedLink"])))
			{
				return "";
			}
			strkey = new XVar("");
			i = new XVar(1);
			for(;i <= MVCFunctions.count(this.masterKeysReq); i++)
			{
				strkey = MVCFunctions.Concat(strkey, "&masterkey", i, "=", MVCFunctions.RawUrlEncode((XVar)(this.masterKeysReq[i])));
			}
			proceedLink = XVar.Clone(MVCFunctions.Concat(MVCFunctions.GetTableLink((XVar)(MVCFunctions.GoodFieldName((XVar)(this.shortTableName))), (XVar)(this.pageType)), "?mastertable=", MVCFunctions.RawUrlEncode((XVar)(this.masterTable)), strkey));
			return MVCFunctions.Concat("<span class=\"rnr-dbebrick\">", "<a href=\"", proceedLink, "\" name=\"dp", this.id, "\">", "Proceed to", " ", CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), "</a>", "&nbsp;&nbsp;</span>");
		}
		protected virtual XVar isDetailTableSubquerySupported(dynamic _param_dDataSourceTName, dynamic _param_dTableIndex)
		{
			#region pass-by-value parameters
			dynamic dDataSourceTName = XVar.Clone(_param_dDataSourceTName);
			dynamic dTableIndex = XVar.Clone(_param_dTableIndex);
			#endregion

			return false;
		}
		protected virtual XVar isDetailTableSubqueryApplied(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			return false;
		}
		public virtual XVar getDetailsParams(dynamic _param_ids)
		{
			#region pass-by-value parameters
			dynamic ids = XVar.Clone(_param_ids);
			#endregion

			dynamic dpParams = XVar.Array();
			dpParams = XVar.Clone(XVar.Array());
			if((XVar)((XVar)(this.pageType != Constants.PAGE_VIEW)  && (XVar)(this.pageType != Constants.PAGE_EDIT))  && (XVar)(this.pageType != Constants.PAGE_ADD))
			{
				return dpParams;
			}
			foreach (KeyValuePair<XVar, dynamic> detailData in this.allDetailsTablesArr.GetEnumerator())
			{
				dynamic dpPermis = XVar.Array(), strDetTableName = null;
				if(XVar.Pack(!(XVar)((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_VIEW)  && (XVar)(detailData.Value["previewOnView"]))  || (XVar)((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(detailData.Value["previewOnEdit"])))  || (XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  && (XVar)(detailData.Value["previewOnAdd"])))))
				{
					continue;
				}
				strDetTableName = XVar.Clone(detailData.Value["dDataSourceTable"]);
				dpPermis = XVar.Clone(getPermissions((XVar)(strDetTableName)));
				if((XVar)((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_VIEW)  || (XVar)(this.pageType == Constants.PAGE_EDIT))  && (XVar)(dpPermis["search"]))  || (XVar)((XVar)(this.pageType == Constants.PAGE_EDIT)  && (XVar)(dpPermis["edit"])))  || (XVar)((XVar)(this.pageType == Constants.PAGE_ADD)  && (XVar)(dpPermis["add"])))
				{
					dpParams.InitAndSetArrayItem(++(ids), "ids", null);
					dpParams.InitAndSetArrayItem(strDetTableName, "strTableNames", null);
					dpParams.InitAndSetArrayItem(detailData.Value["dType"], "type", null);
					dpParams.InitAndSetArrayItem(detailData.Value["dShortTable"], "shorTNames", null);
				}
			}
			return dpParams;
		}
		public virtual XVar setDetailPreview(dynamic _param_dpType, dynamic _param_dpTableName, dynamic _param_dpId, dynamic data)
		{
			#region pass-by-value parameters
			dynamic dpType = XVar.Clone(_param_dpType);
			dynamic dpTableName = XVar.Clone(_param_dpTableName);
			dynamic dpId = XVar.Clone(_param_dpId);
			#endregion

			if((XVar)((XVar)((XVar)(this.pageType != Constants.PAGE_EDIT)  && (XVar)(this.pageType != Constants.PAGE_VIEW))  && (XVar)(this.pageType != Constants.PAGE_ADD))  || (XVar)(!(XVar)(CommonFunctions.CheckTablePermissions((XVar)(dpTableName), new XVar("S")))))
			{
				return null;
			}
			if(dpType == Constants.PAGE_CHART)
			{
				setDetailChartOnEditView((XVar)(dpTableName), (XVar)(dpId), (XVar)(data));
			}
			else
			{
				if(dpType == Constants.PAGE_REPORT)
				{
					setDetailReportOnEditView((XVar)(dpTableName), (XVar)(dpId), (XVar)(data));
				}
				else
				{
					setDetailList((XVar)(dpTableName), (XVar)(dpId), (XVar)(data));
				}
			}
			return null;
		}
		protected virtual XVar getDetailsPageObject(dynamic _param_tName, dynamic _param_listId = null, dynamic _param_data = null)
		{
			#region default values
			if(_param_listId as Object == null) _param_listId = new XVar(0);
			if(_param_data as Object == null) _param_data = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic listId = XVar.Clone(_param_listId);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic entityType = null, mKeys = XVar.Array(), masterKeys = XVar.Array(), mkr = null, options = XVar.Array(), pageObject = null;
			if(XVar.Pack(this.detailsTableObjects[tName]))
			{
				return this.detailsTableObjects[tName];
			}
			if(XVar.Pack(!(XVar)(listId)))
			{
				return null;
			}
			entityType = XVar.Clone(CommonFunctions.GetEntityType((XVar)(tName)));
			options = XVar.Clone(XVar.Array());
			options.InitAndSetArrayItem(listId, "id");
			options.InitAndSetArrayItem(1, "firstTime");
			options.InitAndSetArrayItem(this.pdfMode, "pdfMode");
			options.InitAndSetArrayItem(this.tName, "masterTable");
			options.InitAndSetArrayItem(this.pageType, "masterPageType");
			options.InitAndSetArrayItem(new XTempl(new XVar(true)), "xt");
			options.InitAndSetArrayItem(genId() + 1, "flyId");
			options.InitAndSetArrayItem(XVar.Array(), "masterKeysReq");
			options.InitAndSetArrayItem(false, "pushContext");
			mkr = new XVar(1);
			mKeys = XVar.Clone(this.pSet.getMasterKeysByDetailTable((XVar)(tName)));
			masterKeys = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> mk in mKeys.GetEnumerator())
			{
				options.InitAndSetArrayItem(data[mk.Value], "masterKeysReq", mkr);
				masterKeys.InitAndSetArrayItem(data[mk.Value], MVCFunctions.Concat("masterKey", mkr));
				mkr++;
			}
			if((XVar)(Constants.titTABLE == entityType)  || (XVar)(Constants.titVIEW == entityType))
			{
				options.InitAndSetArrayItem(Constants.LIST_DETAILS, "mode");
				options.InitAndSetArrayItem(Constants.PAGE_LIST, "pageType");
				pageObject = XVar.Clone(ListPage.createListPage((XVar)(tName), (XVar)(options)));
			}
			else
			{
				if(Constants.titREPORT == entityType)
				{
					options.InitAndSetArrayItem(tName, "tName");
					options.InitAndSetArrayItem(Constants.REPORT_DETAILS, "mode");
					options.InitAndSetArrayItem(Constants.PAGE_REPORT, "pageType");
					pageObject = XVar.Clone(new ReportPage((XVar)(options)));
				}
				else
				{
					if(Constants.titCHART == entityType)
					{
						options.InitAndSetArrayItem(tName, "tName");
						options.InitAndSetArrayItem(Constants.CHART_DETAILS, "mode");
						options.InitAndSetArrayItem(Constants.PAGE_CHART, "pageType");
						pageObject = XVar.Clone(new ChartPage((XVar)(options)));
					}
				}
			}
			this.detailsTableObjects.InitAndSetArrayItem(pageObject, tName);
			return pageObject;
		}
		public virtual XVar assignButtonsOnMasterEdit(dynamic _param_masterXt)
		{
			#region pass-by-value parameters
			dynamic masterXt = XVar.Clone(_param_masterXt);
			#endregion

			return null;
		}
		protected virtual XVar setDetailList(dynamic _param_listTName, dynamic _param_listId, dynamic data)
		{
			#region pass-by-value parameters
			dynamic listTName = XVar.Clone(_param_listTName);
			dynamic listId = XVar.Clone(_param_listId);
			#endregion

			dynamic listPageObject = null, tc = null;
			listPageObject = XVar.Clone(getDetailsPageObject((XVar)(listTName), (XVar)(listId), (XVar)(data)));
			using(tc = XVar.Clone(new TempPageContext((XVar)(listPageObject))))
			{
				listPageObject.prepareForBuildPage();
				if(XVar.Pack(listPageObject.shouldDisplayDetailsPage()))
				{
					foreach (KeyValuePair<XVar, dynamic> name in listPageObject.eventsObject.events.GetEnumerator())
					{
						listPageObject.xt.assign_event((XVar)(name.Key), (XVar)(listPageObject.eventsObject), (XVar)(name.Key), (XVar)(XVar.Array()));
					}
					listPageObject.addControlsJSAndCSS();
					listPageObject.fillSetCntrlMaps();
					listPageObject.BeforeShowList();
					assignDisplayDetailTableXtVariable((XVar)(listPageObject));
					copyDetailPreviewJSAndCSS((XVar)(listPageObject));
					updateSettingsWidthDPData((XVar)(listPageObject));
					this.viewControlsMap.InitAndSetArrayItem(listPageObject.viewControlsMap, "dViewControlsMap", listTName);
					this.controlsMap.InitAndSetArrayItem(listPageObject.controlsMap, "dControlsMap", listTName);
					if(this.pageType == Constants.PAGE_EDIT)
					{
						dynamic masterKeys = null;
						this.controlsMap.InitAndSetArrayItem(masterKeys, "dControlsMap", "masterKeys");
					}
					this.controlsMap.InitAndSetArrayItem(new XVar("tName", listTName, "id", listId, "pType", Constants.PAGE_LIST), "dpTablesParams", null);
				}
				this.flyId = XVar.Clone(listPageObject.recId + 1);
			}
			return null;
		}
		protected virtual XVar setDetailReportOnEditView(dynamic _param_reportTName, dynamic _param_reportId, dynamic data)
		{
			#region pass-by-value parameters
			dynamic reportTName = XVar.Clone(_param_reportTName);
			dynamic reportId = XVar.Clone(_param_reportId);
			#endregion

			dynamic mKeys = XVar.Array(), mkr = null, options = XVar.Array(), reportPageObject = null, tc = null;
			options = XVar.Clone(XVar.Array());
			options.InitAndSetArrayItem(reportId, "id");
			options.InitAndSetArrayItem(Constants.REPORT_DETAILS, "mode");
			options.InitAndSetArrayItem(this.pdfMode, "pdfMode");
			options.InitAndSetArrayItem(reportTName, "tName");
			options.InitAndSetArrayItem(Constants.PAGE_REPORT, "pageType");
			options.InitAndSetArrayItem(this.pageType, "masterPageType");
			options.InitAndSetArrayItem(this.tName, "masterTable");
			options.InitAndSetArrayItem(new XTempl(new XVar(true)), "xt");
			options.InitAndSetArrayItem(genId() + 1, "flyId");
			options.InitAndSetArrayItem(XVar.Array(), "masterKeysReq");
			mkr = new XVar(1);
			mKeys = XVar.Clone(this.pSet.getMasterKeysByDetailTable((XVar)(reportTName)));
			foreach (KeyValuePair<XVar, dynamic> mk in mKeys.GetEnumerator())
			{
				options.InitAndSetArrayItem(data[mk.Value], "masterKeysReq", mkr++);
			}
			reportPageObject = XVar.Clone(new ReportPage((XVar)(options)));
			using(tc = XVar.Clone(new TempPageContext((XVar)(reportPageObject))))
			{
				reportPageObject.init();
				if(XVar.Pack(mobileTemplateMode()))
				{
					reportPageObject.pageSize = XVar.Clone(-1);
				}
				reportPageObject.processGridTabs();
				reportPageObject.prepareDetailsForEditViewPage();
				if(XVar.Pack(!(XVar)(reportPageObject.shouldDisplayDetailsPage())))
				{
					return false;
				}
				reportPageObject.addControlsJSAndCSS();
				reportPageObject.fillSetCntrlMaps();
				reportPageObject.beforeShowReport();
				assignDisplayDetailTableXtVariable((XVar)(reportPageObject));
				copyDetailPreviewJSAndCSS((XVar)(reportPageObject));
				updateSettingsWidthDPData((XVar)(reportPageObject));
				this.viewControlsMap.InitAndSetArrayItem(reportPageObject.viewControlsMap, "dViewControlsMap", reportTName);
				this.controlsMap.InitAndSetArrayItem(reportPageObject.controlsMap, "dControlsMap", reportTName);
				this.controlsMap.InitAndSetArrayItem(new XVar("tName", reportTName, "id", options["id"], "pType", Constants.PAGE_REPORT), "dpTablesParams", null);
			}
			return null;
		}
		protected virtual XVar setDetailChartOnEditView(dynamic _param_chartTName, dynamic _param_chartId, dynamic data)
		{
			#region pass-by-value parameters
			dynamic chartTName = XVar.Clone(_param_chartTName);
			dynamic chartId = XVar.Clone(_param_chartId);
			#endregion

			dynamic chartPageObject = null, chartXtParams = XVar.Array(), mKeys = XVar.Array(), masterKeysReq = XVar.Array(), mkr = null, options = XVar.Array(), tc = null;
			XTempl xt;
			if(XVar.Pack(this.pdfMode))
			{
				return null;
			}
			xt = XVar.UnPackXTempl(new XTempl(new XVar(true)));
			options = XVar.Clone(XVar.Array());
			options.InitAndSetArrayItem(xt, "xt");
			options.InitAndSetArrayItem(chartId, "id");
			options.InitAndSetArrayItem(chartTName, "tName");
			options.InitAndSetArrayItem(Constants.CHART_DETAILS, "mode");
			options.InitAndSetArrayItem(Constants.PAGE_CHART, "pageType");
			options.InitAndSetArrayItem(this.pageType, "masterPageType");
			options.InitAndSetArrayItem(this.tName, "masterTable");
			options.InitAndSetArrayItem(genId() + 1, "flyId");
			mkr = new XVar(1);
			mKeys = XVar.Clone(this.pSet.getMasterKeysByDetailTable((XVar)(chartTName)));
			foreach (KeyValuePair<XVar, dynamic> mk in mKeys.GetEnumerator())
			{
				options.InitAndSetArrayItem(data[mk.Value], "masterKeysReq", mkr++);
			}
			masterKeysReq = XVar.Clone(options["masterKeysReq"]);
			if(XVar.Pack(MVCFunctions.count(masterKeysReq)))
			{
				dynamic i = null;
				i = new XVar(1);
				for(;i <= MVCFunctions.count(masterKeysReq); i++)
				{
					XSession.Session[MVCFunctions.Concat(chartTName, "_masterkey", i)] = masterKeysReq[i];
				}
				if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(chartTName, "_masterkey", i))))
				{
					XSession.Session.Remove(MVCFunctions.Concat(chartTName, "_masterkey", i));
				}
			}
			chartPageObject = XVar.Clone(new ChartPage((XVar)(options)));
			using(tc = XVar.Clone(new TempPageContext((XVar)(chartPageObject))))
			{
				chartPageObject.init();
				chartXtParams.InitAndSetArrayItem(options["flyId"], "id");
				chartXtParams.InitAndSetArrayItem(chartTName, "table");
				chartXtParams.InitAndSetArrayItem(chartPageObject.pSet.getChartType(), "ctype");
				chartXtParams.InitAndSetArrayItem(chartPageObject.shortTableName, "chartName");
				chartXtParams.InitAndSetArrayItem(true, "singlePage");
				chartXtParams.InitAndSetArrayItem(MVCFunctions.Concat("rnr", chartXtParams["chartName"], chartXtParams["id"]), "containerId");
				xt.assign_function((XVar)(MVCFunctions.Concat(chartPageObject.shortTableName, "_chart")), new XVar("xt_showchart"), (XVar)(chartXtParams));
				chartPageObject.processGridTabs();
				chartPageObject.prepareDetailsForEditViewPage();
				if(XVar.Pack(mobileTemplateMode()))
				{
					xt.assign(new XVar("container_menu"), new XVar(false));
				}
				chartPageObject.addControlsJSAndCSS();
				chartPageObject.fillSetCntrlMaps();
				AddJSFile(new XVar("libs/js/anychart.min.js"));
				AddJSFile(new XVar("libs/js/migrationTool.js"));
				chartPageObject.beforeShowChart();
				assignDisplayDetailTableXtVariable((XVar)(chartPageObject));
				copyDetailPreviewJSAndCSS((XVar)(chartPageObject));
				updateSettingsWidthDPData((XVar)(chartPageObject));
				this.viewControlsMap.InitAndSetArrayItem(chartPageObject.viewControlsMap, "dViewControlsMap", chartTName);
				this.controlsMap.InitAndSetArrayItem(chartPageObject.controlsMap, "dControlsMap", chartTName);
				this.controlsMap.InitAndSetArrayItem(new XVar("tName", chartTName, "id", options["id"], "pType", Constants.PAGE_CHART), "dpTablesParams", null);
			}
			return null;
		}
		protected virtual XVar getKeysFromData(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic keyFields = XVar.Array(), keys = XVar.Array();
			keys = XVar.Clone(XVar.Array());
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			foreach (KeyValuePair<XVar, dynamic> keyField in keyFields.GetEnumerator())
			{
				keys.InitAndSetArrayItem(data[keyField.Value], keyField.Value);
			}
			return keys;
		}
		protected virtual XVar copyDetailPreviewJSAndCSS(dynamic dtPageObject)
		{
			dynamic layout = null;
			layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(MVCFunctions.GoodFieldName((XVar)(dtPageObject.tName))), (XVar)(dtPageObject.pageType)));
			if(XVar.Pack(layout))
			{
				AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(CommonFunctions.isPageLayoutMobile((XVar)(this.templatefile))), (XVar)(this.pdfMode != ""))));
			}
			copyAllJsFiles((XVar)(dtPageObject.grabAllJsFiles()));
			copyAllCssFiles((XVar)(dtPageObject.grabAllCSSFiles()));
			return null;
		}
		protected virtual XVar updateSettingsWidthDPData(dynamic dtPageObject)
		{
			dynamic tName = null;
			tName = XVar.Clone(dtPageObject.tName);
			this.jsSettings.InitAndSetArrayItem(dtPageObject.jsSettings["tableSettings"][tName], "tableSettings", tName);
			foreach (KeyValuePair<XVar, dynamic> val in dtPageObject.jsSettings["global"]["shortTNames"].GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.settingsMap["globalSettings"]["shortTNames"].KeyExists(val.Key))))
				{
					this.settingsMap.InitAndSetArrayItem(val.Value, "globalSettings", "shortTNames", val.Key);
				}
			}
			return null;
		}
		protected virtual XVar assignDisplayDetailTableXtVariable(dynamic dtPageObject)
		{
			this.xt.assign((XVar)(MVCFunctions.Concat("details_", dtPageObject.shortTableName)), new XVar(true));
			this.xt.assign_method((XVar)(MVCFunctions.Concat("displayDetailTable_", dtPageObject.shortTableName)), (XVar)(dtPageObject), new XVar("showPageDp"), new XVar(false));
			return null;
		}
		public virtual XVar removeHiddenColumnsFromInlineFields(dynamic _param_inlineControlFields, dynamic _param_screenWidth, dynamic _param_screenHeight, dynamic _param_orientation)
		{
			#region pass-by-value parameters
			dynamic inlineControlFields = XVar.Clone(_param_inlineControlFields);
			dynamic screenWidth = XVar.Clone(_param_screenWidth);
			dynamic screenHeight = XVar.Clone(_param_screenHeight);
			dynamic orientation = XVar.Clone(_param_orientation);
			#endregion

			dynamic devices = XVar.Array();
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				return inlineControlFields;
			}
			devices = XVar.Clone(new XVar(0, Constants.DESKTOP, 1, Constants.TABLET_10_IN, 2, Constants.SMARTPHONE_LANDSCAPE, 3, Constants.SMARTPHONE_PORTRAIT, 4, Constants.TABLET_7_IN));
			foreach (KeyValuePair<XVar, dynamic> d in devices.GetEnumerator())
			{
				dynamic columnsToHide = XVar.Array();
				columnsToHide = XVar.Clone(this.pSet.getHiddenFields((XVar)(d.Value)));
				if((XVar)(!(XVar)(MVCFunctions.count(columnsToHide)))  || (XVar)(!(XVar)(isColumnHiddenForDevice((XVar)(d.Value), (XVar)(screenWidth), (XVar)(screenHeight), (XVar)(orientation)))))
				{
					continue;
				}
				foreach (KeyValuePair<XVar, dynamic> status in columnsToHide.GetEnumerator())
				{
					dynamic fieldPos = null;
					fieldPos = XVar.Clone(MVCFunctions.array_search((XVar)(status.Key), (XVar)(inlineControlFields)));
					if(!XVar.Equals(XVar.Pack(fieldPos), XVar.Pack(false)))
					{
						MVCFunctions.array_splice((XVar)(inlineControlFields), (XVar)(fieldPos), new XVar(1));
					}
				}
				return inlineControlFields;
			}
			return inlineControlFields;
		}
		protected virtual XVar isColumnHiddenForDevice(dynamic _param_d, dynamic _param_screenWidth, dynamic _param_screenHeight, dynamic _param_orientation)
		{
			#region pass-by-value parameters
			dynamic d = XVar.Clone(_param_d);
			dynamic screenWidth = XVar.Clone(_param_screenWidth);
			dynamic screenHeight = XVar.Clone(_param_screenHeight);
			dynamic orientation = XVar.Clone(_param_orientation);
			#endregion

			if(d == Constants.DESKTOP)
			{
				return 1281 <= screenWidth;
			}
			if(d == Constants.TABLET_10_IN)
			{
				return (XVar)((XVar)((XVar)(screenWidth == 768)  && (XVar)(screenHeight == 1024))  || (XVar)((XVar)((XVar)(1025 <= screenWidth)  && (XVar)(screenWidth <= 1280))  && (XVar)(screenHeight <= 1023)))  || (XVar)((XVar)((XVar)(1025 <= screenHeight)  && (XVar)(screenHeight <= 1280))  && (XVar)(screenWidth <= 1023));
			}
			if(d == Constants.TABLET_7_IN)
			{
				return (XVar)((XVar)(screenWidth <= 1024)  && (XVar)(screenHeight <= 800))  || (XVar)((XVar)(screenHeight <= 1024)  && (XVar)(screenWidth <= 800));
			}
			if(d == Constants.SMARTPHONE_LANDSCAPE)
			{
				return (XVar)((XVar)(screenHeight <= 420)  && (XVar)(orientation == "landscape"))  || (XVar)((XVar)(screenWidth <= 420)  && (XVar)(orientation == "landscape"));
			}
			if(d == Constants.SMARTPHONE_PORTRAIT)
			{
				return (XVar)((XVar)(screenHeight <= 420)  && (XVar)(orientation == "portrait"))  || (XVar)((XVar)(screenWidth <= 420)  && (XVar)(orientation == "portrait"));
			}
			return false;
		}
		protected static XVar getKeysTitleTemplate(dynamic _param_table, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic keys = XVar.Array(), str = null;
			keys = XVar.Clone(pSet.getTableKeys());
			str = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> k in keys.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(str))))
				{
					str = MVCFunctions.Concat(str, ", ");
				}
				str = MVCFunctions.Concat(str, "{%", MVCFunctions.GoodFieldName((XVar)(k.Value)), "}");
			}
			return str;
		}
		public static XVar getDefaultPageTitle(dynamic _param_page, dynamic _param_table, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			dynamic table = XVar.Clone(_param_table);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			if(page == "add")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), ", ", "Add new");
			}
			if(page == "edit")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), ", ", "Edit", " [", getKeysTitleTemplate((XVar)(table), (XVar)(pSet)), "]");
			}
			if(page == "view")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), " [", getKeysTitleTemplate((XVar)(table), (XVar)(pSet)), "]");
			}
			if(page == "export")
			{
				return "Export";
			}
			if(page == "import")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), ", ", "Import");
			}
			if(page == "search")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), " - ", "Advanced search");
			}
			if(page == "print")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "rprint")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "list")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "masterlist")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), " [", getKeysTitleTemplate((XVar)(table), (XVar)(pSet)), "]");
			}
			if(page == "masterchart")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "masterreport")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), " [", getKeysTitleTemplate((XVar)(table), (XVar)(pSet)), "]");
			}
			if(page == "masterprint")
			{
				return MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(table)), " [", getKeysTitleTemplate((XVar)(table), (XVar)(pSet)), "]");
			}
			if(page == "login")
			{
				return "Login";
			}
			if(page == "register")
			{
				return "Register";
			}
			if(page == "register_success")
			{
				return "Registration successful!";
			}
			if(page == "changepwd")
			{
				return "Change password";
			}
			if(page == "changepwd")
			{
				return "";
			}
			if(page == "remind")
			{
				return "Password reminder";
			}
			if(page == "chart")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "report")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "dashboard")
			{
				return CommonFunctions.GetTableCaption((XVar)(table));
			}
			if(page == "menu")
			{
				return "Menu";
			}
			return null;
		}
		protected virtual XVar getPageTitleTemplate(dynamic _param_page, dynamic _param_table, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			dynamic table = XVar.Clone(_param_table);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic templ = null;
			if((XVar)(!(XVar)(table))  || (XVar)(page == Constants.PAGE_REGISTER))
			{
				table = new XVar(".global");
			}
			templ = new XVar("");
			if(XVar.Pack(GlobalVars.page_titles.KeyExists(table)))
			{
				templ = XVar.Clone(GlobalVars.page_titles[table][CommonFunctions.mlang_getcurrentlang()][page]);
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(templ))))
			{
				return templ;
			}
			return getDefaultPageTitle((XVar)(page), (XVar)(table), (XVar)(pSet));
		}
		public virtual XVar getPageTitle(dynamic _param_page, dynamic _param_table = null, dynamic _param_record = null, dynamic _param_settings = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_record as Object == null) _param_record = new XVar();
			if(_param_settings as Object == null) _param_settings = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			dynamic table = XVar.Clone(_param_table);
			dynamic record = XVar.Clone(_param_record);
			dynamic settings = XVar.Clone(_param_settings);
			#endregion

			dynamic currentRecord = null, masterRecord = null, templ = null;
			ProjectSettings pSet;
			pSet = XVar.UnPackProjectSettings((XVar.Pack(settings == null) ? XVar.Pack(this.pSet) : XVar.Pack(settings)));
			templ = XVar.Clone(getPageTitleTemplate((XVar)(page), (XVar)(table), (XVar)(pSet)));
			masterRecord = XVar.Clone(XVar.Array());
			if(!XVar.Equals(XVar.Pack(MVCFunctions.stripos((XVar)(templ), new XVar("{%master."))), XVar.Pack(false)))
			{
				masterRecord = XVar.Clone(getMasterRecord());
			}
			currentRecord = XVar.Clone(XVar.Array());
			if(XVar.Pack(record))
			{
				currentRecord = XVar.Clone(record);
			}
			else
			{
				if(!XVar.Equals(XVar.Pack(MVCFunctions.preg_match(new XVar("/{\\%(?!master\\.)[\\w\\s\\-\\.]*}/"), (XVar)(templ))), XVar.Pack(false)))
				{
					currentRecord = XVar.Clone(getCurrentRecord());
				}
			}
			return calcPageTitle((XVar)(templ), (XVar)(currentRecord), (XVar)(this.masterTable), (XVar)(masterRecord), (XVar)(pSet));
		}
		public virtual XVar calcPageTitle(dynamic _param_templ, dynamic _param_currentRecord = null, dynamic _param_masterTable = null, dynamic _param_masterRecord = null, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_currentRecord as Object == null) _param_currentRecord = new XVar(XVar.Array());
			if(_param_masterTable as Object == null) _param_masterTable = new XVar("");
			if(_param_masterRecord as Object == null) _param_masterRecord = new XVar(XVar.Array());
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic templ = XVar.Clone(_param_templ);
			dynamic currentRecord = XVar.Clone(_param_currentRecord);
			dynamic masterTable = XVar.Clone(_param_masterTable);
			dynamic masterRecord = XVar.Clone(_param_masterRecord);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic matches = XVar.Array();
			if(XVar.Pack(!(XVar)(pSet)))
			{
				pSet = XVar.UnPackProjectSettings(this.pSet);
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(masterRecord)))))
			{
				masterRecord = XVar.Clone(XVar.Array());
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(currentRecord)))))
			{
				currentRecord = XVar.Clone(XVar.Array());
			}
			matches = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(MVCFunctions.preg_match_all(new XVar("/{\\%([\\w\\.\\s\\-]*)\\}/"), (XVar)(templ), (XVar)(matches)))))
			{
				return templ;
			}
			foreach (KeyValuePair<XVar, dynamic> m in matches[0].GetEnumerator())
			{
				dynamic field = null;
				if(XVar.Pack(!(XVar)(MVCFunctions.strcasecmp((XVar)(MVCFunctions.substr((XVar)(m.Value), new XVar(0), new XVar(9))), new XVar("{%master.")))))
				{
					dynamic mSettings = null, masterViewControl = null;
					mSettings = XVar.Clone(new ProjectSettings((XVar)(masterTable), new XVar(Constants.PAGE_LIST)));
					field = XVar.Clone(mSettings.getFieldByGoodFieldName((XVar)(MVCFunctions.trim((XVar)(MVCFunctions.substr((XVar)(m.Value), new XVar(9), (XVar)(MVCFunctions.strlen((XVar)(m.Value)) - 10)))))));
					masterViewControl = XVar.Clone(new ViewControlsContainer((XVar)(mSettings), new XVar(Constants.PAGE_LIST)));
					templ = XVar.Clone(MVCFunctions.str_replace((XVar)(m.Value), (XVar)((XVar.Pack(masterRecord) ? XVar.Pack(masterViewControl.showDBValue((XVar)(field), (XVar)(masterRecord))) : XVar.Pack(""))), (XVar)(templ)));
				}
				else
				{
					field = XVar.Clone(pSet.getFieldByGoodFieldName((XVar)(MVCFunctions.trim((XVar)(MVCFunctions.substr((XVar)(m.Value), new XVar(2), (XVar)(MVCFunctions.strlen((XVar)(m.Value)) - 3)))))));
					templ = XVar.Clone(MVCFunctions.str_replace((XVar)(m.Value), (XVar)((XVar.Pack(currentRecord) ? XVar.Pack(showDBValue((XVar)(field), (XVar)(currentRecord))) : XVar.Pack(""))), (XVar)(templ)));
				}
			}
			return templ;
		}
		public virtual XVar setPageTitle(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			this.xt.assign(new XVar("pagetitlelabel"), (XVar)(str));
			return null;
		}
		public virtual XVar getCurrentRecord()
		{
			return XVar.Array();
		}
		public virtual XVar setFieldLabel(dynamic _param_field, dynamic _param_label)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic label = XVar.Clone(_param_label);
			#endregion

			if(XVar.Pack(GlobalVars.field_labels[MVCFunctions.GoodFieldName((XVar)(this.tName))][CommonFunctions.mlang_getcurrentlang()].KeyExists(MVCFunctions.GoodFieldName((XVar)(field)))))
			{
				GlobalVars.field_labels.InitAndSetArrayItem(label, MVCFunctions.GoodFieldName((XVar)(this.tName)), CommonFunctions.mlang_getcurrentlang(), MVCFunctions.GoodFieldName((XVar)(field)));
				return true;
			}
			else
			{
				return false;
			}
			return null;
		}
		protected virtual XVar assignBody()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage(new XVar(false)));
			if(XVar.Pack(!(XVar)(mobileTemplateMode())))
			{
				this.body["begin"] = MVCFunctions.Concat(this.body["begin"], "<div id=\"search_suggest", this.id, "\"></div>\r\n");
			}
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assign(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public virtual XVar getInputElementId(dynamic _param_field, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic format = null;
			if(XVar.Pack(!(XVar)(pSet)))
			{
				pSet = XVar.UnPackProjectSettings(this.pSet);
			}
			format = XVar.Clone(pSet.getEditFormat((XVar)(field)));
			if(format == Constants.EDIT_FORMAT_DATE)
			{
				dynamic var_type = null;
				var_type = XVar.Clone(pSet.getDateEditType((XVar)(field)));
				if((XVar)(var_type == Constants.EDIT_DATE_DD)  || (XVar)(var_type == Constants.EDIT_DATE_DD_DP))
				{
					return MVCFunctions.Concat("dayvalue_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id);
				}
				else
				{
					return MVCFunctions.Concat("value_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id);
				}
			}
			else
			{
				if(format == Constants.EDIT_FORMAT_RADIO)
				{
					return MVCFunctions.Concat("radio_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id, "_0");
				}
				else
				{
					if(format == Constants.EDIT_FORMAT_LOOKUP_WIZARD)
					{
						dynamic lookuptype = null;
						lookuptype = XVar.Clone(pSet.lookupControlType((XVar)(field)));
						if((XVar)(mobileTemplateMode())  && (XVar)(lookuptype == Constants.LCT_AJAX))
						{
							lookuptype = new XVar(Constants.LCT_DROPDOWN);
						}
						if((XVar)(lookuptype == Constants.LCT_AJAX)  || (XVar)(lookuptype == Constants.LCT_LIST))
						{
							return MVCFunctions.Concat("display_value_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id);
						}
						else
						{
							return MVCFunctions.Concat("value_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id);
						}
					}
					else
					{
						return MVCFunctions.Concat("value_", MVCFunctions.GoodFieldName((XVar)(field)), "_", this.id);
					}
				}
			}
			return null;
		}
		public virtual XVar getFieldControlsData()
		{
			return XVar.Array();
		}
		public virtual XVar isSearchPanelActivated()
		{
			return this.searchPanelActivated;
		}
		public virtual XVar keysSQLExpression(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			dynamic chunks = XVar.Array(), keyFields = XVar.Array();
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			chunks = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> kf in keyFields.GetEnumerator())
			{
				dynamic value = null, valueisnull = null;
				value = XVar.Clone(this.cipherer.MakeDBValue((XVar)(kf.Value), (XVar)(keys[kf.Value]), new XVar(""), new XVar(true)));
				if(this.connection.dbType == Constants.nDATABASE_Oracle)
				{
					valueisnull = XVar.Clone((XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack("null")))  || (XVar)(value == "''"));
				}
				else
				{
					valueisnull = XVar.Clone(XVar.Equals(XVar.Pack(value), XVar.Pack("null")));
				}
				if(XVar.Pack(valueisnull))
				{
					chunks.InitAndSetArrayItem(MVCFunctions.Concat(getFieldSQL((XVar)(kf.Value)), " is null"), null);
				}
				else
				{
					chunks.InitAndSetArrayItem(MVCFunctions.Concat(getFieldSQLDecrypt((XVar)(kf.Value)), "=", this.cipherer.MakeDBValue((XVar)(kf.Value), (XVar)(keys[kf.Value]), new XVar(""), new XVar(true))), null);
				}
			}
			return MVCFunctions.implode(new XVar(" and "), (XVar)(chunks));
		}
		public virtual XVar countTotals(dynamic totals, dynamic data)
		{
			dynamic curTotalFieldValue = null, i = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.totalsFields); i++)
			{
				curTotalFieldValue = XVar.Clone(data[this.totalsFields[i]["fName"]]);
				if(XVar.Pack(!(XVar)(totals.KeyExists(this.totalsFields[i]["fName"]))))
				{
					totals.InitAndSetArrayItem(0, this.totalsFields[i]["fName"]);
				}
				if(this.totalsFields[i]["totalsType"] == "COUNT")
				{
					if((XVar)(this.totalsFields[i]["viewFormat"] == Constants.FORMAT_CHECKBOX)  && (XVar)((XVar)(curTotalFieldValue == null)  || (XVar)(!(XVar)(curTotalFieldValue))))
					{
						continue;
					}
					if(0 != MVCFunctions.strlen((XVar)(curTotalFieldValue)))
					{
						totals[this.totalsFields[i]["fName"]]++;
					}
				}
				else
				{
					if(this.totalsFields[i]["viewFormat"] == "Time")
					{
						dynamic time = XVar.Array();
						time = XVar.Clone(CommonFunctions.GetTotalsForTime((XVar)(curTotalFieldValue)));
						totals[this.totalsFields[i]["fName"]] += (time[2] + time[1] * 60) + time[0] * 3600;
					}
					else
					{
						totals[this.totalsFields[i]["fName"]] += curTotalFieldValue + 0;
					}
				}
				if(this.totalsFields[i]["totalsType"] == "AVERAGE")
				{
					if((XVar)(!(XVar)(curTotalFieldValue == null))  && (XVar)(!XVar.Equals(XVar.Pack(curTotalFieldValue), XVar.Pack(""))))
					{
						this.totalsFields[i]["numRows"]++;
					}
				}
			}
			return null;
		}
		public static XVar deleteAvailable(dynamic instance)
		{
			return (XVar)(instance.pSet.hasDelete())  && (XVar)(instance.permis[instance.tName]["delete"]);
		}
		public virtual XVar deleteAvailable()
		{
			return (XVar)(this.pSet.hasDelete())  && (XVar)(this.permis[this.tName]["delete"]);
		}
		public virtual XVar importAvailable()
		{
			return (XVar)(this.permis[this.tName]["import"])  && (XVar)(this.pSet.hasImportPage());
		}
		public static XVar editAvailable(dynamic instance)
		{
			return (XVar)(instance.pSet.hasEditPage())  && (XVar)(instance.permis[instance.tName]["edit"]);
		}
		public virtual XVar editAvailable()
		{
			return (XVar)(this.pSet.hasEditPage())  && (XVar)(this.permis[this.tName]["edit"]);
		}
		public static XVar addAvailable(dynamic instance)
		{
			return (XVar)(instance.pSet.hasAddPage())  && (XVar)(instance.permis[instance.tName]["add"]);
		}
		public virtual XVar addAvailable()
		{
			return (XVar)(this.pSet.hasAddPage())  && (XVar)(this.permis[this.tName]["add"]);
		}
		public virtual XVar copyAvailable()
		{
			return (XVar)(this.pSet.hasCopyPage())  && (XVar)(this.permis[this.tName]["add"]);
		}
		public virtual XVar inlineEditAvailable()
		{
			return (XVar)(this.permis[this.tName]["edit"])  && (XVar)(this.pSet.hasInlineEdit());
		}
		public virtual XVar updateSelectedAvailable()
		{
			return (XVar)(this.permis[this.tName]["edit"])  && (XVar)(this.pSet.hasUpdateSelected());
		}
		public virtual XVar inlineAddAvailable()
		{
			return (XVar)(this.permis[this.tName]["add"])  && (XVar)(this.pSet.hasInlineAdd());
		}
		public static XVar viewAvailable(dynamic instance)
		{
			return (XVar)(instance.permis[instance.tName]["search"])  && (XVar)(instance.pSet.hasViewPage());
		}
		public virtual XVar viewAvailable()
		{
			return (XVar)(this.permis[this.tName]["search"])  && (XVar)(this.pSet.hasViewPage());
		}
		public virtual XVar exportAvailable()
		{
			return (XVar)(this.permis[this.tName]["export"])  && (XVar)(this.pSet.hasExportPage());
		}
		public virtual XVar printAvailable()
		{
			return (XVar)(this.permis[this.tName]["export"])  && (XVar)(this.pSet.hasPrintPage());
		}
		public virtual XVar advSearchAvailable()
		{
			return (XVar)(this.permis[this.tName]["search"])  && (XVar)(MVCFunctions.count(this.pSet.getAdvSearchFields()));
		}
		public virtual XVar gridTabsAvailable()
		{
			return false;
		}
		public virtual XVar getIncludeFileMapProvider()
		{
			switch(((XVar)CommonFunctions.getMapProvider()).ToInt())
			{
				case Constants.GOOGLE_MAPS:
					return "gmap.js";
					break;
				case Constants.OPEN_STREET_MAPS:
					return "osmap.js";
					break;
				case Constants.BING_MAPS:
					return "bingmap.js";
					break;
			}
			return null;
		}
		public virtual XVar includeOSMfile()
		{
			if(CommonFunctions.getMapProvider() == Constants.OPEN_STREET_MAPS)
			{
				AddJSFile(new XVar("plugins/OpenLayers.js"));
			}
			return null;
		}
		public virtual XVar displayTabsSections(dynamic _param_tabId)
		{
			#region pass-by-value parameters
			dynamic tabId = XVar.Clone(_param_tabId);
			#endregion

			dynamic i = null, tab = XVar.Array(), tabs = XVar.Array();
			tabs = XVar.Clone(getArrTabs());
			tab = new XVar(null);
			i = new XVar(0);
			for(;i < MVCFunctions.count(tabs); i++)
			{
				if(tabs[i]["tabId"] == tabId)
				{
					tab = XVar.Clone(tabs[i]);
					break;
				}
			}
			if(XVar.Pack(!(XVar)(tab)))
			{
				return null;
			}
			if(tab["nType"] == Constants.TAB_TYPE_TAB)
			{
				displayTabGroup((XVar)(i));
			}
			else
			{
				if(tab["nType"] == Constants.TAB_TYPE_SECTION)
				{
					displaySection((XVar)(tab));
				}
				else
				{
					if(tab["nType"] == Constants.TAB_TYPE_STEP)
					{
						displayStep((XVar)(i));
					}
				}
			}
			return null;
		}
		public virtual XVar displaySection(dynamic _param_tabInfo)
		{
			#region pass-by-value parameters
			dynamic tabInfo = XVar.Clone(_param_tabInfo);
			#endregion

			dynamic layout = null;
			layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(tabInfo["tabId"])));
			if(XVar.Pack(layout))
			{
				AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(mobileTemplateMode()), (XVar)(this.pdfMode != ""))));
			}
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				dynamic hiddenStyle = null, layoutClasses = null, src = null;
				if((XVar)(tabInfo["expandSec"])  || (XVar)(this.pdfMode))
				{
					src = new XVar("images/minus.gif");
					hiddenStyle = new XVar("");
				}
				else
				{
					src = new XVar("images/plus.gif");
					hiddenStyle = new XVar("style=\"display: none;\"");
				}
				layoutClasses = new XVar("");
				if(XVar.Pack(layout))
				{
					layoutClasses = XVar.Clone(MVCFunctions.Concat(" ", layout.style, " page-", layout.name));
				}
				if(XVar.Pack(!(XVar)(this.pdfMode)))
				{
					MVCFunctions.Echo(MVCFunctions.Concat("<img id=\"section_", tabInfo["tabId"], this.id, "Butt\" border=\"0\" src=\"", MVCFunctions.GetRootPathForResources((XVar)(src)), "\" valign=\"middle\" alt=\"*\" />"));
				}
				MVCFunctions.Echo(MVCFunctions.Concat(tabInfo["tabName"], "<br>\r\n\t\t\t\t\t<div id=\"section_", tabInfo["tabId"], this.id, "\" class=\"sectionFrame rnr-pagewrapper", layoutClasses, "\" ", hiddenStyle, " >"));
				this.xt.displayPartial((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(MVCFunctions.Concat(this.pageType, "_", tabInfo["tabId"])))));
				MVCFunctions.Echo("</div>");
			}
			else
			{
				this.xt.displayPartial((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(MVCFunctions.Concat(this.pageType, "_", tabInfo["tabId"])))));
			}
			return null;
		}
		public virtual XVar displayTabGroup(dynamic _param_startIndex)
		{
			#region pass-by-value parameters
			dynamic startIndex = XVar.Clone(_param_startIndex);
			#endregion

			dynamic firstTabId = null, i = null, layout = null, selected = null, tabGroupId = null, tabs = XVar.Array();
			tabs = XVar.Clone(getArrTabs());
			firstTabId = XVar.Clone(tabs[startIndex]["tabId"]);
			tabGroupId = XVar.Clone(tabs[startIndex]["tabGroup"]);
			if((XVar)(!(XVar)(tabGroupId))  || (XVar)(tabs[startIndex]["nType"] != Constants.TAB_TYPE_TAB))
			{
				return null;
			}
			if(XVar.Pack(this.pdfMode))
			{
				i = XVar.Clone(startIndex);
				for(;i < MVCFunctions.count(tabs); ++(i))
				{
					if(tabGroupId != tabs[i]["tabGroup"])
					{
						break;
					}
					displaySection((XVar)(tabs[i]));
				}
				return null;
			}
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				dynamic firstTab = null, layoutClasses = null;
				MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"tabGroup_", firstTabId, this.id, "\" class=\"yui-navset\">"));
				MVCFunctions.Echo("<ul class=\"yui-nav\">");
				selected = new XVar("selected");
				i = XVar.Clone(startIndex);
				for(;i < MVCFunctions.count(tabs); ++(i))
				{
					if(tabGroupId != tabs[i]["tabGroup"])
					{
						break;
					}
					MVCFunctions.Echo(MVCFunctions.Concat("<li class=\"rnr-tab ", selected, " rnr-tab-navigation\">"));
					MVCFunctions.Echo(MVCFunctions.Concat("<a href=\"#", tabs[i]["tabId"], "\"><span>", tabs[i]["tabName"], "</span></a></li>"));
					selected = new XVar("");
				}
				MVCFunctions.Echo("</ul>");
				MVCFunctions.Echo("<div class=\"yui-content\">");
				firstTab = new XVar(true);
				i = XVar.Clone(startIndex);
				for(;i < MVCFunctions.count(tabs); ++(i))
				{
					if(tabGroupId != tabs[i]["tabGroup"])
					{
						break;
					}
					layoutClasses = new XVar("");
					layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(tabs[i]["tabId"])));
					if(XVar.Pack(layout))
					{
						AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(mobileTemplateMode()), (XVar)(this.pdfMode != ""))));
						layoutClasses = XVar.Clone(MVCFunctions.Concat(" ", layout.style, " page-", layout.name));
					}
					if(XVar.Pack(!(XVar)(firstTab)))
					{
						layoutClasses = MVCFunctions.Concat(layoutClasses, " rnr-hidden-tab-panel");
					}
					firstTab = new XVar(false);
					MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"", tabs[i]["tabId"], this.id, "\" class=\"rnr-pagewrapper", layoutClasses, "\">"));
					this.xt.displayPartial((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(MVCFunctions.Concat(this.pageType, "_", tabs[i]["tabId"])))));
					MVCFunctions.Echo("</div>");
				}
				MVCFunctions.Echo("</div></div>");
			}
			else
			{
				MVCFunctions.Echo("<div class=\"tab-content\">");
				selected = new XVar("active");
				i = XVar.Clone(startIndex);
				for(;i < MVCFunctions.count(tabs); ++(i))
				{
					if(tabGroupId != tabs[i]["tabGroup"])
					{
						break;
					}
					layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(tabs[i]["tabId"])));
					if(XVar.Pack(layout))
					{
						AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(mobileTemplateMode()), (XVar)(this.pdfMode != ""))));
					}
					MVCFunctions.Echo(MVCFunctions.Concat("<div role=\"tabpanel\" class=\"tab-pane ", selected, "\" id=\"", tabs[i]["tabId"], this.id, "\">"));
					this.xt.displayPartial((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(MVCFunctions.Concat(this.pageType, "_", tabs[i]["tabId"])))));
					MVCFunctions.Echo("</div>");
					selected = new XVar("");
				}
				MVCFunctions.Echo("</div>");
			}
			return null;
		}
		public virtual XVar displayStep(dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic index = XVar.Clone(_param_index);
			#endregion

			dynamic hiddenStyle = null, layout = null, layoutClasses = null, tabInfo = XVar.Array(), tabs = XVar.Array();
			tabs = XVar.Clone(getArrTabs());
			tabInfo = XVar.Clone(tabs[index]);
			hiddenStyle = new XVar("");
			if(index != this.initialStep)
			{
				hiddenStyle = new XVar("style=\"display:none\"");
			}
			layoutClasses = new XVar("");
			layout = XVar.Clone(CommonFunctions.GetPageLayout((XVar)(this.shortTableName), (XVar)(this.pageType), (XVar)(tabInfo["tabId"])));
			if(XVar.Pack(layout))
			{
				layoutClasses = XVar.Clone(MVCFunctions.Concat(" ", layout.style, " page-", layout.name));
				AddCSSFile((XVar)(layout.getCSSFiles((XVar)(CommonFunctions.isRTL()), (XVar)(mobileTemplateMode()), (XVar)(this.pdfMode != ""))));
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<div id=\"step_", index, "_", this.id, "\" class=\"stepFrame rnr-pagewrapper", layoutClasses, "\" data-step=\"", index, "\" ", hiddenStyle, " >"));
			this.xt.displayPartial((XVar)(MVCFunctions.GetTemplateName((XVar)(this.shortTableName), (XVar)(MVCFunctions.Concat(this.pageType, "_", tabInfo["tabId"])))));
			MVCFunctions.Echo("</div>");
			return null;
		}
		public virtual XVar isMultistepped()
		{
			return false;
		}
		public virtual XVar prepareSteps()
		{
			dynamic steps = null;
			if(XVar.Pack(!(XVar)(isMultistepped())))
			{
				return null;
			}
			steps = XVar.Clone(getArrTabs());
			if(1 < MVCFunctions.count(steps))
			{
				this.xt.assign(new XVar("prevStepButton"), new XVar(true));
				this.xt.assign(new XVar("nextStepButton"), new XVar(true));
				this.xt.assign(new XVar("nextstep_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"nextstep", this.id, "\"")));
				this.xt.assign(new XVar("prevstep_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"prevstep", this.id, "\"")));
			}
			this.xt.assign(new XVar("stepnav_attrs"), (XVar)(MVCFunctions.Concat("id=\"stepnav", this.id, "\"")));
			return null;
		}
		protected virtual XVar preparePdfControls()
		{
			if(XVar.Pack(!(XVar)(this.viewPdfEnabled)))
			{
				return null;
			}
			if(XVar.Pack(this.pdfMode))
			{
				return null;
			}
			this.controlsMap.InitAndSetArrayItem(XVar.Array(), "printPdf");
			this.controlsMap.InitAndSetArrayItem(this.pageType, "printPdf", "pageType");
			this.xt.assign(new XVar("pdflink_block"), new XVar(true));
			return null;
		}
		public virtual XVar formatReportFieldValue(dynamic _param_field, dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			if((XVar)(this.format == "excel")  || (XVar)(this.format == "word"))
			{
				return getExportValue((XVar)(field), (XVar)(data), (XVar)(keylink));
			}
			return showDBValue((XVar)(field), (XVar)(data), (XVar)(keylink));
		}
		public virtual XVar getMasterTableInfo(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(table == XVar.Pack(""))
			{
				table = XVar.Clone(this.masterTable);
			}
			return getMasterTableInfoByPSet((XVar)(this.tName), (XVar)(table), (XVar)(this.pSet));
		}
		protected virtual XVar getMasterTableInfoByPSet(dynamic _param_tName, dynamic _param_mtName, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic mtName = XVar.Clone(_param_mtName);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic masterTablesInfoArr = XVar.Array();
			masterTablesInfoArr = XVar.Clone(pSet.getMasterTablesArr((XVar)(tName)));
			if(XVar.Pack(!(XVar)(masterTablesInfoArr)))
			{
				return XVar.Array();
			}
			foreach (KeyValuePair<XVar, dynamic> masterTableData in masterTablesInfoArr.GetEnumerator())
			{
				if(mtName == masterTableData.Value["mDataSourceTable"])
				{
					return masterTableData.Value;
				}
			}
			return XVar.Array();
		}
		public virtual XVar getSearchObject()
		{
			return SearchClause.getSearchObject((XVar)(this.tName), (XVar)(this.dashTName), (XVar)(this.sessionPrefix), (XVar)(this.cipherer), (XVar)(this.searchSavingEnabled), (XVar)(this.pSet));
		}
		public virtual XVar displayMenu(dynamic _param_menuTag, dynamic _param_menuType)
		{
			#region pass-by-value parameters
			dynamic menuTag = XVar.Clone(_param_menuTag);
			dynamic menuType = XVar.Clone(_param_menuType);
			#endregion

			dynamic countGroups = null, countLinks = null, mainmenu = XVar.Array(), menuMode = null, menuName = null, menuRoot = null, menufile = null, peers = XVar.Array(), showMenuCollapseExpandAll = null;
			XTempl xt;
			if(menuTag != Constants.WELCOME_MENU)
			{
				menuName = XVar.Clone(this.pSet.getMenuName((XVar)(this.xt.template_file), (XVar)(menuTag), (XVar)(menuType)));
			}
			else
			{
				menuName = XVar.Clone(menuTag);
			}
			GlobalVars.menuStyle = XVar.Clone(this.pSet.getMenuStyle((XVar)(this.xt.template_file), (XVar)(menuTag), (XVar)(menuType)));
			if(XVar.Pack(isAdminTable()))
			{
				menuName = new XVar("adminarea");
			}
			xt = XVar.UnPackXTempl(new XTempl());
			xt.assign(new XVar("menuName"), (XVar)(menuName));
			xt.assign(new XVar("menustyle"), (XVar)((XVar.Pack(GlobalVars.menuStyle) ? XVar.Pack("second") : XVar.Pack("main"))));
			if(menuType == "quickjump")
			{
				menuMode = new XVar(Constants.MENU_QUICKJUMP);
			}
			else
			{
				if(menuType == "horizontal")
				{
					menuMode = new XVar(Constants.MENU_HORIZONTAL);
				}
				else
				{
					menuMode = new XVar(Constants.MENU_VERTICAL);
				}
			}
			if(XVar.Pack(!(XVar)(isAdminTable())))
			{
				if(menuMode != Constants.MENU_QUICKJUMP)
				{
					if(XVar.Pack(ProjectSettings.isMenuTreelike((XVar)(menuName))))
					{
						if(Constants.MENU_VERTICAL == menuMode)
						{
							xt.assign(new XVar("treeLikeTypeMenu"), new XVar(true));
						}
						else
						{
							xt.assign(new XVar("simpleTypeMenu"), new XVar(true));
						}
					}
					else
					{
						if(XVar.Pack(!(XVar)(mobileTemplateMode())))
						{
							xt.assign(new XVar("simpleTypeMenu"), new XVar(true));
						}
						else
						{
							xt.assign(new XVar("treeLikeTypeMenu"), new XVar(true));
						}
					}
				}
				if((XVar)((XVar)(this.pageType == Constants.PAGE_MENU)  && (XVar)(CommonFunctions.IsAdmin()))  && (XVar)(!(XVar)(mobileTemplateMode())))
				{
					xt.assign(new XVar("adminarea_link"), new XVar(true));
				}
			}
			else
			{
				xt.assign(new XVar("adminAreaTypeMenu"), new XVar(true));
			}
			menuRoot = XVar.Clone(getMenuRoot((XVar)(menuName), (XVar)(menuMode)));
			MenuItem.setMenuSession();
			if((XVar)(getLayoutVersion() == 3)  && (XVar)(menuRoot.isDrillDown()))
			{
				peers = XVar.Clone(prepareActiveMenuBranch((XVar)(menuRoot), (XVar)(xt)));
				menuRoot.setCurrMenuElem((XVar)(xt));
			}
			else
			{
				menuRoot.assignMenuAttrsToTempl((XVar)(xt));
				menuRoot.setCurrMenuElem((XVar)(xt));
			}
			xt.assign(new XVar("mainmenu_block"), new XVar(true));
			mainmenu = XVar.Clone(XVar.Array());
			if(XVar.Pack(CommonFunctions.isEnableSection508()))
			{
				mainmenu.InitAndSetArrayItem("<a name=\"skipmenu\"></a>", "begin");
			}
			mainmenu.InitAndSetArrayItem("", "end");
			countLinks = new XVar(0);
			countGroups = new XVar(0);
			showMenuCollapseExpandAll = new XVar(false);
			foreach (KeyValuePair<XVar, dynamic> val in menuRoot.children.GetEnumerator())
			{
				if(XVar.Pack(val.Value.showAsLink))
				{
					countLinks++;
				}
				if(XVar.Pack(val.Value.showAsGroup))
				{
					if(XVar.Pack(MVCFunctions.count(val.Value.children)))
					{
						showMenuCollapseExpandAll = new XVar(true);
					}
					countGroups++;
				}
			}
			xt.assign(new XVar("menu_collapse_expand_all"), (XVar)(showMenuCollapseExpandAll));
			xt.assignbyref(new XVar("mainmenu_block"), (XVar)(mainmenu));
			menufile = XVar.Clone(menuName);
			if(getLayoutVersion() == 1)
			{
				menufile = XVar.Clone(MVCFunctions.Concat("old", menuName));
			}
			if(getLayoutVersion() == 3)
			{
				menufile = XVar.Clone(MVCFunctions.Concat("bs", menuName));
			}
			if(Constants.MENU_QUICKJUMP == menuMode)
			{
				menufile = MVCFunctions.Concat(menufile, "_", "mainmenu_quickjump.htm");
			}
			else
			{
				if(Constants.MENU_HORIZONTAL == menuMode)
				{
					menufile = MVCFunctions.Concat(menufile, "_", "mainmenu_horiz.htm");
				}
				else
				{
					if((XVar)(mobileTemplateMode())  && (XVar)(getLayoutVersion() != 1))
					{
						menufile = MVCFunctions.Concat(menufile, "_", "mainmenu_m.htm");
					}
					else
					{
						if((XVar)(getLayoutVersion() == 3)  && (XVar)(menuName != Constants.WELCOME_MENU))
						{
							if(XVar.Pack(ProjectSettings.isMenuTreelike((XVar)(menuName))))
							{
								menufile = MVCFunctions.Concat(menufile, "_", "mainmenu_tree.htm");
							}
							else
							{
								menufile = MVCFunctions.Concat(menufile, "_", "mainmenu_horiz.htm");
							}
						}
						else
						{
							menufile = MVCFunctions.Concat(menufile, "_", "mainmenu.htm");
						}
					}
				}
			}
			xt.load_template((XVar)(menufile));
			if(XVar.Pack(peers))
			{
				dynamic menuContent = XVar.Array();
				menuContent = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> p in peers.GetEnumerator())
				{
					menuContent.InitAndSetArrayItem(xt.fetch_loaded((XVar)(MVCFunctions.Concat("item", p.Value.id, "_menulink"))), null);
				}
				xt.assign(new XVar("active_submenu"), (XVar)(MVCFunctions.implode(new XVar(""), (XVar)(menuContent))));
			}
			xt.display_loaded();
			return null;
		}
		protected virtual XVar getMenuItemDetailPeersData(dynamic _param_mTName, dynamic _param_mType, dynamic _param_tName, dynamic _param_pType)
		{
			#region pass-by-value parameters
			dynamic mTName = XVar.Clone(_param_mTName);
			dynamic mType = XVar.Clone(_param_mType);
			dynamic tName = XVar.Clone(_param_tName);
			dynamic pType = XVar.Clone(_param_pType);
			#endregion

			dynamic mPSet = null, peers = XVar.Array();
			mPSet = XVar.Clone(new ProjectSettings((XVar)(mTName), (XVar)(mType)));
			peers = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> dt in mPSet.getDetailTablesArr().GetEnumerator())
			{
				dynamic caption = null, detailsData = XVar.Array(), href = null, keyLabelTemplates = XVar.Array(), labelTemplate = null, mKeys = XVar.Array(), masterRecordData = XVar.Array(), var_type = null;
				if((XVar)(dt.Value["dDataSourceTable"] == tName)  && (XVar)(dt.Value["dType"] == pType))
				{
					continue;
				}
				if(dt.Value["dType"] == Constants.PAGE_LIST)
				{
					var_type = new XVar("List");
				}
				else
				{
					if(dt.Value["dType"] == Constants.PAGE_CHART)
					{
						var_type = new XVar("Chart");
					}
					else
					{
						var_type = new XVar("Report");
					}
				}
				if(XVar.Pack(!(XVar)(isUserHaveTablePerm((XVar)(dt.Value["dDataSourceTable"]), (XVar)(var_type)))))
				{
					continue;
				}
				mKeys = XVar.Clone(XVar.Array());
				masterRecordData = XVar.Clone(XSession.Session[MVCFunctions.Concat(mTName, "_masterRecordData")]);
				detailsData = XVar.Clone(XVar.Array());
				if(XVar.Pack(!(XVar)(masterRecordData)))
				{
					masterRecordData = XVar.Clone(getMasterRecord());
				}
				keyLabelTemplates = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> dk in dt.Value["masterKeys"].GetEnumerator())
				{
					mKeys.InitAndSetArrayItem(MVCFunctions.Concat("masterkey", dk.Key + 1, "=", MVCFunctions.RawUrlEncode((XVar)(masterRecordData[dk.Value]))), null);
					keyLabelTemplates.InitAndSetArrayItem(MVCFunctions.Concat("{%", MVCFunctions.GoodFieldName((XVar)(dt.Value["detailKeys"][dk.Key])), "}"), null);
					detailsData.InitAndSetArrayItem(masterRecordData[dk.Value], dt.Value["detailKeys"][dk.Key]);
				}
				href = XVar.Clone(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(dt.Value["dDataSourceTable"]))), (XVar)(dt.Value["dType"])));
				href = MVCFunctions.Concat(href, "?", MVCFunctions.implode(new XVar("&"), (XVar)(mKeys)), "&mastertable=", MVCFunctions.RawUrlEncode((XVar)(mTName)));
				labelTemplate = XVar.Clone(Labels.getBreadcrumbsLabelTempl((XVar)(dt.Value["dDataSourceTable"]), (XVar)(mTName)));
				if(labelTemplate == XVar.Pack(""))
				{
					labelTemplate = XVar.Clone(MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(dt.Value["dDataSourceTable"])))), " [", MVCFunctions.implode(new XVar(", "), (XVar)(keyLabelTemplates)), "]"));
				}
				caption = XVar.Clone(calcPageTitle((XVar)(labelTemplate), (XVar)(detailsData), (XVar)(mTName), (XVar)(masterRecordData), (XVar)(new ProjectSettings((XVar)(dt.Value["dDataSourceTable"])))));
				peers.InitAndSetArrayItem(new XVar("title", caption, "href", href), null);
			}
			return peers;
		}
		protected virtual XVar getMasterDetailMenuItems(dynamic menuRoot, ref dynamic currentMenuItem)
		{
			dynamic caption = null, detailsData = XVar.Array(), href = null, itemData = XVar.Array(), items = XVar.Array(), keyLabelTemplates = XVar.Array(), labelTemplate = null, mTName = null, masterRecordData = XVar.Array(), masterTableData = XVar.Array(), pType = null, sessionPrefix = null, tName = null;
			ProjectSettings pSet;
			items = XVar.Clone(XVar.Array());
			pSet = XVar.UnPackProjectSettings(this.pSet);
			tName = XVar.Clone(this.tName);
			caption = XVar.Clone(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(tName)))));
			pType = XVar.Clone(this.pageType);
			sessionPrefix = XVar.Clone(this.sessionPrefix);
			while(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(sessionPrefix, "_mastertable"))))
			{
				mTName = XVar.Clone(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_mastertable")]);
				masterTableData = XVar.Clone(getMasterTableInfoByPSet((XVar)(tName), (XVar)(mTName), (XVar)(pSet)));
				if(XVar.Pack(!(XVar)(MVCFunctions.count(masterTableData))))
				{
					break;
				}
				masterRecordData = XVar.Clone(XSession.Session[MVCFunctions.Concat(mTName, "_masterRecordData")]);
				detailsData = XVar.Clone(XVar.Array());
				keyLabelTemplates = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> dk in masterTableData["masterKeys"].GetEnumerator())
				{
					keyLabelTemplates.InitAndSetArrayItem(MVCFunctions.Concat("{%", MVCFunctions.GoodFieldName((XVar)(masterTableData["detailKeys"][dk.Key])), "}"), null);
					detailsData.InitAndSetArrayItem(masterRecordData[dk.Value], masterTableData["detailKeys"][dk.Key]);
				}
				if(XVar.Pack(currentMenuItem))
				{
					itemData = XVar.Clone(new XVar("isMenuItem", true, "menuItem", currentMenuItem, "title", currentMenuItem.title));
				}
				else
				{
					caption = XVar.Clone(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(tName)))));
					href = XVar.Clone(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(tName))), (XVar)(pType)));
					itemData = XVar.Clone(new XVar("isMenuItem", false, "menuItem", new XVar("href", href), "title", caption));
				}
				if(XVar.Pack(!(XVar)(MVCFunctions.count(items))))
				{
					dynamic otherDetailsData = null;
					otherDetailsData = XVar.Clone(getMenuItemDetailPeersData((XVar)(mTName), (XVar)(masterTableData["type"]), (XVar)(tName), (XVar)(pType)));
					if(0 < MVCFunctions.count(otherDetailsData))
					{
						itemData.InitAndSetArrayItem(otherDetailsData, "detailPeers");
					}
				}
				labelTemplate = XVar.Clone(Labels.getBreadcrumbsLabelTempl((XVar)(tName), (XVar)(mTName)));
				if(labelTemplate == XVar.Pack(""))
				{
					labelTemplate = XVar.Clone(MVCFunctions.Concat(itemData["title"], " [", MVCFunctions.implode(new XVar(", "), (XVar)(keyLabelTemplates)), "]"));
				}
				itemData.InitAndSetArrayItem(calcPageTitle((XVar)(labelTemplate), (XVar)(detailsData), (XVar)(mTName), (XVar)(masterRecordData), (XVar)(pSet)), "title");
				items.InitAndSetArrayItem(itemData, null);
				currentMenuItem = XVar.Clone(menuRoot.getItemByTypeAndTable((XVar)(mTName), (XVar)(masterTableData["type"])));
				tName = XVar.Clone(mTName);
				pType = XVar.Clone(masterTableData["type"]);
				sessionPrefix = XVar.Clone(mTName);
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(mTName), (XVar)(masterTableData["type"])));
			}
			if(XVar.Pack(MVCFunctions.count(items)))
			{
				dynamic crumb = XVar.Array();
				crumb = XVar.Clone(XVar.Array());
				if(XVar.Pack(currentMenuItem))
				{
					crumb = XVar.Clone(new XVar("isMenuItem", true, "menuItem", currentMenuItem, "title", currentMenuItem.title));
				}
				else
				{
					href = XVar.Clone(MVCFunctions.GetTableLink((XVar)(CommonFunctions.GetTableURL((XVar)(tName))), (XVar)(pType)));
					crumb = XVar.Clone(new XVar("isMenuItem", false, "menuItem", new XVar("href", href), "title", caption));
				}
				labelTemplate = XVar.Clone(Labels.getBreadcrumbsLabelTempl((XVar)(tName)));
				if(labelTemplate != XVar.Pack(""))
				{
					crumb.InitAndSetArrayItem(calcPageTitle((XVar)(labelTemplate)), "title");
				}
				items.InitAndSetArrayItem(crumb, null);
			}
			return items;
		}
		protected virtual XVar prepareBreadcrumbs(dynamic _param_menuId)
		{
			#region pass-by-value parameters
			dynamic menuId = XVar.Clone(_param_menuId);
			#endregion

			dynamic attrs = XVar.Array(), breadChain = XVar.Array(), breadcrumbs = XVar.Array(), crumb = XVar.Array(), currentMenuItem = null, detailItem = null, detailPeers = null, dropItems = XVar.Array(), firstShowPeersIndex = null, href = null, i = null, item = null, itemData = XVar.Array(), menuRoot = null, peers = XVar.Array(), title = null;
			if(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT)
			{
				return null;
			}
			detailItem = XVar.Clone(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_mastertable")));
			menuRoot = XVar.Clone(getMenuRoot((XVar)(menuId), new XVar(Constants.MENU_HORIZONTAL)));
			MenuItem.setMenuSession();
			currentMenuItem = XVar.Clone(menuRoot.getCurrentItem((XVar)(XSession.Session["menuItemId"])));
			if((XVar)(!(XVar)(currentMenuItem))  && (XVar)(!(XVar)(detailItem)))
			{
				return null;
			}
			if((XVar)(currentMenuItem)  && (XVar)(!(XVar)(detailItem)))
			{
				if(XVar.Pack(!(XVar)(currentMenuItem.parentItem)))
				{
					return null;
				}
			}
			this.xt.assign(new XVar("breadcrumbs"), new XVar(true));
			breadChain = XVar.Clone(getMasterDetailMenuItems((XVar)(menuRoot), ref currentMenuItem));
			firstShowPeersIndex = XVar.Clone(MVCFunctions.count(breadChain));
			if((XVar)(XVar.Pack(0) < firstShowPeersIndex)  && (XVar)(breadChain[firstShowPeersIndex - 1]["isMenuItem"]))
			{
				currentMenuItem = XVar.Clone(breadChain[firstShowPeersIndex - 1]["menuItem"].parentItem);
			}
			if(XVar.Pack(currentMenuItem))
			{
				dynamic labelTemplate = null;
				while(XVar.Pack(currentMenuItem.parentItem))
				{
					crumb = XVar.Clone(new XVar("isMenuItem", true, "menuItem", currentMenuItem));
					labelTemplate = XVar.Clone(Labels.getBreadcrumbsLabelTempl((XVar)(currentMenuItem.getTable()), new XVar(""), (XVar)(currentMenuItem.getPageType())));
					if(labelTemplate != XVar.Pack(""))
					{
						crumb.InitAndSetArrayItem(calcPageTitle((XVar)(labelTemplate)), "title");
					}
					else
					{
						crumb.InitAndSetArrayItem(currentMenuItem.title, "title");
					}
					breadChain.InitAndSetArrayItem(crumb, null);
					currentMenuItem = XVar.Clone(currentMenuItem.parentItem);
				}
			}
			breadcrumbs = XVar.Clone(XVar.Array());
			i = XVar.Clone(MVCFunctions.count(breadChain) - 1);
			for(;XVar.Pack(0) <= i; --(i))
			{
				itemData = XVar.Clone(breadChain[i]);
				crumb = XVar.Clone(XVar.Array());
				item = new XVar(null);
				attrs = XVar.Clone(XVar.Array());
				if(XVar.Pack(itemData["isMenuItem"]))
				{
					item = XVar.Clone(itemData["menuItem"]);
					attrs = XVar.Clone(item.getMenuItemAttributes());
					href = XVar.Clone(attrs["href"]);
				}
				else
				{
					href = XVar.Clone(itemData["menuItem"]["href"]);
				}
				title = XVar.Clone(itemData["title"]);
				if((XVar)(i < firstShowPeersIndex)  && (XVar)(i))
				{
					crumb.InitAndSetArrayItem(MVCFunctions.Concat("href=\"", href, (XVar.Pack(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(href), new XVar("?"))), XVar.Pack(false))) ? XVar.Pack("&a=return") : XVar.Pack("?a=return")), "\""), "crumb_attrs");
				}
				else
				{
					crumb.InitAndSetArrayItem(MVCFunctions.Concat("href=\"", href, "\""), "crumb_attrs");
				}
				if((XVar)(XVar.Pack(0) < firstShowPeersIndex)  && (XVar)(i == XVar.Pack(0)))
				{
					crumb.InitAndSetArrayItem(true, "crumb_title_span");
				}
				else
				{
					crumb.InitAndSetArrayItem(true, "crumb_title_link");
				}
				crumb.InitAndSetArrayItem(title, "crumb_title");
				if((XVar)((XVar)(i < firstShowPeersIndex)  && (XVar)(!(XVar)(MVCFunctions.count(itemData["detailPeers"]))))  || (XVar)((XVar)(isAdminTable())  && (XVar)(CommonFunctions.GetGlobalData(new XVar("nLoginMethod"), new XVar(0)) == Constants.SECURITY_AD)))
				{
					breadcrumbs.InitAndSetArrayItem(crumb, null);
					continue;
				}
				if(XVar.Pack(item == null))
				{
					continue;
				}
				dropItems = XVar.Clone(XVar.Array());
				peers = XVar.Clone(XVar.Array());
				detailPeers = XVar.Clone(0 < MVCFunctions.count(itemData["detailPeers"]));
				if(XVar.Pack(detailPeers))
				{
					peers = XVar.Clone(itemData["detailPeers"]);
				}
				else
				{
					item.parentItem.getItemDescendants((XVar)(peers));
				}
				if((XVar)(1 < MVCFunctions.count(peers))  || (XVar)(detailPeers))
				{
					foreach (KeyValuePair<XVar, dynamic> p in peers.GetEnumerator())
					{
						if(XVar.Pack(detailPeers))
						{
							dropItems.InitAndSetArrayItem(MVCFunctions.Concat("<li><a href=\"", p.Value["href"], "\">", p.Value["title"], "</a></li>"), null);
							continue;
						}
						if(p.Value.id == item.id)
						{
							continue;
						}
						attrs = XVar.Clone(XVar.Array());
						if((XVar)(!(XVar)(p.Value.isShowAsLink()))  && (XVar)(p.Value.isShowAsGroup()))
						{
							dynamic childWithLink = null;
							childWithLink = XVar.Clone(p.Value.getFirstChildWithLink());
							if(XVar.Pack(childWithLink))
							{
								attrs = XVar.Clone(childWithLink.getMenuItemAttributes());
							}
						}
						else
						{
							if(XVar.Pack(p.Value.isShowAsLink()))
							{
								attrs = XVar.Clone(p.Value.getMenuItemAttributes());
							}
						}
						if(XVar.Pack(MVCFunctions.count(attrs)))
						{
							dropItems.InitAndSetArrayItem(MVCFunctions.Concat("<li><a href=\"", attrs["href"], "\">", p.Value.title, "</a></li>"), null);
						}
					}
					if(0 < MVCFunctions.count(dropItems))
					{
						crumb["crumb_title"] = MVCFunctions.Concat(crumb["crumb_title"], "<span class=\"caret\"></span>");
						crumb["crumb_attrs"] = MVCFunctions.Concat(crumb["crumb_attrs"], " class=\"dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"");
						crumb.InitAndSetArrayItem("dropdown", "crumb_item_class");
						crumb.InitAndSetArrayItem(MVCFunctions.Concat("<ul class=\"dropdown-menu\">", MVCFunctions.implode(new XVar(""), (XVar)(dropItems)), "</ul>"), "crumb_dropdown");
					}
				}
				breadcrumbs.InitAndSetArrayItem(crumb, null);
			}
			this.xt.assign_loopsection(new XVar("crumb"), (XVar)(breadcrumbs));
			return null;
		}
		protected virtual XVar prepareActiveMenuBranch(dynamic _param_menuRoot, dynamic _param_xt_packed)
		{
			#region packeted values
			XTempl _param_xt = XVar.UnPackXTempl(_param_xt_packed);
			#endregion

			#region pass-by-value parameters
			dynamic menuRoot = XVar.Clone(_param_menuRoot);
			XTempl xt = XVar.Clone(_param_xt);
			#endregion

			dynamic currentMenuItem = null, parentItem = null, peerIds = XVar.Array(), peers = XVar.Array();
			parentItem = XVar.Clone(menuRoot);
			currentMenuItem = XVar.Clone(menuRoot.getCurrentItem((XVar)(XSession.Session["menuItemId"])));
			if(XVar.Pack(currentMenuItem))
			{
				parentItem = XVar.Clone(currentMenuItem.parentItem);
			}
			peers = XVar.Clone(XVar.Array());
			parentItem.getItemDescendants((XVar)(peers), new XVar(1));
			peerIds = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> p in peers.GetEnumerator())
			{
				peerIds.InitAndSetArrayItem(true, p.Value.id);
			}
			menuRoot.assignMenuAttrsToTempl((XVar)(xt), (XVar)(peerIds));
			if((XVar)(peers)  && (XVar)(parentItem.id != menuRoot.id))
			{
				peers = XVar.Clone(XVar.Array());
				parentItem.getItemDescendants((XVar)(peers), new XVar(0));
				return peers;
			}
			else
			{
				return XVar.Array();
			}
			return null;
		}
		public virtual XVar getMenuRoot(dynamic _param_menuId, dynamic _param_menuMode)
		{
			#region pass-by-value parameters
			dynamic menuId = XVar.Clone(_param_menuId);
			dynamic menuMode = XVar.Clone(_param_menuMode);
			#endregion

			if(XVar.Pack(!(XVar)(this.menuRoots.KeyExists(MVCFunctions.Concat(menuMode, "-", menuId)))))
			{
				dynamic menuMap = null, menuNodes = null, nullParent = null, rootInfoArr = null;
				menuNodes = XVar.Clone(getMenuNodes((XVar)(menuId)));
				nullParent = new XVar(null);
				rootInfoArr = XVar.Clone(new XVar("id", 0, "href", ""));
				GlobalVars.menuNodesIndex = new XVar(0);
				menuMap = XVar.Clone(XVar.Array());
				this.menuRoots.InitAndSetArrayItem(new MenuItem((XVar)(rootInfoArr), (XVar)(menuNodes), (XVar)(nullParent), (XVar)(menuMap), this, (XVar)(menuId), (XVar)(menuMode)), MVCFunctions.Concat(menuMode, "-", menuId));
			}
			return this.menuRoots[MVCFunctions.Concat(menuMode, "-", menuId)];
		}
		public virtual XVar fillControlFlags(dynamic _param_field, dynamic _param_required = null)
		{
			#region default values
			if(_param_required as Object == null) _param_required = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic required = XVar.Clone(_param_required);
			#endregion

			if((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)((XVar)(required)  || (XVar)(this.pSet.isRequired((XVar)(field)))))
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field)), "_label")), (XVar)(new XVar("end", "&nbsp;<span class=\"icon-required\"></span>")));
			}
			else
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(field)), "_label")), new XVar(true));
			}
			return null;
		}
		public virtual XVar assignDetailsTablesBadgeColors()
		{
			dynamic colors = XVar.Array(), styles = XVar.Array();
			colors = XVar.Clone(XVar.Array());
			colors.InitAndSetArrayItem("lightslategrey", null);
			colors.InitAndSetArrayItem("dodgerblue", null);
			colors.InitAndSetArrayItem("maroon", null);
			colors.InitAndSetArrayItem("teal", null);
			colors.InitAndSetArrayItem("orange", null);
			colors.InitAndSetArrayItem("chocolate", null);
			colors.InitAndSetArrayItem("crimson", null);
			colors.InitAndSetArrayItem("indianred", null);
			colors.InitAndSetArrayItem("slateblue", null);
			colors.InitAndSetArrayItem("mediumseagreen", null);
			colors.InitAndSetArrayItem("darkolivegreen", null);
			styles = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> dt in this.allDetailsTablesArr.GetEnumerator())
			{
				styles.InitAndSetArrayItem(MVCFunctions.Concat(".", dt.Value["dShortTable"], "_badge { background-color: ", colors[MVCFunctions.rand(new XVar(0), (XVar)(MVCFunctions.count(colors) - 1))], "; }"), null);
			}
			this.xt.assign(new XVar("containerCss"), (XVar)(MVCFunctions.Concat(this.xt.getVar(new XVar("containerCss")), MVCFunctions.implode(new XVar(""), (XVar)(styles)))));
			return null;
		}
		protected virtual XVar setDetailsBadgeStyles()
		{
			if(XVar.Pack(!(XVar)(detailsInGridAvailable())))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> detData in this.allDetailsTablesArr.GetEnumerator())
			{
				dynamic color = null, dSet = null;
				if(XVar.Pack(!(XVar)(detData.Value["dispChildCount"])))
				{
					continue;
				}
				dSet = XVar.Clone(new ProjectSettings((XVar)(detData.Value["dDataSourceTable"]), (XVar)(detData.Value["dType"])));
				color = XVar.Clone(dSet.getDetailsBadgeColor());
				if(XVar.Pack(MVCFunctions.strlen((XVar)(color))))
				{
					this.row_css_rules = XVar.Clone(MVCFunctions.Concat(".badge.badge.", detData.Value["dShortTable"], "_badge { background-color: #", color, " }\n", this.row_css_rules));
				}
			}
			return null;
		}
		public virtual XVar detailsInGridAvailable()
		{
			dynamic dPset = null, detAddAvailabel = null, detEditAvailabel = null, detListAvailabel = null, detTablePermis = XVar.Array(), i = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.allDetailsTablesArr); i++)
			{
				dPset = XVar.Clone(new ProjectSettings((XVar)(this.allDetailsTablesArr[i]["dDataSourceTable"])));
				detTablePermis = XVar.Clone(this.permis[this.allDetailsTablesArr[i]["dDataSourceTable"]]);
				detListAvailabel = XVar.Clone((XVar)(dPset.hasListPage())  && (XVar)(detTablePermis["search"]));
				detAddAvailabel = XVar.Clone((XVar)(dPset.hasAddPage())  && (XVar)(detTablePermis["add"]));
				detEditAvailabel = XVar.Clone((XVar)(dPset.hasEditPage())  && (XVar)(detTablePermis["edit"]));
				if((XVar)(detListAvailabel)  || (XVar)((XVar)((XVar)(this.detailsLinksOnList == Constants.DL_INDIVIDUAL)  || (XVar)(MVCFunctions.count(this.allDetailsTablesArr) == 1))  && (XVar)((XVar)(detAddAvailabel)  || (XVar)(detEditAvailabel))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar mobileTemplateMode()
		{
			return (XVar)(CommonFunctions.mobileDeviceDetected())  && (XVar)(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT);
		}
		protected virtual XVar getColumnsToHide()
		{
			return getHiddenColumnsByDevice();
		}
		protected static XVar deviceClassToMacro(dynamic _param_deviceClass)
		{
			#region pass-by-value parameters
			dynamic deviceClass = XVar.Clone(_param_deviceClass);
			#endregion

			if((XVar)(deviceClass == Constants.TABLET_10_IN)  || (XVar)(deviceClass == Constants.TABLET_7_IN))
			{
				return 1;
			}
			if((XVar)(deviceClass == Constants.SMARTPHONE_LANDSCAPE)  || (XVar)(deviceClass == Constants.SMARTPHONE_PORTRAIT))
			{
				return 2;
			}
			return 0;
		}
		protected virtual XVar getCombinedHiddenColumns()
		{
			dynamic columnsByDeviceEnabled = null, devices = XVar.Array(), hideColumns = XVar.Array(), logger = null, ret = XVar.Array();
			if(XVar.Pack(!(XVar)(this.pSet.isAllowShowHideFields())))
			{
				return getHiddenColumnsByDevice();
			}
			logger = XVar.Clone(new paramsLogger((XVar)(this.tName), new XVar(Constants.SHFIELDS_PARAMS_TYPE)));
			hideColumns = XVar.Clone(logger.getShowHideData());
			columnsByDeviceEnabled = XVar.Clone(this.pSet.columnsByDeviceEnabled());
			ret = XVar.Clone(XVar.Array());
			devices = XVar.Clone(new XVar(0, Constants.DESKTOP, 1, Constants.TABLET_10_IN, 2, Constants.SMARTPHONE_LANDSCAPE, 3, Constants.SMARTPHONE_PORTRAIT, 4, Constants.TABLET_7_IN));
			foreach (KeyValuePair<XVar, dynamic> d in devices.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(columnsByDeviceEnabled)))
				{
					ret.InitAndSetArrayItem(hideColumns[0], d.Value);
				}
				else
				{
					if(XVar.Pack(hideColumns[deviceClassToMacro((XVar)(d.Value))]))
					{
						ret.InitAndSetArrayItem(hideColumns[deviceClassToMacro((XVar)(d.Value))], d.Value);
					}
					else
					{
						ret.InitAndSetArrayItem(MVCFunctions.array_keys((XVar)(this.pSet.getHiddenGoodNameFields((XVar)(d.Value)))), d.Value);
					}
				}
			}
			return ret;
		}
		protected virtual XVar getHiddenColumnsByDevice()
		{
			dynamic columnsToHide = XVar.Array(), devices = XVar.Array();
			columnsToHide = XVar.Clone(XVar.Array());
			devices = XVar.Clone(new XVar(0, Constants.TABLET_7_IN, 1, Constants.SMARTPHONE_PORTRAIT, 2, Constants.SMARTPHONE_LANDSCAPE, 3, Constants.TABLET_10_IN, 4, Constants.DESKTOP));
			foreach (KeyValuePair<XVar, dynamic> d in devices.GetEnumerator())
			{
				columnsToHide.InitAndSetArrayItem(MVCFunctions.array_keys((XVar)(this.pSet.getHiddenGoodNameFields((XVar)(d.Value)))), d.Value);
			}
			return columnsToHide;
		}
		public static XVar sendEmailByTemplate(dynamic _param_toEmail, dynamic _param_template, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic toEmail = XVar.Clone(_param_toEmail);
			dynamic template = XVar.Clone(_param_template);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic body = null, firstRowEndPos = null, subject = null, templatefile = null;
			data.InitAndSetArrayItem(CommonFunctions.GetSiteUrl(), "url");
			if(XVar.Pack(!(XVar)(data.KeyExists("loginUrl"))))
			{
				data.InitAndSetArrayItem(MVCFunctions.Concat(CommonFunctions.GetSiteUrl(), "/login"), "loginUrl");
			}
			templatefile = XVar.Clone(MVCFunctions.Concat("email/", CommonFunctions.mlang_getcurrentlang(), "/", template, ".txt"));
			if(XVar.Pack(MVCFunctions.file_exists((XVar)(MVCFunctions.getabspath((XVar)(templatefile))))))
			{
				body = XVar.Clone(MVCFunctions.myfile_get_contents((XVar)(MVCFunctions.getabspath((XVar)(templatefile))), new XVar("r")));
			}
			else
			{
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> value in data.GetEnumerator())
			{
				body = XVar.Clone(MVCFunctions.preg_replace((XVar)(MVCFunctions.Concat("/%", value.Key, "%/i")), (XVar)(value.Value), (XVar)(body)));
			}
			subject = new XVar("");
			if(XVar.Pack(firstRowEndPos = XVar.Clone(MVCFunctions.strpos((XVar)(body), new XVar("\r")))))
			{
				subject = XVar.Clone(MVCFunctions.substr((XVar)(body), new XVar(0), (XVar)(firstRowEndPos)));
				body = XVar.Clone(MVCFunctions.substr((XVar)(body), (XVar)(firstRowEndPos + 1)));
			}
			return MVCFunctions.runner_mail((XVar)(new XVar("to", toEmail, "subject", subject, "body", body)));
		}
		protected virtual XVar getSubsetSQLComponents()
		{
			dynamic mandatoryHaving = XVar.Array(), mandatoryWhere = XVar.Array(), optionalHaving = XVar.Array(), optionalWhere = XVar.Array(), sqlParts = XVar.Array(), whereComponents = XVar.Array();
			sqlParts = XVar.Clone(this.gQuery.getSqlComponents());
			mandatoryWhere = XVar.Clone(XVar.Array());
			mandatoryHaving = XVar.Clone(XVar.Array());
			optionalWhere = XVar.Clone(XVar.Array());
			optionalHaving = XVar.Clone(XVar.Array());
			whereComponents = XVar.Clone(getWhereComponents());
			if(XVar.Pack(whereComponents["searchUnionRequired"]))
			{
				optionalWhere.InitAndSetArrayItem(whereComponents["searchWhere"], null);
				optionalHaving.InitAndSetArrayItem(whereComponents["searchHaving"], null);
			}
			else
			{
				mandatoryWhere.InitAndSetArrayItem(whereComponents["searchWhere"], null);
				mandatoryHaving.InitAndSetArrayItem(whereComponents["searchHaving"], null);
			}
			sqlParts["from"] = MVCFunctions.Concat(sqlParts["from"], whereComponents["joinFromPart"]);
			foreach (KeyValuePair<XVar, dynamic> f in whereComponents["filterWhere"].GetEnumerator())
			{
				mandatoryWhere.InitAndSetArrayItem(f.Value, null);
			}
			foreach (KeyValuePair<XVar, dynamic> f in whereComponents["filterHaving"].GetEnumerator())
			{
				mandatoryHaving.InitAndSetArrayItem(f.Value, null);
			}
			mandatoryWhere.InitAndSetArrayItem(getCurrentTabWhere(), null);
			mandatoryWhere.InitAndSetArrayItem(getMasterTableSQLClause(), null);
			return new XVar("sqlParts", sqlParts, "mandatoryWhere", mandatoryWhere, "mandatoryHaving", mandatoryHaving, "optionalWhere", optionalWhere, "optionalHaving", optionalHaving);
		}
		protected virtual XVar getSelectedRecords()
		{
			dynamic keyFields = XVar.Array(), keys = XVar.Array(), selected_recs = XVar.Array();
			selected_recs = XVar.Clone(XVar.Array());
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			if(XVar.Pack(MVCFunctions.postvalue("mdelete")))
			{
				foreach (KeyValuePair<XVar, dynamic> ind in MVCFunctions.EnumeratePOST("mdelete"))
				{
					keys = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> f in keyFields.GetEnumerator())
					{
						keys.InitAndSetArrayItem(MVCFunctions.postvalue("mdelete" + (f.Key + 1))[MVCFunctions.mdeleteIndex((XVar)(ind.Value))], f.Value);
					}
					selected_recs.InitAndSetArrayItem(keys, null);
				}
			}
			else
			{
				if(XVar.Pack(this.selection))
				{
					foreach (KeyValuePair<XVar, dynamic> keyblock in this.selection.GetEnumerator())
					{
						dynamic arr = XVar.Array();
						arr = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(keyblock.Value)));
						if(MVCFunctions.count(arr) < MVCFunctions.count(keyFields))
						{
							continue;
						}
						keys = XVar.Clone(XVar.Array());
						foreach (KeyValuePair<XVar, dynamic> f in keyFields.GetEnumerator())
						{
							keys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(arr[f.Key])), f.Value);
						}
						selected_recs.InitAndSetArrayItem(keys, null);
					}
				}
				else
				{
					return null;
				}
			}
			return selected_recs;
		}
		protected virtual XVar getSelection()
		{
			dynamic selection = XVar.Array();
			return this.selection;
			return null;
		}
		public virtual XVar getTabSQLComponents(dynamic _param_tab)
		{
			#region pass-by-value parameters
			dynamic tab = XVar.Clone(_param_tab);
			#endregion

			dynamic sql = null;
			this.tabChangeling = XVar.Clone(tab);
			sql = XVar.Clone(getSubsetSQLComponents());
			this.tabChangeling = new XVar(null);
			return sql;
		}
		public virtual XVar simpleMode()
		{
			return this.id == 1;
		}
		public virtual XVar displayTabsInPage()
		{
			return simpleMode();
		}
		public virtual XVar updateDetailsTabTitles()
		{
			dynamic i = null, id = null;
			if(XVar.Pack(!(XVar)(this.changeDetailsTabTitles)))
			{
				return null;
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.gridTabs); ++(i))
			{
				id = XVar.Clone(this.gridTabs[i]["tabId"]);
				setTabTitle((XVar)(id), (XVar)(MVCFunctions.Concat(CommonFunctions.GetTableCaption((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))), " - ", getTabTitle((XVar)(id)))));
			}
			return null;
		}
		public virtual XVar shouldDisplayDetailsPage()
		{
			return true;
		}
	}
	public partial class DetailsPreview : RunnerPage
	{
		protected static bool skipDetailsPreviewCtor = false;
		public DetailsPreview(dynamic _param_params)
			:base((XVar)_param_params)
		{
			if(skipDetailsPreviewCtor)
			{
				skipDetailsPreviewCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

		}
		protected override XVar assignSessionPrefix()
		{
			this.sessionPrefix = new XVar("_detailsPreview");
			return null;
		}
	}
}
