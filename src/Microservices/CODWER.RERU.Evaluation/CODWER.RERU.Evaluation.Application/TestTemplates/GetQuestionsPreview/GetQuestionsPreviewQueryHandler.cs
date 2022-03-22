﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetQuestionsPreview
{
    public class GetQuestionsPreviewQueryHandler : IRequestHandler<GetQuestionsPreviewQuery, List<QuestionUnitPreviewDto>>
    {
        private readonly Random _random;
        private readonly AppDbContext _appDbContext;
        private readonly List<TestQuestion> _testQuestions;
        private readonly IMapper _mapper;

        public GetQuestionsPreviewQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _random = new Random();
            _appDbContext = appDbContext;
            _mapper = mapper;
            _testQuestions = new List<TestQuestion>();
        }

        public async Task<List<QuestionUnitPreviewDto>> Handle(GetQuestionsPreviewQuery request, CancellationToken cancellationToken)
        {
            var testTemplate = await _appDbContext.TestTemplates
                .Include(x => x.TestTemplateQuestionCategories)
                .Include(x => x.Settings)
                .FirstAsync(x => x.Id == request.TestTemplateId);

            var totalCount = testTemplate.QuestionCount;

            var testTemplateQuestionCategoriesToUse = testTemplate.TestTemplateQuestionCategories;
            var categoriesCount = testTemplateQuestionCategoriesToUse.Count;

            var index = 1;
            int questionsPerCategory = 0;
            var remainsCategoriesCount = categoriesCount;

            if (testTemplate.CategoriesSequence == SequenceEnum.Strict)
            {
                testTemplateQuestionCategoriesToUse = testTemplateQuestionCategoriesToUse.OrderBy(x => x.CategoryIndex).ToList();
            }
            else
            {
                testTemplateQuestionCategoriesToUse = testTemplateQuestionCategoriesToUse.OrderBy(x => Guid.NewGuid()).ToList();
            }

            if (testTemplateQuestionCategoriesToUse.Any(x => x.QuestionCount == 0))
            {
                questionsPerCategory = (totalCount - testTemplateQuestionCategoriesToUse.Where(x => x.QuestionCount.HasValue).Sum(x => x.QuestionCount.Value)) / testTemplateQuestionCategoriesToUse.Where(x => !x.QuestionCount.HasValue).Count();
            }
            foreach (var category in testTemplateQuestionCategoriesToUse)
            {
                int categoryQuestionsCount;
                if (category.QuestionCount.HasValue)
                {
                    categoryQuestionsCount = category.QuestionCount.Value;
                }
                else
                {
                    if (remainsCategoriesCount == 1)
                    {
                        categoryQuestionsCount = questionsPerCategory + (totalCount - testTemplateQuestionCategoriesToUse.Where(x => x.QuestionCount.HasValue).Sum(x => x.QuestionCount.Value) - testTemplateQuestionCategoriesToUse.Where(x => !x.QuestionCount.HasValue).Count() * questionsPerCategory);
                    }
                    else
                    {
                        categoryQuestionsCount = questionsPerCategory;
                    }

                }
                index = await PreworkCategory(category.Id, categoryQuestionsCount, index);
                remainsCategoriesCount--;
            }

            var answer = new List<QuestionUnitPreviewDto>();

            foreach (var testQuestion in _testQuestions.OrderBy(x => x.Index))
            {
                var qUnit = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == testQuestion.Id);
                var qUnitToAdd = _mapper.Map<QuestionUnitPreviewDto>(qUnit);
                qUnitToAdd.Index = testQuestion.Index;
                answer.Add(qUnitToAdd);
            }

            return answer.OrderBy(x => x.Index).ToList();
        }

        private async Task<int> PreworkCategory(int testTemplateQuestionCategoryId, int questionCount, int index)
        {
            var testTemplateQuestionCategory = await _appDbContext.TestTemplateQuestionCategories.FirstAsync(x => x.Id == testTemplateQuestionCategoryId);

            var allQuestions = _appDbContext.QuestionUnits
                    .Where(x => x.QuestionCategoryId == testTemplateQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                    .AsQueryable();

            if (testTemplateQuestionCategory.QuestionType.HasValue) //&& Enum.IsDefined(typeof(QuestionTypeEnum), questionType)
            {
                allQuestions = allQuestions.Where(x => x.QuestionType == testTemplateQuestionCategory.QuestionType.Value);
            }

            var questionIds = await allQuestions.Select(x => x.Id).ToListAsync();
            var strictQuestionsToUse = await _appDbContext.TestCategoryQuestions
                .Include(x => x.QuestionUnit)
                .Where(x => x.TestTemplateQuestionCategoryId == testTemplateQuestionCategoryId)
                .ToListAsync();

            if (testTemplateQuestionCategory.SelectionType == SelectionEnum.Select)
            {
                questionIds = strictQuestionsToUse.Select(x => x.QuestionUnitId).ToList();
            }

            for (int i = 1; i <= questionCount; i++)
            {
                int questionId;
                if (testTemplateQuestionCategory.SequenceType == SequenceEnum.Random)
                {
                    questionId = RandomThis(questionIds);
                    questionIds.Remove(questionId);
                }
                else
                {
                    questionId = strictQuestionsToUse.First(x => x.Index == i).QuestionUnitId;
                }

                var testQuestionToAdd = new TestQuestion
                {
                    Id = questionId,
                    Index = index,
                    QuestionUnitId = questionId,
                    Verified = null,
                    AnswerStatus = (int)AnswerStatusEnum.None
                };
                _testQuestions.Add(testQuestionToAdd);
                index++;
            }

            return index;
        }

        private int RandomThis(List<int> input)
        {
            var index = _random.Next(0, input.Count);
            return input[index];
        }
    }
}
