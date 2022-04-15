using CODWER.RERU.Core.DataTransferObjects.Modules;
using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserForRemoveDto
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { get; set; }
        public string FatherName { set; get; }
        public string Idnp { set; get; }

        public List<UserModuleAccessDto>  ModuleAccess { set; get; }
    }
}
