using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.User
{
    public class UserProfile : SoftDeleteBaseEntity
    {
        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string Email { get; set; }
        public string Idnp { get; set; }
    }
}
