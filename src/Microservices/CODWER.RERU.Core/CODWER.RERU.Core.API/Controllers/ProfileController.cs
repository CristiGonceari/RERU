using System.Threading.Tasks;
using CODWER.RERU.Core.Application.UserProfiles.GetCandidateRegistrationSteps;
using CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview;
using CODWER.RERU.Core.Application.UserProfiles.PrintUserProfileModules;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfileController : BaseController 
    {
        public ProfileController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public async Task<UserProfileOverviewDto> GetProfileOverview ()
        {
            return await Mediator.Send (new GetMyUserProfileOverviewQuery ());
        }

        [HttpGet("candidate-registration-steps")]
        public async Task<CandidateRegistrationStepsDto> GetCandidateRegistrationSteps()
        {
            return await Mediator.Send(new GetCandidateRegistrationStepsQuery());
        }


        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintCurrentUserModules([FromBody] PrintUserProfileModulesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

    }
}
