using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities.Studies
{
    public class Study : SoftDeleteBaseEntity
    {
        public string Institution { get; set; }
        public StudyFrequencyEnum? StudyFrequency { get; set; }
        public string Faculty { get; set; }
        public string Qualification { get; set; }
        public string Specialty { get; set; }
        public string DiplomaNumber { get; set; }
        public DateTime? DiplomaReleaseDay { get; set; }
        public bool IsActiveStudy { get; set; }

        public int? StudyTypeId { get; set; }
        public NomenclatureRecord StudyType { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }

}
