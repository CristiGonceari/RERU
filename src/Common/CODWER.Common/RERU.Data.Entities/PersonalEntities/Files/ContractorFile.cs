using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.Files
{
    public class ContractorFile : SoftDeleteBaseEntity
    {
        public ContractorFile(int contractorId, string fileId)
        {
            ContractorId = contractorId;
            FileId = fileId;
        }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string FileId { get; set; }
    }
}
