using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;

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
            var curUser = await _userProfileService.GetCurrentUser();

            var tests = _appDbContext.Tests
                .Include(t => t.TestType)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .OrderByDescending(x => x.CreateDate)
                .Select(t => new Test
                {
                    Id = t.Id,
                    UserProfile = t.UserProfile,
                    TestType = t.TestType,
                    TestQuestions = t.TestQuestions,
                    Location = t.Location,
                    Event = t.Event,
                    AccumulatedPercentage = t.AccumulatedPercentage,
                    EvaluatorId = t.EvaluatorId,
                    EventId = t.EventId,
                    ResultStatus = t.ResultStatus,
                    TestStatus = t.TestStatus,
                    ProgrammedTime = t.ProgrammedTime,
                    EndTime = t.EndTime,
                    TestTypeId = t.TestTypeId,
                    TestPassStatus = t.TestPassStatus
                })
                .AsQueryable();

            if (request != null)
            {
                if (!string.IsNullOrWhiteSpace(request.TestTypeName))
                {
                    tests = tests.Where(x => EF.Functions.Like(x.TestType.Name, $"%{request.TestTypeName}%"));
                }

                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    tests = tests.Where(x => x.UserProfile.FirstName.Contains(request.UserName) || x.UserProfile.LastName.Contains(request.UserName) || x.UserProfile.Patronymic.Contains(request.UserName));
                }

                if (request.TestStatus.HasValue)
                {
                    tests = tests.Where(x => x.TestStatus == request.TestStatus);
                }

                if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
                {
                    tests = tests.Where(x => x.Location.Name.Contains(request.LocationKeyword) || x.Location.Address.Contains(request.LocationKeyword));
                }

                if (!string.IsNullOrWhiteSpace(request.EventName))
                {
                    tests = tests.Where(x =>x.Event.Name.Contains(request.EventName));
                }

                if (request.ProgrammedTimeFrom.HasValue)
                {
                    tests = tests.Where(x => x.ProgrammedTime >= request.ProgrammedTimeFrom);
                }

                if (request.ProgrammedTimeTo.HasValue)
                {
                    tests = tests.Where(x => x.ProgrammedTime <= request.ProgrammedTimeTo);
                }
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            foreach (var testDto in paginatedModel.Items)
            {
                var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.EventId == testDto.EventId);
                var testEvaluator = _appDbContext.Tests.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.Id == testDto.Id);

                if (eventEvaluator != null)
                {
                    testDto.ShowUserName = eventEvaluator.ShowUserName;
                }
                else if (testEvaluator != null && testEvaluator.ShowUserName != null)
                {
                    testDto.ShowUserName = (bool)testEvaluator.ShowUserName;
                }
                else
                {
                    testDto.ShowUserName = true;
                }
            }

            return paginatedModel;
        }
    }
}
