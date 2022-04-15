using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
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
            EmailTestNotifications = new HashSet<EmailTestNotification>();
            UserFiles = new HashSet<UserFile>();
            ModuleRoles = new List<UserProfileModuleRole>();
            Identities = new List<UserProfileIdentity>();
        }

        public string CoreUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Idnp { get; set; }
        public string Email { get; set; }
        public string MediaFileId { get; set; }
        public bool RequiresDataEntry { get; set; }
        
        public string Token { set; get; }
        public bool IsActive { set; get; }
        public DateTime? TokenLifetime { get; set; }

        public int? CandidatePositionId { set; get; }
        public CandidatePosition CandidatePosition { set; get; }

        [JsonIgnore]
        public List<UserProfileModuleRole> ModuleRoles { set; get; }
        [JsonIgnore]
        public List<UserProfileIdentity> Identities { set; get; }

        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Test> TestsWithEvaluator { get; set; }
        public virtual ICollection<LocationResponsiblePerson> LocationResponsiblePersons { get; set; }
        public virtual ICollection<EventResponsiblePerson> EventResponsiblePersons { get; set; }
        public virtual ICollection<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<EmailTestNotification> EmailTestNotifications { get; set; }
    }
}
