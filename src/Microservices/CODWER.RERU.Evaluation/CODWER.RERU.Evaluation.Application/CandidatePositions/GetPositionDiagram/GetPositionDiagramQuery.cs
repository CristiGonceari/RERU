using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionDiagram
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class GetPositionDiagramQuery : IRequest<PositionDiagramDto>
    {
        public int PositionId { get; set; }
    }
}
