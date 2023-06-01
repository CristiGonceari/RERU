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

namespace CODWER.RERU.Evaluation.Application.Tests.GetEvaluations
{
    public class GetEvaluationsQueryHandler : IRequestHandler<GetEvaluationsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetEvaluationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var filterData = new TestFiltersDto
            {
                TestTemplateName = request.TestTemplateName,
                UserName = request.UserName,
                Email = request.Email,
                TestStatus = request.TestStatus,
                ResultStatus = request.ResultStatus,
                LocationKeyword = request.LocationKeyword,
                EventName = request.EventName,
                ProgrammedTimeFrom = request.ProgrammedTimeFrom,
                ProgrammedTimeTo = request.ProgrammedTimeTo,
                EvaluatorName = request.EvaluatorName,
                DepartmentId = request.DepartmentId,
                RoleId = request.RoleId,
                FunctionId = request.FunctionId
            };

            var tests = GetAndFilterTestsOptimized.Filter(_appDbContext, filterData, currentUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Evaluation);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            return await CheckIfHasCandidatePosition(paginatedModel);
        }

        private async Task<PaginatedModel<TestDto>> CheckIfHasCandidatePosition2(PaginatedModel<TestDto> paginatedModel)
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

        private async Task<PaginatedModel<TestDto>> CheckIfHasCandidatePosition(PaginatedModel<TestDto> paginatedModel)
        {
            //pregatim datele
            var itemsEvents = paginatedModel.Items.Select(i => i.EventId);
            var itemsUsers = paginatedModel.Items.Select(i => i.UserId);

            //trimitem un singur request la DB optimizat
            var eventUsers = _appDbContext.EventUsers
                .Include(x => x.EventUserCandidatePositions)
                .ThenInclude(x => x.CandidatePosition)
                .Where(x => itemsEvents.Contains(x.EventId) && itemsUsers.Contains(x.UserProfileId))
                .Select(x => new EventUser{
                    EventId = x.EventId,
                    UserProfileId = x.UserProfileId
                })
                .ToList();

            foreach (var item in paginatedModel.Items)
            {
                var eventUser = eventUsers.FirstOrDefault(x => x.EventId == item.EventId && x.UserProfileId == item.UserId); // nu mai facem call la DB dar la lista

                if (eventUser is null  // check eventUser && eventUser.PositionId 
                    || eventUser.PositionId is null
                    || eventUser.PositionId == 0) continue;

                item.CandidatePositionNames = eventUser.EventUserCandidatePositions //nu mai faci call la DB dar iai itemi din lista
                        .Select(x => x.CandidatePosition.Name)
                        .ToList();
            }

            return paginatedModel;
        }
    }
}
