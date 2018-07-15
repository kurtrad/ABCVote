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
	public partial class RunnerContextItem : XClass
	{
		public dynamic var_type;
		public dynamic pageObj;
		public dynamic data;
		public dynamic oldData;
		public dynamic newData;
		public dynamic masterData;
		public RunnerContextItem(dynamic _param_type, dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			this.var_type = XVar.Clone(var_type);
		}
		public virtual XVar getType()
		{
			return this.var_type;
		}
		public virtual XVar getValues()
		{
			if(XVar.Pack(this.data))
			{
				return this.data;
			}
			if(XVar.Pack(this.pageObj))
			{
				return this.pageObj.getCurrentRecord();
			}
			return XVar.Array();
		}
		public virtual XVar getFieldValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic data = null;
			data = XVar.Clone(getValues());
			return CommonFunctions.getArrayElementNC((XVar)(data), (XVar)(field));
		}
		public virtual XVar getOldValues()
		{
			if(XVar.Pack(this.oldData))
			{
				return this.oldData;
			}
			if(XVar.Pack(this.pageObj))
			{
				return this.pageObj.getOldRecordData();
			}
			return XVar.Array();
		}
		public virtual XVar getOldFieldValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic oldData = null;
			oldData = XVar.Clone(getOldValues());
			return CommonFunctions.getArrayElementNC((XVar)(oldData), (XVar)(field));
		}
		public virtual XVar getNewFieldValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(this.newData))
			{
				return CommonFunctions.getArrayElementNC((XVar)(this.newData), (XVar)(field));
			}
			return getFieldValue((XVar)(field));
		}
		public virtual XVar getMasterValues()
		{
			if(XVar.Pack(this.masterData))
			{
				return this.masterData;
			}
			if(XVar.Pack(this.pageObj))
			{
				return this.pageObj.getMasterRecord();
			}
			return XVar.Array();
		}
		public virtual XVar getMasterFieldValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic masterData = null;
			masterData = XVar.Clone(getMasterValues());
			return CommonFunctions.getArrayElementNC((XVar)(masterData), (XVar)(field));
		}
		public virtual XVar getUserValue(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			return CommonFunctions.getArrayElementNC((XVar)(Security.currentUserData()), (XVar)(key));
		}
		public virtual XVar getSessionValue(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			return CommonFunctions.getSessionElementNC((XVar)(key));
		}
		public virtual XVar getValue(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic dotPos = null, prefix = null;
			prefix = new XVar("");
			dotPos = XVar.Clone(MVCFunctions.strpos((XVar)(key), new XVar(".")));
			if(!XVar.Equals(XVar.Pack(dotPos), XVar.Pack(false)))
			{
				prefix = XVar.Clone(MVCFunctions.strtolower((XVar)(MVCFunctions.substr((XVar)(key), new XVar(0), (XVar)(dotPos)))));
				key = XVar.Clone(MVCFunctions.substr((XVar)(key), (XVar)(dotPos + 1)));
			}
			if(prefix == "master")
			{
				return getMasterFieldValue((XVar)(key));
			}
			if(prefix == "session")
			{
				return getSessionValue((XVar)(key));
			}
			if(prefix == "user")
			{
				return getUserValue((XVar)(key));
			}
			if(prefix == "old")
			{
				return getOldFieldValue((XVar)(key));
			}
			if(prefix == "new")
			{
				return getNewFieldValue((XVar)(key));
			}
			return getFieldValue((XVar)(key));
		}
	}
	public partial class RunnerContext : XClass
	{
		protected dynamic stack = XVar.Array();
		public RunnerContext()
		{
			dynamic context = null;
			context = XVar.Clone(new RunnerContextItem(new XVar(Constants.CONTEXT_GLOBAL), (XVar)(XVar.Array())));
			this.stack.InitAndSetArrayItem(context, MVCFunctions.count(this.stack));
		}
		public static XVar push(dynamic _param_context)
		{
			#region pass-by-value parameters
			dynamic context = XVar.Clone(_param_context);
			#endregion

			GlobalVars.contextStack.stack.InitAndSetArrayItem(context, MVCFunctions.count(GlobalVars.contextStack.stack));
			return null;
		}
		public static XVar current()
		{
			return GlobalVars.contextStack.stack[MVCFunctions.count(GlobalVars.contextStack.stack) - 1];
		}
		public static XVar pop()
		{
			dynamic context = null;
			if(XVar.Pack(!(XVar)(MVCFunctions.count(GlobalVars.contextStack.stack))))
			{
				return null;
			}
			context = XVar.Clone(GlobalVars.contextStack.stack[MVCFunctions.count(GlobalVars.contextStack.stack) - 1]);
			GlobalVars.contextStack.stack.Remove(MVCFunctions.count(GlobalVars.contextStack.stack) - 1);
			return context;
		}
		public static XVar pushPageContext(dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			push((XVar)(new RunnerContextItem(new XVar(Constants.CONTEXT_PAGE), (XVar)(new XVar("pageObj", pageObj)))));
			return null;
		}
		public static XVar pushRecordContext(dynamic _param_record, dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic record = XVar.Clone(_param_record);
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			push((XVar)(new RunnerContextItem(new XVar(Constants.CONTEXT_ROW), (XVar)(new XVar("pageObj", pageObj, "data", record)))));
			return null;
		}
		public static XVar getMasterValues()
		{
			dynamic ctx = null;
			ctx = XVar.Clone(current());
			return ctx.getMasterValues();
		}
		public static XVar getValues()
		{
			dynamic ctx = null;
			ctx = XVar.Clone(current());
			return ctx.getValues();
		}
	}
	public partial class TempContext : XClass
	{
		public TempContext(dynamic _param_context)
		{
			#region pass-by-value parameters
			dynamic context = XVar.Clone(_param_context);
			#endregion

			RunnerContext.push((XVar)(context));
		}
		public virtual XVar __destruct()
		{
			RunnerContext.pop();
			return null;
		}
	}
	public partial class TempPageContext : XClass
	{
		public TempPageContext(dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			RunnerContext.push((XVar)(pageObj.standaloneContext));
		}
		public virtual XVar __destruct()
		{
			RunnerContext.pop();
			return null;
		}
	}
}
