using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Plans.PrintPlans
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class PrintPlansCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
