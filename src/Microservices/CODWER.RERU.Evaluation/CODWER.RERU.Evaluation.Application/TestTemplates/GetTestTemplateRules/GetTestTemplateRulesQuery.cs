using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateRules
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]

    public class GetTestTemplateRulesQuery : IRequest<RulesDto>
    {
        public int TestTypeId { get; set; }
    }
}
