using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Data.Entities.Files
{
    public class ByteArrayFile : SoftDeleteBaseEntity
    {
        public ByteArrayFile()
        {
        }

        public string UniqueFileName { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public FileTypeEnum Type { get; set; }
        public bool IsSigned { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }

}
