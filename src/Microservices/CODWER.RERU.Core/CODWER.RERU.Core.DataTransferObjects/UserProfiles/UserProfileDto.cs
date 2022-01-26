namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { set; get; }
    }
}
