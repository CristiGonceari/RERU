using CVU.ERP.Common.Pagination;
using MediatR;
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

            var filterData = new TestFiltersDto
            {
                TestTemplateName = request.TestTemplateName,
                UserName = request.UserName,
                TestStatus = request.TestStatus,
                LocationKeyword = request.LocationKeyword,
                EventName = request.EventName,
                Idnp = request.Idnp,
                ProgrammedTimeFrom = request.ProgrammedTimeFrom,
                ProgrammedTimeTo = request.ProgrammedTimeTo
            };

            var tests = GetAndFilterTests.Filter(_appDbContext, filterData);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            foreach (var testDto in paginatedModel.Items)
            {
                var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.EventId == testDto.EventId);
                var testEvaluator = _appDbContext.Tests.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.Id == testDto.Id);

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
    }
}
