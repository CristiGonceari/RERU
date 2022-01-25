using System;
using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Studies
{
    public class StudyDataDto
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public StudyFrequencyEnum StudyFrequency { get; set; }
        public string Faculty { get; set; }
        public string Qualification { get; set; }
        public string Specialty { get; set; }
        public string DiplomaNumber { get; set; }
        public DateTime? DiplomaReleaseDay { get; set; }
        public bool IsActiveStudy { get; set; }

        public int StudyTypeId { get; set; } // nomenclature

        public int ContractorId { get; set; }
    }
}
