using System;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.ContractorEvents
{
    public class Vacation : SoftDeleteBaseEntity
    {
        public string Mentions { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int CountDays { get; set; }
        public StageStatusEnum Status { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public VacationType VacationType { get; set; }

        public int? VacationRequestId { get; set; }
        public ByteArrayFile VacationRequest { get; set; }

        public int? VacationOrderId { get; set; }
        public ByteArrayFile VacationOrder { get; set; }


        //specific fields
        public string Institution { get; set; } // for study type
        public int ChildAge { get; set; } // for child
    }

}
