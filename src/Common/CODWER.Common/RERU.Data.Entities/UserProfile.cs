﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using Role = RERU.Data.Entities.PersonalEntities.Role;

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
            SolicitedVacantPositionUserFiles = new HashSet<SolicitedVacantPositionUserFile>();
            SolicitedVacantPositions = new HashSet<SolicitedVacantPosition>();
        }

        public string FullName => $"{LastName} {FirstName} {FatherName}";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Idnp { get; set; }
        public string Email { get; set; }
        public string MediaFileId { get; set; }
        public bool RequiresDataEntry { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Token { set; get; }
        public bool IsActive { set; get; }
        public DateTime? TokenLifetime { get; set; }
        public AccessModeEnum? AccessModeEnum { get; set; }

        public int? DepartmentColaboratorId { get; set; }
        public Department Department { get; set; }

        public int? RoleColaboratorId { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public Contractor Contractor { get; set; }


        [JsonIgnore]
        public List<UserProfileModuleRole> ModuleRoles { set; get; }
        [JsonIgnore]
        public List<UserProfileIdentity> Identities { set; get; }
        [JsonIgnore]
        public virtual ICollection<UserFile> UserFiles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Test> Tests { get; set; }
        [JsonIgnore]
        public virtual ICollection<Test> TestsWithEvaluator { get; set; }
        [JsonIgnore]
        public virtual ICollection<LocationResponsiblePerson> LocationResponsiblePersons { get; set; }
        [JsonIgnore]
        public virtual ICollection<EventResponsiblePerson> EventResponsiblePersons { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlanResponsiblePerson> PlanResponsiblePersons { get; set; }
        [JsonIgnore]
        public virtual ICollection<EventUser> EventUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Notification> Notifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmailTestNotification> EmailTestNotifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<SolicitedVacantPositionUserFile> SolicitedVacantPositionUserFiles { get; set; }
        [JsonIgnore]
        public virtual ICollection<RegistrationFluxStep> RegistrationFluxSteps { get; set; }
        [JsonIgnore]
        public virtual ICollection<SolicitedVacantPosition> SolicitedVacantPositions { get; set; }
    }
}
