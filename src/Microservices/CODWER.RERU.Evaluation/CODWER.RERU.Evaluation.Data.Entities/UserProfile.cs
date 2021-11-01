using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class UserProfile : SoftDeleteBaseEntity
    {
        public UserProfile()
        {
            Tests = new HashSet<Test>();
            TestsWithEvaluator = new HashSet<Test>();
            LocationResponsiblePersons = new HashSet<LocationResponsiblePerson>();
            EventResponsiblePersons = new HashSet<EventResponsiblePerson>();
            PlanResponsiblePersons = new HashSet<PlanResponsiblePerson>();
            EventUsers = new HashSet<EventUser>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string CoreUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public List<UserProfileIdentity> Identities { set; get; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Test> TestsWithEvaluator { get; set; }
        public virtual ICollection<LocationResponsiblePerson> LocationResponsiblePersons { get; set; }
        public virtual ICollection<EventResponsiblePerson> EventResponsiblePersons { get; set; }
        public virtual ICollection<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
