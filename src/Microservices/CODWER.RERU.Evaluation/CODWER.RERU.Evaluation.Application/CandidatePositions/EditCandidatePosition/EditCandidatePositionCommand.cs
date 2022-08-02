using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class EditCandidatePositionCommand : IRequest<Unit>
    {
        public AddEditCandidatePositionDto Data { get; set; }
    }
    
}
