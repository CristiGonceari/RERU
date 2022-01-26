using CODWER.RERU.Personal.Data.Entities;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Files
{
    public class FileSignature : SoftDeleteBaseEntity
    {
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public int FileId { get; set; }
        public ByteArrayFile File { get; set; }
    }
}
