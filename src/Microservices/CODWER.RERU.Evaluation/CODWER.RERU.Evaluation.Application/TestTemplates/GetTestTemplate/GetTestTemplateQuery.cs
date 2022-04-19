using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ȘABLOANELE_DE_TESTE)]

    public class GetTestTemplateQuery : IRequest<TestTemplateDto>
    {
        public int Id { get; set; }
    }
}
