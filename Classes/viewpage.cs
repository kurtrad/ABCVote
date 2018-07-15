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
	public partial class ViewPage : RunnerPage
	{
		public dynamic jsKeys = XVar.Array();
		public dynamic keyFields = XVar.Array();
		protected static bool skipViewPageCtor = false;
		public ViewPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipViewPageCtor)
			{
				skipViewPageCtor = false;
				return;
			}
			setKeysForJs();
			this.formBricks.InitAndSetArrayItem("viewheader", "header");
			this.formBricks.InitAndSetArrayItem(new XVar(0, "viewbuttons", 1, "rightviewbuttons"), "footer");
			assignFormFooterAndHeaderBricks(new XVar(true));
		}
		protected override XVar assignSessionPrefix()
		{
			if((XVar)(this.mode == Constants.VIEW_DASHBOARD)  || (XVar)((XVar)(this.mode == Constants.VIEW_POPUP)  && (XVar)(this.dashTName)))
			{
				this.sessionPrefix = XVar.Clone(MVCFunctions.Concat(this.dashTName, "_", this.tName));
				return null;
			}
			base.assignSessionPrefix();
			return null;
		}
		public override XVar setSessionVariables()
		{
			base.setSessionVariables();
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_advsearch")] = MVCFunctions.serialize((XVar)(this.searchClauseObj));
			return null;
		}
		public static XVar processEditPageSecurity(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic messageLink = null, pageMode = null;
			pageMode = XVar.Clone(readViewModeFromRequest());
			messageLink = new XVar("");
			if((XVar)(!(XVar)(CommonFunctions.isLogged()))  || (XVar)(CommonFunctions.isLoggedAsGuest()))
			{
				messageLink = XVar.Clone(MVCFunctions.Concat(" <a href='#' id='loginButtonContinue'>", "Login", "</a>"));
			}
			if(XVar.Pack(!(XVar)(Security.processPageSecurity((XVar)(table), new XVar("S"), (XVar)(pageMode != Constants.VIEW_SIMPLE), (XVar)(messageLink)))))
			{
				return false;
			}
			return true;
		}
		public virtual XVar setKeys(dynamic _param_keys)
		{
			#region pass-by-value parameters
			dynamic keys = XVar.Clone(_param_keys);
			#endregion

			this.data = new XVar(null);
			this.keys = XVar.Clone(keys);
			setKeysForJs();
			return null;
		}
		protected virtual XVar prepareJsSettings()
		{
			this.jsSettings.InitAndSetArrayItem(this.jsKeys, "tableSettings", this.tName, "keys");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getTableKeys(), "tableSettings", this.tName, "keyFields");
			if(this.mode == Constants.VIEW_DASHBOARD)
			{
				this.jsSettings.InitAndSetArrayItem(getMarkerMasterKeys((XVar)(getCurrentRecordInternal())), "tableSettings", this.tName, "masterKeys");
			}
			return null;
		}
		public virtual XVar setKeysForJs()
		{
			dynamic i = null;
			i = new XVar(0);
			foreach (KeyValuePair<XVar, dynamic> value in this.keys.GetEnumerator())
			{
				this.jsKeys.InitAndSetArrayItem(value.Value, i++);
			}
			return null;
		}
		public override XVar getCurrentRecord()
		{
			dynamic data = XVar.Array(), oldData = XVar.Array(), tdata = null;
			tdata = XVar.Clone(getCurrentRecordInternal());
			data = XVar.Clone(tdata);
			oldData = XVar.Clone(data);
			foreach (KeyValuePair<XVar, dynamic> val in oldData.GetEnumerator())
			{
				dynamic viewFormat = null;
				viewFormat = XVar.Clone(this.pSet.getViewFormat((XVar)(val.Key)));
				if((XVar)((XVar)(viewFormat == Constants.FORMAT_DATABASE_FILE)  || (XVar)(viewFormat == Constants.FORMAT_DATABASE_IMAGE))  || (XVar)(viewFormat == Constants.FORMAT_FILE_IMAGE))
				{
					if(XVar.Pack(data[val.Key]))
					{
						data.InitAndSetArrayItem(true, val.Key);
					}
					else
					{
						data.InitAndSetArrayItem(false, val.Key);
					}
				}
			}
			return data;
		}
		public virtual XVar getCurrentRecordInternal()
		{
			dynamic fetchedArray = null, keysSet = null, orderClause = null, sql = XVar.Array();
			if(XVar.Pack(!(XVar)(this.data == null)))
			{
				return this.data;
			}
			sql = XVar.Clone(getSubsetSQLComponents());
			orderClause = XVar.Clone(getOrderByClause());
			keysSet = XVar.Clone(checkKeysSet());
			if(XVar.Pack(keysSet))
			{
				sql.InitAndSetArrayItem(XVar.Array(), "optionalWhere");
				sql.InitAndSetArrayItem(XVar.Array(), "optionalHaving");
				sql.InitAndSetArrayItem(XVar.Array(), "mandatoryHaving");
				sql.InitAndSetArrayItem(XVar.Array(), "mandatoryWhere");
				sql.InitAndSetArrayItem(CommonFunctions.KeyWhere((XVar)(this.keys)), "mandatoryWhere", null);
				sql.InitAndSetArrayItem(SecuritySQL(new XVar("Search"), (XVar)(this.tName)), "mandatoryWhere", null);
			}
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			if(XVar.Pack(!(XVar)(keysSet)))
			{
				GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryView"))))
			{
				dynamic strSQLbak = null, strWhereClause = null, strWhereClauseBak = null;
				strWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")));
				strSQLbak = XVar.Clone(GlobalVars.strSQL);
				strWhereClauseBak = XVar.Clone(strWhereClause);
				this.eventsObject.BeforeQueryView((XVar)(GlobalVars.strSQL), ref strWhereClause, this);
				if((XVar)(strSQLbak == GlobalVars.strSQL)  && (XVar)(strWhereClauseBak != strWhereClause))
				{
					GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, strWhereClause))));
					if(XVar.Pack(!(XVar)(keysSet)))
					{
						GlobalVars.strSQL = XVar.Clone(CommonFunctions.applyDBrecordLimit((XVar)(MVCFunctions.Concat(GlobalVars.strSQL, orderClause)), new XVar(1), (XVar)(this.connection.dbType)));
					}
				}
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			fetchedArray = XVar.Clone(this.connection.query((XVar)(GlobalVars.strSQL)).fetchAssoc());
			this.data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(fetchedArray)));
			if(XVar.Pack(!(XVar)(keysSet)))
			{
				this.keys = XVar.Clone(getKeysFromData((XVar)(this.data)));
				setKeysForJs();
			}
			if((XVar)(MVCFunctions.count(this.data))  && (XVar)(this.eventsObject.exists(new XVar("ProcessValuesView"))))
			{
				this.eventsObject.ProcessValuesView((XVar)(this.data), this);
			}
			return this.data;
		}
		protected virtual XVar checkKeysSet()
		{
			foreach (KeyValuePair<XVar, dynamic> kValue in this.keys.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(kValue.Value))))
				{
					return true;
				}
			}
			return false;
		}
		protected virtual XVar readRecord()
		{
			if(XVar.Pack(getCurrentRecordInternal()))
			{
				return true;
			}
			if(this.mode == Constants.VIEW_SIMPLE)
			{
				MVCFunctions.HeaderRedirect((XVar)(this.pSet.getShortTableName()), new XVar("list"), new XVar("a=return"));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return false;
		}
		public virtual XVar process()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessView"))))
			{
				this.eventsObject.BeforeProcessView(this);
			}
			if(XVar.Pack(!(XVar)(readRecord())))
			{
				return null;
			}
			prepareMaps();
			if(this.mode == Constants.VIEW_SIMPLE)
			{
				preparePdfControls();
			}
			doCommonAssignments();
			prepareMockControls();
			prepareButtons();
			prepareSteps();
			prepareFields();
			fillCntrlTabGroups();
			prepareJsSettings();
			prepareDetailsTables();
			addButtonHandlers();
			addCommonJs();
			fillSetCntrlMaps();
			displayViewPage();
			return null;
		}
		public override XVar addCommonJs()
		{
			base.addCommonJs();
			if(XVar.Pack(this.allDetailsTablesArr))
			{
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.min.css"));
				AddCSSFile(new XVar("include/jquery-ui/smoothness/jquery-ui.theme.min.css"));
			}
			return null;
		}
		protected virtual XVar doCommonAssignments()
		{
			dynamic data = XVar.Array();
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
			{
				if(XVar.Equals(XVar.Pack(this.mode), XVar.Pack(Constants.VIEW_SIMPLE)))
				{
					headerCommonAssign();
				}
				else
				{
					this.xt.assign(new XVar("menu_chiddenattr"), new XVar("data-hidden"));
				}
			}
			data = XVar.Clone(getCurrentRecordInternal());
			foreach (KeyValuePair<XVar, dynamic> k in this.pSet.getTableKeys().GetEnumerator())
			{
				dynamic viewFormat = null;
				viewFormat = XVar.Clone(this.pSet.getViewFormat((XVar)(k.Value)));
				if((XVar)((XVar)((XVar)((XVar)((XVar)((XVar)(viewFormat == Constants.FORMAT_HTML)  || (XVar)(viewFormat == Constants.FORMAT_FILE_IMAGE))  || (XVar)(viewFormat == Constants.FORMAT_FILE))  || (XVar)(viewFormat == Constants.FORMAT_HYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_HYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_EMAILHYPERLINK))  || (XVar)(viewFormat == Constants.FORMAT_CHECKBOX))
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("show_key", k.Key + 1)), (XVar)(MVCFunctions.runner_htmlspecialchars((XVar)(data[k.Value]))));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("show_key", k.Key + 1)), (XVar)(showDBValue((XVar)(k.Value), (XVar)(data))));
				}
			}
			assignViewFieldsBlocksAndLabels();
			if(this.mode == Constants.VIEW_SIMPLE)
			{
				assignBody();
				this.xt.assign(new XVar("flybody"), new XVar(true));
			}
			displayMasterTableInfo();
			return null;
		}
		protected virtual XVar displayViewPage()
		{
			dynamic templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowView"))))
			{
				this.eventsObject.BeforeShowView((XVar)(this.xt), ref templatefile, (XVar)(getCurrentRecordInternal()), this);
			}
			if(this.mode == Constants.VIEW_SIMPLE)
			{
				display((XVar)(templatefile));
				return null;
			}
			this.xt.assign(new XVar("footer"), new XVar(false));
			this.xt.assign(new XVar("header"), new XVar(false));
			this.xt.assign(new XVar("body"), (XVar)(this.body));
			displayAJAX((XVar)(templatefile), (XVar)(this.flyId + 1));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar makePdf()
		{
			dynamic landscape = null, page = null, pagewidth = null;
			AddCSSFile(new XVar("styles/defaultPDF.css"));
			assignStyleFiles(new XVar(true));
			this.xt.load_template((XVar)(this.templatefile));
			page = XVar.Clone(this.xt.fetch_loaded());
			landscape = XVar.Clone(this.pSet.isLandscapeViewPDFOrientation());
			if(XVar.Pack(this.pSet.isViewPagePDFFitToPage()))
			{
				dynamic pageheight = null;
				pagewidth = XVar.Clone(MVCFunctions.postvalue(new XVar("width")));
				pageheight = XVar.Clone(MVCFunctions.postvalue(new XVar("height")));
			}
			else
			{
				pagewidth = XVar.Clone((((XVar.Pack(landscape) ? XVar.Pack(Constants.PDF_PAGE_HEIGHT) : XVar.Pack(Constants.PDF_PAGE_WIDTH))) * 100) / this.pSet.getViewPagePDFScale());
			}
			return null;
		}
		protected virtual XVar prepareDetailsTables()
		{
			dynamic d = null, dpParams = XVar.Array(), dpTablesParams = XVar.Array();
			if(XVar.Pack(!(XVar)(this.isShowDetailTables)))
			{
				return null;
			}
			dpParams = XVar.Clone(getDetailsParams((XVar)(this.id)));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(dpParams["ids"]))))
			{
				return null;
			}
			this.xt.assign(new XVar("detail_tables"), new XVar(true));
			if(this.mode == Constants.VIEW_DASHBOARD)
			{
				dpTablesParams = XVar.Clone(XVar.Array());
			}
			d = new XVar(0);
			for(;d < MVCFunctions.count(dpParams["ids"]); d++)
			{
				if(this.mode != Constants.VIEW_DASHBOARD)
				{
					setDetailPreview((XVar)(dpParams["type"][d]), (XVar)(dpParams["strTableNames"][d]), (XVar)(dpParams["ids"][d]), (XVar)(getCurrentRecordInternal()));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat("details_", dpParams["shorTNames"][d])), new XVar(true));
					dpTablesParams.InitAndSetArrayItem(new XVar("tName", dpParams["strTableNames"][d], "id", dpParams["ids"][d], "pType", dpParams["type"][d]), null);
					this.xt.assign((XVar)(MVCFunctions.Concat("displayDetailTable_", MVCFunctions.GoodFieldName((XVar)(dpParams["strTableNames"][d])))), (XVar)(MVCFunctions.Concat("<div id='dp_", MVCFunctions.GoodFieldName((XVar)(this.tName)), "_", this.pageType, "_", dpParams["ids"][d], "'></div>")));
				}
			}
			if(this.mode == Constants.VIEW_DASHBOARD)
			{
				this.controlsMap.InitAndSetArrayItem(dpTablesParams, "dpTablesParams");
			}
			return null;
		}
		protected virtual XVar prepareFields()
		{
			dynamic data = XVar.Array(), keyParams = XVar.Array(), keylink = null, viewFields = XVar.Array();
			viewFields = XVar.Clone(this.pSet.getViewFields());
			data = XVar.Clone(getCurrentRecordInternal());
			foreach (KeyValuePair<XVar, dynamic> kf in this.pSet.getTableKeys().GetEnumerator())
			{
				keyParams.InitAndSetArrayItem(MVCFunctions.Concat("&key", kf.Key + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[kf.Value]))))), null);
			}
			keylink = XVar.Clone(MVCFunctions.implode(new XVar(""), (XVar)(keyParams)));
			foreach (KeyValuePair<XVar, dynamic> f in viewFields.GetEnumerator())
			{
				dynamic gname = null, value = null;
				gname = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(f.Value)));
				value = XVar.Clone(MVCFunctions.Concat("<span id=\"view", this.id, "_", gname, "\" >", showDBValue((XVar)(f.Value), (XVar)(data), (XVar)(keylink)), "</span>"));
				this.xt.assign((XVar)(MVCFunctions.Concat(gname, "_value")), (XVar)(value));
				if(XVar.Pack(!(XVar)(isAppearOnTabs((XVar)(f.Value)))))
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(gname, "_fieldblock")), new XVar(true));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(gname, "_tabfieldblock")), new XVar(true));
				}
				if((XVar)(this.pSet.hideEmptyViewFields())  && (XVar)(data[f.Value] == ""))
				{
					hideField((XVar)(f.Value));
				}
			}
			return null;
		}
		protected virtual XVar prepareMockControls()
		{
			dynamic controlFields = XVar.Array();
			controlFields = XVar.Clone(this.pSet.getViewFields());
			foreach (KeyValuePair<XVar, dynamic> fName in controlFields.GetEnumerator())
			{
				dynamic control = XVar.Array();
				control = XVar.Clone(XVar.Array());
				control.InitAndSetArrayItem(this.id, "id");
				control.InitAndSetArrayItem(0, "ctrlInd");
				control.InitAndSetArrayItem(fName.Value, "fieldName");
				control.InitAndSetArrayItem("view", "mode");
				this.controlsMap.InitAndSetArrayItem(control, "controls", null);
			}
			return null;
		}
		protected virtual XVar prepareMaps()
		{
			dynamic fieldsArr = XVar.Array(), viewFields = XVar.Array();
			fieldsArr = XVar.Clone(XVar.Array());
			viewFields = XVar.Clone(this.pSet.getViewFields());
			foreach (KeyValuePair<XVar, dynamic> f in viewFields.GetEnumerator())
			{
				fieldsArr.InitAndSetArrayItem(new XVar("fName", f.Value, "viewFormat", this.pSet.getViewFormat((XVar)(f.Value))), null);
			}
			setGoogleMapsParams((XVar)(fieldsArr));
			if(XVar.Pack(this.googleMapCfg["isUseGoogleMap"]))
			{
				initGmaps();
			}
			return null;
		}
		protected virtual XVar prepareNextPrevButtons()
		{
			dynamic nextPrev = XVar.Array(), prev = null, var_next = null;
			if((XVar)(!(XVar)(this.pSet.useMoveNext()))  || (XVar)(this.pdfMode))
			{
				return null;
			}
			var_next = XVar.Clone(XVar.Array());
			prev = XVar.Clone(XVar.Array());
			nextPrev = XVar.Clone(getNextPrevRecordKeys((XVar)(getCurrentRecordInternal())));
			assignPrevNextButtons((XVar)(0 < MVCFunctions.count(nextPrev["next"])), (XVar)(0 < MVCFunctions.count(nextPrev["prev"])), (XVar)((XVar)(this.mode == Constants.VIEW_DASHBOARD)  && (XVar)((XVar)(hasTableDashGridElement())  || (XVar)(hasDashMapElement()))));
			this.jsSettings.InitAndSetArrayItem(nextPrev["prev"], "tableSettings", this.tName, "prevKeys");
			this.jsSettings.InitAndSetArrayItem(nextPrev["next"], "tableSettings", this.tName, "nextKeys");
			return null;
		}
		protected virtual XVar prepareButtons()
		{
			dynamic editable = null;
			if(XVar.Pack(this.pdfMode))
			{
				return null;
			}
			prepareNextPrevButtons();
			if(this.mode == Constants.VIEW_DASHBOARD)
			{
				if(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)
				{
					this.xt.assign(new XVar("groupbutton_class"), new XVar("rnr-invisible-button"));
				}
				return null;
			}
			if(this.mode == Constants.VIEW_SIMPLE)
			{
				if(XVar.Pack(this.pSet.hasListPage()))
				{
					this.xt.assign(new XVar("back_button"), new XVar(true));
					this.xt.assign(new XVar("backbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"backButton", this.id, "\"")));
					this.xt.assign(new XVar("mbackbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"extraBackButton", this.id, "\"")));
				}
				else
				{
					if(XVar.Pack(isShowMenu()))
					{
						this.xt.assign(new XVar("back_button"), new XVar(true));
						this.xt.assign(new XVar("backbutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"backToMenuButton", this.id, "\"")));
					}
				}
			}
			if(this.mode == Constants.VIEW_POPUP)
			{
				this.xt.assign(new XVar("close_button"), new XVar(true));
				this.xt.assign(new XVar("closebutton_attrs"), (XVar)(MVCFunctions.Concat("id=\"closeButton", this.id, "\"")));
			}
			editable = new XVar(false);
			if(XVar.Pack(editAvailable()))
			{
				dynamic data = XVar.Array();
				data = XVar.Clone(getCurrentRecordInternal());
				editable = XVar.Clone(CommonFunctions.CheckSecurity((XVar)(data[this.pSet.getTableOwnerID()]), new XVar("Edit")));
				if(XVar.Pack(GlobalVars.globalEvents.exists(new XVar("IsRecordEditable"), (XVar)(this.tName))))
				{
					editable = XVar.Clone(GlobalVars.globalEvents.IsRecordEditable((XVar)(getCurrentRecordInternal()), (XVar)(editable), (XVar)(this.tName)));
				}
				if(XVar.Pack(editable))
				{
					this.xt.assign(new XVar("edit_page_button"), new XVar(true));
					this.xt.assign(new XVar("edit_page_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"editPageButton", this.id, "\"")));
					this.xt.assign(new XVar("header_edit_page_button_attrs"), (XVar)(MVCFunctions.Concat("id=\"headerEditPageButton", this.id, "\"")));
				}
			}
			this.xt.assign(new XVar("view_menu_button"), (XVar)((XVar)(editable)  || (XVar)(this.viewPdfEnabled)));
			return null;
		}
		public static XVar readViewModeFromRequest()
		{
			if(MVCFunctions.postvalue(new XVar("mode")) == "dashrecord")
			{
				return Constants.VIEW_DASHBOARD;
			}
			else
			{
				if(XVar.Pack(MVCFunctions.postvalue(new XVar("onFly"))))
				{
					return Constants.VIEW_POPUP;
				}
			}
			return Constants.VIEW_SIMPLE;
		}
		public override XVar isMultistepped()
		{
			return this.pSet.isViewMultistep();
		}
		public override XVar editAvailable()
		{
			if(XVar.Pack(this.dashElementData))
			{
				return (XVar)(base.editAvailable())  && (XVar)(this.dashElementData["details"][this.tName]["edit"]);
			}
			return base.editAvailable();
		}
		public virtual XVar assignViewFieldsBlocksAndLabels()
		{
			dynamic viewFields = XVar.Array();
			viewFields = XVar.Clone(this.pSet.getViewFields());
			foreach (KeyValuePair<XVar, dynamic> fName in viewFields.GetEnumerator())
			{
				dynamic gfName = null;
				gfName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fName.Value)));
				if(XVar.Pack(!(XVar)(isAppearOnTabs((XVar)(fName.Value)))))
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(gfName, "_fieldblock")), new XVar(true));
				}
				else
				{
					this.xt.assign((XVar)(MVCFunctions.Concat(gfName, "_tabfieldblock")), new XVar(true));
				}
				this.xt.assign((XVar)(MVCFunctions.Concat(gfName, "_label")), new XVar(true));
				if((XVar)(this.is508)  && (XVar)(getLayoutVersion() != Constants.BOOTSTRAP_LAYOUT))
				{
					this.xt.assign_section((XVar)(MVCFunctions.Concat(gfName, "_label")), (XVar)(MVCFunctions.Concat("<label for=\"", getInputElementId((XVar)(fName.Value)), "\">")), new XVar("</label>"));
				}
			}
			return null;
		}
		public static XVar processMasterKeys()
		{
			dynamic i = null, options = XVar.Array();
			i = new XVar(1);
			options = XVar.Clone(XVar.Array());
			while(XVar.Pack(MVCFunctions.REQUESTKeyExists(MVCFunctions.Concat("masterkey", i))))
			{
				options.InitAndSetArrayItem(MVCFunctions.postvalue(MVCFunctions.Concat("masterkey", i)), i);
				i++;
			}
			return options;
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic sql = XVar.Array();
			sql = XVar.Clone(base.getSubsetSQLComponents());
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ROW_NUMBER() over () as DB2_ROW_NUMBER ");
			}
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Search"), (XVar)(this.tName)), "mandatoryWhere", null);
			return sql;
		}
	}
}
