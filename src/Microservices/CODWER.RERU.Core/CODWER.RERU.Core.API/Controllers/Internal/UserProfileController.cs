using System.Threading.Tasks;
using CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUser;
using CODWER.RERU.Core.Application.ApplicationUsers.Internal.GetInternalApplicationUserByIdentity;
using CODWER.RERU.Core.Application.UserProfiles.Internal.CreateInternalUserProfile;
using CODWER.RERU.Core.Application.UserProfiles.Internal.DeactivateUserProfile;
using CODWER.RERU.Core.Application.UserProfiles.Internal.ResetPassword;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers.Internal
{
    [ApiController]
    [Route("internal/api/user-profile")]
    public class UserProfileController : BaseController
    {
        public UserProfileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{identityProvider}/{id}")]
        public async Task<ApplicationUser> GetApplicationUserByIdentity([FromRoute] string identityProvider, [FromRoute] string id)
        {
            return await Mediator.Send(new GetInternalApplicationUserByIdentityQuery(id, identityProvider));
        }

        [HttpGet("{id}")]
        public async Task<ApplicationUser> GetApplicationUser([FromRoute] int id)
        {
            return await Mediator.Send(new GetInternalApplicationUserQuery(id));
        }

        [HttpPost]
        public async Task<ApplicationUser> CreateUserProfile([FromBody] InternalUserProfileCreate userDto)
        {
            var command = new CreateInternalUserProfileCommand(userDto);

            return await Mediator.Send(command);
        }

        [HttpPatch("{userProfileId}/reset-password")]
        public async Task ResetUserPassword([FromRoute] int userProfileId)
        {
            var command = new InternalResetPasswordCommand(userProfileId);
            await Mediator.Send(command);
        }

        [HttpPatch("{userProfileId}/deactivate")]
        public async Task Deactivate([FromRoute] int userProfileId)
        {
            var command = new InternalDeactivateUserProfileCommand(userProfileId);
            await Mediator.Send(command);
        }
    }
}