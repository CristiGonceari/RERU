﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore
{
    [ModuleOperation(permission: PermissionCodes.TEST_VERIFICATION_GENERAL_ACCESS)]
    public class AutoCheckTestScoreCommand : IRequest<Response>
    {
        public int TestId { get; set; }
    }
}
