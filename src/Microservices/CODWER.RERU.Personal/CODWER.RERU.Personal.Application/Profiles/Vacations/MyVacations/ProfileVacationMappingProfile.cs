using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations
{
    public class ProfileVacationMappingProfile : Profile
    {
        public ProfileVacationMappingProfile()
        {
            CreateMap<AddMyVacationDto, Vacation>()
                .ForMember(x => x.ContractorId, opts => opts.Ignore())
                .ForMember(x => x.CountDays, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore())
                .ForMember(x => x.VacationRequestId, opts => opts.Ignore())
                .ForMember(x => x.VacationOrderId, opts => opts.Ignore())
                .ForMember(x => x.Status, opts => opts.MapFrom(op => StageStatusEnum.New));

            CreateMap<Vacation, MyVacationDto>();
        }
    }
}
