using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestionsSummary
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRILE_TESTULUI)]
    public class GetTestQuestionsSummaryQuery : IRequest<List<TestQuestionSummaryDto>>
    {
        public int TestId { get; set; }
    }
}
