using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using RERU.Data.Entities.Enums;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEvaluationRowDto
{
    public class EvaluationRowDtoQuery: PaginatedQueryParameter, IRequest<PaginatedModel<EvaluationRowDto>>
    {
        public string EvaluatedName { set; get; }
        public string EvaluatorName { set; get; }
        public string? CounterSignerName { set; get; }
        public int? Type { set; get; }
        public int? Status { set; get; }
        public DateTime? CreateDateFrom { set; get; }
        public DateTime? CreateDateTo { set; get; }
    }
}