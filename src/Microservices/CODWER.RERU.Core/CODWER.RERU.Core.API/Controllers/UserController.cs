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
using CODWER.RERU.Core.Application.Users.EditCandidate;
using CODWER.RERU.Core.Application.Users.EditUser;
using CODWER.RERU.Core.Application.Users.EditUserFromColaborator;
using CODWER.RERU.Core.Application.Users.EditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.ExportUserTestsList;
using CODWER.RERU.Core.Application.Users.GetEditUserPersonalDetails;
using CODWER.RERU.Core.Application.Users.GetPersonalData;
using CODWER.RERU.Core.Application.Users.GetUserDetails;
using CODWER.RERU.Core.Application.Users.RemoveUser;
using CODWER.RERU.Core.Application.Users.ResetUserPassword;
using CODWER.RERU.Core.Application.Users.SetPassword;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CVU.ERP.Module.Application.ImportProcesses;
using CVU.ERP.Module.Application.ImportProcesses.GetImportProcess;
using CVU.ERP.Module.Application.ImportProcesses.GetImportResult;
using CVU.ERP.Module.Application.ImportProcesses.StartImportProcess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly AppDbContext _appDbContext;
        public UserController(IMediator mediator, AppDbContext appDbContext) : base(mediator)
        {
            _appDbContext = appDbContext;
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
        public async Task<int> StartAddProcess([FromBody] StartImportProcessCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("process/{id}")]
        public async Task<ProcessDataDto> GetImportProcess([FromRoute] int id)
        {
            var query = new GetImportProcessQuery() { ProcessId = id };

            return await Mediator.Send(query);
        }

        [HttpGet("process-result/{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile([FromRoute] string fileId)
        {
            var query = new GetImportResultQuery { FileId = fileId };

            var result = await Mediator.Send(query);
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

        [HttpGet("{id}/export-excel")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetAllTImeSheetTableForExport([FromRoute] int id)
        {
            var result = await Mediator.Send(new ExportUserTestsListCommand(id));
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}