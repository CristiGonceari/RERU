using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ResetPasswordByEmailToken
{
    public class ResetPasswordByEmailTokenCommand : IRequest<Unit>
    {
        public ResetPasswordByEmailTokenCommand(EmailConfirmationDto token)
        {
            Token = token;
        }
        public EmailConfirmationDto Token { set; get; }
    }
}
