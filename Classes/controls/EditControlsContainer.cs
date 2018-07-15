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
	public partial class EditControlsContainer : XClass
	{
		public dynamic controls = XVar.Array();
		public dynamic jsSettings = XVar.Array();
		public ProjectSettings pSetEdit = null;
		public dynamic pageType = XVar.Pack("");
		public dynamic cipherer = XVar.Pack(null);
		public dynamic tName = XVar.Pack("");
		public dynamic pageObject = XVar.Pack(null);
		public dynamic pageAddLikeInline = XVar.Pack(false);
		public dynamic pageEditLikeInline = XVar.Pack(false);
		public dynamic tableBasedSearchPanelAdded = XVar.Pack(false);
		public dynamic searchPanelActivated = XVar.Pack(false);
		public dynamic globalVals = XVar.Array();
		protected dynamic connection;
		public dynamic classNamesForEdit = XVar.Array();
		public dynamic classNamesForSearch = XVar.Array();
		public EditControlsContainer(dynamic _param_pageObject, dynamic _param_pSetEdit_packed, dynamic _param_pageType, dynamic _param_cipherer = null)
		{
			#region packeted values
			ProjectSettings _param_pSetEdit = XVar.UnPackProjectSettings(_param_pSetEdit_packed);
			#endregion

			#region default values
			if(_param_cipherer as Object == null) _param_cipherer = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic pageObject = XVar.Clone(_param_pageObject);
			ProjectSettings pSetEdit = XVar.Clone(_param_pSetEdit);
			dynamic pageType = XVar.Clone(_param_pageType);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			#endregion

			if(pageObject != null)
			{
				this.pageObject = XVar.Clone(pageObject);
				this.pageAddLikeInline = XVar.Clone((XVar)(pageObject.pageType == Constants.PAGE_ADD)  && (XVar)(pageObject.mode == Constants.ADD_INLINE));
				this.pageEditLikeInline = XVar.Clone((XVar)(pageObject.pageType == Constants.PAGE_EDIT)  && (XVar)(pageObject.mode == Constants.EDIT_INLINE));
				this.tName = XVar.Clone(pageObject.tName);
			}
			else
			{
				this.tName = XVar.Clone(pSetEdit._table);
				this.cipherer = XVar.Clone(cipherer);
			}
			fillControlClassNames();
			setEditControlsConnection();
			this.pSetEdit = XVar.UnPackProjectSettings(pSetEdit);
			this.pageType = XVar.Clone(pageType);
			this.searchPanelActivated = new XVar(true);
		}
		protected virtual XVar setEditControlsConnection()
		{
			if(this.pageObject != null)
			{
				this.connection = XVar.Clone(this.pageObject.connection);
			}
			else
			{
				this.connection = XVar.Clone(GlobalVars.cman.byTable((XVar)(this.tName)));
			}
			return null;
		}
		public virtual XVar isSearchPanelActivated()
		{
			if(this.pageObject != null)
			{
				return this.pageObject.isSearchPanelActivated();
			}
			return this.pageType == Constants.PAGE_SEARCH;
		}
		public virtual XVar addControlsJSAndCSS()
		{
			dynamic appearOnPageFunc = null, fields = XVar.Array(), getEditFieldsFunc = null, pageTypeStr = null, pageTypes = null, searchFields = null, searchPanelActivated = null;
			pageTypes = XVar.Clone(XVar.Array());
			switch(((XVar)this.pageType).ToString())
			{
				case Constants.PAGE_ADD:
					pageTypeStr = new XVar("Add");
					break;
				case Constants.PAGE_EDIT:
					pageTypeStr = new XVar("Edit");
					break;
				case Constants.PAGE_VIEW:
				case Constants.PAGE_LIST:
				case Constants.PAGE_SEARCH:
					pageTypeStr = new XVar("List");
					break;
				case Constants.PAGE_REGISTER:
					pageTypeStr = new XVar("RegisterOrSearch");
					break;
				default:
					pageTypeStr = new XVar("");
					break;
			}
			searchPanelActivated = XVar.Clone(isSearchPanelActivated());
			if((XVar)(pageTypeStr == XVar.Pack(""))  && (XVar)(!(XVar)(searchPanelActivated)))
			{
				return null;
			}
			if((XVar)(pageTypeStr != XVar.Pack(""))  && (XVar)(this.pageType != Constants.PAGE_SEARCH))
			{
				getEditFieldsFunc = XVar.Clone(MVCFunctions.Concat("get", (XVar.Pack((XVar)(this.pageAddLikeInline)  || (XVar)(this.pageEditLikeInline)) ? XVar.Pack("Inline") : XVar.Pack("")), pageTypeStr, "Fields"));
				if((XVar)(this.pageAddLikeInline)  || (XVar)(this.pageEditLikeInline))
				{
					appearOnPageFunc = XVar.Clone(MVCFunctions.Concat("appearOnInline", pageTypeStr));
				}
				else
				{
					appearOnPageFunc = XVar.Clone(MVCFunctions.Concat("appearOn", pageTypeStr, "Page"));
				}
			}
			switch(((XVar)this.pageType).ToString())
			{
				case Constants.PAGE_REGISTER:
					fields = XVar.Clone(this.pSetEdit.getFieldsForRegister());
					break;
				case Constants.PAGE_SEARCH:
					fields = XVar.Clone(this.pSetEdit.getAdvSearchFields());
					break;
				default:
					fields = XVar.Clone(XVar.Array());
					if(XVar.Pack(getEditFieldsFunc))
					{
						fields = XVar.Clone(this.pSetEdit.Invoke(getEditFieldsFunc));
					}
					break;
			}
			searchFields = XVar.Clone(XVar.Array());
			if(XVar.Pack(searchPanelActivated))
			{
				searchFields = XVar.Clone(this.pSetEdit.getPanelSearchFields());
				searchFields = XVar.Clone(MVCFunctions.array_merge((XVar)(searchFields), (XVar)(this.pSetEdit.getAllSearchFields())));
				searchFields = XVar.Clone(MVCFunctions.array_unique((XVar)(searchFields)));
				fields = XVar.Clone(MVCFunctions.array_merge((XVar)(searchFields), (XVar)(fields)));
				fields = XVar.Clone(MVCFunctions.array_unique((XVar)(fields)));
			}
			foreach (KeyValuePair<XVar, dynamic> f in fields.GetEnumerator())
			{
				dynamic appear = null;
				appear = new XVar(false);
				if((XVar)((XVar)(this.pageType == Constants.PAGE_REGISTER)  || (XVar)(this.pageType == Constants.PAGE_SEARCH))  || (XVar)(MVCFunctions.in_array((XVar)(f.Value), (XVar)(searchFields))))
				{
					appear = new XVar(true);
				}
				else
				{
					if(XVar.Pack(appearOnPageFunc))
					{
						appear = XVar.Clone(this.pSetEdit.Invoke(appearOnPageFunc, (XVar)(f.Value)));
					}
				}
				if(XVar.Pack(appear))
				{
					getControl((XVar)(f.Value)).addJSFiles();
					getControl((XVar)(f.Value)).addCSSFiles();
				}
			}
			return null;
		}
		public virtual EditControl getControl(dynamic _param_field, dynamic _param_id = null, dynamic _param_extraParmas = null)
		{
			#region default values
			if(_param_id as Object == null) _param_id = new XVar("");
			if(_param_extraParmas as Object == null) _param_extraParmas = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic field = XVar.Clone(_param_field);
			dynamic id = XVar.Clone(_param_id);
			dynamic extraParmas = XVar.Clone(_param_extraParmas);
			#endregion

			dynamic className = null, ctrl = null;
			if((XVar)(MVCFunctions.count(extraParmas))  && (XVar)(extraParmas["getDetKeyReadOnlyCtrl"]))
			{
				className = XVar.Clone(this.classNamesForEdit[Constants.EDIT_FORMAT_READONLY]);
				ctrl = XVar.Clone(MVCFunctions.createControlClass((XVar)(className), (XVar)(field), (XVar)((XVar.Pack(this.pageObject != null) ? XVar.Pack(this.pageObject) : this)), (XVar)(id), (XVar)(this.connection)));
				ctrl.container = XVar.Clone(this);
				return XVar.UnPackEditControl(ctrl ?? new XVar());
			}
			if((XVar)(MVCFunctions.count(extraParmas))  && (XVar)(extraParmas["getConrirmFieldCtrl"]))
			{
				className = XVar.Clone(this.classNamesForEdit[Constants.EDIT_FORMAT_PASSWORD]);
				ctrl = XVar.Clone(MVCFunctions.createControlClass((XVar)(className), (XVar)(field), (XVar)((XVar.Pack(this.pageObject != null) ? XVar.Pack(this.pageObject) : this)), (XVar)(id), (XVar)(this.connection)));
				if(XVar.Pack(extraParmas["isConfirm"]))
				{
					ctrl.field = XVar.Clone(CommonFunctions.GetPasswordField());
				}
				ctrl.container = XVar.Clone(this);
				return XVar.UnPackEditControl(ctrl ?? new XVar());
			}
			if(XVar.Pack(!(XVar)(this.controls.KeyExists(field))))
			{
				dynamic editFormat = null, userControl = null;
				userControl = new XVar(false);
				editFormat = XVar.Clone(this.pSetEdit.getEditFormat((XVar)(field)));
				if((XVar)(editFormat == Constants.EDIT_FORMAT_TEXT_FIELD)  && (XVar)(CommonFunctions.IsDateFieldType((XVar)(this.pSetEdit.getFieldType((XVar)(field))))))
				{
					editFormat = new XVar(Constants.EDIT_FORMAT_DATE);
				}
				if((XVar)(this.pageType == Constants.PAGE_SEARCH)  || (XVar)(this.pageType == Constants.PAGE_LIST))
				{
					dynamic pageTypebyLookupFormat = null;
					pageTypebyLookupFormat = XVar.Clone(this.pSetEdit.getPageTypeByFieldEditFormat((XVar)(field), new XVar(Constants.EDIT_FORMAT_LOOKUP_WIZARD)));
					if((XVar)(editFormat == Constants.EDIT_FORMAT_TEXT_FIELD)  && (XVar)(pageTypebyLookupFormat != XVar.Pack("")))
					{
						dynamic localPSet = null;
						localPSet = XVar.Clone(new ProjectSettings((XVar)(this.pSetEdit._table), (XVar)(pageTypebyLookupFormat)));
						if(localPSet.getLinkField((XVar)(field)) != localPSet.getDisplayField((XVar)(field)))
						{
							className = new XVar("LookupTextField");
						}
						else
						{
							className = XVar.Clone(this.classNamesForSearch[editFormat]);
						}
					}
					else
					{
						className = XVar.Clone(this.classNamesForSearch[editFormat]);
					}
				}
				else
				{
					className = XVar.Clone(this.classNamesForEdit[editFormat]);
				}
				if((XVar)(className == this.classNamesForEdit[Constants.EDIT_FORMAT_FILE])  && (XVar)(this.pSetEdit.isBasicUploadUsed((XVar)(field))))
				{
					className = new XVar("FileFieldSingle");
				}
				if(XVar.Pack(!(XVar)(className)))
				{
					if(editFormat != XVar.Pack(""))
					{
						className = XVar.Clone(MVCFunctions.Concat("Edit", editFormat));
						userControl = new XVar(true);
						if(XVar.Pack(!(XVar)(this.pageObject == null)))
						{
							this.pageObject.AddJSFile((XVar)(MVCFunctions.Concat("include/runnerJS/controls/", className, ".js")), new XVar("include/runnerJS/editControls/Control.js"));
						}
					}
					else
					{
						className = XVar.Clone(this.classNamesForEdit[Constants.EDIT_FORMAT_TEXT_FIELD]);
					}
				}
				this.controls.InitAndSetArrayItem(MVCFunctions.createControlClass((XVar)(className), (XVar)(field), (XVar)((XVar.Pack(this.pageObject != null) ? XVar.Pack(this.pageObject) : this)), (XVar)(id), (XVar)(this.connection)), field);
				this.controls[field].container = XVar.Clone(this);
				if(XVar.Pack(userControl))
				{
					this.controls[field].format = XVar.Clone(className);
					this.controls[field].initUserControl();
				}
			}
			if(!XVar.Equals(XVar.Pack(id), XVar.Pack("")))
			{
				this.controls[field].setID((XVar)(id));
			}
			return XVar.UnPackEditControl(this.controls[field] ?? new XVar());
		}
		public virtual XVar isSystemControl(dynamic _param_className)
		{
			#region pass-by-value parameters
			dynamic className = XVar.Clone(_param_className);
			#endregion

			if((XVar)(this.pageType == Constants.PAGE_SEARCH)  || (XVar)(this.pageType == Constants.PAGE_LIST))
			{
				return this.classNamesForSearch.KeyExists(className);
			}
			else
			{
				return this.classNamesForEdit.KeyExists(className);
			}
			return null;
		}
		public virtual XVar isPageTableBased()
		{
			if((XVar)((XVar)((XVar)(this.pageType == Constants.PAGE_MENU)  || (XVar)(this.pageType == Constants.PAGE_LOGIN))  || (XVar)(this.pageType == Constants.PAGE_REMIND))  || (XVar)(this.pageType == Constants.PAGE_CHANGEPASS))
			{
				return false;
			}
			return true;
		}
		public virtual XVar mobileTemplateMode()
		{
			return false;
		}
		protected virtual XVar fillControlClassNames()
		{
			this.classNamesForEdit.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_TEXT_FIELD);
			this.classNamesForEdit.InitAndSetArrayItem("TimeField", Constants.EDIT_FORMAT_TIME);
			this.classNamesForEdit.InitAndSetArrayItem("TextAreaField", Constants.EDIT_FORMAT_TEXT_AREA);
			this.classNamesForEdit.InitAndSetArrayItem("PasswordField", Constants.EDIT_FORMAT_PASSWORD);
			this.classNamesForEdit.InitAndSetArrayItem("DateField", Constants.EDIT_FORMAT_DATE);
			this.classNamesForEdit.InitAndSetArrayItem("CheckboxField", Constants.EDIT_FORMAT_CHECKBOX);
			this.classNamesForEdit.InitAndSetArrayItem("DatabaseFileField", Constants.EDIT_FORMAT_DATABASE_IMAGE);
			this.classNamesForEdit.InitAndSetArrayItem("DatabaseFileField", Constants.EDIT_FORMAT_DATABASE_FILE);
			this.classNamesForEdit.InitAndSetArrayItem("HiddenField", Constants.EDIT_FORMAT_HIDDEN);
			this.classNamesForEdit.InitAndSetArrayItem("ReadOnlyField", Constants.EDIT_FORMAT_READONLY);
			this.classNamesForEdit.InitAndSetArrayItem("FileField", Constants.EDIT_FORMAT_FILE);
			this.classNamesForEdit.InitAndSetArrayItem("LookupField", Constants.EDIT_FORMAT_LOOKUP_WIZARD);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_TEXT_FIELD);
			this.classNamesForSearch.InitAndSetArrayItem("TimeField", Constants.EDIT_FORMAT_TIME);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_TEXT_AREA);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_PASSWORD);
			this.classNamesForSearch.InitAndSetArrayItem("DateField", Constants.EDIT_FORMAT_DATE);
			this.classNamesForSearch.InitAndSetArrayItem("CheckboxField", Constants.EDIT_FORMAT_CHECKBOX);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_DATABASE_IMAGE);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_DATABASE_FILE);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_HIDDEN);
			this.classNamesForSearch.InitAndSetArrayItem("TextField", Constants.EDIT_FORMAT_READONLY);
			this.classNamesForSearch.InitAndSetArrayItem("FileField", Constants.EDIT_FORMAT_FILE);
			this.classNamesForSearch.InitAndSetArrayItem("LookupField", Constants.EDIT_FORMAT_LOOKUP_WIZARD);
			return null;
		}
	}
}
