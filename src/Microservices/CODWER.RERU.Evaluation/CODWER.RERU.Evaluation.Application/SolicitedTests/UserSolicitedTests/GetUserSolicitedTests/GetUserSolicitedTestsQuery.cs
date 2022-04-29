﻿using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetUserSolicitedTests
{
    public  class GetUserSolicitedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedTestDto>>
    {
        public int UserId { get; set; }
    }
}