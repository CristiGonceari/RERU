using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.GetContractorFile
{
    public class GetContractorFileQueryHandler : IRequestHandler<GetContractorFileQuery, FileDataDto>
    {
        private readonly IStorageFileService _fileService;

        public GetContractorFileQueryHandler(IStorageFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<FileDataDto> Handle(GetContractorFileQuery request, CancellationToken cancellationToken)
        {
            return await _fileService.GetFile(request.FileId);
        }
    }
}
