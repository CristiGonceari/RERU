using CVU.ERP.Common.DataTransferObjects.SelectValues;
using System.Collections.Generic;

namespace CVU.ERP.Module.Application.TableExportServices
{
    public class TableParameter
    {
        public string TableName { get; set; }
        public TableOrientation Orientation { get; set; }
        // field name and column header
        public List<SelectItem> Fields { get; set; }
        public TableExportFormat TableExportFormat { get; set; }
    }
}
