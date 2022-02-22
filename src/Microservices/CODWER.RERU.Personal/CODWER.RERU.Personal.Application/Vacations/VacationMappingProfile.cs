using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;

namespace CODWER.RERU.Personal.Application.Vacations
{
    public class VacationMappingProfile : Profile
    {
        public VacationMappingProfile()
        {
            CreateMap<AddEditVacationDto, Vacation>()
                .ForMember(x => x.ContractorId, opts => opts.Ignore())
                .ForMember(x => x.CountDays, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore())
                .ForMember(x => x.VacationRequestId, opts => opts.Ignore())
                .ForMember(x => x.VacationOrderId, opts => opts.Ignore())
                .ForMember(x => x.Status, opts => opts.MapFrom(op => StageStatusEnum.New))
            ;


            CreateMap<Vacation, VacationDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()))
                .ForMember(x => x.VacationTypeName, opts => opts.MapFrom(op => op.VacationType))
                .ForMember(x => x.VacationTypeId, opts => opts.MapFrom(op => op.VacationType))
                ;
        }
    }
}
