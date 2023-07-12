using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.GetAllContractorDatas
{
    public class GetAllContractorDataQueryHandler : IRequestHandler<GetAllContractorDataQuery, string>
    {
        private readonly AppDbContext _appDbContext;

        public GetAllContractorDataQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(GetAllContractorDataQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                 .Include(x => x.UserProfile)
                     .ThenInclude(x => x.Department)
                 .Include(x => x.UserProfile)
                     .ThenInclude(x => x.Role)
                 .Include(x => x.UserProfile)
                     .ThenInclude(x => x.EmployeeFunction)
                 .Include(x => x.Bulletin)
                 .Include(x => x.Studies)
                 .Include(r => r.Positions)
                     .ThenInclude(p => p.Department)
                 .Include(c => c.Positions)
                     .ThenInclude(p => p.Role)
                 .Include(r => r.BloodType)
                 .Include(r => r.Contacts)
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
                         RoleColaboratorId = c.UserProfile.RoleColaboratorId,
                         EmployeeFunction = c.UserProfile.EmployeeFunction,
                         FunctionColaboratorId = c.UserProfile.FunctionColaboratorId
                     },
                     Id = c.Id,
                     Code = c.Code,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     FatherName = c.FatherName,
                     Idnp = c.UserProfile.Idnp,
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

            var JSON = JsonConvert.SerializeObject(contractor, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

            return JSON;
        }
    }
}
