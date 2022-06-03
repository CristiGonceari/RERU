using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class EditUserPersonalDetailsDto 
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public int? RoleColaboratorId { get; set; }
        public AccessModeEnum AccessModeEnum { get; set; }
        public string MediaFileId { get; set; }
    }
}