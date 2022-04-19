using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRI)]
    public class AssignTagToQuestionUnitCommand : IRequest<Unit>
    {
        public int QuestionUnitId { get; set; }
        public List<string> Tags { get; set; }
    }
}
