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
	public partial class FilterIntervalTimeSlider : FilterIntervalDateSlider
	{
		protected dynamic baseDateArray = XVar.Array();
		protected static bool skipFilterIntervalTimeSliderCtor = false;
		public FilterIntervalTimeSlider(dynamic _param_fName, dynamic _param_pageObject, dynamic _param_id, dynamic _param_viewControls)
			:base((XVar)_param_fName, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_viewControls)
		{
			if(skipFilterIntervalTimeSliderCtor)
			{
				skipFilterIntervalTimeSliderCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.baseDateArray = XVar.Clone(new XVar(0, 1970, 1, 1, 2, 1, 3, 0, 4, 0, 5, 0));
		}
		protected override XVar getDateTimeArray(dynamic _param_value, dynamic _param_forCaption = null)
		{
			#region default values
			if(_param_forCaption as Object == null) _param_forCaption = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic forCaption = XVar.Clone(_param_forCaption);
			#endregion

			dynamic baseDateArray = null, timeInSeonds = null;
			if(XVar.Pack(forCaption))
			{
				dynamic timeArray = XVar.Array();
				timeArray = XVar.Clone(CommonFunctions.parsenumbers((XVar)(value)));
				return new XVar(0, 0, 1, 0, 2, 0, 3, timeArray[0], 4, timeArray[1], 5, timeArray[2]);
			}
			timeInSeonds = XVar.Clone(getValueInSeconds((XVar)(value)));
			baseDateArray = XVar.Clone(this.baseDateArray);
			return CommonFunctions.addSeconds((XVar)(baseDateArray), (XVar)(timeInSeonds));
		}
		protected override XVar getValueInSeconds(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic timeArray = XVar.Array();
			timeArray = XVar.Clone(CommonFunctions.parsenumbers((XVar)(value)));
			return (timeArray[2] + timeArray[1] * 60) + timeArray[0] * 3600;
		}
		protected override XVar getSecondsCaption(dynamic _param_dateArray)
		{
			#region pass-by-value parameters
			dynamic dateArray = XVar.Clone(_param_dateArray);
			#endregion

			return CommonFunctions.str_format_time((XVar)(dateArray));
		}
		protected override XVar getMinutesCaption(dynamic _param_dateArray)
		{
			#region pass-by-value parameters
			dynamic dateArray = XVar.Clone(_param_dateArray);
			#endregion

			dynamic timeFormatString = null;
			dateArray.InitAndSetArrayItem(0, 5);
			timeFormatString = XVar.Clone(MVCFunctions.str_replace((XVar)(MVCFunctions.Concat(GlobalVars.locale_info["LOCALE_STIME"], "ss")), new XVar(""), (XVar)(GlobalVars.locale_info["LOCALE_STIMEFORMAT"])));
			return CommonFunctions.format_datetime_custom((XVar)(dateArray), (XVar)(timeFormatString));
		}
		protected override XVar getHoursCaption(dynamic _param_dateArray, dynamic _param_isLower)
		{
			#region pass-by-value parameters
			dynamic dateArray = XVar.Clone(_param_dateArray);
			dynamic isLower = XVar.Clone(_param_isLower);
			#endregion

			dynamic hours = null, timeFormatString = null;
			hours = XVar.Clone(getAdjustedHoursValue((XVar)(dateArray[3]), (XVar)(dateArray[4]), (XVar)(isLower)));
			dateArray.InitAndSetArrayItem(dateArray.InitAndSetArrayItem(dateArray.InitAndSetArrayItem(0, 5), 4), 3);
			dateArray = XVar.Clone(CommonFunctions.addHours((XVar)(dateArray), (XVar)(hours)));
			timeFormatString = XVar.Clone(MVCFunctions.str_replace((XVar)(MVCFunctions.Concat(GlobalVars.locale_info["LOCALE_STIME"], "ss")), new XVar(""), (XVar)(GlobalVars.locale_info["LOCALE_STIMEFORMAT"])));
			return CommonFunctions.format_datetime_custom((XVar)(dateArray), (XVar)(timeFormatString));
		}
		protected override XVar fieldHasNoRange(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic maxSec = null, minSec = null, step = null;
			if((XVar)((XVar)(data["sliderMin"] == null)  && (XVar)(data["sliderMax"] == null))  || (XVar)(data["sliderMax"] == data["sliderMin"]))
			{
				return true;
			}
			step = XVar.Clone(getStepValue());
			minSec = XVar.Clone(getValueInSeconds((XVar)(data["sliderMin"])));
			maxSec = XVar.Clone(getValueInSeconds((XVar)(data["sliderMax"])));
			if(maxSec - minSec < step)
			{
				return true;
			}
			return false;
		}
		protected override XVar getCaptionSpansHTML()
		{
			dynamic captionSpans = null, maxSpan = null, minSpan = null;
			minSpan = XVar.Clone(MVCFunctions.Concat("<span class=\"slider-min\">", getMinSpanValue(), "</span>"));
			maxSpan = XVar.Clone(MVCFunctions.Concat("<span class=\"slider-max\">", getMaxSpanValue(), "</span>"));
			captionSpans = XVar.Clone(MVCFunctions.Concat(minSpan, "&nbsp;-&nbsp", maxSpan));
			return captionSpans;
		}
		public override XVar addFilterControlToControlsMap(dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			dynamic ctrlsMap = XVar.Array();
			ctrlsMap = XVar.Clone(getBaseContolsMapParams());
			ctrlsMap.InitAndSetArrayItem(true, "isFieldTimeType");
			pageObj.controlsMap.InitAndSetArrayItem(ctrlsMap, "filters", "controls", null);
			return null;
		}
		public static XVar getTimeSliderWhere(dynamic _param_fName, dynamic _param_pSet_packed, dynamic _param_cipherer, dynamic _param_table, dynamic _param_SearchFor, dynamic _param_SearchFor2, dynamic _param_strSearchOption, dynamic _param_fullFieldName)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic table = XVar.Clone(_param_table);
			dynamic SearchFor = XVar.Clone(_param_SearchFor);
			dynamic SearchFor2 = XVar.Clone(_param_SearchFor2);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic fullFieldName = XVar.Clone(_param_fullFieldName);
			#endregion

			dynamic cleanvalue2 = null, firstDelimPos = null, hours = null, lastDelimPos = null, minutes = null, seconds = null, stepType = null, timeArr = XVar.Array(), value1 = null, value2 = null;
			firstDelimPos = XVar.Clone(MVCFunctions.strpos((XVar)(SearchFor), new XVar(":")));
			lastDelimPos = XVar.Clone(MVCFunctions.strrpos((XVar)(SearchFor), new XVar(":")));
			if((XVar)(XVar.Equals(XVar.Pack(firstDelimPos), XVar.Pack(false)))  || (XVar)(firstDelimPos == lastDelimPos))
			{
				return "";
			}
			stepType = XVar.Clone(pSet.getFilterStepType((XVar)(fName)));
			value1 = XVar.Clone(cipherer.MakeDBValue((XVar)(fName), (XVar)(SearchFor), new XVar(""), new XVar(true)));
			switch(((XVar)strSearchOption).ToString())
			{
				case "slider":
					firstDelimPos = XVar.Clone(MVCFunctions.strpos((XVar)(SearchFor2), new XVar(":")));
					lastDelimPos = XVar.Clone(MVCFunctions.strrpos((XVar)(SearchFor2), new XVar(":")));
					if((XVar)(XVar.Equals(XVar.Pack(firstDelimPos), XVar.Pack(false)))  || (XVar)(firstDelimPos == lastDelimPos))
					{
						return "";
					}
					cleanvalue2 = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(fName), (XVar)(SearchFor2), new XVar("")));
					timeArr = XVar.Clone(CommonFunctions.parsenumbers((XVar)(cleanvalue2)));
					if(stepType == Constants.FSST_SECONDS)
					{
						timeArr = XVar.Clone(CommonFunctions.addSecondsToTime((XVar)(timeArr), new XVar(1)));
					}
					else
					{
						timeArr = XVar.Clone(CommonFunctions.addMinutesToTime((XVar)(timeArr), new XVar(1)));
					}
					hours = XVar.Clone((XVar.Pack(timeArr[0] < 10) ? XVar.Pack(MVCFunctions.Concat("0", timeArr[0])) : XVar.Pack(timeArr[0])));
					minutes = XVar.Clone((XVar.Pack(timeArr[1] < 10) ? XVar.Pack(MVCFunctions.Concat("0", timeArr[1])) : XVar.Pack(timeArr[1])));
					seconds = XVar.Clone((XVar.Pack(timeArr[2] < 10) ? XVar.Pack(MVCFunctions.Concat("0", timeArr[2])) : XVar.Pack(timeArr[2])));
					value2 = XVar.Clone(MVCFunctions.Concat(hours, ":", minutes, ":", seconds));
					value2 = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(fName), (XVar)(value2), (XVar)(table)));
					return MVCFunctions.Concat(fullFieldName, ">=", value1, " and ", fullFieldName, "<", value2);
				case "moreequal":
					return MVCFunctions.Concat(fullFieldName, ">=", value1);
				case "lessequal":
					return MVCFunctions.Concat(fullFieldName, "<=", value1);
				default:
					return "";
			}
			return null;
		}
	}
}
