using CVU.ERP.StorageService;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.DeleteSolicitedVacantPositionUserFile
{
    public class DeleteSolicitedVacantPositionUserFileCommandHandler : IRequestHandler<DeleteSolicitedVacantPositionUserFileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;

        public DeleteSolicitedVacantPositionUserFileCommandHandler(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(DeleteSolicitedVacantPositionUserFileCommand request, CancellationToken cancellationToken)
        {
            var file = _appDbContext.SolicitedVacantPositionUserFiles
                .FirstOrDefault(x => x.FileId == request.FileId);

            await _storageFileService.RemoveFile(request.FileId);

            _appDbContext.SolicitedVacantPositionUserFiles.Remove(file);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
