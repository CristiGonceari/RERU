using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions
{
    public class TestCategoryQuestionsQueryHandler : IRequestHandler<TestCategoryQuestionsQuery, TestCategoryQuestionContentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TestCategoryQuestionsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestCategoryQuestionContentDto> Handle(TestCategoryQuestionsQuery request, CancellationToken cancellationToken)
        {
            var testTypeQuestionCategory =await _appDbContext.TestTypeQuestionCategories
                                                                .Include(ttqc => ttqc.QuestionCategory)
                                                                .FirstAsync(x => x.Id == request.TestTypeQuestionCategoryId);

            var result = new TestCategoryQuestionContentDto();
            result.SequenceType = testTypeQuestionCategory.SequenceType;
            result.QuestionCategoryName = testTypeQuestionCategory.QuestionCategory.Name;

            
            if (testTypeQuestionCategory.SequenceType == SequenceEnum.Random && testTypeQuestionCategory.SelectionType == SelectionEnum.All)
            {

                var allQuestions = _appDbContext.QuestionUnits
                                                    .Where(x => x.QuestionCategoryId == testTypeQuestionCategory.QuestionCategoryId && x.Status == QuestionUnitStatusEnum.Active)
                                                    .AsQueryable();

                result.Questions = _mapper.Map<List<QuestionUnitDto>>(allQuestions);
                result.UsedQuestionCount = testTypeQuestionCategory.QuestionCount;

            }
            else
            {

                var questionUnits = await _appDbContext.TestCategoryQuestions
                                                            .Include(tcq => tcq.TestTypeQuestionCategory)
                                                            .Include(tcq => tcq.QuestionUnit)
                                                            .Where(x => x.TestTypeQuestionCategoryId == request.TestTypeQuestionCategoryId)
                                                            .Select(x => x.QuestionUnit)
                                                            .ToListAsync();

                result.Questions = _mapper.Map<List<QuestionUnitDto>>(questionUnits);
                result.UsedQuestionCount = testTypeQuestionCategory.QuestionCount;
            }
           
            return result;
        }
    }
}
