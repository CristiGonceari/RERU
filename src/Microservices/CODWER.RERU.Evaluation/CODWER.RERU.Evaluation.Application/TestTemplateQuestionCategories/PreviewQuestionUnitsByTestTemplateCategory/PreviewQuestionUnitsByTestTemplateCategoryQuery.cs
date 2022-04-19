using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.PreviewQuestionUnitsByTestTemplateCategory
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ȘABLOANELE_DE_TESTE)]
    public class PreviewQuestionUnitsByTestTemplateCategoryQuery : IRequest<List<CategoryQuestionUnitDto>>
    {
        public QuestionCategoryPreviewDto Data { get; set; }
    }
}
