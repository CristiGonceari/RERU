using System;
using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Core.Data.Entities
{
    public class UserProfile : SoftDeleteBaseEntity
    {
        public UserProfile()
        {
            ModuleRoles = new List<UserProfileModuleRole>();
            Identities = new List<UserProfileIdentity>();
        }

        //public string UserId { get; set; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Email { set; get; }
        public string Idnp { set; get; }

        public string Token { set; get; }
        public bool IsActive { set; get; }
        public Document Avatar { get; set; }
        public DateTime? TokenLifetime { get; set; }
        public List<UserProfileModuleRole> ModuleRoles { set; get; }
        public List<UserProfileIdentity> Identities { set; get; }
        public bool RequiresDataEntry { get; set; }
    }
}