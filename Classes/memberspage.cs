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
	public partial class MembersPage : ListPage_Simple
	{
		public dynamic groups = XVar.Array();
		public dynamic groupFullChecked = XVar.Array();
		public dynamic members = XVar.Array();
		public dynamic users = XVar.Array();
		public dynamic addSaveButtons = XVar.Pack(false);
		public dynamic fields = XVar.Array();
		protected static bool skipMembersPageCtor = false;
		public MembersPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipMembersPageCtor)
			{
				skipMembersPageCtor = false;
				return;
			}


			this.listAjax = new XVar(false);
			this.pageSize = XVar.Clone(-1);
		}
		public override XVar commonAssign()
		{
			base.commonAssign();
			if(XVar.Pack(this.addSaveButtons))
			{
				this.xt.assign(new XVar("savebuttons_block"), new XVar(true));
				this.xt.assign(new XVar("savebutton_attrs"), new XVar("id=\"saveBtn\""));
				this.xt.assign(new XVar("resetbutton_attrs"), new XVar("id=\"resetBtn\""));
			}
			this.xt.assign(new XVar("search_records_block"), new XVar(true));
			initLogin();
			this.xt.displayBrickHidden(new XVar("message"));
			this.xt.assign(new XVar("menu_block"), new XVar(true));
			return null;
		}
		public override XVar fillGridData()
		{
			dynamic data = XVar.Array(), displayUserName = null, emailUser = null, groups_sate = XVar.Array(), i = null, member_indexes = XVar.Array(), row = XVar.Array(), rowInfo = XVar.Array(), rowgroups = XVar.Array(), smartyGroups = XVar.Array(), userid = null, username = null;
			rowInfo = XVar.Clone(XVar.Array());
			data = XVar.Clone(beforeProccessRow());
			while(XVar.Pack(data))
			{
				row = XVar.Clone(XVar.Array());
				userid = XVar.Clone(this.recNo);
				row.InitAndSetArrayItem(XVar.Array(), "grid_record");
				row.InitAndSetArrayItem(XVar.Array(), "grid_record", "data");
				username = XVar.Clone(data["username"]);
				groups_sate = XVar.Clone(XVar.Array());
				member_indexes = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> m in this.members.GetEnumerator())
				{
					if(m.Value[1] == username)
					{
						member_indexes.InitAndSetArrayItem(m.Key, null);
					}
				}
				rowgroups = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> g in this.groups.GetEnumerator())
				{
					dynamic smarty_group = XVar.Array(), var_checked = null;
					var_checked = new XVar(0);
					smarty_group = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> _i in member_indexes.GetEnumerator())
					{
						if(this.members[_i.Value][0] == g.Value[0])
						{
							var_checked = new XVar(1);
							break;
						}
					}
					if(XVar.Pack(!(XVar)((XVar)(XSession.Session["UserID"] != username)  || (XVar)(g.Value[0] != -1))))
					{
						var_checked = new XVar(3);
					}
					smarty_group.InitAndSetArrayItem(g.Value[0], "group");
					groups_sate.InitAndSetArrayItem(var_checked, smarty_group["group"]);
					smarty_group.InitAndSetArrayItem(MVCFunctions.Concat("data-checked=\"", var_checked, "\" id=\"box", smarty_group["group"], userid, "\" data-userid=\"", userid, "\" data-group=\"", smarty_group["group"], "\""), "groupbox_attrs");
					rowgroups.InitAndSetArrayItem(new XVar("usergroup_box", new XVar("data", new XVar(0, smarty_group)), "groupcellbox_attrs", MVCFunctions.Concat("id=\"cell", smarty_group["group"], userid, "\" data-col=\"", smarty_group["group"], "\"")), null);
				}
				rowgroups.InitAndSetArrayItem("rnr-edge", MVCFunctions.count(rowgroups) - 1, "rnredgeclass");
				row.InitAndSetArrayItem(new XVar("data", rowgroups), "usergroup_boxes");
				row.InitAndSetArrayItem(MVCFunctions.Concat("data-userid=\"userid\" id=\"cellusername", MVCFunctions.runner_htmlspecialchars((XVar)(userid)), "\""), "usernamecell_attrs");
				row.InitAndSetArrayItem(MVCFunctions.Concat("id=\"usernamerow", MVCFunctions.runner_htmlspecialchars((XVar)(userid)), "\""), "usernamerow_attrs");
				row.InitAndSetArrayItem(MVCFunctions.Concat("data-userid=\"", MVCFunctions.runner_htmlspecialchars((XVar)(userid)), "\" data-checked=\"0\" id=\"rowbox", MVCFunctions.runner_htmlspecialchars((XVar)(userid)), "\""), "usernamebox_attrs");
				row.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(username)), "username");
				emailUser = XVar.Clone(data[""]);
				row.InitAndSetArrayItem(MVCFunctions.runner_htmlspecialchars((XVar)(emailUser)), "emailuser");
				row.InitAndSetArrayItem(MVCFunctions.Concat("id=\"cellEmail", MVCFunctions.runner_htmlspecialchars((XVar)(userid)), "\""), "emailuser_attrs");
				this.users.InitAndSetArrayItem(emailUser, userid, "emailUser");
				this.users.InitAndSetArrayItem(username, userid, "userName");
				this.users.InitAndSetArrayItem(groups_sate, userid, "groups");
				this.users.InitAndSetArrayItem(true, userid, "visible");
				row.InitAndSetArrayItem(this.recNo, "recNo");
				this.recNo++;
				row.InitAndSetArrayItem(true, "grid_rowspace");
				row.InitAndSetArrayItem(new XVar("data", XVar.Array()), "grid_recordspace");
				i = new XVar(0);
				for(;i < this.colsOnPage * 2 - 1; i++)
				{
					row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
				}
				if(XVar.Pack(eventExists(new XVar("BeforeMoveNextList"))))
				{
					dynamic record = null;
					this.eventsObject.BeforeMoveNextList((XVar)(data), (XVar)(row), (XVar)(record), this);
				}
				rowInfo.InitAndSetArrayItem(row, null);
				data = XVar.Clone(beforeProccessRow());
			}
			foreach (KeyValuePair<XVar, dynamic> g in this.groups.GetEnumerator())
			{
				smartyGroups.InitAndSetArrayItem(new XVar("groupname", MVCFunctions.runner_htmlspecialchars((XVar)(g.Value[1])), "groupheadersort_attrs", MVCFunctions.Concat("data-group=\"", g.Value[0], "\" id=\"colsort", g.Value[0], "\" href=\"#\""), "groupheadertdsort_attrs", MVCFunctions.Concat("id=\"tdsort", g.Value[0], "\""), "groupheaderbox_attrs", MVCFunctions.Concat("data-group=\"", g.Value[0], "\" data-checked=\"0\" id=\"colbox", g.Value[0], "\""), "groupheadertdbox_attrs", MVCFunctions.Concat("id=\"tdbox", g.Value[0], "\"")), null);
			}
			this.xt.assign(new XVar("displayuserheadersort_attrs"), new XVar("id=\"displayNameSort\" href=\"#\""));
			this.xt.assign(new XVar("displayuserheadertdsort_attrs"), new XVar("id=\"tdsortDisplayName\" href=\"#\""));
			this.xt.assign(new XVar("displayuserheadertdbox_attrs"), new XVar("id=\"tdboxDisplayName\" href=\"#\""));
			this.xt.assign(new XVar("emailuserheadersort_attrs"), new XVar("id=\"EmailSort\" href=\"#\""));
			this.xt.assign(new XVar("emailuserheadertdsort_attrs"), new XVar("id=\"tdsortEmail\" href=\"#\""));
			this.xt.assign(new XVar("emailuserheadertdbox_attrs"), new XVar("id=\"tdboxEmail\" href=\"#\""));
			this.xt.assign(new XVar("usernameheadersort_attrs"), new XVar("id=\"userNameSort\" href=\"#\""));
			this.xt.assign(new XVar("choosecolumnsbutton_attrs"), new XVar("id=\"chooseColumnsButton\" href=\"#\""));
			this.xt.assign_loopsection(new XVar("grid_row"), (XVar)(rowInfo));
			smartyGroups.InitAndSetArrayItem("rnr-edge", MVCFunctions.count(smartyGroups) - 1, "rnredgeclass");
			this.xt.assign_loopsection(new XVar("usergroup_header"), (XVar)(smartyGroups));
			if(XVar.Pack(MVCFunctions.count(rowInfo)))
			{
				this.addSaveButtons = new XVar(true);
			}
			return null;
		}
		public virtual XVar fillMembers()
		{
			dynamic grConnection = null, qResult = null, sql = null, tdata = XVar.Array();
			grConnection = XVar.Clone(GlobalVars.cman.getForUserGroups());
			sql = XVar.Clone(MVCFunctions.Concat("select ", grConnection.addFieldWrappers(new XVar("")), ", ", grConnection.addFieldWrappers(new XVar("")), " from ", grConnection.addTableWrappers(new XVar("ugmembers")), " order by ", grConnection.addFieldWrappers(new XVar("")), ", ", grConnection.addFieldWrappers(new XVar(""))));
			qResult = XVar.Clone(grConnection.query((XVar)(sql)));
			while(XVar.Pack(tdata = XVar.Clone(qResult.fetchNumeric())))
			{
				this.members.InitAndSetArrayItem(new XVar(0, tdata[1], 1, tdata[0]), null);
			}
			return null;
		}
		public virtual XVar fillGroups()
		{
			dynamic grConnection = null, qResult = null, sql = null, tdata = XVar.Array();
			grConnection = XVar.Clone(GlobalVars.cman.getForUserGroups());
			this.groups.InitAndSetArrayItem(new XVar(0, -1, 1, MVCFunctions.Concat("<", "Admin", ">")), null);
			this.groupFullChecked.InitAndSetArrayItem(true, null);
			sql = XVar.Clone(MVCFunctions.Concat("select ", grConnection.addFieldWrappers(new XVar("")), ", ", grConnection.addFieldWrappers(new XVar("")), " from ", grConnection.addTableWrappers(new XVar("uggroups")), " order by ", grConnection.addFieldWrappers(new XVar(""))));
			qResult = XVar.Clone(grConnection.query((XVar)(sql)));
			while(XVar.Pack(tdata = XVar.Clone(qResult.fetchNumeric())))
			{
				this.groups.InitAndSetArrayItem(new XVar(0, tdata[0], 1, tdata[1]), null);
				this.groupFullChecked.InitAndSetArrayItem(true, null);
			}
			return null;
		}
		public override XVar prepareForResizeColumns()
		{
			return null;
		}
		public override XVar rulePRG()
		{
			if((XVar)(MVCFunctions.no_output_done())  && (XVar)(MVCFunctions.postvalue(new XVar("a")) == "save"))
			{
				MVCFunctions.HeaderRedirect((XVar)(this.shortTableName), (XVar)(getPageType()), new XVar("a=return"));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			return null;
		}
		public override XVar prepareForBuildPage()
		{
			rulePRG();
			fillMembers();
			fillGroups();
			buildSQL();
			seekPageInRecSet((XVar)(this.querySQL));
			fillGridData();
			buildSearchPanel();
			fillFields();
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
		public override XVar addCommonJs()
		{
			RunnerPage.addCommonJs(this);
			addJsGroupsAndRights();
			return null;
		}
		public virtual XVar addJsGroupsAndRights()
		{
			this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "warnOnLeaving");
			this.jsSettings.InitAndSetArrayItem(this.users, "tableSettings", this.tName, "usersList");
			this.jsSettings.InitAndSetArrayItem(this.fields, "tableSettings", this.tName, "fieldsList");
			this.jsSettings.InitAndSetArrayItem(XVar.Array(), "tableSettings", this.tName, "groupsList");
			foreach (KeyValuePair<XVar, dynamic> grArr in this.groups.GetEnumerator())
			{
				this.jsSettings.InitAndSetArrayItem(grArr.Value[1], "tableSettings", this.tName, "groupsList", grArr.Value[0]);
			}
			return null;
		}
		public virtual XVar saveMembers(dynamic modifiedMembers)
		{
			foreach (KeyValuePair<XVar, dynamic> groups in modifiedMembers.GetEnumerator())
			{
				updateUserGroups((XVar)(groups.Key), (XVar)(groups.Value));
			}
			MVCFunctions.Echo(MVCFunctions.my_json_encode((XVar)(new XVar("success", true))));
			return null;
		}
		public virtual XVar updateUserGroups(dynamic _param_user, dynamic _param_groups)
		{
			#region pass-by-value parameters
			dynamic user = XVar.Clone(_param_user);
			dynamic groups = XVar.Clone(_param_groups);
			#endregion

			dynamic grConnection = null, groupIdWFieldName = null, membersWTableName = null, userNameWFieldName = null;
			grConnection = XVar.Clone(GlobalVars.cman.getForUserGroups());
			membersWTableName = XVar.Clone(grConnection.addTableWrappers(new XVar("ugmembers")));
			userNameWFieldName = XVar.Clone(grConnection.addFieldWrappers(new XVar("")));
			groupIdWFieldName = XVar.Clone(grConnection.addFieldWrappers(new XVar("")));
			foreach (KeyValuePair<XVar, dynamic> state in groups.GetEnumerator())
			{
				dynamic sql = null;
				if(state.Value == 1)
				{
					sql = XVar.Clone(MVCFunctions.Concat("insert into ", membersWTableName, " (", userNameWFieldName, ", ", groupIdWFieldName, ") values (", grConnection.prepareString((XVar)(user)), ",", state.Key, ")"));
				}
				else
				{
					sql = XVar.Clone(MVCFunctions.Concat("delete from ", membersWTableName, " where ", userNameWFieldName, "=", grConnection.prepareString((XVar)(user)), " and ", groupIdWFieldName, "=", state.Key));
				}
				grConnection.exec((XVar)(sql));
			}
			return null;
		}
		public virtual XVar fillFields()
		{
			this.fields.InitAndSetArrayItem(new XVar("name", "Email", "visible", 1, "caption", "E-mail"), null);
			foreach (KeyValuePair<XVar, dynamic> g in this.groups.GetEnumerator())
			{
				this.fields.InitAndSetArrayItem(new XVar("name", g.Value[0], "visible", 1, "caption", g.Value[1]), null);
			}
			return null;
		}
		public override XVar eventExists(dynamic _param_name)
		{
			#region pass-by-value parameters
			dynamic name = XVar.Clone(_param_name);
			#endregion

			return false;
		}
	}
}
