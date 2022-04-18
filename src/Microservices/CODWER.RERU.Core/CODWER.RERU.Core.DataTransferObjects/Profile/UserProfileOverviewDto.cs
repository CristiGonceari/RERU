using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.Profile {
    public class UserProfileOverviewDto {
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Idnp { set; get; }
        public string? MediaFileId { set; get; }
        public IEnumerable<UserProfileModuleRowDto> Modules { set; get; }
    }
}