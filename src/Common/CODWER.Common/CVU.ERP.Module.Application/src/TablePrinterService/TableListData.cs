using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.Module.Application.TablePrinterService
{
    public class TableListData<TSource>
    {
        public string Name { get; set; }
        public List<TSource> Items { get; set; }
        public List<SelectItem> Fields { get; set; }
        public TableOrientation Orientation { get; set; }
    }
}
