using System;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Configurations
{
    public class Holiday : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
