using CODWER.RERU.Core.Application.UserProfiles.GetUserForRemove;
using CODWER.RERU.Core.Application.Users.ActivateUser;
using CODWER.RERU.Core.Application.Users.AddUserAvatar;
using CODWER.RERU.Core.Application.Users.BlockUser;
using CODWER.RERU.Core.Application.Users.BulkImportUsers;
using CODWER.RERU.Core.Application.Users.ChangeMyPassword;
using CODWER.RERU.Core.Application.Users.ChangePersonalData;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.DeactivateUser;
using CODWER.RERU.Core.Application.Users.EditCandidate;
using CODWER.RERU.Core.Application.Users.EditUser;
using CODWER.RERU.Core.Application.Users.EditUserFromColaborator;
using CODWER.RERU.Core.Application.Users.EditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetPersonalData;
using CODWER.RERU.Core.Application.Users.GetUserDetails;
using CODWER.RERU.Core.Application.Users.GetUserDetailsByEmail;
using CODWER.RERU.Core.Application.Users.RemoveUser;
using CODWER.RERU.Core.Application.Users.ResetUserPassword;
using CODWER.RERU.Core.Application.Users.ResetUserPasswordByEmailCode;
using CODWER.RERU.Core.Application.Users.SendEmailVerificationCode;
using CODWER.RERU.Core.Application.Users.SetPassword;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CVU.ERP.Module.Application.ImportProcessServices;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IImportProcessService _importProcessService;

        public UserController(IMediator mediator,  IImportProcessService importProcessService) : base(mediator)
        {
            _importProcessService = importProcessService;
        }

        [HttpGet("token")]
        public string GetTokenId()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            var token_id = string.Empty;
            if (!string.IsNullOrEmpty(token))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadJwtToken(token);
                token_id = jwtToken.Id;
            }

            return token_id;
        }

        [HttpGet ("{id:int}")]
        public Task<UserDetailsOverviewDto> GetUserDetails ([FromRoute] int id) 
        {
            return Mediator.Send (new GetUserDetailsQuery (id));
        }

        [HttpGet("personal-details/{email}")]
        public Task<UserDetailsOverviewDto> GetUserPersonalDetails([FromRoute] string email)
        {
            return Mediator.Send(new GetUserDetailsByEmailQuery(email));
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

        [HttpPatch("edit-candidate-details")]
        public Task<int> UpdateCandidateDetails([FromBody] EditCandidateCommand command)
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

        [HttpPatch("from-colaborator")]
        public Task<int> UpdateUserFromColaborator([FromBody] EditUserFromColaboratorCommand command)
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
        public async Task<IActionResult> ImportFromExcelFile([FromForm] BulkImportUsersCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPost]
        [Route("process")]
        public async Task<int> StartAddProcess([FromBody] StartProcessDto startProcessDto)
        {
            return await _importProcessService.StartImportProcess(startProcessDto);
        }

        [HttpGet("process/{id}")]
        public async Task<ProcessDataDto> GetImportProcess([FromRoute] int id)
        {
            return await _importProcessService.GetImportProcess(id);
        }

        [HttpGet("process-result/{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile([FromRoute] string fileId)
        {
            var result = await _importProcessService.GetImportResult(fileId);
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

        [HttpPost("verify-code")]
        public Task<int> VerifyEmail([FromBody] SendEmailVerificationCodeCommand command)
        {
            return Mediator.Send(command);
        }

        [HttpPatch("reset-user-password")]
        public Task<Unit> ResetUserPassword([FromBody] ResetUserPasswordByEmailCodeCommand command)
        {
            return Mediator.Send(command);
        }
    }
}