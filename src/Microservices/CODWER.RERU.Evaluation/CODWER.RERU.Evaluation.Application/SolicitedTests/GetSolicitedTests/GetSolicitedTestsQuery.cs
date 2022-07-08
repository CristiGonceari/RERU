using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTests
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE_SOLICITATE)]
    public class GetSolicitedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedCandidatePositionDto>>
    {
        public int? PositionId { get; set; }
        public string UserName { get; set; }
        public SolicitedPositionStatusEnum? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
