using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables
{
    public class ContractorProfileTimeSheetTableDto
    {
        public string ContractorName { get; set; }
        public List<TimeSheetTableDto> Content { get; set; }
        public int WorkedHours { get; set; }
        public int FreeHours { get; set; }
        public int WorkingDays { get; set; }
    }
}
