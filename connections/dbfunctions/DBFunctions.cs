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
	public partial class DBFunctions : XClass
	{
		protected dynamic strLeftWrapper;
		protected dynamic strRightWrapper;
		protected dynamic escapeChars = XVar.Array();
		public DBFunctions(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			this.strLeftWrapper = new XVar("\"");
			this.strRightWrapper = new XVar("\"");
			this.escapeChars.InitAndSetArrayItem(true, "'");
			if((XVar)(var_params.KeyExists("leftWrap"))  && (XVar)(var_params.KeyExists("rightWrap")))
			{
				this.strLeftWrapper = XVar.Clone(var_params["leftWrap"]);
				this.strRightWrapper = XVar.Clone(var_params["rightWrap"]);
			}
		}
		public virtual XVar addTableWrappers(dynamic _param_strName)
		{
			#region pass-by-value parameters
			dynamic strName = XVar.Clone(_param_strName);
			#endregion

			dynamic arr = XVar.Array(), ret = null;
			if(MVCFunctions.substr((XVar)(strName), new XVar(0), new XVar(1)) == this.strLeftWrapper)
			{
				return strName;
			}
			arr = XVar.Clone(MVCFunctions.explode(new XVar("."), (XVar)(strName)));
			ret = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> e in arr.GetEnumerator())
			{
				if(ret != XVar.Pack(""))
				{
					ret = MVCFunctions.Concat(ret, ".");
				}
				ret = MVCFunctions.Concat(ret, this.strLeftWrapper, e.Value, this.strRightWrapper);
			}
			return ret;
		}
		public virtual XVar schemaSupported()
		{
			return true;
		}
		public virtual XVar crossDbSupported()
		{
			return true;
		}
		public virtual XVar getTableNameComponents(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			dynamic components = XVar.Array(), parts = XVar.Array(), strippedTableName = null;
			strippedTableName = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, this.strLeftWrapper, 1, this.strRightWrapper)), new XVar(""), (XVar)(name)));
			parts = XVar.Clone(MVCFunctions.explode(new XVar("."), (XVar)(strippedTableName)));
			components = XVar.Clone(XVar.Array());
			components.InitAndSetArrayItem(name, "fullName");
			if((XVar)((XVar)(MVCFunctions.count(parts) == 3)  && (XVar)(crossDbSupported()))  && (XVar)(schemaSupported()))
			{
				components.InitAndSetArrayItem(parts[0], "db");
				components.InitAndSetArrayItem(parts[1], "schema");
				components.InitAndSetArrayItem(parts[2], "table");
			}
			else
			{
				if((XVar)(MVCFunctions.count(parts) == 2)  && (XVar)((XVar)(crossDbSupported())  || (XVar)(schemaSupported())))
				{
					if(XVar.Pack(schemaSupported()))
					{
						components.InitAndSetArrayItem(parts[0], "schema");
					}
					else
					{
						components.InitAndSetArrayItem(parts[0], "db");
					}
					components.InitAndSetArrayItem(parts[1], "table");
				}
				else
				{
					components.InitAndSetArrayItem(name, "table");
				}
			}
			return components;
		}
		public virtual XVar getInsertedIdSQL(dynamic _param_key = null, dynamic _param_table = null, dynamic _param_oraSequenceName = null)
		{
			#region default values
			if(_param_key as Object == null) _param_key = new XVar();
			if(_param_table as Object == null) _param_table = new XVar();
			if(_param_oraSequenceName as Object == null) _param_oraSequenceName = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			dynamic table = XVar.Clone(_param_table);
			dynamic oraSequenceName = XVar.Clone(_param_oraSequenceName);
			#endregion

			return MVCFunctions.Concat("SELECT MAX(", addFieldWrappers((XVar)(key)), ") FROM ", addTableWrappers((XVar)(table)));
		}
		public virtual XVar timeToSecWrapper(dynamic _param_strName)
		{
			#region pass-by-value parameters
			dynamic strName = XVar.Clone(_param_strName);
			#endregion

			return addTableWrappers((XVar)(strName));
		}
		public virtual XVar prepareString(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return MVCFunctions.Concat("'", addSlashes((XVar)(str)), "'");
		}
		public virtual XVar addSlashes(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return MVCFunctions.str_replace(new XVar("'"), new XVar("''"), (XVar)(str));
		}
		public virtual XVar escapeString(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return addSlashes((XVar)(str));
		}
		public virtual XVar addFieldWrappers(dynamic _param_strName)
		{
			#region pass-by-value parameters
			dynamic strName = XVar.Clone(_param_strName);
			#endregion

			if(MVCFunctions.substr((XVar)(strName), new XVar(0), new XVar(1)) == this.strLeftWrapper)
			{
				return strName;
			}
			return MVCFunctions.Concat(this.strLeftWrapper, strName, this.strRightWrapper);
		}
		public virtual XVar stripSlashesBinary(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return str;
		}
		public virtual XVar positionQuoted(dynamic _param_sql, dynamic _param_pos)
		{
			#region pass-by-value parameters
			dynamic sql = XVar.Clone(_param_sql);
			dynamic pos = XVar.Clone(_param_pos);
			#endregion

			dynamic afterEscape = null, c = null, i = null, inQuote = null;
			inQuote = new XVar(false);
			afterEscape = new XVar(false);
			i = new XVar(0);
			for(;i < pos; i++)
			{
				c = XVar.Clone(MVCFunctions.substr((XVar)(sql), (XVar)(i), new XVar(1)));
				if(XVar.Pack(!(XVar)(afterEscape)))
				{
					if(c == "'")
					{
						inQuote = XVar.Clone(!(XVar)(inQuote));
					}
					else
					{
						afterEscape = XVar.Clone((XVar)(!(XVar)(inQuote))  && (XVar)(this.escapeChars.KeyExists(c)));
					}
				}
				else
				{
					afterEscape = new XVar(false);
				}
			}
			return inQuote;
		}
		public virtual XVar escapeLIKEpattern(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return str;
		}
		public virtual XVar addSlashesBinary(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return addSlashes((XVar)(str));
		}
		public virtual XVar upper(dynamic _param_dbval)
		{
			#region pass-by-value parameters
			dynamic dbval = XVar.Clone(_param_dbval);
			#endregion

			return dbval;
		}
		public virtual XVar addDateQuotes(dynamic _param_val)
		{
			#region pass-by-value parameters
			dynamic val = XVar.Clone(_param_val);
			#endregion

			if((XVar)(val == XVar.Pack(""))  || (XVar)(XVar.Equals(XVar.Pack(val), XVar.Pack(null))))
			{
				return "null";
			}
			return MVCFunctions.Concat("'", addSlashes((XVar)(val)), "'");
		}
		public virtual XVar field2char(dynamic _param_value, dynamic _param_type = null)
		{
			#region default values
			if(_param_type as Object == null) _param_type = new XVar(3);
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			return value;
		}
		public virtual XVar field2time(dynamic _param_value, dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			return value;
		}
	}
}
