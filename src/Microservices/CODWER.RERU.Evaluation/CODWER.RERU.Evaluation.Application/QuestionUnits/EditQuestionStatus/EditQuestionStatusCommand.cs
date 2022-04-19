using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARI)]
    public class EditQuestionStatusCommand : IRequest<Unit>
    {
        public EditQuestionStatusDto Data { get; set; }
    }
}
