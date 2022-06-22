using System;

namespace CVU.ERP.Common.DataTransferObjects.Users
{
    public class CreateUserDto
    {
        public string FirstName{get;set;} 
        public string LastName{get;set;}
        public string FatherName{get;set;}
        public string Email{get;set;} 
        public string Idnp{get;set; }
        public string MediaFileId { get; set; }
        public int? CandidatePositionId { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public int? RoleColaboratorId { get; set; }
        public bool EmailNotification { get; set; }
        public int AccessModeEnum { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
