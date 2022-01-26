﻿using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Contacts
{
    public class AddEditContactDto
    {
        public int Id { get; set; }
        public ContactTypeEnum? Type { get; set; }
        public string Value { get; set; }
        public int ContractorId { get; set; }
    }
}