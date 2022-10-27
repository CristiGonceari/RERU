using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTests
{
    public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var filterData = new TestFiltersDto
            {
                TestTemplateName = request.TestTemplateName,
                UserName = request.UserName,
                Email= request.Email,
                TestStatus = request.TestStatus,
                ResultStatus = request.ResultStatus,
                LocationKeyword = request.LocationKeyword,
                EventName = request.EventName,
                Idnp = request.Idnp,
                ProgrammedTimeFrom = request.ProgrammedTimeFrom,
                ProgrammedTimeTo = request.ProgrammedTimeTo,
                EvaluatorName = request.EvaluatorName,
                RoleId = request.RoleId,
                DepartmentId = request.DepartmentId
            };

            var tests = GetAndFilterTests.Filter(_appDbContext, filterData, currentUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Poll || x.TestTemplate.Mode == TestTemplateModeEnum.Test);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            paginatedModel = await CheckIfHasCandidatePosition(paginatedModel);

            foreach (var testDto in paginatedModel.Items)
            {
                var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == currentUser.Id && x.EventId == testDto.EventId);
                var testEvaluator = _appDbContext.Tests.FirstOrDefault(x => x.EvaluatorId == currentUser.Id && x.Id == testDto.Id);

                if (eventEvaluator != null)
                {
                    testDto.ShowUserName = eventEvaluator.ShowUserName;
                    testDto.IsEvaluator = true;
                }
                else if (testEvaluator != null && testEvaluator.ShowUserName != null)
                {
                    testDto.ShowUserName = (bool)testEvaluator.ShowUserName;
                    testDto.IsEvaluator = true;
                }
                else
                {
                    testDto.ShowUserName = true;
                    testDto.IsEvaluator = false;
                }
            }

            return paginatedModel;
        }

        private async Task<PaginatedModel<TestDto>> CheckIfHasCandidatePosition(PaginatedModel<TestDto> paginatedModel)
        {
            foreach (var item in paginatedModel.Items)
            {
                var eventUser = _appDbContext.EventUsers.FirstOrDefault(x => x.EventId == item.EventId && x.UserProfileId == item.UserId);

                if (eventUser == null) continue;

                var eventUserCandidatePositions =
                    _appDbContext.EventUserCandidatePositions.Where(x => x.EventUserId == eventUser.Id)
                        .Select(x => x.CandidatePositionId)
                        .ToList();

                if (!(eventUser?.PositionId > 0)) continue;
                {
                    var candidatePositionNames =
                        _appDbContext.CandidatePositions.Where(p => !eventUserCandidatePositions.All(p2 => p2 != p.Id))
                            .Select(x => x.Name)
                            .ToList();

                    item.CandidatePositionNames = candidatePositionNames;
                }
            }

            return paginatedModel;
        }
    }
}
