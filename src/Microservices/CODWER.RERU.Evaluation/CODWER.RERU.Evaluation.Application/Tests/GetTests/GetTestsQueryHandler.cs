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

        public GetTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var filterData = GetFilterData(request);

            var tests = GetAndFilterTestsOptimized.Filter(_appDbContext, filterData, currentUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Poll || x.TestTemplate.Mode == TestTemplateModeEnum.Test);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            paginatedModel = await CheckIfHasCandidatePosition(paginatedModel);

            paginatedModel = await CheckTestEvaluator(paginatedModel, currentUser);

            return paginatedModel;
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

        private async Task<PaginatedModel<TestDto>> CheckIfHasCandidatePosition2(PaginatedModel<TestDto> paginatedModel)
        {
            //daca avem 10 itemi, in foreach se face pentru fiecare 1 request la DB adica in total o sa avem 30 requesturi la DB 
            foreach (var item in paginatedModel.Items)
            {
                var eventUser = _appDbContext.EventUsers.FirstOrDefault(x => x.EventId == item.EventId && x.UserProfileId == item.UserId);

                if (eventUser == null) continue;

                var eventUserCandidatePositions =
                    _appDbContext.EventUserCandidatePositions.Where(x => x.EventUserId == eventUser.Id) //dc .where() nu este din rand nou ?  
                        .Select(x => x.CandidatePositionId)
                        .ToList();

                if (!(eventUser?.PositionId > 0)) continue; //wtf ? se putea de scris position == 0
                {
                    var candidatePositionNames =
                        _appDbContext.CandidatePositions.Where(p => !eventUserCandidatePositions.All(p2 => p2 != p.Id))// wtf? se putea de scris simplu, de ce sa faci atatea negatii care incurca la citire cod?
                            .Select(x => x.Name)                                                                        // eventUserCandidatePositions.Any(p2 => p2 == p.Id) sau daje
                            .ToList();                                                                                  // eventUserCandidatePositions.Contains(p.Id) si era mai clar

                    item.CandidatePositionNames = candidatePositionNames;
                }
            }

            return paginatedModel;
        }

        private async Task<PaginatedModel<TestDto>> CheckTestEvaluator(PaginatedModel<TestDto> paginatedModel, UserProfileDto currentUser)
        {
            var testEventIds = paginatedModel.Items.Select(x => x.EventId).ToList(); //pregatim datele

            var eventEvaluators = _appDbContext.EventEvaluators // evitam 10 requesturi la DB
                .Where(x => testEventIds.Contains(x.EventId))
                .ToList();

            foreach (var testDto in paginatedModel.Items)
            {
                var testEventEvaluators = eventEvaluators.Where(x => x.EventId == testDto.EventId);

                testDto.IsEvaluator = testDto.CreateById == currentUser.Id.ToString() || testDto.EvaluatorId == currentUser.Id;

                if (testDto.EvaluatorId == null && testEventEvaluators.Any())
                {
                    testDto.IsEvaluator = testEventEvaluators.Any(e => e.EvaluatorId == currentUser.Id) || testDto.CreateById == currentUser.Id.ToString();
                }
            }

            return paginatedModel;
        }

        private async Task<PaginatedModel<TestDto>> CheckTestEvaluator2(PaginatedModel<TestDto> paginatedModel, UserProfileDto currentUser)
        {
            // 10 requesturi la DB
            foreach (var testDto in paginatedModel.Items)
            {
                var eventEvaluators = _appDbContext.EventEvaluators.Where(x => x.EventId == testDto.EventId);

                testDto.IsEvaluator = testDto.CreateById == currentUser.Id.ToString() || testDto.EvaluatorId == currentUser.Id;

                if (testDto.EvaluatorId == null && eventEvaluators.Any())
                {
                    testDto.IsEvaluator = eventEvaluators.Any(e => e.EvaluatorId == currentUser.Id) || testDto.CreateById == currentUser.Id.ToString();
                }
            }

            return paginatedModel;
        }
    }
}
