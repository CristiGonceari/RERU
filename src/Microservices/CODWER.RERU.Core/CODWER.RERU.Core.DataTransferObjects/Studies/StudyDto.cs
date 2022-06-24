using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Core.DataTransferObjects.Studies
{
    public class StudyDto
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public StudyFrequencyEnum StudyFrequency { get; set; }
        public string Faculty { get; set; }
        public string InstitutionAddress { get; set; }
        public string Specialty { get; set; }
        public DateTime YearOfAdmission { get; set; }
        public DateTime GraduationYear { get; set; }

        public int StudyTypeId { get; set; } 

        public int UserProfileId { get; set; }
    }
}
