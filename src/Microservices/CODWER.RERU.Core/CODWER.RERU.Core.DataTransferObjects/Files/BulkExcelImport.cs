using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Core.DataTransferObjects.Files
{
    public class BulkExcelImport
    {
        public IFormFile File { get; set; }
    }
}
