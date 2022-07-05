using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTests
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class GetTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public string EvaluatorName { get; set; }
        public string Email { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public TestResultStatusEnum? ResultStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
