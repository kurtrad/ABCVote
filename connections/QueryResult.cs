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
	public partial class QueryResult : XClass
	{
		protected dynamic connectionObj;
		protected dynamic handle;
		protected dynamic data;
		protected dynamic fieldNames = XVar.Array();
		protected dynamic upperMap = XVar.Array();
		protected dynamic fieldMap = XVar.Array();
		protected dynamic state = XVar.Pack(-1);
		public QueryResult(dynamic _param_connectionObj, dynamic _param_qHandle)
		{
			#region pass-by-value parameters
			dynamic connectionObj = XVar.Clone(_param_connectionObj);
			dynamic qHandle = XVar.Clone(_param_qHandle);
			#endregion

			this.connectionObj = XVar.Clone(connectionObj);
			this.handle = XVar.Clone(qHandle);
		}
		public virtual XVar getQueryHandle()
		{
			return this.handle;
		}
		public virtual XVar fetchAssoc()
		{
			dynamic ret = null;
			if(this.state == 1)
			{
				return null;
			}
			if(this.state == 0)
			{
				this.state = XVar.Clone(-1);
				return numericToAssoc((XVar)(this.data));
			}
			ret = XVar.Clone(this.connectionObj.fetch_array((XVar)(this.handle)));
			this.state = XVar.Clone((XVar.Pack(ret) ? XVar.Pack(-1) : XVar.Pack(1)));
			return ret;
		}
		public virtual XVar fetchNumeric()
		{
			dynamic ret = null;
			if(this.state == 1)
			{
				return null;
			}
			if(this.state == 0)
			{
				this.state = XVar.Clone(-1);
				return this.data;
			}
			ret = XVar.Clone(this.connectionObj.fetch_numarray((XVar)(this.handle)));
			this.state = XVar.Clone((XVar.Pack(ret) ? XVar.Pack(-1) : XVar.Pack(1)));
			return ret;
		}
		public virtual XVar closeQuery()
		{
			this.connectionObj.closeQuery((XVar)(this.handle));
			return null;
		}
		public virtual XVar numFields()
		{
			return this.connectionObj.num_fields((XVar)(this.handle));
		}
		public virtual XVar fieldName(dynamic _param_offset)
		{
			#region pass-by-value parameters
			dynamic offset = XVar.Clone(_param_offset);
			#endregion

			return this.connectionObj.field_name((XVar)(this.handle), (XVar)(offset));
		}
		public virtual XVar seekPage(dynamic _param_pageSize, dynamic _param_pageStart)
		{
			#region pass-by-value parameters
			dynamic pageSize = XVar.Clone(_param_pageSize);
			dynamic pageStart = XVar.Clone(_param_pageStart);
			#endregion

			this.connectionObj.seekPage((XVar)(this.handle), (XVar)(pageSize), (XVar)(pageStart));
			return null;
		}
		public virtual XVar eof()
		{
			prepareRecord();
			return this.state == 1;
		}
		protected virtual XVar internalFetch()
		{
			if(this.state == 1)
			{
				return null;
			}
			fillColumnNames();
			this.data = XVar.Clone(this.connectionObj.fetch_numarray((XVar)(this.handle)));
			this.state = XVar.Clone((XVar.Pack(this.data) ? XVar.Pack(0) : XVar.Pack(1)));
			return null;
		}
		protected virtual XVar numericToAssoc(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic i = null, nFields = null, ret = XVar.Array();
			ret = XVar.Clone(XVar.Array());
			nFields = XVar.Clone(numFields());
			i = new XVar(0);
			for(;i < nFields; ++(i))
			{
				ret.InitAndSetArrayItem(data[i], this.fieldNames[i]);
			}
			return ret;
		}
		protected virtual XVar fillColumnNames()
		{
			dynamic fname = null, i = null, nFields = null;
			if(XVar.Pack(this.fieldNames))
			{
				return null;
			}
			nFields = XVar.Clone(numFields());
			i = new XVar(0);
			for(;i < nFields; ++(i))
			{
				fname = XVar.Clone(fieldName((XVar)(i)));
				this.fieldNames.InitAndSetArrayItem(fname, null);
				this.fieldMap.InitAndSetArrayItem(i, fname);
				this.upperMap.InitAndSetArrayItem(i, MVCFunctions.strtoupper((XVar)(fname)));
			}
			return null;
		}
		public virtual XVar func_next()
		{
			prepareRecord();
			internalFetch();
			return null;
		}
		protected virtual XVar prepareRecord()
		{
			if(this.state == -1)
			{
				internalFetch();
			}
			return this.state != 1;
		}
		public virtual XVar value(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(!(XVar)(prepareRecord())))
			{
				return null;
			}
			if(XVar.Pack(MVCFunctions.IsNumeric(field)))
			{
				return this.data[field];
			}
			if(XVar.Pack(this.fieldMap.KeyExists(field)))
			{
				return this.data[this.fieldMap[field]];
			}
			if(XVar.Pack(this.upperMap.KeyExists(MVCFunctions.strtoupper((XVar)(field)))))
			{
				return this.data[this.upperMap[MVCFunctions.strtoupper((XVar)(field))]];
			}
			return null;
		}
		public virtual XVar getData()
		{
			if(XVar.Pack(!(XVar)(prepareRecord())))
			{
				return null;
			}
			return numericToAssoc((XVar)(this.data));
		}
		public virtual XVar getNumData()
		{
			if(XVar.Pack(!(XVar)(prepareRecord())))
			{
				return null;
			}
			return this.data;
		}
	}
}
