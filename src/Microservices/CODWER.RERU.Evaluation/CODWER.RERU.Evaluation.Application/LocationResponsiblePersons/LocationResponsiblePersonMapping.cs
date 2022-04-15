using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons
{
    public class LocationResponsiblePersonMapping : Profile
    {
        public LocationResponsiblePersonMapping()
        {
            CreateMap<AddLocationPersonDto, LocationResponsiblePerson>();
        }
    }
}
