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
	public partial class FilterIntervalSlider : FilterControl
	{
		protected dynamic knobsType;
		protected dynamic stepValue;
		protected dynamic minValue;
		protected dynamic maxValue;
		protected dynamic minKnobValue;
		protected dynamic maxKnobValue;
		protected static bool skipFilterIntervalSliderCtor = false;
		public FilterIntervalSlider(dynamic _param_fName, dynamic _param_pageObject, dynamic _param_id, dynamic _param_viewControls)
			:base((XVar)_param_fName, (XVar)_param_pageObject, (XVar)_param_id, (XVar)_param_viewControls)
		{
			if(skipFilterIntervalSliderCtor)
			{
				skipFilterIntervalSliderCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic viewControls = XVar.Clone(_param_viewControls);
			#endregion

			this.filterFormat = new XVar(Constants.FF_INTERVAL_SLIDER);
			this.useApllyBtn = XVar.Clone(this.pSet.isFilterApplyBtnSet((XVar)(fName)));
			this.knobsType = XVar.Clone(this.pSet.getFilterKnobsType((XVar)(fName)));
			this.stepValue = XVar.Clone(this.pSet.getFilterStepValue((XVar)(fName)));
			buildSQL();
			addJS_CSSfiles((XVar)(pageObject));
			if(XVar.Pack(this.filtered))
			{
				assignKnobsValues();
			}
			this.showCollapsed = XVar.Clone(this.pSet.showCollapsed((XVar)(fName)));
			this.separator = XVar.Clone(getSeparator());
		}
		protected virtual XVar assignKnobsValues()
		{
			dynamic filterValues = XVar.Array();
			filterValues = XVar.Clone(this.filteredFields[this.fName]["values"]);
			if(this.knobsType == Constants.FS_MIN_ONLY)
			{
				this.minKnobValue = XVar.Clone(filterValues[0]);
				return null;
			}
			if(this.knobsType == Constants.FS_MAX_ONLY)
			{
				this.maxKnobValue = XVar.Clone(filterValues[0]);
				return null;
			}
			this.minKnobValue = XVar.Clone(filterValues[0]);
			this.maxKnobValue = XVar.Clone(filterValues[1]);
			return null;
		}
		protected virtual XVar getSeparator()
		{
			if(this.knobsType == Constants.FS_MIN_ONLY)
			{
				return "~moreequal~";
			}
			if(this.knobsType == Constants.FS_MAX_ONLY)
			{
				return "~lessequal~";
			}
			return "~slider~";
		}
		protected override XVar buildSQL()
		{
			dynamic wName = null;
			wName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.fName)));
			this.strSQL = XVar.Clone(MVCFunctions.Concat("select min(", wName, ") as ", this.connection.addFieldWrappers(new XVar("sliderMin")), ", max(", wName, ") as ", this.connection.addFieldWrappers(new XVar("sliderMax"))));
			this.strSQL = MVCFunctions.Concat(this.strSQL, " from ( ", buildBasicSQL(), " ) a");
			this.strSQL = MVCFunctions.Concat(this.strSQL, " where ", MVCFunctions.implode(new XVar(" and "), (XVar)(getNotNullWhere())));
			return null;
		}
		protected override XVar addFilterBlocksFromDB(dynamic filterCtrlBlocks)
		{
			dynamic data = null, filterControl = null;
			data = XVar.Clone(this.connection.query((XVar)(this.strSQL)).fetchAssoc());
			decryptDataRow((XVar)(data));
			if(XVar.Pack(fieldHasNoRange((XVar)(data))))
			{
				return filterCtrlBlocks;
			}
			filterControl = XVar.Clone(buildControl((XVar)(data)));
			filterCtrlBlocks.InitAndSetArrayItem(getFilterBlockStructure((XVar)(filterControl)), null);
			return null;
		}
		protected virtual XVar fieldHasNoRange(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if((XVar)((XVar)(data["sliderMin"] == null)  && (XVar)(data["sliderMax"] == null))  || (XVar)(data["sliderMax"] == data["sliderMin"]))
			{
				return true;
			}
			return false;
		}
		protected virtual XVar buildControl(dynamic _param_data, dynamic _param_parentFiltersData = null)
		{
			#region default values
			if(_param_parentFiltersData as Object == null) _param_parentFiltersData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			dynamic parentFiltersData = XVar.Clone(_param_parentFiltersData);
			#endregion

			this.minValue = XVar.Clone(data["sliderMin"]);
			this.maxValue = XVar.Clone(data["sliderMax"]);
			if(XVar.Pack(!(XVar)(this.filtered)))
			{
				this.minKnobValue = XVar.Clone(data["sliderMin"]);
				this.maxKnobValue = XVar.Clone(data["sliderMax"]);
			}
			else
			{
				if(this.knobsType == Constants.FS_MAX_ONLY)
				{
					this.minKnobValue = XVar.Clone(data["sliderMin"]);
				}
				if(this.knobsType == Constants.FS_MIN_ONLY)
				{
					this.maxKnobValue = XVar.Clone(data["sliderMax"]);
				}
			}
			return getSliderHTML();
		}
		protected virtual XVar getCaptionSpansHTML()
		{
			dynamic captionSpans = null, maxSpan = null, minSpan = null, postfixSpan = null, prefixSpan = null;
			minSpan = XVar.Clone(MVCFunctions.Concat("<span class=\"slider-min\">", getMinSpanValue(), "</span>"));
			maxSpan = XVar.Clone(MVCFunctions.Concat("<span class=\"slider-max\">", getMaxSpanValue(), "</span>"));
			captionSpans = XVar.Clone(MVCFunctions.Concat(minSpan, "&nbsp;-&nbsp", maxSpan));
			prefixSpan = new XVar("<span class=\"slider-caption-prefix\"></span>");
			postfixSpan = new XVar("<span class=\"slider-caption-postfix\"></span>");
			captionSpans = XVar.Clone(MVCFunctions.Concat(prefixSpan, captionSpans, postfixSpan));
			return captionSpans;
		}
		protected virtual XVar getSliderHTML()
		{
			dynamic captionSpans = null, filterControl = null;
			captionSpans = XVar.Clone(getCaptionSpansHTML());
			filterControl = XVar.Clone(MVCFunctions.Concat("<div id=\"slider_values_", this.gfName, "\" class=\"filter-slider-values\">", captionSpans, "</div>"));
			filterControl = MVCFunctions.Concat(filterControl, "<div id=\"slider_", this.gfName, "\" class=\"filter-slider\"></div>");
			return filterControl;
		}
		protected virtual XVar getMinSpanValue()
		{
			dynamic minSpanValue = null, viewFormat = null;
			minSpanValue = XVar.Clone(this.minKnobValue);
			if(minSpanValue < this.minValue)
			{
				minSpanValue = XVar.Clone(this.minValue);
			}
			viewFormat = XVar.Clone(this.viewControl.viewFormat);
			if((XVar)(viewFormat == Constants.FORMAT_CURRENCY)  || (XVar)(viewFormat == Constants.FORMAT_NUMBER))
			{
				dynamic data = null;
				data = XVar.Clone(new XVar(this.fName, minSpanValue));
				minSpanValue = XVar.Clone(this.viewControl.showDBValue((XVar)(data), new XVar("")));
			}
			return minSpanValue;
		}
		protected virtual XVar getMaxSpanValue()
		{
			dynamic maxSpanValue = null, viewFormat = null;
			maxSpanValue = XVar.Clone(this.maxKnobValue);
			if(this.maxValue < maxSpanValue)
			{
				maxSpanValue = XVar.Clone(this.maxValue);
			}
			viewFormat = XVar.Clone(this.viewControl.viewFormat);
			if((XVar)(viewFormat == Constants.FORMAT_CURRENCY)  || (XVar)(viewFormat == Constants.FORMAT_NUMBER))
			{
				dynamic data = null;
				data = XVar.Clone(new XVar(this.fName, maxSpanValue));
				maxSpanValue = XVar.Clone(this.viewControl.showDBValue((XVar)(data), new XVar("")));
			}
			return maxSpanValue;
		}
		public override XVar addFilterControlToControlsMap(dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			dynamic ctrlsMap = XVar.Array(), viewFomat = null;
			ctrlsMap = XVar.Clone(getBaseContolsMapParams());
			ctrlsMap.InitAndSetArrayItem(this.minValue, "minValue");
			ctrlsMap.InitAndSetArrayItem(this.maxValue, "maxValue");
			ctrlsMap.InitAndSetArrayItem(round((XVar)(this.minValue), new XVar(true)), "roundedMin");
			ctrlsMap.InitAndSetArrayItem(round((XVar)(this.maxValue), new XVar(false)), "roundedMax");
			ctrlsMap.InitAndSetArrayItem(round((XVar)(this.minKnobValue), new XVar(true)), "roundedMinKnobValue");
			ctrlsMap.InitAndSetArrayItem(round((XVar)(this.maxKnobValue), new XVar(false)), "roundedMaxKnobValue");
			if(XVar.Pack(this.filtered))
			{
				ctrlsMap.InitAndSetArrayItem(this.filteredFields[this.fName]["values"], "defaultValuesArray");
				ctrlsMap.InitAndSetArrayItem(this.minKnobValue, "minKnobValue");
				ctrlsMap.InitAndSetArrayItem(this.maxKnobValue, "maxKnobValue");
			}
			viewFomat = XVar.Clone(this.viewControl.viewFormat);
			ctrlsMap.InitAndSetArrayItem(viewFomat == Constants.FORMAT_NUMBER, "viewAsNumber");
			ctrlsMap.InitAndSetArrayItem(viewFomat == Constants.FORMAT_CURRENCY, "viewAsCurrency");
			if(XVar.Equals(XVar.Pack(viewFomat), XVar.Pack(Constants.FORMAT_CURRENCY)))
			{
				ctrlsMap.InitAndSetArrayItem(getCurrencySettings(), "formatSettings");
			}
			else
			{
				if(viewFomat == Constants.FORMAT_NUMBER)
				{
					ctrlsMap.InitAndSetArrayItem(getNumberSettings(), "formatSettings");
				}
			}
			if((XVar)(XVar.Equals(XVar.Pack(viewFomat), XVar.Pack(Constants.FORMAT_CURRENCY)))  || (XVar)(viewFomat == Constants.FORMAT_NUMBER))
			{
				ctrlsMap.InitAndSetArrayItem(getCommonFormatSettings((XVar)(viewFomat)), "commonFormatSettings");
			}
			pageObj.controlsMap.InitAndSetArrayItem(ctrlsMap, "filters", "controls", null);
			return null;
		}
		protected virtual XVar getCurrencySettings()
		{
			dynamic currencySettings = XVar.Array();
			currencySettings = XVar.Clone(XVar.Array());
			currencySettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_ICURRENCY"], "LOCALE_ICURRENCY");
			currencySettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_INEGCURR"], "LOCALE_INEGCURR");
			currencySettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SCURRENCY"], "LOCALE_SCURRENCY");
			return currencySettings;
		}
		protected virtual XVar getNumberSettings()
		{
			dynamic numberSettings = XVar.Array();
			numberSettings = XVar.Clone(XVar.Array());
			numberSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SPOSITIVESIGN"], "LOCALE_SPOSITIVESIGN");
			numberSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_INEGNUMBER"], "LOCALE_INEGNUMBER");
			return numberSettings;
		}
		protected virtual XVar getCommonFormatSettings(dynamic _param_viewFomat)
		{
			#region pass-by-value parameters
			dynamic viewFomat = XVar.Clone(_param_viewFomat);
			#endregion

			dynamic formatSettings = XVar.Array();
			formatSettings = XVar.Clone(XVar.Array());
			if(XVar.Equals(XVar.Pack(viewFomat), XVar.Pack(Constants.FORMAT_CURRENCY)))
			{
				formatSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_ICURRDIGITS"], "decimalDigits");
				formatSettings.InitAndSetArrayItem(MVCFunctions.explode(new XVar(";"), (XVar)(GlobalVars.locale_info["LOCALE_SMONGROUPING"])), "grouping");
				formatSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SMONTHOUSANDSEP"], "thousandSep");
				formatSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SMONDECIMALSEP"], "decimalSep");
			}
			if(viewFomat == Constants.FORMAT_NUMBER)
			{
				formatSettings.InitAndSetArrayItem(this.pSet.isDecimalDigits((XVar)(this.fName)), "decimalDigits");
				formatSettings.InitAndSetArrayItem(MVCFunctions.explode(new XVar(";"), (XVar)(GlobalVars.locale_info["LOCALE_SGROUPING"])), "grouping");
				formatSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_STHOUSAND"], "thousandSep");
				formatSettings.InitAndSetArrayItem(GlobalVars.locale_info["LOCALE_SDECIMAL"], "decimalSep");
			}
			return formatSettings;
		}
		protected override XVar getBaseContolsMapParams()
		{
			dynamic ctrlsMap = XVar.Array();
			ctrlsMap = XVar.Clone(XVar.Array());
			ctrlsMap.InitAndSetArrayItem(this.fName, "fieldName");
			ctrlsMap.InitAndSetArrayItem(this.gfName, "gfieldName");
			ctrlsMap.InitAndSetArrayItem(this.filterFormat, "filterFormat");
			ctrlsMap.InitAndSetArrayItem(this.filtered, "filtered");
			ctrlsMap.InitAndSetArrayItem(this.separator, "separator");
			ctrlsMap.InitAndSetArrayItem(this.knobsType, "knobsType");
			ctrlsMap.InitAndSetArrayItem(this.useApllyBtn, "useApllyBtn");
			ctrlsMap.InitAndSetArrayItem(getStepValue(), "step");
			ctrlsMap.InitAndSetArrayItem(this.showCollapsed, "collapsed");
			return ctrlsMap;
		}
		protected virtual XVar getStepValue()
		{
			return this.stepValue;
		}
		protected virtual XVar round(dynamic _param_value, dynamic _param_min)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic min = XVar.Clone(_param_min);
			#endregion

			dynamic step = null;
			step = XVar.Clone(this.stepValue);
			if(XVar.Pack(min))
			{
				return (XVar)Math.Floor((double)(value / step)) * step;
			}
			return (XVar)Math.Ceiling((double)(value / step)) * step;
		}
		protected virtual XVar addJS_CSSfiles(dynamic _param_pageObject)
		{
			#region pass-by-value parameters
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			pageObject.AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
			return null;
		}
		public override XVar buildFilterCtrlBlockArray(dynamic _param_pageObj, dynamic _param_dFilterBlocks = null)
		{
			#region default values
			if(_param_dFilterBlocks as Object == null) _param_dFilterBlocks = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			dynamic dFilterBlocks = XVar.Clone(_param_dFilterBlocks);
			#endregion

			dynamic filterCtrlBlocks = null;
			filterCtrlBlocks = XVar.Clone(XVar.Array());
			addFilterBlocksFromDB((XVar)(filterCtrlBlocks));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(filterCtrlBlocks))))
			{
				this.visible = new XVar(false);
			}
			if(XVar.Pack(this.visible))
			{
				addFilterControlToControlsMap((XVar)(pageObj));
			}
			return filterCtrlBlocks;
		}
	}
}
