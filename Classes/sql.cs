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
	public partial class SQLEntity : XClass
	{
		protected dynamic connection = XVar.Pack(null);
		public SQLEntity()
		{
		}
		public virtual XVar IsAggregationFunctionCall()
		{
			return false;
		}
		public virtual XVar IsBinary()
		{
			return false;
		}
		public virtual XVar IsAsterisk()
		{
			return false;
		}
		public virtual XVar SetQuery(dynamic query)
		{
			return null;
		}
		public virtual XVar IsSQLField()
		{
			return false;
		}
		protected virtual XVar setConnection(dynamic _param_srcTableName)
		{
			#region pass-by-value parameters
			dynamic srcTableName = XVar.Clone(_param_srcTableName);
			#endregion

			this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(srcTableName)));
			return null;
		}
	}
	public partial class SQLNonParsed : SQLEntity
	{
		public dynamic m_sql;
		protected static bool skipSQLNonParsedCtor = false;
		public SQLNonParsed(dynamic _param_proto)
		{
			if(skipSQLNonParsedCtor)
			{
				skipSQLNonParsedCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_sql = XVar.Clone((XVar.Pack(proto.KeyExists("m_sql")) ? XVar.Pack(proto["m_sql"]) : XVar.Pack("")));
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			return this.m_sql;
		}
		public override XVar IsAsterisk()
		{
			dynamic last = null;
			last = XVar.Clone(MVCFunctions.substr((XVar)(this.m_sql), (XVar)(MVCFunctions.strlen((XVar)(this.m_sql)) - 1)));
			return last == "*";
		}
	}
	public partial class SQLField : SQLEntity
	{
		public dynamic m_strName;
		public dynamic m_strTable;
		protected dynamic m_srcTableName;
		protected static bool skipSQLFieldCtor = false;
		public SQLField(dynamic _param_proto)
		{
			if(skipSQLFieldCtor)
			{
				skipSQLFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_strName = XVar.Clone((XVar.Pack(proto.KeyExists("m_strName")) ? XVar.Pack(proto["m_strName"]) : XVar.Pack(null)));
			this.m_strTable = XVar.Clone((XVar.Pack(proto.KeyExists("m_strTable")) ? XVar.Pack(proto["m_strTable"]) : XVar.Pack(null)));
			this.m_srcTableName = XVar.Clone(proto["m_srcTableName"]);
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			dynamic fieldName = null;
			if(XVar.Pack(this.connection == null))
			{
				setConnection((XVar)(this.m_srcTableName));
			}
			if(query.cipherer != null)
			{
				return query.cipherer.GetFieldName((XVar)(MVCFunctions.Concat((XVar.Pack((XVar)(this.m_strTable != "")  && (XVar)(1 < query.TableCount())) ? XVar.Pack(MVCFunctions.Concat(this.connection.addTableWrappers((XVar)(this.m_strTable)), ".")) : XVar.Pack("")), this.connection.addFieldWrappers((XVar)(this.m_strName)))));
			}
			fieldName = XVar.Clone(this.connection.addFieldWrappers((XVar)(this.m_strName)));
			if((XVar)(this.m_strTable == "")  || (XVar)(query.TableCount() == 1))
			{
				return fieldName;
			}
			return MVCFunctions.Concat(this.connection.addTableWrappers((XVar)(this.m_strTable)), ".", fieldName);
		}
		public virtual XVar GetType()
		{
			ProjectSettings pSet;
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.m_strTable)));
			return pSet.getFieldType((XVar)(this.m_strName));
		}
		public override XVar IsBinary()
		{
			return CommonFunctions.IsBinaryType((XVar)(GetType()));
		}
		public override XVar IsSQLField()
		{
			return true;
		}
	}
	public partial class SQLFieldListItem : SQLEntity
	{
		public dynamic m_sql;
		public dynamic m_expr;
		public dynamic m_alias;
		public dynamic m_isEncrypted = XVar.Pack(false);
		protected dynamic m_srcTableName;
		protected static bool skipSQLFieldListItemCtor = false;
		public SQLFieldListItem(dynamic _param_proto)
		{
			if(skipSQLFieldListItemCtor)
			{
				skipSQLFieldListItemCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_expr = XVar.Clone((XVar.Pack(proto.KeyExists("m_expr")) ? XVar.Pack(proto["m_expr"]) : XVar.Pack(null)));
			this.m_alias = XVar.Clone((XVar.Pack(proto.KeyExists("m_alias")) ? XVar.Pack(proto["m_alias"]) : XVar.Pack(null)));
			this.m_sql = XVar.Clone((XVar.Pack(proto.KeyExists("m_sql")) ? XVar.Pack(proto["m_sql"]) : XVar.Pack(null)));
			this.m_srcTableName = XVar.Clone(proto["m_srcTableName"]);
		}
		public virtual XVar toSql(dynamic _param_query, dynamic _param_addAlias = null)
		{
			#region default values
			if(_param_addAlias as Object == null) _param_addAlias = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			dynamic addAlias = XVar.Clone(_param_addAlias);
			#endregion

			dynamic ret = null;
			ret = new XVar("");
			if(XVar.Pack(this.m_expr))
			{
				if(XVar.Pack(MVCFunctions.is_string((XVar)(this.m_expr))))
				{
					ret = XVar.Clone(this.m_expr);
				}
				else
				{
					if(XVar.Pack(MVCFunctions.is_a((XVar)(this.m_expr), new XVar("SQLQuery"))))
					{
						ret = XVar.Clone(this.m_sql);
					}
					else
					{
						ret = XVar.Clone(this.m_expr.toSql((XVar)(query)));
					}
				}
			}
			if(XVar.Pack(this.m_isEncrypted))
			{
				if(XVar.Pack(!(XVar)(query.cipherer.isEncryptionByPHPEnabled())))
				{
					ret = XVar.Clone(query.cipherer.GetEncryptedFieldName((XVar)(ret)));
				}
			}
			if(XVar.Pack(addAlias))
			{
				if(XVar.Pack(this.connection == null))
				{
					setConnection((XVar)(this.m_srcTableName));
				}
				if(this.m_alias != "")
				{
					ret = MVCFunctions.Concat(ret, " as ", this.connection.addFieldWrappers((XVar)(this.m_alias)));
				}
				else
				{
					if(XVar.Pack(MVCFunctions.is_object((XVar)(this.m_expr))))
					{
						if((XVar)(this.m_expr.IsSQLField())  && (XVar)(query.cipherer != null))
						{
							if(XVar.Pack(!(XVar)(query.cipherer.isEncryptionByPHPEnabled())))
							{
								if(XVar.Pack(query.cipherer.isFieldEncrypted((XVar)(this.m_expr.m_strName))))
								{
									ret = MVCFunctions.Concat(ret, " as ", this.connection.addFieldWrappers((XVar)(this.m_expr.m_strName)));
								}
							}
						}
					}
				}
			}
			return ret;
		}
		public override XVar IsAsterisk()
		{
			if(XVar.Pack(MVCFunctions.is_object((XVar)(this.m_expr))))
			{
				return this.m_expr.IsAsterisk();
			}
			return false;
		}
		public override XVar IsAggregationFunctionCall()
		{
			if(XVar.Pack(MVCFunctions.is_object((XVar)(this.m_expr))))
			{
				return this.m_expr.IsAggregationFunctionCall();
			}
			return false;
		}
		public virtual XVar getAlias()
		{
			if((XVar)(true)  && (XVar)(!(XVar)(true)))
			{
				return this.m_alias;
			}
			else
			{
				if(XVar.Pack(true))
				{
					if((XVar)(true)  && (XVar)(this.m_alias != ""))
					{
						return this.m_alias;
					}
					else
					{
						return this.m_expr.m_strName;
					}
				}
			}
			return null;
		}
	}
	public partial class SQLFromListItem : SQLEntity
	{
		public dynamic m_sql;
		public dynamic m_link;
		public dynamic m_table;
		public dynamic m_alias;
		public dynamic m_joinon;
		protected dynamic m_srcTableName;
		protected static bool skipSQLFromListItemCtor = false;
		public SQLFromListItem(dynamic _param_proto)
		{
			if(skipSQLFromListItemCtor)
			{
				skipSQLFromListItemCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_link = XVar.Clone((XVar.Pack(proto.KeyExists("m_link")) ? XVar.Pack(proto["m_link"]) : XVar.Pack(null)));
			this.m_table = XVar.Clone((XVar.Pack(proto.KeyExists("m_table")) ? XVar.Pack(proto["m_table"]) : XVar.Pack(null)));
			this.m_alias = XVar.Clone((XVar.Pack(proto.KeyExists("m_alias")) ? XVar.Pack(proto["m_alias"]) : XVar.Pack(null)));
			this.m_joinon = XVar.Clone((XVar.Pack(proto.KeyExists("m_joinon")) ? XVar.Pack(proto["m_joinon"]) : XVar.Pack(null)));
			this.m_sql = XVar.Clone((XVar.Pack(proto.KeyExists("m_sql")) ? XVar.Pack(proto["m_sql"]) : XVar.Pack(null)));
			this.m_srcTableName = XVar.Clone(proto["m_srcTableName"]);
		}
		public override XVar SetQuery(dynamic query)
		{
			if(XVar.Pack(MVCFunctions.is_object((XVar)(this.m_table))))
			{
				this.m_table.SetQuery((XVar)(query));
			}
			return null;
		}
		public virtual XVar toSql(dynamic _param_query, dynamic _param_first)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			dynamic first = XVar.Clone(_param_first);
			#endregion

			dynamic joinStr = null, ret = null, skipAlias = null;
			ret = new XVar("");
			skipAlias = new XVar(false);
			if(XVar.Pack(MVCFunctions.is_a((XVar)(this.m_table), new XVar("SQLTable"))))
			{
				ret = MVCFunctions.Concat(ret, this.m_table.toSql((XVar)(query)));
			}
			else
			{
				return this.m_sql;
			}
			if((XVar)(this.m_alias != "")  && (XVar)(!(XVar)(skipAlias)))
			{
				if(XVar.Pack(this.connection == null))
				{
					setConnection((XVar)(this.m_srcTableName));
				}
				ret = MVCFunctions.Concat(ret, " ", this.connection.addFieldWrappers((XVar)(this.m_alias)));
			}
			if(this.m_link == "SQLL_MAIN")
			{
				return ret;
			}
			switch(((XVar)this.m_link).ToString())
			{
				case "SQLL_INNERJOIN":
					ret = XVar.Clone(MVCFunctions.Concat(" INNER JOIN ", ret));
					break;
				case "SQLL_NATURALJOIN":
					ret = XVar.Clone(MVCFunctions.Concat(" NATURAL JOIN ", ret));
					break;
				case "SQLL_LEFTJOIN":
					ret = XVar.Clone(MVCFunctions.Concat(" LEFT OUTER JOIN ", ret));
					break;
				case "SQLL_RIGHTJOIN":
					ret = XVar.Clone(MVCFunctions.Concat(" RIGHT OUTER JOIN ", ret));
					break;
				case "SQLL_FULLOUTERJOIN":
					ret = XVar.Clone(MVCFunctions.Concat(" FULL OUTER JOIN ", ret));
					break;
				case "SQLL_CROSSJOIN":
					ret = XVar.Clone(MVCFunctions.Concat((XVar.Pack(!(XVar)(first)) ? XVar.Pack(",") : XVar.Pack("")), ret));
					break;
			}
			joinStr = XVar.Clone(this.m_joinon.toSql((XVar)(query)));
			if(joinStr != XVar.Pack(""))
			{
				ret = MVCFunctions.Concat(ret, " ON ", joinStr);
			}
			return ret;
		}
		public virtual XVar getIdentifier()
		{
			if(this.m_alias != "")
			{
				return this.m_alias;
			}
			return this.m_table;
		}
	}
	public partial class SQLJoinOn : SQLEntity
	{
		public dynamic m_field1;
		public dynamic m_field2;
		protected static bool skipSQLJoinOnCtor = false;
		public SQLJoinOn(dynamic _param_proto)
		{
			if(skipSQLJoinOnCtor)
			{
				skipSQLJoinOnCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_field1 = XVar.Clone((XVar.Pack(proto.KeyExists("m_field1")) ? XVar.Pack(proto["m_field1"]) : XVar.Pack(null)));
			this.m_field2 = XVar.Clone((XVar.Pack(proto.KeyExists("m_field2")) ? XVar.Pack(proto["m_field2"]) : XVar.Pack(null)));
		}
	}
	public partial class SQLFunctionCall : SQLEntity
	{
		public dynamic m_functiontype;
		public dynamic m_strFunctionName;
		public dynamic m_arguments;
		protected static bool skipSQLFunctionCallCtor = false;
		public SQLFunctionCall(dynamic _param_proto)
		{
			if(skipSQLFunctionCallCtor)
			{
				skipSQLFunctionCallCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_functiontype = XVar.Clone((XVar.Pack(proto.KeyExists("m_functiontype")) ? XVar.Pack(proto["m_functiontype"]) : XVar.Pack(null)));
			this.m_strFunctionName = XVar.Clone((XVar.Pack(proto.KeyExists("m_strFunctionName")) ? XVar.Pack(proto["m_strFunctionName"]) : XVar.Pack(null)));
			this.m_arguments = XVar.Clone((XVar.Pack(proto.KeyExists("m_arguments")) ? XVar.Pack(proto["m_arguments"]) : XVar.Pack(null)));
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			dynamic args = XVar.Array(), ret = null;
			ret = new XVar("");
			switch(((XVar)this.m_functiontype).ToString())
			{
				case "SQLF_AVG":
					ret = MVCFunctions.Concat(ret, " AVG");
					break;
				case "SQLF_SUM":
					ret = MVCFunctions.Concat(ret, " SUM");
					break;
				case "SQLF_MIN":
					ret = MVCFunctions.Concat(ret, " MIN");
					break;
				case "SQLF_MAX":
					ret = MVCFunctions.Concat(ret, " MAX");
					break;
				case "SQLF_COUNT":
					ret = MVCFunctions.Concat(ret, " COUNT");
					break;
				default:
					ret = MVCFunctions.Concat(ret, this.m_strFunctionName);
					break;
			}
			args = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> a in this.m_arguments.GetEnumerator())
			{
				args.InitAndSetArrayItem(a.Value.toSql((XVar)(query)), null);
			}
			ret = MVCFunctions.Concat(ret, "(", MVCFunctions.implode(new XVar(","), (XVar)(args)), ")");
			return ret;
		}
		public override XVar IsAggregationFunctionCall()
		{
			switch(((XVar)this.m_functiontype).ToString())
			{
				case "SQLF_AVG":
				case "SQLF_SUM":
				case "SQLF_MIN":
				case "SQLF_MAX":
				case "SQLF_COUNT":
					return true;
			}
			return false;
		}
	}
	public partial class SQLGroupByItem : SQLEntity
	{
		public dynamic m_column;
		protected static bool skipSQLGroupByItemCtor = false;
		public SQLGroupByItem(dynamic _param_proto)
		{
			if(skipSQLGroupByItemCtor)
			{
				skipSQLGroupByItemCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_column = XVar.Clone((XVar.Pack(proto.KeyExists("m_column")) ? XVar.Pack(proto["m_column"]) : XVar.Pack(null)));
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			return this.m_column.toSql((XVar)(query));
		}
	}
	public partial class SQLLogicalExpr : SQLEntity
	{
		public dynamic m_uniontype;
		public dynamic m_column;
		public dynamic m_strCase;
		public dynamic m_havingmode;
		public dynamic m_contained;
		public dynamic m_inBrackets;
		public dynamic m_useAlias;
		public dynamic m_sql;
		public dynamic query;
		public dynamic postSql;
		protected static bool skipSQLLogicalExprCtor = false;
		public SQLLogicalExpr(dynamic _param_proto)
		{
			if(skipSQLLogicalExprCtor)
			{
				skipSQLLogicalExprCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_sql = XVar.Clone((XVar.Pack(proto.KeyExists("m_sql")) ? XVar.Pack(proto["m_sql"]) : XVar.Pack(null)));
			this.m_uniontype = XVar.Clone((XVar.Pack(proto.KeyExists("m_uniontype")) ? XVar.Pack(proto["m_uniontype"]) : XVar.Pack(null)));
			this.m_column = XVar.Clone((XVar.Pack(proto.KeyExists("m_column")) ? XVar.Pack(proto["m_column"]) : XVar.Pack(null)));
			this.m_strCase = XVar.Clone((XVar.Pack(proto.KeyExists("m_strCase")) ? XVar.Pack(proto["m_strCase"]) : XVar.Pack(null)));
			this.m_havingmode = XVar.Clone((XVar.Pack(proto.KeyExists("m_havingmode")) ? XVar.Pack(proto["m_havingmode"]) : XVar.Pack(null)));
			this.m_contained = XVar.Clone((XVar.Pack(proto.KeyExists("m_contained")) ? XVar.Pack(proto["m_contained"]) : XVar.Pack(null)));
			this.m_inBrackets = XVar.Clone((XVar.Pack(proto.KeyExists("m_inBrackets")) ? XVar.Pack(proto["m_inBrackets"]) : XVar.Pack(null)));
			this.m_useAlias = XVar.Clone((XVar.Pack(proto.KeyExists("m_useAlias")) ? XVar.Pack(proto["m_useAlias"]) : XVar.Pack(null)));
			this.postSql = XVar.Clone(XVar.Array());
		}
		public override XVar SetQuery(dynamic query)
		{
			dynamic nCnt = null;
			this.query = query;
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(this.m_contained); nCnt++)
			{
				this.m_contained[nCnt].SetQuery((XVar)(query));
			}
			return null;
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			dynamic ret = null;
			ret = new XVar("");
			if(XVar.Pack(haveContained()))
			{
				dynamic contained = XVar.Array(), glue = null;
				glue = new XVar("");
				if(this.m_uniontype == "SQLL_AND")
				{
					glue = new XVar(" AND ");
				}
				else
				{
					if(this.m_uniontype == "SQLL_OR")
					{
						glue = new XVar(" OR ");
					}
					else
					{
						MVCFunctions.ob_flush();
						HttpContext.Current.Response.End();
						throw new RunnerInlineOutputException();
					}
				}
				contained = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> c in this.m_contained.GetEnumerator())
				{
					dynamic cSql = null;
					cSql = XVar.Clone(c.Value.toSql((XVar)(query)));
					if(cSql != XVar.Pack(""))
					{
						contained.InitAndSetArrayItem(MVCFunctions.Concat("(", cSql, ")"), null);
					}
				}
				if(0 < MVCFunctions.count(contained))
				{
					ret = XVar.Clone(MVCFunctions.implode((XVar)(glue), (XVar)(contained)));
				}
				if(0 < MVCFunctions.count(this.postSql))
				{
					dynamic nCnt = null;
					if(ret == XVar.Pack(""))
					{
						ret = MVCFunctions.Concat(ret, "(", this.postSql[0], ")");
					}
					else
					{
						ret = XVar.Clone(MVCFunctions.Concat("(", ret, ")", glue, "(", this.postSql[0], ")"));
					}
					nCnt = new XVar(1);
					for(;nCnt < MVCFunctions.count(this.postSql); nCnt++)
					{
						ret = MVCFunctions.Concat(ret, glue, "(", this.postSql[nCnt], ")");
					}
				}
				if(XVar.Pack(this.m_inBrackets))
				{
					ret = XVar.Clone(MVCFunctions.Concat("(", ret, ")"));
				}
				return ret;
			}
			if(XVar.Pack(!(XVar)(this.m_column)))
			{
				ret = XVar.Clone(this.m_sql);
			}
			else
			{
				if(XVar.Pack(this.m_useAlias))
				{
					ret = XVar.Clone(this.m_column.m_alias);
				}
				else
				{
					ret = XVar.Clone(this.m_column.toSql((XVar)(query)));
				}
			}
			if(this.m_strCase == "NOT")
			{
				return MVCFunctions.Concat(" NOT ", ret);
			}
			else
			{
				if(ret != XVar.Pack(""))
				{
					ret = MVCFunctions.Concat(ret, " ", this.m_strCase);
				}
			}
			if(XVar.Pack(this.m_inBrackets))
			{
				ret = XVar.Clone(MVCFunctions.Concat("(", ret, ")"));
			}
			return ret;
		}
		public virtual XVar haveContained()
		{
			return (XVar)(0 < MVCFunctions.count(this.m_contained))  || (XVar)(0 < MVCFunctions.count(this.postSql));
		}
	}
	public partial class SQLOrderByItem : SQLEntity
	{
		public dynamic m_column;
		public dynamic m_bAsc;
		public dynamic m_nColumn;
		protected static bool skipSQLOrderByItemCtor = false;
		public SQLOrderByItem(dynamic _param_proto)
		{
			if(skipSQLOrderByItemCtor)
			{
				skipSQLOrderByItemCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_column = XVar.Clone((XVar.Pack(proto.KeyExists("m_column")) ? XVar.Pack(proto["m_column"]) : XVar.Pack(null)));
			this.m_bAsc = XVar.Clone((XVar.Pack(proto.KeyExists("m_bAsc")) ? XVar.Pack(proto["m_bAsc"]) : XVar.Pack(null)));
			this.m_nColumn = XVar.Clone((XVar.Pack(proto.KeyExists("m_nColumn")) ? XVar.Pack(proto["m_nColumn"]) : XVar.Pack(null)));
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			dynamic ret = null;
			ret = new XVar("");
			if(0 == this.m_nColumn)
			{
				ret = MVCFunctions.Concat(ret, this.m_column.toSql((XVar)(query)));
			}
			else
			{
				ret = MVCFunctions.Concat(ret, this.m_nColumn);
			}
			if(XVar.Pack(!(XVar)(this.m_bAsc)))
			{
				ret = MVCFunctions.Concat(ret, " DESC ");
			}
			return ret;
		}
	}
	public partial class SQLTable : SQLEntity
	{
		public dynamic m_strName;
		public dynamic m_columns;
		public dynamic query;
		protected dynamic m_srcTableName;
		protected static bool skipSQLTableCtor = false;
		public SQLTable(dynamic _param_proto)
		{
			if(skipSQLTableCtor)
			{
				skipSQLTableCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			this.m_strName = XVar.Clone((XVar.Pack(proto.KeyExists("m_strName")) ? XVar.Pack(proto["m_strName"]) : XVar.Pack(null)));
			this.m_columns = XVar.Clone((XVar.Pack(proto.KeyExists("m_columns")) ? XVar.Pack(proto["m_columns"]) : XVar.Pack(null)));
			this.m_srcTableName = XVar.Clone(proto["m_srcTableName"]);
		}
		public override XVar SetQuery(dynamic query)
		{
			this.query = XVar.Clone(query);
			return null;
		}
		public virtual XVar toSql(dynamic _param_query)
		{
			#region pass-by-value parameters
			dynamic query = XVar.Clone(_param_query);
			#endregion

			if(XVar.Pack(this.connection == null))
			{
				setConnection((XVar)(this.m_srcTableName));
			}
			return this.connection.addTableWrappers((XVar)(this.m_strName));
		}
	}
	public partial class SQLQuery : SQLEntity
	{
		public dynamic m_strHead;
		public dynamic m_strFieldList;
		public dynamic m_strFrom;
		public dynamic m_strWhere;
		public dynamic m_strOrderBy;
		public dynamic m_where;
		public dynamic m_having;
		public dynamic m_fieldlist;
		public dynamic m_fromlist;
		public dynamic m_groupby;
		public dynamic m_orderby;
		public dynamic bHasAsterisks = XVar.Pack(false);
		public dynamic m_proto = XVar.Array();
		public dynamic cipherer = XVar.Pack(null);
		public dynamic m_srcTableName;
		protected static bool skipSQLQueryCtor = false;
		public SQLQuery(dynamic _param_proto)
		{
			if(skipSQLQueryCtor)
			{
				skipSQLQueryCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic proto = XVar.Clone(_param_proto);
			#endregion

			dynamic i = null, nCnt = null;
			this.m_proto = XVar.Clone(proto);
			this.m_strHead = XVar.Clone((XVar.Pack(proto.KeyExists("m_strHead")) ? XVar.Pack(proto["m_strHead"]) : XVar.Pack(null)));
			this.m_strFieldList = XVar.Clone((XVar.Pack(proto.KeyExists("m_strFieldList")) ? XVar.Pack(proto["m_strFieldList"]) : XVar.Pack(null)));
			this.m_strFrom = XVar.Clone((XVar.Pack(proto.KeyExists("m_strFrom")) ? XVar.Pack(proto["m_strFrom"]) : XVar.Pack(null)));
			this.m_strWhere = XVar.Clone((XVar.Pack(proto.KeyExists("m_strWhere")) ? XVar.Pack(proto["m_strWhere"]) : XVar.Pack(null)));
			this.m_strOrderBy = XVar.Clone((XVar.Pack(proto.KeyExists("m_strOrderBy")) ? XVar.Pack(proto["m_strOrderBy"]) : XVar.Pack(null)));
			this.m_where = XVar.Clone((XVar.Pack(proto.KeyExists("m_where")) ? XVar.Pack(proto["m_where"]) : XVar.Pack(null)));
			this.m_having = XVar.Clone((XVar.Pack(proto.KeyExists("m_having")) ? XVar.Pack(proto["m_having"]) : XVar.Pack(null)));
			this.m_fieldlist = XVar.Clone((XVar.Pack(proto.KeyExists("m_fieldlist")) ? XVar.Pack(proto["m_fieldlist"]) : XVar.Pack(null)));
			this.m_fromlist = XVar.Clone((XVar.Pack(proto.KeyExists("m_fromlist")) ? XVar.Pack(proto["m_fromlist"]) : XVar.Pack(null)));
			this.m_groupby = XVar.Clone((XVar.Pack(proto.KeyExists("m_groupby")) ? XVar.Pack(proto["m_groupby"]) : XVar.Pack(null)));
			this.m_orderby = XVar.Clone((XVar.Pack(proto.KeyExists("m_orderby")) ? XVar.Pack(proto["m_orderby"]) : XVar.Pack(null)));
			this.cipherer = XVar.Clone((XVar.Pack(proto.KeyExists("cipherer")) ? XVar.Pack(proto["cipherer"]) : XVar.Pack(null)));
			this.m_srcTableName = XVar.Clone(proto["m_srcTableName"]);
			nCnt = new XVar(0);
			for(;nCnt < MVCFunctions.count(this.m_fromlist); nCnt++)
			{
				this.m_fromlist[nCnt].SetQuery(this);
			}
			this.m_where.SetQuery(this);
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(this.m_fieldlist)))))
			{
				return;
			}
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.m_fieldlist); i++)
			{
				if(XVar.Pack(this.m_fieldlist[i].IsAsterisk()))
				{
					this.bHasAsterisks = new XVar(true);
					break;
				}
			}
		}
		public virtual XVar updateProto()
		{
			this.m_proto.InitAndSetArrayItem(this.m_strHead, "m_strHead");
			this.m_proto.InitAndSetArrayItem(this.m_strFieldList, "m_strFieldList");
			this.m_proto.InitAndSetArrayItem(this.m_strFrom, "m_strFrom");
			this.m_proto.InitAndSetArrayItem(this.m_strWhere, "m_strWhere");
			this.m_proto.InitAndSetArrayItem(this.m_strOrderBy, "m_strOrderBy");
			this.m_proto.InitAndSetArrayItem(this.m_where, "m_where");
			this.m_proto.InitAndSetArrayItem(this.m_having, "m_having");
			this.m_proto.InitAndSetArrayItem(this.m_fieldlist, "m_fieldlist");
			this.m_proto.InitAndSetArrayItem(this.m_fromlist, "m_fromlist");
			this.m_proto.InitAndSetArrayItem(this.m_groupby, "m_groupby");
			this.m_proto.InitAndSetArrayItem(this.m_orderby, "m_orderby");
			this.m_proto.InitAndSetArrayItem(this.cipherer, "cipherer");
			this.m_proto.InitAndSetArrayItem(this.m_srcTableName, "m_srcTableName");
			return null;
		}
		public virtual XVar CloneObject()
		{
			return new SQLQuery((XVar)(this.m_proto));
		}
		public virtual XVar FromToSql()
		{
			dynamic nCnt = null, sql = null;
			if(XVar.Pack(this.connection == null))
			{
				setConnection((XVar)(this.m_srcTableName));
			}
			sql = new XVar("");
			if(0 < MVCFunctions.count(this.m_fromlist))
			{
				sql = MVCFunctions.Concat(sql, "\r\n");
				sql = MVCFunctions.Concat(sql, " FROM ");
			}
			if(this.connection.dbType == Constants.nDATABASE_Access)
			{
				dynamic sqlFromList = null;
				sqlFromList = new XVar("");
				nCnt = new XVar(0);
				for(;nCnt < MVCFunctions.count(this.m_fromlist); nCnt++)
				{
					if(!XVar.Equals(XVar.Pack(sqlFromList), XVar.Pack("")))
					{
						sqlFromList = XVar.Clone(MVCFunctions.Concat("(", sqlFromList, ")"));
					}
					sqlFromList = MVCFunctions.Concat(sqlFromList, this.m_fromlist[nCnt].toSql(this, (XVar)(nCnt == XVar.Pack(0))));
				}
				sql = MVCFunctions.Concat(sql, sqlFromList);
			}
			else
			{
				dynamic fromlist = XVar.Array();
				fromlist = XVar.Clone(XVar.Array());
				nCnt = new XVar(0);
				for(;nCnt < MVCFunctions.count(this.m_fromlist); nCnt++)
				{
					fromlist.InitAndSetArrayItem(this.m_fromlist[nCnt].toSql(this, (XVar)(nCnt == XVar.Pack(0))), null);
				}
				sql = MVCFunctions.Concat(sql, MVCFunctions.implode(new XVar("\r\n"), (XVar)(fromlist)));
			}
			return sql;
		}
		public virtual XVar HavingToSql()
		{
			return this.m_having.toSql(this);
		}
		public virtual XVar OrderByToSql()
		{
			return this.m_strOrderBy;
		}
		public virtual XVar HeadToSql(dynamic _param_oneRecordMode = null)
		{
			#region default values
			if(_param_oneRecordMode as Object == null) _param_oneRecordMode = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			#endregion

			dynamic fields = XVar.Array(), sql = null;
			if(XVar.Pack(this.connection == null))
			{
				setConnection((XVar)(this.m_srcTableName));
			}
			sql = new XVar("");
			sql = MVCFunctions.Concat(sql, this.m_strHead);
			if((XVar)(this.connection.dbType == Constants.nDATABASE_MSSQLServer)  || (XVar)(this.connection.dbType == Constants.nDATABASE_Access))
			{
				if(XVar.Pack(oneRecordMode))
				{
					sql = MVCFunctions.Concat(sql, " top 1 ");
				}
			}
			if(sql != XVar.Pack(""))
			{
				sql = MVCFunctions.Concat(sql, "\r\n");
			}
			fields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in this.m_fieldlist.GetEnumerator())
			{
				fields.InitAndSetArrayItem(f.Value.toSql(this), null);
			}
			if(0 < MVCFunctions.count(fields))
			{
				sql = MVCFunctions.Concat(sql, MVCFunctions.implode(new XVar(", "), (XVar)(fields)));
			}
			return sql;
		}
		public virtual XVar AddCustomExpression(dynamic _param_expression, dynamic _param_pSet_packed, dynamic _param_mainTable, dynamic _param_mainFiled, dynamic _param_alias = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_alias as Object == null) _param_alias = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic expression = XVar.Clone(_param_expression);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic mainTable = XVar.Clone(_param_mainTable);
			dynamic mainFiled = XVar.Clone(_param_mainFiled);
			dynamic alias = XVar.Clone(_param_alias);
			#endregion

			dynamic index = null, itemFound = null;
			index = XVar.Clone(MVCFunctions.count(this.m_fieldlist));
			itemFound = new XVar(false);
			foreach (KeyValuePair<XVar, dynamic> listItem in this.m_fieldlist.GetEnumerator())
			{
				if((XVar)(listItem.Value.m_expr == expression)  && (XVar)(listItem.Value.m_alias == alias))
				{
					index = XVar.Clone(listItem.Key);
					itemFound = new XVar(true);
					break;
				}
			}
			if(XVar.Pack(!(XVar)(itemFound)))
			{
				this.m_fieldlist.InitAndSetArrayItem(new SQLFieldListItem((XVar)(new XVar("m_expr", expression, "m_alias", alias, "m_srcTableName", this.m_srcTableName))), null);
			}
			pSet.addCustomExpressionIndex((XVar)(mainTable), (XVar)(mainFiled), (XVar)(index));
			return null;
		}
		public virtual XVar GroupByToSql()
		{
			dynamic groupby = XVar.Array(), sql = null;
			sql = new XVar("");
			groupby = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> g in this.m_groupby.GetEnumerator())
			{
				groupby.InitAndSetArrayItem(g.Value.toSql(this), null);
			}
			if(0 < MVCFunctions.count(groupby))
			{
				sql = MVCFunctions.Concat(sql, " GROUP BY ");
				sql = MVCFunctions.Concat(sql, MVCFunctions.implode(new XVar(","), (XVar)(groupby)));
				sql = MVCFunctions.Concat(sql, " ");
			}
			return sql;
		}
		public virtual XVar GroupByHavingToSql()
		{
			dynamic sql = null, sqlGroup = null, sqlHaving = null;
			sql = new XVar("");
			sqlGroup = XVar.Clone(GroupByToSql());
			sqlHaving = XVar.Clone(HavingToSql());
			if(XVar.Pack(MVCFunctions.strlen((XVar)(sqlGroup))))
			{
				sql = MVCFunctions.Concat(sql, sqlGroup);
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(sqlHaving))))
			{
				sql = MVCFunctions.Concat(sql, " HAVING ", sqlHaving);
			}
			return sql;
		}
		public virtual XVar toSql(dynamic _param_strwhere = null, dynamic _param_strorderby = null, dynamic _param_strhaving = null, dynamic _param_oneRecordMode = null, dynamic _param_joinFromPart = null)
		{
			#region default values
			if(_param_strwhere as Object == null) _param_strwhere = new XVar();
			if(_param_strorderby as Object == null) _param_strorderby = new XVar();
			if(_param_strhaving as Object == null) _param_strhaving = new XVar();
			if(_param_oneRecordMode as Object == null) _param_oneRecordMode = new XVar(false);
			if(_param_joinFromPart as Object == null) _param_joinFromPart = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic strwhere = XVar.Clone(_param_strwhere);
			dynamic strorderby = XVar.Clone(_param_strorderby);
			dynamic strhaving = XVar.Clone(_param_strhaving);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			dynamic joinFromPart = XVar.Clone(_param_joinFromPart);
			#endregion

			dynamic sql = null;
			if(XVar.Pack(this.connection == null))
			{
				setConnection((XVar)(this.m_srcTableName));
			}
			if(XVar.Pack(MVCFunctions.is_a((XVar)(strwhere), new XVar("SQLQuery"))))
			{
				strwhere = new XVar(null);
			}
			sql = XVar.Clone(HeadToSql((XVar)(oneRecordMode)));
			sql = MVCFunctions.Concat(sql, FromToSql());
			sql = MVCFunctions.Concat(sql, joinFromPart);
			if(strwhere != null)
			{
				if(!XVar.Equals(XVar.Pack(strwhere), XVar.Pack("")))
				{
					sql = MVCFunctions.Concat(sql, " WHERE ", strwhere, "\r\n");
				}
			}
			else
			{
				dynamic where = null;
				where = XVar.Clone(this.m_where.toSql(this));
				if(where != XVar.Pack(""))
				{
					sql = MVCFunctions.Concat(sql, " WHERE ", where, "\r\n");
				}
			}
			sql = MVCFunctions.Concat(sql, GroupByToSql());
			if(strhaving != null)
			{
				if(!XVar.Equals(XVar.Pack(strhaving), XVar.Pack("")))
				{
					sql = MVCFunctions.Concat(sql, " HAVING ", strhaving, "\r\n");
				}
			}
			else
			{
				dynamic having = null;
				having = XVar.Clone(this.m_having.toSql(this));
				if(having != XVar.Pack(""))
				{
					sql = MVCFunctions.Concat(sql, " HAVING ", having, "\r\n");
				}
			}
			if(!XVar.Equals(XVar.Pack(strorderby), XVar.Pack(null)))
			{
				sql = MVCFunctions.Concat(sql, strorderby, "\r\n");
			}
			else
			{
				dynamic orderby = XVar.Array();
				orderby = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> g in this.m_orderby.GetEnumerator())
				{
					orderby.InitAndSetArrayItem(g.Value.toSql(this), null);
				}
				if(0 < MVCFunctions.count(orderby))
				{
					sql = MVCFunctions.Concat(sql, " ORDER BY ");
					sql = MVCFunctions.Concat(sql, MVCFunctions.implode(new XVar(","), (XVar)(orderby)));
					sql = MVCFunctions.Concat(sql, "\r\n");
				}
			}
			if(this.connection.dbType == Constants.nDATABASE_MySQL)
			{
				if(XVar.Pack(oneRecordMode))
				{
					sql = MVCFunctions.Concat(sql, " limit 0,1");
				}
			}
			if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
			{
				if(XVar.Pack(oneRecordMode))
				{
					sql = MVCFunctions.Concat(sql, " limit 1");
				}
			}
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				if(XVar.Pack(oneRecordMode))
				{
					sql = MVCFunctions.Concat(sql, " fetch first 1 rows only");
				}
			}
			return sql;
		}
		public virtual XVar TableCount()
		{
			return MVCFunctions.count(this.m_fromlist);
		}
		public virtual XVar IsAggrFuncField(dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			if(XVar.Pack(HasAsterisks()))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(this.m_fieldlist.KeyExists(idx))))
			{
				return false;
			}
			return this.m_fieldlist[idx].IsAggregationFunctionCall();
		}
		public virtual XVar ReplaceFieldsWithDummies(dynamic _param_fieldindices)
		{
			#region pass-by-value parameters
			dynamic fieldindices = XVar.Clone(_param_fieldindices);
			#endregion

			if(XVar.Pack(HasAsterisks()))
			{
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> idx in fieldindices.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(this.m_fieldlist.KeyExists(idx.Value - 1))))
				{
					return null;
				}
				this.m_fieldlist.InitAndSetArrayItem(new SQLFieldListItem((XVar)(new XVar("m_alias", MVCFunctions.Concat("runnerdummy", idx.Value), "m_expr", "1", "m_srcTableName", this.m_srcTableName))), idx.Value - 1);
			}
			return null;
		}
		public virtual XVar RemoveAllFieldsExcept(dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			dynamic i = null, removeindices = XVar.Array();
			if(XVar.Pack(HasAsterisks()))
			{
				return null;
			}
			removeindices = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.m_fieldlist); i++)
			{
				if(i != idx - 1)
				{
					removeindices.InitAndSetArrayItem(i + 1, null);
				}
			}
			ReplaceFieldsWithDummies((XVar)(removeindices));
			return null;
		}
		public virtual XVar WhereToSql()
		{
			return this.m_where.toSql(this);
		}
		public virtual XVar Where()
		{
			return this.m_where;
		}
		public virtual SQLLogicalExpr Having()
		{
			return XVar.UnPackSQLLogicalExpr(this.m_having ?? new XVar());
		}
		public virtual XVar Copy()
		{
			return MVCFunctions.unserialize((XVar)(MVCFunctions.serialize(this)));
		}
		public virtual XVar HasGroupBy()
		{
			return 0 < MVCFunctions.count(this.m_groupby);
		}
		public virtual XVar HasSubQueryInFromClause()
		{
			foreach (KeyValuePair<XVar, dynamic> fromList in this.m_fromlist.GetEnumerator())
			{
				if((XVar)(MVCFunctions.is_object((XVar)(fromList.Value.m_table)))  && (XVar)(MVCFunctions.is_a((XVar)(fromList.Value.m_table), new XVar("SQLQuery"))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar HasJoinInFromClause()
		{
			return 1 < MVCFunctions.count(this.m_fromlist);
		}
		public virtual XVar HasTableInFormClause(dynamic _param_tName)
		{
			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> fromList in this.m_fromlist.GetEnumerator())
			{
				if(tName == fromList.Value.getIdentifier())
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar HasAsterisks()
		{
			return this.bHasAsterisks;
		}
		public virtual XVar addWhere(dynamic _param__where)
		{
			#region pass-by-value parameters
			dynamic _where = XVar.Clone(_param__where);
			#endregion

			dynamic myobj = null, myproto = XVar.Array(), newobj = null, newproto = XVar.Array();
			if(MVCFunctions.trim((XVar)(_where)) == "")
			{
				return null;
			}
			myproto = XVar.Clone(XVar.Array());
			myproto.InitAndSetArrayItem(_where, "m_sql");
			myproto.InitAndSetArrayItem("SQLL_UNKNOWN", "m_uniontype");
			myproto.InitAndSetArrayItem(null, "m_column");
			myproto.InitAndSetArrayItem(XVar.Array(), "m_contained");
			myproto.InitAndSetArrayItem("", "m_strCase");
			myproto.InitAndSetArrayItem(false, "m_havingmode");
			myproto.InitAndSetArrayItem(true, "m_inBrackets");
			myproto.InitAndSetArrayItem(false, "m_useAlias");
			myobj = XVar.Clone(new SQLLogicalExpr((XVar)(myproto)));
			myobj.query = XVar.Clone(this);
			if(XVar.Pack(!(XVar)(this.m_where)))
			{
				this.m_where = XVar.Clone(myobj);
				return null;
			}
			newproto = XVar.Clone(XVar.Array());
			newproto.InitAndSetArrayItem("SQLL_AND", "m_uniontype");
			newproto.InitAndSetArrayItem(XVar.Array(), "m_contained");
			newproto.InitAndSetArrayItem(this.m_where, "m_contained", null);
			newproto.InitAndSetArrayItem(myobj, "m_contained", null);
			newobj = XVar.Clone(new SQLLogicalExpr((XVar)(newproto)));
			newobj.query = XVar.Clone(this);
			this.m_where = XVar.Clone(newobj);
			return null;
		}
		public virtual XVar replaceWhere(dynamic _param__where)
		{
			#region pass-by-value parameters
			dynamic _where = XVar.Clone(_param__where);
			#endregion

			dynamic myobj = null, myproto = XVar.Array();
			if(MVCFunctions.trim((XVar)(_where)) == "")
			{
				myproto = XVar.Clone(XVar.Array());
				myobj = XVar.Clone(new SQLLogicalExpr((XVar)(myproto)));
				myobj.query = XVar.Clone(this);
				this.m_where = XVar.Clone(myobj);
				return null;
			}
			myproto = XVar.Clone(XVar.Array());
			myproto.InitAndSetArrayItem(_where, "m_sql");
			myproto.InitAndSetArrayItem("SQLL_UNKNOWN", "m_uniontype");
			myproto.InitAndSetArrayItem(null, "m_column");
			myproto.InitAndSetArrayItem(XVar.Array(), "m_contained");
			myproto.InitAndSetArrayItem("", "m_strCase");
			myproto.InitAndSetArrayItem(false, "m_havingmode");
			myproto.InitAndSetArrayItem(true, "m_inBrackets");
			myproto.InitAndSetArrayItem(false, "m_useAlias");
			myobj = XVar.Clone(new SQLLogicalExpr((XVar)(myproto)));
			myobj.query = XVar.Clone(this);
			this.m_where = XVar.Clone(myobj);
			return null;
		}
		public virtual XVar addField(dynamic _param__field, dynamic _param__alias)
		{
			#region pass-by-value parameters
			dynamic _field = XVar.Clone(_param__field);
			dynamic _alias = XVar.Clone(_param__alias);
			#endregion

			dynamic myobj = null, myproto = XVar.Array();
			myproto = XVar.Clone(XVar.Array());
			myobj = XVar.Clone(new SQLNonParsed((XVar)(new XVar("m_sql", _field))));
			myproto.InitAndSetArrayItem(myobj, "m_expr");
			myproto.InitAndSetArrayItem(_alias, "m_alias");
			myproto.InitAndSetArrayItem(this.m_srcTableName, "m_srcTableName");
			myobj = XVar.Clone(new SQLFieldListItem((XVar)(myproto)));
			this.m_fieldlist.InitAndSetArrayItem(myobj, null);
			updateProto();
			return null;
		}
		public virtual XVar replaceField(dynamic _param__field, dynamic _param__newfield, dynamic _param__newalias = null)
		{
			#region default values
			if(_param__newalias as Object == null) _param__newalias = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic _field = XVar.Clone(_param__field);
			dynamic _newfield = XVar.Clone(_param__newfield);
			dynamic _newalias = XVar.Clone(_param__newalias);
			#endregion

			dynamic myobj = null, myproto = XVar.Array();
			myproto = XVar.Clone(XVar.Array());
			myobj = XVar.Clone(new SQLNonParsed((XVar)(new XVar("m_sql", _newfield))));
			myproto.InitAndSetArrayItem(myobj, "m_expr");
			if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(_field))))
			{
				foreach (KeyValuePair<XVar, dynamic> obj in this.m_fieldlist.GetEnumerator())
				{
					if(this.m_fieldlist[obj.Key].getAlias() == _field)
					{
						_field = XVar.Clone(obj.Key);
						break;
					}
				}
			}
			if(XVar.Pack(MVCFunctions.IsNumeric(_field)))
			{
				if(XVar.Pack(!(XVar)(_newalias)))
				{
					_newalias = XVar.Clone(this.m_fieldlist[_field].getAlias());
				}
				myproto.InitAndSetArrayItem(_newalias, "m_alias");
				myproto.InitAndSetArrayItem(this.m_srcTableName, "m_srcTableName");
				myobj = XVar.Clone(new SQLFieldListItem((XVar)(myproto)));
				this.m_fieldlist.InitAndSetArrayItem(myobj, _field);
			}
			updateProto();
			return null;
		}
		public virtual XVar deleteField(dynamic _param__field)
		{
			#region pass-by-value parameters
			dynamic _field = XVar.Clone(_param__field);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.IsNumeric(_field))))
			{
				foreach (KeyValuePair<XVar, dynamic> obj in this.m_fieldlist.GetEnumerator())
				{
					if(this.m_fieldlist[obj.Key].getAlias() == _field)
					{
						_field = XVar.Clone(obj.Key);
						break;
					}
				}
			}
			if(XVar.Pack(MVCFunctions.IsNumeric(_field)))
			{
				dynamic fieldlist = null;
				fieldlist = XVar.Clone(this.m_fieldlist);
				MVCFunctions.array_splice((XVar)(fieldlist), (XVar)(_field), new XVar(1));
				this.m_fieldlist = XVar.Clone(fieldlist);
			}
			updateProto();
			return null;
		}
		public virtual XVar deleteAllFieldsExcept(dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			dynamic field = null;
			field = XVar.Clone(this.m_fieldlist[idx]);
			this.m_fieldlist = XVar.Clone(XVar.Array());
			this.m_fieldlist.InitAndSetArrayItem(field, null);
			return null;
		}
		public virtual XVar gSQLWhere(dynamic _param_where)
		{
			#region pass-by-value parameters
			dynamic where = XVar.Clone(_param_where);
			#endregion

			return buildSQL_default((XVar)(new XVar(0, where)));
		}
		public virtual XVar getSqlComponents(dynamic _param_oneRecordMode = null)
		{
			#region default values
			if(_param_oneRecordMode as Object == null) _param_oneRecordMode = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			#endregion

			return new XVar("head", HeadToSql((XVar)(oneRecordMode)), "from", FromToSql(), "where", WhereToSql(), "groupby", GroupByToSql(), "having", Having().toSql(this));
		}
		public virtual XVar buildSQL_default(dynamic _param_additionalWhere = null)
		{
			#region default values
			if(_param_additionalWhere as Object == null) _param_additionalWhere = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic additionalWhere = XVar.Clone(_param_additionalWhere);
			#endregion

			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(additionalWhere)))))
			{
				additionalWhere = XVar.Clone(new XVar(0, additionalWhere));
			}
			return buildSQL((XVar)(getSqlComponents()), (XVar)(additionalWhere));
		}
		public static XVar buildSQL(dynamic _param_sqlParts, dynamic _param_mandatoryWhere = null, dynamic _param_mandatoryHaving = null, dynamic _param_optionalWhere = null, dynamic _param_optionalHaving = null)
		{
			#region default values
			if(_param_mandatoryWhere as Object == null) _param_mandatoryWhere = new XVar(XVar.Array());
			if(_param_mandatoryHaving as Object == null) _param_mandatoryHaving = new XVar(XVar.Array());
			if(_param_optionalWhere as Object == null) _param_optionalWhere = new XVar(XVar.Array());
			if(_param_optionalHaving as Object == null) _param_optionalHaving = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic sqlParts = XVar.Clone(_param_sqlParts);
			dynamic mandatoryWhere = XVar.Clone(_param_mandatoryWhere);
			dynamic mandatoryHaving = XVar.Clone(_param_mandatoryHaving);
			dynamic optionalWhere = XVar.Clone(_param_optionalWhere);
			dynamic optionalHaving = XVar.Clone(_param_optionalHaving);
			#endregion

			dynamic mHaving = null, mWhere = null, oHaving = null, oWhere = null, prepSqlParts = XVar.Array(), sqlHead = null, unionMode = null;
			prepSqlParts = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> s in sqlParts.GetEnumerator())
			{
				prepSqlParts.InitAndSetArrayItem(DB.PrepareSQL((XVar)(s.Value)), s.Key);
			}
			sqlHead = XVar.Clone(prepSqlParts["head"]);
			if(0 == MVCFunctions.strlen((XVar)(sqlHead)))
			{
				sqlHead = new XVar("select * ");
			}
			unionMode = XVar.Clone((XVar)(optionalWhere)  && (XVar)(optionalHaving));
			mWhere = XVar.Clone(combineCases((XVar)(mandatoryWhere), new XVar("and")));
			oWhere = XVar.Clone(combineCases((XVar)(optionalWhere), new XVar("or")));
			mHaving = XVar.Clone(combineCases((XVar)(mandatoryHaving), new XVar("and")));
			oHaving = XVar.Clone(combineCases((XVar)(optionalHaving), new XVar("or")));
			if((XVar)(MVCFunctions.strlen((XVar)(oWhere)))  && (XVar)(MVCFunctions.strlen((XVar)(oHaving))))
			{
				dynamic having1 = null, having2 = null, where1 = null, where2 = null;
				where1 = XVar.Clone(combineCases((XVar)(new XVar(0, mWhere, 1, oWhere, 2, prepSqlParts["where"])), new XVar("and")));
				having1 = XVar.Clone(combineCases((XVar)(new XVar(0, mHaving, 1, prepSqlParts["having"])), new XVar("and")));
				where2 = XVar.Clone(combineCases((XVar)(new XVar(0, mWhere, 1, prepSqlParts["where"])), new XVar("and")));
				having2 = XVar.Clone(combineCases((XVar)(new XVar(0, mHaving, 1, oHaving, 2, prepSqlParts["having"])), new XVar("and")));
				if(0 != MVCFunctions.strlen((XVar)(where1)))
				{
					where1 = XVar.Clone(MVCFunctions.Concat(" WHERE ", where1));
				}
				if(0 != MVCFunctions.strlen((XVar)(having1)))
				{
					having1 = XVar.Clone(MVCFunctions.Concat(" HAVING ", having1));
				}
				if(0 != MVCFunctions.strlen((XVar)(where2)))
				{
					where2 = XVar.Clone(MVCFunctions.Concat(" WHERE ", where2));
				}
				if(0 != MVCFunctions.strlen((XVar)(having2)))
				{
					having2 = XVar.Clone(MVCFunctions.Concat(" HAVING ", having2));
				}
				return MVCFunctions.implode(new XVar(" "), (XVar)(new XVar(0, sqlHead, 1, prepSqlParts["from"], 2, where1, 3, prepSqlParts["groupby"], 4, having1, 5, "union", 6, sqlHead, 7, prepSqlParts["from"], 8, where2, 9, prepSqlParts["groupby"], 10, having2)));
			}
			else
			{
				dynamic having = null, where = null;
				where = XVar.Clone(combineCases((XVar)(new XVar(0, mWhere, 1, oWhere, 2, prepSqlParts["where"])), new XVar("and")));
				having = XVar.Clone(combineCases((XVar)(new XVar(0, mHaving, 1, oHaving, 2, prepSqlParts["having"])), new XVar("and")));
				if(0 != MVCFunctions.strlen((XVar)(where)))
				{
					where = XVar.Clone(MVCFunctions.Concat(" WHERE ", where));
				}
				if(0 != MVCFunctions.strlen((XVar)(having)))
				{
					having = XVar.Clone(MVCFunctions.Concat(" HAVING ", having));
				}
				return MVCFunctions.implode(new XVar(" "), (XVar)(new XVar(0, sqlHead, 1, prepSqlParts["from"], 2, where, 3, prepSqlParts["groupby"], 4, having)));
			}
			return null;
		}
		public static XVar combineCases(dynamic _param__cases, dynamic _param_operator)
		{
			#region pass-by-value parameters
			dynamic _cases = XVar.Clone(_param__cases);
			dynamic var_operator = XVar.Clone(_param_operator);
			#endregion

			dynamic cases = XVar.Array(), result = null;
			cases = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> c in _cases.GetEnumerator())
			{
				if(0 != MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(c.Value)))))
				{
					cases.InitAndSetArrayItem(c.Value, null);
				}
			}
			result = XVar.Clone(MVCFunctions.implode((XVar)(MVCFunctions.Concat(" ) ", var_operator, " ( ")), (XVar)(cases)));
			if(0 == MVCFunctions.strlen((XVar)(result)))
			{
				return "";
			}
			return MVCFunctions.Concat("( ", result, " )");
		}
		public virtual XVar fieldComesFromTheTableAsIs(dynamic _param_index, dynamic _param_tableName, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic index = XVar.Clone(_param_index);
			dynamic tableName = XVar.Clone(_param_tableName);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic fieldListItem = null;
			fieldListItem = XVar.Clone(this.m_fieldlist[index]);
			if(XVar.Pack(!(XVar)(MVCFunctions.is_object((XVar)(fieldListItem)))))
			{
				return false;
			}
			if(0 != MVCFunctions.strlen((XVar)(fieldListItem.m_alias)))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.is_a((XVar)(fieldListItem.m_expr), new XVar("SQLField")))))
			{
				return false;
			}
			if((XVar)(MVCFunctions.strlen((XVar)(fieldListItem.m_expr.m_strTable)) != 0)  && (XVar)(fieldListItem.m_expr.m_strTable != tableName))
			{
				return false;
			}
			return 0 == MVCFunctions.strcasecmp((XVar)(fieldListItem.m_expr.m_strName), (XVar)(field));
		}
	}
	public partial class SQLCountQuery : SQLQuery
	{
		protected static bool skipSQLCountQueryCtor = false;
		public SQLCountQuery(dynamic _param_src)
			:base((XVar)_param_src)
		{
			if(skipSQLCountQueryCtor)
			{
				skipSQLCountQueryCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic src = XVar.Clone(_param_src);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> v in src.GetEnumerator())
			{
				this.InitAndSetArrayItem(v.Value, v.Key);
			}
			this.m_strHead = new XVar("");
			if(XVar.Pack(!(XVar)(HasGroupBy())))
			{
				this.m_fieldlist = XVar.Clone(XVar.Array());
			}
		}
		public override XVar toSql(dynamic _param_strwhere = null, dynamic _param_strorderby = null, dynamic _param_strhaving = null, dynamic _param_oneRecordMode = null, dynamic _param_joinFromPart = null)
		{
			#region default values
			if(_param_strwhere as Object == null) _param_strwhere = new XVar();
			if(_param_strorderby as Object == null) _param_strorderby = new XVar();
			if(_param_strhaving as Object == null) _param_strhaving = new XVar();
			if(_param_oneRecordMode as Object == null) _param_oneRecordMode = new XVar(false);
			if(_param_joinFromPart as Object == null) _param_joinFromPart = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic strwhere = XVar.Clone(_param_strwhere);
			dynamic strorderby = XVar.Clone(_param_strorderby);
			dynamic strhaving = XVar.Clone(_param_strhaving);
			dynamic oneRecordMode = XVar.Clone(_param_oneRecordMode);
			dynamic joinFromPart = XVar.Clone(_param_joinFromPart);
			#endregion

			dynamic ret = null, sql = null;
			sql = XVar.Clone(base.toSql((XVar)(strwhere)));
			if(XVar.Pack(HasGroupBy()))
			{
				ret = XVar.Clone(MVCFunctions.Concat("select count(*) from (", sql, ") a"));
			}
			else
			{
				ret = XVar.Clone(MVCFunctions.Concat("select count(*) from ", sql));
			}
			return ret;
		}
	}
}
