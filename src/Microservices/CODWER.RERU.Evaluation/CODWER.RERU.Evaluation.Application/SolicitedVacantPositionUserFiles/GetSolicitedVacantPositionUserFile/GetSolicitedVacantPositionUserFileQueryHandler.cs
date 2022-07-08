using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFile
{
    public class GetSolicitedVacantPositionUserFileQueryHandler : IRequestHandler<GetSolicitedVacantPositionUserFileQuery, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;

        public GetSolicitedVacantPositionUserFileQueryHandler(IStorageFileService storageFileService)
        {
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(GetSolicitedVacantPositionUserFileQuery request, CancellationToken cancellationToken)
        {
            return await _storageFileService.GetFile(request.FileId);
        }
    }
}
