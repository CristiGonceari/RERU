using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Addresses.RemoveAddress
{
    [ModuleOperation(permission: PermissionCodes.ADDRESS_GENERAL_ACCESS)]
    public class RemoveAddressCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
