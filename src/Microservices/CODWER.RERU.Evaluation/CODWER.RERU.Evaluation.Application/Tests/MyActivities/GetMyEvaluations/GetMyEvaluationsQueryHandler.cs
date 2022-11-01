using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyEvaluations
{
    public class GetMyEvaluationsQueryHandler : IRequestHandler<GetMyEvaluationsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetMyEvaluationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }
        public async Task<PaginatedModel<TestDto>> Handle(GetMyEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myEvaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == currentUserId && t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (request.FromDate.HasValue)
            {
                myEvaluations = myEvaluations.Where(x => x.EndTime >= request.FromDate);
            }

            if (request.ToDate.HasValue)
            {
                myEvaluations = myEvaluations.Where(x => x.EndTime <= request.ToDate);
            }

            if (!string.IsNullOrWhiteSpace(request.EvaluatedName))
            {
                myEvaluations = myEvaluations.Where(x => x.UserProfile.FirstName.ToLower().Contains(request.EvaluatedName.ToLower())
                                         || x.UserProfile.LastName.ToLower().Contains(request.EvaluatedName.ToLower())
                                         || x.UserProfile.FatherName.ToLower().Contains(request.EvaluatedName.ToLower())
                                         || x.UserProfile.Idnp.ToLower().Contains(request.EvaluatedName.ToLower()));
            }

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myEvaluations, request);
        }
    }
}
