﻿using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfile;
using CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfileByAnonymous;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers.Internal
{
    [Route("internal/api/[controller]")]
    [ApiController]
    public class InternalUserProfileController : BaseController
    {
        [HttpPost]
        public async Task<Unit> CreateUserProfile([FromBody] BaseUserProfile userDto)
        {
            var command = new AddUpdateUserProfileCommand(userDto);

            return await Mediator.Send(command);
        }

        [HttpPost("by-anonymous")]
        public async Task<Unit> InregistrateUserProfile([FromBody] BaseUserProfile userDto)
        {
            var command = new AddUpdateUserProfileByAnonymousCommand(userDto);

            return await Mediator.Send(command);
        }
    }
}
