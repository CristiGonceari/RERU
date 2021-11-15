using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.References.GetUsersValue
{
    public class GetUsersValueQueryHandler : IRequestHandler<GetUsersValueQuery, List<SelectItem>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetUsersValueQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectItem>> Handle(GetUsersValueQuery request, CancellationToken cancellationToken)
        {
            var users = await _appDbContext.UserProfiles
                .AsQueryable()
                .Select(u => _mapper.Map<SelectItem>(u))
                .ToListAsync();

            return users;
        }
    }
}
