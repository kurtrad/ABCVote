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
	public partial class ReportField : XClass
	{
		public dynamic _interval = XVar.Pack(0);
		public dynamic _tName = XVar.Pack("");
		public dynamic _name = XVar.Pack("");
		public dynamic _alias = XVar.Pack("");
		public dynamic _sqlname = XVar.Pack("");
		public dynamic _start = XVar.Pack(0);
		public dynamic _caseSensitive = XVar.Pack(false);
		public dynamic _recordBasedRequest = XVar.Pack(false);
		public dynamic _rowsInSummary = XVar.Pack(0);
		public dynamic _rowsInHeader = XVar.Pack(0);
		public dynamic _viewFormat = XVar.Pack("");
		public dynamic _orderBy = XVar.Pack("ASC");
		public dynamic _oldAlgorithm = XVar.Pack(false);
		public ProjectSettings pSet = null;
		public dynamic cipherer = XVar.Pack(null);
		public dynamic _connection;
		public ReportField(dynamic _param_name, dynamic _param_interval, dynamic _param_alias, dynamic _param_table, dynamic _param_connection, dynamic _param_cipherer)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic interval = XVar.Clone(_param_interval);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic table = XVar.Clone(_param_table);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			this._name = XVar.Clone(name);
			this._interval = XVar.Clone(interval);
			this._alias = XVar.Clone(alias);
			this._sqlname = XVar.Clone(alias);
			this._tName = XVar.Clone(table);
			this._connection = XVar.Clone(connection);
			this.cipherer = XVar.Clone(cipherer);
			if(table != XVar.Pack(""))
			{
				this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			}
		}

		public virtual XVar getStringSql(dynamic _param_forGroupedField = null)
		{
			#region default values
			if(_param_forGroupedField as Object == null) _param_forGroupedField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic forGroupedField = XVar.Clone(_param_forGroupedField);
			#endregion

			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}

		public virtual XVar getFieldName(dynamic _param_fieldValue, dynamic _param_data = null, dynamic _param_pageObject = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldValue = XVar.Clone(_param_fieldValue);
			dynamic data = XVar.Clone(_param_data);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}

		public virtual XVar getSelectSql(dynamic _param_hasGrouping = null)
		{
			#region default values
			if(_param_hasGrouping as Object == null) _param_hasGrouping = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic hasGrouping = XVar.Clone(_param_hasGrouping);
			#endregion

			return MVCFunctions.Concat(getStringSql(new XVar(true)), (XVar.Pack(alias()) ? XVar.Pack(MVCFunctions.Concat(" as ", CommonFunctions.cached_ffn((XVar)(alias())))) : XVar.Pack("")));
		}

		public virtual XVar getGroupSql()
		{
			return getStringSql();
		}

		public virtual XVar getOrderSql()
		{
			return MVCFunctions.Concat(getStringSql(), " ", this._orderBy, " ");
		}

		public virtual XVar getWhereSql(dynamic _param_groups)
		{
			#region pass-by-value parameters
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}

		public virtual XVar getGroup(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			return data[alias()];
		}

		public virtual XVar getKey(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			return data[alias()];
		}

		public virtual XVar setStart(dynamic _param_start)
		{
			#region pass-by-value parameters
			dynamic start = XVar.Clone(_param_start);
			#endregion

			this._start = XVar.Clone(start);
			this._sqlname = XVar.Clone(alias());
			return start + 1;
		}

		public virtual XVar name()
		{
			return this._name;
		}

		public virtual XVar alias()
		{
			return MVCFunctions.Concat(this._alias, this._start);
		}

		public virtual XVar overrideFormat()
		{
			return false;
		}
		public virtual XVar setCaseSensitive(dynamic _param_cs)
		{
			#region pass-by-value parameters
			dynamic cs = XVar.Clone(_param_cs);
			#endregion

			this._caseSensitive = XVar.Clone(cs);
			return null;
		}
		public virtual XVar cutNull(ref dynamic range, dynamic _param_checkEmty = null)
		{
			#region default values
			if(_param_checkEmty as Object == null) _param_checkEmty = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic checkEmty = XVar.Clone(_param_checkEmty);
			#endregion

			dynamic b = null, nCnt = null, ret = null, var_out = XVar.Array();
			ret = new XVar(false);
			var_out = XVar.Clone(XVar.Array());
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(range); nCnt++)
			{
				b = new XVar(false);
				if(XVar.Equals(XVar.Pack(range[nCnt]), XVar.Pack(null)))
				{
					b = new XVar(true);
					ret = new XVar(true);
				}
				else
				{
					if((XVar)(checkEmty)  && (XVar)((XVar)(!(XVar)(range[nCnt]))  || (XVar)(MVCFunctions.strcasecmp((XVar)(range[nCnt]), new XVar("null")) == 0)))
					{
						b = new XVar(true);
						ret = new XVar(true);
					}
				}
				if(XVar.Pack(!(XVar)(b)))
				{
					var_out.InitAndSetArrayItem(range[nCnt], null);
				}
			}
			range = XVar.Clone(var_out);
			return ret;
		}
		public virtual XVar getLtGt(ref dynamic lt, ref dynamic gt)
		{
			if(this._orderBy != "ASC")
			{
				lt = new XVar(" >= ");
				gt = new XVar(" <= ");
			}
			else
			{
				lt = new XVar(" <= ");
				gt = new XVar(" >= ");
			}
			return null;
		}
	}
	public partial class ReportNumericField : ReportField
	{
		protected static bool skipReportNumericFieldCtor = false;
		public ReportNumericField(dynamic _param_name, dynamic _param_interval, dynamic _param_alias, dynamic _param_table, dynamic _param_connection, dynamic _param_cipherer)
			:base((XVar)_param_name, (XVar)_param_interval, (XVar)_param_alias, (XVar)_param_table, (XVar)_param_connection, (XVar)_param_cipherer)
		{
			if(skipReportNumericFieldCtor)
			{
				skipReportNumericFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic interval = XVar.Clone(_param_interval);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic table = XVar.Clone(_param_table);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

		}
		public override XVar getStringSql(dynamic _param_forGroupedField = null)
		{
			#region default values
			if(_param_forGroupedField as Object == null) _param_forGroupedField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic forGroupedField = XVar.Clone(_param_forGroupedField);
			#endregion

			dynamic fname = null;
			fname = XVar.Clone((XVar.Pack(this._oldAlgorithm) ? XVar.Pack(RunnerPage._getFieldSQL((XVar)(this._name), (XVar)(this._connection), (XVar)(this.pSet))) : XVar.Pack(CommonFunctions.cached_ffn((XVar)(this._name), new XVar(true)))));
			if(0 < this._interval)
			{
				if((XVar)((XVar)(this._connection.dbType == Constants.nDATABASE_MySQL)  || (XVar)(this._connection.dbType == Constants.nDATABASE_MSSQLServer))  || (XVar)(this._connection.dbType == Constants.nDATABASE_PostgreSQL))
				{
					return MVCFunctions.Concat("floor(", fname, "/", this._interval, ")*", this._interval);
				}
				if(this._connection.dbType == Constants.nDATABASE_Access)
				{
					return MVCFunctions.Concat("Int(", fname, "/", this._interval, ")*", this._interval);
				}
				if(this._connection.dbType == Constants.nDATABASE_Oracle)
				{
					return MVCFunctions.Concat("(floor(", fname, "/", this._interval, ")*", this._interval, ")");
				}
			}
			else
			{
				return fname;
			}
			return null;
		}
		public override XVar getFieldName(dynamic _param_fieldValue, dynamic _param_data = null, dynamic _param_pageObject = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldValue = XVar.Clone(_param_fieldValue);
			dynamic data = XVar.Clone(_param_data);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			dynamic value = null;
			value = XVar.Clone(data[(XVar.Pack(this._recordBasedRequest) ? XVar.Pack(this._name) : XVar.Pack(this._sqlname))]);
			if(value == null)
			{
				return "NULL";
			}
			if(0 < this._interval)
			{
				return MVCFunctions.Concat(MVCFunctions.intval((XVar)(value)), " - ", MVCFunctions.intval((XVar)(value)) + this._interval);
			}
			else
			{
				return value;
			}
			return null;
		}
		public override XVar getKey(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(XVar.Pack(this._recordBasedRequest))
			{
				if(0 < this._interval)
				{
					return MVCFunctions.intval((XVar)(data[this._name] / this._interval)) * this._interval;
				}
				else
				{
					return data[this._name];
				}
			}
			else
			{
				return base.getKey((XVar)(data));
			}
			return null;
		}
		public override XVar getWhereSql(dynamic _param_groups)
		{
			#region pass-by-value parameters
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			dynamic hasNull = null, ret = null, ssql = null;
			ret = new XVar("");
			ssql = XVar.Clone(getStringSql());
			hasNull = XVar.Clone(cutNull(ref groups));
			if(0 < MVCFunctions.count(groups))
			{
				dynamic gt = null, lt = null;
				lt = new XVar("");
				gt = new XVar("");
				getLtGt(ref lt, ref gt);
				ret = XVar.Clone(MVCFunctions.Concat("(", ssql, gt, groups[0], " AND ", ssql, lt, groups[MVCFunctions.count(groups) - 1], ")"));
			}
			if(XVar.Pack(hasNull))
			{
				ret = MVCFunctions.Concat(ret, (XVar.Pack(ret) ? XVar.Pack(" OR ") : XVar.Pack("")), ssql, " IS NULL");
			}
			return (XVar.Pack(ret) ? XVar.Pack(MVCFunctions.Concat("(", ret, ")")) : XVar.Pack(""));
		}
	}
	public partial class ReportCharField : ReportField
	{
		protected static bool skipReportCharFieldCtor = false;
		public ReportCharField(dynamic _param_name, dynamic _param_interval, dynamic _param_alias, dynamic _param_table, dynamic _param_connection, dynamic _param_cipherer)
			:base((XVar)_param_name, (XVar)_param_interval, (XVar)_param_alias, (XVar)_param_table, (XVar)_param_connection, (XVar)_param_cipherer)
		{
			if(skipReportCharFieldCtor)
			{
				skipReportCharFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic interval = XVar.Clone(_param_interval);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic table = XVar.Clone(_param_table);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

		}
		public override XVar getStringSql(dynamic _param_forGroupedField = null)
		{
			#region default values
			if(_param_forGroupedField as Object == null) _param_forGroupedField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic forGroupedField = XVar.Clone(_param_forGroupedField);
			#endregion

			dynamic fname = null;
			fname = XVar.Clone((XVar.Pack((XVar)(this._oldAlgorithm)  && (XVar)(!(XVar)(forGroupedField))) ? XVar.Pack(RunnerPage._getFieldSQL((XVar)(this._name), (XVar)(this._connection), (XVar)(this.pSet))) : XVar.Pack(CommonFunctions.cached_ffn((XVar)(this._name), (XVar)(forGroupedField)))));
			if(0 < this._interval)
			{
				if((XVar)(this._connection.dbType == Constants.nDATABASE_MySQL)  || (XVar)(this._connection.dbType == Constants.nDATABASE_PostgreSQL))
				{
					return MVCFunctions.Concat("substr(", fname, ", 1, ", this._interval, ")");
				}
				if(this._connection.dbType == Constants.nDATABASE_MSSQLServer)
				{
					return MVCFunctions.Concat("substring(", fname, ", 1, ", this._interval, ")");
				}
				if(this._connection.dbType == Constants.nDATABASE_Access)
				{
					return MVCFunctions.Concat("Mid(", fname, ", 1, ", this._interval, ")");
				}
				if(this._connection.dbType == Constants.nDATABASE_Oracle)
				{
					return MVCFunctions.Concat("SUBSTR(", fname, ", 1, ", this._interval, ")");
				}
			}
			else
			{
				return fname;
			}
			return null;
		}
		public override XVar getFieldName(dynamic _param_fieldValue, dynamic _param_data = null, dynamic _param_pageObject = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldValue = XVar.Clone(_param_fieldValue);
			dynamic data = XVar.Clone(_param_data);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			dynamic value = null;
			value = XVar.Clone(data[(XVar.Pack(this._recordBasedRequest) ? XVar.Pack(this._name) : XVar.Pack(this._sqlname))]);
			if(value == null)
			{
				return "NULL";
			}
			if(0 < this._interval)
			{
				return MVCFunctions.substr((XVar)(value), new XVar(0), (XVar)(this._interval));
			}
			else
			{
				return value;
			}
			return null;
		}
		public override XVar getKey(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(XVar.Pack(this._recordBasedRequest))
			{
				if(0 < this._interval)
				{
					if(XVar.Pack(this._caseSensitive))
					{
						return MVCFunctions.substr((XVar)(data[this._name]), new XVar(0), (XVar)(this._interval));
					}
					else
					{
						return MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(data[this._name]), new XVar(0), (XVar)(this._interval))));
					}
				}
				else
				{
					if(XVar.Pack(this._caseSensitive))
					{
						return data[this._name];
					}
					else
					{
						return MVCFunctions.strtolower((XVar)(data[this._name]));
					}
				}
			}
			else
			{
				if(XVar.Pack(this._caseSensitive))
				{
					return data[alias()];
				}
				else
				{
					return MVCFunctions.strtolower((XVar)(data[alias()]));
				}
			}
			return null;
		}
		public override XVar getWhereSql(dynamic _param_groups)
		{
			#region pass-by-value parameters
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			dynamic hasNull = null, ret = null, ssql = null;
			ret = new XVar("");
			ssql = XVar.Clone(getStringSql());
			hasNull = XVar.Clone(cutNull(ref groups));
			if(0 < MVCFunctions.count(groups))
			{
				dynamic gr = XVar.Array(), gt = null, lt = null;
				gr = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> g in groups.GetEnumerator())
				{
					gr.InitAndSetArrayItem(MVCFunctions.Concat("'", g.Value, "'"), null);
				}
				lt = new XVar("");
				gt = new XVar("");
				getLtGt(ref lt, ref gt);
				ret = XVar.Clone(MVCFunctions.Concat("(", ssql, gt, this._connection.prepareString((XVar)(groups[0])), " AND ", ssql, lt, this._connection.prepareString((XVar)(groups[MVCFunctions.count(groups) - 1])), ")"));
			}
			if(XVar.Pack(hasNull))
			{
				ret = MVCFunctions.Concat(ret, (XVar.Pack(ret) ? XVar.Pack(" OR ") : XVar.Pack("")), ssql, " IS NULL");
				ret = MVCFunctions.Concat(ret, " OR ", ssql, "=''");
			}
			return (XVar.Pack(ret) ? XVar.Pack(MVCFunctions.Concat("(", ret, ")")) : XVar.Pack(""));
		}
	}
	public partial class ReportDateField : ReportField
	{
		protected static bool skipReportDateFieldCtor = false;
		public ReportDateField(dynamic _param_name, dynamic _param_interval, dynamic _param_alias, dynamic _param_table, dynamic _param_connection, dynamic _param_cipherer)
			:base((XVar)_param_name, (XVar)_param_interval, (XVar)_param_alias, (XVar)_param_table, (XVar)_param_connection, (XVar)_param_cipherer)
		{
			if(skipReportDateFieldCtor)
			{
				skipReportDateFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic interval = XVar.Clone(_param_interval);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic table = XVar.Clone(_param_table);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

		}
		public override XVar setStart(dynamic _param_start)
		{
			#region pass-by-value parameters
			dynamic start = XVar.Clone(_param_start);
			#endregion

			this._start = XVar.Clone(start);
			if(this._interval == 0)
			{
				this._sqlname = XVar.Clone(alias());
			}
			else
			{
				this._sqlname = XVar.Clone(MVCFunctions.Concat(alias(), "MIN"));
			}
			return start + ((XVar.Pack(0 < this._interval) ? XVar.Pack(this._interval) : XVar.Pack(1)));
		}
		public virtual XVar getSqlList(dynamic _param_all = null)
		{
			#region default values
			if(_param_all as Object == null) _param_all = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic all = XVar.Clone(_param_all);
			#endregion

			dynamic fname = null, grp = null;
			grp = XVar.Clone(XVar.Array());
			fname = XVar.Clone((XVar.Pack(this._oldAlgorithm) ? XVar.Pack(RunnerPage._getFieldSQLDecrypt((XVar)(this._name), (XVar)(this._connection), (XVar)(this.pSet), (XVar)(this.cipherer))) : XVar.Pack(CommonFunctions.cached_ffn((XVar)(this._name)))));
			if(XVar.Pack(all))
			{
				dynamic idx = null, symbols = XVar.Array();
				if(this._connection.dbType == Constants.nDATABASE_MySQL)
				{
					symbols = XVar.Clone(new XVar(0, new XVar(0, "YEAR(", 1, ")", 2, -1), 1, new XVar(0, "QUARTER(", 1, ")", 2, 0), 2, new XVar(0, "MONTH(", 1, ")", 2, 0), 3, new XVar(0, "WEEK(", 1, ")", 2, 0), 4, new XVar(0, "DATE(", 1, ")", 2, -1), 5, new XVar(0, "HOUR(", 1, ")", 2, 4), 6, new XVar(0, "MINUTE(", 1, ")", 2, 5)));
				}
				else
				{
					if(this._connection.dbType == Constants.nDATABASE_PostgreSQL)
					{
						symbols = XVar.Clone(new XVar(0, new XVar(0, "date_part('year', ", 1, ")", 2, -1), 1, new XVar(0, "date_trunc('quarter', ", 1, ")", 2, -1), 2, new XVar(0, "date_trunc('month', ", 1, ")", 2, -1), 3, new XVar(0, "date_trunc('week', ", 1, ")", 2, -1), 4, new XVar(0, "date_trunc('day', ", 1, ")", 2, -1), 5, new XVar(0, "date_trunc('hour', ", 1, ")", 2, 1), 6, new XVar(0, "date_trunc('minute', ", 1, ")", 2, -1)));
					}
					else
					{
						if(this._connection.dbType == Constants.nDATABASE_MSSQLServer)
						{
							symbols = XVar.Clone(new XVar(0, new XVar(0, "DATEPART(year, ", 1, ")", 2, -1), 1, new XVar(0, "DATEPART(quarter, ", 1, ")", 2, 0), 2, new XVar(0, "DATEPART(month, ", 1, ")", 2, 0), 3, new XVar(0, "DATEPART(week, ", 1, ")", 2, 0), 4, new XVar(0, "DATEPART(day, ", 1, ")", 2, 2), 5, new XVar(0, "DATEPART(hour, ", 1, ")", 2, 4), 6, new XVar(0, "DATEPART(minute, ", 1, ")", 2, 5)));
						}
						else
						{
							if(this._connection.dbType == Constants.nDATABASE_Access)
							{
								dynamic first_day_of_week = null;
								first_day_of_week = new XVar(1);
								if(GlobalVars.locale_info["LOCALE_IFIRSTDAYOFWEEK"] == "0")
								{
									first_day_of_week = new XVar(2);
								}
								symbols = XVar.Clone(new XVar(0, new XVar(0, "DatePart('yyyy', ", 1, ")", 2, -1), 1, new XVar(0, "DatePart('q', ", 1, ")", 2, 0), 2, new XVar(0, "DatePart('m', ", 1, ")", 2, 0), 3, new XVar(0, "DatePart('ww', ", 1, MVCFunctions.Concat(",", first_day_of_week, ")"), 2, 0), 4, new XVar(0, "DatePart('d', ", 1, ")", 2, 2), 5, new XVar(0, "DatePart('h', ", 1, ")", 2, 4), 6, new XVar(0, "DatePart('n', ", 1, ")", 2, 5)));
							}
							else
							{
								if(this._connection.dbType == Constants.nDATABASE_Oracle)
								{
									symbols = XVar.Clone(new XVar(0, new XVar(0, "EXTRACT(year from ", 1, ")", 2, -1), 1, new XVar(0, "FLOOR(EXTRACT(month from ", 1, ")/3)", 2, 0), 2, new XVar(0, "EXTRACT(month from ", 1, ")", 2, 0), 3, new XVar(0, "TRUNC(", 1, ", 'D')", 2, -1), 4, new XVar(0, "TRUNC(", 1, ", 'J')", 2, -1), 5, new XVar(0, "TRUNC(hour from ", 1, ")", 2, -1), 6, new XVar(0, "TRUNC(minute from ", 1, ")", 2, -1)));
								}
							}
						}
					}
				}
				idx = XVar.Clone(this._interval - 1);
				do
				{
					MVCFunctions.array_unshift((XVar)(grp), (XVar)(MVCFunctions.Concat(symbols[idx][0], CommonFunctions.cached_ffn((XVar)(this._name)), symbols[idx][1])));
					idx = XVar.Clone(symbols[idx][2]);
				}
				while(XVar.Pack(0) <= idx);
			}
			return grp;
		}
		public override XVar getSelectSql(dynamic _param_hasGrouping = null)
		{
			#region default values
			if(_param_hasGrouping as Object == null) _param_hasGrouping = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic hasGrouping = XVar.Clone(_param_hasGrouping);
			#endregion

			dynamic fname = null;
			fname = XVar.Clone((XVar.Pack(this._oldAlgorithm) ? XVar.Pack(RunnerPage._getFieldSQLDecrypt((XVar)(this._name), (XVar)(this._connection), (XVar)(this.pSet), (XVar)(this.cipherer))) : XVar.Pack(CommonFunctions.cached_ffn((XVar)(this._name), new XVar(true)))));
			if(this._interval == 0)
			{
				return MVCFunctions.Concat(fname, " as ", CommonFunctions.cached_ffn((XVar)(alias())));
			}
			else
			{
				dynamic grp = XVar.Array(), nCnt = null;
				grp = XVar.Clone(getSqlList());
				nCnt = new XVar(0);
				for(;nCnt < MVCFunctions.count(grp); nCnt++)
				{
					grp[nCnt] = MVCFunctions.Concat(grp[nCnt], " as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this._alias, nCnt + this._start))));
				}
				if(XVar.Pack(hasGrouping))
				{
					grp.InitAndSetArrayItem(MVCFunctions.Concat("MIN(", fname, ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(alias(), "MIN")))), null);
					grp.InitAndSetArrayItem(MVCFunctions.Concat("MAX(", fname, ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(alias(), "MAX")))), null);
				}
				else
				{
					grp.InitAndSetArrayItem(MVCFunctions.Concat(fname, " as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(alias(), "MIN")))), null);
				}
				return MVCFunctions.join(new XVar(", "), (XVar)(grp));
			}
			return null;
		}
		public override XVar getGroupSql()
		{
			if(this._interval == 0)
			{
				return CommonFunctions.cached_ffn((XVar)(this._name));
			}
			else
			{
				dynamic grp = null;
				grp = XVar.Clone(getSqlList());
				return MVCFunctions.join(new XVar(", "), (XVar)(grp));
			}
			return null;
		}
		public override XVar getOrderSql()
		{
			if(this._interval == 0)
			{
				dynamic fname = null;
				fname = XVar.Clone((XVar.Pack(this._oldAlgorithm) ? XVar.Pack(RunnerPage._getFieldSQLDecrypt((XVar)(this._name), (XVar)(this._connection), (XVar)(this.pSet), (XVar)(this.cipherer))) : XVar.Pack(CommonFunctions.cached_ffn((XVar)(this._name)))));
				return MVCFunctions.Concat(fname, " ", this._orderBy, " ");
			}
			else
			{
				dynamic grp = XVar.Array(), newgrp = XVar.Array();
				grp = XVar.Clone(getSqlList());
				newgrp = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> g in grp.GetEnumerator())
				{
					newgrp.InitAndSetArrayItem(MVCFunctions.Concat(g.Value, " ", this._orderBy, " "), null);
				}
				return MVCFunctions.join(new XVar(", "), (XVar)(newgrp));
			}
			return null;
		}
		public override XVar getWhereSql(dynamic _param_groups)
		{
			#region pass-by-value parameters
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			dynamic hasNull = null, ret = null;
			ret = new XVar("");
			hasNull = XVar.Clone(cutNull(ref groups, new XVar(true)));
			if(0 < MVCFunctions.count(groups))
			{
				dynamic gt = null, lt = null;
				lt = new XVar("");
				gt = new XVar("");
				if(this._interval == 0)
				{
					getLtGt(ref lt, ref gt);
					ret = XVar.Clone(MVCFunctions.Concat("(", CommonFunctions.cached_ffn((XVar)(this._name)), " ", gt, " ", this._connection.addDateQuotes((XVar)(groups[0])), " AND ", CommonFunctions.cached_ffn((XVar)(this._name)), " ", lt, " ", this._connection.addDateQuotes((XVar)(groups[MVCFunctions.count(groups) - 1])), ")"));
				}
				else
				{
					if(groups[0]["MIN"] <= groups[MVCFunctions.count(groups) - 1]["MAX"])
					{
						ret = XVar.Clone(MVCFunctions.Concat("(", CommonFunctions.cached_ffn((XVar)(this._name)), " >= ", this._connection.addDateQuotes((XVar)(groups[0]["MIN"])), " AND ", CommonFunctions.cached_ffn((XVar)(this._name)), " <= ", this._connection.addDateQuotes((XVar)(groups[MVCFunctions.count(groups) - 1]["MAX"])), ")"));
					}
					else
					{
						ret = XVar.Clone(MVCFunctions.Concat("(", CommonFunctions.cached_ffn((XVar)(this._name)), " <= ", this._connection.addDateQuotes((XVar)(groups[0]["MAX"])), " AND ", CommonFunctions.cached_ffn((XVar)(this._name)), " >= ", this._connection.addDateQuotes((XVar)(groups[MVCFunctions.count(groups) - 1]["MIN"])), ")"));
					}
				}
			}
			if(XVar.Pack(hasNull))
			{
				ret = MVCFunctions.Concat(ret, (XVar.Pack(ret) ? XVar.Pack(" OR ") : XVar.Pack("")), CommonFunctions.cached_ffn((XVar)(this._name)), " IS NULL ");
			}
			return (XVar.Pack(ret) ? XVar.Pack(MVCFunctions.Concat("(", ret, ")")) : XVar.Pack(""));
		}
		public override XVar getFieldName(dynamic _param_fieldValue, dynamic _param_data = null, dynamic _param_pageObject = null)
		{
			#region default values
			if(_param_data as Object == null) _param_data = new XVar();
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldValue = XVar.Clone(_param_fieldValue);
			dynamic data = XVar.Clone(_param_data);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			dynamic date = XVar.Array(), value = null;
			value = XVar.Clone(data[(XVar.Pack(this._recordBasedRequest) ? XVar.Pack(this._name) : XVar.Pack(this._sqlname))]);
			if((XVar)((XVar)(value == null)  || (XVar)(!(XVar)(value)))  || (XVar)(MVCFunctions.strcasecmp((XVar)(value), new XVar("null")) == 0))
			{
				return "NULL";
			}
			if(this._interval == 0)
			{
				if(XVar.Pack(this._viewFormat))
				{
					if(XVar.Pack(!(XVar)(this._recordBasedRequest)))
					{
						data.InitAndSetArrayItem(value, this._name);
					}
					return pageObject.formatReportFieldValue((XVar)(this._name), (XVar)(data));
				}
				else
				{
					date = XVar.Clone(CommonFunctions.db2time((XVar)(value)));
					return CommonFunctions.str_format_datetime((XVar)(date));
				}
			}
			switch(((XVar)this._interval).ToInt())
			{
				case 1:
					date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(value)));
					return date[0];
				case 2:
					date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(value)));
					return MVCFunctions.Concat(date[0], "/Q", MVCFunctions.intval((XVar)(date[1] / 3)));
				case 3:
					date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(value)));
					return MVCFunctions.Concat(GlobalVars.locale_info[MVCFunctions.Concat("LOCALE_SABBREVMONTHNAME", date[1])], " ", date[0]);
				case 4:
					return CommonFunctions.cached_formatweekstart((XVar)(value));
				case 5:
					date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(value)));
					return CommonFunctions.format_shortdate((XVar)(date));
				case 6:
					date = XVar.Clone(CommonFunctions.db2time((XVar)(value)));
					date.InitAndSetArrayItem(0, 4);
					date.InitAndSetArrayItem(0, 5);
					return CommonFunctions.str_format_datetime((XVar)(date));
				case 7:
					date = XVar.Clone(CommonFunctions.db2time((XVar)(value)));
					date.InitAndSetArrayItem(0, 5);
					return CommonFunctions.str_format_datetime((XVar)(date));
			}
			return null;
		}
		public override XVar getGroup(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(this._interval == 0)
			{
				return data[alias()];
			}
			else
			{
				if((XVar)(data[MVCFunctions.Concat(alias(), "MIN")] == null)  || (XVar)(data[MVCFunctions.Concat(alias(), "MAX")] == null))
				{
					return null;
				}
				else
				{
					return new XVar("MIN", data[MVCFunctions.Concat(alias(), "MIN")], "MAX", data[MVCFunctions.Concat(alias(), "MAX")]);
				}
			}
			return null;
		}
		public override XVar getKey(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(XVar.Pack(!(XVar)(this._recordBasedRequest)))
			{
				if(this._interval == 0)
				{
					return data[alias()];
				}
				else
				{
					dynamic key = XVar.Array(), nCnt = null;
					key = XVar.Clone(XVar.Array());
					nCnt = XVar.Clone(this._start);
					for(;nCnt < this._interval + this._start; nCnt++)
					{
						key.InitAndSetArrayItem(data[MVCFunctions.Concat(this._alias, nCnt)], null);
					}
					return MVCFunctions.join(new XVar("-"), (XVar)(key));
				}
			}
			else
			{
				dynamic strdate = null;
				strdate = XVar.Clone(data[this._name]);
				if(strdate == null)
				{
					return "NULL";
				}
				if(this._interval == 0)
				{
					return strdate;
				}
				else
				{
					dynamic date = XVar.Array(), start = XVar.Array();
					switch(((XVar)this._interval).ToInt())
					{
						case 1:
							date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strdate)));
							return date[0];
						case 2:
							date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strdate)));
							return MVCFunctions.Concat(date[0], "-", MVCFunctions.intval((XVar)(date[1] / 3)));
						case 3:
							date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strdate)));
							return MVCFunctions.Concat(date[0], "-", date[1]);
						case 4:
							start = XVar.Clone(CommonFunctions.cached_getweekstart((XVar)(strdate)));
							return MVCFunctions.Concat(start[0], "-", start[1], "-", start[2]);
						case 5:
							date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strdate)));
							return MVCFunctions.Concat(date[0], "-", date[1], "-", date[2]);
						case 6:
							date = XVar.Clone(CommonFunctions.db2time((XVar)(strdate)));
							return MVCFunctions.Concat(date[0], "-", date[1], "-", date[2], "-", date[3]);
						case 7:
							date = XVar.Clone(CommonFunctions.db2time((XVar)(strdate)));
							return MVCFunctions.Concat(date[0], "-", date[1], "-", date[2], "-", date[3], "-", date[4]);
					}
				}
			}
			return null;
		}
		public override XVar overrideFormat()
		{
			return true;
		}
		public override XVar cutNull(ref dynamic range, dynamic _param_checkEmpty = null)
		{
			#region default values
			if(_param_checkEmpty as Object == null) _param_checkEmpty = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic checkEmpty = XVar.Clone(_param_checkEmpty);
			#endregion

			dynamic b = null, nCnt = null, ret = null, var_out = XVar.Array();
			ret = new XVar(false);
			var_out = XVar.Clone(XVar.Array());
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(range); nCnt++)
			{
				b = new XVar(false);
				if(XVar.Equals(XVar.Pack(range[nCnt]), XVar.Pack(null)))
				{
					b = new XVar(true);
					ret = new XVar(true);
				}
				else
				{
					if(XVar.Pack(checkEmpty))
					{
						if(XVar.Pack(MVCFunctions.is_array((XVar)(range[nCnt]))))
						{
							if((XVar)(!(XVar)(range[nCnt]["MIN"]))  || (XVar)(MVCFunctions.strcasecmp((XVar)(range[nCnt]["MIN"]), new XVar("null")) == 0))
							{
								b = new XVar(true);
								ret = new XVar(true);
							}
						}
						else
						{
							if((XVar)(!(XVar)(range[nCnt]))  || (XVar)(MVCFunctions.strcasecmp((XVar)(range[nCnt]), new XVar("null")) == 0))
							{
								b = new XVar(true);
								ret = new XVar(true);
							}
						}
					}
				}
				if(XVar.Pack(!(XVar)(b)))
				{
					var_out.InitAndSetArrayItem(range[nCnt], null);
				}
			}
			range = XVar.Clone(var_out);
			return ret;
		}
	}
	public partial class SQLStatement : XClass
	{
		public dynamic _fields = XVar.Array();
		public dynamic _hasDetails = XVar.Pack(true);
		public dynamic _originalSql = XVar.Pack(null);
		public dynamic _order_in;
		public dynamic _order_out;
		public dynamic _order_old;
		public dynamic _aggregates = XVar.Array();
		public dynamic _skipCount = XVar.Pack(0);
		public dynamic _reportGlobalSummary = XVar.Pack(true);
		public dynamic _reportSummary = XVar.Pack(true);
		public dynamic _details = XVar.Pack(true);
		public dynamic _from = XVar.Pack(0);
		public dynamic _groupsTotal;
		public dynamic _limitLevel = XVar.Pack(0);
		public dynamic _hasGroups = XVar.Pack(true);
		public dynamic _recordBasedRequest = XVar.Pack(false);
		public dynamic _oldAlgorithm = XVar.Pack(false);
		public dynamic tName = XVar.Pack("");
		public dynamic shortTName = XVar.Pack("");
		public dynamic repGroupFieldsCount = XVar.Pack(0);
		public dynamic repPageSummary = XVar.Pack(0);
		public dynamic repGlobalSummary = XVar.Pack(0);
		public dynamic repLayout = XVar.Pack(0);
		public dynamic showGroupSummaryCount = XVar.Pack(0);
		public dynamic repShowDet = XVar.Pack(0);
		public dynamic repGroupFields = XVar.Array();
		public dynamic tKeyFields = XVar.Array();
		public dynamic isExistTotalFields = XVar.Pack(false);
		public dynamic fieldsArr = XVar.Array();
		public dynamic orderIndexes;
		public ProjectSettings pSet = null;
		public dynamic _connection;
		public dynamic _cipherer;
		public dynamic pageObject;
		public dynamic searchWhereClause = XVar.Pack("");
		public dynamic searchHavingClause = XVar.Pack("");
		public SQLStatement(dynamic _param_sql, dynamic _param_order, dynamic _param_groupsTotal, dynamic _param_connection, dynamic var_params, dynamic _param_searchWhereClause, dynamic _param_searchHavingClause, dynamic _param_cipherer, dynamic _param_pageObject)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic order = XVar.Clone(_param_order);
			dynamic groupsTotal = XVar.Clone(_param_groupsTotal);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic searchWhereClause = XVar.Clone(_param_searchWhereClause);
			dynamic searchHavingClause = XVar.Clone(_param_searchHavingClause);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			dynamic fields = XVar.Array(), i = null, j = null, start = null;
			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			this._connection = XVar.Clone(connection);
			this._cipherer = XVar.Clone(cipherer);
			this.searchWhereClause = XVar.Clone(searchWhereClause);
			this.searchHavingClause = XVar.Clone(searchHavingClause);
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_REPORT)));
			this.pageObject = XVar.Clone(pageObject);
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(sql)))))
			{
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			this._originalSql = XVar.Clone(applyWhere((XVar)(sql)));
			start = new XVar(0);
			fields = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.repGroupFields); i++)
			{
				j = new XVar(0);
				for(;j < MVCFunctions.count(this.fieldsArr); j++)
				{
					if(this.repGroupFields[i]["strGroupField"] == this.fieldsArr[j]["name"])
					{
						dynamic add = XVar.Array();
						add = XVar.Clone(XVar.Array());
						add.InitAndSetArrayItem(this.fieldsArr[j]["name"], "name");
						if(XVar.Pack(CommonFunctions.IsNumberType((XVar)(this.pSet.getFieldType((XVar)(this.fieldsArr[j]["name"]))))))
						{
							add.InitAndSetArrayItem("numeric", "type");
						}
						else
						{
							if(XVar.Pack(CommonFunctions.IsCharType((XVar)(this.pSet.getFieldType((XVar)(this.fieldsArr[j]["name"]))))))
							{
								add.InitAndSetArrayItem("char", "type");
								add.InitAndSetArrayItem(GlobalVars.reportCaseSensitiveGroupFields, "case_sensitive");
							}
							else
							{
								if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(this.pSet.getFieldType((XVar)(this.fieldsArr[j]["name"]))))))
								{
									add.InitAndSetArrayItem("date", "type");
								}
								else
								{
									add.InitAndSetArrayItem("char", "type");
								}
							}
						}
						add.InitAndSetArrayItem(this.repGroupFields[i]["groupInterval"], "interval");
						add.InitAndSetArrayItem(this.fieldsArr[j]["viewFormat"], "viewformat");
						add.InitAndSetArrayItem(1, "rowsinsummary");
						if((XVar)((XVar)((XVar)(this.fieldsArr[j]["totalMax"])  || (XVar)(this.fieldsArr[j]["totalMin"]))  || (XVar)(this.fieldsArr[j]["totalAvg"]))  || (XVar)(this.fieldsArr[j]["totalSum"]))
						{
							add["rowsinsummary"]++;
						}
						if(this.repLayout == Constants.REPORT_STEPPED)
						{
							add.InitAndSetArrayItem(1, "rowsinheader");
						}
						else
						{
							if(this.repLayout == Constants.REPORT_BLOCK)
							{
								add.InitAndSetArrayItem(0, "rowsinheader");
							}
							else
							{
								if((XVar)(this.repLayout == Constants.REPORT_OUTLINE)  || (XVar)(this.repLayout == Constants.REPORT_ALIGN))
								{
									if(j == MVCFunctions.count(this.fieldsArr) - 1)
									{
										add.InitAndSetArrayItem(2, "rowsinheader");
									}
									else
									{
										add.InitAndSetArrayItem(1, "rowsinheader");
									}
								}
								else
								{
									if(this.repLayout == Constants.REPORT_TABULAR)
									{
										add.InitAndSetArrayItem(0, "rowsinheader");
									}
								}
							}
						}
						fields.InitAndSetArrayItem(add, null);
					}
				}
			}
			this._hasGroups = XVar.Clone(0 < MVCFunctions.count(fields));
			foreach (KeyValuePair<XVar, dynamic> field in fields.GetEnumerator())
			{
				dynamic f = null;
				f = XVar.Clone(CommonFunctions.create_reportfield((XVar)(field.Value["name"]), (XVar)(field.Value["type"]), (XVar)(field.Value["interval"]), new XVar("grp"), (XVar)(this.tName), (XVar)(this._connection), (XVar)(this._cipherer)));
				start = XVar.Clone(f.setStart((XVar)(start)));
				if(XVar.Pack(field.Value.KeyExists("case_sensitive")))
				{
					f.setCaseSensitive((XVar)(field.Value["case_sensitive"]));
				}
				if(XVar.Pack(field.Value.KeyExists("rowsinsummary")))
				{
					f._rowsInSummary = XVar.Clone(field.Value["rowsinsummary"]);
				}
				if(XVar.Pack(field.Value.KeyExists("rowsinheader")))
				{
					f._rowsInHeader = XVar.Clone(field.Value["rowsinheader"]);
				}
				f._viewFormat = XVar.Clone(field.Value["viewformat"]);
				this._fields.InitAndSetArrayItem(f, null);
			}
			if(XVar.Pack(order))
			{
				dynamic order_in = XVar.Array(), order_old = XVar.Array(), order_out = XVar.Array();
				order_in = XVar.Clone(XVar.Array());
				order_out = XVar.Clone(XVar.Array());
				order_old = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> o in order.GetEnumerator())
				{
					dynamic groupField = null;
					order_in.InitAndSetArrayItem(MVCFunctions.Concat(o.Value[2], " as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat("originalorder", o.Value[0])))), null);
					order_out.InitAndSetArrayItem(MVCFunctions.Concat(CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat("originalorder", o.Value[0]))), " ", o.Value[1]), null);
					groupField = new XVar(false);
					i = new XVar(0);
					for(;i < MVCFunctions.count(this.repGroupFields); i++)
					{
						j = new XVar(0);
						for(;j < MVCFunctions.count(this.fieldsArr); j++)
						{
							if(this.repGroupFields[i]["strGroupField"] == this.fieldsArr[j]["name"])
							{
								dynamic fieldIndex = null;
								fieldIndex = XVar.Clone(this.pSet.getFieldIndex((XVar)(this.repGroupFields[i]["strGroupField"])));
								if(fieldIndex == o.Value[0])
								{
									dynamic n = null;
									n = XVar.Clone(this.repGroupFields[i]["groupOrder"] - 1);
									this._fields[n]._orderBy = XVar.Clone(o.Value[1]);
									groupField = new XVar(true);
								}
							}
						}
					}
					if(XVar.Pack(!(XVar)(groupField)))
					{
						order_old.InitAndSetArrayItem(MVCFunctions.Concat(o.Value[2], " ", o.Value[1]), null);
					}
				}
				this._order_in = XVar.Clone(MVCFunctions.join(new XVar(", "), (XVar)(order_in)));
				this._order_out = XVar.Clone(MVCFunctions.join(new XVar(", "), (XVar)(order_out)));
				this._order_old = XVar.Clone(MVCFunctions.join(new XVar(", "), (XVar)(order_old)));
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.fieldsArr); i++)
			{
				if(XVar.Pack(this.fieldsArr[i]["totalMax"]))
				{
					this._aggregates.InitAndSetArrayItem(MVCFunctions.Concat("MAX(", CommonFunctions.cached_ffn((XVar)(this.fieldsArr[i]["name"]), new XVar(true)), ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this.fieldsArr[i]["name"], "MAX")))), null);
				}
				if(XVar.Pack(this.fieldsArr[i]["totalMin"]))
				{
					this._aggregates.InitAndSetArrayItem(MVCFunctions.Concat("MIN(", CommonFunctions.cached_ffn((XVar)(this.fieldsArr[i]["name"]), new XVar(true)), ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this.fieldsArr[i]["name"], "MIN")))), null);
				}
				if(XVar.Pack(this.fieldsArr[i]["totalAvg"]))
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.IsDateFieldType((XVar)(this.pSet.getFieldType((XVar)(this.fieldsArr[i]["name"])))))))
					{
						this._aggregates.InitAndSetArrayItem(MVCFunctions.Concat("AVG(", CommonFunctions.cached_ffn((XVar)(this.fieldsArr[i]["name"]), new XVar(true)), ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this.fieldsArr[i]["name"], "AVG")))), null);
						this._aggregates.InitAndSetArrayItem(MVCFunctions.Concat("COUNT(", CommonFunctions.cached_ffn((XVar)(this.fieldsArr[i]["name"]), new XVar(true)), ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this.fieldsArr[i]["name"], "NAVG")))), null);
					}
				}
				if(XVar.Pack(this.fieldsArr[i]["totalSum"]))
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.IsDateFieldType((XVar)(this.pSet.getFieldType((XVar)(this.fieldsArr[i]["name"])))))))
					{
						this._aggregates.InitAndSetArrayItem(MVCFunctions.Concat("SUM(", CommonFunctions.cached_ffn((XVar)(this.fieldsArr[i]["name"]), new XVar(true)), ") as ", CommonFunctions.cached_ffn((XVar)(MVCFunctions.Concat(this.fieldsArr[i]["name"], "SUM")))), null);
					}
				}
			}
			this._reportSummary = XVar.Clone((XVar)(this.repPageSummary)  || (XVar)(this.repGlobalSummary));
			this._groupsTotal = XVar.Clone(groupsTotal);
		}
		public virtual XVar getOriginal(dynamic _param_useOriginalOrder = null)
		{
			#region default values
			if(_param_useOriginalOrder as Object == null) _param_useOriginalOrder = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic useOriginalOrder = XVar.Clone(_param_useOriginalOrder);
			#endregion

			dynamic sql = XVar.Array();
			sql = XVar.Clone(this._originalSql);
			if(XVar.Pack(this.pageObject))
			{
				dynamic hwhere = null;
				if(this.pageObject.pageType == Constants.PAGE_REPORT)
				{
					if(XVar.Pack(this.pageObject.eventsObject.exists(new XVar("BeforeQueryReport"))))
					{
						hwhere = XVar.Clone(sql[2]);
						this.pageObject.eventsObject.BeforeQueryReport((XVar)(hwhere));
						sql.InitAndSetArrayItem(hwhere, 2);
					}
				}
				else
				{
					if(XVar.Pack(this.pageObject.eventsObject.exists(new XVar("BeforeQueryReportPrint"))))
					{
						hwhere = XVar.Clone(sql[2]);
						this.pageObject.eventsObject.BeforeQueryReportPrint((XVar)(hwhere));
						sql.InitAndSetArrayItem(hwhere, 2);
					}
				}
			}
			return MVCFunctions.Concat(sql[0], " ", (XVar.Pack((XVar)((XVar)(useOriginalOrder)  && (XVar)(this._order_in))  && (XVar)(!(XVar)(this._oldAlgorithm))) ? XVar.Pack(MVCFunctions.Concat(", ", this._order_in, " ")) : XVar.Pack("")), sql[1], " ", (XVar.Pack(sql[2]) ? XVar.Pack(MVCFunctions.Concat(" WHERE ", sql[2])) : XVar.Pack("")), " ", sql[3], " ", (XVar.Pack(sql[4]) ? XVar.Pack(MVCFunctions.Concat(" HAVING ", sql[4])) : XVar.Pack("")));
		}
		public virtual XVar setRecordBasedRequest(dynamic _param_recordBasedRequest)
		{
			#region pass-by-value parameters
			dynamic recordBasedRequest = XVar.Clone(_param_recordBasedRequest);
			#endregion

			dynamic nCnt = null;
			this._recordBasedRequest = XVar.Clone(recordBasedRequest);
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(this._fields); nCnt++)
			{
				this._fields[nCnt]._recordBasedRequest = XVar.Clone(recordBasedRequest);
			}
			return null;
		}
		public virtual XVar getGroup(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			return this._fields[0].getGroup((XVar)(data));
		}
		public virtual XVar field(dynamic _param_num)
		{
			#region pass-by-value parameters
			dynamic num = XVar.Clone(_param_num);
			#endregion

			return this._fields[num];
		}
		public virtual XVar getSQLLimits(dynamic _param_sql, dynamic _param_from)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic from = XVar.Clone(_param_from);
			#endregion

			if((XVar)(XVar.Pack(0) <= from)  && (XVar)(0 < this._groupsTotal))
			{
				dynamic var_out = null;
				if((XVar)(this._connection.dbType == Constants.nDATABASE_MySQL)  || (XVar)(this._connection.dbType == Constants.nDATABASE_PostgreSQL))
				{
					var_out = XVar.Clone(MVCFunctions.Concat(sql, " LIMIT ", MVCFunctions.intval((XVar)(this._groupsTotal)), " OFFSET ", MVCFunctions.intval((XVar)(from))));
					this._skipCount = new XVar(0);
				}
				else
				{
					if((XVar)(this._connection.dbType == Constants.nDATABASE_MSSQLServer)  || (XVar)(this._connection.dbType == Constants.nDATABASE_Access))
					{
						dynamic nsel = null;
						nsel = XVar.Clone(MVCFunctions.stripos((XVar)(sql), new XVar("select")));
						var_out = XVar.Clone(MVCFunctions.substr_replace((XVar)(sql), (XVar)(MVCFunctions.Concat("select top ", from + this._groupsTotal)), (XVar)(nsel), (XVar)(MVCFunctions.strlen(new XVar("select")))));
						this._skipCount = XVar.Clone(from);
					}
					else
					{
						if(this._connection.dbType == Constants.nDATABASE_Oracle)
						{
							var_out = XVar.Clone(MVCFunctions.Concat("select * from (select original2.*, rownum as ", CommonFunctions.cached_ffn(new XVar("rownumber")), " from (", sql, ") original2) where ", CommonFunctions.cached_ffn(new XVar("rownumber")), " between ", from, " and ", from + this._groupsTotal));
							this._skipCount = new XVar(0);
						}
					}
				}
				return var_out;
			}
			return sql;
		}
		public virtual XVar sqlg(dynamic _param_donotlimit = null, dynamic _param_doorder = null)
		{
			#region default values
			if(_param_donotlimit as Object == null) _param_donotlimit = new XVar(false);
			if(_param_doorder as Object == null) _param_doorder = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic donotlimit = XVar.Clone(_param_donotlimit);
			dynamic doorder = XVar.Clone(_param_doorder);
			#endregion

			dynamic g = XVar.Array(), hsql = XVar.Array(), o = XVar.Array(), s = XVar.Array();
			hsql = XVar.Clone(XVar.Array());
			s = XVar.Clone(XVar.Array());
			g = XVar.Clone(XVar.Array());
			o = XVar.Clone(XVar.Array());
			if(XVar.Pack(this._hasGroups))
			{
				s.InitAndSetArrayItem(this._fields[0].getSelectSql(new XVar(true)), null);
				g.InitAndSetArrayItem(this._fields[0].getGroupSql(), null);
				o.InitAndSetArrayItem(this._fields[0].getOrderSql(), null);
			}
			if(XVar.Pack(MVCFunctions.count(s)))
			{
				hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(s)), "select", null);
			}
			if(XVar.Pack(MVCFunctions.count(g)))
			{
				hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(g)), "groupby", null);
			}
			if((XVar)(MVCFunctions.count(o))  && (XVar)(doorder))
			{
				hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(o)), "orderby");
			}
			if((XVar)(this._limitLevel == 1)  && (XVar)(!(XVar)(donotlimit)))
			{
				hsql.InitAndSetArrayItem(1, "limits");
			}
			return buildsql((XVar)(hsql));
		}
		public virtual XVar sqlcg()
		{
			dynamic gsql = null;
			gsql = XVar.Clone(sqlg(new XVar(true), new XVar(false)));
			return MVCFunctions.Concat("select count(*) as ", CommonFunctions.cached_ffn(new XVar("c")), " from (", gsql, ") countgroups");
		}
		public virtual XVar sqlt()
		{
			dynamic hsql = XVar.Array();
			hsql = XVar.Clone(XVar.Array());
			hsql.InitAndSetArrayItem(MVCFunctions.Concat("count(1) as ", CommonFunctions.cached_ffn(new XVar("countField"))), "select", null);
			if(XVar.Pack(MVCFunctions.count(this._aggregates)))
			{
				hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(this._aggregates)), "select", null);
			}
			return buildsql((XVar)(hsql));
		}
		public virtual XVar sql2(dynamic _param_groups = null)
		{
			#region default values
			if(_param_groups as Object == null) _param_groups = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			dynamic hsql = XVar.Array(), o = XVar.Array();
			hsql = XVar.Clone(XVar.Array());
			if((XVar)(!(XVar)(this._hasGroups))  || (XVar)(this._recordBasedRequest))
			{
				hsql.InitAndSetArrayItem(true, "original");
				o = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> f in this._fields.GetEnumerator())
				{
					o.InitAndSetArrayItem(f.Value.getOrderSql(), null);
				}
				if(XVar.Pack(MVCFunctions.count(o)))
				{
					hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(o)), "orderby");
				}
			}
			else
			{
				dynamic g = XVar.Array(), s = XVar.Array();
				if(XVar.Pack(this.repShowDet))
				{
					hsql.InitAndSetArrayItem("original.*", "select", null);
				}
				else
				{
					if(XVar.Pack(MVCFunctions.count(this._aggregates)))
					{
						hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(this._aggregates)), "select", null);
					}
				}
				s = XVar.Clone(XVar.Array());
				g = XVar.Clone(XVar.Array());
				o = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> f in this._fields.GetEnumerator())
				{
					s.InitAndSetArrayItem(f.Value.getSelectSql((XVar)(!(XVar)(this.repShowDet))), null);
					if(XVar.Pack(!(XVar)(this.repShowDet)))
					{
						g.InitAndSetArrayItem(f.Value.getGroupSql(), null);
					}
					o.InitAndSetArrayItem(f.Value.getOrderSql(), null);
				}
				if((XVar)((XVar)(this._reportSummary)  && (XVar)(this._hasGroups))  && (XVar)(!(XVar)(this.repShowDet)))
				{
					hsql.InitAndSetArrayItem(MVCFunctions.Concat("count(1) as ", CommonFunctions.cached_ffn(new XVar("countField"))), "select", null);
				}
				if(XVar.Pack(MVCFunctions.count(s)))
				{
					hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(s)), "select", null);
				}
				if((XVar)(!XVar.Equals(XVar.Pack(groups), XVar.Pack(null)))  && (XVar)(MVCFunctions.count(groups)))
				{
					dynamic where = null;
					where = XVar.Clone(this._fields[0].getWhereSql((XVar)(groups)));
					if(XVar.Pack(where))
					{
						hsql.InitAndSetArrayItem(where, "where");
					}
				}
				if(XVar.Pack(MVCFunctions.count(g)))
				{
					hsql.InitAndSetArrayItem(g, "groupby");
				}
				if(XVar.Pack(MVCFunctions.count(o)))
				{
					hsql.InitAndSetArrayItem(MVCFunctions.join(new XVar(", "), (XVar)(o)), "orderby");
				}
			}
			if(this._limitLevel == 2)
			{
				hsql.InitAndSetArrayItem(1, "limits");
			}
			if(XVar.Pack(this.repShowDet))
			{
				hsql.InitAndSetArrayItem(1, "origorder");
			}
			return hsql;
		}
		public virtual XVar buildsql(dynamic _param_hsql)
		{
			#region pass-by-value parameters
			dynamic hsql = XVar.Clone(_param_hsql);
			#endregion

			dynamic ordered = null, osql = null, sql = null;
			this._skipCount = new XVar(0);
			ordered = new XVar(false);
			if((XVar)(MVCFunctions.count(hsql) == 0)  || (XVar)(hsql["original"]))
			{
				sql = XVar.Clone(getOriginal());
			}
			else
			{
				sql = new XVar("SELECT ");
				if((XVar)(hsql["select"])  && (XVar)(0 < MVCFunctions.count(hsql["select"])))
				{
					sql = MVCFunctions.Concat(sql, MVCFunctions.join(new XVar(", "), (XVar)(hsql["select"])));
				}
				else
				{
					sql = MVCFunctions.Concat(sql, " * ");
				}
				sql = MVCFunctions.Concat(sql, " FROM (", getOriginal((XVar)(hsql["origorder"])), ") original");
				if((XVar)(hsql["where"])  && (XVar)(0 < MVCFunctions.count(hsql["where"])))
				{
					sql = MVCFunctions.Concat(sql, " WHERE ", hsql["where"]);
				}
				if((XVar)(hsql["groupby"])  && (XVar)(0 < MVCFunctions.count(hsql["groupby"])))
				{
					sql = MVCFunctions.Concat(sql, " GROUP BY ", MVCFunctions.join(new XVar(", "), (XVar)(hsql["groupby"])));
				}
			}
			osql = new XVar("");
			if((XVar)(hsql["orderby"])  && (XVar)(0 < MVCFunctions.count(hsql["orderby"])))
			{
				osql = MVCFunctions.Concat(osql, hsql["orderby"]);
				ordered = new XVar(true);
			}
			if(XVar.Pack(hsql["origorder"]))
			{
				if(XVar.Pack(!(XVar)(this._oldAlgorithm)))
				{
					if(XVar.Pack(this._order_out))
					{
						osql = MVCFunctions.Concat(osql, (XVar.Pack(osql) ? XVar.Pack(", ") : XVar.Pack("")), this._order_out);
					}
				}
				else
				{
					if(XVar.Pack(this._order_old))
					{
						osql = MVCFunctions.Concat(osql, (XVar.Pack(osql) ? XVar.Pack(", ") : XVar.Pack("")), this._order_old);
					}
				}
			}
			if(XVar.Pack(osql))
			{
				sql = MVCFunctions.Concat(sql, " ORDER BY ", osql);
			}
			if(XVar.Pack(hsql["limits"]))
			{
				sql = XVar.Clone(getSQLLimits((XVar)(sql), (XVar)(this._from)));
			}
			return sql;
		}
		public virtual XVar applyWhere(dynamic sql)
		{
			if(XVar.Pack(this.searchWhereClause))
			{
				sql.InitAndSetArrayItem(CommonFunctions.whereAdd((XVar)(sql[2]), (XVar)(this.searchWhereClause)), 2);
			}
			if(XVar.Pack(this.searchHavingClause))
			{
				if(XVar.Pack(sql[4]))
				{
					sql.InitAndSetArrayItem(MVCFunctions.Concat("(", sql[4], ") AND "), 4);
				}
				sql[4] = MVCFunctions.Concat(sql[4], "(", this.searchHavingClause, ") ");
			}
			return sql;
		}
		public virtual XVar setOldAlgorithm(dynamic _param_useOldAlgorithm = null)
		{
			#region default values
			if(_param_useOldAlgorithm as Object == null) _param_useOldAlgorithm = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic useOldAlgorithm = XVar.Clone(_param_useOldAlgorithm);
			#endregion

			dynamic nCnt = null;
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(this._fields); nCnt++)
			{
				this._fields[nCnt]._oldAlgorithm = XVar.Clone(useOldAlgorithm);
			}
			this._oldAlgorithm = XVar.Clone(useOldAlgorithm);
			return null;
		}
	}
	public partial class Summarable : XClass
	{
		public dynamic _summary = XVar.Array();
		public dynamic tName = XVar.Pack("");
		public dynamic shortTName = XVar.Pack("");
		public dynamic repGroupFieldsCount = XVar.Pack(0);
		public dynamic repPageSummary = XVar.Pack(0);
		public dynamic repGlobalSummary = XVar.Pack(0);
		public dynamic repLayout = XVar.Pack(0);
		public dynamic showGroupSummaryCount = XVar.Pack(0);
		public dynamic repShowDet = XVar.Pack(0);
		public dynamic repGroupFields = XVar.Array();
		public dynamic tKeyFields = XVar.Array();
		public dynamic isExistTotalFields = XVar.Pack(false);
		public dynamic fieldsArr = XVar.Array();
		public dynamic cipherer = XVar.Pack(null);
		public Summarable(dynamic var_params)
		{
			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			__init();
		}
		public virtual XVar init(dynamic _param_from = null)
			{return __init(_param_from);}

		private XVar __init(dynamic _param_from = null)
		{
			#region default values
			if(_param_from as Object == null) _param_from = new XVar(0);
			#endregion

			#region pass-by-value parameters
			dynamic from = XVar.Clone(_param_from);
			#endregion

			this["field", "_from"] = XVar.Clone(from);
			this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.tName)));
			return null;
		}
		public virtual XVar writeGroup(dynamic begin, dynamic var_end, dynamic _param_gkey, dynamic _param_grp, dynamic _param_nField)
		{
			#region pass-by-value parameters
			dynamic gkey = XVar.Clone(_param_gkey);
			dynamic grp = XVar.Clone(_param_grp);
			dynamic nField = XVar.Clone(_param_nField);
			#endregion

			return null;
		}
		public virtual XVar addSummary(dynamic _param_recordsMode, dynamic summary, dynamic _param_data, ref dynamic nTotalRecords)
		{
			#region pass-by-value parameters
			dynamic recordsMode = XVar.Clone(_param_recordsMode);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic avg_value = null, countInGroup = null, field = XVar.Array(), fieldName = null, i = null, s = XVar.Array();
			countInGroup = XVar.Clone((XVar.Pack(summary.KeyExists("count")) ? XVar.Pack(summary["count"]) : XVar.Pack(0)));
			if(XVar.Pack(this.isExistTotalFields))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(summary["summary"])))))
				{
					summary.InitAndSetArrayItem(XVar.Array(), "summary");
				}
				s = summary["summary"];
			}
			if(XVar.Pack(recordsMode))
			{
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.fieldsArr); i++)
				{
					field = this.fieldsArr[i];
					fieldName = XVar.Clone(field["name"]);
					if((XVar)((XVar)((XVar)(!(XVar)(field["totalMax"]))  && (XVar)(!(XVar)(field["totalMin"])))  && (XVar)(!(XVar)(field["totalAvg"])))  && (XVar)(!(XVar)(field["totalSum"])))
					{
						continue;
					}
					if(XVar.Equals(XVar.Pack(data[fieldName]), XVar.Pack(null)))
					{
						continue;
					}
					if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(s[fieldName])))))
					{
						s.InitAndSetArrayItem(XVar.Array(), fieldName);
					}
					if(XVar.Pack(!(XVar)(s[fieldName].KeyExists("count"))))
					{
						s.InitAndSetArrayItem(0, fieldName, "count");
					}
					if(XVar.Pack(field["totalMax"]))
					{
						if((XVar)(!(XVar)(s[fieldName].KeyExists("MAX")))  || (XVar)(s[fieldName]["MAX"] < data[fieldName]))
						{
							s.InitAndSetArrayItem(data[fieldName], fieldName, "MAX");
						}
					}
					if(XVar.Pack(field["totalMin"]))
					{
						if((XVar)(!(XVar)(s[fieldName].KeyExists("MIN")))  || (XVar)(data[fieldName] < s[fieldName]["MIN"]))
						{
							s.InitAndSetArrayItem(data[fieldName], fieldName, "MIN");
						}
					}
					if(XVar.Pack(field["totalAvg"]))
					{
						if(field["viewFormat"] == "Time")
						{
							avg_value = XVar.Clone(value2time((XVar)(data[fieldName])));
						}
						else
						{
							avg_value = XVar.Clone(data[fieldName]);
						}
						s.InitAndSetArrayItem(s[fieldName]["AVG"] * s[fieldName]["count"] + avg_value, fieldName, "AVG");
						s[fieldName]["count"]++;
						if(s[fieldName]["count"] != 0)
						{
							s.InitAndSetArrayItem(s[fieldName]["AVG"] / s[fieldName]["count"], fieldName, "AVG");
						}
					}
					if(XVar.Pack(field["totalSum"]))
					{
						if(field["viewFormat"] == "Time")
						{
							s[fieldName]["SUM"] += value2time((XVar)(data[fieldName]));
						}
						else
						{
							s[fieldName]["SUM"] += data[fieldName];
						}
					}
				}
				nTotalRecords++;
				countInGroup++;
			}
			else
			{
				dynamic summaryField = XVar.Array();
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.fieldsArr); i++)
				{
					field = this.fieldsArr[i];
					if((XVar)((XVar)((XVar)(!(XVar)(field["totalMax"]))  && (XVar)(!(XVar)(field["totalMin"])))  && (XVar)(!(XVar)(field["totalAvg"])))  && (XVar)(!(XVar)(field["totalSum"])))
					{
						continue;
					}
					fieldName = XVar.Clone(field["name"]);
					if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(s[fieldName])))))
					{
						s.InitAndSetArrayItem(XVar.Array(), fieldName);
					}
					summaryField = s[fieldName];
					if(XVar.Pack(field["totalMax"]))
					{
						if(!XVar.Equals(XVar.Pack(data[MVCFunctions.Concat(fieldName, "MAX")]), XVar.Pack(null)))
						{
							if((XVar)(!(XVar)(summaryField.KeyExists("MAX")))  || (XVar)(summaryField["MAX"] < data[MVCFunctions.Concat(fieldName, "MAX")]))
							{
								summaryField.InitAndSetArrayItem(data[MVCFunctions.Concat(fieldName, "MAX")], "MAX");
							}
						}
					}
					if(XVar.Pack(field["totalMin"]))
					{
						if(!XVar.Equals(XVar.Pack(data[MVCFunctions.Concat(fieldName, "MIN")]), XVar.Pack(null)))
						{
							if((XVar)(!(XVar)(summaryField.KeyExists("MIN")))  || (XVar)(data[MVCFunctions.Concat(fieldName, "MIN")] < summaryField["MIN"]))
							{
								summaryField.InitAndSetArrayItem(data[MVCFunctions.Concat(fieldName, "MIN")], "MIN");
							}
						}
					}
					if(XVar.Pack(field["totalAvg"]))
					{
						if(!XVar.Equals(XVar.Pack(data[MVCFunctions.Concat(fieldName, "AVG")]), XVar.Pack(null)))
						{
							if(field["viewFormat"] == "Time")
							{
								avg_value = XVar.Clone(value2time((XVar)(data[MVCFunctions.Concat(fieldName, "AVG")])));
							}
							else
							{
								avg_value = XVar.Clone(data[MVCFunctions.Concat(fieldName, "AVG")]);
							}
							summaryField.InitAndSetArrayItem(summaryField["AVG"] * summaryField["count"] + avg_value * data[MVCFunctions.Concat(fieldName, "NAVG")], "AVG");
							summaryField["count"] += data[MVCFunctions.Concat(fieldName, "NAVG")];
							if(summaryField["count"] != 0)
							{
								summaryField.InitAndSetArrayItem(summaryField["AVG"] / summaryField["count"], "AVG");
							}
						}
					}
					if(XVar.Pack(field["totalSum"]))
					{
						if(!XVar.Equals(XVar.Pack(data[MVCFunctions.Concat(fieldName, "SUM")]), XVar.Pack(null)))
						{
							if(field["viewFormat"] == "Time")
							{
								summaryField["SUM"] += value2time((XVar)(data[MVCFunctions.Concat(fieldName, "SUM")]));
							}
							else
							{
								summaryField["SUM"] += data[MVCFunctions.Concat(fieldName, "SUM")];
							}
						}
					}
				}
				nTotalRecords += data["countField"];
				countInGroup += data["countField"];
			}
			summary.InitAndSetArrayItem(countInGroup, "count");
			return null;
		}
		public virtual XVar _makeSummary(dynamic summary, dynamic _param_deep)
		{
			#region pass-by-value parameters
			dynamic deep = XVar.Clone(_param_deep);
			#endregion

			if(XVar.Pack(!(XVar)(summary["values"])))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> group in summary["values"].GetEnumerator())
			{
				dynamic grp = XVar.Array(), i = null;
				grp = summary["values"][group.Key];
				if(XVar.Pack(grp.KeyExists("values")))
				{
					_makeSummary((XVar)(grp), (XVar)(deep + 1));
				}
				if((XVar)(grp.KeyExists("_begin"))  && (XVar)(grp.KeyExists("_end")))
				{
					writeGroup((XVar)(grp["_begin"]), (XVar)(grp["_end"]), (XVar)(group.Key), (XVar)(grp), (XVar)(deep));
				}
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(summary["summary"])))))
				{
					summary.InitAndSetArrayItem(XVar.Array(), "summary");
				}
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.fieldsArr); i++)
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(summary["summary"][this.fieldsArr[i]["name"]])))))
					{
						summary.InitAndSetArrayItem(XVar.Array(), "summary", this.fieldsArr[i]["name"]);
					}
					if(XVar.Pack(MVCFunctions.is_array((XVar)(grp["summary"]))))
					{
						if(XVar.Pack(MVCFunctions.is_array((XVar)(grp["summary"][this.fieldsArr[i]["name"]]))))
						{
							if(XVar.Pack(this.fieldsArr[i]["totalMax"]))
							{
								if(XVar.Pack(grp["summary"][this.fieldsArr[i]["name"]].KeyExists("MAX")))
								{
									if((XVar)(!(XVar)(summary["summary"][this.fieldsArr[i]["name"]].KeyExists("MAX")))  || (XVar)(summary["summary"][this.fieldsArr[i]["name"]]["MAX"] < grp["summary"][this.fieldsArr[i]["name"]]["MAX"]))
									{
										summary.InitAndSetArrayItem(grp["summary"][this.fieldsArr[i]["name"]]["MAX"], "summary", this.fieldsArr[i]["name"], "MAX");
									}
								}
							}
							if(XVar.Pack(this.fieldsArr[i]["totalMin"]))
							{
								if(XVar.Pack(grp["summary"][this.fieldsArr[i]["name"]].KeyExists("MIN")))
								{
									if((XVar)(!(XVar)(summary["summary"][this.fieldsArr[i]["name"]].KeyExists("MIN")))  || (XVar)(grp["summary"][this.fieldsArr[i]["name"]]["MIN"] < summary["summary"][this.fieldsArr[i]["name"]]["MIN"]))
									{
										summary.InitAndSetArrayItem(grp["summary"][this.fieldsArr[i]["name"]]["MIN"], "summary", this.fieldsArr[i]["name"], "MIN");
									}
								}
							}
							if(XVar.Pack(this.fieldsArr[i]["totalAvg"]))
							{
								if(XVar.Pack(grp["summary"][this.fieldsArr[i]["name"]].KeyExists("AVG")))
								{
									summary.InitAndSetArrayItem(summary["summary"][this.fieldsArr[i]["name"]]["AVG"] * summary["summary"][this.fieldsArr[i]["name"]]["count"] + grp["summary"][this.fieldsArr[i]["name"]]["AVG"] * grp["summary"][this.fieldsArr[i]["name"]]["count"], "summary", this.fieldsArr[i]["name"], "AVG");
									summary["summary"][this.fieldsArr[i]["name"]]["count"] += grp["summary"][this.fieldsArr[i]["name"]]["count"];
									if(summary["summary"][this.fieldsArr[i]["name"]]["count"] != 0)
									{
										summary.InitAndSetArrayItem(summary["summary"][this.fieldsArr[i]["name"]]["AVG"] / summary["summary"][this.fieldsArr[i]["name"]]["count"], "summary", this.fieldsArr[i]["name"], "AVG");
									}
								}
							}
							if(XVar.Pack(this.fieldsArr[i]["totalSum"]))
							{
								if(XVar.Pack(grp["summary"][this.fieldsArr[i]["name"]]["SUM"]))
								{
									summary["summary"][this.fieldsArr[i]["name"]]["SUM"] += grp["summary"][this.fieldsArr[i]["name"]]["SUM"];
								}
							}
						}
					}
				}
				summary["count"] += grp["count"];
			}
			return null;
		}
		public virtual XVar value2time(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic arr = XVar.Array(), res = null;
			res = new XVar(0);
			arr = XVar.Clone(CommonFunctions.parsenumbers((XVar)(value)));
			if(XVar.Pack(arr.KeyExists(0)))
			{
				res += (arr[0] * 60) * 60;
			}
			if(XVar.Pack(arr.KeyExists(1)))
			{
				res += arr[1] * 60;
			}
			if(XVar.Pack(arr.KeyExists(2)))
			{
				res += arr[2];
			}
			return res;
		}
		public virtual XVar time2printable(dynamic _param_time)
		{
			#region pass-by-value parameters
			dynamic time = XVar.Clone(_param_time);
			#endregion

			return new XVar(0, MVCFunctions.intval((XVar)(time / (60 * 60))), 1, MVCFunctions.intval((XVar)(time / 60)), 2, time  %  60);
		}
	}
	public partial class ReportGroups : Summarable
	{
		public dynamic _global;
		public dynamic _totalRecords;
		public dynamic _maxpages;
		public dynamic _nGroup;
		public dynamic _oldFirst;
		public dynamic _from;
		public dynamic _sql;
		public dynamic _groupsTotal;
		public dynamic _connection;
		public dynamic _allGroupsUsed;
		public dynamic _countGroups;
		protected static bool skipReportGroupsCtor = false;
		public ReportGroups(dynamic sql, dynamic _param_connection, dynamic _param_groupsTotal, dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipReportGroupsCtor)
			{
				skipReportGroupsCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic connection = XVar.Clone(_param_connection);
			dynamic groupsTotal = XVar.Clone(_param_groupsTotal);
			#endregion

			init();
			this._groupsTotal = XVar.Clone(groupsTotal);
			this._sql = sql;
			this._connection = XVar.Clone(connection);
		}
		public override XVar init(dynamic _param_from = null)
		{
			#region default values
			if(_param_from as Object == null) _param_from = new XVar(0);
			#endregion

			#region pass-by-value parameters
			dynamic from = XVar.Clone(_param_from);
			#endregion

			base.init((XVar)(from));
			this._global = XVar.Clone(XVar.Array());
			this._totalRecords = new XVar(0);
			this._maxpages = XVar.Clone(-1);
			this._from = XVar.Clone(from);
			this._nGroup = XVar.Clone(-1);
			this._oldFirst = new XVar("");
			this._allGroupsUsed = new XVar(false);
			this._countGroups = new XVar(0);
			return null;
		}
		public virtual XVar setGlobalSummary(dynamic _param_recordsMode, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic recordsMode = XVar.Clone(_param_recordsMode);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			addSummary((XVar)(recordsMode), (XVar)(this._global), (XVar)(data), ref this._totalRecords);
			return null;
		}
		public virtual XVar setGroup(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic field = null, firstKey = null;
			field = XVar.Clone(this._sql.field(new XVar(0)));
			firstKey = XVar.Clone(field.getKey((XVar)(data)));
			if(firstKey != this._oldFirst)
			{
				this._nGroup++;
				this._oldFirst = XVar.Clone(firstKey);
			}
			return null;
		}
		public virtual XVar isVisibleGroup()
		{
			return (XVar)(this._from <= this._nGroup)  && (XVar)(this._nGroup < this._from + this._groupsTotal);
		}
		public virtual XVar getDisplayGroups(dynamic _param_from)
		{
			#region pass-by-value parameters
			dynamic from = XVar.Clone(_param_from);
			#endregion

			init((XVar)(from));
			if(XVar.Pack(!(XVar)(this._groupsTotal)))
			{
				return XVar.Array();
			}
			else
			{
				dynamic groups = XVar.Array();
				groups = XVar.Clone(XVar.Array());
				this._allGroupsUsed = new XVar(false);
				if(XVar.Pack(this.repGroupFieldsCount))
				{
					dynamic data = null, qResult = null, sql = null;
					sql = XVar.Clone(this._sql.sqlg());
					qResult = XVar.Clone(this._connection.query((XVar)(sql)));
					while(XVar.Pack(data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())))))
					{
						groups.InitAndSetArrayItem(this._sql.getGroup((XVar)(data)), null);
					}
					if(MVCFunctions.count(groups) < this._groupsTotal)
					{
						this._allGroupsUsed = new XVar(true);
					}
				}
				if(0 < this._sql._skipCount)
				{
					MVCFunctions.array_splice((XVar)(groups), new XVar(0), (XVar)(this._sql._skipCount));
					this._allGroupsUsed = new XVar(false);
				}
				if(XVar.Pack(0) < from)
				{
					this._allGroupsUsed = new XVar(false);
				}
				this._countGroups = XVar.Clone(MVCFunctions.count(groups));
				return groups;
			}
			return null;
		}
		public virtual XVar getCountGroups(dynamic _param_fullRequest = null)
		{
			#region default values
			if(_param_fullRequest as Object == null) _param_fullRequest = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic fullRequest = XVar.Clone(_param_fullRequest);
			#endregion

			if(XVar.Pack(this.repGroupFieldsCount))
			{
				if((XVar)(0 <= this._nGroup)  && (XVar)(fullRequest))
				{
					return this._nGroup + 1;
				}
				else
				{
					if(XVar.Pack(this._allGroupsUsed))
					{
						return this._countGroups;
					}
					else
					{
						dynamic data = XVar.Array(), fetchedArray = null, sql = null;
						sql = XVar.Clone(this._sql.sqlcg());
						fetchedArray = XVar.Clone(this._connection.query((XVar)(sql)).fetchAssoc());
						data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
						return data["c"];
					}
				}
			}
			else
			{
				return 0;
			}
			return null;
		}
		public virtual XVar getSummary()
		{
			return this._global;
		}
		public virtual XVar allGroupsUsed()
		{
			return this._allGroupsUsed;
		}
	}
	public partial class ReportLogic : Summarable
	{
		public dynamic _list;
		public dynamic _totalRecords;
		public dynamic _pages;
		public dynamic _groupsTotal;
		public dynamic _groupsPerPage;
		public dynamic _groupCounter = XVar.Pack(0);
		public dynamic _from = XVar.Pack(0);
		public dynamic _connection;
		public dynamic _sql;
		public dynamic _groups;
		public dynamic _groupKeys;
		public dynamic _fullRequest = XVar.Pack(false);
		public dynamic _recordBasedRequest = XVar.Pack(false);
		public dynamic _doPaging = XVar.Pack(false);
		public dynamic _lastPageNumber = XVar.Pack(0);
		public dynamic _pageSummary;
		public dynamic _printRecordCount = XVar.Pack(0);
		public dynamic _listedRows = XVar.Pack(0);
		public dynamic _oldLevels;
		public dynamic pageObject = XVar.Pack(null);
		public ProjectSettings pSet = null;
		protected static bool skipReportLogicCtor = false;
		public ReportLogic(dynamic _param_sql, dynamic _param_order, dynamic _param_connection, dynamic _param_groupsTotal, dynamic _param_groupsPerPage, dynamic var_params, dynamic _param_searchWhereClause, dynamic _param_searchHavingClause, dynamic _param_pageObject = null)
			:base((XVar)var_params)
		{
			if(skipReportLogicCtor)
			{
				skipReportLogicCtor = false;
				return;
			}
			#region default values
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic order = XVar.Clone(_param_order);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic groupsTotal = XVar.Clone(_param_groupsTotal);
			dynamic groupsPerPage = XVar.Clone(_param_groupsPerPage);
			dynamic searchWhereClause = XVar.Clone(_param_searchWhereClause);
			dynamic searchHavingClause = XVar.Clone(_param_searchHavingClause);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			this._connection = XVar.Clone(connection);
			this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.tName)));
			this._sql = XVar.Clone(new SQLStatement((XVar)(sql), (XVar)(order), (XVar)(groupsTotal), (XVar)(connection), (XVar)(var_params), (XVar)(searchWhereClause), (XVar)(searchHavingClause), (XVar)(this.cipherer), (XVar)(pageObject)));
			this._groups = XVar.Clone(new ReportGroups((XVar)(this._sql), (XVar)(connection), (XVar)(groupsTotal), (XVar)(var_params)));
			this._groupsTotal = XVar.Clone(groupsTotal);
			this._groupsPerPage = XVar.Clone(groupsPerPage);
			this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_REPORT)));
			if(XVar.Pack(pageObject == null))
			{
				this.pageObject = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), new XVar(Constants.PAGE_REPORT)));
			}
			else
			{
				this.pageObject = XVar.Clone(pageObject);
			}
			init();
		}
		public override XVar init(dynamic _param_from = null)
		{
			#region default values
			if(_param_from as Object == null) _param_from = new XVar(0);
			#endregion

			#region pass-by-value parameters
			dynamic from = XVar.Clone(_param_from);
			#endregion

			base.init((XVar)(from));
			this._sql._from = XVar.Clone(from);
			this._list = XVar.Clone(XVar.Array());
			this._totalRecords = new XVar(0);
			this._pages = XVar.Clone(XVar.Array());
			this._groupKeys = XVar.Clone(XVar.Array());
			this._lastPageNumber = new XVar(0);
			this._pageSummary = XVar.Clone(XVar.Array());
			this._listedRows = new XVar(0);
			this._oldLevels = XVar.Clone(XVar.Array());
			this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.tName)));
			return null;
		}
		public virtual XVar getPages()
		{
			return this._pages;
		}
		public virtual XVar getFormattedRow(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return null;
		}
		public override XVar writeGroup(dynamic begin, dynamic var_end, dynamic _param_gkey, dynamic _param_grp, dynamic _param_nField)
		{
			#region pass-by-value parameters
			dynamic gkey = XVar.Clone(_param_gkey);
			dynamic grp = XVar.Clone(_param_grp);
			dynamic nField = XVar.Clone(_param_nField);
			#endregion

			return null;
		}
		public virtual XVar _writePage(dynamic page, dynamic _param_src, dynamic _param_count)
		{
			#region pass-by-value parameters
			dynamic src = XVar.Clone(_param_src);
			dynamic count = XVar.Clone(_param_count);
			#endregion

			return null;
		}
		public virtual XVar writeGlobalSummary(dynamic _param_source)
		{
			#region pass-by-value parameters
			dynamic source = XVar.Clone(_param_source);
			#endregion

			return null;
		}
		public virtual XVar writePageSummary()
		{
			dynamic page = XVar.Array(), result = null;
			if(XVar.Pack(this._doPaging))
			{
				dynamic nCnt = null;
				nCnt = new XVar(0);
				for(;nCnt < MVCFunctions.count(this._list); nCnt++)
				{
					if(XVar.Pack(!(XVar)(this._pages.KeyExists(nCnt))))
					{
						this._pages.InitAndSetArrayItem(XVar.Array(), nCnt);
					}
					result = this._pages[nCnt];
					if(XVar.Pack(this._pageSummary.KeyExists(nCnt)))
					{
						page = XVar.Clone(this._pageSummary[nCnt]);
						_writePage((XVar)(result), (XVar)((XVar.Pack(page.KeyExists("summary")) ? XVar.Pack(page["summary"]) : XVar.Pack(XVar.Array()))), (XVar)((XVar.Pack(page.KeyExists("count")) ? XVar.Pack(page["count"]) : XVar.Pack(0))));
					}
					else
					{
						_writePage((XVar)(result), (XVar)(XVar.Array()), new XVar(0));
					}
				}
			}
			else
			{
				result = XVar.Clone(XVar.Array());
				page = XVar.Clone(this._summary);
				_writePage((XVar)(result), (XVar)((XVar.Pack(page.KeyExists("summary")) ? XVar.Pack(page["summary"]) : XVar.Pack(XVar.Array()))), (XVar)((XVar.Pack(page.KeyExists("count")) ? XVar.Pack(page["count"]) : XVar.Pack(0))));
				this._summary = XVar.Clone(result);
			}
			if((XVar)(0 == MVCFunctions.count(this._pages))  && (XVar)(0 < MVCFunctions.count(this._list)))
			{
				this._pages.InitAndSetArrayItem(this._summary, null);
			}
			return null;
		}
		public virtual XVar makeSummary()
		{
			_makeSummary((XVar)(this._summary), new XVar(0));
			return null;
		}
		public virtual XVar setSummary(dynamic _param_recordsMode, dynamic _param_data, dynamic _param_rowToAppend = null)
		{
			#region default values
			if(_param_rowToAppend as Object == null) _param_rowToAppend = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic recordsMode = XVar.Clone(_param_recordsMode);
			dynamic data = XVar.Clone(_param_data);
			dynamic rowToAppend = XVar.Clone(_param_rowToAppend);
			#endregion

			dynamic level = XVar.Array(), levels = XVar.Array(), setBegin = null;
			level = this._summary;
			setBegin = new XVar(false);
			if(XVar.Pack(this.repGroupFieldsCount))
			{
				dynamic field = null, groupIndex = null, groupKey = null, i = null, recordkeys = XVar.Array();
				recordkeys = XVar.Clone(XVar.Array());
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.repGroupFields); i++)
				{
					groupIndex = XVar.Clone(this.repGroupFields[i]["groupOrder"] - 1);
					field = XVar.Clone(this._sql.field((XVar)(groupIndex)));
					recordkeys.InitAndSetArrayItem(field.getKey((XVar)(data)), groupIndex);
				}
				if(0 < MVCFunctions.count(this._groupKeys))
				{
					dynamic changed = null, nKey = null;
					changed = new XVar(false);
					nKey = new XVar(0);
					for(;nKey < MVCFunctions.count(recordkeys); nKey++)
					{
						if(recordkeys[nKey] != this._groupKeys[nKey])
						{
							changed = new XVar(true);
							break;
						}
					}
					if(XVar.Pack(changed))
					{
						dynamic emptyRow = null, nKey2 = null;
						nKey2 = XVar.Clone(MVCFunctions.count(recordkeys) - 1);
						for(;nKey <= nKey2; nKey2--)
						{
							emptyRow = appendRow((XVar)(XVar.Array()));
							field = XVar.Clone(this._sql.field((XVar)(nKey2)));
							this._printRecordCount += field._rowsInSummary;
							this._listedRows++;
							this._oldLevels.InitAndSetArrayItem(emptyRow, nKey2, "_end");
						}
					}
					if(nKey == XVar.Pack(0))
					{
						++(this._groupCounter);
					}
				}
				this._groupKeys = XVar.Clone(recordkeys);
				levels = XVar.Clone(XVar.Array());
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.repGroupFields); i++)
				{
					groupIndex = XVar.Clone(this.repGroupFields[i]["groupOrder"] - 1);
					groupKey = XVar.Clone(recordkeys[groupIndex]);
					if(XVar.Pack(!(XVar)(level.KeyExists("values"))))
					{
						level.InitAndSetArrayItem(XVar.Array(), "values");
					}
					if(XVar.Pack(!(XVar)(level["values"].KeyExists(groupKey))))
					{
						level.InitAndSetArrayItem(XVar.Array(), "values", groupKey);
						level = level["values"][groupKey];
						field = XVar.Clone(this._sql.field((XVar)(groupIndex)));
						this._printRecordCount += field._rowsInHeader;
						setBegin = new XVar(true);
						level.InitAndSetArrayItem(data, "_first");
					}
					else
					{
						level = level["values"][groupKey];
					}
					levels.InitAndSetArrayItem(level, null);
				}
				addSummary((XVar)(recordsMode), (XVar)(level), (XVar)(data), ref this._totalRecords);
				this._oldLevels = levels;
			}
			else
			{
				addSummary((XVar)(recordsMode), (XVar)(level), (XVar)(data), ref this._totalRecords);
				++(this._groupCounter);
			}
			if(XVar.Pack(rowToAppend))
			{
				dynamic added = null;
				added = appendRow((XVar)(rowToAppend));
				this._printRecordCount++;
				this._listedRows++;
				if((XVar)(setBegin)  && (XVar)(this.repGroupFieldsCount))
				{
					dynamic nCnt = null;
					nCnt = new XVar(0);
					for(;nCnt < MVCFunctions.count(levels); nCnt++)
					{
						if(XVar.Pack(!(XVar)(levels[nCnt].KeyExists("_begin"))))
						{
							levels.InitAndSetArrayItem(added, nCnt, "_begin");
						}
					}
				}
			}
			if(XVar.Pack(this.repPageSummary))
			{
				if((XVar)(this._doPaging)  && (XVar)(rowToAppend))
				{
					dynamic nPage = null, summaryCount = null;
					nPage = XVar.Clone(MVCFunctions.count(this._list) - 1);
					if(XVar.Pack(!(XVar)(this._pageSummary.KeyExists(nPage))))
					{
						this._pageSummary.InitAndSetArrayItem(0, nPage, "count");
					}
					summaryCount = XVar.Clone(this._pageSummary[nPage]["count"]);
					addSummary((XVar)(recordsMode), (XVar)(this._pageSummary[nPage]), (XVar)(data), ref summaryCount);
					this._pageSummary.InitAndSetArrayItem(summaryCount, nPage, "count");
				}
			}
			return null;
		}
		public virtual XVar setFinish()
		{
			if(0 < MVCFunctions.count(this._groupKeys))
			{
				dynamic emptyRow = null, field = null, nKey = null;
				nKey = XVar.Clone(MVCFunctions.count(this._groupKeys) - 1);
				for(;XVar.Pack(0) <= nKey; nKey--)
				{
					field = XVar.Clone(this._sql.field((XVar)(nKey)));
					this._printRecordCount += field._rowsInSummary;
					emptyRow = appendRow((XVar)(XVar.Array()));
					this._listedRows++;
					this._oldLevels.InitAndSetArrayItem(emptyRow, nKey, "_end");
				}
			}
			return null;
		}
		public virtual XVar appendRow(dynamic _param_row)
		{
			#region pass-by-value parameters
			dynamic row = XVar.Clone(_param_row);
			#endregion

			if(XVar.Pack(this._groupsPerPage))
			{
				dynamic page = null;
				if(XVar.Pack(!(XVar)(this.repGroupFieldsCount)))
				{
					page = XVar.Clone(MVCFunctions.intval((XVar)((this._groupCounter - 1) / this._groupsPerPage)));
				}
				else
				{
					page = XVar.Clone(MVCFunctions.intval((XVar)(this._groupCounter / this._groupsPerPage)));
				}
				if((XVar)(XVar.Pack(0) < page)  && (XVar)(!(XVar)(this._list.KeyExists(page - 1))))
				{
					MVCFunctions.ob_flush();
					HttpContext.Current.Response.End();
					throw new RunnerInlineOutputException();
				}
				this._list.InitAndSetArrayItem(row, page, null);
				return this._list[page][MVCFunctions.count(this._list[page]) - 1];
			}
			else
			{
				this._list.InitAndSetArrayItem(row, null);
				return this._list[MVCFunctions.count(this._list) - 1];
			}
			return null;
		}
		public virtual XVar recordVisible(dynamic _param_nRecord)
		{
			#region pass-by-value parameters
			dynamic nRecord = XVar.Clone(_param_nRecord);
			#endregion

			return (XVar)((XVar)((XVar)(this._sql._limitLevel == 1)  || (XVar)(this._groupsTotal == 0))  || (XVar)((XVar)(this._sql._limitLevel == 2)  && (XVar)((XVar)(0 <= nRecord - this._sql._skipCount)  && (XVar)(nRecord - this._sql._skipCount < this._groupsTotal))))  || (XVar)((XVar)(this._sql._limitLevel == 0)  && (XVar)((XVar)(0 <= nRecord - this._from)  && (XVar)(nRecord - this._sql._skipCount < this._from + this._groupsTotal)));
		}
		public virtual XVar getTotals()
		{
			if(XVar.Pack(this._fullRequest))
			{
				return this._groups.getSummary();
			}
			else
			{
				if(XVar.Pack(this._groups.allGroupsUsed()))
				{
					return this._summary;
				}
				else
				{
					dynamic sql = null, totals = null;
					totals = XVar.Clone(XVar.Array());
					sql = XVar.Clone(this._sql.sqlt());
					if(!XVar.Equals(XVar.Pack(sql), XVar.Pack(false)))
					{
						dynamic data = XVar.Array(), fetchedArray = null, totalRecords = null;
						totalRecords = new XVar(0);
						fetchedArray = XVar.Clone(this._connection.query((XVar)(sql)).fetchAssoc());
						data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
						data.InitAndSetArrayItem(this.pageObject.limitRowCount((XVar)(data["countField"])), "countField");
						addSummary(new XVar(false), (XVar)(totals), (XVar)(data), ref totalRecords);
					}
					return totals;
				}
			}
			return null;
		}
		public virtual XVar getReport(dynamic _param_from = null)
		{
			#region default values
			if(_param_from as Object == null) _param_from = new XVar(0);
			#endregion

			#region pass-by-value parameters
			dynamic from = XVar.Clone(_param_from);
			#endregion

			dynamic countGroups = null, countrows = null, data = null, global_totals = XVar.Array(), globals = null, hsql = XVar.Array(), i = null, isExistTimeFormatField = null, maxpages = null, nRow = null, nRowVisible = null, page = null, qResult = null, returnthis = null, sql = null;
			init((XVar)(from));
			this._doPaging = XVar.Clone(this._groupsPerPage != 0);
			isExistTimeFormatField = new XVar(false);
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.fieldsArr); i++)
			{
				if(this.fieldsArr[i]["viewFormat"] == "Time")
				{
					isExistTimeFormatField = new XVar(true);
					break;
				}
			}
			this._fullRequest = XVar.Clone((XVar)(this.repGlobalSummary)  && (XVar)(isExistTimeFormatField));
			if(XVar.Pack(!(XVar)(this._connection.checkDBSubqueriesSupport())))
			{
				this._fullRequest = new XVar(true);
			}
			if((XVar)((XVar)((XVar)((XVar)(this._connection.dbType != Constants.nDATABASE_MySQL)  && (XVar)(this._connection.dbType != Constants.nDATABASE_PostgreSQL))  && (XVar)(this._connection.dbType != Constants.nDATABASE_MSSQLServer))  && (XVar)(this._connection.dbType != Constants.nDATABASE_Oracle))  && (XVar)(this._connection.dbType != Constants.nDATABASE_Access))
			{
				this._fullRequest = new XVar(true);
			}
			this._recordBasedRequest = XVar.Clone(this._fullRequest);
			if(XVar.Pack(!(XVar)(this.repGroupFieldsCount)))
			{
				this._recordBasedRequest = new XVar(true);
			}
			this._sql.setRecordBasedRequest((XVar)(this._recordBasedRequest));
			if(XVar.Pack(this._fullRequest))
			{
				this._sql._limitLevel = new XVar(0);
			}
			else
			{
				if(XVar.Pack(!(XVar)(this.repGroupFieldsCount)))
				{
					this._sql._limitLevel = new XVar(2);
				}
				else
				{
					this._sql._limitLevel = new XVar(1);
				}
			}
			page = XVar.Clone(-1);
			nRow = new XVar(0);
			nRowVisible = new XVar(0);
			if(XVar.Pack(!(XVar)(this._recordBasedRequest)))
			{
				dynamic groups = null;
				groups = XVar.Clone(this._groups.getDisplayGroups((XVar)(from)));
				hsql = XVar.Clone(this._sql.sql2((XVar)(groups)));
				if(XVar.Pack(this.pageObject))
				{
					dynamic hwhere = null;
					if(this.pageObject.pageType == Constants.PAGE_REPORT)
					{
						if(XVar.Pack(this.pageObject.eventsObject.exists(new XVar("BeforeQueryReport"))))
						{
							hwhere = XVar.Clone(hsql["where"]);
							this.pageObject.eventsObject.BeforeQueryReport((XVar)(hwhere));
							hsql.InitAndSetArrayItem(hwhere, "where");
						}
					}
					else
					{
						if(XVar.Pack(this.pageObject.eventsObject.exists(new XVar("BeforeQueryReportPrint"))))
						{
							hwhere = XVar.Clone(hsql["where"]);
							this.pageObject.eventsObject.BeforeQueryReportPrint((XVar)(hwhere));
							hsql.InitAndSetArrayItem(hwhere, "where");
						}
					}
				}
				sql = XVar.Clone(this._sql.buildsql((XVar)(hsql)));
				qResult = XVar.Clone(this._connection.query((XVar)(sql)));
				while(XVar.Pack(data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())))))
				{
					this.pageObject.recId = XVar.Clone(nRow);
					setSummary((XVar)(this.repShowDet), (XVar)(data), (XVar)((XVar.Pack(recordVisible((XVar)(nRow))) ? XVar.Pack(getFormattedRow((XVar)(data))) : XVar.Pack(null))));
					nRow++;
				}
			}
			else
			{
				dynamic visible = null;
				this._groups.init((XVar)(from));
				this._sql.setOldAlgorithm();
				hsql = XVar.Clone(this._sql.sql2(new XVar(null)));
				sql = XVar.Clone(this._sql.buildsql((XVar)(hsql)));
				qResult = XVar.Clone(this._connection.query((XVar)(sql)));
				while(XVar.Pack(data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(qResult.fetchAssoc())))))
				{
					if(XVar.Pack(this.repGroupFieldsCount))
					{
						this._groups.setGroup((XVar)(data));
					}
					if(XVar.Pack(this._fullRequest))
					{
						this._groups.setGlobalSummary(new XVar(true), (XVar)(data));
					}
					if(XVar.Pack(this.repGroupFieldsCount))
					{
						visible = XVar.Clone((XVar)(this._groups.isVisibleGroup())  || (XVar)(this._groupsTotal == 0));
					}
					else
					{
						visible = XVar.Clone(recordVisible((XVar)(nRow)));
					}
					if(XVar.Pack(visible))
					{
						nRowVisible++;
						this.pageObject.recId = XVar.Clone(nRow);
						setSummary(new XVar(true), (XVar)(data), (XVar)(getFormattedRow((XVar)(data))));
					}
					else
					{
						if((XVar)(!(XVar)(this._fullRequest))  && (XVar)(0 < MVCFunctions.count(this._list)))
						{
							break;
						}
					}
					nRow++;
					if((XVar)((XVar)(!(XVar)(this.repGroupFieldsCount))  && (XVar)(this.pSet.getRecordsLimit()))  && (XVar)(this.pSet.getRecordsLimit() <= from + nRowVisible))
					{
						break;
					}
				}
				this._sql.setOldAlgorithm(new XVar(false));
			}
			setFinish();
			makeSummary();
			global_totals = XVar.Clone(getTotals());
			writePageSummary();
			globals = XVar.Clone(writeGlobalSummary((XVar)(global_totals)));
			if(XVar.Pack(this.repGroupFieldsCount))
			{
				countrows = XVar.Clone(this._groups.getCountGroups((XVar)(this._fullRequest)));
				countGroups = XVar.Clone(countrows);
			}
			else
			{
				countrows = XVar.Clone(global_totals["count"]);
				countGroups = new XVar(1);
			}
			maxpages = new XVar(1);
			if(0 < this._groupsTotal)
			{
				maxpages = XVar.Clone((XVar)Math.Ceiling((double)(countrows / this._groupsTotal)));
			}
			returnthis = XVar.Clone(new XVar("list", this._list, "global", globals, "page", this._summary, "maxpages", maxpages, "countRows", countrows, "countGroups", countGroups));
			return returnthis;
		}
	}
	public partial class Report : ReportLogic
	{
		public dynamic forExport = XVar.Pack(false);
		public dynamic mode = XVar.Pack(Constants.MODE_LIST);
		protected static bool skipReportCtor = false;
		public Report(dynamic _param_sql, dynamic _param_order, dynamic _param_connection, dynamic _param_groupsTotal, dynamic _param_groupsPerPage, dynamic var_params, dynamic _param_searchWhereClause, dynamic _param_searchHavingClause, dynamic _param_pageObject = null)
			:base((XVar)_param_sql, (XVar)_param_order, (XVar)_param_connection, (XVar)_param_groupsTotal, (XVar)_param_groupsPerPage, (XVar)var_params, (XVar)_param_searchWhereClause, (XVar)_param_searchHavingClause, (XVar)_param_pageObject)
		{
			if(skipReportCtor)
			{
				skipReportCtor = false;
				return;
			}
			#region default values
			if(_param_pageObject as Object == null) _param_pageObject = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic order = XVar.Clone(_param_order);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic groupsTotal = XVar.Clone(_param_groupsTotal);
			dynamic groupsPerPage = XVar.Clone(_param_groupsPerPage);
			dynamic searchWhereClause = XVar.Clone(_param_searchWhereClause);
			dynamic searchHavingClause = XVar.Clone(_param_searchHavingClause);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

		}
		public override XVar getFormattedRow(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic i = null, j = null, keylink = null, pass = null, row = XVar.Array();
			row = XVar.Clone(new XVar("row_data", true));
			keylink = new XVar("");
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.tKeyFields); i++)
			{
				keylink = MVCFunctions.Concat(keylink, "&key", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(value[this.tKeyFields[i]])))));
			}
			if(XVar.Pack(this.forExport))
			{
				this.pageObject.setForExportVar((XVar)(this.forExport));
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.fieldsArr); i++)
			{
				pass = new XVar(false);
				j = new XVar(0);
				for(;j < MVCFunctions.count(this.repGroupFields); j++)
				{
					if((XVar)(!(XVar)(this.fieldsArr[i]["repPage"]))  || (XVar)(!(XVar)((XVar)(this.repShowDet)  || (XVar)((XVar)(this.repGroupFields[j]["strGroupField"] == this.fieldsArr[i]["name"])  && (XVar)(XVar.Equals(XVar.Pack(this.repGroupFields[j]["groupInterval"]), XVar.Pack(0)))))))
					{
						pass = new XVar(true);
					}
				}
				if(XVar.Pack(pass))
				{
					continue;
				}
				row.InitAndSetArrayItem(this.pageObject.formatReportFieldValue((XVar)(this.fieldsArr[i]["name"]), (XVar)(value), (XVar)(keylink)), MVCFunctions.Concat(this.fieldsArr[i]["goodName"], "_value"));
				row.InitAndSetArrayItem(value[this.fieldsArr[i]["name"]], MVCFunctions.Concat(this.fieldsArr[i]["goodName"], "_dbvalue"));
			}
			if(this.repLayout == Constants.REPORT_BLOCK)
			{
				row.InitAndSetArrayItem(true, MVCFunctions.GoodFieldName(new XVar("nonewgroup")));
			}
			return row;
		}
		public override XVar writeGroup(dynamic begin, dynamic var_end, dynamic _param_gkey, dynamic _param_grp, dynamic _param_nField)
		{
			#region pass-by-value parameters
			dynamic gkey = XVar.Clone(_param_gkey);
			dynamic grp = XVar.Clone(_param_grp);
			dynamic nField = XVar.Clone(_param_nField);
			#endregion

			dynamic field = null, gname = null, i = null;
			field = XVar.Clone(this._sql.field((XVar)(nField)));
			gname = XVar.Clone(field.name());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.repGroupFields); i++)
			{
				if(gname == this.repGroupFields[i]["strGroupField"])
				{
					dynamic j = null;
					if(this.repLayout == Constants.REPORT_BLOCK)
					{
						dynamic bFound = null, gname2 = null, nG = null;
						bFound = new XVar(false);
						nG = new XVar(0);
						for(;nG < this.repGroupFieldsCount; nG++)
						{
							field = XVar.Clone(this._sql.field((XVar)(nG)));
							gname2 = XVar.Clone(field.name());
							if(nG < nField)
							{
								if(XVar.Pack(begin.KeyExists(MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname2, "_firstnewgroup"))))))
								{
									bFound = new XVar(true);
								}
							}
							else
							{
								begin.Remove(MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname2, "_firstnewgroup"))));
							}
						}
						if(XVar.Pack(!(XVar)(bFound)))
						{
							begin.InitAndSetArrayItem(true, MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname, "_firstnewgroup"))));
						}
						begin.Remove(MVCFunctions.GoodFieldName(new XVar("nonewgroup")));
					}
					else
					{
						begin.InitAndSetArrayItem(true, MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname, "_newgroup"))));
					}
					var_end.InitAndSetArrayItem(true, MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname, "_endgroup"))));
					if(XVar.Pack(this.repGroupFields[i]["showGroupSummary"]))
					{
						var_end.InitAndSetArrayItem(CommonFunctions.str_format_number((XVar)(grp["count"]), new XVar(0)), MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat("group", gname, "_total_cnt"))));
					}
					j = new XVar(0);
					for(;j < MVCFunctions.count(this.fieldsArr); j++)
					{
						if(XVar.Pack(MVCFunctions.is_array((XVar)(grp["summary"]))))
						{
							if(XVar.Pack(MVCFunctions.is_array((XVar)(grp["summary"][this.fieldsArr[j]["name"]]))))
							{
								if(XVar.Pack(this.fieldsArr[j]["totalMax"]))
								{
									var_end.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(grp["summary"][this.fieldsArr[j]["name"]]["MAX"]), (XVar)(this.fieldsArr[j]["name"]), (XVar)(this.fieldsArr[j]["viewFormat"]), (XVar)(this.fieldsArr[j]["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("group", MVCFunctions.GoodFieldName((XVar)(gname)), "_total", this.fieldsArr[j]["goodName"], "_max"));
								}
								if(XVar.Pack(this.fieldsArr[j]["totalMin"]))
								{
									var_end.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(grp["summary"][this.fieldsArr[j]["name"]]["MIN"]), (XVar)(this.fieldsArr[j]["name"]), (XVar)(this.fieldsArr[j]["viewFormat"]), (XVar)(this.fieldsArr[j]["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("group", MVCFunctions.GoodFieldName((XVar)(gname)), "_total", this.fieldsArr[j]["goodName"], "_min"));
								}
								if(XVar.Pack(this.fieldsArr[j]["totalAvg"]))
								{
									var_end.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(grp["summary"][this.fieldsArr[j]["name"]]["AVG"]), (XVar)(this.fieldsArr[j]["name"]), (XVar)(this.fieldsArr[j]["viewFormat"]), (XVar)(this.fieldsArr[j]["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("group", MVCFunctions.GoodFieldName((XVar)(gname)), "_total", this.fieldsArr[j]["goodName"], "_avg"));
								}
								if(XVar.Pack(this.fieldsArr[j]["totalSum"]))
								{
									var_end.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(grp["summary"][this.fieldsArr[j]["name"]]["SUM"]), (XVar)(this.fieldsArr[j]["name"]), (XVar)(this.fieldsArr[j]["viewFormat"]), (XVar)(this.fieldsArr[j]["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("group", MVCFunctions.GoodFieldName((XVar)(gname)), "_total", this.fieldsArr[j]["goodName"], "_sum"));
								}
							}
						}
						if(this.fieldsArr[j]["name"] == this.repGroupFields[i]["strGroupField"])
						{
							dynamic gvalue = null;
							field = XVar.Clone(this._sql.field((XVar)(nField)));
							gvalue = XVar.Clone(field.getFieldName((XVar)(gkey), (XVar)(grp["_first"]), (XVar)(this.pageObject)));
							if(XVar.Pack(field.overrideFormat()))
							{
								begin.InitAndSetArrayItem((XVar.Pack(this.forExport == "excel") ? XVar.Pack(MVCFunctions.runner_htmlspecialchars((XVar)(gvalue))) : XVar.Pack(gvalue)), MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(gname)), "_grval"))));
								if(XVar.Pack(this.showGroupSummaryCount))
								{
									var_end.InitAndSetArrayItem((XVar.Pack(this.forExport == "excel") ? XVar.Pack(MVCFunctions.runner_htmlspecialchars((XVar)(gvalue))) : XVar.Pack(gvalue)), MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(gname)), "_grval"))));
								}
							}
							else
							{
								dynamic formattedValue = null;
								formattedValue = XVar.Clone(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(gvalue), (XVar)(this.fieldsArr[j]["name"]), (XVar)(this.fieldsArr[j]["viewFormat"]), (XVar)(this.fieldsArr[j]["editFormat"]), (XVar)(this.mode)));
								begin.InitAndSetArrayItem((XVar.Pack(this.forExport == "excel") ? XVar.Pack(MVCFunctions.runner_htmlspecialchars((XVar)(formattedValue))) : XVar.Pack(formattedValue)), MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname, "_grval"))));
								if(XVar.Pack(this.showGroupSummaryCount))
								{
									var_end.InitAndSetArrayItem((XVar.Pack(this.forExport == "excel") ? XVar.Pack(MVCFunctions.runner_htmlspecialchars((XVar)(formattedValue))) : XVar.Pack(formattedValue)), MVCFunctions.GoodFieldName((XVar)(MVCFunctions.Concat(gname, "_grval"))));
								}
							}
						}
					}
				}
			}
			return null;
		}
		public override XVar _writePage(dynamic page, dynamic _param_src, dynamic _param_count)
		{
			#region pass-by-value parameters
			dynamic src = XVar.Clone(_param_src);
			dynamic count = XVar.Clone(_param_count);
			#endregion

			page.InitAndSetArrayItem(true, "page_summary");
			if(XVar.Pack(this.repPageSummary))
			{
				dynamic fGoodName = null, field = XVar.Array(), fieldName = null, i = null;
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.fieldsArr); i++)
				{
					field = this.fieldsArr[i];
					fieldName = XVar.Clone(field["name"]);
					fGoodName = XVar.Clone(field["goodName"]);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(src[fieldName]))))
					{
						if(XVar.Pack(field["totalSum"]))
						{
							page.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(src[fieldName]["SUM"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("page_total", fGoodName, "_sum"));
						}
						if(XVar.Pack(field["totalAvg"]))
						{
							page.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(src[fieldName]["AVG"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("page_total", fGoodName, "_avg"));
						}
						if(XVar.Pack(field["totalMin"]))
						{
							page.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(src[fieldName]["MIN"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("page_total", fGoodName, "_min"));
						}
						if(XVar.Pack(field["totalMax"]))
						{
							page.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(src[fieldName]["MAX"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("page_total", fGoodName, "_max"));
						}
					}
				}
				page.InitAndSetArrayItem(CommonFunctions.str_format_number((XVar)(count), new XVar(0)), "page_total_cnt");
			}
			return null;
		}
		public override XVar writeGlobalSummary(dynamic _param_source)
		{
			#region pass-by-value parameters
			dynamic source = XVar.Clone(_param_source);
			#endregion

			dynamic result = XVar.Array();
			result = XVar.Clone(XVar.Array());
			if(XVar.Pack(!(XVar)(this.repGlobalSummary)))
			{
				return result;
			}
			if(XVar.Pack(MVCFunctions.is_array((XVar)(source["summary"]))))
			{
				dynamic fGoodName = null, field = XVar.Array(), fieldName = null, i = null;
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.fieldsArr); i++)
				{
					field = this.fieldsArr[i];
					fieldName = XVar.Clone(field["name"]);
					fGoodName = XVar.Clone(field["goodName"]);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(source["summary"][fieldName]))))
					{
						if(XVar.Pack(field["totalMax"]))
						{
							result.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(source["summary"][fieldName]["MAX"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("global_total", fGoodName, "_max"));
						}
						if(XVar.Pack(field["totalMin"]))
						{
							result.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(source["summary"][fieldName]["MIN"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("global_total", fGoodName, "_min"));
						}
						if(XVar.Pack(field["totalAvg"]))
						{
							result.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(source["summary"][fieldName]["AVG"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("global_total", fGoodName, "_avg"));
						}
						if(XVar.Pack(field["totalSum"]))
						{
							result.InitAndSetArrayItem(CommonFunctions.getFormattedValue((XVar)(this.pageObject), (XVar)(source["summary"][fieldName]["SUM"]), (XVar)(fieldName), (XVar)(field["viewFormat"]), (XVar)(field["editFormat"]), (XVar)(this.mode)), MVCFunctions.Concat("global_total", fGoodName, "_sum"));
						}
					}
				}
			}
			result.InitAndSetArrayItem(CommonFunctions.str_format_number((XVar)(source["count"]), new XVar(0)), "global_total_cnt");
			return result;
		}
	}
	// Included file globals
	public partial class CommonFunctions
	{

		public static XVar create_reportfield(dynamic _param_name, dynamic _param_type, dynamic _param_interval, dynamic _param_alias, dynamic _param_table, dynamic _param_connection, dynamic _param_cipherer)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			dynamic var_type = XVar.Clone(_param_type);
			dynamic interval = XVar.Clone(_param_interval);
			dynamic alias = XVar.Clone(_param_alias);
			dynamic table = XVar.Clone(_param_table);
			dynamic connection = XVar.Clone(_param_connection);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			if(XVar.Pack(!(XVar)(var_type)))
			{
				return null;
			}
			if(var_type == "char")
			{
				return new ReportCharField((XVar)(name), (XVar)(interval), (XVar)(alias), (XVar)(table), (XVar)(connection), (XVar)(cipherer));
			}
			if(var_type == "date")
			{
				return new ReportDateField((XVar)(name), (XVar)(interval), (XVar)(alias), (XVar)(table), (XVar)(connection), (XVar)(cipherer));
			}
			if(var_type == "numeric")
			{
				return new ReportNumericField((XVar)(name), (XVar)(interval), (XVar)(alias), (XVar)(table), (XVar)(connection), (XVar)(cipherer));
			}
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public static XVar getFormattedValue(dynamic _param_pageObject, dynamic _param_value, dynamic _param_fieldName, dynamic _param_strViewFormat, dynamic _param_strEditFormat = null, dynamic _param_mode = null)
		{
			#region default values
			if(_param_strEditFormat as Object == null) _param_strEditFormat = new XVar("");
			if(_param_mode as Object == null) _param_mode = new XVar(Constants.MODE_LIST);
			#endregion

			#region pass-by-value parameters
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic value = XVar.Clone(_param_value);
			dynamic fieldName = XVar.Clone(_param_fieldName);
			dynamic strViewFormat = XVar.Clone(_param_strViewFormat);
			dynamic strEditFormat = XVar.Clone(_param_strEditFormat);
			dynamic mode = XVar.Clone(_param_mode);
			#endregion

			dynamic val = null;
			if((XVar)(strViewFormat == Constants.FORMAT_TIME)  && (XVar)(MVCFunctions.IsNumeric(value)))
			{
				dynamic d = null, h = null, m = null, s = null;
				val = new XVar("");
				d = XVar.Clone(MVCFunctions.intval((XVar)(value / 86400)));
				h = XVar.Clone(MVCFunctions.intval((XVar)((value  %  86400) / 3600)));
				m = XVar.Clone(MVCFunctions.intval((XVar)(((value  %  86400)  %  3600) / 60)));
				s = XVar.Clone(((value  %  86400)  %  3600)  %  60);
				val = MVCFunctions.Concat(val, (XVar.Pack(XVar.Pack(0) < d) ? XVar.Pack(MVCFunctions.Concat(d, "d ")) : XVar.Pack("")));
				val = MVCFunctions.Concat(val, CommonFunctions.str_format_time((XVar)(new XVar(0, 0, 1, 0, 2, 0, 3, h, 4, m, 5, s))));
			}
			else
			{
				dynamic arrValue = null;
				arrValue = XVar.Clone(new XVar(fieldName, value));
				val = XVar.Clone(pageObject.formatReportFieldValue((XVar)(fieldName), (XVar)(arrValue)));
			}
			return val;
		}
		public static XVar cached_db2time(dynamic _param_strtime)
		{
			#region pass-by-value parameters
			dynamic strtime = XVar.Clone(_param_strtime);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.cache_db2time.KeyExists(strtime))))
			{
				dynamic res = null;
				res = XVar.Clone(CommonFunctions.db2time((XVar)(strtime)));
				GlobalVars.cache_db2time.InitAndSetArrayItem(res, strtime);
				return res;
			}
			else
			{
				return GlobalVars.cache_db2time[strtime];
			}
			return null;
		}
		public static XVar cached_getdayofweek(dynamic _param_strtime)
		{
			#region pass-by-value parameters
			dynamic strtime = XVar.Clone(_param_strtime);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.cache_getdayofweek.KeyExists(strtime))))
			{
				dynamic date = null, res = null;
				date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strtime)));
				res = XVar.Clone(CommonFunctions.getdayofweek((XVar)(date)));
				GlobalVars.cache_getdayofweek.InitAndSetArrayItem(res, strtime);
				return res;
			}
			else
			{
				return GlobalVars.cache_getdayofweek[strtime];
			}
			return null;
		}
		public static XVar cached_getweekstart(dynamic _param_strtime)
		{
			#region pass-by-value parameters
			dynamic strtime = XVar.Clone(_param_strtime);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.cache_getweekstart.KeyExists(strtime))))
			{
				dynamic date = null, res = null;
				date = XVar.Clone(CommonFunctions.cached_db2time((XVar)(strtime)));
				res = XVar.Clone(CommonFunctions.getweekstart((XVar)(date)));
				GlobalVars.cache_getweekstart.InitAndSetArrayItem(res, strtime);
				return res;
			}
			else
			{
				return GlobalVars.cache_getweekstart[strtime];
			}
			return null;
		}
		public static XVar cached_formatweekstart(dynamic _param_strtime)
		{
			#region pass-by-value parameters
			dynamic strtime = XVar.Clone(_param_strtime);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.cache_formatweekstart.KeyExists(strtime))))
			{
				dynamic res = null, start = null, var_end = null;
				start = XVar.Clone(CommonFunctions.cached_getweekstart((XVar)(strtime)));
				var_end = XVar.Clone(CommonFunctions.adddays((XVar)(start), new XVar(6)));
				res = XVar.Clone(MVCFunctions.Concat(CommonFunctions.format_shortdate((XVar)(start)), " - ", CommonFunctions.format_shortdate((XVar)(var_end))));
				GlobalVars.cache_formatweekstart.InitAndSetArrayItem(res, strtime);
				return res;
			}
			else
			{
				return GlobalVars.cache_formatweekstart[strtime];
			}
			return null;
		}
		public static XVar cached_ffn(dynamic _param_field, dynamic _param_forGroupedField = null)
		{
			#region default values
			if(_param_forGroupedField as Object == null) _param_forGroupedField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic forGroupedField = XVar.Clone(_param_forGroupedField);
			#endregion

			if(XVar.Pack(!(XVar)(GlobalVars.cache_fullfieldname.KeyExists(field))))
			{
				dynamic res = null;
				if((XVar)(!(XVar)(GlobalVars.wr_is_standalone))  && (XVar)(!(XVar)(forGroupedField)))
				{
					res = XVar.Clone(CommonFunctions.GetFullFieldName((XVar)(field), (XVar)(GlobalVars.strTableName), new XVar(false)));
				}
				else
				{
					dynamic connection = null;
					connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
					res = XVar.Clone(connection.addFieldWrappers((XVar)(field)));
				}
				GlobalVars.cache_fullfieldname.InitAndSetArrayItem(res, field);
				return res;
			}
			else
			{
				return GlobalVars.cache_fullfieldname[field];
			}
			return null;
		}
	}
}
