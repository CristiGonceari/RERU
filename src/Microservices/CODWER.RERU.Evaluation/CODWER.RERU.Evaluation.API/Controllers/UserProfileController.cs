using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.UserProfiles.GetCurrentUserProfile;
using CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfile;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        [HttpGet("my")]
        public async Task<UserProfileDto> GetCurrentUserProfile()
        {
            return await Mediator.Send(new GetCurrentUserProfileQuery());
        }

        [HttpGet("{id}")]
        public async Task<UserProfileDto> GetUserProfile([FromRoute] int id)
        {
            var query = new GetUserProfileQuery { Id = id };
            return await Mediator.Send(query);
        }
    }
}
