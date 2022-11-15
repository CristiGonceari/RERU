using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.ChangeCandidatePositionStatus
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class ChangeCandidatePositionStatusCommand : IRequest<Unit>
    {
        public int PositionId { get; set; }
    }
}
