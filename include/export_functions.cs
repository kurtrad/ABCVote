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
	public partial class CommonFunctions
	{
		public static XVar ExportToExcel(dynamic _param_rs, dynamic _param_nPageSize, dynamic _param_eventObj, dynamic _param_cipherer, dynamic _param_pageObj)
		{
			#region pass-by-value parameters
			dynamic rs = XVar.Clone(_param_rs);
			dynamic nPageSize = XVar.Clone(_param_nPageSize);
			dynamic eventObj = XVar.Clone(_param_eventObj);
			dynamic cipherer = XVar.Clone(_param_cipherer);
			dynamic pageObj = XVar.Clone(_param_pageObj);
			#endregion

			dynamic arrColumnWidth = XVar.Array(), arrFields = XVar.Array(), arrLabel = XVar.Array(), arrTmpTotal = XVar.Array(), arrTotal = XVar.Array(), arrTotalMessage = XVar.Array(), eventRes = null, iNumberOfRows = null, objPHPExcel = null, row = XVar.Array(), totals = XVar.Array(), totalsFields = XVar.Array(), values = XVar.Array();
			if(XVar.Pack(eventObj.exists(new XVar("ListFetchArray"))))
			{
				row = XVar.Clone(eventObj.ListFetchArray((XVar)(rs), (XVar)(pageObj)));
			}
			else
			{
				row = XVar.Clone(cipherer.DecryptFetchedArray((XVar)(pageObj.connection.fetch_array((XVar)(rs)))));
			}
			totals = XVar.Clone(XVar.Array());
			arrLabel = XVar.Clone(XVar.Array());
			arrTotal = XVar.Clone(XVar.Array());
			arrFields = XVar.Clone(XVar.Array());
			arrColumnWidth = XVar.Clone(XVar.Array());
			arrTotalMessage = XVar.Clone(XVar.Array());
			foreach (KeyValuePair<XVar, dynamic> field in pageObj.selectedFields.GetEnumerator())
			{
				if(XVar.Pack(pageObj.pSet.appearOnExportPage((XVar)(field.Value))))
				{
					arrFields.InitAndSetArrayItem(field.Value, null);
				}
			}
			arrTmpTotal = XVar.Clone(pageObj.pSet.getTotalsFields());
			pageObj.viewControls.setForExportVar(new XVar("excel"));
			foreach (KeyValuePair<XVar, dynamic> field in arrFields.GetEnumerator())
			{
				arrLabel.InitAndSetArrayItem(CommonFunctions.GetFieldLabel((XVar)(MVCFunctions.GoodFieldName((XVar)(pageObj.tName))), (XVar)(MVCFunctions.GoodFieldName((XVar)(field.Value)))), field.Value);
				arrColumnWidth.InitAndSetArrayItem(10, field.Value);
				totals.InitAndSetArrayItem(new XVar("value", 0, "numRows", 0), field.Value);
				foreach (KeyValuePair<XVar, dynamic> tvalue in arrTmpTotal.GetEnumerator())
				{
					if(tvalue.Value["fName"] == field.Value)
					{
						totalsFields.InitAndSetArrayItem(new XVar("fName", field.Value, "totalsType", tvalue.Value["totalsType"], "viewFormat", pageObj.pSet.getViewFormat((XVar)(field.Value))), null);
					}
				}
			}
			iNumberOfRows = new XVar(0);
			objPHPExcel = XVar.Clone(ExportFunctions.ExportExcelInit((XVar)(arrLabel), (XVar)(arrColumnWidth)));
			while((XVar)((XVar)(!(XVar)(nPageSize))  || (XVar)(iNumberOfRows < nPageSize))  && (XVar)(row))
			{
				CommonFunctions.countTotals((XVar)(totals), (XVar)(totalsFields), (XVar)(row));
				values = XVar.Clone(XVar.Array());
				foreach (KeyValuePair<XVar, dynamic> field in arrFields.GetEnumerator())
				{
					if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(pageObj.pSet.getFieldType((XVar)(field.Value))))))
					{
						values.InitAndSetArrayItem(row[field.Value], field.Value);
					}
					else
					{
						values.InitAndSetArrayItem(pageObj.getFormattedFieldValue((XVar)(field.Value), (XVar)(row)), field.Value);
					}
				}
				eventRes = new XVar(true);
				if(XVar.Pack(eventObj.exists(new XVar("BeforeOut"))))
				{
					eventRes = XVar.Clone(eventObj.BeforeOut((XVar)(row), (XVar)(values), (XVar)(pageObj)));
				}
				if(XVar.Pack(eventRes))
				{
					dynamic arrData = XVar.Array(), arrDataType = XVar.Array(), i = null;
					arrData = XVar.Clone(XVar.Array());
					arrDataType = XVar.Clone(XVar.Array());
					iNumberOfRows++;
					i = new XVar(0);
					foreach (KeyValuePair<XVar, dynamic> field in arrFields.GetEnumerator())
					{
						dynamic vFormat = null;
						vFormat = XVar.Clone(pageObj.pSet.getViewFormat((XVar)(field.Value)));
						if(XVar.Pack(CommonFunctions.IsBinaryType((XVar)(pageObj.pSet.getFieldType((XVar)(field.Value))))))
						{
							arrDataType.InitAndSetArrayItem("binary", field.Value);
						}
						else
						{
							if((XVar)((XVar)(vFormat == Constants.FORMAT_DATE_SHORT)  || (XVar)(vFormat == Constants.FORMAT_DATE_LONG))  || (XVar)(vFormat == Constants.FORMAT_DATE_TIME))
							{
								arrDataType.InitAndSetArrayItem("date", field.Value);
							}
							else
							{
								if(vFormat == Constants.FORMAT_FILE_IMAGE)
								{
									arrDataType.InitAndSetArrayItem("file", field.Value);
								}
								else
								{
									arrDataType.InitAndSetArrayItem("", field.Value);
								}
							}
						}
						arrData.InitAndSetArrayItem(values[field.Value], field.Value);
					}
					ExportFunctions.ExportExcelRecord((XVar)(arrData), (XVar)(arrDataType), (XVar)(iNumberOfRows), (XVar)(objPHPExcel), (XVar)(pageObj));
				}
				if(XVar.Pack(eventObj.exists(new XVar("ListFetchArray"))))
				{
					row = XVar.Clone(eventObj.ListFetchArray((XVar)(rs), (XVar)(pageObj)));
				}
				else
				{
					row = XVar.Clone(cipherer.DecryptFetchedArray((XVar)(pageObj.connection.fetch_array((XVar)(rs)))));
				}
			}
			if(XVar.Pack(MVCFunctions.count(arrTmpTotal)))
			{
				foreach (KeyValuePair<XVar, dynamic> fName in arrFields.GetEnumerator())
				{
					dynamic total = null, totalMess = null, value = XVar.Array();
					value = XVar.Clone(XVar.Array());
					foreach (KeyValuePair<XVar, dynamic> tvalue in arrTmpTotal.GetEnumerator())
					{
						if(tvalue.Value["fName"] == fName.Value)
						{
							value = XVar.Clone(tvalue.Value);
						}
					}
					total = new XVar("");
					totalMess = new XVar("");
					if(XVar.Pack(value["totalsType"]))
					{
						if(value["totalsType"] == "COUNT")
						{
							totalMess = XVar.Clone(MVCFunctions.Concat("Count", ": "));
						}
						else
						{
							if(value["totalsType"] == "TOTAL")
							{
								totalMess = XVar.Clone(MVCFunctions.Concat("Total", ": "));
							}
							else
							{
								if(value["totalsType"] == "AVERAGE")
								{
									totalMess = XVar.Clone(MVCFunctions.Concat("Average", ": "));
								}
							}
						}
						total = XVar.Clone(CommonFunctions.GetTotals((XVar)(fName.Value), (XVar)(totals[fName.Value]["value"]), (XVar)(value["totalsType"]), (XVar)(totals[fName.Value]["numRows"]), (XVar)(value["viewFormat"]), new XVar("export"), (XVar)(pageObj.pSet)));
					}
					arrTotal.InitAndSetArrayItem(total, fName.Value);
					arrTotalMessage.InitAndSetArrayItem(totalMess, fName.Value);
				}
			}
			ExportFunctions.ExportExcelTotals((XVar)(arrTotal), (XVar)(arrTotalMessage), (XVar)(++(iNumberOfRows)), (XVar)(objPHPExcel));
			ExportFunctions.ExportExcelSave((XVar)(MVCFunctions.Concat(MVCFunctions.GoodFieldName((XVar)(pageObj.tName)), ".xlsx")), new XVar("Excel2007"), (XVar)(objPHPExcel));
			return null;
		}
	}
}
