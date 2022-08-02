using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.MilitaryObligations.AddMilitaryObligation;
using CODWER.RERU.Personal.Application.MilitaryObligations.BulkAddEditMilitaryObligations;
using CODWER.RERU.Personal.Application.MilitaryObligations.GetContractorMilitaryObligations;
using CODWER.RERU.Personal.Application.MilitaryObligations.RemoveMilitaryObligation;
using CODWER.RERU.Personal.Application.MilitaryObligations.UpdateMilitaryObligation;
using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilitaryObligationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<MilitaryObligationDto>> GetMilitaryObligation([FromQuery] GetContractorMilitaryObligationsQuery query)
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
