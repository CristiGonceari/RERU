using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalization
{
    [ModuleOperation(permission: PermissionCodes.PENALIZATIONS_GENERAL_ACCESS)]

    public class GetPenalizationQuery : IRequest<PenalizationDto>
    {
        public int Id { get; set; }
    }
}
