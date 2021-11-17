using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;

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
