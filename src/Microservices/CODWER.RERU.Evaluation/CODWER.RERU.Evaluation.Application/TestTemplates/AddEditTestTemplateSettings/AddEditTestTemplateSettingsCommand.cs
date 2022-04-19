using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateSettings
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]

    public class AddEditTestTemplateSettingsCommand : IRequest<Unit>
    {
        public TestTemplateSettingsDto Data { get; set; }
    }

}
