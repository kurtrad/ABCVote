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
	public partial class DateField : DateTimeControl
	{
		protected static bool skipDateFieldCtor = false;
		private bool skipDateTimeControlCtorSurrogate = new Func<bool>(() => skipDateTimeControlCtor = true).Invoke();
		public DateField(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
			:base((XVar)_param_field, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_connection)
		{
			if(skipDateFieldCtor)
			{
				skipDateFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.format = new XVar(Constants.EDIT_FORMAT_DATE);
		}
		public override XVar addJSFiles()
		{
			dynamic dateEditType = null;
			dateEditType = XVar.Clone(getDateEditType());
			return null;
		}
		public override XVar addCSSFiles()
		{
			dynamic dateEditType = null;
			dateEditType = XVar.Clone(getDateEditType());
			if((XVar)(dateEditType == Constants.EDIT_DATE_SIMPLE_INLINE)  || (XVar)(dateEditType == Constants.EDIT_DATE_DD_INLINE))
			{
				this.pageObject.AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				this.pageObject.AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				this.pageObject.AddCSSFile(new XVar("include/bootstrap/css/bootstrap-datetimepicker.min.css"));
			}
			return null;
		}
		public virtual XVar getProjectSettings()
		{
			if(this.pageObject.pageType == Constants.PAGE_LIST)
			{
				return new ProjectSettings((XVar)(this.pageObject.tName), new XVar(Constants.PAGE_SEARCH));
			}
			else
			{
				return this.pageObject.pSetEdit;
			}
			return null;
		}
		public virtual XVar getDateEditType(dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic dateEditType = null;
			if(XVar.Pack(!(XVar)(pSet)))
			{
				pSet = XVar.UnPackProjectSettings(getProjectSettings());
			}
			dateEditType = XVar.Clone(pSet.getDateEditType((XVar)(this.field)));
			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				if((XVar)((XVar)((XVar)(this.pageObject.pageType == Constants.PAGE_LIST)  || (XVar)(this.pageObject.pageType == Constants.PAGE_CHART))  || (XVar)(this.pageObject.pageType == Constants.PAGE_REPORT))  || (XVar)((XVar)(this.pageObject.pageType == Constants.PAGE_SEARCH)  && (XVar)(this.pageObject.mode == Constants.SEARCH_LOAD_CONTROL)))
				{
					if(dateEditType == Constants.EDIT_DATE_DD)
					{
						return Constants.EDIT_DATE_SIMPLE;
					}
					if(dateEditType == Constants.EDIT_DATE_DD_DP)
					{
						return Constants.EDIT_DATE_SIMPLE_DP;
					}
					if(dateEditType == Constants.EDIT_DATE_DD_INLINE)
					{
						return Constants.EDIT_DATE_SIMPLE_INLINE;
					}
				}
			}
			return dateEditType;
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

			dynamic alt = null, classString = null, controlWidth = null, dateEditType = null, dayStyle = null, dp = null, fmt = null, hasImgCal = null, initDayOpt = null, initMonthOpt = null, initYearOpt = null, monthStyle = null, ovalue = null, ovalue1 = null, ret = null, retday = null, retmonth = null, retyear = null, setHiddenElem = null, showTime = null, space = null, time = XVar.Array(), tvalue = null, yearStyle = null;
			ProjectSettings pSet;
			base.buildControl((XVar)(value), (XVar)(mode), (XVar)(fieldNum), (XVar)(validate), (XVar)(additionalCtrlParams), (XVar)(data));
			if(XVar.Pack(fieldNum))
			{
				this.cfield = XVar.Clone(MVCFunctions.Concat("value", fieldNum, "_", MVCFunctions.GoodFieldName((XVar)(this.field)), "_", this.id));
			}
			pSet = XVar.UnPackProjectSettings(getProjectSettings());
			dateEditType = XVar.Clone(getDateEditType((XVar)(pSet)));
			MVCFunctions.Echo(MVCFunctions.Concat("<input id=\"", this.ctype, "\" type=\"hidden\" name=\"", this.ctype, "\" value=\"date", dateEditType, "\">"));
			tvalue = XVar.Clone(value);
			time = XVar.Clone(CommonFunctions.db2time((XVar)(tvalue)));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(time))))
			{
				time = XVar.Clone(new XVar(0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0));
			}
			classString = new XVar("");
			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				classString = new XVar(" form-control");
			}
			dp = new XVar(0);
			hasImgCal = new XVar(true);
			showTime = XVar.Clone(pSet.dateEditShowTime((XVar)(this.field)));
			switch(((XVar)dateEditType).ToInt())
			{
				case Constants.EDIT_DATE_SIMPLE_INLINE:
				case Constants.EDIT_DATE_SIMPLE_DP:
					if(dateEditType == Constants.EDIT_DATE_SIMPLE_INLINE)
					{
						hasImgCal = new XVar(false);
					}
					ovalue = XVar.Clone(value);
					if(GlobalVars.locale_info["LOCALE_IDATE"] == 1)
					{
						fmt = XVar.Clone(MVCFunctions.Concat("dd", GlobalVars.locale_info["LOCALE_SDATE"], "MM", GlobalVars.locale_info["LOCALE_SDATE"], "yyyy"));
					}
					else
					{
						if(GlobalVars.locale_info["LOCALE_IDATE"] == 0)
						{
							fmt = XVar.Clone(MVCFunctions.Concat("MM", GlobalVars.locale_info["LOCALE_SDATE"], "dd", GlobalVars.locale_info["LOCALE_SDATE"], "yyyy"));
						}
						else
						{
							fmt = XVar.Clone(MVCFunctions.Concat("yyyy", GlobalVars.locale_info["LOCALE_SDATE"], "MM", GlobalVars.locale_info["LOCALE_SDATE"], "dd"));
						}
					}
					if((XVar)((XVar)((XVar)(showTime)  || (XVar)(time[3]))  || (XVar)(time[4]))  || (XVar)(time[5]))
					{
						dynamic timeAttrs = null;
						timeAttrs = XVar.Clone(this.pageObject.pSetEdit.getFormatTimeAttrs((XVar)(this.field)));
						fmt = MVCFunctions.Concat(fmt, " ", GlobalVars.locale_info["LOCALE_STIMEFORMAT"]);
					}
					if(XVar.Pack(time[0]))
					{
						ovalue = XVar.Clone(CommonFunctions.format_datetime_custom((XVar)(time), (XVar)(fmt)));
					}
					ovalue1 = XVar.Clone(MVCFunctions.Concat(time[2], "-", time[1], "-", time[0]));
					if((XVar)((XVar)((XVar)(showTime)  || (XVar)(time[3]))  || (XVar)(time[4]))  || (XVar)(time[5]))
					{
						ovalue1 = MVCFunctions.Concat(ovalue1, " ", time[3], ":", time[4], ":", time[5]);
					}
					ret = XVar.Clone(MVCFunctions.Concat("<input ", getPlaceholderAttr(), " id=\"", this.cfield, "\" ", this.inputStyle, " class=\"", classString, "\" type=\"Text\" name=\"", this.cfield, "\" value=\"", ovalue, "\">"));
					ret = MVCFunctions.Concat(ret, "<input id=\"ts", this.cfield, "\" type=\"Hidden\" name=\"ts", this.cfield, "\" value=\"", ovalue1, "\">");
					if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						ret = MVCFunctions.Concat(ret, "<span class=\"input-group-addon\" id=\"imgCal_", this.cfield, "\"><span class=\"glyphicon glyphicon-calendar\"></span></span>");
					}
					else
					{
						if(XVar.Pack(hasImgCal))
						{
							ret = MVCFunctions.Concat(ret, "<a href=\"#\" id=\"imgCal_", this.cfield, "\" data-icon=\"calendar\" title=\"Click Here to Pick up the date\"></a>");
						}
					}
					if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						if(XVar.Pack(CommonFunctions.isRTL()))
						{
							ret = MVCFunctions.Concat(ret, "<span></span>");
						}
						ret = XVar.Clone(MVCFunctions.Concat("<div class=\"input-group date\">", ret, "</div>"));
					}
					MVCFunctions.Echo(ret);
					break;
				case Constants.EDIT_DATE_DD_INLINE:
				case Constants.EDIT_DATE_DD_DP:
				case Constants.EDIT_DATE_DD:
					if((dateEditType == Constants.EDIT_DATE_DD_INLINE) || (dateEditType == Constants.EDIT_DATE_DD_DP))
					{
						dp = new XVar(1);
					}
					controlWidth = XVar.Clone(pSet.getControlWidth((XVar)(this.field)));
					if(XVar.Pack(0) < controlWidth)
					{
						dynamic dayWidth = null, mothWidth = null, yearWidth = null;
						controlWidth -= 10;
						yearWidth = XVar.Clone((XVar)Math.Floor((double)(controlWidth * 0.300000)));
						yearStyle = XVar.Clone(MVCFunctions.Concat("style=\"min-width: ", yearWidth, "px;margin-right:5px;\" "));
						dayWidth = XVar.Clone((XVar)Math.Floor((double)(controlWidth * 0.200000)));
						dayStyle = XVar.Clone(MVCFunctions.Concat("style=\"min-width: ", dayWidth, "px; margin-right:5px;\" "));
						mothWidth = XVar.Clone((controlWidth - yearWidth) - dayWidth);
						monthStyle = XVar.Clone(MVCFunctions.Concat("style=\"min-width: ", mothWidth, "px; margin-right:5px;\" "));
					}
					else
					{
						dayStyle = new XVar("");
						monthStyle = new XVar("");
						yearStyle = new XVar("");
					}
					alt = XVar.Clone(MVCFunctions.Concat("alt=\"", this.strLabel, "\" "));
					initMonthOpt = new XVar("<option>&nbsp;</option>");
					if(XVar.Pack(time[1]))
					{
						dynamic months = XVar.Array();
						months = XVar.Clone(CommonFunctions.getMountNames());
						initMonthOpt = XVar.Clone(MVCFunctions.Concat("<option>", months[time[1]], "</option>"));
					}
					initMonthOpt = MVCFunctions.Concat(initMonthOpt, "<option>", maxLengthMonth(), "</option>");
					initDayOpt = XVar.Clone(MVCFunctions.Concat("<option>", (XVar.Pack(time[2]) ? XVar.Pack(time[2]) : XVar.Pack("&nbsp;")), " </option><option>22</option>"));
					initYearOpt = XVar.Clone(MVCFunctions.Concat("<option>", (XVar.Pack(time[0]) ? XVar.Pack(time[0]) : XVar.Pack("&nbsp;")), "</option><option>2000</option>"));
					retday = XVar.Clone(MVCFunctions.Concat("<select class=\"", classString, "\" id=\"day", this.cfield, "\" ", dayStyle, alt, "name=\"day", this.cfield, "\" >", initDayOpt, "</select>"));
					retmonth = XVar.Clone(MVCFunctions.Concat("<select class=\"", classString, "\" id=\"month", this.cfield, "\" ", monthStyle, alt, "name=\"month", this.cfield, "\" >", initMonthOpt, "</option></select>"));
					retyear = XVar.Clone(MVCFunctions.Concat("<select class=\"", classString, "\" id=\"year", this.cfield, "\" ", yearStyle, alt, "name=\"year", this.cfield, "\" >", initYearOpt, "</select>"));
					space = XVar.Clone((XVar.Pack(XVar.Pack(0) < controlWidth) ? XVar.Pack("") : XVar.Pack("&nbsp;")));
					if(GlobalVars.locale_info["LOCALE_ILONGDATE"] == 1)
					{
						ret = XVar.Clone(MVCFunctions.Concat(retday, space, retmonth, space, retyear));
					}
					else
					{
						if(GlobalVars.locale_info["LOCALE_ILONGDATE"] == 0)
						{
							ret = XVar.Clone(MVCFunctions.Concat(retmonth, space, retday, space, retyear));
						}
						else
						{
							ret = XVar.Clone(MVCFunctions.Concat(retyear, space, retmonth, space, retday));
						}
					}
					setHiddenElem = new XVar("type=hidden");
					if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						setHiddenElem = XVar.Clone(MVCFunctions.Concat("class=\"", classString, " hiddenPickerElement\""));
					}
					if((XVar)((XVar)(time[0])  && (XVar)(time[1]))  && (XVar)(time[2]))
					{
						ret = MVCFunctions.Concat(ret, "<input id=\"", this.cfield, "\" ", setHiddenElem, " name=\"", this.cfield, "\" value=\"", time[0], "-", time[1], "-", time[2], "\">");
					}
					else
					{
						ret = MVCFunctions.Concat(ret, "<input id=\"", this.cfield, "\" ", setHiddenElem, " name=\"", this.cfield, "\" value=\"\">");
					}
					if(XVar.Pack(dp))
					{
						if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
						{
							ret = MVCFunctions.Concat(ret, "<button class=\"btn btn-default\" id=\"imgCal_", this.cfield, "\" aria-hidden=true><span class=\"glyphicon glyphicon-calendar\"  ></span></button>");
						}
						else
						{
							ret = MVCFunctions.Concat(ret, "<a href=\"#\" id=\"imgCal_", this.cfield, "\" data-icon=\"calendar\" title=\"Click Here to Pick up the date\"></a>");
							ret = MVCFunctions.Concat(ret, "<input id=\"ts", this.cfield, "\" type=hidden name=\"ts", this.cfield, "\" value=\"", time[2], "-", time[1], "-", time[0], "\">");
						}
					}
					if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
					{
						ret = XVar.Clone(MVCFunctions.Concat("<span class=\"bs-date-control form-inline\">", ret, "</span>"));
					}
					MVCFunctions.Echo(ret);
					break;
				default:
					ovalue = XVar.Clone(value);
					if(XVar.Pack(time[0]))
					{
						if((XVar)((XVar)((XVar)(showTime)  || (XVar)(time[3]))  || (XVar)(time[4]))  || (XVar)(time[5]))
						{
							ovalue = XVar.Clone(CommonFunctions.str_format_datetime((XVar)(time)));
						}
						else
						{
							ovalue = XVar.Clone(CommonFunctions.format_shortdate((XVar)(time)));
						}
					}
					MVCFunctions.Echo(MVCFunctions.Concat("<input ", getPlaceholderAttr(), " id=\"", this.cfield, "\" type=text class=\"", classString, "\" name=\"", this.cfield, "\" ", this.inputStyle, " value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(ovalue)), "\">"));
					break;
			}
			buildControlEnd((XVar)(validate), (XVar)(mode));
			return null;
		}
		public override XVar getFirstElementId()
		{
			dynamic dateEditType = null;
			dateEditType = XVar.Clone(getDateEditType());
			if(XVar.Pack(!(XVar)(dateEditType)))
			{
				return this.cfield;
			}
			switch(((XVar)dateEditType).ToInt())
			{
				case Constants.EDIT_DATE_DD:
				case Constants.EDIT_DATE_DD_INLINE:
				case Constants.EDIT_DATE_DD_DP:
					if(GlobalVars.locale_info["LOCALE_ILONGDATE"] == 1)
					{
						return MVCFunctions.Concat("day", this.cfield);
					}
					else
					{
						if(GlobalVars.locale_info["LOCALE_ILONGDATE"] == 0)
						{
							return MVCFunctions.Concat("month", this.cfield);
						}
						else
						{
							return MVCFunctions.Concat("year", this.cfield);
						}
					}
					break;
				default:
					return this.cfield;
					break;
			}
			return null;
		}
		public virtual XVar maxLengthMonth()
		{
			dynamic curMonthLen = null, curMontn = null, i = null, maxLenght = null, maxLengthMonth = null, mounts = XVar.Array();
			maxLengthMonth = new XVar("");
			mounts = XVar.Clone(CommonFunctions.getMountNames());
			maxLenght = new XVar(0);
			i = new XVar(0);
			for(;i < MVCFunctions.count(mounts); i++)
			{
				curMontn = XVar.Clone(mounts[i]);
				curMonthLen = XVar.Clone(MVCFunctions.runner_strlen((XVar)(curMontn)));
				if(maxLenght < curMonthLen)
				{
					maxLenght = XVar.Clone(curMonthLen);
					maxLengthMonth = XVar.Clone(curMontn);
				}
			}
			return maxLengthMonth;
		}
	}
}
