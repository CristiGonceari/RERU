using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wkhtmltopdf.NetCore;
using Wkhtmltopdf.NetCore.Options;

namespace CVU.ERP.Module.Application.TableExportServices.Implementations
{
    public class TablePrinter<TSource, TDestination> : ITablePrinter<TSource, TDestination> 
    {
        private readonly IMapper _mapper;
        private readonly IGeneratePdf _generatePdf;

        public TablePrinter(IMapper mapper, IGeneratePdf generatePdf)
        {
            _mapper = mapper;
            _generatePdf = generatePdf;
        }

        public FileDataDto ExportTable(TableData<TSource> data)
        {
            var source = Html;
            source = source
                .Replace("{table_name}", data.Name)
                .Replace("{table_header}", GetTableHeader(data.Fields.Select(x=>x.Label).ToList()))
                .Replace("{table_content}", GetTableContent(data.Name, _mapper.Map<List<TDestination>>(data.Items), data.Fields.Select(x=>x.Value).ToList()));

            var options = new ConvertOptions
            {
                PageOrientation = data.Orientation == TableOrientation.Landscape
                    ? Orientation.Landscape
                    : Orientation.Portrait
            };

            _generatePdf.SetConvertOptions(options);

            var parsed = _generatePdf.GetPDF(source);

            return FileDataDto.GetPdf($"{data.Name}", parsed);
        }

        public FileDataDto ExportListTable(TableListData<TDestination> data)
        {
            var source = Html;
            source = source
                .Replace("{table_name}", data.Name)
                .Replace("{table_header}", GetTableHeader(data.Fields.Select(x => x.Label).ToList()))
                .Replace("{table_content}", GetTableContent(data.Name, data.Items, data.Fields.Select(x => x.Value).ToList()));

            var options = new ConvertOptions
            {
                PageOrientation = data.Orientation == TableOrientation.Landscape
                    ? Orientation.Landscape
                    : Orientation.Portrait
            };

            _generatePdf.SetConvertOptions(options);

            var parsed = _generatePdf.GetPDF(source);

            return FileDataDto.GetPdf($"{data.Name}", parsed);
        }

        private string GetTableHeader(List<string> fields)
        {
            return $"<tr>{string.Join(" ", fields.Select(f => $"<th>{f}</th>"))}</tr>";
        }

        private string GetTableContent(string tableName, List<TDestination> items, List<string> fields)
        {
            var records = string.Empty;

            foreach (var item in items)
            {
                var objType = item.GetType();

                records += "<tr>";

                foreach (var field in fields)
                {
                    var propInfo = GetPropertyInfo(objType, field);

                    records += $"<td>{ParseByDataType(propInfo, item, tableName)}</td>";
                }

                records += "</tr>";
            }

            return records;
        }

        private string Html =>
            @"<!DOCTYPE html>
            <html>
            <head>
                <style>
                    table, td, th {
                        border: 1px solid #ddd;
                        text-align: left;
                    }

                    table {
                        border - collapse: collapse;
                        width: 100%;
                    }

                    th, td {
                        padding: 15px;
                    }

                    p {
                        margin: 0;
                    }
                </style>
            </head>
            <body>

            <h2>{table_name}:</h2>
            <table>
                {table_header}
                {table_content}
            </table>
            </body>
            </html>";

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        }

        private string ParseByDataType(PropertyInfo propInfo, object item, string tableName)
        {
            var result = propInfo.GetValue(item, null);

            switch (result) {
                case DateTime: result = Convert.ToDateTime(result).ToString("dd/MM/yyyy, HH:mm");
                    break;
                case List<string>: result = ParseDataByListOfStrings(result);
                    break;
                case bool: result = Convert.ToBoolean(result) ? "+" : "-";
                    break;
                case TestResultStatusEnum: result = ParseDataByTestEnum(tableName, result);
                    break;
                case TestStatusEnum: result = EnumMessages.GetTestStatus((TestStatusEnum)result);
                    break;
                case QuestionTypeEnum: result = EnumMessages.GetQuestionType((QuestionTypeEnum)result);
                    break;
                case QuestionUnitStatusEnum: result = EnumMessages.GetQuestionStatus((QuestionUnitStatusEnum)result);
                    break;
                case TestTemplateModeEnum: result = EnumMessages.GetTestTemplateTypeEnum((TestTemplateModeEnum)result);
                    break;
                case MedicalColumnEnum:
                    result = EnumMessages.EnumMessages.GetMedicalColumnEnum((MedicalColumnEnum)result);
                    break;
                case null: result = "-";
                    break;
            }

            return result.ToString();
        }

        private object ParseDataByTestEnum(string tableName, object result) => EnumMessages.TranslateResultStatus((TestResultStatusEnum)result);

        private object ParseDataByListOfStrings(object result)
        {
            result = string.Join(", ", (List<string>)result);

            return result == "" ? "-" : result;
        }
    }
}
