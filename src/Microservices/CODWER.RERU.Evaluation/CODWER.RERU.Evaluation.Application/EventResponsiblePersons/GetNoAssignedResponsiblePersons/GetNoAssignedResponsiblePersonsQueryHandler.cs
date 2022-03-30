using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.StaticExtensions;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetNoAssignedResponsiblePersons
{
    public class GetNoAssignedResponsiblePersonsQueryHandler : IRequestHandler<GetNoAssignedResponsiblePersonsQuery, List<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNoAssignedResponsiblePersonsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDto>> Handle(GetNoAssignedResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.EventResponsiblePersons
                .Include(x => x.UserProfile)
                    .Where(x => x.EventId == request.EventId)
                    .Select(x => x.UserProfile.Id)
                    .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles
                                            .Include(up => up.EventUsers)
                                            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                userProfiles = userProfiles.FilterByNameAndIdnp(request.Keyword);
            }

            userProfiles = userProfiles.Where(x => !responsiblePersons.Any(s => s == x.Id) && !x.EventUsers.Any(eu => eu.UserProfileId == x.Id));

            return _mapper.Map<List<UserProfileDto>>(userProfiles.ToList());
        }
    }
}
