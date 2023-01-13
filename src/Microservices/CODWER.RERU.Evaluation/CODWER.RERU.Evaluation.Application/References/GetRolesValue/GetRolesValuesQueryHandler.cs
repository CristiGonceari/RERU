using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetRolesValue
{
    public class GetRolesValuesQueryHandler : IRequestHandler<GetRolesValuesQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetRolesValuesQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetRolesValuesQuery request, CancellationToken cancellationToken)
        {
            var roles = _appDbContext.Roles
                .OrderBy(x=>x.Name)
                .AsQueryable();

            return await roles.Select(u => _mapper.Map<SelectItem>(u)).ToListAsync();
        }
    }
}
