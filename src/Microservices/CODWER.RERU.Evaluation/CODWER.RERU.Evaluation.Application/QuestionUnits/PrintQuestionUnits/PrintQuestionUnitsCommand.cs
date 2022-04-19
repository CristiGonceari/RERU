using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.PrintQuestionUnits
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARI)]

    public class PrintQuestionUnitsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string QuestionName { get; set; }
        public string CategoryName { get; set; }
        public int? QuestionCategoryId { get; set; }
        public string QuestionTags { get; set; }
        public QuestionTypeEnum? Type { get; set; }
        public QuestionUnitStatusEnum? Status { get; set; }
    }
}
