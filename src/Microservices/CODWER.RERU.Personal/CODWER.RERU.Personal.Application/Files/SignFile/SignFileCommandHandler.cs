using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using MediatR;

namespace CODWER.RERU.Personal.Application.Files.SignFile
{
    public class SignFileCommandHandler : IRequestHandler<SignFileCommand, Unit>
    {
        private readonly IStorageFileService _storageFileService;

        public SignFileCommandHandler(IStorageFileService storageFileService)
        {
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(SignFileCommand request, CancellationToken cancellationToken)
        {
            return await _storageFileService.SignFile(request.Data);
        }
    }
}
