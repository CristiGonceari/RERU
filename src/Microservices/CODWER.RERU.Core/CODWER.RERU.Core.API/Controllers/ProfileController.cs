using System.Threading.Tasks;
using CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview;
using CODWER.RERU.Core.Application.UserProfiles.RemoveAvatar;
using CODWER.RERU.Core.Application.UserProfiles.UploadAvatar;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfileController : BaseController {
        public ProfileController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public async Task<UserProfileOverviewDto> GetProfileOverview () {
            return await Mediator.Send (new GetMyUserProfileOverviewQuery ());
        }

        [HttpPost("avatar")]
        public async Task UploadAvatar(IFormFile file)
        {
            await Mediator.Send(new UploadAvatarCommand(file));
        }

        [HttpDelete("remove-avatar")]
        public Task RemoveAvatar()
        {
            return Mediator.Send(new RemoveAvatarCommand());
        }

    }
}
