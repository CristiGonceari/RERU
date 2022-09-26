using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.GetContact
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTACTE)]

    public class GetContactQuery : IRequest<ContactDto>
    {
        public int Id { get; set; }
    }
}
