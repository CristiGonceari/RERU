using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation360.API.Config;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation360.DataTransferObjects.UserProfile;
using CODWER.RERU.Evaluation360.Application.UserProfiles.GetUserProfiles;

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
    }    
}