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
	public partial class ViewLookupWizardField : ViewControl
	{
		public dynamic nLookupType;
		public dynamic lookupTable;
		public dynamic displayFieldName;
		public dynamic linkFieldName;
		public ProjectSettings pSet;
		public dynamic lookupPSet;
		public dynamic cipherer;
		public dynamic lookupQueryObj;
		public dynamic displayFieldIndex;
		public dynamic LookupSQL;
		public dynamic resolvedLookupValues = XVar.Array();
		public dynamic resolvedLinkLookupValues = XVar.Array();
		public dynamic linkFieldIndex;
		protected dynamic lookupConnection;
		protected static bool skipViewLookupWizardFieldCtor = false;
		public ViewLookupWizardField(dynamic _param_field, dynamic _param_container, dynamic _param_pageObject)
			:base((XVar)_param_field, (XVar)_param_container, (XVar)_param_pageObject)
		{
			if(skipViewLookupWizardFieldCtor)
			{
				skipViewLookupWizardFieldCtor = false;
				return;
			}
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic container = XVar.Clone(_param_container);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			#endregion

			this.lookupPSet = new XVar(null);
			this.cipherer = new XVar(null);
			this.lookupQueryObj = new XVar(null);
			this.displayFieldIndex = new XVar(0);
			this.linkFieldIndex = new XVar(1);
			this.LookupSQL = new XVar("");
			if(this.container.pSet.getEditFormat((XVar)(field)) != Constants.EDIT_FORMAT_LOOKUP_WIZARD)
			{
				this.pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(this.container.pSet._table)));
				this.pSet.setPage((XVar)(this.container.pageType));
				this.pSet.setPage((XVar)(this.container.pSet.getPageTypeByFieldEditFormat((XVar)(field), new XVar(Constants.EDIT_FORMAT_LOOKUP_WIZARD))));
			}
			else
			{
				this.pSet = XVar.UnPackProjectSettings(this.container.pSet);
			}
			this.nLookupType = XVar.Clone(this.pSet.getLookupType((XVar)(this.field)));
			this.lookupTable = XVar.Clone(this.pSet.getLookupTable((XVar)(this.field)));
			setLookupConnection();
			this.displayFieldName = XVar.Clone(this.pSet.getDisplayField((XVar)(this.field)));
			this.linkFieldName = XVar.Clone(this.pSet.getLinkField((XVar)(this.field)));
			this.linkAndDisplaySame = XVar.Clone(this.displayFieldName == this.linkFieldName);
			if(this.nLookupType == Constants.LT_QUERY)
			{
				dynamic lookupIndexes = XVar.Array();
				this.lookupPSet = XVar.Clone(new ProjectSettings((XVar)(this.lookupTable), (XVar)(this.container.pageType)));
				this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.lookupTable)));
				this.lookupQueryObj = XVar.Clone(this.lookupPSet.getSQLQuery().CloneObject());
				if(XVar.Pack(this.pSet.getCustomDisplay((XVar)(this.field))))
				{
					this.lookupQueryObj.AddCustomExpression((XVar)(this.displayFieldName), (XVar)(this.lookupPSet), (XVar)(this.pSet._table), (XVar)(this.field));
				}
				this.lookupQueryObj.ReplaceFieldsWithDummies((XVar)(this.lookupPSet.getBinaryFieldsIndices()));
				lookupIndexes = XVar.Clone(CommonFunctions.GetLookupFieldsIndexes((XVar)(this.pSet), (XVar)(this.field)));
				this.displayFieldIndex = XVar.Clone(lookupIndexes["displayFieldIndex"]);
				this.linkFieldIndex = XVar.Clone(lookupIndexes["linkFieldIndex"]);
			}
			else
			{
				this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.pSet._table)));
				this.LookupSQL = new XVar("SELECT ");
				this.LookupSQL = MVCFunctions.Concat(this.LookupSQL, RunnerPage.sqlFormattedDisplayField((XVar)(this.field), (XVar)(this.lookupConnection), (XVar)(this.pSet)));
				this.LookupSQL = MVCFunctions.Concat(this.LookupSQL, ", ", this.lookupConnection.addFieldWrappers((XVar)(this.pSet.getLinkField((XVar)(this.field)))));
				this.LookupSQL = MVCFunctions.Concat(this.LookupSQL, " FROM ", this.lookupConnection.addTableWrappers((XVar)(this.lookupTable)), " WHERE ");
			}
			this.localControlsContainer = XVar.Clone(new ViewControlsContainer((XVar)(this.pSet), (XVar)(this.container.pageType), (XVar)(pageObject)));
			this.localControlsContainer.isLocal = new XVar(true);
		}
		protected virtual XVar setLookupConnection()
		{
			dynamic connId = null;
			if(this.nLookupType == Constants.LT_QUERY)
			{
				this.lookupConnection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.lookupTable)));
				return null;
			}
			connId = XVar.Clone(this.pSet.getNotProjectLookupTableConnId((XVar)(this.field)));
			this.lookupConnection = XVar.Clone((XVar.Pack(MVCFunctions.strlen((XVar)(connId))) ? XVar.Pack(GlobalVars.cman.byId((XVar)(connId))) : XVar.Pack(GlobalVars.cman.getDefault())));
			return null;
		}
		protected virtual XVar getDbPreparedValuesList(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic listValues = XVar.Array(), numeric = null, values = XVar.Array(), var_type = null;
			if(XVar.Pack(!(XVar)(this.pSet.multiSelect((XVar)(this.field)))))
			{
				return "";
			}
			values = XVar.Clone(CommonFunctions.splitvalues((XVar)(value)));
			var_type = XVar.Clone(this.pSet.getLWLinkFieldType((XVar)(this.field)));
			numeric = new XVar(true);
			if(XVar.Pack(!(XVar)(var_type)))
			{
				foreach (KeyValuePair<XVar, dynamic> val in values.GetEnumerator())
				{
					if((XVar)(MVCFunctions.strlen((XVar)(val.Value)))  && (XVar)(!(XVar)(MVCFunctions.IsNumeric(val.Value))))
					{
						numeric = new XVar(false);
						break;
					}
				}
			}
			else
			{
				numeric = XVar.Clone(!(XVar)(CommonFunctions.NeedQuotes((XVar)(var_type))));
			}
			listValues = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> val in values.GetEnumerator())
			{
				if((XVar)(numeric)  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(val.Value)))))
				{
					continue;
				}
				if(XVar.Pack(numeric))
				{
					listValues.InitAndSetArrayItem(val.Value + 0, null);
				}
				else
				{
					dynamic fName = null;
					fName = XVar.Clone((XVar.Pack(this.nLookupType == Constants.LT_QUERY) ? XVar.Pack(this.linkFieldName) : XVar.Pack(this.field)));
					listValues.InitAndSetArrayItem(this.lookupConnection.prepareString((XVar)(this.cipherer.EncryptField((XVar)(fName), (XVar)(val.Value)))), null);
				}
			}
			return MVCFunctions.implode(new XVar(","), (XVar)(listValues));
		}
		protected virtual XVar getMultiselectLookupResolvingSQL(dynamic _param_value, dynamic _param_in, dynamic _param_withoutWhere = null)
		{
			#region default values
			if(_param_withoutWhere as Object == null) _param_withoutWhere = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic var_in = XVar.Clone(_param_in);
			dynamic withoutWhere = XVar.Clone(_param_withoutWhere);
			#endregion

			dynamic LookupSQL = null, where = null;
			if(XVar.Pack(!(XVar)(this.pSet.multiSelect((XVar)(this.field)))))
			{
				return "";
			}
			where = XVar.Clone(CommonFunctions.prepareLookupWhere((XVar)(this.field), (XVar)(this.pSet)));
			if(this.nLookupType == Constants.LT_QUERY)
			{
				dynamic inWhere = null;
				inWhere = XVar.Clone(MVCFunctions.Concat(RunnerPage._getFieldSQLDecrypt((XVar)(this.linkFieldName), (XVar)(this.lookupConnection), (XVar)(this.lookupPSet), (XVar)(this.cipherer)), " in (", var_in, ")"));
				if((XVar)(!(XVar)(withoutWhere))  && (XVar)(MVCFunctions.strlen((XVar)(where))))
				{
					inWhere = MVCFunctions.Concat(inWhere, " and (", where, ")");
				}
				LookupSQL = XVar.Clone(this.lookupQueryObj.buildSQL_default((XVar)(inWhere)));
			}
			else
			{
				LookupSQL = XVar.Clone(MVCFunctions.Concat(this.LookupSQL, this.lookupConnection.addFieldWrappers((XVar)(this.pSet.getLinkField((XVar)(this.field)))), " in (", var_in, ")"));
				if((XVar)(!(XVar)(withoutWhere))  && (XVar)(MVCFunctions.strlen((XVar)(where))))
				{
					LookupSQL = MVCFunctions.Concat(LookupSQL, " and (", where, ")");
				}
			}
			return LookupSQL;
		}
		protected virtual XVar getNotMultiselectLookupResolvingSQL(dynamic _param_value, dynamic _param_withoutWhere)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic withoutWhere = XVar.Clone(_param_withoutWhere);
			#endregion

			dynamic LookupSQL = null, strWhere = null, strdata = null, where = null;
			if(XVar.Pack(this.pSet.multiSelect((XVar)(this.field))))
			{
				return "";
			}
			where = XVar.Clone(CommonFunctions.prepareLookupWhere((XVar)(this.field), (XVar)(this.pSet)));
			strdata = XVar.Clone(this.cipherer.MakeDBValue((XVar)((XVar.Pack(this.nLookupType == Constants.LT_QUERY) ? XVar.Pack(this.linkFieldName) : XVar.Pack(this.field))), (XVar)(value), new XVar(""), new XVar(true)));
			if(this.nLookupType == Constants.LT_QUERY)
			{
				strWhere = XVar.Clone(MVCFunctions.Concat(CommonFunctions.GetFullFieldName((XVar)(this.linkFieldName), (XVar)(this.lookupTable), new XVar(false)), " = ", strdata));
				if((XVar)(!(XVar)(withoutWhere))  && (XVar)(MVCFunctions.strlen((XVar)(where))))
				{
					strWhere = MVCFunctions.Concat(strWhere, " and (", where, ")");
				}
				LookupSQL = XVar.Clone(this.lookupQueryObj.buildSQL_default((XVar)(strWhere)));
			}
			else
			{
				strWhere = XVar.Clone(MVCFunctions.Concat(this.lookupConnection.addFieldWrappers((XVar)(this.pSet.getLinkField((XVar)(this.field)))), " = ", strdata));
				if((XVar)(!(XVar)(withoutWhere))  && (XVar)(MVCFunctions.strlen((XVar)(where))))
				{
					strWhere = MVCFunctions.Concat(strWhere, " and (", where, ")");
				}
				LookupSQL = XVar.Clone(MVCFunctions.Concat(this.LookupSQL, strWhere));
			}
			return LookupSQL;
		}
		protected virtual XVar getDecryptLookupValue(dynamic _param_lookupValue)
		{
			#region pass-by-value parameters
			dynamic lookupValue = XVar.Clone(_param_lookupValue);
			#endregion

			if((XVar)(this.nLookupType == Constants.LT_QUERY)  || (XVar)(this.linkAndDisplaySame))
			{
				return this.cipherer.DecryptField((XVar)((XVar.Pack(this.nLookupType == Constants.LT_QUERY) ? XVar.Pack(this.displayFieldName) : XVar.Pack(this.field))), (XVar)(lookupValue));
			}
			return lookupValue;
		}
		protected virtual XVar getMultiselectLookupValues(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic LookupSQL = null, displayValue = null, i = null, lookupArr = XVar.Array(), lookupValues = XVar.Array(), lookuprow = XVar.Array(), qResult = null, var_in = null, withoutWhere = null;
			var_in = XVar.Clone(getDbPreparedValuesList((XVar)(value)));
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(var_in)))))
			{
				return new XVar(0, value);
			}
			if(XVar.Pack(MVCFunctions.count(this.resolvedLookupValues[value])))
			{
				return this.resolvedLookupValues[value];
			}
			withoutWhere = new XVar(false);
			i = new XVar(0);
			for(;i < 2; i++)
			{
				LookupSQL = XVar.Clone(getMultiselectLookupResolvingSQL((XVar)(value), (XVar)(var_in), (XVar)(withoutWhere)));
				CommonFunctions.LogInfo((XVar)(LookupSQL));
				lookupArr = XVar.Clone(XVar.Array());
				qResult = XVar.Clone(this.lookupConnection.query((XVar)(LookupSQL)));
				while(XVar.Pack(lookuprow = XVar.Clone(qResult.fetchNumeric())))
				{
					displayValue = XVar.Clone(lookuprow[this.displayFieldIndex]);
					lookupArr.InitAndSetArrayItem(displayValue, null);
					this.resolvedLinkLookupValues.InitAndSetArrayItem(lookuprow[this.linkFieldIndex], value, displayValue);
				}
				if(MVCFunctions.count(lookupArr) == MVCFunctions.count(MVCFunctions.explode(new XVar(","), (XVar)(var_in))))
				{
					break;
				}
				withoutWhere = new XVar(true);
			}
			lookupValues = XVar.Clone(XVar.Array());
			lookupArr = XVar.Clone(MVCFunctions.array_unique((XVar)(lookupArr)));
			foreach (KeyValuePair<XVar, dynamic> lookupvalue in lookupArr.GetEnumerator())
			{
				lookupValues.InitAndSetArrayItem(getDecryptLookupValue((XVar)(lookupvalue.Value)), null);
			}
			if(XVar.Pack(MVCFunctions.count(lookupValues)))
			{
				this.resolvedLookupValues.InitAndSetArrayItem(lookupValues, value);
			}
			return lookupValues;
		}
		protected virtual XVar getNotMultiselectLookupValues(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic LookupSQL = null, i = null, lookuprow = XVar.Array(), lookupvalue = null, qResult = null, withoutWhere = null;
			if(XVar.Pack(this.resolvedLookupValues.KeyExists(value)))
			{
				return new XVar(0, this.resolvedLookupValues[value]);
			}
			lookupvalue = XVar.Clone(value);
			withoutWhere = new XVar(false);
			i = new XVar(0);
			for(;i < 2; i++)
			{
				LookupSQL = XVar.Clone(getNotMultiselectLookupResolvingSQL((XVar)(value), (XVar)(withoutWhere)));
				CommonFunctions.LogInfo((XVar)(LookupSQL));
				qResult = XVar.Clone(this.lookupConnection.query((XVar)(LookupSQL)));
				if(XVar.Pack(lookuprow = XVar.Clone(qResult.fetchNumeric())))
				{
					lookupvalue = XVar.Clone(getDecryptLookupValue((XVar)(lookuprow[this.displayFieldIndex])));
					break;
				}
				withoutWhere = new XVar(true);
			}
			this.resolvedLookupValues.InitAndSetArrayItem(lookupvalue, value);
			return new XVar(0, lookupvalue);
		}
		protected virtual XVar getLookupValues(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(XVar.Pack(this.pSet.multiSelect((XVar)(this.field))))
			{
				return getMultiselectLookupValues((XVar)(value));
			}
			return getNotMultiselectLookupValues((XVar)(value));
		}
		public override XVar showDBValue(dynamic data, dynamic _param_keylink)
		{
			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			dynamic localData = XVar.Array(), lookupValues = XVar.Array(), outValues = XVar.Array(), value = null;
			value = XVar.Clone(data[this.field]);
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(value)))))
			{
				return "";
			}
			outValues = XVar.Clone(XVar.Array());
			localData = XVar.Clone(data);
			lookupValues = XVar.Clone(getLookupValues((XVar)(value)));
			foreach (KeyValuePair<XVar, dynamic> lookupvalue in lookupValues.GetEnumerator())
			{
				this.localControlsContainer.linkFieldValues.InitAndSetArrayItem(data[this.field], this.field);
				if((XVar)(this.resolvedLinkLookupValues.KeyExists(value))  && (XVar)(this.resolvedLinkLookupValues[value].KeyExists(lookupvalue.Value)))
				{
					this.localControlsContainer.originlinkValues.InitAndSetArrayItem(this.resolvedLinkLookupValues[value][lookupvalue.Value], this.field);
				}
				if(this.pSet.getViewFormat((XVar)(this.field)) != "Custom")
				{
					localData.InitAndSetArrayItem(lookupvalue.Value, this.field);
				}
				outValues.InitAndSetArrayItem(this.localControlsContainer.showDBValue((XVar)(this.field), (XVar)(localData), (XVar)(keylink), (XVar)(lookupvalue.Value)), null);
			}
			return MVCFunctions.implode(new XVar(","), (XVar)(outValues));
		}
		public override XVar getTextValue(dynamic data)
		{
			dynamic localData = XVar.Array(), lookupValues = XVar.Array(), textValues = XVar.Array(), value = null;
			value = XVar.Clone(data[this.field]);
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(value)))))
			{
				return "";
			}
			textValues = XVar.Clone(XVar.Array());
			localData = XVar.Clone(data);
			lookupValues = XVar.Clone(getLookupValues((XVar)(value)));
			foreach (KeyValuePair<XVar, dynamic> lookupvalue in lookupValues.GetEnumerator())
			{
				if(this.pSet.getViewFormat((XVar)(this.field)) != "Custom")
				{
					localData.InitAndSetArrayItem(lookupvalue.Value, this.field);
				}
				textValues.InitAndSetArrayItem(this.localControlsContainer.getControl((XVar)(this.field)).getTextValue((XVar)(localData)), null);
			}
			return MVCFunctions.implode(new XVar(","), (XVar)(textValues));
		}
		public override XVar getExportValue(dynamic data, dynamic _param_keylink = null)
		{
			#region default values
			if(_param_keylink as Object == null) _param_keylink = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic keylink = XVar.Clone(_param_keylink);
			#endregion

			this.localControlsContainer.setForExportVar((XVar)(this.container.forExport));
			if(this.container.forExport == "csv")
			{
				return data[this.field];
			}
			return showDBValue((XVar)(data), (XVar)(keylink));
		}
	}
}
