using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommand : IRequest<int>
    {
        public string Email { get; set; }
    }
}
