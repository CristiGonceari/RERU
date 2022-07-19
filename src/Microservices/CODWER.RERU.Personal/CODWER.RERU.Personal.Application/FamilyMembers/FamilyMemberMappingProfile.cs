using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;

namespace CODWER.RERU.Personal.Application.FamilyMembers
{
    public class FamilyMemberMappingProfile : Profile
    {
        public FamilyMemberMappingProfile()
        {
            CreateMap<AddEditFamilyMemberDto, FamilyMember>()
                .ForMember(x=>x.Id, opts=>opts.Ignore());

            CreateMap<FamilyMember, FamilyMemberDto>()
                .ForMember(x => x.RelationName, opts => opts.MapFrom(op => op.Relation.Name));
        }
    }
}
