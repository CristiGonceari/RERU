using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.CloneTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]

    public class CloneTestTemplateCommand : IRequest<int>
    {
        public int TestTypeId { get; set; }
    }
}
