using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTests
{
    public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private PaginatedModel<TestDto> _paginatedModel;

        public GetTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            await TestV2(request, cancellationToken);

            return _paginatedModel;
        }

        private async Task TestV2(GetTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _appDbContext.UserProfiles.Where(x => x.Id == 1)
                    .Select(up => new UserProfileDto { Id = up.Id, FirstName = up.FirstName, LastName = up.LastName, AccessModeEnum = up.AccessModeEnum }).First();

            var filterData = GetFilterData(request);

            var testTool = await GetAndFilterTestsOptimizedv2.Filter(_appDbContext, filterData, currentUser);

            var queryable = testTool.Queryable.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Test);

            var count = testTool.SelectedTestsIds.Count();
            var skipCount = (request.Page - 1) * request.ItemsPerPage;

            var items = testTool.SelectedTestsIds.Skip(skipCount).Take(request.ItemsPerPage).ToList();

            _paginatedModel = await _paginationService.MapPageAsync<Test, TestDto>(queryable.Where(t => items.Contains(t.Id)), request, count);

            await CheckIfHasCandidatePosition();
            await CheckTestEvaluator(currentUser);
        }

        private TestFiltersDto GetFilterData(GetTestsQuery request) => new TestFiltersDto
        {
            TestTemplateName = request.TestTemplateName,
            UserName = request.UserName,
            Email = request.Email,
            TestStatus = request.TestStatus,
            ResultStatus = request.ResultStatus,
            LocationKeyword = request.LocationKeyword,
            EventName = request.EventName,
            Idnp = request.Idnp,
            ProgrammedTimeFrom = request.ProgrammedTimeFrom,
            ProgrammedTimeTo = request.ProgrammedTimeTo,
            EvaluatorName = request.EvaluatorName,
            RoleId = request.RoleId,
            FunctionId = request.FunctionId,
            DepartmentId = request.DepartmentId,
            ColaboratorId = request.ColaboratorId,
        };

        private async Task<PaginatedModel<TestDto>> CheckIfHasCandidatePosition()
        {
            var itemsEvents = _paginatedModel.Items.Select(i => i.EventId);
            var itemsUsers = _paginatedModel.Items.Select(i => i.UserId);

            var eventUsers = _appDbContext.EventUsers
                .Include(x => x.EventUserCandidatePositions)
                .ThenInclude(x => x.CandidatePosition)
                .Where(x => itemsEvents.Contains(x.EventId) && itemsUsers.Contains(x.UserProfileId))
                .Select(x => new EventUser{
                    EventId = x.EventId,
                    UserProfileId = x.UserProfileId
                })
                .ToList();

            _paginatedModel.Items = _paginatedModel.Items.Select(item =>
            {
                var eventUser = eventUsers.FirstOrDefault(x => x.EventId == item.EventId && x.UserProfileId == item.UserId);

                if (eventUser == null ||
                    eventUser.PositionId == null ||
                    eventUser.PositionId == 0
                    ) return item;

                item.CandidatePositionNames = eventUser.EventUserCandidatePositions
                    .Select(x => x.CandidatePosition.Name)
                    .ToList();

                return item;
            }).ToList();

            return _paginatedModel;
        }

        private async Task<PaginatedModel<TestDto>> CheckTestEvaluator(UserProfileDto currentUser)
        {
            var testEventIds = _paginatedModel.Items.Select(x => x.EventId).ToList();

            var eventEvaluators = _appDbContext.EventEvaluators
                .Where(x => testEventIds.Contains(x.EventId))
                .Select(x => new EventEvaluator{
                    EventId = x.EventId,
                    EvaluatorId = x.EvaluatorId
                })
                .ToList();

            _paginatedModel.Items = _paginatedModel.Items.Select(item =>
            {
                var testEventEvaluators = eventEvaluators.Where(x => x.EventId == item.EventId);

                item.IsEvaluator = item.EvaluatorId == currentUser.Id;

                if (item.EvaluatorId == null && testEventEvaluators.Any())
                {
                    item.IsEvaluator = testEventEvaluators.Any(e => e.EvaluatorId == currentUser.Id) || item.CreateById == currentUser.Id.ToString();
                }

                return item;
            }).ToList();

            return _paginatedModel;
        }
    }
}
