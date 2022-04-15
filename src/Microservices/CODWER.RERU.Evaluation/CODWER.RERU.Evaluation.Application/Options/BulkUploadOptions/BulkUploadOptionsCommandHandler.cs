using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Options.BulkUploadOptions
{
    public class BulkUploadOptionsCommandHandler : IRequestHandler<BulkUploadOptionsCommand, byte[]>
    {
        private readonly IOptionService _optionService;

        public BulkUploadOptionsCommandHandler(IOptionService optionService)
        {
            _optionService = optionService;

        }

        public async Task<byte[]> Handle(BulkUploadOptionsCommand request, CancellationToken cancellationToken)
        {

            return  await _optionService.BulkOptionsUpload(request.Input, request.QuestionUnitId);
        }
    }
}
