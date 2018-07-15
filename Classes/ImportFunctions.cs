using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using LumenWorks.Framework.IO.Csv;
using OfficeOpenXml;

namespace runnerDotNet
{
    public class ImportFunctions
    {
        public static XVar openImportExcelFile(XVar uploadedFile, XVar ext)
        {
            if (uploadedFile == null)
            {
                return null;
            }

			try
			{
				ExcelPackage xlsPack = new ExcelPackage();

				// preview mode - file is in the _FILES list
				for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
				{
					if (HttpContext.Current.Request.Files[i].FileName == uploadedFile)
					{
						xlsPack.Load(HttpContext.Current.Request.Files[i].InputStream);
						return XVar.Pack(xlsPack);
					}
				}

				// import mode - file already saved on disc
				if (MVCFunctions.file_exists(uploadedFile))
				{
					using (FileStream rd = new FileStream(uploadedFile, FileMode.Open))
						xlsPack.Load(rd);
					return XVar.Pack(xlsPack);
				}

				return null;
			}
			catch(Exception ex)
			{
				MVCFunctions.import_error_handler(ex);
				return null;
			}
        }

        public static XVar getImportExcelFields(XVar data)
        {
			XVar fields = XVar.Array();
			
			if(data == null)
				return fields;
			
			try
			{
				ExcelPackage xlsPack = data.Value as ExcelPackage;
				var worksheet = xlsPack.Workbook.Worksheets[1];
				int cellIndex = 1;
				while (worksheet.Cells[1, cellIndex].Value != null)
				{
					fields.Add(XVar.Pack(worksheet.Cells[1, cellIndex].Value));
					cellIndex++;
				}
			}
			catch(Exception ex)
			{
				MVCFunctions.import_error_handler(ex);
			}
            return fields;
        }

		public static XVar ImportDataFromExcel( XVar fileHandle, XVar fieldsData, XVar keys, ImportPage importPageObject, XVar autoinc, XVar useFirstLine )    
        {
			if (fileHandle == null)
				return null;

			XVar metaData = XVar.Array();
			metaData["totalRecords"] = 0;

			dynamic addedRecords = 0;
			dynamic updatedRecords = 0;
			XVar errorMessages = XVar.Array();
			XVar unprocessedData = XVar.Array();

			try
			{
				ExcelPackage xlsPack = fileHandle.Value as ExcelPackage;
				
				foreach(var record in IterateSheet(xlsPack, fieldsData, useFirstLine))
				{
					importPageObject.importRecord(record, keys, autoinc, ref addedRecords, ref updatedRecords, errorMessages, unprocessedData);
					metaData["totalRecords"] = metaData["totalRecords"] + 1;
				}
			}
			catch(Exception ex)
			{
				MVCFunctions.import_error_handler(ex);
			}
			
			metaData["addedRecords"] = addedRecords;
			metaData["updatedRecords"] = updatedRecords;
			metaData["errorMessages"] = errorMessages;
			metaData["unprocessedData"] = unprocessedData;

			return metaData;
        }
		
		public static IEnumerable<XVar> IterateSheet(ExcelPackage xlsPack, XVar fieldsData, bool useFirstLine)
		{
			XVar previewData = null;
				
			foreach(var worksheet in xlsPack.Workbook.Worksheets) 
			{
				for( int row = (useFirstLine? 1 : 2); row <= worksheet.Dimension.End.Row; ++row)
				{
					XVar arr = XVar.Array();
					for (int i = 0; i < worksheet.Dimension.End.Column; i++)
					{
						if( !fieldsData.KeyExists(i) )
							continue;
							
						var cell = worksheet.Cells[ row, i + 1 ];
						XVar val = null;
						if (cell != null && cell.Value != null)
						{
							bool dateCorrectlyExtracted = false;
							if (cell.Value is DateTime) // sometimes date stored in a DateTime
							{
								val = ((DateTime)cell.Value).ToString("yyyy-MM-dd H:mm:ss");
								dateCorrectlyExtracted = true;
							}
							else if (Regex.IsMatch(cell.Style.Numberformat.Format, ("([ymdHis])"))) // sometimes date stored in a weird number
							{
								cell.Style.Numberformat.Format = "yyyy-MM-dd H:mm:ss";
								val = cell.Text;
								dateCorrectlyExtracted = true;
							}
							else if (cell.IsRichText)
							{
								val = cell.RichText.Text;
							}
							else
							{
								val = new XVar(cell.Value.ToString());
							}

							// the following code block looks suspicious (!)
							if (fieldsData.KeyExists(i) && fieldsData[i]["dateTimeType"] // column should have date value, 
								&& !dateCorrectlyExtracted								// but in excel it looks awfull
								&& (row != 0 || !useFirstLine))				// also we still dont know actual format and it is not header row
							{
								previewData = XVar.Array();
								// so try to guess format from the value
								previewData["dateFormat"] = ImportPage.extractDateFormat(val);
							}
						}

						if( previewData != null )
						{
							arr.Add(MVCFunctions.runner_htmlspecialchars(val));
						}
						else 
						{
							arr[ fieldsData[i]["fName"] ] = val;
						}
					}

					yield return arr;
				}
			}
		}

