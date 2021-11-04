using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class OrganizationRole : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }
    }
}
