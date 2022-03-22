using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.Module.Application.TableExportServices
{
    public class TableData<TSource>
    {
        public string Name { get; set; }
        public IQueryable<TSource> Items { get; set; }
        public List<SelectItem> Fields { get; set; }
        public TableOrientation Orientation { get; set; }
        public TableExportFormat ExportFormat { get; set; }
    }
}
