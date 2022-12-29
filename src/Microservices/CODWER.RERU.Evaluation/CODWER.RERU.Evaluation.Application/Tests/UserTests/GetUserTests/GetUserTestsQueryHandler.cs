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
using RERU.Data.Persistence.Extensions;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserTests
{
    public class GetUserTestsQueryHandler : IRequestHandler<GetUserTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ICurrentModuleService _currentModuleService;

        public GetUserTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, ICurrentModuleService currentModuleService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _currentModuleService = currentModuleService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetUserTestsQuery request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == request.UserId && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.CreateDate)
                .DistinctBy2(x => x.HashGroupKey != null ? x.HashGroupKey : x.Id.ToString())
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
