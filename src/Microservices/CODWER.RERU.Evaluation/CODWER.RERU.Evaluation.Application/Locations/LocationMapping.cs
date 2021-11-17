﻿using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;

namespace CODWER.RERU.Evaluation.Application.Locations
{
    public class LocationResponsiblePersonMapping : Profile
    {
        public LocationResponsiblePersonMapping()
        {
            CreateMap<Location, LocationDto>();

            CreateMap<AddEditLocationDto, Location>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
