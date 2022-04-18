using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class QuestionCategory : SoftDeleteBaseEntity
    {
        public QuestionCategory()
        {
            QuestionUnits = new HashSet<QuestionUnit>();
        }

        public string Name { get; set; }

        public virtual ICollection<QuestionUnit> QuestionUnits { get; set; }
    }
}
