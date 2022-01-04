using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeByStatus
{
    public class GetTestTypeByStatusQueryHandler : IRequestHandler<GetTestTypeByStatusQuery, List<SelectTestTypeValueDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetTestTypeByStatusQueryHandler(AppDbContext appDbContext, IMapper mapper, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<SelectTestTypeValueDto>> Handle(GetTestTypeByStatusQuery request, CancellationToken cancellationToken)
        {
            var testTypes = _appDbContext.TestTypes
                .Include(x => x.Settings)
                .Where(x => x.Status == request.TestTypeStatus && x.Mode == (int)TestTypeModeEnum.Test)
                .AsQueryable();

            if (request.EventId.HasValue)
            {
                testTypes = testTypes
                    .Include(x => x.EventTestTypes)
                    .Where(x => x.EventTestTypes.Any(e => e.EventId == request.EventId));
            }

            var onlyOneAnswerTests = testTypes.Select(x => _mapper.Map<SelectTestTypeValueDto>(x)).ToList();

            foreach (var x in onlyOneAnswerTests)
            {
                var testType = testTypes
                    .Include(tt => tt.TestTypeQuestionCategories)
                    .FirstOrDefault(tt => tt.Id == x.TestTypeId);

                var testTypeCategories = testType.TestTypeQuestionCategories
                    .Where(tt => tt.TestTypeId == testType.Id)
                    .ToList();

                var questionsList = new List<QuestionUnitDto>();

                foreach (var testTypeCategory in testTypeCategories)
                {
                    var testCategoryQuestionData = await _mediator.Send(new TestCategoryQuestionsQuery { TestTypeQuestionCategoryId = testTypeCategory.Id });

                    questionsList.AddRange(testCategoryQuestionData.Questions);
                    x.IsOnlyOneAnswer = questionsList.All(x => x.QuestionType == QuestionTypeEnum.OneAnswer);
                }

                if (testTypeCategories.All(tt => tt.QuestionType == QuestionTypeEnum.OneAnswer))
                {
                    x.IsOnlyOneAnswer = true;
                }

                x.PrintTest = await CanPrintTest(testTypeCategories);
            }

            return onlyOneAnswerTests;
        }

        private async Task<bool> CanPrintTest(List<TestTypeQuestionCategory> testTypeCategories)
        {
            var mediaList = new List<string>() { "video", "audio" };
            var result = false;
            var filesIdsList = new List<string>();

            var questions = await _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.Options)
                .Where(q => testTypeCategories
                    .Select(x => x.QuestionCategoryId)
                    .Contains(q.QuestionCategoryId))
                .ToListAsync();

            var mediaOptionsIds = await _appDbContext.Options
                .Where(o => questions
                    .Select(x => x.Id)
                    .Contains(o.QuestionUnitId))
                .Where(o => o.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToListAsync();

            var mediaQuestionsIds = questions
                .Where(q => q.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToList();

            filesIdsList.AddRange(mediaOptionsIds);
            filesIdsList.AddRange(mediaQuestionsIds);

            var files = _appDbContext.Files
                .Where(f => filesIdsList.Contains(f.Id.ToString()))
                .ToList();

            result = !files.Any(x => mediaList.Any(m => x.Type.Contains(m)));

            return result;
        }
    }
}
