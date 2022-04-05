using CVU.ERP.Common.DataTransferObjects.Files;

namespace CVU.ERP.Module.Application.TableExportServices
{
    public interface IExportData<TSource, TDestination> 
    {
        public FileDataDto ExportTableSpecificFormat(TableData<TSource> data);
        public FileDataDto ExportTableSpecificFormatList(TableListData<TDestination> data);
    }
}
