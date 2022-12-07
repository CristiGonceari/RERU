using System.Collections.Generic;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMediator _mediator;

        public GetTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _mediator = mediator;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var filterData = GetFilterData(request);

            var tests = GetAndFilterTests.Filter(_appDbContext, filterData, currentUser);

            tests = tests.Where(x => x.TestTemplate.Mode == TestTemplateModeEnum.Poll || x.TestTemplate.Mode == TestTemplateModeEnum.Test);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(tests, request);

            paginatedModel = await CheckIfHasCandidatePosition(paginatedModel);

            paginatedModel = await CheckTestEvaluator(paginatedModel, currentUser);

            paginatedModel = await CheckIfTestIsCalculatedBySystem(paginatedModel);

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
            DepartmentId = request.DepartmentId
        };

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

        private async Task<PaginatedModel<TestDto>> CheckTestEvaluator(PaginatedModel<TestDto> paginatedModel, UserProfileDto currentUser)
        {
            foreach (var testDto in paginatedModel.Items)
            {
                var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == currentUser.Id && x.EventId == testDto.EventId);
                var testEvaluator = _appDbContext.Tests.FirstOrDefault(x => x.EvaluatorId == currentUser.Id && x.Id == testDto.Id);

                if (testEvaluator != null)
                {
                    testDto.ShowUserName = testEvaluator.ShowUserName != null ? (bool)testEvaluator.ShowUserName : true;
                    testDto.IsEvaluator = true;
                }
                else if (eventEvaluator != null)
                {
                    testDto.ShowUserName = true;
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

        private async Task<PaginatedModel<TestDto>> CheckIfTestIsCalculatedBySystem(PaginatedModel<TestDto> paginatedModel)
        {
            foreach (var testDto in paginatedModel.Items)
            {
                var testTemplate = _appDbContext.TestTemplates
                    .Include(tt => tt.TestTemplateQuestionCategories)
                    .FirstOrDefault(tt => tt.Id == testDto.TestTemplateId);

                var testTemplateCategories = testTemplate.TestTemplateQuestionCategories
                    .Where(tt => tt.TestTemplateId == testTemplate.Id)
                    .ToList();

                var questionsList = new List<QuestionUnitDto>();

                foreach (var testTemplateCategory in testTemplateCategories)
                {
                    var testCategoryQuestionData = await _mediator.Send(new TestCategoryQuestionsQuery { TestTemplateQuestionCategoryId = testTemplateCategory.Id });

                    questionsList.AddRange(testCategoryQuestionData.Questions);
                    testDto.IsVerificatedAutomat = questionsList.All(x => x.QuestionType == QuestionTypeEnum.OneAnswer || x.QuestionType == QuestionTypeEnum.MultipleAnswers);
                }

                if (testTemplateCategories.All(tt => tt.QuestionType == QuestionTypeEnum.OneAnswer || tt.QuestionType == QuestionTypeEnum.MultipleAnswers))
                {
                    testDto.IsVerificatedAutomat = true;
                }
            }
           
            return paginatedModel;
        }
    }
}
