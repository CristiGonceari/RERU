using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Studies;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Studies
{
    public class StudyMappingProfile : Profile
    {
        public StudyMappingProfile()
        {
            CreateMap<StudyDto, Study>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Study, StudyDto>();
        }
    }
}
