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

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserEvaluations
{
    public class GetUserEvaluationsQueryHandler : IRequestHandler<GetUserEvaluationsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ICurrentModuleService _currentModuleService;


        public GetUserEvaluationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, ICurrentModuleService currentModuleService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _currentModuleService = currentModuleService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetUserEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var evaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == request.UserId && t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();

            evaluations = await FilterUsersTestsByModuleRole(evaluations);

            if (request.FromDate.HasValue)
            {
                evaluations = evaluations.Where(x => x.EndTime >= request.FromDate);
            }

            if (request.ToDate.HasValue)
            {
                evaluations = evaluations.Where(x => x.EndTime <= request.ToDate);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(evaluations, request);

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
