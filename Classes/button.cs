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
	public partial class Button : XClass
	{
		public dynamic keys = XVar.Array();
		public dynamic currentKeys = XVar.Array();
		public dynamic selectedKeys = XVar.Array();
		public dynamic isManyKeys = XVar.Pack(false);
		public dynamic isGetNext = XVar.Pack(false);
		public dynamic location = XVar.Pack("");
		public dynamic nextInd;
		public Button(dynamic var_params)
		{
			CommonFunctions.RunnerApply(this, (XVar)(var_params));
			this.nextInd = new XVar(0);
			modifyKeys();
			separateKeys();
		}
		public virtual XVar separateKeys()
		{
			if(this.location == "grid")
			{
				if(XVar.Pack(this.isManyKeys))
				{
					dynamic i = null;
					this.currentKeys = XVar.Clone(this.keys[0]);
					i = new XVar(1);
					for(;i < MVCFunctions.count(this.keys); i++)
					{
						this.selectedKeys.InitAndSetArrayItem(this.keys[i], i - 1);
					}
				}
				else
				{
					this.currentKeys = XVar.Clone(this.keys);
				}
			}
			if(this.location == Constants.PAGE_LIST)
			{
				this.selectedKeys = XVar.Clone(this.keys);
				this.currentKeys = XVar.Clone(this.keys);
			}
			if((XVar)(this.location == Constants.PAGE_EDIT)  || (XVar)(this.location == Constants.PAGE_VIEW))
			{
				this.currentKeys = XVar.Clone(this.keys);
			}
			return null;
		}
		public virtual XVar modifyKeys()
		{
			dynamic keys = XVar.Array();
			keys = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.count(this.keys)))
			{
				dynamic j = null, tKeysNamesArr = XVar.Array();
				tKeysNamesArr = XVar.Clone(GlobalVars.gSettings.getTableKeys());
				if(XVar.Pack(this.isManyKeys))
				{
					foreach (KeyValuePair<XVar, dynamic> value in this.keys.GetEnumerator())
					{
						dynamic recKeyArr = XVar.Array();
						keys.InitAndSetArrayItem(XVar.Array(), value.Key);
						recKeyArr = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(value.Value)));
						j = new XVar(0);
						for(;j < MVCFunctions.count(tKeysNamesArr); j++)
						{
							if(XVar.Pack(recKeyArr.KeyExists(j)))
							{
								keys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(recKeyArr[j])), value.Key, tKeysNamesArr[j]);
							}
						}
					}
				}
				else
				{
					if(XVar.Pack(MVCFunctions.count(this.keys)))
					{
						j = new XVar(0);
						for(;j < MVCFunctions.count(tKeysNamesArr); j++)
						{
							keys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(this.keys[j])), tKeysNamesArr[j]);
						}
					}
				}
			}
			this.keys = XVar.Clone(keys);
			return null;
		}
		public virtual XVar getKeys()
		{
			return this.keys;
		}
		public virtual XVar getCurrentRecord()
		{
			return getRecordData();
		}
		public virtual XVar getNextSelectedRecord()
		{
			if(this.nextInd < MVCFunctions.count(this.selectedKeys))
			{
				this.isGetNext = new XVar(true);
				return getRecordData();
			}
			else
			{
				return false;
			}
			return null;
		}
		public virtual XVar getRecordData()
		{
			dynamic connection = null, data = null, keys = null, var_next = null;
			if((XVar)((XVar)((XVar)((XVar)(this.location != Constants.PAGE_EDIT)  && (XVar)(this.location != Constants.PAGE_VIEW))  && (XVar)(this.location != Constants.PAGE_LIST))  && (XVar)(this.location != "grid"))  && (XVar)(!(XVar)(var_next)))
			{
				return false;
			}
			connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(GlobalVars.strTableName)));
			if(XVar.Pack(this.isGetNext))
			{
				this.isGetNext = new XVar(false);
				keys = XVar.Clone(this.selectedKeys[this.nextInd]);
				this.nextInd = XVar.Clone(this.nextInd + 1);
			}
			else
			{
				keys = XVar.Clone(this.currentKeys);
			}
			GlobalVars.strSQL = XVar.Clone(GlobalVars.gQuery.buildSQL_default((XVar)(new XVar(0, CommonFunctions.KeyWhere((XVar)(keys)), 1, CommonFunctions.SecuritySQL(new XVar("Search"))))));
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			data = XVar.Clone(GlobalVars.cipherer.DecryptFetchedArray((XVar)(connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc())));
			return data;
		}
		public virtual XVar getMasterData(dynamic _param_masterTable)
		{
			#region pass-by-value parameters
			dynamic masterTable = XVar.Clone(_param_masterTable);
			#endregion

			if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(masterTable, "_masterRecordData"))))
			{
				return XSession.Session[MVCFunctions.Concat(masterTable, "_masterRecordData")];
			}
			return false;
		}
	}
}
