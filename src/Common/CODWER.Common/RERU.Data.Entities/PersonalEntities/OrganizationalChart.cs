using System;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class OrganizationalChart : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
    }
}
