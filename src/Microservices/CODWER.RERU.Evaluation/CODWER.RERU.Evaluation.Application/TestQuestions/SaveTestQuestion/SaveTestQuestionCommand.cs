using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion
{
    [ModuleOperation(permission: PermissionCodes.TEST_QUESTIONS_GENERAL_ACCESS)]
    public class SaveTestQuestionCommand : IRequest<Unit>
    {
        public AddTestQuestionDto Data { get; set; }
    }
}
