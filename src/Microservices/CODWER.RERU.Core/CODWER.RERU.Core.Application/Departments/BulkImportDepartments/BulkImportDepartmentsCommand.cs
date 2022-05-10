using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.BulkImportDepartments
{
    public class BulkImportDepartmentsCommand : IRequest<FileDataDto>
    {
        public BulkExcelImport Data { get; set; }
    }
}
