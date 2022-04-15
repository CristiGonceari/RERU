using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using RERU.Data.Entities;

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
