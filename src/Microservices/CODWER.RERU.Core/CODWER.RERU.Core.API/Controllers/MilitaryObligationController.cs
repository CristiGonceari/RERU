using CODWER.RERU.Core.Application.MilitaryObligations.AddMilitaryObligation;
using CODWER.RERU.Core.Application.MilitaryObligations.BulkAddEditMilitaryObligations;
using CODWER.RERU.Core.Application.MilitaryObligations.GetUserProfileMilitaryObligations;
using CODWER.RERU.Core.Application.MilitaryObligations.RemoveMilitaryObligation;
using CODWER.RERU.Core.Application.MilitaryObligations.UpdateMilitaryObligation;
using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilitaryObligationController : BaseController
    {
        public MilitaryObligationController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<MilitaryObligationDto>> GetMilitaryObligation([FromQuery] GetUserProfileMilitaryObligationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateMilitaryObligation([FromBody] MilitaryObligationDto data)
        {
            var command = new AddMilitaryObligationCommand(data)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("bulk-import")]
        public async Task<Unit> AddEditMilitaryObligation([FromBody] List<MilitaryObligationDto> list)
        {
            var command = new BulkAddEditMilitaryObligationsCommand(list);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateMilitaryObligation([FromBody] MilitaryObligationDto data)
        {
            var command = new UpdateMilitaryObligationCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveMilitaryObligation([FromRoute] int id)
        {
            var command = new RemoveMilitaryObligationCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
