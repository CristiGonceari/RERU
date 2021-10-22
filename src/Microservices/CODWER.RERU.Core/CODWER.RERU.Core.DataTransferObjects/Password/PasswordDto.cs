
namespace CODWER.RERU.Core.DataTransferObjects.Password
{
    public class PasswordDto
    {
        public string OldPassword { set; get; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
