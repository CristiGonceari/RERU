﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARILE_TESTULUI)]
    public class GenerateTestQuestionsCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
        public int EvaluatorId { get; set; }
    }
}
