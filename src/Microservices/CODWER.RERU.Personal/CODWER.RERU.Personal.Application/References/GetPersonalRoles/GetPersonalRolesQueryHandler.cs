using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.References.GetPersonalRoles
{
    public class GetPersonalRolesQueryHandler : IRequestHandler<GetPersonalRolesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetPersonalRolesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetPersonalRolesQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = await _appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .Where(x => x.Permission.Code.StartsWith("P02"))
                .Select(x => x.Role)
                .Distinct()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();// Core permissions starts with P02, check PermissionCodes.cs

            return moduleRoles;
        }
    }
}
