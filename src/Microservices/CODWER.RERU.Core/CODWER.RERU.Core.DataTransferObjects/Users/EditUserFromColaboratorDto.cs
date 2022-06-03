using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class EditUserFromColaboratorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string MediaFileId { get; set; }
        public int? CandidatePositionId { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public int? RoleColaboratorId { get; set; }
        public bool EmailNotification { get; set; }
        public AccessModeEnum AccessModeEnum { get; set; }
    }
}
