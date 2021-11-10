using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationByComputer
{
    public class GetLocationByComputerQuery : IRequest<LocationDto>
    {
        public string Keyword { get; set; }
    }
}
