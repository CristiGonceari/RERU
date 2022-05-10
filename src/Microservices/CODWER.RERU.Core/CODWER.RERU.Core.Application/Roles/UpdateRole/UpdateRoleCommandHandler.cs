using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new RoleDto()
            {
                Id = request.Id,
                Name = request.Name,
                ColaboratorId = request.ColaboratorId
            };

            var role = await _appDbContext.Roles.FirstAsync(x => x.Id == newRole.Id);

            _mapper.Map(newRole, role);
            await _appDbContext.SaveChangesAsync();

            return role.Id;
        }
    }
}