		public static IEnumerable<XVar> IterateSheetPreview( ExcelPackage xlsPack )
		{
			foreach(var worksheet in xlsPack.Workbook.Worksheets)
			{
				for( int row = 1; row <= worksheet.Dimension.End.Row; ++row)
				{
					XVar arr = XVar.Array();
					for (int i = 0; i < worksheet.Dimension.End.Column; i++)
					{
						var cell = worksheet.Cells[ row, i + 1 ];
						XVar val = null;
						if (cell != null && cell.Value != null)
						{					
							if (cell.Value is DateTime) // sometimes date stored in a DateTime
							{
								val = ((DateTime)cell.Value).ToString("yyyy-MM-dd H:mm:ss");	
							}
							else if (Regex.IsMatch(cell.Style.Numberformat.Format, ("([ymdHis])"))) // sometimes date stored in a weird number
							{
								cell.Style.Numberformat.Format = "yyyy-MM-dd H:mm:ss";
								val = cell.Text;
							}
							else if (cell.IsRichText)
							{
								val = cell.RichText.Text;
							}
							else
							{
								val = new XVar(cell.Value.ToString());
							}
						}

						arr.Add(MVCFunctions.runner_htmlspecialchars(val));
					}

					yield return arr;
				}
			}
		}

		
		public static XVar getRefinedDateFormat(XVar dateFormat)
		{
			var refinedFormat = "";
	
			string dateFormatStr = dateFormat.ToLower();
			for(int i = 0; i < dateFormatStr.Length; i++)
			{
				var letter = dateFormatStr[i];
				if( ( letter == 'd' || letter == 'm' || letter == 'y' ) && !refinedFormat.Contains(letter))
					refinedFormat += letter;
			}
	
			return refinedFormat;
		}
		
		public static XVar getTimeStamp(XVar value, XVar dateFormat)
		{
			var refinedDateFormat = getRefinedDateFormat(dateFormat);
			return MVCFunctions.strtotime( CommonFunctions.localdatetime2db(value, refinedDateFormat));
		}
		
		public static XVar getImportFileData( XVar fileName )
		{
			var uploadedFile = HttpContext.Current.Request.Files[fileName.ToString()];

			XVar uploadedFileData = XVar.Array();
			uploadedFileData["name"] = uploadedFile.FileName;
			uploadedFileData["tmp_name"] = uploadedFile.FileName;
			uploadedFileData["size"] = uploadedFile.ContentLength;
			uploadedFileData["type"] = uploadedFile.ContentType;
			uploadedFileData["error"] = "";
			uploadedFileData["file"] = uploadedFile;
			return uploadedFileData;
		}

		public static XVar getLineFromFile(XVar handle, XVar length)
		{
			StreamReader reader = handle.Value as StreamReader;
			return (XVar)reader.ReadLine();
		}

		public static void rewindFilePointerPosition(XVar handle)
		{
			(handle.Value as StreamReader).BaseStream.Seek(0,SeekOrigin.Begin);
		}

		public static XVar OpenCSVFile(XVar uploadfile, XVar delimeter)
		{
				
			if (uploadfile.Value is string && MVCFunctions.file_exists(uploadfile))
			{
				FileStream fs = new FileStream(uploadfile.ToString(), FileMode.Open);
				return new XVar(new StreamReader(fs));
			}
			if (uploadfile.Value is HttpPostedFile)
			{
				Stream fs = (uploadfile.Value as HttpPostedFile).InputStream;
				fs.Position = 0;
				return new XVar(new StreamReader(fs));
			}
			
			// preview mode - file is in the _FILES list
			for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (HttpContext.Current.Request.Files[i].FileName == uploadfile.Value)
                {
                    return new XVar(new StreamReader(HttpContext.Current.Request.Files[i].InputStream));
                    
                }
            }
			throw new ApplicationException("CSV file not found");
		}

		public static void CloseCSVFile(XVar handle)
		{
			// no need to do anything here
		}

		public static XVar GetCSVLine(XVar handle, XVar length, XVar delimiter)
		{
			StreamReader reader = handle.Value as StreamReader;
			string s= reader.ReadLine();
            return new XVar(parceCSVLine((XVar)s, delimiter));
		}

