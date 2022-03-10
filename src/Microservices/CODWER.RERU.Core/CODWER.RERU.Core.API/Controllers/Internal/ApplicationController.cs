using CVU.ERP.Module.Application.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers.Internal
{
    [ApiController]
    [Route ("internal/api/[controller]")]
    public class ApplicationController : BaseController
    {
        private readonly IEvaluationClient _evaluationClient;
        public ApplicationController (IMediator mediator, IEvaluationClient evaluationClient) : base (mediator)
        {
            _evaluationClient = evaluationClient;
        }

        [HttpGet]
        public async Task<int> GetTestId()
        {
            return await _evaluationClient.GetTestIdToStartTest();
        }

        // [HttpPost ("user")]
        // public async Task<ApplicationUser> CreateUser ([FromBody] CreateUserDto userDto) 
        // {
        //     var command = new CreateUserCommand (userDto);

        //     var id = await Mediator.Send (command);

        //     return await Mediator.Send (new GetApplicationUserQuery(id));
        // }

        // [HttpPatch ("user/{userProfileId}")]
        // public async Task<Unit> ResetUserPassword ([FromRoute] int userProfileId) 
        // {
        //     var command = new ResetUserPasswordCommand (userProfileId);

        //     return await Mediator.Send (command);
        // }
    }
}