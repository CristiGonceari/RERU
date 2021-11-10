using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationByComputer
{
    public class GetLocationByComputerQuery : IRequest<LocationDto>
    {
        public string Keyword { get; set; }
    }
}
