using CVU.ERP.Common.DataTransferObjects.Files;

namespace CVU.ERP.Module.Application.TableExportServices.Interfaces
{
    public interface IExportDataFormat <TSource, TDestination>
    {
        public FileDataDto ExportTable(TableData<TSource> data);
        public FileDataDto ExportListTable(TableListData<TDestination> data);
    }
}
