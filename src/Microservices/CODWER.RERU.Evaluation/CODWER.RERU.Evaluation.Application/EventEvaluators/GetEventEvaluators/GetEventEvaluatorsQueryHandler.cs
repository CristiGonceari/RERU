using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetEventEvaluators
{
    public class GetEventEvaluatorsQueryHandler : IRequestHandler<GetEventEvaluatorsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventEvaluatorsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetEventEvaluatorsQuery request, CancellationToken cancellationToken)
        {
            var eventEvaluators = _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Evaluator)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(eventEvaluators, request);
        }
    }
}
