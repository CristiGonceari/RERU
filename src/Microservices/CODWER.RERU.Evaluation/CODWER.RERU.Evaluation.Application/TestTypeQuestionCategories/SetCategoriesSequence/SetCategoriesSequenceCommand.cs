using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.SetCategoriesSequence
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPE_QUESTION_CATEGORIES_GENERAL_ACCESS)]
    public class SetCategoriesSequenceCommand : IRequest<Unit>
    {
        public int TestTypeId { get; set; }
        public List<TestTypeQuestionCategoryOrderDto> ItemsOrder { get; set; }

        public SequenceEnum SequenceType { get; set; }
    }
}
