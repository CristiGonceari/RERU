namespace CODWER.RERU.Core.DataTransferObjects.Users {
    public class UserDetailsOverviewDto {
        public string Email { set; get; }
        public string Idnp { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string? MediaFileId { get; set; }
        public string? CandidatePositionName { set; get; }

    }
}