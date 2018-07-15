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
	public partial class CrossTableReport : XClass
	{
		protected dynamic tableName;
		protected dynamic pageType;
		protected dynamic viewControls;
		protected ProjectSettings pSet = null;
		protected dynamic connection;
		protected dynamic group_header = XVar.Array();
		protected dynamic col_summary = XVar.Array();
		protected dynamic rowinfo = XVar.Array();
		protected dynamic total_summary;
		protected dynamic showXSummary;
		protected dynamic showYSummary;
		protected dynamic showTotalSummary;
		protected dynamic groupFieldsData;
		protected dynamic fieldsTotalsData;
		protected dynamic xFName;
		protected dynamic yFName;
		protected dynamic xIntervalType;
		protected dynamic yIntervalType;
		protected dynamic dataField = XVar.Pack("");
		protected dynamic dataFieldSettings = XVar.Pack(null);
		protected dynamic dataGroupFunction = XVar.Pack("");
		public CrossTableReport(dynamic _param_params, dynamic _param_strSQL)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			dynamic strSQL = XVar.Clone(_param_strSQL);
			#endregion

			this.pageType = XVar.Clone(var_params["pageType"]);
			this.tableName = XVar.Clone(var_params["tableName"]);
			setDbConnection();
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.tableName), new XVar(Constants.PAGE_REPORT)));
			this.viewControls = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), new XVar(Constants.PAGE_REPORT)));
			this.groupFieldsData = XVar.Clone(var_params["groupFields"]);
			this.fieldsTotalsData = XVar.Clone(var_params["totals"]);
			this.showXSummary = XVar.Clone(var_params["xSummary"]);
			this.showYSummary = XVar.Clone(var_params["ySummary"]);
			this.showTotalSummary = XVar.Clone(var_params["totalSummary"]);
			this.xFName = XVar.Clone(var_params["x"]);
			this.yFName = XVar.Clone(var_params["y"]);
			this.dataField = XVar.Clone(var_params["data"]);
			this.dataFieldSettings = XVar.Clone(var_params["totals"][this.dataField]);
			this.dataGroupFunction = XVar.Clone(getDataGroupFunction((XVar)(var_params["operation"])));
			this.xIntervalType = XVar.Clone(getIntervalTypeByParam(new XVar("x"), (XVar)(this.xFName), (XVar)(var_params["xType"])));
			this.yIntervalType = XVar.Clone(getIntervalTypeByParam(new XVar("y"), (XVar)(this.yFName), (XVar)(var_params["yType"])));
			fillGridData((XVar)(strSQL), (XVar)(var_params["headerClass"]), (XVar)(var_params["dataClass"]));
		}
		protected virtual XVar fillGridData(dynamic _param_tableSQL, dynamic _param_headerClass, dynamic _param_dataClass)
		{
			#region pass-by-value parameters
			dynamic tableSQL = XVar.Clone(_param_tableSQL);
			dynamic headerClass = XVar.Clone(_param_headerClass);
			dynamic dataClass = XVar.Clone(_param_dataClass);
			#endregion

			dynamic SQL = null, arravgcount = XVar.Array(), arravgsum = XVar.Array(), arrdata = XVar.Array(), avgcountx = XVar.Array(), avgsumx = XVar.Array(), data = XVar.Array(), group_x = XVar.Array(), group_y = XVar.Array(), key_x = null, key_y = null, qResult = null, sort_y = XVar.Array();
			SQL = XVar.Clone(getstrSQL((XVar)(tableSQL)));
			qResult = XVar.Clone(this.connection.query((XVar)(SQL)));
			group_y = XVar.Clone(XVar.Array());
			group_x = XVar.Clone(XVar.Array());
			sort_y = XVar.Clone(XVar.Array());
			arrdata = XVar.Clone(XVar.Array());
			arravgsum = XVar.Clone(XVar.Array());
			arravgcount = XVar.Clone(XVar.Array());
			avgsumx = XVar.Clone(XVar.Array());
			avgcountx = XVar.Clone(XVar.Array());
			while(XVar.Pack(data = XVar.Clone(qResult.fetchNumeric())))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(data[1]), (XVar)(group_y)))))
				{
					group_y.InitAndSetArrayItem(data[1], null);
					sort_y.InitAndSetArrayItem(MVCFunctions.count(sort_y), null);
				}
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(data[2]), (XVar)(group_x)))))
				{
					group_x.InitAndSetArrayItem(data[2], null);
					this.col_summary.InitAndSetArrayItem("&nbsp;", "data", MVCFunctions.count(group_x) - 1, "col_summary");
					this.col_summary.InitAndSetArrayItem(MVCFunctions.Concat("total_x_", MVCFunctions.count(group_x) - 1), "data", MVCFunctions.count(group_x) - 1, "id_col_summary");
				}
				key_y = XVar.Clone(MVCFunctions.array_search((XVar)(data[1]), (XVar)(group_y)));
				key_x = XVar.Clone(MVCFunctions.array_search((XVar)(data[2]), (XVar)(group_x)));
				avgsumx.InitAndSetArrayItem(0, key_x);
				avgcountx.InitAndSetArrayItem(0, key_x);
				arrdata.InitAndSetArrayItem(data[0], key_y, key_x);
				arravgsum.InitAndSetArrayItem(data[3], key_y, key_x);
				arravgcount.InitAndSetArrayItem(data[4], key_y, key_x);
			}
			GlobalVars.group_sort_y = XVar.Clone(group_y);
			MVCFunctions.SortForCrossTable((XVar)(sort_y));
			this.rowinfo = XVar.Clone(getBasicRowsData((XVar)(sort_y), (XVar)(group_y), (XVar)(group_x), (XVar)(arrdata), (XVar)(dataClass)));
			foreach (KeyValuePair<XVar, dynamic> value_x in group_x.GetEnumerator())
			{
				if(value_x.Value != XVar.Pack(""))
				{
					this.group_header.InitAndSetArrayItem(getAxisDisplayValue((XVar)(this.xFName), (XVar)(this.xIntervalType), (XVar)(value_x.Value)), "data", value_x.Key, "gr_value");
				}
				else
				{
					this.group_header.InitAndSetArrayItem("&nbsp;", "data", value_x.Key, "gr_value");
				}
				this.group_header.InitAndSetArrayItem(headerClass, "data", value_x.Key, "gr_x_class");
			}
			setSummariesData((XVar)(arravgsum), (XVar)(arravgcount), (XVar)(avgsumx), (XVar)(avgcountx));
			updateRecordsDisplayedFields();
			return null;
		}
		protected virtual XVar getBasicRowsData(dynamic _param_sort_y, dynamic _param_group_y, dynamic _param_group_x, dynamic _param_arrdata, dynamic _param_dataClass)
		{
			#region pass-by-value parameters
			dynamic sort_y = XVar.Clone(_param_sort_y);
			dynamic group_y = XVar.Clone(_param_group_y);
			dynamic group_x = XVar.Clone(_param_group_x);
			dynamic arrdata = XVar.Clone(_param_arrdata);
			dynamic dataClass = XVar.Clone(_param_dataClass);
			#endregion

			dynamic crossRowsData = XVar.Array(), ftype = null;
			crossRowsData = XVar.Clone(XVar.Array());
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(this.dataField)));
			foreach (KeyValuePair<XVar, dynamic> key_y in sort_y.GetEnumerator())
			{
				dynamic value_y = null;
				value_y = XVar.Clone(group_y[key_y.Value]);
				crossRowsData.InitAndSetArrayItem("&nbsp;", key_y.Value, "row_summary");
				crossRowsData.InitAndSetArrayItem(getAxisDisplayValue((XVar)(this.yFName), (XVar)(this.yIntervalType), (XVar)(value_y)), key_y.Value, "group_y");
				crossRowsData.InitAndSetArrayItem(MVCFunctions.Concat("total_y_", key_y.Value), key_y.Value, "id_row_summary");
				crossRowsData.InitAndSetArrayItem(dataClass, key_y.Value, "summary_class");
				if(XVar.Pack(!(XVar)(arrdata.KeyExists(key_y.Value))))
				{
					continue;
				}
				foreach (KeyValuePair<XVar, dynamic> value_x in group_x.GetEnumerator())
				{
					dynamic rowValue = null;
					rowValue = new XVar("&nbsp;");
					if((XVar)(arrdata[key_y.Value].KeyExists(value_x.Key))  && (XVar)(!(XVar)(arrdata[key_y.Value][value_x.Key] == null)))
					{
						rowValue = XVar.Clone(arrdata[key_y.Value][value_x.Key]);
						if((XVar)(this.dataGroupFunction == "avg")  && (XVar)(!(XVar)(CommonFunctions.IsTimeType((XVar)(ftype)))))
						{
							rowValue = XVar.Clone((XVar)Math.Round((double)(rowValue), 2));
						}
					}
					crossRowsData.InitAndSetArrayItem(rowValue, key_y.Value, "row_record", "data", value_x.Key, "row_value");
					crossRowsData.InitAndSetArrayItem(dataClass, key_y.Value, "row_record", "data", value_x.Key, "row_value_class");
					crossRowsData.InitAndSetArrayItem(MVCFunctions.Concat(key_y.Value, "_", value_x.Key), key_y.Value, "row_record", "data", value_x.Key, "id_data");
				}
			}
			return crossRowsData;
		}
		protected virtual XVar setSummariesData(dynamic _param_arravgsum, dynamic _param_arravgcount, dynamic _param_avgsumx, dynamic _param_avgcountx)
		{
			#region pass-by-value parameters
			dynamic arravgsum = XVar.Clone(_param_arravgsum);
			dynamic arravgcount = XVar.Clone(_param_arravgcount);
			dynamic avgsumx = XVar.Clone(_param_avgsumx);
			dynamic avgcountx = XVar.Clone(_param_avgcountx);
			#endregion

			dynamic totaxSummary = null, xSummaty = null, ySummary = null;
			this.total_summary = new XVar("&nbsp;");
			xSummaty = XVar.Clone(XVar.Array());
			ySummary = XVar.Clone(XVar.Array());
			totaxSummary = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> yData in this.rowinfo.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> value in yData.Value["row_record"]["data"].GetEnumerator())
				{
					if(value.Value["row_value"] == "&nbsp;")
					{
						continue;
					}
					switch(((XVar)this.dataGroupFunction).ToString())
					{
						case "sum":
							this.rowinfo[yData.Key]["row_summary"] += value.Value["row_value"];
							this.col_summary["data"][value.Key]["col_summary"] += value.Value["row_value"];
							this.total_summary += value.Value["row_value"];
							break;
						case "min":
							if((XVar)(XVar.Equals(XVar.Pack(this.rowinfo[yData.Key]["row_summary"]), XVar.Pack("&nbsp;")))  || (XVar)(value.Value["row_value"] < this.rowinfo[yData.Key]["row_summary"]))
							{
								this.rowinfo.InitAndSetArrayItem(value.Value["row_value"], yData.Key, "row_summary");
							}
							if((XVar)(XVar.Equals(XVar.Pack(this.col_summary["data"][value.Key]["col_summary"]), XVar.Pack("&nbsp;")))  || (XVar)(value.Value["row_value"] < this.col_summary["data"][value.Key]["col_summary"]))
							{
								this.col_summary.InitAndSetArrayItem(value.Value["row_value"], "data", value.Key, "col_summary");
							}
							if((XVar)(XVar.Equals(XVar.Pack(this.total_summary), XVar.Pack("&nbsp;")))  || (XVar)(value.Value["row_value"] < this.total_summary))
							{
								this.total_summary = XVar.Clone(value.Value["row_value"]);
							}
							break;
						case "max":
							if((XVar)(XVar.Equals(XVar.Pack(this.rowinfo[yData.Key]["row_summary"]), XVar.Pack("&nbsp;")))  || (XVar)(this.rowinfo[yData.Key]["row_summary"] < value.Value["row_value"]))
							{
								this.rowinfo.InitAndSetArrayItem(value.Value["row_value"], yData.Key, "row_summary");
							}
							if((XVar)(XVar.Equals(XVar.Pack(this.col_summary["data"][value.Key]["col_summary"]), XVar.Pack("&nbsp;")))  || (XVar)(this.col_summary["data"][value.Key]["col_summary"] < value.Value["row_value"]))
							{
								this.col_summary.InitAndSetArrayItem(value.Value["row_value"], "data", value.Key, "col_summary");
							}
							if((XVar)(XVar.Equals(XVar.Pack(this.total_summary), XVar.Pack("&nbsp;")))  || (XVar)(this.total_summary < value.Value["row_value"]))
							{
								this.total_summary = XVar.Clone(value.Value["row_value"]);
							}
							break;
						case "avg":
							this.rowinfo[yData.Key]["avgsumy"] += arravgsum[yData.Key][value.Key];
							this.rowinfo[yData.Key]["avgcounty"] += arravgcount[yData.Key][value.Key];
							this.rowinfo[yData.Key]["row_record"]["data"][value.Key]["avgsumx"] += arravgsum[yData.Key][value.Key];
							this.rowinfo[yData.Key]["row_record"]["data"][value.Key]["avgcountx"] += arravgcount[yData.Key][value.Key];
							break;
					}
					if((XVar)(this.showXSummary)  && (XVar)(!(XVar)(this.col_summary["data"][value.Key]["col_summary"] == null)))
					{
						if(XVar.Pack(MVCFunctions.IsNumeric(this.col_summary["data"][value.Key]["col_summary"])))
						{
							this.col_summary.InitAndSetArrayItem((XVar)Math.Round((double)(this.col_summary["data"][value.Key]["col_summary"]), 2), "data", value.Key, "col_summary");
						}
					}
					else
					{
						this.col_summary.InitAndSetArrayItem("&nbsp;", "data", value.Key, "col_summary");
					}
				}
				if((XVar)(this.showYSummary)  && (XVar)(!(XVar)(this.rowinfo[yData.Key]["row_summary"] == null)))
				{
					if(XVar.Pack(MVCFunctions.IsNumeric(this.rowinfo[yData.Key]["row_summary"])))
					{
						this.rowinfo.InitAndSetArrayItem((XVar)Math.Round((double)(this.rowinfo[yData.Key]["row_summary"]), 2), yData.Key, "row_summary");
					}
				}
				else
				{
					this.rowinfo.InitAndSetArrayItem("&nbsp;", yData.Key, "row_summary");
				}
			}
			if(this.dataGroupFunction == "avg")
			{
				dynamic total_count = null, total_sum = null;
				total_sum = new XVar(0);
				total_count = new XVar(0);
				foreach (KeyValuePair<XVar, dynamic> valuey in this.rowinfo.GetEnumerator())
				{
					if(XVar.Pack(valuey.Value["avgcounty"]))
					{
						this.rowinfo.InitAndSetArrayItem((XVar)Math.Round((double)(valuey.Value["avgsumy"] / valuey.Value["avgcounty"]), 2), valuey.Key, "row_summary");
						total_sum += valuey.Value["avgsumy"];
						total_count += valuey.Value["avgcounty"];
					}
					foreach (KeyValuePair<XVar, dynamic> valuex in valuey.Value["row_record"]["data"].GetEnumerator())
					{
						if(XVar.Pack(valuex.Value["avgcountx"]))
						{
							avgsumx[valuex.Key] += valuex.Value["avgsumx"];
							avgcountx[valuex.Key] += valuex.Value["avgcountx"];
							total_sum += valuex.Value["avgsumx"];
							total_count += valuex.Value["avgcountx"];
						}
					}
				}
				foreach (KeyValuePair<XVar, dynamic> value in avgsumx.GetEnumerator())
				{
					if(XVar.Pack(avgcountx[value.Key]))
					{
						this.col_summary.InitAndSetArrayItem((XVar)Math.Round((double)(value.Value / avgcountx[value.Key]), 2), "data", value.Key, "col_summary");
					}
				}
				if(XVar.Pack(total_count))
				{
					this.total_summary = XVar.Clone(total_sum / total_count);
				}
			}
			if(XVar.Pack(!(XVar)(this.showTotalSummary)))
			{
				this.total_summary = new XVar("&nbsp;");
			}
			else
			{
				if(XVar.Pack(MVCFunctions.IsNumeric(this.total_summary)))
				{
					this.total_summary = XVar.Clone((XVar)Math.Round((double)(this.total_summary), 2));
				}
			}
			return null;
		}
		public virtual XVar getViewValue(dynamic _param_value, dynamic _param_useTimeFormat = null)
		{
			#region default values
			if(_param_useTimeFormat as Object == null) _param_useTimeFormat = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic useTimeFormat = XVar.Clone(_param_useTimeFormat);
			#endregion

			dynamic strViewFormat = null;
			strViewFormat = XVar.Clone(this.pSet.getViewFormat((XVar)(this.dataField)));
			if((XVar)(strViewFormat == Constants.FORMAT_TIME)  && (XVar)(MVCFunctions.IsNumeric(value)))
			{
				dynamic d = null, h = null, m = null, s = null;
				d = XVar.Clone(MVCFunctions.intval((XVar)(value / 86400)));
				h = XVar.Clone(MVCFunctions.intval((XVar)((value  %  86400) / 3600)));
				m = XVar.Clone(MVCFunctions.intval((XVar)(((value  %  86400)  %  3600) / 60)));
				s = XVar.Clone(((value  %  86400)  %  3600)  %  60);
				value = XVar.Clone((XVar.Pack(XVar.Pack(0) < d) ? XVar.Pack(MVCFunctions.Concat(d, "d ")) : XVar.Pack("")));
				if(XVar.Pack(useTimeFormat))
				{
					value = MVCFunctions.Concat(value, CommonFunctions.str_format_time((XVar)(new XVar(0, 0, 1, 0, 2, 0, 3, h, 4, m, 5, s))));
				}
				else
				{
					value = MVCFunctions.Concat(value, MVCFunctions.date(new XVar("H:i:s"), (XVar)(MVCFunctions.strtotime((XVar)(MVCFunctions.Concat(h, ":", m, ":", s))))));
				}
			}
			else
			{
				dynamic control = null, controlData = null;
				control = XVar.Clone(this.viewControls.getControl((XVar)(this.dataField)));
				controlData = XVar.Clone(new XVar(this.dataField, value));
				value = XVar.Clone(control.showDBValue((XVar)(controlData), new XVar("")));
			}
			return value;
		}
		protected virtual XVar updateRecordsDisplayedFields()
		{
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this.rowinfo))))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> data in this.rowinfo.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> fieldData in data.Value["row_record"]["data"].GetEnumerator())
				{
					if(fieldData.Value["row_value"] == "&nbsp;")
					{
						continue;
					}
					this.rowinfo.InitAndSetArrayItem(getViewValue((XVar)(fieldData.Value["row_value"])), data.Key, "row_record", "data", fieldData.Key, "row_value");
				}
				if(data.Value["row_summary"] != "&nbsp;")
				{
					this.rowinfo.InitAndSetArrayItem(getViewValue((XVar)(data.Value["row_summary"]), new XVar(false)), data.Key, "row_summary");
				}
			}
			if(this.total_summary != "&nbsp;")
			{
				this.total_summary = XVar.Clone(getViewValue((XVar)(this.total_summary), new XVar(false)));
			}
			foreach (KeyValuePair<XVar, dynamic> summaryData in this.col_summary["data"].GetEnumerator())
			{
				if(summaryData.Value["col_summary"] == "&nbsp;")
				{
					continue;
				}
				this.col_summary.InitAndSetArrayItem(getViewValue((XVar)(summaryData.Value["col_summary"]), new XVar(false)), "data", summaryData.Key, "col_summary");
			}
			return null;
		}
		public virtual XVar getCrossTableData()
		{
			return this.rowinfo;
		}
		public virtual XVar isEmpty()
		{
			return !(XVar)(MVCFunctions.count(this.rowinfo));
		}
		public virtual XVar getCrossTableHeader()
		{
			return this.group_header;
		}
		public virtual XVar getCrossTableSummary()
		{
			return this.col_summary;
		}
		public virtual XVar getTotalSummary()
		{
			return this.total_summary;
		}
		protected virtual XVar setDbConnection()
		{
			this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.tableName)));
			return null;
		}
		protected virtual XVar getIntervalTypeByParam(dynamic _param_axis, dynamic _param_crossName, dynamic _param_userIntType)
		{
			#region pass-by-value parameters
			dynamic axis = XVar.Clone(_param_axis);
			dynamic crossName = XVar.Clone(_param_crossName);
			dynamic userIntType = XVar.Clone(_param_userIntType);
			#endregion

			dynamic iType = null, intTypes = XVar.Array(), int_type = null;
			iType = XVar.Clone(getRefineIntervalType((XVar)(userIntType), (XVar)(crossName)));
			int_type = XVar.Clone(-1);
			intTypes = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fData in this.groupFieldsData.GetEnumerator())
			{
				if((XVar)(fData.Value["name"] == crossName)  && (XVar)((XVar)(fData.Value["group_type"] == "all")  || (XVar)(fData.Value["group_type"] == axis)))
				{
					if((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(userIntType))))  || (XVar)(iType == fData.Value["int_type"]))
					{
						int_type = XVar.Clone(fData.Value["int_type"]);
						break;
					}
					intTypes.InitAndSetArrayItem(fData.Value["int_type"], null);
				}
			}
			if(int_type != -1)
			{
				return int_type;
			}
			if(0 < MVCFunctions.count(intTypes))
			{
				return intTypes[0];
			}
			return -1;
		}
		protected virtual XVar getstrSQL(dynamic _param_tableSQL)
		{
			#region pass-by-value parameters
			dynamic tableSQL = XVar.Clone(_param_tableSQL);
			#endregion

			dynamic avg_func = null, ftype = null, groupByClause = null, group_x = XVar.Array(), group_y = XVar.Array(), gx0 = null, gx1 = null, gy0 = null, gy1 = null, isTime = null, orderByClause = null, selectClause = null, select_field = null, whereClause = null;
			group_x = XVar.Clone(_getIntervalTypeData((XVar)(this.xFName), (XVar)(this.xIntervalType)));
			group_y = XVar.Clone(_getIntervalTypeData((XVar)(this.yFName), (XVar)(this.yIntervalType)));
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(this.dataField)));
			isTime = XVar.Clone((XVar)(this.pSet.getViewFormat((XVar)(this.dataField)) == Constants.FORMAT_TIME)  || (XVar)(CommonFunctions.IsTimeType((XVar)(ftype))));
			if(XVar.Pack(isTime))
			{
				select_field = XVar.Clone(MVCFunctions.Concat(this.dataGroupFunction, "(", this.connection.timeToSecWrapper((XVar)(this.dataField)), "), "));
			}
			else
			{
				select_field = XVar.Clone(MVCFunctions.Concat(this.dataGroupFunction, "(", this.connection.addFieldWrappers((XVar)(this.dataField)), "), "));
			}
			if((XVar)(this.dataGroupFunction == "avg")  && (XVar)(!(XVar)(CommonFunctions.IsDateFieldType((XVar)(ftype)))))
			{
				dynamic sum_for_avg = null;
				sum_for_avg = XVar.Clone((XVar.Pack(!(XVar)(isTime)) ? XVar.Pack(MVCFunctions.Concat("sum(", this.connection.addFieldWrappers((XVar)(this.dataField)), ")")) : XVar.Pack(MVCFunctions.Concat("sum(", this.connection.timeToSecWrapper((XVar)(this.dataField)), ")"))));
				avg_func = XVar.Clone(MVCFunctions.Concat(", ", sum_for_avg, " as ", this.connection.addFieldWrappers(new XVar("avg_sum")), ", count(", this.connection.addFieldWrappers((XVar)(this.dataField)), ") as ", this.connection.addFieldWrappers(new XVar("avg_count"))));
			}
			else
			{
				avg_func = XVar.Clone(MVCFunctions.Concat(", 1 as ", this.connection.addFieldWrappers(new XVar("avg_sum")), ", 1 as ", this.connection.addFieldWrappers(new XVar("avg_count"))));
			}
			whereClause = new XVar("");
			if(this.pageType == Constants.PAGE_REPORT)
			{
				if(XVar.Pack(CommonFunctions.tableEventExists(new XVar("BeforeQueryReport"), (XVar)(this.tableName))))
				{
					GlobalVars.eventObj = XVar.Clone(CommonFunctions.getEventObject((XVar)(this.tableName)));
					GlobalVars.eventObj.BeforeQueryReport((XVar)(whereClause));
					if(XVar.Pack(whereClause))
					{
						whereClause = XVar.Clone(MVCFunctions.Concat(" where ", whereClause));
					}
				}
			}
			else
			{
				if(XVar.Pack(CommonFunctions.tableEventExists(new XVar("BeforeQueryReportPrint"), (XVar)(this.tableName))))
				{
					GlobalVars.eventObj = XVar.Clone(CommonFunctions.getEventObject((XVar)(this.tableName)));
					GlobalVars.eventObj.BeforeQueryReportPrint((XVar)(whereClause));
					if(XVar.Pack(whereClause))
					{
						whereClause = XVar.Clone(MVCFunctions.Concat(" where ", whereClause));
					}
				}
			}
			gx0 = XVar.Clone(group_x[0]);
			gx1 = XVar.Clone(group_x[1]);
			gy0 = XVar.Clone(group_y[0]);
			gy1 = XVar.Clone(group_y[1]);
			selectClause = XVar.Clone(MVCFunctions.Concat("select ", select_field, gy0, ", ", gx0, avg_func));
			groupByClause = XVar.Clone(MVCFunctions.Concat("group by ", gx1, ", ", gy1));
			orderByClause = XVar.Clone(MVCFunctions.Concat("order by ", gx1, ",", gy1));
			if(this.connection.dbType == Constants.nDATABASE_Oracle)
			{
				return MVCFunctions.Concat(selectClause, " from (", tableSQL, ")", whereClause, " ", groupByClause, " ", orderByClause);
			}
			if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				dynamic pos = null;
				pos = XVar.Clone(MVCFunctions.strrpos((XVar)(MVCFunctions.strtoupper((XVar)(tableSQL))), new XVar("ORDER BY")));
				if(XVar.Pack(pos))
				{
					tableSQL = XVar.Clone(MVCFunctions.substr((XVar)(tableSQL), new XVar(0), (XVar)(pos)));
				}
			}
			return MVCFunctions.Concat(selectClause, " from (", tableSQL, ") as cross_table", whereClause, " ", groupByClause, " ", orderByClause);
		}
		protected virtual XVar _getIntervalTypeData(dynamic _param_fName, dynamic _param_int_type)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic int_type = XVar.Clone(_param_int_type);
			#endregion

			dynamic ftype = null;
			if(int_type == XVar.Pack(0))
			{
				dynamic wrappedGoodFieldName = null;
				wrappedGoodFieldName = XVar.Clone(this.connection.addFieldWrappers((XVar)(fName)));
				return new XVar(0, wrappedGoodFieldName, 1, wrappedGoodFieldName);
			}
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(fName)));
			if(XVar.Pack(CommonFunctions.IsNumberType((XVar)(ftype))))
			{
				return getNumberTypeInterval((XVar)(fName), (XVar)(int_type));
			}
			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(ftype))))
			{
				return getCharTypeInterval((XVar)(fName), (XVar)(int_type));
			}
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(ftype))))
			{
				return getDateTypeInterval((XVar)(fName), (XVar)(int_type));
			}
			return null;
		}
		protected virtual XVar getDateTypeInterval(dynamic _param_fName, dynamic _param_int_type)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic int_type = XVar.Clone(_param_int_type);
			#endregion

			dynamic field = null;
			field = XVar.Clone(this.connection.addFieldWrappers((XVar)(fName)));
			switch(((XVar)this.connection.dbType).ToInt())
			{
				case Constants.nDATABASE_MySQL:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+0101"), 1, MVCFunctions.Concat("YEAR(", field, ")"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+QUARTER(", field, ")*100+1"), 1, MVCFunctions.Concat("year(", field, "),QUARTER(", field, ")"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+month(", field, ")*100+1"), 1, MVCFunctions.Concat("year(", field, "),month(", field, ")"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+week(", field, ")*100+01"), 1, MVCFunctions.Concat("year(", field, "),WEEK(", field, ")"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+month(", field, ")*100+day(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, ")"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*1000000+month(", field, ")*10000+day(", field, ")*100+HOUR(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, "),hour(", field, ")"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*1000000+month(", field, ")*1000000+day(", field, ")*10000+HOUR(", field, ")*100+minute(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, "),hour(", field, "),minute(", field, ")"));
					}
					break;
				case Constants.nDATABASE_Oracle:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*10000+0101"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*10000+TO_CHAR(", field, ",'Q')*100+1"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ",'Q')"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*10000+TO_CHAR(", field, ".'MM')*100+1"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ".'MM')"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*10000+TO_CHAR(", field, ",'W')*100+01"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ",'W')"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*10000+TO_CHAR(", field, ",'MM')*100+TO_CHAR(", field, ",'DD')"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ",'MM'),TO_CHAR(", field, ",'DD')"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*1000000+TO_CHAR(", field, ",'MM')*10000+TO_CHAR(", field, ",'DD')*100+TO_CHAR(", field, ",'HH')"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ",'MM'),TO_CHAR(", field, ",'DD'),TO_CHAR(", field, ",'HH')"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY')*1000000+TO_CHAR(", field, ",'MM')*1000000+TO_CHAR(", field, ",'DD')*10000+TO_CHAR(", field, ",'HH')*100+TO_CHAR(", field, ",'MI')"), 1, MVCFunctions.Concat("TO_CHAR(", field, ", 'YYYY'),TO_CHAR(", field, ",'MM'),TO_CHAR(", field, ",'DD'),TO_CHAR(", field, ",'HH'),TO_CHAR(", field, ",'MI')"));
					}
					break;
				case Constants.nDATABASE_MSSQLServer:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*10000+0101"), 1, MVCFunctions.Concat("datepart(yyyy,", field, ")"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*10000+datepart(qq,", field, ")*100+1"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(qq,", field, ")"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*10000+datepart(mm,", field, ")*100+1"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(mm,", field, ")"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*10000+(datepart(ww,", field, ")-1)*100+01"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(ww,", field, ")"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*10000+datepart(mm,", field, ")*100+datepart(dd,", field, ")"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(mm,", field, "),datepart(dd,", field, ")"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*1000000+datepart(mm,", field, ")*10000+datepart(dd,", field, ")*100+datepart(hh,", field, ")"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(mm,", field, "),datepart(dd,", field, "),datepart(hh,", field, ")"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("datepart(yyyy,", field, ")*1000000+datepart(mm,", field, ")*1000000+datepart(dd,", field, ")*10000+datepart(hh,", field, ")*100+datepart(mi,", field, ")"), 1, MVCFunctions.Concat("datepart(yyyy,", field, "),datepart(mm,", field, "),datepart(dd,", field, "),datepart(hh,", field, "),datepart(mi,", field, ")"));
					}
					break;
				case Constants.nDATABASE_Access:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*10000+0101"), 1, MVCFunctions.Concat("datepart('yyyy',", field, ")"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*10000+datepart('q',", field, ")*100+1"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('q',", field, ")"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*10000+datepart('m',", field, ")*100+1"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('m',", field, ")"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*10000+(datepart('ww',", field, ")-1)*100+01"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('ww',", field, ")"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*10000+datepart('m',", field, ")*100+datepart('d',", field, ")"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('m',", field, "),datepart('d',", field, ")"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*1000000+datepart('m',", field, ")*10000+datepart('d',", field, ")*100+datepart('h',", field, ")"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('m',", field, "),datepart('d',", field, "),datepart('h',", field, ")"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("datepart('yyyy',", field, ")*1000000+datepart('m',", field, ")*1000000+datepart('d',", field, ")*10000+datepart('h',", field, ")*100+datepart('n',", field, ")"), 1, MVCFunctions.Concat("datepart('yyyy',", field, "),datepart('m',", field, "),datepart('d',", field, "),datepart('h',", field, "),datepart('n',", field, ")"));
					}
					break;
				case Constants.nDATABASE_PostgreSQL:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*10000+0101"), 1, MVCFunctions.Concat("date_part('year',", field, ")"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*10000+date_part('quarter',", field, ")*100+1"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('quarter',", field, ")"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*10000+date_part('month',", field, ")*100+1"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('month',", field, ")"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*10000+(date_part('week',", field, ")-1)*100+01"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('week',", field, ")"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*10000+date_part('month',", field, ")*100+date_part('days',", field, ")"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('month',", field, "),date_part('days',", field, ")"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*1000000+date_part('month',", field, ")*10000+date_part('days',", field, ")*100+date_part('hour',", field, ")"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('month',", field, "),date_part('days',", field, "),date_part('hour',", field, ")"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("date_part('year',", field, ")*1000000+date_part('month',", field, ")*1000000+date_part('days',", field, ")*10000+date_part('hour',", field, ")*100+date_part('minute',", field, ")"), 1, MVCFunctions.Concat("date_part('year',", field, "),date_part('month',", field, "),date_part('days',", field, "),date_part('hour',", field, "),date_part('minute',", field, ")"));
					}
					break;
				case Constants.nDATABASE_Informix:
					return MVCFunctions.Concat("substring(", field, " from 1 for ", int_type, ")");
				case Constants.nDATABASE_SQLite3:
					return new XVar(0, field, 1, field);
				case Constants.nDATABASE_DB2:
					if(int_type == 1)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+0101"), 1, MVCFunctions.Concat("YEAR(", field, ")"));
					}
					if(int_type == 2)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+QUARTER(", field, ")*100+1"), 1, MVCFunctions.Concat("year(", field, "),QUARTER(", field, ")"));
					}
					if(int_type == 3)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+month(", field, ")*100+1"), 1, MVCFunctions.Concat("year(", field, "),month(", field, ")"));
					}
					if(int_type == 4)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+week(", field, ")*100+01"), 1, MVCFunctions.Concat("year(", field, "),WEEK(", field, ")"));
					}
					if(int_type == 5)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*10000+month(", field, ")*100+day(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, ")"));
					}
					if(int_type == 6)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*1000000+month(", field, ")*10000+day(", field, ")*100+HOUR(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, "),hour(", field, ")"));
					}
					if(int_type == 7)
					{
						return new XVar(0, MVCFunctions.Concat("year(", field, ")*1000000+month(", field, ")*1000000+day(", field, ")*10000+HOUR(", field, ")*100+minute(", field, ")"), 1, MVCFunctions.Concat("year(", field, "),month(", field, "),day(", field, "),hour(", field, "),minute(", field, ")"));
					}
					break;
			}
			return new XVar(0, "", 1, "");
		}
		protected virtual XVar getNumberTypeInterval(dynamic _param_fName, dynamic _param_int_type)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic int_type = XVar.Clone(_param_int_type);
			#endregion

			dynamic field = null;
			field = XVar.Clone(this.connection.addFieldWrappers((XVar)(fName)));
			return new XVar(0, MVCFunctions.Concat("floor(", field, "/", int_type, ")*", int_type), 1, MVCFunctions.Concat("floor(", field, "/", int_type, ")*", int_type));
		}
		protected virtual XVar getCharTypeInterval(dynamic _param_field, dynamic _param_int_type)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic int_type = XVar.Clone(_param_int_type);
			#endregion

			field = XVar.Clone(this.connection.addFieldWrappers((XVar)(field)));
			switch(((XVar)this.connection.dbType).ToInt())
			{
				case Constants.nDATABASE_MySQL:
				case Constants.nDATABASE_MSSQLServer:
				case Constants.nDATABASE_Access:
					return new XVar(0, MVCFunctions.Concat("left(", field, ",", int_type, ")"), 1, MVCFunctions.Concat("left(", field, ",", int_type, ")"));
				case Constants.nDATABASE_PostgreSQL:
				case Constants.nDATABASE_Informix:
					return new XVar(0, MVCFunctions.Concat("substring(", field, " from 1 for ", int_type, ")"), 1, MVCFunctions.Concat("substring(", field, " from 1 for ", int_type, ")"));
				case Constants.nDATABASE_Oracle:
				case Constants.nDATABASE_SQLite3:
				case Constants.nDATABASE_DB2:
					return new XVar(0, MVCFunctions.Concat("substr(", field, ",1,", int_type, ")"), 1, MVCFunctions.Concat("substr(", field, ",1,", int_type, ")"));
			}
			return null;
		}
		public virtual XVar getSelectedValue()
		{
			dynamic arr = XVar.Array(), firstarr = XVar.Array();
			arr = XVar.Clone(XVar.Array());
			firstarr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in this.fieldsTotalsData.GetEnumerator())
			{
				if(MVCFunctions.count(firstarr) == 0)
				{
					firstarr.InitAndSetArrayItem(value.Value["name"], null);
				}
				if((XVar)((XVar)((XVar)(value.Value["min"] == true)  || (XVar)(value.Value["max"] == true))  || (XVar)(value.Value["sum"] == true))  || (XVar)(value.Value["avg"] == true))
				{
					arr.InitAndSetArrayItem(value.Value["name"], null);
				}
			}
			if(MVCFunctions.count(arr) == 0)
			{
				arr = XVar.Clone(firstarr);
			}
			return arr;
		}
		public virtual XVar getCurrentOperationList()
		{
			dynamic names = XVar.Array(), opData = XVar.Array();
			names = XVar.Clone(XVar.Array());
			names.InitAndSetArrayItem("Sum", "sum");
			names.InitAndSetArrayItem("Min", "min");
			names.InitAndSetArrayItem("Max", "max");
			names.InitAndSetArrayItem("Avg", "avg");
			opData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> n in names.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.dataFieldSettings[n.Key])))
				{
					continue;
				}
				opData.InitAndSetArrayItem(new XVar("value", n.Key, "selected", (XVar.Pack(this.dataGroupFunction == n.Key) ? XVar.Pack("selected") : XVar.Pack("")), "label", n.Value), null);
			}
			return opData;
		}
		public virtual XVar getCrossFieldsData(dynamic _param_axis)
		{
			#region pass-by-value parameters
			dynamic axis = XVar.Clone(_param_axis);
			#endregion

			dynamic dataList = XVar.Array();
			dataList = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> data in this.groupFieldsData.GetEnumerator())
			{
				dynamic intervalType = null, selected = null;
				if((XVar)((XVar)((XVar)(axis != "x")  || (XVar)(data.Value["group_type"] != "x"))  && (XVar)((XVar)(axis != "y")  || (XVar)(data.Value["group_type"] != "y")))  && (XVar)(data.Value["group_type"] != "all"))
				{
					continue;
				}
				selected = new XVar("");
				if((XVar)((XVar)((XVar)(axis == "x")  && (XVar)(data.Value["name"] == this.xFName))  && (XVar)(data.Value["int_type"] == this.xIntervalType))  || (XVar)((XVar)((XVar)(axis == "y")  && (XVar)(data.Value["name"] == this.yFName))  && (XVar)(data.Value["int_type"] == this.yIntervalType)))
				{
					selected = new XVar("selected");
				}
				intervalType = XVar.Clone((XVar.Pack(data.Value["uniqueName"]) ? XVar.Pack("") : XVar.Pack(getIntervalParam((XVar)(data.Value["int_type"]), (XVar)(data.Value["name"])))));
				dataList.InitAndSetArrayItem(new XVar("value", data.Value["name"], "selected", selected, "label", data.Value["label"], "intervalType", intervalType), null);
			}
			return dataList;
		}
		protected virtual XVar getRefineIntervalType(dynamic _param_intType, dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic intType = XVar.Clone(_param_intType);
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic ftype = null;
			if(XVar.Equals(XVar.Pack(intType), XVar.Pack(0)))
			{
				return "normal";
			}
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(fName)));
			if(XVar.Pack(CommonFunctions.IsNumberType((XVar)(ftype))))
			{
				return MVCFunctions.substr((XVar)(intType), new XVar(1));
			}
			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(ftype))))
			{
				return MVCFunctions.substr((XVar)(intType), (XVar)(MVCFunctions.strlen(new XVar("first"))));
			}
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(ftype))))
			{
				switch(((XVar)intType).ToString())
				{
					case "year":
						return 1;
					case "quarter":
						return 2;
					case "month":
						return 3;
					case "week":
						return 4;
					case "day":
						return 5;
					case "hour":
						return 6;
					case "minute":
						return 7;
				}
			}
			return -1;
		}
		protected virtual XVar getIntervalParam(dynamic _param_intType, dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic intType = XVar.Clone(_param_intType);
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic ftype = null;
			if(intType == XVar.Pack(0))
			{
				return "normal";
			}
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(fName)));
			if(XVar.Pack(CommonFunctions.IsNumberType((XVar)(ftype))))
			{
				return MVCFunctions.Concat("n", intType);
			}
			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(ftype))))
			{
				return MVCFunctions.Concat("first", intType);
			}
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(ftype))))
			{
				switch(((XVar)intType).ToInt())
				{
					case 1:
						return "year";
					case 2:
						return "quarter";
					case 3:
						return "month";
					case 4:
						return "week";
					case 5:
						return "day";
					case 6:
						return "hour";
					case 7:
						return "minute";
					default:
						return "";
				}
			}
			return "";
		}
		protected virtual XVar getAxisDisplayValue(dynamic _param_fName, dynamic _param_intervalType, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic intervalType = XVar.Clone(_param_intervalType);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic control = null, ftype = null;
			if((XVar)(value == XVar.Pack(""))  || (XVar)(value == null))
			{
				return "";
			}
			control = XVar.Clone(this.viewControls.getControl((XVar)(fName)));
			if(intervalType == XVar.Pack(0))
			{
				dynamic data = null;
				data = XVar.Clone(new XVar(fName, value));
				return control.showDBValue((XVar)(data), new XVar(""));
			}
			ftype = XVar.Clone(this.pSet.getFieldType((XVar)(fName)));
			if(XVar.Pack(CommonFunctions.IsNumberType((XVar)(ftype))))
			{
				dynamic dataEnd = null, dataStart = null, start = null, var_end = null;
				start = XVar.Clone(value - value  %  intervalType);
				var_end = XVar.Clone(start + intervalType);
				dataStart = XVar.Clone(new XVar(fName, start));
				dataEnd = XVar.Clone(new XVar(fName, var_end));
				return MVCFunctions.Concat(control.showDBValue((XVar)(dataStart), new XVar("")), " - ", control.showDBValue((XVar)(dataEnd), new XVar("")));
			}
			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(ftype))))
			{
				return CommonFunctions.xmlencode((XVar)(MVCFunctions.substr((XVar)(value), new XVar(0), (XVar)(intervalType))));
			}
			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(ftype))))
			{
				dynamic dates = XVar.Array(), dvalue = null, tm = XVar.Array();
				dvalue = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(value), new XVar(0), new XVar(4)), "-", MVCFunctions.substr((XVar)(value), new XVar(4), new XVar(2)), "-", MVCFunctions.substr((XVar)(value), new XVar(6), new XVar(2))));
				if(MVCFunctions.strlen((XVar)(value)) == 10)
				{
					dvalue = MVCFunctions.Concat(dvalue, " ", MVCFunctions.substr((XVar)(value), new XVar(8), new XVar(2)), "00:00");
				}
				else
				{
					if(MVCFunctions.strlen((XVar)(value)) == 12)
					{
						dvalue = MVCFunctions.Concat(dvalue, " ", MVCFunctions.substr((XVar)(value), new XVar(8), new XVar(2)), ":", MVCFunctions.substr((XVar)(value), new XVar(10), new XVar(2)), ":00");
					}
				}
				tm = XVar.Clone(CommonFunctions.db2time((XVar)(dvalue)));
				if(XVar.Pack(!(XVar)(MVCFunctions.count(tm))))
				{
					return "";
				}
				switch(((XVar)intervalType).ToInt())
				{
					case 1:
						return tm[0];
					case 2:
						return MVCFunctions.Concat(tm[0], "/Q", tm[1]);
					case 3:
						return MVCFunctions.Concat(GlobalVars.locale_info[MVCFunctions.Concat("LOCALE_SABBREVMONTHNAME", tm[1])], " ", tm[0]);
					case 4:
						dates = XVar.Clone(getDatesByWeek((XVar)(tm[1] + 1), (XVar)(tm[0])));
						return MVCFunctions.Concat(CommonFunctions.format_shortdate((XVar)(CommonFunctions.db2time((XVar)(dates[0])))), " - ", CommonFunctions.format_shortdate((XVar)(CommonFunctions.db2time((XVar)(dates[1])))));
					case 5:
						return CommonFunctions.format_shortdate((XVar)(tm));
					case 6:
						tm.InitAndSetArrayItem(0, 4);
						tm.InitAndSetArrayItem(0, 5);
						return CommonFunctions.str_format_datetime((XVar)(tm));
					case 7:
						tm.InitAndSetArrayItem(0, 5);
						return CommonFunctions.str_format_datetime((XVar)(tm));
					default:
						return CommonFunctions.str_format_datetime((XVar)(tm));
				}
			}
			return "";
		}
		protected virtual XVar getDatesByWeek(dynamic _param_week, dynamic _param_year)
		{
			#region pass-by-value parameters
			dynamic week = XVar.Clone(_param_week);
			dynamic year = XVar.Clone(_param_year);
			#endregion

			dynamic L = null, dates = XVar.Array(), day = null, day_of_week = null, i = null, month = null, months = XVar.Array(), startweekday = null, sum = null, total_days = null;
			startweekday = new XVar(0);
			if(0 < GlobalVars.locale_info["LOCALE_IFIRSTDAYOFWEEK"])
			{
				startweekday = XVar.Clone(7 - GlobalVars.locale_info["LOCALE_IFIRSTDAYOFWEEK"]);
			}
			L = XVar.Clone((XVar.Pack(CommonFunctions.isleapyear((XVar)(year))) ? XVar.Pack(1) : XVar.Pack(0)));
			months = XVar.Clone(new XVar(0, 31, 1, 28 + L, 2, 31, 3, 30, 4, 31, 5, 30, 6, 31, 7, 31, 8, 30, 9, 31, 10, 30, 11, 31));
			total_days = XVar.Clone((week - 1) * 7);
			i = new XVar(0);
			sum = new XVar(0);
			while(sum <= total_days)
			{
				sum += months[i++];
			}
			sum -= months[i - 1];
			month = XVar.Clone(i);
			day = XVar.Clone(total_days - sum);
			day_of_week = XVar.Clone(CommonFunctions.getdayofweek((XVar)(new XVar(0, year, 1, month, 2, day))));
			if(day_of_week == XVar.Pack(0))
			{
				day_of_week = new XVar(7);
			}
			day = XVar.Clone((day - (day_of_week - 1)) - startweekday);
			dates = XVar.Clone(XVar.Array());
			dates.InitAndSetArrayItem(MVCFunctions.getYMDdate((XVar)(MVCFunctions.mktime(new XVar(0), new XVar(0), new XVar(0), (XVar)(month), (XVar)(day), (XVar)(year)))), 0);
			dates.InitAndSetArrayItem(MVCFunctions.getYMDdate((XVar)(MVCFunctions.mktime(new XVar(1), new XVar(1), new XVar(1), (XVar)(month), (XVar)(day + 6), (XVar)(year)))), 1);
			return dates;
		}
		public virtual XVar getDataFieldsList()
		{
			dynamic listData = XVar.Array();
			listData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in this.fieldsTotalsData.GetEnumerator())
			{
				if((XVar)((XVar)((XVar)(value.Value["min"] == true)  || (XVar)(value.Value["max"] == true))  || (XVar)(value.Value["sum"] == true))  || (XVar)(value.Value["avg"] == true))
				{
					dynamic selected = null;
					selected = XVar.Clone((XVar.Pack(value.Value["name"] == this.dataField) ? XVar.Pack("selected") : XVar.Pack("")));
					listData.InitAndSetArrayItem(new XVar("value", value.Value["name"], "selected", selected, "label", value.Value["label"]), null);
				}
			}
			return listData;
		}
		public virtual XVar getPrintCrossHeader()
		{
			return MVCFunctions.Concat("Group X", ":<b>", this.fieldsTotalsData[this.xFName]["label"], "</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "Group Y", ":<b>", this.fieldsTotalsData[this.yFName]["label"], "</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "Field", ":<b>", this.fieldsTotalsData[this.dataField]["label"], "</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "Group function", ":<b>", this.dataGroupFunction, "</b>");
		}
		public virtual XVar getTotalsName()
		{
			switch(((XVar)this.dataGroupFunction).ToString())
			{
				case "sum":
					return "Sum";
					break;
				case "min":
					return "Min";
					break;
				case "max":
					return "Max";
					break;
				case "avg":
					return "Average";
					break;
				default:
					return "";
			}
			return null;
		}
		protected virtual XVar getDataGroupFunction(dynamic _param_operation)
		{
			#region pass-by-value parameters
			dynamic operation = XVar.Clone(_param_operation);
			#endregion

			dynamic gfuncs = XVar.Array();
			if(this.dataFieldSettings[operation] == true)
			{
				return operation;
			}
			gfuncs = XVar.Clone(new XVar(0, "sum", 1, "max", 2, "min", 3, "avg"));
			foreach (KeyValuePair<XVar, dynamic> gf in gfuncs.GetEnumerator())
			{
				if(this.dataFieldSettings[gf.Value] == true)
				{
					return gf.Value;
				}
			}
			return "sum";
		}
		public virtual XVar getCurrentGroupFunction()
		{
			return this.dataGroupFunction;
		}
		public static XVar getCrossIntervalName(dynamic _param_ftype, dynamic _param_int_type)
		{
			#region pass-by-value parameters
			dynamic ftype = XVar.Clone(_param_ftype);
			dynamic int_type = XVar.Clone(_param_int_type);
			#endregion

			if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(ftype))))
			{
				if(int_type == 1)
				{
					return "year";
				}
				if(int_type == 2)
				{
					return "quarter";
				}
				if(int_type == 3)
				{
					return "month";
				}
				if(int_type == 4)
				{
					return "week";
				}
				if(int_type == 5)
				{
					return "day";
				}
				if(int_type == 6)
				{
					return "hour";
				}
				if(int_type == 7)
				{
					return "minute";
				}
			}
			return int_type;
		}
	}
}
