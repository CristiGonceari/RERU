﻿using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.EditMySolicitedTest
{
    public class EditMySolicitedTestCommand : IRequest<int>
    {
        public AddEditSolicitedTestDto Data { get; set; }
    }
}