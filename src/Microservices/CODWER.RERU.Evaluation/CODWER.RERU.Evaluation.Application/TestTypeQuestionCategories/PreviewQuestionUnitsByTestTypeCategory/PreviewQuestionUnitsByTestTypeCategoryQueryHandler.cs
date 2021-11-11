using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.PreviewQuestionUnitsByTestTypeCategory
{
    public class PreviewQuestionUnitsByTestTypeCategoryQueryHandler : IRequestHandler<PreviewQuestionUnitsByTestTypeCategoryQuery, List<CategoryQuestionUnitDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly Random _random;

        public PreviewQuestionUnitsByTestTypeCategoryQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _random = new Random();
        }

        public async Task<List<CategoryQuestionUnitDto>> Handle(PreviewQuestionUnitsByTestTypeCategoryQuery request, CancellationToken cancellationToken)
        {
            var questionCategory = await _appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                    .ThenInclude(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == request.Data.CategoryId);

            var questionsToUse = questionCategory.QuestionUnits.Where(x => x.Status == QuestionUnitStatusEnum.Active);

            var answer = new List<CategoryQuestionUnitDto>();

            if (request.Data.QuestionType.HasValue)
            {
                questionsToUse = questionsToUse.Where(x => x.QuestionType == request.Data.QuestionType.Value).ToList();
            }

            if (request.Data.SelectionType == SelectionEnum.Select)
            {
                questionsToUse = questionsToUse.Where(x => request.Data.SelectedQuestions.Select(s => s.Id).Contains(x.Id)).ToList();
            }

            if (request.Data.SequenceType == SequenceEnum.Strict)
            {
                foreach (var question in questionsToUse)
                {
                    var questionDtoToAdd = new CategoryQuestionUnitDto()
                    {
                        QuestionUnitId = question.Id,
                        QuestionType = question.QuestionType,
                        Question = question.Question,
                        Status = question.Status,
                        OptionsCount = question.Options.Count,
                        Index = request.Data.SelectedQuestions.FirstOrDefault(s => s.Id == question.Id).Index
                    };

                    answer.Add(questionDtoToAdd);
                }
            }
            else
            {
                int count;
                if (request.Data.QuestionCount.HasValue)
                {
                    count = request.Data.QuestionCount.Value;
                }
                else
                {
                    var testType = await _appDbContext.TestTypes
                        .Include(x => x.TestTypeQuestionCategories)
                        .FirstAsync(x => x.Id == request.Data.TestTypeId);

                    count = testType.QuestionCount - testType.TestTypeQuestionCategories.Sum(x => x.QuestionCount.Value);
                }

                var questionIds = questionsToUse.Select(x => x.Id).ToList();

                for (int i = 1; i <= count; i++)
                {
                    var questionId = RandomThis(questionIds);
                    questionIds.Remove(questionId);
                    var question = questionsToUse.First(x => x.Id == questionId);

                    var questionDtoToAdd = new CategoryQuestionUnitDto()
                    {
                        QuestionUnitId = question.Id,
                        QuestionType = question.QuestionType,
                        Question = question.Question,
                        Status = question.Status,
                        OptionsCount = question.Options.Count,
                        Index = i
                    };

                    answer.Add(questionDtoToAdd);
                }
            }

            if (request.Data.SequenceType == SequenceEnum.Strict)
            {
                answer = answer.OrderBy(x => x.Index).ToList();
            }
            else
            {
                answer = answer.OrderBy(x => Guid.NewGuid()).ToList();
            }
            return answer;
        }

        private int RandomThis(List<int> input)
        {
            var index = _random.Next(0, input.Count);
            return input[index];
        }
    }
}
