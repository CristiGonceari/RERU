using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.EditContractorAvatar
{
    public class EditContractorAvatarCommandHandler : IRequestHandler<EditContractorAvatarCommand, Unit>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        public EditContractorAvatarCommandHandler(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }
        public async Task<Unit> Handle(EditContractorAvatarCommand  request, CancellationToken cancellationToken)
        {
            var image = await _appDbContext.Avatars
                .FirstOrDefaultAsync(x => x.ContractorId == request.Data.ContractorId);

            if (request.Data.FileDto != null)
            {
                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                image.MediaFileId = addFile;
            }
            else
            {
                image.MediaFileId = request.Data.MediaFileId;
            }
            image.ContractorId = request.Data.ContractorId;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
