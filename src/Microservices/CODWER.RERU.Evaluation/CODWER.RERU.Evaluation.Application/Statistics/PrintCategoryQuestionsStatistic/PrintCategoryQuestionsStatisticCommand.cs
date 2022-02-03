using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Statistics.PrintCategoryQuestionsStatistic
{
    [ModuleOperation(permission: PermissionCodes.STATISTICS_GENERAL_ACCESS)]
    public class PrintCategoryQuestionsStatisticCommand : TableParameter, IRequest<FileDataDto>
    {
        public int CategoryId { get; set; }
        public int ItemsPerPage { get; set; }
        public StatisticsQuestionFilterEnum FilterEnum { get; set; }
    }
}
