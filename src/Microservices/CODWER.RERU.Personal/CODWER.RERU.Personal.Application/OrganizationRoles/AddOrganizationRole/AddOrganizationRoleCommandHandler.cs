using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    public class AddOrganizationRoleCommandHandler : IRequestHandler<AddOrganizationRoleCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddOrganizationRoleCommand> _loggerService;

        public AddOrganizationRoleCommandHandler(AppDbContext appDbContext, IMapper mapper,
            ILoggerService<AddOrganizationRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddOrganizationRoleCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<OrganizationRole>(request.Data);

            await _appDbContext.OrganizationRoles.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return item.Id;
        }

        private async Task LogAction(OrganizationRole organizationRole)
        {
            await _loggerService.Log(LogData.AsPersonal($"{organizationRole.Name} was added to Roles list", organizationRole));
        }
    }
}
