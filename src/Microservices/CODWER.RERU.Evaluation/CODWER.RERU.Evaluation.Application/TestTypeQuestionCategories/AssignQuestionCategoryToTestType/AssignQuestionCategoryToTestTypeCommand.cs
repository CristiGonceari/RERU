using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.AssignQuestionCategoryToTestType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPE_QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class AssignQuestionCategoryToTestTypeCommand : IRequest<int>
    {
        public AssignQuestionCategoryToTestTypeDto Data { get; set; }
    }
}
