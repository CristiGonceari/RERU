using MediatR;

namespace CODWER.RERU.Core.Application.Users.ResetPasswordByEmail
{
    public class ResetPasswordByEmailCommand : IRequest<Unit>
    {
        public ResetPasswordByEmailCommand(string email)
        {
            Email = email;
        }
        public string Email { set; get; }
    }
}
