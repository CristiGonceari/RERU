using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Configurations
{
    public class VacationConfiguration : SoftDeleteBaseEntity
    {
        #region general area
        public int PaidLeaveDays { get; set; }
        public int NonPaidLeaveDays { get; set; }
        public int StudyLeaveDays { get; set; }
        public int DeathLeaveDays { get; set; }
        public int ChildCareLeaveDays { get; set; }
        public int ChildBirthLeaveDays { get; set; }
        public int MarriageLeaveDays { get; set; }
        public int PaternalistLeaveDays { get; set; }

        public bool IncludeOffDays { get; set; }
        public bool IncludeHolidayDays { get; set; }
        #endregion

        #region week area
        public bool MondayIsWorkDay { get; set; }
        public bool TuesdayIsWorkDay { get; set; }
        public bool WednesdayIsWorkDay { get; set; }
        public bool ThursdayIsWorkDay { get; set; }
        public bool FridayIsWorkDay { get; set; }
        public bool SaturdayIsWorkDay { get; set; }
        public bool SundayIsWorkDay { get; set; }
        #endregion
    }
}
