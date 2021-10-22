using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.Profile {
    public class UserProfileOverviewDto {
        public string Email { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Avatar { set; get; }
        public IEnumerable<UserProfileModuleRowDto> Modules { set; get; }
    }
}