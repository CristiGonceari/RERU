using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateDocumentReplacedKeys
{
    public class GetTestTemplateDocumentReplacedKeysQueryHandler : IRequestHandler<GetTestTemplateDocumentReplacedKeysQuery, string>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGetTestTemplateDocumentReplacedKeys _getTestTemplateDocumentReplacedKeys;

        public GetTestTemplateDocumentReplacedKeysQueryHandler(AppDbContext appDbContext, IGetTestTemplateDocumentReplacedKeys getTestTemplateDocumentReplacedKeys) 
        {
            _appDbContext = appDbContext;
            _getTestTemplateDocumentReplacedKeys = getTestTemplateDocumentReplacedKeys;
        }

        public async Task<string> Handle(GetTestTemplateDocumentReplacedKeysQuery request, CancellationToken cancellationToken)
        {
            var testTemplateValues = _appDbContext.TestTemplates
                .Include(tt => tt.Settings)
                .Include(tt => tt.TestTemplateQuestionCategories)
                .Select(tt => new TestTemplate
                {
                    Id = tt.Id,
                    Name = tt.Name,
                    Rules = tt.Rules,
                    QuestionCount = tt.QuestionCount,
                    TestTemplateQuestionCategories = tt.TestTemplateQuestionCategories,
                    MinPercent = tt.MinPercent,
                    Duration = tt.Duration,
                    Settings = new TestTemplateSettings
                    {
                        MaxErrors = tt.Settings.MaxErrors,
                        FormulaForOneAnswer = tt.Settings.FormulaForOneAnswer,
                        FormulaForMultipleAnswers = tt.Settings.FormulaForMultipleAnswers,
                    },
                    Status = tt.Status,
                    Mode = tt.Mode,
                    CategoriesSequence = tt.CategoriesSequence,
                    BasicTestTemplate = tt.BasicTestTemplate,
                    QualifyingType = tt.QualifyingType,
                })
                .FirstOrDefault(tt => tt.Id == request.TestTemplateId);

            var replacedKeys = await _getTestTemplateDocumentReplacedKeys.GetTestTemplateDocumentReplacedKey(testTemplateValues, request.DocumentTemplateId);

            return replacedKeys;
        }
    }
}
