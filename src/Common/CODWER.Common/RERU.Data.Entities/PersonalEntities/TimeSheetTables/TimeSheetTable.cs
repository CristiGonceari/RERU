using System;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace RERU.Data.Entities.PersonalEntities.TimeSheetTables
{
    public class TimeSheetTable : SoftDeleteBaseEntity
    {
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public DateTime Date { get; set; }

        public TimeSheetValueEnum? Value { get; set; }
    }
}
