using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.UserProfiles.GetCurrentUser;
using CODWER.RERU.Evaluation360.Application.UserProfiles.GetUserProfiles;
using CODWER.RERU.Evaluation360.DataTransferObjects.UserProfile;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetUsers([FromQuery] GetUserProfilesQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("current-user")]
        public async Task<UserProfileDto> GetCurrentUser()
        {
            var query = new GetCurrentUserQuery();

            return await Sender.Send(query);
        }
    }    
}