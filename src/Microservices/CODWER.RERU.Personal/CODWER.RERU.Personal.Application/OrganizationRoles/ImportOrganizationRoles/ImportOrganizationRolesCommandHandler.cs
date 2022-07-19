using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.ImportOrganizationRoles
{
    public class ImportRolesCommandHandler : IRequestHandler<ImportRolesCommand, Unit>
    {
        private readonly IExcelImporter _excelImporter;

        public ImportRolesCommandHandler(IExcelImporter excelImporter)
        {
            _excelImporter = excelImporter;
        }

        public async Task<Unit> Handle(ImportRolesCommand request, CancellationToken cancellationToken)
        {
            await _excelImporter.Import(request.Data.File);

            return Unit.Value;
        }
    }
}
