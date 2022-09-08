using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.FileTestAnswers.GetQuestionFile
{
    public class GetQuestionFileQueryHandler : IRequestHandler<GetQuestionFileQuery, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;

        public GetQuestionFileQueryHandler(IStorageFileService storageFileService)
        {
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(GetQuestionFileQuery request, CancellationToken cancellationToken)
        {
            return await _storageFileService.GetFile(request.FileId);
        }
    }
}
