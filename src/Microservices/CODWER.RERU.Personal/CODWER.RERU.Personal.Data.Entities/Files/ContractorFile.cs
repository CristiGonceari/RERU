using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Files
{
    public class ContractorFile : SoftDeleteBaseEntity
    {
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string FileId { get; set; }
    }
}
