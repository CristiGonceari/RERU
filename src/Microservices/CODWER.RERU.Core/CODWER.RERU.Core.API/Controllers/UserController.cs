using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove;
using CODWER.RERU.Core.Application.Users.ActivateUser;
using CODWER.RERU.Core.Application.Users.AddUserAvatar;
using CODWER.RERU.Core.Application.Users.BlockUser;
using CODWER.RERU.Core.Application.Users.BulkImportUsers;
using CODWER.RERU.Core.Application.Users.ChangeMyPassword;
using CODWER.RERU.Core.Application.Users.ChangePersonalData;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.DeactivateUser;
using CODWER.RERU.Core.Application.Users.EditUser;
using CODWER.RERU.Core.Application.Users.EditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetPersonalData;
using CODWER.RERU.Core.Application.Users.GetUserDetails;
using CODWER.RERU.Core.Application.Users.RemoveUser;
using CODWER.RERU.Core.Application.Users.ResetUserPassword;
using CODWER.RERU.Core.Application.Users.SetPassword;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet ("{id:int}")]
        public Task<UserDetailsOverviewDto> GetUserDetails ([FromRoute] int id) 
        {
            return Mediator.Send (new GetUserDetailsQuery (id));
        }

        [HttpGet ("{id:int}/for-remove")]
        public Task<UserForRemoveDto> GetUserProfile ([FromRoute] int id) 
        {
            return Mediator.Send (new GetUserForRemoveQuery (id));
        }

        [HttpGet ("{id:int}/personal-details/edit")]
        public Task<EditUserPersonalDetailsDto> GetEditPersonalDetails ([FromRoute] int id) 
        {
            return Mediator.Send (new GetEditUserPersonalDetailsQuery (id));
        }

        [HttpPatch ("personal-details")]
        public Task<int> UpdateUserPersonalDetails ([FromBody] EditUserPersonalDetailsCommand command) 
        {
            return Mediator.Send(command);
        }

        [HttpGet ("get-personal-data")]
        public Task<UserPersonalDataDto> GetPersonalData () 
        {
            return Mediator.Send (new GetPersonalDataQuery ());
        }

        [HttpPost]
        public Task<int> CreateUser ([FromBody] CreateUserCommand command) 
        {
            return Mediator.Send(command);
        }

        [HttpPost("avatar")]
        public Task<string> AddAvatar([FromForm] AddUserAvatarCommand command)
        {
            return Mediator.Send(command);
        }

        [HttpPut("excel-import")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> ImportFromExcelFile([FromForm] BulkExcelImport dto)
        {
            var command = new BulkImportUsersCommand { Data = dto };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut]
        public Task EditUser ([FromBody] EditUserDto user) 
        {
            return Mediator.Send (new EditUserCommand (user));
        }

        [HttpPut ("change-my-password")]
        public Task ChangeMyPassword ([FromBody] PasswordDto password) 
        {
            return Mediator.Send (new ChangeMyPasswordCommand (password));
        }

        [HttpPut ("set-password")]
        public Task SetPassword ([FromBody] SetPasswordDto password) 
        {
            return Mediator.Send (new SetPasswordCommand (password));
        }

        [HttpPut ("change-my-data")]
        public Task ChangePersonalData ([FromBody] UserPersonalDataDto user) 
        {
            return Mediator.Send (new ChangePersonalDataCommand (user));
        }

        [HttpPut ("{id}/password-reset")]
        public Task ResetPassword ([FromRoute] int id) 
        {
            return Mediator.Send (new ResetUserPasswordCommand (id));
        }

        [HttpDelete ("{id}/remove-user")]
        public Task RemovePassword ([FromRoute] int id)
        {
            return Mediator.Send (new RemoveUserCommand (id));
        }

        [HttpPut ("{id}/block-unblock-user")]
        public Task BlockUser ([FromRoute] int id) 
        {
            return Mediator.Send (new BlockUnblockUserCommand (id));
        }

        [HttpPut ("{id}/activate")]
        public Task ActivateUser ([FromRoute] int id) 
        {
            return Mediator.Send (new ActivateUserCommand (id));
        }

        [HttpPut ("{id}/deactivate")]
        public Task DeactivateUser ([FromRoute] int id) 
        {
            return Mediator.Send (new DeactivateUserCommand (id));
        }
    }
}