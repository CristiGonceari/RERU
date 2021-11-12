using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEvents
{
    public class GetMyEventsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public TestTypeModeEnum TestTypeMode { get; set; }
    }
}
