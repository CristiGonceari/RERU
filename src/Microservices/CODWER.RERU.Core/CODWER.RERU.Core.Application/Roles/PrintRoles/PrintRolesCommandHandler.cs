using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.PrintRoles
{
    public class PrintRolesCommandHandler : IRequestHandler<PrintRolesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Role, RoleDto> _printer;

        public PrintRolesCommandHandler(AppDbContext appDbContext, IExportData<Role, RoleDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintRolesCommand request, CancellationToken cancellationToken)
        {
            var roles = GetAndFilterRoles.Filter(_appDbContext, request.Name);

            var result = _printer.ExportTableSpecificFormat(new TableData<Role>
            {
                Name = request.TableName,
                Items = roles,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
