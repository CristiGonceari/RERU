using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.SetCategoriesSequence
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]
    public class SetCategoriesSequenceCommand : IRequest<Unit>
    {
        public int TestTemplateId { get; set; }
        public List<TestTemplateQuestionCategoryOrderDto> ItemsOrder { get; set; }

        public SequenceEnum SequenceType { get; set; }
    }
}
