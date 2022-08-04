using System;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.Configurations
{
    public class Holiday : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
