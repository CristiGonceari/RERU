namespace CVU.ERP.Common.DataTransferObjects.Users
{
    public class CreateUserDto
    {
        public string Name{get;set;} 
        public string LastName{get;set;} 
        public string Email{get;set;} 
        public bool EmailNotification { get; set; }
    }
}
