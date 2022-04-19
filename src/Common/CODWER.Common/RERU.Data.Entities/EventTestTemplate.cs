﻿using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventTestTemplate : SoftDeleteBaseEntity
    {
        public int MaxAttempts { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }
    }
}