using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;

namespace CODWER.RERU.Personal.Application.Penalizations
{
    public class PenalizationMappingProfile : Profile
    {
        public PenalizationMappingProfile()
        {
            CreateMap<AddEditPenalizationDto, Penalization>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore())
                ;

            CreateMap<Penalization, PenalizationDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()))
                ;
        }
    }
}
