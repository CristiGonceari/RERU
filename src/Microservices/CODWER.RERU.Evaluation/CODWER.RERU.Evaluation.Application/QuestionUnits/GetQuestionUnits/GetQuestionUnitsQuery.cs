using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnits
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRI)]

    public class GetQuestionUnitsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<QuestionUnitDto>>
    {
        public string QuestionName { get; set; }
        public string CategoryName { get; set; }
        public int? QuestionCategoryId { get; set; }
        public string QuestionTags { get; set; }
        public QuestionTypeEnum? Type { get; set; }
        public QuestionUnitStatusEnum? Status { get; set; }
    }
}
