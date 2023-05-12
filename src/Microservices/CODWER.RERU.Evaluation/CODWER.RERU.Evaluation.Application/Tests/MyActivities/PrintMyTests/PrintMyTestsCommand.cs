using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyTests
{
    public class PrintMyTestsCommand : TableParameter, IRequest<FileDataDto>
    {
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TestName { get; set; }
        public string EventName { get; set; }
    }
}
