using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Addresses.GetAddress
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ADRESA)]
    public class GetAddressQuery : IRequest<AddressDto>
    {
        public int Id { get; set; }
    }
}
