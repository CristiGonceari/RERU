﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions
{
    public class TestCategoryQuestionsQueryHandler : IRequestHandler<TestCategoryQuestionsQuery, TestCategoryQuestionContentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TestCategoryQuestionsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext.NewInstance();
            _mapper = mapper;
        }

        public async Task<TestCategoryQuestionContentDto> Handle(TestCategoryQuestionsQuery request, CancellationToken cancellationToken)
        {
            var testTemplateQuestionCategory = await _appDbContext.TestTemplateQuestionCategories
                                                                .Include(ttqc => ttqc.QuestionCategory)
                                                                .FirstAsync(x => x.Id == request.TestTemplateQuestionCategoryId);

            var result = new TestCategoryQuestionContentDto();
            result.SequenceType = testTemplateQuestionCategory.SequenceType;
            result.QuestionCategoryName = testTemplateQuestionCategory.QuestionCategory.Name;

            
            if (testTemplateQuestionCategory.SequenceType == SequenceEnum.Random && testTemplateQuestionCategory.SelectionType == SelectionEnum.All)
            {

                var allQuestions = _appDbContext.QuestionUnits
                                                    .Where(x => x.QuestionCategoryId == testTemplateQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                                                    .AsQueryable();

                result.Questions = _mapper.Map<List<QuestionUnitDto>>(allQuestions);
                result.UsedQuestionCount = testTemplateQuestionCategory.QuestionCount;

            }
            else
            {

                var questionUnits = await _appDbContext.TestCategoryQuestions
                                                            .Include(tcq => tcq.TestTemplateQuestionCategory)
                                                            .Include(tcq => tcq.QuestionUnit)
                                                            .Where(x => x.TestTemplateQuestionCategoryId == request.TestTemplateQuestionCategoryId)
                                                            .Select(x => x.QuestionUnit)
                                                            .ToListAsync();

                result.Questions = _mapper.Map<List<QuestionUnitDto>>(questionUnits);
                result.UsedQuestionCount = testTemplateQuestionCategory.QuestionCount;
            }
           
            return result;
        }
    }
}
