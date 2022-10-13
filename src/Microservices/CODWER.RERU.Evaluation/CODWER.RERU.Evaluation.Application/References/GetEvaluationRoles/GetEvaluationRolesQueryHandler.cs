using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetEvaluationRoles
{
    public class GetEvaluationRolesQueryHandler : IRequestHandler<GetEvaluationRolesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetEvaluationRolesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetEvaluationRolesQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = await _appDbContext.ModuleRolePermissions
                .Include(x => x.Permission)
                .Include(x => x.Role)
                .Where(x => x.Permission.Code.StartsWith("P03"))
                .Select(x => x.Role)
                .Distinct()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();// Evaluation permissions starts with P03, check PermissionCodes.cs

            return moduleRoles;
        }
    }
}
