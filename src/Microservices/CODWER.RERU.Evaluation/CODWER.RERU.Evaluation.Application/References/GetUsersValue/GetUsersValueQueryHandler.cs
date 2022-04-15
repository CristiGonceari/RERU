using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.StaticExtensions;
using RERU.Data.Persistence.Context;

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
            var users = _appDbContext.UserProfiles.AsQueryable();

            users = await FilterByName(request, users);

            users =  await FilterByEvent(request, users);

            return await users.Select(u => _mapper.Map<SelectItem>(u)).ToListAsync();
        }

        private async Task<IQueryable<UserProfile>> FilterByName(GetUsersValueQuery request, IQueryable<UserProfile> users)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                users = users.FilterByNameAndIdnp(request.Name);
            }

            return users.AsQueryable();
        }

        private async Task<IQueryable<UserProfile>> FilterByEvent(GetUsersValueQuery request, IQueryable<UserProfile> users)
        {
            if (request.EventId != null)
            {
                users = users
                    .Include(x => x.EventUsers)
                    .Where(x => x.EventUsers.Any(e => e.EventId == request.EventId));
            }

            return users.AsQueryable();
        }
    }
}
