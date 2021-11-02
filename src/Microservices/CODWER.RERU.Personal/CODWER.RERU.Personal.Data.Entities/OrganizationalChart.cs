using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class OrganizationalChart : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
    }
}
