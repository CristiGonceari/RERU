using System;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.ContractorEvents
{
    public class DismissalRequest : SoftDeleteBaseEntity
    {
        public DateTime From { get; set; }
        public StageStatusEnum Status { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string RequestId { get; set; }

        public string OrderId { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
