using AutoMapper;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using System;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestions
{
    public class GetTestQuestionsQueryHandler : IRequestHandler<GetTestQuestionsQuery, PaginatedModel<TestQuestionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IPaginationService _paginationService;
        private readonly Random _random;

        public GetTestQuestionsQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _paginationService = paginationService;
            _random = new Random();
        }

        public async Task<PaginatedModel<TestQuestionDto>> Handle(GetTestQuestionsQuery request, CancellationToken cancellationToken)
        {
            var testQuestions = _appDbContext.TestQuestions
                .Include(x => x.QuestionUnit)
                    .ThenInclude(x => x.Options)
                    .ThenInclude(x => x.QuestionUnit.QuestionCategory)
                .Where(x => x.TestId == request.TestId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<TestQuestion, TestQuestionDto>(testQuestions, request);

            foreach (var testQuestionDto in paginatedModel.Items)
            {

                if (testQuestionDto.QuestionType == QuestionTypeEnum.HashedAnswer)
                {
                    testQuestionDto.Question = (await _questionUnitService.GetHashedQuestionUnit(testQuestionDto.QuestionUnitId)).Question;
                }

                if (testQuestionDto.QuestionType == QuestionTypeEnum.MultipleAnswers || testQuestionDto.QuestionType == QuestionTypeEnum.OneAnswer)
                {
                    var options = testQuestionDto.Options.Where(x => x.QuestionUnitId == testQuestionDto.QuestionUnitId).OrderBy(opt => _random.Next()).ToList();
                    testQuestionDto.Options = options;
                }

                if (testQuestionDto.AnswerStatus == AnswerStatusEnum.Answered)
                {
                    if (testQuestionDto.QuestionType == QuestionTypeEnum.FreeText)
                    {
                        testQuestionDto.AnswerText = _appDbContext.TestQuestionsTestAnswers
                            .Include(x => x.TestAnswer)
                            .FirstOrDefault(x => x.TestQuestionId == testQuestionDto.Id)?.TestAnswer.AnswerValue;
                    }
                    else
                    {
                        var savedAnswers = _appDbContext.TestQuestionsTestAnswers
                            .Include(x => x.TestAnswer)
                            .Where(x => x.TestQuestionId == testQuestionDto.Id)
                            .Select(x => x.TestAnswer)
                            .ToList();

                        foreach (var savedAnswer in savedAnswers)
                        {
                            var option = testQuestionDto.Options.FirstOrDefault(x => x.Id == savedAnswer.OptionId);

                            if (testQuestionDto.QuestionType != QuestionTypeEnum.HashedAnswer)
                            {
                                option.IsSelected = true;

                                if (!string.IsNullOrEmpty(savedAnswer.AnswerValue))
                                {
                                    option.Answer = savedAnswer.AnswerValue;
                                }
                            }
                            else
                            {
                                testQuestionDto.Question = testQuestionDto.Question.Replace($"optionId='{savedAnswer.OptionId}'>", $"optionId='{savedAnswer.OptionId}'>{savedAnswer.AnswerValue}");
                                testQuestionDto.HashedOptions.FirstOrDefault(x => x.Id == savedAnswer.OptionId).Answer = savedAnswer.AnswerValue;
                            }

                        }
                    }

                }

            }

            return paginatedModel;
        }
    }
}
