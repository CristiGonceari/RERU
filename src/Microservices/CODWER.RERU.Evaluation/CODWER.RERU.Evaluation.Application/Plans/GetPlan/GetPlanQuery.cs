﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlan
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class GetPlanQuery : IRequest<PlanDto>
    {
        public int Id { get; set; }
    }
}
