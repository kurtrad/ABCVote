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
	public partial class SearchClause : SearchClauseBase
	{
		public dynamic _where = XVar.Array();
		public dynamic tName = XVar.Pack("");
		protected dynamic searchFieldsArr = XVar.Array();
		protected dynamic googleLikeFields = XVar.Array();
		protected dynamic srchType = XVar.Pack("integrated");
		protected dynamic sessionPrefix = XVar.Pack("");
		public dynamic bIsUsedSrch = XVar.Pack(false);
		public dynamic filtersActivated = XVar.Pack(false);
		public dynamic simpleSearchActive = XVar.Pack(false);
		protected dynamic advancedSearchActive = XVar.Pack(false);
		protected dynamic haveAggregateFields = XVar.Pack(false);
		protected dynamic panelSearchFields = XVar.Array();
		public dynamic cipherer = XVar.Pack(null);
		public dynamic pSetSearch;
		protected dynamic searchOptions = XVar.Array();
		protected dynamic fieldDelimiterLeft = XVar.Pack(")");
		protected dynamic fieldDelimiterRight = XVar.Pack("(");
		protected dynamic valueDelimiter = XVar.Pack("~");
		protected dynamic fieldsUsedForSearch = XVar.Array();
		protected dynamic localEditControls = XVar.Pack(null);
		protected dynamic isShowSimpleSrchOpt = XVar.Pack(false);
		protected dynamic searchParams = XVar.Array();
		public dynamic savedSearchIsRun = XVar.Pack(false);
		public dynamic filteredFields = XVar.Array();
		protected dynamic searchSavingEnabled = XVar.Pack(false);
		protected dynamic dashTName = XVar.Pack("");
		protected dynamic wholeDashboardSearch = XVar.Pack(false);
		protected dynamic dashboardSearchClause = XVar.Pack(null);
		protected dynamic customFieldSQLConditions;
		protected static bool skipSearchClauseCtor = false;
		public SearchClause(dynamic var_params)
		{
			if(skipSearchClauseCtor)
			{
				skipSearchClauseCtor = false;
				return;
			}
			setSearchOptions();
			this.tName = XVar.Clone(var_params["tName"]);
			this.sessionPrefix = XVar.Clone((XVar.Pack(var_params["sessionPrefix"]) ? XVar.Pack(var_params["sessionPrefix"]) : XVar.Pack(this.tName)));
			this.cipherer = XVar.Clone(var_params["cipherer"]);
			this.pSetSearch = XVar.Clone(new ProjectSettings((XVar)(this.tName), new XVar(Constants.PAGE_SEARCH)));
			if(XVar.Pack(!(XVar)(this.cipherer)))
			{
				this.cipherer = XVar.Clone(new RunnerCipherer((XVar)(this.tName), (XVar)(this.pSetSearch)));
			}
			this.searchFieldsArr = XVar.Clone((XVar.Pack(var_params["searchFieldsArr"]) ? XVar.Pack(var_params["searchFieldsArr"]) : XVar.Pack(this.pSetSearch.getAllSearchFields())));
			this.panelSearchFields = XVar.Clone((XVar.Pack(var_params["panelSearchFields"]) ? XVar.Pack(var_params["panelSearchFields"]) : XVar.Pack(this.pSetSearch.getPanelSearchFields())));
			this.googleLikeFields = XVar.Clone((XVar.Pack(var_params["googleLikeFields"]) ? XVar.Pack(var_params["googleLikeFields"]) : XVar.Pack(this.pSetSearch.getGoogleLikeFields())));
			this.isShowSimpleSrchOpt = XVar.Clone(this.pSetSearch.showSimpleSearchOptions());
			this.searchSavingEnabled = XVar.Clone((XVar.Pack(var_params["searchSavingEnabled"]) ? XVar.Pack(var_params["searchSavingEnabled"]) : XVar.Pack(false)));
			this.dashTName = XVar.Clone((XVar.Pack(var_params["dashTName"]) ? XVar.Pack(var_params["dashTName"]) : XVar.Pack("")));
			if(XVar.Pack(var_params["haveAggregateFields"]))
			{
				this.haveAggregateFields = new XVar(true);
			}
			this.customFieldSQLConditions = XVar.Clone(XVar.Array());
		}
		protected virtual XVar setSearchOptions()
		{
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Contains", "not", false), "contains");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Equals", "not", false), "equals");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Starts with", "not", false), "startswith");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "More than", "not", false), "morethan");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Less than", "not", false), "lessthan");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Between", "not", false), "between");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Empty", "not", false), "empty");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "More than", "not", true), "lessequal");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Less than", "not", true), "moreequal");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Contains", "not", true), "notcontain");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Equals", "not", true), "notequal");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Starts with", "not", true), "notstartwith");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "More than", "not", true), "notmorethan");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Less than", "not", true), "notlessthan");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Between", "not", true), "notbetween");
			this.searchOptions.InitAndSetArrayItem(new XVar("option", "Empty", "not", true), "notempty");
			return null;
		}
		public virtual XVar buildItegratedWhere(dynamic _param_fieldsArr, dynamic _param_editControls = null)
		{
			#region default values
			if(_param_editControls as Object == null) _param_editControls = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldsArr = XVar.Clone(_param_fieldsArr);
			dynamic editControls = XVar.Clone(_param_editControls);
			#endregion

			dynamic resWhere = null, sWhere = null, srchCriteriaCombineType = null, srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.count(fieldsArr))))
			{
				return "";
			}
			if(XVar.Pack(editControls == null))
			{
				editControls = XVar.Clone(getLocalEditControls());
			}
			resWhere = new XVar("");
			if((XVar)(!(XVar)(this.haveAggregateFields))  || (XVar)(!(XVar)(this.advancedSearchActive)))
			{
				resWhere = XVar.Clone(getSimpleSearchWhere((XVar)(fieldsArr), (XVar)(editControls)));
			}
			srchCriteriaCombineType = XVar.Clone(getCriteriaCombineType());
			srchFields = this._where["_srchFields"];
			sWhere = new XVar("");
			if(XVar.Pack(MVCFunctions.count(srchFields)))
			{
				dynamic prevSrchFieldName = null;
				sWhere = XVar.Clone((XVar.Pack(srchCriteriaCombineType == "and") ? XVar.Pack("(1=1") : XVar.Pack("(1=0")));
				prevSrchFieldName = new XVar("");
				foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
				{
					if((XVar)(MVCFunctions.in_array((XVar)(srchF.Value["fName"]), (XVar)(fieldsArr)))  && (XVar)(!(XVar)(this.customFieldSQLConditions.KeyExists(srchF.Value["fName"]))))
					{
						dynamic control = null, where = null;
						control = XVar.Clone(editControls.getControl((XVar)(srchF.Value["fName"]), (XVar)(Constants.SEARCHID_PANEL + srchF.Key)));
						where = XVar.Clone(control.getSearchWhere((XVar)(srchF.Value["value1"]), (XVar)(srchF.Value["opt"]), (XVar)(srchF.Value["value2"]), (XVar)(srchF.Value["eType"])));
						if(XVar.Pack(where))
						{
							if(XVar.Pack(srchF.Value["not"]))
							{
								where = XVar.Clone(MVCFunctions.Concat("not (", where, ")"));
							}
							if(srchCriteriaCombineType == "and")
							{
								sWhere = MVCFunctions.Concat(sWhere, (XVar.Pack(prevSrchFieldName != srchF.Value["fName"]) ? XVar.Pack(") and (") : XVar.Pack(" or ")), where);
							}
							else
							{
								sWhere = MVCFunctions.Concat(sWhere, " or ", where);
							}
						}
						prevSrchFieldName = XVar.Clone(srchF.Value["fName"]);
					}
				}
				sWhere = MVCFunctions.Concat(sWhere, ")");
			}
			resWhere = XVar.Clone(CommonFunctions.whereAdd((XVar)(resWhere), (XVar)(sWhere)));
			foreach (KeyValuePair<XVar, dynamic> sql in this.customFieldSQLConditions.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(resWhere)))))
				{
					resWhere = XVar.Clone(MVCFunctions.Concat("(", sql.Value, ")"));
				}
				else
				{
					if(srchCriteriaCombineType == "and")
					{
						resWhere = MVCFunctions.Concat(resWhere, " and (", sql.Value, ")");
					}
					else
					{
						resWhere = MVCFunctions.Concat(resWhere, " or (", sql.Value, ")");
					}
				}
			}
			return resWhere;
		}
		protected virtual XVar getSimpleSearchWhere(dynamic _param_fieldsArr, dynamic _param_editControls)
		{
			#region pass-by-value parameters
			dynamic fieldsArr = XVar.Clone(_param_fieldsArr);
			dynamic editControls = XVar.Clone(_param_editControls);
			#endregion

			dynamic resWhereArr = XVar.Array(), simpleSrch = null, simpleSrchArr = XVar.Array(), simpleSrchField = null, simpleSrchOption = null, specLookupFields = XVar.Array(), where = null;
			simpleSrch = XVar.Clone(this._where["_simpleSrch"]);
			if(XVar.Equals(XVar.Pack(MVCFunctions.trim((XVar)(simpleSrch))), XVar.Pack("%")))
			{
				simpleSrch = XVar.Clone(MVCFunctions.Concat("[", simpleSrch, "]"));
			}
			simpleSrchOption = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if((XVar)((XVar)(simpleSrch == null)  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(simpleSrch)))))  && (XVar)(simpleSrchOption != "Empty"))
			{
				return "";
			}
			simpleSrchField = XVar.Clone(this._where["simpleSrchFieldsComboOpt"]);
			if((XVar)((XVar)(simpleSrch != null)  && (XVar)(MVCFunctions.strlen((XVar)(simpleSrchField))))  && (XVar)(!(XVar)(this.customFieldSQLConditions.KeyExists(simpleSrchField))))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(simpleSrchField), (XVar)(fieldsArr)))))
				{
					return "";
				}
				where = XVar.Clone(editControls.getControl((XVar)(simpleSrchField), new XVar(Constants.SEARCHID_SIMPLE)).getSearchWhere((XVar)(simpleSrch), (XVar)(simpleSrchOption), new XVar(""), new XVar("")));
				if((XVar)(where)  && (XVar)(this._where["simpleSrchTypeComboNot"]))
				{
					where = XVar.Clone(MVCFunctions.Concat("not (", where, ")"));
				}
				return where;
			}
			if(XVar.Pack(this.isShowSimpleSrchOpt))
			{
				simpleSrchArr = XVar.Clone(new XVar(0, simpleSrch));
			}
			else
			{
				simpleSrchArr = XVar.Clone(googleLikeParseString((XVar)(simpleSrch)));
			}
			resWhereArr = XVar.Clone(XVar.Array());
			specLookupFields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> simpleSrchItem in simpleSrchArr.GetEnumerator())
			{
				dynamic i = null, sWhere = null;
				sWhere = new XVar("");
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.searchFieldsArr); i++)
				{
					if(XVar.Pack(this.customFieldSQLConditions.KeyExists(this.searchFieldsArr[i])))
					{
						continue;
					}
					if((XVar)(MVCFunctions.in_array((XVar)(this.searchFieldsArr[i]), (XVar)(fieldsArr)))  && (XVar)(MVCFunctions.in_array((XVar)(this.searchFieldsArr[i]), (XVar)(this.googleLikeFields))))
					{
						dynamic control = null;
						if(XVar.Pack(MVCFunctions.in_array((XVar)(this.searchFieldsArr[i]), (XVar)(specLookupFields))))
						{
							control = XVar.Clone(editControls.getControl((XVar)(this.searchFieldsArr[i]), new XVar(Constants.SEARCHID_ALL)));
						}
						else
						{
							control = XVar.Clone(editControls.getControl((XVar)(this.searchFieldsArr[i]), (XVar)(Constants.SEARCHID_ALL + simpleSrchItem.Key)));
						}
						where = XVar.Clone(control.getSearchWhere((XVar)(simpleSrchItem.Value), (XVar)(simpleSrchOption), new XVar(""), new XVar("")));
						if(XVar.Pack(control.checkIfDisplayFieldSearch((XVar)(simpleSrchOption))))
						{
							specLookupFields.InitAndSetArrayItem(this.searchFieldsArr[i], null);
						}
						if((XVar)(MVCFunctions.trim((XVar)(where)) != "")  && (XVar)(this._where["simpleSrchTypeComboNot"]))
						{
							where = XVar.Clone(MVCFunctions.Concat("not (", where, ")"));
						}
						if(MVCFunctions.trim((XVar)(where)) != "")
						{
							if(XVar.Pack(sWhere))
							{
								sWhere = MVCFunctions.Concat(sWhere, " or ");
							}
							sWhere = MVCFunctions.Concat(sWhere, where);
						}
					}
				}
				if(MVCFunctions.count(simpleSrchArr) == 1)
				{
					resWhereArr.InitAndSetArrayItem(sWhere, null);
				}
				else
				{
					if(XVar.Pack(sWhere))
					{
						resWhereArr.InitAndSetArrayItem(MVCFunctions.Concat("(", sWhere, ")"), null);
					}
				}
			}
			return MVCFunctions.implode(new XVar(" and "), (XVar)(resWhereArr));
		}
		protected virtual XVar getLocalEditControls()
		{
			return new EditControlsContainer(new XVar(null), (XVar)(this.pSetSearch), new XVar(Constants.PAGE_SEARCH), (XVar)(this.cipherer));
		}
		public virtual XVar getWhere(dynamic _param_fieldsArr, dynamic _param_editControls = null)
		{
			#region default values
			if(_param_editControls as Object == null) _param_editControls = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic fieldsArr = XVar.Clone(_param_fieldsArr);
			dynamic editControls = XVar.Clone(_param_editControls);
			#endregion

			dynamic sWhere = null;
			switch(((XVar)this.srchType).ToString())
			{
				case "showall":
					sWhere = new XVar("");
					break;
				case "integrated":
					sWhere = XVar.Clone(buildItegratedWhere((XVar)(fieldsArr), (XVar)(editControls)));
					break;
				default:
					sWhere = new XVar("");
					break;
			}
			return sWhere;
		}
		public virtual XVar getCommonJoinFromParts(dynamic _param_editControls)
		{
			#region pass-by-value parameters
			dynamic editControls = XVar.Clone(_param_editControls);
			#endregion

			dynamic joinParts = XVar.Array(), srchFields = XVar.Array();
			joinParts = XVar.Clone(getJoinFromPartsArrayBasingOnSimpleSearch((XVar)(editControls)));
			srchFields = this._where["_srchFields"];
			if(XVar.Pack(!(XVar)(srchFields)))
			{
				srchFields = XVar.Clone(XVar.Array());
			}
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				dynamic clausesData = XVar.Array(), control = null;
				if((XVar)(srchF.Value["opt"] != "Contains")  && (XVar)(srchF.Value["opt"] != "Starts with"))
				{
					continue;
				}
				control = XVar.Clone(editControls.getControl((XVar)(srchF.Value["fName"]), (XVar)(Constants.SEARCHID_PANEL + srchF.Key)));
				clausesData = XVar.Clone(control.getSelectColumnsAndJoinFromPart((XVar)(srchF.Value["value1"]), (XVar)(srchF.Value["opt"]), new XVar(false)));
				joinParts.InitAndSetArrayItem(clausesData["joinFromPart"], null);
			}
			return MVCFunctions.implode(new XVar(" "), (XVar)(joinParts));
		}
		protected virtual XVar getJoinFromPartsArrayBasingOnSimpleSearch(dynamic _param_editControls)
		{
			#region pass-by-value parameters
			dynamic editControls = XVar.Clone(_param_editControls);
			#endregion

			dynamic clausesData = XVar.Array(), control = null, joinParts = XVar.Array(), simleSrchField = null, simpleSrch = null, simpleSrchArr = XVar.Array(), simpleSrchOpt = null, skipFields = XVar.Array();
			joinParts = XVar.Clone(XVar.Array());
			simpleSrch = XVar.Clone(this._where["_simpleSrch"]);
			simpleSrchOpt = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if((XVar)((XVar)((XVar)(this.haveAggregateFields)  && (XVar)(this.advancedSearchActive))  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(simpleSrch)))))  || (XVar)((XVar)(simpleSrchOpt != "Contains")  && (XVar)(simpleSrchOpt != "Starts with")))
			{
				return XVar.Array();
			}
			simleSrchField = XVar.Clone(this._where["simpleSrchFieldsComboOpt"]);
			if(XVar.Pack(MVCFunctions.strlen((XVar)(simleSrchField))))
			{
				control = XVar.Clone(editControls.getControl((XVar)(simleSrchField), new XVar(Constants.SEARCHID_SIMPLE)));
				clausesData = XVar.Clone(control.getSelectColumnsAndJoinFromPart((XVar)(simpleSrch), (XVar)(simpleSrchOpt), new XVar(false)));
				return new XVar(0, clausesData["joinFromPart"]);
			}
			if(XVar.Pack(this.isShowSimpleSrchOpt))
			{
				simpleSrchArr = XVar.Clone(new XVar(0, simpleSrch));
			}
			else
			{
				simpleSrchArr = XVar.Clone(googleLikeParseString((XVar)(simpleSrch)));
			}
			joinParts = XVar.Clone(XVar.Array());
			skipFields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> simpleSrchItem in simpleSrchArr.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> sField in this.searchFieldsArr.GetEnumerator())
				{
					if((XVar)(MVCFunctions.in_array((XVar)(sField.Value), (XVar)(this.googleLikeFields)))  && (XVar)(!(XVar)(MVCFunctions.in_array((XVar)(sField.Value), (XVar)(skipFields)))))
					{
						control = XVar.Clone(editControls.getControl((XVar)(sField.Value), (XVar)(Constants.SEARCHID_ALL + simpleSrchItem.Key)));
						clausesData = XVar.Clone(control.getSelectColumnsAndJoinFromPart((XVar)(simpleSrchItem.Value), (XVar)(simpleSrchOpt), new XVar(false)));
						if(XVar.Pack(control.checkIfDisplayFieldSearch((XVar)(simpleSrchOpt))))
						{
							skipFields.InitAndSetArrayItem(sField.Value, null);
						}
						joinParts.InitAndSetArrayItem(clausesData["joinFromPart"], null);
					}
				}
			}
			return joinParts;
		}
		public virtual XVar processFiltersWhere(dynamic _param_connection)
		{
			#region pass-by-value parameters
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			dynamic filters = XVar.Array(), filtersParams = null, strFieldWhere = XVar.Array();
			this.filteredFields = XVar.Clone(XVar.Array());
			strFieldWhere = XVar.Clone(XVar.Array());
			filtersParams = XVar.Clone(MVCFunctions.postvalue(new XVar("f")));
			if((XVar)(!(XVar)(filtersParams))  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_filters"))))
			{
				filtersParams = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_filters")]);
			}
			if((XVar)(!(XVar)(filtersParams))  || (XVar)(filtersParams == "all"))
			{
				return null;
			}
			filters = XVar.Clone(parseStringToArray((XVar)(filtersParams), new XVar(true)));
			foreach (KeyValuePair<XVar, dynamic> filter in filters.GetEnumerator())
			{
				dynamic fName = null, fValue = null, fValues = XVar.Array(), filterType = null, parentValues = null, parentValuesAdded = null, sValue = null, where = null;
				fName = XVar.Clone(searchUnEscape((XVar)(filter.Value[0])));
				filterType = XVar.Clone(filter.Value[1]);
				fValue = XVar.Clone(searchUnEscape((XVar)(filter.Value[2])));
				fValues = XVar.Clone(getUnescapedFValues((XVar)(fValue)));
				parentValuesAdded = XVar.Clone(1 < MVCFunctions.count(fValues));
				fValue = XVar.Clone((XVar.Pack(parentValuesAdded) ? XVar.Pack(fValues[0]) : XVar.Pack(fValue)));
				parentValues = XVar.Clone((XVar.Pack(parentValuesAdded) ? XVar.Pack(MVCFunctions.array_slice((XVar)(fValues), new XVar(1))) : XVar.Pack(XVar.Array())));
				sValue = XVar.Clone(searchUnEscape((XVar)(filter.Value[3])));
				where = XVar.Clone(getFilterWhereByType((XVar)(filterType), (XVar)(fName), (XVar)(fValue), (XVar)(sValue), (XVar)(parentValues), (XVar)(connection)));
				if(XVar.Pack(where))
				{
					strFieldWhere.InitAndSetArrayItem(where, fName, "where", null);
					strFieldWhere.InitAndSetArrayItem(fValue, fName, "value", null);
					if(XVar.Pack(sValue))
					{
						strFieldWhere.InitAndSetArrayItem(sValue, fName, "value", null);
					}
					strFieldWhere.InitAndSetArrayItem(parentValues, fName, "parentValues", null);
				}
			}
			foreach (KeyValuePair<XVar, dynamic> fData in strFieldWhere.GetEnumerator())
			{
				dynamic fieldWhere = null;
				fieldWhere = XVar.Clone(MVCFunctions.implode(new XVar(" or "), (XVar)(fData.Value["where"])));
				this.filteredFields.InitAndSetArrayItem(new XVar("values", fData.Value["value"], "where", fieldWhere, "parentValues", fData.Value["parentValues"]), fData.Key);
			}
			return null;
		}
		protected virtual XVar getUnescapedFValues(dynamic _param_fValue)
		{
			#region pass-by-value parameters
			dynamic fValue = XVar.Clone(_param_fValue);
			#endregion

			dynamic i = null, start = null, unescapedValues = XVar.Array(), valueLength = null;
			start = new XVar(0);
			unescapedValues = XVar.Clone(XVar.Array());
			valueLength = XVar.Clone(MVCFunctions.strlen((XVar)(fValue)));
			if(XVar.Pack(!(XVar)(valueLength)))
			{
				return unescapedValues;
			}
			i = new XVar(0);
			for(;i < valueLength; i++)
			{
				if(fValue[i] != "|")
				{
					continue;
				}
				if(XVar.Pack(0) < i)
				{
					if(fValue[i - 1] == "\\")
					{
						continue;
					}
				}
				unescapedValues.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar("\\|"), new XVar("|"), (XVar)(MVCFunctions.substr((XVar)(fValue), (XVar)(start), (XVar)(i - start)))), null);
				start = XVar.Clone(i + 1);
			}
			if(start < valueLength)
			{
				unescapedValues.InitAndSetArrayItem(MVCFunctions.str_replace(new XVar("\\|"), new XVar("|"), (XVar)(MVCFunctions.substr((XVar)(fValue), (XVar)(start), (XVar)(valueLength - start)))), null);
			}
			return unescapedValues;
		}
		public virtual XVar getFilterWhereByType(dynamic _param_filterType, dynamic _param_fName, dynamic _param_fValue, dynamic _param_sValue, dynamic _param_parentValues, dynamic _param_connection)
		{
			#region pass-by-value parameters
			dynamic filterType = XVar.Clone(_param_filterType);
			dynamic fName = XVar.Clone(_param_fName);
			dynamic fValue = XVar.Clone(_param_fValue);
			dynamic sValue = XVar.Clone(_param_sValue);
			dynamic parentValues = XVar.Clone(_param_parentValues);
			dynamic connection = XVar.Clone(_param_connection);
			#endregion

			dynamic bNeedQuotes = null, dateField = null, fieldType = null, fullFieldName = null, intervalData = null, parentFiltersNames = XVar.Array(), timeField = null, wheres = XVar.Array();
			fullFieldName = XVar.Clone(RunnerPage._getFieldSQLDecrypt((XVar)(fName), (XVar)(connection), (XVar)(this.pSetSearch), (XVar)(this.cipherer)));
			fieldType = XVar.Clone(this.pSetSearch.getFieldType((XVar)(fName)));
			dateField = XVar.Clone(CommonFunctions.IsDateFieldType((XVar)(fieldType)));
			timeField = XVar.Clone(CommonFunctions.IsTimeType((XVar)(fieldType)));
			if((XVar)(dateField)  || (XVar)(timeField))
			{
			}
			switch(((XVar)filterType).ToString())
			{
				case "interval":
					intervalData = XVar.Clone(this.pSetSearch.getFilterIntervalDatabyIndex((XVar)(fName), (XVar)(fValue)));
					if(XVar.Pack(!(XVar)(MVCFunctions.count(intervalData))))
					{
						return "";
					}
					return FilterIntervalList.getIntervalFilterWhere((XVar)(fName), (XVar)(intervalData), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(connection));
				case "equals":
					if(XVar.Pack(!(XVar)(MVCFunctions.count(parentValues))))
					{
						return MVCFunctions.Concat(fullFieldName, "=", this.cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true)));
					}
					wheres = XVar.Clone(XVar.Array());
					wheres.InitAndSetArrayItem(MVCFunctions.Concat(fullFieldName, "=", this.cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true))), null);
					parentFiltersNames = XVar.Clone(this.pSetSearch.getParentFiltersNames((XVar)(fName)));
					foreach (KeyValuePair<XVar, dynamic> parentName in parentFiltersNames.GetEnumerator())
					{
						wheres.InitAndSetArrayItem(MVCFunctions.Concat(RunnerPage._getFieldSQLDecrypt((XVar)(parentName.Value), (XVar)(connection), (XVar)(this.pSetSearch), (XVar)(this.cipherer)), "=", this.cipherer.MakeDBValue((XVar)(parentName.Value), (XVar)(parentValues[parentName.Key]), new XVar(""), new XVar(true))), null);
					}
					return MVCFunctions.Concat("(", MVCFunctions.implode(new XVar(" AND "), (XVar)(wheres)), ")");
				case "checked":
					if((XVar)(fValue != "on")  && (XVar)(fValue != "off"))
					{
						return "";
					}
					bNeedQuotes = XVar.Clone(CommonFunctions.NeedQuotes((XVar)(fieldType)));
					return CheckboxField.constructFieldWhere((XVar)(fullFieldName), (XVar)(bNeedQuotes), (XVar)(fValue == "on"), (XVar)(this.pSetSearch.getFieldType((XVar)(fName))), (XVar)(connection.dbType));
				case "slider":
					if(XVar.Pack(dateField))
					{
						return FilterIntervalDateSlider.getDateSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					if(XVar.Pack(timeField))
					{
						return FilterIntervalTimeSlider.getTimeSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					return MVCFunctions.Concat(this.cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true)), "<=", fullFieldName, " AND ", fullFieldName, "<=", this.cipherer.MakeDBValue((XVar)(fName), (XVar)(sValue), new XVar(""), new XVar(true)));
				case "moreequal":
					if(XVar.Pack(dateField))
					{
						return FilterIntervalDateSlider.getDateSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					if(XVar.Pack(timeField))
					{
						return FilterIntervalTimeSlider.getTimeSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					return MVCFunctions.Concat(this.cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true)), "<=", fullFieldName);
				case "lessequal":
					if(XVar.Pack(dateField))
					{
						return FilterIntervalDateSlider.getDateSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					if(XVar.Pack(timeField))
					{
						return FilterIntervalTimeSlider.getTimeSliderWhere((XVar)(fName), (XVar)(this.pSetSearch), (XVar)(this.cipherer), (XVar)(this.tName), (XVar)(fValue), (XVar)(sValue), (XVar)(filterType), (XVar)(fullFieldName));
					}
					return MVCFunctions.Concat(fullFieldName, "<=", this.cipherer.MakeDBValue((XVar)(fName), (XVar)(fValue), new XVar(""), new XVar(true)));
				default:
					return "";
			}
			return null;
		}
		public virtual XVar parseItegratedRequest()
		{
			dynamic fieldName = null, searchFieldsArr = XVar.Array(), simpleQueryArr = XVar.Array(), srchCriteriaCombineType = null, tempArr = XVar.Array();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_qs")] = MVCFunctions.postvalue(new XVar("qs"));
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_q")] = MVCFunctions.postvalue(new XVar("q"));
			this.fieldsUsedForSearch = XVar.Clone(XVar.Array());
			this._where.InitAndSetArrayItem((XVar.Pack(GlobalVars.suggestAllContent) ? XVar.Pack("Contains") : XVar.Pack("Starts with")), "simpleSrchTypeComboOpt");
			this._where.InitAndSetArrayItem(false, "simpleSrchTypeComboNot");
			this._where.InitAndSetArrayItem("", "simpleSrchFieldsComboOpt");
			tempArr = XVar.Clone(parseStringToArray((XVar)(MVCFunctions.postvalue(new XVar("qs")))));
			simpleQueryArr = XVar.Clone(tempArr[0]);
			if(XVar.Pack(this.wholeDashboardSearch))
			{
				simpleQueryArr = XVar.Clone(getSimpleSearchFromDashboard());
			}
			this._where.InitAndSetArrayItem(searchUnEscape((XVar)(simpleQueryArr[0])), "_simpleSrch");
			this.simpleSearchActive = XVar.Clone(simpleQueryArr[0] != "");
			if((XVar)(this.simpleSearchActive)  && (XVar)(this.wholeDashboardSearch))
			{
				this.googleLikeFields = XVar.Clone(getGoogleLikeFieldsFromDashboard());
			}
			if(XVar.Pack(this.searchOptions.KeyExists(getArrayValueByIndex((XVar)(simpleQueryArr), new XVar(2), new XVar(true)))))
			{
				dynamic simpleSrchTypeComboNot = null;
				simpleSrchTypeComboNot = XVar.Clone(this.searchOptions[simpleQueryArr[2]]["not"]);
				this._where.InitAndSetArrayItem(this.searchOptions[simpleQueryArr[2]]["option"], "simpleSrchTypeComboOpt");
				if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(this._where["simpleSrchTypeComboOpt"])))))
				{
					this._where.InitAndSetArrayItem((XVar.Pack(GlobalVars.suggestAllContent) ? XVar.Pack("Contains") : XVar.Pack("Starts with")), "simpleSrchTypeComboOpt");
				}
			}
			fieldName = XVar.Clone(MVCFunctions.trim((XVar)(getArrayValueByIndex((XVar)(simpleQueryArr), new XVar(1), new XVar(true)))));
			this._where.InitAndSetArrayItem(fieldName, "simpleSrchFieldsComboOpt");
			if(XVar.Pack(fieldName))
			{
				this.fieldsUsedForSearch.InitAndSetArrayItem(true, fieldName);
			}
			srchCriteriaCombineType = XVar.Clone(MVCFunctions.postvalue(new XVar("criteria")));
			if(XVar.Pack(this.wholeDashboardSearch))
			{
				srchCriteriaCombineType = XVar.Clone(getCriteriaFromDashboard());
			}
			if(XVar.Pack(!(XVar)(srchCriteriaCombineType)))
			{
				srchCriteriaCombineType = new XVar("and");
			}
			this._where.InitAndSetArrayItem(srchCriteriaCombineType, "_srchCriteriaCombineType");
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch")] = getCriteriaCombineType();
			this._where.InitAndSetArrayItem(XVar.Array(), "_srchFields");
			this.advancedSearchActive = new XVar(false);
			searchFieldsArr = XVar.Clone(parseStringToArray((XVar)(MVCFunctions.postvalue(new XVar("q"))), new XVar(true)));
			if(XVar.Pack(this.wholeDashboardSearch))
			{
				searchFieldsArr = XVar.Clone(getSearchFieldsFromDashboard());
			}
			foreach (KeyValuePair<XVar, dynamic> searchItemArr in searchFieldsArr.GetEnumerator())
			{
				dynamic fName = null, opt = null, srchF = XVar.Array();
				if(MVCFunctions.count(searchItemArr.Value) < 2)
				{
					continue;
				}
				fName = XVar.Clone(searchUnEscape((XVar)(searchItemArr.Value[0])));
				if(false == MVCFunctions.in_array((XVar)(fName), (XVar)(this.searchFieldsArr)))
				{
					continue;
				}
				this.advancedSearchActive = new XVar(true);
				srchF = XVar.Clone(XVar.Array());
				srchF.InitAndSetArrayItem(fName, "fName");
				srchF.InitAndSetArrayItem(getArrayValueByIndex((XVar)(searchItemArr.Value), new XVar(3)), "eType");
				srchF.InitAndSetArrayItem(getArrayValueByIndex((XVar)(searchItemArr.Value), new XVar(2), new XVar(true)), "value1");
				opt = XVar.Clone(getArrayValueByIndex((XVar)(searchItemArr.Value), new XVar(1)));
				srchF.InitAndSetArrayItem(false, "not");
				if(XVar.Pack(this.searchOptions.KeyExists(opt)))
				{
					srchF.InitAndSetArrayItem(this.searchOptions[opt]["not"], "not");
					srchF.InitAndSetArrayItem(this.searchOptions[opt]["option"], "opt");
				}
				else
				{
					srchF.InitAndSetArrayItem(getDefaultSearchTypeOption((XVar)(fName), (XVar)(this.pSetSearch)), "opt");
				}
				if(XVar.Pack(this.wholeDashboardSearch))
				{
					srchF.InitAndSetArrayItem(getArrayValueByIndex((XVar)(searchItemArr.Value), new XVar(5)), "not");
				}
				srchF.InitAndSetArrayItem(getArrayValueByIndex((XVar)(searchItemArr.Value), new XVar(4), new XVar(true)), "value2");
				this._where.InitAndSetArrayItem(srchF, "_srchFields", null);
				this.fieldsUsedForSearch.InitAndSetArrayItem(true, fName);
			}
			this._where.InitAndSetArrayItem(XVar.Equals(XVar.Pack(MVCFunctions.postvalue(new XVar("srchOptShowStatus"))), XVar.Pack("1")), "_srchOptShowStatus");
			this._where.InitAndSetArrayItem(XVar.Equals(XVar.Pack(MVCFunctions.postvalue(new XVar("ctrlTypeComboStatus"))), XVar.Pack("1")), "_ctrlTypeComboStatus");
			this._where.InitAndSetArrayItem(XVar.Equals(XVar.Pack(MVCFunctions.postvalue(new XVar("srchWinShowStatus"))), XVar.Pack("1")), "srchWinShowStatus");
			return null;
		}
		protected virtual XVar getCriteriaFromDashboard()
		{
			if(XVar.Pack(this.dashboardSearchClause))
			{
				return this.dashboardSearchClause._where["_srchCriteriaCombineType"];
			}
			return "";
		}
		public virtual XVar getSimpleSearchFromDashboard()
		{
			if(XVar.Pack(this.dashboardSearchClause))
			{
				return new XVar(0, this.dashboardSearchClause._where["_simpleSrch"]);
			}
			else
			{
				return new XVar(0, null);
			}
			return null;
		}
		public virtual XVar getSearchFieldsFromDashboard()
		{
			dynamic dashSearchFieldsSession = XVar.Array(), result = XVar.Array();
			result = XVar.Clone(XVar.Array());
			if(XVar.Pack(this.dashboardSearchClause))
			{
				dashSearchFieldsSession = XVar.Clone(this.dashboardSearchClause._where["_srchFields"]);
			}
			else
			{
				dashSearchFieldsSession = new XVar(null);
			}
			if(XVar.Pack(dashSearchFieldsSession))
			{
				dynamic dashSearchFields = XVar.Array(), dashSettings = null;
				dashSettings = XVar.Clone(new ProjectSettings((XVar)(this.dashTName), new XVar(Constants.PAGE_DASHBOARD)));
				dashSearchFields = XVar.Clone(dashSettings.getDashboardSearchFields());
				foreach (KeyValuePair<XVar, dynamic> data in dashSearchFieldsSession.GetEnumerator())
				{
					foreach (KeyValuePair<XVar, dynamic> fData in dashSearchFields[data.Value["fName"]].GetEnumerator())
					{
						dynamic resutlData = XVar.Array();
						if(fData.Value["table"] != this.tName)
						{
							continue;
						}
						resutlData = XVar.Clone(XVar.Array());
						resutlData.InitAndSetArrayItem(fData.Value["field"], 0);
						foreach (KeyValuePair<XVar, dynamic> optData in this.searchOptions.GetEnumerator())
						{
							if(data.Value["opt"] == optData.Value["option"])
							{
								resutlData.InitAndSetArrayItem(optData.Key, 1);
								break;
							}
						}
						resutlData.InitAndSetArrayItem(data.Value["value1"], 2);
						if(XVar.Pack(data.Value["eType"]))
						{
							resutlData.InitAndSetArrayItem(data.Value["eType"], 3);
						}
						if(XVar.Pack(data.Value["value2"]))
						{
							resutlData.InitAndSetArrayItem(data.Value["value2"], 4);
						}
						resutlData.InitAndSetArrayItem(data.Value["not"], 5);
						result.InitAndSetArrayItem(resutlData, null);
					}
				}
			}
			return result;
		}
		public virtual XVar getGoogleLikeFieldsFromDashboard()
		{
			dynamic dashGoogleLikeFields = XVar.Array(), dashSearchFields = XVar.Array(), dashSettings = null, result = XVar.Array();
			result = XVar.Clone(XVar.Array());
			dashSettings = XVar.Clone(new ProjectSettings((XVar)(this.dashTName), new XVar(Constants.PAGE_DASHBOARD)));
			dashGoogleLikeFields = XVar.Clone(dashSettings.getGoogleLikeFields());
			dashSearchFields = XVar.Clone(dashSettings.getDashboardSearchFields());
			foreach (KeyValuePair<XVar, dynamic> field in dashGoogleLikeFields.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> data in dashSearchFields[field.Value].GetEnumerator())
				{
					if(data.Value["table"] == this.tName)
					{
						result.InitAndSetArrayItem(data.Value["field"], null);
					}
				}
			}
			return result;
		}
		public virtual XVar searchUnEscape(dynamic _param_inputString)
		{
			#region pass-by-value parameters
			dynamic inputString = XVar.Clone(_param_inputString);
			#endregion

			return MVCFunctions.str_replace(new XVar("\\\\"), new XVar("\\"), (XVar)(MVCFunctions.str_replace((XVar)(MVCFunctions.Concat("\\", this.valueDelimiter)), (XVar)(this.valueDelimiter), (XVar)(MVCFunctions.str_replace((XVar)(MVCFunctions.Concat("\\", this.fieldDelimiterLeft, this.fieldDelimiterRight)), (XVar)(MVCFunctions.Concat(this.fieldDelimiterLeft, this.fieldDelimiterRight)), (XVar)(inputString))))));
		}
		public virtual XVar parseStringToArray(dynamic _param_inputString, dynamic _param_advanced = null)
		{
			#region default values
			if(_param_advanced as Object == null) _param_advanced = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic inputString = XVar.Clone(_param_inputString);
			dynamic advanced = XVar.Clone(_param_advanced);
			#endregion

			dynamic i = null, result = XVar.Array(), startPos = null, strLength = null, valuesArray = XVar.Array();
			if(0 == MVCFunctions.strlen((XVar)(inputString)))
			{
				return XVar.Array();
			}
			result = XVar.Clone(XVar.Array());
			valuesArray = XVar.Clone(XVar.Array());
			startPos = new XVar(0);
			if(XVar.Pack(advanced))
			{
				inputString = XVar.Clone(MVCFunctions.substr((XVar)(inputString), new XVar(1), (XVar)(MVCFunctions.strlen((XVar)(inputString)) - 2)));
			}
			strLength = XVar.Clone(MVCFunctions.strlen((XVar)(inputString)));
			i = new XVar(0);
			for(;i < strLength; i++)
			{
				if(inputString[i] == this.valueDelimiter)
				{
					if(XVar.Pack(isDelimiter((XVar)(inputString), (XVar)(startPos), (XVar)(i))))
					{
						valuesArray.InitAndSetArrayItem(MVCFunctions.trim((XVar)(MVCFunctions.substr((XVar)(inputString), (XVar)(startPos), (XVar)(i - startPos)))), null);
						startPos = XVar.Clone(i + 1);
					}
				}
				if((XVar)(i == strLength - 1)  || (XVar)(inputString[i] == this.fieldDelimiterLeft))
				{
					if((XVar)(i == strLength - 1)  || (XVar)(isDelimiter((XVar)(inputString), (XVar)(startPos), (XVar)(i), new XVar(true))))
					{
						valuesArray.InitAndSetArrayItem(MVCFunctions.trim((XVar)(MVCFunctions.substr((XVar)(inputString), (XVar)(startPos), (XVar)((i - startPos) + ((XVar.Pack(i == strLength - 1) ? XVar.Pack(1) : XVar.Pack(0))))))), null);
						result.InitAndSetArrayItem(valuesArray, null);
						valuesArray = XVar.Clone(XVar.Array());
						startPos = XVar.Clone(i + 2);
						i++;
					}
				}
			}
			return result;
		}
		public virtual XVar isDelimiter(dynamic inputString, dynamic _param_startPos, dynamic _param_currentPos, dynamic _param_isFieldDelimiter = null)
		{
			#region default values
			if(_param_isFieldDelimiter as Object == null) _param_isFieldDelimiter = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic startPos = XVar.Clone(_param_startPos);
			dynamic currentPos = XVar.Clone(_param_currentPos);
			dynamic isFieldDelimiter = XVar.Clone(_param_isFieldDelimiter);
			#endregion

			dynamic backSlahesCount = null, i = null, result = null;
			backSlahesCount = new XVar(0);
			i = XVar.Clone(currentPos - 1);
			for(;startPos <= i; i--)
			{
				if(inputString[i] != "\\")
				{
					break;
				}
				backSlahesCount++;
			}
			result = XVar.Clone((XVar)(backSlahesCount == XVar.Pack(0))  || (XVar)(backSlahesCount  %  2 == 0));
			if((XVar)((XVar)(result)  && (XVar)(isFieldDelimiter))  && (XVar)(currentPos + 1 < MVCFunctions.strlen((XVar)(inputString))))
			{
				return inputString[currentPos + 1] == this.fieldDelimiterRight;
			}
			return result;
		}
		public virtual XVar getArrayValueByIndex(dynamic arr, dynamic _param_index, dynamic _param_isEncoded = null)
		{
			#region default values
			if(_param_isEncoded as Object == null) _param_isEncoded = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic index = XVar.Clone(_param_index);
			dynamic isEncoded = XVar.Clone(_param_isEncoded);
			#endregion

			dynamic result = null;
			result = new XVar("");
			if(XVar.Pack(arr.KeyExists(index)))
			{
				result = XVar.Clone(arr[index]);
				if(XVar.Pack(isEncoded))
				{
					result = XVar.Clone(searchUnEscape((XVar)(result)));
				}
			}
			return result;
		}
		public virtual XVar getDefaultSearchTypeOption(dynamic _param_fName, dynamic _param_pSet_packed)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic fType = null, var_option = null;
			fType = XVar.Clone(pSet.getEditFormat((XVar)(fName)));
			var_option = new XVar("Equals");
			if(fType == Constants.EDIT_FORMAT_LOOKUP_WIZARD)
			{
				if(XVar.Pack(pSet.multiSelect((XVar)(fName))))
				{
					var_option = new XVar("Contains");
				}
			}
			else
			{
				if((XVar)((XVar)((XVar)((XVar)(fType == Constants.EDIT_FORMAT_TEXT_FIELD)  || (XVar)(fType == Constants.EDIT_FORMAT_TEXT_AREA))  || (XVar)(fType == Constants.EDIT_FORMAT_PASSWORD))  || (XVar)(fType == Constants.EDIT_FORMAT_HIDDEN))  || (XVar)(fType == Constants.EDIT_FORMAT_READONLY))
				{
					if(XVar.Pack(!(XVar)(this.cipherer.isFieldPHPEncrypted((XVar)(fName)))))
					{
						var_option = new XVar("Contains");
					}
				}
			}
			return var_option;
		}
		protected virtual XVar removeSessionSearchVariables()
		{
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_qs")]))
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_qs"));
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_q")]))
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_q"));
			}
			if(XVar.Pack(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch")]))
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_criteriaSearch"));
			}
			return null;
		}
		public virtual XVar parseRequest()
		{
			this.wholeDashboardSearch = new XVar(false);
			if(MVCFunctions.postvalue("a") == "showall")
			{
				this._where.InitAndSetArrayItem(0, "_search");
				this.srchType = new XVar("showall");
				this.bIsUsedSrch = new XVar(false);
				clearSearch();
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
				removeSessionSearchVariables();
				this.simpleSearchActive = new XVar(false);
			}
			else
			{
				if((XVar)((XVar)(MVCFunctions.REQUESTKeyExists("q"))  || (XVar)(MVCFunctions.REQUESTKeyExists("qs")))  || (XVar)(MVCFunctions.postvalue("f")))
				{
					this.srchType = new XVar("integrated");
					parseItegratedRequest();
					this.bIsUsedSrch = XVar.Clone((XVar)((XVar)(MVCFunctions.REQUESTKeyExists("q"))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.postvalue("q")), XVar.Pack(""))))  || (XVar)((XVar)(MVCFunctions.REQUESTKeyExists("qs"))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.postvalue("qs")), XVar.Pack("")))));
					XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
				}
				else
				{
					if((XVar)(this.dashTName)  && (XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.dashTName, "_advsearch"))))
					{
						this.dashboardSearchClause = XVar.Clone(UnserializeObject((XVar)(XSession.Session[MVCFunctions.Concat(this.dashTName, "_advsearch")])));
						this.wholeDashboardSearch = XVar.Clone(this.dashboardSearchClause.bIsUsedSrch);
						if(XVar.Pack(this.wholeDashboardSearch))
						{
							this.srchType = new XVar("integrated");
							parseItegratedRequest();
							this.bIsUsedSrch = new XVar(true);
						}
						else
						{
							if(this.dashboardSearchClause.srchType == "showall")
							{
								this._where.InitAndSetArrayItem(0, "_search");
								this.srchType = new XVar("showall");
								this.bIsUsedSrch = new XVar(false);
								clearSearch();
								this.simpleSearchActive = new XVar(false);
								removeSessionSearchVariables();
							}
						}
					}
				}
			}
			if(XVar.Pack(MVCFunctions.postvalue("f")))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_filters")] = MVCFunctions.postvalue("f");
			}
			this.filtersActivated = XVar.Clone((XVar)(XSession.Session.KeyExists(MVCFunctions.Concat(this.sessionPrefix, "_filters")))  && (XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_filters")] != "all"));
			if(XVar.Pack(this.searchSavingEnabled))
			{
				if(XVar.Pack(MVCFunctions.REQUESTKeyExists("savedSearch")))
				{
					this.savedSearchIsRun = new XVar(true);
				}
				else
				{
					if((XVar)((XVar)((XVar)(this.bIsUsedSrch)  || (XVar)(this.filtersActivated))  && (XVar)(!(XVar)(searchHasTheSameSearchParams())))  || (XVar)(this.srchType == "showall"))
					{
						this.savedSearchIsRun = new XVar(false);
					}
				}
			}
			return null;
		}
		public virtual XVar storeSearchParamsForLogging()
		{
			if(XVar.Pack(!(XVar)(this.searchSavingEnabled)))
			{
				return null;
			}
			if((XVar)(!(XVar)(MVCFunctions.REQUESTKeyExists("saveSearch")))  && (XVar)(!(XVar)(MVCFunctions.REQUESTKeyExists("deleteSearch"))))
			{
				if(this.srchType == "showall")
				{
					this.searchParams = XVar.Clone(new XVar("f", this.searchParams["f"]));
				}
				else
				{
					if((XVar)((XVar)(!(XVar)(MVCFunctions.postvalue("goto")))  && (XVar)(!(XVar)(MVCFunctions.postvalue("orderby"))))  && (XVar)(!(XVar)(MVCFunctions.postvalue("pagesize"))))
					{
						this.searchParams = XVar.Clone(XVar.Array());
					}
				}
			}
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("q")))
			{
				this.searchParams.InitAndSetArrayItem(MVCFunctions.postvalue("q"), "q");
				this.searchParams.InitAndSetArrayItem(MVCFunctions.postvalue("criteria"), "criteria");
			}
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("qs")))
			{
				this.searchParams.InitAndSetArrayItem(MVCFunctions.postvalue("qs"), "qs");
			}
			if(XVar.Pack(MVCFunctions.REQUESTKeyExists("f")))
			{
				this.searchParams.InitAndSetArrayItem(MVCFunctions.postvalue("f"), "f");
			}
			return null;
		}
		public virtual XVar searchHasTheSameSearchParams()
		{
			if((XVar)((XVar)(MVCFunctions.postvalue("goto"))  || (XVar)(MVCFunctions.postvalue("orderby")))  || (XVar)(MVCFunctions.postvalue("pagesize")))
			{
				return true;
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this.searchParams))))
			{
				return false;
			}
			if((XVar)((XVar)(MVCFunctions.postvalue("q") != this.searchParams["q"])  || (XVar)(MVCFunctions.postvalue("qs") != this.searchParams["qs"]))  || (XVar)(MVCFunctions.postvalue("f") != this.searchParams["f"]))
			{
				return false;
			}
			return true;
		}
		public virtual XVar getSearchParamsForSaving()
		{
			return this.searchParams;
		}
		public virtual XVar clearSearch()
		{
			this._where.InitAndSetArrayItem("", "_simpleSrch");
			this._where.InitAndSetArrayItem("and", "_srchCriteriaCombineType");
			this._where.InitAndSetArrayItem("Contains", "simpleSrchTypeComboOpt");
			this._where.InitAndSetArrayItem(false, "simpleSrchTypeComboNot");
			this._where.InitAndSetArrayItem("", "simpleSrchFieldsComboOpt");
			this._where.InitAndSetArrayItem(XVar.Array(), "_srchFields");
			this._where.InitAndSetArrayItem(false, "_srchOptShowStatus");
			this._where.InitAndSetArrayItem(false, "_ctrlTypeComboStatus");
			this._where.InitAndSetArrayItem(false, "srchWinShowStatus");
			this.fieldsUsedForSearch = XVar.Clone(XVar.Array());
			return null;
		}
		public virtual XVar getSearchCtrlParams(dynamic _param_fName)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			#endregion

			dynamic editControls = null, resArr = XVar.Array();
			resArr = XVar.Clone(XVar.Array());
			editControls = XVar.Clone(getLocalEditControls());
			if(XVar.Pack(this._where["_srchFields"]))
			{
				foreach (KeyValuePair<XVar, dynamic> srchField in this._where["_srchFields"].GetEnumerator())
				{
					if(MVCFunctions.strtolower((XVar)(srchField.Value["fName"])) == MVCFunctions.strtolower((XVar)(fName)))
					{
						dynamic ctrl = null, eType = null, tField = XVar.Array();
						tField = XVar.Clone(srchField.Value);
						ctrl = XVar.Clone(editControls.getControl((XVar)(fName)));
						eType = XVar.Clone(tField["eType"]);
						if(XVar.Pack(ctrl.checkIfDisplayFieldSearch((XVar)(tField["opt"]))))
						{
							eType = new XVar("display");
						}
						tField.InitAndSetArrayItem(CommonFunctions.prepare_for_db((XVar)(tField["fName"]), (XVar)(tField["value1"]), (XVar)(eType), new XVar(""), (XVar)(this.tName)), "value1");
						tField.InitAndSetArrayItem(CommonFunctions.prepare_for_db((XVar)(tField["fName"]), (XVar)(tField["value2"]), (XVar)(eType), new XVar(""), (XVar)(this.tName)), "value2");
						resArr.InitAndSetArrayItem(tField, null);
					}
				}
			}
			return resArr;
		}
		public virtual XVar getUsedCtrlsCount()
		{
			if(XVar.Pack(this._where["_srchFields"]))
			{
				return MVCFunctions.count(this._where["_srchFields"]);
			}
			return 0;
		}
		public virtual XVar getSearchGlobalParams()
		{
			return new XVar("simpleSrch", this._where["_simpleSrch"], "srchTypeRadio", getCriteriaCombineType(), "srchType", this.srchType, "simpleSrchTypeComboOpt", this._where["simpleSrchTypeComboOpt"], "simpleSrchTypeComboNot", this._where["simpleSrchTypeComboNot"], "simpleSrchFieldsComboOpt", this._where["simpleSrchFieldsComboOpt"]);
		}
		public virtual XVar getSrchPanelAttrs()
		{
			return new XVar("srchOptShowStatus", (XVar)(this._where["_srchOptShowStatus"])  || (XVar)(MVCFunctions.count(this.panelSearchFields)), "ctrlTypeComboStatus", this._where["_ctrlTypeComboStatus"], "srchWinShowStatus", this._where["srchWinShowStatus"]);
		}
		public virtual XVar isUsedSrch()
		{
			return this.bIsUsedSrch;
		}
		public virtual XVar isShowAll()
		{
			return this.bIsUsedSrch;
		}
		public virtual XVar isSearchFunctionalityActivated()
		{
			return (XVar)((XVar)(this.bIsUsedSrch)  || (XVar)(this.filtersActivated))  || (XVar)(0 < MVCFunctions.count(getSearchFields()));
		}
		public virtual XVar isRequiredSearchRunning()
		{
			if(XVar.Pack(!(XVar)(this.bIsUsedSrch)))
			{
				return false;
			}
			if(XVar.Pack(MVCFunctions.count(this.pSetSearch)))
			{
				dynamic requiredSearchFields = XVar.Array();
				requiredSearchFields = XVar.Clone(this.pSetSearch.getSearchRequiredFields());
				foreach (KeyValuePair<XVar, dynamic> fName in requiredSearchFields.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(this.fieldsUsedForSearch[fName.Value])))
					{
						return false;
					}
				}
			}
			return true;
		}
		public virtual XVar getSearchToHighlight(dynamic _param_fname, dynamic _param_lookupParams = null)
		{
			#region default values
			if(_param_lookupParams as Object == null) _param_lookupParams = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic fname = XVar.Clone(_param_fname);
			dynamic lookupParams = XVar.Clone(_param_lookupParams);
			#endregion

			dynamic multiselect = null, needLookupProcessing = null, opt = null, options = XVar.Array(), simpleSearch = XVar.Array(), srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(fname), (XVar)(this.searchFieldsArr)))))
			{
				return false;
			}
			options = XVar.Clone(XVar.Array());
			simpleSearch.InitAndSetArrayItem(this._where["simpleSrchFieldsComboOpt"], "fname");
			opt = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if(XVar.Pack(this.isShowSimpleSrchOpt))
			{
				simpleSearch.InitAndSetArrayItem(new XVar(0, this._where["_simpleSrch"]), "value");
			}
			else
			{
				simpleSearch.InitAndSetArrayItem(googleLikeParseString((XVar)(this._where["_simpleSrch"])), "value");
			}
			if((XVar)((XVar)(simpleSearch.KeyExists("value"))  && (XVar)(MVCFunctions.count(simpleSearch["value"])))  && (XVar)((XVar)(!(XVar)(simpleSearch["fname"]))  || (XVar)(simpleSearch["fname"] == fname)))
			{
				foreach (KeyValuePair<XVar, dynamic> simpleSearchValue in simpleSearch["value"].GetEnumerator())
				{
					if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(simpleSearchValue.Value))))))
					{
						options.InitAndSetArrayItem(simpleSearchValue.Value, opt, fname, null);
					}
				}
			}
			srchFields = XVar.Clone(this._where["_srchFields"]);
			if(XVar.Pack(!(XVar)(srchFields)))
			{
				srchFields = XVar.Clone(XVar.Array());
			}
			multiselect = XVar.Clone(lookupParams["multiselect"]);
			needLookupProcessing = XVar.Clone(lookupParams["needLookupProcessing"]);
			foreach (KeyValuePair<XVar, dynamic> srchFieldData in srchFields.GetEnumerator())
			{
				dynamic values = XVar.Array();
				if((XVar)(srchFieldData.Value["fName"] != fname)  || (XVar)(srchFieldData.Value["not"]))
				{
					continue;
				}
				opt = XVar.Clone(srchFieldData.Value["opt"]);
				if((XVar)((XVar)(opt != "Contains")  && (XVar)(opt != "Equals"))  && (XVar)(opt != "Starts with"))
				{
					continue;
				}
				if((XVar)(needLookupProcessing)  && (XVar)(opt == "Equals"))
				{
					options.InitAndSetArrayItem(MVCFunctions.implode(new XVar(","), (XVar)(CommonFunctions.splitvalues((XVar)(srchFieldData.Value["value1"])))), opt, srchFieldData.Value["fName"], null);
					continue;
				}
				if((XVar)(!(XVar)(multiselect))  || (XVar)(opt != "Contains"))
				{
					options.InitAndSetArrayItem(srchFieldData.Value["value1"], opt, srchFieldData.Value["fName"], null);
					continue;
				}
				values = XVar.Clone(CommonFunctions.splitvalues((XVar)(srchFieldData.Value["value1"])));
				foreach (KeyValuePair<XVar, dynamic> value in values.GetEnumerator())
				{
					options.InitAndSetArrayItem(value.Value, opt, srchFieldData.Value["fName"], null);
				}
			}
			if(XVar.Pack(options["Equals"][fname]))
			{
				return new XVar("searchWords", options["Equals"][fname], "option", "Equals");
			}
			if(XVar.Pack(options["Starts with"][fname]))
			{
				return new XVar("searchWords", options["Starts with"][fname], "option", "Starts with");
			}
			if(XVar.Pack(options["Contains"][fname]))
			{
				return new XVar("searchWords", options["Contains"][fname], "option", "Contains");
			}
			return false;
		}
		public virtual XVar getSearchHighlightingData(dynamic _param_fname, dynamic _param_value, dynamic _param_encoded, dynamic _param_lookupParams)
		{
			#region pass-by-value parameters
			dynamic fname = XVar.Clone(_param_fname);
			dynamic value = XVar.Clone(_param_value);
			dynamic encoded = XVar.Clone(_param_encoded);
			dynamic lookupParams = XVar.Clone(_param_lookupParams);
			#endregion

			dynamic flags = null, searchData = XVar.Array(), searchOpt = null, searchWordArr = XVar.Array();
			searchData = XVar.Clone(getSearchToHighlight((XVar)(fname), (XVar)(lookupParams)));
			if(XVar.Pack(!(XVar)(searchData)))
			{
				return false;
			}
			flags = XVar.Clone((XVar.Pack(GlobalVars.useUTF8) ? XVar.Pack("iu") : XVar.Pack("i")));
			searchWordArr = XVar.Clone(XVar.Array());
			searchOpt = XVar.Clone(searchData["option"]);
			foreach (KeyValuePair<XVar, dynamic> searchWord in searchData["searchWords"].GetEnumerator())
			{
				dynamic curSearchWord = null, isMatched = null, matches = XVar.Array(), pattern = null;
				curSearchWord = XVar.Clone(searchWord.Value);
				if((XVar)((XVar)(searchOpt == "Contains")  && (XVar)(lookupParams["originLinkValue"] == searchWord.Value))  || (XVar)((XVar)(searchOpt == "Equals")  && (XVar)(lookupParams["linkFieldValue"] == searchWord.Value)))
				{
					return new XVar("searchWords", new XVar(0, value), "searchOpt", searchData["option"]);
				}
				if(XVar.Pack(encoded))
				{
					curSearchWord = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(curSearchWord)));
				}
				pattern = XVar.Clone(MVCFunctions.Concat("/", MVCFunctions.preg_quote((XVar)(curSearchWord), new XVar("/")), "/", flags));
				if(searchOpt == "Starts with")
				{
					pattern = XVar.Clone(MVCFunctions.Concat("/^", MVCFunctions.preg_quote((XVar)(curSearchWord), new XVar("/")), "/", flags));
				}
				isMatched = XVar.Clone(MVCFunctions.preg_match((XVar)(pattern), (XVar)(value), (XVar)(matches)));
				if((XVar)(isMatched)  && (XVar)((XVar)(searchOpt != "Equals")  || (XVar)(value == matches[0])))
				{
					curSearchWord = XVar.Clone(matches[0]);
					searchWordArr.InitAndSetArrayItem(curSearchWord, null);
				}
			}
			if(XVar.Pack(MVCFunctions.count(searchWordArr)))
			{
				return new XVar("searchWords", searchWordArr, "searchOpt", searchOpt);
			}
			return false;
		}
		protected virtual XVar googleLikeParseString(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic matches = XVar.Array(), ret = XVar.Array();
			ret = XVar.Clone(XVar.Array());
			matches = XVar.Clone(XVar.Array());
			if(XVar.Pack(MVCFunctions.preg_match_all(new XVar("/(\\\"[^\"]+\\\")|([^\\s]+)/"), (XVar)(str), (XVar)(matches))))
			{
				foreach (KeyValuePair<XVar, dynamic> match in matches[0].GetEnumerator())
				{
					ret.InitAndSetArrayItem((XVar.Pack(match.Value[0] == "\"") ? XVar.Pack(MVCFunctions.substr((XVar)(match.Value), new XVar(1), new XVar(-1))) : XVar.Pack(match.Value)), null);
				}
			}
			return MVCFunctions.array_unique((XVar)(ret));
		}
		public virtual XVar getCriteriaCombineType()
		{
			if(this._where["_srchCriteriaCombineType"] == "or")
			{
				return "or";
			}
			if((XVar)(this.simpleSearchActive)  && (XVar)(!(XVar)(MVCFunctions.count(this._where["_srchFields"]))))
			{
				return "or";
			}
			return "and";
		}
		public static XVar UnserializeObject(dynamic _param_str)
		{
			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			#endregion

			dynamic obj = null;
			if(XVar.Pack(!(XVar)(str)))
			{
				return null;
			}
			obj = XVar.Clone(MVCFunctions.unserialize((XVar)(str)));
			obj.pSetSearch = XVar.Clone(new ProjectSettings((XVar)(obj.tName), new XVar(Constants.PAGE_SEARCH)));
			obj.cipherer = XVar.Clone(new RunnerCipherer((XVar)(obj.tName), (XVar)(obj.pSetSearch)));
			return obj;
		}
		public virtual XVar getSearchFields()
		{
			dynamic fieldsData = XVar.Array(), simpleSrch = null, simpleSrchArr = XVar.Array(), simpleSrchField = null, simpleSrchOption = null;
			fieldsData = XVar.Clone(XVar.Array());
			if(0 < MVCFunctions.count(this._where["_srchFields"]))
			{
				foreach (KeyValuePair<XVar, dynamic> sfData in this._where["_srchFields"].GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(fieldsData[sfData.Value["fName"]])))
					{
						fieldsData.InitAndSetArrayItem(XVar.Array(), sfData.Value["fName"]);
					}
					fieldsData.InitAndSetArrayItem(Constants.SEARCHID_PANEL + sfData.Key, sfData.Value["fName"], null);
				}
			}
			if((XVar)(this.haveAggregateFields)  && (XVar)(this.advancedSearchActive))
			{
				return fieldsData;
			}
			simpleSrch = XVar.Clone(this._where["_simpleSrch"]);
			simpleSrchOption = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if((XVar)((XVar)(simpleSrch == null)  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(simpleSrch)))))  && (XVar)(simpleSrchOption != "Empty"))
			{
				return fieldsData;
			}
			simpleSrchField = XVar.Clone(this._where["simpleSrchFieldsComboOpt"]);
			if((XVar)((XVar)(simpleSrch != null)  && (XVar)(MVCFunctions.strlen((XVar)(simpleSrchField))))  && (XVar)(MVCFunctions.in_array((XVar)(simpleSrchField), (XVar)(this.googleLikeFields))))
			{
				if(XVar.Pack(!(XVar)(fieldsData[simpleSrchField])))
				{
					fieldsData.InitAndSetArrayItem(XVar.Array(), simpleSrchField);
				}
				fieldsData.InitAndSetArrayItem(Constants.SEARCHID_SIMPLE, simpleSrchField, null);
			}
			if(XVar.Pack(this.isShowSimpleSrchOpt))
			{
				simpleSrchArr = XVar.Clone(new XVar(0, simpleSrch));
			}
			else
			{
				simpleSrchArr = XVar.Clone(googleLikeParseString((XVar)(simpleSrch)));
			}
			foreach (KeyValuePair<XVar, dynamic> simpleSrchItem in simpleSrchArr.GetEnumerator())
			{
				dynamic i = null;
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.searchFieldsArr); i++)
				{
					if(XVar.Pack(MVCFunctions.in_array((XVar)(this.searchFieldsArr[i]), (XVar)(this.googleLikeFields))))
					{
						if(XVar.Pack(!(XVar)(fieldsData[this.searchFieldsArr[i]])))
						{
							fieldsData.InitAndSetArrayItem(XVar.Array(), this.searchFieldsArr[i]);
						}
						fieldsData.InitAndSetArrayItem(Constants.SEARCHID_ALL + simpleSrchItem.Key, this.searchFieldsArr[i], null);
					}
				}
			}
			return fieldsData;
		}
		public virtual XVar isSearchPanelByUserApiRun()
		{
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this._where["_srchFields"]))))
			{
				return false;
			}
			foreach (KeyValuePair<XVar, dynamic> sfData in this._where["_srchFields"].GetEnumerator())
			{
				if(XVar.Pack(sfData.Value["byUserApi"]))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getFieldValue(dynamic _param_field, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic simpleSrch = null, simpleSrchArr = XVar.Array(), simpleSrchField = null, simpleSrchOption = null, srchFields = XVar.Array();
			srchFields = this._where["_srchFields"];
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					return srchF.Value["value1"];
				}
			}
			if((XVar)(this.haveAggregateFields)  && (XVar)(this.advancedSearchActive))
			{
				return null;
			}
			simpleSrch = XVar.Clone(this._where["_simpleSrch"]);
			simpleSrchOption = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if((XVar)((XVar)((XVar)(simpleSrch == null)  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(simpleSrch)))))  && (XVar)(simpleSrchOption != "Empty"))  || (XVar)(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(this.googleLikeFields)))))
			{
				return null;
			}
			simpleSrchField = XVar.Clone(this._where["simpleSrchFieldsComboOpt"]);
			if((XVar)((XVar)(simpleSrchField == field)  && (XVar)(simpleSrch != null))  && (XVar)((XVar)(id == Constants.SEARCHID_SIMPLE)  || (XVar)(id == null)))
			{
				return simpleSrch;
			}
			if(XVar.Pack(MVCFunctions.strlen((XVar)(simpleSrchField))))
			{
				return null;
			}
			if(XVar.Pack(this.isShowSimpleSrchOpt))
			{
				simpleSrchArr = XVar.Clone(new XVar(0, simpleSrch));
			}
			else
			{
				simpleSrchArr = XVar.Clone(googleLikeParseString((XVar)(simpleSrch)));
			}
			foreach (KeyValuePair<XVar, dynamic> simpleSrchItem in simpleSrchArr.GetEnumerator())
			{
				dynamic i = null;
				i = new XVar(0);
				for(;i < MVCFunctions.count(this.searchFieldsArr); i++)
				{
					if((XVar)(this.searchFieldsArr[i] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_ALL + simpleSrchItem.Key)  || (XVar)(id == null)))
					{
						return simpleSrchItem.Value;
					}
				}
			}
			return null;
		}
		public virtual XVar getSecondFieldValue(dynamic _param_field, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic srchFields = XVar.Array();
			srchFields = this._where["_srchFields"];
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					return srchF.Value["value2"];
				}
			}
			return null;
		}
		protected virtual XVar getSearchOptionUserText(dynamic _param_opt, dynamic _param_not)
		{
			#region pass-by-value parameters
			dynamic opt = XVar.Clone(_param_opt);
			dynamic var_not = XVar.Clone(_param_not);
			#endregion

			switch(((XVar)opt).ToString())
			{
				case "Contains":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_CONTAINS) : XVar.Pack(Constants.CONTAINS));
				case "Equals":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_EQUALS) : XVar.Pack(Constants.EQUALS));
				case "Starts with":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_STARTS_WITH) : XVar.Pack(Constants.STARTS_WITH));
				case "More than":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_MORE_THAN) : XVar.Pack(Constants.MORE_THAN));
				case "Less than":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_LESS_THAN) : XVar.Pack(Constants.LESS_THAN));
				case "Between":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_BETWEEN) : XVar.Pack(Constants.BETWEEN));
				case "Empty":
					return (XVar.Pack(var_not) ? XVar.Pack(Constants.NOT_EMPTY) : XVar.Pack(Constants.EMPTY_SEARCH));
				default:
					return Constants.CONTAINS;
			}
			return null;
		}
		public virtual XVar getSearchOption(dynamic _param_field, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic simpleSrch = null, simpleSrchOption = null, srchFields = XVar.Array();
			srchFields = this._where["_srchFields"];
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					return getSearchOptionUserText((XVar)(srchF.Value["opt"]), (XVar)(srchF.Value["not"]));
				}
			}
			if((XVar)(this.haveAggregateFields)  && (XVar)(this.advancedSearchActive))
			{
				return "";
			}
			simpleSrchOption = XVar.Clone(this._where["simpleSrchTypeComboOpt"]);
			if(XVar.Pack(MVCFunctions.strlen((XVar)(simpleSrchOption))))
			{
				return simpleSrchOption;
			}
			simpleSrch = XVar.Clone(this._where["_simpleSrch"]);
			if((XVar)(simpleSrch == null)  || (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(simpleSrch)))))
			{
				return "";
			}
			return Constants.CONTAINS;
		}
		public virtual XVar getAllFieldsSearchValue()
		{
			if((XVar)(this.haveAggregateFields)  && (XVar)(this.advancedSearchActive))
			{
				return null;
			}
			return this._where["_simpleSrch"];
		}
		protected virtual XVar addSPControlData(dynamic _param_field, dynamic _param_value1, dynamic _param_value2, dynamic _param_opt = null, dynamic _param_not = null)
		{
			#region default values
			if(_param_opt as Object == null) _param_opt = new XVar("");
			if(_param_not as Object == null) _param_not = new XVar(false);
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value1 = XVar.Clone(_param_value1);
			dynamic value2 = XVar.Clone(_param_value2);
			dynamic opt = XVar.Clone(_param_opt);
			dynamic var_not = XVar.Clone(_param_not);
			#endregion

			dynamic srchF = XVar.Array(), srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(this.searchFieldsArr)))))
			{
				return -1;
			}
			if(XVar.Pack(!(XVar)(opt)))
			{
				opt = XVar.Clone(getDefaultSearchTypeOption((XVar)(field), (XVar)(this.pSetSearch)));
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.count(this._where["_srchFields"]))))
			{
				this._where.InitAndSetArrayItem(XVar.Array(), "_srchFields");
			}
			srchFields = this._where["_srchFields"];
			srchF = XVar.Clone(XVar.Array());
			srchF.InitAndSetArrayItem(field, "fName");
			srchF.InitAndSetArrayItem(getSPCtrlEType((XVar)(field), (XVar)(opt)), "eType");
			srchF.InitAndSetArrayItem(value1, "value1");
			srchF.InitAndSetArrayItem(var_not, "not");
			srchF.InitAndSetArrayItem(opt, "opt");
			srchF.InitAndSetArrayItem(value2, "value2");
			srchF.InitAndSetArrayItem(true, "byUserApi");
			srchFields.InitAndSetArrayItem(srchF, null);
			return (Constants.SEARCHID_PANEL + MVCFunctions.count(srchFields)) - 1;
		}
		protected virtual XVar getSPCtrlEType(dynamic _param_fName, dynamic _param_opt = null)
		{
			#region default values
			if(_param_opt as Object == null) _param_opt = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic opt = XVar.Clone(_param_opt);
			#endregion

			dynamic ctrl = null, editControls = null, editFormat = null;
			editFormat = XVar.Clone(this.pSetSearch.getEditFormat((XVar)(fName)));
			if(editFormat == Constants.EDIT_FORMAT_DATE)
			{
				dynamic dateEditType = null;
				dateEditType = XVar.Clone(this.pSetSearch.getDateEditType((XVar)(fName)));
				return MVCFunctions.Concat("date", dateEditType);
			}
			if(editFormat == Constants.EDIT_FORMAT_CHECKBOX)
			{
				return "checkbox";
			}
			editControls = XVar.Clone(getLocalEditControls());
			ctrl = XVar.Clone(editControls.getControl((XVar)(fName)));
			if(XVar.Pack(ctrl.checkIfDisplayFieldSearch((XVar)(opt))))
			{
				return "display";
			}
			return "";
		}
		public virtual XVar setFieldValue(dynamic _param_field, dynamic _param_value, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic newId = null, srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(this.searchFieldsArr)))))
			{
				return -1;
			}
			srchFields = this._where["_srchFields"];
			if((XVar)((XVar)((XVar)(this.isShowSimpleSrchOpt)  && (XVar)((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(this._where["simpleSrchFieldsComboOpt"]))))  || (XVar)(this._where["simpleSrchFieldsComboOpt"] == field)))  && (XVar)(this._where["_simpleSrch"] == ""))  && (XVar)((XVar)(id == Constants.SEARCHID_SIMPLE)  || (XVar)((XVar)(!(XVar)(MVCFunctions.count(srchFields)))  && (XVar)(id == null))))
			{
				this._where.InitAndSetArrayItem(field, "simpleSrchFieldsComboOpt");
				this._where.InitAndSetArrayItem(value, "_simpleSrch");
				return Constants.SEARCHID_SIMPLE;
			}
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					srchFields.InitAndSetArrayItem(value, srchF.Key, "value1");
					return Constants.SEARCHID_PANEL + srchF.Key;
				}
			}
			newId = XVar.Clone(addSPControlData((XVar)(field), (XVar)(value), new XVar(""), new XVar("")));
			return newId;
		}
		public virtual XVar setSecondFieldValue(dynamic _param_field, dynamic _param_value, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic newId = null, srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(this.searchFieldsArr)))))
			{
				return -1;
			}
			srchFields = this._where["_srchFields"];
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					srchFields.InitAndSetArrayItem(value, srchF.Key, "value2");
					return Constants.SEARCHID_PANEL + srchF.Key;
				}
			}
			newId = XVar.Clone(addSPControlData((XVar)(field), new XVar(""), (XVar)(value), new XVar("Between")));
			return newId;
		}
		protected virtual XVar convertUserSearchOption(dynamic _param_opt)
		{
			#region pass-by-value parameters
			dynamic opt = XVar.Clone(_param_opt);
			#endregion

			switch(((XVar)opt).ToString())
			{
				case Constants.CONTAINS:
					return "contains";
				case Constants.EQUALS:
					return "equals";
				case Constants.STARTS_WITH:
					return "startswith";
				case Constants.MORE_THAN:
					return "morethan";
				case Constants.LESS_THAN:
					return "lessthan";
				case Constants.BETWEEN:
					return "between";
				case Constants.EMPTY_SEARCH:
					return "empty";
				case Constants.NOT_CONTAINS:
					return "notcontain";
				case Constants.NOT_EQUALS:
					return "notequal";
				case Constants.NOT_STARTS_WITH:
					return "notstartwith";
				case Constants.NOT_MORE_THAN:
					return "notmorethan";
				case Constants.NOT_LESS_THAN:
					return "notlessthan";
				case Constants.NOT_BETWEEN:
					return "notbetween";
				case Constants.NOT_EMPTY:
					return "notempty";
				default:
					return "contains";
			}
			return null;
		}
		public virtual XVar setSearchOption(dynamic _param_field, dynamic _param_opt, dynamic _param_id = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar();
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic opt = XVar.Clone(_param_opt);
			dynamic id = XVar.Clone(_param_id);
			#endregion

			dynamic srchFields = XVar.Array();
			if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(field), (XVar)(this.searchFieldsArr)))))
			{
				return -1;
			}
			srchFields = this._where["_srchFields"];
			if((XVar)((XVar)(this.isShowSimpleSrchOpt)  && (XVar)((XVar)(!(XVar)(MVCFunctions.strlen((XVar)(this._where["simpleSrchFieldsComboOpt"]))))  || (XVar)(this._where["simpleSrchFieldsComboOpt"] == field)))  && (XVar)((XVar)(id == Constants.SEARCHID_SIMPLE)  || (XVar)((XVar)(!(XVar)(MVCFunctions.count(srchFields)))  && (XVar)(id == null))))
			{
				dynamic simpleSrchOption = null;
				this._where.InitAndSetArrayItem(field, "simpleSrchFieldsComboOpt");
				simpleSrchOption = XVar.Clone(this._where.InitAndSetArrayItem(opt, "simpleSrchTypeComboOpt"));
				return Constants.SEARCHID_SIMPLE;
			}
			opt = XVar.Clone(convertUserSearchOption((XVar)(opt)));
			foreach (KeyValuePair<XVar, dynamic> srchF in srchFields.GetEnumerator())
			{
				if((XVar)(srchF.Value["fName"] == field)  && (XVar)((XVar)(id == Constants.SEARCHID_PANEL + srchF.Key)  || (XVar)(id == null)))
				{
					if(XVar.Pack(this.searchOptions.KeyExists(opt)))
					{
						srchFields.InitAndSetArrayItem(this.searchOptions[opt]["not"], srchF.Key, "not");
						srchFields.InitAndSetArrayItem(this.searchOptions[opt]["option"], srchF.Key, "opt");
					}
					return Constants.SEARCHID_PANEL + srchF.Key;
				}
			}
			return null;
		}
		public virtual XVar addFieldValue(dynamic _param_field, dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic value = XVar.Clone(_param_value);
			#endregion

			return addSPControlData((XVar)(field), (XVar)(value), new XVar(""), new XVar(""));
		}
		public virtual XVar setAllFieldsSearchValue(dynamic _param_value)
		{
			#region pass-by-value parameters
			dynamic value = XVar.Clone(_param_value);
			#endregion

			if((XVar)(!(XVar)(this.haveAggregateFields))  || (XVar)(!(XVar)(this.advancedSearchActive)))
			{
				this._where.InitAndSetArrayItem(value, "_simpleSrch");
				this._where.InitAndSetArrayItem((XVar.Pack(GlobalVars.suggestAllContent) ? XVar.Pack("Contains") : XVar.Pack("Starts with")), "simpleSrchTypeComboOpt");
				this._where.InitAndSetArrayItem(false, "simpleSrchTypeComboNot");
			}
			return null;
		}
		public virtual XVar setSearchSQL(dynamic _param_field, dynamic _param_sqlCondition)
		{
			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic sqlCondition = XVar.Clone(_param_sqlCondition);
			#endregion

			if(XVar.Pack(MVCFunctions.in_array((XVar)(field), (XVar)(this.searchFieldsArr))))
			{
				this.customFieldSQLConditions.InitAndSetArrayItem(sqlCondition, field);
			}
			return null;
		}
		public static XVar getSearchObject(dynamic _param_tName = null, dynamic _param_dashTName = null, dynamic _param_sessionPrefix = null, dynamic _param_cipherer = null, dynamic _param_searchSavingEnabled = null, dynamic _param_pSet_packed = null)
		{
			#region packeted values
			ProjectSettings _param_pSet = XVar.UnPackProjectSettings(_param_pSet_packed);
			#endregion

			#region default values
			if(_param_tName as Object == null) _param_tName = new XVar("");
			if(_param_dashTName as Object == null) _param_dashTName = new XVar("");
			if(_param_sessionPrefix as Object == null) _param_sessionPrefix = new XVar("");
			if(_param_cipherer as Object == null) _param_cipherer = new XVar();
			if(_param_searchSavingEnabled as Object == null) _param_searchSavingEnabled = new XVar(false);
			if(_param_pSet as Object == null) _param_pSet = null;
			#endregion

			#region pass-by-value parameters
			dynamic tName = XVar.Clone(_param_tName);
			dynamic dashTName = XVar.Clone(_param_dashTName);
			dynamic sessionPrefix = XVar.Clone(_param_sessionPrefix);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic searchSavingEnabled = XVar.Clone(_param_searchSavingEnabled);
			ProjectSettings pSet = XVar.Clone(_param_pSet);
			#endregion

			dynamic searchClauseObj = null;
			if(XVar.Pack(!(XVar)(tName)))
			{
				tName = XVar.Clone(GlobalVars.strTableName);
			}
			if(XVar.Pack(!(XVar)(sessionPrefix)))
			{
				sessionPrefix = XVar.Clone((XVar.Pack(dashTName) ? XVar.Pack(MVCFunctions.Concat(dashTName, "_", tName)) : XVar.Pack(tName)));
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.postvalue(new XVar("saveSearch")))))
			{
				searchSavingEnabled = new XVar(true);
			}
			if(XVar.Pack(GlobalVars._cachedSeachClauses[sessionPrefix]))
			{
				return GlobalVars._cachedSeachClauses[sessionPrefix];
			}
			if(tName != GlobalVars.strTableName)
			{
				dynamic currentSearchClause = null;
				currentSearchClause = XVar.Clone(getSearchObject((XVar)(GlobalVars.strTableName)));
			}
			if(XVar.Pack(XSession.Session.KeyExists(MVCFunctions.Concat(sessionPrefix, "_advsearch"))))
			{
				searchClauseObj = XVar.Clone(UnserializeObject((XVar)(XSession.Session[MVCFunctions.Concat(sessionPrefix, "_advsearch")])));
			}
			else
			{
				dynamic var_params = XVar.Array();
				var_params = XVar.Clone(XVar.Array());
				var_params.InitAndSetArrayItem(tName, "tName");
				var_params.InitAndSetArrayItem(dashTName, "dashTName");
				var_params.InitAndSetArrayItem(sessionPrefix, "sessionPrefix");
				var_params.InitAndSetArrayItem(searchSavingEnabled, "searchSavingEnabled");
				if(XVar.Pack(cipherer))
				{
					var_params.InitAndSetArrayItem(cipherer, "cipherer");
				}
				if(XVar.Pack(!(XVar)(pSet)))
				{
					pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(tName)));
				}
				var_params.InitAndSetArrayItem(0 < MVCFunctions.count(pSet.getListOfFieldsByExprType(new XVar(true))), "haveAggregateFields");
				searchClauseObj = XVar.Clone(new SearchClause((XVar)(var_params)));
			}
			searchClauseObj.parseRequest();
			GlobalVars._cachedSeachClauses.InitAndSetArrayItem(searchClauseObj, sessionPrefix);
			return searchClauseObj;
		}
	}
}
