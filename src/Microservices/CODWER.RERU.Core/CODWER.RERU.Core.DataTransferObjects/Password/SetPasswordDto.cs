namespace CODWER.RERU.Core.DataTransferObjects.Password {
    public class SetPasswordDto {
        public int Id { set; get; }
        public string Password { get; set; }
        public string RepeatNewPassword { get; set; }
        public bool EmailNotification { get; set; }
    }
}