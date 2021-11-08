using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionBulkTemplate
{
    public class GetQuestionBulkTemplateQueryHandler : IRequestHandler<GetQuestionBulkTemplateQuery, byte[]>
    {
        private readonly IQuestionUnitService _questionUnitService;

        public GetQuestionBulkTemplateQueryHandler(IQuestionUnitService questionUnitService)
        {
            _questionUnitService = questionUnitService;
        }

        public async Task<byte[]> Handle(GetQuestionBulkTemplateQuery request, CancellationToken cancellationToken)
        {
            return await _questionUnitService.GenerateExcelTemplate(request.QuestionType);
        }
    }
}
