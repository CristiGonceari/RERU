using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]
    public class DeleteAllOptionsByQuestionCommand : IRequest<Unit>
    {
        public int QuestionUnitId { get; set; }
    }
}
