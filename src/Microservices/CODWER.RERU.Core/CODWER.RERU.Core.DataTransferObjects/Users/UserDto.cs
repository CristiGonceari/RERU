namespace CODWER.RERU.Core.DataTransferObjects.Users {
    public class UserDto {
        public string Id { get; set; }
        public string? MediaFileId { get; set; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string LockoutEnabled { get; set; }
    }
}