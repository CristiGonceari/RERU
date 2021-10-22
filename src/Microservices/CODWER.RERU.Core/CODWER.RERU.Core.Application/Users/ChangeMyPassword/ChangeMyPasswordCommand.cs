using CODWER.RERU.Core.DataTransferObjects.Password;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ChangeMyPassword
{
    public class ChangeMyPasswordCommand : IRequest<Unit>
    {
        public ChangeMyPasswordCommand(PasswordDto password)
        {
            oldPassword = password.OldPassword;
            newPassword = password.NewPassword;
            repeatPassword = password.RepeatPassword;

        }
        public string oldPassword { set; get; }
        public string newPassword { set; get; }
        public string repeatPassword { set; get; }
    }

}
