using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ȘABLOANELE_DE_TESTE)]

    public class CloneTestTemplateCommand : IRequest<int>
    {
        public int TestTemplateId { get; set; }
    }
}
