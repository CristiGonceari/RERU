using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Events.GetUserEvents
{
    public class GetUserEventsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public TestTemplateModeEnum TestTemplateMode { get; set; }
        public int UserId { get; set; }
    }
}
