//using System;
//using System.Collections.Generic;
//using CVU.ERP.Common.Data.Entities;

//namespace CODWER.RERU.Evaluation.Data.Entities
//{
//    public class Event : SoftDeleteBaseEntity
//    {
//        public Event()
//        {
//            Tests = new HashSet<Test>();
//            EventTestTemplates = new HashSet<EventTestTemplate>();
//            EventResponsiblePersons = new HashSet<EventResponsiblePerson>();
//            EventUsers = new HashSet<EventUser>();
//            EventLocations = new HashSet<EventLocation>();
//            EventEvaluators = new HashSet<EventEvaluator>();
//        }

//        public string Name { get; set; }
//        public string Description { get; set; }

//        public DateTime FromDate { get; set; }
//        public DateTime TillDate { get; set; }

//        public int? PlanId { get; set; }
//        public Plan Plan { get; set; }

//        public virtual ICollection<Test> Tests { get; set; }
//        public virtual ICollection<EventTestTemplate> EventTestTemplates { get; set; }
//        public virtual ICollection<EventResponsiblePerson> EventResponsiblePersons { get; set; }
//        public virtual ICollection<EventUser> EventUsers { get; set; }
//        public virtual ICollection<EventLocation> EventLocations { get; set; }
//        public virtual ICollection<EventEvaluator> EventEvaluators { get; set; }
//    }
//}
