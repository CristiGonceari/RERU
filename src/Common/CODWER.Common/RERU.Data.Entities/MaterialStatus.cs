using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class MaterialStatus : SoftDeleteBaseEntity
    {

        public int MaterialStatusTypeId { get; set; }
        public MaterialStatusType MaterialStatusType { get; set; }

        public int UserProfileid { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
