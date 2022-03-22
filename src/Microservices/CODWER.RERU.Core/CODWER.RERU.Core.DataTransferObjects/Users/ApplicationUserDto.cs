using CODWER.RERU.Core.DataTransferObjects.Modules;

namespace CODWER.RERU.Core.DataTransferObjects.Users {
    public class ApplicationUserDto {
        public string Name { set; get; }
        public string Email { set; get; }
        public string? MediaFileId { get; set; }
        public string[] Permissions { set; get; }
        public ApplicationUserModuleDto[] Modules { set; get; }
    }
}