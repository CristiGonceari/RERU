using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class DeleteQuestionUnitCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
