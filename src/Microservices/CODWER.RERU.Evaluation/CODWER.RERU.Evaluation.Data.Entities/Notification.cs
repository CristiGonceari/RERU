﻿using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class Notification : SoftDeleteBaseEntity
    {
        public int UserProfileId { get; set; }
        public bool Seen { get; set; }
        public string MessageCode { get; set; }
        public string Value { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
