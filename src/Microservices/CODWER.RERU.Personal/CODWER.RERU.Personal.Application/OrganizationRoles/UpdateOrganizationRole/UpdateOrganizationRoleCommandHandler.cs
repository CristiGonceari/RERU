using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.UpdateOrganizationRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<UpdateRoleCommand> _loggerService;

        public UpdateRoleCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<UpdateRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Roles.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return Unit.Value;
        }
        private async Task LogAction(Role Role)
        {
            await _loggerService.Log(LogData.AsPersonal($"{Role.Name} was edited", Role));
        }
    }
}
