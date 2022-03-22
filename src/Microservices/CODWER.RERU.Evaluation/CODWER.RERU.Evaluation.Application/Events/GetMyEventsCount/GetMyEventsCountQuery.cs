using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEventsCount
{
    public class GetMyEventsCountQuery : IRequest<List<EventCount>>
    {
        public TestTemplateModeEnum TestTemplateMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
    }
}
