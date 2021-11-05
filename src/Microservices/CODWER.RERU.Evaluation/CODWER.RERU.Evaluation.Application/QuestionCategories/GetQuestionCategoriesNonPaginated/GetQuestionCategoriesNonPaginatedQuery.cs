using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategoriesNonPaginated
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class GetQuestionCategoriesNonPaginatedQuery : IRequest<List<QuestionCategoryDto>>
    {
        public string Name { get; set; }
    }
}
