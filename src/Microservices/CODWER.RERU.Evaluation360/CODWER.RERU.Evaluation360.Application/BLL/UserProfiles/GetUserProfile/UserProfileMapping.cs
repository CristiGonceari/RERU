using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.UserProfile;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation360.Application.BLL.UserProfiles.GetUserProfile
{
    public class UserProfileMapping: Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(x => x.UserStatusEnum, opts => opts.MapFrom(src => src.DepartmentColaboratorId == null && src.RoleColaboratorId == null ? UserStatusEnum.Candidate : UserStatusEnum.Employee))
                .ForMember(x => x.DepartmentColaboratorId, opts => opts.MapFrom(src => src.DepartmentColaboratorId))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(src => src.Department.Name))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(x => x.RoleColaboratorId, opts => opts.MapFrom(src => src.RoleColaboratorId));

        }
    }
}