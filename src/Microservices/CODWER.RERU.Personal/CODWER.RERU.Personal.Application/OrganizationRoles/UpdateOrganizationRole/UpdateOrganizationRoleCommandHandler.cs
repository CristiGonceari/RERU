using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.UpdateOrganizationRole
{
    public class UpdateOrganizationRoleCommandHandler : IRequestHandler<UpdateOrganizationRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<UpdateOrganizationRoleCommand> _loggerService;

        public UpdateOrganizationRoleCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<UpdateOrganizationRoleCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UpdateOrganizationRoleCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.OrganizationRoles.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return Unit.Value;
        }
        private async Task LogAction(OrganizationRole organizationRole)
        {
            await _loggerService.Log(LogData.AsPersonal($"{organizationRole.Name} was edited", organizationRole));
        }
    }
}
