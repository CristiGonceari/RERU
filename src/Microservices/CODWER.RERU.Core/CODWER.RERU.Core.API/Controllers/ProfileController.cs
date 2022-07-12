using System.Threading.Tasks;
using CODWER.RERU.Core.Application.UserProfiles.GetCandidateRegistrationSteps;
using CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
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
    }
}
