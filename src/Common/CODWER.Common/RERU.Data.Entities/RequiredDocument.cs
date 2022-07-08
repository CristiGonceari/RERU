using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class RequiredDocument : SoftDeleteBaseEntity
    {
        public RequiredDocument()
        {
            UserFiles = new HashSet<UserFile>();
            RequiredDocumentPositions = new HashSet<RequiredDocumentPosition>();
        }
        public string Name { get; set; }
        public bool Mandatory { get; set; }

        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<RequiredDocumentPosition> RequiredDocumentPositions { get; set; }
    }
}
