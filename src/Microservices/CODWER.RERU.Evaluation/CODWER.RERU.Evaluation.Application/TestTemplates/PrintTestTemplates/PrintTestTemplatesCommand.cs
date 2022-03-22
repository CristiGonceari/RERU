using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.PrintTestTemplates
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]
    public class PrintTestTemplatesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public TestTemplateStatusEnum? Status { get; set; }
    }
}
