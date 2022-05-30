using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.GetRolesValue
{
    public class GetRolesValueQueryHandler : IRequestHandler<GetRolesValueQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetRolesValueQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetRolesValueQuery request, CancellationToken cancellationToken)
        {
            var departments = await _appDbContext.Roles.ToListAsync();

            return _mapper.Map<List<SelectItem>>(departments);
        }
    }
}
