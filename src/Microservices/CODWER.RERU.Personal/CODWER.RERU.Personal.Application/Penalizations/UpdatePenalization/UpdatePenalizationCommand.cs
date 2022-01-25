using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.UpdatePenalization
{
    [ModuleOperation(permission: PermissionCodes.PENALIZATIONS_GENERAL_ACCESS)]

    public class UpdatePenalizationCommand : IRequest<Unit>
    {
        public AddEditPenalizationDto Data { get; set; }
    }
}
