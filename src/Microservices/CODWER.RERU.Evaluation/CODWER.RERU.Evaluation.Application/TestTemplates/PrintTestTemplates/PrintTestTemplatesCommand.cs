using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.PrintTestTemplates
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]
    public class PrintTestTemplatesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public TestTemplateStatusEnum? Status { get; set; }
        public TestTemplateModeEnum? Mode { get; set; }
    }
}
