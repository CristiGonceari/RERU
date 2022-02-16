using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Events.GetEventCount
{
    public class GetEventCountQuery : IRequest<List<EventCount>>
    {
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
    }
}
