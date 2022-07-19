using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Badges;

namespace CODWER.RERU.Personal.Application.Badges
{
    public class BadgeMappingProfile : Profile
    {
        public BadgeMappingProfile()
        {
            CreateMap<AddEditBadgeDto, Badge>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore());

            CreateMap<Badge, BadgeDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()));
        }
    }
}
