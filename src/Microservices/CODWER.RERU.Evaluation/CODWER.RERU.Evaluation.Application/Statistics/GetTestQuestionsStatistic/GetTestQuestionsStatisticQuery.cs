using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Statistics.GetTestQuestionsStatistic
{
    [ModuleOperation(permission: PermissionCodes.STATISTICS_GENERAL_ACCESS)]
    public class GetTestQuestionsStatisticQuery : IRequest<List<TestQuestionStatisticDto>>
    {
        public int TestTemplateId { get; set; }
        public int ItemsPerPage { get; set; }
        public StatisticsQuestionFilterEnum FilterEnum { get; set; }
    }
}
