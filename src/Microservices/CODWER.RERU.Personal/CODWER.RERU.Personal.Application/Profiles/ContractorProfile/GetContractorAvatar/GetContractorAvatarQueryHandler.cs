using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorAvatar
{
    public class GetContractorAvatarQueryHandler : IRequestHandler<GetContractorAvatarQuery, ContractorAvatarDetailsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly int _contractorId;
        public GetContractorAvatarQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _contractorId = userProfileService.GetCurrentContractorId().Result;
        }
        public async Task<ContractorAvatarDetailsDto> Handle(GetContractorAvatarQuery request, CancellationToken cancellationToken)
        {
            var image = await _appDbContext.Avatars.FirstOrDefaultAsync(x => x.ContractorId == _contractorId);
           
            if (image != null)
            {
                return _mapper.Map<ContractorAvatarDetailsDto>(image);
            }

            return new ContractorAvatarDetailsDto();
        }
    }
}
