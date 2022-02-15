using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.PrintQuestionUnits
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class PrintQuestionUnitsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string QuestionName { get; set; }
        public string CategoryName { get; set; }
        public string QuestionTags { get; set; }
        public QuestionTypeEnum? Type { get; set; }
        public QuestionUnitStatusEnum? Status { get; set; }
    }
}
