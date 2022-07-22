using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.GetSolicitedPosition
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE_SOLICITATE)]
    public class GetSolicitedPositionQuery : IRequest<SolicitedCandidatePositionDto>
    {
        public int Id { get; set; }
        public int CandidatePositionId { get; set; }
    }
}
