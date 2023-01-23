using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;

namespace CODWER.RERU.Logging.Application.PrintLogs
{
    public class PrintLogsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Event { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        public string EventMessage { get; set; }
        public string JsonMessage { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
