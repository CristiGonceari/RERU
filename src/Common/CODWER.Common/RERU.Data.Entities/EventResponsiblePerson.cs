﻿using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventResponsiblePerson : SoftDeleteBaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}