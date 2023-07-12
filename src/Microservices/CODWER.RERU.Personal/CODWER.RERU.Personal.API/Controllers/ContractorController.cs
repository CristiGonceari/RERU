using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Contractors.AddContractor;
using CODWER.RERU.Personal.Application.Contractors.AddUpdateContractorPermissions;
using CODWER.RERU.Personal.Application.Contractors.EditContractorAvatar;
using CODWER.RERU.Personal.Application.Contractors.GetAllContractorDatas;
using CODWER.RERU.Personal.Application.Contractors.GetCandidateContractorSteps;
using CODWER.RERU.Personal.Application.Contractors.GetContractor;
using CODWER.RERU.Personal.Application.Contractors.GetContractorAvatar;
using CODWER.RERU.Personal.Application.Contractors.GetContractorFiles;
using CODWER.RERU.Personal.Application.Contractors.GetContractorPermissions;
using CODWER.RERU.Personal.Application.Contractors.GetContractors;
using CODWER.RERU.Personal.Application.Contractors.RemoveContractor;
using CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar;
using CODWER.RERU.Personal.Application.Contractors.UpdateContractor;
using CODWER.RERU.Personal.Application.Contractors.UpdateContractorName;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ContractorDto>> GetContractors([FromQuery] GetContractorsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ContractorDetailsDto> GetContractor([FromRoute] int id)
        {
            var query = new GetContractorQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("allDatas/{id}")]
        public async Task<string> GetAllContractorData([FromRoute] int id)
        {
            var query = new GetAllContractorDataQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}/avatar")]
        public async Task<ContractorAvatarDetailsDto> GetContractorAvatar([FromRoute] int id)
        {
            var query = new GetContractorAvatarQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateContractor([FromBody] AddContractorCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateContractor([FromBody] UpdateContractorCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch("name")]
        public async Task<Unit> UpdateContractorName([FromBody] UpdateContractorNameCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveContractor([FromRoute] int id)
        {
            var command = new RemoveContractorCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpGet("files")]
        public async Task<PaginatedModel<FileNameDto>> GetContractor([FromQuery] GetContractorFilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPatch("avatar")]
        public async Task<Unit> SetContractorAvatar([FromForm] SetContractorAvatarCommand command)
        {
            return await Mediator.Send(command); 
        }

        [HttpPatch("edit-avatar")]
        public async Task<string> EditContractorAvatar([FromForm] EditContractorAvatarCommand command)
        {
            return await Mediator.Send(command); 
        }

        [HttpPut("contractor-permissions")]
        public async Task<Unit> PermissionsUpdate([FromBody] ContractorLocalPermissionsDto dto)
        {
            var command = new AddUpdateContractorPermissionsCommand(dto);

            return await Mediator.Send(command);
        }

        [HttpGet("contractor-permissions/{contractorId}")]
        public async Task<ContractorLocalPermissionsDto> GetPermission([FromRoute] int contractorId)
        {
            var query = new GetContractorPermissionsQuery
            {
                ContractorId = contractorId
            };

            return await Mediator.Send(query);
        }

        [HttpGet("candidate-registration-steps/{contractorId}")]
        public async Task<CandidateContractorStepsDto> GetContractorCandidateSteps([FromRoute] int contractorId)
        {
            var query = new GetCandidateContractorStepsQuery()
            {
                ContractorId = contractorId
            };

            return await Mediator.Send(query);
        }
    }
}
