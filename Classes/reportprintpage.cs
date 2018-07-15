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
	public partial class ReportPrintPage : ReportPage
	{
		public dynamic pageWidth = XVar.Pack(Constants.PDF_PAGE_WIDTH);
		public dynamic pageHeight = XVar.Pack(Constants.PDF_PAGE_HEIGHT);
		public dynamic pdfWidth = XVar.Pack(Constants.PDF_PAGE_WIDTH);
		public dynamic splitAtServer = XVar.Pack(false);
		public dynamic splitByGroups = XVar.Pack(0);
		public dynamic pages = XVar.Array();
		public dynamic arrPages = XVar.Array();
		public dynamic pdfContent = XVar.Pack("");
		public dynamic pdfFitToPage = XVar.Pack(1);
		public dynamic landscape = XVar.Pack(0);
		public dynamic isDetail = XVar.Pack(false);
		public dynamic isReportEmpty = XVar.Pack(false);
		public dynamic multipleDetails = XVar.Pack(false);
		public dynamic exportPdf;
		protected static bool skipReportPrintPageCtor = false;
		public ReportPrintPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipReportPrintPageCtor)
			{
				skipReportPrintPageCtor = false;
				return;
			}
			this.crossTable = XVar.Clone((XVar.Pack(this.pSet.isCrossTabReport()) ? XVar.Pack(1) : XVar.Pack(0)));
			this.jsSettings.InitAndSetArrayItem(this.crossTable, "tableSettings", this.tName, "reportType");
			if(XVar.Pack(this.pdfMode))
			{
				if(this.pSet.getReportPrintPDFGroupsPerPage() != 0)
				{
					this.splitAtServer = new XVar(true);
					this.splitByGroups = XVar.Clone(this.pSet.getReportPrintPDFGroupsPerPage());
				}
			}
			else
			{
				if((XVar)(this.format == "excel")  || (XVar)(this.format == "word"))
				{
					this.splitAtServer = new XVar(false);
					this.splitByGroups = new XVar(0);
				}
				else
				{
					if(this.pSet.getReportPrintPartitionType() != 0)
					{
						this.splitAtServer = new XVar(true);
						if(XVar.Pack(!(XVar)(this.splitByGroups)))
						{
							this.splitByGroups = XVar.Clone(this.pSet.getReportPrintGroupsPerPage());
						}
					}
				}
			}
			if(XVar.Pack(this.isDetail))
			{
				this.splitAtServer = new XVar(false);
				this.splitByGroups = new XVar(0);
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessReportPrint"))))
			{
				this.eventsObject.BeforeProcessReportPrint(this);
			}
			if(XVar.Pack(CommonFunctions.isRTL()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isRTL");
			}
			this.jsSettings.InitAndSetArrayItem(this.pSet.getReportPrintPartitionType(), "tableSettings", this.tName, "reportPrintPartitionType");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getReportPrintGroupsPerPage(), "tableSettings", this.tName, "reportPrintGroupsPerPage");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getReportPrintLayout(), "tableSettings", this.tName, "reportPrintLayout");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getLowGroup(), "tableSettings", this.tName, "lowGroup");
			this.jsSettings.InitAndSetArrayItem(this.pSet.isPrinterPagePDF(), "tableSettings", this.tName, "printerPagePDF");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPageOrientation(), "tableSettings", this.tName, "printerPageOrientation");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPageScale(), "tableSettings", this.tName, "printerPageScale");
			this.jsSettings.InitAndSetArrayItem(this.pSet.isPrinterPageFitToPage(), "tableSettings", this.tName, "isPrinterPageFitToPage");
			if(this.pSet.getReportPrintPartitionType() == 0)
			{
				this.jsSettings.InitAndSetArrayItem(0, "tableSettings", this.tName, "printerSplitRecords");
			}
			else
			{
				this.jsSettings.InitAndSetArrayItem(this.pSet.getReportPrintGroupsPerPage(), "tableSettings", this.tName, "printerSplitRecords");
			}
			this.jsSettings.InitAndSetArrayItem(this.pSet.getReportPrintPDFGroupsPerPage(), "tableSettings", this.tName, "printerPDFSplitRecords");
		}
		public virtual XVar assignPDFFormatSettings()
		{
			if(XVar.Pack(this.exportPdf))
			{
				this.jsSettings.InitAndSetArrayItem(1, "tableSettings", this.tName, "exportPdf");
			}
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				return null;
			}
			this.landscape = XVar.Clone(this.pSet.isLandscapePrinterPagePDFOrientation());
			this.pdfFitToPage = XVar.Clone((XVar.Pack(this.crossTable) ? XVar.Pack(1) : XVar.Pack(this.pSet.isPrinterPagePDFFitToPage())));
			this.pageWidth = new XVar(Constants.PDF_PAGE_WIDTH);
			this.pageHeight = new XVar(Constants.PDF_PAGE_HEIGHT);
			if(XVar.Pack(!(XVar)(this.pdfFitToPage)))
			{
				dynamic PrinterPagePDFScale = null;
				PrinterPagePDFScale = XVar.Clone(this.pSet.getPrinterPagePDFScale());
				this.pageWidth = XVar.Clone((this.pageWidth * 100) / PrinterPagePDFScale);
				this.pageHeight = XVar.Clone((this.pageHeight * 100) / PrinterPagePDFScale);
			}
			this.jsSettings.InitAndSetArrayItem(this.pSet.isLandscapePrinterPagePDFOrientation(), "tableSettings", this.tName, "pdfPrinterPageOrientation");
			this.jsSettings.InitAndSetArrayItem(this.landscape, "tableSettings", this.tName, "printerPageOrientation");
			this.jsSettings.InitAndSetArrayItem(1, "tableSettings", this.tName, "createPdf");
			this.jsSettings.InitAndSetArrayItem(this.pdfFitToPage, "tableSettings", this.tName, "pdfFitToPage");
			if(XVar.Pack(this.landscape))
			{
				dynamic temp = null;
				temp = XVar.Clone(this.pageWidth);
				this.pageWidth = XVar.Clone(this.pageHeight);
				this.pageHeight = XVar.Clone(temp);
			}
			this.jsSettings.InitAndSetArrayItem(this.pageWidth, "tableSettings", this.tName, "pageWidth");
			this.jsSettings.InitAndSetArrayItem(this.pageHeight, "tableSettings", this.tName, "pageHeight");
			return null;
		}
		public override XVar getExtraReportParams()
		{
			dynamic extraParams = XVar.Array();
			extraParams = XVar.Clone(base.getExtraReportParams());
			if(this.format == "excel")
			{
				extraParams.InitAndSetArrayItem("excel", "forExport");
			}
			else
			{
				if(this.format == "word")
				{
					extraParams.InitAndSetArrayItem("word", "forExport");
				}
				else
				{
					extraParams.InitAndSetArrayItem(false, "forExport");
				}
			}
			if(XVar.Pack(!(XVar)(this.crossTable)))
			{
				extraParams.InitAndSetArrayItem(Constants.MODE_PRINT, "mode");
			}
			return extraParams;
		}
		public override XVar process()
		{
			dynamic extraParams = XVar.Array(), forExport = null;
			displayMasterTableInfo();
			assignPDFFormatSettings();
			forExport = new XVar(false);
			if(this.format == "excel")
			{
				forExport = new XVar("excel");
				MVCFunctions.Header("Content-Type", "application/vnd.ms-excel");
				MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", this.shortTableName, ".xls")));
				MVCFunctions.Echo("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
				MVCFunctions.Echo(MVCFunctions.Concat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=", GlobalVars.cCharset, "\">"));
			}
			else
			{
				if(this.format == "word")
				{
					forExport = new XVar("word");
					MVCFunctions.Header("Content-Type", "application/vnd.ms-word");
					MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", this.shortTableName, ".doc")));
					MVCFunctions.Echo(MVCFunctions.Concat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=", GlobalVars.cCharset, "\">"));
				}
			}
			doCommonAssignments();
			extraParams = XVar.Clone(getExtraReportParams());
			setGoogleMapsParams((XVar)(extraParams["fieldsArr"]));
			setReportData((XVar)(extraParams));
			addButtonHandlers();
			addCommonJs();
			showPage();
			return null;
		}
		public override XVar doCommonAssignments()
		{
			assignBody();
			this.xt.assign(new XVar("stylesheetlink"), (XVar)(0 < MVCFunctions.strlen((XVar)(this.format))));
			if(this.pSet.getReportPrintPartitionType() == 0)
			{
				this.xt.assign(new XVar("divideintopages_block"), new XVar(true));
			}
			foreach (KeyValuePair<XVar, dynamic> fName in this.pSet.getFieldsList().GetEnumerator())
			{
				this.xt.assign((XVar)(MVCFunctions.Concat(fName.Value, "_fieldheader")), new XVar(true));
			}
			this.xt.assign(new XVar("divideintopages_block"), new XVar(false));
			if(XVar.Pack(this.format))
			{
				this.xt.assign(new XVar("pdflink_block"), new XVar(false));
			}
			else
			{
				this.xt.assign(new XVar("pdflink_block"), (XVar)((XVar)(this.pSet.isPrinterPagePDF())  && (XVar)(!(XVar)(this.pdfMode))));
			}
			if(1 < MVCFunctions.count(this.gridTabs))
			{
				dynamic curTabId = null;
				curTabId = XVar.Clone(getCurrentTabId());
				this.xt.assign(new XVar("printtabheader"), new XVar(true));
				this.xt.assign(new XVar("printtabheader_text"), (XVar)(getTabTitle((XVar)(curTabId))));
			}
			return null;
		}
		protected override XVar assignBody()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage(new XVar(false)));
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.assign(new XVar("grid_header"), new XVar(true));
			if((XVar)(this.format)  && (XVar)(this.format != "pdf"))
			{
				this.body.InitAndSetArrayItem("", "begin");
				this.body.InitAndSetArrayItem("", "end");
				this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			}
			return null;
		}
		public override XVar addCommonJs()
		{
			base.addCommonJs();
			if(XVar.Pack(this.pSet.isPrinterPagePDF()))
			{
				AddJSFile(new XVar("include/pdfinitlink.js"));
			}
			return null;
		}
		protected virtual XVar getnoRecOnFirstPageWhereCondition()
		{
			return "";
		}
		protected override XVar crossTableCommonAssign(dynamic _param_showSummary)
		{
			#region pass-by-value parameters
			dynamic showSummary = XVar.Clone(_param_showSummary);
			#endregion

			dynamic grid_row = XVar.Array(), pages = XVar.Array();
			this.xt.assign(new XVar("report_cross_header"), (XVar)(this.crossTableObj.getPrintCrossHeader()));
			this.xt.assign(new XVar("totals"), (XVar)(this.crossTableObj.getTotalsName()));
			grid_row.InitAndSetArrayItem(this.crossTableObj.getCrossTableData(), "data");
			if(0 < MVCFunctions.count(grid_row["data"]))
			{
				this.xt.assign(new XVar("grid_row"), (XVar)(grid_row));
				this.xt.assignbyref(new XVar("group_header"), (XVar)(this.crossTableObj.getCrossTableHeader()));
				this.xt.assignbyref(new XVar("col_summary"), (XVar)(this.crossTableObj.getCrossTableSummary()));
				this.xt.assignbyref(new XVar("total_summary"), (XVar)(this.crossTableObj.getTotalSummary()));
				this.xt.assign(new XVar("cross_totals"), (XVar)(showSummary));
			}
			else
			{
				if(XVar.Pack(this.isDetail))
				{
					this.isReportEmpty = new XVar(true);
					return null;
				}
			}
			pages = XVar.Clone(XVar.Array());
			pages.InitAndSetArrayItem(grid_row, 0, "grid_row");
			pages.InitAndSetArrayItem("<div name=page class=printpage>", 0, "begin");
			pages.InitAndSetArrayItem("</div>", 0, "end");
			this.xt.assign(new XVar("pageno"), new XVar(1));
			this.xt.assign(new XVar("maxpages"), new XVar(1));
			this.xt.assign(new XVar("printheader"), new XVar(true));
			this.xt.assign_loopsection(new XVar("pages"), (XVar)(pages));
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				this.xt.assign(new XVar("printbuttons"), new XVar(true));
			}
			return null;
		}
		public override XVar setStandartData(dynamic _options)
		{
			dynamic pageSize = null, pagestart = null, rb = null, sqlArray = null, whereComponents = XVar.Array();
			if(XVar.Pack(!(XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")])))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")] = -1;
			}
			if(XVar.Pack(!(XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")])))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] = 1;
			}
			if((XVar)((XVar)(MVCFunctions.REQUESTKeyExists("all"))  && (XVar)(MVCFunctions.postvalue("all")))  || (XVar)(this.isDetail))
			{
				pageSize = new XVar(0);
				pagestart = new XVar(0);
				this.jsSettings.InitAndSetArrayItem(1, "tableSettings", this.tName, "reportPrintMode");
				this.controlsMap.InitAndSetArrayItem(1, "pdfSettings", "allPagesMode");
			}
			else
			{
				pageSize = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")]);
				pagestart = XVar.Clone((XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")] - 1) * pageSize);
			}
			whereComponents = XVar.Clone(getWhereComponents());
			sqlArray = XVar.Clone(getReportSQLData());
			rb = XVar.Clone(new Report((XVar)(sqlArray), (XVar)(this.pSet.getTableData(new XVar(".orderindexes"))), (XVar)(this.connection), (XVar)(pageSize), (XVar)(this.splitByGroups), (XVar)(_options), (XVar)(whereComponents["searchWhere"]), (XVar)(whereComponents["searchHaving"]), this));
			this.arrReport = XVar.Clone(rb.getReport((XVar)(pagestart)));
			this.arrPages = XVar.Clone(rb.getPages());
			standardReportCommonAssign();
			return null;
		}
		protected override XVar standardReportCommonAssign()
		{
			this.xt.assign(new XVar("printheader"), new XVar(true));
			if(XVar.Pack(this.splitAtServer))
			{
				standardReportCommonAssignSplit();
				return null;
			}
			foreach (KeyValuePair<XVar, dynamic> value in this.arrReport["page"].GetEnumerator())
			{
				this.xt.assign((XVar)(value.Key), (XVar)(value.Value));
			}
			if((XVar)(this.isDetail)  && (XVar)(!(XVar)(MVCFunctions.count(this.arrReport["list"]))))
			{
				this.isReportEmpty = new XVar(true);
				return null;
			}
			this.xt.assign_loopsection(new XVar("grid_row"), (XVar)(this.arrReport["list"]));
			if(XVar.Pack(this.arrReport["global"]))
			{
				foreach (KeyValuePair<XVar, dynamic> value in this.arrReport["global"].GetEnumerator())
				{
					this.xt.assign((XVar)(value.Key), (XVar)(value.Value));
				}
			}
			this.xt.assign(new XVar("pageno"), new XVar(1));
			this.xt.assign(new XVar("maxpages"), new XVar(1));
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				this.xt.assign(new XVar("printbuttons"), new XVar(true));
			}
			this.xt.assign(new XVar("global_summary"), new XVar(true));
			this.xt.assign(new XVar("pages"), new XVar(true));
			return null;
		}
		protected virtual XVar standardReportCommonAssignSplit()
		{
			dynamic page = XVar.Array(), pageno = null;
			page = XVar.Clone(new XVar("grid_row", new XVar("data", XVar.Array())));
			pageno = new XVar(1);
			foreach (KeyValuePair<XVar, dynamic> pagerecords in this.arrReport["list"].GetEnumerator())
			{
				page.InitAndSetArrayItem(pagerecords.Value, "grid_row", "data");
				addPage((XVar)(page), (XVar)(pageno));
				++(pageno);
				page = XVar.Clone(new XVar("grid_row", new XVar("data", XVar.Array())));
			}
			if(XVar.Pack(this.arrReport["global"]))
			{
				dynamic lastPage = XVar.Array();
				lastPage = this.pages[MVCFunctions.count(this.pages) - 1];
				foreach (KeyValuePair<XVar, dynamic> value in this.arrReport["global"].GetEnumerator())
				{
					lastPage.InitAndSetArrayItem(value.Value, value.Key);
				}
				lastPage.InitAndSetArrayItem(true, "global_summary");
			}
			this.xt.assign(new XVar("maxpages"), (XVar)(pageno));
			this.body.InitAndSetArrayItem(this.pages, "data");
			this.xt.assign(new XVar("page_number"), new XVar(true));
			this.xt.assign(new XVar("pagecount"), (XVar)(pageno - 1));
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				this.xt.assign(new XVar("printbuttons"), new XVar(true));
			}
			this.xt.assign(new XVar("pages"), new XVar(true));
			return null;
		}
		protected virtual XVar addPage(dynamic page, dynamic _param_pageno)
		{
			#region pass-by-value parameters
			dynamic pageno = XVar.Clone(_param_pageno);
			#endregion

			page.InitAndSetArrayItem((XVar)(pageno == 1)  && (XVar)(!(XVar)(this.pdfMode)), "printbuttons");
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				page.InitAndSetArrayItem("<div class=\"rp-presplitpage rp-page\">", "begin");
			}
			else
			{
				page.InitAndSetArrayItem("<div class=\"rp-page\">", "begin");
			}
			page.InitAndSetArrayItem("</div>", "end");
			page.InitAndSetArrayItem(pageno, "pageno");
			if(XVar.Pack(MVCFunctions.is_array((XVar)(this.arrPages[pageno - 1]))))
			{
				foreach (KeyValuePair<XVar, dynamic> value in this.arrPages[pageno - 1].GetEnumerator())
				{
					page.InitAndSetArrayItem(value.Value, value.Key);
				}
			}
			page.InitAndSetArrayItem(pageno, "pageno");
			this.pages.InitAndSetArrayItem(page, null);
			return null;
		}
		public virtual XVar prepareWordOrExcelTemplate(dynamic _param_contents)
		{
			#region pass-by-value parameters
			dynamic contents = XVar.Clone(_param_contents);
			#endregion

			dynamic pos1 = null;
			pos1 = new XVar(0);
			while(!XVar.Equals(XVar.Pack(pos1), XVar.Pack(false)))
			{
				pos1 = XVar.Clone(MVCFunctions.stripos((XVar)(contents), new XVar("<link "), (XVar)(pos1)));
				if(!XVar.Equals(XVar.Pack(pos1), XVar.Pack(false)))
				{
					dynamic pos2 = null;
					pos2 = XVar.Clone(MVCFunctions.strpos((XVar)(contents), new XVar(">"), (XVar)(pos1)));
					if(!(XVar)(pos2) == false)
					{
						contents = XVar.Clone(MVCFunctions.Concat(MVCFunctions.substr((XVar)(contents), new XVar(0), (XVar)(pos1)), MVCFunctions.substr((XVar)(contents), (XVar)(pos2 + 1))));
					}
				}
			}
			contents = XVar.Clone(MVCFunctions.str_ireplace((XVar)(MVCFunctions.Concat("<img src=\"/", MVCFunctions.GetRootPathForResources(new XVar("images/spacer.gif")), "\">")), new XVar(""), (XVar)(contents)));
			contents = XVar.Clone(MVCFunctions.str_ireplace((XVar)(MVCFunctions.Concat("<img src=\"/", MVCFunctions.GetRootPathForResources(new XVar("images/spacer.gif")), "\"/>")), new XVar(""), (XVar)(contents)));
			contents = XVar.Clone(MVCFunctions.str_ireplace(new XVar("<img src=\"@webRootPath/images/spacer.gif\" />"), new XVar(""), (XVar)(contents)));
			return contents;
		}
		public virtual XVar processDetailPrint()
		{
			dynamic extraParams = null;
			extraParams = XVar.Clone(getExtraReportParams());
			setReportData((XVar)(extraParams));
			if(XVar.Pack(this.isReportEmpty))
			{
				return null;
			}
			showDetailPrint();
			return null;
		}
		public virtual XVar showDetailPrint()
		{
			this.xt.hideAllBricksExcept((XVar)(new XVar(0, "grid")));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.load_template((XVar)(this.templatefile));
			MVCFunctions.Echo("<div class='rnr-print-details'>");
			if(XVar.Pack(this.multipleDetails))
			{
				MVCFunctions.Echo("<div class='rnr-pd-title'>");
				MVCFunctions.Echo(getPageTitle((XVar)(this.pageType), (XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName)))));
				MVCFunctions.Echo("</div>");
			}
			MVCFunctions.Echo("<div class='rnr-pd-grid'>");
			MVCFunctions.Echo(this.xt.fetch_loaded(new XVar("container_grid")));
			MVCFunctions.Echo("</div>");
			MVCFunctions.Echo("</div>");
			return null;
		}
		public override XVar showPage()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowReportPrint"))))
			{
				this.eventsObject.BeforeShowReportPrint((XVar)(this.xt), ref this.templatefile, this);
			}
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				if((XVar)(this.format == "excel")  || (XVar)(this.format == "word"))
				{
					dynamic contents = null;
					this.xt.load_template((XVar)(this.templatefile));
					contents = XVar.Clone(prepareWordOrExcelTemplate((XVar)(this.xt.template)));
					this.xt.template = XVar.Clone(contents);
					this.xt.display_loaded();
				}
				else
				{
					if(this.format == "pdf")
					{
						this.xt.displayBrickHidden(new XVar("printpdf"));
						AddCSSFile(new XVar("styles/defaultPDF.css"));
						assignStyleFiles();
						this.xt.load_template((XVar)(this.templatefile));
						this.xt.display_loaded();
					}
					else
					{
						display((XVar)(this.templatefile));
					}
				}
			}
			else
			{
				dynamic landscape = null, page = null, pageWidth = null;
				AddCSSFile(new XVar("styles/defaultPDF.css"));
				assignStyleFiles(new XVar(true));
				this.xt.load_template((XVar)(this.templatefile));
				page = XVar.Clone(this.xt.fetch_loaded());
				if(XVar.Pack(this.pdfFitToPage))
				{
					pageWidth = XVar.Clone(MVCFunctions.postvalue(new XVar("width")));
				}
				else
				{
					pageWidth = XVar.Clone(this.pageWidth);
				}
				landscape = XVar.Clone(this.landscape);
			}
			return null;
		}
	}
}
