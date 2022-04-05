using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Events.PrintEvents
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class PrintEventsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string LocationKeyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
