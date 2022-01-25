using System;

namespace CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables
{
   public class AddEditTimeSheetTableDto
    {
        public int ContractorId { get; set; }
        public DateTime Date { get; set; }
        public int? ValueId { get; set; }
    }
}
