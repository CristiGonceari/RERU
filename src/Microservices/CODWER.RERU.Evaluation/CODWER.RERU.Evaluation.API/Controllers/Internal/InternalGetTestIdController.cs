using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation.API.Controllers.Internal
{
    [Route("internal/api/[controller]")]
    [ApiController]
    public class InternalGetTestIdController : BaseController
    {
        [HttpGet("{coreUserProfileId}")]
        public async Task<int> GetUserTestId([FromRoute] string coreUserProfileId)
        {
            return await Mediator.Send(new GetTestIdForFastStartQuery { CoreUserProfileId = coreUserProfileId });
        }
    }
}