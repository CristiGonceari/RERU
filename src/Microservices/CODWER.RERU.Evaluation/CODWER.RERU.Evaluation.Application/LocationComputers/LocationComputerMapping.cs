using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;

namespace CODWER.RERU.Evaluation.Application.LocationComputers
{
    public class LocationComputerMapping : Profile
    {
        public LocationComputerMapping()
        {
            CreateMap<AddLocationClientDto, LocationClient>();

            CreateMap<LocationClient, LocationClientDto>();
        }
        
    }
}
