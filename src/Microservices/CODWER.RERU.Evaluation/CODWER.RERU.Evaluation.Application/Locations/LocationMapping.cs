using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Locations
{
    public class LocationResponsiblePersonMapping : Profile
    {
        public LocationResponsiblePersonMapping()
        {
            CreateMap<Location, LocationDto>();

            CreateMap<AddEditLocationDto, Location>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Location, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Address));
        }
    }
}
