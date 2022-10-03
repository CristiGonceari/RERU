using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.AddContact
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTACTE)]

    public class AddContactCommand : IRequest<int>
    {
        public AddEditContactDto Data { get; set; }
    }
}
