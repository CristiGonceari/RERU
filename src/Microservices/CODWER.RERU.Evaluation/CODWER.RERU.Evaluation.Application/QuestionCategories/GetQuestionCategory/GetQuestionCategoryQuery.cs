using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategory
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class GetQuestionCategoryQuery : IRequest<QuestionCategoryDto>
    {
        public int Id { get; set; }
    }
}
