using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Avatars;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar
{
    public class SetContractorAvatarCommandHandler : IRequestHandler<SetContractorAvatarCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public SetContractorAvatarCommandHandler(AppDbContext appDbContext, IMapper mapper, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(SetContractorAvatarCommand request, CancellationToken cancellationToken)
        {
            var storage = await _storageFileService.AddFile(request.FileDto);

            var image = await _appDbContext.Avatars
                .FirstOrDefaultAsync(x => x.ContractorId == request.ContractorId);

            var newAvatar = new ContractorAvatarDto()
            {
                ContractorId = request.ContractorId,
                MediaFileId  = storage,
            };

            var contractorAvatar = _mapper.Map<ContractorAvatar>(newAvatar);
            await _appDbContext.Avatars.AddAsync(contractorAvatar);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
