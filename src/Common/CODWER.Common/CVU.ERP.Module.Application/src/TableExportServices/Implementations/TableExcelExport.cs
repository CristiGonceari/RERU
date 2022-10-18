using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.TableExportServices.Implementations
{
    public class TableExcelExport<TSource, TDestination> : ITableExcelExport<TSource, TDestination>
    {
        private readonly IMapper _mapper;

        public TableExcelExport(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FileDataDto ExportTable(TableData<TSource> data)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            GetHeaderContent(workSheet, data.Fields.Select(x => x.Label).ToList());
            GetTableContent(data.Name, workSheet, _mapper.Map<List<TDestination>>(data.Items), data.Fields.Select(x => x.Value).ToList());

            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel($"{data.Name}", streamBytesArray);
        }

        public FileDataDto ExportListTable(TableListData<TDestination> data)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            GetHeaderContent(workSheet, data.Fields.Select(x => x.Label).ToList());
            GetTableContent(data.Name, workSheet, data.Items, data.Fields.Select(x => x.Value).ToList());

            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel($"{data.Name}", streamBytesArray);
        }

        private void GetHeaderContent(ExcelWorksheet workSheet, List<string> fields)
        {
            for (int i = 0; i < fields.Count; i++)
            {
                workSheet.Cells[1, i + 1].Value = fields[i];
                workSheet.Cells[1, i + 1].Style.Font.Bold = true;
                SetBordersStyleOnCells(workSheet, 1, i + 1);
            }
        }

        private void GetTableContent(string tableName, ExcelWorksheet workSheet, List<TDestination> items, List<string> fields)
        {
            //every Row
            for (int i = 0; i < items.Count(); i++)
            {
                var objType = items[i].GetType();

                //every Column in this Row
                for (int j = 0; j < fields.Count; j++)
                {
                    var propInfo = GetPropertyInfo(objType, fields[j]);
                    workSheet.Cells[i + 2, j + 1].Value = ParseByDataType(propInfo, items[i], tableName);
                    SetBordersStyleOnCells(workSheet, i + 2, j + 1 );
                }
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        }
        private string ParseByDataType(PropertyInfo propInfo, object item, string tableName)
        {
            var result = propInfo.GetValue(item, null);

            switch (result)
            {
                case DateTime:
                    result = Convert.ToDateTime(result).ToString("dd/MM/yyyy, HH:mm");
                    break;
                case List<string>:
                    result = ParseDataByListOfStrings(result);
                    break;
                case bool:
                    result = Convert.ToBoolean(result) ? "+" : "-";
                    break;
                case TestResultStatusEnum:
                    result = ParseDataByTestEnum(tableName, result);
                    break;
                case TestStatusEnum:
                    result = EnumMessages.GetTestStatus((TestStatusEnum)result);
                    break;
                case QuestionTypeEnum:
                    result = EnumMessages.GetQuestionType((QuestionTypeEnum)result);
                    break;
                case QuestionUnitStatusEnum:
                    result = EnumMessages.GetQuestionStatus((QuestionUnitStatusEnum)result);
                    break;
                case TestTemplateModeEnum:
                    result = EnumMessages.GetTestTemplateTypeEnum((TestTemplateModeEnum)result);
                    break;
                case MedicalColumnEnum:
                    result = EnumMessages.GetMedicalColumnEnum((MedicalColumnEnum)result);
                    break;
                case null:
                    result = "-";
                    break;
            }

            return result.ToString();
        }
        private static void SetBordersStyleOnCells(ExcelWorksheet worksheet, int row, int column)
        {
            worksheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Column(column).Width = 24; ;
        }

        private object ParseDataByTestEnum(string tableName, object result) => EnumMessages.TranslateResultStatus((TestResultStatusEnum)result);

        private object ParseDataByListOfStrings(object result)
        {
            result = string.Join(", ", (List<string>)result);

            return result == "" ? "-" : result;
        }
    }
}
