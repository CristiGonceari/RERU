using System;
using System.Collections.Generic;
using System.Text;

namespace CVU.ERP.Common.Data.Entities
{
    public abstract class SoftDeleteBaseEntity : BaseEntity, ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
