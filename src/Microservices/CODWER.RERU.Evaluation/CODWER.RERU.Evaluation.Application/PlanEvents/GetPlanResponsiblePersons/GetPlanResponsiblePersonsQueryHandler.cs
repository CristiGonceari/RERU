using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanResponsiblePersons
{
    public class GetPlanResponsiblePersonsQueryHandler : IRequestHandler<GetPlanResponsiblePersonsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetPlanResponsiblePersonsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetPlanResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.PlanResponsiblePersons
                .Include(x => x.UserProfile)
                .Where(x => x.PlanId == request.PlanId)
                .Select(x => x.UserProfile)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(responsiblePersons, request);
        }
    }

}
