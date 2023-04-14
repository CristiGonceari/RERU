using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
                .Replace("{table_content}", GetTableContent(_mapper.Map<List<TDestination>>(data.Items), data.Fields.Select(x=>x.Value).ToList()));

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
                .Replace("{table_content}", GetTableContent(data.Items, data.Fields.Select(x => x.Value).ToList()));

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

        private string GetTableContent(List<TDestination> items, List<string> fields)
        {
            var records = string.Empty;
            //every Row
            foreach (var item in items)
            {
                var objType = item.GetType();

                records += "<tr>";
                //every column
                foreach (var field in fields)
                {
                    var propInfo = objType.GetPropertyInfo(field);

                    records += $"<td>{propInfo.ParseByDataType(item)}</td>";
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
                        border-collapse: collapse;
                        width: 100%;
                    }

                    th, td {
                        padding: 15px;
                        word-break: break-word;
                        max-width:400px;
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
    }
}
