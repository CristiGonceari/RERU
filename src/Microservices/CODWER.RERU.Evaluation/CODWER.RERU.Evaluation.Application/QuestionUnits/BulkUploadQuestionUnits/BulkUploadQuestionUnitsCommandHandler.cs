using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.BulkUploadQuestionUnits
{
    public class BulkUploadQuestionUnitsCommandHandler : IRequestHandler<BulkUploadQuestionUnitsCommand, byte[]>
    {
        private readonly IQuestionUnitService _questionUnitService;

        public BulkUploadQuestionUnitsCommandHandler(IQuestionUnitService questionUnitService)
        {
            _questionUnitService = questionUnitService;
        }

        public async Task<byte[]> Handle(BulkUploadQuestionUnitsCommand request, CancellationToken cancellationToken)
        {
            return await _questionUnitService.BulkQuestionsUpload(request.Input);
        }
    }
}
