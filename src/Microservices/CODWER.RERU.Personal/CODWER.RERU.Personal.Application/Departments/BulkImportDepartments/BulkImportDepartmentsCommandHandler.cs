using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Departments.BulkImportDepartments
{
    public class BulkImportDepartmentsCommandHandler : IRequestHandler<BulkImportDepartmentsCommand, FileDataDto>
    {

        private readonly IImportDepartmentsAndRolesService _service;

        public BulkImportDepartmentsCommandHandler(IImportDepartmentsAndRolesService service)
        {
            _service = service;
        }

        public async Task<FileDataDto> Handle(BulkImportDepartmentsCommand request, CancellationToken cancellationToken)
        {
            return await _service.Import(request.Data.File, ImportTypeEnum.ImportDepartments);
        }
       
    }
}
