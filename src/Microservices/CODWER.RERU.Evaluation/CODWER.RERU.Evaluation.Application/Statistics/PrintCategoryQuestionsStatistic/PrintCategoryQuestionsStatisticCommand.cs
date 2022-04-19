using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Statistics.PrintCategoryQuestionsStatistic
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_STATISTICI)]
    public class PrintCategoryQuestionsStatisticCommand : TableParameter, IRequest<FileDataDto>
    {
        public int CategoryId { get; set; }
        public int ItemsPerPage { get; set; }
        public StatisticsQuestionFilterEnum FilterEnum { get; set; }
    }
}
