using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.UnassignTagFromQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]
    public class UnassignTagFromQuestionUnitCommand : IRequest<Unit>
    {
        public int TagId { get; set; }
        public int QuestionId { get; set; }
    }
}
