using CODWER.RERU.Personal.Data.Entities.Files;
using CVU.ERP.Common.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class IndividualContract : SoftDeleteBaseEntity
    {
        public string No { get; set; }
        public int NetSalary { get; set; }
        public int BrutSalary { get; set; }
        public int VacationDays { get; set; }

        public int? CurrencyTypeId { get; set; }

        public int? SuperiorId { get; set; }
        [ForeignKey("SuperiorId")]
        public virtual Contractor Superior { get; set; }

        public int ContractorId { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }

        public int? FileId { get; set; }
        public ByteArrayFile File { get; set; }
    }

}
