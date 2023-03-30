using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using System;

namespace RERU.Data.Entities
{
    public class Study : SoftDeleteBaseEntity
    {
        public string Institution { get; set; }
        public StudyFrequencyEnum StudyFrequency { get; set; }
        public StudyProfileEnum? StudyProfile { get; set; }
        public StudyCourseType? StudyCourse { get; set; }
        public string Faculty { get; set; }
        public string InstitutionAddress { get; set; }
        public string Specialty { get; set; }
        public string YearOfAdmission { get; set; }
        public string GraduationYear { get; set; }

        public DateTime? StartStudyPeriod { get; set; }
        public DateTime? EndStudyPeriod { get; set; }

        public string Title { get; set; }
        public string Qualification { get; set; }
        public int? CreditCount { get; set; }
        public string StudyActSeries { get; set; }
        public int? StudyActNumber { get; set; }
        public DateTime? StudyActRelaseDay { get; set; }

        public int? StudyTypeId { get; set; }
        public StudyType StudyType { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
