using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class RequiredDocument : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public bool Mandatory { get; set; }

        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<RequiredDocumentPosition> RequiredDocumentPositions { get; set; }
    }
}
