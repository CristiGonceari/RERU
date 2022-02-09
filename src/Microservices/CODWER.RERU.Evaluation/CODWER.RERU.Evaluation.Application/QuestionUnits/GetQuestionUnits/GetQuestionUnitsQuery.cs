using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnits
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class GetQuestionUnitsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<QuestionUnitDto>>
    {
        public string QuestionName { get; set; }
        public int? QuestionCategoryId { get; set; }
        public QuestionTypeEnum? Type { get; set; }
    }
}
