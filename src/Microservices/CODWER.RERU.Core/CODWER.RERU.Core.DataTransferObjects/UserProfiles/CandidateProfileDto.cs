using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class CandidateProfileDto
    {
        public string Id { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string UserName { set; get; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MediaFileId { get; set; }
        public int? CandidatePositionId { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public string DepartmentName { get; set; }
        public int? RoleColaboratorId { get; set; }
        public string RoleName { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }
        public AccessModeEnum AccessModeEnum { get; set; }
        public bool IsActive { set; get; }
        public int BulletinId { get; set; }
        public int StudyCount { get; set; }
        public int ModernLanguageLevelsCount { get; set; }
        public int RecomendationsForStudyCount { get; set; }
        public int MaterialStatusId { get; set; }
        public int KinshipRelationsCount { get; set; }
        public int KinshipRelationCriminalDataId { get; set; }
        public int KinshipRelationWithUserProfilesCount { get; set; }
        public int MilitaryObligationsCount { get; set; }
        public int AutobiographyId { get; set; }
        public int ContractorId { get; set; }

    }
}
