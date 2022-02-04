using CVU.ERP.Common.DataTransferObjects.Files;

namespace CVU.ERP.Module.Application.TablePrinterService
{
    public interface ITablePrinter <TSource,TDestination>
    {
        public FileDataDto PrintTable(TableData<TSource> data);
        public FileDataDto PrintListTable(TableListData<TSource> data);
    }
}
