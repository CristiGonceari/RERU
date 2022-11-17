using System.Collections.Generic;
using System.Linq;

namespace CVU.ERP.ServiceProvider.Models
{
    ///<summary>
    /// This class is used to represent the user against the application
    /// If Id is null or empty, the User will have IsAnonymous = true
    ///</summary>
    public class ApplicationUser 
    {
        public ApplicationUser () 
        {
            Modules = new ApplicationUserModule[] { };
        }

        public string Id { set; get; }

        public bool IsAnonymous => string.IsNullOrEmpty(Id);
        public string FullName => $"{LastName} {FirstName} {FatherName}";

        public string FirstName { set; get; }
        public string LastName { get; set; }
        public string FatherName { get; set; }

        public string Email { set; get; }
        public string Idnp { set; get; }

        public IEnumerable<ApplicationUserModule> Modules { set; get; }
        public IEnumerable<string> Permissions => Modules.SelectMany (m => m.Permissions);
    }
}