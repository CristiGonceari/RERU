using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using System;

namespace CVU.ERP.Module.Application.TableExportServices
{
    public class ExportData<TSource, TDestination> : IExportData<TSource, TDestination>
    {
        private readonly ITablePrinter<TSource, TDestination> _tablePrinter;
        private readonly ITableExcelExport<TSource, TDestination> _tableExcelExport;
        private readonly ITableXmlExport<TSource, TDestination> _tableXmlExport;

        public ExportData(ITablePrinter<TSource, TDestination> tablePrinter, 
            ITableExcelExport<TSource, TDestination> tableExcelExport, 
            ITableXmlExport<TSource, TDestination> tableXmlExport)
        {
            _tablePrinter = tablePrinter;
            _tableExcelExport = tableExcelExport;
            _tableXmlExport = tableXmlExport;
        }

        public FileDataDto ExportTableSpecificFormat(TableData<TSource> data)
        {
            return data.ExportFormat switch
            {
                TableExportFormat.Pdf => _tablePrinter.ExportTable(data),
                TableExportFormat.Excel => _tableExcelExport.ExportTable(data),
                TableExportFormat.Xml => _tableXmlExport.ExportTable(data),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public FileDataDto ExportTableSpecificFormatList(TableListData<TDestination> data)
        {
            return data.ExportFormat switch
            {
                TableExportFormat.Pdf => _tablePrinter.ExportListTable(data),
                TableExportFormat.Excel => _tableExcelExport.ExportListTable(data),
                TableExportFormat.Xml => _tableXmlExport.ExportListTable(data),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
