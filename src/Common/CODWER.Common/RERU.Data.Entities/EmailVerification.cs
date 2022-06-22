using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EmailVerification : SoftDeleteBaseEntity
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
