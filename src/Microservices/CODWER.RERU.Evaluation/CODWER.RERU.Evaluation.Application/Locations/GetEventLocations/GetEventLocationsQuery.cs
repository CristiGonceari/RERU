using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Locations.GetEventLocations
{
    public class GetEventLocationsQuery : IRequest<List<LocationDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
