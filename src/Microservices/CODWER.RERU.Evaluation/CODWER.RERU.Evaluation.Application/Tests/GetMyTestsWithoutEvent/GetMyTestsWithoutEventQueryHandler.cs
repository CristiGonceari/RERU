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

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent
{
    public class GetMyTestsWithoutEventQueryHandler : IRequestHandler<GetMyTestsWithoutEventQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMyTestsWithoutEventQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyTestsWithoutEventQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestType)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == myUserProfile.Id && t.Event == null)
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();

            var paginatedModel = _paginationService.MapAndPaginateModel<Test, TestDto>(myTests, request);

            foreach (var myTest in paginatedModel.Items)
            {
                var testType = await _appDbContext.TestTypes
                    .Include(tt => tt.Settings)
                    .FirstOrDefaultAsync(tt => tt.Id == myTest.TestTypeId);

                if (testType.Settings.CanViewResultWithoutVerification)
                {
                    myTest.ViewTestResult = true;
                }
            }

            return paginatedModel;
        }
    }
}
