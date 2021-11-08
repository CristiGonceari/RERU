using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.PreviewQuestionUnitsByTestTypeCategory
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPE_QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class PreviewQuestionUnitsByTestTypeCategoryQuery : IRequest<List<CategoryQuestionUnitDto>>
    {
        public QuestionCategoryPreviewDto Data { get; set; }
    }
}
