using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.BulkImportDepartments
{
    public class BulkImportDepartmentsCommand : IRequest<FileDataDto>
    {
        public ExcelDataDto Data { get; set; }
    }
}
