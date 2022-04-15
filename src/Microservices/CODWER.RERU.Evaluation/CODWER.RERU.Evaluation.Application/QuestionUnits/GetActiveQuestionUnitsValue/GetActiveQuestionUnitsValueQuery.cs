using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetActiveQuestionUnitsValue
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class GetActiveQuestionUnitsValueQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ActiveQuestionUnitValueDto>>
    {
        public QuestionTypeEnum? Type { get; set; }
        public int? CategoryId { get; set; }
    }
}
