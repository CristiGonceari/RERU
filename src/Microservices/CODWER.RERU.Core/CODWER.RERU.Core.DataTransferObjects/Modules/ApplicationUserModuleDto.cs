namespace CODWER.RERU.Core.DataTransferObjects.Modules {
    public class ApplicationUserModuleDto {
        public ApplicationModuleDto Module { set; get; }
        public string[] Permissions { set; get; }
        public string Role { set; get; }
    }
}