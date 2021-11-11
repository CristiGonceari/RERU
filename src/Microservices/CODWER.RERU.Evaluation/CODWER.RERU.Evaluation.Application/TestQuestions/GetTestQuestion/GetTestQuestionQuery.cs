using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion
{
    [ModuleOperation(permission: PermissionCodes.TEST_QUESTIONS_GENERAL_ACCESS)]
    public class GetTestQuestionQuery : IRequest<TestQuestionDto>
    {
        public AddTestQuestionDto Data { get; set; }
    }
}
