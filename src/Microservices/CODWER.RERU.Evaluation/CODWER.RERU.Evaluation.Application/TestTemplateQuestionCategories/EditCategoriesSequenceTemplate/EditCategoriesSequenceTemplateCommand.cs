using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.EditCategoriesSequenceTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]
    public class EditCategoriesSequenceTemplateCommand : IRequest<Unit>
    {
        public int TestTemplateId { get; set; }
        public SequenceEnum CategoriesSequenceType { get; set; }
    }
}
