using AutoMapper;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.StorageService.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus
{
    public class GetTestTemplateByStatusQueryHandler : IRequestHandler<GetTestTemplateByStatusQuery, List<SelectTestTemplateValueDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly StorageDbContext _storageDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetTestTemplateByStatusQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            IMediator mediator, 
            StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _mediator = mediator;
            _storageDbContext = storageDbContext;
        }

        public async Task<List<SelectTestTemplateValueDto>> Handle(GetTestTemplateByStatusQuery request, CancellationToken cancellationToken)
        {
            var testTemplates = _appDbContext.TestTemplates
                .Include(x => x.Settings)
                .Where(x => x.Status == request.TestTemplateStatus && x.Mode == (int)TestTemplateModeEnum.Test)
                .AsQueryable();

            if (request.EventId.HasValue)
            {
                testTemplates = testTemplates
                    .Include(x => x.EventTestTemplates)
                    .Where(x => x.EventTestTemplates.Any(e => e.EventId == request.EventId));
            }

            var onlyOneAnswerTests = testTemplates.Select(x => _mapper.Map<SelectTestTemplateValueDto>(x)).ToList();

            foreach (var x in onlyOneAnswerTests)
            {
                var testTemplate = testTemplates
                    .Include(tt => tt.TestTemplateQuestionCategories)
                    .FirstOrDefault(tt => tt.Id == x.TestTemplateId);

                var testTemplateCategories = testTemplate.TestTemplateQuestionCategories
                    .Where(tt => tt.TestTemplateId == testTemplate.Id)
                    .ToList();

                var questionsList = new List<QuestionUnitDto>();

                foreach (var testTemplateCategory in testTemplateCategories)
                {
                    var testCategoryQuestionData = await _mediator.Send(new TestCategoryQuestionsQuery { TestTemplateQuestionCategoryId = testTemplateCategory.Id });

                    questionsList.AddRange(testCategoryQuestionData.Questions);
                    x.IsOnlyOneAnswer = questionsList.All(x => x.QuestionType == QuestionTypeEnum.OneAnswer || x.QuestionType == QuestionTypeEnum.MultipleAnswers);
                }

                if (testTemplateCategories.All(tt => tt.QuestionType == QuestionTypeEnum.OneAnswer || tt.QuestionType == QuestionTypeEnum.MultipleAnswers))
                {
                    x.IsOnlyOneAnswer = true;
                }

                x.PrintTest = await CanPrintTest(questionsList);
            }

            return onlyOneAnswerTests;
        }

        private async Task<bool> CanPrintTest(List<QuestionUnitDto> questionsList)
        {
            var mediaList = new List<string>() { "video", "audio" };
            var filesIdsList = new List<string>();

            var mediaOptionsIds = await _appDbContext.Options
                .Where(o => questionsList
                    .Select(x => x.Id)
                    .Contains(o.QuestionUnitId))
                .Where(o => o.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToListAsync();

            var mediaQuestionsIds = questionsList
                .Where(q => q.MediaFileId != null)
                .Select(o => o.MediaFileId)
                .ToList();

            filesIdsList.AddRange(mediaOptionsIds);
            filesIdsList.AddRange(mediaQuestionsIds);

            var files = _storageDbContext.Files
                .Where(f => filesIdsList.Contains(f.Id.ToString()))
                .ToList();

            var result = !files.Any(x => mediaList.Any(m => x.Type.Contains(m)));

            return result;
        }
    }
}
