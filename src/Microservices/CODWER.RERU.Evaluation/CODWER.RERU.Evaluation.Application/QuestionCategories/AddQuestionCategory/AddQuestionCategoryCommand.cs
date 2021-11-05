using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.AddQuestionCategory
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class AddQuestionCategoryCommand : IRequest<int>
    {
        public AddEditQuestionCategoryDto Data { get; set; }
    }
}
