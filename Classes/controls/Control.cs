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
	public partial class EditControl : XClass
	{
		public dynamic pageObject = XVar.Pack(null);
		public dynamic container = XVar.Pack(null);
		public dynamic id = XVar.Pack("");
		public dynamic field = XVar.Pack("");
		public dynamic goodFieldName = XVar.Pack("");
		public dynamic format = XVar.Pack("");
		public dynamic cfieldname = XVar.Pack("");
		public dynamic cfield = XVar.Pack("");
		public dynamic ctype = XVar.Pack("");
		public dynamic is508 = XVar.Pack(false);
		public dynamic strLabel = XVar.Pack("");
		public dynamic var_type = XVar.Pack("");
		public dynamic inputStyle = XVar.Pack("");
		public dynamic iquery = XVar.Pack("");
		public dynamic keylink = XVar.Pack("");
		public dynamic webValue = XVar.Pack(null);
		public dynamic webType = XVar.Pack(null);
		public dynamic settings = XVar.Array();
		public dynamic isOracle = XVar.Pack(false);
		public dynamic ismssql = XVar.Pack(false);
		public dynamic isdb2 = XVar.Pack(false);
		public dynamic btexttype = XVar.Pack(false);
		public dynamic isMysql = XVar.Pack(false);
		public dynamic var_like = XVar.Pack("like");
		public dynamic searchOptions = XVar.Array();
		public dynamic searchPanelControl = XVar.Pack(false);
		public dynamic data = XVar.Array();
		protected dynamic connection;
		public EditControl(dynamic _param_field, dynamic _param_pageObject, dynamic _param_id, dynamic _param_connection)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic pageObject = XVar.Clone(_param_pageObject);
			dynamic id = XVar.Clone(_param_id);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			this.field = XVar.Clone(field);
			this.goodFieldName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
			setID((XVar)(id));
			this.connection = XVar.Clone(connection);
			this.pageObject = XVar.Clone(pageObject);
			this.is508 = XVar.Clone(CommonFunctions.isEnableSection508());
			this.strLabel = XVar.Clone(pageObject.pSetEdit.label((XVar)(field)));
			this.var_type = XVar.Clone(pageObject.pSetEdit.getFieldType((XVar)(this.field)));
			if(this.connection.dbType == Constants.nDATABASE_Oracle)
			{
				this.isOracle = new XVar(true);
			}
			if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				this.ismssql = new XVar(true);
			}
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				this.isdb2 = new XVar(true);
			}
			if(this.connection.dbType == Constants.nDATABASE_MySQL)
			{
				this.isMysql = new XVar(true);
			}
			if(this.connection.dbType == Constants.nDATABASE_PostgreSQL)
			{
				this.var_like = new XVar("ilike");
			}
			this.searchOptions.InitAndSetArrayItem("Contains", Constants.CONTAINS);
			this.searchOptions.InitAndSetArrayItem("Equals", Constants.EQUALS);
			this.searchOptions.InitAndSetArrayItem("Starts with", Constants.STARTS_WITH);
			this.searchOptions.InitAndSetArrayItem("More than", Constants.MORE_THAN);
			this.searchOptions.InitAndSetArrayItem("Less than", Constants.LESS_THAN);
			this.searchOptions.InitAndSetArrayItem("Between", Constants.BETWEEN);
			this.searchOptions.InitAndSetArrayItem("Empty", Constants.EMPTY_SEARCH);
			this.searchOptions.InitAndSetArrayItem("Doesn't contain", Constants.NOT_CONTAINS);
			this.searchOptions.InitAndSetArrayItem("Doesn't equal", Constants.NOT_EQUALS);
			this.searchOptions.InitAndSetArrayItem("Doesn't start with", Constants.NOT_STARTS_WITH);
			this.searchOptions.InitAndSetArrayItem("Is not more than", Constants.NOT_MORE_THAN);
			this.searchOptions.InitAndSetArrayItem("Is not less than", Constants.NOT_LESS_THAN);
			this.searchOptions.InitAndSetArrayItem("Is not between", Constants.NOT_BETWEEN);
			this.searchOptions.InitAndSetArrayItem("Is not empty", Constants.NOT_EMPTY);
			init();
		}
		public virtual XVar setID(dynamic _param_id)
		{
			#region pass-by-value parameters
			dynamic id = XVar.Clone(_param_id);
			#endregion

			this.id = XVar.Clone(id);
			this.cfieldname = XVar.Clone(MVCFunctions.Concat(this.goodFieldName, "_", id));
			this.cfield = XVar.Clone(MVCFunctions.Concat("value_", this.goodFieldName, "_", id));
			this.ctype = XVar.Clone(MVCFunctions.Concat("type_", this.goodFieldName, "_", id));
			return null;
		}
		public virtual XVar addJSFiles()
		{
			return null;
		}
		public virtual XVar addCSSFiles()
		{
			return null;
		}
		public virtual XVar getSetting(dynamic _param_key)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			#endregion

			return this.pageObject.pSetEdit.getFieldData((XVar)(this.field), (XVar)(key));
		}
		public virtual XVar addJSSetting(dynamic _param_key, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic key = XVar.Clone(_param_key);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			this.pageObject.jsSettings.InitAndSetArrayItem(value, "tableSettings", this.pageObject.tName, "fieldSettings", this.field, this.container.pageType, key);
			return null;
		}
		public virtual XVar buildControl(dynamic _param_value, dynamic _param_mode, dynamic _param_fieldNum, dynamic _param_validate, dynamic _param_additionalCtrlParams, dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic fieldNum = XVar.Clone(_param_fieldNum);
			dynamic validate = XVar.Clone(_param_validate);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic additionalClass = null, arrKeys = XVar.Array(), isHidden = null, j = null;
			this.searchPanelControl = XVar.Clone(isSearchPanelControl((XVar)(mode), (XVar)(additionalCtrlParams)));
			this.inputStyle = XVar.Clone(getInputStyle((XVar)(mode)));
			if(XVar.Pack(fieldNum))
			{
				this.cfield = XVar.Clone(MVCFunctions.Concat("value", fieldNum, "_", this.goodFieldName, "_", this.id));
				this.ctype = XVar.Clone(MVCFunctions.Concat("type", fieldNum, "_", this.goodFieldName, "_", this.id));
			}
			this.iquery = XVar.Clone(MVCFunctions.Concat("field=", MVCFunctions.RawUrlEncode((XVar)(this.field))));
			arrKeys = XVar.Clone(this.pageObject.pSetEdit.getTableKeys());
			j = new XVar(0);
			for(;j < MVCFunctions.count(arrKeys); j++)
			{
				this.keylink = MVCFunctions.Concat(this.keylink, "&key", j + 1, "=", MVCFunctions.RawUrlEncode((XVar)(data[arrKeys[j]])));
			}
			this.iquery = MVCFunctions.Concat(this.iquery, this.keylink);
			isHidden = XVar.Clone((XVar)(additionalCtrlParams.KeyExists("hidden"))  && (XVar)(additionalCtrlParams["hidden"]));
			additionalClass = new XVar("");
			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				additionalClass = MVCFunctions.Concat(additionalClass, "bs-ctrlspan rnr-nowrap ");
				if(this.format == Constants.EDIT_FORMAT_READONLY)
				{
					additionalClass = MVCFunctions.Concat(additionalClass, "form-control-static ");
				}
				if((XVar)(MVCFunctions.count(validate["basicValidate"]))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.array_search(new XVar("IsRequired"), (XVar)(validate["basicValidate"]))), XVar.Pack(false))))
				{
					additionalClass = MVCFunctions.Concat(additionalClass, "bs-inlinerequired");
				}
			}
			else
			{
				additionalClass = MVCFunctions.Concat(additionalClass, "rnr-nowrap ");
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<span id=\"edit", this.id, "_", this.goodFieldName, "_", fieldNum, "\" class=\"", additionalClass, "\"", (XVar.Pack(isHidden) ? XVar.Pack(" style=\"display:none\"") : XVar.Pack("")), ">"));
			return null;
		}
		public virtual XVar getFirstElementId()
		{
			return false;
		}
		public virtual XVar isSearchPanelControl(dynamic _param_mode, dynamic _param_additionalCtrlParams)
		{
			#region pass-by-value parameters
			dynamic mode = XVar.Clone(_param_mode);
			dynamic additionalCtrlParams = XVar.Clone(_param_additionalCtrlParams);
			#endregion

			return (XVar)((XVar)((XVar)(mode == Constants.MODE_SEARCH)  && (XVar)(additionalCtrlParams.KeyExists("searchPanelControl")))  && (XVar)(additionalCtrlParams["searchPanelControl"]))  && (XVar)(!(XVar)(this.pageObject.mobileTemplateMode()));
		}
		public virtual XVar buildControlEnd(dynamic _param_validate, dynamic _param_mode)
		{
			#region pass-by-value parameters
			dynamic validate = XVar.Clone(_param_validate);
			dynamic mode = XVar.Clone(_param_mode);
			#endregion

			if(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				MVCFunctions.Echo("</span>");
			}
			else
			{
				if((XVar)(MVCFunctions.count(validate["basicValidate"]))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.array_search(new XVar("IsRequired"), (XVar)(validate["basicValidate"]))), XVar.Pack(false))))
				{
					MVCFunctions.Echo("&nbsp;<font color=\"red\">*</font></span>");
				}
				else
				{
					MVCFunctions.Echo("</span>");
				}
			}
			return null;
		}
		public virtual XVar getPostValueAndType()
		{
			this.webValue = XVar.Clone(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("value_", this.goodFieldName, "_", this.id))));
			this.webType = XVar.Clone(MVCFunctions.postvalue((XVar)(MVCFunctions.Concat("type_", this.goodFieldName, "_", this.id))));
			return null;
		}
		public virtual XVar getWebValue()
		{
			return this.webValue;
		}
		public virtual XVar readWebValue(dynamic avalues, dynamic blobfields, dynamic _param_legacy1, dynamic _param_legacy2, dynamic filename_values)
		{
			#region pass-by-value parameters
			dynamic legacy1 = XVar.Clone(_param_legacy1);
			dynamic legacy2 = XVar.Clone(_param_legacy2);
			#endregion

			getPostValueAndType();
			if(XVar.Pack(MVCFunctions.FieldSubmitted((XVar)(MVCFunctions.Concat(this.goodFieldName, "_", this.id)))))
			{
				this.webValue = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(this.field), (XVar)(this.webValue), (XVar)(this.webType)));
			}
			else
			{
				this.webValue = new XVar(false);
			}
			if((XVar)(this.pageObject.pageType == Constants.PAGE_EDIT)  && (XVar)(this.pageObject.pSetEdit.isReadonly((XVar)(this.field))))
			{
				if(XVar.Pack(this.pageObject.pSetEdit.getAutoUpdateValue((XVar)(this.field))))
				{
					this.webValue = XVar.Clone(this.pageObject.pSetEdit.getAutoUpdateValue((XVar)(this.field)));
				}
				else
				{
					if(this.pageObject.pSetEdit.getOwnerTable((XVar)(this.field)) != this.pageObject.pSetEdit.getStrOriginalTableName())
					{
						this.webValue = new XVar(false);
					}
				}
			}
			if(XVar.Pack(!(XVar)(XVar.Equals(XVar.Pack(this.webValue), XVar.Pack(false)))))
			{
				if(this.connection.dbType == Constants.nDATABASE_Informix)
				{
					if(XVar.Pack(CommonFunctions.IsTextType((XVar)(this.pageObject.pSetEdit.getFieldType((XVar)(this.field))))))
					{
						blobfields.InitAndSetArrayItem(this.field, null);
					}
				}
				avalues.InitAndSetArrayItem(this.webValue, this.field);
			}
			return null;
		}
		public virtual XVar baseSQLWhere(dynamic _param_strSearchOption)
		{
			#region pass-by-value parameters
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			#endregion

			dynamic fullFieldName = null;
			if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(this.var_type))))
			{
				return false;
			}
			if(this.connection.dbType != Constants.nDATABASE_MySQL)
			{
				this.btexttype = XVar.Clone(CommonFunctions.IsTextType((XVar)(this.var_type)));
			}
			if(this.connection.dbType == Constants.nDATABASE_MSSQLServer)
			{
				if((XVar)((XVar)(this.btexttype)  && (XVar)(strSearchOption != "Contains"))  && (XVar)(strSearchOption != "Starts with"))
				{
					return false;
				}
			}
			if(strSearchOption != "Empty")
			{
				return "";
			}
			fullFieldName = XVar.Clone(getFieldSQLDecrypt());
			if((XVar)((XVar)(CommonFunctions.IsCharType((XVar)(this.var_type)))  && (XVar)((XVar)(!(XVar)(this.ismssql))  || (XVar)(!(XVar)(this.btexttype))))  && (XVar)(!(XVar)(this.isOracle)))
			{
				return MVCFunctions.Concat("(", fullFieldName, " is null or ", fullFieldName, "='')");
			}
			if((XVar)(this.ismssql)  && (XVar)(this.btexttype))
			{
				return MVCFunctions.Concat("(", fullFieldName, " is null or ", fullFieldName, " LIKE '')");
			}
			return MVCFunctions.Concat(fullFieldName, " is null");
		}
		public virtual XVar SQLWhere(dynamic _param_SearchFor, dynamic _param_strSearchOption, dynamic _param_SearchFor2, dynamic _param_etype, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic SearchFor = XVar.Clone(_param_SearchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic SearchFor2 = XVar.Clone(_param_SearchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			dynamic baseResult = null, cleanvalue2 = null, gstrField = null, searchIsCaseInsensitive = null, value1 = null, value2 = null;
			baseResult = XVar.Clone(baseSQLWhere((XVar)(strSearchOption)));
			if(XVar.Equals(XVar.Pack(baseResult), XVar.Pack(false)))
			{
				return "";
			}
			if(baseResult != XVar.Pack(""))
			{
				return baseResult;
			}
			if((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(SearchFor))))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(SearchFor2)))))
			{
				return "";
			}
			value1 = XVar.Clone(this.pageObject.cipherer.MakeDBValue((XVar)(this.field), (XVar)(SearchFor), (XVar)(etype), new XVar(true)));
			value2 = new XVar(false);
			cleanvalue2 = new XVar(false);
			if(strSearchOption == "Between")
			{
				cleanvalue2 = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(this.field), (XVar)(SearchFor2), (XVar)(etype)));
				value2 = XVar.Clone(CommonFunctions.make_db_value((XVar)(this.field), (XVar)(SearchFor2), (XVar)(etype)));
			}
			if((XVar)((XVar)((XVar)(strSearchOption != "Contains")  && (XVar)(strSearchOption != "Starts with"))  && (XVar)((XVar)(XVar.Equals(XVar.Pack(value1), XVar.Pack("null")))  && (XVar)(XVar.Equals(XVar.Pack(value2), XVar.Pack("null")))))  && (XVar)(!(XVar)(this.pageObject.cipherer.isFieldPHPEncrypted((XVar)(this.field)))))
			{
				return "";
			}
			if((XVar)((XVar)(strSearchOption == "Contains")  || (XVar)(strSearchOption == "Starts with"))  && (XVar)(!(XVar)(isStringValidForLike((XVar)(SearchFor)))))
			{
				return "";
			}
			searchIsCaseInsensitive = XVar.Clone(this.pageObject.pSetEdit.getNCSearch());
			if((XVar)(CommonFunctions.IsCharType((XVar)(this.var_type)))  && (XVar)(!(XVar)(this.btexttype)))
			{
				gstrField = XVar.Clone(getFieldSQLDecrypt());
				if((XVar)(!(XVar)(this.pageObject.cipherer.isFieldPHPEncrypted((XVar)(this.field))))  && (XVar)(searchIsCaseInsensitive))
				{
					if(XVar.Pack(MVCFunctions.strlen((XVar)(SearchFor))))
					{
						value1 = XVar.Clone(this.connection.upper((XVar)(value1)));
					}
					if(XVar.Pack(MVCFunctions.strlen((XVar)(SearchFor2))))
					{
						value2 = XVar.Clone(this.connection.upper((XVar)(value2)));
					}
					gstrField = XVar.Clone(this.connection.upper((XVar)(gstrField)));
				}
			}
			else
			{
				if((XVar)(strSearchOption == "Contains")  || (XVar)(strSearchOption == "Starts with"))
				{
					gstrField = XVar.Clone(this.connection.field2char((XVar)(getFieldSQLDecrypt()), (XVar)(this.var_type)));
				}
				else
				{
					if(this.pageObject.pSetEdit.getViewFormat((XVar)(this.field)) == Constants.FORMAT_TIME)
					{
						gstrField = XVar.Clone(this.connection.field2time((XVar)(getFieldSQLDecrypt()), (XVar)(this.var_type)));
					}
					else
					{
						gstrField = XVar.Clone(getFieldSQLDecrypt());
					}
				}
			}
			if(strSearchOption == "Contains")
			{
				if(XVar.Pack(this.pageObject.cipherer.isFieldPHPEncrypted((XVar)(this.field))))
				{
					return MVCFunctions.Concat(gstrField, "=", this.pageObject.cipherer.MakeDBValue((XVar)(this.field), (XVar)(SearchFor)));
				}
				SearchFor = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(SearchFor)));
				if((XVar)((XVar)(CommonFunctions.IsCharType((XVar)(this.var_type)))  && (XVar)(!(XVar)(this.btexttype)))  && (XVar)(searchIsCaseInsensitive))
				{
					return MVCFunctions.Concat(gstrField, " ", this.var_like, " ", this.connection.upper((XVar)(this.connection.prepareString((XVar)(MVCFunctions.Concat("%", SearchFor, "%"))))));
				}
				return MVCFunctions.Concat(gstrField, " ", this.var_like, " ", this.connection.prepareString((XVar)(MVCFunctions.Concat("%", SearchFor, "%"))));
			}
			if(strSearchOption == "Equals")
			{
				return MVCFunctions.Concat(gstrField, "=", value1);
			}
			if(strSearchOption == "Starts with")
			{
				SearchFor = XVar.Clone(this.connection.escapeLIKEpattern((XVar)(SearchFor)));
				if((XVar)((XVar)(CommonFunctions.IsCharType((XVar)(this.var_type)))  && (XVar)(!(XVar)(this.btexttype)))  && (XVar)(searchIsCaseInsensitive))
				{
					return MVCFunctions.Concat(gstrField, " ", this.var_like, " ", this.connection.upper((XVar)(this.connection.prepareString((XVar)(MVCFunctions.Concat(SearchFor, "%"))))));
				}
				return MVCFunctions.Concat(gstrField, " ", this.var_like, " ", this.connection.prepareString((XVar)(MVCFunctions.Concat(SearchFor, "%"))));
			}
			if(strSearchOption == "More than")
			{
				return MVCFunctions.Concat(gstrField, ">", value1);
			}
			if(strSearchOption == "Less than")
			{
				return MVCFunctions.Concat(gstrField, "<", value1);
			}
			if(strSearchOption == "Equal or more than")
			{
				return MVCFunctions.Concat(gstrField, ">=", value1);
			}
			if(strSearchOption == "Equal or less than")
			{
				return MVCFunctions.Concat(gstrField, "<=", value1);
			}
			if(strSearchOption == "Between")
			{
				dynamic betweenRange = XVar.Array();
				betweenRange = XVar.Clone(XVar.Array());
				if((XVar)(!XVar.Equals(XVar.Pack(value1), XVar.Pack("null")))  && (XVar)(MVCFunctions.strlen((XVar)(SearchFor))))
				{
					betweenRange.InitAndSetArrayItem(MVCFunctions.Concat(gstrField, ">=", value1), "from");
				}
				if((XVar)(!XVar.Equals(XVar.Pack(value2), XVar.Pack("null")))  && (XVar)(MVCFunctions.strlen((XVar)(SearchFor2))))
				{
					if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(this.var_type))))
					{
						dynamic timeArr = XVar.Array();
						timeArr = XVar.Clone(CommonFunctions.db2time((XVar)(cleanvalue2)));
						if((XVar)((XVar)(timeArr[3] == 0)  && (XVar)(timeArr[4] == 0))  && (XVar)(timeArr[5] == 0))
						{
							timeArr = XVar.Clone(CommonFunctions.adddays((XVar)(timeArr), new XVar(1)));
							value2 = XVar.Clone(MVCFunctions.mysprintf(new XVar("%02d-%02d-%02d"), (XVar)(timeArr)));
							value2 = XVar.Clone(CommonFunctions.add_db_quotes((XVar)(this.field), (XVar)(value2), (XVar)(this.pageObject.tName)));
							betweenRange.InitAndSetArrayItem(MVCFunctions.Concat(gstrField, "<", value2), "to");
						}
						else
						{
							betweenRange.InitAndSetArrayItem(MVCFunctions.Concat(gstrField, "<=", value2), "to");
						}
					}
					else
					{
						betweenRange.InitAndSetArrayItem(MVCFunctions.Concat(gstrField, "<=", value2), "to");
					}
				}
				return MVCFunctions.implode(new XVar(" and "), (XVar)(betweenRange));
			}
			return "";
		}
		public virtual XVar getSearchWhere(dynamic _param_searchFor, dynamic _param_strSearchOption, dynamic _param_searchFor2, dynamic _param_etype)
		{
			#region pass-by-value parameters
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			dynamic searchFor2 = XVar.Clone(_param_searchFor2);
			dynamic etype = XVar.Clone(_param_etype);
			#endregion

			return SQLWhere((XVar)(searchFor), (XVar)(strSearchOption), (XVar)(searchFor2), (XVar)(etype), new XVar(false));
		}
		public virtual XVar getSuggestWhere(dynamic _param_searchOpt, dynamic _param_searchFor, dynamic _param_isAggregateField = null)
		{
			#region default values
			if(_param_isAggregateField as Object == null) _param_isAggregateField = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic isAggregateField = XVar.Clone(_param_isAggregateField);
			#endregion

			if(XVar.Pack(isAggregateField))
			{
				return "";
			}
			return SQLWhere((XVar)(searchFor), (XVar)(searchOpt), new XVar(""), new XVar(""), new XVar(true));
		}
		public virtual XVar getSuggestHaving(dynamic _param_searchOpt, dynamic _param_searchFor, dynamic _param_isAggregateField = null)
		{
			#region default values
			if(_param_isAggregateField as Object == null) _param_isAggregateField = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic isAggregateField = XVar.Clone(_param_isAggregateField);
			#endregion

			if(XVar.Pack(isAggregateField))
			{
				return SQLWhere((XVar)(searchFor), (XVar)(searchOpt), new XVar(""), new XVar(""), new XVar(true));
			}
			return "";
		}
		public virtual XVar getSelectColumnsAndJoinFromPart(dynamic _param_searchFor, dynamic _param_searchOpt, dynamic _param_isSuggest)
		{
			#region pass-by-value parameters
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic searchOpt = XVar.Clone(_param_searchOpt);
			dynamic isSuggest = XVar.Clone(_param_isSuggest);
			#endregion

			return new XVar("selectColumns", getFieldSQLDecrypt(), "joinFromPart", "");
		}
		public virtual XVar checkIfDisplayFieldSearch(dynamic _param_strSearchOption)
		{
			#region pass-by-value parameters
			dynamic strSearchOption = XVar.Clone(_param_strSearchOption);
			#endregion

			return false;
		}
		public virtual XVar buildSearchOptions(dynamic _param_optionsArray, dynamic _param_selOpt, dynamic _param_not, dynamic _param_both)
		{
			#region pass-by-value parameters
			dynamic optionsArray = XVar.Clone(_param_optionsArray);
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			dynamic currentOption = null, defaultOption = null, result = null, userSearchOptions = XVar.Array();
			userSearchOptions = XVar.Clone(this.pageObject.pSetEdit.getSearchOptionsList((XVar)(this.field)));
			currentOption = XVar.Clone((XVar.Pack(var_not) ? XVar.Pack(MVCFunctions.Concat("NOT ", selOpt)) : XVar.Pack(selOpt)));
			if((XVar)(MVCFunctions.count(userSearchOptions))  && (XVar)(this.searchOptions.KeyExists(currentOption)))
			{
				userSearchOptions.InitAndSetArrayItem(currentOption, null);
			}
			if(XVar.Pack(MVCFunctions.count(userSearchOptions)))
			{
				optionsArray = XVar.Clone(MVCFunctions.array_intersect((XVar)(optionsArray), (XVar)(userSearchOptions)));
			}
			defaultOption = XVar.Clone(this.pageObject.pSetEdit.getDefaultSearchOption((XVar)(this.field)));
			if(XVar.Pack(!(XVar)(defaultOption)))
			{
				defaultOption = XVar.Clone(optionsArray[0]);
			}
			result = new XVar("");
			foreach (KeyValuePair<XVar, dynamic> var_option in optionsArray.GetEnumerator())
			{
				dynamic dataAttr = null, selected = null;
				if((XVar)(!(XVar)(this.searchOptions.KeyExists(var_option.Value)))  || (XVar)((XVar)(!(XVar)(both))  && (XVar)(MVCFunctions.substr((XVar)(var_option.Value), new XVar(0), new XVar(4)) == "NOT ")))
				{
					continue;
				}
				selected = XVar.Clone((XVar.Pack(currentOption == var_option.Value) ? XVar.Pack("selected") : XVar.Pack("")));
				dataAttr = XVar.Clone((XVar.Pack(defaultOption == var_option.Value) ? XVar.Pack(" data-default-option=\"true\"") : XVar.Pack("")));
				result = MVCFunctions.Concat(result, "<option value=\"", var_option.Value, "\" ", selected, dataAttr, ">", this.searchOptions[var_option.Value], "</option>");
			}
			return result;
		}
		public virtual XVar getSearchOptions(dynamic _param_selOpt, dynamic _param_not, dynamic _param_both)
		{
			#region pass-by-value parameters
			dynamic selOpt = XVar.Clone(_param_selOpt);
			dynamic var_not = XVar.Clone(_param_not);
			dynamic both = XVar.Clone(_param_both);
			#endregion

			return buildSearchOptions((XVar)(new XVar(0, Constants.EQUALS, 1, Constants.NOT_EQUALS)), (XVar)(selOpt), (XVar)(var_not), (XVar)(both));
		}
		public virtual XVar suggestValue(dynamic _param_value, dynamic _param_searchFor, dynamic var_response, dynamic row)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			#endregion

			dynamic SuggestStringSize = null, realValue = null, viewFormat = null;
			SuggestStringSize = XVar.Clone(CommonFunctions.GetGlobalData(new XVar("suggestStringSize"), new XVar(40)));
			if(SuggestStringSize <= MVCFunctions.runner_strlen((XVar)(searchFor)))
			{
				var_response.InitAndSetArrayItem(searchFor, MVCFunctions.Concat("_", searchFor));
				return null;
			}
			viewFormat = XVar.Clone(this.pageObject.pSetEdit.getViewFormat((XVar)(this.field)));
			if(viewFormat == Constants.FORMAT_NUMBER)
			{
				dynamic dotPosition = null;
				dotPosition = XVar.Clone(MVCFunctions.strpos((XVar)(value), new XVar(".")));
				if(!XVar.Equals(XVar.Pack(dotPosition), XVar.Pack(false)))
				{
					dynamic i = null;
					i = XVar.Clone(MVCFunctions.strlen((XVar)(value)) - 1);
					for(;dotPosition < i; i--)
					{
						if(MVCFunctions.substr((XVar)(value), (XVar)(i), new XVar(1)) != "0")
						{
							if(i < MVCFunctions.strlen((XVar)(value)) - 1)
							{
								value = XVar.Clone(MVCFunctions.substr((XVar)(value), new XVar(0), (XVar)(i + 1)));
							}
							break;
						}
						if((XVar)(i == dotPosition + 1)  && (XVar)(XVar.Pack(0) < dotPosition))
						{
							value = XVar.Clone(MVCFunctions.substr((XVar)(value), new XVar(0), (XVar)(dotPosition)));
							break;
						}
					}
				}
			}
			realValue = XVar.Clone(value);
			if(viewFormat == Constants.FORMAT_HTML)
			{
				dynamic get_text = null, html_tags = null, match = XVar.Array();
				html_tags = XVar.Clone(MVCFunctions.Concat("/<.*?>/i", (XVar.Pack(GlobalVars.useUTF8) ? XVar.Pack("u") : XVar.Pack(""))));
				get_text = XVar.Clone(MVCFunctions.Concat("/(.*<.*>|^.*?)([.]*", MVCFunctions.preg_quote((XVar)(searchFor), new XVar("/")), ".*?)(<.*>|$)/i", (XVar.Pack(GlobalVars.useUTF8) ? XVar.Pack("u") : XVar.Pack(""))));
				value = XVar.Clone(MVCFunctions.preg_replace((XVar)(html_tags), new XVar(""), (XVar)(MVCFunctions.runner_html_entity_decode((XVar)(value)))));
				if(XVar.Equals(XVar.Pack(MVCFunctions.stristr((XVar)(value), (XVar)(searchFor))), XVar.Pack(false)))
				{
					return null;
				}
				if(XVar.Pack(MVCFunctions.preg_match((XVar)(get_text), (XVar)(realValue), (XVar)(match))))
				{
					realValue = XVar.Clone(match[2]);
				}
				else
				{
					realValue = XVar.Clone(value);
				}
			}
			if(SuggestStringSize < MVCFunctions.runner_strlen((XVar)(value)))
			{
				dynamic startPos = null, valueLength = null;
				startPos = new XVar(0);
				valueLength = new XVar(0);
				value = XVar.Clone(cutStr((XVar)(value), (XVar)(searchFor), (XVar)(SuggestStringSize), ref startPos, ref valueLength));
				if(!XVar.Equals(XVar.Pack(viewFormat), XVar.Pack(Constants.FORMAT_HTML)))
				{
					realValue = XVar.Clone(value);
				}
				if(XVar.Pack(0) < startPos)
				{
					value = XVar.Clone(MVCFunctions.Concat("...", value));
				}
				if(startPos + SuggestStringSize != valueLength)
				{
					value = MVCFunctions.Concat(value, "...");
				}
			}
			var_response.InitAndSetArrayItem(realValue, value);
			return null;
		}
		public virtual XVar cutStr(dynamic _param_value, dynamic _param_searchFor, dynamic _param_SuggestStringSize, ref dynamic startPos, ref dynamic valueLength)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			dynamic searchFor = XVar.Clone(_param_searchFor);
			dynamic SuggestStringSize = XVar.Clone(_param_SuggestStringSize);
			#endregion

			dynamic diffLength = null, found = null, i = null, leftContextLength = null, rigthContextLength = null, searchStartPos = null;
			diffLength = XVar.Clone(SuggestStringSize - MVCFunctions.runner_strlen((XVar)(searchFor)));
			leftContextLength = XVar.Clone((XVar)Math.Floor((double)(diffLength / 2)));
			rigthContextLength = XVar.Clone(diffLength - leftContextLength);
			if(XVar.Pack(this.pageObject.pSetEdit.getNCSearch()))
			{
				startPos = XVar.Clone(MVCFunctions.stripos((XVar)(value), (XVar)(searchFor)));
				startPos = XVar.Clone(MVCFunctions.runner_strlen((XVar)(MVCFunctions.substr((XVar)(value), new XVar(0), (XVar)(startPos)))));
			}
			else
			{
				startPos = XVar.Clone(MVCFunctions.runner_strpos((XVar)(value), (XVar)(searchFor)));
			}
			searchStartPos = XVar.Clone(startPos);
			valueLength = XVar.Clone(MVCFunctions.runner_strlen((XVar)(value)));
			if(startPos < leftContextLength)
			{
				rigthContextLength -= startPos - leftContextLength;
				startPos = new XVar(0);
			}
			else
			{
				startPos -= leftContextLength;
			}
			if(XVar.Pack(0) < startPos)
			{
				found = new XVar(false);
				i = XVar.Clone(startPos - 1);
				for(;(XVar)(startPos - 5 <= i)  && (XVar)(XVar.Pack(0) <= i); i--)
				{
					if((XVar)(i == XVar.Pack(0))  || (XVar)(isStopSymbol((XVar)(MVCFunctions.runner_substr((XVar)(value), (XVar)(i), new XVar(1))))))
					{
						if(i == XVar.Pack(0))
						{
							startPos = new XVar(0);
						}
						else
						{
							startPos = XVar.Clone(i + 1);
						}
						found = new XVar(true);
						break;
					}
				}
				if(XVar.Pack(!(XVar)(found)))
				{
					i = XVar.Clone(startPos);
					for(;(XVar)(i <= startPos + 5)  && (XVar)(i < searchStartPos); i++)
					{
						if(XVar.Pack(isStopSymbol((XVar)(MVCFunctions.runner_substr((XVar)(value), (XVar)(i), new XVar(1))))))
						{
							startPos = XVar.Clone(i + 1);
							break;
						}
					}
				}
			}
			if(valueLength < startPos + SuggestStringSize)
			{
				SuggestStringSize = XVar.Clone(valueLength - startPos);
			}
			if(startPos + SuggestStringSize < valueLength)
			{
				dynamic tempStartPos = null;
				found = new XVar(false);
				tempStartPos = XVar.Clone(startPos + SuggestStringSize);
				i = XVar.Clone(tempStartPos + 1);
				for(;(XVar)(i <= tempStartPos + 5)  && (XVar)(i < valueLength); i++)
				{
					if((XVar)(i == valueLength - 1)  || (XVar)(isStopSymbol((XVar)(MVCFunctions.runner_substr((XVar)(value), (XVar)(i), new XVar(1))))))
					{
						if(i == valueLength - 1)
						{
							SuggestStringSize = XVar.Clone((i - startPos) + 1);
						}
						else
						{
							SuggestStringSize = XVar.Clone(i - startPos);
						}
						found = new XVar(true);
						break;
					}
				}
				if(XVar.Pack(!(XVar)(found)))
				{
					i = XVar.Clone(tempStartPos);
					for(;tempStartPos - 5 <= i; i--)
					{
						if(XVar.Pack(isStopSymbol((XVar)(MVCFunctions.runner_substr((XVar)(value), (XVar)(i), new XVar(1))))))
						{
							SuggestStringSize = XVar.Clone(i - startPos);
							break;
						}
					}
				}
			}
			return MVCFunctions.runner_substr((XVar)(value), (XVar)(startPos), (XVar)(SuggestStringSize));
		}
		public virtual XVar isStopSymbol(dynamic _param_smb)
		{
			#region pass-by-value parameters
			dynamic smb = XVar.Clone(_param_smb);
			#endregion

			return !XVar.Equals(XVar.Pack(MVCFunctions.strpos(new XVar(" .,;:\"'?!|\\/=(){}[]*-+\n\r"), (XVar)(smb))), XVar.Pack(false));
			return null;
		}
		public virtual XVar afterSuccessfulSave()
		{
			return null;
		}
		public virtual XVar init()
		{
			return null;
		}
		public virtual XVar isStringValidForLike(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			if((XVar)(!(XVar)(CommonFunctions.IsCharType((XVar)(this.var_type))))  && (XVar)(MVCFunctions.hasNonAsciiSymbols((XVar)(str))))
			{
				return false;
			}
			return true;
		}
		public virtual XVar getInputStyle(dynamic _param_mode)
		{
			#region pass-by-value parameters
			dynamic mode = XVar.Clone(_param_mode);
			#endregion

			dynamic style = null, width = null;
			if((XVar)((XVar)(this.pageObject.getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)((XVar)(this.pageObject.pageType != Constants.PAGE_ADD)  || (XVar)(this.pageObject.mode != Constants.ADD_INLINE)))  && (XVar)((XVar)(this.pageObject.pageType != Constants.PAGE_EDIT)  || (XVar)(this.pageObject.mode != Constants.EDIT_INLINE)))
			{
				return "";
			}
			width = XVar.Clone((XVar.Pack(this.searchPanelControl) ? XVar.Pack(150) : XVar.Pack(this.pageObject.pSetEdit.getControlWidth((XVar)(this.field)))));
			style = XVar.Clone(makeWidthStyle((XVar)(width)));
			return MVCFunctions.Concat("style=\"", style, "\"");
		}
		public virtual XVar makeWidthStyle(dynamic _param_widthPx)
		{
			#region pass-by-value parameters
			dynamic widthPx = XVar.Clone(_param_widthPx);
			#endregion

			if(XVar.Pack(0) == widthPx)
			{
				return "";
			}
			return MVCFunctions.Concat("width: ", widthPx, "px;");
		}
		public virtual XVar loadLookupContent(dynamic _param_parentValuesData, dynamic _param_childVal = null, dynamic _param_doCategoryFilter = null, dynamic _param_initialLoad = null)
		{
			#region default values
			if(_param_childVal as Object == null) _param_childVal = new XVar("");
			if(_param_doCategoryFilter as Object == null) _param_doCategoryFilter = new XVar(true);
			if(_param_initialLoad as Object == null) _param_initialLoad = new XVar(true);
			#endregion

			#region pass-by-value parameters
			dynamic parentValuesData = XVar.Clone(_param_parentValuesData);
			dynamic childVal = XVar.Clone(_param_childVal);
			dynamic doCategoryFilter = XVar.Clone(_param_doCategoryFilter);
			dynamic initialLoad = XVar.Clone(_param_initialLoad);
			#endregion

			return "";
		}
		public virtual XVar getLookupContentToReload(dynamic _param_isExistParent, dynamic _param_mode, dynamic _param_parentCtrlsData)
		{
			#region pass-by-value parameters
			dynamic isExistParent = XVar.Clone(_param_isExistParent);
			dynamic mode = XVar.Clone(_param_mode);
			dynamic parentCtrlsData = XVar.Clone(_param_parentCtrlsData);
			#endregion

			return "";
		}
		public virtual XVar getFieldValueCopy(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return value;
		}
		public virtual XVar getFieldSQLDecrypt()
		{
			return RunnerPage._getFieldSQLDecrypt((XVar)(this.field), (XVar)(this.connection), (XVar)(this.pageObject.pSetEdit), (XVar)(this.pageObject.cipherer));
		}
		protected virtual XVar getPlaceholderAttr()
		{
			if((XVar)(!(XVar)(this.searchPanelControl))  && (XVar)(this.container.pageType != Constants.PAGE_SEARCH))
			{
				return MVCFunctions.Concat(" placeholder=\"", CommonFunctions.GetFieldPlaceHolder((XVar)(MVCFunctions.GoodFieldName((XVar)(this.pageObject.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.field)))), "\"");
			}
			return "";
		}
		public virtual XVar getConnection()
		{
			return this.Invoke("connection");
		}
	}
}
