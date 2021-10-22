namespace CODWER.RERU.Core.DataTransferObjects.UserProfileModuleRoles {
    public class AddEditModuleAccessWithDetailsDto : AddEditModuleAccessDto {
        public string UserName { set; get; }
        public string UserLastName { set; get; }
        public string UserEmail { set; get; }
        public string ModuleName { set; get; }
        public string RoleName { set; get; }
    }
}