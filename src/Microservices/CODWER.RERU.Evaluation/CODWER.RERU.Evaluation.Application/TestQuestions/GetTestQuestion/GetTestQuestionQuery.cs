using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRILE_TESTULUI)]
    public class GetTestQuestionQuery : IRequest<TestQuestionDto>
    {
        public int TestId { get; set; }
        public int QuestionIndex { get; set; }
        public AnswerStatusEnum Status { get; set; }
        public List<TestAnswerDto> Answers { get; set; }
    }
}
