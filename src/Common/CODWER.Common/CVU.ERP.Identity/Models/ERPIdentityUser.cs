using Microsoft.AspNetCore.Identity;

namespace CVU.ERP.Identity.Models {
    public class ERPIdentityUser : IdentityUser {
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Avatar { set; get; }
    }
}