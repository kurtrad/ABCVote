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
	public partial class MSSQLFunctions : DBFunctions
	{
		protected static bool skipMSSQLFunctionsCtor = false;
		public MSSQLFunctions(dynamic _param_params)
			:base((XVar)_param_params)
		{
			if(skipMSSQLFunctionsCtor)
			{
				skipMSSQLFunctionsCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			this.strLeftWrapper = new XVar("[");
			this.strRightWrapper = new XVar("]");
		}
		public override XVar escapeLIKEpattern(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return str;
		}
		public override XVar prepareString(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return MVCFunctions.Concat("N'", addSlashes((XVar)(str)), "'");
		}
		public override XVar addSlashesBinary(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			return MVCFunctions.Concat("0x", MVCFunctions.bin2hex((XVar)(str)));
		}
		public override XVar addFieldWrappers(dynamic _param_strName)
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
		public override XVar upper(dynamic _param_dbval)
		{
			#region pass-by-value parameters
			dynamic dbval = XVar.Clone(_param_dbval);
			#endregion

			return MVCFunctions.Concat("upper(", dbval, ")");
		}
		public override XVar addDateQuotes(dynamic _param_val)
		{
			#region pass-by-value parameters
			dynamic val = XVar.Clone(_param_val);
			#endregion

			if((XVar)(val == XVar.Pack(""))  || (XVar)(XVar.Equals(XVar.Pack(val), XVar.Pack(null))))
			{
				return "null";
			}
			return MVCFunctions.Concat("convert(datetime,'", addSlashes((XVar)(val)), "',120)");
		}
		public override XVar field2char(dynamic _param_value, dynamic _param_type = null)
		{
			#region default values
			if(_param_type as Object == null) _param_type = new XVar(3);
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			if(XVar.Pack(CommonFunctions.IsCharType((XVar)(var_type))))
			{
				return value;
			}
			if(XVar.Pack(!(XVar)(CommonFunctions.IsDateFieldType((XVar)(var_type)))))
			{
				return MVCFunctions.Concat("convert(varchar(250),", value, ")");
			}
			return MVCFunctions.Concat("convert(varchar(50),", value, ", 120)");
		}
		public override XVar field2time(dynamic _param_value, dynamic _param_type)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_type = XVar.Clone(_param_type);
			#endregion

			return value;
		}
		public override XVar getInsertedIdSQL(dynamic _param_key = null, dynamic _param_table = null, dynamic _param_oraSequenceName = null)
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

			return "SELECT @@IDENTITY";
		}
		public override XVar timeToSecWrapper(dynamic _param_strName)
		{
			#region pass-by-value parameters
			dynamic strName = XVar.Clone(_param_strName);
			#endregion

			dynamic wrappedFieldName = null;
			wrappedFieldName = XVar.Clone(addTableWrappers((XVar)(strName)));
			return MVCFunctions.Concat("(DATEPART(HOUR, ", wrappedFieldName, ") * 3600) + (DATEPART(MINUTE, ", wrappedFieldName, ") * 60) + (DATEPART(SECOND, ", wrappedFieldName, "))");
		}
	}
}
