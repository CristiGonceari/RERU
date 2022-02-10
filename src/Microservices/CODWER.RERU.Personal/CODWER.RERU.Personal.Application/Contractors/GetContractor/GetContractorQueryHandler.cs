using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractor
{
    public class GetContractorQueryHandler : IRequestHandler<GetContractorQuery, ContractorDetailsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICoreClient _coreClient;
        private readonly StorageDbContext _storageDbContext;

        public GetContractorQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ICoreClient coreClient, 
            StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _coreClient = coreClient;
            _storageDbContext = storageDbContext;
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
                .Include(x => x.Contracts)
                .Include(x => x.UserProfile)
                .Include(x => x.Bulletin)
                .Include(x => x.Avatar)
                .Include(x => x.ContractorFiles)
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
                    Contracts = c.Contracts,
                    UserProfile = c.UserProfile,
                    Bulletin = c.Bulletin,
                    Avatar = c.Avatar
                })
                .FirstAsync(rt => rt.Id == request.Id);

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

            var mappedContractor = _mapper.Map<ContractorDetailsDto>(contractor);

            mappedContractor.HasUserProfile = await GetUserProfile(contractor.UserProfile);
            mappedContractor.HasEmploymentRequest = files.Any(x => x.FileType == FileTypeEnum.Request);
            mappedContractor.HasIdentityDocuments = files.Any(x => x.FileType == FileTypeEnum.IdentityFiles);

            return mappedContractor;
        }

        private async Task<bool> GetUserProfile(UserProfile userProfile)
        {
            //return userProfile != null && await _coreClient.ExistUserInCore(userProfile.UserId);
            return true;
        }

        
    }
}
