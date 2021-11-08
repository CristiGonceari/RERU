using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.DeleteQuestionCategoryFromTestType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPE_QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class DeleteQuestionCategoryFromTestTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
