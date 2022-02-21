using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.testTemplateQuestionCategories;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.AssignQuestionCategoryToTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class AssignQuestionCategoryToTestTemplateCommand : IRequest<int>
    {
        public AssignQuestionCategoryToTestTemplateDto Data { get; set; }
    }
}
