using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.EditCategoriesSequenceType
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]
    public class EditCategoriesSequenceTypeCommand : IRequest<Unit>
    {
        public int TestTypeId { get; set; }
        public SequenceEnum CategoriesSequenceType { get; set; }
    }
}
