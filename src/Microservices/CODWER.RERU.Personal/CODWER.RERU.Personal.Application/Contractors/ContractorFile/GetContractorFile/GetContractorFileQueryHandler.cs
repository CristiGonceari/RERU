using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;

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
