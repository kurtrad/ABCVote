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
	public partial class CheckboxField : EditControl
	{
		protected static bool skipCheckboxFieldCtor = false;
		public CheckboxField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipCheckboxFieldCtor)
			{
				skipCheckboxFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.format = new XVar(Constants.EDIT_FORMAT_CHECKBOX);
		}
		public override XVar buildControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			if((XVar)((XVar)((XVar)(mode == Constants.MODE_ADD)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  || (XVar)(mode == Constants.MODE_EDIT))  || (XVar)(mode == Constants.MODE_INLINE_EDIT))
			{
				dynamic var_checked = null;
				var_checked = new XVar("");
				if((XVar)((XVar)(this.connection.dbType == Constants.nDATABASE_PostgreSQL)  && (XVar)((XVar)(XVar.Equals(XVar.Pack(value), XVar.Pack("t")))  || (XVar)((XVar)(value != XVar.Pack(""))  && (XVar)(value != XVar.Pack(0)))))  || (XVar)((XVar)(this.connection.dbType != Constants.nDATABASE_PostgreSQL)  && (XVar)((XVar)(value != XVar.Pack(""))  && (XVar)(value != XVar.Pack(0)))))
				{
					var_checked = new XVar(" checked");
				}
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"checkbox\">"));
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.cfield, "\" type=\"Checkbox\" ", (XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508 == true)) ? XVar.Pack(MVCFunctions.Concat("alt=\"", this.strLabel, "\" ")) : XVar.Pack("")), "name=\"", this.cfield, "\" ", var_checked, ">"));
			}
			else
			{
				dynamic optval = XVar.Array(), show = XVar.Array(), val = XVar.Array();
				MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"checkbox\">"));
				MVCFunctions.Echo(MVCFunctions.Concat("<select id=\"", this.cfield, "\" ", (XVar.Pack((XVar)((XVar)(mode == Constants.MODE_INLINE_EDIT)  || (XVar)(mode == Constants.MODE_INLINE_ADD))  && (XVar)(this.is508 == true)) ? XVar.Pack(MVCFunctions.Concat("alt=\"", this.strLabel, "\" ")) : XVar.Pack("")), "name=\"", this.cfield, "\" class=\"", (XVar.Pack(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT) ? XVar.Pack(" form-control") : XVar.Pack("")), "\">"));
				val = XVar.Clone(new XVar("", XVar.Array(), "True", new XVar(0, "on", 1, "1"), "False", new XVar(0, "off", 1, "0")));
				optval = XVar.Clone(new XVar(0, "", 1, "on", 2, "off"));
				show = XVar.Clone(new XVar(0, "", 1, "True", 2, "False"));
				foreach (KeyValuePair<XVar, dynamic> shownValue in show.GetEnumerator())
				{
					dynamic sel = null;
					sel = XVar.Clone((XVar.Pack(MVCFunctions.in_array((XVar)(value), (XVar)(val[shownValue.Value]))) ? XVar.Pack(" selected") : XVar.Pack("")));
					MVCFunctions.Echo(MVCFunctions.Concat("<option value=\"", optval[shownValue.Key], "\"", sel, ">", shownValue.Value, "</option>"));
				}
				MVCFunctions.Echo("</select>");
			}
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		public override XVar getFirstElementId()
		{
			return this.cfield;
		}
		public override XVar SQLWhere(dynamic _param_SearchFor, dynamic _param_strSearchOption, dynamic _param_SearchFor2, dynamic _param_etype, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic SearchFor = XVar.Clone(_param_SearchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic SearchFor2 = XVar.Clone(_param_SearchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			dynamic bNeedQuotes = null, baseResult = null, fullFieldName = null;
			baseResult = XVar.Clone(baseSQLWhere((XVar)(strSearchOption)));
			if(XVar.Equals(XVar.Pack(baseResult), XVar.Pack(false)))
			{
				return "";
			}
			if(baseResult != XVar.Pack(""))
			{
				return baseResult;
			}
			if((XVar)(SearchFor == "none")  || (XVar)((XVar)(SearchFor != "on")  && (XVar)(SearchFor != "off")))
			{
				return "";
			}
			fullFieldName = XVar.Clone(getFieldSQLDecrypt());
			bNeedQuotes = XVar.Clone(CommonFunctions.NeedQuotes((XVar)(this.var_type)));
			return constructFieldWhere((XVar)(fullFieldName), (XVar)(bNeedQuotes), (XVar)(SearchFor == "on"), (XVar)(this.var_type), (XVar)(this.connection.dbType));
		}
		public static XVar constructFieldWhere(dynamic _param_fullFieldName, dynamic _param_bNeedQuotes, dynamic _param_checked, dynamic _param_fieldType, dynamic _param_dbType)
		{
			#region pass-by-value parameters
			dynamic fullFieldName = XVar.Clone(_param_fullFieldName);
			dynamic bNeedQuotes = XVar.Clone(_param_bNeedQuotes);
			dynamic var_checked = XVar.Clone(_param_checked);
			dynamic fieldType = XVar.Clone(_param_fieldType);
			dynamic dbType = XVar.Clone(_param_dbType);
			#endregion

			dynamic falseval = null;
			if(XVar.Pack(bNeedQuotes))
			{
				dynamic whereStr = null;
				if(XVar.Pack(var_checked))
				{
					whereStr = XVar.Clone(MVCFunctions.Concat("(", fullFieldName, "<>'0' "));
					if(dbType != Constants.nDATABASE_Oracle)
					{
						whereStr = MVCFunctions.Concat(whereStr, " and ", fullFieldName, "<>'' ");
					}
					whereStr = MVCFunctions.Concat(whereStr, " and ", fullFieldName, " is not null)");
					if(dbType == Constants.nDATABASE_Oracle)
					{
						whereStr = MVCFunctions.Concat(whereStr, " and abs(case when LENGTH(TRIM(TRANSLATE(", fullFieldName, ", ' +-.0123456789', ' '))) is null then cast(", fullFieldName, " as integer) else 0 end) > 0");
					}
					if(dbType == Constants.nDATABASE_MSSQLServer)
					{
						whereStr = MVCFunctions.Concat(whereStr, " and ABS(case WHEN ISNUMERIC(", fullFieldName, ") = 1 THEN convert(integer, ", fullFieldName, ") else 0 end) > 0");
					}
					if(dbType == Constants.nDATABASE_MySQL)
					{
						whereStr = MVCFunctions.Concat(whereStr, " and abs(cast(", fullFieldName, " as signed)) > 0");
					}
					if(dbType == Constants.nDATABASE_PostgreSQL)
					{
						whereStr = MVCFunctions.Concat(whereStr, " and abs(case textregexeq(", fullFieldName, ", '^(\\-)?[[:digit:]]+(\\.[[:digit:]]+)?$') when true then to_number(", fullFieldName, ", '999999999') else 0 end) > 0");
					}
					return whereStr;
				}
				whereStr = XVar.Clone(MVCFunctions.Concat("(", fullFieldName, "='0' "));
				if(dbType != Constants.nDATABASE_Oracle)
				{
					whereStr = MVCFunctions.Concat(whereStr, " or ", fullFieldName, "='' ");
				}
				whereStr = MVCFunctions.Concat(whereStr, " or ", fullFieldName, " is null)");
				if(dbType == Constants.nDATABASE_Oracle)
				{
					whereStr = MVCFunctions.Concat(whereStr, " or abs(case when LENGTH(TRIM(TRANSLATE(", fullFieldName, ", ' +-.0123456789', ' '))) is null then cast(", fullFieldName, " as integer) else 0 end) = 0");
				}
				if(dbType == Constants.nDATABASE_MSSQLServer)
				{
					whereStr = MVCFunctions.Concat(whereStr, " or ABS(case WHEN ISNUMERIC(", fullFieldName, ") = 1 THEN convert(integer, ", fullFieldName, ") else 0 end) = 0");
				}
				if(dbType == Constants.nDATABASE_MySQL)
				{
					whereStr = MVCFunctions.Concat(whereStr, " or cast(", fullFieldName, " as unsigned) = 0");
				}
				if(dbType == Constants.nDATABASE_PostgreSQL)
				{
					whereStr = MVCFunctions.Concat(whereStr, " or abs(case textregexeq(", fullFieldName, ", '^(\\-)?[[:digit:]]+(\\.[[:digit:]]+)?$') when true then to_number(", fullFieldName, ", '999999999') else 0 end) = 0");
				}
				return whereStr;
			}
			falseval = new XVar(0);
			if(dbType == Constants.nDATABASE_PostgreSQL)
			{
				if(fieldType == 11)
				{
					falseval = new XVar("false");
				}
			}
			if(XVar.Pack(var_checked))
			{
				return MVCFunctions.Concat("(", fullFieldName, "<>", falseval, " and ", fullFieldName, " is not null)");
			}
			return MVCFunctions.Concat("(", fullFieldName, "=", falseval, " or ", fullFieldName, " is null)");
		}
	}
}
