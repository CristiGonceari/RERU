using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Profiles;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorProfile
{
    public class GetContractorProfileQueryHandler : IRequestHandler<GetContractorProfileQuery, ContractorProfileDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly UserProfile _userProfile;
        private readonly StorageDbContext _storageDbContext;

        public GetContractorProfileQueryHandler(AppDbContext appDbContext, 
            IUserProfileService userProfileService, 
            IMapper mapper, 
            StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageDbContext = storageDbContext;
            _userProfile = userProfileService.GetCurrentUserProfile().Result;
        }

        public async Task<ContractorProfileDto> Handle(GetContractorProfileQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(r => r.Positions)
                .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                .ThenInclude(p => p.OrganizationRole)
                .Include(r => r.BloodType)
                .Include(r => r.Studies)
                .Include(r => r.Contacts)
                .Include(x => x.Contracts)
                .Include(x => x.UserProfile)
                .Include(x => x.Bulletin)
                .Select(c => new Contractor
                 {
                     Id = c.Id,
                     Code = c.Code,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     FatherName = c.FatherName,
                     BirthDate = c.BirthDate,
                     Sex = c.Sex,
                     BloodTypeId = c.BloodTypeId,
                     Positions = c.Positions,
                     BloodType = c.BloodType,
                     Studies = c.Studies,
                     Contacts = c.Contacts,
                     Contracts = c.Contracts,
                     UserProfile = c.UserProfile,
                     Bulletin = c.Bulletin
                 })
                .FirstAsync(rt => rt.Id == _userProfile.ContractorId);

            var files = _storageDbContext.Files
                .Where(x => contractor.ContractorFiles.Any(i => i.FileId == x.Id.ToString()))
                .Select(f => new File
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    Type = f.Type,
                    FileType = f.FileType
                })
                .AsQueryable();

            var mappedContractor = _mapper.Map<ContractorProfileDto>(contractor);

            mappedContractor.HasEmploymentRequest = files.Any(x => x.FileType == FileTypeEnum.request);
            mappedContractor.HasIdentityDocuments = files.Any(x => x.FileType == FileTypeEnum.identityfiles);

            return mappedContractor;
        }
    }
}
