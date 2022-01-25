using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contacts.GetContacts
{
    [ModuleOperation(permission: PermissionCodes.CONTACTS_GENERAL_ACCESS)]

    public class GetContactsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContactDto>>
    {
        public int? ContractorId { get; set; }
    }
}
