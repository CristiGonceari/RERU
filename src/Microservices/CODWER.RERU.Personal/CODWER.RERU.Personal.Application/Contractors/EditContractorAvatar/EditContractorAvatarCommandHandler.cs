using System;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.EditContractorAvatar
{
    public class EditContractorAvatarCommandHandler : IRequestHandler<EditContractorAvatarCommand, string>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        private string _avatarId = String.Empty;
        public EditContractorAvatarCommandHandler(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }
        public async Task<string> Handle(EditContractorAvatarCommand  request, CancellationToken cancellationToken)
        {
            var image = await _appDbContext.Avatars
                .FirstOrDefaultAsync(x => x.ContractorId == request.Data.ContractorId);

            if (image is not null)
            {
                _avatarId = await _storageFileService.AddFile(request.Data.FileDto);

                image.MediaFileId = _avatarId;
                image.ContractorId = request.Data.ContractorId;
            }
            else
            {
                _avatarId = await _storageFileService.AddFile(request.Data.FileDto);

                var contractorAvatar = new ContractorAvatar
                {
                    ContractorId = request.Data.ContractorId,
                    MediaFileId = _avatarId
                };

               await _appDbContext.Avatars.AddAsync(contractorAvatar);
            }

            await _appDbContext.SaveChangesAsync();

            return _avatarId;
        }
    }
}
