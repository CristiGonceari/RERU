using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation360.Application.BLL.References.GetEvaluation360Roles
{
    public class GetEvaluation360RolesQueryHandler : IRequestHandler<GetEvaluation360RolesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetEvaluation360RolesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetEvaluation360RolesQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.GetModuleRolePermissionsByPrefix(ModulePrefix.Evaluation360)
                .Select(x => x.Role)
                .Distinct()
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }
    }
}
