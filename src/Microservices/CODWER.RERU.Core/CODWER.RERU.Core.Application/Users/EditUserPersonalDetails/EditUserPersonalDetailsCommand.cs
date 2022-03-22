using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails 
{
    public class EditUserPersonalDetailsCommand : IRequest<Unit> 
    {
        public EditUserPersonalDetailsDto Data { set; get; }
    }
}