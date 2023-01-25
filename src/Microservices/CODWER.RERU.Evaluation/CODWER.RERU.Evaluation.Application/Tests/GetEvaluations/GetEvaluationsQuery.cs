using System;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.GetEvaluations
{
    public class GetEvaluationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public string EvaluatorName { get; set; }
        public string Email { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public TestResultStatusEnum? ResultStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public int? FunctionId { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
