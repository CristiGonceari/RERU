﻿using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class QuestionUnitTag : SoftDeleteBaseEntity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int QuestionUnitId { get; set; }
        public QuestionUnit QuestionUnit { get; set; }
    }
}