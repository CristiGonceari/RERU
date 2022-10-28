using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using System;

namespace RERU.Data.Entities
{
    public class Study : SoftDeleteBaseEntity
    {
        public string Institution { get; set; }
        public StudyFrequencyEnum? StudyFrequency { get; set; }
        public string Faculty { get; set; }
        public string InstitutionAddress { get; set; }
        public string Specialty { get; set; }
        public DateTime? YearOfAdmission { get; set; }
        public DateTime? GraduationYear { get; set; }
        

        public int StudyTypeId { get; set; }
        public StudyType StudyType { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
