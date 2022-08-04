using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.ImportOrganizationRoles
{
    public class ImportRolesCommandHandler : IRequestHandler<ImportRolesCommand, FileDataDto>
    {
        private readonly IImportDepartmentsAndRolesService _service;
        public ImportRolesCommandHandler(IImportDepartmentsAndRolesService service)
        {
            _service = service;
        }

        public async Task<FileDataDto> Handle(ImportRolesCommand request, CancellationToken cancellationToken)
        {
            return await _service.Import(request.Data.File, ImportTypeEnum.ImportRoles);
        }
    }
}
