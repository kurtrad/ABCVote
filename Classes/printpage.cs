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
	public partial class PrintPage : RunnerPage
	{
		public dynamic allPagesMode = XVar.Pack(false);
		public dynamic masterKeys = XVar.Array();
		public dynamic recordset = XVar.Pack(null);
		public dynamic pdfWidth = XVar.Pack(Constants.PDF_PAGE_WIDTH);
		public dynamic pdfContent = XVar.Pack("");
		public dynamic fetchedRecordCount = XVar.Pack(0);
		public dynamic splitByRecords = XVar.Pack(0);
		public dynamic detailTables;
		public dynamic pageBody = XVar.Array();
		public dynamic countSQL = XVar.Pack("");
		public dynamic totals = XVar.Array();
		public dynamic totalRowCount = XVar.Pack(false);
		public dynamic queryPageNo = XVar.Pack(1);
		public dynamic queryPageSize = XVar.Pack(0);
		public dynamic _eof = XVar.Pack(false);
		public dynamic nextRecord = XVar.Pack(null);
		public dynamic customFieldForSort = XVar.Array();
		public dynamic customHowFieldSort = XVar.Array();
		public dynamic pageNo = XVar.Pack(1);
		public dynamic hideColumns = XVar.Array();
		protected static bool skipPrintPageCtor = false;
		public PrintPage(dynamic var_params = null)
			:base((XVar)var_params)
		{
			if(skipPrintPageCtor)
			{
				skipPrintPageCtor = false;
				return;
			}
			#region default values
			if(var_params as Object == null) var_params = new XVar("");
			#endregion

			dynamic i = null;
			if(XVar.Pack(this.selection))
			{
				this.allPagesMode = new XVar(true);
			}
			if(XVar.Pack(!(XVar)(this.detailTables)))
			{
				this.detailTables = XVar.Clone(XVar.Array());
			}
			if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(this.detailTables)))))
			{
				this.detailTables = XVar.Clone(new XVar(0, this.detailTables));
			}
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				this.controlsMap.InitAndSetArrayItem(XVar.Array(), "pdfSettings");
				this.controlsMap.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(MVCFunctions.my_json_encode((XVar)(this.selection)))), "pdfSettings", "selectedJSON");
				this.controlsMap.InitAndSetArrayItem(MVCFunctions.RawUrlEncode((XVar)(MVCFunctions.my_json_encode((XVar)(this.detailTables)))), "pdfSettings", "detailTablesJSON");
				this.controlsMap.InitAndSetArrayItem(this.allPagesMode, "pdfSettings", "allPagesMode");
			}
			else
			{
				this.allPagesMode = XVar.Clone(MVCFunctions.postvalue(new XVar("allPagesMode")));
				this.selection = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.RawUrlDecode((XVar)(MVCFunctions.postvalue(new XVar("selectedJSON")))))));
				if(XVar.Pack(!(XVar)(this.selection)))
				{
					this.selection = XVar.Clone(XVar.Array());
				}
				this.detailTables = XVar.Clone(MVCFunctions.my_json_decode((XVar)(MVCFunctions.RawUrlDecode((XVar)(MVCFunctions.postvalue(new XVar("detailTablesJSON")))))));
			}
			this.printGridLayout = XVar.Clone(this.pSet.getPrintGridLayout());
			this.recsPerRowPrint = XVar.Clone(this.pSet.getRecordsPerRowPrint());
			i = new XVar(0);
			for(;i < MVCFunctions.count(this.detailKeysByM); i++)
			{
				this.masterKeys.InitAndSetArrayItem(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_masterkey", i + 1)], null);
			}
			this.masterTable = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_mastertable")]);
			this.totalsFields = XVar.Clone(this.pSet.getTotalsFields());
			if(XVar.Pack(!(XVar)(this.splitByRecords)))
			{
				this.splitByRecords = XVar.Clone(this.pSet.getPrinterSplitRecords());
			}
			if(XVar.Pack(this.pdfMode))
			{
				this.splitByRecords = XVar.Clone(this.pSet.getPrinterPDFSplitRecords());
			}
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				dynamic hideColumns = XVar.Array();
				hideColumns = XVar.Clone(getColumnsToHide());
				this.hideColumns = XVar.Clone(hideColumns[Constants.DESKTOP]);
				if(XVar.Pack(!(XVar)(MVCFunctions.is_array((XVar)(this.hideColumns)))))
				{
					this.hideColumns = XVar.Clone(XVar.Array());
				}
			}
		}
		public static XVar readSelectedRecordsFromRequest(dynamic _param_table)
		{
			#region pass-by-value parameters
			dynamic table = XVar.Clone(_param_table);
			#endregion

			dynamic keyFields = XVar.Array(), selected_recs = XVar.Array();
			ProjectSettings pSet;
			if(XVar.Pack(!(XVar)(MVCFunctions.postvalue("selection"))))
			{
				return XVar.Array();
			}
			pSet = XVar.UnPackProjectSettings(new ProjectSettings((XVar)(table)));
			keyFields = XVar.Clone(pSet.getTableKeys());
			selected_recs = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> keyblock in MVCFunctions.EnumeratePOST("selection"))
			{
				dynamic arr = XVar.Array(), keys = XVar.Array();
				arr = XVar.Clone(MVCFunctions.explode(new XVar("&"), (XVar)(keyblock.Value)));
				if(MVCFunctions.count(arr) < MVCFunctions.count(keyFields))
				{
					continue;
				}
				keys = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> value in arr.GetEnumerator())
				{
					keys.InitAndSetArrayItem(MVCFunctions.urldecode((XVar)(value.Value)), keyFields[value.Key]);
				}
				selected_recs.InitAndSetArrayItem(keys, null);
			}
			return selected_recs;
		}
		protected virtual XVar calcRowCount()
		{
			dynamic rowcount = null;
			rowcount = new XVar(false);
			if(XVar.Pack(eventExists(new XVar("ListGetRowCount"))))
			{
				rowcount = XVar.Clone(this.eventsObject.ListGetRowCount((XVar)(this.searchClauseObj), (XVar)(this.masterTable), (XVar)(this.masterKeysReq), new XVar(null), this));
				if(!XVar.Equals(XVar.Pack(rowcount), XVar.Pack(false)))
				{
					this.totalRowCount = XVar.Clone(rowcount);
					return null;
				}
			}
			this.totalRowCount = XVar.Clone(this.connection.getFetchedRowsNumber((XVar)(this.countSQL)));
			return null;
		}
		protected virtual XVar prepareCustomListQueryLegacySorting()
		{
			dynamic arrFieldForSort = null, arrHowFieldSort = null, fieldList = XVar.Array(), i = null;
			if(XVar.Pack(!(XVar)(this.eventsObject.exists(new XVar("ListQuery")))))
			{
				return null;
			}
			arrFieldForSort = XVar.Clone(XVar.Array());
			arrHowFieldSort = XVar.Clone(XVar.Array());
			fieldList = XVar.Clone(MVCFunctions.unserialize((XVar)(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_orderFieldsList")])));
			i = new XVar(0);
			for(;i < MVCFunctions.count(fieldList); i++)
			{
				this.customFieldForSort.InitAndSetArrayItem(fieldList[i].fieldIndex, null);
				this.customHowFieldSort.InitAndSetArrayItem(fieldList[i].orderDirection, null);
			}
			return null;
		}
		protected virtual XVar calcPageSizeAndNumber()
		{
			if(XVar.Pack(this.allPagesMode))
			{
				return null;
			}
			this.queryPageNo = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagenumber")]);
			if(XVar.Pack(!(XVar)(this.queryPageNo)))
			{
				this.queryPageNo = new XVar(1);
			}
			this.queryPageSize = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_pagesize")]);
			if(XVar.Pack(!(XVar)(this.queryPageSize)))
			{
				this.queryPageSize = XVar.Clone(this.pSet.getInitialPageSize());
			}
			if(this.queryPageSize < 0)
			{
				this.allPagesMode = new XVar(true);
			}
			return null;
		}
		protected virtual XVar openQuery()
		{
			dynamic listarray = null;
			prepareCustomListQueryLegacySorting();
			calcPageSizeAndNumber();
			listarray = new XVar(false);
			if(XVar.Pack(this.eventsObject.exists(new XVar("ListQuery"))))
			{
				listarray = XVar.Clone(this.eventsObject.ListQuery((XVar)(this.searchClauseObj), (XVar)(this.customFieldForSort), (XVar)(this.customHowFieldSort), (XVar)(this.masterTable), (XVar)(this.masterKeys), (XVar)(this.selection), (XVar)(this.queryPageSize), (XVar)(this.queryPageNo), this));
			}
			if(!XVar.Equals(XVar.Pack(listarray), XVar.Pack(false)))
			{
				this.recordset = XVar.Clone(listarray);
			}
			else
			{
				if(XVar.Pack(this.allPagesMode))
				{
					this.recordset = XVar.Clone(this.connection.query((XVar)(this.querySQL)));
				}
				else
				{
					this.recordset = XVar.Clone(this.connection.queryPage((XVar)(this.querySQL), (XVar)(this.queryPageNo), (XVar)(this.queryPageSize), (XVar)(this.totalRowCount)));
				}
			}
			return null;
		}
		protected virtual XVar setMapParams()
		{
			dynamic fieldsArr = XVar.Array();
			fieldsArr = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> f in this.pSet.getPrinterFields().GetEnumerator())
			{
				fieldsArr.InitAndSetArrayItem(new XVar("fName", f.Value, "viewFormat", this.pSet.getViewFormat((XVar)(f.Value))), null);
			}
			setGoogleMapsParams((XVar)(fieldsArr));
			return null;
		}
		public virtual XVar process()
		{
			if(this.pdfMode == "convert")
			{
				convertToPDF();
				return null;
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessPrint"))))
			{
				this.eventsObject.BeforeProcessPrint(this);
			}
			prepareJsSettings();
			addButtonHandlers();
			addCommonJs();
			commonAssign();
			setMapParams();
			buildSQL();
			processGridTabs();
			calcRowCount();
			openQuery();
			doFirstPageAssignments();
			if(XVar.Pack(!(XVar)(this.splitByRecords)))
			{
				fillGridPage();
				showTotals();
				displayMasterTableInfo();
				this.body.InitAndSetArrayItem(this.pageBody, "data", null);
			}
			else
			{
				dynamic masterAdded = null;
				masterAdded = new XVar(false);
				while(XVar.Pack(true))
				{
					if(XVar.Pack(!(XVar)(masterAdded)))
					{
						displayMasterTableInfo();
						masterAdded = new XVar(true);
					}
					else
					{
						this.pageBody.InitAndSetArrayItem(false, "container_master");
						this.pageBody.InitAndSetArrayItem(false, "container_pdf");
					}
					fillGridPage();
					if(XVar.Pack(EOF()))
					{
						break;
					}
					wrapPageBody();
					this.body.InitAndSetArrayItem(this.pageBody, "data", null);
					++(this.pageNo);
					this.pageBody = XVar.Clone(XVar.Array());
				}
				showTotals();
				wrapPageBody();
				this.body.InitAndSetArrayItem(this.pageBody, "data", null);
			}
			this.xt.assign(new XVar("pagecount"), (XVar)(this.pageNo));
			doCommonAssignments();
			addCustomCss();
			displayPrintPage();
			return null;
		}
		protected virtual XVar wrapPageBody()
		{
			if(XVar.Pack(this.pdfMode))
			{
				this.pageBody.InitAndSetArrayItem("<div class=\"rp-page\">", "begin");
			}
			else
			{
				this.pageBody.InitAndSetArrayItem("<div class=\"rp-presplitpage rp-page\">", "begin");
			}
			this.pageBody.InitAndSetArrayItem("</div>", "end");
			return null;
		}
		protected virtual XVar showTotals()
		{
			dynamic record = XVar.Array();
			if(XVar.Pack(!(XVar)(this.totalsFields)))
			{
				return null;
			}
			record = XVar.Clone(XVar.Array());
			this.pageBody.InitAndSetArrayItem(true, "totals_record");
			foreach (KeyValuePair<XVar, dynamic> tf in this.totalsFields.GetEnumerator())
			{
				dynamic total = null;
				total = XVar.Clone(CommonFunctions.GetTotals((XVar)(tf.Value["fName"]), (XVar)(this.totals[tf.Value["fName"]]), (XVar)(tf.Value["totalsType"]), (XVar)(tf.Value["numRows"]), (XVar)(tf.Value["viewFormat"]), new XVar(Constants.PAGE_PRINT), (XVar)(this.pSet)));
				this.pageBody.InitAndSetArrayItem(total, MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(tf.Value["fName"])), "_total"));
				record.InitAndSetArrayItem(true, MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(tf.Value["fName"])), "_showtotal"));
			}
			this.pageBody.InitAndSetArrayItem(new XVar("data", new XVar(0, record)), "totals_row");
			return null;
		}
		protected virtual XVar EOF()
		{
			dynamic currentPageSize = null;
			currentPageSize = XVar.Clone(this.queryPageSize);
			if((XVar)(!(XVar)(this.allPagesMode))  && (XVar)(this.pSet.getRecordsLimit()))
			{
				currentPageSize = XVar.Clone(this.pSet.getRecordsLimit() - this.queryPageSize * (this.queryPageNo - 1));
			}
			else
			{
				if(XVar.Pack(this.allPagesMode))
				{
					currentPageSize = XVar.Clone(limitRowCount((XVar)(this.totalRowCount)));
				}
			}
			if(currentPageSize <= this.fetchedRecordCount)
			{
				return true;
			}
			readNextRecordInternal();
			if(XVar.Pack(this._eof))
			{
				return true;
			}
			return false;
		}
		protected virtual XVar readNextRecordInternal()
		{
			dynamic data = null;
			if(XVar.Pack(this._eof))
			{
				return null;
			}
			if(XVar.Pack(this.nextRecord))
			{
				return null;
			}
			while(XVar.Pack(true))
			{
				if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
				{
					data = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(this.recordset), this));
				}
				else
				{
					data = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.recordset.fetchAssoc())));
				}
				if(XVar.Pack(!(XVar)(data)))
				{
					this._eof = new XVar(true);
					return null;
				}
				if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessRowPrint"))))
				{
					if(XVar.Pack(!(XVar)(this.eventsObject.BeforeProcessRowPrint((XVar)(data), this))))
					{
						continue;
					}
				}
				this.nextRecord = XVar.Clone(data);
				return null;
			}
			return null;
		}
		protected virtual XVar readNextRecord()
		{
			dynamic data = null;
			if(XVar.Pack(EOF()))
			{
				return false;
			}
			++(this.fetchedRecordCount);
			data = XVar.Clone(this.nextRecord);
			this.nextRecord = new XVar(false);
			return data;
		}
		protected virtual XVar buildGridRecord(dynamic _param_data, dynamic row)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic i = null, keyFields = XVar.Array(), keylink = null, printFields = XVar.Array(), record = XVar.Array();
			genId();
			record = XVar.Clone(XVar.Array());
			record.InitAndSetArrayItem(MVCFunctions.Concat("data-record-id=\"", this.recId, "\""), "recordattrs");
			countTotals((XVar)(this.totals), (XVar)(data));
			keyFields = XVar.Clone(this.pSet.getTableKeys());
			keylink = new XVar("");
			i = new XVar(0);
			for(;i < MVCFunctions.count(keyFields); i++)
			{
				keylink = MVCFunctions.Concat(keylink, "&key", i + 1, "=", MVCFunctions.runner_htmlspecialchars((XVar)(MVCFunctions.RawUrlEncode((XVar)(data[keyFields[i]])))));
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeMoveNextPrint"))))
			{
				this.eventsObject.BeforeMoveNextPrint((XVar)(data), (XVar)(row), (XVar)(record), this);
			}
			printFields = this.pSet.getPrinterFields();
			i = new XVar(0);
			for(;i < MVCFunctions.count(printFields); i++)
			{
				record.InitAndSetArrayItem(showDBValue((XVar)(printFields[i]), (XVar)(data), (XVar)(keylink)), MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(printFields[i])), "_value"));
				setRowClassNames((XVar)(record), (XVar)(printFields[i]));
			}
			spreadRowStyles((XVar)(data), (XVar)(row), (XVar)(record));
			setRowCssRules((XVar)(record));
			record.InitAndSetArrayItem(true, "grid_recordheader");
			record.InitAndSetArrayItem(true, "grid_vrecord");
			return record;
		}
		protected virtual XVar showGridHeader(dynamic _param_columns)
		{
			#region pass-by-value parameters
			dynamic columns = XVar.Clone(_param_columns);
			#endregion

			dynamic i = null, rfooter = XVar.Array(), rheader = XVar.Array();
			foreach (KeyValuePair<XVar, dynamic> f in this.pSet.getPrinterFields().GetEnumerator())
			{
				dynamic gf = null;
				gf = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(f.Value)));
				this.pageBody.InitAndSetArrayItem(true, MVCFunctions.Concat(gf, "_fieldheadercolumn"));
				this.pageBody.InitAndSetArrayItem(true, MVCFunctions.Concat(gf, "_fieldheader"));
				this.pageBody.InitAndSetArrayItem(fieldClass((XVar)(f.Value)), MVCFunctions.Concat(gf, "_class"));
				this.pageBody.InitAndSetArrayItem(true, MVCFunctions.Concat(gf, "_fieldcolumn"));
				this.pageBody.InitAndSetArrayItem(true, MVCFunctions.Concat(gf, "_fieldfootercolumn"));
			}
			this.pageBody.InitAndSetArrayItem(new XVar("data", XVar.Array()), "record_header");
			this.pageBody.InitAndSetArrayItem(new XVar("data", XVar.Array()), "record_footer");
			i = new XVar(0);
			for(;i < columns; i++)
			{
				rheader = XVar.Clone(XVar.Array());
				rfooter = XVar.Clone(XVar.Array());
				if(i < columns - 1)
				{
					rheader.InitAndSetArrayItem(true, "endrecordheader_block");
					rfooter.InitAndSetArrayItem(true, "endrecordheader_block");
				}
				this.pageBody.InitAndSetArrayItem(rheader, "record_header", "data", null);
				this.pageBody.InitAndSetArrayItem(rfooter, "record_footer", "data", null);
			}
			this.pageBody.InitAndSetArrayItem(true, "grid_header");
			this.pageBody.InitAndSetArrayItem(true, "grid_footer");
			return null;
		}
		protected virtual XVar fillGridPage()
		{
			dynamic builtDetails = null, col = null, data = null, recno = null, recordsPrinted = null, row = XVar.Array();
			this.pageBody.InitAndSetArrayItem(XVar.Array(), "grid_row");
			this.pageBody.InitAndSetArrayItem(XVar.Array(), "grid_row", "data");
			recno = new XVar(0);
			recordsPrinted = new XVar(0);
			row = XVar.Clone(XVar.Array());
			col = new XVar(0);
			while(XVar.Pack(data = XVar.Clone(readNextRecord())))
			{
				row.InitAndSetArrayItem(XVar.Array(), "details");
				if(XVar.Pack(!(XVar)(col)))
				{
					row = XVar.Clone(XVar.Array());
					row.InitAndSetArrayItem(XVar.Array(), "grid_record");
					row.InitAndSetArrayItem(XVar.Array(), "grid_record", "data");
					row.InitAndSetArrayItem(XVar.Array(), "details_record");
					row.InitAndSetArrayItem(XVar.Array(), "details_record", "data");
				}
				else
				{
					row.InitAndSetArrayItem(true, "grid_record", "data", col - 1, "endrecord_block");
					row.InitAndSetArrayItem(true, "details_record", "data", col - 1, "endrecord_block");
					row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
					row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
				}
				if((XVar)(this.printGridLayout == Constants.gltVERTICAL)  || (XVar)(this.recsPerRowPrint != 1))
				{
					row.InitAndSetArrayItem(buildGridRecord((XVar)(data), (XVar)(row)), "grid_record", "data", null);
					builtDetails = XVar.Clone(buildDetails((XVar)(data)));
					if(XVar.Pack(builtDetails))
					{
						row.InitAndSetArrayItem(new XVar("details_table", new XVar("data", builtDetails)), "details_record", "data", null);
						row.InitAndSetArrayItem(true, "details_row");
					}
				}
				else
				{
					dynamic builtrow = XVar.Array();
					builtrow = XVar.Clone(buildGridRecord((XVar)(data), (XVar)(row)));
					foreach (KeyValuePair<XVar, dynamic> value in builtrow.GetEnumerator())
					{
						row.InitAndSetArrayItem(value.Value, value.Key);
					}
					row.InitAndSetArrayItem(true, "grid_record");
					builtDetails = XVar.Clone(buildDetails((XVar)(data)));
					if(XVar.Pack(builtDetails))
					{
						row.InitAndSetArrayItem(true, "details_record");
						row.InitAndSetArrayItem(new XVar("data", builtDetails), "details_table");
						row.InitAndSetArrayItem(true, "details_row");
					}
				}
				++(col);
				++(recno);
				if(this.recsPerRowPrint <= col)
				{
					row.InitAndSetArrayItem(true, "grid_recordspace", "data", null);
					row.InitAndSetArrayItem(true, "grid_rowspace");
					this.pageBody.InitAndSetArrayItem(row, "grid_row", "data", null);
					col = new XVar(0);
				}
				if((XVar)(this.splitByRecords)  && (XVar)(this.splitByRecords <= recno))
				{
					break;
				}
			}
			if(XVar.Pack(col))
			{
				if((XVar)((XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT)  && (XVar)(builtDetails))  && (XVar)((XVar)(this.printGridLayout == Constants.gltVERTICAL)  || (XVar)(this.recsPerRowPrint != 1)))
				{
					row.InitAndSetArrayItem("bs-print-details-clear", "details_record", "data", 0, "bs_clear_class");
				}
				this.pageBody.InitAndSetArrayItem(row, "grid_row", "data", null);
			}
			showGridHeader((XVar)((XVar.Pack(this.recsPerRowPrint < recno) ? XVar.Pack(this.recsPerRowPrint) : XVar.Pack(recno))));
			this.pageBody.InitAndSetArrayItem(this.pageNo, "pageno");
			return null;
		}
		public virtual XVar doCommonAssignments()
		{
			this.body["begin"] = MVCFunctions.Concat(this.body["begin"], CommonFunctions.GetBaseScriptsForPage(new XVar(false)));
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			this.xt.assign(new XVar("grid_block"), new XVar(true));
			this.xt.assign(new XVar("page_number"), new XVar(true));
			if(XVar.Pack(!(XVar)(this.splitByRecords)))
			{
				this.xt.assign(new XVar("divideintopages_block"), new XVar(true));
			}
			if((XVar)(!(XVar)(this.pdfMode))  && (XVar)((XVar)(!(XVar)(this.splitByRecords))  || (XVar)(this.pSet.isPrinterPagePDF())))
			{
				this.xt.assign(new XVar("printbuttons"), new XVar(true));
			}
			this.xt.assign(new XVar("printheader"), new XVar(true));
			if(1 < MVCFunctions.count(this.gridTabs))
			{
				dynamic curTabId = null;
				curTabId = XVar.Clone(getCurrentTabId());
				this.xt.assign(new XVar("printtabheader"), new XVar(true));
				this.xt.assign(new XVar("printtabheader_text"), (XVar)(getTabTitle((XVar)(curTabId))));
			}
			return null;
		}
		protected virtual XVar prepareJsSettings()
		{
			if(XVar.Pack(CommonFunctions.isRTL()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isRTL");
			}
			if(XVar.Pack(this.pSet.isPrinterPagePDF()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "printerPagePDF");
			}
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPageOrientation(), "tableSettings", this.tName, "printerPageOrientation");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPageScale(), "tableSettings", this.tName, "printerPageScale");
			this.jsSettings.InitAndSetArrayItem(this.pSet.isPrinterPageFitToPage(), "tableSettings", this.tName, "isPrinterPageFitToPage");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterSplitRecords(), "tableSettings", this.tName, "printerSplitRecords");
			this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPDFSplitRecords(), "tableSettings", this.tName, "printerPDFSplitRecords");
			if(XVar.Pack(this.printGridLayout))
			{
				this.jsSettings.InitAndSetArrayItem(this.printGridLayout, "tableSettings", this.tName, "printGridLayout");
			}
			if(XVar.Pack(this.pSet.isAllowShowHideFields()))
			{
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isAllowShowHideFields");
			}
			if((XVar)((XVar)(this.pSet.isAllowFieldsReordering())  && (XVar)(this.printGridLayout == Constants.gltHORIZONTAL))  && (XVar)(this.recsPerRowPrint == 1))
			{
				dynamic columnOrder = null, logger = null;
				this.jsSettings.InitAndSetArrayItem(true, "tableSettings", this.tName, "isAllowFieldsReordering");
				logger = XVar.Clone(new paramsLogger((XVar)(this.tName), new XVar(Constants.FORDER_PARAMS_TYPE)));
				columnOrder = XVar.Clone(logger.getData());
				if(XVar.Pack(columnOrder))
				{
					this.jsSettings.InitAndSetArrayItem(columnOrder, "tableSettings", this.tName, "columnOrder");
				}
			}
			return null;
		}
		public virtual XVar convertToPDF()
		{
			preparePDFDimensions();
			buildPdf((XVar)(this.pSet.isLandscapePrinterPagePDFOrientation()), (XVar)(this.pdfWidth), (XVar)(this.pdfContent));
			return null;
		}
		public virtual XVar preparePDFDisplay()
		{
			if(XVar.Pack(!(XVar)(this.pdfMode)))
			{
				return null;
			}
			AddCSSFile(new XVar("styles/defaultPDF.css"));
			if(this.pdfMode == "prepare")
			{
				this.jsSettings.InitAndSetArrayItem(this.pSet.isLandscapePrinterPagePDFOrientation(), "tableSettings", this.tName, "pdfPrinterPageOrientation");
				setProxyValue(new XVar("createPdf"), new XVar(1));
				this.jsSettings.InitAndSetArrayItem(this.pSet.isPrinterPagePDFFitToPage(), "tableSettings", this.tName, "pdfFitToPage");
				this.jsSettings.InitAndSetArrayItem(this.pSet.getPrinterPagePDFScale(), "tableSettings", this.tName, "pdfPrinterPageScale");
			}
			return null;
		}
		public virtual XVar displayPrintPage()
		{
			dynamic templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowPrint"))))
			{
				this.eventsObject.BeforeShowPrint((XVar)(this.xt), ref templatefile, this);
			}
			display((XVar)(this.templatefile));
			return null;
		}
		protected virtual XVar preparePDFDimensions()
		{
			if(XVar.Pack(this.pSet.isPrinterPagePDFFitToPage()))
			{
				return null;
			}
			if(XVar.Pack(!(XVar)(this.pSet.isLandscapePrinterPagePDFOrientation())))
			{
				this.pdfWidth = XVar.Clone((Constants.PDF_PAGE_WIDTH * 100) / this.pSet.getPrinterPagePDFScale());
			}
			else
			{
				this.pdfWidth = XVar.Clone((Constants.PDF_PAGE_HEIGHT * 100) / this.pSet.getPrinterPagePDFScale());
			}
			return null;
		}
		public virtual XVar doFirstPageAssignments()
		{
			if((XVar)(this.pSet.isPrinterPagePDF())  && (XVar)(!(XVar)(this.pdfMode)))
			{
				this.pageBody.InitAndSetArrayItem(true, "pdflink_block");
			}
			return null;
		}
		protected virtual XVar buildPdf(dynamic _param__landscape, dynamic _param__pagewidth, dynamic _param__page)
		{
			#region pass-by-value parameters
			dynamic _landscape = XVar.Clone(_param__landscape);
			dynamic _pagewidth = XVar.Clone(_param__pagewidth);
			dynamic _page = XVar.Clone(_param__page);
			#endregion

			dynamic landscape = null, page = null, pagewidth = null;
			landscape = XVar.Clone(_landscape);
			pagewidth = XVar.Clone(_pagewidth);
			page = XVar.Clone(_page);
			return null;
		}
		public override XVar hideField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			dynamic gf = null;
			gf = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fieldName)));
			foreach (KeyValuePair<XVar, dynamic> value in this.body["data"].GetEnumerator())
			{
				this.body["data"][value.Key].Remove(MVCFunctions.Concat(gf, "_fieldheadercolumn"));
				this.body["data"][value.Key].Remove(MVCFunctions.Concat(gf, "_fieldcolumn"));
				this.body["data"][value.Key].Remove(MVCFunctions.Concat(gf, "_fieldfootercolumn"));
			}
			return null;
		}
		public override XVar showField(dynamic _param_fieldName)
		{
			#region pass-by-value parameters
			dynamic fieldName = XVar.Clone(_param_fieldName);
			#endregion

			dynamic gf = null;
			gf = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(fieldName)));
			foreach (KeyValuePair<XVar, dynamic> value in this.body["data"].GetEnumerator())
			{
				this.body.InitAndSetArrayItem(true, "data", value.Key, MVCFunctions.Concat(gf, "_fieldheadercolumn"));
				this.body.InitAndSetArrayItem(true, "data", value.Key, MVCFunctions.Concat(gf, "_fieldcolumn"));
				this.body.InitAndSetArrayItem(true, "data", value.Key, MVCFunctions.Concat(gf, "_fieldfootercolumn"));
			}
			return null;
		}
		protected virtual XVar buildDetails(dynamic _param_data)
		{
			#region pass-by-value parameters
			dynamic data = XVar.Clone(_param_data);
			#endregion

			dynamic details = XVar.Array(), multipleDetails = null;
			details = XVar.Clone(XVar.Array());
			multipleDetails = XVar.Clone(1 < MVCFunctions.count(this.detailTables));
			foreach (KeyValuePair<XVar, dynamic> dt in this.detailTables.GetEnumerator())
			{
				dynamic dTable = null, dtableArrParams = XVar.Array(), mkeys = XVar.Array(), tSet = null, tType = null;
				dTable = XVar.Clone(CommonFunctions.GetTableByShort((XVar)(dt.Value)));
				mkeys = XVar.Clone(this.pSet.getMasterKeysByDetailTable((XVar)(dTable)));
				if(XVar.Pack(!(XVar)(mkeys)))
				{
					continue;
				}
				tSet = XVar.Clone(this.pSet.getTable((XVar)(dTable)));
				tType = XVar.Clone(tSet.getTableType());
				dtableArrParams = XVar.Clone(XVar.Array());
				dtableArrParams = XVar.Clone(XVar.Array());
				dtableArrParams.InitAndSetArrayItem(1, "id");
				dtableArrParams.InitAndSetArrayItem(new XTempl(), "xt");
				dtableArrParams.InitAndSetArrayItem(dTable, "tName");
				dtableArrParams.InitAndSetArrayItem(multipleDetails, "multipleDetails");
				if(tType == Constants.PAGE_REPORT)
				{
					dynamic i = null;
					dtableArrParams.InitAndSetArrayItem(Constants.PAGE_RPRINT, "pageType");
					dtableArrParams.InitAndSetArrayItem(true, "isDetail");
					dtableArrParams.InitAndSetArrayItem(this.tName, "masterTable");
					dtableArrParams.InitAndSetArrayItem(XVar.Array(), "masterKeysReq");
					i = new XVar(0);
					foreach (KeyValuePair<XVar, dynamic> mkey in mkeys.GetEnumerator())
					{
						i++;
						dtableArrParams.InitAndSetArrayItem(data[mkey.Value], "masterKeysReq", i);
					}
				}
				else
				{
					dtableArrParams.InitAndSetArrayItem(Constants.PAGE_PRINT, "pageType");
					dtableArrParams.InitAndSetArrayItem(this.tName, "printMasterTable");
					dtableArrParams.InitAndSetArrayItem(XVar.Array(), "printMasterKeys");
					foreach (KeyValuePair<XVar, dynamic> mkey in mkeys.GetEnumerator())
					{
						dtableArrParams.InitAndSetArrayItem(data[mkey.Value], "printMasterKeys", null);
					}
				}
				details.InitAndSetArrayItem(new XVar("details", XTempl.create_method_assignment(new XVar("showDetails"), this, (XVar)(dtableArrParams))), null);
			}
			return details;
		}
		public virtual XVar showDetails(dynamic _param_params)
		{
			#region pass-by-value parameters
			dynamic var_params = XVar.Clone(_param_params);
			#endregion

			dynamic detailsObject = null;
			if(var_params["pageType"] == Constants.PAGE_RPRINT)
			{
				detailsObject = XVar.Clone(new ReportPrintPage((XVar)(var_params)));
				detailsObject.init();
				detailsObject.processDetailPrint();
			}
			else
			{
				detailsObject = XVar.Clone(new PrintPage_Details((XVar)(var_params)));
				detailsObject.init();
				detailsObject.process();
			}
			return null;
		}
		protected override XVar getColumnsToHide()
		{
			return getCombinedHiddenColumns();
		}
		public override XVar fieldClass(dynamic _param_f)
		{
			#region pass-by-value parameters
			dynamic f = XVar.Clone(_param_f);
			#endregion

			dynamic ret = null;
			ret = XVar.Clone(base.fieldClass((XVar)(f)));
			if(!XVar.Equals(XVar.Pack(MVCFunctions.array_search((XVar)(MVCFunctions.GoodFieldName((XVar)(f))), (XVar)(this.hideColumns))), XVar.Pack(false)))
			{
				ret = MVCFunctions.Concat(ret, " bs-hidden-column");
			}
			return ret;
		}
		protected override XVar getSubsetSQLComponents()
		{
			dynamic selectedRecords = XVar.Array(), sql = XVar.Array();
			sql = XVar.Clone(XVar.Array());
			selectedRecords = XVar.Clone(getSelectedRecords());
			if(!XVar.Equals(XVar.Pack(selectedRecords), XVar.Pack(null)))
			{
				dynamic selectedWhereParts = XVar.Array();
				sql.InitAndSetArrayItem(this.gQuery.getSqlComponents(), "sqlParts");
				selectedWhereParts = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> keys in selectedRecords.GetEnumerator())
				{
					selectedWhereParts.InitAndSetArrayItem(CommonFunctions.KeyWhere((XVar)(keys.Value)), null);
				}
				sql.InitAndSetArrayItem(MVCFunctions.implode(new XVar(" or "), (XVar)(selectedWhereParts)), "mandatoryWhere", null);
				if(0 == MVCFunctions.count(selectedRecords))
				{
					sql.InitAndSetArrayItem("1=0", "mandatoryWhere", null);
				}
			}
			else
			{
				sql = XVar.Clone(base.getSubsetSQLComponents());
			}
			if(this.connection.dbType == Constants.nDATABASE_DB2)
			{
				sql["sqlParts"]["head"] = MVCFunctions.Concat(sql["sqlParts"]["head"], ", ROW_NUMBER() over () as DB2_ROW_NUMBER ");
			}
			sql.InitAndSetArrayItem(SecuritySQL(new XVar("Export"), (XVar)(this.tName)), "mandatoryWhere", null);
			return sql;
		}
		protected virtual XVar buildSQL()
		{
			dynamic orderClause = null, orderbyModifiedInEvent = null, sql = XVar.Array(), strSQLbak = null, whereModifiedInEvent = null;
			sql = XVar.Clone(getSubsetSQLComponents());
			orderClause = XVar.Clone(getOrderByClause());
			GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
			this.countSQL = XVar.Clone(GlobalVars.strSQL);
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, orderClause);
			strSQLbak = XVar.Clone(GlobalVars.strSQL);
			whereModifiedInEvent = new XVar(false);
			orderbyModifiedInEvent = new XVar(false);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryPrint"))))
			{
				dynamic strWhereBak = null, tstrOrderBy = null, tstrWhereClause = null;
				tstrWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				strWhereBak = XVar.Clone(tstrWhereClause);
				tstrOrderBy = XVar.Clone(orderClause);
				this.eventsObject.BeforeQueryPrint((XVar)(GlobalVars.strSQL), ref tstrWhereClause, ref tstrOrderBy, this);
				whereModifiedInEvent = XVar.Clone(tstrWhereClause != strWhereBak);
				orderbyModifiedInEvent = XVar.Clone(tstrOrderBy != orderClause);
				orderClause = XVar.Clone(tstrOrderBy);
				if(XVar.Pack(whereModifiedInEvent))
				{
					GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, tstrWhereClause)), (XVar)(sql["mandatoryHaving"])));
					this.countSQL = XVar.Clone(GlobalVars.strSQL);
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, orderClause);
				}
				else
				{
					if(XVar.Pack(orderbyModifiedInEvent))
					{
						GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
						this.countSQL = XVar.Clone(GlobalVars.strSQL);
						GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, orderClause);
					}
				}
			}
			CommonFunctions.LogInfo((XVar)(GlobalVars.strSQL));
			this.querySQL = XVar.Clone(GlobalVars.strSQL);
			return null;
		}
	}
}
