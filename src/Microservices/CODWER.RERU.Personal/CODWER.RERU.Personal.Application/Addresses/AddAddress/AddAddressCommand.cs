using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Addresses.AddAddress
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ADRESA)]
    public class AddAddressCommand : IRequest<int>
    {
        public AddressDto Data { get; set; }
    }
}