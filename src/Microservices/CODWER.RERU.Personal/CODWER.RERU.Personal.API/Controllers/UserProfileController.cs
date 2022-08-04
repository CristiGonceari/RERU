using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor;
using CODWER.RERU.Personal.Application.UserProfiles.GetAvailableModuleRoles;
using CODWER.RERU.Personal.Application.UserProfiles.GetOrCreateUserProfile;
using CODWER.RERU.Personal.Application.UserProfiles.ResetContractorPassword;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        [HttpPost]
        public async Task<int> CreateUserForContractor([FromBody] CreateUserForContractorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task<Unit> ResetContractorPassword([FromRoute] int id)
        {
            return await Mediator.Send(new ResetContractorPasswordCommand
            {
                ContractorId = id
            });
        }

        [HttpGet]
        public async Task<UserProfileDto> GetOrCreateUserProfile()
        {
            return await Mediator.Send(new GetOrCreateUserProfileCommand());
        }

        [HttpGet("module-roles")]
        public async Task<List<ModuleRolesDto>> GetModuleRoles()
        {
            return await Mediator.Send(new GetAvailableModuleRolesQuery());
        }
    }
}
