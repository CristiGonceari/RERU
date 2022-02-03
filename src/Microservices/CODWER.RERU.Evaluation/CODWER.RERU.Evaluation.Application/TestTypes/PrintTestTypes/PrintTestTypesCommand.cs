using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.PrintTestTypes
{
    [ModuleOperation(permission: PermissionCodes.TEST_TYPES_GENERAL_ACCESS)]
    public class PrintTestTypesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public TestTypeStatusEnum? Status { get; set; }
    }
}
