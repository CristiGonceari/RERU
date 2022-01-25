using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole
{
    public class GetOrganizationRoleQueryHandler : IRequestHandler<GetOrganizationRoleQuery, OrganizationRoleDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOrganizationRoleQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OrganizationRoleDto> Handle(GetOrganizationRoleQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.OrganizationRoles
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<OrganizationRoleDto>(item);

            return mappedItem;
        }
    }
}
