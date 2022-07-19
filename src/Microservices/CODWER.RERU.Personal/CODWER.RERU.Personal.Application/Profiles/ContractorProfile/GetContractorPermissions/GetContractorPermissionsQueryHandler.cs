using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorPermissions
{
    public class GetContractorPermissionsQueryHandler : IRequestHandler<GetContractorPermissionsQuery, ContractorLocalPermissionsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetContractorPermissionsQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<ContractorLocalPermissionsDto> Handle(GetContractorPermissionsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var permission = await _appDbContext.ContractorPermissions
                .FirstOrDefaultAsync(x => x.ContractorId == contractorId);

            if (permission == null)
            {
                return new ContractorLocalPermissionsDto
                {
                    ContractorId = contractorId
                };
            };

            return _mapper.Map<ContractorLocalPermissionsDto>(permission);
        }
    }
}
