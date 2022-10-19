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

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserEvaluatedTests
{
    public class GetUserEvaluatedTestsQueryHandler : IRequestHandler<GetUserEvaluatedTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ICurrentModuleService _currentModuleService;

        public GetUserEvaluatedTestsQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            ICurrentModuleService currentModuleService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _currentModuleService = currentModuleService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetUserEvaluatedTestsQuery request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => (t.EvaluatorId == request.UserId ||
                             _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == request.UserId)) && 
                            t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .AsQueryable();

            userTests = await FilterUsersTestsByModuleRole(userTests);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(userTests, request);

            return paginatedModel;
        }

        public async Task<IQueryable<Test>> FilterUsersTestsByModuleRole(IQueryable<Test> userTests)
        {
            var userCurrentRole = await _currentModuleService.GetUserCurrentModuleRole();

            var currentUserProfile = await _currentModuleService.GetCurrentUserProfile();

            userTests = FilterByModuleRole.Filter(userTests, userCurrentRole, currentUserProfile);

            return userTests;
        }
    }
}
