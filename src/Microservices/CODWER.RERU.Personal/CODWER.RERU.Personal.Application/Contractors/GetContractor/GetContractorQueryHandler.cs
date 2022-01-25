using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractor
{
    public class GetContractorQueryHandler : IRequestHandler<GetContractorQuery, ContractorDetailsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICoreClient _coreClient;


        public GetContractorQueryHandler(AppDbContext appDbContext, IMapper mapper, ICoreClient coreClient)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _coreClient = coreClient;
        }

        public async Task<ContractorDetailsDto> Handle(GetContractorQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(r => r.Positions)
                    .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                    .ThenInclude(p => p.OrganizationRole)
                .Include(r => r.BloodType)
                .Include(r => r.Studies)
                .Include( r=> r.Contacts)
                .Include(c => c.ByteArrayFiles)
                .Include(x => x.Contracts)
                .Include(x => x.UserProfile)
                .Include(x => x.Bulletin)
                .Include(x => x.Avatar)
                .Select(c => new Contractor
                {
                    Id = c.Id,
                    Code = c.Code,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FatherName = c.FatherName,
                    BirthDate = c.BirthDate,
                    Sex = c.Sex,
                    Positions = c.Positions,
                    BloodTypeId = c.BloodTypeId,
                    Studies = c.Studies,
                    Contacts = c.Contacts,
                    ByteArrayFiles = c.ByteArrayFiles,
                    Contracts = c.Contracts,
                    UserProfile = c.UserProfile,
                    Bulletin = c.Bulletin,
                    Avatar = c.Avatar
                })
                .FirstAsync(rt => rt.Id == request.Id);

            var mappedContractor = _mapper.Map<ContractorDetailsDto>(contractor);

            mappedContractor.HasUserProfile = await GetUserProfile(contractor.UserProfile);

            return mappedContractor;
        }

        private async Task<bool> GetUserProfile(UserProfile userProfile)
        {
            //return userProfile != null && await _coreClient.ExistUserInCore(userProfile.UserId);
            return true;
        }
    }
}
