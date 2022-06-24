using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Bulletins
{
    public class BulletinMappingProfile : Profile
    {
        public BulletinMappingProfile()
        {
            CreateMap<BulletinDto, Bulletin>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Bulletin, BulletinDto>();
        }
    }
}
