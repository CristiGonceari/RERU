using System;
using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class Plan : SoftDeleteBaseEntity
    {
        public Plan()
        {
            Events = new HashSet<Event>();
            PlanResponsiblePersons = new HashSet<PlanResponsiblePerson>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
    }
}
