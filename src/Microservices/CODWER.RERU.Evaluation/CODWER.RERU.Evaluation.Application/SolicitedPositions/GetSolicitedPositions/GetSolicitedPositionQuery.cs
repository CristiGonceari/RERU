using System;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.GetSolicitedPositions
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE_SOLICITATE)]
    public class GetSolicitedPositionQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedCandidatePositionDto>>
    {
        public int? PositionId { get; set; }
        public string UserName { get; set; }
        public SolicitedPositionStatusEnum? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
