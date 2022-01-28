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

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetNoAssignedEvaluators
{
    public class GetNoAssignedEvaluatorsQueryHandler : IRequestHandler<GetNoAssignedEvaluatorsQuery, List<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNoAssignedEvaluatorsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDto>> Handle(GetNoAssignedEvaluatorsQuery request, CancellationToken cancellationToken)
        {
            var evaluators = _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Evaluator.Id)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                userProfiles = userProfiles.FilterByNameAndIdnp(request.Keyword);
            }

            userProfiles = userProfiles.Where(x => !evaluators.Any(e => e == x.Id));

            return _mapper.Map<List<UserProfileDto>>(userProfiles.ToList());
        }
    }
}
