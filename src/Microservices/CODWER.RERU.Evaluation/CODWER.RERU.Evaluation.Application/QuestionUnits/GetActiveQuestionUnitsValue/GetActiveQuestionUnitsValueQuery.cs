using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetActiveQuestionUnitsValue
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]
    public class GetActiveQuestionUnitsValueQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ActiveQuestionUnitValueDto>>
    {
        public QuestionTypeEnum? Type { get; set; }
        public int? CategoryId { get; set; }
    }
}
