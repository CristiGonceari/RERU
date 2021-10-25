using System.Collections.Generic;

namespace CVU.ERP.Module.Application.Models.Internal
{
    public class InternalUserProfileCreate
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public bool NotifyAccountCreated { set; get; }
        public List<int> ModuleRoles { get; set; }
    }
}
