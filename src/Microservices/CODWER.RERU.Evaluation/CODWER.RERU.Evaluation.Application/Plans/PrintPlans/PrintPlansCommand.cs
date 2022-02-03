using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Plans.PrintPlans
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class PrintPlansCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