		public static XVar parceCSVLine(XVar line, XVar delimiter, XVar removeBOM = null)
		{
			var linestr = line.ToString();
			if (linestr.Length<3)
				return false;
			if(linestr.Substring(0, 3) == "\xEF\xBB\xBF")
				linestr = linestr.Substring(3);
		
			CsvReader csvRdr = new CsvReader(new StringReader(linestr), false, delimiter.ToString()[0]);
            try
            {
                if (csvRdr.ReadNextRecord())
                {
                    XVar row = XVar.Array();
                    for (int i = 0; i < csvRdr.FieldCount; i++)
                    {
                        row.Add(csvRdr[i]);
                    }
                    return row;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
			return false;
		}

		public static XVar testMonth( XVar number ) 
		{
			return Regex.IsMatch(number, "0[1-9]|1[0-2]");
		}

		public static XVar getImportFileExtension(XVar fname)
		{
			return System.IO.Path.GetExtension(getTempImportFileName(fname)).TrimStart('.');
		}

		public static XVar getTempImportFileName(XVar fname)
		{
			return HttpContext.Current.Request.Files[fname.ToString()].FileName;
		}

		public static XVar getFileNamesFromDir( XVar dirPath )
		{
			XVar names = XVar.Array();

			var dirPathStr = dirPath.ToString().Replace('/', '\\');
			
			if (!System.IO.Directory.Exists(dirPathStr))
				return names;
			
			foreach (var file in System.IO.Directory.EnumerateFiles(dirPathStr))
				names.Add(file);

			return names;
		}

		public static void deleteImportTempFile( XVar filePath )
		{
			var filePathStr = filePath.ToString().Replace('/', '\\');
			if (System.IO.File.Exists(filePathStr))
				System.IO.File.Delete(filePathStr);
		}

		public static void saveTextInFile(XVar filePath, XVar text)
		{
			var filePathStr = filePath.ToString().Replace('/', '\\');

			var dir = System.IO.Path.GetDirectoryName(filePathStr);
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			System.IO.File.WriteAllText(filePathStr, text);
		}

		// not yet implemented
		public static XVar getPreviewDataFromExcel(XVar fileHandle, XVar fieldsData) 
		{
			XVar previewData = XVar.Array();
			XVar tableData = XVar.Array();
			var remainNumOfPreviewRows = 100;
			
			try
			{
				ExcelPackage xlsPack = fileHandle.Value as ExcelPackage;
				
				foreach(var record in IterateSheetPreview(xlsPack))
				{
				
					if (remainNumOfPreviewRows<0)
						break;
					
					XVar row = XVar.Array();
					foreach (KeyValuePair<XVar, dynamic> field in record.GetEnumerator())
					{
						row.Add(field.Value);
					}
					tableData.Add(row);
										
					remainNumOfPreviewRows--;
				}
			}
			catch(Exception ex)
			{
				MVCFunctions.import_error_handler(ex);
			}

			previewData["tableData"]=tableData;
			return previewData;	
		}
		// not yet implemented
		public static XVar rewindFilePointerPosition(XVar handle, XVar filePath) 
		{
			(handle.Value as StreamReader).BaseStream.Seek(0,SeekOrigin.Begin);
			return new XVar(new StreamReader((handle.Value as StreamReader).BaseStream));
		}
		
		
        public static XVar getImportCVSFields(XVar uploadfile)
        {
			XVar csvFields = XVar.Array();
			try
			{
				CsvReader csvRdr = new CsvReader(new StreamReader((uploadfile.Value as HttpPostedFile).InputStream), true);
				foreach (var header in csvRdr.GetFieldHeaders())
				{
					csvFields.Add(header);
				}
			}
			catch(Exception ex)
			{
				MVCFunctions.import_error_handler(ex);
			}
            return csvFields;
        }

        public static XVar getImportTableName(XVar tname)
        {
            return XVar.Pack(HttpContext.Current.Request.Files[tname.ToString()]);
        }
        
        public static XVar db_exec_import(XVar sql, XVar conn, XVar table, XVar isIdentityOffNeeded)
        {
			Connection connection = XVar.UnPackConnection(conn);
            try
            {
                connection.db_multipleInsertQuery(new XVar(0, sql), table, isIdentityOffNeeded);
                return true;
            }
			
			catch(Exception e)
            {
				bool handle = (( connection.dbType != Constants.nDATABASE_MySQL 
							 && connection.dbType != Constants.nDATABASE_MSSQLServer
							 && connection.dbType != Constants.nDATABASE_Access
							 && connection.dbType != Constants.nDATABASE_PostgreSQL
							 && connection.dbType != Constants.nDATABASE_Oracle
							 && connection.dbType != Constants.nDATABASE_SQLite3)
							 
							|| (connection.dbType == Constants.nDATABASE_MSSQLServer	&& e is System.Data.SqlClient.SqlException)
							|| (connection.dbType == Constants.nDATABASE_Access			&& e is System.Data.OleDb.OleDbException)
							);

				if(!handle)
					throw;

                GlobalVars.LastDBError = e.Message;
                return false;
            }
        }
    }
}