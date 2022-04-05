namespace CODWER.RERU.Core.DataTransferObjects.Users {
    public class UserDetailsOverviewDto {
        public string Email { set; get; }
        public string Idnp { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string? MediaFileId { get; set; }

    }
}