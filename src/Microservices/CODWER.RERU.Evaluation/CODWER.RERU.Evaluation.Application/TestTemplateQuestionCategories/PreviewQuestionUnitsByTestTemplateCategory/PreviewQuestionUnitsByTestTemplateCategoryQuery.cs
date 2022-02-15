using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.PreviewQuestionUnitsByTestTemplateCategory
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class PreviewQuestionUnitsByTestTemplateCategoryQuery : IRequest<List<CategoryQuestionUnitDto>>
    {
        public QuestionCategoryPreviewDto Data { get; set; }
    }
}
