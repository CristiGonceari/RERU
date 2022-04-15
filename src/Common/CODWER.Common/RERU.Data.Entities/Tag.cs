using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class Tag : SoftDeleteBaseEntity
    {
        public Tag()
        {
            QuestionUnitTags = new HashSet<QuestionUnitTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<QuestionUnitTag> QuestionUnitTags { get; set; }
    }
}
