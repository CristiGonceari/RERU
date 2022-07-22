using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.EditSolicitedPositionStatus
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE_SOLICITATE)]
    public class EditSolicitedPositionStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public SolicitedPositionStatusEnum Status { get; set; }
    }
}
