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
	public static partial class Settings_dbo__ABCSecurity
	{
		static public void Apply()
		{
			SettingsMap arrAddTabs = SettingsMap.GetArray(), arrEditTabs = SettingsMap.GetArray(), arrGPP = SettingsMap.GetArray(), arrGridTabs = SettingsMap.GetArray(), arrRPP = SettingsMap.GetArray(), arrRegisterTabs = SettingsMap.GetArray(), arrViewTabs = SettingsMap.GetArray(), dIndex = null, detailsParam = SettingsMap.GetArray(), edata = SettingsMap.GetArray(), eventsData = SettingsMap.GetArray(), fdata = SettingsMap.GetArray(), fieldLabelsArray = new XVar(), fieldToolTipsArray = new XVar(), hours = null, intervalData = SettingsMap.GetArray(), masterParams = SettingsMap.GetArray(), pageTitlesArray = new XVar(), placeHoldersArray = new XVar(), popupPagesLayoutNames = SettingsMap.GetArray(), query = null, queryData_Array = new XVar(), reportGroupFields = SettingsMap.GetArray(), rgroupField = SettingsMap.GetArray(), strOriginalDetailsTable = null, tabFields = SettingsMap.GetArray(), table = null, tableKeysArray = new XVar(), tdataArray = new XVar(), tstrOrderBy = null, vdata = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".truncateText"] = true;
			tdataArray["dbo__ABCSecurity"][".NumberOfChars"] = 80;
			tdataArray["dbo__ABCSecurity"][".ShortName"] = "dbo__ABCSecurity";
			tdataArray["dbo__ABCSecurity"][".OwnerID"] = "";
			tdataArray["dbo__ABCSecurity"][".OriginalTable"] = "dbo._ABCSecurity";
			fieldLabelsArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			fieldToolTipsArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			pageTitlesArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			placeHoldersArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			if(CommonFunctions.mlang_getcurrentlang() == "English")
			{
				fieldLabelsArray["dbo__ABCSecurity"]["English"] = SettingsMap.GetArray();
				fieldToolTipsArray["dbo__ABCSecurity"]["English"] = SettingsMap.GetArray();
				placeHoldersArray["dbo__ABCSecurity"]["English"] = SettingsMap.GetArray();
				pageTitlesArray["dbo__ABCSecurity"]["English"] = SettingsMap.GetArray();
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["ID"] = "ID";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["ID"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["ID"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["username"] = "Username";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["username"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["username"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["password"] = "Password";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["password"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["password"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["admin"] = "Admin";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["admin"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["admin"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["byear"] = "Begin Term";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["byear"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["byear"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["eyear"] = "End Term";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["eyear"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["eyear"] = "";
				fieldLabelsArray["dbo__ABCSecurity"]["English"]["role"] = "Role";
				fieldToolTipsArray["dbo__ABCSecurity"]["English"]["role"] = "";
				placeHoldersArray["dbo__ABCSecurity"]["English"]["role"] = "";
				if(XVar.Pack(MVCFunctions.count(fieldToolTipsArray["dbo__ABCSecurity"]["English"])))
				{
					tdataArray["dbo__ABCSecurity"][".isUseToolTips"] = true;
				}
			}
			if(CommonFunctions.mlang_getcurrentlang() == "")
			{
				fieldLabelsArray["dbo__ABCSecurity"][""] = SettingsMap.GetArray();
				fieldToolTipsArray["dbo__ABCSecurity"][""] = SettingsMap.GetArray();
				placeHoldersArray["dbo__ABCSecurity"][""] = SettingsMap.GetArray();
				pageTitlesArray["dbo__ABCSecurity"][""] = SettingsMap.GetArray();
				if(XVar.Pack(MVCFunctions.count(fieldToolTipsArray["dbo__ABCSecurity"][""])))
				{
					tdataArray["dbo__ABCSecurity"][".isUseToolTips"] = true;
				}
			}
			tdataArray["dbo__ABCSecurity"][".NCSearch"] = true;
			tdataArray["dbo__ABCSecurity"][".shortTableName"] = "dbo__ABCSecurity";
			tdataArray["dbo__ABCSecurity"][".nSecOptions"] = 0;
			tdataArray["dbo__ABCSecurity"][".recsPerRowPrint"] = 1;
			tdataArray["dbo__ABCSecurity"][".mainTableOwnerID"] = "";
			tdataArray["dbo__ABCSecurity"][".moveNext"] = 1;
			tdataArray["dbo__ABCSecurity"][".entityType"] = 0;
			tdataArray["dbo__ABCSecurity"][".strOriginalTableName"] = "dbo._ABCSecurity";
			tdataArray["dbo__ABCSecurity"][".showAddInPopup"] = false;
			tdataArray["dbo__ABCSecurity"][".showEditInPopup"] = false;
			tdataArray["dbo__ABCSecurity"][".showViewInPopup"] = false;
			popupPagesLayoutNames = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".popupPagesLayoutNames"] = popupPagesLayoutNames;
			tdataArray["dbo__ABCSecurity"][".fieldsForRegister"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".listAjax"] = false;
			tdataArray["dbo__ABCSecurity"][".audit"] = false;
			tdataArray["dbo__ABCSecurity"][".locking"] = false;
			tdataArray["dbo__ABCSecurity"][".edit"] = true;
			tdataArray["dbo__ABCSecurity"][".afterEditAction"] = 1;
			tdataArray["dbo__ABCSecurity"][".closePopupAfterEdit"] = 1;
			tdataArray["dbo__ABCSecurity"][".afterEditActionDetTable"] = "";
			tdataArray["dbo__ABCSecurity"][".add"] = true;
			tdataArray["dbo__ABCSecurity"][".afterAddAction"] = 1;
			tdataArray["dbo__ABCSecurity"][".closePopupAfterAdd"] = 1;
			tdataArray["dbo__ABCSecurity"][".afterAddActionDetTable"] = "";
			tdataArray["dbo__ABCSecurity"][".list"] = true;
			tdataArray["dbo__ABCSecurity"][".inlineEdit"] = true;
			tdataArray["dbo__ABCSecurity"][".reorderRecordsByHeader"] = true;
			tdataArray["dbo__ABCSecurity"][".exportFormatting"] = 2;
			tdataArray["dbo__ABCSecurity"][".exportDelimiter"] = ",";
			tdataArray["dbo__ABCSecurity"][".inlineAdd"] = true;
			tdataArray["dbo__ABCSecurity"][".view"] = true;
			tdataArray["dbo__ABCSecurity"][".import"] = true;
			tdataArray["dbo__ABCSecurity"][".exportTo"] = true;
			tdataArray["dbo__ABCSecurity"][".printFriendly"] = true;
			tdataArray["dbo__ABCSecurity"][".delete"] = true;
			tdataArray["dbo__ABCSecurity"][".showSimpleSearchOptions"] = false;
			tdataArray["dbo__ABCSecurity"][".allowShowHideFields"] = false;
			tdataArray["dbo__ABCSecurity"][".allowFieldsReordering"] = false;
			tdataArray["dbo__ABCSecurity"][".searchSaving"] = false;
			tdataArray["dbo__ABCSecurity"][".showSearchPanel"] = true;
			tdataArray["dbo__ABCSecurity"][".flexibleSearch"] = true;
			tdataArray["dbo__ABCSecurity"][".isUseAjaxSuggest"] = true;
			tdataArray["dbo__ABCSecurity"][".rowHighlite"] = true;


			tdataArray["dbo__ABCSecurity"][".ajaxCodeSnippetAdded"] = false;
			tdataArray["dbo__ABCSecurity"][".buttonsAdded"] = false;
			tdataArray["dbo__ABCSecurity"][".addPageEvents"] = false;
			tdataArray["dbo__ABCSecurity"][".isUseTimeForSearch"] = false;
			tdataArray["dbo__ABCSecurity"][".badgeColor"] = "5F9EA0";
			tdataArray["dbo__ABCSecurity"][".allSearchFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".filterFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".requiredSearchFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".allSearchFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".allSearchFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".allSearchFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".allSearchFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".allSearchFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("ID");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("admin");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".googleLikeFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".advSearchFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".advSearchFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".advSearchFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".advSearchFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".advSearchFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".advSearchFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".tableType"] = "list";
			tdataArray["dbo__ABCSecurity"][".printerPageOrientation"] = 0;
			tdataArray["dbo__ABCSecurity"][".nPrinterPageScale"] = 100;
			tdataArray["dbo__ABCSecurity"][".nPrinterSplitRecords"] = 40;
			tdataArray["dbo__ABCSecurity"][".nPrinterPDFSplitRecords"] = 40;
			tdataArray["dbo__ABCSecurity"][".geocodingEnabled"] = false;
			tdataArray["dbo__ABCSecurity"][".listGridLayout"] = 3;
			tdataArray["dbo__ABCSecurity"][".pageSize"] = 20;
			tdataArray["dbo__ABCSecurity"][".warnLeavingPages"] = true;
			tstrOrderBy = "";
			if(MVCFunctions.strlen(tstrOrderBy) && MVCFunctions.strtolower(MVCFunctions.substr(tstrOrderBy, new XVar(0), new XVar(8))) != "order by")
			{
				tstrOrderBy = MVCFunctions.Concat("order by ", tstrOrderBy);
			}
			tdataArray["dbo__ABCSecurity"][".strOrderBy"] = tstrOrderBy;
			tdataArray["dbo__ABCSecurity"][".orderindexes"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".sqlHead"] = "SELECT ID,  	username,  	password,  	[admin],  	byear,  	eyear,  	[role]";
			tdataArray["dbo__ABCSecurity"][".sqlFrom"] = "FROM dbo.[_ABCSecurity]";
			tdataArray["dbo__ABCSecurity"][".sqlWhereExpr"] = "";
			tdataArray["dbo__ABCSecurity"][".sqlTail"] = "";
			arrRPP = SettingsMap.GetArray();
			arrRPP.Add(10);
			arrRPP.Add(20);
			arrRPP.Add(30);
			arrRPP.Add(50);
			arrRPP.Add(100);
			arrRPP.Add(500);
			arrRPP.Add(-1);
			tdataArray["dbo__ABCSecurity"][".arrRecsPerPage"] = arrRPP;
			arrGPP = SettingsMap.GetArray();
			arrGPP.Add(1);
			arrGPP.Add(3);
			arrGPP.Add(5);
			arrGPP.Add(10);
			arrGPP.Add(50);
			arrGPP.Add(100);
			arrGPP.Add(-1);
			tdataArray["dbo__ABCSecurity"][".arrGroupsPerPage"] = arrGPP;
			tdataArray["dbo__ABCSecurity"][".highlightSearchResults"] = true;
			tableKeysArray["dbo__ABCSecurity"] = SettingsMap.GetArray();
			tableKeysArray["dbo__ABCSecurity"].Add("ID");
			tdataArray["dbo__ABCSecurity"][".Keys"] = tableKeysArray["dbo__ABCSecurity"];
			tdataArray["dbo__ABCSecurity"][".listFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".listFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".listFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".listFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".listFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".listFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".hideMobileList"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".viewFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".viewFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".viewFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".viewFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".viewFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".viewFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".addFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".addFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".addFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".addFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".addFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".addFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".masterListFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("ID");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("admin");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".masterListFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".inlineAddFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".editFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".editFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".editFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".editFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".editFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".editFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".inlineEditFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".updateSelectedFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".exportFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".exportFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".exportFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".exportFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".exportFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".exportFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".importFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".importFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".importFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".importFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".importFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".importFields"].Add("role");
			tdataArray["dbo__ABCSecurity"][".printFields"] = SettingsMap.GetArray();
			tdataArray["dbo__ABCSecurity"][".printFields"].Add("username");
			tdataArray["dbo__ABCSecurity"][".printFields"].Add("password");
			tdataArray["dbo__ABCSecurity"][".printFields"].Add("byear");
			tdataArray["dbo__ABCSecurity"][".printFields"].Add("eyear");
			tdataArray["dbo__ABCSecurity"][".printFields"].Add("role");
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 1;
			fdata["strName"] = "ID";
			fdata["GoodName"] = "ID";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","ID");
			fdata["FieldType"] = 3;
			fdata["strField"] = "ID";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "ID";
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Text field");
			edata["IsRequired"] = true;
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["HTML5InuptType"] = "number";
			edata["EditParams"] = "";
			edata["controlWidth"] = 200;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"].Add(CommonFunctions.getJsValidatorName(new XVar("Number")));
			edata["validateAs"]["basicValidate"].Add("IsRequired");
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			tdataArray["dbo__ABCSecurity"]["ID"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 2;
			fdata["strName"] = "username";
			fdata["GoodName"] = "username";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","username");
			fdata["FieldType"] = 202;
			fdata["bListPage"] = true;
			fdata["bAddPage"] = true;
			fdata["bInlineAdd"] = true;
			fdata["bEditPage"] = true;
			fdata["bInlineEdit"] = true;
			fdata["bUpdateSelected"] = true;
			fdata["bViewPage"] = true;
			fdata["bAdvancedSearch"] = true;
			fdata["bPrinterPage"] = true;
			fdata["bExportPage"] = true;
			fdata["strField"] = "username";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "username";
			fdata["FieldPermissions"] = true;
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Text field");
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["HTML5InuptType"] = "text";
			edata["EditParams"] = "";
			edata["EditParams"] = MVCFunctions.Concat(edata["EditParams"], " maxlength=255");
			edata["controlWidth"] = 200;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			fdata["searchOptionsList"] = new XVar(0, "Contains", 1, "Equals", 2, "Starts with", 3, "More than", 4, "Less than", 5, "Between", 6, "Empty", 7, Constants.NOT_EMPTY);
			tdataArray["dbo__ABCSecurity"]["username"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 3;
			fdata["strName"] = "password";
			fdata["GoodName"] = "password";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","password");
			fdata["FieldType"] = 202;
			fdata["bListPage"] = true;
			fdata["bAddPage"] = true;
			fdata["bInlineAdd"] = true;
			fdata["bEditPage"] = true;
			fdata["bInlineEdit"] = true;
			fdata["bUpdateSelected"] = true;
			fdata["bViewPage"] = true;
			fdata["bAdvancedSearch"] = true;
			fdata["bPrinterPage"] = true;
			fdata["bExportPage"] = true;
			fdata["strField"] = "password";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "password";
			fdata["FieldPermissions"] = true;
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Text field");
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["HTML5InuptType"] = "text";
			edata["EditParams"] = "";
			edata["EditParams"] = MVCFunctions.Concat(edata["EditParams"], " maxlength=255");
			edata["controlWidth"] = 200;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			fdata["searchOptionsList"] = new XVar(0, "Contains", 1, "Equals", 2, "Starts with", 3, "More than", 4, "Less than", 5, "Between", 6, "Empty", 7, Constants.NOT_EMPTY);
			tdataArray["dbo__ABCSecurity"]["password"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 4;
			fdata["strName"] = "admin";
			fdata["GoodName"] = "admin";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","admin");
			fdata["FieldType"] = 11;
			fdata["strField"] = "admin";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "[admin]";
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "Checkbox");
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Checkbox");
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["controlWidth"] = 200;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			tdataArray["dbo__ABCSecurity"]["admin"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 5;
			fdata["strName"] = "byear";
			fdata["GoodName"] = "byear";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","byear");
			fdata["FieldType"] = 3;
			fdata["bListPage"] = true;
			fdata["bAddPage"] = true;
			fdata["bInlineAdd"] = true;
			fdata["bEditPage"] = true;
			fdata["bInlineEdit"] = true;
			fdata["bUpdateSelected"] = true;
			fdata["bViewPage"] = true;
			fdata["bAdvancedSearch"] = true;
			fdata["bPrinterPage"] = true;
			fdata["bExportPage"] = true;
			fdata["strField"] = "byear";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "byear";
			fdata["FieldPermissions"] = true;
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Text field");
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["HTML5InuptType"] = "number";
			edata["EditParams"] = "";
			edata["controlWidth"] = 50;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"].Add(CommonFunctions.getJsValidatorName(new XVar("Number")));
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			fdata["searchOptionsList"] = new XVar(0, "Contains", 1, "Equals", 2, "Starts with", 3, "More than", 4, "Less than", 5, "Between", 6, "Empty", 7, Constants.NOT_EMPTY);
			tdataArray["dbo__ABCSecurity"]["byear"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 6;
			fdata["strName"] = "eyear";
			fdata["GoodName"] = "eyear";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","eyear");
			fdata["FieldType"] = 3;
			fdata["bListPage"] = true;
			fdata["bAddPage"] = true;
			fdata["bInlineAdd"] = true;
			fdata["bEditPage"] = true;
			fdata["bInlineEdit"] = true;
			fdata["bUpdateSelected"] = true;
			fdata["bViewPage"] = true;
			fdata["bAdvancedSearch"] = true;
			fdata["bPrinterPage"] = true;
			fdata["bExportPage"] = true;
			fdata["strField"] = "eyear";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "eyear";
			fdata["FieldPermissions"] = true;
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Text field");
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["HTML5InuptType"] = "number";
			edata["EditParams"] = "";
			edata["controlWidth"] = 50;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"].Add(CommonFunctions.getJsValidatorName(new XVar("Number")));
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			fdata["searchOptionsList"] = new XVar(0, "Contains", 1, "Equals", 2, "Starts with", 3, "More than", 4, "Less than", 5, "Between", 6, "Empty", 7, Constants.NOT_EMPTY);
			tdataArray["dbo__ABCSecurity"]["eyear"] = fdata;
			fdata = SettingsMap.GetArray();
			fdata["Index"] = 7;
			fdata["strName"] = "role";
			fdata["GoodName"] = "role";
			fdata["ownerTable"] = "dbo._ABCSecurity";
			fdata["Label"] = CommonFunctions.GetFieldLabel("dbo__ABCSecurity","role");
			fdata["FieldType"] = 202;
			fdata["bListPage"] = true;
			fdata["bAddPage"] = true;
			fdata["bInlineAdd"] = true;
			fdata["bEditPage"] = true;
			fdata["bInlineEdit"] = true;
			fdata["bUpdateSelected"] = true;
			fdata["bViewPage"] = true;
			fdata["bAdvancedSearch"] = true;
			fdata["bPrinterPage"] = true;
			fdata["bExportPage"] = true;
			fdata["strField"] = "role";
			fdata["isSQLExpression"] = true;
			fdata["FullName"] = "[role]";
			fdata["FieldPermissions"] = true;
			fdata["UploadFolder"] = "files";
			fdata["ViewFormats"] = SettingsMap.GetArray();
			vdata = new XVar("ViewFormat", "");
			vdata["NeedEncode"] = true;
			fdata["ViewFormats"]["view"] = vdata;
			fdata["EditFormats"] = SettingsMap.GetArray();
			edata = new XVar("EditFormat", "Lookup wizard");
			edata["LookupType"] = 0;
			edata["autoCompleteFieldsOnEdit"] = 0;
			edata["autoCompleteFields"] = SettingsMap.GetArray();
			edata["LCType"] = 0;
			edata["LookupValues"] = SettingsMap.GetArray();
			edata["LookupValues"].Add("admin");
			edata["LookupValues"].Add("readonly");
			edata["LookupValues"].Add("member");
			edata["SelectSize"] = 1;
			edata["acceptFileTypes"] = ".+$";
			edata["maxNumberOfFiles"] = 1;
			edata["controlWidth"] = 200;
			edata["validateAs"] = SettingsMap.GetArray();
			edata["validateAs"]["basicValidate"] = SettingsMap.GetArray();
			edata["validateAs"]["customMessages"] = SettingsMap.GetArray();
			fdata["EditFormats"]["edit"] = edata;
			fdata["isSeparate"] = false;
			fdata["defaultSearchOption"] = "Equals";
			fdata["searchOptionsList"] = new XVar(0, "Contains", 1, "Equals", 2, "Starts with", 3, "More than", 4, "Less than", 5, "Between", 6, "Empty", 7, Constants.NOT_EMPTY);
			tdataArray["dbo__ABCSecurity"]["role"] = fdata;
			GlobalVars.tables_data["dbo._ABCSecurity"] = tdataArray["dbo__ABCSecurity"];
			GlobalVars.field_labels["dbo__ABCSecurity"] = fieldLabelsArray["dbo__ABCSecurity"];
			GlobalVars.fieldToolTips["dbo__ABCSecurity"] = fieldToolTipsArray["dbo__ABCSecurity"];
			GlobalVars.placeHolders["dbo__ABCSecurity"] = placeHoldersArray["dbo__ABCSecurity"];
			GlobalVars.page_titles["dbo__ABCSecurity"] = pageTitlesArray["dbo__ABCSecurity"];
			GlobalVars.detailsTablesData["dbo._ABCSecurity"] = SettingsMap.GetArray();
			GlobalVars.masterTablesData["dbo._ABCSecurity"] = SettingsMap.GetArray();


			strOriginalDetailsTable = "dbo._ABCVotes";
			masterParams = SettingsMap.GetArray();
			masterParams["mDataSourceTable"] = "dbo._ABCVotes";
			masterParams["mOriginalTable"] = strOriginalDetailsTable;
			masterParams["mShortTable"] = "dbo__ABCVotes";
			masterParams["masterKeys"] = SettingsMap.GetArray();
			masterParams["detailKeys"] = SettingsMap.GetArray();
			masterParams["dispChildCount"] = 0;
			masterParams["hideChild"] = 0;
			masterParams["dispMasterInfo"] = SettingsMap.GetArray();
			masterParams["previewOnList"] = 2;
			masterParams["previewOnAdd"] = 0;
			masterParams["previewOnEdit"] = 0;
			masterParams["previewOnView"] = 0;
			masterParams["proceedLink"] = 1;
			masterParams["type"] = Constants.PAGE_LIST;
			GlobalVars.masterTablesData["dbo._ABCSecurity"][0] = masterParams;
			GlobalVars.masterTablesData["dbo._ABCSecurity"][0]["masterKeys"] = SettingsMap.GetArray();
			GlobalVars.masterTablesData["dbo._ABCSecurity"][0]["masterKeys"].Add("committee_member");
			GlobalVars.masterTablesData["dbo._ABCSecurity"][0]["detailKeys"] = SettingsMap.GetArray();
			GlobalVars.masterTablesData["dbo._ABCSecurity"][0]["detailKeys"].Add("username");

SQLEntity obj = null;
var protoArray = SettingsMap.GetArray();
protoArray["0"] = SettingsMap.GetArray();
protoArray["0"]["m_strHead"] = "SELECT";
protoArray["0"]["m_strFieldList"] = "ID,  	username,  	password,  	[admin],  	byear,  	eyear,  	[role]";
protoArray["0"]["m_strFrom"] = "FROM dbo.[_ABCSecurity]";
protoArray["0"]["m_strWhere"] = "";
protoArray["0"]["m_strOrderBy"] = "";
	
		
protoArray["0"]["cipherer"] = null;
protoArray["2"] = SettingsMap.GetArray();
protoArray["2"]["m_sql"] = "";
protoArray["2"]["m_uniontype"] = "SQLL_UNKNOWN";
obj = new SQLNonParsed(new XVar("m_sql", ""));

protoArray["2"]["m_column"] = obj;
protoArray["2"]["m_contained"] = SettingsMap.GetArray();
protoArray["2"]["m_strCase"] = "";
protoArray["2"]["m_havingmode"] = false;
protoArray["2"]["m_inBrackets"] = false;
protoArray["2"]["m_useAlias"] = false;
obj = new SQLLogicalExpr(protoArray["2"]);

protoArray["0"]["m_where"] = obj;
protoArray["4"] = SettingsMap.GetArray();
protoArray["4"]["m_sql"] = "";
protoArray["4"]["m_uniontype"] = "SQLL_UNKNOWN";
obj = new SQLNonParsed(new XVar("m_sql", ""));

protoArray["4"]["m_column"] = obj;
protoArray["4"]["m_contained"] = SettingsMap.GetArray();
protoArray["4"]["m_strCase"] = "";
protoArray["4"]["m_havingmode"] = false;
protoArray["4"]["m_inBrackets"] = false;
protoArray["4"]["m_useAlias"] = false;
obj = new SQLLogicalExpr(protoArray["4"]);

protoArray["0"]["m_having"] = obj;
protoArray["0"]["m_fieldlist"] = SettingsMap.GetArray();
protoArray["6"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "ID", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["6"]["m_sql"] = "ID";
protoArray["6"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["6"]["m_expr"] = obj;
protoArray["6"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["6"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["8"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "username", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["8"]["m_sql"] = "username";
protoArray["8"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["8"]["m_expr"] = obj;
protoArray["8"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["8"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["10"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "password", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["10"]["m_sql"] = "password";
protoArray["10"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["10"]["m_expr"] = obj;
protoArray["10"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["10"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["12"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "admin", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["12"]["m_sql"] = "[admin]";
protoArray["12"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["12"]["m_expr"] = obj;
protoArray["12"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["12"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["14"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "byear", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["14"]["m_sql"] = "byear";
protoArray["14"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["14"]["m_expr"] = obj;
protoArray["14"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["14"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["16"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "eyear", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["16"]["m_sql"] = "eyear";
protoArray["16"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["16"]["m_expr"] = obj;
protoArray["16"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["16"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["18"] = SettingsMap.GetArray();
obj = new SQLField(new XVar("m_strName", "role", "m_strTable", "dbo._ABCSecurity", "m_srcTableName", "dbo._ABCSecurity"));

protoArray["18"]["m_sql"] = "[role]";
protoArray["18"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["18"]["m_expr"] = obj;
protoArray["18"]["m_alias"] = "";
obj = new SQLFieldListItem(protoArray["18"]);

protoArray["0"]["m_fieldlist"].Add(obj);
protoArray["0"]["m_fromlist"] = SettingsMap.GetArray();
protoArray["20"] = SettingsMap.GetArray();
protoArray["20"]["m_link"] = "SQLL_MAIN";
protoArray["21"] = SettingsMap.GetArray();
protoArray["21"]["m_strName"] = "dbo._ABCSecurity";
protoArray["21"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["21"]["m_columns"] = SettingsMap.GetArray();
protoArray["21"]["m_columns"].Add("ID");
protoArray["21"]["m_columns"].Add("username");
protoArray["21"]["m_columns"].Add("password");
protoArray["21"]["m_columns"].Add("admin");
protoArray["21"]["m_columns"].Add("byear");
protoArray["21"]["m_columns"].Add("eyear");
protoArray["21"]["m_columns"].Add("role");
obj = new SQLTable(protoArray["21"]);

protoArray["20"]["m_table"] = obj;
protoArray["20"]["m_sql"] = "dbo.[_ABCSecurity]";
protoArray["20"]["m_alias"] = "";
protoArray["20"]["m_srcTableName"] = "dbo._ABCSecurity";
protoArray["22"] = SettingsMap.GetArray();
protoArray["22"]["m_sql"] = "";
protoArray["22"]["m_uniontype"] = "SQLL_UNKNOWN";
obj = new SQLNonParsed(new XVar("m_sql", ""));

protoArray["22"]["m_column"] = obj;
protoArray["22"]["m_contained"] = SettingsMap.GetArray();
protoArray["22"]["m_strCase"] = "";
protoArray["22"]["m_havingmode"] = false;
protoArray["22"]["m_inBrackets"] = false;
protoArray["22"]["m_useAlias"] = false;
obj = new SQLLogicalExpr(protoArray["22"]);

protoArray["20"]["m_joinon"] = obj;
obj = new SQLFromListItem(protoArray["20"]);

protoArray["0"]["m_fromlist"].Add(obj);
protoArray["0"]["m_groupby"] = SettingsMap.GetArray();
protoArray["0"]["m_orderby"] = SettingsMap.GetArray();
protoArray["0"]["m_srcTableName"] = "dbo._ABCSecurity";
obj = new SQLQuery(protoArray["0"]);

queryData_Array["dbo__ABCSecurity"] = obj;

				
		
			tdataArray["dbo__ABCSecurity"][".sqlquery"] = queryData_Array["dbo__ABCSecurity"];
			GlobalVars.tableEvents["dbo._ABCSecurity"] = new eventsBase();
			tdataArray["dbo__ABCSecurity"][".hasEvents"] = false;
			GlobalVars.tables_data["dbo__ABCSecurity"] = tdataArray["dbo__ABCSecurity"];
		}
	}

}
