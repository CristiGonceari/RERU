namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string? MediaFileId { get; set; }
        public int? CandidatePositionId { set; get; }
        public bool IsActive { set; get; }
    }
}
