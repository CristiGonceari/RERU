using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables
{
    public class ContractorTimeSheetTableDto
    {
        public ContractorTimeSheetTableDto()
        {
            Content = new List<TimeSheetTableDto>();
        }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }

        public List<TimeSheetTableDto> Content { get; set; }

        public int WorkedHours { get; set; }
        public int FreeHours { get; set; }
        public int WorkingDays { get; set; }
    }
}
