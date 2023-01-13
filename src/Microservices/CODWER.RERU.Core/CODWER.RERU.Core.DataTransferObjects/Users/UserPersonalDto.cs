﻿using System;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserPersonalDataDto
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { get; set; }
        public bool EmailNotification { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MediaFileId { get; set; }
    }
}
