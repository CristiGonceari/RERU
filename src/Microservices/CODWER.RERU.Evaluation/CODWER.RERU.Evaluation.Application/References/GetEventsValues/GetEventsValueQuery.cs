using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.References.GetEventsValues
{
    public class GetEventsValueQuery : IRequest<List<SelectEventValueDto>>
    {
    }
}
