using System;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserPersonalDataDto
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? MediaFileId { get; set; }
    }
}
