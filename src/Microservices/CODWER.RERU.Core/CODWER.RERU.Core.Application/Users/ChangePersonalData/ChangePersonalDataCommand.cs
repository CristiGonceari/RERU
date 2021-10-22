using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData
{
    [ModuleOperation(null, requiresAuthentication: true)]
    public class ChangePersonalDataCommand : IRequest<Unit>
    {
        public ChangePersonalDataCommand(UserPersonalDataDto user)
        {
            User = user;
        }
        public UserPersonalDataDto User { set; get; }
    }
}

