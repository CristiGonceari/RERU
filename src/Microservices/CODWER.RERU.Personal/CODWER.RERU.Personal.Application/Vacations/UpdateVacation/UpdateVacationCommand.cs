using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacation
{
    [ModuleOperation(permission: PermissionCodes.VACATIONS_GENERAL_ACCESS)]

    public class UpdateVacationCommand : IRequest<Unit>
    {
        public AddEditVacationDto Data { get; set; }

        public UpdateVacationCommand(AddEditVacationDto dto)
        {
            Data = dto;
        }
    }
}
