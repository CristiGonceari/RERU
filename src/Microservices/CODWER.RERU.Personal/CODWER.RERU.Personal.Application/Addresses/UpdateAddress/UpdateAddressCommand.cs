using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Addresses.UpdateAddress
{
    [ModuleOperation(permission: PermissionCodes.ADDRESS_GENERAL_ACCESS)]
    public class UpdateAddressCommand : IRequest<Unit>
    {
        public AddressDto Data { get; set; }
    }
}
