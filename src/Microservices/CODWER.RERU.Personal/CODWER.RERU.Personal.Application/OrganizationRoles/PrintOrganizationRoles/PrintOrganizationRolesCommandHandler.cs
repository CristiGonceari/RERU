using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.PrintOrganizationRoles
{
    public class PrintOrganizationRolesCommandHandler : IRequestHandler<PrintOrganizationRolesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Role, RoleDto> _printer;

        public PrintOrganizationRolesCommandHandler(AppDbContext appDbContext, IExportData<Role, RoleDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintOrganizationRolesCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Roles
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.SearchWord.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Role>
            {
                Name = request.TableName,
                Items = items,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
