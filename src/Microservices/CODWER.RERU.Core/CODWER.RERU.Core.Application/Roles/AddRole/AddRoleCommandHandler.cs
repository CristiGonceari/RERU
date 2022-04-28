using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities;
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
            var role = _mapper.Map<Role>(request.Data);

            await _appDbContext.Roles.AddAsync(role);

            await _appDbContext.SaveChangesAsync();

            return role.Id;
        }
    }
}
