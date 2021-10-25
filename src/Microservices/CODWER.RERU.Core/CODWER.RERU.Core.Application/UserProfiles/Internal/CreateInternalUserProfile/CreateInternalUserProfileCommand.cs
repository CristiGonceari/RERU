using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.Internal.CreateInternalUserProfile
{
    public class CreateInternalUserProfileCommand : IRequest<ApplicationUser>
    {
        public CreateInternalUserProfileCommand(InternalUserProfileCreate dto)
        {
            Data = dto;
        }
        public InternalUserProfileCreate Data { set; get; }
    }
}