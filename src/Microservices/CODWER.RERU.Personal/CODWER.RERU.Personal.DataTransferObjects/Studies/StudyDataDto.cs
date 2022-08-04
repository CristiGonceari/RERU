using System;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Studies
{
    public class StudyDataDto
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

        public int ContractorId { get; set; }
    }
}
