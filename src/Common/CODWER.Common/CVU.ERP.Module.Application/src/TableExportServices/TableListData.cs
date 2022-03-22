using CVU.ERP.Common.DataTransferObjects.SelectValues;
using System.Collections.Generic;

namespace CVU.ERP.Module.Application.TableExportServices
{
    public class TableListData<TDestination>
    {
        public string Name { get; set; }
        public List<TDestination> Items { get; set; }
        public List<SelectItem> Fields { get; set; }
        public TableOrientation Orientation { get; set; }
        public TableExportFormat ExportFormat { get; set; }
    }
}
