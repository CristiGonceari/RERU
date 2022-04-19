namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public bool EmailNotification { get; set; }
    }
}
