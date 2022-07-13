using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas
{
    public class UserProfileGeneralDataMappingProfile : Profile
    {
        public UserProfileGeneralDataMappingProfile()
        {
            CreateMap<UserProfileGeneralDataDto, UserProfileGeneralData>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<UserProfileGeneralData, UserProfileGeneralDataDto>();
        }
    }
}
