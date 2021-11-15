using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;

namespace CODWER.RERU.Evaluation.Application.Statistics.GetCategoryQuestionsStatistic
{
    [ModuleOperation(permission: PermissionCodes.STATISTICS_GENERAL_ACCESS)]
    public class GetCategoryQuestionsStatisticQuery : IRequest<List<TestQuestionStatisticDto>>
    {
        public int CategoryId { get; set; }
        public int ItemsPerPage { get; set; }
        public StatisticsQuestionFilterEnum FilterEnum { get; set; }
    }
}
