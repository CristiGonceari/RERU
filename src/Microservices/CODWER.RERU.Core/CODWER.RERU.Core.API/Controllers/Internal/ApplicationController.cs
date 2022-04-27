using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers.Internal
{
    [ApiController]
    [Route ("/api/[controller]/internal")]
    public class ApplicationController : BaseController
    {
        public ApplicationController (IMediator mediator) : base (mediator)
        {
        }

        //[HttpGet]
        //public async Task<TestDataDto> GetTestId()
        //{
        //    return await Mediator.Send(new GetTestIdFastStartQuery());
        //}

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