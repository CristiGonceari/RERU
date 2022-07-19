using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorBulletin.GetContractorBulletin
{
    public class GetContractorBulletinQueryHandler : IRequestHandler<GetContractorBulletinQuery, BulletinsDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetContractorBulletinQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<BulletinsDataDto> Handle(GetContractorBulletinQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var bulletin = await _appDbContext.Bulletins
                .Include(x=>x.UserProfile)
                .Include(x=>x.BirthPlace)
                .Include(x=>x.ResidenceAddress)
                .FirstOrDefaultAsync(x => x.UserProfile.Contractor.Id == contractorId);

            return _mapper.Map<BulletinsDataDto>(bulletin);
        }
    }
}
