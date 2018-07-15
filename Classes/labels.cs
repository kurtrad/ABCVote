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
	public partial class Labels : XClass
	{
		public static XVar getLanguages()
		{
			dynamic languages = XVar.Array();
			languages = XVar.Clone(XVar.Array());
			languages.InitAndSetArrayItem("English", null);
			return languages;
		}
		private static XVar findLanguage(dynamic _param_lng)
		{
			#region pass-by-value parameters
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic languages = XVar.Array();
			languages = XVar.Clone(getLanguages());
			if(!XVar.Equals(XVar.Pack(MVCFunctions.array_search((XVar)(lng), (XVar)(languages))), XVar.Pack(false)))
			{
				return lng;
			}
			lng = XVar.Clone(MVCFunctions.strtoupper((XVar)(lng)));
			foreach (KeyValuePair<XVar, dynamic> l in languages.GetEnumerator())
			{
				if(MVCFunctions.strtoupper((XVar)(l.Value)) == lng)
				{
					return l.Value;
				}
			}
			return CommonFunctions.mlang_getcurrentlang();
		}
		private static XVar findTable(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			return table;
		}
		public static XVar getFieldLabel(dynamic _param_table, dynamic _param_field, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return "";
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			return GlobalVars.field_labels[MVCFunctions.GoodFieldName((XVar)(table))][lng][MVCFunctions.GoodFieldName((XVar)(field))];
		}
		public static XVar setFieldLabel(dynamic _param_table, dynamic _param_field, dynamic _param_str, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic str = XVar.Clone(_param_str);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return false;
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return false;
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			GlobalVars.field_labels.InitAndSetArrayItem(str, MVCFunctions.GoodFieldName((XVar)(table)), lng, MVCFunctions.GoodFieldName((XVar)(field)));
			return true;
		}
		public static XVar getTableCaption(dynamic _param_table, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			return GlobalVars.tableCaptions[lng][MVCFunctions.GoodFieldName((XVar)(table))];
		}
		public static XVar setTableCaption(dynamic _param_table, dynamic _param_str, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic str = XVar.Clone(_param_str);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return false;
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			GlobalVars.tableCaptions.InitAndSetArrayItem(str, lng, MVCFunctions.GoodFieldName((XVar)(table)));
			return true;
		}
		public static XVar getProjectLogo(dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			lng = XVar.Clone(findLanguage((XVar)(lng)));
			return GlobalVars.globalSettings["ProjectLogo"][lng];
		}
		public static XVar setProjectLogo(dynamic _param_str, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic str = XVar.Clone(_param_str);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			lng = XVar.Clone(findLanguage((XVar)(lng)));
			GlobalVars.globalSettings.InitAndSetArrayItem(str, "ProjectLogo", lng);
			return true;
		}
		public static XVar setFieldTooltip(dynamic _param_table, dynamic _param_field, dynamic _param_str, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic str = XVar.Clone(_param_str);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return false;
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return false;
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			GlobalVars.fieldToolTips.InitAndSetArrayItem(str, MVCFunctions.GoodFieldName((XVar)(table)), lng, MVCFunctions.GoodFieldName((XVar)(field)));
			return true;
		}
		public static XVar getFieldTooltip(dynamic _param_table, dynamic _param_field, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return "";
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			return GlobalVars.fieldToolTips[MVCFunctions.GoodFieldName((XVar)(table))][lng][MVCFunctions.GoodFieldName((XVar)(field))];
		}
		public static XVar setPageTitleTempl(dynamic _param_table, dynamic _param_page, dynamic _param_str, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic page = XVar.Clone(_param_page);
			dynamic str = XVar.Clone(_param_str);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return false;
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			GlobalVars.page_titles.InitAndSetArrayItem(str, MVCFunctions.GoodFieldName((XVar)(table)), lng, page);
			return true;
		}
		public static XVar getPageTitleTempl(dynamic _param_table, dynamic _param_page, dynamic _param_lng = null)
		{
			#region default values
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic page = XVar.Clone(_param_page);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null, templ = null;
			table = XVar.Clone(findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			templ = XVar.Clone(GlobalVars.page_titles[MVCFunctions.GoodFieldName((XVar)(table))][lng][page]);
			if(XVar.Pack(MVCFunctions.strlen((XVar)(templ))))
			{
				return templ;
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			return RunnerPage.getDefaultPageTitle((XVar)(page), (XVar)(MVCFunctions.GoodFieldName((XVar)(table))), (XVar)(ps));
		}
		public static XVar setBreadcrumbsLabelTempl(dynamic _param_table, dynamic _param_str, dynamic _param_master = null, dynamic _param_page = null, dynamic _param_lng = null)
		{
			#region default values
			if(_param_master as Object == null) _param_master = new XVar("");
			if(_param_page as Object == null) _param_page = new XVar("");
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic str = XVar.Clone(_param_str);
			dynamic master = XVar.Clone(_param_master);
			dynamic page = XVar.Clone(_param_page);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			table = XVar.Clone(findTable((XVar)(table)));
			if(XVar.Pack(!(XVar)(table)))
			{
				table = new XVar(".");
			}
			master = XVar.Clone(CommonFunctions.findTable((XVar)(master)));
			if(XVar.Pack(!(XVar)(master)))
			{
				master = new XVar(".");
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			if(page == XVar.Pack(""))
			{
				page = new XVar(".");
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels.KeyExists(lng))))
			{
				GlobalVars.breadcrumb_labels.InitAndSetArrayItem(XVar.Array(), lng);
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels[lng].KeyExists(table))))
			{
				GlobalVars.breadcrumb_labels.InitAndSetArrayItem(XVar.Array(), lng, table);
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels[lng][table].KeyExists(master))))
			{
				GlobalVars.breadcrumb_labels.InitAndSetArrayItem(XVar.Array(), lng, table, master);
			}
			GlobalVars.breadcrumb_labels.InitAndSetArrayItem(str, lng, table, master, page);
			return null;
		}
		public static XVar getBreadcrumbsLabelTempl(dynamic _param_table, dynamic _param_master = null, dynamic _param_page = null, dynamic _param_lng = null)
		{
			#region default values
			if(_param_master as Object == null) _param_master = new XVar("");
			if(_param_page as Object == null) _param_page = new XVar("");
			if(_param_lng as Object == null) _param_lng = new XVar("");
			#endregion

			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic master = XVar.Clone(_param_master);
			dynamic page = XVar.Clone(_param_page);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			table = XVar.Clone(findTable((XVar)(table)));
			if(XVar.Pack(!(XVar)(table)))
			{
				table = new XVar(".");
			}
			master = XVar.Clone(CommonFunctions.findTable((XVar)(master)));
			if(XVar.Pack(!(XVar)(master)))
			{
				master = new XVar(".");
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			if(page == XVar.Pack(""))
			{
				page = new XVar(".");
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels.KeyExists(lng))))
			{
				return "";
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels[lng].KeyExists(table))))
			{
				return "";
			}
			if(XVar.Pack(!(XVar)(GlobalVars.breadcrumb_labels[lng][table].KeyExists(master))))
			{
				return "";
			}
			return GlobalVars.breadcrumb_labels[lng][table][master][page];
		}
		public virtual XVar getPlaceholder(dynamic _param_table, dynamic _param_field, dynamic _param_lng)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic ps = null;
			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return "";
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return "";
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			return GlobalVars.placeHolders[MVCFunctions.GoodFieldName((XVar)(table))][lng][MVCFunctions.GoodFieldName((XVar)(field))];
		}
		public virtual XVar setPlaceholder(dynamic _param_table, dynamic _param_field, dynamic _param_placeHolder, dynamic _param_lng)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic field = XVar.Clone(_param_field);
			dynamic placeHolder = XVar.Clone(_param_placeHolder);
			dynamic lng = XVar.Clone(_param_lng);
			#endregion

			dynamic fName = null, ps = null, tName = null;
			table = XVar.Clone(CommonFunctions.findTable((XVar)(table)));
			if(table == XVar.Pack(""))
			{
				return false;
			}
			ps = XVar.Clone(new ProjectSettings((XVar)(table)));
			field = XVar.Clone(ps.findField((XVar)(field)));
			if(field == XVar.Pack(""))
			{
				return false;
			}
			lng = XVar.Clone(findLanguage((XVar)(lng)));
			tName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(table)));
			fName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(field)));
			if(XVar.Pack(!(XVar)(GlobalVars.placeHolders[tName])))
			{
				GlobalVars.placeHolders.InitAndSetArrayItem(XVar.Array(), tName);
			}
			if(XVar.Pack(!(XVar)(GlobalVars.placeHolders[tName][lng])))
			{
				GlobalVars.placeHolders.InitAndSetArrayItem(XVar.Array(), tName, lng);
			}
			GlobalVars.placeHolders.InitAndSetArrayItem(placeHolder, tName, lng, fName);
			return true;
		}
	}
}
