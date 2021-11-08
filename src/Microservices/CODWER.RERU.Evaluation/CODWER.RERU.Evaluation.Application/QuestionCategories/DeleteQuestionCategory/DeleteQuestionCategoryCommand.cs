using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class DeleteQuestionCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
