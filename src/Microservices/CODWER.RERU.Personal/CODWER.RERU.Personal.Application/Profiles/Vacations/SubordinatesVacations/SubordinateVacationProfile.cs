using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations
{
    public class SubordinateVacationProfile : Profile
    {
        public SubordinateVacationProfile()
        {
            CreateMap<Vacation, SubordinateVacationDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(x => x.Contractor.FirstName))
                .ForMember(x => x.ContractorLastName, opts => opts.MapFrom(x => x.Contractor.LastName))
                ;
        }
    }
}
