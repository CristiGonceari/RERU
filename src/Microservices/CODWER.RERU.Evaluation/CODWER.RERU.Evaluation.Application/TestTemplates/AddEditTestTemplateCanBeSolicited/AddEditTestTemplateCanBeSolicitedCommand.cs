using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateCanBeSolicited
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]
    public class AddEditTestTemplateCanBeSolicitedCommand : IRequest<int>
    {
        public int TestTemplateId { get; set; }
    }
}
