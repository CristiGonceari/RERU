using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using RERU.Data.Entities;

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
