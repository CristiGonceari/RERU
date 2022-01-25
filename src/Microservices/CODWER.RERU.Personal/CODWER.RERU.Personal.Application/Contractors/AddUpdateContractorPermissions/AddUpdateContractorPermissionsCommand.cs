using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.AddUpdateContractorPermissions
{
    public class AddUpdateContractorPermissionsCommand : IRequest<Unit>
    {
        public ContractorLocalPermissionsDto Data { get; set; }

        public AddUpdateContractorPermissionsCommand(ContractorLocalPermissionsDto data)
        {
            Data = data;
        }
    }
}
