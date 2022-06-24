using CODWER.RERU.Core.Application.MaterialStatuses.AddMaterialStatus;
using CODWER.RERU.Core.Application.MaterialStatuses.GetUserProfilesMaterialStatus;
using CODWER.RERU.Core.Application.MaterialStatuses.UpdateMaterialStatus;
using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialStatusController : BaseController
    {
        public MaterialStatusController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<MaterialStatusDto> GetMaterialStatus([FromRoute] int id)
        {
            var query = new GetUserProfilesMaterialStatusQuery { UserProfileId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateMaterialStatus([FromBody] AddEditMaterialStatusDto data)
        {
            var command = new AddMaterialStatusCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateMaterialStatus([FromBody] AddEditMaterialStatusDto data)
        {
            var command = new UpdateMaterialStatusCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
