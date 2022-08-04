using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.Documents
{
    public class HrDocumentTemplateCategory : SoftDeleteBaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<HrDocumentTemplateKey> HrDocumentKeys { get; set; }
    }
}
