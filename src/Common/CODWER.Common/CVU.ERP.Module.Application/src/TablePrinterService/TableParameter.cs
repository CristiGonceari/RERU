using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.Module.Application.TablePrinterService
{
    public class TableParameter
    {
        public string TableName { get; set; }
        public TableOrientation Orientation { get; set; }
        // field name and column header
        public List<SelectItem> Fields { get; set; }
    }
}
