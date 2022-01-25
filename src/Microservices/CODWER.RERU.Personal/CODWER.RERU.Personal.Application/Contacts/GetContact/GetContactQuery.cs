using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.GetContact
{
    [ModuleOperation(permission: PermissionCodes.CONTACTS_GENERAL_ACCESS)]

    public class GetContactQuery : IRequest<ContactDto>
    {
        public int Id { get; set; }
    }
}
