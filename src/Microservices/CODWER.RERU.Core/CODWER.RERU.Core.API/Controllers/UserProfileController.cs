using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Modules.GetUserModuleAccess;
using CODWER.RERU.Core.Application.UserProfileModuleRoles.GetCreateUserProdileModuleRoleWithDetails;
using CODWER.RERU.Core.Application.UserProfileModuleRoles.RemoveModuleAccess;
using CODWER.RERU.Core.Application.UserProfileModuleRoles.UpdateUserProfileModuleAccess;
using CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles;
using CODWER.RERU.Core.Application.UserProfiles.GetUserProfile;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CODWER.RERU.Core.DataTransferObjects.UserProfileModuleRoles;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : BaseController
    {
        public UserProfileController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id:int}")]
        public Task<UserProfileDto> GetUserProfile([FromRoute] int id)
        {
            return Mediator.Send(new GetUserProfileQuery(id));
        }

        [HttpGet]
        public async Task<PaginatedModel<UserProfileDto>> GetUserProfiles([FromQuery] GetAllUserProfilesQuery query)
        {
            var mediatorResponse = await Mediator.Send(query);
            return mediatorResponse;
        }

        [HttpGet("{userId}/module-access/{moduleId}")]
        public Task<AddEditModuleAccessWithDetailsDto> GetUserProfileModuleRoleWithDetails([FromRoute] int userId, [FromRoute] int moduleId)
        {
            return Mediator.Send(new GetAddEditModuleAccessWithDetailsQuery(userId, moduleId));
        }

        [HttpPut("module-access")]
        public Task GetUserProfileModuleRoleWithDetails([FromBody] AddEditModuleAccessDto dto)
        {
            return Mediator.Send(new UpdateUserProfileModuleAccessCommand(dto));
        }

        [HttpDelete("{userId}/module-access/{moduleId}")]
        public Task RemoveModuleAccess([FromRoute] int userId, [FromRoute] int moduleId)
        {
            return Mediator.Send(new RemoveModuleAccessCommand(userId, moduleId));
        }

        [HttpGet("access/{id}")]
        public Task<List<UserModuleAccessDto>> GetUserModuleAccessDetails([FromRoute] int id)
        {
            var getModuleDetails = new GetUserModuleAccessQuery(id);
            return Mediator.Send(getModuleDetails);
        }
    }
}