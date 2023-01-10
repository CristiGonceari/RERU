using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetUserDetailsByEmail
{
    public  class GetUserDetailsByEmailQuery : IRequest<UserDetailsOverviewDto>
    {
        public GetUserDetailsByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { set; get; }
    }
}
