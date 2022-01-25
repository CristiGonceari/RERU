using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Holidays
{
    public class AddEditHolidayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
