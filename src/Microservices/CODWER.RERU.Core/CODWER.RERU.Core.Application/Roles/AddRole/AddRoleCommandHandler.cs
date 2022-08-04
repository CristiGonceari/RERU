using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddRoleCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new RoleDto()
            {
                Name = request.Name,
                ColaboratorId = request.ColaboratorId
            };

            var role = _mapper.Map<Role>(newRole);

            await _appDbContext.Roles.AddAsync(role);

            await _appDbContext.SaveChangesAsync();

            return role.Id;
        }
    }
}
