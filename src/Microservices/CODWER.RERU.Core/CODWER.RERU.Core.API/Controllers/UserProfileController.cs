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
using CODWER.RERU.Core.Application.UserProfiles.PrintUserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.EnumConverters;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using RERU.Data.Entities.Enums;
using CODWER.RERU.Core.Application.UserProfiles.GetCandidateProfile;

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

        [HttpGet("candidate-profile/{id:int}")]
        public Task<CandidateProfileDto> GetCandidateProfile([FromRoute] int id)
        {
            return Mediator.Send(new GetCandidateProfileQuery(id));
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

        [HttpGet("user-status/select-values")]
        public async Task<List<SelectItem>> GetUserEnum()
        {
            var items = EnumConverter<UserStatusEnum>.SelectValues;

            return items;
        }

        [HttpGet("access-mode/select-values")]
        public async Task<List<SelectItem>> GetAccessModeEnum()
        {
            var items = EnumConverter<AccessModeEnum>.SelectValues;

            return items;
        }

        [HttpGet("access/{id}")]
        public Task<List<UserModuleAccessDto>> GetUserModuleAccessDetails([FromRoute] int id)
        {
            var getModuleDetails = new GetUserModuleAccessQuery(id);
            return Mediator.Send(getModuleDetails);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserProfiles([FromBody] PrintUserProfilesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}