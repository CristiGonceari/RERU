using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;

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
            GetTableContent(workSheet, _mapper.Map<List<TDestination>>(data.Items), data.Fields.Select(x => x.Value).ToList());

            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel($"{data.Name}", streamBytesArray);
        }

        public FileDataDto ExportListTable(TableListData<TDestination> data)
        {
            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            GetHeaderContent(workSheet, data.Fields.Select(x => x.Label).ToList());
            GetTableContent(workSheet, data.Items, data.Fields.Select(x => x.Value).ToList());

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

        private void GetTableContent(ExcelWorksheet workSheet, List<TDestination> items, List<string> fields)
        {
            //every Row
            for (int i = 0; i < items.Count(); i++)
            {
                var objType = items[i].GetType();

                //every Column in this Row
                for (int j = 0; j < fields.Count; j++)
                {
                    var propInfo = GetPropertyInfo(objType, fields[j]);
                    workSheet.Cells[i + 2, j + 1].Value = ParseByDataType(propInfo, items[i]);
                    SetBordersStyleOnCells(workSheet, i + 2, j + 1 );
                }
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        }
        private string ParseByDataType(PropertyInfo propInfo, object item)
        {
            var result = propInfo.GetValue(item, null);

            if (propInfo.PropertyType == typeof(DateTime))
            {
                result = Convert.ToDateTime(result).ToString("dd/MM/yyyy, HH:mm");
            }
            else if (propInfo.PropertyType == typeof(List<string>))
            {
                result = string.Join(", ", (List<string>)result);

                if (result == "")
                {
                    result = "-";
                }
            }
            else if (propInfo.PropertyType == typeof(bool))
            {
                result = Convert.ToBoolean(result) ? "+" : "-";
            }
            else if (result == null)
            {
                result = "-";
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
    }
}
