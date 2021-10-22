using CODWER.RERU.Core.DataTransferObjects.Modules;
using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserForRemoveDto
    {
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Email { get; set; }
        public List<UserModuleAccessDto>  ModuleAccess { set; get; }
    }
}
