using CODWER.RERU.Personal.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.ImportDepartmentRoleRelation
{
    public class ImportDepartmentRoleRelationCommandHandler : IRequestHandler<ImportDepartmentRoleRelationCommand, FileDataDto>
    {
        private readonly IExcelImportDepartmentRoleRelationService _service;

        public ImportDepartmentRoleRelationCommandHandler(IExcelImportDepartmentRoleRelationService service)
        {
            _service = service;
        }

        public async Task<FileDataDto> Handle(ImportDepartmentRoleRelationCommand request, CancellationToken cancellationToken)
        {
            return await _service.ImportDepartmentToDepartmentRelation(request.Data.File, request.OrganizationalChartId);

        }
    }
}
