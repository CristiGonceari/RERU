using MediatR;

namespace CODWER.RERU.Core.Application.Users.SaveUserPasswordByEmail
{
    public class SaveUserPasswordByEmailCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
