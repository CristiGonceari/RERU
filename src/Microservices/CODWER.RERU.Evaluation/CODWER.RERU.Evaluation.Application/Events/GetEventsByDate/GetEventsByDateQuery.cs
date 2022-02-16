using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Events.GetEventsByDate
{
    public class GetEventsByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public DateTime Date { get; set; }
    }
}
