using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategories
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class GetQuestionCategoriesQuery: PaginatedQueryParameter, IRequest<PaginatedModel<QuestionCategoryDto>>
    {
        public string Name { get; set; }
    }
}
