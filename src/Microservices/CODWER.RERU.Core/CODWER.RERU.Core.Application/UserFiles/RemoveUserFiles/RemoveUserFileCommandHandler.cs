using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.StorageService;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserFiles.RemoveUserFiles
{
    public class RemoveUserFileCommandHandler : BaseHandler, IRequestHandler<RemoveUserFileCommand, Unit>
    {
        private readonly IStorageFileService _storageFileService;

        public RemoveUserFileCommandHandler(ICommonServiceProvider commonServiceProvider, 
            IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(RemoveUserFileCommand request, CancellationToken cancellationToken)
        {
            var userFile = AppDbContext.UserFiles.FirstOrDefault(x => x.FileId == request.FileId);

            await _storageFileService.RemoveFile(request.FileId);

            AppDbContext.UserFiles.Remove(userFile);
            await AppDbContext.SaveChangesAsync();

            return Unit.Value;
        }
       
    }
}
