using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractor
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CONTRATORI)]

    public class GetContractorQuery : IRequest<ContractorDetailsDto>
    {
        public int Id { get; set; }
    }
}
