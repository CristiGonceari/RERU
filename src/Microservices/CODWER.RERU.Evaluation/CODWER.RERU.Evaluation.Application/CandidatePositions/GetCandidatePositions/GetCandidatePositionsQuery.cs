using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePositions
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class GetCandidatePositionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<CandidatePositionDto>>
    {
        public string Name { get; set; }
    }
}
