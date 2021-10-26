using CVU.ERP.Common.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.CreateUser 
{

    public class CreateUserCommand : IRequest<int> 
    {
        public CreateUserCommand (CreateUserDto user) 
        {
            User = user;
        }

        public CreateUserDto User { set; get; }
    }
}