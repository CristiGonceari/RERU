using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class SolicitedVacantPositionEmailMessage : SoftDeleteBaseEntity
    {
        public string Message { get; set; }
        public SolicitedVacantPositionEmailMessageEnum MessageType { get; set; }
    }
}
