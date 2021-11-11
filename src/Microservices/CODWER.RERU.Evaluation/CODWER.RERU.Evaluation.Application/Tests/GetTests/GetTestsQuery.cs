﻿using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTests
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class GetTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string TestTypeName { get; set; }
        public string UserName { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
