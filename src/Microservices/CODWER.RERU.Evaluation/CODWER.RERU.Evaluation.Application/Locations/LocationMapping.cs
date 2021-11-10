using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;

namespace CODWER.RERU.Evaluation.Application.Locations
{
    public class LocationMapping : Profile
    {
        public LocationMapping()
        {
            CreateMap<Location, LocationDto>();

            CreateMap<AddEditLocationDto, Location>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<AddLocationClientDto, LocationClient>();

            CreateMap<LocationClient, LocationClientDto>();
        }
    }
}
