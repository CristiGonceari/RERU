using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]

    public class AddTestTemplateCommand : IRequest<int>
    {
        public AddEditTestTemplateDto Data { get; set; }
    }
}
