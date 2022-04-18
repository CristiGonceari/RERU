using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class UserProfileIdentity : BaseEntity 
    {
        public int UserProfileId { set; get; }
        public string Type { set; get; }
        public string Identificator { set; get; }
    }
}