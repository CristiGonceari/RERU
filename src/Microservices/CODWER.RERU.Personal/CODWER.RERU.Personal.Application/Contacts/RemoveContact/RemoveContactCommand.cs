using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.RemoveContact
{
    [ModuleOperation(permission: PermissionCodes.CONTACTS_GENERAL_ACCESS)]

    public class RemoveContactCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
