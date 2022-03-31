namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class EditUserPersonalDetailsDto {
        public int Id { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string MediaFileId { get; set; }
    }
}