using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;

namespace CODWER.RERU.Identity.Web.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateUserDto, UserProfile>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true));
        }
    }
}
