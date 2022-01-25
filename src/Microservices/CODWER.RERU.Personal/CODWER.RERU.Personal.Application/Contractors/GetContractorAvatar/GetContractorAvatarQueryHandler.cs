using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorAvatar
{
    public class GetContractorAvatarQueryHandler : IRequestHandler<GetContractorAvatarQuery, ContractorAvatarDetailsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorAvatarQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ContractorAvatarDetailsDto> Handle(GetContractorAvatarQuery request, CancellationToken cancellationToken)
        {
            var image = await _appDbContext.Avatars.FirstOrDefaultAsync(x => x.ContractorId == request.Id);

            if (image != null)
            {
                return _mapper.Map<ContractorAvatarDetailsDto>(image);
            }

            return new ContractorAvatarDetailsDto();
        }
    }
}
