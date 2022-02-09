using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.DeleteQuestionCategoryFromTestType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class DeleteQuestionCategoryFromTestTemplateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
