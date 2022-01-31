using System.Collections.Generic;

namespace CVU.ERP.Module.Application.Services
{
    public interface ITablePrinter <T>
    {
        public string FormatTable(List<T> items, List<string> fields);
    }
}
