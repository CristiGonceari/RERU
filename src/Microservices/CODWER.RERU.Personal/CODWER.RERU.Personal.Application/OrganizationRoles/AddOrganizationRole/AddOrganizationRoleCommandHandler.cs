using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddRoleCommand> _loggerService;

        public AddRoleCommandHandler(AppDbContext appDbContext, IMapper mapper,
            ILoggerService<AddRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Role>(request.Data);

            await _appDbContext.Roles.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return item.Id;
        }

        private async Task LogAction(Role Role)
        {
            await _loggerService.Log(LogData.AsPersonal($"Rolul {Role.Name} a fost adăugat în sistem", Role));
        }
    }
}
