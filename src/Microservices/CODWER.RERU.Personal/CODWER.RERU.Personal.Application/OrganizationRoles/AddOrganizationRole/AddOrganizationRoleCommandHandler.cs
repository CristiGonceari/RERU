using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    public class AddOrganizationRoleCommandHandler : IRequestHandler<AddOrganizationRoleCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddOrganizationRoleCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddOrganizationRoleCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<OrganizationRole>(request.Data);

            await _appDbContext.OrganizationRoles.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
