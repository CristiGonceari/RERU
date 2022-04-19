using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARILE_TESTULUI)]
    public class SaveTestQuestionCommand : IRequest<Unit>
    {
        public AddTestQuestionDto Data { get; set; }
    }
}
