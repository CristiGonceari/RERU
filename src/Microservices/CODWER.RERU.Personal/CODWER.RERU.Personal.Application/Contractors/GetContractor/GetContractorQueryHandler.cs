using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Clients;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractor
{
    public class GetContractorQueryHandler : IRequestHandler<GetContractorQuery, ContractorDetailsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICoreClient _coreClient;
        private readonly IPersonalStorageClient _personalStorageClient;

        public GetContractorQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ICoreClient coreClient, 
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _coreClient = coreClient;
            _personalStorageClient = personalStorageClient;
        }

        public async Task<ContractorDetailsDto> Handle(GetContractorQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x=>x.UserProfile)
                    .ThenInclude(x => x.Department)
                .Include(x => x.UserProfile)
                    .ThenInclude(x => x.Role)
                .Include(x=>x.Bulletin)
                .Include(x => x.Studies)
                .Include(r => r.Positions)
                    .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                    .ThenInclude(p => p.Role)
                .Include(r => r.BloodType)
                .Include( r=> r.Contacts)
                .Include(x => x.Contracts)
                .Include(x => x.UserProfile)
                .Include(x => x.Avatar)
                .Select(c => new Contractor
                {
                    UserProfile = new UserProfile
                    {
                        Department = c.UserProfile.Department,
                        Role = c.UserProfile.Role,
                        DepartmentColaboratorId = c.UserProfile.DepartmentColaboratorId,
                        RoleColaboratorId = c.UserProfile.RoleColaboratorId
                    },
                    Id = c.Id,
                    Code = c.Code,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FatherName = c.FatherName,
                    BirthDate = c.BirthDate,
                    Sex = c.Sex,
                    PhoneNumber = c.PhoneNumber,
                    WorkPhone = c.WorkPhone,
                    HomePhone = c.HomePhone,
                    CandidateNationalityId = c.CandidateNationalityId,
                    CandidateCitizenshipId = c.CandidateCitizenshipId,
                    StateLanguageLevel = c.StateLanguageLevel,
                    Positions = c.Positions,
                    BloodTypeId = c.BloodTypeId,
                    Studies = c.Studies,
                    Contacts = c.Contacts,
                    Contracts = c.Contracts,
                    Bulletin = c.Bulletin,
                    Avatar = c.Avatar,
                    RecommendationForStudies = c.RecommendationForStudies,
                    ModernLanguageLevels = c.ModernLanguageLevels,
                    MaterialStatus = c.MaterialStatus,
                    KinshipRelations = c.KinshipRelations,
                    KinshipRelationCriminalData = c.KinshipRelationCriminalData,
                    KinshipRelationWithUserProfiles = c.KinshipRelationWithUserProfiles,
                    MilitaryObligations = c.MilitaryObligations,
                    Autobiography = c.Autobiography,
                })
                .FirstAsync(rt => rt.Id == request.Id);


            var mappedContractor = _mapper.Map<ContractorDetailsDto>(contractor);

            mappedContractor.HasEmploymentRequest = await _personalStorageClient.HasFile(request.Id, FileTypeEnum.request);
            mappedContractor.HasIdentityDocuments = await _personalStorageClient.HasFile(request.Id, FileTypeEnum.identityfiles);

            return mappedContractor;
        }
    }
}
