using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile
{
    public class AddContractorFileCommandHandler : IRequestHandler<AddContractorFileCommand, int>
    {
        private readonly IStorageFileService _fileService;

        public AddContractorFileCommandHandler(IStorageFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<int> Handle(AddContractorFileCommand request, CancellationToken cancellationToken)
        {
            return await _fileService.AddFile(request.Data);
        }
    }
}
