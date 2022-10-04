using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetTestTemplatesByEventsIds
{
    public class GetTestTemplatesByEventsIdsQuery : IRequest<List<TestTemplatesByEventDto>>
    {
        public List<int> EventIds { get; set; }
    }
}
