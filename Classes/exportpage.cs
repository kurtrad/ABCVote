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
	public partial class ExportPage : RunnerPage
	{
		public dynamic exportType = XVar.Pack("");
		public dynamic action = XVar.Pack("");
		public dynamic records = XVar.Pack("");
		protected dynamic textFormattingType;
		public dynamic useRawValues = XVar.Pack(false);
		public dynamic csvDelimiter = XVar.Pack(",");
		public dynamic selectedFields = XVar.Array();
		protected static bool skipExportPageCtor = false;
		public ExportPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipExportPageCtor)
			{
				skipExportPageCtor = false;
				return;
			}
			this.formBricks.InitAndSetArrayItem("exportheader", "header");
			this.formBricks.InitAndSetArrayItem("exportbuttons", "footer");
			assignFormFooterAndHeaderBricks(new XVar(true));
			if(XVar.Pack(this.pSet.chekcExportDelimiterSelection()))
			{
				this.jsSettings.InitAndSetArrayItem(this.pSet.getExportDelimiter(), "tableSettings", this.tName, "csvDelimiter");
			}
			this.textFormattingType = XVar.Clone(this.pSet.getExportTxtFormattingType());
			this.useRawValues = XVar.Clone((XVar)(this.useRawValues)  || (XVar)(this.textFormattingType == Constants.EXPORT_RAW));
			if((XVar)((XVar)(this.exportType)  && (XVar)(this.useRawValues))  && (XVar)(this.textFormattingType == Constants.EXPORT_FORMATTED))
			{
				this.useRawValues = new XVar(false);
			}
			if(XVar.Pack(!(XVar)(this.selectedFields)))
			{
				this.selectedFields = XVar.Clone(this.pSet.getExportFields());
			}
			if(XVar.Pack(!(XVar)(this.searchClauseObj)))
			{
				this.searchClauseObj = XVar.Clone(getSearchObject());
			}
			if(XVar.Pack(this.selection))
			{
				this.jsSettings.InitAndSetArrayItem(getSelection(), "tableSettings", this.tName, "selection");
			}
		}
		public virtual XVar process()
		{
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessExport"))))
			{
				this.eventsObject.BeforeProcessExport(this);
			}
			if(XVar.Pack(this.exportType))
			{
				buildSQL();
				exportByType();
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
				return null;
			}
			fillSettings();
			doCommonAssignments();
			addButtonHandlers();
			addCommonJs();
			displayExportPage();
			return null;
		}
		public override XVar addCommonJs()
		{
			base.addCommonJs();
			if((XVar)(this.pSet.checkExportFieldsSelection())  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				AddCSSFile(new XVar("include/chosen/bootstrap-chosen.css"));
				AddJSFile(new XVar("include/chosen/chosen.jquery.js"));
			}
			return null;
		}
		protected virtual XVar doCommonAssignments()
		{
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			if(this.mode == Constants.EXPORT_SIMPLE)
			{
				this.body.InitAndSetArrayItem(CommonFunctions.GetBaseScriptsForPage(new XVar(false)), "begin");
				this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
				this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			}
			else
			{
				this.xt.assign(new XVar("cancel_button"), new XVar(true));
			}
			this.xt.assign(new XVar("groupExcel"), new XVar(true));
			this.xt.assign(new XVar("exportlink_attrs"), (XVar)(MVCFunctions.Concat("id=\"saveButton", this.id, "\"")));
			if((XVar)(this.pSet.checkExportFieldsSelection())  && (XVar)(getLayoutVersion() == Constants.BOOTSTRAP_LAYOUT))
			{
				this.xt.assign(new XVar("choosefields"), new XVar(true));
				this.xt.assign(new XVar("exportFieldsCtrl"), (XVar)(getChooseFieldsCtrlMarkup()));
			}
			if((XVar)(!(XVar)(this.selection))  || (XVar)(!(XVar)(MVCFunctions.count(this.selection))))
			{
				this.xt.assign(new XVar("rangeheader_block"), new XVar(true));
				this.xt.assign(new XVar("range_block"), new XVar(true));
			}
			if(this.textFormattingType == Constants.EXPORT_BOTH)
			{
				this.xt.assign(new XVar("exportformat"), new XVar(true));
			}
			return null;
		}
		protected virtual XVar getChooseFieldsCtrlMarkup()
		{
			dynamic options = XVar.Array();
			options = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in this.pSet.getExportFields().GetEnumerator())
			{
				options.InitAndSetArrayItem(MVCFunctions.Concat("<option value=\"", MVCFunctions.runner_htmlspecialchars((XVar)(field.Value)), "\" selected=\"selected\">", MVCFunctions.runner_htmlspecialchars((XVar)(this.pSet.label((XVar)(field.Value)))), "</option>"), null);
			}
			return MVCFunctions.Concat("<select name=\"exportFields\" multiple style=\"width: 100%;\" data-placeholder=\"", "Please select", "\" id=\"exportFields", this.id, "\">", MVCFunctions.implode(new XVar(""), (XVar)(options)), "</select>");
		}
		protected virtual XVar exportByType()
		{
			dynamic listarray = null, myPage = null, nPageSize = null, rs = null;
			myPage = new XVar(1);
			nPageSize = new XVar(0);
			if(this.records == "page")
			{
				myPage = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.tName, "_pagenumber")]);
				if(XVar.Pack(!(XVar)(myPage)))
				{
					myPage = new XVar(1);
				}
				nPageSize = XVar.Clone((int)XSession.Session[MVCFunctions.Concat(this.tName, "_pagesize")]);
				if(XVar.Pack(!(XVar)(nPageSize)))
				{
					nPageSize = XVar.Clone(this.pSet.getInitialPageSize());
				}
				if(nPageSize < XVar.Pack(0))
				{
					nPageSize = new XVar(0);
				}
			}
			listarray = new XVar(null);
			if(XVar.Pack(this.eventsObject.exists(new XVar("ListQuery"))))
			{
				dynamic orderClause = null, orderFieldsData = XVar.Array();
				orderClause = XVar.Clone(OrderClause.createFromPage(this));
				orderFieldsData = XVar.Clone(orderClause.getOrderFieldsData());
				listarray = XVar.Clone(this.eventsObject.ListQuery((XVar)(this.searchClauseObj), (XVar)(orderFieldsData["fieldsForSort"]), (XVar)(orderFieldsData["howToSortData"]), (XVar)(this.masterTable), (XVar)(this.masterKeysReq), (XVar)(getSelectedRecords()), (XVar)(nPageSize), (XVar)(myPage), this));
			}
			if(listarray != null)
			{
				rs = XVar.Clone(listarray);
			}
			else
			{
				dynamic _rs = null;
				_rs = XVar.Clone(this.connection.queryPage((XVar)(this.querySQL), (XVar)(myPage), (XVar)(nPageSize), (XVar)(XVar.Pack(0) < nPageSize)));
				rs = XVar.Clone(_rs.getQueryHandle());
			}
			MVCFunctions.runner_set_page_timeout(new XVar(300));
			if(XVar.Pack(this.pSet.getRecordsLimit()))
			{
				nPageSize = XVar.Clone(this.pSet.getRecordsLimit() - (myPage - 1) * nPageSize);
			}
			exportTo((XVar)(this.exportType), (XVar)(rs), (XVar)(nPageSize));
			this.connection.close();
			return null;
		}
		public virtual XVar exportTo(dynamic _param_type, dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic var_type = XVar.Clone(_param_type);
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			if(MVCFunctions.substr((XVar)(var_type), new XVar(0), new XVar(5)) == "excel")
			{
				GlobalVars.locale_info.InitAndSetArrayItem("0", "LOCALE_SGROUPING");
				GlobalVars.locale_info.InitAndSetArrayItem("0", "LOCALE_SMONGROUPING");
				CommonFunctions.ExportToExcel((XVar)(rs), (XVar)(nPageSize), (XVar)(this.eventsObject), (XVar)(this.cipherer), this);
				return null;
			}
			if(var_type == "word")
			{
				ExportToWord((XVar)(rs), (XVar)(nPageSize));
				return null;
			}
			if(var_type == "xml")
			{
				ExportToXML((XVar)(rs), (XVar)(nPageSize));
				return null;
			}
			if(var_type == "csv")
			{
				GlobalVars.locale_info.InitAndSetArrayItem("0", "LOCALE_SGROUPING");
				GlobalVars.locale_info.InitAndSetArrayItem(".", "LOCALE_SDECIMAL");
				GlobalVars.locale_info.InitAndSetArrayItem("0", "LOCALE_SMONGROUPING");
				GlobalVars.locale_info.InitAndSetArrayItem(".", "LOCALE_SMONDECIMALSEP");
				ExportToCSV((XVar)(rs), (XVar)(nPageSize));
			}
			return null;
		}
		public virtual XVar ExportToWord(dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			MVCFunctions.Header("Content-Type", "application/vnd.ms-word");
			MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", CommonFunctions.GetTableURL((XVar)(this.tName)), ".doc")));
			MVCFunctions.Echo("<html>");
			MVCFunctions.Echo(MVCFunctions.Concat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=", GlobalVars.cCharset, "\">"));
			MVCFunctions.Echo("<body>");
			MVCFunctions.Echo("<table border=1>");
			WriteTableData((XVar)(rs), (XVar)(nPageSize));
			MVCFunctions.Echo("</table>");
			MVCFunctions.Echo("</body>");
			MVCFunctions.Echo("</html>");
			return null;
		}
		public virtual XVar ExportToXML(dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			dynamic eventRes = null, i = null, row = null, values = XVar.Array();
			MVCFunctions.Header("Content-Type", "text/xml");
			MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", CommonFunctions.GetTableURL((XVar)(this.tName)), ".xml")));
			if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
			{
				row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
			}
			else
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
			}
			MVCFunctions.Echo(MVCFunctions.Concat("<?xml version=\"1.0\" encoding=\"", GlobalVars.cCharset, "\" standalone=\"yes\"?>\r\n"));
			MVCFunctions.Echo("<table>\r\n");
			i = new XVar(0);
			this.viewControls.setForExportVar(new XVar("xml"));
			while((XVar)((XVar)(!(XVar)(nPageSize))  || (XVar)(i < nPageSize))  && (XVar)(row))
			{
				values = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
				{
					dynamic fType = null;
					fType = XVar.Clone(this.pSet.getFieldType((XVar)(field.Value)));
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(fType))))
					{
						values.InitAndSetArrayItem("LONG BINARY DATA - CANNOT BE DISPLAYED", field.Value);
					}
					else
					{
						values.InitAndSetArrayItem(getFormattedFieldValue((XVar)(field.Value), (XVar)(row)), field.Value);
					}
				}
				eventRes = new XVar(true);
				if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeOut"))))
				{
					eventRes = XVar.Clone(this.eventsObject.BeforeOut((XVar)(row), (XVar)(values), this));
				}
				if(XVar.Pack(eventRes))
				{
					i++;
					MVCFunctions.Echo("<row>\r\n");
					foreach (KeyValuePair<XVar, dynamic> val in values.GetEnumerator())
					{
						dynamic field = null;
						field = XVar.Clone(MVCFunctions.runner_htmlspecialchars((XVar)(CommonFunctions.XMLNameEncode((XVar)(val.Key)))));
						MVCFunctions.Echo(MVCFunctions.Concat("<", field, ">"));
						MVCFunctions.Echo(CommonFunctions.xmlencode((XVar)(values[val.Key])));
						MVCFunctions.Echo(MVCFunctions.Concat("</", field, ">\r\n"));
					}
					MVCFunctions.Echo("</row>\r\n");
				}
				if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
				{
					row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
				}
				else
				{
					row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
				}
			}
			MVCFunctions.Echo("</table>\r\n");
			return null;
		}
		public virtual XVar ExportToCSV(dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			dynamic delimiter = null, eventRes = null, headerParts = XVar.Array(), iNumberOfRows = null, row = null, values = XVar.Array();
			if((XVar)(this.pSet.chekcExportDelimiterSelection())  && (XVar)(MVCFunctions.strlen((XVar)(this.csvDelimiter))))
			{
				delimiter = XVar.Clone(this.csvDelimiter);
			}
			else
			{
				delimiter = XVar.Clone(this.pSet.getExportDelimiter());
			}
			MVCFunctions.Header("Content-Type", "application/csv");
			MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", CommonFunctions.GetTableURL((XVar)(this.tName)), ".csv")));
			MVCFunctions.printBOM();
			if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
			{
				row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
			}
			else
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
			}
			headerParts = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
			{
				headerParts.InitAndSetArrayItem(MVCFunctions.Concat("\"", MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(field.Value)), "\""), null);
			}
			MVCFunctions.Echo(MVCFunctions.implode((XVar)(delimiter), (XVar)(headerParts)));
			MVCFunctions.Echo("\r\n");
			this.viewControls.setForExportVar(new XVar("csv"));
			iNumberOfRows = new XVar(0);
			while((XVar)((XVar)(!(XVar)(nPageSize))  || (XVar)(iNumberOfRows < nPageSize))  && (XVar)(row))
			{
				values = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
				{
					dynamic fType = null;
					fType = XVar.Clone(this.pSet.getFieldType((XVar)(field.Value)));
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(fType))))
					{
						values.InitAndSetArrayItem("LONG BINARY DATA - CANNOT BE DISPLAYED", field.Value);
					}
					else
					{
						values.InitAndSetArrayItem(getFormattedFieldValue((XVar)(field.Value), (XVar)(row)), field.Value);
					}
				}
				eventRes = new XVar(true);
				if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeOut"))))
				{
					eventRes = XVar.Clone(this.eventsObject.BeforeOut((XVar)(row), (XVar)(values), this));
				}
				if(XVar.Pack(eventRes))
				{
					dynamic dataRowParts = XVar.Array();
					dataRowParts = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
					{
						dataRowParts.InitAndSetArrayItem(MVCFunctions.Concat("\"", MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(values[field.Value])), "\""), null);
					}
					MVCFunctions.Echo(MVCFunctions.implode((XVar)(delimiter), (XVar)(dataRowParts)));
				}
				iNumberOfRows++;
				if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
				{
					row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
				}
				else
				{
					row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
				}
				if((XVar)((XVar)((XVar)(!(XVar)(nPageSize))  || (XVar)(iNumberOfRows < nPageSize))  && (XVar)(row))  && (XVar)(eventRes))
				{
					MVCFunctions.Echo("\r\n");
				}
			}
			return null;
		}
		public virtual XVar getFormattedFieldValue(dynamic _param_fName, dynamic _param_row)
		{
			#region pass-by-value parameters
			dynamic fName = XVar.Clone(_param_fName);
			dynamic row = XVar.Clone(_param_row);
			#endregion

			if(XVar.Pack(this.useRawValues))
			{
				return row[fName];
			}
			return getExportValue((XVar)(fName), (XVar)(row));
		}
		protected virtual XVar WriteTableData(dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			dynamic eventRes = null, fType = null, iNumberOfRows = null, row = null, totalFieldsData = XVar.Array(), totals = XVar.Array(), totalsFields = XVar.Array(), values = XVar.Array();
			totalFieldsData = XVar.Clone(this.pSet.getTotalsFields());
			if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
			{
				row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
			}
			else
			{
				row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
			}
			MVCFunctions.Echo("<tr>");
			if(this.exportType == "excel")
			{
				foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
				{
					MVCFunctions.Echo(MVCFunctions.Concat("<td style=\"width: 100\" x:str>", CommonFunctions.PrepareForExcel((XVar)(this.pSet.label((XVar)(field.Value)))), "</td>"));
				}
			}
			else
			{
				foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
				{
					MVCFunctions.Echo(MVCFunctions.Concat("<td>", this.pSet.label((XVar)(field.Value)), "</td>"));
				}
			}
			MVCFunctions.Echo("</tr>");
			totals = XVar.Clone(XVar.Array());
			totalsFields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> data in totalFieldsData.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(data.Value["fName"]), (XVar)(this.selectedFields)))))
				{
					continue;
				}
				totals.InitAndSetArrayItem(new XVar("value", 0, "numRows", 0), data.Value["fName"]);
				totalsFields.InitAndSetArrayItem(new XVar("fName", data.Value["fName"], "totalsType", data.Value["totalsType"], "viewFormat", this.pSet.getViewFormat((XVar)(data.Value["fName"]))), null);
			}
			iNumberOfRows = new XVar(0);
			this.viewControls.setForExportVar(new XVar("export"));
			while((XVar)((XVar)(!(XVar)(nPageSize))  || (XVar)(iNumberOfRows < nPageSize))  && (XVar)(row))
			{
				CommonFunctions.countTotals((XVar)(totals), (XVar)(totalsFields), (XVar)(row));
				values = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
				{
					fType = XVar.Clone(this.pSet.getFieldType((XVar)(field.Value)));
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(fType))))
					{
						values.InitAndSetArrayItem("LONG BINARY DATA - CANNOT BE DISPLAYED", field.Value);
					}
					else
					{
						values.InitAndSetArrayItem(getFormattedFieldValue((XVar)(field.Value), (XVar)(row)), field.Value);
					}
				}
				eventRes = new XVar(true);
				if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeOut"))))
				{
					eventRes = XVar.Clone(this.eventsObject.BeforeOut((XVar)(row), (XVar)(values), this));
				}
				if(XVar.Pack(eventRes))
				{
					iNumberOfRows++;
					MVCFunctions.Echo("<tr>");
					foreach (KeyValuePair<XVar, dynamic> field in this.selectedFields.GetEnumerator())
					{
						dynamic editFormat = null;
						fType = XVar.Clone(this.pSet.getFieldType((XVar)(field.Value)));
						if(XVar.Pack(CommonFunctions.IsCharType((XVar)(fType))))
						{
							if(this.exportType == "excel")
							{
								MVCFunctions.Echo("<td x:str>");
							}
							else
							{
								MVCFunctions.Echo("<td>");
							}
						}
						else
						{
							MVCFunctions.Echo("<td>");
						}
						editFormat = XVar.Clone(this.pSet.getEditFormat((XVar)(field.Value)));
						if(editFormat == Constants.EDIT_FORMAT_LOOKUP_WIZARD)
						{
							if(XVar.Pack(this.pSet.NeedEncode((XVar)(field.Value))))
							{
								if(this.exportType == "excel")
								{
									MVCFunctions.Echo(CommonFunctions.PrepareForExcel((XVar)(values[field.Value])));
								}
								else
								{
									MVCFunctions.Echo(values[field.Value]);
								}
							}
							else
							{
								MVCFunctions.Echo(values[field.Value]);
							}
						}
						else
						{
							if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(fType))))
							{
								MVCFunctions.Echo(values[field.Value]);
							}
							else
							{
								if((XVar)(editFormat == Constants.FORMAT_CUSTOM)  || (XVar)(this.pSet.isUseRTE((XVar)(field.Value))))
								{
									MVCFunctions.Echo(values[field.Value]);
								}
								else
								{
									if(XVar.Pack(CommonFunctions.NeedQuotes((XVar)(field.Value))))
									{
										if(this.exportType == "excel")
										{
											MVCFunctions.Echo(CommonFunctions.PrepareForExcel((XVar)(values[field.Value])));
										}
										else
										{
											MVCFunctions.Echo(values[field.Value]);
										}
									}
									else
									{
										MVCFunctions.Echo(values[field.Value]);
									}
								}
							}
						}
						MVCFunctions.Echo("</td>");
					}
					MVCFunctions.Echo("</tr>");
				}
				if(XVar.Pack(this.eventsObject.exists(new XVar("ListFetchArray"))))
				{
					row = XVar.Clone(this.eventsObject.ListFetchArray((XVar)(rs), this));
				}
				else
				{
					row = XVar.Clone(this.cipherer.DecryptFetchedArray((XVar)(this.connection.fetch_array((XVar)(rs)))));
				}
			}
			if(XVar.Pack(MVCFunctions.count(totalFieldsData)))
			{
				MVCFunctions.Echo("<tr>");
				foreach (KeyValuePair<XVar, dynamic> data in totalFieldsData.GetEnumerator())
				{
					if(XVar.Pack(!(XVar)(MVCFunctions.in_array((XVar)(data.Value["fName"]), (XVar)(this.selectedFields)))))
					{
						continue;
					}
					MVCFunctions.Echo("<td>");
					if(XVar.Pack(MVCFunctions.strlen((XVar)(data.Value["totalsType"]))))
					{
						if(data.Value["totalsType"] == "COUNT")
						{
							MVCFunctions.Echo(MVCFunctions.Concat("Count", ": "));
						}
						else
						{
							if(data.Value["totalsType"] == "TOTAL")
							{
								MVCFunctions.Echo(MVCFunctions.Concat("Total", ": "));
							}
							else
							{
								if(data.Value["totalsType"] == "AVERAGE")
								{
									MVCFunctions.Echo(MVCFunctions.Concat("Average", ": "));
								}
							}
						}
						MVCFunctions.Echo(MVCFunctions.runner_htmlspecialchars((XVar)(CommonFunctions.GetTotals((XVar)(data.Value["fName"]), (XVar)(totals[data.Value["fName"]]["value"]), (XVar)(data.Value["totalsType"]), (XVar)(totals[data.Value["fName"]]["numRows"]), (XVar)(this.pSet.getViewFormat((XVar)(data.Value["fName"]))), new XVar(Constants.PAGE_EXPORT), (XVar)(this.pSet)))));
					}
					MVCFunctions.Echo("</td>");
				}
				MVCFunctions.Echo("</tr>");
			}
			return null;
		}
		public virtual XVar ExportToExcel_old(dynamic _param_rs, dynamic _param_nPageSize)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			#endregion

			MVCFunctions.Header("Content-Type", "application/vnd.ms-excel");
			MVCFunctions.Header((XVar)(MVCFunctions.Concat("Content-Disposition: attachment;Filename=", CommonFunctions.GetTableURL((XVar)(this.tName)), ".xls")));
			MVCFunctions.Echo("<html>");
			MVCFunctions.Echo("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
			MVCFunctions.Echo(MVCFunctions.Concat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=", GlobalVars.cCharset, "\">"));
			MVCFunctions.Echo("<body>");
			MVCFunctions.Echo("<table border=1>");
			WriteTableData((XVar)(rs), (XVar)(nPageSize));
			MVCFunctions.Echo("</table>");
			MVCFunctions.Echo("</body>");
			MVCFunctions.Echo("</html>");
			return null;
		}
		protected virtual XVar displayExportPage()
		{
			dynamic templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowExport"))))
			{
				this.eventsObject.BeforeShowExport((XVar)(this.xt), ref templatefile, this);
			}
			if(this.mode == Constants.EXPORT_POPUP)
			{
				this.xt.assign(new XVar("footer"), new XVar(false));
				this.xt.assign(new XVar("header"), new XVar(false));
				this.xt.assign(new XVar("body"), (XVar)(this.body));
				displayAJAX((XVar)(templatefile), (XVar)(this.id + 1));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			display((XVar)(templatefile));
			return null;
		}
		public static XVar readModeFromRequest()
		{
			if(XVar.Pack(MVCFunctions.postvalue(new XVar("onFly"))))
			{
				return Constants.EXPORT_POPUP;
			}
			return Constants.EXPORT_SIMPLE;
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
			GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, orderClause);
			strSQLbak = XVar.Clone(GlobalVars.strSQL);
			whereModifiedInEvent = new XVar(false);
			orderbyModifiedInEvent = new XVar(false);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeQueryExport"))))
			{
				dynamic strWhereBak = null, tstrOrderBy = null, tstrWhereClause = null;
				tstrWhereClause = XVar.Clone(SQLQuery.combineCases((XVar)(new XVar(0, SQLQuery.combineCases((XVar)(sql["mandatoryWhere"]), new XVar("and")), 1, SQLQuery.combineCases((XVar)(sql["optionalWhere"]), new XVar("or")))), new XVar("and")));
				strWhereBak = XVar.Clone(tstrWhereClause);
				tstrOrderBy = XVar.Clone(orderClause);
				this.eventsObject.BeforeQueryExport((XVar)(GlobalVars.strSQL), ref tstrWhereClause, ref tstrOrderBy, this);
				whereModifiedInEvent = XVar.Clone(tstrWhereClause != strWhereBak);
				orderbyModifiedInEvent = XVar.Clone(tstrOrderBy != orderClause);
				orderClause = XVar.Clone(tstrOrderBy);
				if(XVar.Pack(whereModifiedInEvent))
				{
					GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(new XVar(0, tstrWhereClause)), (XVar)(sql["mandatoryHaving"])));
					GlobalVars.strSQL = MVCFunctions.Concat(GlobalVars.strSQL, orderClause);
				}
				else
				{
					if(XVar.Pack(orderbyModifiedInEvent))
					{
						GlobalVars.strSQL = XVar.Clone(SQLQuery.buildSQL((XVar)(sql["sqlParts"]), (XVar)(sql["mandatoryWhere"]), (XVar)(sql["mandatoryHaving"]), (XVar)(sql["optionalWhere"]), (XVar)(sql["optionalHaving"])));
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
