using CVU.ERP.StorageService;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.AddSolicitedVacantPositionUserFile
{
    public class AddSolicitedVacantPositionUserFileCommandHandler : IRequestHandler<AddSolicitedVacantPositionUserFileCommand, string>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly AppDbContext _appDbContext;

        public AddSolicitedVacantPositionUserFileCommandHandler(IStorageFileService storageFileService, AppDbContext appDbContext)
        {
            _storageFileService = storageFileService;
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(AddSolicitedVacantPositionUserFileCommand request, CancellationToken cancellationToken)
        {
            var fileId = await _storageFileService.AddFile(request.File);

            var item = new SolicitedVacantPositionUserFile
            {
                UserProfileId = request.UserProfileId,
                SolicitedVacantPositionId = request.SolicitedVacantPositionId,
                FileId = fileId,
                RequiredDocumentId = request.RequiredDocumentId
            };

            await _appDbContext.SolicitedVacantPositionUserFiles.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return fileId;
        }
    }
}
