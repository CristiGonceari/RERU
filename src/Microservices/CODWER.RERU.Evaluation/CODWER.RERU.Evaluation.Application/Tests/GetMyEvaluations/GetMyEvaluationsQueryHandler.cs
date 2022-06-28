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

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluations
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
            var currentUser = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == currentUser.Id && t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request);
        }
    }
}
