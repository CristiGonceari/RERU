using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.GetUserEvents
{
    public class GetUserEventsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public TestTypeModeEnum TestTypeMode { get; set; }
        public int UserId { get; set; }
    }
}
