using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Bulletins
{
    public class BulletinMappingProfile : Profile
    {
        public BulletinMappingProfile()
        {
            CreateMap<BulletinsDataDto, Bulletin>()
                .ForMember(x=>x.Id, opts => opts.Ignore());

            CreateMap<Bulletin, BulletinsDataDto>();
        }
    }
}
