using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.References
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
            
            CreateMap<EmployeeFunction, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.ColaboratorId.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name));

            CreateMap<MaterialStatusType, SelectValue>()
                  .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.Id))
                  .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name))
                  .ForMember(x => x.TranslateId, opts => opts.MapFrom(sv => sv.TranslateId));
        }
    }
}
