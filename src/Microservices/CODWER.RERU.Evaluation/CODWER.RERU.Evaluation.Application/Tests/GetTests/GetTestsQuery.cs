using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTests
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class GetTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
