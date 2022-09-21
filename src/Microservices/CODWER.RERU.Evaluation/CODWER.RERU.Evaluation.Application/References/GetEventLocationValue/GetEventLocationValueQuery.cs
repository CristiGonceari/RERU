using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetEventLocationValue
{
    public class GetEventLocationValueQuery : IRequest<List<LocationDto>>
    {
        public int EventId { get; set; }
    }
}
