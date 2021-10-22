using System.Collections.Generic;
using System.Linq;

namespace CVU.ERP.Module.Application.Models {
    ///<summary>
    /// This class is used to represent the user against the application
    /// If Id is null or empty, the User will have IsAnonymous = true
    ///</summary>
    public class ApplicationUser {
        public ApplicationUser () {
            Modules = new ApplicationUserModule[] { };
        }
        public string Id { set; get; }
        public bool IsAnonymous => string.IsNullOrEmpty(Id);
        public string Name { set; get; }
        public string Email { set; get; }
        public IEnumerable<ApplicationUserModule> Modules { set; get; }
        public IEnumerable<string> Permissions => Modules.SelectMany (m => m.Permissions);

    }
}