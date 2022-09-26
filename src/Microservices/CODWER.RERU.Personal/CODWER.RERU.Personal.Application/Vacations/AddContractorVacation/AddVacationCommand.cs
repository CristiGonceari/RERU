using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.AddContractorVacation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_VACANTE)]

    public class AddVacationCommand : IRequest<int>
    {
        public AddEditVacationDto Data { get; set; }

        public AddVacationCommand(AddEditVacationDto data)
        {
            Data = data;
        }
    }
}
