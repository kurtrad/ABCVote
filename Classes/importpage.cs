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
	public partial class ImportPage : RunnerPage
	{
		public dynamic audit = XVar.Pack(null);
		public dynamic action;
		public dynamic importType;
		public dynamic importText;
		public dynamic useXHR = XVar.Pack(false);
		public dynamic importData;
		public dynamic currentDateFormat;
		protected static bool skipImportPageCtor = false;
		public ImportPage(dynamic var_params)
			:base((XVar)var_params)
		{
			if(skipImportPageCtor)
			{
				skipImportPageCtor = false;
				return;
			}
			this.audit = XVar.Clone(CommonFunctions.GetAuditObject((XVar)(this.tName)));
			this.jsSettings.InitAndSetArrayItem(getImportfieldsLabels(), "tableSettings", this.tName, "importFieldsLables");
		}
		protected virtual XVar getImportfieldsLabels()
		{
			dynamic importFields = XVar.Array(), importFieldsLabels = XVar.Array();
			importFieldsLabels = XVar.Clone(XVar.Array());
			importFields = XVar.Clone(this.pSet.getImportFields());
			foreach (KeyValuePair<XVar, dynamic> importField in importFields.GetEnumerator())
			{
				importFieldsLabels.InitAndSetArrayItem(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(importField.Value)))), importField.Value);
			}
			return importFieldsLabels;
		}
		public virtual XVar process()
		{
			if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(this.action)))))
			{
				removeOldTemporaryFiles();
			}
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeProcessImport"))))
			{
				this.eventsObject.BeforeProcessImport(this);
			}
			if(this.action == "importPreview")
			{
				prepareAndSentPreviewData();
				return null;
			}
			if(this.action == "importData")
			{
				runImportAndSendResultReport();
				return null;
			}
			if(this.action == "downloadReport")
			{
				downloadReport();
				return null;
			}
			if(this.action == "downloadUnprocessed")
			{
				downloadUnprocessed();
				return null;
			}
			doCommonAssignments();
			addButtonHandlers();
			addCommonJs();
			displayImportPage();
			return null;
		}
		protected virtual XVar prepareAndSentPreviewData()
		{
			dynamic returnJSON = null, rnrTempFileName = null, rnrTempImportFilePath = null, var_response = XVar.Array();
			var_response = XVar.Clone(XVar.Array());
			rnrTempFileName = XVar.Clone(getImportTempFileName());
			if(this.importType == "text")
			{
				var_response.InitAndSetArrayItem(getPreviewDataFromText((XVar)(this.importText)), "previewData");
				rnrTempImportFilePath = XVar.Clone(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat("templates_c/", rnrTempFileName, ".csv"))));
				MVCFunctions.runner_save_textfile((XVar)(rnrTempImportFilePath), (XVar)(this.importText));
			}
			else
			{
				dynamic ext = null, importFileData = null, importTempFileName = null;
				ext = XVar.Clone(ImportFunctions.getImportFileExtension((XVar)(MVCFunctions.Concat("importFile", this.id))));
				importTempFileName = XVar.Clone(ImportFunctions.getTempImportFileName((XVar)(MVCFunctions.Concat("importFile", this.id))));
				var_response.InitAndSetArrayItem(getPreviewDataFromFile((XVar)(importTempFileName), (XVar)(ext)), "previewData");
				importFileData = XVar.Clone(ImportFunctions.getImportFileData((XVar)(MVCFunctions.Concat("importFile", this.id))));
				rnrTempImportFilePath = XVar.Clone(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat("templates_c/", rnrTempFileName, ".", ext))));
				MVCFunctions.upload_File((XVar)(importFileData), (XVar)(rnrTempImportFilePath));
			}
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempImportFilePath")] = rnrTempImportFilePath;
			returnJSON = XVar.Clone(CommonFunctions.printJSON((XVar)(var_response), (XVar)(this.useXHR)));
			if(returnJSON != false)
			{
				MVCFunctions.Echo(returnJSON);
			}
			else
			{
				MVCFunctions.Echo("The file you're trying to import cannot be parsed");
			}
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar runImportAndSendResultReport()
		{
			dynamic resultData = XVar.Array(), rnrTempImportFilePath = null;
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeImport"))))
			{
				dynamic message = null;
				message = new XVar("");
				if(XVar.Equals(XVar.Pack(this.eventsObject.BeforeImport(this, ref message)), XVar.Pack(false)))
				{
					MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(new XVar("failed", true, "message", message))));
					MVCFunctions.ob_flush();
					HttpContext.Current.Response.End();
					throw new RunnerInlineOutputException();
				}
			}
			rnrTempImportFilePath = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempImportFilePath")]);
			resultData = XVar.Clone(ImportFromFile((XVar)(rnrTempImportFilePath), (XVar)(this.importData)));
			MVCFunctions.runner_delete_file((XVar)(rnrTempImportFilePath));
			if(XVar.Pack(this.eventsObject.exists(new XVar("AfterImport"))))
			{
				this.eventsObject.AfterImport((XVar)(resultData["totalRecordsNumber"]), (XVar)(resultData["unprocessedRecordsNumber"]), this);
			}
			XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempImportLogFilePath")] = resultData["logFilePath"];
			if(XVar.Pack(resultData["unprocessedRecordsNumber"]))
			{
				XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempDataFilePath")] = resultData["unprocessedFilePath"];
			}
			MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(resultData)));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar downloadReport()
		{
			dynamic logFilePath = null;
			logFilePath = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempImportLogFilePath")]);
			if(XVar.Pack(!(XVar)(MVCFunctions.myfile_exists((XVar)(logFilePath)))))
			{
				dynamic data = null;
				data = XVar.Clone(new XVar("success", false));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(data)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			MVCFunctions.Header("Content-Type", "text/plain");
			MVCFunctions.Header("Content-Disposition", "attachment;Filename=importLog.txt");
			MVCFunctions.Header("Cache-Control", "private");
			MVCFunctions.printfile((XVar)(logFilePath));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		protected virtual XVar downloadUnprocessed()
		{
			dynamic dataFilePath = null;
			dataFilePath = XVar.Clone(XSession.Session[MVCFunctions.Concat(this.sessionPrefix, "_tempDataFilePath")]);
			if(XVar.Pack(!(XVar)(MVCFunctions.myfile_exists((XVar)(dataFilePath)))))
			{
				dynamic data = null;
				data = XVar.Clone(new XVar("success", false));
				MVCFunctions.Echo(CommonFunctions.printJSON((XVar)(data)));
				MVCFunctions.ob_flush();
				HttpContext.Current.Response.End();
				throw new RunnerInlineOutputException();
			}
			MVCFunctions.Header("Content-Type", "application/csv");
			MVCFunctions.Header("Content-Disposition", "attachment;Filename=unpocessedData.csv");
			MVCFunctions.printfile((XVar)(dataFilePath));
			MVCFunctions.ob_flush();
			HttpContext.Current.Response.End();
			throw new RunnerInlineOutputException();
			return null;
		}
		public virtual XVar doCommonAssignments()
		{
			this.xt.assign(new XVar("id"), (XVar)(this.id));
			this.body.InitAndSetArrayItem(CommonFunctions.GetBaseScriptsForPage(new XVar(false)), "begin");
			this.body.InitAndSetArrayItem(XTempl.create_method_assignment(new XVar("assignBodyEnd"), this), "end");
			this.xt.assignbyref(new XVar("body"), (XVar)(this.body));
			return null;
		}
		public override XVar clearSessionKeys()
		{
			base.clearSessionKeys();
			if((XVar)(!(XVar)(MVCFunctions.POSTSize()))  && (XVar)(!(XVar)(MVCFunctions.GETSize())))
			{
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_tempImportFilePath"));
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_tempImportLogFilePath"));
				XSession.Session.Remove(MVCFunctions.Concat(this.sessionPrefix, "_tempDataFilePath"));
			}
			return null;
		}
		public virtual XVar getPreviewDataFromFile(dynamic _param_filePath, dynamic _param_ext)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			dynamic ext = XVar.Clone(_param_ext);
			#endregion

			dynamic normalizedExt = null;
			normalizedExt = XVar.Clone(MVCFunctions.strtoupper((XVar)(ext)));
			if((XVar)(normalizedExt == "XLS")  || (XVar)(normalizedExt == "XLSX"))
			{
				return getPreviewDataFromExcel((XVar)(filePath), (XVar)(ext));
			}
			return getPreviewDataFromCsv((XVar)(filePath));
		}
		protected virtual XVar getPreviewDataFromExcel(dynamic _param_filePath, dynamic _param_ext)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			dynamic ext = XVar.Clone(_param_ext);
			#endregion

			dynamic fieldsData = null, fileHandle = null, headerFieldsFromExcel = null, previewData = XVar.Array();
			fileHandle = XVar.Clone(ImportFunctions.openImportExcelFile((XVar)(filePath), (XVar)(ext)));
			headerFieldsFromExcel = XVar.Clone(ImportFunctions.getImportExcelFields((XVar)(fileHandle)));
			fieldsData = XVar.Clone(getCorrespondingImportFieldsData((XVar)(headerFieldsFromExcel)));
			previewData = XVar.Clone(ImportFunctions.getPreviewDataFromExcel((XVar)(fileHandle), (XVar)(fieldsData)));
			previewData.InitAndSetArrayItem(fieldsData, "fieldsData");
			return previewData;
		}
		public virtual XVar getPreviewDataFromText(dynamic _param_importText)
		{
			#region pass-by-value parameters
			dynamic importText = XVar.Clone(_param_importText);
			#endregion

			dynamic dateFormat = null, delimiter = null, fieldsData = XVar.Array(), fieldsNamesLine = null, firstTwoLinesData = null, hasDTFields = null, headerFieldsFromCSV = null, lines = XVar.Array(), linesData = XVar.Array(), previewData = XVar.Array(), tableData = null;
			lines = XVar.Clone(importExplode((XVar)(importText)));
			lines = XVar.Clone(removeEmptyLines((XVar)(lines)));
			if(XVar.Pack(!(XVar)(MVCFunctions.count(lines))))
			{
				return XVar.Array();
			}
			previewData = XVar.Clone(XVar.Array());
			previewData.InitAndSetArrayItem(true, "CSVPreview");
			firstTwoLinesData = XVar.Clone(MVCFunctions.array_slice((XVar)(lines), new XVar(0), new XVar(2)));
			delimiter = XVar.Clone(getCSVDelimiter((XVar)(firstTwoLinesData)));
			previewData.InitAndSetArrayItem(delimiter, "delimiter");
			fieldsNamesLine = XVar.Clone(lines[0]);
			headerFieldsFromCSV = XVar.Clone(ImportFunctions.parceCSVLine((XVar)(fieldsNamesLine), (XVar)(delimiter)));
			fieldsData = XVar.Clone(getCorrespondingImportFieldsData((XVar)(headerFieldsFromCSV)));
			previewData.InitAndSetArrayItem(fieldsData, "fieldsData");
			hasDTFields = XVar.Clone(hasDateTimeFields((XVar)(fieldsData)));
			linesData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> line in lines.GetEnumerator())
			{
				dynamic elems = XVar.Array();
				if(100 <= MVCFunctions.count(linesData))
				{
					break;
				}
				linesData.InitAndSetArrayItem(line.Value, null);
				if(XVar.Pack(!(XVar)(hasDTFields)))
				{
					continue;
				}
				elems = XVar.Clone(ImportFunctions.parceCSVLine((XVar)(line.Value), (XVar)(delimiter)));
				foreach (KeyValuePair<XVar, dynamic> elem in elems.GetEnumerator())
				{
					if((XVar)((XVar)(fieldsData.KeyExists(elem.Key))  && (XVar)(fieldsData[elem.Key]["dateTimeType"]))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(dateFormat)))))
					{
						dateFormat = XVar.Clone(extractDateFormat((XVar)(elem.Value)));
					}
				}
			}
			if(XVar.Pack(hasDTFields))
			{
				previewData.InitAndSetArrayItem(getImportDateFormat((XVar)(dateFormat)), "dateFormat");
			}
			previewData.InitAndSetArrayItem(tableData, "tableData");
			previewData.InitAndSetArrayItem(linesData, "CSVlinesData");
			return previewData;
		}
		protected virtual XVar removeEmptyLines(dynamic _param_lines)
		{
			#region pass-by-value parameters
			dynamic lines = XVar.Clone(_param_lines);
			#endregion

			dynamic resultLines = XVar.Array();
			resultLines = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> line in lines.GetEnumerator())
			{
				if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(line.Value))))))
				{
					resultLines.InitAndSetArrayItem(line.Value, null);
				}
			}
			return resultLines;
		}
		protected virtual XVar getPreviewDataFromCsv(dynamic _param_filePath)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			#endregion

			dynamic dateFormat = null, delimiter = null, elems = XVar.Array(), fieldsData = XVar.Array(), fieldsNamesLine = null, fileHandle = null, firstTwoLinesData = null, fistNotEmptyLineData = XVar.Array(), hasDTFields = null, headerFieldsFromCSV = null, line = null, linesData = XVar.Array(), previewData = XVar.Array();
			fileHandle = XVar.Clone(ImportFunctions.OpenCSVFile((XVar)(filePath), new XVar("")));
			if(XVar.Equals(XVar.Pack(fileHandle), XVar.Pack(false)))
			{
				return XVar.Array();
			}
			previewData = XVar.Clone(XVar.Array());
			previewData.InitAndSetArrayItem(true, "CSVPreview");
			firstTwoLinesData = XVar.Clone(getNLinesFromFile((XVar)(fileHandle), new XVar(2)));
			delimiter = XVar.Clone(getCSVDelimiter((XVar)(firstTwoLinesData)));
			previewData.InitAndSetArrayItem(delimiter, "delimiter");
			fileHandle = XVar.Clone(ImportFunctions.rewindFilePointerPosition((XVar)(fileHandle), (XVar)(filePath)));
			linesData = XVar.Clone(XVar.Array());
			fistNotEmptyLineData = XVar.Clone(getNLinesFromFile((XVar)(fileHandle), new XVar(1)));
			fieldsNamesLine = XVar.Clone(fistNotEmptyLineData[0]);
			headerFieldsFromCSV = XVar.Clone(ImportFunctions.parceCSVLine((XVar)(fieldsNamesLine), (XVar)(delimiter), new XVar(true)));
			linesData.InitAndSetArrayItem(fieldsNamesLine, null);
			fieldsData = XVar.Clone(getCorrespondingImportFieldsData((XVar)(headerFieldsFromCSV)));
			previewData.InitAndSetArrayItem(fieldsData, "fieldsData");
			hasDTFields = XVar.Clone(hasDateTimeFields((XVar)(fieldsData)));
			while((XVar)(!XVar.Equals(XVar.Pack(line = XVar.Clone(ImportFunctions.getLineFromFile((XVar)(fileHandle), new XVar(1000000)))), XVar.Pack(false)))  && (XVar)(MVCFunctions.count(linesData) < 100))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(line)))))))
				{
					break;
				}
				linesData.InitAndSetArrayItem(line, null);
				if(XVar.Pack(!(XVar)(hasDTFields)))
				{
					continue;
				}
				elems = XVar.Clone(ImportFunctions.parceCSVLine((XVar)(line), (XVar)(delimiter), new XVar(true)));
				foreach (KeyValuePair<XVar, dynamic> elem in elems.GetEnumerator())
				{
					if((XVar)((XVar)(fieldsData.KeyExists(elem.Key))  && (XVar)(fieldsData[elem.Key]["dateTimeType"]))  && (XVar)(!(XVar)(MVCFunctions.strlen((XVar)(dateFormat)))))
					{
						dateFormat = XVar.Clone(extractDateFormat((XVar)(elem.Value)));
					}
				}
			}
			if(XVar.Pack(hasDateTimeFields((XVar)(fieldsData))))
			{
				previewData.InitAndSetArrayItem(getImportDateFormat((XVar)(dateFormat)), "dateFormat");
			}
			previewData.InitAndSetArrayItem(linesData, "CSVlinesData");
			ImportFunctions.CloseCSVFile((XVar)(fileHandle));
			return previewData;
		}
		public static XVar extractDateFormat(dynamic _param_dateString)
		{
			#region pass-by-value parameters
			dynamic dateString = XVar.Clone(_param_dateString);
			#endregion

			dynamic dateComponents = XVar.Array(), dateSeparator = null, format = null, year = null;
			dateComponents = XVar.Clone(CommonFunctions.parsenumbers((XVar)(dateString)));
			if(MVCFunctions.count(dateComponents) < 3)
			{
				return "";
			}
			dateSeparator = XVar.Clone(GlobalVars.locale_info["LOCALE_SDATE"]);
			format = new XVar("");
			if((XVar)((XVar)(31 < dateComponents[0])  && (XVar)(ImportFunctions.testMonth((XVar)(dateComponents[1]))))  && (XVar)(12 <= dateComponents[2]))
			{
				year = XVar.Clone(dateComponents[0]);
				format = XVar.Clone(MVCFunctions.Concat("Y", dateSeparator, "M", dateSeparator, "D"));
			}
			if((XVar)((XVar)(12 <= dateComponents[0])  && (XVar)(ImportFunctions.testMonth((XVar)(dateComponents[1]))))  && (XVar)(31 < dateComponents[2]))
			{
				year = XVar.Clone(dateComponents[3]);
				format = XVar.Clone(MVCFunctions.Concat("D", dateSeparator, "M", dateSeparator, "Y"));
			}
			if((XVar)((XVar)(ImportFunctions.testMonth((XVar)(dateComponents[0])))  && (XVar)(12 <= dateComponents[1]))  && (XVar)(31 < dateComponents[2]))
			{
				year = XVar.Clone(dateComponents[3]);
				format = XVar.Clone(MVCFunctions.Concat("M", dateSeparator, "D", dateSeparator, "Y"));
			}
			if(XVar.Pack(format))
			{
				format = XVar.Clone(MVCFunctions.str_replace(new XVar("Y"), (XVar)((XVar.Pack(year < 100) ? XVar.Pack("YY") : XVar.Pack("YYYY"))), (XVar)(format)));
			}
			return format;
		}
		protected virtual XVar getNLinesFromFile(dynamic _param_fileHandle, dynamic _param_N)
		{
			#region pass-by-value parameters
			dynamic fileHandle = XVar.Clone(_param_fileHandle);
			dynamic N = XVar.Clone(_param_N);
			#endregion

			dynamic line = null, lines = XVar.Array();
			lines = XVar.Clone(XVar.Array());
			while(MVCFunctions.count(lines) < N)
			{
				line = XVar.Clone(ImportFunctions.getLineFromFile((XVar)(fileHandle), new XVar(1000000)));
				if(XVar.Equals(XVar.Pack(line), XVar.Pack(false)))
				{
					break;
				}
				line = XVar.Clone(MVCFunctions.cutBOM((XVar)(line)));
				if(XVar.Pack(MVCFunctions.strlen((XVar)(MVCFunctions.trim((XVar)(line))))))
				{
					lines.InitAndSetArrayItem(line, null);
				}
			}
			return lines;
		}
		protected virtual XVar getCSVDelimiter(dynamic _param_firstTwoLines)
		{
			#region pass-by-value parameters
			dynamic firstTwoLines = XVar.Clone(_param_firstTwoLines);
			#endregion

			dynamic delimiter = null, delimiters = XVar.Array(), delimitersData = XVar.Array(), maxNumOfElems = null;
			delimiters = XVar.Clone(new XVar(0, ",", 1, ";", 2, "\t", 3, " "));
			delimitersData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> delim in delimiters.GetEnumerator())
			{
				delimitersData.InitAndSetArrayItem(XVar.Array(), delim.Value);
				foreach (KeyValuePair<XVar, dynamic> line in firstTwoLines.GetEnumerator())
				{
					dynamic elemsNumber = null;
					elemsNumber = XVar.Clone(MVCFunctions.count(ImportFunctions.parceCSVLine((XVar)(line.Value), (XVar)(delim.Value))));
					if(elemsNumber <= 1)
					{
						break;
					}
					delimitersData.InitAndSetArrayItem(elemsNumber, delim.Value, line.Key);
				}
			}
			delimiter = new XVar(",");
			maxNumOfElems = new XVar(1);
			foreach (KeyValuePair<XVar, dynamic> data in delimitersData.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.count(data.Value))))
				{
					continue;
				}
				if((XVar)((XVar)(MVCFunctions.count(firstTwoLines) == 1)  || (XVar)((XVar)(MVCFunctions.count(firstTwoLines) == 2)  && (XVar)(data.Value[0] == data.Value[1])))  && (XVar)(maxNumOfElems < data.Value[0]))
				{
					maxNumOfElems = XVar.Clone(data.Value[0]);
					delimiter = XVar.Clone(data.Key);
				}
			}
			return delimiter;
		}
		public static XVar hasDateTimeFields(dynamic _param_fieldsData)
		{
			#region pass-by-value parameters
			dynamic fieldsData = XVar.Clone(_param_fieldsData);
			#endregion

			return true;
			foreach (KeyValuePair<XVar, dynamic> fieldData in fieldsData.GetEnumerator())
			{
				if(XVar.Pack(fieldData.Value["dateTimeType"]))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getCorrespondingImportFieldsData(dynamic _param_headerFields)
		{
			#region pass-by-value parameters
			dynamic headerFields = XVar.Clone(_param_headerFields);
			#endregion

			dynamic importFields = XVar.Array(), tempFieldArray = XVar.Array(), tempGNamesArray = XVar.Array(), tempLabelArray = XVar.Array();
			importFields = XVar.Clone(this.pSet.getImportFields());
			tempFieldArray = XVar.Clone(XVar.Array());
			tempLabelArray = XVar.Clone(XVar.Array());
			tempGNamesArray = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> headerField in headerFields.GetEnumerator())
			{
				foreach (KeyValuePair<XVar, dynamic> importField in importFields.GetEnumerator())
				{
					dynamic dateTimeType = null, gName = null, label = null;
					dateTimeType = XVar.Clone(CommonFunctions.IsDateFieldType((XVar)(this.pSet.getFieldType((XVar)(importField.Value)))));
					if(MVCFunctions.strtoupper((XVar)(headerField.Value)) == MVCFunctions.strtoupper((XVar)(importField.Value)))
					{
						tempFieldArray.InitAndSetArrayItem(importField.Value, headerField.Key, "fName");
						tempFieldArray.InitAndSetArrayItem(dateTimeType, headerField.Key, "dateTimeType");
					}
					gName = XVar.Clone(MVCFunctions.GoodFieldName((XVar)(importField.Value)));
					if(MVCFunctions.strtoupper((XVar)(headerField.Value)) == MVCFunctions.strtoupper((XVar)(MVCFunctions.trim((XVar)(gName)))))
					{
						tempGNamesArray.InitAndSetArrayItem(importField.Value, headerField.Key, "fName");
						tempGNamesArray.InitAndSetArrayItem(dateTimeType, headerField.Key, "dateTimeType");
					}
					label = XVar.Clone(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(this.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(importField.Value)))));
					if(MVCFunctions.strtoupper((XVar)(headerField.Value)) == MVCFunctions.strtoupper((XVar)(MVCFunctions.trim((XVar)(label)))))
					{
						tempLabelArray.InitAndSetArrayItem(importField.Value, headerField.Key, "fName");
						tempLabelArray.InitAndSetArrayItem(dateTimeType, headerField.Key, "dateTimeType");
					}
				}
			}
			if((XVar)((XVar)(!(XVar)(MVCFunctions.count(tempFieldArray)))  && (XVar)(!(XVar)(MVCFunctions.count(tempGNamesArray))))  && (XVar)(!(XVar)(MVCFunctions.count(tempLabelArray))))
			{
				return XVar.Array();
			}
			if((XVar)(MVCFunctions.count(tempLabelArray) <= MVCFunctions.count(tempFieldArray))  && (XVar)(MVCFunctions.count(tempGNamesArray) <= MVCFunctions.count(tempFieldArray)))
			{
				return tempFieldArray;
			}
			if((XVar)(MVCFunctions.count(tempFieldArray) <= MVCFunctions.count(tempLabelArray))  && (XVar)(MVCFunctions.count(tempGNamesArray) <= MVCFunctions.count(tempLabelArray)))
			{
				return tempLabelArray;
			}
			return tempGNamesArray;
		}
		public virtual XVar ImportFromFile(dynamic _param_filePath, dynamic importData)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			#endregion

			dynamic dateFormat = null, fieldsData = null, logFilePath = null, metaData = XVar.Array(), reportFileText = null, resultData = XVar.Array();
			fieldsData = XVar.Clone(refineImportFielsData((XVar)(importData["importFieldsData"])));
			dateFormat = XVar.Clone(ImportFunctions.getRefinedDateFormat((XVar)(getImportDateFormat((XVar)(importData["dateFormat"])))));
			this.currentDateFormat = XVar.Clone(dateFormat);
			if(XVar.Pack(importData["CSV"]))
			{
				metaData = XVar.Clone(importFromCSV((XVar)(filePath), (XVar)(fieldsData), (XVar)(importData["useHeadersLine"]), (XVar)(importData["delimiter"])));
			}
			else
			{
				metaData = XVar.Clone(importFromExcel((XVar)(filePath), (XVar)(fieldsData), (XVar)(importData["useHeadersLine"])));
			}
			resultData = XVar.Clone(XVar.Array());
			resultData.InitAndSetArrayItem(getBasicReportText((XVar)(metaData["totalRecords"]), (XVar)(metaData["addedRecords"]), (XVar)(metaData["updatedRecords"])), "reportText");
			resultData.InitAndSetArrayItem(MVCFunctions.count(metaData["errorMessages"]), "unprocessedRecordsNumber");
			resultData.InitAndSetArrayItem(metaData["totalRecords"] - resultData["unprocessedRecordsNumber"], "totalRecordsNumber");
			reportFileText = XVar.Clone(getBasicReportText((XVar)(metaData["totalRecords"]), (XVar)(metaData["addedRecords"]), (XVar)(metaData["updatedRecords"]), new XVar(false), new XVar("\r\n"), (XVar)(metaData["errorMessages"]), (XVar)(metaData["unprocessedData"])));
			logFilePath = XVar.Clone(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat("templates_c/", getImportLogFileName(), ".txt"))));
			MVCFunctions.runner_save_file((XVar)(logFilePath), (XVar)(reportFileText));
			resultData.InitAndSetArrayItem(logFilePath, "logFilePath");
			if(XVar.Pack(MVCFunctions.count(metaData["unprocessedData"])))
			{
				dynamic unprocContent = null, unprocFilePath = null;
				unprocFilePath = XVar.Clone(MVCFunctions.getabspath((XVar)(MVCFunctions.Concat("templates_c/", getUnprocessedDataFileName(), ".csv"))));
				unprocContent = XVar.Clone(getUnprocessedDataContent((XVar)(metaData["unprocessedData"])));
				MVCFunctions.runner_save_file((XVar)(unprocFilePath), (XVar)(unprocContent));
				resultData.InitAndSetArrayItem(unprocFilePath, "unprocessedFilePath");
			}
			return resultData;
		}
		protected virtual XVar getImportDateFormat(dynamic _param_dateFormat)
		{
			#region pass-by-value parameters
			dynamic dateFormat = XVar.Clone(_param_dateFormat);
			#endregion

			return (XVar.Pack(!(XVar)(MVCFunctions.strlen((XVar)(dateFormat)))) ? XVar.Pack(GlobalVars.locale_info["LOCALE_SSHORTDATE"]) : XVar.Pack(dateFormat));
		}
		protected virtual XVar refineImportFielsData(dynamic _param_importFiledsData)
		{
			#region pass-by-value parameters
			dynamic importFiledsData = XVar.Clone(_param_importFiledsData);
			#endregion

			dynamic fieldsData = XVar.Array();
			fieldsData = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fData in importFiledsData.GetEnumerator())
			{
				dynamic fName = null;
				fName = XVar.Clone(fData.Value["fName"]);
				if(XVar.Pack(fName))
				{
					fieldsData.InitAndSetArrayItem(new XVar("fName", fName, "type", this.pSet.getFieldType((XVar)(fName))), fData.Key);
				}
			}
			return fieldsData;
		}
		protected virtual XVar importFromCSV(dynamic _param_filePath, dynamic _param_fieldsData, dynamic _param_useFirstLine, dynamic _param_delimiter)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			dynamic fieldsData = XVar.Clone(_param_fieldsData);
			dynamic useFirstLine = XVar.Clone(_param_useFirstLine);
			dynamic delimiter = XVar.Clone(_param_delimiter);
			#endregion

			dynamic addedRecords = null, autoinc = null, elems = XVar.Array(), errorMessages = null, fieldsValuesData = XVar.Array(), fileHandle = null, keys = null, metaData = XVar.Array(), row = null, sql = null, unprocessedData = null, updatedRecords = null;
			metaData = XVar.Clone(XVar.Array());
			metaData.InitAndSetArrayItem(0, "totalRecords");
			fileHandle = XVar.Clone(ImportFunctions.OpenCSVFile((XVar)(filePath), (XVar)(delimiter)));
			if(XVar.Equals(XVar.Pack(fileHandle), XVar.Pack(false)))
			{
				return metaData;
			}
			keys = XVar.Clone(this.pSet.getTableKeys());
			autoinc = XVar.Clone(hasAutoincImportFields((XVar)(fieldsData)));
			errorMessages = XVar.Clone(XVar.Array());
			unprocessedData = XVar.Clone(XVar.Array());
			addedRecords = new XVar(0);
			updatedRecords = new XVar(0);
			if((XVar)(this.connection.dbType == Constants.nDATABASE_MSSQLServer)  && (XVar)(autoinc))
			{
				sql = XVar.Clone(MVCFunctions.Concat("SET IDENTITY_INSERT ", this.connection.addTableWrappers((XVar)(this.strOriginalTableName)), " ON"));
				this.connection.exec((XVar)(sql));
			}
			row = new XVar(0);
			while(!XVar.Equals(XVar.Pack(elems = XVar.Clone(ImportFunctions.GetCSVLine((XVar)(fileHandle), new XVar(1000000), (XVar)(delimiter)))), XVar.Pack(false)))
			{
				if((XVar)(XVar.Equals(XVar.Pack(row), XVar.Pack(0)))  && (XVar)((XVar)(!(XVar)(useFirstLine))  || (XVar)(XVar.Equals(XVar.Pack(useFirstLine), XVar.Pack("false")))))
				{
					row++;
					continue;
				}
				fieldsValuesData = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> elem in elems.GetEnumerator())
				{
					dynamic fType = null, importFieldName = null;
					if(XVar.Pack(!(XVar)(fieldsData.KeyExists(elem.Key))))
					{
						continue;
					}
					importFieldName = XVar.Clone(fieldsData[elem.Key]["fName"]);
					fType = XVar.Clone(fieldsData[elem.Key]["type"]);
					fieldsValuesData.InitAndSetArrayItem(elem.Value, importFieldName);
				}
				importRecord((XVar)(fieldsValuesData), (XVar)(keys), (XVar)(autoinc), ref addedRecords, ref updatedRecords, (XVar)(errorMessages), (XVar)(unprocessedData));
				metaData.InitAndSetArrayItem(metaData["totalRecords"] + 1, "totalRecords");
				row++;
			}
			ImportFunctions.CloseCSVFile((XVar)(fileHandle));
			if((XVar)(this.connection.dbType == Constants.nDATABASE_MSSQLServer)  && (XVar)(autoinc))
			{
				sql = XVar.Clone(MVCFunctions.Concat("SET IDENTITY_INSERT ", this.connection.addTableWrappers((XVar)(this.strOriginalTableName)), " OFF"));
				this.connection.exec((XVar)(sql));
			}
			metaData.InitAndSetArrayItem(addedRecords, "addedRecords");
			metaData.InitAndSetArrayItem(updatedRecords, "updatedRecords");
			metaData.InitAndSetArrayItem(errorMessages, "errorMessages");
			metaData.InitAndSetArrayItem(unprocessedData, "unprocessedData");
			return metaData;
		}
		protected virtual XVar importFromExcel(dynamic _param_filePath, dynamic _param_fieldsData, dynamic _param_useFirstLine)
		{
			#region pass-by-value parameters
			dynamic filePath = XVar.Clone(_param_filePath);
			dynamic fieldsData = XVar.Clone(_param_fieldsData);
			dynamic useFirstLine = XVar.Clone(_param_useFirstLine);
			#endregion

			dynamic autoinc = null, ext = null, fileHandle = null, keys = null, metaData = null, sql = null;
			ext = XVar.Clone(CommonFunctions.getFileExtension((XVar)(filePath)));
			fileHandle = XVar.Clone(ImportFunctions.openImportExcelFile((XVar)(filePath), (XVar)(ext)));
			keys = XVar.Clone(this.pSet.getTableKeys());
			autoinc = XVar.Clone(hasAutoincImportFields((XVar)(fieldsData)));
			metaData = XVar.Clone(ImportFunctions.ImportDataFromExcel((XVar)(fileHandle), (XVar)(fieldsData), (XVar)(keys), this, (XVar)(autoinc), (XVar)(useFirstLine)));
			return metaData;
		}
		protected virtual XVar hasAutoincImportFields(dynamic _param_fieldsData)
		{
			#region pass-by-value parameters
			dynamic fieldsData = XVar.Clone(_param_fieldsData);
			#endregion

			foreach (KeyValuePair<XVar, dynamic> f in fieldsData.GetEnumerator())
			{
				if(XVar.Pack(this.pSet.isAutoincField((XVar)(f.Value["fName"]))))
				{
					return true;
				}
			}
			return false;
		}
		public virtual XVar getInsertSQL(dynamic _param_fields, dynamic _param_fieldsValuesData)
		{
			#region pass-by-value parameters
			dynamic fields = XVar.Clone(_param_fields);
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			#endregion

			dynamic fieldsList = XVar.Array(), valuesList = XVar.Array();
			fieldsList = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in fields.GetEnumerator())
			{
				fieldsList.InitAndSetArrayItem(getTableField((XVar)(field.Value)), null);
			}
			valuesList = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> val in fieldsValuesData.GetEnumerator())
			{
				if(XVar.Pack(!(XVar)(val.Value == null)))
				{
					valuesList.InitAndSetArrayItem(this.cipherer.AddDBQuotes((XVar)(val.Key), (XVar)(val.Value)), null);
				}
				else
				{
					valuesList.InitAndSetArrayItem("NULL", null);
				}
			}
			return MVCFunctions.Concat("insert into ", this.connection.addTableWrappers((XVar)(this.strOriginalTableName)), " (", MVCFunctions.implode(new XVar(","), (XVar)(fieldsList)), ") values (", MVCFunctions.implode(new XVar(","), (XVar)(valuesList)), ")");
		}
		public virtual XVar getUpdateSQL(dynamic _param_notKeyFields, dynamic _param_fieldsValuesData, dynamic _param_updateWhere)
		{
			#region pass-by-value parameters
			dynamic notKeyFields = XVar.Clone(_param_notKeyFields);
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			dynamic updateWhere = XVar.Clone(_param_updateWhere);
			#endregion

			dynamic sql = null, sqlset = XVar.Array();
			sql = XVar.Clone(MVCFunctions.Concat("update ", this.connection.addTableWrappers((XVar)(this.strOriginalTableName)), " set "));
			sqlset = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in notKeyFields.GetEnumerator())
			{
				dynamic dbFName = null;
				dbFName = XVar.Clone(getTableField((XVar)(field.Value)));
				if(XVar.Pack(!(XVar)(fieldsValuesData[field.Value] == null)))
				{
					sqlset.InitAndSetArrayItem(MVCFunctions.Concat(dbFName, "=", this.cipherer.AddDBQuotes((XVar)(field.Value), (XVar)(fieldsValuesData[field.Value]))), null);
				}
				else
				{
					sqlset.InitAndSetArrayItem(MVCFunctions.Concat(dbFName, " = NULL"), null);
				}
			}
			sql = MVCFunctions.Concat(sql, MVCFunctions.implode(new XVar(", "), (XVar)(sqlset)), " where ", updateWhere);
			return sql;
		}
		public virtual XVar getUpdateSQLWhere(dynamic _param_keyFields, dynamic _param_fieldsValuesData)
		{
			#region pass-by-value parameters
			dynamic keyFields = XVar.Clone(_param_keyFields);
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			#endregion

			dynamic where = XVar.Array();
			where = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in keyFields.GetEnumerator())
			{
				where.InitAndSetArrayItem(MVCFunctions.Concat(getFieldSQL((XVar)(field.Value)), "=", this.cipherer.AddDBQuotes((XVar)(field.Value), (XVar)(fieldsValuesData[field.Value]))), null);
			}
			return MVCFunctions.implode(new XVar(" and "), (XVar)(where));
		}
		protected virtual XVar prepareFiledsValuesData(dynamic _param_fieldsValuesData)
		{
			#region pass-by-value parameters
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			#endregion

			dynamic refinedFieldsValuesData = XVar.Array();
			refinedFieldsValuesData = XVar.Clone(XVar.Array());
			setUpdatedLatLng((XVar)(fieldsValuesData));
			foreach (KeyValuePair<XVar, dynamic> val in fieldsValuesData.GetEnumerator())
			{
				dynamic value = null, var_type = null;
				var_type = XVar.Clone(this.pSet.getFieldType((XVar)(val.Key)));
				if(XVar.Pack(CommonFunctions.IsTimeType((XVar)(var_type))))
				{
					value = XVar.Clone(CommonFunctions.prepare_for_db((XVar)(val.Key), (XVar)(val.Value), new XVar("time"), new XVar(""), (XVar)(this.tName)));
					if(0 < MVCFunctions.strlen((XVar)(value)))
					{
						refinedFieldsValuesData.InitAndSetArrayItem(value, val.Key);
					}
					else
					{
						refinedFieldsValuesData.InitAndSetArrayItem(null, val.Key);
					}
					continue;
				}
				if(XVar.Pack(CommonFunctions.IsDateFieldType((XVar)(var_type))))
				{
					if(XVar.Pack(!(XVar)(CommonFunctions.dateInDbFormat((XVar)(val.Value)))))
					{
						value = XVar.Clone(CommonFunctions.localdatetime2db((XVar)(val.Value), (XVar)(this.currentDateFormat)));
					}
					else
					{
						value = XVar.Clone(val.Value);
					}
					if(0 < MVCFunctions.strlen((XVar)(value)))
					{
						refinedFieldsValuesData.InitAndSetArrayItem(value, val.Key);
					}
					else
					{
						refinedFieldsValuesData.InitAndSetArrayItem(null, val.Key);
					}
					continue;
				}
				if(XVar.Pack(!(XVar)(CommonFunctions.IsNumberType((XVar)(var_type)))))
				{
					refinedFieldsValuesData.InitAndSetArrayItem(val.Value, val.Key);
					continue;
				}
				value = XVar.Clone(MVCFunctions.str_replace(new XVar(","), new XVar("."), (XVar)(val.Value)));
				if(0 < MVCFunctions.strlen((XVar)(value)))
				{
					if(!XVar.Equals(XVar.Pack(MVCFunctions.strpos((XVar)(value), (XVar)(GlobalVars.locale_info["LOCALE_SCURRENCY"]))), XVar.Pack(false)))
					{
						dynamic matches = XVar.Array();
						value = XVar.Clone(MVCFunctions.str_replace((XVar)(new XVar(0, GlobalVars.locale_info["LOCALE_SCURRENCY"], 1, " ")), (XVar)(new XVar(0, "", 1, "")), (XVar)(value)));
						matches = XVar.Clone(XVar.Array());
						if(XVar.Pack(MVCFunctions.preg_match(new XVar("/^\\((.*)\\)$/"), (XVar)(value), (XVar)(matches))))
						{
							value = XVar.Clone((-1) * matches[1]);
						}
					}
					refinedFieldsValuesData.InitAndSetArrayItem(0 + value, val.Key);
				}
				else
				{
					refinedFieldsValuesData.InitAndSetArrayItem(null, val.Key);
				}
			}
			return refinedFieldsValuesData;
		}
		public virtual XVar importRecord(dynamic _param_fieldsValuesData, dynamic _param_keys, dynamic _param_isIdentityOffNeeded, ref dynamic addedRecords, ref dynamic updatedRecords, dynamic errorMessages, dynamic unprocessedData)
		{
			#region pass-by-value parameters
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			dynamic keys = XVar.Clone(_param_keys);
			dynamic isIdentityOffNeeded = XVar.Clone(_param_isIdentityOffNeeded);
			#endregion

			dynamic data = XVar.Array(), errorMessage = null, failed = null, fieldNames = null, keyFieldsNames = null, rawvalues = null, sql = null, updateWhere = null;
			rawvalues = XVar.Clone(fieldsValuesData);
			fieldsValuesData = XVar.Clone(prepareFiledsValuesData((XVar)(fieldsValuesData)));
			fieldNames = XVar.Clone(MVCFunctions.array_keys((XVar)(fieldsValuesData)));
			errorMessage = new XVar("");
			failed = new XVar(false);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeInsert"))))
			{
				if(XVar.Equals(XVar.Pack(this.eventsObject.BeforeInsert((XVar)(rawvalues), (XVar)(fieldsValuesData), this, ref errorMessage)), XVar.Pack(false)))
				{
					failed = new XVar(true);
				}
			}
			if(XVar.Pack(!(XVar)(failed)))
			{
				fieldNames = XVar.Clone(MVCFunctions.array_keys((XVar)(fieldsValuesData)));
				sql = XVar.Clone(getInsertSQL((XVar)(fieldNames), (XVar)(fieldsValuesData)));
				if(XVar.Pack(ImportFunctions.db_exec_import((XVar)(sql), (XVar)(this.connection), (XVar)(this.connection.addTableWrappers((XVar)(this.strOriginalTableName))), (XVar)(isIdentityOffNeeded))))
				{
					addedRecords = XVar.Clone(addedRecords + 1);
					if(XVar.Pack(this.audit))
					{
						this.audit.LogAdd((XVar)(this.tName), (XVar)(fieldsValuesData), (XVar)(CommonFunctions.GetKeysArray((XVar)(fieldsValuesData), this, new XVar(true))));
					}
					return null;
				}
				errorMessage = XVar.Clone(this.connection.lastError());
				keyFieldsNames = XVar.Clone(MVCFunctions.array_intersect((XVar)(fieldNames), (XVar)(keys)));
				if((XVar)(!(XVar)(keyFieldsNames))  || (XVar)(MVCFunctions.count(keyFieldsNames) != MVCFunctions.count(keys)))
				{
					failed = new XVar(true);
				}
			}
			if(XVar.Pack(!(XVar)(failed)))
			{
				dynamic getAllUpdatedSQL = null, rs = null;
				updateWhere = XVar.Clone(getUpdateSQLWhere((XVar)(keyFieldsNames), (XVar)(fieldsValuesData)));
				getAllUpdatedSQL = XVar.Clone(MVCFunctions.Concat("select * from ", this.connection.addTableWrappers((XVar)(this.strOriginalTableName)), " where ", updateWhere));
				rs = XVar.Clone(this.connection.querySilent((XVar)(getAllUpdatedSQL)));
				data = new XVar(null);
				if(XVar.Pack(rs))
				{
					data = XVar.Clone(rs.fetchAssoc());
				}
				if(XVar.Pack(!(XVar)(data)))
				{
					failed = new XVar(true);
				}
			}
			if(XVar.Pack(!(XVar)(failed)))
			{
				dynamic notKeyFieldsNames = null;
				notKeyFieldsNames = XVar.Clone(MVCFunctions.array_diff((XVar)(fieldNames), (XVar)(keys)));
				sql = XVar.Clone(getUpdateSQL((XVar)(notKeyFieldsNames), (XVar)(fieldsValuesData), (XVar)(updateWhere)));
				if(XVar.Pack(!(XVar)(ImportFunctions.db_exec_import((XVar)(sql), (XVar)(this.connection), (XVar)(this.connection.addTableWrappers((XVar)(this.strOriginalTableName))), (XVar)(isIdentityOffNeeded)))))
				{
					failed = new XVar(true);
				}
			}
			if(XVar.Pack(!(XVar)(failed)))
			{
				updatedRecords = XVar.Clone(updatedRecords + 1);
				if(XVar.Pack(this.audit))
				{
					dynamic auditOldValues = XVar.Array();
					auditOldValues = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> val in data.GetEnumerator())
					{
						auditOldValues.InitAndSetArrayItem(val.Value, val.Key);
					}
					this.audit.LogEdit((XVar)(this.tName), (XVar)(fieldsValuesData), (XVar)(auditOldValues), (XVar)(CommonFunctions.GetKeysArray((XVar)(fieldsValuesData), this)));
				}
			}
			if(XVar.Pack(failed))
			{
				if(XVar.Pack(!(XVar)(MVCFunctions.count(unprocessedData))))
				{
					unprocessedData.InitAndSetArrayItem(getImportFieldsLogCSVLine((XVar)(fieldNames)), null);
				}
				unprocessedData.InitAndSetArrayItem(parseValuesDataInLogCSVLine((XVar)(rawvalues)), null);
				errorMessages.InitAndSetArrayItem(errorMessage, null);
			}
			return null;
		}
		protected virtual XVar parseValuesDataInLogCSVLine(dynamic _param_fieldsValuesData)
		{
			#region pass-by-value parameters
			dynamic fieldsValuesData = XVar.Clone(_param_fieldsValuesData);
			#endregion

			dynamic values = XVar.Array();
			values = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> value in fieldsValuesData.GetEnumerator())
			{
				dynamic fType = null;
				fType = XVar.Clone(this.pSet.getFieldType((XVar)(value.Key)));
				if(XVar.Pack(!(XVar)(CommonFunctions.IsBinaryType((XVar)(fType)))))
				{
					values.InitAndSetArrayItem(MVCFunctions.Concat("\"", MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(value.Value)), "\""), null);
				}
			}
			return MVCFunctions.implode(new XVar(","), (XVar)(values));
		}
		protected virtual XVar getImportFieldsLogCSVLine(dynamic _param_importFields)
		{
			#region pass-by-value parameters
			dynamic importFields = XVar.Clone(_param_importFields);
			#endregion

			dynamic headerFields = XVar.Array();
			headerFields = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> fName in importFields.GetEnumerator())
			{
				dynamic fType = null;
				fType = XVar.Clone(this.pSet.getFieldType((XVar)(fName.Value)));
				if(XVar.Pack(!(XVar)(CommonFunctions.IsBinaryType((XVar)(fType)))))
				{
					headerFields.InitAndSetArrayItem(MVCFunctions.Concat("\"", MVCFunctions.str_replace(new XVar("\""), new XVar("\"\""), (XVar)(fName.Value)), "\""), null);
				}
			}
			return MVCFunctions.implode(new XVar(","), (XVar)(headerFields));
		}
		protected virtual XVar getUnprocessedDataContent(dynamic _param_unprocessedData)
		{
			#region pass-by-value parameters
			dynamic unprocessedData = XVar.Clone(_param_unprocessedData);
			#endregion

			dynamic content = null, headerLine = null;
			content = XVar.Clone(MVCFunctions.Concat(headerLine, MVCFunctions.implode(new XVar("\r\n"), (XVar)(unprocessedData))));
			return (XVar.Pack(GlobalVars.useUTF8) ? XVar.Pack(MVCFunctions.Concat("﻿", content)) : XVar.Pack(content));
		}
		protected virtual XVar getBasicReportText(dynamic _param_totalRecords, dynamic _param_addedRecords, dynamic _param_updatedRecords, dynamic _param_isNotLogFile = null, dynamic _param_lineBreak = null, dynamic _param_errorMessages = null, dynamic _param_unprocessedData = null)
		{
			#region default values
			if(_param_isNotLogFile as Object == null) _param_isNotLogFile = new XVar(true);
			if(_param_lineBreak as Object == null) _param_lineBreak = new XVar("<br>");
			if(_param_errorMessages as Object == null) _param_errorMessages = new XVar(XVar.Array());
			if(_param_unprocessedData as Object == null) _param_unprocessedData = new XVar(XVar.Array());
			#endregion

			#region pass-by-value parameters
			dynamic totalRecords = XVar.Clone(_param_totalRecords);
			dynamic addedRecords = XVar.Clone(_param_addedRecords);
			dynamic updatedRecords = XVar.Clone(_param_updatedRecords);
			dynamic isNotLogFile = XVar.Clone(_param_isNotLogFile);
			dynamic lineBreak = XVar.Clone(_param_lineBreak);
			dynamic errorMessages = XVar.Clone(_param_errorMessages);
			dynamic unprocessedData = XVar.Clone(_param_unprocessedData);
			#endregion

			dynamic boldBegin = null, boldEnd = null, importedReords = null, notImportedRecords = null, reportText = null;
			importedReords = XVar.Clone(addedRecords + updatedRecords);
			notImportedRecords = XVar.Clone(totalRecords - importedReords);
			boldBegin = new XVar("");
			boldEnd = new XVar("");
			reportText = new XVar("");
			if(XVar.Pack(isNotLogFile))
			{
				boldBegin = new XVar("<b>");
				boldEnd = new XVar("</b>");
			}
			else
			{
				reportText = MVCFunctions.Concat(reportText, "Import into", " ", this.strOriginalTableName, lineBreak, CommonFunctions.str_format_datetime((XVar)(CommonFunctions.db2time((XVar)(MVCFunctions.now())))), lineBreak, lineBreak);
			}
			reportText = MVCFunctions.Concat(reportText, MVCFunctions.mysprintf(new XVar("%s out of %s records processed successfully."), (XVar)(new XVar(0, MVCFunctions.Concat(boldBegin, importedReords, boldEnd), 1, MVCFunctions.Concat(boldBegin, totalRecords, boldEnd)))), lineBreak, MVCFunctions.mysprintf(new XVar("%s records added."), (XVar)(new XVar(0, MVCFunctions.Concat(boldBegin, addedRecords, boldEnd)))), lineBreak, MVCFunctions.mysprintf(new XVar("%s records updated."), (XVar)(new XVar(0, MVCFunctions.Concat(boldBegin, updatedRecords, boldEnd)))), lineBreak);
			if(XVar.Pack(notImportedRecords))
			{
				reportText = MVCFunctions.Concat(reportText, MVCFunctions.mysprintf(new XVar("%s records processed with errors"), (XVar)(new XVar(0, MVCFunctions.Concat(boldBegin, notImportedRecords, boldEnd)))));
			}
			if((XVar)(notImportedRecords)  && (XVar)(MVCFunctions.count(errorMessages)))
			{
				dynamic i = null;
				reportText = MVCFunctions.Concat(reportText, ":");
				i = new XVar(0);
				for(;i < MVCFunctions.count(errorMessages); i++)
				{
					if(XVar.Pack(isNotLogFile))
					{
						reportText = MVCFunctions.Concat(reportText, lineBreak, errorMessages[i]);
					}
					else
					{
						reportText = MVCFunctions.Concat(reportText, lineBreak, lineBreak, errorMessages[i], lineBreak, unprocessedData[i + 1]);
					}
				}
			}
			return reportText;
		}
		public virtual XVar getImportTempFileName()
		{
			return MVCFunctions.Concat("import", getUniqueFileNameSuffix());
		}
		public virtual XVar getImportLogFileName()
		{
			return MVCFunctions.Concat("importLog", getUniqueFileNameSuffix());
		}
		public virtual XVar getUnprocessedDataFileName()
		{
			return MVCFunctions.Concat("importUnprocessed", getUniqueFileNameSuffix());
		}
		protected virtual XVar getUniqueFileNameSuffix()
		{
			dynamic dateMarker = null;
			dateMarker = XVar.Clone(MVCFunctions.getYMDdate((XVar)(MVCFunctions.time())));
			return MVCFunctions.Concat(dateMarker, "_", this.tName, "_", CommonFunctions.generatePassword(new XVar(5)));
		}
		public virtual XVar removeOldTemporaryFiles()
		{
			deleteTemporaryFilesFromDir(new XVar("templates_c/"));
			return null;
		}
		public virtual XVar deleteTemporaryFilesFromDir(dynamic _param_dir)
		{
			#region pass-by-value parameters
			dynamic dir = XVar.Clone(_param_dir);
			#endregion

			dynamic currentTime = null, fileNamesList = XVar.Array(), tempFilesDirectory = null, tempNamePattern = null;
			tempFilesDirectory = XVar.Clone(MVCFunctions.getabspath((XVar)(dir)));
			fileNamesList = XVar.Clone(ImportFunctions.getFileNamesFromDir((XVar)(tempFilesDirectory)));
			currentTime = XVar.Clone(MVCFunctions.strtotime((XVar)(MVCFunctions.now())));
			tempNamePattern = XVar.Clone(MVCFunctions.Concat("/^import.*([\\d]{4}-(0[1-9]|1[0-2])-([0-2][1-9]|3[0-1])).*_", this.tName, "_.{5}\\.\\w+/"));
			foreach (KeyValuePair<XVar, dynamic> fileName in fileNamesList.GetEnumerator())
			{
				dynamic matches = XVar.Array();
				matches = XVar.Clone(XVar.Array());
				if(XVar.Pack(MVCFunctions.preg_match((XVar)(tempNamePattern), (XVar)(fileName.Value), (XVar)(matches))))
				{
					dynamic timeFromFileName = null;
					timeFromFileName = XVar.Clone(MVCFunctions.strtotime((XVar)(matches[1])));
					if((XVar)(!XVar.Equals(XVar.Pack(timeFromFileName), XVar.Pack(false)))  && (XVar)(259200 < currentTime - timeFromFileName))
					{
						ImportFunctions.deleteImportTempFile((XVar)(MVCFunctions.Concat(tempFilesDirectory, fileName.Value)));
					}
				}
			}
			return null;
		}
		public virtual XVar importExplode(dynamic _param_importText)
		{
			#region pass-by-value parameters
			dynamic importText = XVar.Clone(_param_importText);
			#endregion

			dynamic charNext = null, flag = null, i = null, j = null, lines = XVar.Array(), tmpText = null, var_char = null;
			flag = new XVar(true);
			tmpText = new XVar("");
			j = new XVar(0);
			lines = XVar.Clone(XVar.Array());
			i = new XVar(0);
			for(;i < MVCFunctions.strlen((XVar)(importText)); i++)
			{
				var_char = XVar.Clone(MVCFunctions.substr((XVar)(importText), (XVar)(i), new XVar(1)));
				charNext = XVar.Clone(MVCFunctions.substr((XVar)(importText), (XVar)(i + 1), new XVar(1)));
				if(var_char == "\"")
				{
					if(XVar.Pack(flag))
					{
						flag = new XVar(false);
					}
					else
					{
						if(charNext == "\"")
						{
							i++;
							lines[j] = MVCFunctions.Concat(lines[j], var_char);
						}
						else
						{
							flag = new XVar(true);
						}
					}
				}
				if((XVar)((XVar)(flag)  && (XVar)(MVCFunctions.ord((XVar)(var_char)) == 13))  && (XVar)(MVCFunctions.ord((XVar)(charNext)) == 10))
				{
					j++;
					i += 1;
				}
				else
				{
					lines[j] = MVCFunctions.Concat(lines[j], var_char);
				}
			}
			return lines;
		}
		protected virtual XVar displayImportPage()
		{
			dynamic hiddenBricks = null, templatefile = null;
			templatefile = XVar.Clone(this.templatefile);
			if(XVar.Pack(this.eventsObject.exists(new XVar("BeforeShowImport"))))
			{
				this.eventsObject.BeforeShowImport((XVar)(this.xt), ref templatefile, this);
			}
			hiddenBricks = XVar.Clone(new XVar(0, "import_rawtext_control", 1, "import_preview", 2, "import_process", 3, "import_results", 4, "error_message"));
			this.xt.displayBricksHidden((XVar)(hiddenBricks));
			display((XVar)(templatefile));
			return null;
		}
	}
}
