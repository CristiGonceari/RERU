using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Studies;
using CODWER.RERU.Personal.DataTransferObjects.Studies;

namespace CODWER.RERU.Personal.Application.Studies
{
    public class StudyMappingProfile : Profile
    {
        public StudyMappingProfile()
        {
            CreateMap<StudyDataDto, Study>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Study, StudyDataDto>();
        }
    }
}
