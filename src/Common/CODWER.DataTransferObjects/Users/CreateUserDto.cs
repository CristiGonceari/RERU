namespace CVU.ERP.Common.DataTransferObjects.Users
{
    public class CreateUserDto
    {
        public string Name{get;set;} 
        public string LastName{get;set;}
        public string FatherName{get;set;}
        public string Email{get;set;} 
        public string Idnp{get;set; }
        public string MediaFileId { get; set; }
        public bool EmailNotification { get; set; }
    }
}
