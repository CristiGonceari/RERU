using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.References
{
    public class ReferenceMappingProfile : Profile
    {
        public ReferenceMappingProfile()
        {
            CreateMap<CandidateNationality, SelectValue>()
                .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.NationalityName))
                .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));

            CreateMap<CandidateCitizenship, SelectValue>()
               .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
               .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.CitizenshipName))
               .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));

            CreateMap<StudyType, SelectValue>()
               .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
               .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name))
               .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));

            CreateMap<ModernLanguage, SelectValue>()
               .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
               .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name))
               .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));

            CreateMap<MaterialStatusType, SelectValue>()
              .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
              .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name))
              .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));
        }
    }
}
