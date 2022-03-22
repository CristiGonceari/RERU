using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;


namespace CODWER.RERU.Personal.Data.Entities.Documents
{
    public class DocumentTemplateCategory : SoftDeleteBaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<DocumentTemplateKey> DocumentKeys { get; set; }
    }
}
