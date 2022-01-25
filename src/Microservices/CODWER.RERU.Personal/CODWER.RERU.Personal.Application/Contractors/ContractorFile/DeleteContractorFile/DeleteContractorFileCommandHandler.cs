using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.DeleteContractorFile
{
    public class DeleteContractorFileCommandHandler : IRequestHandler<DeleteContractorFileCommand, Unit>
    {
        private readonly IStorageFileService _fileService;

        public DeleteContractorFileCommandHandler(IStorageFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteContractorFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.RemoveFile(request.FileId);
        }
    }
}
