using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetAllEventsValues
{
    public class GetAllEventsValuesQuery : IRequest<List<SelectEventValueDto>>
    {
    }
}
