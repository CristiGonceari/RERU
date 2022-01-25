using System;

namespace CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables
{
   public class TimeSheetTableDto
    {
        public int ContractorId { get; set; }
        public DateTime Date { get; set; }
        public int? ValueId { get; set; }
        public string Value { get; set; }
    }
}
