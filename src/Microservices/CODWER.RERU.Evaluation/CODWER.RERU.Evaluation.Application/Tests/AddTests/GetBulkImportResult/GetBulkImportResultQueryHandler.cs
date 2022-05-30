using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.GetBulkImportResult
{
    public class GetBulkImportResultQueryHandler : IRequestHandler<GetBulkImportResultQuery, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;

        public GetBulkImportResultQueryHandler(IStorageFileService storageFileService)
        {
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(GetBulkImportResultQuery request, CancellationToken cancellationToken)
        {
            return await _storageFileService.GetFile(request.FileId);
        }
    }
}
