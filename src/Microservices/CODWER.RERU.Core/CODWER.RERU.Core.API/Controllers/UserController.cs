using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove;
using CODWER.RERU.Core.Application.Users.ActivateUser;
using CODWER.RERU.Core.Application.Users.BlockUser;
using CODWER.RERU.Core.Application.Users.ChangeMyPassword;
using CODWER.RERU.Core.Application.Users.ChangePersonalData;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.DeactivateUser;
using CODWER.RERU.Core.Application.Users.EditUser;
using CODWER.RERU.Core.Application.Users.EditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetPersonalData;
using CODWER.RERU.Core.Application.Users.GetUserDetails;
using CODWER.RERU.Core.Application.Users.GetUsers;
using CODWER.RERU.Core.Application.Users.RemoveUser;
using CODWER.RERU.Core.Application.Users.ResetPasswordByEmail;
using CODWER.RERU.Core.Application.Users.ResetPasswordByEmailToken;
using CODWER.RERU.Core.Application.Users.ResetUserPassword;
using CODWER.RERU.Core.Application.Users.SetPassword;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class UserController : BaseController {
        public UserController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<UserDto>> GetUsers ([FromQuery] GetUsersQuery query) {
            var mediatorResponse = await Mediator.Send (query);
            return mediatorResponse;
        }

        [HttpGet ("{id:int}")]
        public Task<UserDetailsOverviewDto> GetUserDetails ([FromRoute] int id) {
            return Mediator.Send (new GetUserDetailsQuery (id));
        }

        [HttpGet ("{id:int}/for-remove")]
        public Task<UserForRemoveDto> GetUserProfile ([FromRoute] int id) {
            return Mediator.Send (new GetUserForRemoveQuery (id));
        }

        [HttpGet ("{id:int}/personal-details/edit")]
        public Task<EditUserPersonalDetailsDto> GetEditPersonalDetails ([FromRoute] int id) {
            return Mediator.Send (new GetEditUserPersonalDetailsQuery (id));
        }

        [HttpPut ("personal-details")]
        public Task UpdateUserPersonalDetails ([FromBody] EditUserPersonalDetailsDto dto) {
            return Mediator.Send (new EditUserPersonalDetailsCommand (dto));
        }

        [HttpGet ("get-personal-data")]
        public Task<UserPersonalDataDto> GetPersonalData () {
            return Mediator.Send (new GetPersonalDataQuery ());
        }

        [HttpPost]
        public Task<int> CreateUser ([FromBody] CVU.ERP.Common.DataTransferObjects.Users.CreateUserDto user) {
            return Mediator.Send (new CreateUserCommand (user));
        }

        [HttpPut]
        public Task EditUser ([FromBody] EditUserDto user) {
            return Mediator.Send (new EditUserCommand (user));
        }

        [HttpPut ("change-my-password")]
        public Task ChangeMyPassword ([FromBody] PasswordDto password) {
            return Mediator.Send (new ChangeMyPasswordCommand (password));
        }

        [HttpPut ("set-password")]
        public Task SetPassword ([FromBody] SetPasswordDto password) {
            return Mediator.Send (new SetPasswordCommand (password));
        }

        [HttpPut ("change-my-data")]
        public Task ChangePersonalData ([FromBody] UserPersonalDataDto user) {
            return Mediator.Send (new ChangePersonalDataCommand (user));
        }

        [HttpPut ("{id}/password-reset")]
        public Task ResetPassword ([FromRoute] int id) {
            return Mediator.Send (new ResetUserPasswordCommand (id));
        }

        [HttpDelete ("{id}/remove-user")]
        public Task RemovePassword ([FromRoute] int id) {
            return Mediator.Send (new RemoveUserCommand (id));
        }

        [HttpPut ("{id}/block-unblock-user")]
        public Task BlockUser ([FromRoute] int id) {
            return Mediator.Send (new BlockUnblockUserCommand (id));
        }

        [HttpPut ("{id}/activate")]
        public Task ActivateUser ([FromRoute] int id) {
            return Mediator.Send (new ActivateUserCommand (id));
        }

        [HttpPut ("{id}/deactivate")]
        public Task DeactivateUser ([FromRoute] int id) {
            return Mediator.Send (new DeactivateUserCommand (id));
        }

        [HttpPut ("reset-password-by-email")]
        public Task ResetPasswordByEmail ([FromQuery] string email) {
            return Mediator.Send (new ResetPasswordByEmailCommand (email));
        }

        [HttpPut ("reset-password-by-email-token")]
        public Task ResetPasswordByEmailToken ([FromBody] EmailConfirmationDto token) {
            return Mediator.Send (new ResetPasswordByEmailTokenCommand (token));
        }
    }
}