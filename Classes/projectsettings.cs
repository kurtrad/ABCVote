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
	public partial class ProjectSettings : XClass
	{
		public dynamic _table;
		public dynamic _pageMode;
		public dynamic _viewPage = XVar.Pack(Constants.PAGE_VIEW);
		public dynamic _defaultViewPage = XVar.Pack(Constants.PAGE_VIEW);
		public dynamic _editPage = XVar.Pack(Constants.PAGE_EDIT);
		public dynamic _defaultEditPage = XVar.Pack(Constants.PAGE_EDIT);
		public dynamic _tableData = XVar.Array();
		public dynamic _mastersTableData = XVar.Array();
		public dynamic _detailsTableData = XVar.Array();
		public dynamic _dashboardElemPSet = XVar.Array();
		public ProjectSettings(dynamic _param_table = null, dynamic _param_page = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			if(_param_page as Object == null) _param_page = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic page = XVar.Clone(_param_page);
			#endregion

			if((XVar)(table)  && (XVar)(table != Constants.NOT_TABLE_BASED_TNAME))
			{
				setTable((XVar)(table));
			}
			if(XVar.Pack(page))
			{
				setPage((XVar)(page));
			}
		}
		public virtual XVar setTable(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic tableType = null;
			this._table = XVar.Clone(table);
			if(CommonFunctions.GetTableURL((XVar)(table)) != "")
			{
				if(!GlobalVars.tables_data.KeyExists(table))
				{
					Type t = Type.GetType(MVCFunctions.Concat("runnerDotNet.Settings_", (string)CommonFunctions.GetTableURL(table)));
					t.GetMethod("Apply", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, null);
				}
			}
			if(XVar.Pack(GlobalVars.tables_data.KeyExists(this._table)))
			{
				this._tableData = GlobalVars.tables_data[this._table];
			}
			this._mastersTableData = GlobalVars.masterTablesData[this._table];
			this._detailsTableData = GlobalVars.detailsTablesData[this._table];
			tableType = XVar.Clone(getTableType());
			this._editPage = XVar.Clone(getDefaultEditPageType((XVar)(tableType)));
			this._viewPage = XVar.Clone(getDefaultViewPageType((XVar)(tableType)));
			this._defaultEditPage = XVar.Clone(this._editPage);
			this._defaultViewPage = XVar.Clone(this._viewPage);
			return null;
		}
		public virtual XVar getDefaultViewPageType(dynamic _param_tableType)
		{
			#region pass-by-value parameters
			dynamic tableType = XVar.Clone(_param_tableType);
			#endregion

			if((XVar)(tableType == Constants.PAGE_CHART)  || (XVar)(tableType == Constants.PAGE_REPORT))
			{
				return tableType;
			}
			return Constants.PAGE_VIEW;
		}
		public virtual XVar getDefaultEditPageType(dynamic _param_tableType)
		{
			#region pass-by-value parameters
			dynamic tableType = XVar.Clone(_param_tableType);
			#endregion

			if((XVar)(tableType == Constants.PAGE_CHART)  || (XVar)(tableType == Constants.PAGE_REPORT))
			{
				return Constants.PAGE_SEARCH;
			}
			return Constants.PAGE_EDIT;
		}
		public virtual XVar setPage(dynamic _param_page)
		{
			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			#endregion

			if(XVar.Pack(isPageTypeForView((XVar)(page))))
			{
				dynamic tableType = null;
				tableType = XVar.Clone(getTableType());
				if((XVar)((XVar)(tableType != "report")  && (XVar)(tableType != "chart"))  && (XVar)((XVar)(page == Constants.PAGE_CHART)  || (XVar)(page == Constants.PAGE_REPORT)))
				{
					this._viewPage = new XVar(Constants.PAGE_LIST);
				}
				else
				{
					this._viewPage = XVar.Clone(page);
				}
				this._defaultViewPage = XVar.Clone(getDefaultViewPageType((XVar)(tableType)));
			}
			if(XVar.Pack(isPageTypeForEdit((XVar)(page))))
			{
				this._editPage = XVar.Clone(page);
				this._defaultEditPage = XVar.Clone(getDefaultEditPageType((XVar)(getTableType())));
			}
			return null;
		}
		public virtual XVar getEditPageType()
		{
			return this._editPage;
		}
		public virtual XVar isPageTypeForView(dynamic _param_ptype)
		{
			#region pass-by-value parameters
			dynamic ptype = XVar.Clone(_param_ptype);
			#endregion

			return MVCFunctions.in_array((XVar)(MVCFunctions.strtolower((XVar)(ptype))), (XVar)(GlobalVars.pageTypesForView));
		}
		public virtual XVar isPageTypeForEdit(dynamic _param_ptype)
		{
			#region pass-by-value parameters
			dynamic ptype = XVar.Clone(_param_ptype);
			#endregion

			return MVCFunctions.in_array((XVar)(MVCFunctions.strtolower((XVar)(ptype))), (XVar)(GlobalVars.pageTypesForEdit));
		}
		public virtual XVar getTable(dynamic _param_table, dynamic _param_page = null)
		{
			#region default values
			if(_param_page as Object == null) _param_page = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic page = XVar.Clone(_param_page);
			#endregion

			return new ProjectSettings((XVar)(table), (XVar)(page));
		}
		public virtual XVar getPageTypeByFieldEditFormat(dynamic _param_field, dynamic _param_editFormat)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic editFormat = XVar.Clone(_param_editFormat);
			#endregion

			if((XVar)(this._tableData.KeyExists(field))  && (XVar)(this._tableData[field].KeyExists(Constants.FORMAT_EDIT)))
			{
				foreach (KeyValuePair<XVar, dynamic> editSettings in this._tableData[field][Constants.FORMAT_EDIT].GetEnumerator())
				{
					if((XVar)(editSettings.Value.KeyExists("EditFormat"))  && (XVar)(editSettings.Value["EditFormat"] == editFormat))
					{
						return editSettings.Key;
					}
				}
			}
			return "";
		}
		public virtual XVar getTableData(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			if(XVar.Pack(!(XVar)(isExistsTableKey((XVar)(key)))))
			{
				return getDefaultValueByKey((XVar)(MVCFunctions.substr((XVar)(key), new XVar(1))));
			}
			return this._tableData[key];
		}
		private XVar getEffectiveEditPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(isSeparate((XVar)(field))))
			{
				return this._editPage;
			}
			return this._defaultEditPage;
		}
		private XVar getEffectiveViewPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(isSeparate((XVar)(field))))
			{
				if((XVar)(this._pageMode == Constants.EDIT_INLINE)  && (XVar)(this._viewPage != Constants.PAGE_VIEW))
				{
					return Constants.PAGE_LIST;
				}
				else
				{
					if((XVar)(this._pageMode == Constants.LIST_MASTER)  && (XVar)(this._viewPage == Constants.PAGE_LIST))
					{
						return Constants.PAGE_MASTER_INFO_LIST;
					}
					else
					{
						if((XVar)(this._pageMode == Constants.LIST_MASTER)  && (XVar)(this._viewPage == Constants.PAGE_REPORT))
						{
							return Constants.PAGE_MASTER_INFO_REPORT;
						}
						else
						{
							if((XVar)(this._pageMode == Constants.PRINT_MASTER)  && (XVar)(this._viewPage == Constants.PAGE_RPRINT))
							{
								return Constants.PAGE_MASTER_INFO_RPRINT;
							}
							else
							{
								if(this._pageMode == Constants.PRINT_MASTER)
								{
									return Constants.PAGE_MASTER_INFO_PRINT;
								}
							}
						}
					}
				}
				return this._viewPage;
			}
			return this._defaultViewPage;
		}
		public virtual XVar getFieldData(dynamic _param_field, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic editPage = null, settingType = null, viewPage = null;
			if(getEntityType() == Constants.titDASHBOARD)
			{
				return getDashFieldData((XVar)(field), (XVar)(key));
			}
			if(XVar.Pack(!(XVar)(this._tableData.KeyExists(field))))
			{
				return getDefaultValueByKey((XVar)(key));
			}
			settingType = XVar.Clone(GlobalVars.g_settingsType[key]);
			if(settingType == null)
			{
				settingType = new XVar("");
			}
			switch(((XVar)settingType).ToString())
			{
				case Constants.SETTING_TYPE_VIEW:
					viewPage = XVar.Clone(getEffectiveViewPage((XVar)(field)));
					if(XVar.Pack(this._tableData[field][Constants.FORMAT_VIEW][viewPage].KeyExists(key)))
					{
						return this._tableData[field][Constants.FORMAT_VIEW][viewPage][key];
					}
					break;
				case Constants.SETTING_TYPE_EDIT:
					editPage = XVar.Clone(getEffectiveEditPage((XVar)(field)));
					if(XVar.Pack(this._tableData[field][Constants.FORMAT_EDIT][editPage].KeyExists(key)))
					{
						return this._tableData[field][Constants.FORMAT_EDIT][editPage][key];
					}
					break;
				default:
					if(XVar.Pack(this._tableData[field].KeyExists(key)))
					{
						return this._tableData[field][key];
					}
					break;
			}
			return getDefaultValueByKey((XVar)(key));
		}
		public virtual XVar setFieldData(dynamic _param_field, dynamic _param_key, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic key = XVar.Clone(_param_key);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			dynamic editPage = null, oldValue = null, settingType = null, viewPage = null;
			oldValue = XVar.Clone(getFieldData((XVar)(field), (XVar)(key)));
			settingType = XVar.Clone(GlobalVars.g_settingsType[key]);
			if(settingType == null)
			{
				settingType = new XVar("");
			}
			switch(((XVar)settingType).ToString())
			{
				case Constants.SETTING_TYPE_VIEW:
					viewPage = XVar.Clone(getEffectiveViewPage((XVar)(field)));
					this._tableData.InitAndSetArrayItem(value, field, Constants.FORMAT_VIEW, viewPage, key);
					break;
				case Constants.SETTING_TYPE_EDIT:
					editPage = XVar.Clone(getEffectiveEditPage((XVar)(field)));
					this._tableData.InitAndSetArrayItem(value, field, Constants.FORMAT_EDIT, editPage, key);
					break;
				default:
					this._tableData.InitAndSetArrayItem(value, field, key);
					break;
			}
			return oldValue;
		}
		public virtual XVar getTableName()
		{
			return this._table;
		}
		public virtual XVar findField(dynamic _param_f)
		{
			#region pass-by-value parameters
			dynamic f = XVar.Clone(_param_f);
			#endregion

			dynamic fields = XVar.Array(), gTable = null;
			fields = XVar.Clone(getFieldsList());
			if(!XVar.Equals(XVar.Pack(MVCFunctions.array_search((XVar)(f), (XVar)(fields))), XVar.Pack(false)))
			{
				return f;
			}
			gTable = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(this._table)));
			if(XVar.Pack(GlobalVars.field_labels[GlobalVars.mlang_defaultlang].KeyExists(f)))
			{
				return getFieldByGoodFieldName((XVar)(f));
			}
			f = XVar.Clone(MVCFunctions.strtoupper((XVar)(f)));
			foreach (KeyValuePair<XVar, dynamic> ff in fields.GetEnumerator())
			{
				if(MVCFunctions.strtoupper((XVar)(ff.Value)) == f)
				{
					return ff.Value;
				}
				if(MVCFunctions.strtoupper((XVar)(MVCFunctions.GoodFieldName((XVar)(ff.Value)))) == f)
				{
					return ff.Value;
				}
			}
			return "";
		}
		public virtual XVar addCustomExpressionIndex(dynamic _param_mainTable, dynamic _param_mainField, dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic mainTable = XVar.Clone(_param_mainTable);
			dynamic mainField = XVar.Clone(_param_mainField);
			dynamic index = XVar.Clone(_param_index);
			#endregion

			if(XVar.Pack(!(XVar)(isExistsTableKey(new XVar(".customExpressionIndexes")))))
			{
				this._tableData.InitAndSetArrayItem(XVar.Array(), ".customExpressionIndexes");
			}
			if(XVar.Pack(!(XVar)(this._tableData[".customExpressionIndexes"].KeyExists(mainTable))))
			{
				this._tableData.InitAndSetArrayItem(XVar.Array(), ".customExpressionIndexes", mainTable);
			}
			this._tableData.InitAndSetArrayItem(index, ".customExpressionIndexes", mainTable, mainField);
			return null;
		}
		public virtual XVar getCustomExpressionIndex(dynamic _param_mainTable, dynamic _param_mainField)
		{
			#region pass-by-value parameters
			dynamic mainTable = XVar.Clone(_param_mainTable);
			dynamic mainField = XVar.Clone(_param_mainField);
			#endregion

			if(XVar.Pack(!(XVar)(isExistsTableKey(new XVar(".customExpressionIndexes")))))
			{
				this._tableData.InitAndSetArrayItem(XVar.Array(), ".customExpressionIndexes");
			}
			if((XVar)(this._tableData[".customExpressionIndexes"].KeyExists(mainTable))  && (XVar)(this._tableData[".customExpressionIndexes"][mainTable].KeyExists(mainField)))
			{
				return this._tableData[".customExpressionIndexes"][mainTable][mainField];
			}
			return false;
		}
		public virtual XVar isExistsTableKey(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			if(XVar.Pack(!(XVar)(this._tableData.KeyExists(key))))
			{
				return false;
			}
			return true;
		}
		public virtual XVar isExistsFieldKey(dynamic _param_field, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic key = XVar.Clone(_param_key);
			#endregion

			if(XVar.Pack(!(XVar)(isExistsTableKey((XVar)(field)))))
			{
				return false;
			}
			if(XVar.Pack(!(XVar)(this._tableData[field].KeyExists(key))))
			{
				return false;
			}
			return true;
		}
		public virtual XVar getDefaultValueByKey(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			if(XVar.Pack(GlobalVars.g_defaultOptionValues.KeyExists(key)))
			{
				return GlobalVars.g_defaultOptionValues[key];
			}
			return false;
		}
		public virtual XVar getMasterTablesArr(dynamic _param_tName)
		{
			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			#endregion

			return this._mastersTableData;
		}
		public virtual XVar getMasterKeysByDetailTable(dynamic _param_dTableName, dynamic _param_default = null)
		{
			#region default values
			if(_param_default as Object == null) _param_default = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic dTableName = XVar.Clone(_param_dTableName);
			dynamic var_default = XVar.Clone(_param_default);
			#endregion

			if(XVar.Pack(!(XVar)(dTableName)))
			{
				return var_default;
			}
			foreach (KeyValuePair<XVar, dynamic> dTableDataArr in this._detailsTableData.GetEnumerator())
			{
				if(dTableDataArr.Value["dDataSourceTable"] == dTableName)
				{
					return dTableDataArr.Value["masterKeys"];
				}
			}
			return var_default;
		}
		public virtual XVar getDetailTablesArr()
		{
			return this._detailsTableData;
		}
		public virtual XVar getDetailKeysByMasterTable(dynamic _param_mTableName = null, dynamic _param_default = null)
		{
			#region default values
			if(_param_mTableName as Object == null) _param_mTableName = new XVar("");
			if(_param_default as Object == null) _param_default = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic mTableName = XVar.Clone(_param_mTableName);
			dynamic var_default = XVar.Clone(_param_default);
			#endregion

			if(XVar.Pack(!(XVar)(mTableName)))
			{
				return var_default;
			}
			foreach (KeyValuePair<XVar, dynamic> mTableDataArr in this._mastersTableData.GetEnumerator())
			{
				if(mTableDataArr.Value["mDataSourceTable"] == mTableName)
				{
					return mTableDataArr.Value["detailKeys"];
				}
			}
			return var_default;
		}
		public virtual XVar getDetailKeysByDetailTable(dynamic _param_dTableName, dynamic _param_default = null)
		{
			#region default values
			if(_param_default as Object == null) _param_default = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic dTableName = XVar.Clone(_param_dTableName);
			dynamic var_default = XVar.Clone(_param_default);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> dTableDataArr in this._detailsTableData.GetEnumerator())
			{
				if(dTableDataArr.Value["dDataSourceTable"] == dTableName)
				{
					return dTableDataArr.Value["detailKeys"];
				}
			}
			return var_default;
		}
		public virtual XVar getDPType(dynamic _param_dTableName)
		{
			#region pass-by-value parameters
			dynamic dTableName = XVar.Clone(_param_dTableName);
			#endregion

			if(XVar.Pack(!(XVar)(dTableName)))
			{
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> dTableDataArr in this._detailsTableData.GetEnumerator())
			{
				if(dTableDataArr.Value["dDataSourceTable"] == dTableName)
				{
					return dTableDataArr.Value["previewOnList"];
				}
			}
			return false;
		}
		public virtual XVar GetFieldByIndex(dynamic _param_index)
		{
			#region pass-by-value parameters
			dynamic index = XVar.Clone(_param_index);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in this._tableData.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(value.Value)))))
				{
					continue;
				}
				else
				{
					if(XVar.Pack(!(XVar)(value.Value.KeyExists("Index"))))
					{
						continue;
					}
				}
				if((XVar)(value.Value["Index"] == index)  && (XVar)(getFieldIndex((XVar)(value.Key))))
				{
					return value.Key;
				}
			}
			return null;
		}
		public virtual XVar isSeparate(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(this._tableData[field].KeyExists("isSeparate")))
			{
				return this._tableData[field]["isSeparate"];
			}
			return false;
		}
		public virtual XVar label(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic result = null;
			result = XVar.Clone(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(this._table))), (XVar)(MVCFunctions.GoodFieldName((XVar)(field)))));
			return (XVar.Pack(result != XVar.Pack("")) ? XVar.Pack(result) : XVar.Pack(field));
		}
		public virtual XVar getFilenameField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("Filename"));
		}
		public virtual XVar getLinkPrefix(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LinkPrefix"));
		}
		public virtual XVar getLinkType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("hlType"));
		}
		public virtual XVar getLinkDisplayField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("hlTitleField"));
		}
		public virtual XVar openLinkInNewWindow(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("hlNewWindow"));
		}
		public virtual XVar getLinkWordNameType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("hlLinkWordNameType"));
		}
		public virtual XVar getLinkWordText(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("hlLinkWordText"));
		}
		public virtual XVar getFieldType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("FieldType"));
		}
		public virtual XVar isAutoincField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("AutoInc"));
		}
		public virtual XVar getOraSequenceName(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("OraSequenceName"));
		}
		public virtual XVar getDefaultValue(dynamic _param_field, dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic editPage = null, tableName = null;
			tableName = XVar.Clone((XVar.Pack(table) ? XVar.Pack(table) : XVar.Pack(this._table)));
			editPage = XVar.Clone(this._editPage);
			if(XVar.Pack(!(XVar)(isSeparate((XVar)(field)))))
			{
				return null;
			}
			return MVCFunctions.GetDefaultValue((XVar)(field), (XVar)(editPage), (XVar)(tableName));
		}
		public virtual XVar isAutoUpdatable(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("autoUpdatable"));
		}
		public virtual XVar getAutoUpdateValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic editPage = null;
			editPage = XVar.Clone(this._editPage);
			if(XVar.Pack(!(XVar)(isSeparate((XVar)(field)))))
			{
				editPage = XVar.Clone(getDefaultEditPageType((XVar)(getTableType())));
			}
			return MVCFunctions.GetAutoUpdateValue((XVar)(field), (XVar)(editPage), (XVar)(this._table));
		}
		public virtual XVar getEditFormat(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("EditFormat"));
		}
		public virtual XVar isReadonly(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(getEditFormat((XVar)(field)) == Constants.EDIT_FORMAT_READONLY)
			{
				return true;
			}
			return false;
		}
		public virtual XVar getViewFormat(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ViewFormat"));
		}
		public virtual XVar dateEditShowTime(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowTime"));
		}
		public virtual XVar lookupControlType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LCType"));
		}
		public virtual XVar isDeleteAssociatedFile(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("DeleteAssociatedFile"));
		}
		public virtual XVar useCategory(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("UseCategory"));
		}
		public virtual XVar multiSelect(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("Multiselect"));
		}
		public virtual XVar selectSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("SelectSize"));
		}
		public virtual XVar showThumbnail(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowThumbnail"));
		}
		public virtual XVar isImageURL(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("fieldIsImageUrl"));
		}
		public virtual XVar showCustomExpr(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowCustomExpr"));
		}
		public virtual XVar showFileSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowFileSize"));
		}
		public virtual XVar showIcon(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowIcon"));
		}
		public virtual XVar getImageWidth(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ImageWidth"));
		}
		public virtual XVar getImageHeight(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ImageHeight"));
		}
		public virtual XVar getThumbnailWidth(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ThumbWidth"));
		}
		public virtual XVar getThumbnailHeight(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ThumbHeight"));
		}
		public virtual XVar getLookupType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupType"));
		}
		public virtual XVar getLookupTable(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupTable"));
		}
		public virtual XVar isLookupWhereCode(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupWhereCode"));
		}
		public virtual XVar isLookupWhereSet(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(isLookupWhereCode((XVar)(field))))
			{
				return true;
			}
			return 0 != MVCFunctions.strlen((XVar)(getFieldData((XVar)(field), new XVar("LookupWhere"))));
		}
		public virtual XVar getLookupWhere(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(isLookupWhereCode((XVar)(field))))
			{
				return MVCFunctions.GetLWWhere((XVar)(field), (XVar)(getEffectiveEditPage((XVar)(field))), (XVar)(this._table));
			}
			else
			{
				return getFieldData((XVar)(field), new XVar("LookupWhere"));
			}
			return null;
		}
		public virtual XVar getNotProjectLookupTableConnId(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupConnId"));
		}
		public virtual XVar getLinkField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LinkField"));
		}
		public virtual XVar getLWLinkFieldType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LinkFieldType"));
		}
		public virtual XVar getDisplayField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("DisplayField"));
		}
		public virtual XVar getCustomDisplay(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("CustomDisplay"));
		}
		public virtual XVar NeedEncode(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("NeedEncode"));
		}
		public virtual XVar getValidation(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("validateAs"));
		}
		public virtual XVar appearOnListPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bListPage"));
		}
		public virtual XVar appearOnAddPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bAddPage"));
		}
		public virtual XVar appearOnInlineAdd(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bInlineAdd"));
		}
		public virtual XVar appearOnEditPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bEditPage"));
		}
		public virtual XVar appearOnInlineEdit(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bInlineEdit"));
		}
		public virtual XVar appearOnUpdateSelected(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bUpdateSelected"));
		}
		public virtual XVar appearOnViewPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bViewPage"));
		}
		public virtual XVar appearOnPrinterPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bPrinterPage"));
		}
		public virtual XVar isVideoUrlField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("fieldIsVideoUrl"));
		}
		public virtual XVar isAbsolute(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("Absolute"));
		}
		public virtual XVar getAudioTitleField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("audioTitleField"));
		}
		public virtual XVar getVideoWidth(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("videoWidth"));
		}
		public virtual XVar getVideoHeight(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("videoHeight"));
		}
		public virtual XVar isRewindEnabled(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("RewindEnabled"));
		}
		public virtual XVar getParentFieldsData(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("categoryFields"));
		}
		public virtual XVar getLookupParentFNames(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic fNames = XVar.Array();
			fNames = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> data in getParentFieldsData((XVar)(field)).GetEnumerator())
			{
				fNames.InitAndSetArrayItem(data.Value["main"], null);
			}
			return fNames;
		}
		public virtual XVar isLookupUnique(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupUnique"));
		}
		public virtual XVar getLookupOrderBy(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupOrderBy"));
		}
		public virtual XVar isLookupDesc(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupDesc"));
		}
		public virtual XVar getOwnerTable(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ownerTable"));
		}
		public virtual XVar isFieldEncrypted(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bIsEncrypted"));
		}
		public virtual XVar isAllowToAdd(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("AllowToAdd"));
		}
		public virtual XVar isSimpleAdd(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("SimpleAdd"));
		}
		public virtual XVar getAutoCompleteFields(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic editPageType = null;
			editPageType = XVar.Clone(getEditPageType());
			if((XVar)((XVar)(editPageType == Constants.PAGE_REGISTER)  || (XVar)(editPageType == Constants.PAGE_ADD))  || (XVar)((XVar)(editPageType == Constants.PAGE_EDIT)  && (XVar)((XVar)(isSeparate((XVar)(field)))  || (XVar)(isAutoCompleteFieldsOnEdit((XVar)(field))))))
			{
				return getFieldData((XVar)(field), new XVar("autoCompleteFields"));
			}
			return getDefaultValueByKey(new XVar("autoCompleteFields"));
		}
		public virtual XVar isAutoCompleteFieldsOnEdit(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("autoCompleteFieldsOnEdit"));
		}
		public virtual XVar isFreeInput(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("freeInput"));
		}
		public virtual XVar getMapData(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("mapData"));
		}
		public virtual XVar getFormatTimeAttrs(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("FormatTimeAttrs"));
		}
		public virtual XVar appearOnExportPage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("bExportPage"));
		}
		public virtual XVar appearOnRegisterOrSearchPage(dynamic _param_field, dynamic _param_pageType)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageType = XVar.Clone(_param_pageType);
			#endregion

			dynamic arrFields = null;
			if((XVar)(pageType != Constants.PAGE_REGISTER)  && (XVar)(pageType != Constants.PAGE_SEARCH))
			{
				return false;
			}
			arrFields = XVar.Clone(XVar.Array());
			if(pageType == Constants.PAGE_REGISTER)
			{
				arrFields = XVar.Clone(getFieldsForRegister());
			}
			else
			{
				if(pageType == Constants.PAGE_SEARCH)
				{
					arrFields = XVar.Clone(getAllSearchFields());
				}
			}
			if(XVar.Pack(MVCFunctions.in_array((XVar)(field), (XVar)(arrFields))))
			{
				return true;
			}
			return false;
		}
		public virtual XVar getStrOriginalTableName()
		{
			return getTableData(new XVar(".strOriginalTableName"));
		}
		public virtual XVar getFieldsForRegister()
		{
			return getTableData(new XVar(".fieldsForRegister"));
		}
		public virtual XVar getAllSearchFields()
		{
			return getTableData(new XVar(".allSearchFields"));
		}
		public virtual XVar getAdvSearchFields()
		{
			return (XVar.Pack(getEntityType() == Constants.titDASHBOARD) ? XVar.Pack(getAllSearchFields()) : XVar.Pack(getTableData(new XVar(".advSearchFields"))));
		}
		public virtual XVar isUseTimeForSearch()
		{
			return getTableData(new XVar(".isUseTimeForSearch"));
		}
		public virtual XVar isUseToolTips()
		{
			return getTableData(new XVar(".isUseToolTips"));
		}
		public virtual XVar isUseVideo()
		{
			return getTableData(new XVar(".isUseVideo"));
		}
		public virtual XVar isUseAudio()
		{
			return getTableData(new XVar(".isUseAudio"));
		}
		public virtual XVar isUseAudioOnDetails()
		{
			dynamic i = null;
			i = new XVar(0);
			for(;i < MVCFunctions.count(this._detailsTableData); i++)
			{
				if(XVar.Pack(this._detailsTableData[i]["isUseAudio"]))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getTableType()
		{
			return getTableData(new XVar(".tableType"));
		}
		public virtual XVar getShortTableName()
		{
			return getTableData(new XVar(".shortTableName"));
		}
		public virtual XVar isShowAddInPopup()
		{
			return getTableData(new XVar(".showAddInPopup"));
		}
		public virtual XVar isShowEditInPopup()
		{
			return getTableData(new XVar(".showEditInPopup"));
		}
		public virtual XVar isShowViewInPopup()
		{
			return getTableData(new XVar(".showViewInPopup"));
		}
		public virtual XVar getPopupPagesLayoutNames()
		{
			return getTableData(new XVar(".popupPagesLayoutNames"));
		}
		public virtual XVar isResizeColumns()
		{
			return getTableData(new XVar(".isResizeColumns"));
		}
		public virtual XVar isUseAjaxSuggest()
		{
			return getTableData(new XVar(".isUseAjaxSuggest"));
		}
		public virtual XVar getDetailsLinksOnList()
		{
			return getTableData(new XVar(".detailsLinksOnList"));
		}
		public virtual XVar getPanelSearchFields()
		{
			return getTableData(new XVar(".panelSearchFields"));
		}
		public virtual XVar getSearchPanelOptions()
		{
			return getTableData(new XVar(".searchPanelOptions"));
		}
		public virtual XVar getGoogleLikeFields()
		{
			return getTableData(new XVar(".googleLikeFields"));
		}
		public virtual XVar getInlineEditFields()
		{
			return getTableData(new XVar(".inlineEditFields"));
		}
		public virtual XVar getUpdateSelectedFields()
		{
			return getTableData(new XVar(".updateSelectedFields"));
		}
		public virtual XVar getExportFields()
		{
			return getTableData(new XVar(".exportFields"));
		}
		public virtual XVar getImportFields()
		{
			return getTableData(new XVar(".importFields"));
		}
		public virtual XVar getEditFields()
		{
			return getTableData(new XVar(".editFields"));
		}
		public virtual XVar getInlineAddFields()
		{
			return getTableData(new XVar(".inlineAddFields"));
		}
		public virtual XVar getAddFields()
		{
			return getTableData(new XVar(".addFields"));
		}
		public virtual XVar getMasterListFields()
		{
			return getTableData(new XVar(".masterListFields"));
		}
		public virtual XVar getViewFields()
		{
			return getTableData(new XVar(".viewFields"));
		}
		public virtual XVar getPrinterFields()
		{
			return getTableData(new XVar(".printFields"));
		}
		public virtual XVar getListFields()
		{
			return getTableData(new XVar(".listFields"));
		}
		public virtual XVar isAddPageEvents()
		{
			return getTableData(new XVar(".addPageEvents"));
		}
		public virtual XVar hasAjaxSnippet()
		{
			return getTableData(new XVar(".ajaxCodeSnippetAdded"));
		}
		public virtual XVar hasButtonsAdded()
		{
			return getTableData(new XVar(".buttonsAdded"));
		}
		public virtual XVar isUseMainMaps()
		{
			return getTableData(new XVar(".isUseMainMaps"));
		}
		public virtual XVar isUseFieldsMaps()
		{
			return getTableData(new XVar(".isUseFieldsMaps"));
		}
		public virtual XVar getOrderIndexes()
		{
			return getTableData(new XVar(".orderindexes"));
		}
		public virtual XVar getStrOrderBy()
		{
			return getTableData(new XVar(".strOrderBy"));
		}
		public virtual SQLQuery getSQLQuery()
		{
			dynamic query = null;
			query = XVar.Clone(getTableData(new XVar(".sqlquery")));
			if(query != null)
			{
				return query;
			}
			return null;
		}
		public virtual XVar getSQLQueryByField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic query = null;
			if(getTableType() == Constants.PAGE_DASHBOARD)
			{
				query = XVar.Clone(getDashTableData((XVar)(field), new XVar(".sqlquery")));
				if(query != null)
				{
					return query;
				}
				return null;
			}
			else
			{
				query = XVar.Clone(getTableData(new XVar(".sqlquery")));
				if(query != null)
				{
					return query;
				}
				return null;
			}
			return null;
		}
		public virtual XVar getCreateThumbnail(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("CreateThumbnail"));
		}
		public virtual XVar getStrThumbnail(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("StrThumbnail"));
		}
		public virtual XVar getThumbnailSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ThumbnailSize"));
		}
		public virtual XVar getResizeOnUpload(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ResizeImage"));
		}
		public virtual XVar isBasicUploadUsed(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("CompatibilityMode"));
		}
		public virtual XVar isAutoUpload(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("autoUpload"));
		}
		public virtual XVar getNewImageSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("NewSize"));
		}
		public virtual XVar getAcceptFileTypes(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("acceptFileTypes"));
		}
		public virtual XVar getMaxFileSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("maxFileSize"));
		}
		public virtual XVar getMaxTotalFilesSize(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("maxTotalFilesSize"));
		}
		public virtual XVar getMaxNumberOfFiles(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("maxNumberOfFiles"));
		}
		public virtual XVar getMaxImageWidth(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("maxImageWidth"));
		}
		public virtual XVar getMaxImageHeight(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("maxImageHeight"));
		}
		public virtual XVar getStrFilename(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("strFilename"));
		}
		public virtual XVar getNRows(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("nRows"));
		}
		public virtual XVar getNCols(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("nCols"));
		}
		public virtual XVar getOriginalTableName()
		{
			dynamic result = null;
			result = XVar.Clone(getTableData(new XVar(".OriginalTable")));
			return (XVar.Pack(result != XVar.Pack("")) ? XVar.Pack(result) : XVar.Pack(this._table));
		}
		public virtual XVar getTableKeys()
		{
			return getTableData(new XVar(".Keys"));
		}
		public virtual XVar isLargeTextTruncationSet()
		{
			return getTableData(new XVar(".truncateText"));
		}
		public virtual XVar getNumberOfChars()
		{
			return getTableData(new XVar(".NumberOfChars"));
		}
		public virtual XVar isSQLExpression(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("isSQLExpression"));
		}
		public virtual XVar getFullFieldName(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("FullName"));
		}
		public virtual XVar setFullFieldName(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return setFieldData((XVar)(field), new XVar("FullName"), (XVar)(value));
		}
		public virtual XVar getTableOwnerID()
		{
			return getTableData(new XVar(".OwnerID"));
		}
		public virtual XVar isRequired(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("IsRequired"));
		}
		public virtual XVar isUseRTE(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("UseRTE"));
		}
		public virtual XVar isUseRTEBasic(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return (XVar)(isUseRTE((XVar)(field)))  && (XVar)(GlobalVars.isUseRTEBasic);
		}
		public virtual XVar isUseRTEFCK(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return (XVar)(isUseRTE((XVar)(field)))  && (XVar)(GlobalVars.isUseRTECK);
		}
		public virtual XVar isUseRTEInnova(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return (XVar)(isUseRTE((XVar)(field)))  && (XVar)(GlobalVars.isUseRTEInnova);
		}
		public virtual XVar isUseTimestamp(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("UseTimestamp"));
		}
		public virtual XVar getFieldIndex(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("Index"));
		}
		public virtual XVar getEntityType()
		{
			return getTableData(new XVar(".entityType"));
		}
		public virtual XVar getDateEditType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("DateEditType"));
		}
		public virtual XVar getHTML5InputType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("HTML5InuptType"));
		}
		public virtual XVar getEditParams(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("EditParams"));
		}
		public virtual XVar getControlWidth(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("controlWidth"));
		}
		public virtual XVar checkFieldPermissions(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("FieldPermissions"));
		}
		public virtual XVar getTableOwnerIdField()
		{
			return getTableData(new XVar(".mainTableOwnerID"));
		}
		public virtual XVar isHorizontalLookup(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("HorizontalLookup"));
		}
		public virtual XVar isDecimalDigits(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("DecimalDigits"));
		}
		public virtual XVar getLookupValues(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("LookupValues"));
		}
		public virtual XVar hasEditPage()
		{
			return getTableData(new XVar(".edit"));
		}
		public virtual XVar hasAddPage()
		{
			return getTableData(new XVar(".add"));
		}
		public virtual XVar hasListPage()
		{
			return getTableData(new XVar(".list"));
		}
		public virtual XVar hasImportPage()
		{
			return getTableData(new XVar(".import"));
		}
		public virtual XVar hasInlineEdit()
		{
			return getTableData(new XVar(".inlineEdit"));
		}
		public virtual XVar hasUpdateSelected()
		{
			return getTableData(new XVar(".updateSelected"));
		}
		public virtual XVar hasReorderingByHeader()
		{
			return getTableData(new XVar(".reorderRecordsByHeader"));
		}
		public virtual XVar hasSortByDropdown()
		{
			return getTableData(new XVar(".createSortByDropdown"));
		}
		public virtual XVar getSortControlSettingsJSONString()
		{
			return getTableData(new XVar(".strSortControlSettingsJSON"));
		}
		public virtual XVar getClickActionJSONString()
		{
			return getTableData(new XVar(".strClickActionJSON"));
		}
		public virtual XVar hasCopyPage()
		{
			return getTableData(new XVar(".copy"));
		}
		public virtual XVar hasViewPage()
		{
			return getTableData(new XVar(".view"));
		}
		public virtual XVar hasExportPage()
		{
			return getTableData(new XVar(".exportTo"));
		}
		public virtual XVar hasPrintPage()
		{
			return getTableData(new XVar(".printFriendly"));
		}
		public virtual XVar hasDelete()
		{
			return getTableData(new XVar(".delete"));
		}
		public virtual XVar getTotalsFields()
		{
			return getTableData(new XVar(".totalsFields"));
		}
		public virtual XVar getExportTxtFormattingType()
		{
			return getTableData(new XVar(".exportFormatting"));
		}
		public virtual XVar getExportDelimiter()
		{
			return getTableData(new XVar(".exportDelimiter"));
		}
		public virtual XVar chekcExportDelimiterSelection()
		{
			return getTableData(new XVar(".selectExportDelimiter"));
		}
		public virtual XVar checkExportFieldsSelection()
		{
			return getTableData(new XVar(".selectExportFields"));
		}
		public virtual XVar getAdvancedSecurityType()
		{
			if((XVar)(!(XVar)(CommonFunctions.GetGlobalData(new XVar("createLoginPage"), new XVar(null))))  || (XVar)((XVar)(CommonFunctions.GetGlobalData(new XVar("nLoginMethod"), new XVar(null)) != Constants.SECURITY_TABLE)  && (XVar)(CommonFunctions.GetGlobalData(new XVar("nLoginMethod"), new XVar(null)) != Constants.SECURITY_AD)))
			{
				return Constants.ADVSECURITY_ALL;
			}
			return getTableData(new XVar(".nSecOptions"));
		}
		public virtual XVar displayLoading()
		{
			return getTableData(new XVar(".isDisplayLoading"));
		}
		public virtual XVar getRecordsPerPageArray()
		{
			return getTableData(new XVar(".arrRecsPerPage"));
		}
		public virtual XVar getGroupsPerPageArray()
		{
			return getTableData(new XVar(".arrGroupsPerPage"));
		}
		public virtual XVar isReportWithGroups()
		{
			return getTableData(new XVar(".reportGroupFields"));
		}
		public virtual XVar isCrossTabReport()
		{
			return getTableData(new XVar(".crossTabReport"));
		}
		public virtual XVar getReportGroupFieldsData()
		{
			return getTableData(new XVar(".reportGroupFieldsData"));
		}
		public virtual XVar reportHasHorizontalSummary()
		{
			return getTableData(new XVar(".reportHorizontalSummary"));
		}
		public virtual XVar reportHasVerticalSummary()
		{
			return getTableData(new XVar(".reportVerticalSummary"));
		}
		public virtual XVar reportHasPageSummary()
		{
			return getTableData(new XVar(".reportPageSummary"));
		}
		public virtual XVar reportHasGlobalSummary()
		{
			return getTableData(new XVar(".reportGlobalSummary"));
		}
		public virtual XVar getReportLayout()
		{
			return getTableData(new XVar(".reportLayout"));
		}
		public virtual XVar isGroupSummaryCountShown()
		{
			return getTableData(new XVar(".showGroupSummaryCount"));
		}
		public virtual XVar reportDetailsShown()
		{
			return getTableData(new XVar(".repShowDet"));
		}
		public virtual XVar reportTotalFieldsExist()
		{
			return getTableData(new XVar(".isExistTotalFields"));
		}
		public virtual XVar noRecordsOnFirstPage()
		{
			return getTableData(new XVar(".noRecordsFirstPage"));
		}
		public virtual XVar isViewPagePDF()
		{
			return getTableData(new XVar(".isViewPagePDF"));
		}
		public virtual XVar isLandscapeViewPDFOrientation()
		{
			return getTableData(new XVar(".isLandscapeViewPDFOrientation"));
		}
		public virtual XVar isViewPagePDFFitToPage()
		{
			return getTableData(new XVar(".isViewPagePDFFitToPage"));
		}
		public virtual XVar getViewPagePDFScale()
		{
			return getTableData(new XVar(".nViewPagePDFScale"));
		}
		public virtual XVar isLandscapePrinterPagePDFOrientation()
		{
			return getTableData(new XVar(".isLandscapePrinterPagePDFOrientation"));
		}
		public virtual XVar isPrinterPagePDFFitToPage()
		{
			return getTableData(new XVar(".isPrinterPagePDFFitToPage"));
		}
		public virtual XVar getPrinterPagePDFScale()
		{
			return getTableData(new XVar(".nPrinterPagePDFScale"));
		}
		public virtual XVar isPrinterPageFitToPage()
		{
			return getTableData(new XVar(".isPrinterPageFitToPage"));
		}
		public virtual XVar getPrinterPageScale()
		{
			return getTableData(new XVar(".nPrinterPageScale"));
		}
		public virtual XVar getPrinterSplitRecords()
		{
			return getTableData(new XVar(".nPrinterSplitRecords"));
		}
		public virtual XVar getPrinterPDFSplitRecords()
		{
			return getTableData(new XVar(".nPrinterPDFSplitRecords"));
		}
		public virtual XVar isPrinterPagePDF()
		{
			return getTableData(new XVar(".isPrinterPagePDF"));
		}
		public virtual XVar isCaptchaEnabledOnEdit()
		{
			return getTableData(new XVar(".isCaptchaEnabledOnEdit"));
		}
		public virtual XVar isCaptchaEnabledOnAdd()
		{
			return getTableData(new XVar(".isCaptchaEnabledOnAdd"));
		}
		public virtual XVar captchaEditFieldName()
		{
			return getTableData(new XVar(".captchaEditFieldName"));
		}
		public virtual XVar captchaAddFieldName()
		{
			return getTableData(new XVar(".captchaAddFieldName"));
		}
		public virtual XVar isSearchRequiredForFiltering()
		{
			return getTableData(new XVar(".searchIsRequiredForFilters"));
		}
		public virtual XVar warnLeavingPages()
		{
			return getTableData(new XVar(".warnLeavingPages"));
		}
		public virtual XVar hideEmptyViewFields()
		{
			return getTableData(new XVar(".hideEmptyFieldsOnView"));
		}
		public virtual XVar getInitialPageSize()
		{
			return getTableData(new XVar(".pageSize"));
		}
		public virtual XVar getRecordsPerRowList()
		{
			return getTableData(new XVar(".recsPerRowList"));
		}
		public virtual XVar getRecordsPerRowPrint()
		{
			return getTableData(new XVar(".recsPerRowPrint"));
		}
		public virtual XVar getRecordsLimit()
		{
			return getTableData(new XVar(".recsLimit"));
		}
		public virtual XVar useMoveNext()
		{
			return getTableData(new XVar(".moveNext"));
		}
		public virtual XVar highlightRows()
		{
			return getTableData(new XVar(".rowHighlite"));
		}
		public virtual XVar hasInlineAdd()
		{
			return getTableData(new XVar(".inlineAdd"));
		}
		public virtual XVar getListGridLayout()
		{
			return getTableData(new XVar(".listGridLayout"));
		}
		public virtual XVar getPrintGridLayout()
		{
			return getTableData(new XVar(".printGridLayout"));
		}
		public virtual XVar getReportPrintLayout()
		{
			return getTableData(new XVar(".printReportLayout"));
		}
		public virtual XVar getPrinterPageOrientation()
		{
			return getTableData(new XVar(".printerPageOrientation"));
		}
		public virtual XVar getReportPrintPartitionType()
		{
			return getTableData(new XVar(".reportPrintPartitionType"));
		}
		public virtual XVar getReportPrintGroupsPerPage()
		{
			return getTableData(new XVar(".reportPrintGroupsPerPage"));
		}
		public virtual XVar getReportPrintPDFGroupsPerPage()
		{
			return getTableData(new XVar(".reportPrintPDFGroupsPerPage"));
		}
		public virtual XVar getLowGroup()
		{
			return getTableData(new XVar(".lowGroup"));
		}
		public virtual XVar ajaxBasedListPage()
		{
			return getTableData(new XVar(".listAjax"));
		}
		public virtual XVar isAddMultistep()
		{
			return getTableData(new XVar(".addMultistep"));
		}
		public virtual XVar isEditMultistep()
		{
			return getTableData(new XVar(".editMultistep"));
		}
		public virtual XVar isViewMultistep()
		{
			return getTableData(new XVar(".viewMultistep"));
		}
		public virtual XVar isRegisterMultistep()
		{
			return getTableData(new XVar(".registerMultistep"));
		}
		public virtual XVar getGridTabs()
		{
			return getTableData(new XVar(".arrGridTabs"));
		}
		public virtual XVar getAddTabs()
		{
			return getTableData(new XVar(".arrAddTabs"));
		}
		public virtual XVar useTabsOnAdd()
		{
			if(XVar.Pack(MVCFunctions.count(getAddTabs())))
			{
				return true;
			}
			return false;
		}
		public virtual XVar getEditTabs()
		{
			return getTableData(new XVar(".arrEditTabs"));
		}
		public virtual XVar useTabsOnEdit()
		{
			if(XVar.Pack(MVCFunctions.count(getEditTabs())))
			{
				return true;
			}
			return false;
		}
		public virtual XVar getViewTabs()
		{
			return getTableData(new XVar(".arrViewTabs"));
		}
		public virtual XVar useTabsOnView()
		{
			if(XVar.Pack(MVCFunctions.count(getViewTabs())))
			{
				return true;
			}
			return false;
		}
		public virtual XVar getRegisterTabs()
		{
			return getTableData(new XVar(".arrRegisterTabs"));
		}
		public virtual XVar useTabsOnRegister()
		{
			if(XVar.Pack(MVCFunctions.count(getRegisterTabs())))
			{
				return true;
			}
			return false;
		}
		public virtual XVar highlightSearchResults()
		{
			return getTableData(new XVar(".highlightSearchResults"));
		}
		public virtual XVar getFieldsList()
		{
			dynamic arr = XVar.Array(), t = XVar.Array();
			if(XVar.Pack(this._tableData == null))
			{
				return XVar.Array();
			}
			t = XVar.Clone(MVCFunctions.array_keys((XVar)(this._tableData)));
			arr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in t.GetEnumerator())
			{
				if(MVCFunctions.substr((XVar)(f.Value), new XVar(0), new XVar(1)) != ".")
				{
					arr.InitAndSetArrayItem(f.Value, null);
				}
			}
			return arr;
		}
		public virtual XVar getBinaryFieldsIndices()
		{
			dynamic fields = XVar.Array(), var_out = XVar.Array();
			fields = XVar.Clone(getFieldsList());
			var_out = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(getFieldType((XVar)(f.Value))))))
				{
					var_out.InitAndSetArrayItem(f.Key + 1, null);
				}
			}
			return var_out;
		}
		public virtual XVar getNBFieldsList()
		{
			dynamic arr = XVar.Array(), t = XVar.Array();
			t = XVar.Clone(getFieldsList());
			arr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in t.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(CommonFunctions.IsBinaryType((XVar)(getFieldType((XVar)(f.Value)))))))
				{
					arr.InitAndSetArrayItem(f.Value, null);
				}
			}
			return arr;
		}
		public virtual XVar getFieldByGoodFieldName(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> value in this._tableData.GetEnumerator())
			{
				if(1 < MVCFunctions.count(value.Value))
				{
					if(value.Value["GoodName"] == field)
					{
						return value.Key;
					}
				}
			}
			return "";
		}
		public virtual XVar getUploadFolder(dynamic _param_field, dynamic _param_fileData = null)
		{
			#region default values
			if(_param_fileData as Object == null) _param_fileData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic fileData = XVar.Clone(_param_fileData);
			#endregion

			dynamic path = null;
			if(XVar.Pack(isUploadCodeExpression((XVar)(field))))
			{
				path = XVar.Clone(MVCFunctions.GetUploadFolderExpression((XVar)(field), (XVar)(fileData)));
			}
			else
			{
				path = XVar.Clone(getFieldData((XVar)(field), new XVar("UploadFolder")));
			}
			if((XVar)(MVCFunctions.strlen((XVar)(path)))  && (XVar)(MVCFunctions.substr((XVar)(path), (XVar)(MVCFunctions.strlen((XVar)(path)) - 1)) != "/"))
			{
				path = MVCFunctions.Concat(path, "/");
			}
			return path;
		}
		public virtual XVar isMakeDirectoryNeeded(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return (XVar)(isUploadCodeExpression((XVar)(field)))  || (XVar)(!(XVar)(isAbsolute((XVar)(field))));
		}
		public virtual XVar getFinalUploadFolder(dynamic _param_field, dynamic _param_fileData = null)
		{
			#region default values
			if(_param_fileData as Object == null) _param_fileData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic fileData = XVar.Clone(_param_fileData);
			#endregion

			dynamic path = null;
			if(XVar.Pack(isAbsolute((XVar)(field))))
			{
				path = XVar.Clone(getUploadFolder((XVar)(field), (XVar)(fileData)));
			}
			else
			{
				path = XVar.Clone(MVCFunctions.getabspath((XVar)(getUploadFolder((XVar)(field), (XVar)(fileData)))));
			}
			if((XVar)(MVCFunctions.strlen((XVar)(path)))  && (XVar)(MVCFunctions.substr((XVar)(path), (XVar)(MVCFunctions.strlen((XVar)(path)) - 1)) != "\\"))
			{
				path = MVCFunctions.Concat(path, "\\");
			}
			return path;
		}
		public virtual XVar isUploadCodeExpression(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("UploadCodeExpression"));
		}
		public virtual XVar getQueryObject()
		{
			dynamic queryObj = null;
			queryObj = XVar.Clone(getSQLQuery());
			return queryObj;
		}
		public virtual XVar getListOfFieldsByExprType(dynamic _param_needaggregate)
		{
			#region pass-by-value parameters
			dynamic needaggregate = XVar.Clone(_param_needaggregate);
			#endregion

			dynamic fields = XVar.Array(), query = null, var_out = XVar.Array();
			query = getSQLQuery();
			fields = XVar.Clone(getFieldsList());
			var_out = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				dynamic aggr = null;
				aggr = XVar.Clone(query.IsAggrFuncField((XVar)(f.Key)));
				if((XVar)((XVar)(needaggregate)  && (XVar)(aggr))  || (XVar)((XVar)(!(XVar)(needaggregate))  && (XVar)(!(XVar)(aggr))))
				{
					var_out.InitAndSetArrayItem(f.Value, null);
				}
			}
			return var_out;
		}
		public virtual XVar isAggregateField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic idx = null, query = null;
			query = getSQLQuery();
			idx = XVar.Clone(getFieldIndex((XVar)(field)) - 1);
			return query.IsAggrFuncField((XVar)(idx));
		}
		public virtual XVar getNCSearch()
		{
			return getTableData(new XVar(".NCSearch"));
		}
		public virtual XVar getChartType()
		{
			return getTableData(new XVar(".chartType"));
		}
		public virtual XVar getChartRefreshTime()
		{
			return getTableData(new XVar(".ChartRefreshTime"));
		}
		public virtual XVar getChartXml()
		{
			return getTableData(new XVar(".chartXml"));
		}
		public virtual XVar auditEnabled()
		{
			return getTableData(new XVar(".audit"));
		}
		public virtual XVar isSearchSavingEnabled()
		{
			return getTableData(new XVar(".searchSaving"));
		}
		public virtual XVar isAllowShowHideFields()
		{
			if(XVar.Pack(getScrollGridBody()))
			{
				return false;
			}
			return getTableData(new XVar(".allowShowHideFields"));
		}
		public virtual XVar isAllowFieldsReordering()
		{
			if((XVar)(getScrollGridBody())  || (XVar)(1 < getRecordsPerRowList()))
			{
				return false;
			}
			return getTableData(new XVar(".allowFieldsReordering"));
		}
		public virtual XVar lockingEnabled()
		{
			return getTableData(new XVar(".locking"));
		}
		public virtual XVar hasEncryptedFields()
		{
			return getTableData(new XVar(".hasEncryptedFields"));
		}
		public virtual XVar showSearchPanel()
		{
			return getTableData(new XVar(".showSearchPanel"));
		}
		public virtual XVar isFlexibleSearch()
		{
			return getTableData(new XVar(".flexibleSearch"));
		}
		public virtual XVar getSearchRequiredFields()
		{
			return getTableData(new XVar(".requiredSearchFields"));
		}
		public virtual XVar showSimpleSearchOptions()
		{
			return getTableData(new XVar(".showSimpleSearchOptions"));
		}
		public virtual XVar getFilterFields()
		{
			return getTableData(new XVar(".filterFields"));
		}
		public virtual XVar getFilterFieldFormat(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterFormat"));
		}
		public virtual XVar getFilterFieldTotal(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterTotals"));
		}
		public virtual XVar showWithNoRecords(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("showWithNoRecords"));
		}
		public virtual XVar getFilterSortValueType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("sortValueType"));
		}
		public virtual XVar isFilterSortOrderDescending(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("descendingOrder"));
		}
		public virtual XVar getNumberOfVisibleFilterItems(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("numberOfVisibleItems"));
		}
		public virtual XVar getParentFilterName(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("parentFilterField"));
		}
		public virtual XVar getParentFiltersNames(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("parentFilters"));
		}
		public virtual XVar hasDependentFilter(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getDependentFilterName((XVar)(field)) != "";
		}
		public virtual XVar getDependentFilterName(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("dependentFilterName"));
		}
		public virtual XVar getDependentFiltersNames(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("dependentFilters"));
		}
		public virtual XVar getFilterIntervals(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterIntervals"));
		}
		public virtual XVar showCollapsed(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("showCollapsed"));
		}
		public virtual XVar getFilterIntervalDatabyIndex(dynamic _param_field, dynamic _param_idx)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic idx = XVar.Clone(_param_idx);
			#endregion

			dynamic filterIntervalsData = XVar.Array(), intervalData = null;
			intervalData = XVar.Clone(XVar.Array());
			filterIntervalsData = XVar.Clone(getFilterIntervals((XVar)(field)));
			foreach (KeyValuePair<XVar, dynamic> interval in filterIntervalsData.GetEnumerator())
			{
				if(interval.Value["index"] == idx)
				{
					intervalData = XVar.Clone(interval.Value);
					break;
				}
			}
			return intervalData;
		}
		public virtual XVar getFilterTotalsField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterTotalFields"));
		}
		public virtual XVar getFilterFiledMultiSelect(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterMultiSelect"));
		}
		public virtual XVar getFilterCheckedMessage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterCheckedMessageText"));
		}
		public virtual XVar getFilterCheckedMessageType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterCheckedMessageType"));
		}
		public virtual XVar getFilterUncheckedMessage(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterUncheckedMessageText"));
		}
		public virtual XVar getFilterUncheckedMessageType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterUncheckedMessageType"));
		}
		public virtual XVar getFilterStepType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterSliderStepType"));
		}
		public virtual XVar getFilterStepValue(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterSliderStepValue"));
		}
		public virtual XVar getFilterKnobsType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterKnobsType"));
		}
		public virtual XVar isFilterApplyBtnSet(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("filterApplyBtn"));
		}
		public virtual XVar isCaseInsensitiveUsername()
		{
			return GlobalVars.caseInsensitiveUsername;
		}
		public virtual XVar getCaseSensitiveUsername(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if(XVar.Pack(!(XVar)(isCaseInsensitiveUsername())))
			{
				return value;
			}
			return MVCFunctions.strtoupper((XVar)(value));
		}
		public virtual XVar getStrField(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("strField"));
		}
		public virtual XVar getScrollGridBody()
		{
			return getTableData(new XVar(".scrollGridBody"));
		}
		public virtual XVar isUpdateLatLng()
		{
			return getTableData(new XVar(".geocodingEnabled"));
		}
		public virtual XVar getGeocodingData()
		{
			return getTableData(new XVar(".geocodingData"));
		}
		public virtual XVar allowDuplicateValues(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return !(XVar)(getFieldData((XVar)(field), new XVar("denyDuplicates")));
		}
		public virtual XVar getDashFieldData(dynamic _param_field, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic dashSearchFields = XVar.Array(), dfield = XVar.Array(), table = null;
			dashSearchFields = XVar.Clone(getDashboardSearchFields());
			dfield = XVar.Clone(dashSearchFields[field]);
			if(XVar.Pack(dfield))
			{
				table = XVar.Clone(dfield[0]["table"]);
			}
			if((XVar)(!(XVar)(dfield))  || (XVar)(!(XVar)(table)))
			{
				return getDefaultValueByKey((XVar)(key));
			}
			if(XVar.Pack(!(XVar)(this._dashboardElemPSet[table])))
			{
				this._dashboardElemPSet.InitAndSetArrayItem(new ProjectSettings((XVar)(table), (XVar)(this._editPage)), table);
			}
			return this._dashboardElemPSet[table].getFieldData((XVar)(dfield[0]["field"]), (XVar)(key));
		}
		public virtual XVar getDashTableData(dynamic _param_field, dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic key = XVar.Clone(_param_key);
			#endregion

			dynamic dashSearchFields = XVar.Array(), tableSettings = null;
			dashSearchFields = XVar.Clone(getDashboardSearchFields());
			tableSettings = XVar.Clone(new ProjectSettings((XVar)(dashSearchFields[field][0]["table"]), (XVar)(this._editPage)));
			return tableSettings.getTableData((XVar)(key));
		}
		public virtual XVar getSearchOptionsList(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("searchOptionsList"));
		}
		public virtual XVar getDefaultSearchOption(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic defaultOpt = null;
			defaultOpt = XVar.Clone(getFieldData((XVar)(field), new XVar("defaultSearchOption")));
			if(XVar.Pack(!(XVar)(defaultOpt)))
			{
				dynamic searchOptionsList = XVar.Array();
				searchOptionsList = XVar.Clone(getSearchOptionsList((XVar)(field)));
				if(XVar.Pack(MVCFunctions.count(searchOptionsList)))
				{
					defaultOpt = XVar.Clone(searchOptionsList[0]);
				}
			}
			return defaultOpt;
		}
		public virtual XVar showListOfThumbnails(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			return getFieldData((XVar)(field), new XVar("ShowListOfThumbnails"));
		}
		public virtual XVar getMenuName(dynamic _param_page, dynamic _param_id, dynamic _param_horizontal)
		{
			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			dynamic id = XVar.Clone(_param_id);
			dynamic horizontal = XVar.Clone(_param_horizontal);
			#endregion

			dynamic isho = null, menuId = null;
			menuId = XVar.Clone(id);
			isho = XVar.Clone((XVar.Pack(0 < MVCFunctions.strlen((XVar)(horizontal))) ? XVar.Pack("1") : XVar.Pack("0")));
			foreach (KeyValuePair<XVar, dynamic> m in GlobalVars.menuAssignments.GetEnumerator())
			{
				if((XVar)((XVar)(m.Value["page"] == page)  && (XVar)(m.Value["id"] == menuId))  && (XVar)(isho == m.Value["horizontal"]))
				{
					return m.Value["name"];
				}
			}
			return "main";
		}
		public virtual XVar getMenuStyle(dynamic _param_page, dynamic _param_id, dynamic _param_horizontal)
		{
			#region pass-by-value parameters
			dynamic page = XVar.Clone(_param_page);
			dynamic id = XVar.Clone(_param_id);
			dynamic horizontal = XVar.Clone(_param_horizontal);
			#endregion

			dynamic isho = null, menuId = null;
			menuId = XVar.Clone(id);
			isho = XVar.Clone((bool)MVCFunctions.strlen((XVar)(horizontal)));
			foreach (KeyValuePair<XVar, dynamic> m in GlobalVars.menuStyles.GetEnumerator())
			{
				if((XVar)((XVar)(m.Value["page"] == page)  && (XVar)(m.Value["id"] == menuId))  && (XVar)(isho == (bool)m.Value["horizontal"]))
				{
					return m.Value["style"];
				}
			}
			if(id == "main")
			{
				return 0;
			}
			return 1;
		}
		public static XVar isMenuTreelike(dynamic _param_menuName)
		{
			#region pass-by-value parameters
			dynamic menuName = XVar.Clone(_param_menuName);
			#endregion

			return GlobalVars.menuTreelikeFlags[menuName];
		}
		public static XVar isMenuDrillDown(dynamic _param_menuName)
		{
			#region pass-by-value parameters
			dynamic menuName = XVar.Clone(_param_menuName);
			#endregion

			return GlobalVars.menuDrillDownFlags[menuName];
		}
		public virtual XVar setPageMode(dynamic _param_pageMode)
		{
			#region pass-by-value parameters
			dynamic pageMode = XVar.Clone(_param_pageMode);
			#endregion

			this._pageMode = XVar.Clone(pageMode);
			return null;
		}
		public virtual XVar editPageHasDenyDuplicatesFields()
		{
			foreach (KeyValuePair<XVar, dynamic> fieldName in getEditFields().GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(allowDuplicateValues((XVar)(fieldName.Value)))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getRTEType(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			if(XVar.Pack(isUseRTEBasic((XVar)(field))))
			{
				return "RTE";
			}
			if(XVar.Pack(isUseRTEFCK((XVar)(field))))
			{
				return "RTECK";
			}
			if(XVar.Pack(isUseRTEInnova((XVar)(field))))
			{
				return "RTEINNOVA";
			}
			return "";
		}
		public virtual XVar getHiddenFields(dynamic _param_device)
		{
			#region pass-by-value parameters
			dynamic device = XVar.Clone(_param_device);
			#endregion

			dynamic list = XVar.Array();
			list = XVar.Clone(getTableData(new XVar(".hideMobileList")));
			if(XVar.Pack(list.KeyExists(device)))
			{
				return list[device];
			}
			return XVar.Array();
		}
		public virtual XVar getHiddenGoodNameFields(dynamic _param_device)
		{
			#region pass-by-value parameters
			dynamic device = XVar.Clone(_param_device);
			#endregion

			dynamic hFields = XVar.Array(), hGoodFields = XVar.Array();
			hGoodFields = XVar.Clone(XVar.Array());
			hFields = XVar.Clone(getHiddenFields((XVar)(device)));
			foreach (KeyValuePair<XVar, dynamic> isShow in hFields.GetEnumerator())
			{
				hGoodFields.InitAndSetArrayItem(isShow.Value, MVCFunctions.GoodFieldName((XVar)(isShow.Key)));
			}
			return hGoodFields;
		}
		public virtual XVar columnsByDeviceEnabled()
		{
			dynamic list = XVar.Array();
			list = XVar.Clone(getTableData(new XVar(".hideMobileList")));
			foreach (KeyValuePair<XVar, dynamic> v in list.GetEnumerator())
			{
				if(XVar.Pack(v.Value))
				{
					return true;
				}
			}
			return false;
		}
		public static XVar getDeviceMediaClause(dynamic _param_device)
		{
			#region pass-by-value parameters
			dynamic device = XVar.Clone(_param_device);
			#endregion

			if(device == Constants.DESKTOP)
			{
				return "@media (min-device-width: 1281px)";
			}
			else
			{
				if(device == Constants.TABLET_10_IN)
				{
					return MVCFunctions.Concat("@media (device-width: 768px) and (device-height: 1024px)", " , (min-device-width: 1025px) and (max-device-width: 1280px) and (max-device-height: 1023px) , (min-device-height: 1025px) and (max-device-height: 1280px) and (max-device-width: 1023px)");
				}
				else
				{
					if(device == Constants.TABLET_7_IN)
					{
						return "@media (min-device-height: 401px) and (max-device-height: 800px) and (min-device-width: 401px) and (max-device-width: 1024px) , (min-device-height: 401px) and (min-device-width: 401px) and (max-device-height: 1024px) and (max-device-width: 800px)";
					}
					else
					{
						if(device == Constants.SMARTPHONE_LANDSCAPE)
						{
							return "@media (orientation: landscape) and (max-device-height: 400px), (orientation: landscape) and (max-device-width: 400px)";
						}
						else
						{
							if(device == Constants.SMARTPHONE_PORTRAIT)
							{
								return "@media (orientation: portrait) and (max-device-height: 400px), (orientation: portrait) and (max-device-width: 400px)";
							}
						}
					}
				}
			}
			return null;
		}
		public static XVar getForLogin()
		{
			return new ProjectSettings(new XVar("dbo._ABCSecurity"), new XVar(Constants.PAGE_LIST));
			return null;
		}
		public virtual XVar getDashboardSearchFields()
		{
			return getTableData(new XVar(".searchFields"));
		}
		public virtual XVar getDashboardElements()
		{
			return getTableData(new XVar(".dashElements"));
		}
		public virtual XVar getDashboardElementData(dynamic _param_dashElementName)
		{
			#region pass-by-value parameters
			dynamic dashElementName = XVar.Clone(_param_dashElementName);
			#endregion

			dynamic dElements = XVar.Array();
			dElements = XVar.Clone(getTableData(new XVar(".dashElements")));
			foreach (KeyValuePair<XVar, dynamic> dElemData in dElements.GetEnumerator())
			{
				if(dElemData.Value["elementName"] == dashElementName)
				{
					return dElemData.Value;
				}
			}
			return XVar.Array();
		}
		public virtual XVar getAfterAddAction()
		{
			return getTableData(new XVar(".afterAddAction"));
		}
		public virtual XVar getAADetailTable()
		{
			return getTableData(new XVar(".afterAddActionDetTable"));
		}
		public virtual XVar checkClosePopupAfterAdd()
		{
			return getTableData(new XVar(".closePopupAfterAdd"));
		}
		public virtual XVar getAfterEditAction()
		{
			return getTableData(new XVar(".afterEditAction"));
		}
		public virtual XVar getAEDetailTable()
		{
			return getTableData(new XVar(".afterEditActionDetTable"));
		}
		public virtual XVar checkClosePopupAfterEdit()
		{
			return getTableData(new XVar(".closePopupAfterEdit"));
		}
		public virtual XVar getMapIcon(dynamic _param_field, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			if(XVar.Pack(!(XVar)(isMapIconCustom((XVar)(field)))))
			{
				dynamic mapData = XVar.Array();
				mapData = XVar.Clone(getMapData((XVar)(field)));
				if(mapData["mapIcon"] != "")
				{
					return MVCFunctions.Concat("images/menuicons/", mapData["mapIcon"]);
				}
				return "";
			}
			else
			{
				return MVCFunctions.getCustomMapIcon((XVar)(field), new XVar(""), (XVar)(data));
			}
			return null;
		}
		public virtual XVar getDashMapIcon(dynamic _param_dashElementName, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic dashElementName = XVar.Clone(_param_dashElementName);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic dashElementData = XVar.Array();
			dashElementData = XVar.Clone(getDashboardElementData((XVar)(dashElementName)));
			if(XVar.Pack(dashElementData["isMarkerIconCustom"]))
			{
				return MVCFunctions.getDashMapCustomIcon((XVar)(this._table), (XVar)(dashElementName), (XVar)(data));
			}
			if(XVar.Pack(dashElementData["iconF"]))
			{
				return dashElementData["iconF"];
			}
			return "";
		}
		public virtual XVar isMapIconCustom(dynamic _param_field)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			#endregion

			dynamic mapData = XVar.Array();
			mapData = XVar.Clone(getMapData((XVar)(field)));
			return mapData["isMapIconCustom"];
		}
		public virtual XVar getDetailsBadgeColor()
		{
			return getTableData(new XVar(".badgeColor"));
		}
	}
	// Included file globals
	public partial class CommonFunctions
	{
		public static XVar fillProjectEntites()
		{
			if(XVar.Pack(MVCFunctions.count(GlobalVars.projectEntities)))
			{
				return null;
			}
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "dbo__ABCReports", "type", 0), "dbo._ABCReports");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("dbo._ABCReports", "dbo__ABCReports");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "dbo__ABCVotes", "type", 0), "dbo._ABCVotes");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("dbo._ABCVotes", "dbo__ABCVotes");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "dbo__ABCSecurity", "type", 0), "dbo._ABCSecurity");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("dbo._ABCSecurity", "dbo__ABCSecurity");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "ABC_Voting_Submitted1", "type", 1), "ABC_Voting_Submitted");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("ABC_Voting_Submitted", "ABC_Voting_Submitted1");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "ABC_Voting_Recirculated1", "type", 1), "ABC_Voting_Recirculated");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("ABC_Voting_Recirculated", "ABC_Voting_Recirculated1");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "ABC_Voting_My_Voting", "type", 1), "ABC_Voting_My_Voting");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("ABC_Voting_My_Voting", "ABC_Voting_My_Voting");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "ABC_Voting_Batch_Create", "type", 1), "ABC_Voting_Batch_Create");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("ABC_Voting_Batch_Create", "ABC_Voting_Batch_Create");
			GlobalVars.projectEntities.InitAndSetArrayItem(new XVar("url", "dbo_vwABCReportsVoteCount", "type", 0), "dbo.vwABCReportsVoteCount");
			GlobalVars.projectEntitiesReverse.InitAndSetArrayItem("dbo.vwABCReportsVoteCount", "dbo_vwABCReportsVoteCount");
			return null;
		}
		public static XVar findTable(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic uTable = null;
			CommonFunctions.fillProjectEntites();
			if(XVar.Pack(GlobalVars.projectEntities.KeyExists(table)))
			{
				return table;
			}
			uTable = XVar.Clone(MVCFunctions.strtoupper((XVar)(table)));
			foreach (KeyValuePair<XVar, dynamic> d in GlobalVars.projectEntities.GetEnumerator())
			{
				dynamic gt = null;
				if(uTable == MVCFunctions.strtoupper((XVar)(d.Key)))
				{
					return d.Key;
				}
				gt = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(d.Key)));
				if((XVar)(table == gt)  || (XVar)(uTable == MVCFunctions.strtoupper((XVar)(gt))))
				{
					return d.Key;
				}
			}
			if(XVar.Pack(GlobalVars.projectEntitiesReverse.KeyExists(table)))
			{
				return GlobalVars.projectEntitiesReverse[table];
			}
			foreach (KeyValuePair<XVar, dynamic> v in GlobalVars.projectEntitiesReverse.GetEnumerator())
			{
				if(uTable == MVCFunctions.strtoupper((XVar)(v.Key)))
				{
					return v.Value;
				}
			}
			return "";
		}
		public static XVar GetTableURL(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			CommonFunctions.fillProjectEntites();
			if(XVar.Pack(GlobalVars.projectEntities.KeyExists(table)))
			{
				return GlobalVars.projectEntities[table]["url"];
			}
			return "";
		}
		public static XVar GetEntityType(dynamic _param_table = null)
		{
			#region default values
			if(_param_table as Object == null) _param_table = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			if(XVar.Pack(!(XVar)(table)))
			{
				table = XVar.Clone(GlobalVars.strTableName);
			}
			CommonFunctions.fillProjectEntites();
			if(XVar.Pack(GlobalVars.projectEntities.KeyExists(table)))
			{
				return GlobalVars.projectEntities[table]["type"];
			}
			return "";
		}
		public static XVar GetTableByShort(dynamic _param_shortTName)
		{
			#region pass-by-value parameters
			dynamic shortTName = XVar.Clone(_param_shortTName);
			#endregion

			CommonFunctions.fillProjectEntites();
			if(XVar.Pack(!(XVar)(shortTName)))
			{
				return false;
			}
			return GlobalVars.projectEntitiesReverse[shortTName];
		}
	}
}
