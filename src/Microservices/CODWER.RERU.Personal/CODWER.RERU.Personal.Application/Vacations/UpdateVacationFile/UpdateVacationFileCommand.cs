using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacationFile
{
    [ModuleOperation(permission: PermissionCodes.VACATIONS_GENERAL_ACCESS)]

    public class UpdateVacationFileCommand : IRequest<Unit>
    {
        public UpdateVacationFileDto Data { get; set; }

        public UpdateVacationFileCommand(UpdateVacationFileDto dto)
        {
            Data = dto;
        }
    }
}
