using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.PrintUserEvents
{
    public class PrintUserEventsCommand : TableParameter, IRequest<FileDataDto>
    {
        public TestTemplateModeEnum TestTemplateMode { get; set; }
        public int UserId { get; set; }
    }
}
