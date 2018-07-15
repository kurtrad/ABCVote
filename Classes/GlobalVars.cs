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
	public static partial class GlobalVars
	{
		public static dynamic Settings
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["Settings"];
			}
			set
			{
				HttpContext.Current.Items["Settings"] = value;
			}
		}
		public static dynamic Variables
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["Variables"];
			}
			set
			{
				HttpContext.Current.Items["Variables"] = value;
			}
		}
		public static dynamic WRAdminPagePassword
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["WRAdminPagePassword"];
			}
			set
			{
				HttpContext.Current.Items["WRAdminPagePassword"] = value;
			}
		}
		public static dynamic _cachedSeachClauses
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["_cachedSeachClauses"];
			}
			set
			{
				HttpContext.Current.Items["_cachedSeachClauses"] = value;
			}
		}
		public static dynamic _currentLanguage
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["_currentLanguage"];
			}
			set
			{
				HttpContext.Current.Items["_currentLanguage"] = value;
			}
		}
		public static dynamic _gmdays
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["_gmdays"];
			}
			set
			{
				HttpContext.Current.Items["_gmdays"] = value;
			}
		}
		public static dynamic _pagetypeToPermissions_dict
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["_pagetypeToPermissions_dict"];
			}
			set
			{
				HttpContext.Current.Items["_pagetypeToPermissions_dict"] = value;
			}
		}
		public static dynamic adNestedPermissions
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["adNestedPermissions"];
			}
			set
			{
				HttpContext.Current.Items["adNestedPermissions"] = value;
			}
		}
		public static dynamic ajaxSearchStartsWith
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["ajaxSearchStartsWith"];
			}
			set
			{
				HttpContext.Current.Items["ajaxSearchStartsWith"] = value;
			}
		}
		public static dynamic allDetailsTablesArr
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["allDetailsTablesArr"];
			}
			set
			{
				HttpContext.Current.Items["allDetailsTablesArr"] = value;
			}
		}
		public static dynamic arrDBFieldsList
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["arrDBFieldsList"];
			}
			set
			{
				HttpContext.Current.Items["arrDBFieldsList"] = value;
			}
		}
		public static dynamic bSubqueriesSupported
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["bSubqueriesSupported"];
			}
			set
			{
				HttpContext.Current.Items["bSubqueriesSupported"] = value;
			}
		}
		public static dynamic breadcrumb_labels
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["breadcrumb_labels"];
			}
			set
			{
				HttpContext.Current.Items["breadcrumb_labels"] = value;
			}
		}
		public static dynamic cAdvSecurityMethod
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cAdvSecurityMethod"];
			}
			set
			{
				HttpContext.Current.Items["cAdvSecurityMethod"] = value;
			}
		}
		public static dynamic cCharset
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cCharset"];
			}
			set
			{
				HttpContext.Current.Items["cCharset"] = value;
			}
		}
		public static dynamic cCodepage
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cCodepage"];
			}
			set
			{
				HttpContext.Current.Items["cCodepage"] = value;
			}
		}
		public static dynamic cDisplayNameField
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cDisplayNameField"];
			}
			set
			{
				HttpContext.Current.Items["cDisplayNameField"] = value;
			}
		}
		public static dynamic cDisplayNameFieldType
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cDisplayNameFieldType"];
			}
			set
			{
				HttpContext.Current.Items["cDisplayNameFieldType"] = value;
			}
		}
		public static dynamic cEmailField
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cEmailField"];
			}
			set
			{
				HttpContext.Current.Items["cEmailField"] = value;
			}
		}
		public static dynamic cEmailFieldType
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cEmailFieldType"];
			}
			set
			{
				HttpContext.Current.Items["cEmailFieldType"] = value;
			}
		}
		public static dynamic cLoginTable
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cLoginTable"];
			}
			set
			{
				HttpContext.Current.Items["cLoginTable"] = value;
			}
		}
		public static dynamic cMySQLNames
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cMySQLNames"];
			}
			set
			{
				HttpContext.Current.Items["cMySQLNames"] = value;
			}
		}
		public static dynamic cPasswordField
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cPasswordField"];
			}
			set
			{
				HttpContext.Current.Items["cPasswordField"] = value;
			}
		}
		public static dynamic cPasswordFieldType
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cPasswordFieldType"];
			}
			set
			{
				HttpContext.Current.Items["cPasswordFieldType"] = value;
			}
		}
		public static dynamic cUserGroupField
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cUserGroupField"];
			}
			set
			{
				HttpContext.Current.Items["cUserGroupField"] = value;
			}
		}
		public static dynamic cUserNameField
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cUserNameField"];
			}
			set
			{
				HttpContext.Current.Items["cUserNameField"] = value;
			}
		}
		public static dynamic cUserNameFieldType
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cUserNameFieldType"];
			}
			set
			{
				HttpContext.Current.Items["cUserNameFieldType"] = value;
			}
		}
		public static dynamic cache_db2time
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cache_db2time"];
			}
			set
			{
				HttpContext.Current.Items["cache_db2time"] = value;
			}
		}
		public static dynamic cache_formatweekstart
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cache_formatweekstart"];
			}
			set
			{
				HttpContext.Current.Items["cache_formatweekstart"] = value;
			}
		}
		public static dynamic cache_fullfieldname
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cache_fullfieldname"];
			}
			set
			{
				HttpContext.Current.Items["cache_fullfieldname"] = value;
			}
		}
		public static dynamic cache_getdayofweek
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cache_getdayofweek"];
			}
			set
			{
				HttpContext.Current.Items["cache_getdayofweek"] = value;
			}
		}
		public static dynamic cache_getweekstart
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cache_getweekstart"];
			}
			set
			{
				HttpContext.Current.Items["cache_getweekstart"] = value;
			}
		}
		public static dynamic caseInsensitiveUsername
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["caseInsensitiveUsername"];
			}
			set
			{
				HttpContext.Current.Items["caseInsensitiveUsername"] = value;
			}
		}
		public static dynamic cipherer
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cipherer"];
			}
			set
			{
				HttpContext.Current.Items["cipherer"] = value;
			}
		}
		public static dynamic cman
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["cman"];
			}
			set
			{
				HttpContext.Current.Items["cman"] = value;
			}
		}
		public static dynamic conn
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["conn"];
			}
			set
			{
				HttpContext.Current.Items["conn"] = value;
			}
		}
		public static dynamic contextStack
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["contextStack"];
			}
			set
			{
				HttpContext.Current.Items["contextStack"] = value;
			}
		}
		public static dynamic currentConnection
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["currentConnection"];
			}
			set
			{
				HttpContext.Current.Items["currentConnection"] = value;
			}
		}
		public static dynamic customLDAPSettings
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["customLDAPSettings"];
			}
			set
			{
				HttpContext.Current.Items["customLDAPSettings"] = value;
			}
		}
		public static dynamic custom_labels
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["custom_labels"];
			}
			set
			{
				HttpContext.Current.Items["custom_labels"] = value;
			}
		}
		public static dynamic dDebug
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["dDebug"];
			}
			set
			{
				HttpContext.Current.Items["dDebug"] = value;
			}
		}
		public static dynamic dSQL
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["dSQL"];
			}
			set
			{
				HttpContext.Current.Items["dSQL"] = value;
			}
		}
		public static dynamic dal
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["dal"];
			}
			set
			{
				HttpContext.Current.Items["dal"] = value;
			}
		}
		public static dynamic dalTables
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["dalTables"];
			}
			set
			{
				HttpContext.Current.Items["dalTables"] = value;
			}
		}
		public static dynamic dal_info
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["dal_info"];
			}
			set
			{
				HttpContext.Current.Items["dal_info"] = value;
			}
		}
		public static dynamic db_query_safe_err
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["db_query_safe_err"];
			}
			set
			{
				HttpContext.Current.Items["db_query_safe_err"] = value;
			}
		}
		public static dynamic db_query_safe_errstr
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["db_query_safe_errstr"];
			}
			set
			{
				HttpContext.Current.Items["db_query_safe_errstr"] = value;
			}
		}
		public static dynamic detailsTablesData
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["detailsTablesData"];
			}
			set
			{
				HttpContext.Current.Items["detailsTablesData"] = value;
			}
		}
		public static dynamic eventObj
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["eventObj"];
			}
			set
			{
				HttpContext.Current.Items["eventObj"] = value;
			}
		}
		public static dynamic fieldToolTips
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["fieldToolTips"];
			}
			set
			{
				HttpContext.Current.Items["fieldToolTips"] = value;
			}
		}
		public static dynamic field_labels
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["field_labels"];
			}
			set
			{
				HttpContext.Current.Items["field_labels"] = value;
			}
		}
		public static dynamic fields_type
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["fields_type"];
			}
			set
			{
				HttpContext.Current.Items["fields_type"] = value;
			}
		}
		public static dynamic gLoadSearchControls
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gLoadSearchControls"];
			}
			set
			{
				HttpContext.Current.Items["gLoadSearchControls"] = value;
			}
		}
		public static dynamic gPermissionsRead
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gPermissionsRead"];
			}
			set
			{
				HttpContext.Current.Items["gPermissionsRead"] = value;
			}
		}
		public static dynamic gPermissionsRefreshTime
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gPermissionsRefreshTime"];
			}
			set
			{
				HttpContext.Current.Items["gPermissionsRefreshTime"] = value;
			}
		}
		public static dynamic gQuery
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gQuery"];
			}
			set
			{
				HttpContext.Current.Items["gQuery"] = value;
			}
		}
		public static dynamic gSettings
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gSettings"];
			}
			set
			{
				HttpContext.Current.Items["gSettings"] = value;
			}
		}
		public static dynamic g_defaultOptionValues
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["g_defaultOptionValues"];
			}
			set
			{
				HttpContext.Current.Items["g_defaultOptionValues"] = value;
			}
		}
		public static dynamic g_settingsType
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["g_settingsType"];
			}
			set
			{
				HttpContext.Current.Items["g_settingsType"] = value;
			}
		}
		public static dynamic globalEvents
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["globalEvents"];
			}
			set
			{
				HttpContext.Current.Items["globalEvents"] = value;
			}
		}
		public static dynamic globalSettings
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["globalSettings"];
			}
			set
			{
				HttpContext.Current.Items["globalSettings"] = value;
			}
		}
		public static dynamic group_sort_y
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["group_sort_y"];
			}
			set
			{
				HttpContext.Current.Items["group_sort_y"] = value;
			}
		}
		public static dynamic gstrOrderBy
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gstrOrderBy"];
			}
			set
			{
				HttpContext.Current.Items["gstrOrderBy"] = value;
			}
		}
		public static dynamic gstrSQL
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["gstrSQL"];
			}
			set
			{
				HttpContext.Current.Items["gstrSQL"] = value;
			}
		}
		public static dynamic isGroupSecurity
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["isGroupSecurity"];
			}
			set
			{
				HttpContext.Current.Items["isGroupSecurity"] = value;
			}
		}
		public static dynamic isUseRTEBasic
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["isUseRTEBasic"];
			}
			set
			{
				HttpContext.Current.Items["isUseRTEBasic"] = value;
			}
		}
		public static dynamic isUseRTECK
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["isUseRTECK"];
			}
			set
			{
				HttpContext.Current.Items["isUseRTECK"] = value;
			}
		}
		public static dynamic isUseRTEInnova
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["isUseRTEInnova"];
			}
			set
			{
				HttpContext.Current.Items["isUseRTEInnova"] = value;
			}
		}
		public static dynamic locale_info
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["locale_info"];
			}
			set
			{
				HttpContext.Current.Items["locale_info"] = value;
			}
		}
		public static dynamic logoutPerformed
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["logoutPerformed"];
			}
			set
			{
				HttpContext.Current.Items["logoutPerformed"] = value;
			}
		}
		public static dynamic lookupTableLinks
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["lookupTableLinks"];
			}
			set
			{
				HttpContext.Current.Items["lookupTableLinks"] = value;
			}
		}
		public static dynamic masterTablesData
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["masterTablesData"];
			}
			set
			{
				HttpContext.Current.Items["masterTablesData"] = value;
			}
		}
		public static dynamic mbEnabled
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["mbEnabled"];
			}
			set
			{
				HttpContext.Current.Items["mbEnabled"] = value;
			}
		}
		public static dynamic menuAssignments
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuAssignments"];
			}
			set
			{
				HttpContext.Current.Items["menuAssignments"] = value;
			}
		}
		public static dynamic menuDrillDownFlags
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuDrillDownFlags"];
			}
			set
			{
				HttpContext.Current.Items["menuDrillDownFlags"] = value;
			}
		}
		public static dynamic menuNodesIndex
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuNodesIndex"];
			}
			set
			{
				HttpContext.Current.Items["menuNodesIndex"] = value;
			}
		}
		public static dynamic menuNodesObject
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuNodesObject"];
			}
			set
			{
				HttpContext.Current.Items["menuNodesObject"] = value;
			}
		}
		public static dynamic menuSelector
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuSelector"];
			}
			set
			{
				HttpContext.Current.Items["menuSelector"] = value;
			}
		}
		public static dynamic menuStyle
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuStyle"];
			}
			set
			{
				HttpContext.Current.Items["menuStyle"] = value;
			}
		}
		public static dynamic menuStyles
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuStyles"];
			}
			set
			{
				HttpContext.Current.Items["menuStyles"] = value;
			}
		}
		public static dynamic menuTreelikeFlags
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["menuTreelikeFlags"];
			}
			set
			{
				HttpContext.Current.Items["menuTreelikeFlags"] = value;
			}
		}
		public static dynamic mlang_charsets
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["mlang_charsets"];
			}
			set
			{
				HttpContext.Current.Items["mlang_charsets"] = value;
			}
		}
		public static dynamic mlang_defaultlang
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["mlang_defaultlang"];
			}
			set
			{
				HttpContext.Current.Items["mlang_defaultlang"] = value;
			}
		}
		public static dynamic mlang_messages
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["mlang_messages"];
			}
			set
			{
				HttpContext.Current.Items["mlang_messages"] = value;
			}
		}
		public static dynamic pageTypesForEdit
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["pageTypesForEdit"];
			}
			set
			{
				HttpContext.Current.Items["pageTypesForEdit"] = value;
			}
		}
		public static dynamic pageTypesForView
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["pageTypesForView"];
			}
			set
			{
				HttpContext.Current.Items["pageTypesForView"] = value;
			}
		}
		public static dynamic page_layouts
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["page_layouts"];
			}
			set
			{
				HttpContext.Current.Items["page_layouts"] = value;
			}
		}
		public static dynamic page_titles
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["page_titles"];
			}
			set
			{
				HttpContext.Current.Items["page_titles"] = value;
			}
		}
		public static dynamic pagesData
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["pagesData"];
			}
			set
			{
				HttpContext.Current.Items["pagesData"] = value;
			}
		}
		public static dynamic placeHolders
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["placeHolders"];
			}
			set
			{
				HttpContext.Current.Items["placeHolders"] = value;
			}
		}
		public static dynamic print_r_depth
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["print_r_depth"];
			}
			set
			{
				HttpContext.Current.Items["print_r_depth"] = value;
			}
		}
		public static dynamic projectEntities
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["projectEntities"];
			}
			set
			{
				HttpContext.Current.Items["projectEntities"] = value;
			}
		}
		public static dynamic projectEntitiesReverse
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["projectEntitiesReverse"];
			}
			set
			{
				HttpContext.Current.Items["projectEntitiesReverse"] = value;
			}
		}
		public static dynamic projectPath
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["projectPath"];
			}
			set
			{
				HttpContext.Current.Items["projectPath"] = value;
			}
		}
		public static dynamic reportCaseSensitiveGroupFields
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["reportCaseSensitiveGroupFields"];
			}
			set
			{
				HttpContext.Current.Items["reportCaseSensitiveGroupFields"] = value;
			}
		}
		public static dynamic rpt_array
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["rpt_array"];
			}
			set
			{
				HttpContext.Current.Items["rpt_array"] = value;
			}
		}
		public static dynamic scriptname
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["scriptname"];
			}
			set
			{
				HttpContext.Current.Items["scriptname"] = value;
			}
		}
		public static dynamic sessPrefix
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["sessPrefix"];
			}
			set
			{
				HttpContext.Current.Items["sessPrefix"] = value;
			}
		}
		public static dynamic showCustomMarkerOnPrint
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["showCustomMarkerOnPrint"];
			}
			set
			{
				HttpContext.Current.Items["showCustomMarkerOnPrint"] = value;
			}
		}
		public static dynamic sortgroup
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["sortgroup"];
			}
			set
			{
				HttpContext.Current.Items["sortgroup"] = value;
			}
		}
		public static dynamic sortorder
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["sortorder"];
			}
			set
			{
				HttpContext.Current.Items["sortorder"] = value;
			}
		}
		public static dynamic strLastSQL
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["strLastSQL"];
			}
			set
			{
				HttpContext.Current.Items["strLastSQL"] = value;
			}
		}
		public static dynamic strOriginalTableName
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["strOriginalTableName"];
			}
			set
			{
				HttpContext.Current.Items["strOriginalTableName"] = value;
			}
		}
		public static dynamic strSQL
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["strSQL"];
			}
			set
			{
				HttpContext.Current.Items["strSQL"] = value;
			}
		}
		public static dynamic strTableName
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["strTableName"];
			}
			set
			{
				HttpContext.Current.Items["strTableName"] = value;
			}
		}
		public static dynamic suggestAllContent
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["suggestAllContent"];
			}
			set
			{
				HttpContext.Current.Items["suggestAllContent"] = value;
			}
		}
		public static dynamic t_layout
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["t_layout"];
			}
			set
			{
				HttpContext.Current.Items["t_layout"] = value;
			}
		}
		public static dynamic tableCaptions
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tableCaptions"];
			}
			set
			{
				HttpContext.Current.Items["tableCaptions"] = value;
			}
		}
		public static dynamic tableEvents
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tableEvents"];
			}
			set
			{
				HttpContext.Current.Items["tableEvents"] = value;
			}
		}
		public static dynamic tableinfo_cache
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tableinfo_cache"];
			}
			set
			{
				HttpContext.Current.Items["tableinfo_cache"] = value;
			}
		}
		public static dynamic tablesByGoodName
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tablesByGoodName"];
			}
			set
			{
				HttpContext.Current.Items["tablesByGoodName"] = value;
			}
		}
		public static dynamic tablesByUpperCase
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tablesByUpperCase"];
			}
			set
			{
				HttpContext.Current.Items["tablesByUpperCase"] = value;
			}
		}
		public static dynamic tablesByUpperGoodname
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tablesByUpperGoodname"];
			}
			set
			{
				HttpContext.Current.Items["tablesByUpperGoodname"] = value;
			}
		}
		public static dynamic tables_data
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tables_data"];
			}
			set
			{
				HttpContext.Current.Items["tables_data"] = value;
			}
		}
		public static dynamic tbl
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["tbl"];
			}
			set
			{
				HttpContext.Current.Items["tbl"] = value;
			}
		}
		public static dynamic testingLinks
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["testingLinks"];
			}
			set
			{
				HttpContext.Current.Items["testingLinks"] = value;
			}
		}
		public static dynamic twilioAuth
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["twilioAuth"];
			}
			set
			{
				HttpContext.Current.Items["twilioAuth"] = value;
			}
		}
		public static dynamic twilioNumber
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["twilioNumber"];
			}
			set
			{
				HttpContext.Current.Items["twilioNumber"] = value;
			}
		}
		public static dynamic twilioSID
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["twilioSID"];
			}
			set
			{
				HttpContext.Current.Items["twilioSID"] = value;
			}
		}
		public static dynamic useAJAX
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["useAJAX"];
			}
			set
			{
				HttpContext.Current.Items["useAJAX"] = value;
			}
		}
		public static dynamic useOldMysqlLib
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["useOldMysqlLib"];
			}
			set
			{
				HttpContext.Current.Items["useOldMysqlLib"] = value;
			}
		}
		public static dynamic useUTF8
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["useUTF8"];
			}
			set
			{
				HttpContext.Current.Items["useUTF8"] = value;
			}
		}
		public static dynamic version
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["version"];
			}
			set
			{
				HttpContext.Current.Items["version"] = value;
			}
		}
		public static dynamic wr_is_standalone
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["wr_is_standalone"];
			}
			set
			{
				HttpContext.Current.Items["wr_is_standalone"] = value;
			}
		}
		public static dynamic wr_pagestylepath
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["wr_pagestylepath"];
			}
			set
			{
				HttpContext.Current.Items["wr_pagestylepath"] = value;
			}
		}
		public static Stack<StringBuilder> BufferStack
		{
			get
			{
				return (Stack<StringBuilder>)HttpContext.Current.Items["BufferStack"];
			}
			set
			{
				HttpContext.Current.Items["BufferStack"] = value;
			}
		}
		public static XVar ConnectionStrings
		{
			get
			{
				return (XVar)HttpContext.Current.Items["ConnectionStrings"];
			}
			set
			{
				HttpContext.Current.Items["ConnectionStrings"] = value;
			}
		}
		public static dynamic IsOutputDone
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["IsOutputDone"];
			}
			set
			{
				HttpContext.Current.Items["IsOutputDone"] = value;
			}
		}
		public static XVar LastDBError
		{
			get
			{
				return (XVar)HttpContext.Current.Items["LastDBError"];
			}
			set
			{
				HttpContext.Current.Items["LastDBError"] = value;
			}
		}
		public static dynamic pageObject
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["pageObject"];
			}
			set
			{
				HttpContext.Current.Items["pageObject"] = value;
			}
		}
		public static dynamic strErrorHandler
		{
			get
			{
				return (dynamic)HttpContext.Current.Items["strErrorHandler"];
			}
			set
			{
				HttpContext.Current.Items["strErrorHandler"] = value;
			}
		}
	}
}
