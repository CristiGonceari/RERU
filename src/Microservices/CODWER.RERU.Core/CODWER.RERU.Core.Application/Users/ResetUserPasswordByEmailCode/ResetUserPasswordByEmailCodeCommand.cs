using MediatR;

namespace CODWER.RERU.Core.Application.Users.ResetUserPasswordByEmailCode
{
    public class ResetUserPasswordByEmailCodeCommand : IRequest<Unit>
    {
        public string Code { set; get; }
        public string Email { get; set; }

    }
}
