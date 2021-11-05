using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities.TimeSheetTables
{
    public class TimeSheetTable : SoftDeleteBaseEntity
    {
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public DateTime Date { get; set; }

        public TimeSheetValueEnum? Value { get; set; }
    }
}
