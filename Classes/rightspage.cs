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
	public partial class RightsPage : ListPage
	{
		public dynamic tables = XVar.Array();
		public dynamic pageMasks = XVar.Array();
		public dynamic rights = XVar.Array();
		public dynamic groups = XVar.Array();
		public dynamic smartyGroups = XVar.Array();
		public dynamic cbxNames;
		public dynamic permissionNames = XVar.Array();
		public dynamic sortedTables;
		public dynamic menuOrderedTables;
		public dynamic alphaOrderedTables;
		protected static bool skipRightsPageCtor = false;
		private bool skipListPageCtorSurrogate = new Func<bool>(() => skipListPageCtor = true).Invoke();
		public RightsPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipRightsPageCtor)
			{
				skipRightsPageCtor = false;
				return;
			}
			this.permissionNames.InitAndSetArrayItem(true, "A");
			this.permissionNames.InitAndSetArrayItem(true, "D");
			this.permissionNames.InitAndSetArrayItem(true, "E");
			this.permissionNames.InitAndSetArrayItem(true, "S");
			this.permissionNames.InitAndSetArrayItem(true, "P");
			this.permissionNames.InitAndSetArrayItem(true, "I");
			this.permissionNames.InitAndSetArrayItem(true, "M");
			this.cbxNames = XVar.Clone(new XVar("add", new XVar("mask", "A", "rightName", "add"), "edt", new XVar("mask", "E", "rightName", "edit"), "del", new XVar("mask", "D", "rightName", "delete"), "lst", new XVar("mask", "S", "rightName", "list"), "exp", new XVar("mask", "P", "rightName", "export"), "imp", new XVar("mask", "I", "rightName", "import"), "adm", new XVar("mask", "M")));
			initLogin();
			setLangParams();
			sortTables();
			fillGroupsArr();
		}
		public virtual XVar fillGroupsArr()
		{
			dynamic grConnection = null, qResult = null, sql = null, tdata = XVar.Array();
			grConnection = XVar.Clone(GlobalVars.cman.getForUserGroups());
			this.groups.InitAndSetArrayItem(MVCFunctions.Concat("<", "Admin", ">"), -1);
			this.groups.InitAndSetArrayItem(MVCFunctions.Concat("<", "Default", ">"), -2);
			this.groups.InitAndSetArrayItem(MVCFunctions.Concat("<", "Guest", ">"), -3);
			sql = XVar.Clone(MVCFunctions.Concat("select ", grConnection.addFieldWrappers(new XVar("")), ", ", grConnection.addFieldWrappers(new XVar("")), " from ", grConnection.addTableWrappers(new XVar("uggroups")), " order by ", grConnection.addFieldWrappers(new XVar(""))));
			qResult = XVar.Clone(grConnection.query((XVar)(sql)));
			while(XVar.Pack(tdata = XVar.Clone(qResult.fetchNumeric())))
			{
				this.groups.InitAndSetArrayItem(tdata[1], tdata[0]);
			}
			return null;
		}
		public virtual XVar fillSmartyAndRights()
		{
			dynamic first = null;
			first = new XVar(true);
			foreach (KeyValuePair<XVar, dynamic> name in this.groups.GetEnumerator())
			{
				dynamic sg = XVar.Array();
				sg = XVar.Clone(XVar.Array());
				sg.InitAndSetArrayItem(MVCFunctions.Concat("value=\"", name.Key, "\""), "group_attrs");
				if(XVar.Pack(first))
				{
					sg.InitAndSetArrayItem("active", "group_class");
					first = new XVar(false);
				}
				sg.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(name.Value)), "groupname");
				this.smartyGroups.InitAndSetArrayItem(sg, null);
			}
			return null;
		}
		public virtual XVar getRights()
		{
			dynamic group = null, mask = null, qResult = null, sql = null, table = null, tdata = XVar.Array();
			sql = XVar.Clone(MVCFunctions.Concat("select ", this.connection.addFieldWrappers(new XVar("")), ", ", this.connection.addFieldWrappers(new XVar("")), ", ", this.connection.addFieldWrappers(new XVar("")), " from ", this.connection.addTableWrappers(new XVar("ugrights")), " order by ", this.connection.addFieldWrappers(new XVar(""))));
			qResult = XVar.Clone(this.connection.query((XVar)(sql)));
			while(XVar.Pack(tdata = XVar.Clone(qResult.fetchNumeric())))
			{
				group = XVar.Clone(tdata[0]);
				table = XVar.Clone(tdata[1]);
				mask = XVar.Clone(tdata[2]);
				if(XVar.Pack(!(XVar)(this.tables.KeyExists(table))))
				{
					continue;
				}
				if(XVar.Pack(!(XVar)(this.groups.KeyExists(group))))
				{
					continue;
				}
				if(XVar.Pack(!(XVar)(this.rights.KeyExists(table))))
				{
					this.rights.InitAndSetArrayItem(XVar.Array(), table);
				}
				this.rights.InitAndSetArrayItem(fixMask((XVar)(mask), (XVar)(this.pageMasks[table])), table, group);
			}
			return null;
		}
		public virtual XVar addJsGroupsAndRights()
		{
			this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "warnOnLeaving");
			this.jsSettings.InitAndSetArrayItem(this.rights, "tableSettings", this.tName, "rights");
			this.jsSettings.InitAndSetArrayItem(this.groups, "tableSettings", this.tName, "groups");
			this.jsSettings.InitAndSetArrayItem(this.tables, "tableSettings", this.tName, "tables");
			this.jsSettings.InitAndSetArrayItem(this.pageMasks, "tableSettings", this.tName, "pageMasks");
			this.jsSettings.InitAndSetArrayItem(this.menuOrderedTables, "tableSettings", this.tName, "menuOrderedTables");
			this.jsSettings.InitAndSetArrayItem(this.alphaOrderedTables, "tableSettings", this.tName, "alphaOrderedTables");
			return null;
		}
		public override XVar commonAssign()
		{
			this.xt.assign_loopsection(new XVar("groups"), (XVar)(this.smartyGroups));
			base.commonAssign();
			foreach (KeyValuePair<XVar, dynamic> t in this.permissionNames.GetEnumerator())
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(t.Key, "_headcheckbox")), (XVar)(MVCFunctions.Concat(" id=\"colbox", t.Key, "\" data-perm=\"", t.Key, "\"")));
			}
			this.xt.assign(new XVar("addgroup_attrs"), new XVar("id=\"addGroupBtn\""));
			this.xt.assign(new XVar("delgroup_attrs"), new XVar("id=\"delGroupBtn\""));
			this.xt.assign(new XVar("rengroup_attrs"), new XVar("id=\"renGroupBtn\""));
			this.xt.assign(new XVar("savegroup_attrs"), new XVar("id=\"saveGroupBtn\""));
			this.xt.assign(new XVar("savebutton_attrs"), new XVar("id=\"saveBtn\""));
			this.xt.assign(new XVar("resetbutton_attrs"), new XVar("id=\"resetBtn\""));
			this.xt.assign(new XVar("cancelgroup_attrs"), new XVar("id=\"cancelBtn\""));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.assign(new XVar("menu_block"), new XVar(true));
			this.xt.assign(new XVar("left_block"), new XVar(true));
			this.xt.assign(new XVar("rights_block"), new XVar(true));
			this.xt.assign(new XVar("message_block"), new XVar(true));
			this.xt.assign(new XVar("security_block"), new XVar(true));
			this.xt.assign(new XVar("logoutbutton"), (XVar)(CommonFunctions.isSingleSign()));
			this.xt.assign(new XVar("savebuttons_block"), new XVar(true));
			this.xt.assign(new XVar("search_records_block"), new XVar(true));
			this.xt.assign(new XVar("recordcontrols_block"), new XVar(true));
			this.xt.assign(new XVar("username"), (XVar)(XSession.Session["UserName"]));
			if(XVar.Pack(this.createLoginPage))
			{
				this.xt.assign(new XVar("userid"), (XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(XSession.Session["UserID"]))));
			}
			this.xt.displayBrickHidden(new XVar("message"));
			prepareBreadcrumbs(new XVar("adminarea"));
			return null;
		}
		public virtual XVar sortTables()
		{
			dynamic addedTables = XVar.Array(), allTables = null, arr = XVar.Array(), groupsMap = XVar.Array(), menu = XVar.Array();
			this.sortedTables = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> _tbl in this.tables.GetEnumerator())
			{
				this.sortedTables.InitAndSetArrayItem(new XVar(0, _tbl.Key, 1, _tbl.Value[1]), null);
			}
			MVCFunctions.usort((XVar)(this.sortedTables), new XVar("rightsSortFunc"));
			this.alphaOrderedTables = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> t in this.sortedTables.GetEnumerator())
			{
				this.alphaOrderedTables.InitAndSetArrayItem(t.Value[0], null);
			}
			this.menuOrderedTables = XVar.Clone(XVar.Array());
			menu = XVar.Clone(getMenuNodes());
			addedTables = XVar.Clone(XVar.Array());
			groupsMap = XVar.Clone(XVar.Array());
			allTables = XVar.Clone(CommonFunctions.GetTablesListWithoutSecurity());
			foreach (KeyValuePair<XVar, dynamic> m in menu.GetEnumerator())
			{
				arr = XVar.Clone(XVar.Array());
				if((XVar)(m.Value["pageType"] == "WebReports")  || (XVar)(m.Value["type"] == "Separator"))
				{
					continue;
				}
				if((XVar)((XVar)(m.Value["table"])  && (XVar)(!(XVar)(addedTables[m.Value["table"]])))  && (XVar)(!XVar.Equals(XVar.Pack(MVCFunctions.array_search((XVar)(m.Value["table"]), (XVar)(allTables))), XVar.Pack(false))))
				{
					addedTables.InitAndSetArrayItem(true, m.Value["table"]);
					arr.InitAndSetArrayItem(m.Value["table"], "table");
				}
				if(XVar.Pack(m.Value["parent"]))
				{
					arr.InitAndSetArrayItem(groupsMap[m.Value["parent"]], "parent");
					this.menuOrderedTables.InitAndSetArrayItem(MVCFunctions.count(this.menuOrderedTables), arr["parent"], "items", null);
				}
				if((XVar)(true)  || (XVar)(m.Value["type"] == "Group"))
				{
					groupsMap.InitAndSetArrayItem(MVCFunctions.count(this.menuOrderedTables), m.Value["id"]);
					arr.InitAndSetArrayItem(m.Value["title"], "title");
					arr.InitAndSetArrayItem(XVar.Array(), "items");
					arr.InitAndSetArrayItem(true, "collapsed");
				}
				this.menuOrderedTables.InitAndSetArrayItem(arr, null);
			}
			if(MVCFunctions.count(addedTables) < MVCFunctions.count(this.alphaOrderedTables))
			{
				dynamic unlistedId = null;
				unlistedId = XVar.Clone(MVCFunctions.count(this.menuOrderedTables));
				arr = XVar.Clone(XVar.Array());
				arr.InitAndSetArrayItem(true, "collapsed");
				arr.InitAndSetArrayItem("Unlisted tables", "title");
				arr.InitAndSetArrayItem(XVar.Array(), "items");
				this.menuOrderedTables.InitAndSetArrayItem(arr, null);
				foreach (KeyValuePair<XVar, dynamic> table in this.alphaOrderedTables.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(addedTables[table.Value])))
					{
						this.menuOrderedTables.InitAndSetArrayItem(MVCFunctions.count(this.menuOrderedTables), unlistedId, "items", null);
						this.menuOrderedTables.InitAndSetArrayItem(new XVar("table", table.Value, "parent", unlistedId), null);
					}
				}
			}
			return null;
		}
		public virtual XVar getItemsCount(dynamic _param_itemIdx)
		{
			#region pass-by-value parameters
			dynamic itemIdx = XVar.Clone(_param_itemIdx);
			#endregion

			dynamic count = null;
			count = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> idx in this.menuOrderedTables[itemIdx]["items"].GetEnumerator())
			{
				if(XVar.Pack(this.menuOrderedTables[idx.Value].KeyExists("items")))
				{
					count += getItemsCount((XVar)(idx.Value));
				}
				if(XVar.Pack(this.menuOrderedTables[idx.Value].KeyExists("table")))
				{
					count++;
				}
			}
			return count;
		}
		public virtual XVar fillTablesGrid(dynamic rowInfoArr)
		{
			dynamic copylink = null, editlink = null, parentStack = XVar.Array(), recNo = null, rowClass = null;
			rowClass = new XVar(false);
			recNo = new XVar(1);
			editlink = new XVar("");
			copylink = new XVar("");
			parentStack = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> _tbl in this.menuOrderedTables.GetEnumerator())
			{
				dynamic childrenCount = null, parent = null, row = XVar.Array(), table = null;
				table = XVar.Clone(_tbl.Value["table"]);
				parent = XVar.Clone(_tbl.Value["parent"]);
				if(XVar.Pack(MVCFunctions.strlen((XVar)(table))))
				{
					dynamic caption = null, mask = null, shortTable = null;
					caption = XVar.Clone(this.tables[table][1]);
					shortTable = XVar.Clone(this.tables[table][0]);
					row = XVar.Clone(XVar.Array());
					if(caption == table)
					{
						row.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(table)), "tablename");
					}
					else
					{
						row.InitAndSetArrayItem(MVCFunctions.Concat("<span dir='LTR'>", MVCFunctions.runner_htmlspecialchars((XVar)(caption)), "&nbsp;(", MVCFunctions.runner_htmlspecialchars((XVar)(table)), ")</span>"), "tablename");
					}
					row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"row_", shortTable, "\""), "tablerowattrs");
					row.InitAndSetArrayItem(MVCFunctions.Concat("id=\"rowbox", shortTable, "\" data-table=\"", shortTable, "\" data-checked=0"), "tablecheckbox_attrs");
					row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"tblcell", shortTable, "\""), "tbl_cell");
					mask = XVar.Clone(this.pageMasks[table]);
					foreach (KeyValuePair<XVar, dynamic> x in this.permissionNames.GetEnumerator())
					{
						if(XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(mask), (XVar)(x.Key))), XVar.Pack(false)))
						{
							continue;
						}
						row.InitAndSetArrayItem(true, MVCFunctions.Concat(x.Key, "_group"));
						row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"box", x.Key, shortTable, "\" data-checked=0"), MVCFunctions.Concat(x.Key, "_checkbox"));
						row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"cell", x.Key, shortTable, "\""), MVCFunctions.Concat(x.Key, "_cell"));
					}
				}
				else
				{
					dynamic title = null;
					title = XVar.Clone(_tbl.Value["title"]);
					row = XVar.Clone(XVar.Array());
					row.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(title)), "tablename");
					row.InitAndSetArrayItem(" data-checked=-2", "tablecheckbox_attrs");
					row.InitAndSetArrayItem(MVCFunctions.Concat(" id=\"grouprow_", _tbl.Key, "\""), "tablerowattrs");
				}
				if(XVar.Pack(!(XVar)(parent as object != null)))
				{
					parentStack = XVar.Clone(XVar.Array());
				}
				else
				{
					dynamic stackPos = null;
					stackPos = XVar.Clone(MVCFunctions.array_search((XVar)(parent), (XVar)(parentStack)));
					if(XVar.Equals(XVar.Pack(stackPos), XVar.Pack(false)))
					{
						parentStack.InitAndSetArrayItem(parent, null);
					}
					else
					{
						parentStack = XVar.Clone(MVCFunctions.array_slice((XVar)(parentStack), new XVar(0), (XVar)(stackPos + 1)));
					}
					row["tblrowclass"] = MVCFunctions.Concat(row["tblrowclass"], "rightsindent", MVCFunctions.count(parentStack));
				}
				childrenCount = XVar.Clone(getItemsCount((XVar)(_tbl.Key)));
				if((XVar)(_tbl.Value.KeyExists("items"))  && (XVar)(childrenCount))
				{
					row["tablename"] = MVCFunctions.Concat(row["tablename"], "<span class='tablecount' dir='LTR'>&nbsp;(", getItemsCount((XVar)(_tbl.Key)), ")</span>");
					row["tablerowattrs"] = MVCFunctions.Concat(row["tablerowattrs"], " data-groupid=\"", _tbl.Key, "\"");
					row.InitAndSetArrayItem(true, "groupControl");
					row.InitAndSetArrayItem(" data-state='closed'", "groupControlState");
					row.InitAndSetArrayItem(" data-state='closed'", "groupControlClass");
					row["tblrowclass"] = MVCFunctions.Concat(row["tblrowclass"], " menugroup");
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(table)))))
					{
						row["tblrowclass"] = MVCFunctions.Concat(row["tblrowclass"], " menugrouponly");
					}
				}
				else
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(table)))))
					{
						continue;
					}
				}
				if(XVar.Pack(parent))
				{
					row["tablerowattrs"] = MVCFunctions.Concat(row["tablerowattrs"], " style='display:none;'");
				}
				rowInfoArr.InitAndSetArrayItem(row, null);
			}
			return null;
		}
		public override XVar fillGridData()
		{
			dynamic rowInfo = null;
			rowInfo = XVar.Clone(XVar.Array());
			fillTablesGrid((XVar)(rowInfo));
			this.xt.assign_loopsection(new XVar("grid_row"), (XVar)(rowInfo));
			return null;
		}
		public override XVar setSessionVariables()
		{
			return null;
		}
		public override XVar prepareForBuildPage()
		{
			fillSmartyAndRights();
			getRights();
			fillGridData();
			addCommonJs();
			addCommonHtml();
			commonAssign();
			return null;
		}
		public override XVar showPage()
		{
			display((XVar)(this.templatefile));
			return null;
		}
		public override XVar addCommonHtml()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage((XVar)(this.isDisplayLoading)));
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			return null;
		}
		public override XVar prepareForResizeColumns()
		{
			return null;
		}
		public override XVar addCommonJs()
		{
			RunnerPage.addCommonJs(this);
			addJsGroupsAndRights();
			return null;
		}
		public virtual XVar fixMask(dynamic _param_mask, dynamic _param_possibleMask)
		{
			#region pass-by-value parameters
			dynamic mask = XVar.Clone(_param_mask);
			dynamic possibleMask = XVar.Clone(_param_possibleMask);
			#endregion

			dynamic i = null, l = null, outMask = null;
			outMask = new XVar("");
			l = XVar.Clone(MVCFunctions.strlen((XVar)(possibleMask)));
			i = new XVar(0);
			for(;i < l; ++(i))
			{
				if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(mask), (XVar)(possibleMask[i]))), XVar.Pack(false)))
				{
					outMask = MVCFunctions.Concat(outMask, possibleMask[i]);
				}
			}
			return outMask;
		}
		public virtual XVar saveRights(dynamic modifiedRights)
		{
			foreach (KeyValuePair<XVar, dynamic> rights in modifiedRights.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> mask in modifiedRights[rights.Key].GetEnumerator())
				{
					updateTablePermissions((XVar)(mask.Key), (XVar)(rights.Key), (XVar)(mask.Value));
				}
			}
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(new XVar("success", true))));
			return null;
		}
		public virtual XVar updateTablePermissions(dynamic _param_table, dynamic _param_group, dynamic _param_mask)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			dynamic group = XVar.Clone(_param_group);
			dynamic mask = XVar.Clone(_param_mask);
			#endregion

			dynamic accessMaskWFieldName = null, data = XVar.Array(), groupWhere = null, groupisWFieldName = null, rightWTableName = null, sql = null, tableNameWFieldName = null;
			rightWTableName = XVar.Clone(this.connection.addTableWrappers(new XVar("ugrights")));
			accessMaskWFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("")));
			groupisWFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("")));
			tableNameWFieldName = XVar.Clone(this.connection.addFieldWrappers(new XVar("")));
			groupWhere = XVar.Clone(MVCFunctions.Concat(groupisWFieldName, "=", group, " and ", tableNameWFieldName, "=", this.connection.prepareString((XVar)(table))));
			sql = XVar.Clone(MVCFunctions.Concat("select ", accessMaskWFieldName, " from ", rightWTableName, "where", groupWhere));
			data = XVar.Clone(this.connection.query((XVar)(sql)).fetchNumeric());
			if(XVar.Pack(data))
			{
				dynamic correctedMask = null, pageMask = null, savedMask = null;
				savedMask = XVar.Clone(data[0]);
				pageMask = XVar.Clone(this.pageMasks[table]);
				correctedMask = new XVar("");
				foreach (KeyValuePair<XVar, dynamic> t in this.permissionNames.GetEnumerator())
				{
					if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(pageMask), (XVar)(t.Key))), XVar.Pack(false)))
					{
						if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(mask), (XVar)(t.Key))), XVar.Pack(false)))
						{
							correctedMask = MVCFunctions.Concat(correctedMask, t.Key);
						}
					}
					else
					{
						if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(savedMask), (XVar)(t.Key))), XVar.Pack(false)))
						{
							correctedMask = MVCFunctions.Concat(correctedMask, t.Key);
						}
					}
				}
				mask = XVar.Clone(correctedMask);
				if(XVar.Pack(MVCFunctions.strlen((XVar)(mask))))
				{
					sql = XVar.Clone(MVCFunctions.Concat("update ", rightWTableName, " set ", accessMaskWFieldName, "='", mask, "',", tableNameWFieldName, "=", this.connection.prepareString((XVar)(table)), " where ", groupWhere));
				}
				else
				{
					sql = XVar.Clone(MVCFunctions.Concat("delete from ", rightWTableName, " where ", groupWhere));
				}
			}
			else
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(mask)))))
				{
					return null;
				}
				sql = XVar.Clone(MVCFunctions.Concat("insert into ", rightWTableName, " (", groupisWFieldName, ", ", tableNameWFieldName, ", ", accessMaskWFieldName, ")", " values (", group, ", ", this.connection.prepareString((XVar)(table)), ", '", mask, "')"));
			}
			this.connection.exec((XVar)(sql));
			return null;
		}
	}
	// Included file globals
	public partial class CommonFunctions
	{
		public static XVar rightsSortFunc(dynamic _param_a, dynamic _param_b)
		{
			#region pass-by-value parameters
			dynamic a = XVar.Clone(_param_a);
			dynamic b = XVar.Clone(_param_b);
			#endregion

			if(a[1] == b[1])
			{
				return 0;
			}
			if(a[1] < b[1])
			{
				return -1;
			}
			return 1;
		}
	}
}
