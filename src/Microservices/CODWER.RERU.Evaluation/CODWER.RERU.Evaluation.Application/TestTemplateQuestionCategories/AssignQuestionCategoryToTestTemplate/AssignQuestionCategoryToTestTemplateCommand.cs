using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.AssignQuestionCategoryToTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ȘABLOANELE_DE_TESTE)]
    public class AssignQuestionCategoryToTestTemplateCommand : IRequest<int>
    {
        public AssignQuestionCategoryToTestTemplateDto Data { get; set; }
    }
}
