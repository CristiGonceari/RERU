using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.AddContractorVacation
{
    [ModuleOperation(permission: PermissionCodes.VACATIONS_GENERAL_ACCESS)]

    public class AddVacationCommand : IRequest<int>
    {
        public AddEditVacationDto Data { get; set; }

        public AddVacationCommand(AddEditVacationDto data)
        {
            Data = data;
        }
    }
}
