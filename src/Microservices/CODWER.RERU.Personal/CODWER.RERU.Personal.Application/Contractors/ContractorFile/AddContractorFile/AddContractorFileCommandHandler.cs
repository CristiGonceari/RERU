using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    public class AddContractorFileCommandHandler : IRequestHandler<AddContractorFileCommand, string>
    {
        private readonly IStorageFileService _fileService;
        private readonly IPersonalStorageClient _personalStorageClient;
        public AddContractorFileCommandHandler(IStorageFileService fileService, IPersonalStorageClient personalStorageClient)
        {
            _fileService = fileService;
            _personalStorageClient = personalStorageClient;
        }

        public async Task<string> Handle(AddContractorFileCommand request, CancellationToken cancellationToken)
        {
            var file = new AddFileDto
            {
                File = request.Data.File,
                Type = request.Data.Type
            };

            var fileId = await _fileService.AddFile(file);

            await _personalStorageClient.AddFileToContractor(request.Data.ContractorId, fileId);

            return fileId;
        }
    }
}
