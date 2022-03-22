using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Statistics.PrintTestQuestionsStatistic
{
    public class PrintTestQuestionsStatisticCommand : TableParameter, IRequest<FileDataDto>
    {
        [ModuleOperation(permission: PermissionCodes.STATISTICS_GENERAL_ACCESS)]

        public int TestTemplateId { get; set; }
        public int ItemsPerPage { get; set; }
        public StatisticsQuestionFilterEnum FilterEnum { get; set; }
    }
}
