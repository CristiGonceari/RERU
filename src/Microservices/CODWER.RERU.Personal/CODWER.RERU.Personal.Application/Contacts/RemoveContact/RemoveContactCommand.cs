using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.RemoveContact
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTACTE)]

    public class RemoveContactCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
