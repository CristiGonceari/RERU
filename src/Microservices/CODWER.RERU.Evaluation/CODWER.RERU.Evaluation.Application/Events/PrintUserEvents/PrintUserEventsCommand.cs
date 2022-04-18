using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Events.PrintUserEvents
{
    public class PrintUserEventsCommand : TableParameter, IRequest<FileDataDto>
    {
        public TestTemplateModeEnum TestTemplateMode { get; set; }
        public int UserId { get; set; }
    }
}
