using CODWER.RERU.Core.DataTransferObjects.Roles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.GetRole
{
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<RoleDto>(role);
        }
    }
}
