using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEventsByDate
{
    public class GetMyEventsByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public TestTemplateModeEnum TestTemplateMode { get; set; }
        public DateTime Date { get; set; }
    }
}
