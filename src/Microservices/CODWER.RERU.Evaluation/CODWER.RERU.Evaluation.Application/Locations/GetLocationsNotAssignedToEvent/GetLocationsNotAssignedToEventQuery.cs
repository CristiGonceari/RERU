using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationsNotAssignedToEvent
{
    public class GetLocationsNotAssignedToEventQuery : IRequest<List<LocationDto>>
    {
        public string Keyword { get; set; }
        public int EventId { get; set; }
    }
}
