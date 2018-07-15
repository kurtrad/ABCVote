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
	public partial class SearchPageDash : SearchPage
	{
		protected static bool skipSearchPageDashCtor = false;
		public SearchPageDash(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipSearchPageDashCtor)
			{
				skipSearchPageDashCtor = false;
				return;
			}
			if(this.mode == Constants.SEARCH_DASHBOARD)
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isDashSearchPage");
			}
		}
		protected override XVar assignSessionPrefix()
		{
			this.sessionPrefix = XVar.Clone(this.tName);
			return null;
		}
		protected virtual XVar getTableSettings(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(XVar.Pack(!(XVar)(this.tableSettings.KeyExists(table))))
			{
				dynamic tableSettings = XVar.Array();
				this.tableSettings.InitAndSetArrayItem(new ProjectSettings((XVar)(tableSettings[table]), new XVar(Constants.PAGE_SEARCH)), table);
			}
			return this.tableSettings[table];
		}
		protected override XVar prepareFields()
		{
			foreach (KeyValuePair<XVar, dynamic> fdata in this.pSet.getDashboardSearchFields().GetEnumerator())
			{
				dynamic ctrlBlockArr = XVar.Array(), ctrlInd = null, fSet = null, field = null, firstFieldParams = XVar.Array(), isFieldNeedSecCtrl = null, lookupTable = null, srchFields = XVar.Array(), srchTypeFull = null, table = null;
				field = XVar.Clone(fdata.Value[0]["field"]);
				table = XVar.Clone(fdata.Value[0]["table"]);
				fSet = XVar.Clone(getTableSettings((XVar)(table)));
				lookupTable = XVar.Clone(fSet.getLookupTable((XVar)(field)));
				if(XVar.Pack(lookupTable))
				{
					this.settingsMap.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(lookupTable)), "globalSettings", "shortTNames", lookupTable);
				}
				fillFieldToolTips((XVar)(fdata.Key));
				srchFields = XVar.Clone(this.searchClauseObj.getSearchCtrlParams((XVar)(fdata.Key)));
				firstFieldParams = XVar.Clone(XVar.Array());
				if(XVar.Pack(MVCFunctions.count(srchFields)))
				{
					firstFieldParams = XVar.Clone(srchFields[0]);
				}
				else
				{
					firstFieldParams.InitAndSetArrayItem(fdata.Key, "fName");
					firstFieldParams.InitAndSetArrayItem("", "eType");
					firstFieldParams.InitAndSetArrayItem(this.pSet.getDefaultValue((XVar)(field), (XVar)(table)), "value1");
					firstFieldParams.InitAndSetArrayItem("", "value2");
					firstFieldParams.InitAndSetArrayItem(false, "not");
					firstFieldParams.InitAndSetArrayItem(this.pSet.getDefaultSearchOption((XVar)(fdata.Key)), "opt");
					firstFieldParams.InitAndSetArrayItem(false, "not");
					if(MVCFunctions.substr((XVar)(firstFieldParams["opt"]), new XVar(0), new XVar(4)) == "NOT ")
					{
						firstFieldParams.InitAndSetArrayItem(MVCFunctions.substr((XVar)(firstFieldParams["opt"]), new XVar(4)), "opt");
						firstFieldParams.InitAndSetArrayItem(true, "not");
					}
				}
				ctrlBlockArr = XVar.Clone(this.searchControlBuilder.buildSearchCtrlBlockArr((XVar)(this.id), (XVar)(firstFieldParams["fName"]), new XVar(0), (XVar)(firstFieldParams["opt"]), (XVar)(firstFieldParams["not"]), new XVar(false), (XVar)(firstFieldParams["value1"]), (XVar)(firstFieldParams["value2"])));
				if(firstFieldParams["opt"] == "")
				{
					firstFieldParams.InitAndSetArrayItem(this.pSet.getDefaultSearchOption((XVar)(firstFieldParams["fName"])), "opt");
				}
				srchTypeFull = XVar.Clone(this.searchControlBuilder.getCtrlSearchType((XVar)(firstFieldParams["fName"]), (XVar)(this.id), new XVar(0), (XVar)(firstFieldParams["opt"]), (XVar)(firstFieldParams["not"]), new XVar(true), new XVar(true)));
				if(XVar.Pack(CommonFunctions.isEnableSection508()))
				{
					this.xt.assign_section((XVar)(MVCFunctions.Concat(fdata.Key, "_label")), (XVar)(MVCFunctions.Concat("<label for=\"", getInputElementId((XVar)(field), (XVar)(fSet)), "\">")), new XVar("</label>"));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(fdata.Key, "_label")), new XVar(true));
				}
				this.xt.assign((XVar)(MVCFunctions.Concat(fdata.Key, "_fieldblock")), new XVar(true));
				this.xt.assignbyref((XVar)(MVCFunctions.Concat(fdata.Key, "_editcontrol")), (XVar)(ctrlBlockArr["searchcontrol"]));
				this.xt.assign((XVar)(MVCFunctions.Concat(fdata.Key, "_notbox")), (XVar)(ctrlBlockArr["notbox"]));
				this.xt.assignbyref((XVar)(MVCFunctions.Concat(fdata.Key, "_editcontrol1")), (XVar)(ctrlBlockArr["searchcontrol1"]));
				this.xt.assign((XVar)(MVCFunctions.Concat("searchtype_", fdata.Key)), (XVar)(ctrlBlockArr["searchtype"]));
				this.xt.assign((XVar)(MVCFunctions.Concat("searchtypefull_", fdata.Key)), (XVar)(srchTypeFull));
				isFieldNeedSecCtrl = XVar.Clone(this.searchControlBuilder.isNeedSecondCtrl((XVar)(fdata.Key)));
				ctrlInd = new XVar(0);
				if(XVar.Pack(isFieldNeedSecCtrl))
				{
					this.controlsMap.InitAndSetArrayItem(new XVar("fName", fdata.Key, "recId", this.id, "ctrlsMap", new XVar(0, ctrlInd, 1, ctrlInd + 1)), "search", "searchBlocks", null);
					ctrlInd += 2;
				}
				else
				{
					this.controlsMap.InitAndSetArrayItem(new XVar("fName", fdata.Key, "recId", this.id, "ctrlsMap", new XVar(0, ctrlInd)), "search", "searchBlocks", null);
					ctrlInd++;
				}
			}
			return null;
		}
		public override XVar fillFieldSettings()
		{
			dynamic arrFields = null;
			arrFields = XVar.Clone(this.pSet.getAllSearchFields());
			addFieldsSettings((XVar)(arrFields), new XVar(null), new XVar(true), (XVar)(this.pageType));
			return null;
		}
		public virtual XVar locateDashFieldByOriginal(dynamic _param_table, dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic fname = null;
			foreach (KeyValuePair<XVar, dynamic> data in this.pSet.getDashboardSearchFields().GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(data.Value)))
				{
					continue;
				}
				if((XVar)(data.Value[0]["table"] == table)  && (XVar)(data.Value[0]["field"] == field))
				{
					return data.Key;
				}
			}
			return fname;
		}
		public override XVar addFieldsSettings(dynamic _param_arrFields, dynamic _param_pSet_packed, dynamic _param_pageBased, dynamic _param_pageType)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic arrFields = XVar.Clone(_param_arrFields);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			dynamic pageBased = XVar.Clone(_param_pageBased);
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			dynamic dashSearchFields = XVar.Array(), tableSettingsFilled = XVar.Array();
			dashSearchFields = XVar.Clone(this.pSet.getDashboardSearchFields());
			tableSettingsFilled = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fieldName in arrFields.GetEnumerator())
			{
				dynamic lookupTableName = null, tableFieldName = null, tableName = null;
				tableName = XVar.Clone(dashSearchFields[fieldName.Value][0]["table"]);
				pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(tableName), (XVar)(pageType)));
				tableFieldName = XVar.Clone(dashSearchFields[fieldName.Value][0]["field"]);
				if(XVar.Pack(!(XVar)(tableSettingsFilled[tableName])))
				{
					fillTableSettings((XVar)(tableName), (XVar)(pSet));
					tableSettingsFilled.InitAndSetArrayItem(true, tableName);
				}
				if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"].KeyExists(fieldName.Value))))
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", fieldName.Value);
				}
				if(XVar.Pack(!(XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"][fieldName.Value].KeyExists(pageType))))
				{
					this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType);
				}
				foreach (KeyValuePair<XVar, dynamic> val in this.settingsMap["fieldSettings"].GetEnumerator())
				{
					dynamic fData = XVar.Array(), isDefault = null;
					fData = XVar.Clone(pSet.getFieldData((XVar)(tableFieldName), (XVar)(val.Key)));
					if(val.Key == "validateAs")
					{
						if((XVar)((XVar)(pageType == Constants.PAGE_ADD)  || (XVar)(pageType == Constants.PAGE_EDIT))  || (XVar)(pageType == Constants.PAGE_REGISTER))
						{
							fillValidation((XVar)(fData), (XVar)(val.Value), (XVar)(this.jsSettings["tableSettings"][this.tName]["fieldSettings"][fieldName.Value][pageType]));
						}
						continue;
					}
					if(val.Key == "RTEType")
					{
						fData = XVar.Clone(pSet.getRTEType((XVar)(tableFieldName)));
						if(fData == "RTECK")
						{
							this.isUseCK = new XVar(true);
							this.jsSettings.InitAndSetArrayItem(pSet.getNCols((XVar)(tableFieldName)), "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType, "nWidth");
							this.jsSettings.InitAndSetArrayItem(pSet.getNRows((XVar)(tableFieldName)), "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType, "nHeight");
						}
					}
					else
					{
						if(val.Key == "autoCompleteFields")
						{
							fData = XVar.Clone(pSet.getAutoCompleteFields((XVar)(tableFieldName)));
						}
						else
						{
							if(val.Key == "parentFields")
							{
								fData = XVar.Clone(pSet.getLookupParentFNames((XVar)(tableFieldName)));
								foreach (KeyValuePair<XVar, dynamic> parentField in fData.GetEnumerator())
								{
									fData.InitAndSetArrayItem(locateDashFieldByOriginal((XVar)(tableName), (XVar)(parentField.Value)), parentField.Key);
								}
							}
						}
					}
					isDefault = new XVar(false);
					if(XVar.Pack(MVCFunctions.is_array((XVar)(fData))))
					{
						isDefault = XVar.Clone(!(XVar)(MVCFunctions.count(fData)));
					}
					else
					{
						if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(val.Value["default"])))))
						{
							isDefault = XVar.Clone(XVar.Equals(XVar.Pack(fData), XVar.Pack(val.Value["default"])));
						}
					}
					if(XVar.Pack(!(XVar)(isDefault)))
					{
						this.jsSettings.InitAndSetArrayItem(fData, "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType, val.Value["jsName"]);
					}
				}
				this.jsSettings.InitAndSetArrayItem(tableName, "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType, "originalTable");
				this.jsSettings.InitAndSetArrayItem(tableFieldName, "tableSettings", this.tName, "fieldSettings", fieldName.Value, pageType, "originalField");
				this.jsSettings.InitAndSetArrayItem(this.isUseCK, "tableSettings", this.tName, "isUseCK");
				if((XVar)(MVCFunctions.count(this.googleMapCfg) != 0)  && (XVar)(this.googleMapCfg["isUseGoogleMap"]))
				{
					this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isUseGoogleMap");
					this.jsSettings.InitAndSetArrayItem(this.googleMapCfg, "tableSettings", this.tName, "googleMapCfg");
				}
				lookupTableName = XVar.Clone(pSet.getLookupTable((XVar)(tableFieldName)));
				if(XVar.Pack(lookupTableName))
				{
					this.jsSettings.InitAndSetArrayItem(CommonFunctions.GetTableURL((XVar)(lookupTableName)), "global", "shortTNames", lookupTableName);
				}
				if(pSet.getEditFormat((XVar)(tableFieldName)) == "Time")
				{
					fillTimePickSettings((XVar)(tableFieldName), new XVar(""), (XVar)(pSet), (XVar)(pageType));
				}
			}
			return null;
		}
	}
}
