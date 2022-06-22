using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.ImportProcesses.GetImportResult
{
    public class GetImportResultQueryHandler : IRequestHandler<GetImportResultQuery, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;

        public GetImportResultQueryHandler(IStorageFileService storageFileService)
        {
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(GetImportResultQuery request, CancellationToken cancellationToken)
        {
            return await _storageFileService.GetFile(request.FileId);
        }
    }
}
