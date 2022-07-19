using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class EmailNotification : SoftDeleteBaseEntity
    {
        public int ItemId { get; set; }
        public EmailType EmailType { get; set; }
        public bool IsSend { get; set; }
    }
}
