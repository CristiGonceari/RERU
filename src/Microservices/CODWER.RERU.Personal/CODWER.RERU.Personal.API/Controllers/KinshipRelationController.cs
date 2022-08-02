using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.KinshipRelations.AddKinshipRelation;
using CODWER.RERU.Personal.Application.KinshipRelations.BulkAddEditKinshipRelations;
using CODWER.RERU.Personal.Application.KinshipRelations.GetContractorKinshipRelations;
using CODWER.RERU.Personal.Application.KinshipRelations.RemoveKinshipRelation;
using CODWER.RERU.Personal.Application.KinshipRelations.UpdateKinshipRelation;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<KinshipRelationDto>> GetKinshipRelation([FromQuery] GetContractorKinshipRelationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateKinshipRelation([FromBody] KinshipRelationDto dto)
        {
            var command = new AddKinshipRelationCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("bulk-import")]
        public async Task<Unit> AddEditKinshipRelations([FromBody] List<KinshipRelationDto> dtoList)
        {
            var command = new BulkAddEditKinshipRelationsCommand(dtoList);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateKinshipRelation([FromBody] KinshipRelationDto dto)
        {
            var command = new UpdateKinshipRelationCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveKinshipRelation([FromRoute] int id)
        {
            var command = new RemoveKinshipRelationCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
