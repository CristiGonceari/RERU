﻿namespace CODWER.RERU.Personal.DataTransferObjects.Badges
{
    public class BadgeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
    }
}
