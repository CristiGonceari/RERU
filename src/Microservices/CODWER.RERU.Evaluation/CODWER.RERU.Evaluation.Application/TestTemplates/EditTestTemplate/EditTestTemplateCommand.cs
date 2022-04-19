using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]

    public class EditTestTemplateCommand : IRequest<int>
    {
        public AddEditTestTemplateDto Data { get; set; }
    }
}
