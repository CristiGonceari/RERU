using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.UpdateContact
{
    [ModuleOperation(permission: PermissionCodes.CONTACTS_GENERAL_ACCESS)]

    public class UpdateContactCommand : IRequest<Unit>
    {
        public AddEditContactDto Data { get; set; }
    }
}
