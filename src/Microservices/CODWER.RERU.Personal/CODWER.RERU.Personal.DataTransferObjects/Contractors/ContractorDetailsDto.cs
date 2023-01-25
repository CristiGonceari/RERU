using System;
using System.Collections.Generic;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class ContractorDetailsDto
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }

        public string PhoneNumber { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string Idnp { get; set; }

        public int CandidateNationalityId { get; set; }
        public int CandidateCitizenshipId { get; set; }

        public StateLanguageLevel StateLanguageLevel { get; set; }
        public DateTime BirthDate { get; set; }
        public SexTypeEnum Sex { get; set; }

        public int BloodTypeId { get; set; }

        public string DepartmentName { get; set; }
        public string OrganizationRoleName { get; set; }
        public string FunctionName { get; set; }

        public string EmployerState { get; set; }
        public string UserState { get; set; }

        public bool HasUserProfile { get; set; }
        public bool HasAvatar { get; set; }
        public bool HasCim { get; set; }
        public bool HasPositions { get; set; }
        public bool HasEmploymentRequest { get; set; }
        public bool HasBulletin { get; set; }
        public bool HasStudies { get; set; }
        public bool HasIdentityDocuments { get; set; }
        public bool HasModernLanguages { get; set; }
        public bool HasRecommendationsForStudy { get; set; }
        public bool HasMaterialStatus{ get; set; }
        public bool HasKinshipRelations { get; set; }
        public bool HasKinshipRelationCriminalData { get; set; }
        public bool HasKinshipRelationWithUserProfiles { get; set; }
        public bool HasMilitaryObligations { get; set; }
        public bool HasAutobiography { get; set; }


        public List<ContactRowDto> Contacts { get; set; }
    }
}
