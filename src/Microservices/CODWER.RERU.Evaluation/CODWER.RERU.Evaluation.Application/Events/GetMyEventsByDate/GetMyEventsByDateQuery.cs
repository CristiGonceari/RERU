using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEventsByDate
{
    public class GetMyEventsByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public TestTypeModeEnum TestTypeMode { get; set; }
        public DateTime Date { get; set; }
    }
}
