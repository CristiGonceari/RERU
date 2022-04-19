using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.ValidateTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]

    public class ValidateTestTemplateQuery : IRequest<Unit>
    {
        public int TestTemplateId { get; set; }
    }
}
