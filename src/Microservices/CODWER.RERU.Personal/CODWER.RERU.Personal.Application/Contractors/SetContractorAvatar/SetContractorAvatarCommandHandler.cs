using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar
{
    public class SetContractorAvatarCommandHandler : IRequestHandler<SetContractorAvatarCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public SetContractorAvatarCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SetContractorAvatarCommand request, CancellationToken cancellationToken)
        {
            var image = await _appDbContext.Avatars
                .FirstOrDefaultAsync(x => x.ContractorId == request.Data.ContractorId);

            if (image != null)
            {
                _mapper.Map(request.Data, image);
            }
            else
            {
                var contractorAvatar = _mapper.Map<ContractorAvatar>(request.Data);
                await _appDbContext.Avatars.AddAsync(contractorAvatar);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
